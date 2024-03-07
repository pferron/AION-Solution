
/***********************************************************************************************************************
* Object:       usp_select_aion_projects_mmf_complete
* Description:  Retrieves MMF projects completed within certain date range for given parameter(s).
* Parameters:   
*               @startdate                                                   datetime
*				@enddate                                                   datetime

* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval.
*               This proc expects id_person and/or id_file to generate list; modify as necessary.
*               Include ORDER BY clause as necessary.
* Version:      1.0
* Created by:   AION_user
* Created:      2/20/2023
************************************************************************************************************************
* Change History: Date, Name, Description
* 2/20/2023    AION_user     Auto-generated
* 
***********************************************************************************************************************/

CREATE PROCEDURE [usp_select_aion_projects_mmf_complete]
@startdate datetime,
@enddate datetime
AS

select* 
from PROJECT p
inner join PROJECT_TYPE_REF ptr on p.PROJECT_TYP_REF_ID = ptr.PROJECT_TYP_REF_ID
inner join PROJECT_AUDIT pa on pa.PROJECT_ID =  p.PROJECT_ID
inner join AUDIT_ACTION_REF aar on aar.AUDIT_ACTION_REF_ID = pa.AUDIT_ACTION_REF_ID
where 
ptr.PROJECT_TYP_REF_NM = 'MMF' and 
aar.AUDIT_ACTION_NM = 'Accela Status' and 
pa.AUDIT_ACTION_DETAILS_TXT = 'Project Ended - Success' and 
pa.AUDIT_DT between @startdate and @enddate

RETURN

GO