using System;

namespace AION.BL
{
    public abstract class LModelBase
    {

        public LModelBase()
        {
            IsModelUpdated = false;
        }
        public int ID { get; set; }
        public string CreatedUser { get; set; }
        public string UpdatedUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsModelUpdated { get; set; }
    }
}
