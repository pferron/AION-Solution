using Meck.Data;
using AION.Base;
using AION.Estimator.Engine.BusinessEntities;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace AION.Estimator.Engine.BusinessObjects
{
    public class FacilitatorBO:BaseBO
    {
        #region Private Members

        private enum ActionType { GetFacilitatorworkloadSummary };

        private string _errorMsg;


        #endregion

        #region Public Methods

        public List<FacilitatorBE> GetFacilitatorworkloadSummary(DateTime startdate, DateTime enddate)
        {
            

            if (!this.Validate(ActionType.GetFacilitatorworkloadSummary))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<FacilitatorBE> FaciltatorWorloadList = new List<FacilitatorBE>();
           

            try
            {

                SqlParameter[] sqlParameters = new SqlParameter[2];

                sqlParameters[0] = new SqlParameter("@StartDate ", startdate);
                sqlParameters[1] = new SqlParameter("@EndDate", enddate);
          

              


                dataSet = SqlWrapper.RunSPReturnDS("usp_get_facilitator_workload_summary_byDate", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    FacilitatorBE facilitatorBE = new FacilitatorBE();
                    facilitatorBE.UserID = TryToParse<int?>(dataRow["USER_ID"]);
                    facilitatorBE.FirstNm = TryToParse<string>(dataRow["FIRST_NM"]);
                    facilitatorBE.LastNm = TryToParse<string>(dataRow["LAST_NM"]);
                    facilitatorBE.AssignedProjectsHours = TryToParse<int>(dataRow["TotalNoOfProjects"]);
                    FaciltatorWorloadList.Add(facilitatorBE);

                }
              

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return FaciltatorWorloadList;
        }

        #endregion

        #region Private Methods

        private bool Validate(ActionType actionType)
        {
            // TODO: Add validation rules as necessary.

            _errorMsg = String.Empty;
            switch (actionType)
            {
                case ActionType.GetFacilitatorworkloadSummary:
                    return (_errorMsg == String.Empty);

                

                default:
                    break;
            }

            return true;

        }

      

        #endregion

    }
}
