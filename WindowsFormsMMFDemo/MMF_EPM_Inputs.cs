using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoInterface
{
    public class MMFInputs
    {
        
        public string reviewtype { get; set; }

        public string ProptypeCode { get; set; }

        public string Datereceived { get; set; }
        public string ProjectId { get; set; }
        public string OccupancyType { get; set; }

        public decimal SqrFt { get; set; }

        public decimal CostOfConstruction { get; set; }

        public int NumberofSheets { get; set; }

        public MMFInputs()
        {

        }

    }
}