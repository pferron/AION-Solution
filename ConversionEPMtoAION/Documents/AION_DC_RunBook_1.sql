

--========== USER MANAGMENT =================
--Isolate users in order to preserve the eisting users that were created in whichever envrioment this script is being run
SELECT  * FROM [AION].[USER] where ([CREATED_DTTM] <'2020-04-30 14:37:02.223') 
 DELETE  FROM [AION].[USER] where ([CREATED_DTTM] <'2020-04-30 14:37:02.223') 

--  DISABLE TRIGGER FOR USERS TABLE
  DISABLE TRIGGER [AION].[ust_USER_changelog] ON [AION].[USER]



 --TWO OPTIONS TO LOAD USERS, CLEAN AND THAN LOAD OR LOAD ALL USERS AND THAN CLEAN. sAFER TO LOAD ALL THE USERS AND THAN DELETE THE USERS SO NO USER IS LEFT OUT SINCE AION TABLE STRUCTURE IS IN FLUX
 --select * from [AION].[USER] where [USER_ID] >999
--DELETE FROM [AION].[USER] where [USER_ID] >999
set QUOTED_IDENTIFIER ON
SET IDENTITY_INSERT [USER]ON
insert into   [AION].[USER] 
([USER_ID],[FIRST_NM],[LAST_NM], [USER_NM], [PHONE_NUM], [EMAIL_ADDR_TXT], [EXTERNAL_SYSTEM_REF_ID], [SRC_SYSTEM_VAL_TXT], [CITY_IND])

select distinct
CAST((CONCAT(999, [id_user])) AS int), [nme_first], [nme_last], ([txt_email]), 
[txt_phone],([txt_email]) , -1 , 
--[txt_username], 
[txt_username]+'EPM'+CAST([id_user] AS varchar),

( case 
when [city_access_flg]  = 'Y' then 1
when [city_access_flg]  = 'N' then 0
else null end)

from [dbo].[tb_users] 






 --LOAD ALL HITORICAL USERS AND THAN DELET ONLY THE ONES WHO ARE COUNTY AND ACTIVE LICENCES PROFESSIONAL WHO ARE PROJECT MANAGERS
--DELETE ALL THE USERS WHERE 
 (
   SELECT  * FROM [AION].[USER] where [USER_ID] in 
   (
    (select distinct ASSIGNED_ESTIMATOR_ID FROM [AION].[PROJECT]
      union
        select distinct ASSIGNED_FACILITATOR_ID FROM [AION].[PROJECT]
      union
        select distinct USER_ID FROM [AION].[USER_SCHEDULE]
      union
        select distinct ASSIGNED_PLAN_REVIEWER_ID FROM AION.PROJECT_BUSINESS_RELATIONSHIP
      union 
        select distinct USER_ID FROM [AION].[USER_SYSTEM_ROLE_RELATIONSHIP]
      union
        select distinct PROJECT_MANAGER_ID FROM [AION].[PROJECT])
      union
    (
      SELECT  [USER_ID] FROM [AION].[USER] where ([CREATED_DTTM] <'2020-04-30 14:37:02.223') 
      or ([CREATED_DTTM] >'2021-07-15 13:10:36.670' and [CREATED_DTTM] < '2021-10-16 20:57:09.903')
      or ([CREATED_DTTM] >'2021-10-17 20:57:09.903')) 
    )
)




 -- UPDATE USERS WHO ARE CITY AND COUNTY USERS BY UPDATIGN THE INDICATOR COLUNM IN AION WITH THE LIST PROVIDED BY SARA AND JOHAN
 UPDATE [AION].[USER]
SET  [CITY_IND] = 1
where  [USER_ID] in (LATEST_EXCEL_FOR_CITY_COUNTY_USERS)





--====================LOAD PROJECTS========================

--DELETE PREVIOUS RECORDS FROM OTHER REFERENCE TABLES THAT ARE CONNECTED TO PROJECTS TABLE
DELETE FROM [AION].[PROJECT]
DELETE FROM AION.PLAN_REVIEW_SCHEDULE
DELETE from AION.PROJECT_AUDIT
DELETE FROM AION.PLAN_REVIEW_PROJECT_DETAILS
DELETE FROM AION.EXPRESS_MEETING_APPOINTMENT
DELETE FROM AION.PROJECT_EMAIL_NOTIFICATION
DELETE FROM AION.NOTIFICATION_EMAIL_LIST
DELETE FROM AION.FIFO_SCHEDULE
DELETE FROM AION.USER_SCHEDULE_STAGE
DELETE FROM AION.FACILITATOR_MEETING_APPOINTMENT
DELETE FROM AION.SCHEDULE_BUSINESS_RELATIONSHIP_STAGE
DELETE FROM AION.PRELIMINARY_MEETING_APPOINTMENT
DELETE FROM AION.PROJECT_AUDIT

--TWO OPTIONS TO  MAKE SURE ALL THE PROJECTS THAT WERE LOADED INTO ACCELA ARE LOADED INTO AION. LOAD ALL THE REC_IDs INTO PROJECT TABLE AND THAN LOAD OR LOAD BY JOINING THE PROJECT TABLE (MAY CAUSE ISSUE IF THE PROJECT IS MISSED ON ACCELA)
insert INTO [AION].[PROJECT](
[SRC_SYSTEM_VAL_TXT],
[REC_ID_TXT],
[PROJECT_MODE_REF_ID])
(SELECT  [project]
      ,[rec_id], -1
  FROM [AION_CNV].[rec_id_3] -- THIS IS A TEMP TABLE FROM THE EXCEL THAT SUMEET PROVIDES 
)

