SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

/***********************************************************************************************************************
* Object:       usp_delete_aion_user_system_role_relationship_base
* Description:  Deletes UserSystemRoleRelationship record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Number of rows affected.
* Version:      1.0
* Created by:   jlindsay
* Created:      03/06/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 03/06/2020    jlindsay     Auto-generated/created
* 
***********************************************************************************************************************/

CREATE PROCEDURE [AION].[usp_delete_aion_user_system_role_relationship_base] @identity    INT, 
                                                                            @ReturnValue INT OUTPUT
AS
     DECLARE @error INT;
     --
     DELETE FROM USER_SYSTEM_ROLE_RELATIONSHIP
     WHERE USER_SYSTEM_ROLE_RELATIONSHIP_ID = @identity;
     --
     SELECT @error = @@ERROR, 
            @ReturnValue = @@ROWCOUNT;
     --
     IF @error != 0
         RAISERROR('Error deleting UserSystemRoleRelationship record.', 18, 1);
     RETURN;