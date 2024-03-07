/***********************************************************************************************************************          
* Object:       usp_update_aion_project_facilitator         
* Description:  Updates Project record using supplied parameters.          
* Parameters:             
*               @PROJECT_ID                                                  int        
*               @UPDATED_DTTM                                                datetime        
*               @ASSIGNED_FACILITATOR_ID                                       int        
*               @WKR_ID_TXT                                                  varchar(100)*     
*				@ReturnValue INT OUTPUT        
*          
* Returns:      Number of rows affected.          
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.          
* Version:      1.0          
* Created by:   jlindsay          
* Created:      09/27/2021         
************************************************************************************************************************          
* Change History: Date, Name, Description          
* 09/27/2021 jlindsay	create proc
***********************************************************************************************************************/
ALTER PROCEDURE [usp_update_aion_project_facilitator] @PROJECT_ID INT,
	@UPDATED_DTTM DATETIME,
	@ASSIGNED_FACILITATOR_ID INT,
	@WKR_ID_TXT VARCHAR(100),
	@ReturnValue INT OUTPUT
AS
BEGIN
	DECLARE @error INT

	UPDATE PROJECT
	SET WKR_ID_UPDATED_TXT = @WKR_ID_TXT,
		UPDATED_DTTM = GETDATE(),
		ASSIGNED_FACILITATOR_ID = @ASSIGNED_FACILITATOR_ID
	WHERE PROJECT_ID = @PROJECT_ID
		AND ISNULL(CONVERT(VARCHAR(19), UPDATED_DTTM, 120), '') = ISNULL(CONVERT(VARCHAR(19), @UPDATED_DTTM, 120), '')

	SELECT @error = @@ERROR,
		@ReturnValue = @@ROWCOUNT

	IF @error != 0
		RAISERROR (
				'Error updating Project record for Status.',
				18,
				1
				)

	IF @ReturnValue = 0
		RAISERROR (
				'Data was changed/deleted prior to update. Error updating Project record for Status.',
				18,
				100
				)

	RETURN
END
