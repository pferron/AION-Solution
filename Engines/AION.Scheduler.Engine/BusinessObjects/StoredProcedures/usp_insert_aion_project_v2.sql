/***********************************************************************************************************************
* Object:	usp_insert_aion_project
* Description:	Inserts Project record.
* Parameters:
*		@PROJECT_NM                                                  varchar(100)
*		@EXTERNAL_SYSTEM_REF_ID                                      int
*		@PROJECT_STATUS_REF_ID                                       int
*		@PROJECT_TYP_REF_ID                                          int
*		@SRC_SYSTEM_VAL_TXT                                          varchar(255)
*		@TAG_CREATED_ID_NUM                                          varchar(10)
*		@TAG_CREATED_BY_TS                                           datetime
*		@TAG_UPDATED_BY_TS                                           datetime
*		@TAG_UPDATED_BY_ID_NUM                                       varchar(10)
*		@ASSIGNED_ESTIMATOR_ID                                       int
*		@ASSIGNED_FACILITATOR_ID                                     int
*		@PROJECT_MODE_REF_ID                                         int
*		@WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Identity column of new record.
* Comments:     If CREATED_DTTM and/or UPDATED_DTTM fields exist in table, GETDATE() is inserted.
*               Worker ID will be inserted for WKR_ID_CREATED_TXT and WKR_ID_UPDATED_TXT fields if they exist.
* Version:      1.0
* Created by:   AION_user
* Created:      10/10/2019
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/10/2019    AION_user     Auto-generated
* 
***********************************************************************************************************************/

ALTER PROCEDURE [AION].[usp_insert_aion_project_v2]
    @PROJECT_NM                                                  varchar(100)
  , @EXTERNAL_SYSTEM_REF_ID                                      int
  , @PROJECT_STATUS_REF_ID                                       int
  , @PROJECT_TYP_REF_ID                                          int
  , @SRC_SYSTEM_VAL_TXT                                          varchar(255)
  , @TAG_CREATED_ID_NUM                                          varchar(10)
  , @TAG_CREATED_BY_TS                                           datetime
  , @TAG_UPDATED_BY_TS                                           datetime
  , @TAG_UPDATED_BY_ID_NUM                                       varchar(10)
  , @ASSIGNED_ESTIMATOR_ID                                       int
  , @ASSIGNED_FACILITATOR_ID                                     int
  , @PROJECT_MODE_REF_ID                                         int
  , @WKR_ID_TXT                                                  varchar(100)
  , @PROJECT_LVL_TXT		                                     varchar(10) = null
  , @PROJECT_ADDR_TXT											 varchar(255) = null
  , @PROJECT_MANAGER_ID											 int = null
  , @RTAP_IND													 bit
  , @GATE_ACCEPTED_IND                                           bit

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

     INSERT INTO PROJECT
          (
            PROJECT_NM
          , EXTERNAL_SYSTEM_REF_ID
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          , PROJECT_STATUS_REF_ID
          , PROJECT_TYP_REF_ID
          , SRC_SYSTEM_VAL_TXT
          , TAG_CREATED_ID_NUM
          , TAG_CREATED_BY_TS
          , TAG_UPDATED_BY_TS
          , TAG_UPDATED_BY_ID_NUM
          , ASSIGNED_ESTIMATOR_ID
          , ASSIGNED_FACILITATOR_ID
          , PROJECT_MODE_REF_ID
		  , PROJECT_LVL_TXT
		  , PROJECT_ADDR_TXT
		  , PROJECT_MANAGER_ID
		  , RTAP_IND
          , GATE_ACCEPTED_IND
          )
     VALUES
          (
            @PROJECT_NM
          , @EXTERNAL_SYSTEM_REF_ID
          , @WKR_ID_TXT
          , GETDATE()
          , @WKR_ID_TXT
          , GETDATE()
          , @PROJECT_STATUS_REF_ID
          , @PROJECT_TYP_REF_ID
          , @SRC_SYSTEM_VAL_TXT
          , @TAG_CREATED_ID_NUM
          , @TAG_CREATED_BY_TS
          , @TAG_UPDATED_BY_TS
          , @TAG_UPDATED_BY_ID_NUM
          , @ASSIGNED_ESTIMATOR_ID
          , @ASSIGNED_FACILITATOR_ID
          , @PROJECT_MODE_REF_ID
		  , isnull(@PROJECT_LVL_TXT,'')
		  , isnull(@PROJECT_ADDR_TXT,'')
		  ,	@PROJECT_MANAGER_ID
		  , @RTAP_IND
          , @GATE_ACCEPTED_IND
          )

     SELECT @error = @@ERROR
          , @ReturnValue = SCOPE_IDENTITY()

     IF @error != 0
          RAISERROR('Error adding Project record.', 18,1)

RETURN
