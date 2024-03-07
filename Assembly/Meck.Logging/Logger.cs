
using Microsoft.ServiceBus.Messaging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Meck.Logging

{
    public class Logger
    {

        #region Properties

        /// <summary>
        /// Used to send messages to azure service bus.
        /// </summary>
        private MessagingFactory _senderFactory;
        private MessagingFactory SenderFactory
        {
            get { return _senderFactory; }
            set { _senderFactory = value; }
        }

        private readonly string _environment = GetAppSettingString("Environment");
        private string Environment
        {
            get { return _environment; }
        }

        /// <summary>
        /// Defines the service bus connection string we are using, set in the config file.
        /// </summary>
        private readonly string _serviceBusConnectionString = GetAppSettingString("LoggingServiceBusConnectionString");
        private string ServiceBusConnectionString
        {
            get { return _serviceBusConnectionString; }
        }

        /// <summary>
        /// Defines the queue we will be sending our log messages to, set in the config file.
        /// </summary>
        private readonly string _queueName = GetAppSettingString("LoggingQueueName");
        private string QueueName
        {
            get { return _queueName; }
        }
        /// <summary>
        /// Defines the name of the aplication we are logging, set in the config file.
        /// </summary>
        private string _application = GetAppSettingString("LoggingApplication");

        public string Application
        {
            get { return _application; }
            set { _application = value; }
        }
        ///// <summary>
        ///// Defines the name of the aplication we are logging, set in the config file.
        ///// </summary>
        //private readonly string _application = GetAppSettingString("LoggingApplication");
        //private string Application
        //{
        //    get { return _application; }
        //}

        /// <summary>
        /// Defines the type of application we are logging, set in the config file.
        /// </summary>
        private readonly string _loggingApplicationType = GetAppSettingString("LoggingApplicationType");
        private string LoggingApplicationType
        {
            get { return _loggingApplicationType; }
        }

        /// <summary>
        /// Defines the logging location where we are logging, set in the config file.
        /// </summary>
        private readonly string _loggingLocation = GetAppSettingString("LoggingLocation");
        private string LoggingLocation
        {
            get { return _loggingLocation; }
        }

        /// <summary>
        /// Turns the logging on or off (true = On, false = Off), set in the config file.
        /// </summary>
        private readonly bool _loggingOn = (GetAppSettingBool("LoggingOn") == true);
        private bool LoggingOn
        {
            get { return _loggingOn; }
        }

        /// <summary>
        /// Turns the on or off (true = On, false = Off) for Information messages, set in the config file.
        /// </summary>
        private readonly bool _logInformationOn = (GetAppSettingBool("LogInformation") == true);
        private bool LogInformationOn
        {
            get { return _logInformationOn; }
        }

        /// <summary>
        /// Turns the on or off (true = On, false = Off) for ExecutionTime messages, set in the config file.
        /// </summary>
        private readonly bool _logExecutionTimeOn = (GetAppSettingBool("LogExecutionTime") == true);
        private bool LogExecutionTimeOn
        {
            get { return _logExecutionTimeOn; }
        }

        /// <summary>
        /// Turns the on or off (true = On, false = Off) for Tracing messages, set in the config file.
        /// </summary>
        private readonly bool _logTracingOn = (GetAppSettingBool("LogTracing") == true);
        private bool LogTracingOn
        {
            get { return _logTracingOn; }
        }

        /// <summary>
        /// Turns the on or off (true = On, false = Off) for Warning messages, set in the config file.
        /// </summary>
        private readonly bool _logWarningOn = (GetAppSettingBool("LogWarning") == true);
        private bool LogWarningOn
        {
            get { return _logWarningOn; }
        }

        /// <summary>
        /// Turns the on or off (true = On, false = Off) for Exception messages, set in the config file.
        /// </summary>
        private readonly bool _logExceptionOn = (GetAppSettingBool("LogException") == true);
        private bool LogExceptionOn
        {
            get { return _logExceptionOn; }
        }

        /// <summary>
        /// Max retrys to send a logging mesage to service bus.
        /// </summary>
        private readonly int _maxRetryCount = 3;
        private int MaxRetryCount
        {
            get { return _maxRetryCount; }
        }

        /// <summary>
        /// A list of messages GUIDs. Ecah time we try to send a message we add it to this list.
        /// If the count of the same GUID ion the list reaches the max rety count, we abor trying to send the message
        /// </summary>
        private readonly List<String> _messageRetryList = new List<String>();
        private List<String> MessageRetryList
        {
            get { return _messageRetryList; }
        }

        #endregion

        #region Logging Methods

        /// <summary>
        /// Validates the existance of each of our 'required' config file entries
        /// We throw an exception if one of them is missing.
        /// </summary>
        /// <returns></returns>
        private bool ValidateLoggingConfig()
        {

            string errorMessage = string.Empty;

            //see if they have an environment var
            if (Environment == string.Empty)
            {
                throw new Exception("Environment variable in config file is missing. This value is required for logging purposes.");
            }
            else
            {
                //see if the value is in our enum list, if not tell them
                try
                {
                    Enums.Environment env = Enums.Environment.None;
                    env = (Enums.Environment)Enum.Parse(typeof(Enums.Environment), Environment);
                }
                catch (Exception)
                {
                    StringBuilder enumList = new StringBuilder();
                    enumList.Append("(Valid values: ");
                    foreach (Enums.Environment env in Enum.GetValues(typeof(Enums.Environment)))
                    {
                        enumList.Append(env.ToString() + "|");
                    }
                    enumList.Append(")");

                    throw new Exception("Environment variable is not a valid Environment. " + enumList.ToString());

                }
            }

            //if we dont have a service bus connection string var
            if (ServiceBusConnectionString == string.Empty)
            {
                throw new Exception("Loggin Service Bus Connection String variable in config file is missing. This value is required for logging purposes.");
            }

            //if we dont have a queue name
            if (QueueName == string.Empty)
            {
                throw new Exception("Queue Name variable in config file is missing. This value is required for logging purposes.");
            }

            //if we dont have a application name var
            if (Application == string.Empty)
            {
                throw new Exception("Application Name variable in config file is missing. This value is required for logging purposes.");
            }

            //if we dont have an application type var
            if (LoggingApplicationType == string.Empty)
            {
                throw new Exception("Application Type variable in config file is missing. This value is required for logging purposes.");
            }
            else
            {
                //see if the value is in our enum list, if not tell them
                try
                {
                    Enums.LoggingApplicationType appType = Enums.LoggingApplicationType.None;
                    appType = (Enums.LoggingApplicationType)Enum.Parse(typeof(Enums.LoggingApplicationType), LoggingApplicationType);
                }
                catch (Exception)
                {
                    StringBuilder enumList = new StringBuilder();
                    enumList.Append("(Valid values: ");
                    foreach (Enums.LoggingApplicationType appType in Enum.GetValues(typeof(Enums.LoggingApplicationType)))
                    {
                        enumList.Append(appType.ToString() + "|");
                    }
                    enumList.Append(")");

                    throw new Exception("Application Type variable is not a valid Application Type. " + enumList.ToString());

                }
            }

            //if we dont have a location name var
            if (LoggingLocation == string.Empty)
            {
                throw new Exception("Location Name variable in config file is missing. This value is required for logging purposes.");
            }
            else
            {
                //if the location name var is not valid in our enum
                //see if the value is in our enum list, if not tell them
                try
                {
                    Enums.LoggingLocation location = Enums.LoggingLocation.None;
                    location = (Enums.LoggingLocation)Enum.Parse(typeof(Enums.LoggingLocation), LoggingLocation);
                }
                catch (Exception)
                {
                    StringBuilder enumList = new StringBuilder();
                    enumList.Append("(Valid values: ");
                    foreach (Enums.LoggingLocation location in Enum.GetValues(typeof(Enums.LoggingLocation)))
                    {
                        enumList.Append(location.ToString() + "|");
                    }
                    enumList.Append(")");

                    throw new Exception("Environment variable is not a valid Environment. " + enumList.ToString());

                }
            }

            return true;
        }

        /// <summary>
        /// Used to connect to the service bus.
        /// The application will reuse teh same connection for sending each message.
        /// if the connection happens to be closed this will open or reopen it.
        /// </summary>
        /// <returns></returns>
        private async Task<bool> ConnectToServiceBus()
        {
            bool returnVal = false;
            try
            {
                if (SenderFactory == null || SenderFactory.IsClosed == true || SenderFactory.Address == null)
                {
                    //create our sender Factory
                    SenderFactory = MessagingFactory.CreateFromConnectionString(ServiceBusConnectionString);
                }
                returnVal = true;

            }
            catch (Exception ex)
            {
                string loggingMessageText = "An exception occured in the 'ConnectToServiceBus' method. It has been ingonored, but logged, so we can be aware that it occured.";
                await InternalLogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), loggingMessageText, ex);
                returnVal = false;
            }
            return returnVal;
        }

        /// <summary>
        /// Adds a message GUID to our retry list
        /// </summary>
        /// <param name="messageGuid"></param>
        private void AddRetryId(string messageGuid)
        {
            MessageRetryList.Add(messageGuid);
        }

        /// <summary>
        /// Removes ALL entries for a given GUID
        /// </summary>
        /// <param name="messageGuid"></param>
        private void RemoveRetryId(string messageGuid)
        {
            try
            {
                MessageRetryList.RemoveAll(x => ((string)x) == messageGuid);
            }
            catch (Exception)
            {
                //could not find the item in the collection
                //since we are threaded, its possible it has already been removed.
                //do nothing
            }
        }

        /// <summary>
        /// Counts the number of times a given GUID is in the retry list.
        /// This defines how many times we have retried to send the message.
        /// </summary>
        /// <param name="messageGuid"></param>
        private int CountRetryId(string messageGuid)
        {
            int returnVal = 0;
            try
            {
                List<String> tempRetryList = MessageRetryList;
                List<string> countRetries = tempRetryList.Where(x => x.ToString() == messageGuid).ToList();
                returnVal = countRetries.Count;
            }
            catch (Exception)
            {
                //could not find the item in the collection
                //since we are threaded, its possible it has already been removed.
                returnVal = 0;
            }
            return returnVal;
        }

        #region logging with module and method

        /// <summary>
        /// Usedot log messages
        /// </summary>
        /// <param name="loggingPartionName"></param>
        /// <param name="loggingType"></param>
        /// <param name="methodBase"></param>
        /// <param name="loggingText"></param>
        /// <param name="applicationMessageGuid"></param>
        /// <param name="applicationSourceId"></param>
        /// <param name="applicationMessageData"></param>
        /// <param name="ex"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public async Task LogMessageAsync(string loggingPartionName, Meck.Logging.Enums.LoggingType loggingType,
            MethodBase methodBase, string loggingText = "", string applicationMessageGuid = "",
            string applicationSourceId = "", object applicationMessageData = null, Exception ex = null)
        {
            if (LoggingOn)
            {
                try
                {
                    if (applicationMessageGuid == string.Empty)
                    {
                        applicationMessageGuid = Guid.NewGuid().ToString();
                    }

                    bool logMessage = false;
                    //this section conrtols the logging state of different types
                    //if this is a tracing msg, and the application wants to log tracing messages
                    if ((loggingType == Enums.LoggingType.Tracing) && LogTracingOn)
                    {
                        logMessage = true;
                    }

                    //if this is a information msg, and the application wants to log information messages
                    if ((loggingType == Enums.LoggingType.Information) && LogInformationOn)
                    {
                        logMessage = true;
                    }

                    //if this is a warning msg, and the application wants to log warning messages
                    if ((loggingType == Enums.LoggingType.Warning) && LogWarningOn)
                    {
                        logMessage = true;
                    }

                    //if this is a exception msg, and the application wants to log exception messages
                    if ((loggingType == Enums.LoggingType.Exception) && LogExceptionOn)
                    {
                        logMessage = true;
                    }

                    //if this is a execution time msg, and the application wants to log execution time messages
                    if ((loggingType == Enums.LoggingType.ExecutionTime) && LogExecutionTimeOn)
                    {
                        logMessage = true;
                    }

                    if (logMessage && ValidateLoggingConfig())
                    {
                        //if they are passing a logging type of 'Information, Tracing or Warning' to this method,
                        //send a Warning logging message out, they should be using the base LogMessageAsync,
                        //not the overloaded method
                        if (loggingType == Enums.LoggingType.Information && ex != null)
                        {
                            string loggingMessageText =
                                "For Logging type 'Information' method does not accept the ex param.";
                            await InternalLogMessageAsync(Enums.LoggingType.Warning, MethodBase.GetCurrentMethod(),
                                loggingMessageText);
                        }

                        if (loggingType == Enums.LoggingType.Tracing && ex != null)
                        {
                            string loggingMessageText =
                                "For Logging type 'Tracing' method does not accept the ex param.";
                            await InternalLogMessageAsync(Enums.LoggingType.Warning, MethodBase.GetCurrentMethod(),
                                loggingMessageText);
                        }

                        if (loggingType == Enums.LoggingType.Warning && ex != null)
                        {
                            string loggingMessageText =
                                "For Logging type 'Warning' method does not accept the ex param.";
                            await InternalLogMessageAsync(Enums.LoggingType.Warning, MethodBase.GetCurrentMethod(),
                                loggingMessageText);
                        }

                        //If they are passing a logging type of exception to this method,
                        //send a warning logging message, they should be using the overloaded
                        //LogMessageAsync that takes the ex param
                        if (loggingType == Enums.LoggingType.Exception && ex == null)
                        {
                            string loggingMessageText = "For Logging type 'Exception' method needs the ex param.";
                            await InternalLogMessageAsync(Enums.LoggingType.Warning, MethodBase.GetCurrentMethod(),
                                loggingMessageText);
                        }

                        //check if applicationMessageData is string or Model and encrypt the data if it is not null
                        //if string, don't convert to json string else convert to json string
                        string updatedApplicationMessageData = string.Empty;
                        if (applicationMessageData != null)
                        {
                            if (applicationMessageData is string)
                            {
                                updatedApplicationMessageData = (string)applicationMessageData;
                            }
                            else
                            {
                                updatedApplicationMessageData = JsonConvert.SerializeObject(applicationMessageData);
                            }

                            updatedApplicationMessageData = Encryption.Encrypt(updatedApplicationMessageData);
                        }

                        Application = loggingPartionName;
                        //specific info for enterprise logging
                        LoggerModel internalLoggerModel = new LoggerModel
                        {
                            //use the application (name) as the partition
                            PartitionKey = GetApplication(),
                            LoggingType = loggingType.ToString(),
                            RowKey = Guid.NewGuid().ToString(),
                            LoggingApplicationType = LoggingApplicationType,
                            LoggingLocation = LoggingLocation,
                            //use UTC time, since this could be on prem or in azure (in any region) we need a consistent date to query against
                            LoggingDate = DateTime.UtcNow,
                            LoggingMachineName = System.Environment.MachineName.ToString(),

                            //specific logging info from application
                            Environment = Environment,
                            Application = Application,
                            LoggingText = loggingText,
                            ApplicationMessageGuid = applicationMessageGuid,
                            ApplicationSourceId = applicationSourceId,
                            ApplicationMessageData = updatedApplicationMessageData,
                            ModuleName = methodBase.Module.ToString(),
                            ClassName = methodBase.DeclaringType?.Name,
                            MethodName = methodBase.ToString(),
                            ExceptionText = ex != null ? ex.ToString() : string.Empty,
                            StackTrace = ex != null ? ex.StackTrace.ToString() : string.Empty
                        };

                        await SendMessageAsync(applicationMessageGuid, internalLoggerModel);
                    }
                }
                catch (Exception ex1)
                {
                    //do nothing, just ignore then log it and move on.
                    //retry logic will try to send message again.
                    string loggingMessageText =
                        "An exception occured in the overloaded 'ApplicationLogMessageAsync' method. It has been ignored, but logged, so we can be aware that it occured.";
                    await InternalLogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(),
                        loggingMessageText, ex1);
                }
            }
        }

        #endregion
        /// <summary>
        /// Used to log messages
        /// </summary>
        /// <param name="loggingType">Logging Type</param>
        /// <param name="methodBase">Name of method</param>
        /// <param name="loggingText">Logging Text</param>
        /// <param name="applicationMessageGuid">Guid that can be used to group logs for different transactions for same event or used individually</param>
        /// <param name="applicationSourceId">SourceId that may have relationship with data in ApplicationMessageData param</param>
        /// <param name="applicationMessageData">Model or Json string</param>
        /// <param name="ex">Exception</param>
        public async Task LogMessageAsync(Meck.Logging.Enums.LoggingType loggingType, MethodBase methodBase, string loggingText = "", string applicationMessageGuid = "",
                                                                                 string applicationSourceId = "", object applicationMessageData = null, Exception ex = null)
        {
            if (LoggingOn)
            {
                try
                {
                    if (applicationMessageGuid == string.Empty)
                    { applicationMessageGuid = Guid.NewGuid().ToString(); }

                    bool logMessage = false;
                    //this section conrtols the logging state of different types
                    //if this is a tracing msg, and the application wants to log tracing messages
                    if ((loggingType == Enums.LoggingType.Tracing) && LogTracingOn)
                    { logMessage = true; }

                    //if this is a information msg, and the application wants to log information messages
                    if ((loggingType == Enums.LoggingType.Information) && LogInformationOn)
                    { logMessage = true; }

                    //if this is a warning msg, and the application wants to log warning messages
                    if ((loggingType == Enums.LoggingType.Warning) && LogWarningOn)
                    { logMessage = true; }

                    //if this is a exception msg, and the application wants to log exception messages
                    if ((loggingType == Enums.LoggingType.Exception) && LogExceptionOn)
                    { logMessage = true; }

                    //if this is a execution time msg, and the application wants to log execution time messages
                    if ((loggingType == Enums.LoggingType.ExecutionTime) && LogExecutionTimeOn)
                    { logMessage = true; }

                    if (logMessage && ValidateLoggingConfig())
                    {
                        //if they are passing a logging type of 'Information, Tracing or Warning' to this method,
                        //send a Warning logging message out, they should be using the base LogMessageAsync,
                        //not the overloaded method
                        if (loggingType == Enums.LoggingType.Information && ex != null)
                        {
                            string loggingMessageText = "For Logging type 'Information' method does not accept the ex param.";
                            await InternalLogMessageAsync(Enums.LoggingType.Warning, MethodBase.GetCurrentMethod(), loggingMessageText);
                        }
                        if (loggingType == Enums.LoggingType.Tracing && ex != null)
                        {
                            string loggingMessageText = "For Logging type 'Tracing' method does not accept the ex param.";
                            await InternalLogMessageAsync(Enums.LoggingType.Warning, MethodBase.GetCurrentMethod(), loggingMessageText);
                        }
                        if (loggingType == Enums.LoggingType.Warning && ex != null)
                        {
                            string loggingMessageText = "For Logging type 'Warning' method does not accept the ex param.";
                            await InternalLogMessageAsync(Enums.LoggingType.Warning, MethodBase.GetCurrentMethod(), loggingMessageText);
                        }

                        //If they are passing a logging type of exception to this method,
                        //send a warning logging message, they should be using the overloaded
                        //LogMessageAsync that takes the ex param
                        if (loggingType == Enums.LoggingType.Exception && ex == null)
                        {
                            string loggingMessageText = "For Logging type 'Exception' method needs the ex param.";
                            await InternalLogMessageAsync(Enums.LoggingType.Warning, MethodBase.GetCurrentMethod(), loggingMessageText);
                        }

                        //check if applicationMessageData is string or Model and encrypt the data if it is not null
                        //if string, don't convert to json string else convert to json string
                        string updatedApplicationMessageData = string.Empty;
                        if (applicationMessageData != null)
                        {
                            if (applicationMessageData is string)
                            {
                                updatedApplicationMessageData = (string)applicationMessageData;
                            }
                            else
                            {
                                updatedApplicationMessageData = JsonConvert.SerializeObject(applicationMessageData);
                            }
                            updatedApplicationMessageData = Encryption.Encrypt(updatedApplicationMessageData);
                        }

                        //specific info for enterprise logging
                        LoggerModel internalLoggerModel = new LoggerModel
                        {
                            //use the application (name) as the partition
                            PartitionKey = GetApplication(),
                            LoggingType = loggingType.ToString(),
                            RowKey = Guid.NewGuid().ToString(),
                            LoggingApplicationType = LoggingApplicationType,
                            LoggingLocation = LoggingLocation,
                            //use UTC time, since this could be on prem or in azure (in any region) we need a consistent date to query against
                            LoggingDate = DateTime.UtcNow,
                            LoggingMachineName = System.Environment.MachineName.ToString(),

                            //specific logging info from application
                            Environment = Environment,
                            Application = Application,
                            LoggingText = loggingText,
                            ApplicationMessageGuid = applicationMessageGuid,
                            ApplicationSourceId = applicationSourceId,
                            ApplicationMessageData = updatedApplicationMessageData,
                            ModuleName = methodBase.Module.ToString(),
                            ClassName = methodBase.DeclaringType?.Name,
                            MethodName = methodBase.ToString(),
                            ExceptionText = ex != null ? ex.ToString() : string.Empty,
                            StackTrace = ex != null ? ex.StackTrace.ToString() : string.Empty
                        };

                        await SendMessageAsync(applicationMessageGuid, internalLoggerModel);
                    }
                }
                catch
                {
                    //do nothing, just ignore then log it and move on.
                    //retry logic will try to send message again.
                    string loggingMessageText = "An exception occured in the overloaded 'ApplicationLogMessageAsync' method. It has been ignored, but logged, so we can be aware that it occured.";
                    await InternalLogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), loggingMessageText, ex);
                }
            }
        }

        /// <summary>
        /// Append developer's machine name if the logging is run locally in Developer's machine,
        /// if not just log the logs under LoggingApplication name e.g. EnterprisePlanManagement.Manager.HZ4D3E2
        /// if developer is running code locally, else it will be 'EnterprisePlanManagement.Manager' if the code
        /// is not running locally. This is done so that developer can easily/quickly find their application related logs
        /// in storage explorer while working on code locally.
        /// </summary>
        /// <returns></returns>
        private string GetApplication()
        {
            string returnVal = Application;
            if (Environment == "DEV" && LoggingLocation == "OnPrem")
            {
                returnVal = $"{Application}.{System.Environment.MachineName}";
            }
            return returnVal;
        }

        /// <summary>
        /// Used to send messages to the service bus
        /// This also impliments the retry logic.
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="objectToSend"></param>
        private async Task SendMessageAsync(string messageGuid, object objectToSend)
        {
            try
            {
                //add our message to the retry list
                //one entry for every time we try to send message
                AddRetryId(messageGuid);

                var msgBody = JsonConvert.SerializeObject(objectToSend);

                LoggerModel loggerModel = new LoggerModel();

                //create our message
                var message = new BrokeredMessage(new MemoryStream(Encoding.UTF8.GetBytes(msgBody)))
                {
                    ContentType = "application/json",
                    Label = "MeckEnterpriseLogging",
                    MessageId = Guid.NewGuid().ToString(),
                };

                //connect to service bus, re-use if we can
                await ConnectToServiceBus();

                //create the sender and send our message
                MessageSender messageSender = SenderFactory.CreateMessageSender(QueueName);
                await messageSender.SendAsync(message);

                //Remove our message from the retry list as it has been sent successfully
                RemoveRetryId(messageGuid);
            }
            catch (System.TimeoutException toex)
            {
                //chill for a bit, if we are getting timeouts we may be sending too many messages.
                System.Threading.Thread.Sleep(20000);
                if (CountRetryId(messageGuid) <= MaxRetryCount)
                {
                    var task = SendMessageAsync(messageGuid, objectToSend);
                }
                else
                {
                    //Remove our message from list and just let the message die.
                    RemoveRetryId(messageGuid);
                    string loggingMessageText = "A 'System.TimeoutException' exception occured in the  'SendMessageAsync' method and we have exceeded the retry count. It has been ingonored, but logged, so we can be aware that it occured. ";
                    await InternalLogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), loggingMessageText, toex);
                }
            }
            catch (Exception ex)
            {
                //chill for a bit, if we are getting here, this could be any error
                //but likely a conection error.
                System.Threading.Thread.Sleep(20000);
                if (CountRetryId(messageGuid) <= MaxRetryCount)
                {
                    var task = SendMessageAsync(messageGuid, objectToSend);
                }
                else
                {
                    //Remove our message from list and just let the message die.
                    RemoveRetryId(messageGuid);
                    string loggingMessageText = "An exception has occured in the  'SendMessageAsync' method and we have exceeded the retry count. It has been ingonored, but logged, so we can be aware that it occured. ";
                    await InternalLogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), loggingMessageText, ex);
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string GetAppSettingString(string key)
        {
            string returnVal = string.Empty;
            if (ConfigurationManager.AppSettings[key] != null)
            {
                returnVal = ConfigurationManager.AppSettings[key];
            }
            return returnVal;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static bool GetAppSettingBool(string key)
        {
            bool returnVal = false;
            if (ConfigurationManager.AppSettings[key] != null)
            {
                returnVal = Convert.ToBoolean(ConfigurationManager.AppSettings[key].ToLower());
            }
            return returnVal;
        }

        #endregion

        #region Internal Logging methods for messages about logging

        ///  <summary>
        ///
        ///  </summary>
        /// <param name="loggingType"></param>
        /// <param name="methodBase"></param>
        /// <param name="loggingText"></param>
        /// <param name="ex"></param>
        ///  <returns></returns>
        private async Task InternalLogMessageAsync(Meck.Logging.Enums.LoggingType loggingType, MethodBase methodBase, string loggingText, Exception ex = null)
        {
            try
            {
                if (ValidateLoggingConfig())
                {
                    string messageGuid = Guid.NewGuid().ToString();

                    //specific info for enterprise logging
                    LoggerModel internalLoggerModel = new LoggerModel
                    {
                        //use the application (name) as the partition
                        PartitionKey = Application,
                        LoggingType = loggingType.ToString(),
                        RowKey = messageGuid,
                        LoggingApplicationType = LoggingApplicationType,
                        LoggingLocation = LoggingLocation,
                        //use UTC time, since this could be on prem or in azure (in any region) we need a consistent date to query against
                        LoggingDate = DateTime.UtcNow,
                        LoggingMachineName = System.Environment.MachineName.ToString(),

                        //specific logging info from the logging application
                        Environment = Environment,
                        Application = Application,
                        LoggingText = "From logging module - " + loggingText,
                        ModuleName = methodBase.Module.ToString(),
                        ClassName = methodBase.DeclaringType?.Name,
                        MethodName = methodBase.ToString(),

                        ExceptionText = ex != null ? ex.ToString() : string.Empty,
                        StackTrace = ex != null ? ex.StackTrace.ToString() : string.Empty
                    };

                    await SendMessageAsync(messageGuid, internalLoggerModel);
                }
            }
            catch
            {
                //do nothing, just ignore it and move on.
                //since this is logging for the logging logic
                //could make an infinite loop if we are not careful
            }
        }

        #endregion
    }
}
