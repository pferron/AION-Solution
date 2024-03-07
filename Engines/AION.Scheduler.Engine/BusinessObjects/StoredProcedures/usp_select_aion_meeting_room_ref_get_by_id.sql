
/***********************************************************************************************************************
* Object:       usp_select_aion_meeting_room_ref_get_by_id
* Description:  Retrieves MeetingRoomRef record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables,
*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause.
* Version:      1.0
* Created by:   AION_user
* Created:      3/19/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 3/19/2020    AION_user     Auto-generated
* 05/03/2021   jallen        Add user principal name and calendar id
***********************************************************************************************************************/

alter PROCEDURE [AION].[usp_select_aion_meeting_room_ref_get_by_id]

    @identity                                                    int

AS

       SELECT 
            MEETING_ROOM_REF_ID
          , MEETING_ROOM_NM
          , ACTIVE_IND
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          , MEETING_ROOM_EMAIL_ADDR_TXT
          , USER_PRINCIPAL_NM
          , CALENDAR_ID

       FROM MEETING_ROOM_REF

       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          MEETING_ROOM_REF_ID = @identity
          

RETURN

GO