using AION.BL.Adapters;
using AION.Manager.Engines;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Net.Mail;

namespace AION.BL.Test.Engines
{
    [TestClass]
    public class MessageTemplateEngineTests
    {
        [Ignore]
        [TestMethod]
        public void GetMessageTemplateAppointmentListReturnsList()
        {
            MessageTemplateEngine mte = new MessageTemplateEngine();
            EmailAdapter emailAdapter = new EmailAdapter();
            mte.ProjectId = 5464;
            mte.ProjectNumber = "PRE-DEMO-TOWNHOME";
            mte.ProjectScheduleTypDesc = "PR";
            mte.MeetingTypRefId = null;
            mte.MessageTemplateTypeEnum = MessageTemplateTypeEnum.Express_Decision_By_Estimator;
            mte.PlanReviewFee = "9,999,999.01";
            mte.BuildMessage();
            MailMessage mailMessage = emailAdapter.GetMailMessage();
            mailMessage.Subject = MessageTemplateTypeEnum.Express_Decision_By_Estimator.ToStringValue();
            mailMessage.Body = mte.BuildMessage();

            mailMessage.To.Add(new MailAddress("jeanine.lindsay@mecknc.gov"));

            bool success = emailAdapter.SendEmailMessage(mailMessage);

            Assert.IsNotNull(mte.MessageTemplateAppointments);
        }
        [Ignore]
        [TestMethod]
        public void PlanReviewerPhoneListReturnsList()
        {
            MessageTemplateEngine mte = new MessageTemplateEngine();
            mte.ProjectId = 5489;
            mte.ProjectNumber = "JBA-PRELIM-05-05";
            mte.ProjectScheduleTypDesc = "PMA";
            mte.MeetingTypRefId = null;
            mte.MessageTemplateTypeEnum = MessageTemplateTypeEnum.Preliminary_Meeting_AcceptReject_Email;
            mte.BuildMessage();
            Assert.IsNotNull(mte.PlanReviewerPhoneList);
        }
        [Ignore]
        [TestMethod]
        public void GetMessageTemplatePRAcceptRejectReturnsEmail()
        {
            MessageTemplateEngine mte = new MessageTemplateEngine();
            EmailAdapter emailAdapter = new EmailAdapter();
            mte.ProjectId = 5464;
            mte.ProjectNumber = "PRE-DEMO-TOWNHOME";
            mte.ProjectScheduleTypDesc = "PR";
            mte.MeetingTypRefId = null;
            mte.MessageTemplateTypeEnum = MessageTemplateTypeEnum.Plan_Review_AcceptReject_Email;
            mte.PlanReviewFee = "9,999,999.01";
            mte.BuildMessage();
            MailMessage mailMessage = emailAdapter.GetMailMessage();
            mailMessage.Subject = MessageTemplateTypeEnum.Express_Decision_By_Estimator.ToStringValue();
            mailMessage.Body = mte.BuildMessage();

            mailMessage.To.Add(new MailAddress("jeanine.lindsay@mecknc.gov"));

            bool success = emailAdapter.SendEmailMessage(mailMessage);

            Assert.IsNotNull(mte.MessageTemplateAppointments);
        }

        //[TestMethod]
        public void GetPendingEstimationMessageReturnsMessage()
        {
            List<int> projectids = new List<int>();
            projectids.Add(7953);
            projectids.Add(7952);
            projectids.Add(7942);
            projectids.Add(7926);
            projectids.Add(7923);
            foreach (int projectid in projectids)
            {

                MessageTemplateEngine messageTemplateEngine = new MessageTemplateEngine();
                messageTemplateEngine.MessageTemplateTypeEnum = MessageTemplateTypeEnum.PR_Cancellation_Message;
                messageTemplateEngine.ProjectId = projectid;
                var cancellationMessage = messageTemplateEngine.BuildMessage();

                Assert.IsNotNull(cancellationMessage);
            }
        }
    }
}
