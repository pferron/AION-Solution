/***********************************************************************************************************************  
* Object: usp_insert_aion_npa_type_ref  
* Description: Inserts NpaTypeRef record.  
* Parameters:  
*  @APPT_TYP_DESC                                               varchar(30)  
*  @ACTIVE_IND                                                  bit  
*  @WKR_ID_TXT                                                  varchar(100)  
*  @TIME_ALLOCATION_TYP_REF_ID INT
* Returns:      Identity column of new record.  
* Comments:     If CREATED_DTTM and/or UPDATED_DTTM fields exist in table, GETDATE() is inserted.  
*               Worker ID will be inserted for WKR_ID_CREATED_TXT and WKR_ID_UPDATED_TXT fields if they exist.  
* Version:      1.0  
* Created by:   AION_user  
* Created:      3/19/2020  
************************************************************************************************************************  
* Change History: Date, Name, Description  
* 3/19/2020    AION_user     Auto-generated  
*  12/7/2021 jlindsay add time allocation type ref id 
***********************************************************************************************************************/
ALTER PROCEDURE [AION].[usp_insert_aion_npa_type_ref] @APPT_TYP_DESC VARCHAR(30),
	@ACTIVE_IND BIT,
	@WKR_ID_TXT VARCHAR(100),
	@TIME_ALLOCATION_TYP_REF_ID INT,
	@ReturnValue INT OUTPUT
AS
DECLARE @error INT

INSERT INTO NON_PROJECT_APPOINTMENT_TYPE_REF (
	APPT_TYP_DESC,
	ACTIVE_IND,
	WKR_ID_CREATED_TXT,
	CREATED_DTTM,
	WKR_ID_UPDATED_TXT,
	UPDATED_DTTM,
	TIME_ALLOCATION_TYP_REF_ID
	)
VALUES (
	@APPT_TYP_DESC,
	@ACTIVE_IND,
	@WKR_ID_TXT,
	GETDATE(),
	@WKR_ID_TXT,
	GETDATE(),
	@TIME_ALLOCATION_TYP_REF_ID
	)

SELECT @error = @@ERROR,
	@ReturnValue = SCOPE_IDENTITY()

IF @error != 0
	RAISERROR (
			'Error adding NpaTypeRef record.',
			18,
			1
			)

RETURN
