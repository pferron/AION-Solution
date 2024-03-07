
/***********************************************************************************************************************
* Object:       usp_update_aion_standard_note
* Description:  Updates StandardNote record using supplied parameters.
* Parameters:   
*               @STANDARD_NOTE_ID                                            int
*               @STANDARD_NOTE_GRP_NM                                        varchar(300)
*               @STANDARD_NOTE_TYP_REF_ID                                    int
*               @UPDATED_DTTM                                                datetime
*               @STANDARD_NOTE_TXT                                           varchar(8000)
*               @STANDARD_NOTE_TITLE_TXT                                     varchar(300)
*               @PROJECT_TYP_REF_ID                                          int
*               @WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Number of rows affected.
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.
* Version:      1.0
* Created by:   AION_user
* Created:      8/25/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 8/25/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

ALTER PROCEDURE [usp_update_aion_standard_note]

    @STANDARD_NOTE_ID                                            int
  , @STANDARD_NOTE_GRP_NM                                        varchar(300)
  , @STANDARD_NOTE_TYP_REF_ID                                    int
  , @UPDATED_DTTM                                                datetime
  , @STANDARD_NOTE_TXT                                           varchar(8000)
  , @STANDARD_NOTE_TITLE_TXT                                     varchar(300)
  , @PROJECT_TYP_REF_ID                                          int
  , @WKR_ID_TXT                                                  varchar(100)

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       UPDATE STANDARD_NOTE
       SET
            STANDARD_NOTE_GRP_NM                                         = @STANDARD_NOTE_GRP_NM
          , STANDARD_NOTE_TYP_REF_ID                                     = @STANDARD_NOTE_TYP_REF_ID
          , WKR_ID_UPDATED_TXT                                           = @WKR_ID_TXT
          , UPDATED_DTTM                                                 = GETDATE()
          , STANDARD_NOTE_TXT                                            = @STANDARD_NOTE_TXT
          , STANDARD_NOTE_TITLE_TXT                                      = @STANDARD_NOTE_TITLE_TXT
          , PROJECT_TYP_REF_ID                                           = @PROJECT_TYP_REF_ID

       WHERE
          STANDARD_NOTE_ID                                               = @STANDARD_NOTE_ID       
       AND 
          ISNULL(CONVERT(varchar(19), UPDATED_DTTM, 120), '')             = ISNULL(CONVERT(varchar(19), @UPDATED_DTTM, 120), '')
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error updating StandardNote record.', 18,1)

     IF @ReturnValue = 0
          RAISERROR('Data was changed/deleted prior to update.', 18,100)

RETURN
GO