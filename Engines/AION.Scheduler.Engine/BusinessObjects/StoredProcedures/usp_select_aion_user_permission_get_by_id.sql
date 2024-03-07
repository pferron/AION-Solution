
/***********************************************************************************************************************
* Object:       usp_select_aion_user_permission_get_by_id
* Description:  Retrieves UserPermission record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables,
*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause.
* Version:      1.0
* Created by:   AION_user
* Created:      5/8/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 5/8/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_select_aion_user_permission_get_by_id]

    @identity                                                    int

AS

       SELECT 
            USER_ID
          , PERMISSION_REF_ID
          , USER_PERMISSION_CROSS_REF_ID
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM

       FROM USER_PERMISSION_XREF

       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          USER_PERMISSION_CROSS_REF_ID = @identity
          

RETURN

GO