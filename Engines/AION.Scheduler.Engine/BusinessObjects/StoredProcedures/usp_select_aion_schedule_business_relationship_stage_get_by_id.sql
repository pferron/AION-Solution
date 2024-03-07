
/***********************************************************************************************************************
* Object:       usp_select_aion_schedule_business_relationship_stage_get_by_id
* Description:  Retrieves ScheduleBusinessRelationshipStage record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables,
*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause.
* Version:      1.0
* Created by:   AION_user
* Created:      12/17/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 12/17/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_select_aion_schedule_business_relationship_stage_get_by_id]

    @identity                                                    int

AS

       SELECT 
            SCHEDULE_BUSINESS_RELATIONSHIP_STAGE_ID
          , HOURS_NBR
          , CYCLE_NBR
          , BUSINESS_REF_ID
          , PROJECT_ID
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM

       FROM SCHEDULE_BUSINESS_RELATIONSHIP_STAGE

       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          SCHEDULE_BUSINESS_RELATIONSHIP_STAGE_ID = @identity
          

RETURN

GO