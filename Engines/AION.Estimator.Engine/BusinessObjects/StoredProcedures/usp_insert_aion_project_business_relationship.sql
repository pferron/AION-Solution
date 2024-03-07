  
      
/***********************************************************************************************************************      
* Object: usp_insert_aion_project_business_relationship      
* Description: Inserts ProjectBusinessRelationship record.      
* Parameters:      
*  @ESTIMATION_HOURS_NBR                                        decimal(15,7)      
*  @BUSINESS_REF_ID                                             int      
*  @PROJECT_ID                                                  int      
*  @ASSIGNED_PLAN_REVIEWER_ID                                   int      
*  @PROPOSED_PLAN_REVIEWER_ID                                   int      
*  @SECONDARY_PLAN_REVIEWER_ID                                  int      
*  @PRI_PLAN_REVIEWER_ID                                        int      
*  @WKR_ID_TXT                                                  varchar(100)      
*  @PROJECT_BUSINESS_RELATIONSHIP_STATUS_DESC     varchar(8000)    
*  @ESTIMATION_NOT_APPLICABLE_IND                   bit  
*  @PROJECT_STATUS_REF_ID                              int  
*  @IS_DEPT_REQUESTED_IND                       BIT
*  @ACTUAL_HOURS_NBR                                          decimal(15,7)    
* Returns:      Identity column of new record.      
* Comments:     If CREATED_DTTM and/or UPDATED_DTTM fields exist in table, GETDATE() is inserted.      
*               Worker ID will be inserted for WKR_ID_CREATED_TXT and WKR_ID_UPDATED_TXT fields if they exist.      
* Version:      1.0      
* Created by:   AION_user      
* Created:      10/10/2019      
************************************************************************************************************************      
* Change History: Date, Name, Description      
* 10/10/2019    AION_user     Auto-generated      
* 11/13/2019 jeanine  change estimation hours to decimal      
* 12/11/2019 jeanine add status    
* 12/12/2019 jeanine add ESTIMATION_NOT_APPLICABLE_IND  
* 2/10/2020  jeanine add @PROJECT_STATUS_REF_ID                              int  
* 6/22/2020  jeanine add @IS_DEPT_REQUESTED_IND bool
* 2/13/2023  jallen  add @ACTUAL_HOURS_NBR
***********************************************************************************************************************/      
      
ALTER PROCEDURE [AION].[usp_insert_aion_project_business_relationship]      
    @ESTIMATION_HOURS_NBR                                        decimal(15,7)      
  , @BUSINESS_REF_ID                                             int      
  , @PROJECT_ID                                                  int      
  , @ASSIGNED_PLAN_REVIEWER_ID                                   int      
  , @PROPOSED_PLAN_REVIEWER_ID                                   int      
  , @SECONDARY_PLAN_REVIEWER_ID                                  int      
  , @PRI_PLAN_REVIEWER_ID                                        int      
  , @WKR_ID_TXT                                                  varchar(100)      
  , @PROJECT_BUSINESS_RELATIONSHIP_STATUS_DESC                   varchar(8000) = ''  
  , @ESTIMATION_NOT_APPLICABLE_IND                               bit = 0  
  , @PROJECT_STATUS_REF_ID                                       int  
  , @IS_DEPT_REQUESTED_IND                                       bit = 0
  , @ACTUAL_HOURS_NBR                                            decimal(15,7) = NULL
  , @ReturnValue                                                 int OUTPUT      
      
AS      
      
     DECLARE @error   int      
      
     INSERT INTO PROJECT_BUSINESS_RELATIONSHIP      
          (      
            ESTIMATION_HOURS_NBR      
          , BUSINESS_REF_ID      
          , PROJECT_ID      
          , WKR_ID_CREATED_TXT      
          , CREATED_DTTM      
          , WKR_ID_UPDATED_TXT      
          , UPDATED_DTTM      
          , ASSIGNED_PLAN_REVIEWER_ID      
          , PROPOSED_PLAN_REVIEWER_ID      
          , SECONDARY_PLAN_REVIEWER_ID      
          , PRI_PLAN_REVIEWER_ID      
          , PROJECT_BUSINESS_RELATIONSHIP_STATUS_DESC    
          , ESTIMATION_NOT_APPLICABLE_IND  
          , PROJECT_STATUS_REF_ID   
          , IS_DEPT_REQUESTED_IND 
          , ACTUAL_HOURS_NBR
          )      
     VALUES      
          (      
            @ESTIMATION_HOURS_NBR      
          , @BUSINESS_REF_ID      
          , @PROJECT_ID      
          , @WKR_ID_TXT      
          , GETDATE()      
          , @WKR_ID_TXT      
          , GETDATE()      
          , @ASSIGNED_PLAN_REVIEWER_ID      
          , @PROPOSED_PLAN_REVIEWER_ID      
          , @SECONDARY_PLAN_REVIEWER_ID      
          , @PRI_PLAN_REVIEWER_ID      
          , @PROJECT_BUSINESS_RELATIONSHIP_STATUS_DESC    
          , @ESTIMATION_NOT_APPLICABLE_IND  
          , @PROJECT_STATUS_REF_ID    
          , @IS_DEPT_REQUESTED_IND   
          , ISNULL(@ACTUAL_HOURS_NBR,0)
          )      
      
     SELECT @error = @@ERROR      
          , @ReturnValue = SCOPE_IDENTITY()      
      
     IF @error != 0      
          RAISERROR('Error adding ProjectBusinessRelationship record.', 18,1)      
      
RETURN   