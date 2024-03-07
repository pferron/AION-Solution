using Newtonsoft.Json;
using System.Collections.Generic;


namespace Meck.Shared.PosseToAccela
{
    // ContactCustomFormBO myDeserializedClass = JsonConvert.DeserializeObject(myJsonResponse); 
    public class Result
    {

        [JsonProperty("RequestorAssociationOther")]
        public string RequestorAssociationOther { get; set; }

        [JsonProperty("Notify")]
        public string Notify { get; set; }

        [JsonProperty("RequestorAssociation")]
        public string RequestorAssociation { get; set; }

        [JsonProperty("Grade")]
        public string Grade { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class ContactCustomFormBO
    {

        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("result")]
        public IList<Result> Result { get; set; }
    }

}



