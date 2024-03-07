namespace AION.BL
{
    public class ExternalSystem : ModelBase
    {

        public string SystemName { get; set; }
        public string Description { get; set; }

        public ExternalSystemEnum ExternalSystemEnum { get; set; }

        /// <summary>
        /// Gets or sets any reference information which can correlate this user to an external system. 
        /// This could be a correlation information which could come from external system. 
        /// This column could contain one or more values seperated by [] or comma etc eg(loginuserid, empid, emailid, enrollnumber etc)
        /// </summary>
        public string AdditionalInfo { get; set; }

    }
}

