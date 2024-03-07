using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoInterface.Models
{
    public class RecordSearchTestDataModel
    {
        public string id { get; set; }
        public TestDataType type { get; set; }
        public string module { get; set; }
        public long trackingId { get; set; }
        public float jobValue { get; set; }
        public string createdBy { get; set; }
        public string reportedDate { get; set; }
        public string initiatedProduct { get; set; }
        public string statusDate { get; set; }
        public string recordClass { get; set; }
        public string updateDate { get; set; }
        public string serviceProviderCode { get; set; }
        public string customId { get; set; }
        public string openedDate { get; set; }
        public float undistributedCost { get; set; }
        public float totalJobCost { get; set; }
        public string value { get; set; }
        public float totalFee { get; set; }
        public float totalPay { get; set; }
        public float balance { get; set; }
        public bool booking { get; set; }
        public bool infraction { get; set; }
        public bool misdemeanor { get; set; }
        public bool offenseWitnessed { get; set; }
        public bool defendantSignature { get; set; }
        public bool publicOwned { get; set; }
    }

    public class TestDataType
    {
        public string module { get; set; }
        public string value { get; set; }
        public string type { get; set; }
        public string group { get; set; }
        public string text { get; set; }
        public string alias { get; set; }
        public string subType { get; set; }
        public string category { get; set; }
        public string id { get; set; }
    }
}

