/****** Object:  StoredProcedure [AION].[usp_select_aion_project_search]    Script Date: 6/22/2022 11:42:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/***********************************************************************************************************************    
* Object:       usp_select_aion_project_search   
* Description:  Retrieves Project list for given parameter(s).    
* Parameters:       
*                   
*    
* Returns:      Recordset.    
* Comments:      
* Version:      1.0    
* Created by:   j allen    
* Created:      09/03/2020    
************************************************************************************************************************    
* Change History: Date, Name, Description    
* 09/03/2020    j allen               
* 07/08/2021	jlindsay add new field for Accela record id REC_ID_TXT
* 06/22/2022	jallen change Facilitator Name result to 'Unassigned' if equal to -1
* 05/18/2023	jlindsay update preliminary ind check
***********************************************************************************************************************/
ALTER PROCEDURE [AION].[usp_select_aion_project_search] @START_DATE DATETIME = NULL,
	@END_DATE DATETIME = NULL,
	@PROJECT_NUMBER VARCHAR(100),
	@PROJECT_NAME VARCHAR(100),
	@PROJECT_ADDRESS VARCHAR(100),
	@CUSTOMER_NAME VARCHAR(100),
	@PLAN_REVIEWER VARCHAR(100),
	@PROJECT_STATUS INT,
	@ESTIMATOR_ID INT,
	@FACILITATOR_ID INT,
	@MEETING_TYPE INT
