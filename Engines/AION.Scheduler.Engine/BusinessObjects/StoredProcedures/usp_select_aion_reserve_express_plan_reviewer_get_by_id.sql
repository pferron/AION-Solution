
/***********************************************************************************************************************
* Object:       usp_select_aion_reserve_express_plan_reviewer_get_by_id
* Description:  Retrieves ReserveExpressPlanReviewer record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables,
*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause.
* Version:      1.0
* Created by:   AION_user
* Created:      8/6/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 8/6/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_select_aion_reserve_express_plan_reviewer_get_by_id]

    @identity                                                    int

AS

       SELECT 
            RESERVE_EXPRESS_PLAN_REVIEWER_ID
          , BUSINESS_REF_ID
          , PLAN_REVIEWER_ID
          , ROTATION_NBR
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM

       FROM RESERVE_EXPRESS_PLAN_REVIEWER

       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          RESERVE_EXPRESS_PLAN_REVIEWER_ID = @identity
          

RETURN

GO