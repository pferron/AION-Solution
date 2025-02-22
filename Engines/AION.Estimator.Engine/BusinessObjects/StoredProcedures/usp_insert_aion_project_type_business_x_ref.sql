/****** Object:  StoredProcedure [AION].[usp_insert_aion_project_type_business_x_ref]    Script Date: 2/27/2020 1:58:59 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************************************************************
* Object:	usp_insert_aion_project_type_business_x_ref
* Description:	Inserts ProjectTypeBusinessXRef record.
* Parameters:
*		@BUSINESS_REF_ID                                             int
*		@PROJECT_TYP_REF_ID                                          int
*
* Returns:      Identity column of new record.
* Comments:     If CREATED_DTTM and/or UPDATED_DTTM fields exist in table, GETDATE() is inserted.
*               Worker ID will be inserted for WKR_ID_CREATED_TXT and WKR_ID_UPDATED_TXT fields if they exist.
* Version:      1.0
* Created by:   AION_user
* Created:      12/18/2019
************************************************************************************************************************
* Change History: Date, Name, Description
* 12/18/2019    AION_user     Auto-generated
* 
***********************************************************************************************************************/

ALTER PROCEDURE [AION].[usp_insert_aion_project_type_business_x_ref]
    @BUSINESS_REF_ID                                             int
  , @PROJECT_TYP_REF_ID                                          int
  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

     INSERT INTO PROJECT_TYPE_BUSINESS_XREF
          (
            BUSINESS_REF_ID
          , PROJECT_TYP_REF_ID
          )
     VALUES
          (
            @BUSINESS_REF_ID
          , @PROJECT_TYP_REF_ID
          )

     SELECT @error = @@ERROR
          , @ReturnValue = SCOPE_IDENTITY()

     IF @error != 0
          RAISERROR('Error adding ProjectTypeBusinessXRef record.', 18,1)

RETURN
