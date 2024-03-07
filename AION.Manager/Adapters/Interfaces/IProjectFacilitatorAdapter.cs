using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AION.BL.Models;



namespace AION.BL.Adapters
{
    public interface IProjectFacilitatorAdapter
    {
        bool GetAssignedFacilitator(ProjectEstimation model);
        bool GetFacilitator(Project model);
       
      
    }
}
