/***********************************************************************************************************************
* Object:       usp_update_aion_facilitator_meeting_proposed_dates
* Description:  Updates up to three proposed new dates for a facilitator meeting appointment
* Parameters:   
*               @FACILITATOR_MEETING_APPT_IDENTIFIER int
*               @PROPOSED_1_DT datetime 
*               @PROPOSED_2_DT datetime
*               @PROPOSED_3_DT datetime
*
* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval.
*               This proc expects id_person and/or id_file to generate list; modify as necessary.
*               Include ORDER BY clause as necessary.
* Version:      1.0
* Created by:   AION_user
* Created:      04/10/2021
************************************************************************************************************************
* Change History: Date, Name, Description
* 04/10/2021    jallen  Create
***********************************************************************************************************************/

CREATE PROCEDURE [AION].[usp_update_aion_facilitator_meeting_proposed_dates]

	  @FACILITATOR_MEETING_APPT_IDENTIFIER  int
	, @PROPOSED_1_DT datetime
	, @PROPOSED_2_DT datetime
	, @PROPOSED_3_DT datetime
	, @ReturnValue   int OUTPUT
AS

     DECLARE @error   int

       UPDATE [AION].[FACILITATOR_MEETING_APPOINTMENT]
       SET
	  
	    PROPOSED_1_DT = @PROPOSED_1_DT
	   ,PROPOSED_2_DT = @PROPOSED_2_DT
	   ,PROPOSED_3_DT = @PROPOSED_3_DT
           
       WHERE
          FACILITATOR_MEETING_APPT_IDENTIFIER   = @FACILITATOR_MEETING_APPT_IDENTIFIER       
       
     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error deleting Notes record.', 18,1)
RETURN
