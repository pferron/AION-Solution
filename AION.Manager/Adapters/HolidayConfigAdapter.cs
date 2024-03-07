using AION.BL.BusinessObjects;
using AION.BL.Common;
using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using AION.Manager.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AION.BL.Adapters
{
    public class HolidayConfigAdapter : BaseManagerAdapter, IHolidayConfigAdapter
    {
        private List<UserIdentity> _Reviewers;

        public HolidayConfigAdapter()
        {
            _Reviewers = new List<UserIdentity>();
            _Reviewers = GetReviewers();
        }

        public List<HolidayConfig> GetHolidayConfigList()
        {
            List<HolidayConfig> ret = new List<HolidayConfig>();
            HolidayConfigModelBO holidayConfigModelBO = new HolidayConfigModelBO();
            ret = holidayConfigModelBO.GetHolidayConfigList();
            return ret;
        }

        public List<DateTime> GetHolidayConfigDates()
        {
            List<DateTime> ret = new List<DateTime>();
            HolidayConfigModelBO holidayConfigModelBO = new HolidayConfigModelBO();
            ret = holidayConfigModelBO.GetHolidayConfigDateList();
            return ret;
        }

        public int DeleteHoliday(IEnumerable<int> HolidayIds)
        {
            HolidayConfigModelBO holidayConfigModelBO = new HolidayConfigModelBO();

            List<HolidayConfig> holidays = GetHolidayConfigList();

            foreach (int holidayId in HolidayIds)
            {
                HolidayConfig holidayConfig = holidays.FirstOrDefault(x => x.HolidayConfigId == holidayId);
                Appointment appointment = GetAppointmentData(holidayConfig, true);

                ICalendarAppointmentAdapter calendarAppointmentAdapter = new CalendarAppointmentAdapter(appointment);
                calendarAppointmentAdapter.ProcessAppointments();
            }

            int rows = holidayConfigModelBO.InactivateHolidayConfig(HolidayIds);

            return rows;
        }

        public int InsertHolidayConfig(HolidayConfig holidayConfig)
        {
            HolidayConfigBO bo = new HolidayConfigBO();
            HolidayConfigBE be = new HolidayConfigBE();
            be.HolidayDate = holidayConfig.HolidayDate;
            be.HolidayNm = holidayConfig.HolidayNm;
            be.HolidayAnnualRecurInd = holidayConfig.HolidayAnnualRecurInd;
            be.IsActive = true;
            int retValue = bo.Create(be);
            holidayConfig.HolidayConfigId = retValue;

            // since the stored procedure for holiday recurrence inserts up to two rows, retrieve both ids separately
            // for storing each calendar event individually

            if (holidayConfig.HolidayAnnualRecurInd)
            {
                List<HolidayConfigBE> holidayConfigs = bo.GetList()
                    .Where(x => x.HolidayNm == holidayConfig.HolidayNm).ToList();

                foreach (HolidayConfigBE holidayConfigBE in holidayConfigs)
                {
                    HolidayConfig holiday = new HolidayConfig()
                    {
                        HolidayConfigId = holidayConfigBE.HolidayConfigId,
                        HolidayAnnualRecurInd = holidayConfigBE.HolidayAnnualRecurInd,
                        HolidayDate = holidayConfigBE.HolidayDate,
                        HolidayNm = holidayConfigBE.HolidayNm
                    };

                    Appointment appointment = GetAppointmentData(holiday, false);

                    ICalendarAppointmentAdapter calendarAppointmentAdapter = new CalendarAppointmentAdapter(appointment);
                    calendarAppointmentAdapter.ProcessAppointments();
                }
            }
            else
            {
                Appointment appointment = GetAppointmentData(holidayConfig, false);

                ICalendarAppointmentAdapter calendarAppointmentAdapter = new CalendarAppointmentAdapter(appointment);
                calendarAppointmentAdapter.ProcessAppointments();
            }

            return retValue;
        }

        public List<DateTime> GetConfiguredHolidayDates(List<HolidayConfig> holidayConfigs)
        {
            var holidayDateList = new List<DateTime>();

            foreach (var item in holidayConfigs)
            {
                holidayDateList.Add(item.HolidayDate);
            }

            return holidayDateList;
        }

        private List<UserIdentity> GetReviewers()
        {
            IUserAdapter userAdapter = new UserAdapter();

            List<UserIdentity> reviewers = userAdapter.GetUserIdentityListBySystemRoleEnum(SystemRoleEnum.Plan_Reviewer);

            return reviewers;
        }

        private Appointment GetAppointmentData(HolidayConfig holidayConfig, bool isCancellation)
        {
            Appointment appointment = new Appointment();
            appointment.HolidayConfig = holidayConfig;
            appointment.AttendeeDetails = GetAttendeeDetails();
            appointment.IsCancellation = isCancellation;

            List<ScheduleTime> scheduleTimes = new List<ScheduleTime>()
            {
                new ScheduleTime()
                {
                    StartDate = holidayConfig.HolidayDate,
                    EndDate = holidayConfig.HolidayDate.AddDays(1)
                }
            };
            appointment.ScheduleTimes = scheduleTimes;

            return appointment;
        }

        private List<AttendeeDetails> GetAttendeeDetails()
        {
            List<AttendeeDetails> attendees = new List<AttendeeDetails>();

            foreach (UserIdentity userIdentity in _Reviewers)
            {
                AttendeeDetails attendeeDetails = new AttendeeDetails()
                {
                    EmailId = userIdentity.Email,
                    FirstName = userIdentity.FirstName,
                    LastName = userIdentity.LastName,
                    UserId = userIdentity.ID
                };

                attendees.Add(attendeeDetails);
            }

            return attendees;
        }
    }

    public interface IHolidayConfigAdapter
    {
        List<HolidayConfig> GetHolidayConfigList();
        List<DateTime> GetHolidayConfigDates();
        int DeleteHoliday(IEnumerable<int> HolidayIds);
        int InsertHolidayConfig(HolidayConfig holidayConfig);
    }
}
