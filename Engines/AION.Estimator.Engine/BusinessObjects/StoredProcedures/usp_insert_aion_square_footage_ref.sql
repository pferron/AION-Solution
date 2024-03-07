
/***********************************************************************************************************************
* Object:	usp_insert_aion_square_footage_ref
* Description:	Inserts SquareFootageRef record.
* Parameters:
*		@SQUARE_FOOTAGE_REF_ID                                       int
*		@SQUARE_FOOTAGE_DESC                                         varchar(255)
*		@SQUARE_FOOTAGE_VAL_TXT                                      varchar(100)
*		@ENUM_MAPPING_VAL_NBR                                        int
*		@WKR_ID_TXT                                                  varchar(100)
*
* Returns:      N/A
* Comments:     If CREATED_DTTM and/or UPDATED_DTTM fields exist in table, GETDATE() is inserted.
*               Worker ID will be inserted for WKR_ID_CREATED_TXT and WKR_ID_UPDATED_TXT fields if they exist.
* Version:      1.0
* Created by:   AION_user
* Created:      4/30/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 4/30/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_insert_aion_square_footage_ref]
    @SQUARE_FOOTAGE_REF_ID                                       int
  , @SQUARE_FOOTAGE_DESC                                         varchar(255)
  , @SQUARE_FOOTAGE_VAL_TXT                                      varchar(100)
  , @ENUM_MAPPING_VAL_NBR                                        int
  , @WKR_ID_TXT                                                  varchar(100)

AS

     DECLARE @error   int

     INSERT INTO SQUARE_FOOTAGE_REF
          (
            SQUARE_FOOTAGE_REF_ID
          , SQUARE_FOOTAGE_DESC
          , SQUARE_FOOTAGE_VAL_TXT
          , ENUM_MAPPING_VAL_NBR
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          )
     VALUES
          (
            @SQUARE_FOOTAGE_REF_ID
          , @SQUARE_FOOTAGE_DESC
          , @SQUARE_FOOTAGE_VAL_TXT
          , @ENUM_MAPPING_VAL_NBR
          , @WKR_ID_TXT
          , GETDATE()
          , @WKR_ID_TXT
          , GETDATE()
          )

     SELECT @error = @@ERROR
     IF @error != 0
          RAISERROR('Error adding SquareFootageRef record.', 18,1)

RETURN
GO