using AION.BL;

namespace AION.Manager.Models
{
    public class TimeAllocationTypeRef : ModelBase
    {
        public int? TimeAllocationTypeRefId { get; set; }

        public string TimeAllocationTypeRefDesc { get; set; }

        public int? EnumMappingValNbr { get; set; }

        public bool? ActiveInd { get; set; }

        public TimeAllocationType TimeAllocationType { get; set; }


    }
}