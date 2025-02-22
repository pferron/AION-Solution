﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AION.Manager.Models
{
    public class ProjectOccupancyTypeNameModel
    {
        public int UserID { get; set; }
        public string ProjectOccupancyTypeName { get; set; }
        public string OccupancyTypeName { get; set; }
        public int OccupancyTypeRefID { get; set; }
        public int ProjectOccupancyTypeRefID { get; set; }
        public string AllocatedSquareFootage { get; set; }
    }
}