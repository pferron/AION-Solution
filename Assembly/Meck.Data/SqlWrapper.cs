using Meck.Logging;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Net;
using System.Reflection;

namespace Meck.Data
{
    public class SqlWrapper
    {

        #region Properties

        static Logger m_Logger = new Logger();
        public static Logger Logging
        {
            get { return m_Logger; }
            set { m_Logger = value; }
        }

        #endregion

        public static void RunSP(string spName, string connectionString)
        {
            //Adding logging for execution time for every db call -wkb
            //Assuming ExecutionTime Logging is turned on, we should see how long some these calls are taking 

          //  string message1 = "Connection String: " + connectionString;
           // var Tracing = Logging.LogMessageAsync(Meck.Logging.Enums.LoggingType.Tracing, MethodBase.GetCurrentMethod(), message1, string.Empty, String.Empty, String.Empty);


         //   string hostName = Dns.GetHostName(); // Retrive the Name of HOST
           // string message2 = "Host Name: " + hostName;
            //var Tracing2 = Logging.LogMessageAsync(Meck.Logging.Enums.LoggingType.Tracing, MethodBase.GetCurrentMethod(), message2, string.Empty, String.Empty, String.Empty);

            Stopwatch watch = new Stopwatch();
            watch.Start();

            int recordsAffected;
            //  run the stored procedure 
            try
            {
                recordsAffected = SqlHelper.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName);
                string message = "SqlWrapper RunSP Execution Time: " + watch.ElapsedMilliseconds + " milliseconds to execute the stored procedure " + spName + ".";
                var logging = Logging.LogMessageAsync(Meck.Logging.Enums.LoggingType.ExecutionTime, MethodBase.GetCurrentMethod(), message, string.Empty, String.Empty, String.Empty);

            }
            catch (Exception ex)
            {
                string errorMessage = "An Error occured in SqlWrapper RunSP.";
                var logging = Logging.LogMessageAsync(Meck.Logging.Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage
                      , string.Empty, string.Empty, string.Empty, ex);
                throw ex;
            }
            finally
            { watch.Stop(); }

        }

