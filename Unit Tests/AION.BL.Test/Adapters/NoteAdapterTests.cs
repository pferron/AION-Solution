using AION.Manager.Adapters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AION.BL.Test.Adapters
{
    [TestClass]
    public class NoteAdapterTests
    {
        [TestMethod]
        public void ProjectsSearchSuccess()
        {
            ISearchAdapter thisengine = new SearchAdapter();
            DateTime? startDate = DateTime.Parse("2020-11-01");
            DateTime? endDate = DateTime.Parse("2021-01-01");
            string projectNumber = null;
            string projectName = null;
            string projectAddress = null;
            string customerName = null;
            string planReviewer = null;
            int? projectStatus = null;
            int? estimatorId = null;
            int? facilitatorId = null;
            int? meetingType = null;

            var result = thisengine.SearchProjects(startDate, endDate, projectNumber, projectName, projectAddress,
                customerName, planReviewer, projectStatus, estimatorId, facilitatorId, meetingType);

            Assert.IsNotNull(result);

        }
    }
}
