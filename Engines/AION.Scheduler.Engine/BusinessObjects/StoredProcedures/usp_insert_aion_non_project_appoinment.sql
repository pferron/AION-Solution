
/***********************************************************************************************************************
* Object:	usp_insert_aion_non_project_appoinment
* Description:	Inserts NonProjectAppoinment record.
* Parameters:
*		@APPT_NM                                                     varchar(50)
*		@ALL_PLAN_REVIEWERS_IND                                      bit
*		@ALL_DAY_IND                                                 bit
*		@APPT_FROM_DTTM                                              datetime
*		@APPT_TO_DTTM                                                datetime
*		@NON_PROJECT_APPT_TYP_REF_ID                                 int
*		@MEETING_ROOM_REF_ID                                         int
*		@APPT_RECURRENCE_REF_ID                                      int
*		@ALL_BUILD_IND                                               bit
*		@ALL_ELCTR_IND                                               bit
*		@ALL_MECH_IND                                                bit
*		@ALL_PLUMB_IND                                               bit
*		@ALL_ZONING_IND                                              bit
*		@ALL_FIRE_IND                                                bit
*		@ALL_BACKFLOW_IND                                            bit
*		@ALL_EHS_FOOD_IND                                            bit
*		@ALL_EHS_POOL_IND                                            bit
*		@ALL_EHS_LODGE_IND                                           bit
*		@ALL_EHS_DAYCARE_IND                                         bit
*		@WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Identity column of new record.
* Comments:     If CREATED_DTTM and/or UPDATED_DTTM fields exist in table, GETDATE() is inserted.
*               Worker ID will be inserted for WKR_ID_CREATED_TXT and WKR_ID_UPDATED_TXT fields if they exist.
* Version:      1.0
* Created by:   AION_user
* Created:      3/19/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 3/19/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [AION].[usp_insert_aion_non_project_appoinment]
    @APPT_NM                                                     varchar(50)
  , @ALL_PLAN_REVIEWERS_IND                                      bit
  , @ALL_DAY_IND                                                 bit
  , @APPT_FROM_DTTM                                              datetime
  , @APPT_TO_DTTM                                                datetime
  , @NON_PROJECT_APPT_TYP_REF_ID                                 int
  , @MEETING_ROOM_REF_ID                                         int
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

     INSERT INTO NON_PROJECT_APPOINTMENT
          (
            APPT_NM
          , ALL_PLAN_REVIEWERS_IND
          , ALL_DAY_IND
          , APPT_FROM_DTTM
          , APPT_TO_DTTM
          , NON_PROJECT_APPT_TYP_REF_ID
          , MEETING_ROOM_REF_ID
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          , APPT_RECURRENCE_REF_ID
          , ALL_BUILD_IND
          , ALL_ELCTR_IND
          , ALL_MECH_IND
          , ALL_PLUMB_IND
          , ALL_ZONING_IND
          , ALL_FIRE_IND
          , ALL_BACKFLOW_IND
          , ALL_EHS_FOOD_IND
          , ALL_EHS_POOL_IND
          , ALL_EHS_LODGE_IND
          , ALL_EHS_DAYCARE_IND
          )
     VALUES
          (
            @APPT_NM
          , @ALL_PLAN_REVIEWERS_IND
          , @ALL_DAY_IND
          , @APPT_FROM_DTTM
          , @APPT_TO_DTTM
          , @NON_PROJECT_APPT_TYP_REF_ID
          , @MEETING_ROOM_REF_ID
          , @WKR_ID_TXT
          , GETDATE()
          , @WKR_ID_TXT
          , GETDATE()
          , @APPT_RECURRENCE_REF_ID
          , @ALL_BUILD_IND
          , @ALL_ELCTR_IND
          , @ALL_MECH_IND
          , @ALL_PLUMB_IND
          , @ALL_ZONING_IND
          , @ALL_FIRE_IND
          , @ALL_BACKFLOW_IND
          , @ALL_EHS_FOOD_IND
          , @ALL_EHS_POOL_IND
          , @ALL_EHS_LODGE_IND
          , @ALL_EHS_DAYCARE_IND
          )

     SELECT @error = @@ERROR
          , @ReturnValue = SCOPE_IDENTITY()

     IF @error != 0
          RAISERROR('Error adding NonProjectAppoinment record.', 18,1)

RETURN
GO