--LOAD THE AION PROJECT TABLE WITH THE EPM CURRENT TABLES. CURRENT TABLE STRUCTURE OF AION DOES NOT ALLOW HISTORICAL PROJECT DATA, ONLY CYCLE DATA
--set QUOTED_IDENTIFIER ON
--SET IDENTITY_INSERT [AION].[PROJECT] ON
insert INTO [AION].[PROJECT](
  [PROJECT_ID], 
  [PROJECT_NM],
  [EXTERNAL_SYSTEM_REF_ID],
  [PROJECT_STATUS_REF_ID],
  [PROJECT_TYP_REF_ID],
  [ASSIGNED_ESTIMATOR_ID],
  [ASSIGNED_FACILITATOR_ID],
  [PROJECT_MODE_REF_ID],
  [RTAP_IND],
  [PROJECT_ADDR_TXT],
  [TOTAL_FEE_AMT],
  [PROJECT_MANAGER_ID], 
  --[SRC_SYSTEM_VAL_TXT], -- loaded on first step
  --[REC_ID_TXT],
  [TAG_CREATED_BY_TS],
  [TAG_UPDATED_BY_TS],
  [PRELIMINARY_IND], 
  [GATE_ACCEPTED_IND],
  [FIFO_IND],
  [PLANS_READY_ON_DT],
  [TEAM_GRADE_TXT],
  [GATE_DT],
  [CYCLE_NBR],
  [BUILD_CODE_VERSION_DESC],

  [REVIEW_TYP_REF_DESC],
  [CODE_SUMMARY_DESC],
  [CONSTR_TYP_DESC],
  [WORK_TYP_DESC],
  [OVERALL_WORK_SCOPE_DESC],
  [ELCTR_WORK_SCOPE_DESC],
  [MECH_WORK_SCOPE_DESC],
  [PLUMB_WORK_SCOPE_DESC],
  [CIVIL_WORK_SCOPE_DESC],
  [PERMIT_NUM],
  [SHEETS_CNT_DESC],
  [DESIGNER_DESC],
  [SEAL_HOLDERS_DESC],  
  [FIRE_DETAIL_DESC],
  [OCCUPANCY_DESC],
  [PRI_OCCUPANCY_DESC],
  [SECONDARY_OCCUPANCY_DESC],
  [SQUARE_FOOTAGE_DESC],
  [ZONING_OF_SITE_DESC],
  [CHG_OF_USE_DESC],  
  [PREVIOUS_BUSINESS_TYP_DESC],
  [PROPOSED_BUSINESS_TYP_DESC],
  [ConditionalPermitApproval],  
  [CITY_OF_CHARLOTTE_DESC],  
  [WATER_SEWER_DETAIL_DESC],
  [PROPOSED_OUTDOOR_UNDERGROUND_PIPING_DESC],
  [PROPOSED_FIRE_SPRINKLER_PIPING_DESC],
  [INSTALL_CMUD_BACKFLOW_PREVENTER_DESC],
  [EXTENDING_PUBLIC_WATER_SEWER_DESC],
  [GRADE_MOD_WATER_SEWER_EASEMENT_DESC],
  [PROPOSED_ENCROACHMENT_WATER_SEWER_EASEMENT_DESC],
  [DAY_CARE_DESC],
  [HEALTH_DEPT_DETAIL_DESC]
  )




SELECT  distinct  
p.project_number as PROJECT_ID, 
nme_project as PROJECT_NM , 
-1 as EXTERNAL_SYSTEM_REF_ID,
( case 
when [id_project_state] = 1003 then 6
when [id_project_state]  = 1004 then 7
when [id_project_state] = 1015 then 8
when [id_project_state]  = 1016 then 10
when [id_project_state] = 1005 then 11
when [id_project_state]  = 1006 then 13
when [id_project_state] = 1025 then 14
when [id_project_state]  = 1026 then 16
when [id_project_state] = 1025 then 17
when [id_project_state]  = 1013 then 22
when [id_project_state]  = 1027 then 22
when [id_project_state] = 1011 then 24
when [id_project_state]  = 1012 then 26
else -1 end) as PROJECT_STATUS_REF_ID,
( case 
when [txt_review_type_code]  = 'RT' then 2
when [txt_review_type_code]  = 'OS' then 2
when [txt_review_type_code]  = 'ER' then 2
when [txt_review_type_code]  = 'MP' then 2
when [txt_review_type_code]  = 'PS' then 2
when [txt_review_type_code]  = 'PL' then 2
when [txt_review_type_code]  = 'ER' then 1
else -1 end) as PROJECT_TYP_REF_ID,
(case when u.id_user  is null then null else CAST((CONCAT(999, u.id_user)) AS int) end) as ASSIGNED_ESTIMATOR_ID,  
(case when id_project_coordinator  is null then null else CAST((CONCAT(999, id_project_coordinator)) AS int) end) as ASSIGNED_FACILITATOR_ID, 
( case 
when [txt_review_type_code]  = 'OS' then 2
when [txt_review_type_code]  = 'ER' then 1
else -1 end) as PROJECT_MODE_REF_ID,
( case when [txt_review_type_code]  = 'RT' then 1 else 0 end) as RTAP_IND, 
txt_address as PROJECT_ADDR_TXT,
permit_fee as TOTAL_FEE_AMT,
(case when id_project_manager  is null then null else CAST((CONCAT(999, id_project_manager)) AS int) end) as PROJECT_MANAGER_ID, 
application_received_on as TAG_CREATED_BY_TS, --fixed by Carlos Mac Beath --
performed_on as TAG_UPDATED_BY_TS, --fixed by Carlos Mac Beath --
( case when [txt_review_type_code]  = 'PL' then 1 else 0 end) as PRELIMINARY_IND, 
0 as GATE_ACCEPTED_IND,
0 as FIFO_IND
,plans_ready_on as PLANS_READY_ON_DT
,team_score_indicator as TEAM_GRADE_TXT  
,[gate_close_date] as GATE_DT
,[assessment_cycle] as CYCLE_NBR
,[txt_cde_summary_code] as BUILD_CODE_VERSION_DESC
,CASE app_form_xml.value('(//rblPropertyType)[1]', 'varchar(max)')
	  WHEN 'FA' THEN '1-2 Family -=> 3.5 Stories'
	  WHEN 'TH' THEN 'TH, LFS -=> 3.5 Stories'
	  WHEN 'CN' THEN 'Condo or Apts'
	  WHEN 'CO' THEN 'Commercial'
   ELSE ''
 END AS [REVIEW_TYP_REF_DESC]


