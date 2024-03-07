
/***********************************************************************************************************************
* Object:	usp_insert_aion_project_type_ref
* Description:	Inserts ProjectTypeRef record.
* Parameters:
*		@PROJECT_TYP_REF_NM                                          varchar(100)
*		@PROJECT_TYP_REF_DISPLAY_NM                                  varchar(50)
*		@EXTERNAL_SYSTEM_REF_ID                                      int
*		@SRC_SYSTEM_VAL_TXT                                          varchar(255)
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

CREATE PROCEDURE [usp_insert_aion_project_type_ref]
    @PROJECT_TYP_REF_NM                                          varchar(100)
  , @PROJECT_TYP_REF_DISPLAY_NM                                  varchar(50)
  , @EXTERNAL_SYSTEM_REF_ID                                      int
  , @SRC_SYSTEM_VAL_TXT                                          varchar(255)
  , @WKR_ID_TXT                                                  varchar(100)
  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

     INSERT INTO PROJECT_TYPE_REF
          (
            PROJECT_TYP_REF_NM
          , PROJECT_TYP_REF_DISPLAY_NM
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          , EXTERNAL_SYSTEM_REF_ID
          , SRC_SYSTEM_VAL_TXT
          )
     VALUES
          (
            @PROJECT_TYP_REF_NM
          , @PROJECT_TYP_REF_DISPLAY_NM
          , @WKR_ID_TXT
          , GETDATE()
          , @WKR_ID_TXT
          , GETDATE()
          , @EXTERNAL_SYSTEM_REF_ID
          , @SRC_SYSTEM_VAL_TXT
          )

     SELECT @error = @@ERROR
          , @ReturnValue = SCOPE_IDENTITY()

     IF @error != 0
          RAISERROR('Error adding ProjectTypeRef record.', 18,1)

RETURN
GO