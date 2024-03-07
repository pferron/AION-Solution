/****** Object:  StoredProcedure [AION].[usp_update_aion_user]    Script Date: 7/13/2020 12:11:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************************************************************  
* Object:       usp_update_aion_prelim_meeting_Proposed_dates  
* Description:  Updates proposed date in prelim meeting appt  
* Parameters:    @Proposed_date1 datetime
*				 @Proposed_date2 datetime
*				 @Proposed_date3 datetime         
* Returns:      Number of rows affected.  
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.  
* Version:      1.0  
* Created by:   AION_user  
* Created:      07/11/2020  
************************************************************************************************************************  
* Change History: Date, Name, Description  
*  07/11/2020    AION.PRELIMINARY_MEETING_APPOINTMENT    
***********************************************************************************************************************/

ALTER PROCEDURE [AION].[usp_update_aion_prelim_meeting_proposed_dates]

@PRELIMINARY_MEETING_APPT_ID  int
,@PROPOSED_DT_1 datetime
,@PROPOSED_DT_2 datetime
,@PROPOSED_DT_3 datetime
 , @ReturnValue                                                 int OUTPUT
AS

     DECLARE @error   int

       UPDATE [AION].[PRELIMINARY_MEETING_APPOINTMENT]
       SET
	   PROPOSED_1_DT = @PROPOSED_DT_1
	   ,PROPOSED_2_DT = @PROPOSED_DT_2
	   ,PROPOSED_3_DT = @PROPOSED_DT_3
           
       WHERE
          PRELIMINARY_MEETING_APPT_ID   = @PRELIMINARY_MEETING_APPT_ID       
       
     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error deleting Notes record.', 18,1)
RETURN
