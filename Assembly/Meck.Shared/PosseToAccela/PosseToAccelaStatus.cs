namespace Meck.Shared.PosseToAccela
{
    public class PosseToAccelaStatus
    {
        public StandardTextValueObject statusObj { get; set; }

        public StandardTextValueObject  newAltId { get; set; }

        public string Errors { get; set;  }

        public PosseToAccelaStatus(string status, string newPosseProjectId, string  PosseErrors) 
        {
            statusObj = new StandardTextValueObject("StatusValue", status); 

            newAltId = new StandardTextValueObject("altId",newPosseProjectId);

            Errors = PosseErrors; 

        }

}

    public class StandardTextValueObject
    {
       public string text { get; set; }
       public string Value { get; set; }

       public StandardTextValueObject(string textValue, string valueValue)
       {
           text = textValue;
           Value = valueValue; 
       }

    }
}
