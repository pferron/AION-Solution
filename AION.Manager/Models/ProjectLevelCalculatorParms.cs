using AION.BL;
using Meck.Shared.Accela;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AION.Manager.Models
{
    public class ProjectLevelCalculatorParms
    {
        public PropertyTypeEnums PropertyType { get; set; }
        public int? SqrFootage { get; set; }
        public bool? IsHighRise { get; set; }
        public int? NumberOfStories { get; set; }
        public string OccupancyType { get; set; }

    }
}