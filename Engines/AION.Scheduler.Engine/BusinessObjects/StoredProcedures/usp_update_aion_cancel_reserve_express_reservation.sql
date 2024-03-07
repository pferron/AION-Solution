/****** Object:  StoredProcedure [AION].[usp_update_aion_cancel_reserve_express_reservation]    Script Date: 1/7/2021 10:52:39 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:      Allen, Janessa
-- Create Date: September 22, 2020
-- Description: Nightly procedure to cancel Express 
--              Reservations within 5 days of the scheduled date
--
-- =============================================

ALTER PROCEDURE [AION].[usp_update_aion_cancel_reserve_express_reservation]
@cancelThroughDate datetime,
@ReturnValue                                                 int OUTPUT
AS
	 UPDATE AION.RESERVE_EXPRESS_RESERVATION
		SET APPT_RESPONSE_STATUS_REF_ID = (SELECT APPT_RESPONSE_STATUS_REF_ID FROM AION.APPOINTMENT_RESPONSE_STATUS_REF WHERE APPT_RESPONSE_STATUS_DESC='Cancelled')
		WHERE RESERVE_EXPRESS_RESERVATION_ID IN(
		SELECT RESERVE_EXPRESS_RESERVATION_ID FROM AION.RESERVE_EXPRESS_RESERVATION RER WHERE APPT_RESPONSE_STATUS_REF_ID IN
		(SELECT APPT_RESPONSE_STATUS_REF_ID FROM AION.APPOINTMENT_RESPONSE_STATUS_REF WHERE APPT_RESPONSE_STATUS_DESC='Scheduled')
		AND  @cancelThroughDate >= RER.RESERVE_EXPRESS_DT)


     SELECT @ReturnValue = @@ROWCOUNT
   
RETURN
}
}
