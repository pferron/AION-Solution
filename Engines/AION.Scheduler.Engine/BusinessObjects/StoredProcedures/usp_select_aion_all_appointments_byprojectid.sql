
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
* 11/18/2020    jlindsay    removed unused var     
* 12/17/2020    aburnam     added cycle/reschedule logic 
* 1/27/2021    jlindsay    add project id filter to plan review schedule filters, add EMA filters
***********************************************************************************************************************/

ALTER PROCEDURE [AION].[usp_select_aion_all_appointments_byprojectid] @projectid INT
AS
    BEGIN  
        --get the max cycle for the project from PR and EMA tables
        DECLARE @max_cycle INT;
        -- Get the plan review schedule max cycle
        DECLARE @PR_max_cycle INT;
        SELECT @PR_max_cycle = MAX(CYCLE_NBR)
        FROM PLAN_REVIEW_SCHEDULE
        WHERE PROJECT_ID = @projectid
              AND IS_FUTURE_CYCLE_IND = 0;
        --
        -- Get the EMA max cycle
        DECLARE @EMA_max_cycle INT;
        SELECT @EMA_max_cycle = MAX(CYCLE_NBR)
        FROM EXPRESS_MEETING_APPOINTMENT
        WHERE PROJECT_ID = @projectid;
        --
        --Get the Max cycle
        SELECT @max_cycle = CASE
                                WHEN ISNULL(@PR_max_cycle, 0) >= ISNULL(@EMA_max_cycle, 0)
                                THEN ISNULL(@PR_max_cycle, 0)
                                ELSE ISNULL(@EMA_max_cycle, 0)
                            END;

        --Get PR is reschedule filter
        DECLARE @is_reschedule BIT;
        SELECT @is_reschedule = IS_RESCHEDULE_IND
        FROM PLAN_REVIEW_SCHEDULE
        WHERE PROJECT_ID = @projectid
              AND CYCLE_NBR = @max_cycle;  
        --
        --
        --
        --get the status exclusions
        DECLARE @cancelled INT;
        SELECT @cancelled = APPT_RESPONSE_STATUS_REF_ID
        FROM APPOINTMENT_RESPONSE_STATUS_REF
        WHERE ENUM_MAPPING_VAL_NBR = 7;
        --
        SELECT P.PROJECT_ID, 
               P.PROJECT_NM, 
               P.PROJECT_TYP_REF_ID, 
               PS.ENUM_MAPPING_VAL_NBR AS PROJECT_STATUS_REF_ID, 
               PMA.FROM_DT AS START_DT, 
               DATEADD(Day, 2, PMA.UPDATED_DTTM) AS ACCEPTANCE_DEADLINE
        FROM AION.PRELIMINARY_MEETING_APPOINTMENT PMA
             INNER JOIN [AION].[PROJECT] P ON P.[PROJECT_ID] = PMA.PROJECT_ID
             INNER JOIN [AION].[PROJECT_STATUS_REF] PS ON PS.PROJECT_STATUS_REF_ID = P.PROJECT_STATUS_REF_ID
        WHERE PMA.PROJECT_ID = @projectid
              AND PMA.APPT_RESPONSE_STATUS_REF_ID != @cancelled
        UNION
        SELECT P.PROJECT_ID, 
               P.PROJECT_NM, 
               P.PROJECT_TYP_REF_ID, 
               PS.ENUM_MAPPING_VAL_NBR AS PROJECT_STATUS_REF_ID, 
               EMA.FROM_DT AS START_DT, 
               DATEADD(Day, 2, EMA.UPDATED_DTTM) AS ACCEPTANCE_DEADLINE
        FROM AION.EXPRESS_MEETING_APPOINTMENT EMA
             INNER JOIN [AION].[PROJECT] P ON P.[PROJECT_ID] = EMA.PROJECT_ID
             INNER JOIN [AION].[PROJECT_STATUS_REF] PS ON PS.PROJECT_STATUS_REF_ID = P.PROJECT_STATUS_REF_ID
        WHERE EMA.PROJECT_ID = @projectid
              AND EMA.CYCLE_NBR = @max_cycle
              AND EMA.APPT_RESPONSE_STATUS_REF_ID != @cancelled
        UNION
        SELECT P.PROJECT_ID, 
               P.PROJECT_NM, 
               P.PROJECT_TYP_REF_ID, 
               PS.ENUM_MAPPING_VAL_NBR AS PROJECT_STATUS_REF_ID, 
               PRS.START_DT AS START_DT, 
               DATEADD(Day, 2, PRS.UPDATED_DTTM) AS ACCEPTANCE_DEADLINE
        FROM AION.PLAN_REVIEW_SCHEDULE PRS
             INNER JOIN [AION].[PROJECT] P ON P.[PROJECT_ID] = PRS.PROJECT_ID
             INNER JOIN [AION].[PROJECT_STATUS_REF] PS ON PS.PROJECT_STATUS_REF_ID = P.PROJECT_STATUS_REF_ID
        WHERE PRS.PROJECT_ID = @projectid
              AND PRS.CYCLE_NBR = @max_cycle
              AND PRS.IS_RESCHEDULE_IND = @is_reschedule
              AND PRS.APPT_RESPONSE_STATUS_REF_ID != @cancelled
        ORDER BY START_DT;
        RETURN;
    END;