
/***********************************************************************************************************************
* Object:       usp_select_aion_system_role_get_by_id
* Description:  Retrieves SystemRole record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables,
*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause.
* Version:      1.0
* Created by:   AION_user
* Created:      10/3/2019
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/3/2019    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE usp_select_aion_getByExternalSystemRef

    @externalSystemID                                                    INT,
	@externalSystemRef                                                   VARCHAR(255)

AS

       SELECT 
            SYSTEM_ROLE_ID
          , SYSTEM_ROLE_NM
          , WORKER_CREATED_BY_ID_NUM
          , WORKER_CREATED_BY_TS
          , WORKER_UPDATED_BY_ID_NUM
          , WORKER_UPDATED_BY_TS

       FROM SYSTEM_ROLE

       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          EXTERNAL_SYSTEM_REF_ID = @externalSystemID
          AND SRC_SYSTEM_VALUE_TXT = @externalSystemRef

RETURN

GO