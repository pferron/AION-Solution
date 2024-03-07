
/***********************************************************************************************************************
* Object:       usp_update_aion_occupancy_sqr_footage_usr_map
* Description:  Updates OccupancySqrFootageUsrMap record using supplied parameters.
* Parameters:   
*               @USER_ID                                                     int
*               @OCCUPANCY_TYP_REF_ID                                        int
*               @SQUARE_FOOTAGE_REF_ID                                       int
*               @OCCUPANCY_SQUARE_FOOTAGE_USER_MAP_ID                        int
*               @UPDATED_DTTM                                                datetime
*               @WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Number of rows affected.
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.
* Version:      1.0
* Created by:   AION_user
* Created:      4/30/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 4/30/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_update_aion_occupancy_sqr_footage_usr_map]

    @USER_ID                                                     int
  , @OCCUPANCY_TYP_REF_ID                                        int
  , @SQUARE_FOOTAGE_REF_ID                                       int
  , @OCCUPANCY_SQUARE_FOOTAGE_USER_MAP_ID                        int
  , @UPDATED_DTTM                                                datetime
  , @WKR_ID_TXT                                                  varchar(100)

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       UPDATE OCCUPANCY_SQUARE_FOOTAGE_USER_MAP
       SET
            USER_ID                                                      = @USER_ID
          , OCCUPANCY_TYP_REF_ID                                         = @OCCUPANCY_TYP_REF_ID
          , SQUARE_FOOTAGE_REF_ID                                        = @SQUARE_FOOTAGE_REF_ID
          , WKR_ID_UPDATED_TXT                                           = @WKR_ID_TXT
          , UPDATED_DTTM                                                 = GETDATE()

       WHERE
          OCCUPANCY_SQUARE_FOOTAGE_USER_MAP_ID                           = @OCCUPANCY_SQUARE_FOOTAGE_USER_MAP_ID       
       AND 
          ISNULL(CONVERT(varchar(19), UPDATED_DTTM, 120), '')             = ISNULL(CONVERT(varchar(19), @UPDATED_DTTM, 120), '')
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error updating OccupancySqrFootageUsrMap record.', 18,1)

     IF @ReturnValue = 0
          RAISERROR('Data was changed/deleted prior to update.', 18,100)

RETURN
GO