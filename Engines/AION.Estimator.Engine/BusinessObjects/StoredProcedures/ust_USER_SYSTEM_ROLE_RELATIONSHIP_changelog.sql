
SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

/******************************************************************************************************************
* Object:       ust_USER_SYSTEM_ROLE_RELATIONSHIP_changelog
* Description:  Records changed and new info in the change log (table_audit_log)
* Version:      1.0
* Created by:   J Lindsay
* Created:      03/06/2020
*******************************************************************************************************************
* Change History:
* 03/06/2020   jlindsay    create trigger
*
*******************************************************************************************************************/

ALTER TRIGGER [AION].[ust_USER_SYSTEM_ROLE_RELATIONSHIP_changelog] ON [AION].[USER_SYSTEM_ROLE_RELATIONSHIP]
AFTER INSERT, UPDATE, DELETE
AS
     BEGIN
         DECLARE @errno INT, @errmsg VARCHAR(255);
         DECLARE @action CHAR(1), @tablename VARCHAR(100)= 'USER_SYSTEM_ROLE_RELATIONSHIP';
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
         --
         --USER_SYSTEM_ROLE_RELATIONSHIP_ID	int	4
         --USER_ID	int	4
         --SYSTEM_ROLE_ID	int	4
         --WKR_ID_CREATED_TXT	varchar	10
         --CREATED_DTTM	datetime	8
         --WKR_ID_UPDATED_TXT	varchar	10
         --UPDATED_DTTM	datetime	8
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
                               USER_SYSTEM_ROLE_RELATIONSHIP_ID, 
                               '' AS AUDIT_FIELD_NM, 
                               '' AS OLD_VAL_TXT, 
                               '{"rowdatachanges": [
                                        {
                                            "fieldname": "USER_ID",
                                            "oldvalue": "",
                                            "newvalue": "' + ISNULL(CAST([USER_ID] AS VARCHAR), '') + '"
                                        },
                                        {
                                            "fieldname": "SYSTEM_ROLE_ID",
                                            "oldvalue": "",
                                            "newvalue": "' + ISNULL(CAST(SYSTEM_ROLE_ID AS VARCHAR), '') + '"
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
                               i.USER_SYSTEM_ROLE_RELATIONSHIP_ID, 
                               '' AS AUDIT_FIELD_NM, 
                               '' AS OLD_VAL_TXT, 
                               '{"rowdatachanges": [
                                        {
                                            "fieldname": "USER_ID",
                                            "oldvalue": "' + ISNULL(CAST(d.[USER_ID] AS VARCHAR), '') + '",
                                            "newvalue": "' + ISNULL(CAST(i.[USER_ID] AS VARCHAR), '') + '"
                                        },
                                        {
                                            "fieldname": "SYSTEM_ROLE_ID",
                                            "oldvalue": "' + ISNULL(CAST(d.SYSTEM_ROLE_ID AS VARCHAR), '') + '",
                                            "newvalue": "' + ISNULL(CAST(i.SYSTEM_ROLE_ID AS VARCHAR), '') + '"
                                        }
                                    ]
                                }' AS NEW_VAL_TXT, 
                               i.WKR_ID_UPDATED_TXT, 
                               i.UPDATED_DTTM
                        FROM inserted i
                             INNER JOIN deleted d ON i.USER_SYSTEM_ROLE_RELATIONSHIP_ID = d.USER_SYSTEM_ROLE_RELATIONSHIP_ID;
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
                               USER_SYSTEM_ROLE_RELATIONSHIP_ID, 
                               '' AS AUDIT_FIELD_NM, 
                               '' AS OLD_VAL_TXT, 
                               '{"rowdatachanges": [
                                        {
                                            "fieldname": "USER_ID",
                                            "oldvalue": "' + ISNULL(CAST([USER_ID] AS VARCHAR), '') + '",
                                            "newvalue": ""
                                        },
                                        {
                                            "fieldname": "SYSTEM_ROLE_ID",
                                            "oldvalue": "' + ISNULL(CAST(SYSTEM_ROLE_ID AS VARCHAR), '') + '",
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
ALTER TABLE [AION].[USER_SYSTEM_ROLE_RELATIONSHIP] ENABLE TRIGGER [ust_USER_SYSTEM_ROLE_RELATIONSHIP_changelog];
GO