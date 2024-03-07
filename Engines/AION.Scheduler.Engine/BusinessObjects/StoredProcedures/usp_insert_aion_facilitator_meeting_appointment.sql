
/***********************************************************************************************************************
* Object:	usp_insert_aion_facilitator_meeting_appointment
* Description:	Inserts FacilitatorMeetingAppointment record.
* Parameters:
*		@PROJECT_ID                                                  int
*		@MEETING_ROOM_REF_ID                                         int
*		@APPT_RESPONSE_STATUS_REF_ID                                 int
*		@FROM_DT                                                     datetime
*		@TO_DT                                                       datetime
*		@VIRTUAL_MEETING_IND                                         bit
*		@MEETING_TYP_REF_ID                                          int
*		@WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Identity column of new record.
* Comments:     If CREATED_DTTM and/or UPDATED_DTTM fields exist in table, GETDATE() is inserted.
*               Worker ID will be inserted for WKR_ID_CREATED_TXT and WKR_ID_UPDATED_TXT fields if they exist.
* Version:      1.0
* Created by:   AION_user
* Created:      10/7/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/7/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_insert_aion_facilitator_meeting_appointment]
    @PROJECT_ID                                                  int
  , @MEETING_ROOM_REF_ID                                         int
  , @APPT_RESPONSE_STATUS_REF_ID                                 int
  , @FROM_DT                                                     datetime
  , @TO_DT                                                       datetime
  , @VIRTUAL_MEETING_IND                                         bit
  , @MEETING_TYP_REF_ID                                          int
  , @WKR_ID_TXT                                                  varchar(100)
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
          )

     SELECT @error = @@ERROR
          , @ReturnValue = SCOPE_IDENTITY()

     IF @error != 0
          RAISERROR('Error adding FacilitatorMeetingAppointment record.', 18,1)

RETURN
GO