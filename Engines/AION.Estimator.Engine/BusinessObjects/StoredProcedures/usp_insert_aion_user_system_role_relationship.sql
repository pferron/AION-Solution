/****** Object:  StoredProcedure [AION].[usp_insert_aion_user_system_role_relationship]    Script Date: 12/10/2019 1:58:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************************************************************
* Object:	usp_insert_aion_user_system_role_relationship
* Description:	Inserts UserSystemRoleRelationship record.
* Parameters:
*		@USER_ID                                                     int
*		@SYSTEM_ROLE_ID                                              int
*		@WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Identity column of new record.
* Comments:     If CREATED_DTTM and/or UPDATED_DTTM fields exist in table, GETDATE() is inserted.
*               Worker ID will be inserted for WKR_ID_CREATED_TXT and WKR_ID_UPDATED_TXT fields if they exist.
* Version:      1.0
* Created by:   AION_user
* Created:      10/10/2019
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/10/2019    AION_user     Auto-generated
* 
***********************************************************************************************************************/

ALTER PROCEDURE [AION].[usp_insert_aion_user_system_role_relationship]
    @USER_ID                                                     int
  , @SYSTEM_ROLE_ID                                              int
  , @WKR_ID_TXT                                                  varchar(100)
  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

     INSERT INTO USER_SYSTEM_ROLE_RELATIONSHIP
          (
            USER_ID
          , SYSTEM_ROLE_ID
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          )
     VALUES
          (
            @USER_ID
          , @SYSTEM_ROLE_ID
          , @WKR_ID_TXT
          , GETDATE()
          , @WKR_ID_TXT
          , GETDATE()
          )

     SELECT @error = @@ERROR
          , @ReturnValue = SCOPE_IDENTITY()

     IF @error != 0
          RAISERROR('Error adding UserSystemRoleRelationship record.', 18,1)

RETURN
