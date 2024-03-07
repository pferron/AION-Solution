/****** Object:  StoredProcedure [AION].[usp_delete_aion_catalog_ref]    Script Date: 12/11/2019 8:29:06 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************************************************************
* Object:       usp_delete_aion_catalog_ref
* Description:  Deletes CatalogRef record for given CATALOG_KEY_TXT field(s).
* Parameters:   
*               @identity                                                    int
*
* Returns:      Number of rows affected.
* Version:      1.0
* Created by:   AION_user
* Created:      12/4/2019
************************************************************************************************************************
* Change History: Date, Name, Description
* 12/4/2019    AION_user     Auto-generated
* 
***********************************************************************************************************************/

ALTER PROCEDURE [AION].[usp_delete_aion_catalog_ref]

    @identity                                                    int

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       DELETE FROM CATALOG_REF
       WHERE
        
       CATALOG_REF_ID = @identity
        
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error deleting CatalogRef record.', 18,1)

RETURN

