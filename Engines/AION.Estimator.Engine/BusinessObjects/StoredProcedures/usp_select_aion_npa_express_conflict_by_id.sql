/****** Object:  StoredProcedure [AION].[usp_select_aion_npa_express_conflict_by_id]    Script Date: 12/2/2020 3:08:09 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************************************************************
* Object:       usp_select_aion_npa_express_conflict_by_id
* Description:  Finds conflicts between an NPA scheduling and Express / RER scheduling
* Parameters:   
*               @USER_ID                                                     int
*               @FROM_DT                                                     datetime
*               @TO_DT                                                       datetime
*
* Returns:      Recordset.
* Version:      1.0
* Created by:   AION_user
* Created:      12/2/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 12/2/2020    aburnam     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [AION].[usp_select_aion_npa_express_conflict_by_id]

    @USER_ID                                                     int,
	@FROM_DT                                                     datetime,
	@TO_DT                                                       datetime

AS

       DECLARE @reltable TABLE
        (ID INT, 
         nm VARCHAR(20)
        );
	   WITH EMAs AS
	   (SELECT 
           US.USER_ID,
		   EMA.FROM_DT,
		   EMA.TO_DT

		   FROM (SELECT * FROM USER_SCHEDULE WHERE USER_ID = @USER_ID) US
		   INNER JOIN
		   (SELECT * FROM PROJECT_SCHEDULE WHERE PROJECT_SCHEDULE_TYP_DESC = 'EMA') PS ON
		   US.PROJECT_SCHEDULE_ID = PS.PROJECT_SCHEDULE_ID
		   INNER JOIN EXPRESS_MEETING_APPOINTMENT EMA ON
		   EMA.EXPRESS_MEETING_APPT_ID = PS.APPT_ID),

		EXPs AS
		(SELECT
			US.USER_ID,
			EXP.RESERVE_EXPRESS_DT,
			EXP.RESERVE_EXPRESS_DT + CAST(EXP.START_TM AS datetime) AS FROM_DT,
			EXP.RESERVE_EXPRESS_DT + CAST(EXP.END_TM AS datetime) AS TO_DT
			
			FROM (SELECT * FROM USER_SCHEDULE WHERE USER_ID = @USER_ID) US
			INNER JOIN
			(SELECT * FROM PROJECT_SCHEDULE WHERE PROJECT_SCHEDULE_TYP_DESC = 'EXP') PS ON
			US.PROJECT_SCHEDULE_ID = PS.PROJECT_SCHEDULE_ID
			INNER JOIN RESERVE_EXPRESS_RESERVATION EXP ON
			EXP.RESERVE_EXPRESS_RESERVATION_ID = PS.APPT_ID)

		SELECT FROM_DT
		FROM EMAs e
		WHERE @FROM_DT BETWEEN e.FROM_DT AND e.TO_DT
		OR @TO_DT BETWEEN e.FROM_DT AND e.TO_DT
        UNION
		SELECT FROM_DT
		FROM EXPs e
		WHERE @FROM_DT BETWEEN e.FROM_DT AND e.TO_DT
		OR @TO_DT BETWEEN e.FROM_DT AND e.TO_DT
          

RETURN

