
/***********************************************************************************************************************
* Object:	usp_insert_aion_user_project_type_ref
* Description:	Inserts UserProjectTypeRef record.
* Parameters:
*		@PROJECT_TYP_REF_ID                                          int
*		@USER_ID                                                     int
*		@WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Identity column of new record.
* Comments:     If CREATED_DTTM and/or UPDATED_DTTM fields exist in table, GETDATE() is inserted.
*               Worker ID will be inserted for WKR_ID_CREATED_TXT and WKR_ID_UPDATED_TXT fields if they exist.
* Version:      1.0
* Created by:   AION_user
* Created:      5/4/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 5/4/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_insert_aion_user_project_type_ref]
    @PROJECT_TYP_REF_ID                                          int
  , @USER_ID                                                     int
  , @WKR_ID_TXT                                                  varchar(100)
  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

     INSERT INTO USER_PROJECT_TYPE_XREF
          (
            PROJECT_TYP_REF_ID
          , USER_ID
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          )
     VALUES
          (
            @PROJECT_TYP_REF_ID
          , @USER_ID
          , @WKR_ID_TXT
          , GETDATE()
          , @WKR_ID_TXT
          , GETDATE()
          )

     SELECT @error = @@ERROR
          , @ReturnValue = SCOPE_IDENTITY()

     IF @error != 0
          RAISERROR('Error adding UserProjectTypeRef record.', 18,1)

RETURN
GO