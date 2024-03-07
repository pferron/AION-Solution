using AION.BL.Adapters;
using AION.BL.BusinessObjects;
using AION.BL.Models;
using AION.Manager.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace AION.BL.Test.Adapters
{
    [TestClass]
    public class SchedulerAdapterTests
    {
        ISchedulerAdapter _adapter = new SchedulerAdapter();

        //[TestMethod]
        //[Ignore]
        //public void GetSchedMeetingListbyProjectIdReturnsList()
        //{
        //    if (AION.BL.Test.Global.FreezeTesting == true) return;
        //    string accelaprojectid = "";
        //    Assert.IsNotNull(_adapter.GetSchedMeetingListbyProjectId(accelaprojectid));
        //}

        [TestMethod]
        [Ignore]
        public void GetSchedulingDashboardListReturnsList()
        {
            Assert.IsNotNull(new SchedulerAdapter().GetSchedulingDashboardList());
        }

        //[TestMethod]
        //public void GetPMAByProjectIDReturnsObject()
        //{
        //    int projectid = 0;
        //    Assert.IsNotNull(_adapter.GetPMAByProjectID(projectid));
        //}

        //[TestMethod]
        //public void GetPMAAttendeesByApptIdReturnsList()
        //{
        //    int apptid = 0;
        //    Assert.IsNotNull(_adapter.GetPMAAttendeesByApptId(apptid, null));
        //}

        [TestMethod]

        public void GetInternalMeetingsByUserIDReturnsList()
        {
            int userid = 319;
            Assert.IsNotNull(_adapter.GetInternalMeetings(userid));
        }

        [TestMethod]
        public void GetMeetingsByUserIDReturnsList()
        {
            int userid = 0;
            Assert.IsNotNull(_adapter.GetMeetingsByUserID(userid));
        }

        [TestMethod]
        public void GetAllPlanReviewerHoursReturnsList()
        {
            Assert.IsNotNull(_adapter.GetAllPlanReviewerHours());
        }

        //[TestMethod]
        public void CheckIfAllCycleReReviewHoursAreUnderOneHour()
        {
            DepartmentModelBO departmentBO = new DepartmentModelBO();
            List<ReReview> reReviewsUnderOneHour = new List<ReReview>();
            reReviewsUnderOneHour.Add(new ReReview()
            {
                DepartmentId = departmentBO.GetInstance(DepartmentNameEnums.Building).ID,
                EstimatedReReviewTime = (decimal)1.0,
                ProposedReviewerName = "Reviewer01@accela.com"
            });

            reReviewsUnderOneHour.Add(new ReReview()
            {
                DepartmentId = departmentBO.GetInstance(DepartmentNameEnums.Electrical).ID,
                EstimatedReReviewTime = (decimal).5,
                ProposedReviewerName = "Reviewer01@accela.com"
            });

            reReviewsUnderOneHour.Add(new ReReview()
            {
                DepartmentId = departmentBO.GetInstance(DepartmentNameEnums.Plumbing).ID,
                EstimatedReReviewTime = (decimal)1.0,
                ProposedReviewerName = "Reviewer01@accela.com"
            });

            bool expected = true;

            SchedulerAdapter adapter = new SchedulerAdapter();
            bool actual = adapter.AreAllCycleHoursUnderOneHour(reReviewsUnderOneHour);

            Assert.AreEqual(expected, actual);

            List<ReReview> reReviewsNotAllUnderOneHour = new List<ReReview>();
            reReviewsNotAllUnderOneHour.Add(new ReReview()
            {
                DepartmentId = departmentBO.GetInstance(DepartmentNameEnums.Building).ID,
                EstimatedReReviewTime = (decimal)1.0,
                ProposedReviewerName = "Reviewer01@accela.com"
            });

            reReviewsNotAllUnderOneHour.Add(new ReReview()
            {
                DepartmentId = departmentBO.GetInstance(DepartmentNameEnums.Electrical).ID,
                EstimatedReReviewTime = (decimal).5,
                ProposedReviewerName = "Reviewer01@accela.com"
            });

            reReviewsNotAllUnderOneHour.Add(new ReReview()
            {
                DepartmentId = departmentBO.GetInstance(DepartmentNameEnums.Plumbing).ID,
                EstimatedReReviewTime = (decimal)2.0,
                ProposedReviewerName = "Reviewer01@accela.com"
            });

            expected = false;

            actual = adapter.AreAllCycleHoursUnderOneHour(reReviewsNotAllUnderOneHour);

            Assert.AreEqual(expected, actual);
        }

        //[TestMethod]
        public void GetPrelimAutoScheduledDataReturnsValue()
        {
            AutoScheduledPrelimParams model = new AutoScheduledPrelimParams();
            model.SuggestedDate1 = DateTime.Now.AddDays(1);
            model.SuggestedDate2 = DateTime.Now.AddDays(2);
            model.AccelaProjectIDRef = "MMF-PM-000084";
            SchedulerAdapter thisengine = new SchedulerAdapter();
            var result = thisengine.GetPrelimAutoScheduledData(model);
            Assert.IsNotNull(result);
        }
    }
}
