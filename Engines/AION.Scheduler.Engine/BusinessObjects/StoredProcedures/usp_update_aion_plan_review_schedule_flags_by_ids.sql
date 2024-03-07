/****** Object:  StoredProcedure [AION].[usp_update_aion_plan_review_schedule_flags_by_ids]    Script Date: 9/17/2021 12:36:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************************************************************
* Object:       usp_update_aion_plan_review_schedule_flags_by_ids
* Description:  Updates Current/Future Cycle Flag Values
* Parameters:   
*               @planReviewScheduleIds                                                    string
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables,
*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause.
* Version:      1.0
* Created by:   jallen
* Created:      09/17/2021 
************************************************************************************************************************
* Change History: Date, Name, Description
* 09/17/2021    jallen     Created
***********************************************************************************************************************/

ALTER PROCEDURE [AION].[usp_update_aion_plan_review_schedule_flags_by_ids]

    @planReviewScheduleIds       varchar(MAX),
	@isCurrentFlag               bit

AS

	  CREATE TABLE #PlanReviewScheduleIds (
			PlanReviewScheduleId varchar(255)
		);


	  INSERT INTO #PlanReviewScheduleIds select value from String_Split(@planReviewScheduleIds, ',')

	  IF (@isCurrentFlag = 1)
		BEGIN
			UPDATE PLAN_REVIEW_SCHEDULE_JBA
			SET IS_CURRENT_CYCLE_IND = 0
			WHERE PLAN_REVIEW_SCHEDULE_ID in 
			(select PlanReviewScheduleId from #PlanReviewScheduleIds)
		END
		ELSE
		BEGIN 
			UPDATE PLAN_REVIEW_SCHEDULE
			SET IS_FUTURE_CYCLE_IND = 0
			WHERE PLAN_REVIEW_SCHEDULE_ID in 
			(select PlanReviewScheduleId from #PlanReviewScheduleIds)
        END

RETURN

