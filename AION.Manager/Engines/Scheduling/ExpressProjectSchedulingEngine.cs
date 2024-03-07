
using AION.BL;
using AION.BL.Adapters;
using AION.BL.BusinessObjects;
using AION.BL.Common;
using AION.BL.Models.Base;
using AION.Engine.BusinessEntities;
using AION.Manager.Helpers;
using AION.Manager.Models;
using AIONEstimator.Engine.BusinessObjects;
using Meck.Shared.Accela;
using System;
using System.Collections.Generic;
using System.Linq;
using TimeSlot = AION.Scheduler.Engine.BusinessEntities.TimeSlot;

namespace AION.Manager.Engines
{
    public class ExpressProjectSchedulingEngine : BaseSchedulingEngine
    {
        AutoScheduledExpressParams RequestData;
        public AutoScheduledExpressValues AutoScheduledValues { get; private set; }
        bool IsManualDateRequest { get; set; } = false;
        List<RequestedExpressDateBE> RequestedDates { get; set; } = new List<RequestedExpressDateBE>();
        public DateTime AutoSchedulePeriodStart { get; set; }
        public DateTime AllowedMaxEndDate { get; set; }

        public List<UserScheduleBE> ExpressUserSchedules { get; set; }

        private List<UserScheduleBE> EligibleElectricReviewerSchedules { get; set; }
        private List<UserScheduleBE> EligibleMechReviewerSchedules { get; set; }
        private List<UserScheduleBE> EligibleBuildingReviewerSchedules { get; set; }
        private List<UserScheduleBE> EligiblePlumbReviewerSchedules { get; set; }
        private List<UserScheduleBE> EligibleZoneReviewerSchedules { get; set; }
        private List<UserScheduleBE> EligibleFireReviewerSchedules { get; set; }
        private List<UserScheduleBE> EligibleFoodServiceReviewerSchedules { get; set; }
        private List<UserScheduleBE> EligiblePoolReviewerSchedules { get; set; }
        private List<UserScheduleBE> EligibleFacilityReviewerSchedules { get; set; }
        private List<UserScheduleBE> EligibleDayCareReviewerSchedules { get; set; }
        private List<UserScheduleBE> EligibleBackFlowReviewerSchedules { get; set; }

        public ExpressProjectSchedulingEngine(AutoScheduledExpressParams data)
        {
            RequestData = data;

            if (HolidayList == null)
                HolidayList = GetHolidays();

            EstimationCRUDAdapter thisAdapter = new EstimationCRUDAdapter();
            CurrentProject = thisAdapter.GetProjectDetailsByProjectSrcSourceTxt(data.AccelaProjectIDRef);

            SetCycleData(RequestData.Cycle);

            SetRequestedDates();
            
            if (RequestData.ManualStartDateTime != null && RequestData.ManualStartDateTime.HasValue)
            {
                IsManualDateRequest = true;
            }

            AutoSchedulePeriodStart = SetAutoSchedulePeriodStart();
            AllowedMaxEndDate = SetAllowedMaxEndDate();

            SetUpEligibleReviewers();

            CalculateMeetingDuration();

            AutoScheduledValues = GetAutoEstimatedValues();

            if (!string.IsNullOrEmpty(AutoScheduledValues.ErrorMessage) && AllowedMaxEndDate > DateTime.Now.AddYears(2))
            {
                return;
            }

            while (!IsManualDateRequest && !AutoScheduledValues.IsReadyForScheduling && AllowedMaxEndDate <= DateTime.Now.AddYears(2))
            {
                AutoSchedulePeriodStart = NextWorkingDay(AllowedMaxEndDate);
                AllowedMaxEndDate = SetAllowedMaxEndDate();

                //get update reviewer list
                SetUpEligibleReviewers();

                AutoScheduledValues = GetAutoEstimatedValues();
            }

            if (AutoScheduledValues.IsReadyForScheduling)
            {
                AutoScheduledValues.MeetingRoomId = GetReservedExpressMeetingRoom(AutoScheduledValues.SelectedStartDateTime);
            }
            else
            {
                AutoScheduledValues.ErrorMessage = "Please verify that you have plan reviewers configured and available for each reservation.";
            }
        }

        public ExpressProjectSchedulingEngine(AutoScheduleReportParams data)
        {
            //for reporting, just need the first time slot for each trade/agency
            RequestData = data;

            if (HolidayList == null)
                HolidayList = GetHolidays();

            CurrentProject = RequestData.CurrentProject;

            RequestedDates = CurrentProject.AccelaRequestedExpressDates;

            AutoSchedulePeriodStart = NextWorkingDay(data.ManualStartDateTime.Value);
            AllowedMaxEndDate = SetAllowedMaxEndDateForReport();

            SetUpEligibleReviewers();

            MeetingDuration = data.ReviewHours;

            BusinessTimeSlots = GetExpressAutoEstimatedValuesForReport();
        }

        public ExpressProjectSchedulingEngine(DateTime start, DateTime end, string SrcTxt)
        {
            if (HolidayList == null)
                HolidayList = GetHolidays();

            EstimationCRUDAdapter thisAdapter = new EstimationCRUDAdapter();
            CurrentProject = thisAdapter.GetProjectDetailsByProjectSrcSourceTxt(SrcTxt);

            SetCycleData(CurrentProject.CycleNbr.Value);

            AutoSchedulePeriodStart = SetDateRangePeriodStart(start);
            AllowedMaxEndDate = end;

            SetUpEligibleReviewers();

            AvailableDateForRequestExpress = GetFiveAvailDate(start, end);
        }

