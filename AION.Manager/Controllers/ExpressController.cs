using AION.Base;
using AION.BL;
using AION.BL.Adapters;
using AION.BL.BusinessObjects;
using AION.BL.Common;
using AION.BL.Models;
using AION.Manager.Adapters;
using AION.Manager.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace AION.Manager.Controllers
{
    public class ExpressController : BaseApiController
    {
        /// <summary>
        /// Get Express Model for Express
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(ExpressModel))]
        [Route("api/Express/GetExpressModel")]
        public IHttpActionResult GetExpressModel()
        {
            IExpressAdapter thisengine = new ExpressAdapter();

            var result = thisengine.GetExpressModel();

            return Ok(result);
        }

        [HttpGet]
        [ResponseType(typeof(bool))]
        [Route("api/Express/ExpressReservation")]
        public IHttpActionResult ExpressReservation()
        {
            IHolidayConfigAdapter holidayConfigAdapter = new HolidayConfigAdapter();

            var holidayConfigDates = holidayConfigAdapter.GetHolidayConfigDates();
            var expressBlockedDates = DateTimeHelper.GetBlockedHolidayDatesForExpress(DateTime.Now.Year);
            holidayConfigDates.AddRange(expressBlockedDates);

            ExpressAdapter thisengine = new ExpressAdapter(holidayConfigDates);

            var result = thisengine.ExpressReservations();

            return Ok(result);
        }

        [HttpGet]
        [ResponseType(typeof(bool))]
        [Route("api/Express/SearchbyDates")]
        public IHttpActionResult SearchbyDates(DateTime fromdate, DateTime todate)
        {
            ExpressAdapter thisengine = new ExpressAdapter();
            var result = thisengine.GetReservationByDate(fromdate, todate);
            return Ok(result);
        }

        /// <summary>
        /// UpdateAttendees
        /// </summary>
        /// <param name="item">ApptAttendeesManagerModel</param>
        /// <returns>bool </returns>
        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route("api/Express/UpdateAttendees")]
        public IHttpActionResult UpdateAttendees(ApptAttendeesManagerModel item)
        {
            //AppointmentAdapter<ReserveExpressReservation> thisengine = new AppointmentAdapter<ReserveExpressReservation>();
            ExpressAdapter thisengine = new ExpressAdapter();
            List<AttendeeInfo> attendees = new List<AttendeeInfo>();
            foreach (ApptAttendeeManagerModel attendee in item.AttendeeIds)
            {
                int businessRefId = new DepartmentModelBO().GetInstance((DepartmentNameEnums)attendee.DeptNameEnumId).ID;
                AttendeeInfo info = new AttendeeInfo(attendee.AttendeeId, businessRefId);
                attendees.Add(info);
            }
            var result = thisengine.UpdateAttendeeList(attendees, item.ApptId, item.WkrId, item.ProjectScheduleID, item.IsSchedule, item.ProcessInsertRemoveOnly);
            return Ok(result);
        }

        /// <summary>
        /// CancelReservations
        /// </summary>
        /// <param name="items">List ApptAttendeesManagerModel</param>
        /// <returns>bool </returns>
        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route("api/Express/CancelReservations")]
        public IHttpActionResult CancelReservations(List<ApptAttendeesManagerModel> items)
        {
            ExpressAdapter thisengine = new ExpressAdapter();

            var result = thisengine.CancelReservations(items);

            return Ok(result);
        }

        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route("api/Express/SaveReservation")]
        public IHttpActionResult SaveReservation(List<ReserveExpressReservation> Reservations)
        {
            ExpressAdapter thisengine = new ExpressAdapter();
            var result = thisengine.UpdateReservation(Reservations);
            return Ok(result);
        }

        [HttpGet]
        [ResponseType(typeof(List<ReserveExpressReservation>))]
        [Route("api/Express/GetExpressReservationList")]
        public IHttpActionResult GetExpressReservationList()
        {
            ExpressAdapter thisengine = new ExpressAdapter();
            var result = thisengine.GetExpressReservationList();
            return Ok(result);
        }

        [HttpPost]
        [ResponseType(typeof(int))]
        [Route("api/Express/UpsertEXP")]
        public IHttpActionResult UpsertEXP(ReserveExpressReservation reservation)
        {
            IExpressAdapter thisengine = new ExpressAdapter(reservation);
            var result = thisengine.Upsert();
            return Ok(result);
        }

        [HttpGet]
        [ResponseType(typeof(bool))]
        [Route("api/Express/GetExpressScheduledByDates")]
        public IHttpActionResult GetExpressScheduledByDates(DateTime fromdate, DateTime todate)
        {
            ExpressAdapter thisengine = new ExpressAdapter();
            var result = thisengine.GetScheduledByDate(fromdate, todate);
            return Ok(result);
        }

        [HttpGet]
        [ResponseType(typeof(bool))]
        [Route("api/Express/UpdateReserveExpressReviewerRotation")]
        public IHttpActionResult UpdateReserveExpressReviewerRotation()
        {
            ExpressAdapter thisengine = new ExpressAdapter();
            var result = thisengine.UpdateReserveExpressReviewerRotation();
            return Ok(result);
        }
    }
}
