
/***********************************************************************************************************************
* Object:       usp_update_aion_attachment_link
* Description:  Updates AttachmentLink record using supplied parameters.
* Parameters:   
*               @ATTACHMENT_LINK_ID                                          int
*               @LINK_TXT                                                    varchar(8000)
*               @NOTES_ID                                                    int
*               @UPDATED_DTTM                                                datetime
*               @TAG_CREATED_ID_TXT                                          varchar(10)
*               @TAG_CREATED_DTTM                                            datetime
*               @TAG_UPDATED_DTTM                                            datetime
*               @TAG_UPDATED_ID_TXT                                          varchar(10)
*               @ATTACHMENT_TYP_CD                                           varchar(20)
*               @WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Number of rows affected.
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.
* Version:      1.0
* Created by:   AION_user
* Created:      10/10/2019
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/10/2019    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_update_aion_attachment_link]

    @ATTACHMENT_LINK_ID                                          int
  , @LINK_TXT                                                    varchar(8000)
  , @NOTES_ID                                                    int
  , @UPDATED_DTTM                                                datetime
  , @TAG_CREATED_ID_TXT                                          varchar(10)
  , @TAG_CREATED_DTTM                                            datetime
  , @TAG_UPDATED_DTTM                                            datetime
  , @TAG_UPDATED_ID_TXT                                          varchar(10)
  , @ATTACHMENT_TYP_CD                                           varchar(20)
  , @WKR_ID_TXT                                                  varchar(100)

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       UPDATE ATTACHMENT_LINK
       SET
            LINK_TXT                                                     = @LINK_TXT
          , NOTES_ID                                                     = @NOTES_ID
          , WKR_ID_UPDATED_TXT                                           = @WKR_ID_TXT
          , UPDATED_DTTM                                                 = GETDATE()
          , TAG_CREATED_ID_TXT                                           = @TAG_CREATED_ID_TXT
          , TAG_CREATED_DTTM                                             = @TAG_CREATED_DTTM
          , TAG_UPDATED_DTTM                                             = @TAG_UPDATED_DTTM
          , TAG_UPDATED_ID_TXT                                           = @TAG_UPDATED_ID_TXT
          , ATTACHMENT_TYP_CD                                            = @ATTACHMENT_TYP_CD

       WHERE
          ATTACHMENT_LINK_ID                                             = @ATTACHMENT_LINK_ID       
       AND 
          ISNULL(CONVERT(varchar(19), UPDATED_DTTM, 120), '')             = ISNULL(CONVERT(varchar(19), @UPDATED_DTTM, 120), '')
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error updating AttachmentLink record.', 18,1)

     IF @ReturnValue = 0
          RAISERROR('Data was changed/deleted prior to update.', 18,100)

RETURN
GO