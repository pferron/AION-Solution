
/***********************************************************************************************************************
* Object:       usp_update_aion_audit_action_ref
* Description:  Updates AuditActionRef record using supplied parameters.
* Parameters:   
*               @AUDIT_ACTION_REF_ID                                         int
*               @AUDIT_ACTION_NM                                             varchar(100)
*               @UPDATED_DTTM                                                datetime
*               @AUDIT_ACTION_DESC                                           varchar(255)
*               @WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Number of rows affected.
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.
* Version:      1.0
* Created by:   AION_user
* Created:      2/27/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 2/27/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_update_aion_audit_action_ref]

    @AUDIT_ACTION_REF_ID                                         int
  , @AUDIT_ACTION_NM                                             varchar(100)
  , @UPDATED_DTTM                                                datetime
  , @AUDIT_ACTION_DESC                                           varchar(255)
  , @WKR_ID_TXT                                                  varchar(100)

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       UPDATE AUDIT_ACTION_REF
       SET
            AUDIT_ACTION_NM                                              = @AUDIT_ACTION_NM
          , WKR_ID_UPDATED_TXT                                           = @WKR_ID_TXT
          , UPDATED_DTTM                                                 = GETDATE()
          , AUDIT_ACTION_DESC                                            = @AUDIT_ACTION_DESC

       WHERE
          AUDIT_ACTION_REF_ID                                            = @AUDIT_ACTION_REF_ID       
       AND 
          ISNULL(CONVERT(varchar(19), UPDATED_DTTM, 120), '')             = ISNULL(CONVERT(varchar(19), @UPDATED_DTTM, 120), '')
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error updating AuditActionRef record.', 18,1)

     IF @ReturnValue = 0
          RAISERROR('Data was changed/deleted prior to update.', 18,100)

RETURN
GO