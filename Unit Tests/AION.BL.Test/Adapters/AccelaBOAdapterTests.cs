using AION.BL.Models;
using AION.Manager.Adapters;
using AION.Manager.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AION.BL.Test.Adapters
{
    [TestClass]
    public class AccelaBOAdapterTests
    {
        [DataTestMethod]
        [DataRow(PropertyTypeEnums.County_Fire_Shop_Drawings, "Fire Unincorporated Mecklenburg County", "Commercial County Fire")]
        [DataRow(PropertyTypeEnums.County_Fire_Shop_Drawings, "Fire_Davidson", "Commercial County Fire")]
        [DataRow(PropertyTypeEnums.County_Fire_Shop_Drawings, "Fire_Cornelius", "Commercial County Fire")]
        [DataRow(PropertyTypeEnums.County_Fire_Shop_Drawings, "Fire_Pineville", "Commercial County Fire")]
        [DataRow(PropertyTypeEnums.County_Fire_Shop_Drawings, "Fire_Matthews", "Commercial County Fire")]
        [DataRow(PropertyTypeEnums.County_Fire_Shop_Drawings, "Fire_Huntersville", "Commercial County Fire")]
        [DataRow(PropertyTypeEnums.Mega_Multi_Family, "Building", "Commercial Building")]
        [DataRow(PropertyTypeEnums.FIFO_Single_Family_Homes, "Building", "Residential Building")]
        [DataRow(PropertyTypeEnums.FIFO_Small_Commercial, "Mechanical", "Commercial Mechanical")]
        [DataRow(PropertyTypeEnums.Special_Projects_Team, "Zone_Davidson", "Commercial County Zoning")]
        public void TestTableValueTasksMapAionToAccela_PlanReviewTaskName(PropertyTypeEnums propertyType, string businessName, string expectedTaskName)
        {
            PlanReview planReview = new PlanReview();
            ProjectEstimation project = new ProjectEstimation() { AccelaPropertyType = propertyType };

            Tuple<string, string, string, string> actual = new AccelaBOAdapter().TableValueTasksMapAionToAccela(planReview, project, businessName);
            Assert.AreEqual(expectedTaskName, actual.Item4);
        }

        [TestMethod]
        [Ignore]// need to add initialization to test schedule plan review method below
        public void TestSchedulePlanReview()
        {
            List<PlanReview> planReviews = new PlanReviewAdapter().GetPlanReviewsByProjectId(8361);
            PlanReview planReview = planReviews.FirstOrDefault(x => x.IsCurrentCycle == true);
            bool success = new AccelaBOAdapter().SchedulePlanReview(planReview);
            Assert.IsTrue(success);
        }

        //[TestMethod]
        public void ConvertToIntHrsTest()
        {
            decimal hrs = 0.5M;

            var chng = Convert.ToInt32(hrs);

            Assert.IsTrue(chng > 0);

        }
    }
}
