
/***********************************************************************************************************************
* Object:       usp_select_aion_square_footage_ref_get_by_id
* Description:  Retrieves SquareFootageRef record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables,
*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause.
* Version:      1.0
* Created by:   AION_user
* Created:      4/30/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 4/30/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_select_aion_square_footage_ref_get_by_id]

    @identity                                                    int

AS

       SELECT 
            SQUARE_FOOTAGE_REF_ID
          , SQUARE_FOOTAGE_DESC
          , SQUARE_FOOTAGE_VAL_TXT
          , ENUM_MAPPING_VAL_NBR
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM

       FROM SQUARE_FOOTAGE_REF

       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          SQUARE_FOOTAGE_REF_ID = @identity
          

RETURN

GO