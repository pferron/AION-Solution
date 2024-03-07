CREATE PROCEDURE [AION].[usp_update_aion_express_meeting_proposed_dates]

@EXPRESS_MEETING_APPT_ID  int
,@PROPOSED_DT_1 datetime
,@PROPOSED_DT_2 datetime
,@PROPOSED_DT_3 datetime
 , @ReturnValue                                                 int OUTPUT
AS

     DECLARE @error   int

       UPDATE [AION].[EXPRESS_MEETING_APPOINTMENT]
       SET
 	    PROPOSED_1_DT = @PROPOSED_DT_1
	   ,PROPOSED_2_DT = @PROPOSED_DT_2
	   ,PROPOSED_3_DT = @PROPOSED_DT_3
           
       WHERE
            EXPRESS_MEETING_APPT_ID = @EXPRESS_MEETING_APPT_ID       
       
     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error updating Express Meeting Appointment record.', 18,1)
RETURN
