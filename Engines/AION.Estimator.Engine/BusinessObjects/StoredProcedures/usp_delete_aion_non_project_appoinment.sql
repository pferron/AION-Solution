/****** Object:  StoredProcedure [AION].[usp_delete_aion_non_project_appoinment]    Script Date: 5/20/2020 9:57:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************************************************************
* Object:       usp_delete_aion_non_project_appoinment
* Description:  Deletes NonProjectAppoinment record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Number of rows affected.
* Version:      1.0
* Created by:   AION_user
* Created:      3/19/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 3/19/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

ALTER PROCEDURE [AION].[usp_delete_aion_non_project_appoinment]

    @scheduleId	 int
   ,@flag bit
   ,@ReturnValue int OUTPUT

AS
     DECLARE @id int
     DECLARE @count   int
     DECLARE @error   int
	
	IF @flag=0 --Delete 
	BEGIN
	     
		 SET @id = (SELECT APPT_ID FROM PROJECT_SCHEDULE WHERE PROJECT_SCHEDULE_ID = @scheduleId )

		 DELETE FROM USER_SCHEDULE WHERE PROJECT_SCHEDULE_ID = @scheduleId
         DELETE FROM PROJECT_SCHEDULE WHERE PROJECT_SCHEDULE_ID=@scheduleId


	     SELECT @count = count(*) FROM PROJECT_SCHEDULE WHERE APPT_ID=@id

		 IF @count = 0
				DELETE FROM NON_PROJECT_APPOINTMENT WHERE NON_PROJECT_APPT_ID = @id
          
		 SELECT @error = @@ERROR, @ReturnValue = @@ROWCOUNT

         IF @error != 0
         RAISERROR('Error deleting NonProjectAppoinment record.', 18,1)
	END
	ELSE
	BEGIN

	    SET @id = (SELECT APPT_ID FROM PROJECT_SCHEDULE WHERE PROJECT_SCHEDULE_ID = @scheduleId)
		
		DELETE FROM USER_SCHEDULE WHERE PROJECT_SCHEDULE_ID IN(
		SELECT PROJECT_SCHEDULE_ID FROM PROJECT_SCHEDULE WHERE APPT_ID = @id)
        
		DELETE FROM PROJECT_SCHEDULE WHERE APPT_ID=@id

	    SELECT @count = count(*) FROM PROJECT_SCHEDULE WHERE APPT_ID=@id

		IF @count = 0
			DELETE FROM NON_PROJECT_APPOINTMENT WHERE NON_PROJECT_APPT_ID = @id
          
		SELECT @error = @@ERROR, @ReturnValue = @@ROWCOUNT

         IF @error != 0
         RAISERROR('Error deleting NonProjectAppoinment record.', 18,1)
	END

RETURN

