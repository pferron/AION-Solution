
/***********************************************************************************************************************
* Object:	usp_insert_aion_permission_ref
* Description:	Inserts PermissionRef record.
* Parameters:
*		@PERMISSION_NM                                               varchar(50)
*		@MODULE_REF_ID                                               int
*		@ENUM_MAPPING_VAL_NBR                                        int
*		@PERMISSION_DISPLAY_NM                                       varchar(150)
*		@WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Identity column of new record.
* Comments:     If CREATED_DTTM and/or UPDATED_DTTM fields exist in table, GETDATE() is inserted.
*               Worker ID will be inserted for WKR_ID_CREATED_TXT and WKR_ID_UPDATED_TXT fields if they exist.
* Version:      1.0
* Created by:   AION_user
* Created:      3/30/2022
************************************************************************************************************************
* Change History: Date, Name, Description
* 3/30/2022    AION_user     Auto-generated
* 
***********************************************************************************************************************/

ALTER PROCEDURE AION.[usp_insert_aion_permission_ref]
    @PERMISSION_NM                                               varchar(50)
  , @MODULE_REF_ID                                               int
  , @ENUM_MAPPING_VAL_NBR                                        int
  , @PERMISSION_DISPLAY_NM                                       varchar(150)
  , @WKR_ID_TXT                                                  varchar(100)
  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

     INSERT INTO AION.PERMISSION_REF
          (
            PERMISSION_NM
          , MODULE_REF_ID
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          , ENUM_MAPPING_VAL_NBR
          , PERMISSION_DISPLAY_NM
          )
     VALUES
          (
            @PERMISSION_NM
          , @MODULE_REF_ID
          , @WKR_ID_TXT
          , GETDATE()
          , @WKR_ID_TXT
          , GETDATE()
          , @ENUM_MAPPING_VAL_NBR
          , @PERMISSION_DISPLAY_NM
          )

     SELECT @error = @@ERROR
          , @ReturnValue = SCOPE_IDENTITY()

     IF @error != 0
          RAISERROR('Error adding PermissionRef record.', 18,1)

RETURN
GO