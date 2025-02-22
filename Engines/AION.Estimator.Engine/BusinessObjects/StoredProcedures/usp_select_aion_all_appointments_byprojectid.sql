/****** Object:  StoredProcedure [AION].[usp_select_aion_all_appointments_byprojectid]    Script Date: 9/24/2020 12:19:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************************************************************
* Object:       usp_select_aion_all_appointments_byprojectid
* Description:  Retrieves Scheduled Meeting list for given parameter(s).
* Parameters:   
*
* Returns:      Recordset.
* Comments:     Returns any scheduled meeting for existing db projects even if no users have been added
*               
*               
* Version:      1.0
* Created by:   gnadimpalli
* Created:      9/16/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 7/16/2020    jlindsay     initial
* 
***********************************************************************************************************************/

ALTER PROCEDURE [AION].[usp_select_aion_all_appointments_byprojectid] 
@projectid INT
                                                                        
AS
DECLARE @ENUM int

    BEGIN
	 SELECT
	 P.PROJECT_ID,
	 P.PROJECT_NM,
	 P.PROJECT_TYP_REF_ID,
	 PS.ENUM_MAPPING_VAL_NBR as PROJECT_STATUS_REF_ID,
	 PMA.FROM_DT AS START_DT,
	 DATEADD(Day,2,PMA.UPDATED_DTTM) AS ACCEPTANCE_DEADLINE
	 FROM AION.PRELIMINARY_MEETING_APPOINTMENT PMA
	 INNER JOIN [AION].[PROJECT] P ON P.[PROJECT_ID] = PMA.PROJECT_ID
	 INNER JOIN [AION].[PROJECT_STATUS_REF] PS ON PS.PROJECT_STATUS_REF_ID = P.PROJECT_STATUS_REF_ID
	 WHERE PMA.PROJECT_ID=@projectid
	 UNION
	 SELECT
	 P.PROJECT_ID,
	 P.PROJECT_NM,
	 P.PROJECT_TYP_REF_ID,
	 PS.ENUM_MAPPING_VAL_NBR as PROJECT_STATUS_REF_ID,
	 EMA.FROM_DT AS START_DT,
	 DATEADD(Day,2,EMA.UPDATED_DTTM) AS ACCEPTANCE_DEADLINE
	 FROM AION.EXPRESS_MEETING_APPOINTMENT EMA
	 INNER JOIN [AION].[PROJECT] P ON P.[PROJECT_ID] = EMA.PROJECT_ID
	 INNER JOIN [AION].[PROJECT_STATUS_REF] PS ON PS.PROJECT_STATUS_REF_ID = P.PROJECT_STATUS_REF_ID
	 WHERE EMA.PROJECT_ID=@projectid
	 UNION
	 SELECT 
	 P.PROJECT_ID,
	 P.PROJECT_NM,
	 P.PROJECT_TYP_REF_ID,
	 PS.ENUM_MAPPING_VAL_NBR as PROJECT_STATUS_REF_ID,
	 PRS.START_DT AS START_DT,
	 DATEADD(Day,2,PRS.UPDATED_DTTM) AS ACCEPTANCE_DEADLINE
	 FROM AION.PLAN_REVIEW_SCHEDULE PRS
	 INNER JOIN [AION].[PROJECT] P ON P.[PROJECT_ID] = PRS.PROJECT_ID
	 INNER JOIN [AION].[PROJECT_STATUS_REF] PS ON PS.PROJECT_STATUS_REF_ID = P.PROJECT_STATUS_REF_ID
     WHERE PRS.PROJECT_ID = @projectid
     RETURN;
    END;