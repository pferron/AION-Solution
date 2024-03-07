
/***********************************************************************************************************************
* Object:       usp_select_aion_permission_system_role_get_list
* Description:  Retrieves PermissionSystemRole list for given parameter(s).
* Parameters:   
*               @systemroleid                                                   int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval.
*               This proc expects id_person and/or id_file to generate list; modify as necessary.
*               Include ORDER BY clause as necessary.
* Version:      1.0
* Created by:   AION_user
* Created:      5/11/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 5/11/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_select_aion_permission_system_role_get_list]

    @systemroleid                                                   int

AS

       SELECT 
            PERMISSION_REF_ID
          , SYSTEM_ROLE_ID
          , PERMISSION_SYSTEM_ROLE_CROSS_REF_ID
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM

       FROM PERMISSION_SYSTEM_ROLE_XREF

       WHERE
        
          SYSTEM_ROLE_ID = @systemroleid
          

RETURN

GO