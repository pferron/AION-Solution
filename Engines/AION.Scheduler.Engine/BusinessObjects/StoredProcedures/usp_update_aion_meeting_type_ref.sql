
/***********************************************************************************************************************
* Object:       usp_update_aion_meeting_type_ref
* Description:  Updates MeetingTypeRef record using supplied parameters.
* Parameters:   
*               @MEETING_TYP_REF_ID                                          int
*               @MEETING_TYP_DESC                                            varchar(100)
*               @ENUM_MAPPING_VAL_NBR                                        int
*               @ACTIVE_IND                                                  bit
*               @UPDATED_DTTM                                                datetime
*               @WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Number of rows affected.
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.
* Version:      1.0
* Created by:   AION_user
* Created:      10/7/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/7/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_update_aion_meeting_type_ref]

    @MEETING_TYP_REF_ID                                          int
  , @MEETING_TYP_DESC                                            varchar(100)
  , @ENUM_MAPPING_VAL_NBR                                        int
  , @ACTIVE_IND                                                  bit
  , @UPDATED_DTTM                                                datetime
  , @WKR_ID_TXT                                                  varchar(100)

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       UPDATE MEETING_TYPE_REF
       SET
            MEETING_TYP_DESC                                             = @MEETING_TYP_DESC
          , ENUM_MAPPING_VAL_NBR                                         = @ENUM_MAPPING_VAL_NBR
          , ACTIVE_IND                                                   = @ACTIVE_IND
          , WKR_ID_UPDATED_TXT                                           = @WKR_ID_TXT
          , UPDATED_DTTM                                                 = GETDATE()

       WHERE
          MEETING_TYP_REF_ID                                             = @MEETING_TYP_REF_ID       
       AND 
          ISNULL(CONVERT(varchar(19), UPDATED_DTTM, 120), '')             = ISNULL(CONVERT(varchar(19), @UPDATED_DTTM, 120), '')
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error updating MeetingTypeRef record.', 18,1)

     IF @ReturnValue = 0
          RAISERROR('Data was changed/deleted prior to update.', 18,100)

RETURN
GO