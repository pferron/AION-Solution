using Meck.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;


namespace AION.BL.Common
{
    public class LoggingWrapper
    {

        #region Logging setup
        

        Logger _mLogger = new Logger();
        
        public Logger Logging
        {
            get { return _mLogger; }
            set { _mLogger = value; }
        }
        

        static Logger _mStaticLogger = new Logger();
        
        public Logger _mStaticLogging
        {
            get { return _mStaticLogger; }
            set { _mStaticLogger = value; }
        }

        #endregion



        public async Task<bool>  BLLogMessageAsync(MethodBase callingMethod, string errorMessage, Exception exInput)
      {
           string _loggingPartionName = ConfigurationManager.AppSettings["LoggingApplication"];

           try
           {
               if (exInput == null)
               {
                   //  await _mLogger.LogMessageAsync(_loggingPartionName, Meck.Logging.Enums.LoggingType.Information, callingMethod, loggingText: errorMessage, ex: ex));

                   //  await _mLogger.LogMessageAsync(Meck.Logging.Enums.LoggingType.Information, callingMethod, loggingText: errorMessage, ex: ex);

                   await _mLogger.LogMessageAsync(_loggingPartionName, Meck.Logging.Enums.LoggingType.Information,
                       callingMethod, loggingText: errorMessage, ex: exInput);
               }
               else
               {
                   await _mLogger.LogMessageAsync(_loggingPartionName, Meck.Logging.Enums.LoggingType.Exception,
                       callingMethod, loggingText: errorMessage, ex: exInput);
               }

               return true;
           }
           catch (Exception ex)
           {
               throw new Exception("Logging Failure with "+ exInput.Message , ex);
           }
      }

      public bool BLLogMessage(MethodBase callingMethod, string errorMessage, Exception exInput)
      {
          string _loggingPartionName = ConfigurationManager.AppSettings["LoggingApplication"];

          try
          {
              if (exInput == null)
              {
                  Task.Run(() => _mLogger.LogMessageAsync(_loggingPartionName,
                      Meck.Logging.Enums.LoggingType.Information, callingMethod, loggingText: errorMessage, ex: exInput));
              }
              else
              {
                  Task.Run(() => _mLogger.LogMessageAsync(_loggingPartionName, Meck.Logging.Enums.LoggingType.Exception,
                      callingMethod, loggingText: errorMessage, ex: exInput));
              }

              return true; 
          }
          catch (Exception ex)
          {
               throw new Exception("Logging Exception with " + exInput.Message, ex);
    
          }

      }

      public static bool staticBLLogMessage(MethodBase callingMethod, string errorMessage, Exception exInput)
      {
          string _loggingPartionName = ConfigurationManager.AppSettings["LoggingApplication"];
          try
          {
              if (exInput == null)
              {
                  Task.Run(() => _mStaticLogger.LogMessageAsync(_loggingPartionName,
                      Meck.Logging.Enums.LoggingType.Information, callingMethod, loggingText: errorMessage,
                      ex: exInput));
              }
              else
              {
                  Task.Run(() => _mStaticLogger.LogMessageAsync(_loggingPartionName,
                      Meck.Logging.Enums.LoggingType.Exception, callingMethod, loggingText: errorMessage, ex: exInput));
              }

              return true;
          }
          catch (Exception ex)
          {
              throw new Exception(" Logging Failure with " + exInput.Message, ex); 
                
          }
      }
    }
}