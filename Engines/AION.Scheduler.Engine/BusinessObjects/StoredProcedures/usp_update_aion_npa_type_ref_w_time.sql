/***********************************************************************************************************************  
* Object:       usp_update_aion_npa_type_ref_w_time  
* Description:  Updates NpaTypeRef record using supplied parameters.  
* Parameters:     
*               @NON_PROJECT_APPT_TYP_REF_ID                                 int  
*               @APPT_TYP_DESC                                               varchar(30)  
*               @ACTIVE_IND                                                  bit  
*               @UPDATED_DTTM                                                datetime  
*               @WKR_ID_TXT                                                  varchar(100)  
*				@TIME_ALLOCATION_TYP_REF_ID									int
*				APPT_TYP_DESC
* Returns:      Number of rows affected.  
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.  
* Version:      1.0  
* Created by:   AION_user  
* Created:      3/19/2020  
************************************************************************************************************************  
* Change History: Date, Name, Description  
* 3/19/2020    AION_user     Auto-generated  
* 12/7/2021 jlindsay create with time allocation for updating existing records  
***********************************************************************************************************************/
ALTER PROCEDURE [AION].[usp_update_aion_npa_type_ref_w_time] @NON_PROJECT_APPT_TYP_REF_ID INT,
	@ACTIVE_IND BIT,
	@UPDATED_DTTM DATETIME,
	@WKR_ID_TXT VARCHAR(100),
	@TIME_ALLOCATION_TYP_REF_ID INT,
	@APPT_TYP_DESC varchar(255),
	@ReturnValue INT OUTPUT
AS
BEGIN
	DECLARE @error INT

	UPDATE NON_PROJECT_APPOINTMENT_TYPE_REF
	SET ACTIVE_IND = @ACTIVE_IND,
		WKR_ID_UPDATED_TXT = @WKR_ID_TXT,
		UPDATED_DTTM = GETDATE(),
		TIME_ALLOCATION_TYP_REF_ID = @TIME_ALLOCATION_TYP_REF_ID,
		APPT_TYP_DESC = @APPT_TYP_DESC
	WHERE NON_PROJECT_APPT_TYP_REF_ID = @NON_PROJECT_APPT_TYP_REF_ID
		AND ISNULL(CONVERT(VARCHAR(19), UPDATED_DTTM, 120), '') = ISNULL(CONVERT(VARCHAR(19), @UPDATED_DTTM, 120), '')

	SELECT @error = @@ERROR,
		@ReturnValue = @@ROWCOUNT

	IF @error != 0
		RAISERROR (
				'Error updating NpaTypeRef record.',
				18,
				1
				)

	IF @ReturnValue = 0
		RAISERROR (
				'Data was changed/deleted prior to update.',
				18,
				100
				)

	RETURN
END
