using System.Collections.Generic;
//using AccelaRecords.Model;



namespace Meck.Shared.Accela
{

    public class AddressWrapperBE
    {
        public List<AddressBE>  Addresses { get; set; }

       

        public AddressWrapperBE()
        {
            Addresses = new List<AddressBE>();
        }
    }
    

    public partial class State
    {
        public string value { get; set; }
        public string text { get; set; }
    }

    public partial class StreetSuffix
    {
        public string value { get; set; }
        public string text { get; set; }
    }

    public partial class Status
    {
        public string value { get; set; }
        public string text { get; set; }
    }

    #region AddressBE
    public class AddressBE
    {
        public int sourceNumber { get; set; }
        public string houseAlphaStart { get; set; }
        public int streetStart { get; set; }
        public string auditStatus { get; set; }
        public string city { get; set; }
        public string postalCode { get; set; }
        public string streetAddress { get; set; }
        public string streetName { get; set; }
        public string addressLine1 { get; set; }
        public double xCoordinate { get; set; }
        public int id { get; set; }
        public double yCoordinate { get; set; }
        public string sourceFlag { get; set; }
        public State state { get; set; }
        public StreetSuffix streetSuffix { get; set; }
        public Status status { get; set; }
    }
    #endregion

    }