FROM 
  dbo.tb_project_current_status p 
LEFT JOIN dbo.tb_users u on p.txt_performed_by = u.txt_username
WHERE 	
[txt_review_type_code]  != 'WL'

 -- ===============ESTIMATION TABLE=============================
 --DELETE FROM [AION].[PROJECT_BUSINESS_RELATIONSHIP] 
--set QUOTED_IDENTIFIER ON
--SET IDENTITY_INSERT [AION].[PROJECT_BUSINESS_RELATIONSHIP] OFF
insert INTO [azure_sustst].[tst_sustainable].[AION].[PROJECT_BUSINESS_RELATIONSHIP](

      [ESTIMATION_HOURS_NBR]
      ,[BUSINESS_REF_ID]
      ,[PROJECT_ID]
      ,[ASSIGNED_PLAN_REVIEWER_ID]
      ,[ESTIMATION_NOT_APPLICABLE_IND]--0
      ,[PROJECT_BUSINESS_RELATIONSHIP_STATUS_DESC] --C
      ,[PROJECT_STATUS_REF_ID]--7
      ,[IS_DEPT_REQUESTED_IND]--0
)
 select distinct  
[txt_est_hrs],
( case 
when [txt_trade_code] = 'BL' then 1
when [txt_trade_code]  = 'EL' then 2
when [txt_trade_code]  = 'MC' then 3
when [txt_trade_code]  = 'PL' then 4
when [txt_trade_code]  = 'ZN' then 5
when [txt_trade_code]  = 'FR' then 13
when [txt_trade_code]  = 'BF' then 25
when [txt_trade_code]  = 'HL' then 22
when [txt_trade_code]  = 'PA' then 23
when [txt_trade_code]  = 'LD' then 24
when [txt_trade_code]  = 'DC' then 21
else -1 end),
  pp.[PROJECT_ID],  --a.[project_number],
(case when b.[id_user]  is null then null else CAST((CONCAT(999, b.[id_user])) AS int) end),  --b.[id_user]
(case 
when [txt_est_state_code] = 'NA' then 1
else 0 end),
'C',
--22,
( case 
when [id_project_state] = 1003 then 6
when [id_project_state]  = 1004 then 7
when [id_project_state] = 1015 then 8
when [id_project_state]  = 1016 then 10
when [id_project_state] = 1005 then 11
when [id_project_state]  = 1006 then 13
when [id_project_state] = 1025 then 14
when [id_project_state]  = 1026 then 16
when [id_project_state] = 1025 then 17
when [id_project_state]  = 1013 then 22
when [id_project_state]  = 1027 then 22
when [id_project_state] = 1011 then 24
when [id_project_state]  = 1012 then 26
else -1 end),


0
FROM [dbo].[tb_est_current_status] a
 LEFT JOIN [dbo].[tb_users] b on [txt_performed_by] = b.txt_username
 LEFT JOIN dbo.tb_project_current_status p  on a.[project_number]= p.[project_number]
 LEFT JOIN [azure_sustst].[tst_sustainable].[AION].[project] pp  on a.[project_number]= pp.[SRC_SYSTEM_VAL_TXT]


--============PROJECT TASKS==================
--set QUOTED_IDENTIFIER ON
--SET IDENTITY_INSERT [AION].[PROJECT_AUDIT] OFF
INSERT INTO [azure_sustst].[tst_sustainable].[AION].[PROJECT_AUDIT]
           ([PROJECT_ID]
           ,[AUDIT_USER_NM]
           ,[AUDIT_DT]
           ,[AUDIT_ACTION_REF_ID])
(select  distinct 
pp.[PROJECT_ID]  --p.[project_number]          

,(case when [id_user]  is null then null else CAST((CONCAT(999, [id_user])) AS int) end) --,[id_user]
,(p.[performed_on])
,
( case 
when p.[id_task] =	112	then 1
when p.[id_task] =	206	then 28
when p.[id_task] =	225	then 43
when p.[id_task] =	202	then 49
when p.[id_task] =	203	then 48
when p.[id_task] =	205	then 50
when p.[id_task] =	232	then 44
else -1 end)

		   FROM [dbo].[tb_project_history] p
		   LEFT JOIN dbo.tb_users u on p.txt_performed_by = u.txt_username
LEFT JOIN [azure_sustst].[tst_sustainable].[AION].[project] pp  on p.[project_number]= pp.[SRC_SYSTEM_VAL_TXT]
		   WHERE   p.[id_task] in (112,206,225,202,203,205,232	)
		  
)

--==========PROJECT NOTES============
--select * from [AION].[NOTES]
--set QUOTED_IDENTIFIER ON
--SET IDENTITY_INSERT [AION].[NOTES] OFF

