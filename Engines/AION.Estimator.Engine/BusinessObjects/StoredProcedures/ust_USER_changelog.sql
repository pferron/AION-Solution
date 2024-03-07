
SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

/******************************************************************************************************************
* Object:       ust_USER_changelog
* Description:  Records changed and new info in the change log (table_audit_log)
* Version:      1.0
* Created by:   J Lindsay
* Created:      03/06/2020
*******************************************************************************************************************
* Change History:
* 03/06/2020   jlindsay    create trigger
*
*******************************************************************************************************************/

ALTER TRIGGER [AION].[ust_USER_changelog] ON [AION].[USER]
AFTER INSERT, UPDATE, DELETE
AS
     BEGIN
         DECLARE @errno INT, @errmsg VARCHAR(255);
         DECLARE @action CHAR(1), @tablename VARCHAR(100)= 'USER';
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
         --USER_ID	int	4
         --FIRST_NM	varchar	100
         --LAST_NM	varchar	100
         --EXTERNAL_SYSTEM_REF_ID	int	4
         --SRC_SYSTEM_VAL_TXT	varchar	255
         --ACTIVE_IND	bit	1
         --USER_INTERFACE_SETTING_TXT	varchar	8000
         --WKR_ID_CREATED_TXT	varchar	10
         --CREATED_DTTM	datetime	8
         --WKR_ID_UPDATED_TXT	varchar	10
         --UPDATED_DTTM	datetime	8
         --
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
                               [USER_ID], 
                               '' AS AUDIT_FIELD_NM, 
                               '' AS OLD_VAL_TXT, 
                               '{"rowdatachanges": [
                                        {
                                            "fieldname": "FIRST_NM",
                                            "oldvalue": "",
                                            "newvalue": "' + ISNULL(CAST(FIRST_NM AS VARCHAR), '') + '"
                                        },
                                        {
                                            "fieldname": "LAST_NM",
                                            "oldvalue": "",
                                            "newvalue": "' + ISNULL(CAST(LAST_NM AS VARCHAR), '') + '"
                                        },
                                        {
                                            "fieldname": "EXTERNAL_SYSTEM_REF_ID",
                                            "oldvalue": "",
                                            "newvalue": "' + ISNULL(CAST(EXTERNAL_SYSTEM_REF_ID AS VARCHAR), '') + '"
                                        },
                                        {
                                            "fieldname": "SRC_SYSTEM_VAL_TXT",
                                            "oldvalue": "",
                                            "newvalue": "' + ISNULL(CAST(SRC_SYSTEM_VAL_TXT AS VARCHAR), '') + '"
                                        },
                                        {
                                            "fieldname": "ACTIVE_IND",
                                            "oldvalue": "",
                                            "newvalue": "' + ISNULL(CAST(ACTIVE_IND AS VARCHAR), '') + '"
                                        },
                                        {
                                            "fieldname": "USER_INTERFACE_SETTING_TXT",
                                            "oldvalue": "",
                                            "newvalue": "' + ISNULL(CAST(USER_INTERFACE_SETTING_TXT AS VARCHAR), '') + '"
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
                               i.[USER_ID], 
                               '' AS AUDIT_FIELD_NM, 
                               '' AS OLD_VAL_TXT, 
                               '{"rowdatachanges": [
                                        {
                                            "fieldname": "FIRST_NM",
                                            "oldvalue": "' + ISNULL(CAST(d.FIRST_NM AS VARCHAR), '') + '",
                                            "newvalue": "' + ISNULL(CAST(i.FIRST_NM AS VARCHAR), '') + '"
                                        },
                                        {
                                            "fieldname": "LAST_NM",
                                            "oldvalue": "' + ISNULL(CAST(d.LAST_NM AS VARCHAR), '') + '",
                                            "newvalue": "' + ISNULL(CAST(i.LAST_NM AS VARCHAR), '') + '"
                                        },
                                        {
                                            "fieldname": "EXTERNAL_SYSTEM_REF_ID",
                                            "oldvalue": "' + ISNULL(CAST(d.EXTERNAL_SYSTEM_REF_ID AS VARCHAR), '') + '",
                                            "newvalue": "' + ISNULL(CAST(i.EXTERNAL_SYSTEM_REF_ID AS VARCHAR), '') + '"
                                        },
                                        {
                                            "fieldname": "SRC_SYSTEM_VAL_TXT",
                                            "oldvalue": "' + ISNULL(CAST(d.SRC_SYSTEM_VAL_TXT AS VARCHAR), '') + '",
                                            "newvalue": "' + ISNULL(CAST(i.SRC_SYSTEM_VAL_TXT AS VARCHAR), '') + '"
                                        },
                                        {
                                            "fieldname": "ACTIVE_IND",
                                            "oldvalue": "' + ISNULL(CAST(d.ACTIVE_IND AS VARCHAR), '') + '",
                                            "newvalue": "' + ISNULL(CAST(i.ACTIVE_IND AS VARCHAR), '') + '"
                                        },
                                        {
                                            "fieldname": "USER_INTERFACE_SETTING_TXT",
                                            "oldvalue": "' + ISNULL(CAST(d.USER_INTERFACE_SETTING_TXT AS VARCHAR), '') + '",
                                            "newvalue": "' + ISNULL(CAST(i.USER_INTERFACE_SETTING_TXT AS VARCHAR), '') + '"
                                        }
                                    ]
                                }' AS NEW_VAL_TXT, 
                               i.WKR_ID_UPDATED_TXT, 
                               i.UPDATED_DTTM
                        FROM inserted i
                             INNER JOIN deleted d ON i.[USER_ID] = d.[USER_ID];
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
                               [USER_ID], 
                               '' AS AUDIT_FIELD_NM, 
                               '' AS OLD_VAL_TXT, 
                               '{"rowdatachanges": [
                                        {
                                            "fieldname": "FIRST_NM",
                                            "oldvalue": "' + ISNULL(CAST(FIRST_NM AS VARCHAR), '') + '",
                                            "newvalue": ""
                                        },
                                        {
                                            "fieldname": "LAST_NM",
                                            "oldvalue": "' + ISNULL(CAST(LAST_NM AS VARCHAR), '') + '",
                                            "newvalue": ""
                                        },
                                        {
                                            "fieldname": "EXTERNAL_SYSTEM_REF_ID",
                                            "oldvalue": "' + ISNULL(CAST(EXTERNAL_SYSTEM_REF_ID AS VARCHAR), '') + '",
                                            "newvalue": ""
                                        },
                                        {
                                            "fieldname": "SRC_SYSTEM_VAL_TXT",
                                            "oldvalue": "' + ISNULL(CAST(SRC_SYSTEM_VAL_TXT AS VARCHAR), '') + '",
                                            "newvalue": ""
                                        },
                                        {
                                            "fieldname": "ACTIVE_IND",
                                            "oldvalue": "' + ISNULL(CAST(ACTIVE_IND AS VARCHAR), '') + '",
                                            "newvalue": ""
                                        },
                                        {
                                            "fieldname": "USER_INTERFACE_SETTING_TXT",
                                            "oldvalue": "' + ISNULL(CAST(USER_INTERFACE_SETTING_TXT AS VARCHAR), '') + '",
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
ALTER TABLE [AION].[USER] ENABLE TRIGGER [ust_USER_changelog];
GO