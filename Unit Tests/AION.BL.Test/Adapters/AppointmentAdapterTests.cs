using AION.BL.Adapters;
using AION.BL.BusinessObjects;
using AION.BL.Models;
using AION.Manager.Adapters;
using AION.Manager.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace AION.BL.Test.Adapters
{
    [TestClass]
    public class AppointmentAdapterTests
    {
        [TestMethod]
        [Ignore]
        public void GetAttendeesByAppointmentReturnsListOfAttendees()
        {
            //ReserveExpressReservation expressobj = new ReserveExpressReservationModelBO().GetInstance(7969);
            //IAppointmentAdapter appointmentAdapter = new AppointmentAdapter(expressobj, false);

            //List<AttendeeDetails> attendeeDetails = appointmentAdapter.GetAllAttendeesByAppointment();

            //Assert.AreEqual(11, attendeeDetails.Count());
        }

        [TestMethod]
        [Ignore]
        public void TestGenerateAppointmentData()
        {
            //ReserveExpressReservation expressobj = new ReserveExpressReservationModelBO().GetInstance(7969);
            //List<AttendeeDetails> attendees = new List<AttendeeDetails>();

            //attendees.Add(new AttendeeDetails());
            //attendees.Add(new AttendeeDetails());
            //attendees.Add(new AttendeeDetails());
            //attendees.Add(new AttendeeDetails());

            //expressobj.AttendeeDetails = attendees;

            //IAppointmentAdapter appointmentAdapter = new AppointmentAdapter(expressobj, false);
            //AppointmentData appointmentData = appointmentAdapter.GenerateAppointmentData();

            //List<AttendeeDetails> attendeeDetails = appointmentData.AttendeeDetails;

            //Assert.AreEqual(4, attendeeDetails.Count());
        }
    }
}
