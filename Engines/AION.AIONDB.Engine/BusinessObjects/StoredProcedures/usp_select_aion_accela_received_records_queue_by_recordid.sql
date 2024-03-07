SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************************************************************
* Object:	usp_select_aion_accela_received_records_queue_by_recordid
* Description:	Gets the latest queue records by REC_ID_NUM
* Parameters:
*		@REC_ID_NUM                                       varchar(30)
*
* Returns:      N/A
* Comments:     
* Version:      1.0
* Created by:   jallen
* Created:     07/12/2021
************************************************************************************************************************
* Change History: Date, Name, Description
* 07/12/2021   jallen     Added for FIFO Optimization purposes
* 
***********************************************************************************************************************/


ALTER procedure [AION].[usp_select_aion_accela_received_records_queue_by_recordid]
	( @REC_ID_NUM VARCHAR(30) )
 	 as
	  begin
SELECT [ACCELA_RECEIVED_REC_QUEUE_ID]
      ,[REC_ID_NUM]
      ,[REC_TYP_DESC]
      ,[STATUS_DESC]
      ,[WORKSTEP_ID_NUM]
      ,[WORKFLOW_TASK_NM]
	  ,[WORKFLOW_TASK_STATUS]
      ,[ESTIMATED_REREVIEW_HOURS_NBR] 
      ,[RECEIVED_DT]
      ,[LAST_PROCESSING_DT]
      ,[PROCESS_STATUS_DESC]
      ,[WKR_ID_CREATED_TXT]
      ,[CREATED_DTTM]
      ,[WKR_ID_UPDATED_TXT]
      ,[UPDATED_DTTM]
  FROM [AION].[ACCELA_RECEIVED_RECORD_QUEUE]
    where REC_ID_NUM = @REC_ID_NUM
	order by [RECEIVED_DT] desc
 end ; 