        private void SetUpEligibleReviewers()
        {
            AllEligibleReviewers = GetAllEligibleReviewers();
            SplitEligibleReviewersByDept();
        }

        public void SplitExpressUserSchedulesByDepartment()
        {
            List<Department> departments = new DepartmentModelBO().BaseList.ToList();

            Helper helper = new Helper();

            foreach (var item in ExpressUserSchedules)
            {
                Department department = departments.FirstOrDefault(x => x.ID == item.BusinessRefID);

                if (department.DepartmentEnum == DepartmentNameEnums.Electrical)
                {
                    EligibleElectricReviewerSchedules.Add(item);
                }

                if (department.DepartmentEnum == DepartmentNameEnums.Mechanical)
                {
                    EligibleMechReviewerSchedules.Add(item);
                }

                if (department.DepartmentEnum == DepartmentNameEnums.Building)
                {
                    EligibleBuildingReviewerSchedules.Add(item);
                }

                if (department.DepartmentEnum == DepartmentNameEnums.Plumbing)
                {
                    EligiblePlumbReviewerSchedules.Add(item);
                }

                if (department.DepartmentEnum != DepartmentNameEnums.Zone_Huntersville
                 && department.DepartmentEnum != DepartmentNameEnums.Zone_Mint_Hill
                 && department.DepartmentEnum != DepartmentNameEnums.Zone_Cty_Chrlt
                 && helper.CountyZoneDepartmentNames.Contains(department.DepartmentEnum))
                {
                }
                else
                {
                    EligibleZoneReviewerSchedules.Add(item);
                }

                if (department.DepartmentEnum == DepartmentNameEnums.Fire_Cty_Chrlt
                 || helper.CountyFireDepartmentNames.Contains(department.DepartmentEnum))
                {
                    EligibleFireReviewerSchedules.Add(item);
                }

                if (department.DepartmentEnum == DepartmentNameEnums.EH_Food)
                {
                    EligibleFoodServiceReviewerSchedules.Add(item);
                }

                if (department.DepartmentEnum == DepartmentNameEnums.EH_Pool)
                {
                    EligiblePoolReviewerSchedules.Add(item);
                }

                if (department.DepartmentEnum == DepartmentNameEnums.EH_Facilities)
                {
                    EligibleFacilityReviewerSchedules.Add(item);
                }

                if (department.DepartmentEnum == DepartmentNameEnums.EH_Day_Care)
                {
                    EligibleDayCareReviewerSchedules.Add(item);
                }

                if (department.DepartmentEnum == DepartmentNameEnums.Backflow)
                {
                    EligibleBackFlowReviewerSchedules.Add(item);
                }
            }
        }

        public override List<AutoSchedulableReviewer> GetAllEligibleReviewers()
        {
            UserAdapter userAdapter = new UserAdapter();

            UserIdentityModelBO bo = new UserIdentityModelBO();

            ExpressAdapter expressAdapter = new ExpressAdapter();

            List<Department> departments = new DepartmentModelBO().BaseList.ToList();

            List<AutoSchedulableReviewer> reviewers = new List<AutoSchedulableReviewer>();

            List<AutoSchedulableReviewer> ConsolidatedReviewers = new List<AutoSchedulableReviewer>();

            List<ExpressSearchResult> reservations = expressAdapter.GetReservationByDate(AutoSchedulePeriodStart, AllowedMaxEndDate);

            List<UserScheduleBE> allExpressSchedules = new List<UserScheduleBE>();

            foreach (ExpressSearchResult reservation in reservations)
            {
                // get user schedules for reservation
                UserScheduleBO userScheduleBO = new UserScheduleBO();
                List<UserScheduleBE> userSchedules = userScheduleBO.GetListByScheduleID(reservation.ProjectScheduleID);
                allExpressSchedules.AddRange(userSchedules);
            }

            var distinctReviewers = allExpressSchedules.GroupBy(x => x.UserID)
                   .Select(grp => grp.First())
                   .ToList();

            foreach (UserScheduleBE distinctReviewerSchedule in distinctReviewers)
            {
                List<Department> userDepartmentsAssigned = new List<Department>();

                AutoSchedulableReviewer reviewer = new AutoSchedulableReviewer()
                {
                    UserIdentity = userAdapter.GetUserIdentityByID(distinctReviewerSchedule.UserID.Value)
                };

                foreach (var schedule in allExpressSchedules)
                {
                    if (schedule.UserID == distinctReviewerSchedule.UserID.Value)
                    {
                        Department department = departments.Where(x => x.ID == schedule.BusinessRefID).FirstOrDefault();

                        userDepartmentsAssigned.Add(department);
                    }
                }

                reviewer.UserIdentity.DesignatedDepartments = userDepartmentsAssigned.GroupBy(x => x.ID).Select(group => group.First()).ToList();

                ConsolidatedReviewers.Add(reviewer);
            }

            for (int i = 0; i < ConsolidatedReviewers.Count; i++)
            {
                decimal totalHoursForCapacity = 0M;
                ConsolidatedReviewers[i].CurrentMeetings = userAdapter.GetUsedTimeSlotsExtrasByUserID(ConsolidatedReviewers[i].UserIdentity.ID);
                if (ConsolidatedReviewers[i].CurrentMeetings != null)
                {
                    ConsolidatedReviewers[i].CurrentMeetings = AdjustReservedTimeSlots(ConsolidatedReviewers[i].CurrentMeetings);
                    totalHoursForCapacity = GetTotalHoursForCapacity(ConsolidatedReviewers[i].CurrentMeetings);

                }
                ConsolidatedReviewers[i].TotalHoursForCapacity = totalHoursForCapacity;
            }

            return ConsolidatedReviewers;
        }

