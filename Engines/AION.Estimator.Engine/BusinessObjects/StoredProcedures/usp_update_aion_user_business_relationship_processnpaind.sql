/***********************************************************************************************************************  
* Object:       usp_update_aion_user_business_relationship_processnpaind  
* Description:  Updates UserBusinessRelationship record using supplied parameters.  
* Parameters:     
*               @USER_BUSINESS_RELATIONSHIP_ID                               int  
*               @USER_ID                                                     int  
*               @BUSINESS_REF_ID                                             int  
*               @UPDATED_DTTM                                                datetime  
*               @WKR_ID_TXT                                                  varchar(100)  
*  
* Returns:      Number of rows affected.  
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.  
* Version:      1.0  
* Created by:   AION_user  
* Created:      10/10/2019  
************************************************************************************************************************  
* Change History: Date, Name, Description  
* 06/08/2021	jcl			process_npa_ind  
*   
***********************************************************************************************************************/
CREATE PROCEDURE [usp_update_aion_user_business_relationship_processnpaind] @ReturnValue INT OUTPUT
AS
BEGIN
	DECLARE @error INT

	UPDATE USER_BUSINESS_RELATIONSHIP
	SET PROCESS_NPA_IND = 0
	WHERE PROCESS_NPA_IND = 1

	SELECT @error = @@ERROR,
		@ReturnValue = @@ROWCOUNT

	IF @error != 0
		RAISERROR (
				'Error updating UserBusinessRelationship record.',
				18,
				1
				)

	RETURN
END
