
/***********************************************************************************************************************
* Object:	usp_insert_aion_business_division_ref
* Description:	Inserts BusinessDivisionRef record.
* Parameters:
*		@BUSINESS_DIVISION_NM                                        varchar(50)
*		@BUSINESS_DIVISION_DESC                                      varchar(100)
*		@ENUM_MAPPING_VAL_NBR                                        int
*		@WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Identity column of new record.
* Comments:     If CREATED_DTTM and/or UPDATED_DTTM fields exist in table, GETDATE() is inserted.
*               Worker ID will be inserted for WKR_ID_CREATED_TXT and WKR_ID_UPDATED_TXT fields if they exist.
* Version:      1.0
* Created by:   AION_user
* Created:      3/21/2022
************************************************************************************************************************
* Change History: Date, Name, Description
* 3/21/2022    AION_user     Auto-generated
* 
***********************************************************************************************************************/

ALTER PROCEDURE AION.[usp_insert_aion_business_division_ref]
    @BUSINESS_DIVISION_NM                                        varchar(50)
  , @BUSINESS_DIVISION_DESC                                      varchar(100)
  , @ENUM_MAPPING_VAL_NBR                                        int
  , @WKR_ID_TXT                                                  varchar(100)
  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

     INSERT INTO AION.BUSINESS_DIVISION_REF
          (
            BUSINESS_DIVISION_NM
          , BUSINESS_DIVISION_DESC
          , ENUM_MAPPING_VAL_NBR
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          )
     VALUES
          (
            @BUSINESS_DIVISION_NM
          , @BUSINESS_DIVISION_DESC
          , @ENUM_MAPPING_VAL_NBR
          , @WKR_ID_TXT
          , GETDATE()
          , @WKR_ID_TXT
          , GETDATE()
          )

     SELECT @error = @@ERROR
          , @ReturnValue = SCOPE_IDENTITY()

     IF @error != 0
          RAISERROR('Error adding BusinessDivisionRef record.', 18,1)

RETURN
GO