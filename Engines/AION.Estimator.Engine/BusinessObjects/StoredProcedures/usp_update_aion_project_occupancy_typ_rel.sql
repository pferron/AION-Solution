
/***********************************************************************************************************************
* Object:       usp_update_aion_project_occupancy_typ_rel
* Description:  Updates ProjectOccupancyTypRel record using supplied parameters.
* Parameters:   
*               @PROJECT_OCCUPANCY_TYPE_RELATIONSHIP_ID                      int
*               @OCCUPANCY_TYP_REF_ID                                        int
*               @PROJECT_OCCUPANCY_TYP_MAP_REF_ID                            int
*               @UPDATED_DTTM                                                datetime
*               @WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Number of rows affected.
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.
* Version:      1.0
* Created by:   AION_user
* Created:      10/28/2019
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/28/2019    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_update_aion_project_occupancy_typ_rel]

    @PROJECT_OCCUPANCY_TYPE_RELATIONSHIP_ID                      int
  , @OCCUPANCY_TYP_REF_ID                                        int
  , @PROJECT_OCCUPANCY_TYP_MAP_REF_ID                            int
  , @UPDATED_DTTM                                                datetime
  , @WKR_ID_TXT                                                  varchar(100)

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       UPDATE PROJECT_OCCUPANCY_TYPE_RELATIONSHIP
       SET
            OCCUPANCY_TYP_REF_ID                                         = @OCCUPANCY_TYP_REF_ID
          , PROJECT_OCCUPANCY_TYP_MAP_REF_ID                             = @PROJECT_OCCUPANCY_TYP_MAP_REF_ID
          , WKR_ID_UPDATED_TXT                                           = @WKR_ID_TXT
          , UPDATED_DTTM                                                 = GETDATE()

       WHERE
          PROJECT_OCCUPANCY_TYPE_RELATIONSHIP_ID                         = @PROJECT_OCCUPANCY_TYPE_RELATIONSHIP_ID       
       AND 
          ISNULL(CONVERT(varchar(19), UPDATED_DTTM, 120), '')             = ISNULL(CONVERT(varchar(19), @UPDATED_DTTM, 120), '')
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error updating ProjectOccupancyTypRel record.', 18,1)

     IF @ReturnValue = 0
          RAISERROR('Data was changed/deleted prior to update.', 18,100)

RETURN
GO