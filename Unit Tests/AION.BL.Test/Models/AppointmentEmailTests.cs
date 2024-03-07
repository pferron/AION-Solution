using AION.BL.Adapters;
using AION.BL.BusinessObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AION.BL.Test.Models
{
    [TestClass]
    public class AppointmentEmailTests
    {
        private Appointment _Appointment;

        [TestInitialize]
        public void TestInitialize()
        {
            _Appointment = new FacilitatorMeetingApptModelBO().GetInstance(354);
            _Appointment.ProjectEstimation = new EstimationCRUDAdapter().GetProjectDetailsByProjectId(_Appointment.ProjectID.Value);
        }

        [TestMethod]
        [Ignore]
        public void ValidateThatSubjectAndMessageAreCreatedForAppointment()
        {
            var appointmentEmail = new AppointmentEmail(_Appointment);

            Assert.IsNotNull(appointmentEmail.ScheduleSubject);
            Assert.IsNotNull(appointmentEmail.ScheduleMessage);
            Assert.IsNotNull(appointmentEmail.CancelSubject);
            Assert.IsNotNull(appointmentEmail.CancelMessage);
        }
    }
}
