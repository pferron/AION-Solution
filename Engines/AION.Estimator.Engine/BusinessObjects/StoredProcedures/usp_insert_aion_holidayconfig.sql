/****** Object:  StoredProcedure [AION].[usp_insert_aion_holidayconfig]    Script Date: 4/30/2021 3:12:06 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/***********************************************************************************************************************
* Object:	usp_insert_aion_holidayconfig
* Description:	Inserts holiday config record.
*
* Returns:      Identity column of new record.
* Version:      1.0
* Created by:   gayatri
* Created:      02/18/20
************************************************************************************************************************
* Change History: Date, Name, Description
* 02/18/2020    gayatri  
* 04/30/2021    jallen  Add active indicatior
***********************************************************************************************************************/

ALTER PROCEDURE [AION].[usp_insert_aion_holidayconfig]
    @HOLIDAY_NM varchar(255)
  , @HOLIDAY_DT datetime
  , @HOLIDAY_ANNUAL_RECUR_IND bit
  , @ACTIVE_IND bit
  , @ReturnValue                                                 int OUTPUT

AS

     DECLARE @error   int
	 IF(@HOLIDAY_ANNUAL_RECUR_IND = 1)
	 BEGIN
     INSERT INTO [AION].[HOLIDAY_CONFIGURATION](
		    HOLIDAY_NM,HOLIDAY_DT,HOLIDAY_ANNUAL_RECUR_IND,ACTIVE_IND)
     VALUES
          (@HOLIDAY_NM, @HOLIDAY_DT, @HOLIDAY_ANNUAL_RECUR_IND,@ACTIVE_IND)
     INSERT INTO [AION].[HOLIDAY_CONFIGURATION](
		    HOLIDAY_NM,HOLIDAY_DT,HOLIDAY_ANNUAL_RECUR_IND,ACTIVE_IND)
     VALUES
          (@HOLIDAY_NM,  DATEADD(year, 1, @HOLIDAY_DT) , @HOLIDAY_ANNUAL_RECUR_IND,@ACTIVE_IND)

	END
	ELSE
	BEGIN
	 INSERT INTO [AION].[HOLIDAY_CONFIGURATION](
		    HOLIDAY_NM,HOLIDAY_DT,HOLIDAY_ANNUAL_RECUR_IND,ACTIVE_IND)
     VALUES
          (@HOLIDAY_NM, @HOLIDAY_DT, @HOLIDAY_ANNUAL_RECUR_IND,@ACTIVE_IND)
	END
     SELECT @error = @@ERROR
          , @ReturnValue = SCOPE_IDENTITY()

     IF @error != 0
          RAISERROR('Error adding record.', 18,1)

RETURN
