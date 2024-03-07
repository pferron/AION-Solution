
/***********************************************************************************************************************
* Object:       usp_select_aion_configure_reserve_express_get_by_id
* Description:  Retrieves ConfigureReserveExpress record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables,
*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause.
* Version:      1.0
* Created by:   AION_user
* Created:      7/29/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 7/29/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_select_aion_configure_reserve_express_get_by_id]

    @identity                                                    int

AS

       SELECT 
            CONFIGURE_RESERVE_EXPRESS_ID
          , RESERVE_EXPRESS_DAY_DESC
          , START_DTTM
          , END_DTTM
          , ACTIVE_IND
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM

       FROM CONFIGURE_RESERVE_EXPRESS

       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          CONFIGURE_RESERVE_EXPRESS_ID = @identity
          

RETURN

GO