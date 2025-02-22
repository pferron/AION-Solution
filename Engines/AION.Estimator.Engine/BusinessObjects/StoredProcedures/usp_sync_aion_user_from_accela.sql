/****** Object:  StoredProcedure [AION].[usp_sync_aion_user_from_accela]    Script Date: 5/8/2020 10:49:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************************************************************  
* Object:       usp_update_facilitators_v2  
* Description:  Updates User record using supplied parameters.  
* Parameters:     
*
*
* Returns:      Number of rows affected.  
* Comments:     
* Version:      1.0  
* Created by:    
* Created:      
************************************************************************************************************************  
* Change History: Date, Name, Description  
* 04/23/2020    jlindsay add IsExpressSched  
***********************************************************************************************************************/

ALTER PROCEDURE [AION].[usp_sync_aion_user_from_accela] 
													 @firstName                         VARCHAR(50) = NULL, 
                                                     @lastName                          VARCHAR(50) = NULL, 
                                                     @externalSystemRefId               INT         = NULL, 
                                                     @srcSystemValueTxt                 VARCHAR(50) = NULL, 
                                                     @userId                            INT         = NULL, 
													 @USER_NM                           varchar(100) = NULL,
													 @LAN_ID_TXT                        varchar(20) = NULL,
													 @PHONE_NUM                         varchar(20) = NULL,
													 @EMAIL_ADDR_TXT                    varchar(100) = NULL,
													 @NOTES_TXT                         varchar(8000) = NULL,
													 @IS_SCHEDULABLE_IND                bit = NULL,
													 @PLAN_REVIEW_OVERRIDE_HOURS_NBR    decimal = NULL,
													 @HOURS_ESTIMATED_DESC              varchar(50) = NULL,
													 @JURISDICTION_TYP_ID               int = NULL,
													 @SCHEDULABLE_LVL_DESC              varchar(50) = NULL,
                                                     @roleName                          VARCHAR(50), 
                                                     @operation                         VARCHAR(50), 
                                                     @IS_PRELIM_MEETING_ALLOWED_IND     bit = null,
                                                     @ReturnValue                       INT OUTPUT
