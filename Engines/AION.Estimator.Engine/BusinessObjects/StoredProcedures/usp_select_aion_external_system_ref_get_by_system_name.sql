

CREATE PROCEDURE [usp_select_aion_external_system_ref_get_by_system_name]

    @systemName                                                    int

AS

       SELECT 
             EXTERNAL_SYSTEM_REF_ID
          , EXTERNAL_SYSTEM_NM
          , EXTERNAL_SYSTEM_DESC
          , ADDL_INFORMATION_TXT
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          , ENUM_MAPPING_VAL_NBR

       FROM EXTERNAL_SYSTEM_REF

       WHERE
        
       -- @TODO:  Correct the following as necessary
        
          EXTERNAL_SYSTEM_NM = @systemName
          

RETURN

GO