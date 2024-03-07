/****** Object:  StoredProcedure [AION].[usp_update_aion_default_estimation_hours]    Script Date: 2/24/2020 3:33:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************************************************************
* Object:       usp_update_aion_default_estimation_hours_b_o
* Description:  Updates DefaultEstimationHoursBO record using supplied parameters.
* Parameters:   
*               @DEFAULT_ESTIMATION_HOURS_ID                                 int
*               @DEFAULT_HOURS_NBR                                           decimal
*               @UPDATED_DTTM                                                datetime
*               @BUSINESS_REF_ID                                             int
*               @PROJECT_TYP_REF_ID                                          int
*               @ENABLED_IND                                                 bit
*               @ESTIMATION_HOURS_TXT                                        varchar(255)
*               @WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Number of rows affected.
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.
* Version:      1.0
* Created by:   AION_user
* Created:      2/19/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 2/19/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

ALTER PROCEDURE [AION].[usp_update_aion_default_estimation_hours]

    @DEFAULT_ESTIMATION_HOURS_ID                                 int
  , @DEFAULT_HOURS_NBR                                           decimal(15,7)
  , @UPDATED_DTTM                                                datetime
  , @BUSINESS_REF_ID                                             int
  , @PROJECT_TYP_REF_ID                                          int
  , @ENABLED_IND                                                 bit
  , @ESTIMATION_HOURS_TXT                                        varchar(255)
  , @WKR_ID_TXT                                                  varchar(100)

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       UPDATE DEFAULT_ESTIMATION_HOURS
       SET
            DEFAULT_HOURS_NBR                                            = @DEFAULT_HOURS_NBR
          , WKR_ID_UPDATED_TXT                                           = @WKR_ID_TXT
          , UPDATED_DTTM                                                 = GETDATE()
          , BUSINESS_REF_ID                                              = @BUSINESS_REF_ID
          , PROJECT_TYP_REF_ID                                           = @PROJECT_TYP_REF_ID
          , ENABLED_IND                                                  = @ENABLED_IND
          , ESTIMATION_HOURS_TXT                                         = @ESTIMATION_HOURS_TXT

       WHERE
          DEFAULT_ESTIMATION_HOURS_ID                                    = @DEFAULT_ESTIMATION_HOURS_ID       
       AND 
          ISNULL(CONVERT(varchar(19), UPDATED_DTTM, 120), '')             = ISNULL(CONVERT(varchar(19), @UPDATED_DTTM, 120), '')
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error updating DefaultEstimationHoursBO record.', 18,1)

     --IF @ReturnValue = 0
     --     RAISERROR('Data was changed/deleted prior to update.', 18,100)

RETURN
