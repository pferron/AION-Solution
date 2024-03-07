
/***********************************************************************************************************************
* Object:       usp_select_aion_audit_action_ref_get_by_id
* Description:  Retrieves AuditActionRef record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables,
*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause.
* Version:      1.0
* Created by:   AION_user
* Created:      2/27/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 2/27/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_select_aion_audit_action_ref_get_by_id]

    @identity                                                    int

AS

       SELECT 
            AUDIT_ACTION_REF_ID
          , AUDIT_ACTION_NM
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          , AUDIT_ACTION_DESC

       FROM AUDIT_ACTION_REF

       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          AUDIT_ACTION_REF_ID = @identity
          

RETURN

GO