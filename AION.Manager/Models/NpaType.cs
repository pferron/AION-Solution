namespace AION.BL.Models
{
    public class NpaType : ModelBase
    {
        public string AppointmentTypeName { get; set; }

        public bool IsActive { get; set; }

        public TimeAllocationType TimeAllocationType { get; set; }

        public int TimeAllocationTypeRefId { get; set; }

    }
}
