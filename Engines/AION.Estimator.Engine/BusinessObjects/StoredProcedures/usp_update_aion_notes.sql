
/***********************************************************************************************************************
* Object:       usp_update_aion_notes
* Description:  Updates Notes record using supplied parameters.
* Parameters:   
*               @NOTES_ID                                                    int
*               @NOTES_COMMENT                                               varchar(8000)
*               @UPDATED_DTTM                                                datetime
*               @PROJECT_ID                                                  int
*               @NOTES_TYP_REF_ID                                            int
*               @WKR_ID_TXT                                                  varchar(100)
*       @PARENT_NOTES_ID                                             int
*       @BUSINESS_REF_ID                                             int
* Returns:      Number of rows affected.
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.
* Version:      1.0
* Created by:   AION_user
* Created:      10/10/2019
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/10/2019    AION_user     Auto-generated
* 1/31/2020     jeanine lindsay add parent notes id, business ref id
***********************************************************************************************************************/

CREATE PROCEDURE [usp_update_aion_notes]

    @NOTES_ID                                                    int
  , @NOTES_COMMENT                                               varchar(8000)
  , @UPDATED_DTTM                                                datetime
  , @PROJECT_ID                                                  int
  , @NOTES_TYP_REF_ID                                            int
  , @WKR_ID_TXT                                                  varchar(100)
  , @PARENT_NOTES_ID                                             int
  , @BUSINESS_REF_ID                                             int
  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       UPDATE NOTES
       SET
            NOTES_COMMENT                                                = @NOTES_COMMENT
          , WKR_ID_UPDATED_TXT                                           = @WKR_ID_TXT
          , UPDATED_DTTM                                                 = GETDATE()
          , PROJECT_ID                                                   = @PROJECT_ID
          , NOTES_TYP_REF_ID                                             = @NOTES_TYP_REF_ID
          , PARENT_NOTES_ID                                              = @PARENT_NOTES_ID
          , BUSINESS_REF_ID                                              = @BUSINESS_REF_ID
       WHERE
          NOTES_ID                                                       = @NOTES_ID       
       AND 
          ISNULL(CONVERT(varchar(19), UPDATED_DTTM, 120), '')             = ISNULL(CONVERT(varchar(19), @UPDATED_DTTM, 120), '')
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error updating Notes record.', 18,1)

     IF @ReturnValue = 0
          RAISERROR('Data was changed/deleted prior to update.', 18,100)

RETURN
GO