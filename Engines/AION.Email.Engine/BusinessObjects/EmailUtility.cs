#region using

using AION.Base.MSGraph;
using AION.Email.Engine;
using Newtonsoft.Json;
using System;
using System.Configuration;

#endregion

namespace Meck.Email
{
    public class EmailUtility : LoggingWrapper
    {

        #region Enums

        public enum MessageType : int
        {
            ErrorMessage = 0,
            SuccessMessage = 1,
            InformationMessage = 2,
            Formatting = 3
        }

        #region Private Variables 

        //azure settings for AION app
        private static string MSGraphEmailClientId = ConfigurationManager.AppSettings["MSGraphEmailClientId"];
        private static string MSGraphEmailClientSecret = ConfigurationManager.AppSettings["MSGraphEmailClientSecret"];
        private static string tenantid = ConfigurationManager.AppSettings["ida:TenantId"];

        //email address used for "from"
        private static string MSGraphEmailUserId = ConfigurationManager.AppSettings["MSGraphEmailUserId"];

        //environment
        private static string AIONEnvironment = ConfigurationManager.AppSettings["Environment"];


        #endregion


        #endregion

        #region Public Methods

        public static bool SendEmailMessage(System.Net.Mail.MailMessage mailMessage)
        {
            try
            {
                //if in any environment except PRD, add environment to subject
                if (AIONEnvironment != "PRD")
                {
                    mailMessage.Subject += " - " + AIONEnvironment;
                    mailMessage.Body = "<p><b><h4 style='color:red'>" + AIONEnvironment + " - Not a production environment.</h4></b></p>" + mailMessage.Body;
                }
                string jsonBody = GetMessageObjToJson(mailMessage);
                string token = GlobalGraphAPI.GetGlobalGraphAPIAdminAuthToken(MSGraphEmailClientId, MSGraphEmailClientSecret, tenantid);
                string resultstr = GlobalGraphAPI.PostWithJsonBodyAndHeaders(@"https://graph.microsoft.com/v1.0/users/" + MSGraphEmailUserId
                    + @"/sendMail", jsonBody, token).Result;

                //if this is null, then send was a success
                return string.IsNullOrWhiteSpace(resultstr);
            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }
        }
        #endregion

        #region Private Methods
        private static string GetMessageObjToJson(System.Net.Mail.MailMessage mailMessage)
        {
            var mailMessageBE = new MailMessageBE();
            mailMessageBE.Message.Subject = mailMessage.Subject;
            mailMessageBE.Message.Body.Content = mailMessage.Body;
            mailMessageBE.Message.Body.ContentType = "HTML";

            foreach (var item in mailMessage.To)
            {
                mailMessageBE.Message.ToRecipients.Add(new MessageRecipientBE() { EmailAddress = new EmailAddressBE() { Address = item.Address } });
            }


            return JsonConvert.SerializeObject(mailMessageBE);
        }

        #endregion
    }
}
