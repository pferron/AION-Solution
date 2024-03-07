using AION.Base;
using AION.BL.Adapters;
using AION.Manager.Models;
using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace AION.Manager.Controllers
{
    [Authorize]
    public class OutlookController : BaseApiController
    {

        [HttpPost]
        [ResponseType(typeof(MeetingAllocationResponse))]
        [ActionName("GetOutlookMeetingAllocation")]
        [Route("api/Outlook/GetOutlookMeetingAllocation")]
        public IHttpActionResult GetOutlookMeetingAllocation(MeetingAllocationRequest data)
        {
            try
            {
                List<MeetingAllocationResponse> ret = new List<MeetingAllocationResponse>();
                var result = new OutlookAdapter().CheckForMeetingAllocationAvailability(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return Ok("START ERROR: " + ex.Message + ", API StackTrace: " + ex.StackTrace + (ex.InnerException != null ? " INNER EXCEPTION " + ex.InnerException.Message : "") + ". END ERROR");
            }
        }

        [HttpPost]
        [ResponseType(typeof(bool))]
        [ActionName("InjectMeetingToAllAttendeesDefaultCalendars")]
        [Route("api/Outlook/InjectMeetingToAllAttendeesDefaultCalendars")]
        public IHttpActionResult InjectMeetingToAllAttendeesDefaultCalendars(InjectMeetingModel data)
        {
            try
            {
                List<MeetingAllocationResponse> ret = new List<MeetingAllocationResponse>();
                var result = new OutlookAdapter().InjectMeetingToAllAttendeesDefaultCalendars(data.Subject, data.HtmlBody, data.AllAttendeesEmailList,
                    data.LocationDisplayText, data.LocationEmail, data.StartTime, data.EndTime);

                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception("START ERROR: " + ex.Message + ", API StackTrace: " + ex.StackTrace + (ex.InnerException != null ? " INNER EXCEPTION " + ex.InnerException.Message : "") + ". END ERROR");
            }
        }
    }
}
