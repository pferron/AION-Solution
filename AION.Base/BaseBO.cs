using Meck.Azure;
using System;
using System.Configuration;
using Meck.Shared;


namespace AION.Base
{
    public class BaseBO
    {

        #region Properties

        public string ConnectionString
        {
            get
            {
                //string connectionstring = System.Web.HttpContext.Current.Application["AionConnectionString"].ToString();
                //grab connection from globals object
                string connectionstring = Globals.AIONConnectionString;

                //if we don't have a db connection string go get it from keyvault
                if (String.IsNullOrWhiteSpace(connectionstring))
                {
                    connectionstring = KeyVaultUtility.GetSecret("KeyVaultConnectionString");

                    //set it in globals object
				    Globals.AIONConnectionString = connectionstring; 	 
                }
                return connectionstring;
            }
        }

        #endregion

        #region Protected Methods
        protected static T TryToParse<T>(object objectValue)
        {
            //Integer
            if (typeof(T) == typeof(int))
            {
                int intValue;
                return Int32.TryParse(System.Convert.ToString(objectValue), out intValue) ? (T)Convert.ChangeType(intValue, typeof(object)) : default(T);

            }

            //Nullable Integer
            if (typeof(T) == typeof(int?))
            {
                int intValue;
                return Int32.TryParse(System.Convert.ToString(objectValue), out intValue) ? (T)Convert.ChangeType(intValue, typeof(object)) : default(T);

            }

            //Nullable Boolean
            else if (typeof(T) == typeof(bool?))
            {
                bool booleanValue;
                return Boolean.TryParse(System.Convert.ToString(objectValue), out booleanValue) ? (T)Convert.ChangeType(booleanValue, typeof(object)) : default(T);
            }

            // Boolean
            else if (typeof(T) == typeof(bool))
            {
                bool booleanValue;
                return Boolean.TryParse(System.Convert.ToString(objectValue), out booleanValue) ? (T)Convert.ChangeType(booleanValue, typeof(object)) : default(T);
            }

            //Nullable Decimal
            else if (typeof(T) == typeof(decimal?))
            {
                decimal decimalValue;
                return Decimal.TryParse(System.Convert.ToString(objectValue), out decimalValue) ? (T)Convert.ChangeType(decimalValue, typeof(object)) : default(T);
            }


            //Nullable DateTime
            else if (typeof(T) == typeof(DateTime?))
            {
                DateTime dateTimeValue;
                return DateTime.TryParse(System.Convert.ToString(objectValue), out dateTimeValue) ? (T)Convert.ChangeType(dateTimeValue, typeof(object)) : default(T);


            }


            //String
            else if (typeof(T) == typeof(string))
            {
                return !(objectValue == DBNull.Value) ? (T)Convert.ChangeType(objectValue, typeof(T)) : default(T);
            }

            //Default
            else
                return default(T);
        }
        #endregion

    }

}
