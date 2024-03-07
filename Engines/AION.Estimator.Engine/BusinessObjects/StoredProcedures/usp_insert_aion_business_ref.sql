
/***********************************************************************************************************************
* Object:	usp_insert_aion_business_ref
* Description:	Inserts BusinessRef record.
* Parameters:
*		@BUSINESS_NM                                                 varchar(100)
*		@BUSINESS_SHORT_DESC                                         varchar(30)
*		@BUSINESS_TYP_REF_ID                                         int
*		@DIVISION_REF_ID                                             int
*		@ENUM_MAPPING_VAL_NBR                                        int
*		@EXTERNAL_SYSTEM_REF_ID                                      int
*		@REGION_REF_ID                                               int
*		@SRC_SYSTEM_VAL_TXT                                          varchar(255)
*		@WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Identity column of new record.
* Comments:     If CREATED_DTTM and/or UPDATED_DTTM fields exist in table, GETDATE() is inserted.
*               Worker ID will be inserted for WKR_ID_CREATED_TXT and WKR_ID_UPDATED_TXT fields if they exist.
* Version:      1.0
* Created by:   AION_user
* Created:      10/10/2019
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/10/2019    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_insert_aion_business_ref]
    @BUSINESS_NM                                                 varchar(100)
  , @BUSINESS_SHORT_DESC                                         varchar(30)
  , @BUSINESS_TYP_REF_ID                                         int
  , @DIVISION_REF_ID                                             int
  , @ENUM_MAPPING_VAL_NBR                                        int
  , @EXTERNAL_SYSTEM_REF_ID                                      int
  , @REGION_REF_ID                                               int
  , @SRC_SYSTEM_VAL_TXT                                          varchar(255)
  , @WKR_ID_TXT                                                  varchar(100)
  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

     INSERT INTO BUSINESS_REF
          (
            BUSINESS_NM
          , BUSINESS_SHORT_DESC
          , BUSINESS_TYP_REF_ID
          , CREATED_DTTM
          , DIVISION_REF_ID
          , ENUM_MAPPING_VAL_NBR
          , EXTERNAL_SYSTEM_REF_ID
          , REGION_REF_ID
          , SRC_SYSTEM_VAL_TXT
          , UPDATED_DTTM
          , WKR_ID_CREATED_TXT
          , WKR_ID_UPDATED_TXT
          )
     VALUES
          (
            @BUSINESS_NM
          , @BUSINESS_SHORT_DESC
          , @BUSINESS_TYP_REF_ID
          , GETDATE()
          , @DIVISION_REF_ID
          , @ENUM_MAPPING_VAL_NBR
          , @EXTERNAL_SYSTEM_REF_ID
          , @REGION_REF_ID
          , @SRC_SYSTEM_VAL_TXT
          , GETDATE()
          , @WKR_ID_TXT
          , @WKR_ID_TXT
          )

     SELECT @error = @@ERROR
          , @ReturnValue = SCOPE_IDENTITY()

     IF @error != 0
          RAISERROR('Error adding BusinessRef record.', 18,1)

RETURN
GO