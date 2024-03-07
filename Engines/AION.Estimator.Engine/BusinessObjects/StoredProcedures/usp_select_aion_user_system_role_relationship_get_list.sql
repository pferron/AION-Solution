/****** Object:  StoredProcedure [AION].[usp_select_aion_user_system_role_relationship_get_list]    Script Date: 12/10/2019 1:59:13 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************************************************************
* Object:       usp_select_aion_user_system_role_relationship_get_list
* Description:  Retrieves UserSystemRoleRelationship list for given parameter(s).
* Parameters:   
*               @identity                                                   int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval.
*               This proc expects id_person and/or id_file to generate list; modify as necessary.
*               Include ORDER BY clause as necessary.
* Version:      1.0
* Created by:   AION_user
* Created:      10/10/2019
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/10/2019    AION_user     Auto-generated
* 
***********************************************************************************************************************/

ALTER PROCEDURE [AION].[usp_select_aion_user_system_role_relationship_get_list]

    @identity                                                   int

AS

       SELECT 
            USER_SYSTEM_ROLE_RELATIONSHIP_ID
          , USER_ID
          , SYSTEM_ROLE_ID
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM

       FROM USER_SYSTEM_ROLE_RELATIONSHIP

       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          USER_SYSTEM_ROLE_RELATIONSHIP_ID = @identity
          

RETURN

