/***********************************************************************************************************************        
* Object:       usp_select_aion_accela_received_records_queue_for_processing        
* Description:  Retrieves ACCELA_RECEIVED_RECORD_QUEUE list        
* Parameters:   @PROCESS_STATUS_DESC VARCHAR(30)        
    @OFFSET_MINS INT  
*        
* Returns:      Recordset.        
* Comments:           
* Version:      1.0        
* Created by:           
* Created:             
************************************************************************************************************************        
* Change History: Date, Name, Description        
* 07/23/2021 jlindsay added comment block and formatting, added to solution, add % to filter    
* 09/29/2021 jlindsay add filter parameters @PROCESS_STATUS_DESC, @OFFSET_MINS INT  
* 05/04/2022 jallen  Do not select 'Received' records where the updated date is greater than 15 minutes from the created date  
* 05/18/2022 jlindsay fix date offset
***********************************************************************************************************************/
ALTER PROCEDURE [AION].[usp_select_aion_accela_received_records_queue_for_processing] @PROCESS_STATUS_DESC VARCHAR(30) = '',
	@OFFSET_MINS INT = 0
AS
BEGIN
	SELECT [ACCELA_RECEIVED_REC_QUEUE_ID],
		[REC_ID_NUM],
		[REC_TYP_DESC],
		[STATUS_DESC],
		[WORKSTEP_ID_NUM],
		[WORKFLOW_TASK_NM],
		[WORKFLOW_TASK_STATUS],
		[ESTIMATED_REREVIEW_HOURS_NBR],
		[RECEIVED_DT],
		[LAST_PROCESSING_DT],
		[PROCESS_STATUS_DESC],
		[WKR_ID_CREATED_TXT],
		[CREATED_DTTM],
		[WKR_ID_UPDATED_TXT],
		[UPDATED_DTTM]
	FROM [AION].[ACCELA_RECEIVED_RECORD_QUEUE]
	WHERE PROCESS_STATUS_DESC = CASE 
			WHEN isnull(@PROCESS_STATUS_DESC, '') = ''
				THEN 'Received'
			ELSE @PROCESS_STATUS_DESC
			END
		AND [REC_ID_NUM] NOT LIKE '%HIS%'
		AND [REC_ID_NUM] NOT LIKE '%Unit%'
		AND [REC_ID_NUM] NOT LIKE '%POST%'
		--datediff between now and updated dttm is more than or equal to offset mins  
		AND (
			--No offset  
			isnull(@OFFSET_MINS, 0) = 0
			--offset is set, filter rows by date  
			OR (datediff(MI, getdate(), updated_dttm) <= (@OFFSET_MINS * - 1))
			)
	ORDER BY RECEIVED_DT ASC;
END;
