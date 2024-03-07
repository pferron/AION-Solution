/****** Object:  StoredProcedure [AION].[usp_select_aion_meeting_room_ref_get_list]    Script Date: 8/11/2020 2:22:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************************************************************
* Object:       usp_select_aion_meeting_room_ref_get_list
* Description:  Retrieves MeetingRoomRef list for given parameter(s).
* Parameters:   
*               @identity                                                   int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval.
*               This proc expects id_person and/or id_file to generate list; modify as necessary.
*               Include ORDER BY clause as necessary.
* Version:      1.0
* Created by:   AION_user
* Created:      3/19/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 3/19/2020    AION_user     Auto-generated
* 05/03/2021   jallen        Add user principal name and calendar id
***********************************************************************************************************************/

ALTER PROCEDURE [AION].[usp_select_aion_meeting_room_ref_get_list]
@MEETING_TYPE   Varchar(255)

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

       FROM MEETING_ROOM_REF where @MEETING_TYPE IS NULL 
	   OR MEETING_ROOM_REF_ID in(select MEETING_ROOM_REF_ID from [AION].[MEETING_ROOM_TYPE_REF] 
			WHERE UPPER([MEETING_ROOM_TYP_NM]) = UPPER(@MEETING_TYPE) AND ACTIVE_IND = 1)

RETURN

