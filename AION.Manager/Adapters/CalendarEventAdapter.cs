using AION.Engine.BusinessEntities;
using AIONEstimator.Engine.BusinessObjects;
using Meck.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace AION.Manager.Adapters
{
    public class CalendarEventAdapter : BaseManagerAdapter, ICalendarEventAdapter
    {
        public bool AddCalendarEvent(CalendarEventBE calendarEventBE)
        {
            bool success = false;
            try
            {
                CalendarEventBO calendarEventQueueBO = new CalendarEventBO();
                int id = calendarEventQueueBO.Create(calendarEventBE);

                if (id > 0)
                {
                    success = true;
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in AddCalendarEvent CalendarEventAdapter - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }

            return success;
        }

        public bool UpdateCalendarEvent(CalendarEventBE calendarEventBE)
        {
            bool success = false;
            try
            {
                CalendarEventBO calendarEventQueueBO = new CalendarEventBO();
                
                int id = calendarEventQueueBO.Update(calendarEventBE);

                if (id > 0)
                {
                    success = true;
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in UpdateCalendarEvent CalendarEventAdapter - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }

            return success;
        }

        public List<CalendarEventBE> GetCalendarEvents(bool inProcess)
        {
            List<CalendarEventBE> calendarEvents = new List<CalendarEventBE>();
            try
            {
                CalendarEventBO calendarEventBO = new CalendarEventBO();
                calendarEvents = calendarEventBO.GetListByStatus(inProcess);
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in GetCalendarEvents CalendarEventAdapter - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }

            return calendarEvents;
        }
    }

    public interface ICalendarEventAdapter
    {
        List<CalendarEventBE> GetCalendarEvents(bool inProcess);
        bool AddCalendarEvent(CalendarEventBE calendarEventBE);
        bool UpdateCalendarEvent(CalendarEventBE calendarEventBE);
    }
}