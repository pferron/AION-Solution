
/***********************************************************************************************************************
* Object:       usp_update_aion_facilitator_meeting_appointment
* Description:  Updates FacilitatorMeetingAppointment record using supplied parameters.
* Parameters:   
*               @FACILITATOR_MEETING_APPT_IDENTIFIER                         int
*               @PROJECT_ID                                                  int
*               @MEETING_ROOM_REF_ID                                         int
*               @APPT_RESPONSE_STATUS_REF_ID                                 int
*               @FROM_DT                                                     datetime
*               @TO_DT                                                       datetime
*               @UPDATED_DTTM                                                datetime
*               @VIRTUAL_MEETING_IND                                         bit
*               @MEETING_TYP_REF_ID                                          int
*               @WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Number of rows affected.
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.
* Version:      1.0
* Created by:   AION_user
* Created:      10/7/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/7/2020    AION_user     Auto-generated
* 04/10/2021   jallen      add cancellation reference
* 05/25/2021   jallen      remove part of where clause that defaults the input parameter for updated dttm
* 07/29/2021   jallen      add update for new column EXTERNAL_ATTENDEES_CNT
***********************************************************************************************************************/

ALTER PROCEDURE [AION].[usp_update_aion_facilitator_meeting_appointment]

    @FACILITATOR_MEETING_APPT_IDENTIFIER                         int
  , @PROJECT_ID                                                  int
  , @MEETING_ROOM_REF_ID                                         int = null
  , @APPT_RESPONSE_STATUS_REF_ID                                 int
  , @APPT_CANCELLATION_REF_ID                                    int = null
  , @FROM_DT                                                     datetime = null
  , @TO_DT                                                       datetime = null
  , @UPDATED_DTTM                                                datetime
  , @VIRTUAL_MEETING_IND                                         bit
  , @MEETING_TYP_REF_ID                                          int
  , @WKR_ID_TXT                                                  varchar(100)
  , @CANCEL_AFTER_DT											 datetime = null
  , @EXTERNAL_ATTENDEES_CNT									     int = null

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       UPDATE FACILITATOR_MEETING_APPOINTMENT
       SET
            PROJECT_ID                                                   = @PROJECT_ID
          , MEETING_ROOM_REF_ID                                          = @MEETING_ROOM_REF_ID
          , APPT_RESPONSE_STATUS_REF_ID                                  = @APPT_RESPONSE_STATUS_REF_ID
		  , APPT_CANCELLATION_REF_ID								     = isnull(@APPT_CANCELLATION_REF_ID, null)
          , FROM_DT                                                      = @FROM_DT
          , TO_DT                                                        = @TO_DT
          , WKR_ID_UPDATED_TXT                                           = @WKR_ID_TXT
          , UPDATED_DTTM                                                 = GETDATE()
          , VIRTUAL_MEETING_IND                                          = @VIRTUAL_MEETING_IND
          , MEETING_TYP_REF_ID                                           = @MEETING_TYP_REF_ID
		  , CANCEL_AFTER_DT                                              = isnull(@CANCEL_AFTER_DT, CANCEL_AFTER_DT)
          , EXTERNAL_ATTENDEES_CNT                                       = isnull(@EXTERNAL_ATTENDEES_CNT, EXTERNAL_ATTENDEES_CNT)

       WHERE
          FACILITATOR_MEETING_APPT_IDENTIFIER                            = @FACILITATOR_MEETING_APPT_IDENTIFIER       
         

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error updating FacilitatorMeetingAppointment record.', 18,1)

     IF @ReturnValue = 0
          RAISERROR('Data was changed/deleted prior to update.', 18,100)

RETURN
GO