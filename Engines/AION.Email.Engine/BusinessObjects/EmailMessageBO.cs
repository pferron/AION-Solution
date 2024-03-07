using AION.Email.Engine.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AION.Email.Engine.BusinessObjects
{
    public class EmailMessageBO
    {
        EmailMessageBE _emailmessagebe;
        public EmailMessageBO()
        {
            _emailmessagebe = new EmailMessageBE();
        }
        public string CreateNAEmailMessageBody(string projectId, string projectName, string projectAddress, string baseurl = "https://localhost:44392/")
        {

            StringBuilder sb = new StringBuilder(5000);

            sb.Append("</br>");
            sb.Append("<p>Project Dashboard: <a href='");
            sb.Append(baseurl);
            sb.Append("Customer/ProjectDetail?projectid=");
            sb.Append(projectId);
            sb.Append("'>");
            sb.Append(baseurl);
            sb.Append("Customer/ProjectDetail?projectid=");
            sb.Append(projectId);
            sb.Append("</a></p>");
            sb.Append("<p>");
            sb.AppendLine("All Trade/ Agencies Estimated Project [");
            sb.Append(projectId);
            sb.Append("]");
            sb.Append(" as NA");
            sb.Append("</p>");
            sb.Append("<p>");
            sb.Append("</br>");
            sb.AppendLine("\n");
            sb.AppendLine("Project #:");
            sb.Append(projectId);
            sb.Append("(" + projectName + ")");
            sb.Append("</p>");
            sb.Append("<p>");
            sb.AppendLine("\n");
            sb.AppendLine("Address:");
            sb.Append(projectAddress);

            //footer
            sb.AppendLine(_emailmessagebe.GetHtmlParagraphHeader());
            sb.Append("<b><u>PLEASE DO NOT REPLY TO THIS EMAIL<u></b>");
            sb.Append("</p>");

            return sb.ToString();
        }
        public string CreateNPAMessageBody(string npaName, string npaType, string npaDate, string npaHours, string meetingRoom)
        {

            StringBuilder sb = new StringBuilder(5000);

            sb.Append("</br>");
            sb.Append("<p>NPA Name: ");
            sb.Append(npaName);
            sb.Append("</p>");
            sb.Append("<p>");
            sb.Append("NPA Type: ");
            sb.Append(npaType);
            sb.Append("</p>");
            sb.Append("<p>");
            sb.Append("NPA Date: ");
            sb.Append(npaDate);
            sb.Append("</p>");
            sb.Append("<p>");
            sb.Append("NPA Hours: ");
            sb.Append(npaHours);
            sb.Append("</p>");
            sb.Append("<p>");
            if (!string.IsNullOrWhiteSpace(meetingRoom))
            {
                sb.Append("Meeting Room: ");
                sb.Append(meetingRoom);
            }
            sb.Append("</p>");

            //footer
            sb.Append(_emailmessagebe.GetHtmlParagraphHeader());
            sb.Append("<b><u>PLEASE DO NOT REPLY TO THIS EMAIL<u></b>");
            sb.Append("</p>");

            return sb.ToString();
        }

        public string CancelPrelimMeetingScheduledMessageBody(string projectId, string projectName, string projectAddress, DateTime meetingDate)
        {
            StringBuilder sb = new StringBuilder(5000);

            sb.Append("</br>");
            sb.Append("<p>The plan review tentatively scheduled date has been cancelled - either rejected or cancelled due to not being accepted within 48 hours. Please sign into your dashboard application and enter a new plans ready-on date so we can reschedule your prelim meeting.</p>");
            sb.Append("<p><b>The tentative plan review date has been automatically cancelled.</b></p>");
            sb.Append("</br>");
            sb.Append("<p>");
            sb.Append("Project #:");
            sb.Append(projectId);
            sb.Append("(" + projectName + ")");
            sb.Append("</p>");
            sb.Append("<p>");
            sb.Append("Address");
            sb.Append(projectAddress);
            sb.Append("</p>");
            sb.Append("<p>");
            sb.Append("Meeting Date & Time");
            sb.Append(meetingDate);
            sb.Append("</p>");

            sb.Append("</p>");
            sb.Append("</br>");
            sb.Append("<p>Please note the Plan Review Start Date is the date of the actual review. The plans will need to be submitted to the department two (2) business days prior to the Plan Review Start Date. Please refer to the Schedule PDF found on the dashboard for the gate submittal date.</p>");
            sb.Append("<p>If you have any questions, please contact the Mecklenburg County project coordinator listed below:</p>");

            sb.AppendLine(_emailmessagebe.GetHtmlParagraphHeader());
            sb.Append("<b><u>PLEASE DO NOT REPLY TO THIS EMAIL<u></b>");
            sb.Append("</p>");

            return sb.ToString();
        }
        public string CreatePMACancelledMessageBody(string projectnumber, string projectaddress, string apptDateTime, string projectcoordname, string projectcoordphone, string projectcoordemail)
        {

            //New email, using string interpolation
            return "<p> The Preliminary Meeting tentatively scheduled date has been cancelled - either rejected or cancelled due to not being accepted within 48 hours. " +
               "Please sign into your dashboard application and enter a new plans ready-on date so we can reschedule your plan view.</p>" +
               "<p>The tentative preliminary meeting date has been automatically cancelled.</p>" +
               $"<p>Project #: {projectnumber}<br/>" +
               $"Address: {projectaddress}<br/>" +
               $"Preliminary meeting date: {apptDateTime}</p>" +
               "<p>Please note the Plan Review Start Date is the date of the actual review. The plans will need to be submitted to the department two (2) business days prior to the Plan Review Start Date. " +
               "Please refer to the Schedule PDF found on the dashboard for the gate submittal date.</p>" +
               "<p>If you have any questions, please contact the Mecklenburg County project coordinator listed below:</p>" +
               $"<p>{projectcoordname}<br/>" +
               $"Bus Ph: {projectcoordphone}<br/>" +
               $"{projectcoordemail}</p><br/>" +
               "<p>PLEASE DO NOT REPLY TO THIS EMAIL.</p>";

        }

        public string CreateReserveExpressReservationMessageBody()
        {
            //placeholder
            StringBuilder sb = new StringBuilder(5000);

            return sb.ToString();
        }

        public string CreateHolidayConfigMessageBody()
        {
            //placeholder
            StringBuilder sb = new StringBuilder(5000);

            return sb.ToString();
        }

        //For both Express Plan review and Schedule Plan Review
        public string CancelPlanReviewScheduledMessageBody(string projectId, string projectName, string projectAddress, string meetingDate, string projectcoordname, string projectcoordphone, string projectcoordemail)
        {
            StringBuilder sb = new StringBuilder(5000);

            sb.Append("</br>");
            sb.Append("<p>The plan review tentatively scheduled date has been cancelled - either rejected or cancelled due to not being accepted within 48 hours. Please sign into your dashboard application and enter a new plans ready-on date so we can reschedule your prelim meeting.</p>");
            sb.Append("<p><b>The tentative plan review date has been automatically cancelled.</b></p>");
            sb.Append("</br>");
            sb.Append("<p>");
            sb.Append("Project #:");
            sb.Append(projectId);
            sb.Append("(" + projectName + ")");
            sb.Append("</p>");
            sb.Append("<p>");
            sb.Append("Address");
            sb.Append(projectAddress);
            sb.Append("</p>");
            sb.Append("<p>");
            sb.Append("Plan Review Start Date:");
            sb.Append(meetingDate);
            sb.Append("</p>");

            sb.Append("</p>");
            sb.Append("</br>");
            sb.Append("<p>Please note the Plan Review Start Date is the date of the actual review. The plans will need to be submitted to the department two (2) business days prior to the Plan Review Start Date.</p>");
            sb.Append("<p>If you have any questions, please contact the Mecklenburg County project coordinator listed below:</p>");
            sb.Append("<p>");
            sb.Append(projectcoordname);
            sb.Append("</p>");
            sb.Append("<p> Bus Ph: ");
            sb.Append(projectcoordphone);
            sb.Append("</p>");
            sb.Append("<p>");
            sb.Append(projectcoordemail);
            sb.Append("</p>");
            //leave a blank row
            sb.Append("<p>");
            sb.Append("</p>");
            sb.AppendLine(_emailmessagebe.GetHtmlParagraphHeader());
            sb.Append("<b><u>PLEASE DO NOT REPLY TO THIS EMAIL<u></b>");
            sb.Append("</p>");

            return sb.ToString();
        }

        public string CreateGateDateRescheduledMessageBody(string projectId, string projectName, string projectAddress, string planReviewStartDate, string gateDate, string planReviewFee, string projectcoordname, string projectcoordphone, string projectcoordemail)
        {
            StringBuilder sb = new StringBuilder(5000);

            sb.Append("</br>");
            sb.Append("<p>The plan review has been tentatively rescheduled as per the details given below. Please sign into your dashboard application, review the gate instructions, plan review fee and cancellation policy and either accept or reject the plan review dates.");
            sb.Append("</p>");
            sb.Append("<p><b>");
            sb.Append("If we do not hear from you by (" + gateDate + "), the plan review will automatically be rescheduled to the original scheduled review date.");
            sb.Append("</b></p>");
            sb.Append("<p><b>");
            sb.Append("Project #:");
            sb.Append(projectId);
            sb.Append("(" + projectName + ")");
            sb.Append("</b></p>");
            sb.Append("<p><b>");
            sb.Append("Project Address: ");
            sb.Append(projectAddress);
            sb.Append("</b></p>");
            sb.Append("<p><b>");
            sb.Append("Plan Review Start Date:");
            sb.Append(planReviewStartDate);
            sb.Append("</b></p>");
            sb.Append("<p>Please note the Plan Review Start Date is the date of the actual review.  The plans will need to be submitted to the department by (" + gateDate + ").");
            sb.Append("</p>");
            //leave a blank row
            sb.Append("<p>");
            sb.Append("</p>");

            sb.Append("<p><b>");
            sb.Append("Estimated Plan Review / Permit Fee: $");
            sb.Append(planReviewFee);
            sb.Append("</b></p>");

            //leave a blank row
            sb.Append("<p>");
            sb.Append("</p>");

            sb.Append("<p>");
            sb.Append("If you have any questions, please contact the Mecklenburg County project coordinator listed below:");
            sb.Append("</p>");
            sb.Append("<p>");
            sb.Append(projectcoordname);
            sb.Append("</p>");
            sb.Append("<p> Bus Ph: ");
            sb.Append(projectcoordphone);
            sb.Append("</p>");
            sb.Append("<p>");
            sb.Append(projectcoordemail);
            sb.Append("</p>");
            //leave a blank row
            sb.Append("<p>");
            sb.Append("</p>");
            //footer
            sb.Append(_emailmessagebe.GetHtmlParagraphHeader());
            sb.Append("<b><u>PLEASE DO NOT REPLY TO THIS EMAIL<u></b>");
            sb.Append("</p>");

            return sb.ToString();
        }

        public string CreatePlanReviewerNotAvailableForExpressMessageBody(string planReviewerName, List<DateTime?> ReservedExpress, List<DateTime?> AssignedExpress)
        {
            StringBuilder sb = new StringBuilder(5000);
            sb.Append("</br>");
            sb.Append("<p> Plan Reviewer has been marked as not available for express reviews");
            sb.Append("</p>");

            sb.Append("<p><b>");
            sb.Append("Name: ");
            sb.Append(planReviewerName);
            sb.Append("</b></p>");

            sb.Append("<p>");
            sb.Append("The individual is assigned to the below reviews and reserved times.");
            sb.Append("</p>");

            sb.Append("<p>");
            sb.Append("Please replace the individual with another that can participate in express reviews");
            sb.Append("</p>");

            sb.Append("<p><b>");
            sb.Append("Assigned Express Reviews: ");
            sb.Append("<br/>");
            sb.Append("Date: ");
            foreach (DateTime ae in AssignedExpress.Where(x => x.Value.Date >= DateTime.UtcNow.Date).ToList())
            {
                if (ae != null)
                {
                    sb.Append(ae);
                    sb.Append(" , ");

                }

            }

            sb.Append("</b></p>");

            sb.Append("<p><b>");
            sb.Append("Reserved Express: ");
            sb.Append("<br/>");
            sb.Append("Date: ");
            foreach (DateTime re in ReservedExpress.Where(x => x.Value.Date >= DateTime.UtcNow.Date).ToList())
            {
                if (re != null)
                {
                    sb.Append(re);
                    sb.Append(" , ");

                }
            }
            sb.Append("</b></p>");

            //footer
            sb.Append(_emailmessagebe.GetHtmlParagraphHeader());
            sb.Append("<b><u>PLEASE DO NOT REPLY TO THIS EMAIL<u></b>");
            sb.Append("</p>");

            return sb.ToString();

        }

        //Meetings
        public string CancelMeetingScheduledMessageBody(string projectnumber, string projectname, string projectaddress, string meetingDateTime,
           string meetingroom, string meetingtype, string projectcoordname, string projectcoordphone, string projectcoordemail)
        {
            StringBuilder sb = new StringBuilder(5000);


            sb.Append("</br>");
            sb.Append("<p>");
            sb.Append(meetingtype);
            sb.Append("&nbsp;");
            sb.Append("meeting tentatively scheduled has been canceled - either rejected or canceled due to not being accepted within 2 business days.");
            sb.Append("</p>");
            sb.Append("<p><b>");
            sb.Append("Project #:");
            sb.Append(projectnumber);
            sb.Append("(" + projectname + ")");
            sb.Append("</b></p>");
            sb.Append("<p><b>");
            sb.Append("Address");
            sb.Append(projectaddress);
            sb.Append("</b></p>");
            sb.Append("<p><b>");
            sb.Append("Meeting Name: ");
            sb.Append(meetingroom);
            sb.Append("</b></p>");
            sb.Append("<p><b>");
            sb.Append("Meeting Date & Time: ");
            sb.Append(meetingDateTime);
            sb.Append("</b></p>");
            sb.Append("<p><b>");
            sb.Append("Meeting Place: ");
            sb.Append(meetingroom);
            sb.Append("</b></p>");

            //leave a blank row
            sb.Append("<p>");
            sb.Append("</p>");
            sb.Append("<p>");
            sb.Append("If you have any questions, please contact the Mecklenburg County project coordinator listed below:");
            sb.Append("</p>");
            sb.Append("<p>");
            sb.Append(projectcoordname);
            sb.Append("</p>");
            sb.Append("<p> Bus Ph: ");
            sb.Append(projectcoordphone);
            sb.Append("</p>");
            sb.Append("<p>");
            sb.Append(projectcoordemail);
            sb.Append("</p>");
            //leave a blank row
            sb.Append("<p>");
            sb.Append("</p>");
            //footer
            sb.Append(_emailmessagebe.GetHtmlParagraphHeader());
            sb.Append("<b><u>PLEASE DO NOT REPLY TO THIS EMAIL<u></b>");
            sb.Append("</p>");

            return sb.ToString();
        }
    }

}
