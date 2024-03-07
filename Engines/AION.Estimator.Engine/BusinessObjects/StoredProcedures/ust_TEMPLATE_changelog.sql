SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO

/******************************************************************************************************************
* Object:       ust_TEMPLATE_changelog
* Description:  Records changed and new info in the change log (table_audit_log)
* Version:      1.0
* Created by:   J Lindsay
* Created:      10/04/2021
*******************************************************************************************************************
* Change History:
* 10/04/2021    jlindsay    create trigger
*******************************************************************************************************************/
CREATE TRIGGER [AION].[ust_TEMPLATE_changelog] ON [AION].[TEMPLATE]
AFTER INSERT,
	UPDATE,
	DELETE
AS
BEGIN
	DECLARE @errno INT,
		@errmsg VARCHAR(255);
	DECLARE @action CHAR(1),
		@tablename VARCHAR(100) = 'TEMPLATE';

	IF EXISTS (
			SELECT *
			FROM INSERTED
			)
	BEGIN
		IF EXISTS (
				SELECT *
				FROM DELETED
				)
			SET @action = 'U' -- update;
		ELSE
			SET @action = 'I';--insert
	END;
	ELSE IF EXISTS (
			SELECT *
			FROM DELETED
			)
		SET @action = 'D' -- delete;
	ELSE
		RETURN;--no records affected
			--
			--insert values for inserted records
			--select correct values into the table
			--old value is blank because this in an insert
			--TEMPLATE_ID
			--TEMPLATE_NM
			--TEMPLATE_TYP_ID
			--TEMPLATE_TXT
			--ACTIVE_IND
			--ACTIVE_DT

	IF (@action = 'I')
	BEGIN
		INSERT INTO [AION].[TABLE_AUDIT_LOG] (
			[AUDIT_TYP_CD],
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
			TEMPLATE_ID,
			'' AS AUDIT_FIELD_NM,
			'' AS OLD_VAL_TXT,
			'{"rowdatachanges": [
                                        {
                                            "fieldname": "TEMPLATE_NM",
                                            "fieldnamedesc": "Template Name",
                                            "oldvalue": "",
                                            "newvalue": "' + ISNULL(CAST(TEMPLATE_NM AS VARCHAR), '') + '"
                                        },
                                        {
                                            "fieldname": "TEMPLATE_TYP_ID",
                                            "fieldnamedesc": "Template Type",
                                            "oldvalue": "",
                                            "newvalue": "' + ISNULL(CAST(TEMPLATE_TYP_ID AS VARCHAR), '') + 
			'"
                                        },
                                        {
                                            "fieldname": "ACTIVE_IND",
                                            "fieldnamedesc": "Active",
                                            "oldvalue": "",
                                            "newvalue": "' + ISNULL(CAST(ACTIVE_IND AS VARCHAR), '') + '"
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
	IF (@action = 'U')
	BEGIN
		INSERT INTO [AION].[TABLE_AUDIT_LOG] (
			[AUDIT_TYP_CD],
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
			i.TEMPLATE_ID,
			'' AS AUDIT_FIELD_NM,
			'' AS OLD_VAL_TXT,
			'{"rowdatachanges": [
                                        {
                                            "fieldname": "TEMPLATE_NM",
                                            "fieldnamedesc": "Template Name",
                                            "oldvalue": "' + ISNULL(CAST(d.TEMPLATE_NM AS VARCHAR), '') + '",
                                            "newvalue": "' + ISNULL(CAST(i.TEMPLATE_NM AS VARCHAR), '') + '"
                                        },
                                        {
                                            "fieldname": "TEMPLATE_TYP_ID",
                                            "fieldnamedesc": "Template Type",
                                            "oldvalue": "' + ISNULL(CAST(d.TEMPLATE_TYP_ID AS VARCHAR), '') + '",
                                            "newvalue": "' + ISNULL(CAST(i.TEMPLATE_TYP_ID AS VARCHAR), '') + 
			'"
                                        },
                                        {
                                            "fieldname": "ACTIVE_IND",
                                            "fieldnamedesc": "Active",
                                            "oldvalue": "' + ISNULL(CAST(d.ACTIVE_IND AS VARCHAR), '') + '",
                                            "newvalue": "' + ISNULL(CAST(i.ACTIVE_IND AS VARCHAR), '') + '"
                                        }

                                    ]
                                }' AS NEW_VAL_TXT,
			i.WKR_ID_UPDATED_TXT,
			i.UPDATED_DTTM
		FROM inserted i
		INNER JOIN deleted d ON i.TEMPLATE_ID = d.TEMPLATE_ID
		WHERE d.TEMPLATE_NM != i.TEMPLATE_NM
			OR d.TEMPLATE_TYP_ID != i.TEMPLATE_TYP_ID
			OR d.TEMPLATE_TXT != i.TEMPLATE_TXT
			OR d.ACTIVE_IND != i.ACTIVE_IND

		RETURN;
	END;

	--insert values for the deleted records
	--select 
	IF (@action = 'D')
	BEGIN
		INSERT INTO [AION].[TABLE_AUDIT_LOG] (
			[AUDIT_TYP_CD],
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
			TEMPLATE_ID,
			'' AS AUDIT_FIELD_NM,
			'' AS OLD_VAL_TXT,
			'{"rowdatachanges": [
                                        {
                                            "fieldname": "TEMPLATE_NM",
                                            "fieldnamedesc": "Template Name",
                                            "oldvalue": "' + ISNULL(CAST(TEMPLATE_NM AS VARCHAR), '') + '",
                                            "newvalue": ""
                                        },
                                        {
                                            "fieldname": "TEMPLATE_TYP_ID",
                                            "fieldnamedesc": "Template Type",
                                            "oldvalue": "' + ISNULL(CAST(TEMPLATE_TYP_ID AS VARCHAR), '') + 
			'",
                                            "newvalue": ""
                                        },
                                        {
                                            "fieldname": "ACTIVE_IND",
                                            "fieldnamedesc": "Active",
                                            "oldvalue": "' + ISNULL(CAST(ACTIVE_IND AS VARCHAR), '') + '",
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

	RAISERROR (
			@errno,
			@errmsg,
			1
			);
END;
GO

ALTER TABLE [AION].[TEMPLATE] ENABLE TRIGGER [ust_TEMPLATE_changelog];
GO


