/****** Object:  StoredProcedure [AION].[usp_update_aion_average_estimation_hours_factor_byoccpncycnstrctn]    Script Date: 2/22/2023 3:23:08 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/***********************************************************************************************************************
* Object:       usp_update_aion_average_estimation_hours_factor_byoccpncycnstrctn
* Description:  Updates AverageEstimationHoursFactor record using supplied parameters.
* Parameters:   
*               @OCCUPANCY_TYP_REF_ID                                        int
*               @CONSTR_TYP_TXT                                              varchar(255)
*               @ACTIVE_IND                                                  bit
*               @ACTIVE_DT                                                   datetime
*               @WKR_ID_TXT                                                  varchar(100)
*
* Returns:      Number of rows affected.
* Comments:     This stored proc checks UPDATED_DTTM to prevent overwriting another user's data.
* Version:      1.0
* Created by:   AION_user
* Created:      12/8/2020
************************************************************************************************************************
* Change History: Date, Name, Description
* 12/8/2020    AION_user     Auto-generated
* 
***********************************************************************************************************************/
ALTER PROCEDURE [AION].[usp_update_aion_average_estimation_hours_factor_byoccpncycnstrctn] 
	@OCCUPANCY_TYP_REF_ID INT,
	@CONSTR_TYP_TXT VARCHAR(255),
	@ACTIVE_IND BIT,
	@ACTIVE_DT DATETIME,
	@WKR_ID_TXT VARCHAR(100),
	@ReturnValue INT OUTPUT
AS
BEGIN
	DECLARE @error INT;

	UPDATE AVERAGE_ESTIMATION_HOURS_FACTOR
	SET WKR_ID_UPDATED_TXT = @WKR_ID_TXT,
		UPDATED_DTTM = GETDATE(),
		ACTIVE_IND = @ACTIVE_IND,
		ACTIVE_DT = @ACTIVE_DT
	WHERE OCCUPANCY_TYP_REF_ID = @OCCUPANCY_TYP_REF_ID
		AND CONSTR_TYP_TXT = @CONSTR_TYP_TXT

	SELECT @error = @@ERROR,
		@ReturnValue = @@ROWCOUNT;

	IF @error != 0
		RAISERROR (
				'Error updating AverageEstimationHoursFactor record.',
				18,
				1
				);

	RETURN;
END
