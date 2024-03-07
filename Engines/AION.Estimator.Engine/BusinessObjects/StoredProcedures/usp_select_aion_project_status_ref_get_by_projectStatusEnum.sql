
/***********************************************************************************************************************
* Object:       usp_select_aion_project_status_ref_get_by_id
* Description:  Retrieves ProjectStatusRef record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables,
*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause.
* Version:      1.0
* Created by:   AION_user
* Created:      10/2/2019
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/2/2019    AION_user     Auto-generated
* 10/11/2019	JL		Correct columns
***********************************************************************************************************************/

CREATE PROCEDURE [usp_select_aion_project_status_ref_get_by_projectStatusEnum]

    @projectStatusEnum                                                    int

AS

       SELECT 
             PROJECT_STATUS_REF_NM
          , PROJECT_STATUS_REF_DESC
          , PROJECT_STATUS_REF_ID
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          , EXTERNAL_SYSTEM_REF_ID
          , SRC_SYSTEM_VAL_TXT
          , ENUM_MAPPING_VAL_NBR

       FROM PROJECT_STATUS_REF

       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          ENUM_MAPPING_VAL_NBR = @projectStatusEnum
          

RETURN

GO