
/***********************************************************************************************************************
* Object:       usp_update_aion_reserve_express_plan_reviewer
* Description:  Updates ReserveExpressPlanReviewer record using supplied parameters.
* Parameters:   
*               @RESERVE_EXPRESS_PLAN_REVIEWER_ID                            int
*               @BUSINESS_REF_ID                                             int
*               @PLAN_REVIEWER_ID                                            int
*               @ROTATION_NBR                                                int
*               @UPDATED_DTTM                                                datetime
*               @WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Number of rows affected.
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.
* Version:      1.0
* Created by:   AION_user
* Created:      8/6/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 8/6/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_update_aion_reserve_express_plan_reviewer]

    @RESERVE_EXPRESS_PLAN_REVIEWER_ID                            int
  , @BUSINESS_REF_ID                                             int
  , @PLAN_REVIEWER_ID                                            int
  , @ROTATION_NBR                                                int
  , @UPDATED_DTTM                                                datetime
  , @WKR_ID_TXT                                                  varchar(100)

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       UPDATE RESERVE_EXPRESS_PLAN_REVIEWER
       SET
            BUSINESS_REF_ID                                              = @BUSINESS_REF_ID
          , PLAN_REVIEWER_ID                                             = @PLAN_REVIEWER_ID
          , ROTATION_NBR                                                 = @ROTATION_NBR
          , WKR_ID_UPDATED_TXT                                           = @WKR_ID_TXT
          , UPDATED_DTTM                                                 = GETDATE()

       WHERE
          RESERVE_EXPRESS_PLAN_REVIEWER_ID                               = @RESERVE_EXPRESS_PLAN_REVIEWER_ID       
       AND 
          ISNULL(CONVERT(varchar(19), UPDATED_DTTM, 120), '')             = ISNULL(CONVERT(varchar(19), @UPDATED_DTTM, 120), '')
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error updating ReserveExpressPlanReviewer record.', 18,1)

     IF @ReturnValue = 0
          RAISERROR('Data was changed/deleted prior to update.', 18,100)

RETURN
GO