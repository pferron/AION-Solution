/****** Object:  StoredProcedure [AION].[usp_update_aion_express_meeting_appointment]    Script Date: 10/5/2021 1:40:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************************************************************
* Object:       usp_update_aion_express_meeting_appointment
* Description:  Updates ExpressMeetingAppointment record using supplied parameters.
* Parameters:   
*               @PROJECT_ID                                                  int
*               @MEETING_ROOM_REF_ID                                         int
*               @APPT_RESPONSE_STATUS_REF_ID                                 int
*               @FROM_DT                                                     datetime
*               @TO_DT                                                       datetime
*               @UPDATED_DTTM                                                datetime
*               @EXPRESS_MEETING_APPT_ID                                     int
*               @WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Number of rows affected.
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.
* Version:      1.0
* Created by:   AION_user
* Created:      8/31/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 8/31/2020    AION_user     Auto-generated
* 10/5/2021    jallen        Add cancellation ref id
***********************************************************************************************************************/

ALTER PROCEDURE [AION].[usp_update_aion_express_meeting_appointment]

    @PROJECT_ID                                                  int
  , @MEETING_ROOM_REF_ID                                         int
  , @APPT_RESPONSE_STATUS_REF_ID                                 int
  , @FROM_DT                                                     datetime
  , @TO_DT                                                       datetime
  , @UPDATED_DTTM                                                datetime
  , @EXPRESS_MEETING_APPT_ID                                     int
  , @VIRTUAL_MEETING_IND                                         bit
  , @WKR_ID_TXT                                                  varchar(100)
  , @CYCLE_NBR                                                   int
  , @PLAN_REVIEW_PROJECT_DETAILS_ID                              int
  , @PLANS_READY_DT                                              datetime = null
  , @GATE_DT                                                     datetime
  , @CANCEL_AFTER_DT											 datetime = null
  , @APPT_CANCELLATION_REF_ID                                    int = null

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       UPDATE EXPRESS_MEETING_APPOINTMENT
       SET
            PROJECT_ID                                                   = @PROJECT_ID
          , MEETING_ROOM_REF_ID                                          = @MEETING_ROOM_REF_ID
          , APPT_RESPONSE_STATUS_REF_ID                                  = @APPT_RESPONSE_STATUS_REF_ID
          , FROM_DT                                                      = @FROM_DT
          , TO_DT                                                        = @TO_DT
          , WKR_ID_UPDATED_TXT                                           = @WKR_ID_TXT
          , UPDATED_DTTM                                                 = GETDATE()
          , VIRTUAL_MEETING_IND                                          = @VIRTUAL_MEETING_IND
		  , CYCLE_NBR                                                    = @CYCLE_NBR
		  , PLAN_REVIEW_PROJECT_DETAILS_ID                               = @PLAN_REVIEW_PROJECT_DETAILS_ID
		  , PLANS_READY_DT                                               = isnull(@PLANS_READY_DT, PLANS_READY_DT)
		  , GATE_DT                                                      = @GATE_DT
		  , CANCEL_AFTER_DT                                              = isnull(@CANCEL_AFTER_DT, CANCEL_AFTER_DT)
		  , APPT_CANCELLATION_REF_ID                                     = isnull(@APPT_CANCELLATION_REF_ID, null)

       WHERE
          EXPRESS_MEETING_APPT_ID                                        = @EXPRESS_MEETING_APPT_ID       
       AND 
          ISNULL(CONVERT(varchar(19), UPDATED_DTTM, 120), '')             = ISNULL(CONVERT(varchar(19), @UPDATED_DTTM, 120), '')
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error updating ExpressMeetingAppointment record.', 18,1)

     IF @ReturnValue = 0
          RAISERROR('Data was changed/deleted prior to update.', 18,100)

RETURN
