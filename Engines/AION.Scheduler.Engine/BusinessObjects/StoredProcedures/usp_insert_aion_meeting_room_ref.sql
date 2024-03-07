
/***********************************************************************************************************************
* Object:	usp_insert_aion_meeting_room_ref
* Description:	Inserts MeetingRoomRef record.
* Parameters:
*		@MEETING_ROOM_REF_ID                                         int
*		@MEETING_ROOM_NM                                             varchar(50)
*		@ACTIVE_IND                                                  bit
*		@WKR_ID_TXT                                                  varchar(100)
*
* Returns:      N/A
* Comments:     If CREATED_DTTM and/or UPDATED_DTTM fields exist in table, GETDATE() is inserted.
*               Worker ID will be inserted for WKR_ID_CREATED_TXT and WKR_ID_UPDATED_TXT fields if they exist.
* Version:      1.0
* Created by:   AION_user
* Created:      3/19/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 3/19/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [AION].[usp_insert_aion_meeting_room_ref]
    @MEETING_ROOM_REF_ID                                         int
  , @MEETING_ROOM_NM                                             varchar(50)
  , @ACTIVE_IND                                                  bit
  , @WKR_ID_TXT                                                  varchar(100)

AS

     DECLARE @error   int

     INSERT INTO MEETING_ROOM_REF
          (
            MEETING_ROOM_REF_ID
          , MEETING_ROOM_NM
          , ACTIVE_IND
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          )
     VALUES
          (
            @MEETING_ROOM_REF_ID
          , @MEETING_ROOM_NM
          , @ACTIVE_IND
          , @WKR_ID_TXT
          , GETDATE()
          , @WKR_ID_TXT
          , GETDATE()
          )

     SELECT @error = @@ERROR
     IF @error != 0
          RAISERROR('Error adding MeetingRoomRef record.', 18,1)

RETURN
GO