insert INTO [azure_sustst].[tst_sustainable].[AION].[NOTES]( 
 [NOTES_TYP_REF_ID],
 [PROJECT_ID],
 [NOTES_COMMENT], 
 [BUSINESS_REF_ID],
 [CREATED_DTTM],
 [UPDATED_DTTM], 
 [WKR_ID_CREATED_TXT],
 [WKR_ID_UPDATED_TXT])
 
 (
SELECT  
  1, --internal notes
 pp.[PROJECT_ID], -- try_convert(int, [project_number]), 
  [txt_int_notes],
  case 
when txt_trade_code = 'BL' then 1
when txt_trade_code = 'EL' then 2
when txt_trade_code = 'MC' then 3
when txt_trade_code = 'PL' then 4
when txt_trade_code = 'ZN' then 5
when txt_trade_code = 'FR' then 13
when txt_trade_code = 'BF' then 25
when txt_trade_code = 'HL' then 22
when txt_trade_code = 'PA' then 23
when txt_trade_code = 'LD' then 24
when txt_trade_code = 'DC' then 21
else -1 end,
  [performed_on],
  [performed_on],
  (case when u.[id_user]  is null then null else CAST((CONCAT(999, u.[id_user])) AS int) end),  --u.id_user
  (case when u.[id_user]  is null then null else CAST((CONCAT(999, u.[id_user])) AS int) end)  --u.id_user
  FROM [dbo].[tb_est_current_status] e
  LEFT JOIN dbo.tb_users u on e.txt_performed_by = u.txt_username
  LEFT JOIN [azure_sustst].[tst_sustainable].[AION].[project] pp  on e.[project_number]= pp.[SRC_SYSTEM_VAL_TXT]

union

SELECT  
  2, --gate notes
 pp.[PROJECT_ID], -- try_convert(int, [project_number]), 
  [txt_gate_notes],
  case 
when txt_trade_code = 'BL' then 1
when txt_trade_code = 'EL' then 2
when txt_trade_code = 'MC' then 3
when txt_trade_code = 'PL' then 4
when txt_trade_code = 'ZN' then 5
when txt_trade_code = 'FR' then 13
when txt_trade_code = 'BF' then 25
when txt_trade_code = 'HL' then 22
when txt_trade_code = 'PA' then 23
when txt_trade_code = 'LD' then 24
when txt_trade_code = 'DC' then 21
else -1 end,
  [performed_on],
  [performed_on],
  (case when u.[id_user]  is null then null else CAST((CONCAT(999, u.[id_user])) AS int) end),  --u.id_user
  (case when u.[id_user]  is null then null else CAST((CONCAT(999, u.[id_user])) AS int) end)  --u.id_user
  FROM [dbo].[tb_est_current_status] e
  LEFT JOIN dbo.tb_users u on e.txt_performed_by = u.txt_username
  LEFT JOIN [azure_sustst].[tst_sustainable].[AION].[project] pp  on e.[project_number]= pp.[SRC_SYSTEM_VAL_TXT]


union



SELECT  
  3, --cust/gate notes
 pp.[PROJECT_ID], -- try_convert(int, [project_number]), 
  [txt_cust_notes],
  case 
when txt_trade_code = 'BL' then 1
when txt_trade_code = 'EL' then 2
when txt_trade_code = 'MC' then 3
when txt_trade_code = 'PL' then 4
when txt_trade_code = 'ZN' then 5
when txt_trade_code = 'FR' then 13
when txt_trade_code = 'BF' then 25
when txt_trade_code = 'HL' then 22
when txt_trade_code = 'PA' then 23
when txt_trade_code = 'LD' then 24
when txt_trade_code = 'DC' then 21
else -1 end,
  [performed_on],
  [performed_on],
  (case when u.[id_user]  is null then null else CAST((CONCAT(999, u.[id_user])) AS int) end),  --u.id_user
  (case when u.[id_user]  is null then null else CAST((CONCAT(999, u.[id_user])) AS int) end)  --u.id_user
  FROM [dbo].[tb_est_current_status] e
  LEFT JOIN dbo.tb_users u on e.txt_performed_by = u.txt_username
  LEFT JOIN [azure_sustst].[tst_sustainable].[AION].[project] pp  on e.[project_number]= pp.[SRC_SYSTEM_VAL_TXT]
  )



--=================LOAD PROJECT SCHDUELS===============
--LOAD HISTORICAL PROJECT CYCLES, BUT ONLY THE LATEST ONE IS TIED TO THE HISTORICAL SCHDUEL ON AION APPLICATION
  insert INTO [azure_sustst].[tst_sustainable].[AION].[PROJECT_CYCLE] (
	   [PROJECT_ID]
	   ,[CYCLE_NBR]
	  ,[PLANS_READY_ON_DT]
	  ,[GATE_DT]
	  ,[CURRENT_CYCLE_IND]-- 1 for max, all else 0
	  ,[FUTURE_CYCLE_IND] --0
	  ,[IS_COMPLETE_IND] --0
	  )
	  (
	  SELECT distinct --TOP (10) 
	  pa.[PROJECT_ID]
	  ,[assessment_cycle] as CYCLE_NBR
	  ,max (plans_ready_on)
	  ,max([gate_close_date])
	  ,0
	  ,0
	  ,0
	  FROM [tst_EPM].[dbo].[tb_project_history] pe
	  inner join [azure_sustst].[tst_sustainable].[AION].[PROJECT] pa on pa.[SRC_SYSTEM_VAL_TXT] = pe.project_number
	  --where [project_number] = '350250'
	  group by pa.[PROJECT_ID] ,[assessment_cycle]
	  )

      -- update the max cycle so teh "Current_cycle_IND" is 1 while older ones are 0

      UPDATE [AION].project_cycle 
SET  [CURRENT_CYCLE_IND] = 1

--SELECT tt.project_cycle_id, tt.project_id, tt.cycle_nbr, CURRENT_CYCLE_IND
FROM [AION].project_cycle tt
INNER JOIN
    (SELECT project_id, max(cycle_nbr) as m
    FROM [AION].project_cycle
	where CREATED_DTTM > '2021-11-23 16:00:00.000'
    GROUP BY project_id) g 
