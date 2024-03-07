
/***********************************************************************************************************************
* Object:	usp_insert_aion_reserve_express_plan_reviewer
* Description:	Inserts ReserveExpressPlanReviewer record.
* Parameters:
*		@BUSINESS_REF_ID                                             int
*		@PLAN_REVIEWER_ID                                            int
*		@ROTATION_NBR                                                int
*		@WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Identity column of new record.
* Comments:     If CREATED_DTTM and/or UPDATED_DTTM fields exist in table, GETDATE() is inserted.
*               Worker ID will be inserted for WKR_ID_CREATED_TXT and WKR_ID_UPDATED_TXT fields if they exist.
* Version:      1.0
* Created by:   AION_user
* Created:      8/6/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 8/6/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_insert_aion_reserve_express_plan_reviewer]
    @BUSINESS_REF_ID                                             int
  , @PLAN_REVIEWER_ID                                            int
  , @ROTATION_NBR                                                int
  , @WKR_ID_TXT                                                  varchar(100)
  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

     INSERT INTO RESERVE_EXPRESS_PLAN_REVIEWER
          (
            BUSINESS_REF_ID
          , PLAN_REVIEWER_ID
          , ROTATION_NBR
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          )
     VALUES
          (
            @BUSINESS_REF_ID
          , @PLAN_REVIEWER_ID
          , @ROTATION_NBR
          , @WKR_ID_TXT
          , GETDATE()
          , @WKR_ID_TXT
          , GETDATE()
          )

     SELECT @error = @@ERROR
          , @ReturnValue = SCOPE_IDENTITY()

     IF @error != 0
          RAISERROR('Error adding ReserveExpressPlanReviewer record.', 18,1)

RETURN
GO