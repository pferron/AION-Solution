
SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

/******************************************************************************************************************
* Object:       ust_PLAN_REVIEWER_AVAILABLE_HOURS_changelog
* Description:  Records changed and new info in the change log (table_audit_log)
* Version:      1.0
* Created by:   J Lindsay
* Created:      02/15/2021
*******************************************************************************************************************
* Change History:
* 02/15/2021    jlindsay    create trigger
*******************************************************************************************************************/

CREATE TRIGGER [AION].[ust_PLAN_REVIEWER_AVAILABLE_HOURS_changelog] ON [AION].[PLAN_REVIEWER_AVAILABLE_HOURS]
AFTER INSERT, UPDATE, DELETE
AS
     BEGIN
         DECLARE @errno INT, @errmsg VARCHAR(255);
         DECLARE @action CHAR(1), @tablename VARCHAR(100)= 'PLAN_REVIEWER_AVAILABLE_HOURS';
         IF EXISTS
         (
             SELECT *
             FROM INSERTED
         )
             BEGIN
                 IF EXISTS
                 (
                     SELECT *
                     FROM DELETED
                 )
                     SET @action = 'U'  -- update;
                     ELSE
                     SET @action = 'I';  --insert
         END;
             ELSE
             IF EXISTS
             (
                 SELECT *
                 FROM DELETED
             )
                 SET @action = 'D'  -- delete;
                 ELSE
                 RETURN; --no records affected
         --
         --insert values for inserted records
         --select correct values into the table
         --old value is blank because this in an insert
          --PLAN_REVIEWER_AVAILABLE_HOURS_ID                               = @PLAN_REVIEWER_AVAILABLE_HOURS_ID         
          --  AVAILABLE_HOURS_NBR                                          = @AVAILABLE_HOURS_NBR  
          --, WKR_ID_UPDATED_TXT                                           = @WKR_ID_TXT  
          --, UPDATED_DTTM                                                 = GETDATE()  

         IF(@action = 'I')
             BEGIN
                 INSERT INTO [AION].[TABLE_AUDIT_LOG]
                 ([AUDIT_TYP_CD], 
                  [AUDIT_TABLE_NM], 
                  [AUDIT_TABLE_PK_ID], 
                  [AUDIT_FIELD_NM], 
                  [OLD_VAL_TXT], 
                  [NEW_VAL_TXT], 
                  [WKR_ID_UPDATED_TXT], 
                  [UPDATED_DTTM]
                 )
                        SELECT @action, 
                               @tablename, 
                               PLAN_REVIEWER_AVAILABLE_HOURS_ID, 
                               '' AS AUDIT_FIELD_NM, 
                               '' AS OLD_VAL_TXT, 
                               '{"rowdatachanges": [
                                        {
                                            "fieldname": "AVAILABLE_HOURS_NBR",
                                            "fieldnamedesc": "Plan Reviewer Hours Available by Day",
                                            "oldvalue": "",
                                            "newvalue": "' + ISNULL(CAST(AVAILABLE_HOURS_NBR AS VARCHAR), '') + '"
                                        }
                                    ]
                                }' AS NEW_VAL_TXT, 
                               WKR_ID_CREATED_TXT, 
                               CREATED_DTTM
                        FROM inserted;
                 --get out of the function
                 RETURN;
         END;
         --insert values for the updated records
         --select 
         IF(@action = 'U')
             BEGIN
                 INSERT INTO [AION].[TABLE_AUDIT_LOG]
                 ([AUDIT_TYP_CD], 
                  [AUDIT_TABLE_NM], 
                  [AUDIT_TABLE_PK_ID], 
                  [AUDIT_FIELD_NM], 
                  [OLD_VAL_TXT], 
                  [NEW_VAL_TXT], 
                  [WKR_ID_UPDATED_TXT], 
                  [UPDATED_DTTM]
                 )
                        SELECT @action, 
                               @tablename, 
                               i.PLAN_REVIEWER_AVAILABLE_HOURS_ID, 
                               '' AS AUDIT_FIELD_NM, 
                               '' AS OLD_VAL_TXT, 
                               '{"rowdatachanges": [
                                        {
                                            "fieldname": "AVAILABLE_HOURS_NBR",
                                            "fieldnamedesc": "Plan Reviewer Hours Available by Day",
                                            "oldvalue": "' + ISNULL(CAST(d.AVAILABLE_HOURS_NBR AS VARCHAR), '') + '",
                                            "newvalue": "' + ISNULL(CAST(i.AVAILABLE_HOURS_NBR AS VARCHAR), '') + '"
                                        }
                                    ]
                                }' AS NEW_VAL_TXT, 
                               i.WKR_ID_UPDATED_TXT, 
                               i.UPDATED_DTTM
                        FROM inserted i
                             INNER JOIN deleted d ON i.PLAN_REVIEWER_AVAILABLE_HOURS_ID = d.PLAN_REVIEWER_AVAILABLE_HOURS_ID
                        WHERE d.AVAILABLE_HOURS_NBR != i.AVAILABLE_HOURS_NBR
                 RETURN;
         END;
         --insert values for the deleted records
         --select 
         IF(@action = 'D')
             BEGIN
                 INSERT INTO [AION].[TABLE_AUDIT_LOG]
                 ([AUDIT_TYP_CD], 
                  [AUDIT_TABLE_NM], 
                  [AUDIT_TABLE_PK_ID], 
                  [AUDIT_FIELD_NM], 
                  [OLD_VAL_TXT], 
                  [NEW_VAL_TXT], 
                  [WKR_ID_UPDATED_TXT], 
                  [UPDATED_DTTM]
                 )
                        SELECT @action, 
                               @tablename, 
                               PLAN_REVIEWER_AVAILABLE_HOURS_ID, 
                               '' AS AUDIT_FIELD_NM, 
                               '' AS OLD_VAL_TXT, 
                               '{"rowdatachanges": [
                                        {
                                            "fieldname": "AVAILABLE_HOURS_NBR",
                                            "fieldnamedesc": "Plan Reviewer Hours Available by Day",
                                            "oldvalue": "' + ISNULL(CAST(AVAILABLE_HOURS_NBR AS VARCHAR), '') + '",
                                            "newvalue": ""
                                        }
                                    ]
                                }' AS NEW_VAL_TXT, 
                               WKR_ID_UPDATED_TXT, 
                               GETDATE() AS UPDATED_DTTM
                        FROM deleted d;
                 RETURN;
         END;
         RETURN;
         error:
         RAISERROR(@errno, @errmsg, 1);
     END;
GO
ALTER TABLE [AION].[PLAN_REVIEWER_AVAILABLE_HOURS] ENABLE TRIGGER [ust_PLAN_REVIEWER_AVAILABLE_HOURS_changelog];
GO