using AION.Base;
using AION.BL;
using AION.BL.Adapters;
using AION.BL.BusinessObjects;
using AION.BL.Models;
using AION.Manager.Accessors;
using AION.Manager.Adapters;
using AION.Manager.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;


namespace AION.Manager.Controllers
{
    [Authorize]
    public class NPAController : BaseApiController
    {
        // GET: NPA
        public NPAController()
        {
        }

        [HttpGet]
        [ResponseType(typeof(NPAModel))]
        [Route("api/NPA/GetNPAModel")]
        public IHttpActionResult GetNPAModel()
        {
            INPAAdapter thisengine = new NPAAdapter();

            var result = thisengine.GetNPAModel();

            return Ok(result);
        }

        /// <summary>
        ///  UpsertNPA
        /// </summary>
        /// <param name="objectNPA">object of NonProjectAppointment</param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(int))]
        [Route("api/NPA/UpsertNPA")]
        public IHttpActionResult UpsertNPA(NonProjectAppointment objectNPA)
        {
            INPAAdapter thisengine = new NPAAdapter(objectNPA);

            var result = thisengine.Upsert();
            return Ok(result);
        }


        /// <summary>
        /// RemoveNewNPAAttendees 
        /// </summary>
        /// <param name="objectAttendeeIds">object NPAAttendeesModel</param>
        /// <param name="NpaId"> int NpaId</param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(bool))]
        [Route("api/NPA/RemoveNewNPAAttendees")]
        public IHttpActionResult RemoveNewNPAAttendees(ApptAttendeesManagerModel item)
        {
            
            List<AttendeeInfo> attendees = new List<AttendeeInfo>();
            foreach (ApptAttendeeManagerModel attendee in item.AttendeeIds)
            {
                int businessRefId = new DepartmentModelBO().GetInstance((DepartmentNameEnums)attendee.DeptNameEnumId).ID;
                AttendeeInfo info = new AttendeeInfo(attendee.AttendeeId, businessRefId);
                attendees.Add(info);

            }

            NonProjectAppointment npa = new NonProjectAppointmentBO().GetInstance(item.ApptId);

            IAppointmentAdapter thisengine = new NPAAdapter(npa);

            var result = thisengine.RemoveAttendees(attendees);
            return Ok(result);
        }

        /// <summary>
        ///  DeleteNPA delete NPAs by id
        /// </summary>
        /// <param name="idList">string of List<int></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route("api/NPA/DeleteNPA")]
        public IHttpActionResult DeleteNPA(NPADeleteInput npaDeleteInput)
        {
            INPAAccessor thisengine = new NPAAccessor();

            var result = thisengine.DeleteNPA(npaDeleteInput.ScheduleIds, npaDeleteInput.Flag);
            return Ok(result);
        }

        /// <summary>
        /// SearchNPAs Search NPAs by filter list V2
        /// </summary>
        /// <param name="type"></param>
        /// <param name="reviewerId"></param>
        /// <param name="searchtxt"></param>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <returns> string of List<NPASearchResult></returns>
        [HttpGet]
        [ResponseType(typeof(List<NPASearchResult>))]
        [Route("api/NPA/SearchNPAs")]
        public IHttpActionResult SearchNPAs(int type, int reviewerId, string searchtxt, DateTime? startdate, DateTime? enddate)
        {
            INPAAccessor thisengine = new NPAAccessor();
            //check for null in searchtxt
            searchtxt = String.IsNullOrWhiteSpace(searchtxt) ? "" : searchtxt;

            var result = thisengine.SearchNPAs_v2(type, reviewerId, searchtxt, startdate.HasValue ? startdate : DateTime.Now.AddMonths(-12), enddate.HasValue ? enddate : DateTime.Now.AddMonths(12));

            return Ok(result);
        }

