
/***********************************************************************************************************************
* Object:       usp_select_aion_user_project_type_ref_get_list
* Description:  Retrieves UserProjectTypeRef list for given parameter(s).
* Parameters:   
*               @identity                                                   int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval.
*               This proc expects id_person and/or id_file to generate list; modify as necessary.
*               Include ORDER BY clause as necessary.
* Version:      1.0
* Created by:   AION_user
* Created:      5/4/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 5/4/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

alter PROCEDURE [usp_select_aion_user_project_type_ref_get_list]

    @identity                                                   int

AS

       SELECT 
            USER_PROJECT_TYP_CROSS_REF_ID
          , PROJECT_TYP_REF_ID
          , USER_ID
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM

       FROM USER_PROJECT_TYPE_XREF

       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          [USER_ID] = @identity
          

RETURN

GO