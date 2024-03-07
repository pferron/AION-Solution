
/***********************************************************************************************************************
* Object:       usp_select_aion_project_type_business_x_ref_get_by_id
* Description:  Retrieves ProjectTypeBusinessXRef record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables,
*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause.
* Version:      1.0
* Created by:   AION_user
* Created:      12/18/2019
************************************************************************************************************************
* Change History: Date, Name, Description
* 12/18/2019    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_select_aion_project_type_business_x_ref_get_by_id]

    @identity                                                    int

AS

       SELECT 
            BUSINESS_REF_ID
          , PROJECT_TYP_REF_ID
          , PROJECT_TYP_BUSINESS_CROSS_REF_ID

       FROM PROJECT_TYPE_BUSINESS_XREF

       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          PROJECT_TYP_BUSINESS_CROSS_REF_ID = @identity
          

RETURN

GO