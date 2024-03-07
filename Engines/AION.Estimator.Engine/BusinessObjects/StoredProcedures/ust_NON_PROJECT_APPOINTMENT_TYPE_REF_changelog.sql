SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO

/******************************************************************************************************************
* Object:       ust_NON_PROJECT_APPOINTMENT_TYPE_REF_changelog
* Description:  Records changed and new info in the change log (table_audit_log)
* Version:      1.0
* Created by:   J Lindsay
* Created:      10/04/2021
*******************************************************************************************************************
* Change History:
* 10/04/2021    jlindsay    create trigger
*******************************************************************************************************************/
ALTER TRIGGER [AION].[ust_NON_PROJECT_APPOINTMENT_TYPE_REF_changelog] ON [AION].[NON_PROJECT_APPOINTMENT_TYPE_REF]
AFTER INSERT,
	UPDATE,
	DELETE
AS
BEGIN
	DECLARE @errno INT,
		@errmsg VARCHAR(255);
	DECLARE @action CHAR(1),
		@tablename VARCHAR(100) = 'NON_PROJECT_APPOINTMENT_TYPE_REF';

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
			--  @NON_PROJECT_APPT_TYP_REF_ID                                 int
			--           @APPT_TYP_DESC                                               varchar(30)
			--, @ACTIVE_IND                                                  bit
			--, @UPDATED_DTTM                                                datetime
			--, @WKR_ID_TXT                                                  varchar(100)

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
			NON_PROJECT_APPT_TYP_REF_ID,
			'' AS AUDIT_FIELD_NM,
			'' AS OLD_VAL_TXT,
			'{"rowdatachanges": [
                                        {
                                            "fieldname": "APPT_TYP_DESC",
                                            "fieldnamedesc": "Non Project Appointment Type",
                                            "oldvalue": "",
                                            "newvalue": "' + ISNULL(CAST(APPT_TYP_DESC AS VARCHAR), '') + '"
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
			i.NON_PROJECT_APPT_TYP_REF_ID,
			'' AS AUDIT_FIELD_NM,
			'' AS OLD_VAL_TXT,
			'{"rowdatachanges": [
                                        {
                                            "fieldname": "APPT_TYP_DESC",
                                            "fieldnamedesc": "Non Project Appointment Type",
                                            "oldvalue": "' + ISNULL(CAST(d.APPT_TYP_DESC AS VARCHAR), '') + '",
                                            "newvalue": "' + ISNULL(CAST(i.APPT_TYP_DESC AS VARCHAR), '') + '"
                                        },
                                        {
                                            "fieldname": "ACTIVE_IND",
                                            "fieldnamedesc": "Active",
                                            "oldvalue": "' + ISNULL(CAST(d.ACTIVE_IND AS VARCHAR), '') + '",
                                            "newvalue": "' + ISNULL(CAST(i.ACTIVE_IND AS VARCHAR), '') + 
			'"
                                        }
                                    ]
                                }' AS NEW_VAL_TXT,
			i.WKR_ID_UPDATED_TXT,
			i.UPDATED_DTTM
		FROM inserted i
		INNER JOIN deleted d ON i.NON_PROJECT_APPT_TYP_REF_ID = d.NON_PROJECT_APPT_TYP_REF_ID
		WHERE d.APPT_TYP_DESC != i.APPT_TYP_DESC
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
			NON_PROJECT_APPT_TYP_REF_ID,
			'' AS AUDIT_FIELD_NM,
			'' AS OLD_VAL_TXT,
			'{"rowdatachanges": [
                                        {
                                            "fieldname": "APPT_TYP_DESC",
                                            "fieldnamedesc": "Non Project Appointment Type",
                                            "oldvalue": "' + ISNULL(CAST(APPT_TYP_DESC AS VARCHAR), '') + '",
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

ALTER TABLE [AION].[NON_PROJECT_APPOINTMENT_TYPE_REF] ENABLE TRIGGER [ust_NON_PROJECT_APPOINTMENT_TYPE_REF_changelog];
GO


