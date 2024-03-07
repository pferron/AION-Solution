/***********************************************************************************************************************  
* Object:       usp_update_aion_project_type_ref_autoassign_csvlist
* Description:  Updates ProjectTypeRef record using supplied parameters.  
* Parameters:     
*               @PROJECT_TYPE_REF_ID_CSV                                          VARCHAR(100)  
*               @AUTO_ASSIGN_FACILITATOR_IND                                          BIT 
*  
* Returns:      Number of rows affected.  
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.  
* Version:      1.0  
* Created by:   jlindsay  
* Created:      06/13/2022  
************************************************************************************************************************  
* Change History: Date, Name, Description  
* 06/13/2022    jlindsay     initial  
*   
***********************************************************************************************************************/
ALTER PROCEDURE AION.[usp_update_aion_project_type_ref_autoassign_csvlist] (
	@PROJECT_TYPE_REF_ID_CSV VARCHAR(100),
	@AUTO_ASSIGN_FACILITATOR_IND BIT,
	@WKR_ID_UPDATED_TXT VARCHAR(100),
	@ReturnValue INT OUTPUT
	)
AS
BEGIN
	DECLARE @error INT;

	WITH propertyTypes
	AS (
		SELECT [value] AS PROJECT_TYP_REF_ID
		FROM STRING_SPLIT(@PROJECT_TYPE_REF_ID_CSV, ',')
		WHERE [value] != ''
		)
	UPDATE AION.PROJECT_TYPE_REF
	SET UPDATED_DTTM = GETDATE(),
		WKR_ID_UPDATED_TXT = @WKR_ID_UPDATED_TXT,
		AUTO_ASSIGN_FACILITATOR_IND = @AUTO_ASSIGN_FACILITATOR_IND
	WHERE PROJECT_TYP_REF_ID IN (
			SELECT PROJECT_TYP_REF_ID
			FROM propertyTypes
			)

	SELECT @error = @@ERROR,
		@ReturnValue = @@ROWCOUNT

	IF @error != 0
		RAISERROR (
				'Error updating ProjectTypeRef record.',
				18,
				1
				)

	IF @ReturnValue = 0
		RAISERROR (
				'Error updating ProjectTypeRef record.',
				18,
				100
				)

	RETURN
END
