
/***********************************************************************************************************************
* Object:       usp_delete_aion_project_rtap_mapping
* Description:  Deletes ProjectRtapMapping record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Number of rows affected.
* Version:      1.0
* Created by:   AION_user
* Created:      10/2/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/2/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_delete_aion_project_rtap_mapping]

    @identity                                                    int

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       DELETE FROM PROJECT_RTAP_MAPPING
       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          PROJECT_RTAP_MAPPING_ID = @identity
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error deleting ProjectRtapMapping record.', 18,1)

RETURN

GO