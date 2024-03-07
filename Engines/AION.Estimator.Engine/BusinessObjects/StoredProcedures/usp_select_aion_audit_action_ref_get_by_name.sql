
/***********************************************************************************************************************
* Object:       usp_select_aion_audit_action_ref_get_by_name
* Description:  Retrieves AuditActionRef record for given key field(s).
* Parameters:   
*               @AUDIT_ACTION_NM                                                    varchar(300)
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables,
*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause.
* Version:      1.0
* Created by:   AION_user
* Created:      2/28/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 2/28/2020    jlindsay     created
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_select_aion_audit_action_ref_get_by_name]

    @AUDIT_ACTION_NM                                                    int

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
     
          REPLACE(LOWER(AUDIT_ACTION_NM),' ','') = REPLACE(LOWER(@AUDIT_ACTION_NM),' ','')
          

RETURN

GO