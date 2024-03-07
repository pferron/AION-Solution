namespace Meck.Logging
{
    public class Enums
    {

        #region Enums

        public enum LoggingType
        {
            Tracing
            , Information
            , Warning
            , Exception
            , ExecutionTime
            , UnitTest
            , None
        }

        public enum Environment
        {
            DEV
            , QA
            , TST
            , TRN
            , PRD
            , None
        }

        public enum LoggingLocation
        {
            OnPrem
            , Azure
            , None
        }

        public enum LoggingApplicationType
        {
            WindowsService
            , WebApi
            , WebJob
            , FunctionApp
            , LogicApp
            , WebApp
            , None
        }

        #endregion

    }
}
