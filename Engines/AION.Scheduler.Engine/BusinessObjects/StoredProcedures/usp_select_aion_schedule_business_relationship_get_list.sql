
/***********************************************************************************************************************
* Object:       usp_select_aion_schedule_business_relationship_get_list
* Description:  Retrieves ScheduleBusinessRelationship list for given parameter(s).
* Parameters:   
*               @identity                                                   int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval.
*               This proc expects id_person and/or id_file to generate list; modify as necessary.
*               Include ORDER BY clause as necessary.
* Version:      1.0
* Created by:   AION_user
* Created:      9/14/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 9/14/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_select_aion_schedule_business_relationship_get_list]

    @identity                                                   int

AS

       SELECT 
            SCHEDULE_BUSINESS_RELATIONSHIP_ID
          , PLAN_REVIEW_SCHEDULE_ID
          , BUSINESS_REF_ID
          , PROJECT_ID
          , REREVIEW_HOURS_NBR
          , CYCLE_NBR
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM

       FROM SCHEDULE_BUSINESS_RELATIONSHIP

       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          PLAN_REVIEW_SCHEDULE_ID = @identity
          

RETURN

GO