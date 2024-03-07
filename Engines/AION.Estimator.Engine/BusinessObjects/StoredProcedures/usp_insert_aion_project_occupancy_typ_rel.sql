
/***********************************************************************************************************************
* Object:	usp_insert_aion_project_occupancy_typ_rel
* Description:	Inserts ProjectOccupancyTypRel record.
* Parameters:
*		@OCCUPANCY_TYP_REF_ID                                        int
*		@PROJECT_OCCUPANCY_TYP_MAP_REF_ID                            int
*		@WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Identity column of new record.
* Comments:     If CREATED_DTTM and/or UPDATED_DTTM fields exist in table, GETDATE() is inserted.
*               Worker ID will be inserted for WKR_ID_CREATED_TXT and WKR_ID_UPDATED_TXT fields if they exist.
* Version:      1.0
* Created by:   AION_user
* Created:      10/28/2019
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/28/2019    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_insert_aion_project_occupancy_typ_rel]
    @OCCUPANCY_TYP_REF_ID                                        int
  , @PROJECT_OCCUPANCY_TYP_MAP_REF_ID                            int
  , @WKR_ID_TXT                                                  varchar(100)
  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

     INSERT INTO PROJECT_OCCUPANCY_TYPE_RELATIONSHIP
          (
            OCCUPANCY_TYP_REF_ID
          , PROJECT_OCCUPANCY_TYP_MAP_REF_ID
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          )
     VALUES
          (
            @OCCUPANCY_TYP_REF_ID
          , @PROJECT_OCCUPANCY_TYP_MAP_REF_ID
          , @WKR_ID_TXT
          , GETDATE()
          , @WKR_ID_TXT
          , GETDATE()
          )

     SELECT @error = @@ERROR
          , @ReturnValue = SCOPE_IDENTITY()

     IF @error != 0
          RAISERROR('Error adding ProjectOccupancyTypRel record.', 18,1)

RETURN
GO