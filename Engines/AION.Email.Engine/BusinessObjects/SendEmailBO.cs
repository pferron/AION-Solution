using AION.Email.Engine.BusinessEntities;
using Meck.Email;
using System;
using System.Net.Mail;
using System.Reflection;

namespace AION.Email.Engine.BusinessObjects
{
    public class SendEmailBO : LoggingWrapper
    {
        SendEmailBE _sendEmailBE;
        EmailMessageBE _emailMessageBE;
        public SendEmailBO()
        {
            _emailMessageBE = new EmailMessageBE();
            _sendEmailBE = new SendEmailBE();

        }
        public bool SendEmailMessage(MailMessage mailMessage)
        {
            try
            {
                return EmailUtility.SendEmailMessage(mailMessage);
            }
            catch (Exception ex)
            {
                string errorMessage = "Email.Utility.SendMailMessage Error:  " + ex.Message + "-" + DateTime.Now;
                staticBLLogMessage(MethodBase.GetCurrentMethod(), errorMessage, ex);

                throw ex;
            }
        }


    }
}