ON tt.project_id = g.project_id 
AND tt.cycle_nbr = g.m


--PUT WHERE CLAUSE IF THE ENVRIOMENT HAVE TEST RECORDS THAT NEED TO PRESERVDED
  --where PROJECT_ID  not in ( 12345)    )


--LOAD HISTORICAL SCHDUELES FOR THE PROJECTS
-- loading into bridge table to show what kind of project it is 
insert INTO [AION].[PLAN_REVIEW_SCHEDULE] (
 [PROJECT_CYCLE_ID]
 ,[IS_RESCHEDULE_IND]
      ,[PROJECT_SCHEDULE_TYP_DESC])
	  (SELECT  [PROJECT_CYCLE_ID]
	 ,0
      ,'PR'
  FROM [AION].[PROJECT_CYCLE])

  -- load the actual project schduel detail  table that contains the actual schduels (recently changed table, users still testing)
  insert INTO [AION].[PLAN_REVIEW_SCHEDULE_DETAIL] (
 PLAN_REVIEW_SCHEDULE_ID, 
BUSINESS_REF_ID,
START_DT,
END_DT,
ASSIGNED_HOURS_NBR,
ASSIGNED_PLAN_REVIEWER_ID,
POOL_REQUEST_IND)
	  (
      
 SELECT 
  --ps.PLAN_REVIEW_SCHEDULE_ID,
 p.PROJECT_TYP_REF_ID,
 s.[StartDate],
 s.[StartDate],
 s.[EstimatedReviewTime],
  u.[id_user], --s.[Assignee],
 s.[PoolReview][SRC_SYSTEM_VAL_TXT]

 --select p.PROJECT_ID[PROJECT_ID]
-- from [azure_sustst].[tst_sustainable].[AION].[PROJECT] p 
--select count(*)
  FROM [tst_EPM].[epm_conv].[AATABLE_ASIT_REVIEW_TASK_ACTIVATION2] s 
  inner JOIN [azure_sustst].[tst_sustainable].[AION].[PROJECT] p on p.SRC_SYSTEM_VAL_TXT = s.PERMITNUM
  inner JOIN [azure_sustst].[tst_sustainable].[AION].[PROJECT_CYCLE] pc on (p.[PROJECT_ID] = pc.[PROJECT_ID] and [CURRENT_CYCLE_IND] = 1)
 -- inner JOIN [azure_sustst].[tst_sustainable].[AION].[PLAN_REVIEW_SCHEDULE] ps on pc.[PROJECT_CYCLE_ID] = ps.[PROJECT_CYCLE_ID]
  LEFT JOIN [dbo].[tb_users] u on s.[Assignee] = u.txt_username
  )


  --- NPA load after projects loaded
SELECT  distinct  [id_appointment_type] ,[txt_appointment_type_desc]
FROM [tst_EPM].[dbo].[ctb_appointment_types]

SELECT distinct  [NON_PROJECT_APPT_TYP_REF_ID],[APPT_TYP_DESC]
FROM [azure_sustst].[tst_sustainable].[AION].[NON_PROJECT_APPOINTMENT_TYPE_REF] where [ACTIVE_IND] = 1

--LOAD the NPA Apporintments table first 
--set QUOTED_IDENTIFIER ON
--SET IDENTITY_INSERT [AION].[NON_PROJECT_APPOINTMENT] ON
insert into   [azure_sustst].[tst_sustainable].[AION].[NON_PROJECT_APPOINTMENT](
[APPT_NM] ,
[APPT_FROM_DTTM] ,
[APPT_TO_DTTM] ,
[NON_PROJECT_APPT_TYP_REF_ID],
[APPT_RECURRENCE_REF_ID])
(
SELECT TOP (10) [txt_appointment_desc],[start_date],[start_date]
	 , case 
when id_appointment_type = 2 then 4
when id_appointment_type = 4 then 10
else 1 end,
	  106
  FROM [tst_EPM].[dbo].[tb_appointments] where id_appointment_type in (2,4)
  and start_date > '2020-01-01 00:00:00.000'
)

-- loading into bridge table to show what kind of project it is 
insert INTO [AION].[PROJECT_SCHEDULE] (
 [APPT_ID]
      ,[PROJECT_SCHEDULE_TYP_DESC])
	  (SELECT  [NON_PROJECT_APPT_ID]
      ,'NPA'
  FROM [AION].[NON_PROJECT_APPOINTMENT])



