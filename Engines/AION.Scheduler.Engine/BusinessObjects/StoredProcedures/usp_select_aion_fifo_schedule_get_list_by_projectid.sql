/****** Object:  StoredProcedure [AION].[usp_select_aion_fifo_schedule_get_list_by_projectid]    Script Date: 6/1/2021 8:01:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************************************************************
* Object:       [usp_select_aion_fifo_schedule_get_list_by_projectid]
* Description:  Retrieves FIFOSchedule list for given parameter(s).
* Parameters:   
*               @identity                                                   int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval.
*               This proc expects id_person and/or id_file to generate list; modify as necessary.
*               Include ORDER BY clause as necessary.
* Version:      1.0
* Created by:   AION_user
* Created:      8/12/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 8/12/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

ALTER PROCEDURE [AION].[usp_select_aion_fifo_schedule_get_list_by_projectid]

    @project_id                                                   int

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
        
       
          PROJECT_ID = @project_id
          

RETURN


