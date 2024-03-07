    
/***********************************************************************************************************************    
* Object:       usp_select_aion_project_business_relationship_get_list    
* Description:  Retrieves ProjectBusinessRelationship list for given parameter(s).    
* Parameters:       
*               @identity                                                   int    
*    
* Returns:      Recordset.    
* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval.    
*               This proc expects id_person and/or id_file to generate list; modify as necessary.    
*               Include ORDER BY clause as necessary.    
* Version:      1.0    
* Created by:   AION_user    
* Created:      10/10/2019    
************************************************************************************************************************    
* Change History: Date, Name, Description    
* 10/10/2019    AION_user     Auto-generated    
* 12/11/2019 jeanine  add status    
* 12/12/2019 jeanine add ESTIMATION_NOT_APPLICABLE_IND  
* 2/10/2020  jeanine add PROJECT_STATUS_REF_ID                              int     
* 6/22/2020  jeanine add IS_DEPT_REQUESTED_IND bool
* 2/13/2023  jallen add ACTUAL_HOURS_NBR
***********************************************************************************************************************/    
    
ALTER PROCEDURE [usp_select_aion_project_business_relationship_get_list]    
    
    @identity                                                   int    
    
AS    
    
       SELECT     
            PROJECT_BUSINESS_RELATIONSHIP_ID    
          , ESTIMATION_HOURS_NBR    
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
       FROM PROJECT_BUSINESS_RELATIONSHIP    
    
       WHERE    
            
       -- @TODO:  Correct the following as necessary    
            
          PROJECT_BUSINESS_RELATIONSHIP_ID = @identity    
              
    
RETURN    