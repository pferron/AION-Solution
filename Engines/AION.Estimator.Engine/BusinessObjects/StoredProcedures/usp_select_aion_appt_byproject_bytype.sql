/***********************************************************************************************************************      
* Object:       usp_select_aion_appt_byproject_bytype     
* Description:  get the template by the type       
* Parameters:         
*                @projectid INT, @projecttypdesc varchar(20),@meetingtyprefid INT      
*      
* Returns:      Recordset.      
* Comments:           
* Version:      1.0      
* Created by:   jlindsay      
* Created:      5/11/2021      
************************************************************************************************************************      
* Change History: Date, Name, Description      
* 5/11/2021    jlindsay     get the template by the type (FMA      
EMA      
PR      
PMA      
FIFO)      
and if FMA, meeting type ref id      
*   11/3/2021 jlindsay remove jba notation   
* 11/19/2021 jlindsay update for new table struction with sub cycle integration  
* 01/07/2022 jlindsay only get rows for detail start date that have a date
***********************************************************************************************************************/
ALTER PROCEDURE [AION].[usp_select_aion_appt_byproject_bytype] @projectid INT,
	@projecttypdesc VARCHAR(20),
	@meetingtyprefid INT
AS
BEGIN
	--depending on type, select the latest cycle number appt reviewers, start date, calculate acceptance deadline      
	IF (
			@projecttypdesc = 'PR'
			OR @projecttypdesc = 'EMA'
			OR @projecttypdesc = 'FIFO'
			)
	BEGIN
		WITH cycle
		AS (
			SELECT max(cycle_nbr) cycle_nbr,
				project_cycle_id
			FROM [AION].project_cycle
			WHERE project_id = @projectid
			GROUP BY cycle_nbr,
				project_cycle_id
			),
		pr
		AS (
			SELECT pr.*
			FROM [AION].plan_review_schedule pr
			INNER JOIN cycle ON pr.PROJECT_CYCLE_ID = cycle.project_cycle_id
			),
		prd
		AS (
			SELECT prd.start_dt
			FROM [AION].plan_review_schedule_detail prd
			INNER JOIN pr ON pr.plan_review_schedule_id = prd.plan_review_schedule_id
			where isnull(prd.start_dt,'') != ''
			),
		ps
		AS (
			SELECT ps.*
			FROM [AION].project_schedule ps
			INNER JOIN pr ON ps.appt_id = pr.plan_review_schedule_id
				AND ps.project_schedule_typ_desc = @projecttypdesc
			),
		us
		AS (
			SELECT us.*
			FROM [AION].user_schedule us
			INNER JOIN ps ON us.project_schedule_id = ps.project_schedule_id
			),
		pendingnotes
		AS (
			SELECT notes.*
			FROM AION.notes
			INNER JOIN AION.notes_type_ref typ ON notes.Notes_typ_ref_id = typ.notes_typ_ref_id
			WHERE typ.enum_mapping_val_nbr = 3
				AND project_id = @projectid
			)
		SELECT @projectid AS Project_id,
			@projecttypdesc AS Project_Schedule_Typ_Desc,
			@meetingtyprefid AS Meeting_Typ_Ref_Id,
			'Start_Dt' AS row_type,
			prd.start_dt AS start_dt,
			NULL AS [scheduled_user_id],
			NULL AS cycle_nbr,
			NULL AS meeting_room_ref_id,
			NULL AS Pending_Note
		FROM prd
		
		UNION
		
		SELECT @projectid AS Project_id,
			@projecttypdesc AS Project_Schedule_Typ_Desc,
			@meetingtyprefid AS Meeting_Typ_Ref_Id,
			'User_Id',
			NULL,
			[user_id],
			NULL,
			NULL AS meeting_room_ref_id,
			NULL AS Pending_Note
		FROM us
		
		UNION
		
		SELECT @projectid AS Project_id,
			@projecttypdesc AS Project_Schedule_Typ_Desc,
			@meetingtyprefid AS Meeting_Typ_Ref_Id,
			'Cycle_Nbr',
			NULL,
			NULL,
			cycle_nbr,
			NULL AS meeting_room_ref_id,
			NULL AS Pending_Note
		FROM cycle
		
		UNION
		
		SELECT @projectid AS Project_id,
			@projecttypdesc AS Project_Schedule_Typ_Desc,
			@meetingtyprefid AS Meeting_Typ_Ref_Id,
			'Pending_Note',
			NULL,
			NULL,
			NULL,
			NULL AS meeting_room_ref_id,
			notes_comment AS Pending_Note
		FROM pendingnotes;
	END

	IF (@projecttypdesc = 'PMA')
	BEGIN
		WITH pma
		AS (
			SELECT pma.*
			FROM [AION].preliminary_meeting_appointment pma
			WHERE project_id = @projectid
			),
		ps
		AS (
			SELECT ps.*
			FROM [AION].project_schedule ps
			INNER JOIN pma ON ps.appt_id = pma.preliminary_meeting_appt_id
				AND ps.project_schedule_typ_desc = 'PMA'
			),
		us
		AS (
			SELECT us.*
			FROM [AION].user_schedule us
			INNER JOIN ps ON us.project_schedule_id = ps.project_schedule_id
			),
		pendingnotes
		AS (
			SELECT notes.*
			FROM AION.notes
			INNER JOIN AION.notes_type_ref typ ON notes.Notes_typ_ref_id = typ.notes_typ_ref_id
			WHERE typ.enum_mapping_val_nbr = 3
				AND project_id = @projectid
			)
		SELECT @projectid AS Project_id,
			@projecttypdesc AS Project_Schedule_Typ_Desc,
			@meetingtyprefid AS Meeting_Typ_Ref_Id,
			'Start_Dt' AS row_type,
			pma.from_dt AS start_dt,
			NULL AS [scheduled_user_id],
			NULL AS cycle_nbr,
			pma.meeting_room_ref_id AS meeting_room_ref_id,
			NULL AS Pending_Note
		FROM pma
		
		UNION
		
		SELECT @projectid AS Project_id,
			@projecttypdesc AS Project_Schedule_Typ_Desc,
			@meetingtyprefid AS Meeting_Typ_Ref_Id,
			'User_Id',
			NULL,
			[user_id],
			NULL,
			NULL AS meeting_room_ref_id,
			NULL AS Pending_Note
		FROM us
		
		UNION
		
		SELECT @projectid AS Project_id,
			@projecttypdesc AS Project_Schedule_Typ_Desc,
			@meetingtyprefid AS Meeting_Typ_Ref_Id,
			'Pending_Note',
			NULL,
			NULL,
			NULL,
			NULL AS meeting_room_ref_id,
			notes_comment AS Pending_Note
		FROM pendingnotes;
	END

	IF (@projecttypdesc = 'FMA')
	BEGIN
		WITH fma
		AS (
			SELECT fma.*
			FROM [AION].facilitator_meeting_appointment fma
			WHERE project_id = @projectid
				AND meeting_typ_ref_id = @meetingtyprefid
			),
		ps
		AS (
			SELECT ps.*
			FROM [AION].project_schedule ps
			INNER JOIN fma ON ps.appt_id = fma.facilitator_meeting_appt_identifier
				AND ps.project_schedule_typ_desc = 'FMA'
			),
		us
		AS (
			SELECT us.*
			FROM [AION].user_schedule us
			INNER JOIN ps ON us.project_schedule_id = ps.project_schedule_id
			)
		SELECT @projectid AS Project_id,
			@projecttypdesc AS Project_Schedule_Typ_Desc,
			@meetingtyprefid AS Meeting_Typ_Ref_Id,
			'Start_Dt' AS row_type,
			fma.from_dt AS start_dt,
			NULL AS [scheduled_user_id],
			NULL AS cycle_nbr,
			fma.meeting_room_ref_id AS meeting_room_ref_id,
			NULL AS Pending_Note
		FROM fma
		
		UNION
		
		SELECT @projectid AS Project_id,
			@projecttypdesc AS Project_Schedule_Typ_Desc,
			@meetingtyprefid AS Meeting_Typ_Ref_Id,
			'User_Id',
			NULL,
			[user_id],
			NULL,
			NULL AS meeting_room_ref_id,
			NULL AS Pending_Note
		FROM us;
	END
END