--epm projects , return list of historic details from a project
select  TOP (10000) * from 
(select 
			ph.id_tb_project_history,			
			txt_performed_by = case 
				when ph.id_task = 502 then (select u1.nme_first + ' ' + u1.nme_last from tb_users u1 where u1.txt_username = mh.updated_by)
				when ph.id_task = 201 then COALESCE((select u2.nme_first + ' ' + u2.nme_last from tb_users u2 where u2.txt_username = eh.txt_performed_by), eh.txt_performed_by)
                when ph.txt_performed_by = 'MODELER' then 'Posse'
                when ph.txt_performed_by = 'SCHEDULER' then 'Posse'
				else ISNULL((select top 1 u3.nme_first + ' ' + u3.nme_last from tb_users u3 where u3.txt_username = ph.txt_performed_by), ph.txt_performed_by)
			end,
			case 
				when ph.id_task = 502 then mh.updated_on
				when ph.id_task = 201 then eh.performed_on
				else ph.performed_on
			end perf_on,
			state.nme_status,
			task.nme_task,
            task_desc = 
			case 
				when ph.id_task = 201 and eh.txt_trade_code IS NOT NULL then 
					td.txt_trade_desc + ' Estimation ' + 
						case eh.txt_est_state_code
							when 'NA' THEN 'Not Required'
							when 'PE' THEN 'Pending'
							when 'ES' THEN 'Completed'
						end
				when ph.id_task = 201 and eh.txt_trade_code IS NULL THEN 'All Estimations Completed'
				when ph.id_task = 502 THEN mh.txt_status_text
				when ph.id_task = 405 THEN 'Cycle ' + LTRIM(STR(ph.assessment_cycle)) + ' Created'
				when ph.id_task = 403 or ph.id_task = 5037 THEN 'Cycle ' + LTRIM(STR(ph.assessment_cycle)) + ' Disapproved'
				when ph.id_task = 404 or ph.id_task = 5038 THEN 'Cycle ' + LTRIM(STR(ph.assessment_cycle)) + ' Approved'
				when ph.id_task = 406 THEN 'Cycle ' + LTRIM(STR(ph.assessment_cycle)) + ' Approved'
                
                when ph.id_task = 603 THEN /* Waive Fees */     COALESCE(ftd.nme_fee_type, 'Fee') + ' Waived'
                when ph.id_task = 604 THEN /* Collect Fees */   COALESCE(ftd.nme_fee_type, 'Fee') + ' Collected'
                when ph.id_task = 605 THEN /* Pay Fees */       COALESCE(ftd.nme_fee_type, 'Fee') + ' Paid'
                
				when ph.id_task = 1002 THEN 
						case when la.txt_link_action_type_code = 'L' THEN 'Project Linked'
								 when la.txt_link_action_type_code = 'D' THEN 'Project Link Removed'
						end
				when ph.id_task = 2040 then ptd.txt_trade_desc + ' Trade Result: ' + prh.result
				when ph.id_task = 2050 then 'Minutes Review Cycle ' + LTRIM(STR(ph.assessment_cycle)) + ': Disapproved'
				when ph.id_task = 2055 then 'Minutes Review Cycle ' + LTRIM(STR(ph.assessment_cycle)) + ': Approved'
                
                when ph.id_task = 3000 THEN /* Add Fees */      COALESCE(ftd.nme_fee_type, 'Fee') + ' Added'
                when ph.id_task = 3014 THEN /* Update Fees */   COALESCE(ftd.nme_fee_type, 'Fee') + ' Updated'
                
                when ph.id_task = 3016 then /* Start Plan Review */ 
                    txt_history_text + ': Cycle ' + cast(ph.assessment_cycle as varchar)
                
                when ph.id_task = 3007 then /* Enter Trade Review Result */ 
                    rrt_td.txt_trade_desc + ' ' + txt_history_text
                
                when ph.id_task = 3008 then /* Enter Agency Review Result */ 
                    rrca_ad.nme_agency + ' ' + txt_history_text
                
                when ph.id_task = 5004 or ph.id_task = 5005 or ph.id_task = 5027 then /* Save, Accept or Reject Intake */ 
                     case when ia.id_agency_def = 4 then 'County '
                          else 'Town '
                     end + txt_history_text
                
                when ph.id_task = 5006 THEN /* Create Cycle */ 'Cycle ' + LTRIM(STR(ph.assessment_cycle)) + ' Created'
                when ph.id_task = 5008 THEN /* Reopen Cycle */ 'Cycle ' + LTRIM(STR(ph.assessment_cycle)) + ' Reopened'
                when ph.id_task = 5010 THEN /* Add Fees */      COALESCE(ftd.nme_fee_type, 'Fee') + ' Added'
                when ph.id_task = 5011 THEN /* Waive Fees */    COALESCE(ftd.nme_fee_type, 'Fee') + ' Waived'
                when ph.id_task = 5012 THEN /* Collect Fees */  COALESCE(ftd.nme_fee_type, 'Fee') + ' Collected'
                
                when ph.id_task = 5021 then /* WLR Enter Agency Review Result */ 
                    rra_ad.nme_agency + ' ' + txt_history_text
                
                when ph.id_task = 5025 THEN /* Update Fees */   COALESCE(ftd.nme_fee_type, 'Fee') + ' Updated'
                
				else txt_history_text                
			end
			from 
				tb_project_history ph 
                left outer join ctb_state_defs state on ph.id_project_state = state.id_status 
                left outer join ctb_task_defs task on ph.id_task = task.id_task 
                left outer join tb_est_history eh on eh.id_project_history = ph.id_tb_project_history 
                left outer join ctb_trade_defs td on td.txt_trade_code = eh.txt_trade_code 
                left outer join tb_meeting_history mh on mh.id_project_history = ph.id_tb_project_history 
                left outer join tb_link_actions la on ph.id_tb_project_history = la.id_project_history 
                left outer join tb_prelim_results_history prh on ph.id_tb_project_history = prh.id_project_history and ph.id_task = 2040 
                left outer join ctb_trade_defs ptd on ptd.txt_trade_code = prh.txt_trade_code

                left outer join tb_rvw_res_trade_history rrt on ph.id_task = 3007 /* Enter Trade Review Result */ and ph.id_tb_project_history = rrt.id_tb_project_history
                left outer join ctb_trade_defs rrt_td on rrt.txt_trade_code = rrt_td.txt_trade_code
            
                left outer join tb_rvw_res_ce_agency_history rrca on ph.id_task = 3008 /* Enter Agency Review Result */ and ph.id_tb_project_history = rrca.id_tb_project_history
                left outer join ctb_agency_defs rrca_ad on rrca.id_agency = rrca_ad.id_agency_def
                                        
                left outer join tb_intake_action ia on ph.id_tb_project_history = ia.id_tb_project_history
                
				left outer join tb_rvw_res_agency_history rra on ph.id_task = 5021 /* WLR Enter Agency Review Result */ and ph.id_tb_project_history = rra.id_tb_project_history
                left outer join ctb_agency_defs rra_ad on rra.id_agency_def = rra_ad.id_agency_def
            
                left outer join tb_project_fee_history pfh on ph.id_task in (603, 604, 605, 3000, 3014, 5010, 5011, 5012, 5025) /* Fee Related Tasks */ and ph.id_tb_project_history = pfh.id_tb_project_history
                left outer join ctb_feetype_defs ftd on pfh.id_fee_types = ftd.id_fee_types
			where
				ph.project_number = '417111'				
			--order by        
			--	ph.id_tb_project_history asc 
				) as temp

