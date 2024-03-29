
/***********************************************************************************************************************
* Object:       usp_select_aion_reserve_express_reservation_get_list_bydate_v2
* Description:  Retrieves Reserve Express Reservation records for a given key date range.
* Parameters:   
*               @from_dt datetime
*               @to_dt   datetime
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables,
*               to get additional info for retrieval.  Also, developer needs to verify id columns in WHERE clause.
* Version:      1.0
* Created by:   AION_user
* Created:      9/02/2021
************************************************************************************************************************
* Change History: Date, Name, Description
* 9/02/2021    AION_user     Auto-generated
* 9/02/2021    jallen        Modified to make sure project schedules selected are ones only related to EXP
* 11/15/2021   jallen        Only search for reservations not in cancelled status
***********************************************************************************************************************/SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [AION].[usp_select_aion_reserve_express_reservation_get_list_bydate_v2]
@FROM_DT DATETIME,
@TO_DT DATETIME
AS


BEGIN

 --'Cancelled'  
        DECLARE @CANCELLED_APPT_RESPONSE_STATUS_REF_ID INT= 0;
        SELECT @CANCELLED_APPT_RESPONSE_STATUS_REF_ID = APPT_RESPONSE_STATUS_REF_ID
        FROM AION.APPOINTMENT_RESPONSE_STATUS_REF
        WHERE ENUM_MAPPING_VAL_NBR = 7;  

DECLARE @ReviewerAppts TABLE
        (ID                  INT IDENTITY(1, 1), 
         APPT_ID             INT NOT NULL, 
         PROJECT_SCHEDULE_ID INT NOT NULL, 
         RECURRING_APPT_DT   DATETIME NOT NULL
        );

INSERT INTO @ReviewerAppts
        SELECT APPT_ID, 
                PROJECT_SCHEDULE_ID, 
                RECURRING_APPT_DT
        FROM PROJECT_SCHEDULE ps
        WHERE PS.PROJECT_SCHEDULE_TYP_DESC = 'EXP'
        AND CAST(ps.RECURRING_APPT_DT AS DATE) >= CAST(@FROM_DT AS DATE)
                AND CAST(ps.RECURRING_APPT_DT AS DATE) <= CAST(@TO_DT AS DATE);

SELECT rer.[RESERVE_EXPRESS_RESERVATION_ID]
      ,rer.[RESERVE_EXPRESS_DT]
      ,rer.[START_TM]
      ,rer.[END_TM]
      ,rer.[MEETING_ROOM_REF_ID]
	  ,revappts.[PROJECT_SCHEDULE_ID]
      ,revappts.[RECURRING_APPT_DT]

  FROM [AION].[RESERVE_EXPRESS_RESERVATION] rer
  INNER JOIN @ReviewerAppts revappts ON rer.[RESERVE_EXPRESS_RESERVATION_ID] = revappts.APPT_ID
  WHERE rer.[APPT_RESPONSE_STATUS_REF_ID] != @CANCELLED_APPT_RESPONSE_STATUS_REF_ID

  ORDER BY rer.RESERVE_EXPRESS_DT, rer.START_TM
END
RETURN

