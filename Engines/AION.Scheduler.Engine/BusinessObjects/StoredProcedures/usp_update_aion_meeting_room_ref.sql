
/***********************************************************************************************************************
* Object:       usp_update_aion_meeting_room_ref
* Description:  Updates MeetingRoomRef record using supplied parameters.
* Parameters:   
*               @MEETING_ROOM_REF_ID                                         int
*               @MEETING_ROOM_NM                                             varchar(50)
*               @ACTIVE_IND                                                  bit
*               @UPDATED_DTTM                                                datetime
*               @WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Number of rows affected.
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.
* Version:      1.0
* Created by:   AION_user
* Created:      3/19/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 3/19/2020    AION_user     Auto-generated
* 05/03/2021   jallen        Add user principal name and calendar id
***********************************************************************************************************************/

CREATE PROCEDURE [AION].[usp_update_aion_meeting_room_ref]

    @MEETING_ROOM_REF_ID                                         int
  , @MEETING_ROOM_NM                                             varchar(50)
  , @USER_PRINCIPAL_NM                                           varchar(100)
  , @CALENDAR_ID                                                 varchar(255)
  , @ACTIVE_IND                                                  bit
  , @UPDATED_DTTM                                                datetime
  , @WKR_ID_TXT                                                  varchar(100)

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       UPDATE MEETING_ROOM_REF
       SET
            MEETING_ROOM_NM                                              = @MEETING_ROOM_NM
          , USER_PRINCIPAL_NM                                            = @USER_PRINCIPAL_NM
          , CALENDAR_ID                                                  = @CALENDAR_ID
          , ACTIVE_IND                                                   = @ACTIVE_IND
          , WKR_ID_UPDATED_TXT                                           = @WKR_ID_TXT
          , UPDATED_DTTM                                                 = GETDATE()

       WHERE
          MEETING_ROOM_REF_ID                                            = @MEETING_ROOM_REF_ID       
       AND 
          ISNULL(CONVERT(varchar(19), UPDATED_DTTM, 120), '')             = ISNULL(CONVERT(varchar(19), @UPDATED_DTTM, 120), '')
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error updating MeetingRoomRef record.', 18,1)

     IF @ReturnValue = 0
          RAISERROR('Data was changed/deleted prior to update.', 18,100)

RETURN
GO