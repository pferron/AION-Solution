using System.Collections.Generic;

namespace Meck.Shared.PosseToAccela
{
    public class PosseInboundUpDate
    {
        public string CustomerRecordId { get; set; }
        public string AccelRecordId { get; set; }

        public List<UpdateFields> updateValues { get; set; }

        public PosseInboundUpDate()
        {
            updateValues = new List<UpdateFields>();
        }
    }

    public class UpdateFields
    {
        public string customFieldName { get; set; }
        public string custonFieldValue { get; set; }

        public UpdateFields()
        {

        }

    }

}
