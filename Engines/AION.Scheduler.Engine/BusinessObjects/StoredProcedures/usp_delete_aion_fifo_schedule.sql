/****** Object:  StoredProcedure [AION].[usp_delete_aion_fifo_schedule]    Script Date: 6/1/2021 7:59:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************************************************************
* Object:       usp_delete_aion_fifo_schedule
* Description:  Deletes PlanReviewSchedule record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Number of rows affected.
* Version:      1.0
* Created by:   AION_user
* Created:      8/12/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 8/12/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

ALTER PROCEDURE [AION].[usp_delete_aion_fifo_schedule]

    @identity                                                    int

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       DELETE FROM FIFO_SCHEDULE
       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          FIFO_SCHEDULE_ID = @identity
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error deleting FIFOSchedule record.', 18,1)

RETURN


