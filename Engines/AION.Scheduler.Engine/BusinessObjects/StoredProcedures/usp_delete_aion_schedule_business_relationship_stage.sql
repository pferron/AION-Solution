
/***********************************************************************************************************************
* Object:       usp_delete_aion_schedule_business_relationship_stage
* Description:  Deletes ScheduleBusinessRelationshipStage record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Number of rows affected.
* Version:      1.0
* Created by:   AION_user
* Created:      12/17/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 12/17/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_delete_aion_schedule_business_relationship_stage]

    @identity                                                    int

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       DELETE FROM SCHEDULE_BUSINESS_RELATIONSHIP_STAGE
       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          SCHEDULE_BUSINESS_RELATIONSHIP_STAGE_ID = @identity
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error deleting ScheduleBusinessRelationshipStage record.', 18,1)

RETURN

GO