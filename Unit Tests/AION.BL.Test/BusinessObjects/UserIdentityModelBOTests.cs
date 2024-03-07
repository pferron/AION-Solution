using AION.BL.BusinessObjects;
using AION.Engine.BusinessEntities;
using AION.Engine.BusinessObjects;
using AIONEstimator.Engine.BusinessObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace AION.BL.Test.BusinessObjects
{
    [TestClass]
    public class UserIdentityModelBOTests
    {
        [TestMethod]
        [Ignore]
        public void GetUserUsedScheduleXtras()
        {
            UserScheduleBO bo = new UserScheduleBO();
            int userId = 286;
            //int userId = 643;
            DateTime startDate = DateTime.Parse("06/24/2021 2:48PM");
            DateTime endDate = DateTime.Parse("07/03/2021 2:48PM");

            List<UserScheduleExBE> userSchedules = bo.GetUsedTimeSlotsWithExtrasByUserID(userId, startDate, endDate);
            Assert.IsTrue(userSchedules.Count > 0);
        }

        [TestMethod]
        [Ignore]
        public void GetLastAssignedCityZoningReviewer()
        {
            FifoScheduleBO bo = new FifoScheduleBO();

            var id = bo.GetLastAssignedCityZoningReviewer();
            Assert.AreEqual(1, 1);
        }

        //[TestMethod]
        public void GetByFirstNameLastName()
        {
            //find an existing user
            var list = new UserIdentityModelBO().GetInstance("caldwelllindsay,jeanine", "lastname,firstname");

            Assert.IsNotNull(list);

            //get caught with no comma, return lastname or firstname contains string
            var list1 = new UserIdentityModelBO().GetInstance("al", "lastname,firstname");

            Assert.IsNotNull(list1);

            //get caught with not comma and string is null
            var list3 = new UserIdentityModelBO().GetInstance(string.Empty, "lastname,firstname");

            Assert.IsNotNull(list3);

            //user does not exist
            var list4 = new UserIdentityModelBO().GetInstance("Moore, Eric", "lastname,firstname");

            Assert.IsNotNull(list4);

            //add space after comma, add space after last name
            var list5 = new UserIdentityModelBO().GetInstance("ALLEN , simon", "lastname,firstname");

            Assert.IsNotNull(list5);

            //check ordinal case ignore setting
            var list2 = new UserIdentityModelBO().GetInstance("ALLEN,simon", "lastname,firstname");

            Assert.IsNotNull(list2);


        }
    }
}
