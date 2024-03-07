
/***********************************************************************************************************************
* Object:       usp_select_aion_schedule_business_relationship_get_by_id
* Description:  Retrieves ScheduleBusinessRelationship record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables,
*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause.
* Version:      1.0
* Created by:   AION_user
* Created:      9/14/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 9/14/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_select_aion_schedule_business_relationship_get_by_id]

    @identity                                                    int

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
        
          SCHEDULE_BUSINESS_RELATIONSHIP_ID = @identity
          

RETURN

GO