
/***********************************************************************************************************************
* Object:	usp_insert_aion_template
* Description:	Inserts Template record.
* Parameters:
*		@TEMPLATE_NM                                                 varchar(50)
*		@TEMPLATE_TYP_ID                                             int
*		@TEMPLATE_TXT                                                varchar(8000)
*		@ACTIVE_IND                                                  bit
*		@ACTIVE_DT                                                   datetime
*		@WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Identity column of new record.
* Comments:     If CREATED_DTTM and/or UPDATED_DTTM fields exist in table, GETDATE() is inserted.
*               Worker ID will be inserted for WKR_ID_CREATED_TXT and WKR_ID_UPDATED_TXT fields if they exist.
* Version:      1.0
* Created by:   AION_user
* Created:      1/26/2021
************************************************************************************************************************
* Change History: Date, Name, Description
* 1/26/2021    AION_user     Auto-generated
* 
***********************************************************************************************************************/

ALTER PROCEDURE AION.[usp_insert_aion_template]
    @TEMPLATE_NM                                                 varchar(300)
  , @TEMPLATE_TYP_ID                                             int
  , @TEMPLATE_TXT                                                varchar(8000)
  , @ACTIVE_IND                                                  bit
  , @ACTIVE_DT                                                   datetime
  , @WKR_ID_TXT                                                  varchar(100)
  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

     INSERT INTO AION.TEMPLATE
          (
            TEMPLATE_NM
          , TEMPLATE_TYP_ID
          , TEMPLATE_TXT
          , ACTIVE_IND
          , ACTIVE_DT
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          )
     VALUES
          (
            @TEMPLATE_NM
          , @TEMPLATE_TYP_ID
          , @TEMPLATE_TXT
          , @ACTIVE_IND
          , @ACTIVE_DT
          , @WKR_ID_TXT
          , GETDATE()
          , @WKR_ID_TXT
          , GETDATE()
          )

     SELECT @error = @@ERROR
          , @ReturnValue = SCOPE_IDENTITY()

     IF @error != 0
          RAISERROR('Error adding Template record.', 18,1)

RETURN
GO