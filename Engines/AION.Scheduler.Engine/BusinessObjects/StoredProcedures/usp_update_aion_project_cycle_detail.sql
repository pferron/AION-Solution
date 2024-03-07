
/***********************************************************************************************************************
* Object:       usp_update_aion_project_cycle_detail
* Description:  Updates ProjectCycleDetail record using supplied parameters.
* Parameters:   
*               @PROJECT_CYCLE_DETAIL_ID                                     int
*               @PROJECT_CYCLE_ID                                            int
*               @BUSINESS_REF_ID                                             int
*               @REREVIEW_HOURS_NBR                                          decimal
*               @UPDATED_DTTM                                                datetime
*               @WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Number of rows affected.
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.
* Version:      1.0
* Created by:   AION_user
* Created:      10/14/2021
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/14/2021    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_update_aion_project_cycle_detail]

    @PROJECT_CYCLE_DETAIL_ID                                     int
  , @PROJECT_CYCLE_ID                                            int
  , @BUSINESS_REF_ID                                             int
  , @REREVIEW_HOURS_NBR                                          decimal(9,2)
  , @UPDATED_DTTM                                                datetime
  , @WKR_ID_TXT                                                  varchar(100)

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       UPDATE PROJECT_CYCLE_DETAIL
       SET
            PROJECT_CYCLE_ID                                             = @PROJECT_CYCLE_ID
          , BUSINESS_REF_ID                                              = @BUSINESS_REF_ID
          , REREVIEW_HOURS_NBR                                           = @REREVIEW_HOURS_NBR
          , WKR_ID_UPDATED_TXT                                           = @WKR_ID_TXT
          , UPDATED_DTTM                                                 = GETDATE()

       WHERE
          PROJECT_CYCLE_DETAIL_ID                                        = @PROJECT_CYCLE_DETAIL_ID       
       AND 
          ISNULL(CONVERT(varchar(19), UPDATED_DTTM, 120), '')             = ISNULL(CONVERT(varchar(19), @UPDATED_DTTM, 120), '')
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error updating ProjectCycleDetail record.', 18,1)

     IF @ReturnValue = 0
          RAISERROR('Data was changed/deleted prior to update.', 18,100)

RETURN
GO