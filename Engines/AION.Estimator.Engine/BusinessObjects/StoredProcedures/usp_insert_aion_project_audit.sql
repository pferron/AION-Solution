/***********************************************************************************************************************  
* Object: usp_insert_aion_project_audit  
* Description: Inserts ProjectAudit record.  
* Parameters:  
*  @PROJECT_ID                                                  int  
*  @AUDIT_ACTION_DETAILS_TXT                                    varchar(8000)  
*  @AUDIT_USER_NM                                               varchar(50)  
*  @AUDIT_DT                                                    datetime  
*  @AUDIT_ACTION_REF_ID                                         int  
*  @WKR_ID_TXT                                                  varchar(100)  
*  
* Returns:      Identity column of new record.  
* Comments:     If CREATED_DTTM and/or UPDATED_DTTM fields exist in table, GETDATE() is inserted.  
*               Worker ID will be inserted for WKR_ID_CREATED_TXT and WKR_ID_UPDATED_TXT fields if they exist.  
* Version:      1.0  
* Created by:   AION_user  
* Created:      2/27/2020  
************************************************************************************************************************  
* Change History: Date, Name, Description  
* 2/27/2020    AION_user     Auto-generated  
* 2/10/2022 jlindsay add project cycle id  
***********************************************************************************************************************/
ALTER PROCEDURE AION.[usp_insert_aion_project_audit] @PROJECT_ID INT,
	@AUDIT_ACTION_DETAILS_TXT VARCHAR(8000),
	@AUDIT_USER_NM VARCHAR(50),
	@AUDIT_DT DATETIME,
	@AUDIT_ACTION_REF_ID INT,
	@WKR_ID_TXT VARCHAR(100),
	@ReturnValue INT OUTPUT
AS
BEGIN
	DECLARE @error INT
	DECLARE @CurrentProjectCycleId INT;

	SELECT TOP 1 @CurrentProjectCycleId = PROJECT_CYCLE_ID
	FROM AION.PROJECT_CYCLE
	WHERE PROJECT_ID = @PROJECT_ID
		AND CURRENT_CYCLE_IND = 1
	ORDER BY PROJECT_CYCLE_ID DESC;

	INSERT INTO AION.PROJECT_AUDIT (
		PROJECT_ID,
		AUDIT_ACTION_DETAILS_TXT,
		AUDIT_USER_NM,
		AUDIT_DT,
		WKR_ID_CREATED_TXT,
		CREATED_DTTM,
		WKR_ID_UPDATED_TXT,
		UPDATED_DTTM,
		AUDIT_ACTION_REF_ID,
		PROJECT_CYCLE_ID
		)
	VALUES (
		@PROJECT_ID,
		@AUDIT_ACTION_DETAILS_TXT,
		@AUDIT_USER_NM,
		@AUDIT_DT,
		@WKR_ID_TXT,
		GETDATE(),
		@WKR_ID_TXT,
		GETDATE(),
		@AUDIT_ACTION_REF_ID,
		@CurrentProjectCycleId
		)

	SELECT @error = @@ERROR,
		@ReturnValue = SCOPE_IDENTITY()

	IF @error != 0
		RAISERROR (
				'Error adding ProjectAudit record.',
				18,
				1
				)

	RETURN
END
