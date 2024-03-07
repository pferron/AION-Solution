namespace ReadAIONDb.Enums
{
    /// <summary>
    /// Used in the table ProjectSchedule to determine what type of appointment is required
    /// The text of this enum is the field "PMA", "NPA", etc
    /// </summary>
    public enum ProjectScheduleRefEnum
    {
        //Non project appointment
        NPA,
        //Preliminary Meeting appointment
        PMA,
        //Express Reservations
        EXP,
        //Plan Review
        PR,
        //Express Meeting Appointment
        EMA,
        //Facilitator Meeting Appointment
        FMA,
        //Fifo
        FIFO
    }


    //Public Shared Function ToReviewTypeEnum(ByVal reviewText As String) As EPMCommon.ReviewType

    //   Select Case reviewText
    //       Case "OS"

    //           Return EPMCommon.ReviewType.OnSchedule

    //       Case "ER"

    //           Return EPMCommon.ReviewType.ExpressReview

    //       Case "MP"

    //           Return EPMCommon.ReviewType.MegaProject

    //       Case "PS"

    //           Return EPMCommon.ReviewType.PublicSchool

    //       Case "RT"

    //           Return EPMCommon.ReviewType.RTAP

    //       Case "PL"

    //           Return EPMCommon.ReviewType.PrelimReview

    //       Case "WL"

    //           Return EPMCommon.ReviewType.WLRProjectReview

    //       Case Else
    //           Throw New ApplicationException("Unsupported review type: " + reviewText)
    //    End Select
    //End Function
}
