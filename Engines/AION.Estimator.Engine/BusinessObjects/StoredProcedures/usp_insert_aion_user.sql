/****** Object:  StoredProcedure [AION].[usp_insert_aion_user]    Script Date: 5/8/2020 12:14:03 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************************************************************
* Object:	usp_insert_aion_user
* Description:	Inserts User record.
* Parameters:
*		@FIRST_NM                                                    varchar(100)
*		@LAST_NM                                                     varchar(100)
*		@EXTERNAL_SYSTEM_REF_ID                                      int
*		@SRC_SYSTEM_VAL_TXT                                          varchar(255)
*		@WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Identity column of new record.
* Comments:     If CREATED_DTTM and/or UPDATED_DTTM fields exist in table, GETDATE() is inserted.
*               Worker ID will be inserted for WKR_ID_CREATED_TXT and WKR_ID_UPDATED_TXT fields if they exist.
* Version:      1.0
* Created by:   AION_user
* Created:      10/10/2019
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/10/2019    AION_user     Auto-generated
* 11/20/2019    Shijo         Adding new colum to keep user active or inactive instead of deletion.  
***********************************************************************************************************************/

ALTER PROCEDURE [AION].[usp_insert_aion_user]
    @FIRST_NM                                                    varchar(100)
  , @LAST_NM                                                     varchar(100)
  , @EXTERNAL_SYSTEM_REF_ID                                      int
  , @SRC_SYSTEM_VAL_TXT                                          varchar(255)
  , @ACTIVE_IND                                                  bit
  , @USER_INTERFACE_SETTING_TXT                                  varchar(8000)
  , @IS_EXPRESS_SCHEDULED_IND                                    bit
  , @USER_NM                                                     varchar(100)
  , @LAN_ID_TXT                                                  varchar(20)
  , @PHONE_NUM                                                   varchar(20)
  , @EMAIL_ADDR_TXT                                              varchar(100)
  , @NOTES_TXT                                                   varchar(8000)
  , @IS_SCHEDULABLE_IND                                          bit
  , @PLAN_REVIEW_OVERRIDE_HOURS_NBR                              decimal
  , @HOURS_ESTIMATED_DESC                                        varchar(50)
  , @JURISDICTION_TYP_ID                                         int
  , @SCHEDULABLE_LVL_DESC                                        varchar(50)
  , @WKR_ID_TXT                                                  varchar(100)
  , @IS_PRELIM_MEETING_ALLOWED_IND                               bit
  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

     INSERT INTO [AION].[USER]
          (
            FIRST_NM
          , LAST_NM
          , EXTERNAL_SYSTEM_REF_ID
          , SRC_SYSTEM_VAL_TXT
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          , ACTIVE_IND
          , USER_INTERFACE_SETTING_TXT
          , IS_EXPRESS_SCHEDULED_IND
          , USER_NM
          , LAN_ID_TXT
          , PHONE_NUM
          , EMAIL_ADDR_TXT
          , NOTES_TXT
          , IS_SCHEDULABLE_IND
          , PLAN_REVIEW_OVERRIDE_HOURS_NBR
          , HOURS_ESTIMATED_DESC
          , JURISDICTION_TYP_ID
          , SCHEDULABLE_LVL_DESC
          , IS_PRELIM_MEETING_ALLOWED_IND
          )
     VALUES
          (
            @FIRST_NM
          , @LAST_NM
          , @EXTERNAL_SYSTEM_REF_ID
          , @SRC_SYSTEM_VAL_TXT
          , @WKR_ID_TXT
          , GETDATE()
          , @WKR_ID_TXT
          , GETDATE()
          , @ACTIVE_IND
          , @USER_INTERFACE_SETTING_TXT
          , @IS_EXPRESS_SCHEDULED_IND
          , @USER_NM
          , @LAN_ID_TXT
          , @PHONE_NUM
          , @EMAIL_ADDR_TXT
          , @NOTES_TXT
          , @IS_SCHEDULABLE_IND
          , @PLAN_REVIEW_OVERRIDE_HOURS_NBR
          , @HOURS_ESTIMATED_DESC
          , @JURISDICTION_TYP_ID
          , @SCHEDULABLE_LVL_DESC
          , @IS_PRELIM_MEETING_ALLOWED_IND
          )

     SELECT @error = @@ERROR
          , @ReturnValue = SCOPE_IDENTITY()

     IF @error != 0
          RAISERROR('Error adding User record.', 18,1)

RETURN