        private List<TimeSlot> GetExpressAutoEstimatedValuesForReport()
        {
            List<TimeSlot> allReviewerReservations = GetEligibleReviewerExpressReservations(AllEligibleReviewers, MeetingDuration);

            List<DateTime> reservationDates = GetDaysOfReservations(allReviewerReservations);

            // iterate through possible reservation dates to find reviewers
            List<TimeSlot> timeSlots = new List<TimeSlot>();
            bool isComplete = true;
            //if all timeslots filled, break
            foreach (ProjectAgency item in CurrentProject.Agencies.ToList())
            {
                if (timeSlots.Where(x => x.DepartmentName == item.DepartmentInfo).Any() == false)
                {
                    foreach (DateTime reservationDate in reservationDates.OrderBy(x => x))
                    {
                        if (timeSlots.Where(x => x.DepartmentName == item.DepartmentInfo).Any() == false)
                        {
                            List<TimeSlot> reviewerReservations = new List<TimeSlot>();

                            ProjectAgency curtrade = CurrentProject.Agencies.Where(x => x.DepartmentInfo == item.DepartmentInfo).FirstOrDefault();

                            reviewerReservations.AddRange(GetEligibleReviewerExpressReservations(GetTradeReviewersForReport(curtrade.DepartmentInfo), MeetingDuration, curtrade));

                            isComplete = false;
                            TimeSlot reviewerts = reviewerReservations.Where(x => x.StartTime.Date == reservationDate.Date).FirstOrDefault();
                            if (reviewerts != null)
                            {
                                TimeSlot ts = new TimeSlot
                                {
                                    DepartmentName = item.DepartmentInfo,
                                    StartTime = reviewerts.StartTime,
                                    EndTime = reviewerts.EndTime,
                                    TotalTimeOfProject = reviewerts.EndTime - reviewerts.StartTime
                                };

                                isComplete = true;
                                timeSlots.Add(ts);
                            }
                            if (isComplete) break;
                        }
                    }
                }

            }

            isComplete = true;
            foreach (ProjectTrade item in CurrentProject.Trades.ToList())
            {
                if (timeSlots.Where(x => x.DepartmentName == item.DepartmentInfo).Any() == false)
                {
                    foreach (DateTime reservationDate in reservationDates.OrderBy(x => x))
                    {
                        if (timeSlots.Where(x => x.DepartmentName == item.DepartmentInfo).Any() == false)
                        {
                            List<TimeSlot> reviewerReservations = new List<TimeSlot>();

                            ProjectTrade curtrade = CurrentProject.Trades.Where(x => x.DepartmentInfo == item.DepartmentInfo).FirstOrDefault();

                            reviewerReservations.AddRange(GetEligibleReviewerExpressReservations(GetTradeReviewersForReport(curtrade.DepartmentInfo), MeetingDuration, curtrade));

                            isComplete = false;

                            TimeSlot reviewerts = reviewerReservations.Where(x => x.StartTime.Date == reservationDate.Date).FirstOrDefault();
                            if (reviewerts != null)
                            {
                                TimeSlot ts = new TimeSlot
                                {
                                    DepartmentName = item.DepartmentInfo,
                                    StartTime = reviewerts.StartTime,
                                    EndTime = reviewerts.EndTime,
                                    TotalTimeOfProject = reviewerts.EndTime - reviewerts.StartTime
                                };

                                isComplete = true;
                                timeSlots.Add(ts);
                            }

                            if (isComplete) break;
                        }
                    }
                }

            }

            return timeSlots;
        }

