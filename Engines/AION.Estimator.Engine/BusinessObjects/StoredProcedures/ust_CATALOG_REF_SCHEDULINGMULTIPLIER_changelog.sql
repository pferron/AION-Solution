/****** Object:  Trigger [AION].[ust_CATALOG_REF_SCHEDULINGMULTIPLIER_changelog]    Script Date: 3/11/2021 7:47:37 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/******************************************************************************************************************
* Object:       ust_USER_changelog
* Description:  Records changed and new info in the change log (table_audit_log)
* Version:      1.0
* Created by:   J Lindsay
* Created:      03/06/2020
*******************************************************************************************************************
* Change History:
* 06/12/2020   gnadimpalli    create trigger
* 06/12/2020    gnadimpalli    only save I,D and U that has a changed field value
* 03/25/2021	jlindsay	add CATALOG_KEY_TXT,CATALOG_SUBKEY_TXT,CATALOG_GRP_REF_ID for parsing for audit messaging
*******************************************************************************************************************/
ALTER TRIGGER [AION].[ust_CATALOG_REF_SCHEDULINGMULTIPLIER_changelog] ON [AION].[CATALOG_REF]
AFTER UPDATE
AS
BEGIN
	DECLARE @errno INT,
		@errmsg VARCHAR(255);
	DECLARE @action CHAR(1),
		@tablename VARCHAR(100) = 'CATALOG_REF';

	BEGIN
		SET @action = 'U' -- update;
	END;

	--[CATALOG_REF_ID] [int] IDENTITY(1,1) NOT NULL,
	--[CATALOG_VAL_TXT] [varchar](500) NULL,
	--[CATALOG_KEY_TXT] [varchar](500) NULL,
	--[CATALOG_SUBKEY_TXT] [varchar](100) NULL,
	--[CATALOG_GRP_REF_ID] [int] NULL,
	--[WKR_ID_CREATED_TXT] [varchar](10) NULL,
	--[CREATED_DTTM] [datetime] NULL,
	--[WKR_ID_UPDATED_TXT] [varchar](10) NULL,
	--[UPDATED_DTTM] [datetime] NULL,
	--
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
			i.[CATALOG_REF_ID],
			i.CATALOG_SUBKEY_TXT,
			'' AS OLD_VAL_TXT,
			'{"rowdatachanges": [
									{
										"fieldname": "CATALOG_VAL_TXT",
										"fieldnamedesc": "Catalog Value",
										"oldvalue": "' + ISNULL(CAST(d.CATALOG_VAL_TXT AS VARCHAR(500)), '') + '",
										"newvalue": "' + ISNULL(CAST(i.CATALOG_VAL_TXT AS VARCHAR(500)), '') + '"
									} ,
									{
										"fieldname": "CATALOG_KEY_TXT",
										"fieldnamedesc": "Catalog Key",
										"oldvalue": "' + ISNULL(CAST(d.CATALOG_KEY_TXT AS VARCHAR(500)), '') + '",
										"newvalue": "' + ISNULL(CAST(i.CATALOG_KEY_TXT AS VARCHAR(500)), '') + '"
									} ,
									{
										"fieldname": "CATALOG_SUBKEY_TXT",
										"fieldnamedesc": "Catalog Sub Key",
										"oldvalue": "' + ISNULL(CAST(d.CATALOG_SUBKEY_TXT AS VARCHAR(500)), '') + '",
										"newvalue": "' + ISNULL(CAST(i.CATALOG_SUBKEY_TXT AS VARCHAR(500)), '') + '"
									} ,
									{
										"fieldname": "CATALOG_GRP_REF_ID",
										"fieldnamedesc": "Catalog Group Ref Id",
										"oldvalue": "' + ISNULL(CAST(d.CATALOG_GRP_REF_ID AS VARCHAR(500)), '') + '",
										"newvalue": "' + ISNULL(CAST(i.CATALOG_GRP_REF_ID AS VARCHAR(500)), '') + '"
									} 
								]
                                }' AS NEW_VAL_TXT,
			i.WKR_ID_UPDATED_TXT,
			i.UPDATED_DTTM
		FROM inserted i
		INNER JOIN deleted d ON i.[CATALOG_REF_ID] = d.[CATALOG_REF_ID]
		WHERE d.CATALOG_VAL_TXT != i.CATALOG_VAL_TXT

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
