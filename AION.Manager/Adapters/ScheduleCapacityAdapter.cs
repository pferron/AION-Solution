using AION.BL;
using AION.BL.BusinessObjects;
using AION.Engine.BusinessEntities;
using AION.Manager.Models;
using AION.Scheduler.Engine.BusinessEntities;
using AION.Scheduler.Engine.BusinessObjects;
using AIONEstimator.Engine.BusinessObjects;
using Meck.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AION.Manager.Adapters
{
    public class ScheduleCapacityAdapter : BaseManagerAdapter, IScheduleCapacityAdapter
    {
        public List<ScheduleCapacitySearchResult> ScheduleCapacitySearch(ScheduleCapacitySearch search)
        {
            try
            {
                //get the cross join users with dates
                ScheduleCapacitySearchResultBO bo = new ScheduleCapacitySearchResultBO();
                List<ScheduleCapacitySearchResult> results = new List<ScheduleCapacitySearchResult>();
                List<Meeting> meetingresults = new List<Meeting>();
                List<ScheduleCapacitySearchResultBE> scheduleCapacitySearchResults = bo.GetReviewersDateList(search.BeginDateTime, search.EndDateTime, string.Join(",", search.ReviewerSearchList));
                foreach (ScheduleCapacitySearchResultBE item in scheduleCapacitySearchResults)
                {
                    results.Add(ConvertBEToModel(item));
                }
                //get the meetings
                List<ScheduleCapacitySearchResultBE> scheduleCapacityMeetingResults = bo.GetMeetingsForDateList(search.BeginDateTime, search.EndDateTime, string.Join(",", search.ReviewerSearchList));
                foreach (ScheduleCapacitySearchResult user in results)
                {
                    List<Meeting> meetings = new List<Meeting>();
                    List<Meeting> npas = new List<Meeting>();
                    List<Meeting> expressReservations = new List<Meeting>();
                    List<Meeting> expressMeetings = new List<Meeting>();
                    List<Meeting> planreviews = new List<Meeting>();
                    List<Meeting> fifoplanreview = new List<Meeting>();
                    var totalnpahours = 0.0;
                    var totalpmahours = 0.0;
                    var totalexphours = 0.0;
                    var totalemahours = 0.0;
                    var totalprhours = 0.0;
                    var totalfmahours = 0.0;
                    var totalfifohurs = 0.0;
                    var totalhours = 0.0;

                    ProjectScheduleBE projectScheduleBE = new ProjectScheduleBE();
                    ProjectScheduleBO projectScheduleBO = new ProjectScheduleBO();
                    ProjectScheduleModelBO modelBO = new ProjectScheduleModelBO();
                    ProjectSchedule projectSchedule = new ProjectSchedule();

                    foreach (ScheduleCapacitySearchResultBE item in
                        scheduleCapacityMeetingResults.Where(x => x.UserId == user.UserId
                        && x.StartDttm.Value.Date == user.ScheduleDate.Date).ToList())
                    {
                        projectScheduleBE = projectScheduleBO.GetByApptId(item.ApptId.Value, item.MeetingType).FirstOrDefault();
                        projectSchedule = modelBO.ConvertBEToModel(projectScheduleBE);

                        if (item.MeetingType == ProjectScheduleRefEnum.NPA.ToString())
                        {
                            totalnpahours += item.EndDttm.Value.Subtract(item.StartDttm.Value).TotalHours;

                            npas.Add(new Meeting
                            {
                                AppointmentId = item.ApptId.Value,
                                MeetingStart = item.StartDttm.Value,
                                MeetingEnd = item.EndDttm.Value,
                                MeetingName = item.MeetingName,
                                ProjectSchedule = projectSchedule
                            });
                        }
                        if (item.MeetingType == ProjectScheduleRefEnum.PMA.ToString())
                        {
                            totalpmahours += item.EndDttm.Value.Subtract(item.StartDttm.Value).TotalHours;
                            meetings.Add(new Meeting
                            {
                                AppointmentId = item.ApptId.Value,
                                MeetingStart = item.StartDttm.Value,
                                MeetingEnd = item.EndDttm.Value,
                                MeetingName = item.MeetingName,
                                MeetingType = MeetingTypeEnum.Preliminary,
                                ProjectSchedule = projectSchedule
                            });
                        }
                        if (item.MeetingType == ProjectScheduleRefEnum.EXP.ToString())
                        {
                            totalexphours += item.EndDttm.Value.Subtract(item.StartDttm.Value).TotalHours;
                            expressReservations.Add(new Meeting
                            {
                                AppointmentId = item.ApptId.Value,
                                MeetingStart = item.StartDttm.Value,
                                MeetingEnd = item.EndDttm.Value,
                                MeetingName = item.MeetingName,
                                MeetingType = MeetingTypeEnum.NA,
                                ProjectSchedule = projectSchedule
                            });
                        }
                        if (item.MeetingType == ProjectScheduleRefEnum.EMA.ToString())
                        {
                            totalemahours += item.EndDttm.Value.Subtract(item.StartDttm.Value).TotalHours;
                            expressMeetings.Add(new Meeting
                            {
                                AppointmentId = item.ApptId.Value,
                                MeetingStart = item.StartDttm.Value,
                                MeetingEnd = item.EndDttm.Value,
                                MeetingName = item.MeetingName,
                                MeetingType = MeetingTypeEnum.Express,
                                ProjectSchedule = projectSchedule
                            });
                        }
                        if (item.MeetingType == ProjectScheduleRefEnum.PR.ToString())
                        {
                            planreviews.Add(new Meeting
                            {
                                AppointmentId = item.ApptId.Value,
                                MeetingStart = item.StartDttm.Value,
                                MeetingEnd = item.EndDttm.Value,
                                MeetingName = string.IsNullOrWhiteSpace(item.MeetingName) ? "Plan Review" : item.MeetingName,
                                ProjectSchedule = projectSchedule
                            });
                            totalprhours += item.EndDttm.Value.Subtract(item.StartDttm.Value).TotalHours;
                        }
                        //if this type is none of the previous, then check if it's a facilitator meeting type
                        if (item.MeetingType == ProjectScheduleRefEnum.FMA.ToString())
                        {
                            totalfmahours += item.EndDttm.Value.Subtract(item.StartDttm.Value).TotalHours;
                            int meetingTypEnumMappingValNbr = 0;
                            bool hasMeetingName = int.TryParse(item.MeetingName, out meetingTypEnumMappingValNbr);
                            meetings.Add(new Meeting
                            {
                                AppointmentId = item.ApptId.Value,
                                MeetingStart = item.StartDttm.Value,
                                MeetingEnd = item.EndDttm.Value,
                                MeetingName = ((MeetingTypeEnum)meetingTypEnumMappingValNbr).ToStringValue(),
                                MeetingType = (MeetingTypeEnum)meetingTypEnumMappingValNbr,
                                ProjectSchedule = projectSchedule
                            });
                        }
                        if (item.MeetingType == ProjectScheduleRefEnum.FIFO.ToString())
                        {
                            fifoplanreview.Add(new Meeting
                            {
                                AppointmentId = item.ApptId.Value,
                                MeetingStart = item.StartDttm.Value,
                                MeetingEnd = item.EndDttm.Value,
                                MeetingName = string.IsNullOrWhiteSpace(item.MeetingName) ? "fifo" : item.MeetingName,
                                ProjectSchedule = projectSchedule
                            });
                            totalfifohurs += item.EndDttm.Value.Subtract(item.StartDttm.Value).TotalHours;
                        }
                    }
                    totalhours += totalemahours + totalexphours + totalfmahours + totalnpahours + totalpmahours + totalprhours + totalfifohurs;

                    expressReservations.Add(new Meeting
                    {
                        MeetingName = "Reserved: " + totalexphours.ToString()
                    });
                    expressReservations.Add(new Meeting
                    {
                        MeetingName = "Scheduled: " + totalemahours.ToString()
                    });
                    user.Meetings = meetings;
                    user.NpaMeetings = npas;
                    user.ExpressReservations = expressReservations;
                    user.ExpressMeetings = expressMeetings;
                    user.PlanReviewHours = (totalprhours + totalfifohurs).ToString();
                    user.PlanReviews = planreviews;
                    user.FIFOPlanReview = fifoplanreview;
                    user.TotalHours = totalhours;
                    user.MaxHours = GetMaxHours(search);
                }
                return results;

            }
            catch (System.Exception ex)
            {
                string errorMessage = "Error in ScheduleCapacitySearch - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw;
            }
        }

        public double GetMaxHours(ScheduleCapacitySearch search)
        {
            double hours = 0.0;
            DateTime currentdate = search.BeginDateTime;
            while (currentdate <= search.EndDateTime)
            {
                if (!IsDateHolidayOrWeekEnd(currentdate)) hours += 8;
                currentdate = NextWorkingDay(currentdate);
            }
            return hours;
        }

        public List<ScheduleCapacitySearchResult> SearchReviewerCapacity(List<ScheduleCapacitySearch> searchlist)
        {
            try
            {
                ScheduleCapacitySearchResultBO bo = new ScheduleCapacitySearchResultBO();
                List<ScheduleCapacitySearchResult> results = new List<ScheduleCapacitySearchResult>();
                UserIdentityModelBO userIdentityModelBO = new UserIdentityModelBO();
                foreach (ScheduleCapacitySearch search in searchlist)
                {
                    List<ScheduleCapacitySearchResult> searchresults = new List<ScheduleCapacitySearchResult>();

                    //do the search for each one, build the list
                    //get the meetings
                    List<ScheduleCapacitySearchResultBE> scheduleCapacityMeetingResults = bo.GetMeetingsForDateList(search.BeginDateTime, search.EndDateTime, string.Join(",", search.ReviewerSearchList));

                    foreach (ScheduleCapacitySearchResultBE item in scheduleCapacityMeetingResults)
                    {
                        //filter based on the search times since USP doesn't
                        if (!(item.EndDttm >= search.BeginDateTime && item.StartDttm <= search.EndDateTime)) continue;
                        //since in this instance we don't get the user data first, we need to fill this data
                        UserIdentity userIdentity = userIdentityModelBO.GetInstance(item.UserId);
                        item.FirstName = userIdentity.FirstName;
                        item.LastName = userIdentity.LastName;

                        ScheduleCapacitySearchResult usersearch = ConvertBEToModel(item);
                        usersearch.IsAvailable = true;

                        usersearch.IsAvailable = false;
                        if (item.MeetingType == ProjectScheduleRefEnum.NPA.ToString())
                        {
                            usersearch.NpaMeetings.Add(new Meeting
                            {
                                AppointmentId = item.ApptId.Value,
                                MeetingStart = item.StartDttm.Value,
                                MeetingEnd = item.EndDttm.Value,
                                MeetingName = item.MeetingName
                            });
                        }
                        if (item.MeetingType == ProjectScheduleRefEnum.PMA.ToString())
                        {
                            usersearch.Meetings.Add(new Meeting
                            {
                                AppointmentId = item.ApptId.Value,
                                MeetingStart = item.StartDttm.Value,
                                MeetingEnd = item.EndDttm.Value,
                                MeetingName = item.MeetingName
                            });
                        }
                        if (item.MeetingType == ProjectScheduleRefEnum.EXP.ToString())
                        {
                            usersearch.ExpressReservations.Add(new Meeting
                            {
                                AppointmentId = item.ApptId.Value,
                                MeetingStart = item.StartDttm.Value,
                                MeetingEnd = item.EndDttm.Value,
                                MeetingName = item.MeetingName
                            });
                        }
                        if (item.MeetingType == ProjectScheduleRefEnum.EMA.ToString())
                        {
                            usersearch.ExpressMeetings.Add(new Meeting
                            {
                                AppointmentId = item.ApptId.Value,
                                MeetingStart = item.StartDttm.Value,
                                MeetingEnd = item.EndDttm.Value,
                                MeetingName = item.MeetingName
                            });
                        }
                        if (item.MeetingType == ProjectScheduleRefEnum.PR.ToString())
                        {
                            usersearch.PlanReviews.Add(new Meeting
                            {
                                AppointmentId = item.ApptId.Value,
                                MeetingStart = item.StartDttm.Value,
                                MeetingEnd = item.EndDttm.Value,
                                MeetingName = string.IsNullOrWhiteSpace(item.MeetingName) ? "Plan Review" : item.MeetingName
                            });

                        }
                        //if this type is none of the previous, then check if it's a facilitator meeting type
                        if (item.MeetingType == ProjectScheduleRefEnum.FMA.ToString())
                        {

                            int meetingTypEnumMappingValNbr = 0;
                            bool hasMeetingName = int.TryParse(item.MeetingName, out meetingTypEnumMappingValNbr);
                            usersearch.Meetings.Add(new Meeting
                            {
                                AppointmentId = item.ApptId.Value,
                                MeetingStart = item.StartDttm.Value,
                                MeetingEnd = item.EndDttm.Value,
                                MeetingName = ((MeetingTypeEnum)meetingTypEnumMappingValNbr).ToStringValue(),
                                MeetingType = (MeetingTypeEnum)meetingTypEnumMappingValNbr
                            });
                        }
                        if (item.MeetingType == ProjectScheduleRefEnum.FIFO.ToString())
                        {
                            usersearch.FIFOPlanReview.Add(new Meeting
                            {
                                AppointmentId = item.ApptId.Value,
                                MeetingStart = item.StartDttm.Value,
                                MeetingEnd = item.EndDttm.Value,
                                MeetingName = string.IsNullOrWhiteSpace(item.MeetingName) ? "fifo" : item.MeetingName
                            });
                        }

                        results.Add(usersearch);
                    }
                }
                return results;

            }
            catch (System.Exception ex)
            {
                string errorMessage = "Error in SearchReviewerCapacity - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw;
            }
        }

        public List<ScheduleCapacitySearchResult> SearchPlanReviewCapacity(List<ScheduleCapacitySearch> searchlist)
        {
            try
            {
                ScheduleCapacitySearchResultBO bo = new ScheduleCapacitySearchResultBO();
                List<ScheduleCapacitySearchResult> results = new List<ScheduleCapacitySearchResult>();
                UserIdentityModelBO userIdentityModelBO = new UserIdentityModelBO();
                foreach (ScheduleCapacitySearch search in searchlist)
                {
                    List<ScheduleCapacitySearchResult> searchresults = new List<ScheduleCapacitySearchResult>();

                    //do the search for each one, build the list
                    //get the meetings
                    List<ScheduleCapacitySearchResultBE> scheduleCapacityMeetingResults = bo.GetPlanReviewsForDateList(search.BeginDateTime, search.EndDateTime, string.Join(",", search.ReviewerSearchList));

                    foreach (ScheduleCapacitySearchResultBE item in scheduleCapacityMeetingResults)
                    {

                        //since in this instance we don't get the user data first, we need to fill this data
                        UserIdentity userIdentity = userIdentityModelBO.GetInstance(item.UserId);
                        item.FirstName = userIdentity.FirstName;
                        item.LastName = userIdentity.LastName;

                        ScheduleCapacitySearchResult usersearch = ConvertBEToModel(item);

                        if (item.MeetingType == ProjectScheduleRefEnum.PR.ToString())
                        {
                            usersearch.PlanReviews.Add(new Meeting
                            {
                                AppointmentId = item.ApptId.Value,
                                MeetingStart = item.StartDttm.Value,
                                MeetingEnd = item.EndDttm.Value,
                                MeetingName = string.IsNullOrWhiteSpace(item.MeetingName) ? "Plan Review" : item.MeetingName
                            });

                        }
                        results.Add(usersearch);
                    }
                }
                return results;

            }
            catch (System.Exception ex)
            {
                string errorMessage = "Error in SearchReviewerCapacity - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw;
            }
        }

        private ScheduleCapacitySearchResult ConvertBEToModel(ScheduleCapacitySearchResultBE item)
        {
            ScheduleCapacitySearchResult result = new ScheduleCapacitySearchResult();
            result.UserId = item.UserId;
            result.FirstName = item.FirstName;
            result.LastName = item.LastName;
            result.ScheduleDate = item.ScheduleDate.HasValue ? item.ScheduleDate.Value : DateTime.Now;
            result.Meetings = new List<Meeting>();
            result.NpaMeetings = new List<Meeting>();
            result.ExpressMeetings = new List<Meeting>();
            result.ExpressReservations = new List<Meeting>();
            result.PlanReviews = new List<Meeting>();
            result.FIFOPlanReview = new List<Meeting>();
            result.IsAvailable = true;
            return result;
        }
    }
}