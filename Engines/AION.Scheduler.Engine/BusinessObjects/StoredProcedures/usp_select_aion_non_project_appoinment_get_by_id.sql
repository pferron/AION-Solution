
/***********************************************************************************************************************
* Object:       usp_select_aion_non_project_appoinment_get_by_id
* Description:  Retrieves NonProjectAppoinment record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables,
*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause.
* Version:      1.0
* Created by:   AION_user
* Created:      3/19/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 3/19/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [AION].[usp_select_aion_non_project_appoinment_get_by_id]

    @identity                                                    int

AS

       SELECT 
            NON_PROJECT_APPT_ID
          , APPT_NM
          , ALL_PLAN_REVIEWERS_IND
          , ALL_DAY_IND
          , APPT_FROM_DTTM
          , APPT_TO_DTTM
          , NON_PROJECT_APPT_TYP_REF_ID
          , MEETING_ROOM_REF_ID
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          , APPT_RECURRENCE_REF_ID
          , ALL_BUILD_IND
          , ALL_ELCTR_IND
          , ALL_MECH_IND
          , ALL_PLUMB_IND
          , ALL_ZONING_IND
          , ALL_FIRE_IND
          , ALL_BACKFLOW_IND
          , ALL_EHS_FOOD_IND
          , ALL_EHS_POOL_IND
          , ALL_EHS_LODGE_IND
          , ALL_EHS_DAYCARE_IND

       FROM NON_PROJECT_APPOINTMENT

       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          NON_PROJECT_APPT_ID = @identity
          

RETURN

GO