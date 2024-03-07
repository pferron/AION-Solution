
/***********************************************************************************************************************    
* Object:       usp_update_aion_user    
* Description:  Updates User record using supplied parameters.    
* Parameters:       
                                                @USER_ID                        INT, 
                                               @UPDATED_DTTM                   DATETIME, 
                                               @FIRST_NM                       VARCHAR(100), 
                                               @LAST_NM                        VARCHAR(100), 
                                               @EXTERNAL_SYSTEM_REF_ID         INT, 
                                               @SRC_SYSTEM_VAL_TXT             VARCHAR(255), 
                                               @ACTIVE_IND                     BIT, 
                                               @USER_INTERFACE_SETTING_TXT     VARCHAR(8000), 
                                               @IS_EXPRESS_SCHEDULED_IND       BIT, 
                                               @USER_NM                        VARCHAR(100), 
                                               @LAN_ID_TXT                     VARCHAR(20), 
                                               @PHONE_NUM                      VARCHAR(20), 
                                               @EMAIL_ADDR_TXT                 VARCHAR(100), 
                                               @NOTES_TXT                      VARCHAR(8000), 
                                               @IS_SCHEDULABLE_IND             BIT, 
                                               @PLAN_REVIEW_OVERRIDE_HOURS_NBR DECIMAL(4,2), 
                                               @HOURS_ESTIMATED_DESC           VARCHAR(50), 
                                               @JURISDICTION_TYP_ID            INT, 
                                               @SCHEDULABLE_LVL_DESC           VARCHAR(50), 
                                               @WKR_ID_TXT                     VARCHAR(100), 
                                               @IS_PRELIM_MEETING_ALLOWED_IND  BIT, 
                                               @ReturnValue                    INT OUTPUT
* Returns:      Number of rows affected.    
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.    
* Version:      1.0    
* Created by:   AION_user    
* Created:      10/10/2019    
************************************************************************************************************************    
* Change History: Date, Name, Description    
* 10/10/2019    AION_user     Auto-generated    
* 12/10/2019 jeanine  add ui setting field          
* 04/23/2020    jlindsay add IsExpressSched    
* 07/24/2020    jlindsay    update @PLAN_REVIEW_OVERRIDE_HOURS_NBR param to DECIMAL(4,2)
***********************************************************************************************************************/

ALTER PROCEDURE [AION].[usp_update_aion_user] @USER_ID                        INT, 
                                               @UPDATED_DTTM                   DATETIME, 
                                               @FIRST_NM                       VARCHAR(100), 
                                               @LAST_NM                        VARCHAR(100), 
                                               @EXTERNAL_SYSTEM_REF_ID         INT, 
                                               @SRC_SYSTEM_VAL_TXT             VARCHAR(255), 
                                               @ACTIVE_IND                     BIT, 
                                               @USER_INTERFACE_SETTING_TXT     VARCHAR(8000), 
                                               @IS_EXPRESS_SCHEDULED_IND       BIT, 
                                               @USER_NM                        VARCHAR(100), 
                                               @LAN_ID_TXT                     VARCHAR(20), 
                                               @PHONE_NUM                      VARCHAR(20), 
                                               @EMAIL_ADDR_TXT                 VARCHAR(100), 
                                               @NOTES_TXT                      VARCHAR(8000), 
                                               @IS_SCHEDULABLE_IND             BIT, 
                                               @PLAN_REVIEW_OVERRIDE_HOURS_NBR DECIMAL(4,2), 
                                               @HOURS_ESTIMATED_DESC           VARCHAR(50), 
                                               @JURISDICTION_TYP_ID            INT, 
                                               @SCHEDULABLE_LVL_DESC           VARCHAR(50), 
                                               @WKR_ID_TXT                     VARCHAR(100), 
                                               @IS_PRELIM_MEETING_ALLOWED_IND  BIT, 
                                               @ReturnValue                    INT OUTPUT
AS
     DECLARE @error INT;
     UPDATE [AION].[USER]
       SET 
           FIRST_NM = @FIRST_NM, 
           LAST_NM = @LAST_NM, 
           EXTERNAL_SYSTEM_REF_ID = @EXTERNAL_SYSTEM_REF_ID, 
           SRC_SYSTEM_VAL_TXT = @SRC_SYSTEM_VAL_TXT, 
           WKR_ID_UPDATED_TXT = @WKR_ID_TXT, 
           UPDATED_DTTM = GETDATE(), 
           ACTIVE_IND = @ACTIVE_IND, 
           USER_INTERFACE_SETTING_TXT = @USER_INTERFACE_SETTING_TXT, 
           IS_EXPRESS_SCHEDULED_IND = @IS_EXPRESS_SCHEDULED_IND, 
           USER_NM = @USER_NM, 
           LAN_ID_TXT = @LAN_ID_TXT, 
           PHONE_NUM = @PHONE_NUM, 
           EMAIL_ADDR_TXT = @EMAIL_ADDR_TXT, 
           NOTES_TXT = @NOTES_TXT, 
           IS_SCHEDULABLE_IND = @IS_SCHEDULABLE_IND, 
           PLAN_REVIEW_OVERRIDE_HOURS_NBR = @PLAN_REVIEW_OVERRIDE_HOURS_NBR, 
           HOURS_ESTIMATED_DESC = @HOURS_ESTIMATED_DESC, 
           JURISDICTION_TYP_ID = @JURISDICTION_TYP_ID, 
           SCHEDULABLE_LVL_DESC = @SCHEDULABLE_LVL_DESC, 
           IS_PRELIM_MEETING_ALLOWED_IND = @IS_PRELIM_MEETING_ALLOWED_IND
     WHERE USER_ID = @USER_ID
           AND ISNULL(CONVERT(VARCHAR(19), UPDATED_DTTM, 120), '') = ISNULL(CONVERT(VARCHAR(19), @UPDATED_DTTM, 120), '');
     SELECT @error = @@ERROR, 
            @ReturnValue = @@ROWCOUNT;
     IF @error != 0
         RAISERROR('Error updating User record.', 18, 1);
     IF @ReturnValue = 0
         RAISERROR('Data was changed/deleted prior to update.', 18, 100);
     RETURN;