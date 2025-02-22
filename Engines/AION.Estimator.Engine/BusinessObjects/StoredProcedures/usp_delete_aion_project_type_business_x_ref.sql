/****** Object:  StoredProcedure [AION].[usp_delete_aion_project_type_business_x_ref]    Script Date: 2/27/2020 1:39:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************************************************************
* Object:       usp_delete_aion_project_type_business_x_ref
* Description:  Deletes ProjectTypeBusinessXRef record for given key field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Number of rows affected.
* Version:      1.0
* Created by:   AION_user
* Created:      12/18/2019
************************************************************************************************************************
* Change History: Date, Name, Description
* 12/18/2019    AION_user     Auto-generated
* 
***********************************************************************************************************************/

ALTER PROCEDURE [AION].[usp_delete_aion_project_type_business_x_ref]

    @BUSINESS_REF_ID                                             int
  , @PROJECT_TYP_REF_ID                                          int
  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       DELETE FROM PROJECT_TYPE_BUSINESS_XREF
	   WHERE
			BUSINESS_REF_ID = @BUSINESS_REF_ID and PROJECT_TYP_REF_ID = @PROJECT_TYP_REF_ID
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error deleting ProjectTypeBusinessXRef record.', 18,1)

RETURN