        public AutoScheduledExpressValues GetAutoEstimatedValues()
        {
            AutoScheduledExpressValues ret = new AutoScheduledExpressValues();

            Helper helper = new Helper();

            if (AllEligibleReviewers.Count == 0)
            {
                ret.ErrorMessage = "No Eligible Reviewers Found! Check user settings for scheduling eligibility.";
                return ret;
            }

            List<TimeSlot> allReviewerReservations = GetEligibleReviewerExpressReservations(AllEligibleReviewers, MeetingDuration);

            List<DateTime> reservationDates = GetDaysOfReservations(allReviewerReservations);

            bool buildingRequested = false;
            bool electricRequested = false;
            bool mechRequested = false;
            bool plumbRequested = false;
            bool zoningRequested = false;
            bool fireRequested = false;
            bool backflowRequested = false;
            bool poolRequested = false;
            bool facilityRequested = false;
            bool foodRequested = false;
            bool daycareRequested = false;

            //Building
            List<TimeSlot> buildingReviewerReservations = new List<TimeSlot>();

            ProjectDepartment curtrade = CurrentProject.Trades.Where(x => x.DepartmentInfo == DepartmentNameEnums.Building).FirstOrDefault();
            if (curtrade == null || curtrade.EstimationNotApplicable == true || EligibleBuildingReviewersList == null)
            {
                ret.BuildingUserID = -1;
            }
            else
            {
                buildingRequested = true;
                buildingReviewerReservations.AddRange(GetEligibleReviewerExpressReservations(EligibleBuildingReviewersList, MeetingDurationBuild, curtrade));
            }

            //Electric
            List<TimeSlot> electricReviewerReservations = new List<TimeSlot>();

            curtrade = CurrentProject.Trades.Where(x => x.DepartmentInfo == DepartmentNameEnums.Electrical).FirstOrDefault();
            if (curtrade == null || curtrade.EstimationNotApplicable == true || EligibleElectricReviewersList == null)
            {
                ret.ElectricUserID = -1;
            }
            else
            {
                electricRequested = true;
                electricReviewerReservations.AddRange(GetEligibleReviewerExpressReservations(EligibleElectricReviewersList, MeetingDurationElectric, curtrade));
            }

            //Mechanical
            List<TimeSlot> mechReviewerReservations = new List<TimeSlot>();

            curtrade = CurrentProject.Trades.Where(x => x.DepartmentInfo == DepartmentNameEnums.Mechanical).FirstOrDefault();
            if (curtrade == null || curtrade.EstimationNotApplicable == true || EligibleMechReviewersList == null)
            {
                ret.MechUserID = -1;
            }
            else
            {
                mechRequested = true;
                mechReviewerReservations.AddRange(GetEligibleReviewerExpressReservations(EligibleMechReviewersList, MeetingDurationMech, curtrade));
            }

            //Plumbing
            List<TimeSlot> plumbReviewerReservations = new List<TimeSlot>();

            curtrade = CurrentProject.Trades.Where(x => x.DepartmentInfo == DepartmentNameEnums.Plumbing).FirstOrDefault();
            if (curtrade == null || curtrade.EstimationNotApplicable == true || EligiblePlumbReviewersList == null)
            {
                ret.PlumbUserID = -1;
            }
            else
            {
                plumbRequested = true;
                plumbReviewerReservations.AddRange(GetEligibleReviewerExpressReservations(EligiblePlumbReviewersList, MeetingDurationPlumb, curtrade));
            }

            //Zoning
            List<TimeSlot> zoneReviewerReservations = new List<TimeSlot>();

            curtrade = CurrentProject.Agencies.Where(x => helper.ZoneDepartmentNames.Contains(x.DepartmentInfo)).FirstOrDefault();
            if (curtrade == null || curtrade.EstimationNotApplicable == true || EligibleZoneReviewersList == null)
            {
                ret.ZoneUserID = -1;
            }
            else
            {
                zoningRequested = true;
                zoneReviewerReservations.AddRange(GetEligibleReviewerExpressReservations(EligibleZoneReviewersList, MeetingDurationZone, curtrade));
            }

            //Fire
            List<TimeSlot> fireReviewerReservations = new List<TimeSlot>();

            curtrade = CurrentProject.Agencies.Where(x => helper.FireDepartmentNames.Contains(x.DepartmentInfo)).FirstOrDefault();
            if (curtrade == null || curtrade.EstimationNotApplicable == true || EligibleFireReviewersList == null)
            {
                ret.FireUserID = -1;
            }
            else
            {
                fireRequested = true;
                fireReviewerReservations.AddRange(GetEligibleReviewerExpressReservations(EligibleFireReviewersList, MeetingDurationFire, curtrade));
            }

            //Backflow
            List<TimeSlot> backflowReviewerReservations = new List<TimeSlot>();

            curtrade = CurrentProject.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.Backflow).FirstOrDefault();
            if (curtrade == null || curtrade.EstimationNotApplicable == true || EligibleBackFlowReviewersList == null)
            {
                ret.BackFlowUserID = -1;
            }
            else
            {
                backflowRequested = true;
                backflowReviewerReservations.AddRange(GetEligibleReviewerExpressReservations(EligibleBackFlowReviewersList, MeetingDurationBackFlow, curtrade));
            }

            //Food
            List<TimeSlot> foodReviewerReservations = new List<TimeSlot>();

            curtrade = CurrentProject.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.EH_Food).FirstOrDefault();
            if (curtrade == null || curtrade.EstimationNotApplicable == true || EligibleFoodServiceReviewersList == null)
            {
                ret.FoodServiceUserID = -1;
            }
            else
            {
                foodRequested = true;
                foodReviewerReservations.AddRange(GetEligibleReviewerExpressReservations(EligibleFoodServiceReviewersList, MeetingDurationFood, curtrade));
            }

            //Pool
            List<TimeSlot> poolReviewerReservations = new List<TimeSlot>();

