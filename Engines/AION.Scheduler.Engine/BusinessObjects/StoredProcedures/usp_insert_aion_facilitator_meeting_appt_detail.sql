
/***********************************************************************************************************************
* Object:	usp_insert_aion_facilitator_meeting_appt_detail
* Description:	Inserts FacilitatorMeetingApptDetail record.
* Parameters:
*		@FACILITATOR_MEETING_APPT_IDENTIFIER                         int
*		@BUSINESS_REF_ID                                             int
*		@ASSIGNED_PLAN_REVIEWER_ID                                   int
*		@WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Identity column of new record.
* Comments:     If CREATED_DTTM and/or UPDATED_DTTM fields exist in table, GETDATE() is inserted.
*               Worker ID will be inserted for WKR_ID_CREATED_TXT and WKR_ID_UPDATED_TXT fields if they exist.
* Version:      1.0
* Created by:   AION_user
* Created:      4/21/2022
************************************************************************************************************************
* Change History: Date, Name, Description
* 4/21/2022    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_insert_aion_facilitator_meeting_appt_detail]
    @FACILITATOR_MEETING_APPT_IDENTIFIER                         int
  , @BUSINESS_REF_ID                                             int
  , @ASSIGNED_PLAN_REVIEWER_ID                                   int
  , @WKR_ID_TXT                                                  varchar(100)
  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

     INSERT INTO FACILITATOR_MEETING_APPOINTMENT_DETAIL
          (
            FACILITATOR_MEETING_APPT_IDENTIFIER
          , BUSINESS_REF_ID
          , ASSIGNED_PLAN_REVIEWER_ID
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          )
     VALUES
          (
            @FACILITATOR_MEETING_APPT_IDENTIFIER
          , @BUSINESS_REF_ID
          , @ASSIGNED_PLAN_REVIEWER_ID
          , @WKR_ID_TXT
          , GETDATE()
          , @WKR_ID_TXT
          , GETDATE()
          )

     SELECT @error = @@ERROR
          , @ReturnValue = SCOPE_IDENTITY()

     IF @error != 0
          RAISERROR('Error adding FacilitatorMeetingApptDetail record.', 18,1)

RETURN
GO