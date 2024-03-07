using AION.Manager.Models;
using System;

namespace AION.Manager.Helpers
{
    public class AccelaSyncHelper
    {
        internal static AccelaFailure GetAccelaFailure(string accelaEnvironment, string failureType, string message,
            string projectNumber, string recordId)
        {
            AccelaFailure accelafailure = new AccelaFailure
            {
                AccelaEnvironment = accelaEnvironment,
                FailureType = failureType,
                Message = message,
                ProjectNumber = projectNumber,
                RecordId = recordId,
                TimeStamp = DateTime.Now.ToString()
            };

            return accelafailure;
        }
    }
}