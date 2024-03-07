using AION.Email.Engine.BusinessObjects;
using Meck.Logging;
using System;
using System.Configuration;
using System.Net.Mail;
using System.Reflection;

namespace AION.BL.Adapters
{
    public class EmailAdapter : IEmailAdapter
    {
        Logger _mLogger = new Logger();

        public Logger Logging
        {
            get { return _mLogger; }
            set { _mLogger = value; }
        }

        /*REF: ICAL references.*/
        //https://github.com/rianjs/ical.net/wiki // https://github.com/rianjs/ical.net
        //https://www.codeproject.com/Articles/17980/Adding-iCalendar-Support-to-Your-Program-Part-1
        //https://rbalajiprasad.blogspot.com/2012/11/mvc-c-create-ical-calendar-ics-feed.html
        //https://code.daypilot.org/68025/asp-net-event-calendar-export-to-icalendar
        //https://stackoverflow.com/questions/461889/sending-outlook-meeting-requests-without-outlook
        //http://www.ddaysoftware.com/Pages/Projects/DDay.iCal/
        //https://www.nylas.com/blog/calendar-events-rrules/
        //https://tools.ietf.org/html/rfc2445#section-4.8.5.4
        //https://github.com/rianjs/ical.net/wiki/Working-with-recurring-elements
        //https://stackoverflow.com/questions/461889/sending-outlook-meeting-requests-without-outlook
        /*ISSUES */
        // 1)   Organizer cannot create an event to him self. So Ical.Net cannot create an event to himself if created with Method = REQUEST. 
        // 1.1) Ref: https://stackoverflow.com/questions/15084236/icalendar-does-not-create-an-event-for-organizer

        public string MailUserName { get; set; }

        public string ErrorRecipientList { get; set; }

        public bool IsRunningFromAzure { get; set; }

        public string MandatoryEmailRecipientList { get; set; }

        public string AccelaEnvironment { get; set; }
        public EmailAdapter()
        {
            IsRunningFromAzure = bool.Parse(ConfigurationManager.AppSettings["IsRunningFromAzure"]);

            ErrorRecipientList = IsRunningFromAzure ? ConfigurationManager.AppSettings["AzureRecipientList"].ToString() : ConfigurationManager.AppSettings["LocalRecipientList"].ToString();
            MailUserName = IsRunningFromAzure ? ConfigurationManager.AppSettings["AzureMailFromUsername"].ToString() : ConfigurationManager.AppSettings["LocalMailUserName"].ToString();

            MandatoryEmailRecipientList = IsRunningFromAzure ? ConfigurationManager.AppSettings["AzureRecipientList"].ToString() : ConfigurationManager.AppSettings["LocalMailUserName"].ToString();

            AccelaEnvironment = ConfigurationManager.AppSettings["AccelaEnvironment"].ToString();

        }
        public bool SendErrorEmail(string errormessage)
        {
            bool isSuccessful = false;
            var mailMessage = GetMailMessage();

            try
            {
                mailMessage.From = new MailAddress(MailUserName);

                //loop through our list and add each of our recipients 
                string[] recipientList = MandatoryEmailRecipientList.Split(new Char[] { ';' });
                foreach (string s in recipientList)
                {
                    if (!String.IsNullOrWhiteSpace(s))
                        mailMessage.To.Add(new MailAddress(s));
                }
                mailMessage.Subject = "AION.Accela.WebApi : Exception occurred";

                mailMessage.IsBodyHtml = true;

                mailMessage.Body = errormessage;

                isSuccessful = SendEmailMessage(mailMessage);
            }

            catch (Exception ex)
            {
                string errorMessage = "SendErrorEmail Error:  " + ex.Message + "-" + DateTime.Now;
                var logging = _mLogger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                isSuccessful = false;

            }

            return isSuccessful;
        }
        public bool SendEmailMessage(System.Net.Mail.MailMessage mailMessage)
        {
            try
            {
                //replace send to with mandatory list emails
                mailMessage.To.Clear();
                string[] recipientList = MandatoryEmailRecipientList.Split(new Char[] { ';' });
                foreach (string s in recipientList)
                {
                    if (!String.IsNullOrWhiteSpace(s))
                        mailMessage.To.Add(new MailAddress(s));
                }

                return new SendEmailBO().SendEmailMessage(mailMessage);
            }
            catch (Exception ex)
            {
                string errorMessage = $"Error SendingEmail - Message: {ex.Message} From: {mailMessage.From.ToString()} To: {mailMessage.To.ToString()} InnerException: {ex.InnerException}";

                var logging = _mLogger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
        }

        public MailMessage GetMailMessage()
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(MailUserName);

            string[] recipientList = MandatoryEmailRecipientList.Split(new Char[] { ';' });


            foreach (string s in recipientList)
            {
                if (!String.IsNullOrWhiteSpace(s))
                    mailMessage.To.Add(new MailAddress(s));
            }

            mailMessage.IsBodyHtml = true;
            return mailMessage;
        }

    }

    public interface IEmailAdapter
    {
        /// <summary>
        /// Use Email engine to send email message
        /// </summary>
        /// <param name="mailMessage"></param>
        /// <returns></returns>
        bool SendEmailMessage(MailMessage mailMessage);

    }




}
