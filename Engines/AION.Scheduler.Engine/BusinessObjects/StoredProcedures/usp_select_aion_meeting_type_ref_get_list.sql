
/***********************************************************************************************************************
* Object:       usp_select_aion_meeting_type_ref_get_list
* Description:  Retrieves MeetingTypeRef list for given parameter(s).
* Parameters:   
*               @identity                                                   int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval.
*               This proc expects id_person and/or id_file to generate list; modify as necessary.
*               Include ORDER BY clause as necessary.
* Version:      1.0
* Created by:   AION_user
* Created:      10/7/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/7/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_select_aion_meeting_type_ref_get_list]

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

  

RETURN

GO