using AION.Manager.Adapters;
using AION.Manager.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AION.BL.Test.Adapters
{
    [TestClass]
    public class PlanReviewAdapterTests
    {
        [TestMethod]
        public void UpsertPlanReviewSuccess()
        {
            //if (AION.BL.Test.Global.FreezeTesting == true) return;
            ////int UpsertPlanReview(PlanReview planReview)
            //PlanReviewAdapter adapter = new PlanReviewAdapter();
            //UserIdentityModelBO userIdentity = new UserIdentityModelBO();
            //List<AttendeeInfo> attendees = new List<AttendeeInfo>();
            ////add the reviewers to attendees
            //attendees.Add(new AttendeeInfo { AttendeeId = -1, DeptNameEnumId = (int)DepartmentNameEnums.Building });
            //attendees.Add(new AttendeeInfo { AttendeeId = -1, DeptNameEnumId = (int)DepartmentNameEnums.Backflow });
            //attendees.Add(new AttendeeInfo { AttendeeId = -1, DeptNameEnumId = (int)DepartmentNameEnums.EH_Day_Care });
            //attendees.Add(new AttendeeInfo { AttendeeId = -1, DeptNameEnumId = (int)DepartmentNameEnums.Electrical });
            //attendees.Add(new AttendeeInfo { AttendeeId = -1, DeptNameEnumId = (int)DepartmentNameEnums.EH_Facilities });
            //attendees.Add(new AttendeeInfo { AttendeeId = -1, DeptNameEnumId = (int)DepartmentNameEnums.Fire_Davidson });
            //attendees.Add(new AttendeeInfo { AttendeeId = -1, DeptNameEnumId = (int)DepartmentNameEnums.EH_Food });
            //attendees.Add(new AttendeeInfo { AttendeeId = -1, DeptNameEnumId = (int)DepartmentNameEnums.Mechanical });
            //attendees.Add(new AttendeeInfo { AttendeeId = -1, DeptNameEnumId = (int)DepartmentNameEnums.Plumbing });
            //attendees.Add(new AttendeeInfo { AttendeeId = -1, DeptNameEnumId = (int)DepartmentNameEnums.EH_Pool });
            //attendees.Add(new AttendeeInfo { AttendeeId = -1, DeptNameEnumId = (int)DepartmentNameEnums.Zone_Davidson });

            //List<string> excluded = new List<string>();

            //PlanReview planReview = new PlanReview
            //{
            //    ApptResponseStatusEnum = AppointmentResponseStatusEnum.Not_Scheduled,
            //    AssignedFacilitator = null,
            //    Attendees = null,
            //    AssignedReviewers = attendees,
            //    BackfEndDate = null,
            //    BackfFifo = false,
            //    BackfPlanReviewScheduleId = 0,
            //    BackfPool = false,
            //    BackfPRSUpdateDate = null,
            //    BackfStartDate = null,
            //    BuildEndDate = DateTime.Parse("08/30/2020"),
            //    BuildFifo = false,
            //    BuildPlanReviewScheduleId = 0,
            //    BuildPool = false,
            //    BuildPRSUpdateDate = null,
            //    BuildStartDate = DateTime.Parse("08/20/2020"),
            //    CreatedDate = DateTime.Now,
            //    CreatedUser = null,
            //    DaycEndDate = null,
            //    DaycFifo = false,
            //    DaycPlanReviewScheduleId = 0,
            //    DaycPool = false,
            //    DaycPRSUpdateDate = null,
            //    DaycStartDate = null,
            //    ElectEndDate = null,
            //    ElectFifo = false,
            //    ElectPlanReviewScheduleId = 0,
            //    ElectPool = false,
            //    ElectPRSUpdateDate = null,
            //    ElectStartDate = null,
            //    ExcludedPlanReviewersBackFlow = excluded,
            //    ExcludedPlanReviewersBuild = excluded,
            //    ExcludedPlanReviewersDayCare = excluded,
            //    ExcludedPlanReviewersElectric = excluded,
            //    ExcludedPlanReviewersFire = excluded,
            //    ExcludedPlanReviewersFood = excluded,
            //    ExcludedPlanReviewersLodge = excluded,
            //    ExcludedPlanReviewersMech = excluded,
            //    ExcludedPlanReviewersPlumb = excluded,
            //    ExcludedPlanReviewersPool = excluded,
            //    ExcludedPlanReviewersZone = excluded,
            //    FacilEndDate = null,
            //    FacilFifo = false,
            //    FacilPlanReviewScheduleId = 0,
            //    FacilPool = false,
            //    FacilPRSUpdateDate = null,
            //    FacilStartDate = null,
            //    FireEndDate = null,
            //    FireFifo = false,
            //    FirePlanReviewScheduleId = 0,
            //    FirePool = false,
            //    FirePRSUpdateDate = null,
            //    FireStartDate = null,
            //    FoodEndDate = null,
            //    FoodFifo = false,
            //    FoodPlanReviewScheduleId = 0,
            //    FoodPool = false,
            //    FoodPRSUpdateDate = null,
            //    FoodStartDate = null,
            //    ID = 0,
            //    InternalNotes = null,
            //    IsModelUpdated = false,
            //    IsReschedule = false,
            //    IsSubmit = false,
            //    MechaEndDate = null,
            //    MechaFifo = false,
            //    MechaPlanReviewScheduleId = 0,
            //    MechaPool = false,
            //    MechaPRSUpdateDate = null,
            //    MechaStartDate = null,
            //    NewAttendees = null,
            //    PlanReviewId = null,
            //    PlumbEndDate = null,
            //    PlumbFifo = false,
            //    PlumbPlanReviewScheduleId = 0,
            //    PlumbPool = false,
            //    PlumbPRSUpdateDate = null,
            //    PlumbStartDate = null,
            //    PoolEndDate = null,
            //    PoolFifo = false,
            //    PoolPlanReviewScheduleId = 0,
            //    PoolPool = false,
            //    PoolPRSUpdateDate = null,
            //    PoolStartDate = null,
            //    PrimaryReviewers = attendees,
            //    ProjectId = 1249,
            //    SecondaryReviewers = attendees,
            //    UpdatedDate = DateTime.Now,
            //    UpdatedUser = userIdentity.GetInstance("jeanine.lindsay@mecknc.gov", ExternalSystemEnum.Accela),
            //    ZoneEndDate = null,
            //    ZoneFifo = false,
            //    ZonePlanReviewScheduleId = 0,
            //    ZonePool = false,
            //    ZonePRSUpdateDate = null,
            //    ZoneStartDate = null

            //};

            //bool ret = adapter.UpsertPlanReview(planReview);

            //Assert.IsTrue(ret);
        }

        [TestMethod]
        public void GetPlanReviewByProjectIdSuccess()
        {
            //if (AION.BL.Test.Global.FreezeTesting == true) return;
            //string projectid = "JEANINE-TESTINSERT-08142020-DEV";

            //Assert.IsNotNull(new PlanReviewAdapter().GetPlanReviewByProjectId(projectid));
        }

        //[TestMethod]
        public void GetPlandReviewStartDateTimeByDeptReturnsNull()
        {
            //DateTime? GetPlandReviewStartDateTimeByDept(PlanReview planReview, DepartmentNameEnums dept)
            PlanReview planReview = new PlanReview();
            DepartmentNameEnums dept = DepartmentNameEnums.Mechanical;

            DateTime? ret = new PlanReviewAdapter().GetPlandReviewStartDateTimeByDept(planReview, dept);

            Assert.IsTrue(ret == null);
        }

        //[TestMethod]
        public void CreateNewCycleSchedulingNotRequiredEmailTest()
        {
            PlanReviewEmailModel model = new PlanReviewEmailModel()
            {
                AccelaProjectRefId = "MMF-000307",
                FacilitatorId = 269,
                ProjectAddress = "Project Address",
                ProjectName = "Project Name",
                RecIdTxt = "REC22-00000-00018"
            };

            bool success = new PlanReviewAdapter().CreateNewCycleSchedulingNotRequiredEmail(model);

            Assert.IsTrue(success);
        }
    }
}
