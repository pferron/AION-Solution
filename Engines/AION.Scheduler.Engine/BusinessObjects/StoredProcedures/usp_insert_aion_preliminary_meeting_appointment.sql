
/***********************************************************************************************************************
* Object:	usp_insert_aion_preliminary_meeting_appointment
* Description:	Inserts PreliminaryMeetingAppointment record.
* Parameters:
*		@FROM_DT                                                     datetime
*		@TO_DT                                                       datetime
*		@MEETING_ROOM_REF_ID                                         int
*		@APPT_RESPONSE_STATUS_REF_ID                                 int
*		@PROJECT_ID                                                  int
*		@APPENDIX_AGENDA_DUE_DT                                      datetime
*		@WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Identity column of new record.
* Comments:     If CREATED_DTTM and/or UPDATED_DTTM fields exist in table, GETDATE() is inserted.
*               Worker ID will be inserted for WKR_ID_CREATED_TXT and WKR_ID_UPDATED_TXT fields if they exist.
* Version:      1.0
* Created by:   AION_user
* Created:      6/24/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 6/24/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_insert_aion_preliminary_meeting_appointment]
    @FROM_DT                                                     datetime
  , @TO_DT                                                       datetime
  , @MEETING_ROOM_REF_ID                                         int
  , @APPT_RESPONSE_STATUS_REF_ID                                 int
  , @PROJECT_ID                                                  int
  , @APPENDIX_AGENDA_DUE_DT                                      datetime
  , @WKR_ID_TXT                                                  varchar(100)
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
          )

     SELECT @error = @@ERROR
          , @ReturnValue = SCOPE_IDENTITY()

     IF @error != 0
          RAISERROR('Error adding PreliminaryMeetingAppointment record.', 18,1)

RETURN
GO