/****** Object:  StoredProcedure [AION].[usp_select_aion_fifo_schedule_get_by_id]    Script Date: 6/1/2021 8:00:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  
/***********************************************************************************************************************  
* Object:       [usp_select_aion_fifo_schedule_get_by_id]  
* Description:  Retrieves fifoSchedule record for given key field(s).  
* Parameters:     
*               @identity                                                    int  
*  
* Returns:      Recordset.  
* Comments:     Developer may need to manually join to other tables, such as code tables,  
*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause.  
* Version:      1.0  
* Created by:   AION_user  
* Created:      8/12/2020  
************************************************************************************************************************  
* Change History: Date, Name, Description  
* 8/12/2020    AION_user     Auto-generated  

***********************************************************************************************************************/  
  
ALTER PROCEDURE [AION].[usp_select_aion_fifo_schedule_get_by_id]  
  
  @identity                                                    int

AS

       SELECT 
            FIFO_SCHEDULE_ID
          , PROJECT_ID
          , BUSINESS_REF_ID
          , APPT_RESPONSE_STATUS_REF_ID
          , CYCLE_NBR
          , FIFO_SAME_BUILD_CONTR_IND
          , FIFO_MANUAL_ASSIGNMENT_IND
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          , PLAN_REVIEW_START_DT
          , START_DT
          , END_DT
          , PLANS_READY_ON_DT
          , GATE_DT
          , ASSIGNED_PLAN_REVIEWER_ID
          , ASSIGNED_HOURS_NBR
          , APPT_CANCELLATION_REF_ID

       FROM FIFO_SCHEDULE

       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          FIFO_SCHEDULE_ID = @identity
          

RETURN


