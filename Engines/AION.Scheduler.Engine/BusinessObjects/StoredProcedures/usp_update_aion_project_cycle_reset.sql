-- =======================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Janessa Allen
-- Create Date: January 16, 2024
-- Description: Procedure to correct the project cycle status. The parameters required
--              are to input the project number the cycle number that the user wishes 
--              to set the project back to.
-- =============================================
CREATE PROCEDURE usp_update_aion_project_cycle_reset
(
    -- Add the parameters for the stored procedure here
    @project_number varchar(50),
	@cycle_number int
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON;

	DECLARE @ProjectCyclesForRemoval TABLE
	(
        PROJECT_CYCLE_ID int,
		CYCLE_NBR int
	);

	INSERT INTO @ProjectCyclesForRemoval
	SELECT PROJECT_CYCLE_ID, CYCLE_NBR
		FROM AION.PROJECT_CYCLE
		WHERE PROJECT_ID IN (
			SELECT PROJECT_ID
			FROM AION.PROJECT
			WHERE SRC_SYSTEM_VAL_TXT = @project_number)
			AND CYCLE_NBR > @cycle_number;

-- PLAN REVIEWS
	DECLARE @PlanReviewSchedulesForRemoval TABLE
	(
        PLAN_REVIEW_SCHEDULE_ID int,
		PROJECT_SCHEDULE_TYP_DESC varchar(50)
	);

	INSERT INTO @PlanReviewSchedulesForRemoval
	SELECT PLAN_REVIEW_SCHEDULE_ID,
			PROJECT_SCHEDULE_TYP_DESC
			FROM AION.PLAN_REVIEW_SCHEDULE
			WHERE PROJECT_CYCLE_ID IN (
					SELECT PROJECT_CYCLE_ID
					FROM @ProjectCyclesForRemoval
					);

	DELETE AION.PLAN_REVIEW_SCHEDULE_DETAIL
		WHERE PLAN_REVIEW_SCHEDULE_ID IN (
				SELECT PLAN_REVIEW_SCHEDULE_ID
				FROM @PlanReviewSchedulesForRemoval
				);

-- PROJECT AND USER SCHEDULES
	DECLARE @ProjectSchedulesForRemoval TABLE
	(
        PROJECT_SCHEDULE_ID int
	);

	INSERT INTO @ProjectSchedulesForRemoval
	SELECT PROJECT_SCHEDULE_ID
			FROM AION.PROJECT_SCHEDULE A
			INNER JOIN @PlanReviewSchedulesForRemoval B 
				ON A.APPT_ID = B.PLAN_REVIEW_SCHEDULE_ID 
				AND A.PROJECT_SCHEDULE_TYP_DESC = B.PROJECT_SCHEDULE_TYP_DESC;

	DELETE
		FROM AION.USER_SCHEDULE
		WHERE PROJECT_SCHEDULE_ID IN (
				SELECT PROJECT_SCHEDULE_ID
				FROM @ProjectSchedulesForRemoval
				);

	DELETE
		FROM AION.PROJECT_SCHEDULE
		WHERE PROJECT_SCHEDULE_ID IN (
				SELECT PROJECT_SCHEDULE_ID
				FROM @ProjectSchedulesForRemoval
				);

-- PROJECT CYCLE/DETAIS
	DELETE
		FROM AION.PROJECT_CYCLE_DETAIL
		WHERE PROJECT_CYCLE_ID IN (
				SELECT PROJECT_CYCLE_ID
				FROM @ProjectCyclesForRemoval
				);

	DELETE
		FROM AION.PROJECT_AUDIT
		WHERE PROJECT_CYCLE_ID IN (
				SELECT PROJECT_CYCLE_ID
				FROM @ProjectCyclesForRemoval
				);

	DELETE
		FROM AION.PLAN_REVIEW_SCHEDULE
		WHERE PROJECT_CYCLE_ID IN (
				SELECT PROJECT_CYCLE_ID
				FROM @ProjectCyclesForRemoval
				);

	DELETE
		FROM AION.PROJECT_CYCLE
		WHERE PROJECT_CYCLE_ID IN (
				SELECT PROJECT_CYCLE_ID
				FROM @ProjectCyclesForRemoval
				);

-- RESET TO DESIRED CYCLE NUMBER
	UPDATE PROJECT_CYCLE
	SET CURRENT_CYCLE_IND = 1,
	FUTURE_CYCLE_IND = 0,
	PLANS_READY_ON_DT = NULL,
	IS_COMPLETE_IND = 0,
	IS_APRV_IND = 0,
	GATE_DT = NULL,
	SCHEDULE_AFTER_DT = NULL,
	RESPONDER_USER_ID = NULL
	WHERE PROJECT_ID IN (SELECT PROJECT_ID FROM AION.PROJECT WHERE SRC_SYSTEM_VAL_TXT = @project_number )
	AND CYCLE_NBR = @cycle_number;

	UPDATE PROJECT 
	SET CYCLE_NBR = @cycle_number
	WHERE SRC_SYSTEM_VAL_TXT = @project_number

END
GO
