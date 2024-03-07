
/***********************************************************************************************************************
* Object:	usp_insert_aion_schedule_business_relationship_stage
* Description:	Inserts ScheduleBusinessRelationshipStage record.
* Parameters:
*		@HOURS_NBR                                                   decimal
*		@CYCLE_NBR                                                   int
*		@BUSINESS_REF_ID                                             int
*		@PROJECT_ID                                                  int
*		@WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Identity column of new record.
* Comments:     If CREATED_DTTM and/or UPDATED_DTTM fields exist in table, GETDATE() is inserted.
*               Worker ID will be inserted for WKR_ID_CREATED_TXT and WKR_ID_UPDATED_TXT fields if they exist.
* Version:      1.0
* Created by:   AION_user
* Created:      12/17/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 12/17/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_insert_aion_schedule_business_relationship_stage]
    @HOURS_NBR                                                   decimal
  , @CYCLE_NBR                                                   int
  , @BUSINESS_REF_ID                                             int
  , @PROJECT_ID                                                  int
  , @WKR_ID_TXT                                                  varchar(100)
  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

     INSERT INTO SCHEDULE_BUSINESS_RELATIONSHIP_STAGE
          (
            HOURS_NBR
          , CYCLE_NBR
          , BUSINESS_REF_ID
          , PROJECT_ID
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          )
     VALUES
          (
            @HOURS_NBR
          , @CYCLE_NBR
          , @BUSINESS_REF_ID
          , @PROJECT_ID
          , @WKR_ID_TXT
          , GETDATE()
          , @WKR_ID_TXT
          , GETDATE()
          )

     SELECT @error = @@ERROR
          , @ReturnValue = SCOPE_IDENTITY()

     IF @error != 0
          RAISERROR('Error adding ScheduleBusinessRelationshipStage record.', 18,1)

RETURN
GO