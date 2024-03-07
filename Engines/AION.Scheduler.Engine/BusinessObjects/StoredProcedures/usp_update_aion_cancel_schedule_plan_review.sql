/***********************************************************************************************************************            
* Object:      [usp_update_aion_cancel_schedule_plan_review]         
*                 
* Returns:      Number of rows affected.            
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.            
* Version:      1.0            
* Created by:   AION_user            
* Created:      07/11/2020            
************************************************************************************************************************            
* Change History: Date, Name, Description            
*  09/08/2020    gnadimpalli          
*  11/01/2021    jallen    update for project cycle table 
* 11/3/2021 jlindsay remove jba notation
***********************************************************************************************************************/
ALTER PROCEDURE [AION].[usp_update_aion_cancel_schedule_plan_review]
AS
BEGIN
	--'tentatively Scheduled'      
	DECLARE @APPT_RESPONSE_STATUS_REF_ID INT = 0;

	SELECT @APPT_RESPONSE_STATUS_REF_ID = APPT_RESPONSE_STATUS_REF_ID
	FROM AION.APPOINTMENT_RESPONSE_STATUS_REF
	WHERE ENUM_MAPPING_VAL_NBR = 9;

	--      
	--'No Reply'      
	DECLARE @CANCELLED_APPT_RESPONSE_STATUS_REF_ID INT = 0;

	SELECT @CANCELLED_APPT_RESPONSE_STATUS_REF_ID = APPT_RESPONSE_STATUS_REF_ID
	FROM AION.APPOINTMENT_RESPONSE_STATUS_REF
	WHERE ENUM_MAPPING_VAL_NBR = 3;

	--'Project Status - Prod unknown'      
	DECLARE @PROD_NOT_KNOWN_PROJECT_STATUS_REF INT = 0;

	SELECT @PROD_NOT_KNOWN_PROJECT_STATUS_REF = PROJECT_STATUS_REF_ID
	FROM AION.PROJECT_STATUS_REF
	WHERE ENUM_MAPPING_VAL_NBR = 7;

	DECLARE @cycle_ids TABLE (projectCycleId INT);
	DECLARE @ids TABLE (projectId INT);

	UPDATE AION.PLAN_REVIEW_SCHEDULE
	SET APPT_RESPONSE_STATUS_REF_ID = @CANCELLED_APPT_RESPONSE_STATUS_REF_ID
	OUTPUT inserted.PROJECT_CYCLE_ID
	INTO @cycle_ids
	WHERE APPT_RESPONSE_STATUS_REF_ID = @APPT_RESPONSE_STATUS_REF_ID
		AND GETDATE() >= DATEADD(DAY, 2, UPDATED_DTTM);

	INSERT INTO @ids
	SELECT PROJECT_ID
	FROM AION.PROJECT_CYCLE
	WHERE PROJECT_CYCLE_ID IN (
			SELECT projectCycleId
			FROM @cycle_ids
			)

	UPDATE AION.PROJECT
	SET PROJECT_STATUS_REF_ID = @PROD_NOT_KNOWN_PROJECT_STATUS_REF
	WHERE PROJECT_ID IN (
			SELECT projectId
			FROM @ids
			)

	SELECT DISTINCT *
	FROM @ids

	RETURN;
END;
