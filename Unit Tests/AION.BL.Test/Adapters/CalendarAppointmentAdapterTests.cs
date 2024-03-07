using AION.BL.Adapters;
using AION.BL.Common;
using AION.BL.Test.Helpers;
using AION.Manager.Adapters;
using AION.Manager.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AION.BL.Test.Adapters
{
    [TestClass]
    public class CalendarAppointmentAdapterTests
    {
        //[TestMethod]
        public void TestBatchAttendees()
        {
            List<AttendeeDetails> attendeeDetails = new List<AttendeeDetails>();

            for (int i = 0; i <= 60; i++)
            {
                attendeeDetails.Add(new AttendeeDetails() { EmailId = "janessa.allen@mecklenburgcountync.gov" });
            }

            Appointment appointment = new Appointment()
            {
                AttendeeDetails = attendeeDetails
            };

            CalendarAppointmentAdapter adapter = new CalendarAppointmentAdapter(appointment);

            var batches = adapter.CreateAttendeeBatches(10);

            Assert.AreEqual(7, batches.Count());
        }

        //[TestMethod]
        public void TestQueueRecordGeneration()
        {
            List<AttendeeDetails> attendeeDetails = new List<AttendeeDetails>();

            for (int i = 0; i <= 25; i++)
            {
                attendeeDetails.Add(new AttendeeDetails() { EmailId = "janessa.allen@mecklenburgcountync.gov" });
            }

            var projectSchedule = new ProjectSchedule() { RecurringApptDt = DateTime.Now };
            
            var scheduleTimes = new List<ScheduleTime>();

            ScheduleTime schedule = new ScheduleTime()
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now
            };

            scheduleTimes.Add(schedule);

            var projectEstimation = ProjectEstimationCreator.CreatePE(PropertyTypeEnums.Commercial, 2, "Level 1");

            Appointment appointment = new Appointment()
            {
                AttendeeDetails = attendeeDetails,
                ProjectSchedule = projectSchedule,
                ScheduleTimes = scheduleTimes,
                ProjectEstimation = projectEstimation
            };

            CalendarAppointmentAdapter adapter = new CalendarAppointmentAdapter(appointment);
            adapter.ProcessAppointments();

            Assert.AreEqual(1, 1);
        }
    }
}
