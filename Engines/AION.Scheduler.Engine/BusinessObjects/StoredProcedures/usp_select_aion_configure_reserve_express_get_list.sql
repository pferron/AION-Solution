
/***********************************************************************************************************************
* Object:       usp_select_aion_configure_reserve_express_get_list
* Description:  Retrieves ConfigureReserveExpress list for given parameter(s).
* Parameters:   
*               @identity                                                   int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval.
*               This proc expects id_person and/or id_file to generate list; modify as necessary.
*               Include ORDER BY clause as necessary.
* Version:      1.0
* Created by:   AION_user
* Created:      7/29/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 7/29/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_select_aion_configure_reserve_express_get_list]


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

     

RETURN

GO