        /// <summary>
        /// GetNPAList V2
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet]
        [ResponseType(typeof(List<NPASearchResult>))]
        [Route("api/NPA/GetNPAList")]
        public IHttpActionResult GetNPAList()
        {
            INPAAccessor thisengine = new NPAAccessor();

            //TODO: make sure this is getting the right attendees
            var result = thisengine.SearchNPAs_v2(0, 0, string.Empty, DateTime.Now, DateTime.Now.AddMonths(3));

            return Ok(result);
        }
        /// <summary>
        /// GetEndingSoonList V2
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet]
        [ResponseType(typeof(List<NPASearchResult>))]
        [Route("api/NPA/GetEndingSoonList")]
        public IHttpActionResult GetEndingSoonList()
        {
            INPAAccessor thisengine = new NPAAccessor();

            var result = thisengine.SearchNPAs_v2(0, 0, string.Empty, DateTime.Now, DateTime.Now.AddMonths(1));

            return Ok(result);
        }
        /// <summary>
        /// GetAllNpaTypes
        /// </summary>
        /// <returns>strong of List<NpaType> </returns>
        [HttpGet]
        [ResponseType(typeof(List<NpaType>))]
        [Route("api/NPA/GetAllNpaTypes")]
        public IHttpActionResult GetAllNpaTypes()
        {
            INPATypeAdapter thisengine = new NPATypeAdapter();

            var mAllNpaTypes = thisengine.GetAll();

            return Ok(mAllNpaTypes);
        }

        /// <summary>
        /// InsertNpaType
        /// </summary>
        /// <param name="data">object NpaType</param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route("api/NPA/InsertNpaType")]
        public IHttpActionResult InsertNpaType(NpaType data)
        {
            INPATypeAdapter thisengine = new NPATypeAdapter();

            var result = thisengine.Insert(data);

            return Ok(result);
        }

        /// <summary>
        /// MakeNPATypeActive
        /// </summary>
        /// <param name="NpaType_data">Json string object of NpaType </param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route("api/NPA/MakeNpaTypeActive")]
        public IHttpActionResult MakeNpaTypeActive(NpaType data)
        {
            INPATypeAdapter thisengine = new NPATypeAdapter();

            var result = thisengine.MakeActive(data);

            return Ok(result);
        }

        /// <summary>
        /// Make npa type inactive
        /// </summary>
        /// <param name="npaTypeID">object NpaType</param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route("api/NPA/MakeNPATypeInActive")]
        public IHttpActionResult MakeNPATypeInActive(NpaType data)
        {
            INPATypeAdapter thisengine = new NPATypeAdapter();

            var result = thisengine.MakeInActive(data);

            return Ok(result);
        }

        /// <summary>
        ///  UpdateNPAConfiguration
        /// </summary>
        /// <param name="npaConfig"> List<string> of NPA configuration</param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route("api/NPA/UpdateNPAConfiguration")]
        public IHttpActionResult UpdateNPAConfiguration(List<string> NpaConfig)
        {
            INPATypeAdapter thisengine = new NPATypeAdapter();

            var result = thisengine.UpdateNPAConfiguration(NpaConfig);

            return Ok(result);
        }


        /// <summary>
        /// GetActiveMeetingRoom
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(List<MeetingRoom>))]
        [Route("api/NPA/GetActiveMeetingRooms")]
        public IHttpActionResult GetActiveMeetingRooms(string meetingType = "")
        {
            IMeetingRoomAdapter thisengine = new MeetingRoomAdapter();

            var result = thisengine.GetMeetingRooms(true, meetingType);

            return Ok(result);
        }
        /// <summary>
        /// Get useridentity list by NPA for Add/Remove button
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(List<UserIdentity>))]
        [Route("api/NPA/GetAttendeesByNpaId")]
        public IHttpActionResult GetAttendeesByNpaId(int id)
        {
            NonProjectAppointment npa = new NonProjectAppointmentBO().GetInstance(id);

            IAppointmentAdapter thisengine = new NPAAdapter(npa);

            var result = thisengine.GetAttendeesByApptId(id);

            return Ok(result);

        }

        /// <summary>
        /// UpdateAttendees
        /// </summary>
        /// <param name="item">ApptAttendeesManagerModel</param>
        /// <returns>bool </returns>
        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route("api/NPA/UpdateAttendees")]
        public IHttpActionResult UpdateAttendees(ApptAttendeesManagerModel item)
        {
            var t = Newtonsoft.Json.JsonConvert.SerializeObject(item);
            List<AttendeeInfo> attendees = new List<AttendeeInfo>();
            foreach (ApptAttendeeManagerModel attendee in item.AttendeeIds)
            {
                int businessRefId = new DepartmentModelBO().GetInstance((DepartmentNameEnums)attendee.DeptNameEnumId).ID;
                AttendeeInfo info = new AttendeeInfo(attendee.AttendeeId, businessRefId);
                attendees.Add(info);

            }

            NonProjectAppointment npa = new NonProjectAppointmentBO().GetInstance(item.ApptId);

            IAppointmentAdapter thisengine = new NPAAdapter(npa);

            var result = thisengine.UpdateAttendeeList(attendees, item.WkrId, item.ProjectScheduleID, false);
            return Ok(result);
        }

        /// <summary>
        ///  InsertNewNPAAttendees
        ///  </summary>
        /// <param name="item"> object NPAAttendeesModel </param>
        /// <param name="NpaId"> integer NpaId</param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route("api/NPA/InsertNewNPAAttendees")]
        public IHttpActionResult InsertNewNPAAttendees(ApptAttendeesManagerModel item)
        {
            List<AttendeeInfo> attendees = new List<AttendeeInfo>();
            foreach (ApptAttendeeManagerModel attendee in item.AttendeeIds)
            {
                int businessRefId = new DepartmentModelBO().GetInstance((DepartmentNameEnums)attendee.DeptNameEnumId).ID;
                AttendeeInfo info = new AttendeeInfo(attendee.AttendeeId, businessRefId);
                attendees.Add(info);
            }

            NonProjectAppointment npa = new NonProjectAppointmentBO().GetInstance(item.ApptId);

            IAppointmentAdapter thisengine = new NPAAdapter(npa);
            var result = thisengine.InsertAttendees(attendees, item.WkrId);

            return Ok(result);
        }
    }
}