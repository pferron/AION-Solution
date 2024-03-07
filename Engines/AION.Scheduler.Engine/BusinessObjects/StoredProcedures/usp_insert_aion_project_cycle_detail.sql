
/***********************************************************************************************************************
* Object:	usp_insert_aion_project_cycle_detail
* Description:	Inserts ProjectCycleDetail record.
* Parameters:
*		@PROJECT_CYCLE_ID                                            int
*		@BUSINESS_REF_ID                                             int
*		@REREVIEW_HOURS_NBR                                          decimal
*		@WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Identity column of new record.
* Comments:     If CREATED_DTTM and/or UPDATED_DTTM fields exist in table, GETDATE() is inserted.
*               Worker ID will be inserted for WKR_ID_CREATED_TXT and WKR_ID_UPDATED_TXT fields if they exist.
* Version:      1.0
* Created by:   AION_user
* Created:      10/14/2021
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/14/2021    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_insert_aion_project_cycle_detail]
    @PROJECT_CYCLE_ID                                            int
  , @BUSINESS_REF_ID                                             int
  , @REREVIEW_HOURS_NBR                                          decimal(9,2)
  , @WKR_ID_TXT                                                  varchar(100)
  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

     INSERT INTO PROJECT_CYCLE_DETAIL
          (
            PROJECT_CYCLE_ID
          , BUSINESS_REF_ID
          , REREVIEW_HOURS_NBR
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          )
     VALUES
          (
            @PROJECT_CYCLE_ID
          , @BUSINESS_REF_ID
          , @REREVIEW_HOURS_NBR
          , @WKR_ID_TXT
          , GETDATE()
          , @WKR_ID_TXT
          , GETDATE()
          )

     SELECT @error = @@ERROR
          , @ReturnValue = SCOPE_IDENTITY()

     IF @error != 0
          RAISERROR('Error adding ProjectCycleDetail record.', 18,1)

RETURN
GO