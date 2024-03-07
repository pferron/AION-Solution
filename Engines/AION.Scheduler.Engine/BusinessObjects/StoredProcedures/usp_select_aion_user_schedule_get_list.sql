  
/***********************************************************************************************************************  
* Object:       usp_select_aion_user_schedule_get_list  
* Description:  Retrieves UserSchedule list for given parameter(s).  
* Parameters:     
*               @identity                                                   int  
*  
* Returns:      Recordset.  
* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval.  
*               This proc expects id_person and/or id_file to generate list; modify as necessary.  
*               Include ORDER BY clause as necessary.  
* Version:      1.0  
* Created by:   AION_user  
* Created:      3/19/2020  
************************************************************************************************************************  
* Change History: Date, Name, Description  
* 3/19/2020    AION_user     Auto-generated  
* 1/26/2021 jlindsay    remove user_business_relationship_id    
***********************************************************************************************************************/  
  
ALTER PROCEDURE [AION].[usp_select_aion_user_schedule_get_list]  
  
    @identity                                                   int  
  
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
  