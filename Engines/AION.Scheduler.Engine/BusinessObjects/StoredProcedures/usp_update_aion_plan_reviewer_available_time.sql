/***********************************************************************************************************************  
* Object:       usp_update_aion_plan_reviewer_available_time  
* Description:  Updates PlanReviewerAvailableTime record using supplied parameters.  
* Parameters:     
*               @PLAN_REVIEWER_AVAILABLE_TM_ID                               int  
*               @AVAILABLE_START_TM                                          datetime  
*               @AVAILABLE_END_TM                                            datetime  
*               @PROJECT_TYP_DESC                                            varchar(100)  
*               @PROJECT_TYP_REF_ID                                          int  
*               @UPDATED_DTTM                                                datetime  
*               @WKR_ID_TXT                                                  varchar(100)  
*  
* Returns:      Number of rows affected.  
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.  
* Version:      1.0  
* Created by:   AION_user  
* Created:      8/28/2020  
************************************************************************************************************************  
* Change History: Date, Name, Description  
* 8/28/2020    AION_user     Auto-generated  
* 12/14/2022    jlindsay    update to use the project_type_ref table
***********************************************************************************************************************/
ALTER PROCEDURE [AION].[usp_update_aion_plan_reviewer_available_time] (
	@PROJECT_TYP_REF_ID INT,
	@AVAILABLE_START_TM DATETIME,
	@AVAILABLE_END_TM DATETIME,
	@UPDATED_DTTM DATETIME,
	@WKR_ID_TXT VARCHAR(100),
	@ReturnValue INT OUTPUT
	)
AS
BEGIN
	DECLARE @error INT

	UPDATE PROJECT_TYPE_REF
	SET AVAILABLE_START_TM = @AVAILABLE_START_TM,
		AVAILABLE_END_TM = @AVAILABLE_END_TM,
		WKR_ID_UPDATED_TXT = @WKR_ID_TXT,
		UPDATED_DTTM = GETDATE()
	WHERE PROJECT_TYP_REF_ID = @PROJECT_TYP_REF_ID
		AND ISNULL(CONVERT(VARCHAR(19), UPDATED_DTTM, 120), '') = ISNULL(CONVERT(VARCHAR(19), @UPDATED_DTTM, 120), '')

	SELECT @error = @@ERROR,
		@ReturnValue = @@ROWCOUNT

	IF @error != 0
		RAISERROR (
				'Error updating PROJECT_TYP_REF record.',
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
