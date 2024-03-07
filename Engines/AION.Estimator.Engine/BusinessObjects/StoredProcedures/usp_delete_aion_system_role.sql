/***********************************************************************************************************************  
* Object:       usp_delete_aion_system_role  
* Description:  Deletes SystemRole record for given key field(s).  
* Parameters:     
*               @identity                                                    int  
*  
* Returns:      Number of rows affected.  
* Version:      1.0  
* Created by:   AION_user  
* Created:      10/3/2019  
************************************************************************************************************************  
* Change History: Date, Name, Description  
* 10/3/2019    AION_user     Auto-generated  
*   
***********************************************************************************************************************/
ALTER PROCEDURE [AION].[usp_delete_aion_system_role] (
	@identity INT,
	@ReturnValue INT OUTPUT
	)
AS
BEGIN
	DECLARE @error INT

	DELETE
	FROM AION.SYSTEM_ROLE
	WHERE SYSTEM_ROLE_ID = @identity

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
