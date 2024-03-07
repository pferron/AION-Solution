
/***********************************************************************************************************************
* Object:       usp_select_aion_user_schedule_get_list
* Description:  Retrieves UserSchedule list for given parameter(s).
* Parameters:   
*               @identity                                                   int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval.
*               This proc expects id_person and/or id_file to generate list; modify as necessary.
*               Include ORDER BY clause as necessary.
* Version:      1.0
* Created by:   AION_user
* Created:      3/19/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 3/19/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

ALTER PROCEDURE [AION].[usp_select_aion_non_project_appoinment_by_prj_sch_id]
 
    @projectScheduleID                                                   int

AS
       
       SELECT 
            NPA.NON_PROJECT_APPT_ID
          , NPA.APPT_NM
          , NPA.ALL_PLAN_REVIEWERS_IND
          , NPA.ALL_DAY_IND
          , NPA.APPT_FROM_DTTM
          , NPA.APPT_TO_DTTM
          , NPA.NON_PROJECT_APPT_TYP_REF_ID
          , NPA.MEETING_ROOM_REF_ID
          , NPA.WKR_ID_CREATED_TXT
          , NPA.CREATED_DTTM
          , NPA.WKR_ID_UPDATED_TXT
          , NPA.UPDATED_DTTM
          , NPA.APPT_RECURRENCE_REF_ID
          , NPA.ALL_BUILD_IND
          , NPA.ALL_ELCTR_IND
          , NPA.ALL_MECH_IND
          , NPA.ALL_PLUMB_IND
          , NPA.ALL_ZONING_IND
          , NPA.ALL_FIRE_IND
          , NPA.ALL_BACKFLOW_IND
          , NPA.ALL_EHS_FOOD_IND
          , NPA.ALL_EHS_POOL_IND
          , NPA.ALL_EHS_LODGE_IND
          , NPA.ALL_EHS_DAYCARE_IND
       
       from 
       
       [AION].[PROJECT_SCHEDULE] PS INNER JOIN [AION].[NON_PROJECT_APPOINTMENT] NPA on PS.APPT_ID = NPA.[NON_PROJECT_APPT_ID]

       where PROJECT_SCHEDULE_ID = @projectScheduleID AND [PROJECT_SCHEDULE_TYP_DESC] = 'NPA'

RETURN

GO