using System.Runtime.Serialization;

namespace AION.Accela.Engine.Models
{
    [DataContract]
    public class AccelaParmsDetailBE 
    {
        //  Loadup Key Vault values 
        public string baseUrl { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string Agency { get; set; }
        public string Environment { get; set; }
        public string UserName { get; set; }
        public string password { get; set; }

        public AccelaParmsDetailBE()
        {

        }
    }
}
