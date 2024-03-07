/****** Object:  StoredProcedure [AION].[usp_insert_aion_reserve_express_reservation_department]    Script Date: 8/21/2020 8:50:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



ALTER PROCEDURE [AION].[usp_insert_aion_reserve_express_reservation_department]
    @RESERVE_EXPRESS_RESERVATION_ID int
  , @BUSINESS_REF_ID int
  , @PLAN_REVIEWER_ID   int
  , @WKR_ID_TXT varchar(100)
  , @ReturnValue  int OUTPUT

AS

     DECLARE @error   int
	 DECLARE @ReservationDepartmentId int

	INSERT INTO RESERVE_EXPRESS_DEPARTMENT
          (
            RESERVE_EXPRESS_RESERVATION_ID
          , BUSINESS_REF_ID
          , PLAN_REVIEWER_ID
          , WKR_ID_CREATED_TXT
          , CREATED_DTTM
          , WKR_ID_UPDATED_TXT
          , UPDATED_DTTM
          )
     VALUES
          (
            @RESERVE_EXPRESS_RESERVATION_ID
		  ,	@BUSINESS_REF_ID
		  ,	@PLAN_REVIEWER_ID
		  ,	@WKR_ID_TXT
		  ,	GETDATE()
		  ,	@WKR_ID_TXT
		  ,	GETDATE()
          )

	 SET @ReservationDepartmentId=SCOPE_IDENTITY()

     SELECT @error = @@ERROR
          , @ReturnValue = SCOPE_IDENTITY()

     IF @error != 0
          RAISERROR('Error adding ReserveExpressDepartment record.', 18,1)

RETURN