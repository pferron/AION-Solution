/****** Object:  StoredProcedure [AION].[usp_select_aion_reserve_express_reservation_get_by_id]    Script Date: 8/21/2020 8:53:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
ALTER PROCEDURE [AION].[usp_select_aion_reserve_express_reservation_get_by_id]
(
    @identity int
)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

   SELECT [RESERVE_EXPRESS_RESERVATION_ID]
      ,[RESERVE_EXPRESS_DT]
      ,[START_TM]
      ,[END_TM]
      ,[MEETING_ROOM_REF_ID]
      ,[WKR_ID_CREATED_TXT]
      ,[CREATED_DTTM]
      ,[WKR_ID_UPDATED_TXT]
	  ,[APPT_RESPONSE_STATUS_REF_ID]
	  ,[CANCEL_AFTER_DT]
      ,[UPDATED_DTTM]
  FROM [AION].[RESERVE_EXPRESS_RESERVATION] 
  WHERE [RESERVE_EXPRESS_RESERVATION_ID] = @identity
END
