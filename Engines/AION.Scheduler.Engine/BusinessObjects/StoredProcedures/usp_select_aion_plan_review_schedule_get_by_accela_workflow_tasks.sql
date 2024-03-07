/****** Object:  StoredProcedure [AION].[usp_select_aion_plan_review_schedule_get_by_accela_workflow_tasks]    Script Date: 9/13/2021 9:12:51 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************************************************************
* Object:       [usp_select_aion_plan_review_schedule_get_by_accela_workflow_tasks]
* Description:  Retrieves Project record for given key field(s).
* Parameters:   
*               @srcSystemValTxt                                             varchar(255)
*				@accelaEndDate												 datetime
*				@businessRefId												 int
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables,
*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause.
* Version:      1.0
* Created by:  smithtc
* Created:      12/4/2020
************************************************************************************************************************
* Change History: Date, Name, Description
*12/4/2020  smithtc     manually generated
* 
***********************************************************************************************************************/
ALTER PROCEDURE [AION].[usp_select_aion_plan_review_schedule_get_by_accela_workflow_tasks]

     @srcSystemValTxt                                             varchar(255),
	 @businessRefList											varchar(5000),
	 @accelaEndDate												 datetime

	

AS
	DECLARE @projectId int


	SET @projectId = (select project_id from PROJECT where SRC_SYSTEM_VAL_TXT = @srcSystemValTxt)
   
   

   Select    PLAN_REVIEW_SCHEDULE_ID  
         
   from PLAN_REVIEW_SCHEDULE 

  where PROJECT_ID = @projectId and BUSINESS_REF_ID IN(SELECT [value] AS BUSINESS_REF_ID
                 FROM STRING_SPLIT(@businessRefList, ',')) and END_DT > @accelaEndDate
RETURN

