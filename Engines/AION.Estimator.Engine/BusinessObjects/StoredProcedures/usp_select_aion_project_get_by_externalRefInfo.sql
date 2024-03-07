
/***********************************************************************************************************************
* Object:       usp_select_aion_project_get_by_externalRefInfo
* Description:  Retrieves Project record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables,
*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause.
* Version:      1.0
* Created by:   AION_user
* Created:      10/10/2019
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/10/2019    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_select_aion_project_get_by_externalRefInfo]

    @externalRefInfo                                                    VARCHAR(256)

AS

       SELECT 
            PROJECT_ID
          , PROJECT_NM
          , EXTERNAL_SYSTEM_REF_ID
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          , PROJECT_STATUS_REF_ID
          , PROJECT_TYP_REF_ID
          , SRC_SYSTEM_VAL_TXT
          , TAG_CREATED_ID_NUM
          , TAG_CREATED_BY_TS
          , TAG_UPDATED_BY_TS
          , TAG_UPDATED_BY_ID_NUM
          , ASSIGNED_ESTIMATOR_ID
          , ASSIGNED_FACILITATOR_ID
          , PROJECT_MODE_REF_ID

       FROM PROJECT

       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          SRC_SYSTEM_VAL_TXT = @externalRefInfo
          

RETURN

GO