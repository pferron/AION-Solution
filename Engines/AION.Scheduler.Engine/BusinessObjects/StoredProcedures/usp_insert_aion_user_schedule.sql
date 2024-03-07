/***********************************************************************************************************************  
* Object: usp_insert_aion_user_schedule  
* Description: Inserts UserSchedule record.  
* Parameters:  
*  @START_DTTM                                                  datetime  
*  @END_DTTM                                                    datetime  
*  @PROJECT_SCHEDULE_ID                                         int  
*  @BUSINESS_REF_ID                               int  
*  @USER_ID                                                     int  
*  @WKR_ID_TXT                                                  varchar(100)  
*  
* Returns:      Identity column of new record.  
* Comments:     If CREATED_DTTM and/or UPDATED_DTTM fields exist in table, GETDATE() is inserted.  
*               Worker ID will be inserted for WKR_ID_CREATED_TXT and WKR_ID_UPDATED_TXT fields if they exist.  
* Version:      1.0  
* Created by:   AION_user  
* Created:      3/19/2020  
************************************************************************************************************************  
* Change History: Date, Name, Description  
* 3/19/2020    AION_user     Auto-generated  
* 4/3/2020  jlindsay    remove user_business_reliationship_id, add business_ref_id  
* 7/12/2022	jlindsay	check for duplicate entry, return duplicate id if exists without inserting new record
*						this will prevent rogue duplication
***********************************************************************************************************************/
ALTER PROCEDURE [AION].[usp_insert_aion_user_schedule] @START_DTTM DATETIME,
	@END_DTTM DATETIME,
	@PROJECT_SCHEDULE_ID INT,
	@BUSINESS_REF_ID INT,
	@USER_ID INT,
	@WKR_ID_TXT VARCHAR(100),
	@ReturnValue INT OUTPUT
AS
BEGIN
	DECLARE @error INT
	DECLARE @existingID INT = 0;

	SELECT @existingID = user_schedule_id
	FROM AION.user_schedule
	WHERE [user_id] = @USER_ID
		AND project_schedule_id = @PROJECT_SCHEDULE_ID
		AND business_ref_id = @BUSINESS_REF_ID
		AND start_dttm = @START_DTTM
		AND end_dttm = @END_DTTM;

	IF (ISNULL(@existingID, 0) > 0)
	BEGIN
		SELECT @error = @@ERROR,
			@ReturnValue = @existingID;

		IF @error != 0
			RAISERROR (
					'Error adding UserSchedule record.',
					18,
					1
					)
	END
	ELSE
	BEGIN
		INSERT INTO AION.USER_SCHEDULE (
			START_DTTM,
			END_DTTM,
			PROJECT_SCHEDULE_ID,
			BUSINESS_REF_ID,
			USER_ID,
			WKR_ID_CREATED_TXT,
			CREATED_DTTM,
			WKR_ID_UPDATED_TXT,
			UPDATED_DTTM
			)
		VALUES (
			@START_DTTM,
			@END_DTTM,
			@PROJECT_SCHEDULE_ID,
			@BUSINESS_REF_ID,
			@USER_ID,
			@WKR_ID_TXT,
			GETDATE(),
			@WKR_ID_TXT,
			GETDATE()
			)

		SELECT @error = @@ERROR,
			@ReturnValue = SCOPE_IDENTITY()

		IF @error != 0
			RAISERROR (
					'Error adding UserSchedule record.',
					18,
					1
					)
	END

	RETURN
END
