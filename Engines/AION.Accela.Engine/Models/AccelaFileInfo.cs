
namespace AION.Accela.Engine.Models
{
    public class AccelaFileInfo
    {
        //   [{ "serviceProviderCode": "MECKLENBURG","fileName": "1.png","type": "image/png","description": "TEST DOCUMENT"}]
        public string serviceProviderCode { get; set; }
        public string fileName { get; set; }
        public string type { get; set; }
        public string description { get; set; }

        public AccelaFileInfo(string ServiceProviderCode, string InFileName, string InType, string InDescription)
        {
            serviceProviderCode = ServiceProviderCode;
            fileName = InFileName;
            type = InType;
            description = InDescription;
        }
    }
}
