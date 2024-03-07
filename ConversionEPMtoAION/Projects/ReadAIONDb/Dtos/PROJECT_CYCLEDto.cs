using System;

namespace ReadAIONDb.Dtos
{
    public  class PROJECT_CYCLEDto
    {
        public int PROJECT_CYCLE_ID { get; set; }
        public int PROJECT_ID { get; set; }
        public decimal CYCLE_NBR { get; set; }
        public DateTime? PLANS_READY_ON_DT { get; set; }
        public DateTime? GATE_DT { get; set; }
        public int CURRENT_CYCLE_IND { get; set; }
        public int FUTURE_CYCLE_IND { get; set; }
        public int IS_COMPLETE_IND { get; set; }
        public int IS_APRV_IND { get; set; }
    }
}
