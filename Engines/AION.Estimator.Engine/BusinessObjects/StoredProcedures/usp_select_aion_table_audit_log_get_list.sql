/***********************************************************************************************************************  
* Object:       usp_select_aion_table_audit_log_get_list  
* Description:  Retrieves TableAuditLog list for given parameter(s).  
* Parameters:     
*    @StartDate DATETIME = '01/01/1900',  
*    @EndDate DATETIME = '12/31/2050'  ,
*	@TableNamesCsv varchar(8000)
*  
* Returns:      Recordset.  
* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval.  
*               This proc expects id_person and/or id_file to generate list; modify as necessary.  
*               Include ORDER BY clause as necessary.  
* Version:      1.0  
* Created by:   AION_user  
* Created:      3/5/2020  
************************************************************************************************************************  
* Change History: Date, Name, Description  
* 3/5/2020    AION_user     Auto-generated  
* 3/5/2020  jlindsay    alter to get by dates  
*	04/05/2021	jlindsay	add ability to choose what table to view
***********************************************************************************************************************/
ALTER PROCEDURE [usp_select_aion_table_audit_log_get_list] @StartDate DATETIME = '01/01/1900',
	@EndDate DATETIME = '12/31/2050',
	@TableNamesCsv VARCHAR(8000) = ''
AS
BEGIN
	IF (ISNULL(@TableNamesCsv, '') = '')
	BEGIN
		SELECT TABLE_AUDIT_LOG_ID,
			AUDIT_TYP_CD,
			AUDIT_TABLE_NM,
			AUDIT_TABLE_PK_ID,
			AUDIT_FIELD_NM,
			OLD_VAL_TXT,
			NEW_VAL_TXT,
			WKR_ID_UPDATED_TXT,
			UPDATED_DTTM
		FROM TABLE_AUDIT_LOG
		WHERE UPDATED_DTTM BETWEEN @StartDate
				AND @EndDate
		ORDER BY TABLE_AUDIT_LOG_ID DESC;
	END
	ELSE
	BEGIN
		WITH csvTables
		AS (
			SELECT [value] AS AUDIT_TABLE_NM
			FROM STRING_SPLIT(@TableNamesCsv, ',')
			)
		SELECT TOP 100 TABLE_AUDIT_LOG_ID,
			AUDIT_TYP_CD,
			l.AUDIT_TABLE_NM,
			AUDIT_TABLE_PK_ID,
			AUDIT_FIELD_NM,
			OLD_VAL_TXT,
			NEW_VAL_TXT,
			WKR_ID_UPDATED_TXT,
			UPDATED_DTTM
		FROM TABLE_AUDIT_LOG l
		INNER JOIN csvTables c ON l.AUDIT_TABLE_NM = c.AUDIT_TABLE_NM
		WHERE UPDATED_DTTM BETWEEN @StartDate
				AND @EndDate
		ORDER BY TABLE_AUDIT_LOG_ID DESC;
	END

	RETURN;
END
