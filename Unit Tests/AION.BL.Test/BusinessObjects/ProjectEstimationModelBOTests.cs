using AION.BL.Models;
using Meck.Shared.MeckDataMapping;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AION.BL.Test.BusinessObjects
{
    [TestClass]
    public class ProjectEstimationModelBOTests
    {
        ProjectEstimation _projectEstimation = new ProjectEstimation();
        Meck.Shared.MeckDataMapping.AccelaProjectModel _accelaProjectModel = new Meck.Shared.MeckDataMapping.AccelaProjectModel();

        public void CreateAccelaProjectModel()
        {
            _accelaProjectModel = new AccelaProjectModel
            {

            };
        }
        //[TestMethod]
        public void CheckTotalJobCost()
        {

        }

    }
}
