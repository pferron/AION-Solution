-- =============================================  
-- Author:      <Author, , Name>  
-- Create Date: <Create Date, , >  
-- Description: <Description, , >  
-- =============================================  
ALTER PROCEDURE [AION].[usp_update_aion_cancel_entities_not_in_json_object] (
	-- Add the parameters for the stored procedure here  
	@excludedProjectIds VARCHAR(max)
	)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from  
	-- interfering with SELECT statements.  
	SET NOCOUNT ON

	DECLARE @projectcancelledstsid INT;
	DECLARE @apptcancelledstsid INT;
	DECLARE @projectids TABLE (project_id VARCHAR(255) NOT NULL)
	DECLARE @apptids TABLE (
		id INT identity(1, 1) NOT NULL,
		appt_id INT NOT NULL,
		project_schedule_typ_desc VARCHAR(50) NOT NULL
		)

	INSERT INTO @projectids
	SELECT [value]
	FROM STRING_SPLIT(@excludedProjectIds, ',')

	SELECT @projectcancelledstsid = project_status_ref_id
	FROM [AION].project_status_ref
	WHERE src_system_val_txt = 'Cancelled';

	SELECT @apptcancelledstsid = appt_response_status_ref_id
	FROM [AION].appointment_response_status_ref
	WHERE appt_response_status_desc = 'Cancelled';

	UPDATE [AION].PROJECT
	SET project_status_ref_id = @projectcancelledstsid
	WHERE SRC_SYSTEM_VAL_TXT NOT IN (
			SELECT project_id
			FROM @projectids
			)

	UPDATE [AION].FACILITATOR_MEETING_APPOINTMENT
	SET APPT_RESPONSE_STATUS_REF_ID = @apptcancelledstsid
	FROM [AION].FACILITATOR_MEETING_APPOINTMENT FMA
	INNER JOIN [AION].PROJECT P ON FMA.PROJECT_ID = P.PROJECT_ID
	WHERE FMA.APPT_RESPONSE_STATUS_REF_ID != @apptcancelledstsid
		AND SRC_SYSTEM_VAL_TXT NOT IN (
			SELECT project_id
			FROM @projectids
			)

	UPDATE [AION].PRELIMINARY_MEETING_APPOINTMENT
	SET APPT_RESPONSE_STATUS_REF_ID = @apptcancelledstsid
	FROM [AION].PRELIMINARY_MEETING_APPOINTMENT PMA
	INNER JOIN [AION].PROJECT P ON PMA.PROJECT_ID = P.PROJECT_ID
	WHERE PMA.APPT_RESPONSE_STATUS_REF_ID != @apptcancelledstsid
		AND SRC_SYSTEM_VAL_TXT NOT IN (
			SELECT project_id
			FROM @projectids
			)

	UPDATE [AION].EXPRESS_MEETING_APPOINTMENT
	SET APPT_RESPONSE_STATUS_REF_ID = @apptcancelledstsid
	FROM [AION].EXPRESS_MEETING_APPOINTMENT EMA
	INNER JOIN [AION].PROJECT P ON EMA.PROJECT_ID = P.PROJECT_ID
	WHERE EMA.APPT_RESPONSE_STATUS_REF_ID != @apptcancelledstsid
		AND SRC_SYSTEM_VAL_TXT NOT IN (
			SELECT project_id
			FROM @projectids
			)

	UPDATE [AION].PLAN_REVIEW_SCHEDULE
	SET APPT_RESPONSE_STATUS_REF_ID = @apptcancelledstsid
	FROM [AION].PLAN_REVIEW_SCHEDULE EMA
	INNER JOIN [AION].PROJECT P ON EMA.PROJECT_ID = P.PROJECT_ID
	WHERE EMA.APPT_RESPONSE_STATUS_REF_ID != @apptcancelledstsid
		AND SRC_SYSTEM_VAL_TXT NOT IN (
			SELECT project_id
			FROM @projectids
			)

	UPDATE [AION].FIFO_SCHEDULE
	SET APPT_RESPONSE_STATUS_REF_ID = @apptcancelledstsid
	FROM [AION].FIFO_SCHEDULE EMA
	INNER JOIN [AION].PROJECT P ON EMA.PROJECT_ID = P.PROJECT_ID
	WHERE EMA.APPT_RESPONSE_STATUS_REF_ID != @apptcancelledstsid
		AND SRC_SYSTEM_VAL_TXT NOT IN (
			SELECT project_id
			FROM @projectids
			)
END