            curtrade = CurrentProject.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.EH_Pool).FirstOrDefault();
            if (curtrade == null || curtrade.EstimationNotApplicable == true || EligiblePoolReviewersList == null)
            {
                ret.PoolUserID = -1;
            }
            else
            {
                poolRequested = true;
                poolReviewerReservations.AddRange(GetEligibleReviewerExpressReservations(EligiblePoolReviewersList, MeetingDurationPool, curtrade));
            }

            //Facilities
            List<TimeSlot> facilitiesReviewerReservations = new List<TimeSlot>();

            curtrade = CurrentProject.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.EH_Facilities).FirstOrDefault();
            if (curtrade == null || curtrade.EstimationNotApplicable == true || EligibleFacilityReviewersList == null)
            {
                ret.FacilityUserID = -1;
            }
            else
            {
                facilityRequested = true;
                facilitiesReviewerReservations.AddRange(GetEligibleReviewerExpressReservations(EligibleFacilityReviewersList, MeetingDurationFacility, curtrade));
            }

            //Daycare
            List<TimeSlot> daycareReviewerReservations = new List<TimeSlot>();

            curtrade = CurrentProject.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.EH_Day_Care).FirstOrDefault();
            if (curtrade == null || curtrade.EstimationNotApplicable == true || EligibleDayCareReviewersList == null)
            {
                ret.DayCareUserID = -1;
            }
            else
            {
                daycareRequested = true;
                daycareReviewerReservations.AddRange(GetEligibleReviewerExpressReservations(EligibleDayCareReviewersList, MeetingDurationDayCare, curtrade));
            }

            // iterate through possible reservation dates to find reviewers
            foreach (DateTime reservationDate in reservationDates)
            {
                if (ret.IsReadyForScheduling) break;

                List<TimeSlot> timeSlots = new List<TimeSlot>();

                if (buildingRequested)
                {
                    TimeSlot tsBuilding = buildingReviewerReservations.Where(x => x.StartTime.Date == reservationDate.Date).FirstOrDefault();
                    if (tsBuilding == null) continue;
                    timeSlots.Add(tsBuilding);
                    ret.BuildingUserID = tsBuilding.UserID;
                }

                if (electricRequested)
                {
                    TimeSlot tsElectric = electricReviewerReservations.Where(x => x.StartTime.Date == reservationDate.Date).FirstOrDefault();
                    if (tsElectric == null) continue;
                    timeSlots.Add(tsElectric);
                    ret.ElectricUserID = tsElectric.UserID;
                }

                if (mechRequested)
                {
                    TimeSlot tsMechanical = mechReviewerReservations.Where(x => x.StartTime.Date == reservationDate.Date).FirstOrDefault();
                    if (tsMechanical == null) continue;
                    timeSlots.Add(tsMechanical);
                    ret.MechUserID = tsMechanical.UserID;
                }

                if (plumbRequested)
                {
                    TimeSlot tsPlumbing = plumbReviewerReservations.Where(x => x.StartTime.Date == reservationDate.Date).FirstOrDefault();
                    if (tsPlumbing == null) continue;
                    timeSlots.Add(tsPlumbing);
                    ret.PlumbUserID = tsPlumbing.UserID;
                }

                if (zoningRequested)
                {
                    TimeSlot tsZoning = zoneReviewerReservations.Where(x => x.StartTime.Date == reservationDate.Date).FirstOrDefault();
                    if (tsZoning == null) continue;
                    timeSlots.Add(tsZoning);
                    ret.ZoneUserID = tsZoning.UserID;
                }

                if (fireRequested)
                {
                    TimeSlot tsFire = fireReviewerReservations.Where(x => x.StartTime.Date == reservationDate.Date).FirstOrDefault();
                    if (tsFire == null) continue;
                    timeSlots.Add(tsFire);
                    ret.FireUserID = tsFire.UserID;
                }

                if (backflowRequested)
                {
                    TimeSlot tsBackflow = backflowReviewerReservations.Where(x => x.StartTime.Date == reservationDate.Date).FirstOrDefault();
                    if (tsBackflow == null) continue;
                    timeSlots.Add(tsBackflow);
                    ret.BackFlowUserID = tsBackflow.UserID;
                }

                if (foodRequested)
                {
                    TimeSlot tsFood = foodReviewerReservations.Where(x => x.StartTime.Date == reservationDate.Date).FirstOrDefault();
                    if (tsFood == null) continue;
                    timeSlots.Add(tsFood);
                    ret.FoodServiceUserID = tsFood.UserID;
                }

                if (poolRequested)
                {
                    TimeSlot tsPool = poolReviewerReservations.Where(x => x.StartTime.Date == reservationDate.Date).FirstOrDefault();
                    if (tsPool == null) continue;
                    timeSlots.Add(tsPool);
                    ret.PoolUserID = tsPool.UserID;
                }

                if (facilityRequested)
                {
                    TimeSlot tsFacility = facilitiesReviewerReservations.Where(x => x.StartTime.Date == reservationDate.Date).FirstOrDefault();
                    if (tsFacility == null) continue;
                    timeSlots.Add(tsFacility);
                    ret.FacilityUserID = tsFacility.UserID;
                }

                if (daycareRequested)
                {
                    TimeSlot tsDaycare = daycareReviewerReservations.Where(x => x.StartTime.Date == reservationDate.Date).FirstOrDefault();
                    if (tsDaycare == null) continue;
                    timeSlots.Add(tsDaycare);
                    ret.DayCareUserID = tsDaycare.UserID;
                }

                ret.IsReadyForScheduling = true;

                TimeSlot minTimeSlot = timeSlots.OrderBy(x => x.StartTime).First();

                ret.SelectedStartDateTime = minTimeSlot.StartTime;
                ret.SelectedEndDateTime = minTimeSlot.StartTime.AddHours((double)MeetingDuration);
            }

            return ret;
        }
        public List<DateTime> GetFiveAvailDate(DateTime start, DateTime end)
        {
            AutoScheduledExpressValues ret = new AutoScheduledExpressValues();
            List<DateTime> AvailDate = new List<DateTime>();

            Helper helper = new Helper();

            if (AllEligibleReviewers.Count == 0)
            {
                ret.ErrorMessage = "No Eligible Reviewers Found! Check user settings for scheduling eligibility.";
                return null;
            }

            List<TimeSlot> allReviewerReservations = GetEligibleReviewerExpressReservations(AllEligibleReviewers, MeetingDuration);

            List<DateTime> reservationDates = GetDaysOfReservations(allReviewerReservations);

            bool buildingRequested = false;
            bool electricRequested = false;
            bool mechRequested = false;
            bool plumbRequested = false;
            bool zoningRequested = false;
            bool fireRequested = false;
            bool backflowRequested = false;
            bool poolRequested = false;
            bool facilityRequested = false;
            bool foodRequested = false;
            bool daycareRequested = false;

            //Building
            List<TimeSlot> buildingReviewerReservations = new List<TimeSlot>();

            ProjectDepartment curtrade = CurrentProject.Trades.Where(x => x.DepartmentInfo == DepartmentNameEnums.Building).FirstOrDefault();
            if (curtrade == null || curtrade.EstimationNotApplicable == true || EligibleBuildingReviewersList == null)
            {
                ret.BuildingUserID = -1;
            }
            else
            {
                buildingRequested = true;
                buildingReviewerReservations.AddRange(GetEligibleReviewerExpressReservations(EligibleBuildingReviewersList, MeetingDurationBuild, curtrade));
            }

            //Electric
            List<TimeSlot> electricReviewerReservations = new List<TimeSlot>();

            curtrade = CurrentProject.Trades.Where(x => x.DepartmentInfo == DepartmentNameEnums.Electrical).FirstOrDefault();
            if (curtrade == null || curtrade.EstimationNotApplicable == true || EligibleElectricReviewersList == null)
            {
                ret.ElectricUserID = -1;
            }
            else
            {
                electricRequested = true;
                electricReviewerReservations.AddRange(GetEligibleReviewerExpressReservations(EligibleElectricReviewersList, MeetingDurationElectric, curtrade));
            }

            //Mechanical
            List<TimeSlot> mechReviewerReservations = new List<TimeSlot>();

            curtrade = CurrentProject.Trades.Where(x => x.DepartmentInfo == DepartmentNameEnums.Mechanical).FirstOrDefault();
            if (curtrade == null || curtrade.EstimationNotApplicable == true || EligibleMechReviewersList == null)
            {
                ret.MechUserID = -1;
            }
            else
            {
                mechRequested = true;
                mechReviewerReservations.AddRange(GetEligibleReviewerExpressReservations(EligibleMechReviewersList, MeetingDurationMech, curtrade));
            }

            //Plumbing
            List<TimeSlot> plumbReviewerReservations = new List<TimeSlot>();

            curtrade = CurrentProject.Trades.Where(x => x.DepartmentInfo == DepartmentNameEnums.Plumbing).FirstOrDefault();
            if (curtrade == null || curtrade.EstimationNotApplicable == true || EligiblePlumbReviewersList == null)
            {
                ret.PlumbUserID = -1;
            }
            else
            {
                plumbRequested = true;
                plumbReviewerReservations.AddRange(GetEligibleReviewerExpressReservations(EligiblePlumbReviewersList, MeetingDurationPlumb, curtrade));
            }

            //Zoning
            List<TimeSlot> zoneReviewerReservations = new List<TimeSlot>();

            curtrade = CurrentProject.Agencies.Where(x => helper.ZoneDepartmentNames.Contains(x.DepartmentInfo)).FirstOrDefault();
            if (curtrade == null || curtrade.EstimationNotApplicable == true || EligibleZoneReviewersList == null)
            {
                ret.ZoneUserID = -1;
            }
            else
            {
                zoningRequested = true;
                zoneReviewerReservations.AddRange(GetEligibleReviewerExpressReservations(EligibleZoneReviewersList, MeetingDurationZone, curtrade));
            }

            //Fire
            List<TimeSlot> fireReviewerReservations = new List<TimeSlot>();

            curtrade = CurrentProject.Agencies.Where(x => helper.FireDepartmentNames.Contains(x.DepartmentInfo)).FirstOrDefault();
            if (curtrade == null || curtrade.EstimationNotApplicable == true || EligibleFireReviewersList == null)
            {
                ret.FireUserID = -1;
            }
            else
            {
                fireRequested = true;
                fireReviewerReservations.AddRange(GetEligibleReviewerExpressReservations(EligibleFireReviewersList, MeetingDurationFire, curtrade));
            }

            //Backflow
            List<TimeSlot> backflowReviewerReservations = new List<TimeSlot>();

            curtrade = CurrentProject.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.Backflow).FirstOrDefault();
            if (curtrade == null || curtrade.EstimationNotApplicable == true || EligibleBackFlowReviewersList == null)
            {
                ret.BackFlowUserID = -1;
            }
            else
            {
                backflowRequested = true;
                backflowReviewerReservations.AddRange(GetEligibleReviewerExpressReservations(EligibleBackFlowReviewersList, MeetingDurationBackFlow, curtrade));
            }

            //Food
            List<TimeSlot> foodReviewerReservations = new List<TimeSlot>();

            curtrade = CurrentProject.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.EH_Food).FirstOrDefault();
            if (curtrade == null || curtrade.EstimationNotApplicable == true || EligibleFoodServiceReviewersList == null)
            {
                ret.FoodServiceUserID = -1;
            }
            else
            {
                foodRequested = true;
                foodReviewerReservations.AddRange(GetEligibleReviewerExpressReservations(EligibleFoodServiceReviewersList, MeetingDurationFood, curtrade));
            }

            //Pool
            List<TimeSlot> poolReviewerReservations = new List<TimeSlot>();

            curtrade = CurrentProject.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.EH_Pool).FirstOrDefault();
            if (curtrade == null || curtrade.EstimationNotApplicable == true || EligiblePoolReviewersList == null)
            {
                ret.PoolUserID = -1;
            }
            else
            {
                poolRequested = true;
                poolReviewerReservations.AddRange(GetEligibleReviewerExpressReservations(EligiblePoolReviewersList, MeetingDurationPool, curtrade));
            }

            //Facilities
            List<TimeSlot> facilitiesReviewerReservations = new List<TimeSlot>();

            curtrade = CurrentProject.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.EH_Facilities).FirstOrDefault();
            if (curtrade == null || curtrade.EstimationNotApplicable == true || EligibleFacilityReviewersList == null)
            {
                ret.FacilityUserID = -1;
            }
            else
            {
                facilityRequested = true;
                facilitiesReviewerReservations.AddRange(GetEligibleReviewerExpressReservations(EligibleFacilityReviewersList, MeetingDurationFacility, curtrade));
            }

            //Daycare
            List<TimeSlot> daycareReviewerReservations = new List<TimeSlot>();

            curtrade = CurrentProject.Agencies.Where(x => x.DepartmentInfo == DepartmentNameEnums.EH_Day_Care).FirstOrDefault();
            if (curtrade == null || curtrade.EstimationNotApplicable == true || EligibleDayCareReviewersList == null)
            {
                ret.DayCareUserID = -1;
            }
            else
            {
                daycareRequested = true;
                daycareReviewerReservations.AddRange(GetEligibleReviewerExpressReservations(EligibleDayCareReviewersList, MeetingDurationDayCare, curtrade));
            }

            // iterate through possible reservation dates to find reviewers
            foreach (DateTime reservationDate in reservationDates)
            {
                List<TimeSlot> timeSlots = new List<TimeSlot>();

                if (buildingRequested)
                {
                    TimeSlot tsBuilding = buildingReviewerReservations.Where(x => x.StartTime.Date == reservationDate.Date).FirstOrDefault();
                    if (tsBuilding == null) continue;
                    timeSlots.Add(tsBuilding);
                    ret.BuildingUserID = tsBuilding.UserID;
                }

                if (electricRequested)
                {
                    TimeSlot tsElectric = electricReviewerReservations.Where(x => x.StartTime.Date == reservationDate.Date).FirstOrDefault();
                    if (tsElectric == null) continue;
                    timeSlots.Add(tsElectric);
                    ret.ElectricUserID = tsElectric.UserID;
                }

                if (mechRequested)
                {
                    TimeSlot tsMechanical = mechReviewerReservations.Where(x => x.StartTime.Date == reservationDate.Date).FirstOrDefault();
                    if (tsMechanical == null) continue;
                    timeSlots.Add(tsMechanical);
                    ret.MechUserID = tsMechanical.UserID;
                }

                if (plumbRequested)
                {
                    TimeSlot tsPlumbing = plumbReviewerReservations.Where(x => x.StartTime.Date == reservationDate.Date).FirstOrDefault();
                    if (tsPlumbing == null) continue;
                    timeSlots.Add(tsPlumbing);
                    ret.PlumbUserID = tsPlumbing.UserID;
                }

                if (zoningRequested)
                {
                    TimeSlot tsZoning = zoneReviewerReservations.Where(x => x.StartTime.Date == reservationDate.Date).FirstOrDefault();
                    if (tsZoning == null) continue;
                    timeSlots.Add(tsZoning);
                    ret.ZoneUserID = tsZoning.UserID;
                }

                if (fireRequested)
                {
                    TimeSlot tsFire = fireReviewerReservations.Where(x => x.StartTime.Date == reservationDate.Date).FirstOrDefault();
                    if (tsFire == null) continue;
                    timeSlots.Add(tsFire);
                    ret.FireUserID = tsFire.UserID;
                }

                if (backflowRequested)
                {
                    TimeSlot tsBackflow = backflowReviewerReservations.Where(x => x.StartTime.Date == reservationDate.Date).FirstOrDefault();
                    if (tsBackflow == null) continue;
                    timeSlots.Add(tsBackflow);
                    ret.BackFlowUserID = tsBackflow.UserID;
                }

                if (foodRequested)
                {
                    TimeSlot tsFood = foodReviewerReservations.Where(x => x.StartTime.Date == reservationDate.Date).FirstOrDefault();
                    if (tsFood == null) continue;
                    timeSlots.Add(tsFood);
                    ret.FoodServiceUserID = tsFood.UserID;
                }

                if (poolRequested)
                {
                    TimeSlot tsPool = poolReviewerReservations.Where(x => x.StartTime.Date == reservationDate.Date).FirstOrDefault();
                    if (tsPool == null) continue;
                    timeSlots.Add(tsPool);
                    ret.PoolUserID = tsPool.UserID;
                }

                if (facilityRequested)
                {
                    TimeSlot tsFacility = facilitiesReviewerReservations.Where(x => x.StartTime.Date == reservationDate.Date).FirstOrDefault();
                    if (tsFacility == null) continue;
                    timeSlots.Add(tsFacility);
                    ret.FacilityUserID = tsFacility.UserID;
                }

                if (daycareRequested)
                {
                    TimeSlot tsDaycare = daycareReviewerReservations.Where(x => x.StartTime.Date == reservationDate.Date).FirstOrDefault();
                    if (tsDaycare == null) continue;
                    timeSlots.Add(tsDaycare);
                    ret.DayCareUserID = tsDaycare.UserID;
                }

                if (reservationDate >= start && reservationDate <= end) {
                    if (AvailDate.Contains(reservationDate.Date)) continue;
                    AvailDate.Add(reservationDate.Date);
                }

                if (AvailDate.Count() == 5) break;
            }

            return AvailDate;
        }

        private void SetRequestedDates()
        {
            if (PlanReviewSchedule != null)
            {
                if (PlanReviewSchedule.Proposed1Dt.HasValue)
                {
                    RequestedDates.Add(new RequestedExpressDateBE() { RequestedExpressDate = PlanReviewSchedule.Proposed1Dt.Value });
                }

                if (PlanReviewSchedule.Proposed2Dt.HasValue)
                {
                    RequestedDates.Add(new RequestedExpressDateBE() { RequestedExpressDate = PlanReviewSchedule.Proposed2Dt.Value });
                }

                if (PlanReviewSchedule.Proposed3Dt.HasValue)
                {
                    RequestedDates.Add(new RequestedExpressDateBE() { RequestedExpressDate = PlanReviewSchedule.Proposed3Dt.Value });
                }
            }
        }

        private List<DateTime> GetDaysOfReservations(List<TimeSlot> allReviewerReservations)
        {
            return allReviewerReservations.Select(x => x.StartTime).Distinct().ToList();
        }

        private List<TimeSlot> GetEligibleReviewerExpressReservations(
            List<AutoSchedulableReviewer> deptEligibleReviewers,
            decimal meetingDuration,
            ProjectDepartment projectDepartment = null)
        {
            List<AutoSchedulableReviewer> reviewersForSearch = new List<AutoSchedulableReviewer>();

            List<TimeSlot> reservedTimeSlots = new List<TimeSlot>();
            Helper helper = new Helper();
            foreach (AutoSchedulableReviewer reviewer in deptEligibleReviewers)
            {
                // NOTE: these need to be netted with EMA to get the actual review reserved slots

                List<TimeSlot> reviewerReservedSlots = new List<TimeSlot>();

                if (projectDepartment == null)
                {
                    reviewerReservedSlots = reviewer.CurrentMeetings.Where(x => x.AllocationType == TimeAllocationType.Project_Express_Blocked).ToList();
                }
                else
                {
                    if (IsDepartmentZoning(projectDepartment))
                    {
                        reviewerReservedSlots = reviewer.CurrentMeetings.Where(x => x.AllocationType == TimeAllocationType.Project_Express_Blocked &&
                      helper.ZoneDepartmentNames.Contains(x.DepartmentName)).ToList();
                    }
                    else if (IsDepartmentFire(projectDepartment))
                    {
                        reviewerReservedSlots = reviewer.CurrentMeetings.Where(x => x.AllocationType == TimeAllocationType.Project_Express_Blocked &&
                       helper.FireDepartmentNames.Contains(x.DepartmentName)).ToList();
                    }
                    else
                    {
                        reviewerReservedSlots = reviewer.CurrentMeetings.Where(x => x.AllocationType == TimeAllocationType.Project_Express_Blocked
                        && x.DepartmentName == projectDepartment.DepartmentInfo).ToList();
                    }
                }

                List<TimeSlot> flattenedTimeSlots = SchedulingHelper.FlattenTimeSlots(reviewerReservedSlots);

                TimeSpan noonStart = new TimeSpan(12, 0, 0);
                TimeSpan noonEnd = new TimeSpan(13, 0, 0);

                foreach (TimeSlot ts in flattenedTimeSlots)
                {
                    if ((ts.StartTime.TimeOfDay < noonStart || ts.StartTime.TimeOfDay > noonEnd)
                        && (ts.EndTime.TimeOfDay < noonStart || ts.EndTime.TimeOfDay > noonEnd))
                    {
                        TimeSpan duration = ts.EndTime - ts.StartTime;
                        if (duration.TotalHours >= (double)meetingDuration)
                        {
                            reservedTimeSlots.Add(ts);
                        }
                    }
                }
            }

            return reservedTimeSlots.OrderBy(x => x.StartTime).ToList();
        }

        private bool IsDepartmentZoning(ProjectDepartment projectDepartment)
        {
            Helper helper = new Helper();
            if (helper.ZoneDepartmentNames.Contains(projectDepartment.DepartmentInfo))
                return true;
            return false;
        }

        private bool IsDepartmentFire(ProjectDepartment projectDepartment)
        {
            Helper helper = new Helper();
            if (helper.FireDepartmentNames.Contains(projectDepartment.DepartmentInfo))
                return true;

            return false;
        }

        private DateTime SetAutoSchedulePeriodStart()
        {
            if (IsManualDateRequest)
            {
                return (DateTime)RequestData.ManualStartDateTime; // don't adjust for Manual
            }
            else
            {
                if (RequestedDates != null && RequestedDates.Count() > 0)
                {
                    for (int i = 0; i < RequestedDates.Count; i++)
                    {
                        if (!IsDateHolidayOrWeekEnd(RequestedDates[i].RequestedExpressDate))
                        {
                            return RequestedDates[i].RequestedExpressDate;
                        }
                        else
                        {
                            return NextWorkingDay(RequestedDates[i].RequestedExpressDate);
                        }
                    }
                }
            }

            return NextWorkingDay(DateTime.Now);
        }
        private DateTime SetDateRangePeriodStart(DateTime start)
        {
            if (!IsDateHolidayOrWeekEnd(start))
            {
                return start;
            }
            else
            {
                return NextWorkingDay(start);
            }
        }
        private DateTime SetAllowedMaxEndDate()
        {
            if (RequestData.ManualStartDateTime != null && RequestData.ManualStartDateTime.HasValue)
            {
                return AutoSchedulePeriodStart;
            }

            DateTime twoWeeksFromStart = AutoSchedulePeriodStart.AddDays(14);

            if (!IsDateHolidayOrWeekEnd(twoWeeksFromStart))
            {
                return twoWeeksFromStart;
            }
            else
            {
                return NextWorkingDay(twoWeeksFromStart);
            }
        }

        private DateTime SetAllowedMaxEndDateForReport()
        {
            DateTime fourWeeksFromStart = AutoSchedulePeriodStart.AddDays(28);

            if (!IsDateHolidayOrWeekEnd(fourWeeksFromStart))
            {
                return fourWeeksFromStart;
            }
            else
            {
                return NextWorkingDay(fourWeeksFromStart);
            }
        }

        private int GetReservedExpressMeetingRoom(DateTime startDateTime)
        {
            ReserveExpressReservationBO bo = new ReserveExpressReservationBO();
            List<ReserveExpressReservationBE> reserveExpressReservations = bo.GetList();

            int meetingRoomId =
                reserveExpressReservations.Where(x => x.ReserveExpressDt.Date == startDateTime.Date).FirstOrDefault()
                .MeetingRoomRefId.Value;

            return meetingRoomId;
        }
    }
}