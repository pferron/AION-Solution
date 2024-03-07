/****** Object:  StoredProcedure [AION].[usp_delete_aion_occupancy_sqr_footage_usr_map]    Script Date: 6/11/2020 3:44:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************************************************************
* Object:       usp_delete_aion_occupancy_sqr_footage_usr_map
* Description:  Deletes OccupancySqrFootageUsrMap record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Number of rows affected.
* Version:      1.0
* Created by:   AION_user
* Created:      4/30/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 4/30/2020    AION_user     Auto-generated
* 6/12         Gaya		delete by userid
***********************************************************************************************************************/

CREATE PROCEDURE [AION].[usp_delete_aion_occupancy_sqr_footage_usr_map]

    @identity                                                    int

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       DELETE FROM OCCUPANCY_SQUARE_FOOTAGE_USER_MAP
       WHERE
        
       -- @TODO:  Correct the following as necessary
	   USER_ID = @identity
        
         
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error deleting OccupancySqrFootageUsrMap record.', 18,1)

RETURN

