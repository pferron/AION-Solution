
/***********************************************************************************************************************
* Object:       usp_select_aion_project_type_business_x_ref_get_list
* Description:  Retrieves ProjectTypeBusinessXRef list for given parameter(s).
* Parameters:   
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval.
*               This proc expects id_person and/or id_file to generate list; modify as necessary.
*               Include ORDER BY clause as necessary.
* Version:      1.0
* Created by:   AION_user
* Created:      12/18/2019
************************************************************************************************************************
* Change History: Date, Name, Description
* 12/18/2019    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_select_aion_project_type_business_x_ref_get_list]

AS

       SELECT 
            BUSINESS_REF_ID
          , PROJECT_TYP_REF_ID
          , PROJECT_TYP_BUSINESS_CROSS_REF_ID

       FROM PROJECT_TYPE_BUSINESS_XREF

          

RETURN

GO