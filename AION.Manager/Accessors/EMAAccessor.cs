using AION.Base;
using AION.BL;
using AION.BL.BusinessObjects;
using AION.Manager.Models;
using Meck.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace AION.Manager.Accessors
{
    public class EMAAccessor : BaseAdapter, IEMAAccessor
    {
        public List<ExpressSearchResult> GetScheduledByDate(DateTime fromdate, DateTime todate)
        {
            List<ExpressSearchResult> expressMeetingAppointments = new List<ExpressSearchResult>();

            try
            {
                // this needs to be replaced by getting the plan review schedules and schedule details

                // get all EMA plan review schedule details that fall within the data range
                // need appt id and all the details
                PlanReviewScheduleModelBO bo = new PlanReviewScheduleModelBO();

                expressMeetingAppointments = bo.GetExpressSearchResults(fromdate, todate);

            }
            catch (Exception ex)
            {
                string errorMessage = "Error in EMAAccessor GetScheduledByDate - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }

            //return ret;
            return expressMeetingAppointments;
        }
    }
}