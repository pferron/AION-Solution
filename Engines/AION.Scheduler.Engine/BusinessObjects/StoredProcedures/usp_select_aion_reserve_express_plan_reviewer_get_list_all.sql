/****** Object:  StoredProcedure [AION].[usp_select_aion_reserve_express_plan_reviewer_get_list_all]    Script Date: 8/7/2020 3:34:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [AION].[usp_select_aion_reserve_express_plan_reviewer_get_list_all]

AS

       SELECT 
            repr.RESERVE_EXPRESS_PLAN_REVIEWER_ID
          , repr.BUSINESS_REF_ID
          , repr.PLAN_REVIEWER_ID
          , repr.ROTATION_NBR
          , repr.WKR_ID_CREATED_TXT
          , repr.CREATED_DTTM
          , repr.WKR_ID_UPDATED_TXT
          , repr.UPDATED_DTTM
		  , u.FIRST_NM
		  , u.LAST_NM

       FROM RESERVE_EXPRESS_PLAN_REVIEWER repr
	   inner join [USER] u on repr.PLAN_REVIEWER_ID = u.[USER_ID]

      ORDER BY BUSINESS_REF_ID, ROTATION_NBR
          

RETURN

