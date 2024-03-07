
/***********************************************************************************************************************
* Object:	usp_insert_aion_audit_action_ref
* Description:	Inserts AuditActionRef record.
* Parameters:
*		@AUDIT_ACTION_NM                                             varchar(100)
*		@AUDIT_ACTION_DESC                                           varchar(255)
*		@WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Identity column of new record.
* Comments:     If CREATED_DTTM and/or UPDATED_DTTM fields exist in table, GETDATE() is inserted.
*               Worker ID will be inserted for WKR_ID_CREATED_TXT and WKR_ID_UPDATED_TXT fields if they exist.
* Version:      1.0
* Created by:   AION_user
* Created:      2/27/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 2/27/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_insert_aion_audit_action_ref]
    @AUDIT_ACTION_NM                                             varchar(100)
  , @AUDIT_ACTION_DESC                                           varchar(255)
  , @WKR_ID_TXT                                                  varchar(100)
  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

     INSERT INTO AUDIT_ACTION_REF
          (
            AUDIT_ACTION_NM
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          , AUDIT_ACTION_DESC
          )
     VALUES
          (
            @AUDIT_ACTION_NM
          , @WKR_ID_TXT
          , GETDATE()
          , @WKR_ID_TXT
          , GETDATE()
          , @AUDIT_ACTION_DESC
          )

     SELECT @error = @@ERROR
          , @ReturnValue = SCOPE_IDENTITY()

     IF @error != 0
          RAISERROR('Error adding AuditActionRef record.', 18,1)

RETURN
GO