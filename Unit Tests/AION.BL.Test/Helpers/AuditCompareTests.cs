using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AION.BL.Test.Helpers
{
    [TestClass]
    public class AuditCompareTests
    {
        //[TestMethod]
        public void TestMethod1()
        {
            DateTime ndt = DateTime.Now;
            var b = CompareReviewer(string.Empty, 0);
            var d = SetNullAuditDateTime((DateTime?)null);
            var dt = DateTime.Compare(SetNullAuditDateTime((DateTime?)null), ndt);

            Assert.IsTrue(b);
        }


        private static bool CompareReviewer(string reviewerId, int auditReviewerId)
        {
            int scheduledReviewerId = 0;
            if (!string.IsNullOrWhiteSpace(reviewerId))
            {
                scheduledReviewerId = int.Parse(reviewerId);
            }
            else
            {
                //string empty or null will never equal int which defaults to 0
                return false;
            }
            return auditReviewerId == scheduledReviewerId;
        }
        private static DateTime SetNullAuditDateTime(DateTime? dateTime)
        {
            if (dateTime.HasValue)
            {
                return dateTime.Value;
            }
            else
            {
                return DateTime.MinValue;
            }
        }
    }
}
