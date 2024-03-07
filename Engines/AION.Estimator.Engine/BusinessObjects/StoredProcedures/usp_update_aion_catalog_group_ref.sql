/****** Object:  StoredProcedure [AION].[usp_update_aion_catalog_GRP_ref]    Script Date: 12/11/2019 8:52:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************************************************************
* Object:       usp_update_aion_catalog_group_ref
* Description:  Updates CatalogGroupRef record using supplied parameters.
* Parameters:   
*               @CATALOG_GRP_REF_ID                                        int
*               @CATALOG_GRP_NM                                            varchar(500)
*               @UPDATED_DTTM                                                datetime
*               @WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Number of rows affected.
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.
* Version:      1.0
* Created by:   AION_user
* Created:      12/4/2019
************************************************************************************************************************
* Change History: Date, Name, Description
* 12/4/2019    AION_user     Auto-generated
* 
***********************************************************************************************************************/

ALTER PROCEDURE [AION].[usp_update_aion_catalog_group_ref]

    @CATALOG_GRP_REF_ID                                        int
  , @CATALOG_GRP_NM                                            varchar(500) = NULL
  , @UPDATED_DTTM                                                datetime = NULL
  , @WKR_ID_TXT                                                  varchar(100) = NULL

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       UPDATE CATALOG_GROUP_REF
       SET
            
			CATALOG_GRP_NM                                             = ISNULL(@CATALOG_GRP_NM,CATALOG_GRP_NM)
          , WKR_ID_UPDATED_TXT                                           = ISNULL(@WKR_ID_TXT,WKR_ID_UPDATED_TXT)
          , UPDATED_DTTM                                                 = GETDATE()

		WHERE CATALOG_GRP_REF_ID                                         = @CATALOG_GRP_REF_ID

       AND 
          ISNULL(CONVERT(varchar(19), UPDATED_DTTM, 120), '')             = ISNULL(CONVERT(varchar(19), @UPDATED_DTTM, 120), '')
          
		
     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error updating CatalogGroupRef record.', 18,1)

     IF @ReturnValue = 0
          RAISERROR('Data was changed/deleted prior to update.', 18,100)

RETURN
