/****** Object:  StoredProcedure [AION].[usp_delete_aion_user_system_role_relationship]    Script Date: 12/10/2019 1:58:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************************************************************
* Object:       usp_delete_aion_user_system_role_relationship
* Description:  Deletes UserSystemRoleRelationship record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Number of rows affected.
* Version:      1.0
* Created by:   AION_user
* Created:      10/3/2019
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/3/2019    AION_user     Auto-generated
* 
***********************************************************************************************************************/

ALTER PROCEDURE [AION].[usp_delete_aion_user_system_role_relationship]

    @identity                                                    int

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       DELETE FROM USER_SYSTEM_ROLE_RELATIONSHIP
       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          USER_SYSTEM_ROLE_RELATIONSHIP_ID = @identity
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error deleting UserSystemRoleRelationship record.', 18,1)

RETURN

