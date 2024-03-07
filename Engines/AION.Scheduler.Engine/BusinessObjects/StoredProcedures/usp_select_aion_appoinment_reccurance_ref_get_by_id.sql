
/***********************************************************************************************************************
* Object:       usp_select_aion_appoinment_reccurance_ref_get_by_id
* Description:  Retrieves AppoinmentReccuranceRef record for given key field(s).
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

CREATE PROCEDURE [AION].[usp_select_aion_appoinment_reccurance_ref_get_by_id]

    @identity                                                    int

AS

       SELECT 
            APPT_RECURRENCE_REF_ID
          , RECURRENCE_WEEK_DESC
          , RECURRENCE_DAY_DESC
          , ENUM_MAPPING_VAL_NBR
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          , ACTIVE_IND

       FROM APPOINTMENT_RECURRENCE_REF

       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          APPT_RECURRENCE_REF_ID = @identity
          

RETURN

GO