--locate a permit id by project name:
SELECT *
  FROM [tst_EPM].[epm_conv].[AATABLE_PERMIT_HISTORY]
    WHERE APP_NAME like '%Menya Darum%'

--update historic data on AIO historic screen:
USE [tst_sustainable]
GO
UPDATE [AION].[PROJECT_AUDIT] 
   SET [AUDIT_DT] = DATEADD(HOUR, 5, AUDIT_DT)   
   where [AION].[PROJECT_AUDIT].PROJECT_AUDIT_ID='707539' 
GO



exclude :
Review Date Rejected
Appointments Created (Auto)
Appointments Cancelled
Project Coordinator Assigned



SELECT 
RESULT.PROJECT_ID,
RESULT.AUDIT_USER_NM,
RESULT.txt_userid,
RESULT.AUDIT_DT,
RESULT.AUDIT_ACTION_REF_ID
FROM (
select
	PROJECT_ID=510662,
	txt_userid,
	AUDIT_USER_NM=(select  
					(case when [id_user]  is null then null else CAST((CONCAT(999, [id_user])) AS int) end) id_user					 
				      FROM [dbo].[tb_project_history] p
							   LEFT JOIN dbo.tb_users u on p.txt_performed_by = u.txt_username
					LEFT JOIN [azure_sustst].[tst_sustainable].[AION].[project] pp  on p.[project_number]= pp.[SRC_SYSTEM_VAL_TXT]
					where p.id_tb_project_history =temp2.id_tb_project_history),	
	DATEADD(HOUR, 5, temp2.perf_on)    AS AUDIT_DT,
	AUDIT_ACTION_REF_ID=(SELECT [AUDIT_ACTION_REF_ID]     
						FROM [azure_sustst].[tst_sustainable].[AION].[AUDIT_ACTION_REF]
						where AUDIT_ACTION_NM=temp2.task_desc),		
	  temp2.task_desc	  
from (
	select  * from (select
			ph.id_tb_project_history,			
			ph.project_number,
			txt_userid = case 
				when ph.id_task = 502 then (select u1.nme_first + ' ' + u1.nme_last  from tb_users u1 where u1.txt_username = mh.updated_by)
				when ph.id_task = 201 then COALESCE((select u2.nme_first + ' ' + u2.nme_last from tb_users u2 where u2.txt_username = eh.txt_performed_by), eh.txt_performed_by)
                when ph.txt_performed_by = 'MODELER' then 'Posse'
                when ph.txt_performed_by = 'SCHEDULER' then 'Posse'
				else ISNULL((select top 1 u3.nme_first + ' ' + u3.nme_last  from tb_users u3 where u3.txt_username = ph.txt_performed_by), ph.txt_performed_by)
			end,
			userid = case 
				when ph.id_task = 502 then (select CAST(u1.id_user as varchar(12)) from tb_users u1 where u1.txt_username = mh.updated_by)
				when ph.id_task = 201 then COALESCE((select CAST(u2.id_user as varchar(12))  from tb_users u2 where u2.txt_username = eh.txt_performed_by), eh.txt_performed_by)
                when ph.txt_performed_by = 'MODELER' then 'Posse'
                when ph.txt_performed_by = 'SCHEDULER' then 'Posse'
				else ISNULL((select top 1 CAST(u3.id_user as varchar(12)) from tb_users u3 where u3.txt_username = ph.txt_performed_by), ph.txt_performed_by)
			end,
			txt_performed_by = case 
				when ph.id_task = 502 then (select u1.nme_first + ' ' + u1.nme_last from tb_users u1 where u1.txt_username = mh.updated_by)
				when ph.id_task = 201 then COALESCE((select u2.nme_first + ' ' + u2.nme_last from tb_users u2 where u2.txt_username = eh.txt_performed_by), eh.txt_performed_by)
                when ph.txt_performed_by = 'MODELER' then 'Posse'
                when ph.txt_performed_by = 'SCHEDULER' then 'Posse'
				else ISNULL((select top 1 u3.nme_first + ' ' + u3.nme_last from tb_users u3 where u3.txt_username = ph.txt_performed_by), ph.txt_performed_by)
			end,
			case 
				when ph.id_task = 502 then mh.updated_on
				when ph.id_task = 201 then eh.performed_on
				else ph.performed_on
			end perf_on,			 
            task_desc = 
			case 
				when ph.id_task = 201 and eh.txt_trade_code IS NOT NULL then 
					td.txt_trade_desc + ' Estimation ' + 
						case eh.txt_est_state_code
							when 'NA' THEN 'Not Required'
							when 'PE' THEN 'Pending'
							when 'ES' THEN 'Completed'
						end
				when ph.id_task = 201 and eh.txt_trade_code IS NULL THEN 'All Estimations Completed'
				when ph.id_task = 502 THEN mh.txt_status_text
				when ph.id_task = 405 THEN 'Cycle ' + LTRIM(STR(ph.assessment_cycle)) + ' Created'
				when ph.id_task = 403 or ph.id_task = 5037 THEN 'Cycle ' + LTRIM(STR(ph.assessment_cycle)) + ' Disapproved'
				when ph.id_task = 404 or ph.id_task = 5038 THEN 'Cycle ' + LTRIM(STR(ph.assessment_cycle)) + ' Approved'
				when ph.id_task = 406 THEN 'Cycle ' + LTRIM(STR(ph.assessment_cycle)) + ' Approved'
                
                when ph.id_task = 603 THEN /* Waive Fees */     COALESCE(ftd.nme_fee_type, 'Fee') + ' Waived'
                when ph.id_task = 604 THEN /* Collect Fees */   COALESCE(ftd.nme_fee_type, 'Fee') + ' Collected'
                when ph.id_task = 605 THEN /* Pay Fees */       COALESCE(ftd.nme_fee_type, 'Fee') + ' Paid'
                
				when ph.id_task = 1002 THEN 
						case when la.txt_link_action_type_code = 'L' THEN 'Project Linked'
								 when la.txt_link_action_type_code = 'D' THEN 'Project Link Removed'
						end
				when ph.id_task = 2040 then ptd.txt_trade_desc + ' Trade Result: ' + prh.result
				when ph.id_task = 2050 then 'Minutes Review Cycle ' + LTRIM(STR(ph.assessment_cycle)) + ': Disapproved'
				when ph.id_task = 2055 then 'Minutes Review Cycle ' + LTRIM(STR(ph.assessment_cycle)) + ': Approved'
                
                when ph.id_task = 3000 THEN /* Add Fees */      COALESCE(ftd.nme_fee_type, 'Fee') + ' Added'
                when ph.id_task = 3014 THEN /* Update Fees */   COALESCE(ftd.nme_fee_type, 'Fee') + ' Updated'
                
                when ph.id_task = 3016 then /* Start Plan Review */ 
                    txt_history_text + ': Cycle ' + cast(ph.assessment_cycle as varchar)
                
                when ph.id_task = 3007 then /* Enter Trade Review Result */ 
                    rrt_td.txt_trade_desc + ' ' + txt_history_text                
                when ph.id_task = 3008 then /* Enter Agency Review Result */ 
                    rrca_ad.nme_agency + ' ' + txt_history_text                
                when ph.id_task = 5004 or ph.id_task = 5005 or ph.id_task = 5027 then /* Save, Accept or Reject Intake */ 
                     case when ia.id_agency_def = 4 then 'County '
                          else 'Town '
                     end + txt_history_text                
                when ph.id_task = 5006 THEN /* Create Cycle */ 'Cycle ' + LTRIM(STR(ph.assessment_cycle)) + ' Created'
                when ph.id_task = 5008 THEN /* Reopen Cycle */ 'Cycle ' + LTRIM(STR(ph.assessment_cycle)) + ' Reopened'
                when ph.id_task = 5010 THEN /* Add Fees */      COALESCE(ftd.nme_fee_type, 'Fee') + ' Added'
                when ph.id_task = 5011 THEN /* Waive Fees */    COALESCE(ftd.nme_fee_type, 'Fee') + ' Waived'
                when ph.id_task = 5012 THEN /* Collect Fees */  COALESCE(ftd.nme_fee_type, 'Fee') + ' Collected'                
                when ph.id_task = 5021 then /* WLR Enter Agency Review Result */ 
                    rra_ad.nme_agency + ' ' + txt_history_text                
                when ph.id_task = 5025 THEN /* Update Fees */   COALESCE(ftd.nme_fee_type, 'Fee') + ' Updated'                
				else txt_history_text                
			end
			from 
				tb_project_history ph                 
                left outer join ctb_task_defs task on ph.id_task = task.id_task 
                left outer join tb_est_history eh on eh.id_project_history = ph.id_tb_project_history 
                left outer join ctb_trade_defs td on td.txt_trade_code = eh.txt_trade_code 
                left outer join tb_meeting_history mh on mh.id_project_history = ph.id_tb_project_history 
                left outer join tb_link_actions la on ph.id_tb_project_history = la.id_project_history 
                left outer join tb_prelim_results_history prh on ph.id_tb_project_history = prh.id_project_history and ph.id_task = 2040 
                left outer join ctb_trade_defs ptd on ptd.txt_trade_code = prh.txt_trade_code

                left outer join tb_rvw_res_trade_history rrt on ph.id_task = 3007 /* Enter Trade Review Result */ and ph.id_tb_project_history = rrt.id_tb_project_history
                left outer join ctb_trade_defs rrt_td on rrt.txt_trade_code = rrt_td.txt_trade_code
            					
                left outer join tb_rvw_res_ce_agency_history rrca on ph.id_task = 3008 /* Enter Agency Review Result */ and ph.id_tb_project_history = rrca.id_tb_project_history
                left outer join ctb_agency_defs rrca_ad on rrca.id_agency = rrca_ad.id_agency_def
                                
                left outer join tb_intake_action ia on ph.id_tb_project_history = ia.id_tb_project_history
                				
				left outer join tb_rvw_res_agency_history rra on ph.id_task = 5021 /* WLR Enter Agency Review Result */ and ph.id_tb_project_history = rra.id_tb_project_history
                left outer join ctb_agency_defs rra_ad on rra.id_agency_def = rra_ad.id_agency_def
            					
                left outer join tb_project_fee_history pfh on ph.id_task in (603, 604, 605, 3000, 3014, 5010, 5011, 5012, 5025) /* Fee Related Tasks */ and ph.id_tb_project_history = pfh.id_tb_project_history
                left outer join ctb_feetype_defs ftd on pfh.id_fee_types = ftd.id_fee_types				
			where
				ph.project_number = '427927'
		) as temp1  
			WHERE NOT
			temp1.task_desc IN ('Package Submitted','Potential Agencies for Permitting','Project Linked','Plan Review Fee added ')
		) as temp2 
			WHERE NOT 
	   temp2.id_tb_project_history in (2506201,2505391,2506200)) AS RESULT