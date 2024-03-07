
/***********************************************************************************************************************
* Object:       usp_update_aion_non_project_appoinment
* Description:  Updates NonProjectAppoinment record using supplied parameters.
* Parameters:   
*               @NON_PROJECT_APPT_ID                                         int
*               @APPT_NM                                                     varchar(50)
*               @ALL_PLAN_REVIEWERS_IND                                      bit
*               @ALL_DAY_IND                                                 bit
*               @APPT_FROM_DTTM                                              datetime
*               @APPT_TO_DTTM                                                datetime
*               @NON_PROJECT_APPT_TYP_REF_ID                                 int
*               @MEETING_ROOM_REF_ID                                         int
*               @UPDATED_DTTM                                                datetime
*               @APPT_RECURRENCE_REF_ID                                      int
*               @ALL_BUILD_IND                                               bit
*               @ALL_ELCTR_IND                                               bit
*               @ALL_MECH_IND                                                bit
*               @ALL_PLUMB_IND                                               bit
*               @ALL_ZONING_IND                                              bit
*               @ALL_FIRE_IND                                                bit
*               @ALL_BACKFLOW_IND                                            bit
*               @ALL_EHS_FOOD_IND                                            bit
*               @ALL_EHS_POOL_IND                                            bit
*               @ALL_EHS_LODGE_IND                                           bit
*               @ALL_EHS_DAYCARE_IND                                         bit
*               @WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Number of rows affected.
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.
* Version:      1.0
* Created by:   AION_user
* Created:      3/19/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 3/19/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [AION].[usp_update_aion_non_project_appoinment]

    @NON_PROJECT_APPT_ID                                         int
  , @APPT_NM                                                     varchar(50)
  , @ALL_PLAN_REVIEWERS_IND                                      bit
  , @ALL_DAY_IND                                                 bit
  , @APPT_FROM_DTTM                                              datetime
  , @APPT_TO_DTTM                                                datetime
  , @NON_PROJECT_APPT_TYP_REF_ID                                 int
  , @MEETING_ROOM_REF_ID                                         int
  , @UPDATED_DTTM                                                datetime
  , @APPT_RECURRENCE_REF_ID                                      int
  , @ALL_BUILD_IND                                               bit
  , @ALL_ELCTR_IND                                               bit
  , @ALL_MECH_IND                                                bit
  , @ALL_PLUMB_IND                                               bit
  , @ALL_ZONING_IND                                              bit
  , @ALL_FIRE_IND                                                bit
  , @ALL_BACKFLOW_IND                                            bit
  , @ALL_EHS_FOOD_IND                                            bit
  , @ALL_EHS_POOL_IND                                            bit
  , @ALL_EHS_LODGE_IND                                           bit
  , @ALL_EHS_DAYCARE_IND                                         bit
  , @WKR_ID_TXT                                                  varchar(100)

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       UPDATE NON_PROJECT_APPOINTMENT
       SET
            APPT_NM                                                      = @APPT_NM
          , ALL_PLAN_REVIEWERS_IND                                       = @ALL_PLAN_REVIEWERS_IND
          , ALL_DAY_IND                                                  = @ALL_DAY_IND
          , APPT_FROM_DTTM                                               = @APPT_FROM_DTTM
          , APPT_TO_DTTM                                                 = @APPT_TO_DTTM
          , NON_PROJECT_APPT_TYP_REF_ID                                  = @NON_PROJECT_APPT_TYP_REF_ID
          , MEETING_ROOM_REF_ID                                          = @MEETING_ROOM_REF_ID
          , WKR_ID_UPDATED_TXT                                           = @WKR_ID_TXT
          , UPDATED_DTTM                                                 = GETDATE()
          , APPT_RECURRENCE_REF_ID                                       = @APPT_RECURRENCE_REF_ID
          , ALL_BUILD_IND                                                = @ALL_BUILD_IND
          , ALL_ELCTR_IND                                                = @ALL_ELCTR_IND
          , ALL_MECH_IND                                                 = @ALL_MECH_IND
          , ALL_PLUMB_IND                                                = @ALL_PLUMB_IND
          , ALL_ZONING_IND                                               = @ALL_ZONING_IND
          , ALL_FIRE_IND                                                 = @ALL_FIRE_IND
          , ALL_BACKFLOW_IND                                             = @ALL_BACKFLOW_IND
          , ALL_EHS_FOOD_IND                                             = @ALL_EHS_FOOD_IND
          , ALL_EHS_POOL_IND                                             = @ALL_EHS_POOL_IND
          , ALL_EHS_LODGE_IND                                            = @ALL_EHS_LODGE_IND
          , ALL_EHS_DAYCARE_IND                                          = @ALL_EHS_DAYCARE_IND

       WHERE
          NON_PROJECT_APPT_ID                                            = @NON_PROJECT_APPT_ID       
       AND 
          ISNULL(CONVERT(varchar(19), UPDATED_DTTM, 120), '')             = ISNULL(CONVERT(varchar(19), @UPDATED_DTTM, 120), '')
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error updating NonProjectAppoinment record.', 18,1)

     IF @ReturnValue = 0
          RAISERROR('Data was changed/deleted prior to update.', 18,100)

RETURN
GO