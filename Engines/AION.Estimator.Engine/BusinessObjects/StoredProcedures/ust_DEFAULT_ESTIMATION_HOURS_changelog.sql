
SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

/******************************************************************************************************************
* Object:       ust_DEFAULT_ESTIMATION_HOURS_changelog
* Description:  Records changed and new info in the change log (table_audit_log)
* Version:      1.0
* Created by:   J Lindsay
* Created:      03/06/2020
*******************************************************************************************************************
* Change History:
* 03/06/2020   jlindsay    create trigger
*
*******************************************************************************************************************/

ALTER TRIGGER [AION].[ust_DEFAULT_ESTIMATION_HOURS_changelog] ON [AION].[DEFAULT_ESTIMATION_HOURS]
AFTER INSERT, UPDATE, DELETE
AS
     BEGIN
         DECLARE @errno INT, @errmsg VARCHAR(255);
         DECLARE @action CHAR(1), @tablename VARCHAR(100)= 'DEFAULT_ESTIMATION_HOURS';
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
                               DEFAULT_ESTIMATION_HOURS_ID, 
                               '' AS AUDIT_FIELD_NM, 
                               '' AS OLD_VAL_TXT, 
                               '{"rowdatachanges": [
                                        {
                                            "fieldname": "DEFAULT_HOURS_NBR",
                                            "oldvalue": "",
                                            "newvalue": "' + ISNULL(CAST(DEFAULT_HOURS_NBR AS VARCHAR),'') + '"
                                        },
                                        {
                                            "fieldname": "BUSINESS_REF_ID",
                                            "oldvalue": "",
                                            "newvalue": "' + ISNULL(CAST(BUSINESS_REF_ID AS VARCHAR),'') + '"
                                        },
                                        {
                                            "fieldname": "PROJECT_TYP_REF_ID",
                                            "oldvalue": "",
                                            "newvalue": "' + ISNULL(CAST(PROJECT_TYP_REF_ID AS VARCHAR),'') + '"
                                        },
                                        {
                                            "fieldname": "ENABLED_IND",
                                            "oldvalue": "",
                                            "newvalue": "' + ISNULL(CAST(ENABLED_IND AS VARCHAR),'') + '"
                                        },
                                        {
                                            "fieldname": "ESTIMATION_HOURS_TXT",
                                            "oldvalue": "",
                                            "newvalue": "' + ISNULL(CAST(ESTIMATION_HOURS_TXT AS VARCHAR),'') + '"
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
                               i.DEFAULT_ESTIMATION_HOURS_ID, 
                               '' AS AUDIT_FIELD_NM, 
                               '' AS OLD_VAL_TXT, 
                               '{"rowdatachanges": [
                                        {
                                            "fieldname": "DEFAULT_HOURS_NBR",
                                            "oldvalue": "' + ISNULL(CAST(d.DEFAULT_HOURS_NBR AS VARCHAR),'') + '",
                                            "newvalue": "' + ISNULL(CAST(i.DEFAULT_HOURS_NBR AS VARCHAR),'') + '"
                                        },
                                        {
                                            "fieldname": "BUSINESS_REF_ID",
                                            "oldvalue": "' + ISNULL(CAST(d.BUSINESS_REF_ID AS VARCHAR),'') + '",
                                            "newvalue": "' + ISNULL(CAST(i.BUSINESS_REF_ID AS VARCHAR),'') + '"
                                        },
                                        {
                                            "fieldname": "PROJECT_TYP_REF_ID",
                                            "oldvalue": "' + ISNULL(CAST(d.PROJECT_TYP_REF_ID AS VARCHAR),'') + '",
                                            "newvalue": "' + ISNULL(CAST(i.PROJECT_TYP_REF_ID AS VARCHAR),'') + '"
                                        },
                                        {
                                            "fieldname": "ENABLED_IND",
                                            "oldvalue": "' + ISNULL(CAST(d.ENABLED_IND AS VARCHAR),'') + '",
                                            "newvalue": "' + ISNULL(CAST(i.ENABLED_IND AS VARCHAR),'') + '"
                                        },
                                        {
                                            "fieldname": "ESTIMATION_HOURS_TXT",
                                            "oldvalue": "' + ISNULL(CAST(d.ESTIMATION_HOURS_TXT AS VARCHAR),'') + '",
                                            "newvalue": "' + ISNULL(CAST(i.ESTIMATION_HOURS_TXT AS VARCHAR),'') + '"
                                        }
                                    ]
                                }' AS NEW_VAL_TXT, 
                               i.WKR_ID_UPDATED_TXT, 
                               i.UPDATED_DTTM
                        FROM inserted i
                             INNER JOIN deleted d ON i.DEFAULT_ESTIMATION_HOURS_ID = d.DEFAULT_ESTIMATION_HOURS_ID;
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
                               DEFAULT_ESTIMATION_HOURS_ID, 
                               '' AS AUDIT_FIELD_NM, 
                               '' AS OLD_VAL_TXT, 
                               '{"rowdatachanges": [
                                        {
                                            "fieldname": "DEFAULT_HOURS_NBR",
                                            "oldvalue": "' + ISNULL(CAST(DEFAULT_HOURS_NBR AS VARCHAR),'') + '",
                                            "newvalue": ""
                                        },
                                        {
                                            "fieldname": "BUSINESS_REF_ID",
                                            "oldvalue": "' + ISNULL(CAST(BUSINESS_REF_ID AS VARCHAR),'') + '",
                                            "newvalue": ""
                                        },
                                        {
                                            "fieldname": "PROJECT_TYP_REF_ID",
                                            "oldvalue": "' + ISNULL(CAST(PROJECT_TYP_REF_ID AS VARCHAR),'') + '",
                                            "newvalue": ""
                                        },
                                        {
                                            "fieldname": "ENABLED_IND",
                                            "oldvalue": "' + ISNULL(CAST(ENABLED_IND AS VARCHAR),'') + '",
                                            "newvalue": ""
                                        },
                                        {
                                            "fieldname": "ESTIMATION_HOURS_TXT",
                                            "oldvalue": "' + ISNULL(CAST(ESTIMATION_HOURS_TXT AS VARCHAR),'') + '",
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
ALTER TABLE [AION].[DEFAULT_ESTIMATION_HOURS] ENABLE TRIGGER [ust_DEFAULT_ESTIMATION_HOURS_changelog];
GO