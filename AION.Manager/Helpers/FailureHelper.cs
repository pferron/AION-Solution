using AION.Manager.Models;
using System;

namespace AION.Manager.Helpers
{
    public class FailureHelper
    {
        internal static Failure GetFailure(string environment, string failureType, string message)
        {
            Failure failure = new Failure
            {
                Environment = environment,
                FailureType = failureType,
                Message = message,
                TimeStamp = DateTime.Now.ToString()
            };

            return failure;
        }
    }
}