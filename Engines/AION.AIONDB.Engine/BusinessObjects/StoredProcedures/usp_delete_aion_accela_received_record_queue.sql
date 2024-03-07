/***********************************************************************************************************************    
* Object:       usp_delete_aion_accela_received_record_queue    
* Description:  deletes ACCELA_RECEIVED_RECORD_QUEUE by identity    
* Parameters:       

*    
* Returns:      Recordset.    
* Comments:       
* Version:      1.0    
* Created by:    jlindsay   
* Created:        7/23/2021 
************************************************************************************************************************    
* Change History: Date, Name, Description    
* 07/23/2021	jlindsay	created
***********************************************************************************************************************/
CREATE PROCEDURE [AION].[usp_delete_aion_accela_received_record_queue] @ACCELA_RECEIVED_REC_QUEUE_ID INT,
	@ReturnValue INT OUTPUT
AS
BEGIN
	DECLARE @error INT;

	DELETE
	FROM [AION].[ACCELA_RECEIVED_RECORD_QUEUE]
	WHERE [ACCELA_RECEIVED_REC_QUEUE_ID] = @ACCELA_RECEIVED_REC_QUEUE_ID;

	SELECT @error = @@ERROR,
		@ReturnValue = @@ROWCOUNT;

	IF @error != 0
		RAISERROR (
				'Error deleting ACCELA_RECEIVED_RECORD_QUEUE record.',
				18,
				1
				);
END;
