
/***********************************************************************************************************************
* Object:       usp_select_aion_appointment_cancellation_ref_get_by_id
* Description:  Retrieves AppointmentCancellationRef record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables,
*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause.
* Version:      1.0
* Created by:   AION_user
* Created:      4/5/2021
************************************************************************************************************************
* Change History: Date, Name, Description
* 4/5/2021    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_select_aion_appointment_cancellation_ref_get_by_id]

    @identity                                                    int

AS

       SELECT 
            APPT_CANCELLATION_REF_ID
          , CANCELLATION_DESC
          , ENUM_MAPPING_VAL_NBR
          , ACTIVE_IND
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM

       FROM APPOINTMENT_CANCELLATION_REF

       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          APPT_CANCELLATION_REF_ID = @identity
          

RETURN

GO