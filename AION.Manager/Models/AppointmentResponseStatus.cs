namespace AION.BL
{
    public class AppointmentResponseStatus : ModelBase
    {
        #region Properties

        public int? ApptResponseStatusRefId { get; set; }

        public string ApptResponseStatusDesc { get; set; }

        public int? EnumMappingValNbr { get; set; }

        public bool? ActiveInd { get; set; }

        public AppointmentResponseStatusEnum ApptResponseStatusEnum { get; set; }
        #endregion

    }
}