/****** Object:  StoredProcedure [AION].[usp_select_aion_project_schedule_get_by_npaid]    Script Date: 5/12/2020 2:49:14 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************************************************************  
* Object:       usp_select_aion_project_schedule_get_by_schedule_details  
* Description:  Retrieves ProjectSchedule record for given key field(s).  
* Parameters:     
*               @npaid                                                    int  
*  
* Returns:      Recordset.  
* Comments:     Developer may need to manually join to other tables, such as code tables,  
*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause.  
* Version:      1.0  
* Created by:   j lindsay  
* Created:      3/26/2020  
************************************************************************************************************************  
* Change History: Date, Name, Description  
* 3/26/2020    jlindsay     get schedule by npaid 
* 5/12/2020    sjoseph      added params and renamed sproc
***********************************************************************************************************************/

ALTER PROCEDURE [AION].[usp_select_aion_project_schedule_get_by_schedule_details] 

@npaid INT,
@projectScheduleTypeDesc varchar(100),
@scheduleid int = null

AS
     SELECT PROJECT_SCHEDULE_ID, 
            PROJECT_SCHEDULE_TYP_DESC, 
            APPT_ID, 
            WKR_ID_CREATED_TXT, 
            CREATED_DTTM, 
            WKR_ID_UPDATED_TXT, 
            UPDATED_DTTM,
			RECURRING_APPT_DT
     FROM PROJECT_SCHEDULE
     WHERE APPT_ID = @npaid AND PROJECT_SCHEDULE_TYP_DESC = @projectScheduleTypeDesc AND 
	 (PROJECT_SCHEDULE_ID =  @scheduleid OR @scheduleid IS NULL);
     RETURN;