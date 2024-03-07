/***********************************************************************************************************************
* Object:	usp_insert_aion_user_v2
* Description:	Inserts User record.
* Parameters:
*		@FIRST_NM                                                    varchar(100)
*		@LAST_NM                                                     varchar(100)
*		@EXTERNAL_SYSTEM_REF_ID                                      int
*		@SRC_SYSTEM_VAL_TXT                                          varchar(255)
*		@WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Identity column of new record.
* Comments:     If CREATED_DTTM and/or UPDATED_DTTM fields exist in table, GETDATE() is inserted.
*               Worker ID will be inserted for WKR_ID_CREATED_TXT and WKR_ID_UPDATED_TXT fields if they exist.
* Version:      1.0
* Created by:   AION_user
* Created:      10/10/2019
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/10/2019    AION_user     Auto-generated
* 11/20/2019    Shijo         Adding new colum to keep user active or inactive instead of deletion.  
* 04/13/2021    jallen        update Azure AD params @USER_PRINCIPAL_NAME and @CALENDAR_ID
* 05/10/2021    jallen        add @CITY_IND
***********************************************************************************************************************/
ALTER PROCEDURE [AION].[usp_insert_aion_user_v2] @FIRST_NM VARCHAR(100)
	,@LAST_NM VARCHAR(100)
	,@EXTERNAL_SYSTEM_REF_ID INT
	,@SRC_SYSTEM_VAL_TXT VARCHAR(255)
	,@ACTIVE_IND BIT
	,@USER_INTERFACE_SETTING_TXT VARCHAR(8000)
	,@IS_EXPRESS_SCHEDULED_IND BIT
	,@USER_NM VARCHAR(100)
	,@LAN_ID_TXT VARCHAR(20)
	,@PHONE_NUM VARCHAR(20)
	,@EMAIL_ADDR_TXT VARCHAR(100)
	,@NOTES_TXT VARCHAR(8000)
	,@IS_SCHEDULABLE_IND BIT
	,@PLAN_REVIEW_OVERRIDE_HOURS_NBR DECIMAL
	,@HOURS_ESTIMATED_DESC VARCHAR(50)
	,@JURISDICTION_TYP_ID INT
	,@SCHEDULABLE_LVL_DESC VARCHAR(50)
	,@WKR_ID_TXT VARCHAR(100)
	,@IS_PRELIM_MEETING_ALLOWED_IND BIT
	,@USER_PRINCIPAL_NM VARCHAR(100)
	,@CALENDAR_ID VARCHAR(255)
	,@CITY_IND BIT
	,@ReturnValue INT OUTPUT
AS
DECLARE @error INT

INSERT INTO [AION].[USER] (
	FIRST_NM
	,LAST_NM
	,EXTERNAL_SYSTEM_REF_ID
	,SRC_SYSTEM_VAL_TXT
	,WKR_ID_CREATED_TXT
	,CREATED_DTTM
	,WKR_ID_UPDATED_TXT
	,UPDATED_DTTM
	,ACTIVE_IND
	,USER_INTERFACE_SETTING_TXT
	,IS_EXPRESS_SCHEDULED_IND
	,USER_NM
	,LAN_ID_TXT
	,PHONE_NUM
	,EMAIL_ADDR_TXT
	,NOTES_TXT
	,IS_SCHEDULABLE_IND
	,PLAN_REVIEW_OVERRIDE_HOURS_NBR
	,HOURS_ESTIMATED_DESC
	,JURISDICTION_TYP_ID
	,SCHEDULABLE_LVL_DESC
	,IS_PRELIM_MEETING_ALLOWED_IND
	,USER_PRINCIPAL_NM
	,CALENDAR_ID
	,CITY_IND
	)
VALUES (
	@FIRST_NM
	,@LAST_NM
	,@EXTERNAL_SYSTEM_REF_ID
	,@SRC_SYSTEM_VAL_TXT
	,@WKR_ID_TXT
	,GETDATE()
	,@WKR_ID_TXT
	,GETDATE()
	,@ACTIVE_IND
	,@USER_INTERFACE_SETTING_TXT
	,@IS_EXPRESS_SCHEDULED_IND
	,@USER_NM
	,@LAN_ID_TXT
	,@PHONE_NUM
	,@EMAIL_ADDR_TXT
	,@NOTES_TXT
	,@IS_SCHEDULABLE_IND
	,@PLAN_REVIEW_OVERRIDE_HOURS_NBR
	,@HOURS_ESTIMATED_DESC
	,@JURISDICTION_TYP_ID
	,@SCHEDULABLE_LVL_DESC
	,@IS_PRELIM_MEETING_ALLOWED_IND
	,@USER_PRINCIPAL_NM
	,@CALENDAR_ID
	,@CITY_IND
	)

SELECT @error = @@ERROR
	,@ReturnValue = SCOPE_IDENTITY()

IF @error != 0
	RAISERROR (
			'Error adding User record.'
			,18
			,1
			)

RETURN