
/***********************************************************************************************************************      
* Object:       usp_select_aion_non_project_appoinment_search_v2     
* Description:  Retrieves NonProjectAppoinment list for given parameter(s).      
* Parameters:         
*                     
*      
* Returns:      Recordset.      
* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval.      
*               This proc expects id_person and/or id_file to generate list; modify as necessary.      
*               Include ORDER BY clause as necessary    
*               gaya: modified searchtxt as wildcard search, Commented the select temp table.      
* Version:      1.0      
* Created by:   j lindsay      
* Created:      04/16/2020      
************************************************************************************************************************      
* Change History: Date, Name, Description      
* 05/20/2020    j lindsay  initial   
* 02/18/2021    jlindsay    add appt type desc 'npa'
* 05/07/2021    jallen    add sort on recurring date
***********************************************************************************************************************/

ALTER PROCEDURE [AION].[usp_select_aion_non_project_appoinment_search_v2] @APPT_FROM_DTTM              DATETIME, 
                                                                          @APPT_TO_DTTM                DATETIME, 
                                                                          @NON_PROJECT_APPT_TYP_REF_ID INT, 
                                                                          @SEARCH_TXT                  VARCHAR(2000), 
                                                                          @REVIEWER_ID                 INT
AS
    BEGIN    
        --set end date if null to today + 1 year    
        --set begin date if null to today    
        --set begin date if null to today    

        DECLARE @SearchEndDate DATETIME;
        DECLARE @SearchStartDate DATETIME;
        DECLARE @NonProjectApptTypeRefId INT;
        DECLARE @SearchTxt VARCHAR(2000);
        DECLARE @ReviewerId INT;
        DECLARE @Today DATE= CONVERT(DATE, GETDATE());
        SET @ReviewerId = CASE
                              WHEN ISNULL(@REVIEWER_ID, '') = ''
                              THEN 0
                              ELSE @REVIEWER_ID
                          END;    
        --    
        SET @SearchTxt = CASE
                             WHEN ISNULL(@SEARCH_TXT, '') = ''
                             THEN ''
                             ELSE '%' + @SEARCH_TXT + '%'
                         END;

        --    
        SET @NonProjectApptTypeRefId = CASE
                                           WHEN ISNULL(@NON_PROJECT_APPT_TYP_REF_ID, '') = ''
                                           THEN 0
                                           ELSE @NON_PROJECT_APPT_TYP_REF_ID
                                       END;    
        --    
        SET @SearchEndDate = CASE
                                 WHEN ISNULL(@APPT_TO_DTTM, '') = ''
                                 THEN DATEADD(year, 1, @Today)
                                 ELSE CONVERT(DATE, @APPT_TO_DTTM)
                             END;

        --    

        SET @SearchStartDate = CASE
                                   WHEN ISNULL(@APPT_FROM_DTTM, '') = ''
                                   THEN @Today
                                   ELSE CONVERT(DATE, @APPT_FROM_DTTM)
                               END;

        --    
        --    
        DECLARE @ReviewerAppts TABLE
        (ID                  INT IDENTITY(1, 1), 
         APPT_ID             INT NOT NULL, 
         PROJECT_SCHEDULE_ID INT NOT NULL, 
         RECURRING_APPT_DT   DATETIME NOT NULL
        );

        /*******************************/

        IF(@ReviewerId > 0)
            BEGIN    
                --get appts for this reviewer   
                INSERT INTO @ReviewerAppts
                       SELECT APPT_ID, 
                              ps.PROJECT_SCHEDULE_ID, 
                              RECURRING_APPT_DT
                       FROM PROJECT_SCHEDULE ps
                            INNER JOIN USER_SCHEDULE us ON ps.PROJECT_SCHEDULE_ID = us.PROJECT_SCHEDULE_ID
                                                           AND ps.PROJECT_SCHEDULE_TYP_DESC = 'NPA'
                       WHERE us.[USER_ID] = @ReviewerId
                             AND ps.RECURRING_APPT_DT >= @SearchStartDate
                             AND ps.RECURRING_APPT_DT <= @SearchEndDate;
        END;
            ELSE
            BEGIN

                --  get appts for this date range  
                INSERT INTO @ReviewerAppts
                       SELECT APPT_ID, 
                              PROJECT_SCHEDULE_ID, 
                              RECURRING_APPT_DT
                       FROM PROJECT_SCHEDULE ps
                       WHERE ps.RECURRING_APPT_DT >= @SearchStartDate
                             AND ps.RECURRING_APPT_DT <= @SearchEndDate
                             AND ps.PROJECT_SCHEDULE_TYP_DESC = 'NPA';
        END;  
        --  
        --  

        /**********************************/

        SELECT npa.NON_PROJECT_APPT_ID, 
               revappts.PROJECT_SCHEDULE_ID, 
               revappts.RECURRING_APPT_DT, 
               npa.APPT_NM, 
               npa.ALL_PLAN_REVIEWERS_IND, 
               npa.ALL_DAY_IND, 
               npa.APPT_FROM_DTTM, 
               npa.APPT_TO_DTTM, 
               npa.NON_PROJECT_APPT_TYP_REF_ID, 
               npa.MEETING_ROOM_REF_ID, 
               npa.WKR_ID_CREATED_TXT, 
               npa.CREATED_DTTM, 
               npa.WKR_ID_UPDATED_TXT, 
               npa.UPDATED_DTTM, 
               npa.APPT_RECURRENCE_REF_ID, 
               npa.ALL_BUILD_IND, 
               npa.ALL_ELCTR_IND, 
               npa.ALL_MECH_IND, 
               npa.ALL_PLUMB_IND, 
               npa.ALL_ZONING_IND, 
               npa.ALL_FIRE_IND, 
               npa.ALL_BACKFLOW_IND, 
               npa.ALL_EHS_FOOD_IND, 
               npa.ALL_EHS_POOL_IND, 
               npa.ALL_EHS_LODGE_IND, 
               npa.ALL_EHS_DAYCARE_IND
        FROM NON_PROJECT_APPOINTMENT npa
             INNER JOIN @ReviewerAppts revappts ON npa.NON_PROJECT_APPT_ID = revappts.APPT_ID
        WHERE npa.NON_PROJECT_APPT_TYP_REF_ID = CASE
                                                    WHEN @NonProjectApptTypeRefId = 0
                                                    THEN NON_PROJECT_APPT_TYP_REF_ID
                                                    ELSE @NonProjectApptTypeRefId
                                                END
              AND npa.APPT_NM LIKE CASE
                                       WHEN @SearchTxt = ''
                                       THEN APPT_NM
                                       ELSE @SearchTxt
                                   END

        ORDER BY revappts.RECURRING_APPT_DT;

        --    
        RETURN;
    END;