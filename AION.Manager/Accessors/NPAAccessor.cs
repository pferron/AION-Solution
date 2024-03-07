using AION.Base;
using AION.BL;
using AION.BL.Adapters;
using AION.BL.BusinessObjects;
using AION.BL.Models;
using AION.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessEntities;
using AION.Manager.Adapters;
using AION.Manager.Models;
using AIONEstimator.Engine.BusinessObjects;
using Meck.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AION.Manager.Accessors
{
    public class NPAAccessor : BaseAdapter, INPAAccessor
    {
        public bool DeleteNPA(List<int> scheduleIdList, bool cancelRecurringflag)
        {
            bool success = false;

            NonProjectAppointmentBO bo = new NonProjectAppointmentBO();
            ProjectScheduleBO projectScheduleBO = new ProjectScheduleBO();

            foreach (var id in scheduleIdList)
            {
                try
                {
                    ProjectScheduleBE projectSchedule = projectScheduleBO.GetById(id);
                    NonProjectAppointment npa = bo.GetInstance(projectSchedule.AppoinmentID.Value);

                    IAppointmentAdapter adapter = new NPAAdapter(npa);

                    if (cancelRecurringflag)
                    {
                        List<ProjectScheduleBE> projectSchedules = adapter.GetProjectScheduleByAppointmentId(npa.NonProjectAppointmentID.Value);
                        //find all project schedules with that appointment id and remove each one

                        foreach (ProjectScheduleBE schedule in projectSchedules)
                        {
                            adapter.RemoveAttendees(schedule.ProjectScheduleID.Value);
                        }
                    }
                    else
                    {
                        adapter.RemoveAttendees(projectSchedule.ProjectScheduleID.Value);
                    }

                    bo.DeleteNPAById(id, cancelRecurringflag);
                    success = true;
                }
                catch (Exception ex)
                {
                    string errorMessage = "Error in NPAAccessor DeleteNPA - " + ex.Message;

                    var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                    success = false;
                }
            }
            return success;
        }

        public List<NPASearchResult> GetNPAList()
        {
            return SearchNPAs(0, 0, string.Empty, DateTime.Now, DateTime.Now.AddMonths(3));
        }

        public List<NPASearchResult> GetEndingSoonList()
        {
            return SearchNPAs(0, 0, string.Empty, DateTime.Now, DateTime.Now.AddMonths(1));
        }

        public List<NPASearchResult> SearchNPAs(int? type, int? reviewerId, string searchtxt, DateTime? startdate, DateTime? enddate)
        {
            List<NPASearchResult> ret = new List<NPASearchResult>();
            try
            {
                NonProjectAppointmentBO bo = new NonProjectAppointmentBO();
                UserScheduleBO userScheduleBO = new UserScheduleBO();
                UserIdentityModelBO userIdentityModelBO = new UserIdentityModelBO();

                List<NonProjectAppointment> getListbySearch = bo.SearchNPAs(type, reviewerId, searchtxt, startdate.HasValue ? startdate : null, enddate.HasValue ? enddate : null);
                foreach (NonProjectAppointment item in getListbySearch)
                {
                    int npaId = item.NonProjectAppointmentID.Value;

                    IAppointmentAdapter adapter = new NPAAdapter(item);

                    DateTime Apptfrom = DateTime.Parse(item.AppointmentFrom.ToString());
                    DateTime ApptTo = DateTime.Parse(item.AppointmentTo.ToString());
                    NpaTypeRefBO boType = new NpaTypeRefBO();
                    NpaTypeRefBE beType = boType.GetById(item.NPATypeRefID.Value);
                    AppoinmentReccuranceRefBO borecur = new AppoinmentReccuranceRefBO();
                    AppoinmentReccuranceRefBE berecur = borecur.GetById(item.AppoinmentRecurrenceRefID.Value);
                    string recurrenceDay = berecur.ReccuranceDay;
                    string meetingroomname = string.Empty;
                    meetingroomname = item.MeetingRoomRefId != null ? new MeetingRoomBO().GetById(item.MeetingRoomRefId.Value).MeetingRoomName : string.Empty;

                    List<ProjectSchedule> projectscheduleList = new List<ProjectSchedule>();
                    List<ProjectScheduleBE> schedulelst = adapter.GetProjectScheduleByAppointmentId(npaId);

                    if (schedulelst.Count > 0 && schedulelst[0].ProjectScheduleID != null)
                    {
                        ProjectScheduleBE schedule = schedulelst[0];

                        List<AttendeeInfo> attendees = adapter.GetAttendeesByApptId(npaId);

                        foreach (ProjectScheduleBE sche in schedulelst)
                        {
                            if (sche.ProjectScheduleID.HasValue)
                            {
                                projectscheduleList.Add(new ProjectSchedule
                                {
                                    ProjectScheduleID = (sche.ProjectScheduleID.HasValue) ? sche.ProjectScheduleID.Value : 0,
                                    ProjectScheduleTypeRef = sche.ProjectScheduleTypeRef,
                                    AppointmentID = sche.AppoinmentID.Value,
                                    RecurringApptDt = (sche.RecurringApptDt.HasValue) ? sche.RecurringApptDt.Value : (DateTime?)null

                                });
                            }
                        }
                        ret.Add(new NPASearchResult()
                        {
                            NPAId = npaId,
                            NPADate = Apptfrom.ToString("MM/dd/yyyy"),
                            Schedules = projectscheduleList.Where(s => s.RecurringApptDt >= startdate).Where(s => s.RecurringApptDt <= enddate).ToList(),
                            NPAType = beType.AppointmentTypeName,
                            NPAName = item.AppoinmentName,
                            IsRecurring = recurrenceDay == "Once" ? "N" : "Y",
                            MeetingRoomName = meetingroomname,
                            Day = (recurrenceDay == "Once" || recurrenceDay == "Yearly" || recurrenceDay == "Daily") ? "" : recurrenceDay,
                            Time = Apptfrom.ToString("hh:mm tt") + " to " + ApptTo.ToString("hh:mm tt"),
                            Attendees = attendees
                        });
                    }
                }

            }
            catch (Exception ex)
            {
                string errorMessage = "Error NPASearch - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
            return ret;

        }

        /// <summary>
        /// On saving a user with new Trade/Agency in the Admin panel, adds that user to NPAs associated with each Trade/Agency
        /// check for ce/agency plan reviewer role, active and schedulable
        /// </summary>
        /// <param name="item"></param>
        public void SendExistingNPACalendarAppts(UserDepartmentXref item)
        {
            //Create NPABO to search for existing NPAs
            NonProjectAppoinmentBO npabo = new NonProjectAppoinmentBO();
            List<int[]> npasToAdd = new List<int[]>();
            bool allbuildind = false;
            bool allelctrind = false;
            bool allmechind = false;
            bool allplumbind = false;
            bool allzoningind = false;
            bool allfireind = false;
            bool allbackflowind = false;
            bool allehsfoodind = false;
            bool allehspoolind = false;
            bool allehslodgeind = false;
            bool allehsdaycareind = false;
            //departments is businessRefIds
            List<Department> departments = new DepartmentModelBO().BaseList.ToList();
            foreach (var dept in item.UserDepartmentIDList)
            {
                Department department = departments.Where(x => x.ID == dept).FirstOrDefault();
                switch (department.DepartmentDivision.DepartmentDivision)
                {
                    case DepartmentDivisionEnum.Building:
                        allbuildind = true;
                        break;
                    case DepartmentDivisionEnum.Electrical:
                        allelctrind = true;
                        break;
                    case DepartmentDivisionEnum.Mechanical:
                        allmechind = true;
                        break;
                    case DepartmentDivisionEnum.Plumbing:
                        allplumbind = true;
                        break;
                    case DepartmentDivisionEnum.Zoning:
                        allzoningind = true;
                        break;
                    case DepartmentDivisionEnum.Fire:
                        allfireind = true;
                        break;
                    case DepartmentDivisionEnum.Environmental:
                        switch (department.DepartmentEnum)
                        {
                            case DepartmentNameEnums.EH_Day_Care:
                                allehsdaycareind = true;
                                break;
                            case DepartmentNameEnums.EH_Food:
                                allehsfoodind = true;
                                break;
                            case DepartmentNameEnums.EH_Pool:
                                allehspoolind = true;
                                break;
                            case DepartmentNameEnums.EH_Facilities:
                                allehslodgeind = true;
                                break;
                        }
                        break;
                    case DepartmentDivisionEnum.Backflow:
                        allbackflowind = true;
                        break;
                }

            }

            try
            {
                npasToAdd = npabo.GetNonExistingNPAsByUser(DateTime.Now,
                                     DateTime.Now.AddMonths(24),
                                     item.UserID,
                                     allbuildind,
                                     allelctrind,
                                     allmechind,
                                     allplumbind,
                                     allzoningind,
                                     allfireind,
                                     allbackflowind,
                                     allehsfoodind,
                                     allehspoolind,
                                     allehslodgeind,
                                     allehsdaycareind);


                //foreach npas to add, get the business ref id and add another attendee for that one
                foreach (int[] npaId in npasToAdd)
                {
                    //get appointment
                    int userid = npaId[0];
                    int apptid = npaId[1];
                    int businessrefid = npaId[2];

                    //Create UserBO to find user info
                    UserIdentityModelBO ubo = new UserIdentityModelBO();

                    //Get user info 
                    UserIdentity userInfo = ubo.GetInstance(userid);

                    //Create empty Attendee list to populate and send to InsertAttendees
                    List<AttendeeInfo> attendees = new List<AttendeeInfo>();

                    //Create new attendee and add to temp attendee list
                    AttendeeInfo attendee = new AttendeeInfo();
                    attendee.AttendeeId = userid;
                    attendee.BusinessRefId = businessrefid;
                    attendee.FirstName = userInfo.FirstName;
                    attendee.LastName = userInfo.LastName;
                    attendees.Add(attendee);

                    NonProjectAppointment npaobj = new NonProjectAppointmentBO().GetInstance(apptid);

                    IAppointmentAdapter adapter = new NPAAdapter(npaobj);

                    //get the project schedule id
                    List<ProjectScheduleBE> projectScheduleList = adapter.GetProjectScheduleByAppointmentId(apptid);
                    ProjectScheduleBE projectSchedule = projectScheduleList.FirstOrDefault();

                    adapter.InsertAttendees(attendees, item.UpdatedUserId, projectSchedule.ProjectScheduleID.Value);
                }
            }
            catch (System.Exception ex)
            {
                string errorMessage = "Error in NPAAccessor SendExistingNPACalendarApptsSaveUser - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
        }
        /// <summary>
        /// adds user to NPAs associated with each Trade/Agency
        /// </summary>
        /// <param name="item"></param>
        public bool ProcessNpas(List<UserBusinessRelationshipBE> items, int updatedUserId, DateTime timeStamp)
        {
            //for each row, for each business ref, add attendee to relevant npa
            //for each user, create the xref and process the npas
            foreach (UserBusinessRelationshipBE dept in items)
            {
                List<int> businessrefids = new List<int>();
                businessrefids.Add(dept.BusinessRefId.Value);
                UserDepartmentXref item = new UserDepartmentXref
                {
                    UserID = (int)dept.UserID,
                    TimeStamp = timeStamp,
                    UpdatedUserId = updatedUserId,
                    UserDepartmentIDList = businessrefids
                };

                SendExistingNPACalendarAppts(item);

            }
            return true;
        }

        public List<NPASearchResult> SearchNPAs_v2(int? type, int? reviewerId, string searchtxt, DateTime? startdate, DateTime? enddate)
        {
            List<NPASearchResult> ret = new List<NPASearchResult>();
            try
            {
                NonProjectAppointmentBO bo = new NonProjectAppointmentBO();
                UserScheduleBO userScheduleBO = new UserScheduleBO();
                UserIdentityModelBO userIdentityModelBO = new UserIdentityModelBO();
                NpaTypeRefBO boType = new NpaTypeRefBO();
                List<NpaTypeRefBE> npaTypeRefBEs = boType.GetAllNPaTypes(false);

                List<NPASearchResultBE> getListbySearch = bo.SearchNPAs_v2(type, reviewerId, searchtxt, startdate.HasValue ? startdate : null, enddate.HasValue ? enddate : null);
                foreach (NPASearchResultBE item in getListbySearch)
                {
                    NonProjectAppointment npa = new NonProjectAppointment()
                    {
                        AppoinmentName = item.AppoinmentName,
                        AppoinmentRecurrenceRefID = item.AppoinmentRecurrenceRefID,
                        AppointmentFrom = item.AppointmentFrom,
                        AppointmentTo = item.AppointmentTo,
                        NonProjectAppointmentID = item.NonProjectAppointmentID,
                        MeetingRoomRefId = item.MeetingRoomRefID,
                        IsAllBackFlow = item.IsAllBackFlow,
                        IsAllDay = item.IsAllDay,
                        IsAllBuild = item.IsAllBuild,
                        IsAllEhsDayCare = item.IsAllEhsDayCare,
                        IsAllEhsFood = item.IsAllEhsFood,
                        IsAllEhsLodge = item.IsAllEhsLodge,
                        IsAllEhsPool = item.IsAllEhsPool,
                        IsAllElectric = item.IsAllElectric,
                        IsAllFire = item.IsAllFire,
                        IsAllMech = item.IsAllMech,
                        IsAllPlanReviewers = item.IsAllPlanReviewers,
                        IsAllPlumb = item.IsAllPlumb,
                        IsAllZoning = item.IsAllZoning,
                        NPATypeRefID = item.NPATypeRefID,
                    };

                    IAppointmentAdapter adapter = new NPAAdapter(npa);

                    DateTime Apptfrom = DateTime.Parse(item.AppointmentFrom.ToString());
                    DateTime ApptTo = DateTime.Parse(item.AppointmentTo.ToString());
                    NpaTypeRefBE beType = npaTypeRefBEs.Where(x => x.NpaTypeRefID.Value == item.NPATypeRefID.Value).FirstOrDefault();
                    AppoinmentReccuranceRefBO borecur = new AppoinmentReccuranceRefBO();
                    AppoinmentReccuranceRefBE berecur = borecur.GetById(item.AppoinmentRecurrenceRefID.Value);
                    int npaId = item.NonProjectAppointmentID.Value;
                    string recurrenceDay = berecur.ReccuranceDay;
                    string meetingroomname = string.Empty;
                    meetingroomname = item.MeetingRoomRefID != null ? new MeetingRoomBO().GetById(item.MeetingRoomRefID.Value).MeetingRoomName : string.Empty;

                    List<ProjectSchedule> projectscheduleList = new List<ProjectSchedule>();
                    List<ProjectScheduleBE> schedulelst = adapter.GetProjectScheduleByAppointmentId(npaId);

                    //get the users per the projectscheduleid in the search list
                    List<AttendeeInfo> attendees = adapter.GetAttendeesByApptId(npaId);

                    foreach (ProjectScheduleBE sche in schedulelst)
                    {
                        if (sche.ProjectScheduleID.HasValue)
                        {
                            projectscheduleList.Add(new ProjectSchedule
                            {
                                ProjectScheduleID = (sche.ProjectScheduleID.HasValue) ? sche.ProjectScheduleID.Value : 0,
                                ProjectScheduleTypeRef = sche.ProjectScheduleTypeRef,
                                AppointmentID = sche.AppoinmentID.Value,
                                RecurringApptDt = (sche.RecurringApptDt.HasValue) ? sche.RecurringApptDt.Value : (DateTime?)null

                            });
                        }
                    }
                    ret.Add(new NPASearchResult()
                    {
                        NPAId = npaId,
                        NPADate = Apptfrom.ToString("MM/dd/yyyy"),
                        Schedules = projectscheduleList.Where(s => s.RecurringApptDt >= startdate).Where(s => s.RecurringApptDt <= enddate).ToList(),
                        NPAType = beType.AppointmentTypeName,
                        NPAName = item.AppoinmentName,
                        IsRecurring = recurrenceDay == "Once" ? "N" : "Y",
                        MeetingRoomName = meetingroomname,
                        Day = item.RecurringApptDt.Value.DayOfWeek.ToString(),
                        Time = Apptfrom.ToString("hh:mm tt") + " to " + ApptTo.ToString("hh:mm tt"),
                        Attendees = attendees,
                        ProjectScheduleID = item.ProjectScheduleID.Value,
                        Recurring_Date = item.RecurringApptDt.Value
                    });
                }

            }
            catch (Exception ex)
            {
                string errorMessage = "Error NPASearchV2 - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
            return ret;

        }

    }
}