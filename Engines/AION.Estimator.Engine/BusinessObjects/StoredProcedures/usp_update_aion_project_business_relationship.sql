  
/***********************************************************************************************************************  
* Object:       usp_update_aion_project_business_relationship  
* Description:  Updates ProjectBusinessRelationship record using supplied parameters.  
* Parameters:     
*               @PROJECT_BUSINESS_RELATIONSHIP_ID                            int  
*               @ESTIMATION_HOURS_NBR                                        decimal(15,7)  
*               @BUSINESS_REF_ID                                             int  
*               @PROJECT_ID                                                  int  
*               @UPDATED_DTTM                                                datetime  
*               @ASSIGNED_PLAN_REVIEWER_ID                                   int  
*               @PROPOSED_PLAN_REVIEWER_ID                                   int  
*               @SECONDARY_PLAN_REVIEWER_ID                                  int  
*               @PRI_PLAN_REVIEWER_ID                                        int  
*               @WKR_ID_TXT                                                  varchar(100)  
*               @PROJECT_BUSINESS_RELATIONSHIP_STATUS_DESC                   varchar(8000)
*               @ESTIMATION_NOT_APPLICABLE_IND                               bit
*               @PROJECT_STATUS_REF_ID                                       int
*               @ACTUAL_HOURS_NBR                                            decimal(15,7)  
*
*  
* Returns:      Number of rows affected.  
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.  
* Version:      1.0  
* Created by:   AION_user  
* Created:      10/10/2019  
************************************************************************************************************************  
* Change History: Date, Name, Description  
* 10/10/2019    AION_user     Auto-generated  
* 11/13/2019 jeanine  change estimation hours to decimal  
* 12/11/2019 jeanine  add status  
* 02/10/2020 jeanine add @PROJECT_STATUS_REF_ID                              int
* 02/13/2023 jallen add @ACTUAL_HOURS_NBR
***********************************************************************************************************************/  
  
ALTER PROCEDURE [usp_update_aion_project_business_relationship]  
  
    @PROJECT_BUSINESS_RELATIONSHIP_ID                            int  
  , @ESTIMATION_HOURS_NBR                                        decimal(15,7)  
  , @BUSINESS_REF_ID                                             int  
  , @PROJECT_ID                                                  int  
  , @UPDATED_DTTM                                                datetime  
  , @ASSIGNED_PLAN_REVIEWER_ID                                   int  
  , @PROPOSED_PLAN_REVIEWER_ID                                   int  
  , @SECONDARY_PLAN_REVIEWER_ID                                  int  
  , @PRI_PLAN_REVIEWER_ID                                        int  
  , @WKR_ID_TXT                                                  varchar(100)  
  , @PROJECT_BUSINESS_RELATIONSHIP_STATUS_DESC                   varchar(8000) = ''
  , @ESTIMATION_NOT_APPLICABLE_IND                               bit = 0
  , @PROJECT_STATUS_REF_ID                                       int
  , @ACTUAL_HOURS_NBR                                            decimal(15,7)  = NULL
  , @ReturnValue                                                 int OUTPUT  
  
AS  
  
     DECLARE @error   int  
  
       UPDATE PROJECT_BUSINESS_RELATIONSHIP  
       SET  
            ESTIMATION_HOURS_NBR                                         = @ESTIMATION_HOURS_NBR  
          , BUSINESS_REF_ID                                              = @BUSINESS_REF_ID  
          , PROJECT_ID                                                   = @PROJECT_ID  
          , WKR_ID_UPDATED_TXT                                           = @WKR_ID_TXT  
          , UPDATED_DTTM                                                 = GETDATE()  
          , ASSIGNED_PLAN_REVIEWER_ID                                    = @ASSIGNED_PLAN_REVIEWER_ID  
          , PROPOSED_PLAN_REVIEWER_ID                                    = @PROPOSED_PLAN_REVIEWER_ID  
          , SECONDARY_PLAN_REVIEWER_ID                                   = @SECONDARY_PLAN_REVIEWER_ID  
          , PRI_PLAN_REVIEWER_ID                                         = @PRI_PLAN_REVIEWER_ID  
          , PROJECT_BUSINESS_RELATIONSHIP_STATUS_DESC                    = @PROJECT_BUSINESS_RELATIONSHIP_STATUS_DESC  
          , ESTIMATION_NOT_APPLICABLE_IND                                = @ESTIMATION_NOT_APPLICABLE_IND
          , PROJECT_STATUS_REF_ID                                        = @PROJECT_STATUS_REF_ID       
          , ACTUAL_HOURS_NBR                                             = ISNULL(@ACTUAL_HOURS_NBR, ACTUAL_HOURS_NBR)
       WHERE  
          PROJECT_BUSINESS_RELATIONSHIP_ID                               = @PROJECT_BUSINESS_RELATIONSHIP_ID         
       AND   
          ISNULL(CONVERT(varchar(19), UPDATED_DTTM, 120), '')             = ISNULL(CONVERT(varchar(19), @UPDATED_DTTM, 120), '')  
            
  
     SELECT @error = @@ERROR  
          , @ReturnValue = @@ROWCOUNT  
  
     IF @error != 0  
          RAISERROR('Error updating ProjectBusinessRelationship record.', 18,1)  
  
     IF @ReturnValue = 0  
          RAISERROR('Data was changed/deleted prior to update.', 18,100)  
  
RETURN  