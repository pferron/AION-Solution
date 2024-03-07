/***********************************************************************************************************************      
* Object:       usp_select_aion_preliminary_meeting_appointment_get_list_search     
* Description:  Retrieves PreliminaryMeetingAppointment record for given key field(s).      
* Parameters:         
*              @startdate  DATETIME,   
*              @enddate    DATETIME  
*              @apptresponsestatusrefid INT  
*  
* Returns:      Recordset.      
* Comments:     Developer may need to manually join to other tables, such as code tables,      
*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause.      
* Version:      1.0      
* Created by:   jlindsay      
* Created:      7/10/2020     
************************************************************************************************************************      
* Change History: Date, Name, Description      
* 7/10/2020    jlindsay     initial  
* 04/15/2021   jallen       add APPT_CANCELLATION_REF_ID and CANCEL_AFTER_DT to results  
*  01/06/2022    jlindsay    add reschedule 
***********************************************************************************************************************/
ALTER PROCEDURE [AION].[usp_select_aion_preliminary_meeting_appointment_get_list_search] @startdate DATETIME,
	@enddate DATETIME,
	@apptresponsestatusrefid INT
AS
BEGIN
	DECLARE @SearchEndDate DATETIME;
	DECLARE @SearchStartDate DATETIME;
	DECLARE @SearchStatus INT;
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
	SET @SearchStatus = CASE 
			WHEN ISNULL(@apptresponsestatusrefid, 0) = 0
				THEN 0
			ELSE @apptresponsestatusrefid
			END;

	--  
	SELECT PRELIMINARY_MEETING_APPT_ID,
		FROM_DT,
		TO_DT,
		MEETING_ROOM_REF_ID,
		APPT_RESPONSE_STATUS_REF_ID,
		WKR_ID_CREATED_TXT,
		CREATED_DTTM,
		WKR_ID_UPDATED_TXT,
		UPDATED_DTTM,
		PROJECT_ID,
		APPENDIX_AGENDA_DUE_DT,
		PROPOSED_1_DT,
		PROPOSED_2_DT,
		PROPOSED_3_DT,
		APPT_CANCELLATION_REF_ID,
		CANCEL_AFTER_DT,
		RESCHEDULE_IND
	FROM [AION].PRELIMINARY_MEETING_APPOINTMENT pma
	WHERE pma.FROM_DT >= @SearchStartDate
		AND pma.TO_DT <= @SearchEndDate
		AND pma.APPT_RESPONSE_STATUS_REF_ID = CASE 
			WHEN @SearchStatus = 0
				THEN pma.APPT_RESPONSE_STATUS_REF_ID
			ELSE @SearchStatus
			END;

	RETURN;
END;
