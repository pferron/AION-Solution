using AION.Manager.BusinessObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AION.BL.Test.Models
{
    [TestClass]
    public class TimeAllocationTypeRefModelBOTests
    {
        //[TestMethod]
        public void TestMethod1()
        {

            string projectScheduleTypeName = "NPA";
            string projectCategory = null;

            var result = GetAllocationType(projectScheduleTypeName, projectCategory);
            Assert.IsTrue(result.GetType() == typeof(TimeAllocationType));

        }

        TimeAllocationType GetAllocationType(string projectScheduleTypeName, string projectCategory)
        {
            if (string.IsNullOrEmpty(projectScheduleTypeName))
                return TimeAllocationType.NA;
            if (projectScheduleTypeName.ToUpper() == "NPA")
            {
                if (!string.IsNullOrWhiteSpace(projectCategory) && int.TryParse(projectCategory, out int output))
                {
                    //get the time allocation
                    return new TimeAllocationTypeRefModelBO().GetInstance(output).TimeAllocationType;
                }
                else
                {
                    //return default of NPA
                    return TimeAllocationType.NPA;
                }
            }
            else if (projectScheduleTypeName.ToUpper() == "EXP")
                return TimeAllocationType.Project_Express_Blocked;
            else if (projectScheduleTypeName.ToUpper() == "EMA")
                return TimeAllocationType.Project_Express_Reserved;
            else if (
                projectScheduleTypeName.ToUpper() == "PMA" ||
                projectScheduleTypeName.ToUpper() == "PR" ||
                projectScheduleTypeName.ToUpper() == "FIFO")
                return TimeAllocationType.Project_PlanReview;
            else if (projectScheduleTypeName.ToUpper() == "FMA")
                return TimeAllocationType.Project_Facilitator_Meeting;
            else
                return TimeAllocationType.Project;
        }
    }
}
