
/***********************************************************************************************************************
* Object:	usp_insert_aion_external_system_ref
* Description:	Inserts ExternalSystemRef record.
* Parameters:
*		@EXTERNAL_SYSTEM_NM                                          varchar(100)
*		@EXTERNAL_SYSTEM_DESC                                        varchar(100)
*		@ADDL_INFORMATION_TXT                                        varchar(255)
*		@ENUM_MAPPING_VAL_NBR                                        int
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

CREATE PROCEDURE [usp_insert_aion_external_system_ref]
    @EXTERNAL_SYSTEM_NM                                          varchar(100)
  , @EXTERNAL_SYSTEM_DESC                                        varchar(100)
  , @ADDL_INFORMATION_TXT                                        varchar(255)
  , @ENUM_MAPPING_VAL_NBR                                        int
  , @WKR_ID_TXT                                                  varchar(100)
  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

     INSERT INTO EXTERNAL_SYSTEM_REF
          (
            EXTERNAL_SYSTEM_NM
          , EXTERNAL_SYSTEM_DESC
          , ADDL_INFORMATION_TXT
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          , ENUM_MAPPING_VAL_NBR
          )
     VALUES
          (
            @EXTERNAL_SYSTEM_NM
          , @EXTERNAL_SYSTEM_DESC
          , @ADDL_INFORMATION_TXT
          , @WKR_ID_TXT
          , GETDATE()
          , @WKR_ID_TXT
          , GETDATE()
          , @ENUM_MAPPING_VAL_NBR
          )

     SELECT @error = @@ERROR
          , @ReturnValue = SCOPE_IDENTITY()

     IF @error != 0
          RAISERROR('Error adding ExternalSystemRef record.', 18,1)

RETURN
GO