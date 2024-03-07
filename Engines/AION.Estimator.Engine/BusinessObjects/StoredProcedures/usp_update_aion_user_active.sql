
SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

/***********************************************************************************************************************
* Object:       usp_update_aion_user_active
* Description:  Updates User record using supplied parameters.
* Parameters:   
*               @USER_ID                                                     int
*             , @ACTIVE_IND                                                bit
*             , @WKR_ID_TXT                                                  varchar(100)
*               , @UPDATED_DTTM                                                datetime
* Returns:      Number of rows affected.
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.
* Version:      1.0
* Created by:   jlindsay
* Created:      03/06/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 03/06/2020    jlindsay     created
* 
***********************************************************************************************************************/

CREATE PROCEDURE [AION].[usp_update_aion_user_active] @USER_ID      INT, 
                                              @UPDATED_DTTM DATETIME, 
                                              @ACTIVE_IND   BIT, 
                                              @WKR_ID_TXT   VARCHAR(100), 
                                              @ReturnValue  INT OUTPUT
AS
     DECLARE @error INT;
     --
     UPDATE [AION].[USER]
       SET 
           WKR_ID_UPDATED_TXT = @WKR_ID_TXT, 
           UPDATED_DTTM = GETDATE(), 
           ACTIVE_IND = @ACTIVE_IND
     WHERE [USER_ID] = @USER_ID
           AND ISNULL(CONVERT(VARCHAR(19), UPDATED_DTTM, 120), '') = ISNULL(CONVERT(VARCHAR(19), @UPDATED_DTTM, 120), '');
           --
     SELECT @error = @@ERROR, 
            @ReturnValue = @@ROWCOUNT;
            --
     IF @error != 0
         RAISERROR('Error updating User record.', 18, 1);
     IF @ReturnValue = 0
         RAISERROR('Data was changed/deleted prior to update.', 18, 100);
     RETURN;