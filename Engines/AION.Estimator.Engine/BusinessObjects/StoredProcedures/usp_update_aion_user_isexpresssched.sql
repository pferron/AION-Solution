
/***********************************************************************************************************************  
* Object:       usp_update_aion_user_isexpresssched
* Description:  Updates User record using supplied parameters.  
* Parameters:     
*               @USER_ID                                                     int  
*               @UPDATED_DTTM                                                datetime  
*               @WKR_ID_TXT                                                  varchar(100)  
*               @IS_EXPRESS_SCHEDULED_IND                                   BIT,
* Returns:      Number of rows affected.  
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.  
* Version:      1.0  
* Created by:   jlindsay  
* Created:      04/23/2020  
************************************************************************************************************************  
* Change History: Date, Name, Description  
* 10/10/2019    AION_user     Auto-generated  
* 12/10/2019 jeanine  add ui setting field        
* 04/23/2020    jlindsay add IsExpressSched  
***********************************************************************************************************************/

CREATE PROCEDURE [usp_update_aion_user_isexpresssched] @USER_ID                  INT, 
                                       @UPDATED_DTTM             DATETIME, 
                                       @WKR_ID_TXT               VARCHAR(100), 
                                       @IS_EXPRESS_SCHEDULED_IND BIT, 
                                       @ReturnValue              INT OUTPUT
AS
     DECLARE @error INT;
     UPDATE [AION].[USER]
       SET 
           WKR_ID_UPDATED_TXT = @WKR_ID_TXT, 
           UPDATED_DTTM = GETDATE(), 
           IS_EXPRESS_SCHEDULED_IND = @IS_EXPRESS_SCHEDULED_IND
     WHERE [USER_ID] = @USER_ID
           AND ISNULL(CONVERT(VARCHAR(19), UPDATED_DTTM, 120), '') = ISNULL(CONVERT(VARCHAR(19), @UPDATED_DTTM, 120), '');
     SELECT @error = @@ERROR, 
            @ReturnValue = @@ROWCOUNT;
     IF @error != 0
         RAISERROR('Error updating User record.', 18, 1);
     IF @ReturnValue = 0
         RAISERROR('Data was changed/deleted prior to update.', 18, 100);
     RETURN;