
/***********************************************************************************************************************
* Object:       usp_update_aion_user_permission
* Description:  Updates UserPermission record using supplied parameters.
* Parameters:   
*               @USER_ID                                                     int
*               @PERMISSION_REF_ID                                           int
*               @USER_PERMISSION_CROSS_REF_ID                                int
*               @UPDATED_DTTM                                                datetime
*               @WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Number of rows affected.
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.
* Version:      1.0
* Created by:   AION_user
* Created:      5/8/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 5/8/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_update_aion_user_permission]

    @USER_ID                                                     int
  , @PERMISSION_REF_ID                                           int
  , @USER_PERMISSION_CROSS_REF_ID                                int
  , @UPDATED_DTTM                                                datetime
  , @WKR_ID_TXT                                                  varchar(100)

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       UPDATE USER_PERMISSION_XREF
       SET
            USER_ID                                                      = @USER_ID
          , PERMISSION_REF_ID                                            = @PERMISSION_REF_ID
          , WKR_ID_UPDATED_TXT                                           = @WKR_ID_TXT
          , UPDATED_DTTM                                                 = GETDATE()

       WHERE
          USER_PERMISSION_CROSS_REF_ID                                   = @USER_PERMISSION_CROSS_REF_ID       
       AND 
          ISNULL(CONVERT(varchar(19), UPDATED_DTTM, 120), '')             = ISNULL(CONVERT(varchar(19), @UPDATED_DTTM, 120), '')
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error updating UserPermission record.', 18,1)

     IF @ReturnValue = 0
          RAISERROR('Data was changed/deleted prior to update.', 18,100)

RETURN
GO