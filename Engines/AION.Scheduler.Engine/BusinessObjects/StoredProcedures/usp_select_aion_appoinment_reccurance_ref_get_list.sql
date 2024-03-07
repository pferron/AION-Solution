
/***********************************************************************************************************************
* Object:       usp_select_aion_appoinment_reccurance_ref_get_list
* Description:  Retrieves AppoinmentReccuranceRef list for given parameter(s).
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

ALTER PROCEDURE [AION].[usp_select_aion_appoinment_reccurance_ref_get_list]

    @identity                                                   int = null

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
        
          @identity is null OR APPT_RECURRENCE_REF_ID = @identity
          

RETURN

GO