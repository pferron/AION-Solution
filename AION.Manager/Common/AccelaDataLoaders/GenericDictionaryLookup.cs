using Posse.Accela.Engine.Helpers;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Posse.Data.Engine.Helpers
{
    public static class GenericDictionaryLookup
    {

        public static object  GetDictionaryValueByKey(Dictionary<String, object> dictSource, string keyValue)
        {
            try
            {

                object mDictObjectOut;

                dictSource.TryGetValue(keyValue, out mDictObjectOut);

                return mDictObjectOut;
            }

            catch (Exception ex)
            {
                string objname = dictSource.GetType().Name.ToString();

                string ErrMsg = "Error Extracting Accela Value from Record " + dictSource.GetType().Name.ToString()+ "KeyValue: " + keyValue + " - "+ ex.Message + " - " + DateTime.Now;

               LoggingWrapper.staticBLLogMessage(MethodBase.GetCurrentMethod(), ErrMsg, ex); 
                
              return null; 
            }

        }
    }
}