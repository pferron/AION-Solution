
/***********************************************************************************************************************
* Object:       usp_update_aion_project
* Description:  Updates Project record using supplied parameters.
* Parameters:   
*               @PROJECT_ID                                                  int
*               @PROJECT_NM                                                  varchar(100)
*               @EXTERNAL_SYSTEM_REF_ID                                      int
*               @UPDATED_DTTM                                                datetime
*               @PROJECT_STATUS_REF_ID                                       int
*               @PROJECT_TYP_REF_ID                                          int
*               @SRC_SYSTEM_VAL_TXT                                          varchar(255)
*               @TAG_CREATED_ID_NUM                                          varchar(10)
*               @TAG_CREATED_BY_TS                                           datetime
*               @TAG_UPDATED_BY_TS                                           datetime
*               @TAG_UPDATED_BY_ID_NUM                                       varchar(10)
*               @ASSIGNED_ESTIMATOR_ID                                       int
*               @ASSIGNED_FACILITATOR_ID                                     int
*               @PROJECT_MODE_REF_ID                                         int
*               @WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Number of rows affected.
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.
* Version:      1.0
* Created by:   AION_user
* Created:      10/10/2019
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/10/2019    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_update_aion_project]

    @PROJECT_ID                                                  int
  , @PROJECT_NM                                                  varchar(100)
  , @EXTERNAL_SYSTEM_REF_ID                                      int
  , @UPDATED_DTTM                                                datetime
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

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       UPDATE PROJECT
       SET
            PROJECT_NM                                                   = @PROJECT_NM
          , EXTERNAL_SYSTEM_REF_ID                                       = @EXTERNAL_SYSTEM_REF_ID
          , WKR_ID_UPDATED_TXT                                           = @WKR_ID_TXT
          , UPDATED_DTTM                                                 = GETDATE()
          , PROJECT_STATUS_REF_ID                                        = @PROJECT_STATUS_REF_ID
          , PROJECT_TYP_REF_ID                                           = @PROJECT_TYP_REF_ID
          , SRC_SYSTEM_VAL_TXT                                           = @SRC_SYSTEM_VAL_TXT
          , TAG_CREATED_ID_NUM                                           = @TAG_CREATED_ID_NUM
          , TAG_CREATED_BY_TS                                            = @TAG_CREATED_BY_TS
          , TAG_UPDATED_BY_TS                                            = @TAG_UPDATED_BY_TS
          , TAG_UPDATED_BY_ID_NUM                                        = @TAG_UPDATED_BY_ID_NUM
          , ASSIGNED_ESTIMATOR_ID                                        = @ASSIGNED_ESTIMATOR_ID
          , ASSIGNED_FACILITATOR_ID                                      = @ASSIGNED_FACILITATOR_ID
          , PROJECT_MODE_REF_ID                                          = @PROJECT_MODE_REF_ID

       WHERE
          PROJECT_ID                                                     = @PROJECT_ID       
       AND 
          ISNULL(CONVERT(varchar(19), UPDATED_DTTM, 120), '')             = ISNULL(CONVERT(varchar(19), @UPDATED_DTTM, 120), '')
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error updating Project record.', 18,1)

     IF @ReturnValue = 0
          RAISERROR('Data was changed/deleted prior to update.', 18,100)

RETURN
GO