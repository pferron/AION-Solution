CREATE PROCEDURE [usp_select_aion_getDefaultHoursByDepartmentProjectType]

    @departmentID  int,
	@projectTypeID int

AS

      
       SELECT 
            DEFAULT_ESTIMATION_HOURS_ID
          , DEFAULT_HOURS_NBR
          , WKR_ID_CREATED_TXT
          , WKR_ID_UPDATED_TXT
          , CREATED_DTTM
          , UPDATED_DTTM
          , BUSINESS_REF_ID
          , PROJECT_TYP_REF_ID

       FROM DEFAULT_ESTIMATION_HOURS

       WHERE
        
       -- @TODO:  Correct the following as necessary
        
         BUSINESS_REF_ID = @departmentID
         AND PROJECT_TYP_REF_ID = @projectTypeID
          

RETURN

GO