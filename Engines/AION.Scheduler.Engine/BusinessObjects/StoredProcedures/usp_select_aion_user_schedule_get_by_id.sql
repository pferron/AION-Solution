  
/***********************************************************************************************************************  
* Object:       usp_select_aion_user_schedule_get_by_id  
* Description:  Retrieves UserSchedule record for given key field(s).  
* Parameters:     
*               @identity                                                    int  
*  
* Returns:      Recordset.  
* Comments:     Developer may need to manually join to other tables, such as code tables,  
*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause.  
* Version:      1.0  
* Created by:   AION_user  
* Created:      3/19/2020  
************************************************************************************************************************  
* Change History: Date, Name, Description  
* 3/19/2020    AION_user     Auto-generated  
* 1/26/2021 jlindsay    remove user_business_relationship_id  
***********************************************************************************************************************/  
  
ALTER PROCEDURE [AION].[usp_select_aion_user_schedule_get_by_id]  
  
    @identity                                                    int  
  
AS  
  
       SELECT   
            USER_SCHEDULE_ID  
          , START_DTTM  
          , END_DTTM  
          , PROJECT_SCHEDULE_ID  
          , USER_ID  
          , WKR_ID_CREATED_TXT  
          , CREATED_DTTM  
          , WKR_ID_UPDATED_TXT  
          , UPDATED_DTTM  
  
       FROM USER_SCHEDULE  
  
       WHERE  
          
       -- @TODO:  Correct the following as necessary  
          
          USER_SCHEDULE_ID = @identity  
            
  
RETURN  
  