/***********************************************************************************************************************          
* Object:       usp_select_aion_city_zoning_reviewer_assigned_latest_v2         
* Description:  Retrieve the last assigned city zoning reviewer assigned for FIFO scheduling.           
* Parameters:             
*                         
*          
* Returns:      Recordset.          
* Comments:         
*                        
* Version:      1.0          
* Created by:   Janessa Allen          
* Created:      June 23, 2021          
************************************************************************************************************************          
* Change History: Date, Name, Description          
* June 23, 2021     janessa allen  initial       
* 06/28/2021    jlindsay    order by updated datetime, exclude cancelled  
* 10/26/2021    jallen      updated for using plan review schedule details table
* 11/3/2021 jlindsay remove jba notation
***********************************************************************************************************************/
ALTER PROCEDURE [AION].[usp_select_aion_city_zoning_reviewer_assigned_latest_v2]
AS
BEGIN
	SET NOCOUNT ON

	-- not scheduled (this is saved and has to be considered)        
	DECLARE @cancelled INT;

	SELECT @cancelled = APPT_RESPONSE_STATUS_REF_ID
	FROM [AION].[APPOINTMENT_RESPONSE_STATUS_REF]
	WHERE ENUM_MAPPING_VAL_NBR = 7;

	DECLARE @CITY_ZONING INT

	SELECT @CITY_ZONING = BUSINESS_REF_ID
	FROM BUSINESS_REF
	WHERE BUSINESS_SHORT_DESC = 'Zone_Cty_Chrlt'

	DECLARE @CITY_ZONING_REVIEWERS TABLE (
		ASSIGNED_PLAN_REVIEWER_ID INT,
		START_DT DATETIME,
		UPDATED_DTTM DATETIME
		)

	INSERT INTO @CITY_ZONING_REVIEWERS
	SELECT A.ASSIGNED_PLAN_REVIEWER_ID,
		A.START_DT,
		A.UPDATED_DTTM
	FROM PLAN_REVIEW_SCHEDULE_DETAIL A
	INNER JOIN PLAN_REVIEW_SCHEDULE B ON A.PLAN_REVIEW_SCHEDULE_ID = B.PLAN_REVIEW_SCHEDULE_ID
	WHERE A.BUSINESS_REF_ID = @CITY_ZONING
		AND B.PROJECT_SCHEDULE_TYP_DESC = 'FIFO'
		AND B.APPT_RESPONSE_STATUS_REF_ID != @cancelled

	DECLARE @MAX_START_DATE DATETIME

	SELECT @MAX_START_DATE = MAX(START_DT)
	FROM @CITY_ZONING_REVIEWERS

	DECLARE @ASSIGNED_PLAN_REVIEWER_ID INT

	SELECT TOP 1 @ASSIGNED_PLAN_REVIEWER_ID = ASSIGNED_PLAN_REVIEWER_ID
	FROM @CITY_ZONING_REVIEWERS
	WHERE START_DT = @MAX_START_DATE
	ORDER BY UPDATED_DTTM DESC

	SELECT ISNULL(@ASSIGNED_PLAN_REVIEWER_ID, 0) AS ASSIGNED_PLAN_REVIEWER_ID
END
