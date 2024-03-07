-- =======================================================
-- Create Stored Procedure Template for Azure SQL Database
-- =======================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/***********************************************************************************************************************                            
* Object:       usp_select_aion_get_projects_mmf_complete                  
* Description:  Gets a list of closed MMF projects between two dates.                          
* Parameters:                               
*               @startdate    DATETIME,                             
*               @enddate      DATETIME,                             
* Returns:      list                            
* Comments:                                 
* Version:      1.0                            
* Created by:   jallen                            
* Created:      8/03/2023                            
************************************************************************************************************************                            
* Change History: Date, Name, Description             
* 8/03/2023  jallen initial create  
***********************************************************************************************************************/
CREATE PROCEDURE usp_select_aion_get_projects_mmf_complete
(
    -- Add the parameters for the stored procedure here
    @startdate datetime,
	@enddate datetime
)
AS
BEGIN
    WITH 
	CTE_PROJECT_AUDITS
	AS
	(
		SELECT PA.PROJECT_ID
		
		FROM PROJECT_AUDIT PA
		INNER JOIN PROJECT P ON PA.PROJECT_ID = P.PROJECT_ID 
		INNER JOIN PROJECT_TYPE_REF PTR ON P.PROJECT_TYP_REF_ID = PTR.PROJECT_TYP_REF_ID
		INNER JOIN AUDIT_ACTION_REF AAR ON AAR.AUDIT_ACTION_REF_ID = PA.AUDIT_ACTION_REF_ID
		WHERE PTR.PROJECT_TYP_REF_NM = 'MMF'
		AND PA.AUDIT_DT BETWEEN @startdate AND @enddate
		AND AAR.AUDIT_ACTION_NM = 'Accela Status'
		AND PA.AUDIT_ACTION_DETAILS_TXT = 'Complete - Project Ended - Success' 
	)
	SELECT *
	FROM PROJECT
	WHERE PROJECT_ID IN (SELECT PROJECT_ID FROM CTE_PROJECT_AUDITS)
END

