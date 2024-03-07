using System;

namespace Meck.Logging
{
    public class LoggerModel
    {
        public string PartitionKey { get; set; }

        public string RowKey { get; set; }

        public string LoggingType { get; set; }

        public string LoggingApplicationType { get; set; }

        public string LoggingLocation { get; set; }

        public DateTime? LoggingDate { get; set; }

        public string LoggingMachineName { get; set; }

        public string Environment { get; set; }

        public string Application { get; set; }

        public string LoggingText { get; set; }

        public string ApplicationMessageGuid { get; set; }

        public string ApplicationSourceId { get; set; }

        public string ApplicationMessageData { get; set; }

        public string ModuleName { get; set; }

        public string ClassName { get; set; }

        public string MethodName { get; set; }

        public string ExceptionText { get; set; }

        public string StackTrace { get; set; }
    }

    public class MessageRetryList
    {
        public string MessageGuid { get; set; }
        
    }
}