AS
    BEGIN 
        --init vars  
        DECLARE @User_ID INT;
        DECLARE @System_Role_id INT;
        DECLARE @error INT;
        DECLARE @disableduserid INT= 0;
        DECLARE @FIRST_NM VARCHAR(100);
        DECLARE @LAST_NM VARCHAR(100);
        DECLARE @EXTERNAL_SYSTEM_REF_ID INT;
        DECLARE @SRC_SYSTEM_VAL_TXT VARCHAR(255);
        DECLARE @WKR_ID_TXT VARCHAR(100)= '1';--this is system  
        DECLARE @ACTIVE_IND INT;
        DECLARE @UPDATED_DTTM DATETIME;
        DECLARE @USER_INTERFACE_SETTING_TXT VARCHAR(8000);
        DECLARE @IS_EXPRESS_SCHEDULED_IND BIT;
        --  
        IF(@operation = 'add')
            BEGIN
                SELECT @disableduserid = MIN(USER_ID)
                FROM [AION].[USER]
                WHERE SRC_SYSTEM_VAL_TXT = @srcSystemValueTxt
                GROUP BY SRC_SYSTEM_VAL_TXT;
                IF(@disableduserid = 0)
                    BEGIN
                        SET @FIRST_NM = @firstName;
                        SET @LAST_NM = @lastName;
                        SET @EXTERNAL_SYSTEM_REF_ID = @externalSystemRefId;
                        SET @SRC_SYSTEM_VAL_TXT = @srcSystemValueTxt;
                        SET @WKR_ID_TXT = '1';--this is system  
                        SET @ACTIVE_IND = 0;  
                        --  
                        DECLARE @SYSTEM_ROLE_ReturnValue INT;  
                        --insert the user  
                        EXECUTE [AION].[usp_insert_aion_user] 
                                  @FIRST_NM
                                , @LAST_NM 
                                , @EXTERNAL_SYSTEM_REF_ID
                                , @SRC_SYSTEM_VAL_TXT
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
								, @WKR_ID_TXT
                                , @IS_PRELIM_MEETING_ALLOWED_IND
                                , @UserID OUTPUT;  
                        --get the user id   
                        SELECT @User_ID = @UserID;  
                        --  
                        --get the system role id  
                        SELECT @System_Role_id = SYSTEM_ROLE_ID
                        FROM [AION].[SYSTEM_ROLE]
                        WHERE SYSTEM_ROLE_NM = @roleName;  
                        --  
                        --insert the relationship  
                        EXECUTE [AION].[usp_insert_aion_user_system_role_relationship_base] 
                                @USER_ID, 
                                @SYSTEM_ROLE_ID, 
                                @WKR_ID_TXT, 
                                @SYSTEM_ROLE_ReturnValue OUTPUT;  
                        --  
                        SELECT @error = @@ERROR, 
                               @ReturnValue = @@ROWCOUNT;
                END;
                ELSE
                    BEGIN
                        SET @USER_ID = @disableduserid;
                        SET @FIRST_NM = @firstName;
                        SET @LAST_NM = @lastName;
                        SET @EXTERNAL_SYSTEM_REF_ID = @externalSystemRefId;
                        SET @ACTIVE_IND = 1;
                        SET @WKR_ID_TXT = '1'; --this is system  
                        --  
                        DECLARE @UpdateDisabledUserReturnValue INT;  
                        --get from db  
                        SELECT @UPDATED_DTTM = UPDATED_DTTM, 
                               @USER_INTERFACE_SETTING_TXT = USER_INTERFACE_SETTING_TXT, 
                               @SRC_SYSTEM_VAL_TXT = SRC_SYSTEM_VAL_TXT, 
                               @IS_EXPRESS_SCHEDULED_IND = IS_EXPRESS_SCHEDULED_IND
                        FROM [AION].[USER]
                        WHERE [USER_ID] = @USER_ID;  
                        --  
                        --update the user  
                        EXECUTE [AION].[usp_update_aion_user] 
                                  @USER_ID
								, @UPDATED_DTTM
                                , @FIRST_NM
                                , @LAST_NM
                                , @EXTERNAL_SYSTEM_REF_ID
                                , @SRC_SYSTEM_VAL_TXT
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
								, @WKR_ID_TXT
                                , @IS_PRELIM_MEETING_ALLOWED_IND
                                , @UpdateDisabledUserReturnValue OUTPUT;  
                        --  
                        SELECT @error = @@ERROR, 
                               @ReturnValue = @@ROWCOUNT;
                    END;
        END;
        IF(@operation = 'update')
            BEGIN
                SET @USER_ID = @userId;
                SET @FIRST_NM = @firstName;
                SET @LAST_NM = @lastName;
                SET @EXTERNAL_SYSTEM_REF_ID = @externalSystemRefId;
                SET @ACTIVE_IND = 1;
                SET @WKR_ID_TXT = '1'; --this is system  
                SET @SRC_SYSTEM_VAL_TXT = @srcSystemValueTxt;  
                --  
                DECLARE @UpdateUserReturnValue INT;  
                --get from db  
                SELECT @UPDATED_DTTM = UPDATED_DTTM, 
                       @USER_INTERFACE_SETTING_TXT = USER_INTERFACE_SETTING_TXT, 
                       @IS_EXPRESS_SCHEDULED_IND = IS_EXPRESS_SCHEDULED_IND
                FROM [AION].[USER]
                WHERE [USER_ID] = @USER_ID;  
                --  
               --update the user  
                        EXECUTE [AION].[usp_update_aion_user] 
                                  @USER_ID
								, @UPDATED_DTTM
                                , @FIRST_NM
                                , @LAST_NM
                                , @EXTERNAL_SYSTEM_REF_ID
                                , @SRC_SYSTEM_VAL_TXT
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
								, @WKR_ID_TXT
                                , @IS_PRELIM_MEETING_ALLOWED_IND
                                , @UpdateDisabledUserReturnValue OUTPUT;  
                --  
                SELECT @error = @@ERROR, 
                       @ReturnValue = @@ROWCOUNT;
        END;
        IF(@operation = 'delete')
            BEGIN
                SET @USER_ID = @UserId;
                SET @ACTIVE_IND = 0;
                SET @WKR_ID_TXT = '1'; --this is system  
                --  
                DECLARE @DisableUserReturnValue INT;  
                --get from db  
                --  
                SELECT @UPDATED_DTTM = UPDATED_DTTM
                FROM [AION].[USER]
                WHERE [USER_ID] = @USER_ID;  
                --  
                EXECUTE [AION].[usp_update_aion_user_active] 
                        @USER_ID, 
                        @UPDATED_DTTM, 
                        @ACTIVE_IND, 
                        @WKR_ID_TXT, 
                        @DisableUserReturnValue OUTPUT;  
                --  
                DECLARE @DeleteReturnValue INT;  
                --  
                EXECUTE [AION].[usp_delete_aion_user_system_role_relationship_base] 
                        @USER_ID, 
                        @DeleteReturnValue OUTPUT;

                --  
                SELECT @error = @@ERROR, 
                       @ReturnValue = @@ROWCOUNT;
        END;
        RETURN;
    END;