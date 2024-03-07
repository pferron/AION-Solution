/***********************************************************************************************************************  
* Object:       usp_update_aion_plan_reviewer_available_hours  
* Description:  Updates PlanReviewerAvailableHours record using supplied parameters.  
* Parameters:     
*               @PLAN_REVIEWER_AVAILABLE_HOURS_ID                            int  
*               @AVAILABLE_HOURS_NBR                                         int  
*               @UPDATED_DTTM                                                datetime  
*               @ENUM_MAPPING_VAL_NBR                                        int  
*               @PLAN_REVIEWER_TYP_DESC                                      varchar(100)  
*               @WKR_ID_TXT                                                  varchar(100)  
*  
* Returns:      Number of rows affected.  
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.  
* Version:      1.0  
* Created by:   AION_user  
* Created:      3/18/2020  
************************************************************************************************************************  
* Change History: Date, Name, Description  
* 3/18/2020    AION_user     Auto-generated  
* 12/14/2022    jlindsay    update to use the project_type_ref table
***********************************************************************************************************************/
ALTER PROCEDURE [AION].[usp_update_aion_plan_reviewer_available_hours] (
	@PROJECT_TYP_REF_ID INT,
	@AVAILABLE_HOURS_NBR DECIMAL(10, 4),
	@UPDATED_DTTM DATETIME,
	@WKR_ID_TXT VARCHAR(100),
	@ReturnValue INT OUTPUT
	)
AS
BEGIN
	DECLARE @error INT

	UPDATE PROJECT_TYPE_REF
	SET AVAILABLE_HOURS_NBR = @AVAILABLE_HOURS_NBR,
		WKR_ID_UPDATED_TXT = @WKR_ID_TXT,
		UPDATED_DTTM = GETDATE()
	WHERE PROJECT_TYP_REF_ID = @PROJECT_TYP_REF_ID
		AND ISNULL(CONVERT(VARCHAR(19), UPDATED_DTTM, 120), '') = ISNULL(CONVERT(VARCHAR(19), @UPDATED_DTTM, 120), '')

	SELECT @error = @@ERROR,
		@ReturnValue = @@ROWCOUNT

	IF @error != 0
		RAISERROR (
				'Error updating PROJECT_TYPE_REF record.',
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
