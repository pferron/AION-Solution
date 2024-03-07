
/***********************************************************************************************************************
* Object:	usp_insert_aion_project_rtap_mapping
* Description:	Inserts ProjectRtapMapping record.
* Parameters:
*		@PROJECT_ID                                                  int
*		@ORIGINAL_PROJECT_ID                                         int
*		@WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Identity column of new record.
* Comments:     If CREATED_DTTM and/or UPDATED_DTTM fields exist in table, GETDATE() is inserted.
*               Worker ID will be inserted for WKR_ID_CREATED_TXT and WKR_ID_UPDATED_TXT fields if they exist.
* Version:      1.0
* Created by:   AION_user
* Created:      10/2/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/2/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_insert_aion_project_rtap_mapping]
    @PROJECT_ID                                                  int
  , @ORIGINAL_PROJECT_ID                                         int
  , @WKR_ID_TXT                                                  varchar(100)
  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

     IF NOT EXISTS ( SELECT 1 FROM PROJECT_RTAP_MAPPING WHERE PROJECT_ID = @PROJECT_ID )
     BEGIN

     INSERT INTO PROJECT_RTAP_MAPPING
          (
            PROJECT_ID
          , ORIGINAL_PROJECT_ID
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          )
     VALUES
          (
            @PROJECT_ID
          , @ORIGINAL_PROJECT_ID
          , @WKR_ID_TXT
          , GETDATE()
          , @WKR_ID_TXT
          , GETDATE()
          )

     END

     SELECT @error = @@ERROR
          , @ReturnValue = SCOPE_IDENTITY()

     IF @error != 0
          RAISERROR('Error adding ProjectRtapMapping record.', 18,1)

RETURN
GO