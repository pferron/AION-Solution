
/***********************************************************************************************************************
* Object:       usp_update_aion_facilitator_meeting_appt_detail
* Description:  Updates FacilitatorMeetingApptDetail record using supplied parameters.
* Parameters:   
*               @FACILITATOR_MEETING_APPT_DETAIL_ID                          int
*               @FACILITATOR_MEETING_APPT_IDENTIFIER                         int
*               @BUSINESS_REF_ID                                             int
*               @ASSIGNED_PLAN_REVIEWER_ID                                   int
*               @UPDATED_DTTM                                                datetime
*               @WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Number of rows affected.
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.
* Version:      1.0
* Created by:   AION_user
* Created:      4/21/2022
************************************************************************************************************************
* Change History: Date, Name, Description
* 4/21/2022    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_update_aion_facilitator_meeting_appt_detail]

    @FACILITATOR_MEETING_APPT_DETAIL_ID                          int
  , @FACILITATOR_MEETING_APPT_IDENTIFIER                         int
  , @BUSINESS_REF_ID                                             int
  , @ASSIGNED_PLAN_REVIEWER_ID                                   int
  , @UPDATED_DTTM                                                datetime
  , @WKR_ID_TXT                                                  varchar(100)

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       UPDATE FACILITATOR_MEETING_APPOINTMENT_DETAIL
       SET
            FACILITATOR_MEETING_APPT_IDENTIFIER                          = @FACILITATOR_MEETING_APPT_IDENTIFIER
          , BUSINESS_REF_ID                                              = @BUSINESS_REF_ID
          , ASSIGNED_PLAN_REVIEWER_ID                                    = @ASSIGNED_PLAN_REVIEWER_ID
          , WKR_ID_UPDATED_TXT                                           = @WKR_ID_TXT
          , UPDATED_DTTM                                                 = GETDATE()

       WHERE
          FACILITATOR_MEETING_APPT_DETAIL_ID                             = @FACILITATOR_MEETING_APPT_DETAIL_ID       
       AND 
          ISNULL(CONVERT(varchar(19), UPDATED_DTTM, 120), '')             = ISNULL(CONVERT(varchar(19), @UPDATED_DTTM, 120), '')
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error updating FacilitatorMeetingApptDetail record.', 18,1)

     IF @ReturnValue = 0
          RAISERROR('Data was changed/deleted prior to update.', 18,100)

RETURN
GO