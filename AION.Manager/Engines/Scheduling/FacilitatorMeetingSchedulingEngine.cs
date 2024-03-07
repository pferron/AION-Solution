using AION.BL;
using AION.BL.Adapters;
using AION.Manager.Models;
using AION.Scheduler.Engine.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AION.Manager.Engines
{
    public class FacilitatorMeetingSchedulingEngine : BaseSchedulingEngine
    {
        AutoScheduledFacilitatorMeetingParams RequestData;
        public AutoSchedulableReviewer BuildingReviewerRequested { get; set; }
        public AutoSchedulableReviewer ElectricReviewerRequested { get; set; }
        public AutoSchedulableReviewer MechReviewerRequested { get; set; }
        public AutoSchedulableReviewer PlumbReviewerRequested { get; set; }
        public AutoSchedulableReviewer ZoneReviewerRequested { get; set; }
        public AutoSchedulableReviewer FireReviewerRequested { get; set; }
        public AutoSchedulableReviewer FoodServiceReviewerRequested { get; set; }
        public AutoSchedulableReviewer PoolReviewerRequested { get; set; }
        public AutoSchedulableReviewer FacilityReviewerRequested { get; set; }
        public AutoSchedulableReviewer DayCareReviewerRequested { get; set; }
        public AutoSchedulableReviewer BackFlowReviewerRequested { get; set; }

        List<DateTime?> RequestedDates = new List<DateTime?>();

        List<DateTime> _WorkingDaysForSearch = new List<DateTime>();

        DateTime _SixMonthsAhead;

        private bool _SearchedAllPossibleReservations = false;

        int _TotalMeetingTimeSlots;

        private Dictionary<int, TimeSlot> _TimeSlotsForDay;

        public FacilitatorMeetingSchedulingEngine(AutoScheduledFacilitatorMeetingParams data)
        {
            RequestData = data;
            //These days must be excluded from scheduling
            if (HolidayList == null) //keeping static since this can be called many times and mostly no need to reload saving time.
                HolidayList = GetHolidays();
            EstimationCRUDAdapter thisAdapter = new EstimationCRUDAdapter();

            _SixMonthsAhead = DateTime.Now.AddMonths(6);

            PopulateRequestedDates();
            CurrentProject = thisAdapter.GetProjectDetailsByProjectSrcSourceTxt(data.AccelaProjectIDRef);
            SetMeetingReviewers();
            CalculateMeetingDuration();
        }

        private void PopulateRequestedDates()
        {
            if (RequestData.SuggestedDate1 != null)
            {
                RequestedDates.Add(RequestData.SuggestedDate1);
            }

            if (RequestData.SuggestedDate2 != null)
            {
                RequestedDates.Add(RequestData.SuggestedDate2);
            }

            if (RequestData.SuggestedDate3 != null)
            {
                RequestedDates.Add(RequestData.SuggestedDate3);
            }
        }

        private void SetMeetingReviewers()
        {
            BuildingReviewerRequested = RequestData.BuildingUserID > 0 ? GetReviewer(RequestData.BuildingUserID) : null;
            ElectricReviewerRequested = RequestData.ElectricUserID > 0 ? GetReviewer(RequestData.ElectricUserID) : null;
            MechReviewerRequested = RequestData.MechUserID > 0 ? GetReviewer(RequestData.MechUserID) : null;
            PlumbReviewerRequested = RequestData.PlumbUserID > 0 ? GetReviewer(RequestData.PlumbUserID) : null;
            ZoneReviewerRequested = RequestData.ZoneUserID > 0 ? GetReviewer(RequestData.ZoneUserID) : null;
            FireReviewerRequested = RequestData.FireUserID > 0 ? GetReviewer(RequestData.FireUserID) : null;
            FoodServiceReviewerRequested = RequestData.FoodServiceUserID > 0 ? GetReviewer(RequestData.FoodServiceUserID) : null;
            PoolReviewerRequested = RequestData.PoolUserID > 0 ? GetReviewer(RequestData.PoolUserID) : null;
            FacilityReviewerRequested = RequestData.FacilityUserID > 0 ? GetReviewer(RequestData.FacilityUserID) : null;
            DayCareReviewerRequested = RequestData.DayCareUserID > 0 ? GetReviewer(RequestData.DayCareUserID) : null;
            BackFlowReviewerRequested = RequestData.BackFlowUserID > 0 ? GetReviewer(RequestData.BackFlowUserID) : null;
        }

        private AutoSchedulableReviewer GetReviewer(int id)
        {
            UserAdapter userAdapter = new UserAdapter();

            UserIdentity userIdentity = userAdapter.GetUserIdentityByID(id);

            List<TimeSlot> currentMeetings = userAdapter.GetUsedTimeSlotsByUserID(userIdentity.ID);
            currentMeetings.OrderBy((x) => x.StartTime);

            List<TimeSlot> wipTimeSlots = currentMeetings.SelectMany(x => SplitTimeSlotByTimeSlotIntervalMinsHalfTime(x)).ToList();
            wipTimeSlots.OrderBy((x) => x.StartTime);

            AutoSchedulableReviewer reviewer = new AutoSchedulableReviewer()
            {
                UserIdentity = userIdentity,
                CurrentMeetings = currentMeetings,
                WIPTimeSlots = wipTimeSlots
            };

            return reviewer;
        }

        private void CalculateMeetingDuration()
        {
            int hours = int.Parse(RequestData.DurationHours);
            int minutes = int.Parse(RequestData.DurationMinutes);

            int meetingInMinutes = (hours * 60) + minutes;

            _TotalMeetingTimeSlots = meetingInMinutes / TimeSlotIntervalByMinutes;
        }

        public AutoScheduledFacilitatorMeetingValues GetAutoEstimatedValues()
        {
            GenerateWorkDaysForSearch();

            AutoScheduledFacilitatorMeetingValues returnValues = new AutoScheduledFacilitatorMeetingValues();
            returnValues.DurationHours = RequestData.DurationHours;
            returnValues.DurationMinutes = RequestData.DurationMinutes;

            returnValues = SelectReviewersWithinDateRange();

            bool hasRequiredValues = HasRequiredValues(returnValues);

            while (!hasRequiredValues && !_SearchedAllPossibleReservations)
            {
                //get max workday searched and find next workday start date

                DateTime startDate = NextWorkingDay(_WorkingDaysForSearch.Last());
                BatchWorkDaysTwoWeeksAtATime(startDate);
                returnValues = SelectReviewersWithinDateRange();
                hasRequiredValues = HasRequiredValues(returnValues);
            }

            returnValues.MeetingRoomId = -1;

            return returnValues;
        }

        private AutoScheduledFacilitatorMeetingValues SelectReviewersWithinDateRange()
        {
            AutoScheduledFacilitatorMeetingValues returnValues = new AutoScheduledFacilitatorMeetingValues();

            for (int i = 0; i < _WorkingDaysForSearch.Count(); i++)
            {
                returnValues = SelectReviewers(_WorkingDaysForSearch[i]);
                if (HasRequiredValues(returnValues))
                {
                    break;
                }
            }

            return returnValues;
        }

        private AutoScheduledFacilitatorMeetingValues SelectReviewers(DateTime meetingDate)
        {
            _TimeSlotsForDay = CreateAllTimeSlotsForTheDay(meetingDate);

            AutoScheduledFacilitatorMeetingValues ret = new AutoScheduledFacilitatorMeetingValues();

            List<KeyValuePair<int, TimeSlot>> buildingDayTimeSlots = new List<KeyValuePair<int, TimeSlot>>();
            List<KeyValuePair<int, TimeSlot>> electricalDayTimeSlots = new List<KeyValuePair<int, TimeSlot>>();
            List<KeyValuePair<int, TimeSlot>> mechanicalDayTimeSlots = new List<KeyValuePair<int, TimeSlot>>();
            List<KeyValuePair<int, TimeSlot>> plumbingDayTimeSlots = new List<KeyValuePair<int, TimeSlot>>();
            List<KeyValuePair<int, TimeSlot>> zoneDayTimeSlots = new List<KeyValuePair<int, TimeSlot>>();
            List<KeyValuePair<int, TimeSlot>> fireDayTimeSlots = new List<KeyValuePair<int, TimeSlot>>();
            List<KeyValuePair<int, TimeSlot>> backflowDayTimeSlots = new List<KeyValuePair<int, TimeSlot>>();
            List<KeyValuePair<int, TimeSlot>> foodDayTimeSlots = new List<KeyValuePair<int, TimeSlot>>();
            List<KeyValuePair<int, TimeSlot>> poolDayTimeSlots = new List<KeyValuePair<int, TimeSlot>>();
            List<KeyValuePair<int, TimeSlot>> facilityDayTimeSlots = new List<KeyValuePair<int, TimeSlot>>();
            List<KeyValuePair<int, TimeSlot>> dayCareDayTimeSlots = new List<KeyValuePair<int, TimeSlot>>();


            if (BuildingReviewerRequested != null)
            {
                List<TimeSlot> buildingDayTimeSlotsUsed = BuildingReviewerRequested.WIPTimeSlots.Where(x => x.StartTime.Date == meetingDate.Date).ToList();

                buildingDayTimeSlots = _TimeSlotsForDay.Where(a => !buildingDayTimeSlotsUsed.Any(b => a.Value.StartTime == b.StartTime && a.Value.EndTime == b.EndTime)).ToList();
                if (buildingDayTimeSlots.Count() == 0 || buildingDayTimeSlots.Count() < _TotalMeetingTimeSlots)
                {
                    ret.BuildingUserID = -1;
                }
            }

            if (ElectricReviewerRequested != null)
            {
                List<TimeSlot> electricalDayTimeSlotsUsed = ElectricReviewerRequested.WIPTimeSlots.Where(x => x.StartTime.Date == meetingDate.Date).ToList();

                electricalDayTimeSlots = _TimeSlotsForDay.Where(a => !electricalDayTimeSlotsUsed.Any(b => a.Value.StartTime == b.StartTime && a.Value.EndTime == b.EndTime)).ToList();
                if (electricalDayTimeSlots.Count() == 0 || electricalDayTimeSlots.Count() < _TotalMeetingTimeSlots)
                {
                    ret.ElectricUserID = -1;
                }
            }

            if (MechReviewerRequested != null)
            {
                List<TimeSlot> mechanicalDayTimeSlotsUsed = MechReviewerRequested.WIPTimeSlots.Where(x => x.StartTime.Date == meetingDate.Date).ToList();

                mechanicalDayTimeSlots = _TimeSlotsForDay.Where(a => !mechanicalDayTimeSlotsUsed.Any(b => a.Value.StartTime == b.StartTime && a.Value.EndTime == b.EndTime)).ToList();
                if (mechanicalDayTimeSlots.Count() == 0 || mechanicalDayTimeSlots.Count() < _TotalMeetingTimeSlots)
                {
                    ret.MechUserID = -1;
                }
            }

            if (PlumbReviewerRequested != null)
            {
                List<TimeSlot> plumbingDayTimeSlotsUsed = PlumbReviewerRequested.WIPTimeSlots.Where(x => x.StartTime.Date == meetingDate.Date).ToList();

                plumbingDayTimeSlots = _TimeSlotsForDay.Where(a => !plumbingDayTimeSlotsUsed.Any(b => a.Value.StartTime == b.StartTime && a.Value.EndTime == b.EndTime)).ToList();
                if (plumbingDayTimeSlots.Count() == 0 || plumbingDayTimeSlots.Count() < _TotalMeetingTimeSlots)
                {
                    ret.PlumbUserID = -1;
                }
            }

            if (ZoneReviewerRequested != null)
            {
                List<TimeSlot> zoneDayTimeSlotsUsed = ZoneReviewerRequested.WIPTimeSlots.Where(x => x.StartTime.Date == meetingDate.Date).ToList();

                zoneDayTimeSlots = _TimeSlotsForDay.Where(a => !zoneDayTimeSlotsUsed.Any(b => a.Value.StartTime == b.StartTime && a.Value.EndTime == b.EndTime)).ToList();
                if (zoneDayTimeSlots.Count() == 0 || zoneDayTimeSlots.Count() < _TotalMeetingTimeSlots)
                {
                    ret.ZoneUserID = -1;
                }
            }

            if (FireReviewerRequested != null)
            {
                List<TimeSlot> fireDayTimeSlotsUsed = FireReviewerRequested.WIPTimeSlots.Where(x => x.StartTime.Date == meetingDate.Date).ToList();

                fireDayTimeSlots = _TimeSlotsForDay.Where(a => !fireDayTimeSlotsUsed.Any(b => a.Value.StartTime == b.StartTime && a.Value.EndTime == b.EndTime)).ToList();
                if (fireDayTimeSlots.Count() == 0 || fireDayTimeSlots.Count() < _TotalMeetingTimeSlots)
                {
                    ret.FireUserID = -1;
                }
            }

            if (BackFlowReviewerRequested != null)
            {
                List<TimeSlot> backflowDayTimeSlotsUsed = BackFlowReviewerRequested.WIPTimeSlots.Where(x => x.StartTime.Date == meetingDate.Date).ToList();

                backflowDayTimeSlots = _TimeSlotsForDay.Where(a => !backflowDayTimeSlotsUsed.Any(b => a.Value.StartTime == b.StartTime && a.Value.EndTime == b.EndTime)).ToList();
                if (backflowDayTimeSlots.Count() == 0 || backflowDayTimeSlots.Count() < _TotalMeetingTimeSlots)
                {
                    ret.BackFlowUserID = -1;
                }
            }

            if (FoodServiceReviewerRequested != null)
            {
                List<TimeSlot> foodDayTimeSlotsUsed = FoodServiceReviewerRequested.WIPTimeSlots.Where(x => x.StartTime.Date == meetingDate.Date).ToList();

                foodDayTimeSlots = _TimeSlotsForDay.Where(a => !foodDayTimeSlotsUsed.Any(b => a.Value.StartTime == b.StartTime && a.Value.EndTime == b.EndTime)).ToList();
                if (foodDayTimeSlots.Count() == 0 || foodDayTimeSlots.Count() < _TotalMeetingTimeSlots)
                {
                    ret.FoodServiceUserID = -1;
                }
            }

            if (PoolReviewerRequested != null)
            {
                List<TimeSlot> poolDayTimeSlotsUsed = PoolReviewerRequested.WIPTimeSlots.Where(x => x.StartTime.Date == meetingDate.Date).ToList();

                poolDayTimeSlots = _TimeSlotsForDay.Where(a => !poolDayTimeSlotsUsed.Any(b => a.Value.StartTime == b.StartTime && a.Value.EndTime == b.EndTime)).ToList();
                if (poolDayTimeSlots.Count() == 0 || poolDayTimeSlots.Count() < _TotalMeetingTimeSlots)
                {
                    ret.PoolUserID = -1;
                }
            }

            if (FacilityReviewerRequested != null)
            {
                List<TimeSlot> facilityDayTimeSlotsUsed = FacilityReviewerRequested.WIPTimeSlots.Where(x => x.StartTime.Date == meetingDate.Date).ToList();

                facilityDayTimeSlots = _TimeSlotsForDay.Where(a => !facilityDayTimeSlotsUsed.Any(b => a.Value.StartTime == b.StartTime && a.Value.EndTime == b.EndTime)).ToList();
                if (facilityDayTimeSlots.Count() == 0 || facilityDayTimeSlots.Count() < _TotalMeetingTimeSlots)
                {
                    ret.FacilityUserID = -1;
                }
            }

            if (DayCareReviewerRequested != null)
            {
                List<TimeSlot> dayCareDayTimeSlotsUsed = DayCareReviewerRequested.WIPTimeSlots.Where(x => x.StartTime.Date == meetingDate.Date).ToList();

                dayCareDayTimeSlots = _TimeSlotsForDay.Where(a => !dayCareDayTimeSlotsUsed.Any(b => a.Value.StartTime == b.StartTime && a.Value.EndTime == b.EndTime)).ToList();
                if (dayCareDayTimeSlots.Count() == 0 || dayCareDayTimeSlots.Count() < _TotalMeetingTimeSlots)
                {
                    ret.DayCareUserID = -1;
                }
            }

            var dayTimeSlots = new List<List<KeyValuePair<int, TimeSlot>>>();

            if (buildingDayTimeSlots.Any() && buildingDayTimeSlots.Count() > _TotalMeetingTimeSlots)
            {
                ret.BuildingUserID = BuildingReviewerRequested.UserIdentity.ID;
                dayTimeSlots.Add(buildingDayTimeSlots);
            }

            if (electricalDayTimeSlots.Any() && electricalDayTimeSlots.Count() > _TotalMeetingTimeSlots)
            {
                ret.ElectricUserID = ElectricReviewerRequested.UserIdentity.ID;
                dayTimeSlots.Add(electricalDayTimeSlots);
            }

            if (mechanicalDayTimeSlots.Any() && mechanicalDayTimeSlots.Count() > _TotalMeetingTimeSlots)
            {
                ret.MechUserID = MechReviewerRequested.UserIdentity.ID;
                dayTimeSlots.Add(mechanicalDayTimeSlots);
            }

            if (plumbingDayTimeSlots.Any() && plumbingDayTimeSlots.Count() > _TotalMeetingTimeSlots)
            {
                ret.PlumbUserID = PlumbReviewerRequested.UserIdentity.ID;
                dayTimeSlots.Add(plumbingDayTimeSlots);
            }

            if (zoneDayTimeSlots.Any() && zoneDayTimeSlots.Count() > _TotalMeetingTimeSlots)
            {
                ret.ZoneUserID = ZoneReviewerRequested.UserIdentity.ID;
                dayTimeSlots.Add(zoneDayTimeSlots);
            }

            if (fireDayTimeSlots.Any() && fireDayTimeSlots.Count() > _TotalMeetingTimeSlots)
            {
                ret.FireUserID = FireReviewerRequested.UserIdentity.ID;
                dayTimeSlots.Add(fireDayTimeSlots);
            }

            if (backflowDayTimeSlots.Any() && backflowDayTimeSlots.Count() > _TotalMeetingTimeSlots)
            {
                ret.BackFlowUserID = BackFlowReviewerRequested.UserIdentity.ID;
                dayTimeSlots.Add(backflowDayTimeSlots);
            }

            if (foodDayTimeSlots.Any() && foodDayTimeSlots.Count() > _TotalMeetingTimeSlots)
            {
                ret.FoodServiceUserID = FoodServiceReviewerRequested.UserIdentity.ID;
                dayTimeSlots.Add(foodDayTimeSlots);
            }

            if (poolDayTimeSlots.Any() && poolDayTimeSlots.Count() > _TotalMeetingTimeSlots)
            {
                ret.PoolUserID = PoolReviewerRequested.UserIdentity.ID;
                dayTimeSlots.Add(poolDayTimeSlots);
            }

            if (facilityDayTimeSlots.Any() && facilityDayTimeSlots.Count() > _TotalMeetingTimeSlots)
            {
                ret.FacilityUserID = FacilityReviewerRequested.UserIdentity.ID;
                dayTimeSlots.Add(facilityDayTimeSlots);
            }

            if (dayCareDayTimeSlots.Any() && dayCareDayTimeSlots.Count() > _TotalMeetingTimeSlots)
            {
                ret.DayCareUserID = DayCareReviewerRequested.UserIdentity.ID;
                dayTimeSlots.Add(dayCareDayTimeSlots);
            }

            if (dayTimeSlots.Count() > 0)
            {
                var commonTimeSlots = GetCommonItems(dayTimeSlots);

                var min = commonTimeSlots.OrderBy(kvp => kvp.Key).First();
                var max = commonTimeSlots.OrderBy(kvp => kvp.Key).Last();
                List<int> listofAllKeys = commonTimeSlots.Select(kvp => kvp.Key).ToList();

                List<int> consecutiveTimeSlots = new List<int>();
                int prevNumber = 0;

                for (int number = min.Key; number <= max.Key; number++)
                {
                    if (number == min.Key + prevNumber)
                    {
                        prevNumber++;
                        if (listofAllKeys.Contains(number))
                        {
                            consecutiveTimeSlots.Add(number);

                            if (prevNumber >= _TotalMeetingTimeSlots)
                            {
                                var consecutiveTimeSlotsMin = commonTimeSlots.First(kvp => kvp.Key == consecutiveTimeSlots.Min());
                                var consecutiveTimeSlotsMax = commonTimeSlots.First(kvp => kvp.Key == consecutiveTimeSlots.Max());

                                //set dept values

                                ret.SelectedStartDateTime = consecutiveTimeSlotsMin.Value.StartTime;
                                ret.SelectedEndDateTime = consecutiveTimeSlotsMax.Value.EndTime;

                                break;
                            }
                        }
                        else
                        {
                            break; // then not consecutive
                        }
                    }
                }
                return ret;
            }

            return ret;
        }

        private bool HasRequiredValues(AutoScheduledFacilitatorMeetingValues returnValues)
        {
            bool hasRequiredValues = true;

            if (BuildingReviewerRequested != null && returnValues.BuildingUserID <= 0)
            {
                returnValues.BuildingUserID = -1;
                hasRequiredValues = false;
            }

            if (ElectricReviewerRequested != null && returnValues.ElectricUserID <= 0)
            {
                returnValues.ElectricUserID = -1;
                hasRequiredValues = false;
            }

            if (MechReviewerRequested != null && returnValues.MechUserID <= 0)
            {
                returnValues.MechUserID = -1;
                hasRequiredValues = false;
            }

            if (PlumbReviewerRequested != null && returnValues.PlumbUserID <= 0)
            {
                returnValues.PlumbUserID = -1;
                hasRequiredValues = false;
            }

            if (ZoneReviewerRequested != null && returnValues.ZoneUserID <= 0)
            {
                returnValues.ZoneUserID = -1;
                hasRequiredValues = false;
            }

            if (FireReviewerRequested != null && returnValues.FireUserID <= 0)
            {
                returnValues.FireUserID = -1;
                hasRequiredValues = false;
            }

            if (FoodServiceReviewerRequested != null && returnValues.FoodServiceUserID <= 0)
            {
                returnValues.FoodServiceUserID = -1;
                hasRequiredValues = false;
            }

            if (PoolReviewerRequested != null && returnValues.PoolUserID <= 0)
            {
                returnValues.PoolUserID = -1;
                hasRequiredValues = false;
            }

            if (FacilityReviewerRequested != null && returnValues.FacilityUserID <= 0)
            {
                returnValues.FacilityUserID = -1;
                hasRequiredValues = false;
            }

            if (DayCareReviewerRequested != null && returnValues.DayCareUserID <= 0)
            {
                returnValues.DayCareUserID = -1;
                hasRequiredValues = false;
            }

            if (BackFlowReviewerRequested != null && returnValues.BackFlowUserID <= 0)
            {
                returnValues.BackFlowUserID = -1;
                hasRequiredValues = false;
            }

            if (returnValues.SelectedStartDateTime == DateTime.MinValue || returnValues.SelectedEndDateTime == DateTime.MinValue)
            {
                hasRequiredValues = false;
            }

            return hasRequiredValues;
        }

        private void GenerateWorkDaysForSearch()
        {
            if (RequestedDates != null && RequestedDates.Count() > 0)
            {
                DateTime maxRequestedDate = (DateTime)RequestedDates.Max();
                if (maxRequestedDate > DateTime.Now)
                {
                    for (int i = 0; i < RequestedDates.Count; i++)
                    {
                        if (RequestedDates[i] != null && !IsDateHolidayOrWeekEnd((DateTime)RequestedDates[i]))
                        {
                            _WorkingDaysForSearch.Add((DateTime)RequestedDates[i]);
                        }
                    }
                }
                else
                {
                    DateTime startDate = DateTime.Now.AddDays(3);
                    BatchWorkDaysTwoWeeksAtATime(startDate);
                }
            }
            else
            {
                DateTime startDate = DateTime.Now.AddDays(3);
                BatchWorkDaysTwoWeeksAtATime(startDate);
            }
        }

        private void BatchWorkDaysTwoWeeksAtATime(DateTime startDate)
        {
            _WorkingDaysForSearch.Clear();

            DateTime nextDay = startDate.AddDays(-1); // readjust because incremented in NextWorkingDay;

            for (int i = 1; i <= 10; i++)
            {
                nextDay = NextWorkingDay(nextDay);
                if (nextDay <= _SixMonthsAhead)
                {
                    _WorkingDaysForSearch.Add(nextDay);
                }
                else
                {
                    _SearchedAllPossibleReservations = true;
                    break;
                }
            }
        }

        private new Dictionary<int, TimeSlot> CreateAllTimeSlotsForTheDay(DateTime startDt)
        {
            Dictionary<int, TimeSlot> ret = new Dictionary<int, TimeSlot>();
            DateTime current = startDt.Date.AddHours(8); //starts at 8 am
            int i = 1;
            do //8 AM to 5 PM 
            {
                TimeSlot t = new TimeSlot();
                t.AllocationType = TimeAllocationType.NA;
                t.StartTime = current;
                current = current.AddMinutes(TimeSlotIntervalByMinutes);
                t.EndTime = current;
                if (i == 17 || i == 18 || i == 19 || i == 20) { i = i + 1; continue; }// skip 12-1PM for lunch break
                ret.Add(i, t);
                i++;
            } while (current != startDt.Date.AddHours(17));
            return ret;
        }

        static IEnumerable<T> GetCommonItems<T>(List<List<T>> lists)
        {
            HashSet<T> hs = new HashSet<T>(lists.First());
            for (int i = 1; i < lists.Count; i++)
                hs.IntersectWith(lists[i]);
            return hs;
        }
    }
}