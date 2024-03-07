
/***********************************************************************************************************************
* Object:	usp_insert_aion_schedule_business_relationship
* Description:	Inserts ScheduleBusinessRelationship record.
* Parameters:
*		@PLAN_REVIEW_SCHEDULE_ID                                     int
*		@BUSINESS_REF_ID                                             int
*		@PROJECT_ID                                                  int
*		@REREVIEW_HOURS_NBR                                          int
*		@CYCLE_NBR                                                   int
*		@WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Identity column of new record.
* Comments:     If CREATED_DTTM and/or UPDATED_DTTM fields exist in table, GETDATE() is inserted.
*               Worker ID will be inserted for WKR_ID_CREATED_TXT and WKR_ID_UPDATED_TXT fields if they exist.
* Version:      1.0
* Created by:   AION_user
* Created:      9/14/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 9/14/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_insert_aion_schedule_business_relationship]
    @PLAN_REVIEW_SCHEDULE_ID                                     int
  , @BUSINESS_REF_ID                                             int
  , @PROJECT_ID                                                  int
  , @REREVIEW_HOURS_NBR                                          int
  , @CYCLE_NBR                                                   int
  , @WKR_ID_TXT                                                  varchar(100)
  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

     INSERT INTO SCHEDULE_BUSINESS_RELATIONSHIP
          (
            PLAN_REVIEW_SCHEDULE_ID
          , BUSINESS_REF_ID
          , PROJECT_ID
          , REREVIEW_HOURS_NBR
          , CYCLE_NBR
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          )
     VALUES
          (
            @PLAN_REVIEW_SCHEDULE_ID
          , @BUSINESS_REF_ID
          , @PROJECT_ID
          , @REREVIEW_HOURS_NBR
          , @CYCLE_NBR
          , @WKR_ID_TXT
          , GETDATE()
          , @WKR_ID_TXT
          , GETDATE()
          )

     SELECT @error = @@ERROR
          , @ReturnValue = SCOPE_IDENTITY()

     IF @error != 0
          RAISERROR('Error adding ScheduleBusinessRelationship record.', 18,1)

RETURN
GO