/***********************************************************************************************************************      
* Object:       usp_select_aion_notes_get_list      
* Description:  Retrieves Notes list for given parameter(s).      
* Parameters:         
*    @projectID INT,    
*     @noteTypeID INT  ,   
*     @projectNumber varchar(100)  
*      
* Returns:      Recordset.      
* Comments:      
* Version:      1.0      
* Created by:   AION_user      
* Created:      10/2/2019      
************************************************************************************************************************      
* Change History: Date, Name, Description      
* 10/2/2019    AION_user     Auto-generated      
* 1/31/2020     jeanine lindsay add parent notes id, business ref id      
* 12/3/2021 jlindsay order by created dttm desc   
* 1/26/2022 jlindsay add project number  
* 2/9/2022 jlindsay add  ISNULL(@noteTypeID,0)= 0  for note type id
***********************************************************************************************************************/
ALTER PROCEDURE [AION].[usp_select_aion_notes_get_list_by_Project] @projectID INT,
	@noteTypeID INT,
	@projectNumber VARCHAR(100) = NULL
AS
BEGIN
	--if the project id is null, get it from the project number  
	IF (isnull(@projectid, 0) = 0)
	BEGIN
		SELECT @projectid = PROJECT_ID
		FROM AION.PROJECT
		WHERE SRC_SYSTEM_VAL_TXT = @projectNumber;
	END

	SELECT [NOTES_ID],
		[NOTES_COMMENT],
		[WKR_ID_CREATED_TXT],
		[CREATED_DTTM],
		[WKR_ID_UPDATED_TXT],
		[UPDATED_DTTM],
		[PROJECT_ID],
		[NOTES_TYP_REF_ID],
		[PARENT_NOTES_ID],
		[BUSINESS_REF_ID]
	FROM AION.NOTES n
	WHERE n.PROJECT_ID = @projectID
		AND (
			ISNULL(@noteTypeID, 0) = 0
			OR n.NOTES_TYP_REF_ID = @noteTypeID
			)
	ORDER BY CREATED_DTTM DESC;

	RETURN
END
