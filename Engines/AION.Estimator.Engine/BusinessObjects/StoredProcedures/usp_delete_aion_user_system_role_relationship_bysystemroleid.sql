/***********************************************************************************************************************  
* Object:       usp_delete_aion_user_system_role_relationship_bysystemroleid  
* Description:  Deletes SystemRole record for given key field(s).  
* Parameters:     
*               @identity                                                    int  
*  
* Returns:      Number of rows affected.  
* Version:      1.0  
* Created by:   jlindsay  
* Created:      06/14/2022 
************************************************************************************************************************  
* Change History: Date, Name, Description  
* 06/14/2022    jlindsay     initial  
*   
***********************************************************************************************************************/
CREATE PROCEDURE [AION].[usp_delete_aion_user_system_role_relationship_bysystemroleid] (
	@identity INT,
	@ReturnValue INT OUTPUT
	)
AS
BEGIN
	DECLARE @error INT

	DELETE
	FROM [AION].[USER_SYSTEM_ROLE_RELATIONSHIP]
	WHERE SYSTEM_ROLE_ID = @identity;

	SELECT @error = @@ERROR,
		@ReturnValue = @@ROWCOUNT

	IF @error != 0
		RAISERROR (
				'Error deleting SystemRole record.',
				18,
				1
				)

	RETURN
END