        public static void RunSP(string spName, string connectionString, ref SqlParameter[] sqlParams)
        {
            //Adding logging for execution time for every db call -wkb
            //Assuming ExecutionTime Logging is turned on, we should see how long some these calls are taking 

            //string message1 = "Connection String: " + connectionString;
            //var Tracing = Logging.LogMessageAsync(Meck.Logging.Enums.LoggingType.Tracing, MethodBase.GetCurrentMethod(), message1, string.Empty, String.Empty, String.Empty);


          //  string hostName = Dns.GetHostName(); // Retrive the Name of HOST
           // string message2 = "Host Name: " + hostName;
            //var Tracing2 = Logging.LogMessageAsync(Meck.Logging.Enums.LoggingType.Tracing, MethodBase.GetCurrentMethod(), message2, string.Empty, String.Empty, String.Empty);

            Stopwatch watch = new Stopwatch();
            watch.Start();

            int recordsAffected;
            //  run the stored procedure 
            try
            {
                recordsAffected = SqlHelper.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName, sqlParams);
                string message = "SqlWrapper RunSP [with params] Execution Time: " + watch.ElapsedMilliseconds + " milliseconds to execute the stored procedure " + spName + ".";
                var logging = Logging.LogMessageAsync(Meck.Logging.Enums.LoggingType.ExecutionTime, MethodBase.GetCurrentMethod(), message, string.Empty, String.Empty, String.Empty);
            }
            catch (Exception ex)
            {
                string errorMessage = "An Error occured in SqlWrapper RunSP [with params].";
                var logging = Logging.LogMessageAsync(Meck.Logging.Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage
                      , string.Empty, string.Empty, string.Empty, ex);
                throw ex;
            }
            finally
            { watch.Stop(); }
        }

        public static DataSet RunSPReturnDS(string spName, string connectionString)
        {
            //Adding logging for execution time for every db call -wkb
            //Assuming ExecutionTime Logging is turned on, we should see how long some these calls are taking 

           // string message1 = "Connection String: " + connectionString;
            //var Tracing = Logging.LogMessageAsync(Meck.Logging.Enums.LoggingType.Tracing, MethodBase.GetCurrentMethod(), message1, string.Empty, String.Empty, String.Empty);


          //  string hostName = Dns.GetHostName(); // Retrive the Name of HOST
           // string message2 = "Host Name: " + hostName;
            //var Tracing2 = Logging.LogMessageAsync(Meck.Logging.Enums.LoggingType.Tracing, MethodBase.GetCurrentMethod(), message2, string.Empty, String.Empty, String.Empty);

            Stopwatch watch = new Stopwatch();
            watch.Start();

            //  run the stored procedure 
            DataSet ds = new DataSet();

            try
            {
                ds = SqlHelper.ExecuteDataset(connectionString, CommandType.StoredProcedure, spName);
                string message = "SqlWrapper RunSPReturnDS Execution Time: " + watch.ElapsedMilliseconds + " milliseconds to execute the stored procedure " + spName + ".";
                var logging = Logging.LogMessageAsync(Meck.Logging.Enums.LoggingType.ExecutionTime, MethodBase.GetCurrentMethod(), message, string.Empty, String.Empty, String.Empty);

                return ds;
            }
            catch (Exception ex)
            {
                string errorMessage = "An Error occured in SqlWrapper RunSPReturnDS.";
                var logging = Logging.LogMessageAsync(Meck.Logging.Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage
                      , string.Empty, string.Empty, string.Empty, ex);
                throw ex;
            }
            finally
            { watch.Stop(); }
        }

        public static DataSet RunSPReturnDS(string spName, string connectionString, ref SqlParameter[] sqlParams)
        {
            //Adding logging for execution time for every db call -wkb
            //Assuming ExecutionTime Logging is turned on, we should see how long some these calls are taking 

          //  string message1 = "Connection String: " + connectionString;
           // var Tracing = Logging.LogMessageAsync(Meck.Logging.Enums.LoggingType.Tracing, MethodBase.GetCurrentMethod(), message1, string.Empty, String.Empty, String.Empty);


          //  string hostName = Dns.GetHostName(); // Retrive the Name of HOST
          //  string message2 = "Host Name: " + hostName;
           // var Tracing2 = Logging.LogMessageAsync(Meck.Logging.Enums.LoggingType.Tracing, MethodBase.GetCurrentMethod(), message2, string.Empty, String.Empty, String.Empty);

            Stopwatch watch = new Stopwatch();
            watch.Start();

            //  run the stored procedure 
            DataSet ds = new DataSet();

            try
            {
                ds = SqlHelper.ExecuteDataset(connectionString, CommandType.StoredProcedure, spName, sqlParams);
                string message = "SqlWrapper RunSPReturnDS [with params] Execution Time: " + watch.ElapsedMilliseconds + " milliseconds to execute the stored procedure " + spName + ".";
                var logging = Logging.LogMessageAsync(Meck.Logging.Enums.LoggingType.ExecutionTime, MethodBase.GetCurrentMethod(), message, string.Empty, String.Empty, String.Empty);

                return ds;
            }
            catch (Exception ex)
            {
                string errorMessage = "An Error occured in SqlWrapper RunSPReturnDS [with params].";
                var logging = Logging.LogMessageAsync(Meck.Logging.Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage
                      , string.Empty, string.Empty, string.Empty, ex);
                throw ex;
            }
            finally
            { watch.Stop(); }
        }

        //  This method fills the dataset by executing the SP with input parameters and the filled dataset's name will be name of dsName parameter \
        //  and the dataset contains Data Table with tbName parameter value.
        public static DataSet RunSPFillDS(string spName, string connectionString, DataSet dsName, string[] tbName, ref SqlParameter[] sqlParams)
        {
            //Adding logging for execution time for every db call -wkb
            //Assuming ExecutionTime Logging is turned on, we should see how long some these calls are taking 

           // string message1 = "Connection String: " + connectionString;
           // var Tracing = Logging.LogMessageAsync(Meck.Logging.Enums.LoggingType.Tracing, MethodBase.GetCurrentMethod(), message1, string.Empty, String.Empty, String.Empty);


           // string hostName = Dns.GetHostName(); // Retrive the Name of HOST
          //  string message2 = "Host Name: " + hostName;
           // var Tracing2 = Logging.LogMessageAsync(Meck.Logging.Enums.LoggingType.Tracing, MethodBase.GetCurrentMethod(), message2, string.Empty, String.Empty, String.Empty);

            Stopwatch watch = new Stopwatch();
            watch.Start();

            //  run the stored procedure 
            DataSet ds = new DataSet();
            try
            {
                //  execute query 
                SqlHelper.FillDataset(connectionString, spName, ds, tbName, sqlParams);
                string message = "SqlWrapper RunSPFillDS [with params] Execution Time: " + watch.ElapsedMilliseconds + " milliseconds to execute the stored procedure " + spName + ".";
                var logging = Logging.LogMessageAsync(Meck.Logging.Enums.LoggingType.ExecutionTime, MethodBase.GetCurrentMethod(), message, string.Empty, String.Empty, String.Empty);

                return ds;
            }
            catch (SqlException ex)
            {
                string errorMessage = "An Error occured in SqlWrapper RunSPFillDS [with params].";
                var logging = Logging.LogMessageAsync(Meck.Logging.Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage
                      , string.Empty, string.Empty, string.Empty, ex);
                throw new Exception("RunSPFillDS" + spName + " " + ex.ToString(), ex);
            }
            catch (Exception ex)
            {
                string errorMessage = "An Error occured in SqlWrapper RunSPFillDS [with params].";
                var logging = Logging.LogMessageAsync(Meck.Logging.Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage
                      , string.Empty, string.Empty, string.Empty, ex);
                throw new Exception("RunSPFillDS" + spName + " " + ex.ToString(), ex);
            }
            finally
            { watch.Stop(); }
        }

        public static int RunSPReturnInteger(string spName, string connectionString)
        {
            //Adding logging for execution time for every db call -wkb
            //Assuming ExecutionTime Logging is turned on, we should see how long some these calls are taking 

           // string message1 = "Connection String: " + connectionString;
           // var Tracing = Logging.LogMessageAsync(Meck.Logging.Enums.LoggingType.Tracing, MethodBase.GetCurrentMethod(), message1, string.Empty, String.Empty, String.Empty);


           // string hostName = Dns.GetHostName(); // Retrive the Name of HOST
           // string message2 = "Host Name: " + hostName;
           // var Tracing2 = Logging.LogMessageAsync(Meck.Logging.Enums.LoggingType.Tracing, MethodBase.GetCurrentMethod(), message2, string.Empty, String.Empty, String.Empty);

            Stopwatch watch = new Stopwatch();
            watch.Start();

            SqlParameter[] sqlParams = new SqlParameter[1];

            //  run the stored procedure 
            try
            {
                //  create our "retval" 
                sqlParams[0] = new SqlParameter("@ReturnValue", SqlDbType.Int);
                sqlParams[0].Direction = ParameterDirection.Output;

                //  execute query 
                SqlHelper.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName, sqlParams);
                string message = "SqlWrapper RunSPReturnInteger Execution Time: " + watch.ElapsedMilliseconds + " milliseconds to execute the stored procedure " + spName + ".";
                var logging = Logging.LogMessageAsync(Meck.Logging.Enums.LoggingType.ExecutionTime, MethodBase.GetCurrentMethod(), message, string.Empty, String.Empty, String.Empty);

                //  get our integer return 
                return System.Convert.IsDBNull(sqlParams[0].Value) ? 0 : System.Convert.ToInt32(sqlParams[0].Value);
            }

            catch (SqlException ex)
            {
                string errorMessage = "An Error occured in SqlWrapper RunSPReturnInteger.";
                var logging = Logging.LogMessageAsync(Meck.Logging.Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage
                      , string.Empty, string.Empty, string.Empty, ex);
                throw new Exception("RunSPReturnInteger" + spName + " " + ex.ToString(), ex);
            }
            catch (Exception ex)
            {
                string errorMessage = "An Error occured in SqlWrapper RunSPReturnInteger.";
                var logging = Logging.LogMessageAsync(Meck.Logging.Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage
                      , string.Empty, string.Empty, string.Empty, ex);
                throw new Exception("RunSPReturnInteger" + spName + " " + ex.ToString(), ex);
            }
            finally
            { watch.Stop(); }
        }

        public static int RunSPReturnInteger(string spName, string connectionString, ref SqlParameter[] sqlParams)
        {
            //Adding logging for execution time for every db call -wkb
            //Assuming ExecutionTime Logging is turned on, we should see how long some these calls are taking 

           // string message1 = "Connection String: " + connectionString;
           // var Tracing = Logging.LogMessageAsync(Meck.Logging.Enums.LoggingType.Tracing, MethodBase.GetCurrentMethod(), message1, string.Empty, String.Empty, String.Empty);


          //  string hostName = Dns.GetHostName(); // Retrive the Name of HOST
          //  string message2 = "Host Name: " + hostName;
          //  var Tracing2 = Logging.LogMessageAsync(Meck.Logging.Enums.LoggingType.Tracing, MethodBase.GetCurrentMethod(), message2, string.Empty, String.Empty, String.Empty);

            Stopwatch watch = new Stopwatch();
            watch.Start();

            int id;
            //  run the stored procedure 
            try
            {
                //  execute query 
                SqlHelper.ExecuteNonQuery(connectionString, CommandType.StoredProcedure, spName, sqlParams);
                string message = "SqlWrapper RunSPReturnInteger [with params] Execution Time: " + watch.ElapsedMilliseconds + " milliseconds to execute the stored procedure " + spName + ".";
                var logging = Logging.LogMessageAsync(Meck.Logging.Enums.LoggingType.ExecutionTime, MethodBase.GetCurrentMethod(), message, string.Empty, String.Empty, String.Empty);

                if (sqlParams[sqlParams.GetUpperBound(0)].ParameterName == "@ReturnValue")
                {
                    id = Convert.ToInt32(sqlParams[sqlParams.GetUpperBound(0)].Value);
                }
                else
                {
                    id = 0;
                }

                return id;
            }
            //catch (SqlException e)
            //{
            //    throw new Exception("RunSPReturnInteger" + spName + " " + e.ToString(), e);
            //}
            catch (Exception ex)
            {
                string errorMessage = "An Error occured in SqlWrapper RunSPReturnInteger [with params].";
                var logging = Logging.LogMessageAsync(Meck.Logging.Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage
                      , string.Empty, string.Empty, string.Empty, ex);
                throw new Exception("RunSPReturnInteger" + spName + " " + ex.ToString(), ex);
            }
            finally
            { watch.Stop(); }
        }

        public static SqlDataReader RunSPReturnReader(string spName, string connectionString)
        {
            //Adding logging for execution time for every db call -wkb
            //Assuming ExecutionTime Logging is turned on, we should see how long some these calls are taking 

           // string message1 = "Connection String: " + connectionString;
           // var Tracing = Logging.LogMessageAsync(Meck.Logging.Enums.LoggingType.Tracing, MethodBase.GetCurrentMethod(), message1, string.Empty, String.Empty, String.Empty);


          //  string hostName = Dns.GetHostName(); // Retrive the Name of HOST
          //  string message2 = "Host Name: " + hostName;
          //  var Tracing2 = Logging.LogMessageAsync(Meck.Logging.Enums.LoggingType.Tracing, MethodBase.GetCurrentMethod(), message2, string.Empty, String.Empty, String.Empty);

            Stopwatch watch = new Stopwatch();
            watch.Start();

            SqlDataReader dr;
            //  run the stored procedure
            try
            {
                //  execute query 
                dr = SqlHelper.ExecuteReader(connectionString, CommandType.StoredProcedure, spName);
                string message = "SqlWrapper RunSPReturnReader Execution Time: " + watch.ElapsedMilliseconds + " milliseconds to execute the stored procedure " + spName + ".";
                var logging = Logging.LogMessageAsync(Meck.Logging.Enums.LoggingType.ExecutionTime, MethodBase.GetCurrentMethod(), message, string.Empty, String.Empty, String.Empty);

                return dr;
            }
            catch (SqlException ex)
            {
                string errorMessage = "An Error occured in SqlWrapper RunSPReturnReader.";
                var logging = Logging.LogMessageAsync(Meck.Logging.Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage
                      , string.Empty, string.Empty, string.Empty, ex);
                throw new Exception("RunSPReturnReader" + spName + " " + ex.ToString(), ex);
            }
            catch (Exception ex)
            {
                string errorMessage = "An Error occured in SqlWrapper RunSPReturnReader.";
                var logging = Logging.LogMessageAsync(Meck.Logging.Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage
                      , string.Empty, string.Empty, string.Empty, ex);
                throw new Exception("RunSPReturnReader" + spName + " " + ex.ToString(), ex);
            }
            finally
            { watch.Stop(); }
        }

        public static SqlDataReader RunSPReturnReader(string spName, string connectionString, ref SqlParameter[] sqlParams)
        {
            //Adding logging for execution time for every db call -wkb
            //Assuming ExecutionTime Logging is turned on, we should see how long some these calls are taking 

          //  string message1 = "Connection String: " + connectionString;
           // var Tracing = Logging.LogMessageAsync(Meck.Logging.Enums.LoggingType.Tracing, MethodBase.GetCurrentMethod(), message1, string.Empty, String.Empty, String.Empty);


           // string hostName = Dns.GetHostName(); // Retrive the Name of HOST
          //  string message2 = "Host Name: " + hostName;
           // var Tracing2 = Logging.LogMessageAsync(Meck.Logging.Enums.LoggingType.Tracing, MethodBase.GetCurrentMethod(), message2, string.Empty, String.Empty, String.Empty);

            Stopwatch watch = new Stopwatch();
            watch.Start();

            SqlDataReader dr;
            //  run the stored procedure
            try
            {
                //  execute query 
                dr = SqlHelper.ExecuteReader(connectionString, CommandType.StoredProcedure, spName, sqlParams);
                string message = "SqlWrapper RunSPReturnReader [with params] Execution Time: " + watch.ElapsedMilliseconds + " milliseconds to execute the stored procedure " + spName + ".";
                var logging = Logging.LogMessageAsync(Meck.Logging.Enums.LoggingType.ExecutionTime, MethodBase.GetCurrentMethod(), message, string.Empty, String.Empty, String.Empty);

                return dr;
            }
            catch (SqlException e)
            {
                throw new Exception("RunSPReturnReader" + spName + " " + e.ToString(), e);
            }
            catch (Exception e)
            {
                throw new Exception("RunSPReturnReader" + spName + " " + e.ToString(), e);
            }
            finally
            { watch.Stop(); }
        }

        public static SqlDataReader RunSQLReturnReader(string sql, string connectionString)
        {
            //Adding logging for execution time for every db call -wkb
            //Assuming ExecutionTime Logging is turned on, we should see how long some these calls are taking 

         //   string message1 = "Connection String: " + connectionString;
          //  var Tracing = Logging.LogMessageAsync(Meck.Logging.Enums.LoggingType.Tracing, MethodBase.GetCurrentMethod(), message1, string.Empty, String.Empty, String.Empty);


         //   string hostName = Dns.GetHostName(); // Retrive the Name of HOST
         //   string message2 = "Host Name: " + hostName;
          //  var Tracing2 = Logging.LogMessageAsync(Meck.Logging.Enums.LoggingType.Tracing, MethodBase.GetCurrentMethod(), message2, string.Empty, String.Empty, String.Empty);

            Stopwatch watch = new Stopwatch();
            watch.Start();

            SqlDataReader dr;
            //  run the stored procedure
            try
            {
                //  execute query 
                dr = SqlHelper.ExecuteReader(connectionString, CommandType.Text, sql);
                string message = "SqlWrapper RunSQLReturnReader Execution Time: " + watch.ElapsedMilliseconds + " milliseconds to execute the sql " + sql + ".";
                var logging = Logging.LogMessageAsync(Meck.Logging.Enums.LoggingType.ExecutionTime, MethodBase.GetCurrentMethod(), message, string.Empty, String.Empty, String.Empty);

                return dr;
            }
            catch (SqlException ex)
            {
                string errorMessage = "An Error occured in SqlWrapper RunSQLReturnReader.";
                var logging = Logging.LogMessageAsync(Meck.Logging.Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage
                      , string.Empty, string.Empty, string.Empty, ex);
                throw new Exception("RunSPReturnReader" + sql + " " + ex.ToString(), ex);
            }
            catch (Exception ex)
            {
                string errorMessage = "An Error occured in SqlWrapper RunSQLReturnReader.";
                var logging = Logging.LogMessageAsync(Meck.Logging.Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage
                      , string.Empty, string.Empty, string.Empty, ex);
                throw new Exception("RunSPReturnReader" + sql + " " + ex.ToString(), ex);
            }
            finally
            { watch.Stop(); }
        }

        public static DataSet RunSQLReturnDS(string sql, string connectionString)
        {
            //Adding logging for execution time for every db call -wkb
            //Assuming ExecutionTime Logging is turned on, we should see how long some these calls are taking 

          //  string message1 = "Connection String: " + connectionString;
          //  var Tracing = Logging.LogMessageAsync(Meck.Logging.Enums.LoggingType.Tracing, MethodBase.GetCurrentMethod(), message1, string.Empty, String.Empty, String.Empty);


          //  string hostName = Dns.GetHostName(); // Retrive the Name of HOST
           // string message2 = "Host Name: " + hostName;
          //  var Tracing2 = Logging.LogMessageAsync(Meck.Logging.Enums.LoggingType.Tracing, MethodBase.GetCurrentMethod(), message2, string.Empty, String.Empty, String.Empty);

            Stopwatch watch = new Stopwatch();
            watch.Start();

            DataSet ds;
            //  run the stored procedure
            try
            {
                //  execute query 
                ds = SqlHelper.ExecuteDataset(connectionString, CommandType.Text, sql);
                string message = "SqlWrapper RunSQLReturnDS Execution Time: " + watch.ElapsedMilliseconds + " milliseconds to execute the sql " + sql + ".";
                var logging = Logging.LogMessageAsync(Meck.Logging.Enums.LoggingType.ExecutionTime, MethodBase.GetCurrentMethod(), message, string.Empty, String.Empty, String.Empty);

                return ds;
            }
            catch (SqlException ex)
            {
                string errorMessage = "An Error occured in SqlWrapper RunSQLReturnDS.";
                var logging = Logging.LogMessageAsync(Meck.Logging.Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage
                      , string.Empty, string.Empty, string.Empty, ex);
                throw new Exception("RunSQLReturnDS" + sql + " " + ex.ToString(), ex);
            }
            catch (Exception ex)
            {
                string errorMessage = "An Error occured in SqlWrapper RunSQLReturnDS.";
                var logging = Logging.LogMessageAsync(Meck.Logging.Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage
                      , string.Empty, string.Empty, string.Empty, ex);
                throw new Exception("RunSQLReturnDS" + sql + " " + ex.ToString(), ex);
            }
            finally
            { watch.Stop(); }
        }
    }
}