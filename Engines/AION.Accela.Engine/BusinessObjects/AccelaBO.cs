
using Meck.Shared.Accela;
using System;
using System.Threading.Tasks;

namespace AION.Accela.Engine.BusinessObjects
{
    public partial class AccelaOldBO : AccelaBase
    {

      

        #region Accela API


        public async Task<string> GetRecords()
        {
            AccelaApiJsonBO _mAccelaApiBO = new AccelaApiJsonBO();

            //var result = await _mAccelaApiBO.GetRawAccelaRecord(AccelaRecordId);
            var result = await _mAccelaApiBO.GetRawAccelaRecordJsonTestFile();

            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        public async Task<string> GetallEstimators()
        {

            AccelaApiJsonBO _mAccelaApiBO = new AccelaApiJsonBO();

            //var result = await _mAccelaApiBO.GetAllEstimators();
            var result = await _mAccelaApiBO.GetAllEstimatorsJsonTestFile();

            return result;
        }

        public async Task<string> GetAllCycles()
        {
            AccelaApiJsonBO _mAccelaApiBO = new AccelaApiJsonBO();

            var result = await _mAccelaApiBO.GetAllPlanReviewCyclesJsonTestFile();

            return result;
        }

        public async Task<string> GetAllFacilitatorMeetings()
        {
            AccelaApiJsonBO _mAccelaApiBO = new AccelaApiJsonBO();

            var result = await _mAccelaApiBO.GetAllFacilitatorMeetingsJsonTestFile();

            return result;
        }


        ///// <summary>
        ///// GetThisAgencyInfo gets info on the Accela Agency object named  
        ///// </summary>
        ///// <param name="AccelaAgencyName"></param>
        ///// <returns></returns>
        //public async Task<AgencyBE> GetThisAgencyInfo(string AccelaAgencyName)
        //{
        //    AccelaApiBO _mAccelaApiBO = new AccelaApiBO(userId, passWord);

        //    var result = await _mAccelaApiBO.GetAllAgenciesInfo(AccelaAgencyName);

        //    return result;
        //}


        /// <summary>
        /// 
        /// </summary>
        //public async Task<string> GetAllAgencies()
        //{
        //    AccelaApiBO _mAccelaApiBO = new AccelaApiBO(userId, passWord);

        //    var result = await TaskGetAllAgencyList();

        //    return result;
        //}


        /// <summary>
        /// 
        /// </summary>
        public async Task<TradeWrapperBE> GetAllTradesList()
        {
            
            AccelaContactsAndProfessionalsBO  mAccelContactsAndProfessional = new AccelaContactsAndProfessionalsBO();

            var result = await mAccelContactsAndProfessional.TaskGetAllTradesList();

            return result;
        }

       

        /// <summary>
        /// 
        /// </summary>
        public async Task<string> GetAllFacilitators()
        {
            AccelaApiJsonBO _mAccelaApiBO = new AccelaApiJsonBO();

            //var result = await _mAccelaApiBO.GetAllFacilitators();
            var result = await _mAccelaApiBO.GetAllFacilitatorsJsonTestFile();

            return result;
        }

        /// <summary>
        ///  GetProjectList() parameters to get list for estimation
        /// </summary>
        /// <param name=""></param>
        public void GetProjectList()
        {
            AccelaApiBO _mAccelaApiBO = new AccelaApiBO();
            throw new Exception("Not Implemented Yet");
        }
        /// <summary>
        /// GetALLPLanReviewers list of plan reviewers  
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        public async Task<string> GetAllPlanReviewers()
        {
            AccelaApiJsonBO _mAccelaApiBO = new AccelaApiJsonBO();

            //var result = await _mAccelaApiBO.GetAllReviewers();
            var result = await _mAccelaApiBO.GetAllReviewersJsonTestFile();

            return result;
        }

        ///// <summary>
        ///// 
        ///// </summary>
        //public void GetAllDepartments()
        //{
        //    AccelaApiBO _mAccelaApiBO = new AccelaApiBO(userId, passWord);

        //    //  https://apis.accela.com/v4/settings/departments



        //    throw new Exception("Not Implemented Yet");
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        public void GetUserGroup(string username)
        {
            AccelaApiBO _mAccelaApiBO = new AccelaApiBO();
            throw new Exception("Not Implemented Yet");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name=""></param>
        public void GetUserGroupPermissions(string groupname)
        {
            AccelaApiBO _mAccelaApiBO = new AccelaApiBO();
            throw new Exception("Not Implemented Yet");
        }

        #endregion

    }
}
