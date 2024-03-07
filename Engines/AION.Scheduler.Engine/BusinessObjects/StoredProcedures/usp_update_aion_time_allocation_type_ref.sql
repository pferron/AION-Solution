
/***********************************************************************************************************************
* Object:       usp_update_aion_time_allocation_type_ref
* Description:  Updates TimeAllocationTypeRef record using supplied parameters.
* Parameters:   
*               @TIME_ALLOCATION_TYP_REF_ID                                  int
*               @TIME_ALLOCATION_TYP_REF_DESC                                varchar(30)
*               @ENUM_MAPPING_VAL_NBR                                        int
*               @ACTIVE_IND                                                  bit
*               @UPDATED_DTTM                                                datetime
*               @WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Number of rows affected.
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.
* Version:      1.0
* Created by:   AION_user
* Created:      12/7/2021
************************************************************************************************************************
* Change History: Date, Name, Description
* 12/7/2021    AION_user     Auto-generated
* 
***********************************************************************************************************************/

ALTER PROCEDURE [AION].[usp_update_aion_time_allocation_type_ref]

    @TIME_ALLOCATION_TYP_REF_ID                                  int
  , @TIME_ALLOCATION_TYP_REF_DESC                                varchar(30)
  , @ENUM_MAPPING_VAL_NBR                                        int
  , @ACTIVE_IND                                                  bit
  , @UPDATED_DTTM                                                datetime
  , @WKR_ID_TXT                                                  varchar(100)

  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int

       UPDATE [AION].TIME_ALLOCATION_TYPE_REF
       SET
            TIME_ALLOCATION_TYP_REF_DESC                                 = @TIME_ALLOCATION_TYP_REF_DESC
          , ENUM_MAPPING_VAL_NBR                                         = @ENUM_MAPPING_VAL_NBR
          , ACTIVE_IND                                                   = @ACTIVE_IND
          , WKR_ID_UPDATED_TXT                                           = @WKR_ID_TXT
          , UPDATED_DTTM                                                 = GETDATE()

       WHERE
          TIME_ALLOCATION_TYP_REF_ID                                     = @TIME_ALLOCATION_TYP_REF_ID       
       AND 
          ISNULL(CONVERT(varchar(19), UPDATED_DTTM, 120), '')             = ISNULL(CONVERT(varchar(19), @UPDATED_DTTM, 120), '')
          

     SELECT @error = @@ERROR
          , @ReturnValue = @@ROWCOUNT

     IF @error != 0
          RAISERROR('Error updating TimeAllocationTypeRef record.', 18,1)

     IF @ReturnValue = 0
          RAISERROR('Data was changed/deleted prior to update.', 18,100)

RETURN
GO