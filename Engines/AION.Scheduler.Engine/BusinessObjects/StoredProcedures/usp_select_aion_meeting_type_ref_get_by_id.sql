
/***********************************************************************************************************************
* Object:       usp_select_aion_meeting_type_ref_get_by_id
* Description:  Retrieves MeetingTypeRef record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables,
*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause.
* Version:      1.0
* Created by:   AION_user
* Created:      10/7/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/7/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_select_aion_meeting_type_ref_get_by_id]

    @identity                                                    int

AS

       SELECT 
            MEETING_TYP_REF_ID
          , MEETING_TYP_DESC
          , ENUM_MAPPING_VAL_NBR
          , ACTIVE_IND
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM

       FROM MEETING_TYPE_REF

       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          MEETING_TYP_REF_ID = @identity
          

RETURN

GO