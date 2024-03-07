
/***********************************************************************************************************************
* Object:	usp_insert_aion_standard_note
* Description:	Inserts StandardNote record.
* Parameters:
*		@STANDARD_NOTE_GRP_NM                                        varchar(300)
*		@STANDARD_NOTE_TYP_REF_ID                                    int
*		@STANDARD_NOTE_TXT                                           varchar(8000)
*		@STANDARD_NOTE_TITLE_TXT                                     varchar(300)
*		@PROJECT_TYP_REF_ID                                          int
*		@WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Identity column of new record.
* Comments:     If CREATED_DTTM and/or UPDATED_DTTM fields exist in table, GETDATE() is inserted.
*               Worker ID will be inserted for WKR_ID_CREATED_TXT and WKR_ID_UPDATED_TXT fields if they exist.
* Version:      1.0
* Created by:   AION_user
* Created:      8/25/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 8/25/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

ALTER PROCEDURE [usp_insert_aion_standard_note]
    @STANDARD_NOTE_GRP_NM                                        varchar(300)
  , @STANDARD_NOTE_TYP_REF_ID                                    int
  , @STANDARD_NOTE_TXT                                           varchar(8000)
  , @STANDARD_NOTE_TITLE_TXT                                     varchar(300)
  , @PROJECT_TYP_REF_ID                                          int
  , @WKR_ID_TXT                                                  varchar(100)
  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

     INSERT INTO STANDARD_NOTE
          (
            STANDARD_NOTE_GRP_NM
          , STANDARD_NOTE_TYP_REF_ID
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          , STANDARD_NOTE_TXT
          , STANDARD_NOTE_TITLE_TXT
          , PROJECT_TYP_REF_ID
          )
     VALUES
          (
            @STANDARD_NOTE_GRP_NM
          , @STANDARD_NOTE_TYP_REF_ID
          , @WKR_ID_TXT
          , GETDATE()
          , @WKR_ID_TXT
          , GETDATE()
          , @STANDARD_NOTE_TXT
          , @STANDARD_NOTE_TITLE_TXT
          , @PROJECT_TYP_REF_ID
          )

     SELECT @error = @@ERROR
          , @ReturnValue = SCOPE_IDENTITY()

     IF @error != 0
          RAISERROR('Error adding StandardNote record.', 18,1)

RETURN
GO