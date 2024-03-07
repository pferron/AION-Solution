
/***********************************************************************************************************************
* Object:       usp_update_aion_project_type_ref
* Description:  Updates ProjectTypeRef record using supplied parameters.
* Parameters:   
*               @PROJECT_TYP_REF_ID                                          int
*               @PROJECT_TYP_REF_NM                                          varchar(100)
*               @PROJECT_TYP_REF_DISPLAY_NM                                  varchar(50)
*               @UPDATED_DTTM                                                datetime
*               @EXTERNAL_SYSTEM_REF_ID                                      int
*               @SRC_SYSTEM_VAL_TXT                                          varchar(255)
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

CREATE PROCEDURE [usp_update_aion_project_type_ref]

    @PROJECT_TYP_REF_ID                                          int
  , @PROJECT_TYP_REF_NM                                          varchar(100)
  , @PROJECT_TYP_REF_DISPLAY_NM                                  varchar(50)
  , @UPDATED_DTTM                                                datetime
  , @EXTERNAL_SYSTEM_REF_ID                                      int
  , @SRC_SYSTEM_VAL_TXT                                          varchar(255)
  , @WKR_ID_TXT                                                  varchar(100)

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       UPDATE PROJECT_TYPE_REF
       SET
            PROJECT_TYP_REF_NM                                           = @PROJECT_TYP_REF_NM
          , PROJECT_TYP_REF_DISPLAY_NM                                   = @PROJECT_TYP_REF_DISPLAY_NM
          , WKR_ID_UPDATED_TXT                                           = @WKR_ID_TXT
          , UPDATED_DTTM                                                 = GETDATE()
          , EXTERNAL_SYSTEM_REF_ID                                       = @EXTERNAL_SYSTEM_REF_ID
          , SRC_SYSTEM_VAL_TXT                                           = @SRC_SYSTEM_VAL_TXT

       WHERE
          PROJECT_TYP_REF_ID                                             = @PROJECT_TYP_REF_ID       
       AND 
          ISNULL(CONVERT(varchar(19), UPDATED_DTTM, 120), '')             = ISNULL(CONVERT(varchar(19), @UPDATED_DTTM, 120), '')
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error updating ProjectTypeRef record.', 18,1)

     IF @ReturnValue = 0
          RAISERROR('Data was changed/deleted prior to update.', 18,100)

RETURN
GO