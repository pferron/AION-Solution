/****** Object:  StoredProcedure [AION].[usp_insert_aion_catalog_group_ref]    Script Date: 12/11/2019 8:52:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************************************************************
* Object:	usp_insert_aion_catalog_group_ref
* Description:	Inserts CatalogGroupRef record.
* Parameters:
*		@CATALOG_GROUP_NM                                            varchar(500)
*		@WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Identity column of new record.
* Comments:     If CREATED_DTTM and/or UPDATED_DTTM fields exist in table, GETDATE() is inserted.
*               Worker ID will be inserted for WKR_ID_CREATED_TXT and WKR_ID_UPDATED_TXT fields if they exist.
* Version:      1.0
* Created by:   AION_user
* Created:      12/4/2019
************************************************************************************************************************
* Change History: Date, Name, Description
* 12/4/2019    AION_user     Auto-generated
* 
***********************************************************************************************************************/

ALTER PROCEDURE [AION].[usp_insert_aion_catalog_group_ref]
    @CATALOG_GROUP_NM                                            varchar(500)
  , @WKR_ID_TXT                                                  varchar(100)
  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

     INSERT INTO CATALOG_GROUP_REF
          (
            CATALOG_GRP_NM
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          )
     VALUES
          (
            @CATALOG_GROUP_NM
          , @WKR_ID_TXT
          , GETDATE()
          , @WKR_ID_TXT
          , GETDATE()
          )

     SELECT @error = @@ERROR
          , @ReturnValue = SCOPE_IDENTITY()

     IF @error != 0
          RAISERROR('Error adding CatalogGroupRef record.', 18,1)

RETURN