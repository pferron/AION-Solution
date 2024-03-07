using System;
using System.Configuration;
using System.IO;
using System.Threading.Tasks;

namespace AION.Accela.Engine.BusinessObjects
{
    public class AccelaApiJsonBO : AccelaBase
    {
        #region Bring Back Json File For Testing
        public async Task<string> GetRawAccelaRecordJsonTestFile()
        {
            string _filename = AppContext.BaseDirectory + ConfigurationManager.AppSettings["projectJSONFILE"].ToString();
            
            //string _filename = "C:\\Repos\\AION\\AION Solution\\WindowsFormsMMFDemo\\DataFiles\\projects.json";
          
            try
            {

                return  Task.Run(() => LoadJsonReturnsString(_filename)).Result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

        }
        public async Task<string> GetAllFacilitatorsJsonTestFile()
        {
            string _filename = AppContext.BaseDirectory + ConfigurationManager.AppSettings["facilitatorJSONFILE"].ToString();

            try
            {
                return Task.Run(() => LoadJsonReturnsString(_filename)).Result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

        }
        public async Task<string> GetAllReviewersJsonTestFile()
        {
            string _filename = AppContext.BaseDirectory + ConfigurationManager.AppSettings["reviewerJSONFILE"].ToString();

            try
            {
                return Task.Run(() => LoadJsonReturnsString(_filename)).Result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

        }
        public async Task<string> GetAllEstimatorsJsonTestFile()
        {
            string _filename = AppContext.BaseDirectory + ConfigurationManager.AppSettings["estimatorJSONFILE"].ToString();

            try
            {
                return Task.Run(() => LoadJsonReturnsString(_filename)).Result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

        }

        public async Task<string> GetAllPlanReviewCyclesJsonTestFile()
        {
            string _filename = AppContext.BaseDirectory + ConfigurationManager.AppSettings["cycleJSONFILE"].ToString();

            try
            {
                return Task.Run(() => LoadJsonReturnsString(_filename)).Result;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public async Task<string> GetAllFacilitatorMeetingsJsonTestFile()
        {
            string _filename = AppContext.BaseDirectory + ConfigurationManager.AppSettings["meetingJSONFILE"].ToString();

            try
            {
                return Task.Run(() => LoadJsonReturnsString(_filename)).Result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
        }

        public async Task<string> GetPlanReviewStatusListJsonTestFile()
        {
            string _filename = AppContext.BaseDirectory + ConfigurationManager.AppSettings["planReviewStatusJSONFILE"].ToString();

            try
            {
                return Task.Run(() => LoadJsonReturnsString(_filename)).Result;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }

        }


        private string LoadJsonReturnsString(string filename)
        {
            using (StreamReader r = new StreamReader(filename))
            {
                string json = r.ReadToEnd();
                return json;
            }

        }

        #endregion


    }
}
