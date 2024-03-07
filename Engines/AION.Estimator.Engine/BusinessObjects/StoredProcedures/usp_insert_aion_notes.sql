/****** Object:  StoredProcedure [AION].[usp_insert_aion_notes]    Script Date: 1/31/2020 3:27:03 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/***********************************************************************************************************************
* Object:	usp_insert_aion_notes
* Description:	Inserts Notes record.
* Parameters:
*		@NOTES_COMMENT                                               varchar(8000)
*		@WORKER_CREATED_BY_ID_NUM                                    varchar(10)
*		@WORKER_CREATED_BY_TS                                        datetime
*		@WORKER_UPDATED_BY_ID_NUM                                    varchar(10)
*		@WORKER_UPDATED_BY_TS                                        datetime
*		@PROJECT_ID                                                  int
*		@NOTES_TYP_REF_ID                                            int
*		@WKR_ID_TXT                                                  varchar(100)
*       @PARENT_NOTES_ID                                             int
*       @BUSINESS_REF_ID                                             int
*
* Returns:      Identity column of new record.
* Comments:     If CREATED_DTTM and/or UPDATED_DTTM fields exist in table, GETDATE() is inserted.
*               Worker ID will be inserted for WKR_ID_CREATED_TXT and WKR_ID_UPDATED_TXT fields if they exist.
* Version:      1.0
* Created by:   AION_user
* Created:      10/2/2019
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/2/2019    AION_user     Auto-generated
* 1/31/2020     jeanine lindsay add parent notes id, business ref id
***********************************************************************************************************************/

ALTER PROCEDURE [AION].[usp_insert_aion_notes]
    @NOTES_COMMENT                                               varchar(8000)
  , @WORKER_CREATED_BY_ID_NUM                                    varchar(10)
  , @WORKER_CREATED_BY_TS                                        datetime
  , @WORKER_UPDATED_BY_ID_NUM                                    varchar(10)
  , @WORKER_UPDATED_BY_TS                                        datetime
  , @PROJECT_ID                                                  int
  , @NOTES_TYP_REF_ID                                            int
  , @WKR_ID_TXT                                                  varchar(100)
  , @PARENT_NOTES_ID                                             int
  , @BUSINESS_REF_ID                                             int
  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

     INSERT INTO NOTES
          (
            NOTES_COMMENT
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          , PROJECT_ID
          , NOTES_TYP_REF_ID
          , PARENT_NOTES_ID
          , BUSINESS_REF_ID

          )
     VALUES
          (
            @NOTES_COMMENT
          , @WORKER_CREATED_BY_ID_NUM
          , @WORKER_CREATED_BY_TS
          , @WORKER_UPDATED_BY_ID_NUM
          , @WORKER_UPDATED_BY_TS
          , @PROJECT_ID
          , @NOTES_TYP_REF_ID
          , @PARENT_NOTES_ID
          , @BUSINESS_REF_ID
          )

     SELECT @error = @@ERROR
          , @ReturnValue = SCOPE_IDENTITY()

     IF @error != 0
          RAISERROR('Error adding Notes record.', 18,1)

RETURN
GO

