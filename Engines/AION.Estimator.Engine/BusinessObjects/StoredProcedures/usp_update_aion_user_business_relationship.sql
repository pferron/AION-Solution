/****** Object:  StoredProcedure [AION].[usp_update_aion_user_business_relationship]    Script Date: 12/10/2019 1:59:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************************************************************
* Object:       usp_update_aion_user_business_relationship
* Description:  Updates UserBusinessRelationship record using supplied parameters.
* Parameters:   
*               @USER_BUSINESS_RELATIONSHIP_ID                               int
*               @USER_ID                                                     int
*               @BUSINESS_REF_ID                                             int
*               @UPDATED_DTTM                                                datetime
*               @WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Number of rows affected.
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.
* Version:      1.0
* Created by:   AION_user
* Created:      10/10/2019
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/10/2019    AION_user     Auto-generated
* 
***********************************************************************************************************************/

ALTER PROCEDURE [AION].[usp_update_aion_user_business_relationship]

    @USER_BUSINESS_RELATIONSHIP_ID                               int
  , @USER_ID                                                     int
  , @BUSINESS_REF_ID                                             int
  , @UPDATED_DTTM                                                datetime
  , @WKR_ID_TXT                                                  varchar(100)

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       UPDATE USER_BUSINESS_RELATIONSHIP
       SET
            USER_ID                                                      = @USER_ID
          , BUSINESS_REF_ID                                              = @BUSINESS_REF_ID
          , WKR_ID_UPDATED_TXT                                           = @WKR_ID_TXT
          , UPDATED_DTTM                                                 = GETDATE()

       WHERE
          USER_BUSINESS_RELATIONSHIP_ID                                  = @USER_BUSINESS_RELATIONSHIP_ID       
       AND 
          ISNULL(CONVERT(varchar(19), UPDATED_DTTM, 120), '')             = ISNULL(CONVERT(varchar(19), @UPDATED_DTTM, 120), '')
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error updating UserBusinessRelationship record.', 18,1)

     IF @ReturnValue = 0
          RAISERROR('Data was changed/deleted prior to update.', 18,100)

RETURN
