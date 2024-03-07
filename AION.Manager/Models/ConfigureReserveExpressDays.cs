using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AION.BL;

namespace AION.Manager.Models
{
    public class ConfigureReserveExpressDays:ModelBase
    {
        public int Id { get; set; }

        public string Day { get; set; }

        public bool ActiveInd { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}