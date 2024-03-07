CREATE PROCEDURE [AION].[usp_insert_aion_facilitator_meeting_appointment_v2]
    @PROJECT_ID                                                  int
  , @MEETING_ROOM_REF_ID                                         int = null
  , @APPT_RESPONSE_STATUS_REF_ID                                 int
  , @FROM_DT                                                     datetime = null
  , @TO_DT                                                       datetime = null
  , @VIRTUAL_MEETING_IND                                         bit
  , @MEETING_TYP_REF_ID                                          int
  , @WKR_ID_TXT                                                  varchar(100)
  , @CANCEL_AFTER_DT                                             datetime = null
  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

     INSERT INTO FACILITATOR_MEETING_APPOINTMENT
          (
            PROJECT_ID
          , MEETING_ROOM_REF_ID
          , APPT_RESPONSE_STATUS_REF_ID
          , FROM_DT
          , TO_DT
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          , VIRTUAL_MEETING_IND
          , MEETING_TYP_REF_ID
		  , CANCEL_AFTER_DT
          )
     VALUES
          (
            @PROJECT_ID
          , @MEETING_ROOM_REF_ID
          , @APPT_RESPONSE_STATUS_REF_ID
          , @FROM_DT
          , @TO_DT
          , @WKR_ID_TXT
          , GETDATE()
          , @WKR_ID_TXT
          , GETDATE()
          , @VIRTUAL_MEETING_IND
          , @MEETING_TYP_REF_ID
		  , @CANCEL_AFTER_DT
          )

     SELECT @error = @@ERROR
          , @ReturnValue = SCOPE_IDENTITY()

     IF @error != 0
          RAISERROR('Error adding FacilitatorMeetingAppointment record.', 18,1)

RETURN

