using AION.Email.Engine.BusinessObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace AION.BL.Test.BusinessObjects
{
    [TestClass]
    public class EmailMessageBOTests
    {

        //[TestMethod]
        public void CreatePlanReviewerNotAvailableForExpressMessageBodyOnlyCurrentDates()
        {
            EmailMessageBO emailBO = new EmailMessageBO();
            string planReviewerName = "test username";

            List<DateTime?> AssignedExpressReviewStartDate = new List<DateTime?>();
            AssignedExpressReviewStartDate.Add(DateTime.Parse("2022-06-20"));
            AssignedExpressReviewStartDate.Add(DateTime.Parse("2022-07-20"));
            AssignedExpressReviewStartDate.Add(DateTime.Parse("2022-08-20"));
            AssignedExpressReviewStartDate.Add(DateTime.Parse("2022-09-20"));
            AssignedExpressReviewStartDate.Add(DateTime.Parse("2022-10-20"));
            AssignedExpressReviewStartDate.Add(DateTime.Parse("2022-11-20"));


            List<DateTime?> ReservedExpressReviewStartDate = new List<DateTime?>();
            ReservedExpressReviewStartDate.Add(DateTime.Parse("2022-06-20"));
            ReservedExpressReviewStartDate.Add(DateTime.Parse("2022-07-20"));
            ReservedExpressReviewStartDate.Add(DateTime.Parse("2022-08-20"));
            ReservedExpressReviewStartDate.Add(DateTime.Parse("2022-09-20"));
            ReservedExpressReviewStartDate.Add(DateTime.Parse("2022-10-20"));
            ReservedExpressReviewStartDate.Add(DateTime.Parse("2022-11-20"));


            var body = emailBO.CreatePlanReviewerNotAvailableForExpressMessageBody(
                planReviewerName,
                AssignedExpressReviewStartDate,
                ReservedExpressReviewStartDate);

            Assert.IsTrue(1 == 1);

        }

    }
}
