
SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

/******************************************************************************************************************
* Object:       ust_SYSTEM_ROLE_changelog
* Description:  Records changed and new info in the change log (table_audit_log)
* Version:      1.0
* Created by:   J Lindsay
* Created:      03/06/2020
*******************************************************************************************************************
* Change History:
* 03/06/2020   jlindsay    create trigger
*
*******************************************************************************************************************/

ALTER TRIGGER [AION].[ust_SYSTEM_ROLE_changelog] ON [AION].[SYSTEM_ROLE]
AFTER INSERT, UPDATE, DELETE
AS
     BEGIN
         DECLARE @errno INT, @errmsg VARCHAR(255);
         DECLARE @action CHAR(1), @tablename VARCHAR(100)= 'SYSTEM_ROLE';
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
         --SYSTEM_ROLE_ID	int	4
         --SYSTEM_ROLE_NM	varchar	100
         --EXTERNAL_SYSTEM_REF_ID	int	4
         --SRC_SYSTEM_VAL_TXT	varchar	255
         --ENUM_MAPPING_VAL_NBR	int	4
         --SYSTEM_ROLE_TXT	varchar	255
         --ROLE_OPTIONS_TXT	varchar	1000
         --ENABLED_IND	bit	1
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
                               SYSTEM_ROLE_ID, 
                               '' AS AUDIT_FIELD_NM, 
                               '' AS OLD_VAL_TXT, 
                               '{"rowdatachanges": [
                                        {
                                            "fieldname": "SYSTEM_ROLE_NM",
                                            "oldvalue": "",
                                            "newvalue": "' + ISNULL(CAST(SYSTEM_ROLE_NM AS VARCHAR), '') + '"
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
                                            "fieldname": "ENUM_MAPPING_VAL_NBR",
                                            "oldvalue": "",
                                            "newvalue": "' + ISNULL(CAST(ENUM_MAPPING_VAL_NBR AS VARCHAR), '') + '"
                                        },
                                        {
                                            "fieldname": "SYSTEM_ROLE_TXT",
                                            "oldvalue": "",
                                            "newvalue": "' + ISNULL(CAST(SYSTEM_ROLE_TXT AS VARCHAR), '') + '"
                                        },
                                        {
                                            "fieldname": "ROLE_OPTIONS_TXT",
                                            "oldvalue": "",
                                            "newvalue": "' + ISNULL(CAST(ROLE_OPTIONS_TXT AS VARCHAR), '') + '"
                                        },
                                        {
                                            "fieldname": "ENABLED_IND",
                                            "oldvalue": "",
                                            "newvalue": "' + ISNULL(CAST(ENABLED_IND AS VARCHAR), '') + '"
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
                               i.SYSTEM_ROLE_ID, 
                               '' AS AUDIT_FIELD_NM, 
                               '' AS OLD_VAL_TXT, 
                               '{"rowdatachanges": [
                                        {
                                            "fieldname": "SYSTEM_ROLE_NM",
                                            "oldvalue": "' + ISNULL(CAST(d.SYSTEM_ROLE_NM AS VARCHAR), '') + '",
                                            "newvalue": "' + ISNULL(CAST(i.SYSTEM_ROLE_NM AS VARCHAR), '') + '"
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
                                            "fieldname": "ENUM_MAPPING_VAL_NBR",
                                            "oldvalue": "' + ISNULL(CAST(d.ENUM_MAPPING_VAL_NBR AS VARCHAR), '') + '",
                                            "newvalue": "' + ISNULL(CAST(i.ENUM_MAPPING_VAL_NBR AS VARCHAR), '') + '"
                                        },
                                        {
                                            "fieldname": "SYSTEM_ROLE_TXT",
                                            "oldvalue": "' + ISNULL(CAST(d.SYSTEM_ROLE_TXT AS VARCHAR), '') + '",
                                            "newvalue": "' + ISNULL(CAST(i.SYSTEM_ROLE_TXT AS VARCHAR), '') + '"
                                        },
                                        {
                                            "fieldname": "ROLE_OPTIONS_TXT",
                                            "oldvalue": "' + ISNULL(CAST(d.ROLE_OPTIONS_TXT AS VARCHAR), '') + '",
                                            "newvalue": "' + ISNULL(CAST(i.ROLE_OPTIONS_TXT AS VARCHAR), '') + '"
                                        },
                                        {
                                            "fieldname": "ENABLED_IND",
                                            "oldvalue": "' + ISNULL(CAST(d.ENABLED_IND AS VARCHAR), '') + '",
                                            "newvalue": "' + ISNULL(CAST(i.ENABLED_IND AS VARCHAR), '') + '"
                                        }
                                    ]
                                }' AS NEW_VAL_TXT, 
                               i.WKR_ID_UPDATED_TXT, 
                               i.UPDATED_DTTM
                        FROM inserted i
                             INNER JOIN deleted d ON i.SYSTEM_ROLE_ID = d.SYSTEM_ROLE_ID;
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
                               SYSTEM_ROLE_ID, 
                               '' AS AUDIT_FIELD_NM, 
                               '' AS OLD_VAL_TXT, 
                               '{"rowdatachanges": [
                                        {
                                            "fieldname": "SYSTEM_ROLE_NM",
                                            "oldvalue": "' + ISNULL(CAST(SYSTEM_ROLE_NM AS VARCHAR), '') + '",
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
                                            "fieldname": "ENUM_MAPPING_VAL_NBR",
                                            "oldvalue": "' + ISNULL(CAST(ENUM_MAPPING_VAL_NBR AS VARCHAR), '') + '",
                                            "newvalue": ""
                                        },
                                        {
                                            "fieldname": "SYSTEM_ROLE_TXT",
                                            "oldvalue": "' + ISNULL(CAST(SYSTEM_ROLE_TXT AS VARCHAR), '') + '",
                                            "newvalue": ""
                                        },
                                        {
                                            "fieldname": "ROLE_OPTIONS_TXT",
                                            "oldvalue": "' + ISNULL(CAST(ROLE_OPTIONS_TXT AS VARCHAR), '') + '",
                                            "newvalue": ""
                                        },
                                        {
                                            "fieldname": "ENABLED_IND",
                                            "oldvalue": "' + ISNULL(CAST(ENABLED_IND AS VARCHAR), '') + '",
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
ALTER TABLE [AION].[SYSTEM_ROLE] ENABLE TRIGGER [ust_SYSTEM_ROLE_changelog];
GO