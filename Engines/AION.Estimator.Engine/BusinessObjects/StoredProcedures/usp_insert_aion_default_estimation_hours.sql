/****** Object:  StoredProcedure [AION].[usp_insert_aion_default_estimation_hours]    Script Date: 2/19/2020 10:53:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************************************************************
* Object:	usp_insert_aion_default_estimation_hours
* Description:	Inserts DefaultEstimationHours record.
* Parameters:
*		@DEFAULT_HOURS_NBR                                           int
*		@WORKER_CREATED_BY_ID_NUM                                    varchar(10)
*		@WORKER_UPDATED_BY_ID_NUM                                    varchar(10)
*		@WORKER_CREATED_BY_TS                                        datetime
*		@WORKER_UPDATED_BY_TS                                        datetime
*		@BUSINESS_REF_ID                                             int
*		@PROJECT_TYP_REF_ID                                          int
*		@WKR_ID_TXT                                                  varchar(100)
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
* 
***********************************************************************************************************************/

ALTER PROCEDURE [AION].[usp_insert_aion_default_estimation_hours]
    @DEFAULT_HOURS_NBR                                           int
  , @WORKER_CREATED_BY_ID_NUM                                    varchar(10)
  , @WORKER_UPDATED_BY_ID_NUM                                    varchar(10)
  , @WORKER_CREATED_BY_TS                                        datetime
  , @WORKER_UPDATED_BY_TS                                        datetime
  , @BUSINESS_REF_ID                                             int
  , @PROJECT_TYP_REF_ID                                          int
  , @WKR_ID_TXT                                                  varchar(100)
  , @ENABLED_IND												 bit
  , @ESTIMATION_HOURS_TXT										 varchar(255)	
  , @ReturnValue                                                 int OUTPUT
AS

     DECLARE @error   int

     INSERT INTO DEFAULT_ESTIMATION_HOURS
          (
            DEFAULT_HOURS_NBR
          , WKR_ID_CREATED_TXT
          , WKR_ID_UPDATED_TXT
		  , CREATED_DTTM
          , UPDATED_DTTM
          , BUSINESS_REF_ID
          , PROJECT_TYP_REF_ID
		  , ENABLED_IND
          , ESTIMATION_HOURS_TXT
          )
     VALUES
          (
            @DEFAULT_HOURS_NBR
          , @WORKER_CREATED_BY_ID_NUM
          , @WORKER_UPDATED_BY_ID_NUM
          , @WORKER_CREATED_BY_TS
          , @WORKER_UPDATED_BY_TS
          , @BUSINESS_REF_ID
          , @PROJECT_TYP_REF_ID
		  , @ENABLED_IND
		  , @ESTIMATION_HOURS_TXT
          )

     SELECT @error = @@ERROR
          , @ReturnValue = SCOPE_IDENTITY()

     IF @error != 0
          RAISERROR('Error adding DefaultEstimationHours record.', 18,1)

RETURN
