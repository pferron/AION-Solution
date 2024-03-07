using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AION.BL
{
    public class HolidayConfig
    {

       
       public int HolidayConfigId { get; set; }

     
        public string HolidayNm { get; set; }

     
        public DateTime HolidayDate { get; set; }

       
        public bool HolidayAnnualRecurInd { get; set; }

        public bool IsActive { get; set; }
    }
}
