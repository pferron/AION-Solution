/****** Object:  StoredProcedure [AION].[USP_select_GetUsersListbySystemRole]    Script Date: 5/5/2020 9:42:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************************************************************  
* Object:       USP_select_GetUsersListbySystemRole  
* Description:  select User record using supplied parameters.  
* Parameters:     
*               @SystemRoleName                                                     nvarchar(50)  
*               @GetInactiveUsers                                                    int  
* Returns:      selected  
* Version:      1.0  
* Created by:     
* Created:        
************************************************************************************************************************  
* Change History: Date, Name, Description  
* 12/10/2019 jeanine  add ui setting field      
* 04/23/2020    jlindsay add IsExpressSched  
* 04/29/2020 gnadimpall  add IsSchedulable
***********************************************************************************************************************/

ALTER PROCEDURE [AION].[USP_select_GetUsersListbySystemRole] @SystemRoleName   NVARCHAR(50), 
                                                             @GetInactiveUsers INT          = '0'
AS
     SELECT us.USER_ID, 
            us.FIRST_NM, 
            us.LAST_NM, 
            us.EXTERNAL_SYSTEM_REF_ID, 
            us.SRC_SYSTEM_VAL_TXT, 
            us.WKR_ID_CREATED_TXT, 
            us.CREATED_DTTM, 
            us.WKR_ID_UPDATED_TXT, 
            us.UPDATED_DTTM, 
            us.ACTIVE_IND, 
            us.USER_INTERFACE_SETTING_TXT, 
            IS_EXPRESS_SCHEDULED_IND,
			IS_SCHEDULABLE_IND,
			USER_NM,
			LAN_ID_TXT,
			PHONE_NUM,
			EMAIL_ADDR_TXT,
			NOTES_TXT,
			PLAN_REVIEW_OVERRIDE_HOURS_NBR,
			HOURS_ESTIMATED_DESC,
			SCHEDULABLE_LVL_DESC,
			JURISDICTION_TYP_ID,
            IS_PRELIM_MEETING_ALLOWED_IND

     FROM [AION].[USER] AS us
          RIGHT JOIN [AION].[USER_SYSTEM_ROLE_RELATIONSHIP] AS usr ON us.USER_ID = usr.USER_ID
          RIGHT JOIN [AION].[SYSTEM_ROLE] AS sr ON sr.SYSTEM_ROLE_ID = usr.SYSTEM_ROLE_ID
     WHERE sr.SYSTEM_ROLE_NM = @SystemRoleName   
           --shijo adding check for optionally adding inacitive users or else get only active users.  
           AND (us.ACTIVE_IND = 1
                OR @GetInactiveUsers = 1);