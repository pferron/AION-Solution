
/***********************************************************************************************************************
* Object:       usp_select_aion_audit_action_ref_get_list
* Description:  Retrieves AuditActionRef list for given parameter(s).
* Parameters:   
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval.
*               This proc expects id_person and/or id_file to generate list; modify as necessary.
*               Include ORDER BY clause as necessary.
* Version:      1.0
* Created by:   AION_user
* Created:      2/27/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 2/27/2020    AION_user     Auto-generated
* 2/27/2020 jlindsay    remove id parameter
***********************************************************************************************************************/

ALTER PROCEDURE [usp_select_aion_audit_action_ref_get_list]


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

          

RETURN

GO