AS
BEGIN
	DECLARE @projectNumber VARCHAR(100)
	DECLARE @projectName VARCHAR(100)
	DECLARE @projectAddress VARCHAR(100)
	DECLARE @customerName VARCHAR(100)
	DECLARE @planReviewer VARCHAR(100)

	SET @projectNumber = LOWER(AION.RemoveNonAlphaCharacters(@PROJECT_NUMBER))
	SET @projectName = LOWER(AION.RemoveNonAlphaCharacters(@PROJECT_NAME))
	SET @projectAddress = LOWER(AION.RemoveNonAlphaCharacters(@PROJECT_ADDRESS))
	SET @customerName = LOWER(AION.RemoveNonAlphaCharacters(@CUSTOMER_NAME))
	SET @planReviewer = LOWER(AION.RemoveNonAlphaCharacters(@PLAN_REVIEWER))

	IF ISNULL(@END_DATE, '') = ''
		SET @END_DATE = @START_DATE

	SELECT DISTINCT P.CREATED_DTTM AS 'DATE_OF_APPLICATION',
		P.SRC_SYSTEM_VAL_TXT AS 'PROJECT_NUMBER',
		P.PROJECT_NM AS 'PROJECT_NAME',
		PTR.PROJECT_TYP_REF_DISPLAY_NM AS 'PROJECT_TYPE',
		UCUSTOMER.FIRST_NM + ' ' + UCUSTOMER.LAST_NM AS 'CUSTOMER_NAME',
		CASE 
			WHEN P.ASSIGNED_FACILITATOR_ID > 0 
			THEN UFACILITATOR.FIRST_NM + ' ' + UFACILITATOR.LAST_NM 
			ELSE 'Unassigned' 
		END AS 'FACILITATOR_NAME',
		PSR.PROJECT_STATUS_REF_DESC AS 'PROJECT_STATUS',
		CASE 
			WHEN (
					ISNULL(PMA.PRELIMINARY_MEETING_APPT_ID, 0) = 0
					AND ISNULL(@MEETING_TYPE, 0) = 0
					)
				THEN ''
			ELSE CASE 
					WHEN (ISNULL(@MEETING_TYPE, 0) = 7)
						THEN 'Preliminary Meeting'
					ELSE MTR.MEETING_TYP_DESC
					END
			END AS 'MEETING_TYPE',
			P.REC_ID_TXT
	FROM PROJECT P
	INNER JOIN PROJECT_TYPE_REF PTR ON P.PROJECT_TYP_REF_ID = PTR.PROJECT_TYP_REF_ID
	LEFT JOIN [USER] UFACILITATOR ON P.ASSIGNED_FACILITATOR_ID = UFACILITATOR.[USER_ID]
	LEFT JOIN [USER] UCUSTOMER ON P.PROJECT_MANAGER_ID = UCUSTOMER.[USER_ID]
	INNER JOIN PROJECT_STATUS_REF PSR ON P.PROJECT_STATUS_REF_ID = PSR.PROJECT_STATUS_REF_ID
	INNER JOIN PROJECT_BUSINESS_RELATIONSHIP PBR ON P.PROJECT_ID = PBR.PROJECT_ID
	LEFT JOIN PRELIMINARY_MEETING_APPOINTMENT PMA ON P.PROJECT_ID = PMA.PROJECT_ID
	LEFT JOIN FACILITATOR_MEETING_APPOINTMENT FMA ON P.PROJECT_ID = FMA.PROJECT_ID AND ISNULL(@MEETING_TYPE,0) > 0
	LEFT JOIN MEETING_TYPE_REF MTR ON FMA.MEETING_TYP_REF_ID = MTR.MEETING_TYP_REF_ID
	LEFT JOIN PROJECT_SCHEDULE PS ON P.PROJECT_ID = PS.APPT_ID
	LEFT JOIN USER_SCHEDULE US ON PS.PROJECT_SCHEDULE_ID = US.PROJECT_SCHEDULE_ID
	LEFT JOIN [USER] UASSIGNED ON UASSIGNED.[USER_ID] = PBR.ASSIGNED_PLAN_REVIEWER_ID
	LEFT JOIN [USER] USECONDARY ON USECONDARY.[USER_ID] = PBR.SECONDARY_PLAN_REVIEWER_ID
	WHERE (
			(NULLIF(@START_DATE, '') IS NULL)
			OR (
				CAST(P.CREATED_DTTM AS DATE) >= @START_DATE
				AND CAST(P.CREATED_DTTM AS DATE) <= @END_DATE
				)
			)
		AND (
			ISNULL(@PROJECT_NUMBER, '') = ''
			OR LOWER(AION.RemoveNonAlphaCharacters(P.SRC_SYSTEM_VAL_TXT)) LIKE '%' + @projectNumber + '%'
			)
		AND (
			ISNULL(@PROJECT_NAME, '') = ''
			OR LOWER(AION.RemoveNonAlphaCharacters(P.PROJECT_NM)) LIKE '%' + @projectName + '%'
			)
		AND (
			ISNULL(@PROJECT_ADDRESS, '') = ''
			OR LOWER(AION.RemoveNonAlphaCharacters(P.PROJECT_ADDR_TXT)) LIKE '%' + @projectAddress + '%'
			)
		AND (
			@PROJECT_STATUS = 0
			OR PSR.ENUM_MAPPING_VAL_NBR = @PROJECT_STATUS
			)
		AND (
			@FACILITATOR_ID = 0
			OR P.ASSIGNED_FACILITATOR_ID = @FACILITATOR_ID
			)
		AND (
			@ESTIMATOR_ID = 0
			OR P.ASSIGNED_ESTIMATOR_ID = @ESTIMATOR_ID
			)
		AND (
			ISNULL(@CUSTOMER_NAME, '') = ''
			OR LOWER(AION.RemoveNonAlphaCharacters(UCUSTOMER.FIRST_NM)) + LOWER(AION.RemoveNonAlphaCharacters(UCUSTOMER.LAST_NM)) LIKE '%' + @customerName + '%'
			)
		AND (
			ISNULL(@PLAN_REVIEWER, '') = ''
			OR LOWER(AION.RemoveNonAlphaCharacters(UASSIGNED.FIRST_NM)) + LOWER(AION.RemoveNonAlphaCharacters(UASSIGNED.LAST_NM)) LIKE '%' + @planReviewer + '%'
			)
		AND (
			(
				@MEETING_TYPE != 7
				AND FMA.MEETING_TYP_REF_ID = @MEETING_TYPE
				)
			OR (
				@MEETING_TYPE = 7
				AND P.PRELIMINARY_IND = 1
				)
			OR @MEETING_TYPE = 0
			)
	ORDER BY P.CREATED_DTTM
END;
