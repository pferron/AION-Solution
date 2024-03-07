
/***********************************************************************************************************************
* Object:       usp_update_aion_project_type_business_x_ref
* Description:  Updates ProjectTypeBusinessXRef record using supplied parameters.
* Parameters:   
*               @BUSINESS_REF_ID                                             int
*               @PROJECT_TYP_REF_ID                                          int
*               @PROJECT_TYP_BUSINESS_CROSS_REF_ID                           int
*               @WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Number of rows affected.
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.
* Version:      1.0
* Created by:   AION_user
* Created:      12/18/2019
************************************************************************************************************************
* Change History: Date, Name, Description
* 12/18/2019    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_update_aion_project_type_business_x_ref]

    @BUSINESS_REF_ID                                             int
  , @PROJECT_TYP_REF_ID                                          int
  , @PROJECT_TYP_BUSINESS_CROSS_REF_ID                           int
  , @WKR_ID_TXT                                                  varchar(100)

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       UPDATE PROJECT_TYPE_BUSINESS_XREF
       SET
            BUSINESS_REF_ID                                              = @BUSINESS_REF_ID
          , PROJECT_TYP_REF_ID                                           = @PROJECT_TYP_REF_ID

       WHERE
          PROJECT_TYP_BUSINESS_CROSS_REF_ID                              = @PROJECT_TYP_BUSINESS_CROSS_REF_ID       
       AND 
          ISNULL(CONVERT(varchar(19), UPDATED_DTTM, 120), '')             = ISNULL(CONVERT(varchar(19), @UPDATED_DTTM, 120), '')
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error updating ProjectTypeBusinessXRef record.', 18,1)

     IF @ReturnValue = 0
          RAISERROR('Data was changed/deleted prior to update.', 18,100)

RETURN
GO