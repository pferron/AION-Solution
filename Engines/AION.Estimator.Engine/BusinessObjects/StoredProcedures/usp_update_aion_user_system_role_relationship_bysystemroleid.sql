/***********************************************************************************************************************  
* Object:       usp_update_aion_user_system_role_relationship_bysystemroleid 
* Description:  Updates UserSystemRoleRelationship record using supplied parameters.  
* Parameters:     
*               @SYSTEM_ROLE_ID                            int  
*               @NEW_SYSTEM_ROLE_ID                                                     int  
*  
* Returns:      Number of rows affected.  
* Comments:       
* Version:      1.0  
* Created by:   jlindsay  
* Created:      6-16-2022
************************************************************************************************************************  
* Change History: Date, Name, Description  
* 6-16-2022    jlindsay     initial 
*   
***********************************************************************************************************************/
CREATE PROCEDURE AION.[usp_update_aion_user_system_role_relationship_bysystemroleid] (
	@SYSTEM_ROLE_ID INT,
	@NEW_SYSTEM_ROLE_ID INT,
	@WKR_ID_TXT VARCHAR(100),
	@ReturnValue INT OUTPUT
	)
AS
BEGIN
	DECLARE @error INT;

	UPDATE AION.USER_SYSTEM_ROLE_RELATIONSHIP
	SET SYSTEM_ROLE_ID = @NEW_SYSTEM_ROLE_ID,
		WKR_ID_UPDATED_TXT = @WKR_ID_TXT,
		UPDATED_DTTM = GETDATE()
	WHERE SYSTEM_ROLE_ID = @SYSTEM_ROLE_ID;

	SELECT @error = @@ERROR,
		@ReturnValue = @@ROWCOUNT

	IF @error != 0
		RAISERROR (
				'Error updating UserSystemRoleRelationship record.',
				18,
				1
				)

	IF @ReturnValue = 0
		RAISERROR (
				'Error updating UserSystemRoleRelationship record.',
				18,
				100
				)

	RETURN
END
