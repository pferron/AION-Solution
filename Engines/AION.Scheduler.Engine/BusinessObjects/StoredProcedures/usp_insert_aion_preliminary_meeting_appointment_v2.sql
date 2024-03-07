CREATE PROCEDURE [AION].[usp_insert_aion_preliminary_meeting_appointment_v2]
    @FROM_DT                                                     datetime
  , @TO_DT                                                       datetime
  , @MEETING_ROOM_REF_ID                                         int
  , @APPT_RESPONSE_STATUS_REF_ID                                 int
  , @PROJECT_ID                                                  int
  , @APPENDIX_AGENDA_DUE_DT                                      datetime
  , @WKR_ID_TXT                                                  varchar(100)
  , @CANCEL_AFTER_DT                                             datetime
  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

     INSERT INTO PRELIMINARY_MEETING_APPOINTMENT
          (
            FROM_DT
          , TO_DT
          , MEETING_ROOM_REF_ID
          , APPT_RESPONSE_STATUS_REF_ID
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          , PROJECT_ID
          , APPENDIX_AGENDA_DUE_DT
		  , CANCEL_AFTER_DT
          )
     VALUES
          (
            @FROM_DT
          , @TO_DT
          , @MEETING_ROOM_REF_ID
          , @APPT_RESPONSE_STATUS_REF_ID
          , @WKR_ID_TXT
          , GETDATE()
          , @WKR_ID_TXT
          , GETDATE()
          , @PROJECT_ID
          , @APPENDIX_AGENDA_DUE_DT
		  , @CANCEL_AFTER_DT
          )

     SELECT @error = @@ERROR
          , @ReturnValue = SCOPE_IDENTITY()

     IF @error != 0
          RAISERROR('Error adding PreliminaryMeetingAppointment record.', 18,1)

RETURN

