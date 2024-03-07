
/***********************************************************************************************************************
* Object:	usp_insert_aion_meeting_type_ref
* Description:	Inserts MeetingTypeRef record.
* Parameters:
*		@MEETING_TYP_REF_ID                                          int
*		@MEETING_TYP_DESC                                            varchar(100)
*		@ENUM_MAPPING_VAL_NBR                                        int
*		@ACTIVE_IND                                                  bit
*		@WKR_ID_TXT                                                  varchar(100)
*
* Returns:      N/A
* Comments:     If CREATED_DTTM and/or UPDATED_DTTM fields exist in table, GETDATE() is inserted.
*               Worker ID will be inserted for WKR_ID_CREATED_TXT and WKR_ID_UPDATED_TXT fields if they exist.
* Version:      1.0
* Created by:   AION_user
* Created:      10/7/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/7/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_insert_aion_meeting_type_ref]
    @MEETING_TYP_REF_ID                                          int
  , @MEETING_TYP_DESC                                            varchar(100)
  , @ENUM_MAPPING_VAL_NBR                                        int
  , @ACTIVE_IND                                                  bit
  , @WKR_ID_TXT                                                  varchar(100)

AS

     DECLARE @error   int

     INSERT INTO MEETING_TYPE_REF
          (
            MEETING_TYP_REF_ID
          , MEETING_TYP_DESC
          , ENUM_MAPPING_VAL_NBR
          , ACTIVE_IND
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          )
     VALUES
          (
            @MEETING_TYP_REF_ID
          , @MEETING_TYP_DESC
          , @ENUM_MAPPING_VAL_NBR
          , @ACTIVE_IND
          , @WKR_ID_TXT
          , GETDATE()
          , @WKR_ID_TXT
          , GETDATE()
          )

     SELECT @error = @@ERROR
     IF @error != 0
          RAISERROR('Error adding MeetingTypeRef record.', 18,1)

RETURN
GO