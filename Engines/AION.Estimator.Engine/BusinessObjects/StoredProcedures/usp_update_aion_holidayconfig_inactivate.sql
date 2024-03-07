SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************************************************************
* Object:       usp_update_aion_holidayconfig_inactivate
* Description:  Inactivates Project record for given key field(s).
* Parameters:     @identity                                                    int
*
* Returns:      Number of rows affected.
* Version:      1.0
* Created by:   Janessa Allen
* Created:      04/30/2021
************************************************************************************************************************
* Change History: Date, Name, Description
* 04/30/2021    jallen
* 
***********************************************************************************************************************/

CREATE PROCEDURE [AION].[usp_update_aion_holidayconfig_inactivate]

    @identity                                                    int

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       UPDATE AION.HOLIDAY_CONFIGURATION
	   SET ACTIVE_IND = 0
       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          HOLIDAY_CONFIG_ID = @identity
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error updating Project record.', 18,1)

RETURN

