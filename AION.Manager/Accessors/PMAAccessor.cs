using AION.Base;
using AION.BL;
using AION.BL.BusinessObjects;
using AION.Manager.Adapters;
using AIONEstimator.Engine.BusinessObjects;
using Meck.Logging;
using System;
using System.Reflection;

namespace AION.Manager.Accessors
{
    public class PMAAccessor : BaseAdapter, IPMAAccessor
    {
        public PreliminaryMeetingAppointment GetByProjectId(int id)
        {
            PreliminaryMeetingAppointment item = new PreliminaryMeetingAppointment();

            try
            {
                item = new PreliminaryMeetingApptModelBO().GetByProjectId(id);
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in PMAAccessor GetByProjectID - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }
            return item;
        }

        public bool CancelByProjectId(int projectId)
        {
            //Get the most recent plan review
            PreliminaryMeetingAppointment pma = GetByProjectId(projectId);
            if (!pma.PreliminaryMeetingApptID.HasValue) return true;

            IAppointmentAdapter adapter = new PMAAdapter(pma);

            adapter.CancelAppointment();

            //Cancel the PMA
            PreliminaryMeetingAppointmentBO bo = new PreliminaryMeetingAppointmentBO();
            bo.CancelPMAByProjectId(projectId);

            return true;
        }
    }
}