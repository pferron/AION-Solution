using AION.BL.Adapters;
using AION.BL.BusinessObjects;
using AION.BL.Common;
using AION.BL.Models;
using AION.Engine.BusinessEntities;
using AION.Manager.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AION.BL.Test.Adapters
{
    [TestClass]
    public class ExpressAdapterTests
    {
        ExpressAdapter _expressAdapter;
        List<DateTime> _holidays;

        [TestInitialize]
        public void Initialize()
        {
            _holidays = SetUpHolidays();
            _expressAdapter = new ExpressAdapter(_holidays);
        }

        //[TestMethod]
        // Use to load up express reservations manually
        public void TestProcessReservations()
        {
            bool success = _expressAdapter.ExpressReservations();
            Assert.IsTrue(success);
        }

        private List<DateTime> SetUpHolidays()
        {
            var holidays = new List<DateTime>();
            holidays.Add(new DateTime(2020, 1, 20));
            holidays.Add(new DateTime(2020, 2, 15));
            holidays.Add(new DateTime(2020, 6, 29));
            holidays.Add(new DateTime(2020, 8, 17));
            holidays.Add(new DateTime(2020, 11, 15));

            return holidays;
        }

        [TestMethod]
        public void CheckIfDateIsHoliday_WhenDateIsAHoliday()
        {
            var date = new DateTime(2020, 6, 29);
            var isHoliday = _expressAdapter.CheckIfDateIsHoliday(date);
            Assert.IsTrue(isHoliday);
        }

        [TestMethod]
        public void CheckIfDateIsHoliday_WhenDateNotAHoliday()
        {
            var date = new DateTime(2020, 3, 30);
            var isHoliday = _expressAdapter.CheckIfDateIsHoliday(date);
            Assert.IsFalse(isHoliday);
        }

        [TestMethod]
        public void GetExpressReservationListIsNotNull()
        {
            ExpressAdapter expressAdapter = new ExpressAdapter();
            var items = expressAdapter.GetExpressReservationList();
            Assert.IsTrue(1 == 1);
        }

        [TestMethod]
        public void UpdateEligibleReviewersNotInDepartment()
        {
            List<Department> departmentsA = new List<Department>()
            {
                new Department() { ID = 1 },
                new Department() { ID = 2 },
            };

            List<Department> departmentsB = new List<Department>()
            {
                new Department() { ID = 2 },
            };

            List<Reviewer> eligibleReviewers = new List<Reviewer>()
            {
                new Reviewer() { ID = 1, FirstName = "Joe", LastName = "Smith", DesignatedDepartments = departmentsA },
                new Reviewer() { ID = 2, FirstName = "Mary", LastName = "Jones", DesignatedDepartments = departmentsA },
                new Reviewer() { ID = 3, FirstName = "David", LastName = "Boyd", DesignatedDepartments = departmentsA },
                new Reviewer() { ID = 4, FirstName = "Julie", LastName = "Allen", DesignatedDepartments = departmentsB}
            };

            List<ReserveExpressPlanReviewerBE> existingReviewers = new List<ReserveExpressPlanReviewerBE>()
            {
                new ReserveExpressPlanReviewerBE() { PlanReviewerId = 1, BusinessRefId = 1, RotationNbr = 1 },
                new ReserveExpressPlanReviewerBE() { PlanReviewerId = 4, BusinessRefId = 1, RotationNbr = 2 },
                new ReserveExpressPlanReviewerBE() { PlanReviewerId = 2, BusinessRefId = 1, RotationNbr = 3 },
                new ReserveExpressPlanReviewerBE() { PlanReviewerId = 3, BusinessRefId = 1, RotationNbr = 4 },
            };

            ExpressAdapter adapter = new ExpressAdapter();

            List<ReserveExpressPlanReviewerBE> updatedReviewers = 
                adapter.UpdateExpressReviewers(eligibleReviewers, existingReviewers);

            Assert.IsTrue(!updatedReviewers.Any(x => x.PlanReviewerId == 4));

            int expectedRotationNumber = 2;
            int actualRotationNumber = updatedReviewers.First(x => x.PlanReviewerId.Value == 2).RotationNbr.Value;
            
            Assert.AreEqual(expectedRotationNumber, actualRotationNumber);

            expectedRotationNumber = 3;
            actualRotationNumber = updatedReviewers.First(x => x.PlanReviewerId.Value == 3).RotationNbr.Value;

            Assert.AreEqual(expectedRotationNumber, actualRotationNumber);
        }
    }
}
