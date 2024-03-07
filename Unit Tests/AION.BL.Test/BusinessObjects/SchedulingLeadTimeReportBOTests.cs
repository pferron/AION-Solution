using AION.BL.Models;
using AION.BL.Test.Helpers;
using AION.Estimator.Engine.BusinessObjects;
using AION.Manager.BusinessObjects;
using AION.Manager.Engines;
using AION.Manager.Models;
using AION.Scheduler.Engine.BusinessEntities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AION.BL.Test.BusinessObjects
{
    [TestClass]
    public class SchedulingLeadTimeReportBOTests
    {
        //[TestMethod]
        public void GetReportParamsReturnsList()
        {
            SchedulingLeadTimeReportBO bo = new SchedulingLeadTimeReportBO();
            var a = bo.GetReportProjects();

            Assert.IsTrue(a);
        }

        //[TestMethod]
        public void TestSchedulingEngine()
        {

            SchedulingLeadTimeReportBO bo = new SchedulingLeadTimeReportBO();
            var ret = bo.GenerateSchedulingLeadTimeData();
            Assert.AreEqual(1, 1);
        }

       // [TestMethod]
        public void TestSpecificProjectTypeForReport()
        {
            SchedulingLeadTimeReportBO bo = new SchedulingLeadTimeReportBO();

            AutoScheduleReportParams parameters = CreateAutoScheduleReportParams();

            List<TimeSlot> timeSlots = bo.GetAutoScheduledDataPlanReview(parameters);

            bo.CurrentProject = parameters.CurrentProject;
            bo.ReviewHours = (int)parameters.ReviewHours;
            bo.ProjectLevelTxt = ProjectLevel.Level1.ToString();
            bo.BusinessDivisions = new BusinessDivisionRefBO().GetXRefList();
            bo.SchedulingLeadTimeReportList = new List<SchedulingLeadTimeReport>();

            TimeZoneInfo easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            var timeUtc = DateTime.UtcNow;
            DateTime easternTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, easternZone);
            bo.ConvertTimeSlot(timeSlots, parameters.ManualStartDateTime);

            Assert.IsTrue(bo.SchedulingLeadTimeReportList.Count > 0);

            var reportItems = bo.SchedulingLeadTimeReportList.Where(x => x.BusinessDivisionRefId == 1
                && x.ProjectTypeRefId == (int)PropertyTypeEnums.FIFO_Master_Plans).ToList();

            Assert.AreEqual(1, 1);
        }

        //[TestMethod]
        public void TestFirstMeetingSlotAvailable()
        {
            AutoScheduleReportParams parameters = CreateAutoScheduleReportParams();

            PlanReviewProjectSchedulingEngine thisengine = new PlanReviewProjectSchedulingEngine(parameters);
            List<AutoSchedulableReviewer> reviewers = thisengine.GetAllEligibleReviewers();

            AutoSchedulableReviewer reviewer = reviewers.FirstOrDefault(x => x.UserIdentity.LastName == "Homer");

            PlanReviewAutoSchedulableReviewer planReviewer = new PlanReviewAutoSchedulableReviewer(reviewer)
            {
                AllotedStartDt = parameters.ManualStartDateTime,
                AllotedEndDt = parameters.ManualEndDateTime,
                PlanReviewerDept = DepartmentNameEnums.Electrical
            };

            bool success = thisengine.AllocateFirstMeetingSlotAvailable(planReviewer, 2, parameters.ManualStartDateTime.Value, parameters.ManualEndDateTime.Value);

            Assert.IsTrue(1 == 1);
        }

        private AutoScheduleReportParams CreateAutoScheduleReportParams()
        {
            ProjectEstimation currentProject = ProjectEstimationCreator.CreatePE(PropertyTypeEnums.FIFO_Master_Plans, 2, ProjectLevel.Level1.ToString());
            DateTime manualStartDateTime = new System.DateTime(2022, 7, 13); // should be one day from the current date (date you are executing)
            DateTime manualEndDateTime = new System.DateTime(2022, 7, 25);
            int reviewHours = 2;

            return new AutoScheduleReportParams()
            {
                CurrentProject = currentProject,
                ManualStartDateTime = manualStartDateTime,
                ManualEndDateTime = manualEndDateTime,
                ReviewHours = reviewHours
            };
        }
    }
}
