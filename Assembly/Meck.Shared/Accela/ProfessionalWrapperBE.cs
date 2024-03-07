using System.Collections.Generic;


namespace Meck.Shared.Accela
{
    public class ProfessionalWrapperBE
    {

        public List<ProfessionalBE> Professional { get; set; }
        public ProfessionalWrapperBE()
    {

    }
  }

    //public partial class LicenseType
    //{
    //    public string value { get; set; }
    //    public string text { get; set; }
    //}
    

    public class ProfessionalBE
    {
        public string id { get; set; }
        public string fullName { get; set; }
        public string lastName { get; set; }
        public string firstName { get; set; }
        public string isPrimary { get; set; }
        public string licenseNumber { get; set; }
        public string businessName { get; set; }
        public string city { get; set; }
        public bool updateOnUI { get; set; }
        public string referenceLicenseId { get; set; }
        public string addressLine1 { get; set; }
        public RecordId recordId { get; set; }
        public LicenseType licenseType { get; set; }
        public State state { get; set; }

       

    }

 
}
