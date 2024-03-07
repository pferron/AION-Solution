/***********************************************************************************************************************
* Object:       usp_select_aion_average_estimation_hours_factor_getbyoccpncycnstrctn
* Description:  Retrieves AverageEstimationHoursFactor list for given parameter(s).
* Parameters:   
*               @PROJECT_OCCUPANCY_TYP_MAP_NM VARCHAR(50), 
*               @CONSTR_TYP_TXT               VARCHAR(50)
*
* Returns:      Recordset.
* Comments:     Developer may need to manually join to other tables, such as code tables, to get additional info for retrieval.
*               This proc expects id_person and/or id_file to generate list; modify as necessary.
*               Include ORDER BY clause as necessary.
* Version:      1.0
* Created by:   carol.lindsay
* Created:      10/29/2019
************************************************************************************************************************
* Change History: Date, Name, Description
* 10/29/2019    carol.lindsay     joins necessary tables for factors
* 07/16/2021    jallen        change join for occupancy type ref id
***********************************************************************************************************************/

ALTER PROCEDURE usp_select_aion_average_estimation_hours_factor_getbyoccpncycnstrctn
(
  @PROJECT_OCCUPANCY_TYP_MAP_NM VARCHAR(50), 
  @CONSTR_TYP_TXT               VARCHAR(50))
AS
BEGIN
--get the id for the occupancy type 
DECLARE 
  @OCCUPANCY_TYP_REF_ID INT;
SELECT @OCCUPANCY_TYP_REF_ID = OCCUPANCY_TYP_REF_ID
FROM AION.OCCUPANCY_TYPE_REF
WHERE OCCUPANCY_TYP_NM=@PROJECT_OCCUPANCY_TYP_MAP_NM;
SELECT aehf.OCCUPANCY_TYP_REF_ID, 
       aehf.CONSTR_TYP_TXT, 
       aehf.AVERAGE_ESTIMATION_HOURS_FACTOR_ID, 
       aehf.BUILD_SQFT_FACTOR_NBR, 
       aehf.ELCTR_SQFT_FACTOR_NBR, 
       aehf.MECH_SQFT_FACTOR_NBR, 
       aehf.PLUMB_SQFT_FACTOR_NBR, 
       aehf.BUILD_COC_FACTOR_NBR, 
       aehf.ELCTR_COC_FACTOR_NBR, 
       aehf.MECH_COC_FACTOR_NBR, 
       aehf.PLUMB_COC_FACTOR_NBR, 
       aehf.BUILD_SHEETS_FACTOR_NBR, 
       aehf.ELCTR_SHEETS_FACTOR_NBR, 
       aehf.MECH_SHEETS_FACTOR_NBR, 
       aehf.PLUMB_SHEETS_FACTOR_NBR,
       aehf.WKR_ID_CREATED_TXT,
       aehf.CREATED_DTTM,
       aehf.WKR_ID_UPDATED_TXT,
       aehf.UPDATED_DTTM,
       aehf.ACTIVE_IND,
       aehf.ACTIVE_DT
FROM AION.AVERAGE_ESTIMATION_HOURS_FACTOR aehf
WHERE aehf.OCCUPANCY_TYP_REF_ID=@OCCUPANCY_TYP_REF_ID
      AND aehf.CONSTR_TYP_TXT=@CONSTR_TYP_TXT;
RETURN;
END; 
GO