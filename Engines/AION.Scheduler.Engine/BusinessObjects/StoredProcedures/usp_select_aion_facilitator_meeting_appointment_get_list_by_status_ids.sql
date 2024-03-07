SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************************************************************    
* Object:       usp_select_aion_facilitator_meeting_appointment_get_list_by_status_ids
* Description:  Retrieves FacilitatorMeetingAppointment record for given key field(s).    
* Parameters:       
*              @startdate  DATETIME, 
*              @enddate    DATETIME
*              @apptresponsestatusrefid INT
*
* Returns:      Recordset.    
* Comments:     Developer may need to manually join to other tables, such as code tables,    
*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause.    
* Version:      1.0    
* Created by:   jallen    
* Created:      08/16/2022
************************************************************************************************************************    
* Change History: Date, Name, Description    
* 08/16/2022    jallen     initial
***********************************************************************************************************************/
ALTER PROCEDURE [AION].[usp_select_aion_facilitator_meeting_appointment_get_list_by_status_ids] 
	@startdate DATETIME
	,@enddate DATETIME
	,@fmaStatusIds VARCHAR(50)
AS
BEGIN
	DECLARE @SearchEndDate DATETIME;
	DECLARE @SearchStartDate DATETIME;
	DECLARE @Today DATE = CONVERT(DATE, GETDATE());

	--
	--
	SET @SearchEndDate = CASE 
			WHEN ISNULL(@enddate, '') = ''
				THEN DATEADD(year, 1, @Today)
			ELSE CONVERT(DATE, @enddate)
			END;
	--
	SET @SearchStartDate = CASE 
			WHEN ISNULL(@startdate, '') = ''
				THEN @Today
			ELSE CONVERT(DATE, @startdate)
			END;
	--
	--
	--
	SELECT FACILITATOR_MEETING_APPT_IDENTIFIER
		,FROM_DT
		,TO_DT
		,MEETING_ROOM_REF_ID
		,APPT_RESPONSE_STATUS_REF_ID
		,WKR_ID_CREATED_TXT
		,CREATED_DTTM
		,WKR_ID_UPDATED_TXT
		,UPDATED_DTTM
		,PROJECT_ID
		,MEETING_TYP_REF_ID
		,VIRTUAL_MEETING_IND
		,APPT_CANCELLATION_REF_ID
		,CANCEL_AFTER_DT
		,PROPOSED_1_DT
		,PROPOSED_2_DT
		,PROPOSED_3_DT
		,EXTERNAL_ATTENDEES_CNT
	FROM [AION].FACILITATOR_MEETING_APPOINTMENT fma
	WHERE (
			isnull(fma.FROM_DT, '') = ''
			OR fma.FROM_DT >= @SearchStartDate
			)
		AND (
			isnull(fma.TO_DT, '') = ''
			OR fma.TO_DT <= @SearchEndDate
			)
		AND fma.APPT_RESPONSE_STATUS_REF_ID IN (
				SELECT LTRIM(RTRIM(Value))
				FROM STRING_SPLIT(@fmaStatusIds, ',', 0)
			)
	RETURN;
END;