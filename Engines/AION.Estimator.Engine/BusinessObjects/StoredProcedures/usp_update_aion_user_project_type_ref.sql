
/***********************************************************************************************************************
* Object:       usp_update_aion_user_project_type_ref
* Description:  Updates UserProjectTypeRef record using supplied parameters.
* Parameters:   
*               @USER_PROJECT_TYP_CROSS_REF_ID                               int
*               @PROJECT_TYP_REF_ID                                          int
*               @USER_ID                                                     int
*               @UPDATED_DTTM                                                datetime
*               @WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Number of rows affected.
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.
* Version:      1.0
* Created by:   AION_user
* Created:      5/4/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 5/4/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_update_aion_user_project_type_ref]

    @USER_PROJECT_TYP_CROSS_REF_ID                               int
  , @PROJECT_TYP_REF_ID                                          int
  , @USER_ID                                                     int
  , @UPDATED_DTTM                                                datetime
  , @WKR_ID_TXT                                                  varchar(100)

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       UPDATE USER_PROJECT_TYPE_XREF
       SET
            PROJECT_TYP_REF_ID                                           = @PROJECT_TYP_REF_ID
          , USER_ID                                                      = @USER_ID
          , WKR_ID_UPDATED_TXT                                           = @WKR_ID_TXT
          , UPDATED_DTTM                                                 = GETDATE()

       WHERE
          USER_PROJECT_TYP_CROSS_REF_ID                                  = @USER_PROJECT_TYP_CROSS_REF_ID       
       AND 
          ISNULL(CONVERT(varchar(19), UPDATED_DTTM, 120), '')             = ISNULL(CONVERT(varchar(19), @UPDATED_DTTM, 120), '')
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error updating UserProjectTypeRef record.', 18,1)

     IF @ReturnValue = 0
          RAISERROR('Data was changed/deleted prior to update.', 18,100)

RETURN
GO