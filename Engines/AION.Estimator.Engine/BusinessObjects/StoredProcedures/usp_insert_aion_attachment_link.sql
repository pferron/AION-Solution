
/***********************************************************************************************************************
* Object:	usp_insert_aion_attachment_link
* Description:	Inserts AttachmentLink record.
* Parameters:
*		@LINK_TXT                                                    varchar(8000)
*		@NOTES_ID                                                    int
*		@TAG_CREATED_ID_TXT                                          varchar(10)
*		@TAG_CREATED_DTTM                                            datetime
*		@TAG_UPDATED_DTTM                                            datetime
*		@TAG_UPDATED_ID_TXT                                          varchar(10)
*		@ATTACHMENT_TYP_CD                                           varchar(20)
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

CREATE PROCEDURE [usp_insert_aion_attachment_link]
    @LINK_TXT                                                    varchar(8000)
  , @NOTES_ID                                                    int
  , @TAG_CREATED_ID_TXT                                          varchar(10)
  , @TAG_CREATED_DTTM                                            datetime
  , @TAG_UPDATED_DTTM                                            datetime
  , @TAG_UPDATED_ID_TXT                                          varchar(10)
  , @ATTACHMENT_TYP_CD                                           varchar(20)
  , @WKR_ID_TXT                                                  varchar(100)
  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

     INSERT INTO ATTACHMENT_LINK
          (
            LINK_TXT
          , NOTES_ID
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          , TAG_CREATED_ID_TXT
          , TAG_CREATED_DTTM
          , TAG_UPDATED_DTTM
          , TAG_UPDATED_ID_TXT
          , ATTACHMENT_TYP_CD
          )
     VALUES
          (
            @LINK_TXT
          , @NOTES_ID
          , @WKR_ID_TXT
          , GETDATE()
          , @WKR_ID_TXT
          , GETDATE()
          , @TAG_CREATED_ID_TXT
          , @TAG_CREATED_DTTM
          , @TAG_UPDATED_DTTM
          , @TAG_UPDATED_ID_TXT
          , @ATTACHMENT_TYP_CD
          )

     SELECT @error = @@ERROR
          , @ReturnValue = SCOPE_IDENTITY()

     IF @error != 0
          RAISERROR('Error adding AttachmentLink record.', 18,1)

RETURN
GO