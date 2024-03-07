
/***********************************************************************************************************************
* Object:       usp_update_aion_project_rtap_mapping
* Description:  Updates ProjectRtapMapping record using supplied parameters.
* Parameters:   
*               @PROJECT_RTAP_MAPPING_ID                                     int
*               @PROJECT_ID                                                  int
*               @ORIGINAL_PROJECT_ID                                         int
*               @UPDATED_DTTM                                                datetime
*               @WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Number of rows affected.
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.
* Version:      1.0
* Created by:   AION_user
* Created:      10/2/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/2/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_update_aion_project_rtap_mapping]

    @PROJECT_RTAP_MAPPING_ID                                     int
  , @PROJECT_ID                                                  int
  , @ORIGINAL_PROJECT_ID                                         int
  , @UPDATED_DTTM                                                datetime
  , @WKR_ID_TXT                                                  varchar(100)

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       UPDATE PROJECT_RTAP_MAPPING
       SET
            PROJECT_ID                                                   = @PROJECT_ID
          , ORIGINAL_PROJECT_ID                                          = @ORIGINAL_PROJECT_ID
          , WKR_ID_UPDATED_TXT                                           = @WKR_ID_TXT
          , UPDATED_DTTM                                                 = GETDATE()

       WHERE
          PROJECT_RTAP_MAPPING_ID                                        = @PROJECT_RTAP_MAPPING_ID       
       AND 
          ISNULL(CONVERT(varchar(19), UPDATED_DTTM, 120), '')             = ISNULL(CONVERT(varchar(19), @UPDATED_DTTM, 120), '')
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error updating ProjectRtapMapping record.', 18,1)

     IF @ReturnValue = 0
          RAISERROR('Data was changed/deleted prior to update.', 18,100)

RETURN
GO