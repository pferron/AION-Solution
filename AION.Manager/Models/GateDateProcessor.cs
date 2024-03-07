using AION.BL.Common;
using System;

namespace AION.Manager.Models
{
    public class GateDateProcessor
    {
        private readonly int _gateDateConfig;
        private readonly DateTime _startDateTime;

        public GateDateProcessor(int gateDateConfig, DateTime startDateTime)
        {
            if (gateDateConfig < 0) { throw new ArgumentOutOfRangeException("gateDateConfig", "GateDateConfig must be a positive number."); }
            if (startDateTime == DateTime.MinValue) { throw new ArgumentOutOfRangeException("startDatetime", "StartDateTime must be a valid date."); }

            _gateDateConfig = gateDateConfig;
            _startDateTime = startDateTime;
        }

        public DateTime CalculateGateDate()
        {
            DateTime gateDate = DateTimeHelper.DetermineWorkDateBeforeDateSpecified(_startDateTime.Date, _gateDateConfig);

            if (gateDate < DateTime.Now.Date) // upon a reschedule, do not issue a gate date that is prior to current date 
            {
                gateDate = DateTime.Now.Date;
            }

            return gateDate;
        }
    }
}