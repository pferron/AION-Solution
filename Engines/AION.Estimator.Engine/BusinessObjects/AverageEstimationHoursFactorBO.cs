#region Using

using AION.Base;
using AION.Estimator.Engine.BusinessEntities;
using Meck.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#endregion

namespace AION.Estimator.Engine.BusinessObjects
{

    #region BusinessObject - AverageEstimationHoursFactorBO

    public class AverageEstimationHoursFactorBO : BaseBO, IDataContextAverageEstimationHoursFactorBO
    {

        #region Private Members

        private enum ActionType { Create, Delete, GetById, GetDataSet, GetList, Update, GetByOccupancyConstructionTyp };

        private string _errorMsg;

        private AverageEstimationHoursFactorBE _averageEstimationHoursFactorBE;

        private int _id;

        #endregion

        #region Public Methods

        public int Create(AverageEstimationHoursFactorBE averageEstimationHoursFactorBE)
        {
            int id;
            _averageEstimationHoursFactorBE = averageEstimationHoursFactorBE;

            if (!this.Validate(ActionType.Create))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[18];

                sqlParameters[0] = new SqlParameter("@OCCUPANCY_TYP_REF_ID", averageEstimationHoursFactorBE.OccupancyTypRefId);
                sqlParameters[1] = new SqlParameter("@CONSTR_TYP_TXT", averageEstimationHoursFactorBE.ConstructionType);
                sqlParameters[2] = new SqlParameter("@BUILD_SQFT_FACTOR_NBR", averageEstimationHoursFactorBE.BuildingSqftFactor);
                sqlParameters[3] = new SqlParameter("@ELCTR_SQFT_FACTOR_NBR", averageEstimationHoursFactorBE.ElectricalSqftFactor);
                sqlParameters[4] = new SqlParameter("@MECH_SQFT_FACTOR_NBR", averageEstimationHoursFactorBE.MechanicalSqftFactor);
                sqlParameters[5] = new SqlParameter("@PLUMB_SQFT_FACTOR_NBR", averageEstimationHoursFactorBE.PlumbingSqftFactor);
                sqlParameters[6] = new SqlParameter("@BUILD_COC_FACTOR_NBR", averageEstimationHoursFactorBE.BuildingCocFactor);
                sqlParameters[7] = new SqlParameter("@ELCTR_COC_FACTOR_NBR", averageEstimationHoursFactorBE.ElectricalCocFactor);
                sqlParameters[8] = new SqlParameter("@MECH_COC_FACTOR_NBR", averageEstimationHoursFactorBE.MechanicalCocFactor);
                sqlParameters[9] = new SqlParameter("@PLUMB_COC_FACTOR_NBR", averageEstimationHoursFactorBE.PlumbingCocFactor);
                sqlParameters[10] = new SqlParameter("@BUILD_SHEETS_FACTOR_NBR", averageEstimationHoursFactorBE.BuildingSheetsFactor);
                sqlParameters[11] = new SqlParameter("@ELCTR_SHEETS_FACTOR_NBR", averageEstimationHoursFactorBE.ElectricalSheetsFactor);
                sqlParameters[12] = new SqlParameter("@MECH_SHEETS_FACTOR_NBR", averageEstimationHoursFactorBE.MechanicalSheetsFactor);
                sqlParameters[13] = new SqlParameter("@PLUMB_SHEETS_FACTOR_NBR", averageEstimationHoursFactorBE.PlumbingSheetsFactor);
                sqlParameters[14] = new SqlParameter("@ACTIVE_IND", averageEstimationHoursFactorBE.ActiveInd);
                sqlParameters[15] = new SqlParameter("@ACTIVE_DT", averageEstimationHoursFactorBE.ActiveDate);

                sqlParameters[16] = new SqlParameter("@WKR_ID_TXT", averageEstimationHoursFactorBE.UserId);

                sqlParameters[17] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[17].Direction = ParameterDirection.Output;

                id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_average_estimation_hours_factor", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return id;
        }

        public int Delete(int id)
        {
            int rows;
            _id = id;

            if (!this.Validate(ActionType.Delete))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[2];

                sqlParameters[0] = new SqlParameter("identity", id);

                sqlParameters[1] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[1].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_average_estimation_hours_factor", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;

        }

        public AverageEstimationHoursFactorBE GetById(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            AverageEstimationHoursFactorBE averageEstimationHoursFactorBE = new AverageEstimationHoursFactorBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_average_estimation_hours_factor_get_by_id", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                averageEstimationHoursFactorBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
            }

            return averageEstimationHoursFactorBE;
        }

        public DataSet GetDataSet(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetDataSet))
                throw (new Exception(_errorMsg));

            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_average_estimation_hours_factor_get_list", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return dataSet;
        }

        public List<AverageEstimationHoursFactorBE> GetList(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<AverageEstimationHoursFactorBE> averageEstimationHoursFactorBEList = new List<AverageEstimationHoursFactorBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_average_estimation_hours_factor_get_list", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    averageEstimationHoursFactorBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return averageEstimationHoursFactorBEList;

        }

        public int Update(AverageEstimationHoursFactorBE averageEstimationHoursFactorBE)
        {
            int rows;
            _averageEstimationHoursFactorBE = averageEstimationHoursFactorBE;

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[20];

                sqlParameters[0] = new SqlParameter("@AVERAGE_ESTIMATION_HOURS_FACTOR_ID", averageEstimationHoursFactorBE.AverageEstimationHoursFactorId);
                sqlParameters[1] = new SqlParameter("@OCCUPANCY_TYP_REF_ID", averageEstimationHoursFactorBE.OccupancyTypRefId);
                sqlParameters[2] = new SqlParameter("@CONSTR_TYP_TXT", averageEstimationHoursFactorBE.ConstructionType);
                sqlParameters[3] = new SqlParameter("@BUILD_SQFT_FACTOR_NBR", averageEstimationHoursFactorBE.BuildingSqftFactor);
                sqlParameters[4] = new SqlParameter("@ELCTR_SQFT_FACTOR_NBR", averageEstimationHoursFactorBE.ElectricalSqftFactor);
                sqlParameters[5] = new SqlParameter("@MECH_SQFT_FACTOR_NBR", averageEstimationHoursFactorBE.MechanicalSqftFactor);
                sqlParameters[6] = new SqlParameter("@PLUMB_SQFT_FACTOR_NBR", averageEstimationHoursFactorBE.PlumbingSqftFactor);
                sqlParameters[7] = new SqlParameter("@BUILD_COC_FACTOR_NBR", averageEstimationHoursFactorBE.BuildingCocFactor);
                sqlParameters[8] = new SqlParameter("@ELCTR_COC_FACTOR_NBR", averageEstimationHoursFactorBE.ElectricalCocFactor);
                sqlParameters[9] = new SqlParameter("@MECH_COC_FACTOR_NBR", averageEstimationHoursFactorBE.MechanicalCocFactor);
                sqlParameters[10] = new SqlParameter("@PLUMB_COC_FACTOR_NBR", averageEstimationHoursFactorBE.PlumbingCocFactor);
                sqlParameters[11] = new SqlParameter("@BUILD_SHEETS_FACTOR_NBR", averageEstimationHoursFactorBE.BuildingSheetsFactor);
                sqlParameters[12] = new SqlParameter("@ELCTR_SHEETS_FACTOR_NBR", averageEstimationHoursFactorBE.ElectricalSheetsFactor);
                sqlParameters[13] = new SqlParameter("@MECH_SHEETS_FACTOR_NBR", averageEstimationHoursFactorBE.MechanicalSheetsFactor);
                sqlParameters[14] = new SqlParameter("@PLUMB_SHEETS_FACTOR_NBR", averageEstimationHoursFactorBE.PlumbingSheetsFactor);
                sqlParameters[15] = new SqlParameter("@UPDATED_DTTM", averageEstimationHoursFactorBE.UpdatedDate);
                sqlParameters[16] = new SqlParameter("@ACTIVE_IND", averageEstimationHoursFactorBE.ActiveInd);
                sqlParameters[17] = new SqlParameter("@ACTIVE_DT", averageEstimationHoursFactorBE.ActiveDate);

                sqlParameters[18] = new SqlParameter("@WKR_ID_TXT", averageEstimationHoursFactorBE.UserId);

                sqlParameters[19] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[19].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_average_estimation_hours_factor", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;
        }

        public AverageEstimationHoursFactorBE GetByOccupancyTypConstructionTyp(string occupancytyp, string constructiontyp)
        {

            if (!this.Validate(ActionType.GetByOccupancyConstructionTyp))
                throw (new Exception(_errorMsg));

            AverageEstimationHoursFactorBE averageEstimationHoursFactorBE = new AverageEstimationHoursFactorBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[2];

                sqlParameters[0] = new SqlParameter("@PROJECT_OCCUPANCY_TYP_MAP_NM", occupancytyp);
                sqlParameters[1] = new SqlParameter("@CONSTR_TYP_TXT", constructiontyp);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_average_estimation_hours_factor_getbyoccpncycnstrctn", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                averageEstimationHoursFactorBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
            }

            return averageEstimationHoursFactorBE;
        }

        /// <summary>
        /// Set row inactive by constructiontype and occupancy type
        /// </summary>
        /// <param name="averageEstimationHoursFactorBE"></param>
        /// <returns></returns>
        public int SetRowActive(int occupancytyp, string constructiontyp, bool active, string wrkId)
        {
            int id = 0;
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[18];

                sqlParameters[0] = new SqlParameter("@OCCUPANCY_TYP_REF_ID", occupancytyp);
                sqlParameters[1] = new SqlParameter("@CONSTR_TYP_TXT", constructiontyp);
                sqlParameters[14] = new SqlParameter("@ACTIVE_IND", active);
                sqlParameters[15] = new SqlParameter("@ACTIVE_DT", DateTime.Now);

                sqlParameters[16] = new SqlParameter("@WKR_ID_TXT", wrkId);

                sqlParameters[17] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[17].Direction = ParameterDirection.Output;

                id = SqlWrapper.RunSPReturnInteger("usp_update_aion_average_estimation_hours_factor_byoccpncycnstrctn", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return id;
        }

        #endregion

        #region Private Methods

        private bool Validate(ActionType actionType)
        {
            // TODO: Add validation rules as necessary.

            _errorMsg = String.Empty;

            switch (actionType)
            {
                case ActionType.Create:
                    return (_errorMsg == String.Empty);

                case ActionType.Delete:
                    return (_errorMsg == String.Empty);

                case ActionType.GetById:
                    return (_errorMsg == String.Empty);

                case ActionType.GetDataSet:
                    return (_errorMsg == String.Empty);

                case ActionType.GetList:
                    return (_errorMsg == String.Empty);

                case ActionType.Update:
                    return (_errorMsg == String.Empty);
                case ActionType.GetByOccupancyConstructionTyp:
                    return (_errorMsg == String.Empty);
                default:
                    break;
            }

            return true;

        }

        private AverageEstimationHoursFactorBE ConvertDataRowToBE(DataRow dataRow)
        {
            AverageEstimationHoursFactorBE averageEstimationHoursFactorBE = new AverageEstimationHoursFactorBE();

            averageEstimationHoursFactorBE.AverageEstimationHoursFactorId = TryToParse<int?>(dataRow["AVERAGE_ESTIMATION_HOURS_FACTOR_ID"]);
            averageEstimationHoursFactorBE.OccupancyTypRefId = TryToParse<int?>(dataRow["OCCUPANCY_TYP_REF_ID"]);
            averageEstimationHoursFactorBE.ConstructionType = TryToParse<string>(dataRow["CONSTR_TYP_TXT"]);
            averageEstimationHoursFactorBE.BuildingSqftFactor = TryToParse<decimal?>(dataRow["BUILD_SQFT_FACTOR_NBR"]);
            averageEstimationHoursFactorBE.ElectricalSqftFactor = TryToParse<decimal?>(dataRow["ELCTR_SQFT_FACTOR_NBR"]);
            averageEstimationHoursFactorBE.MechanicalSqftFactor = TryToParse<decimal?>(dataRow["MECH_SQFT_FACTOR_NBR"]);
            averageEstimationHoursFactorBE.PlumbingSqftFactor = TryToParse<decimal?>(dataRow["PLUMB_SQFT_FACTOR_NBR"]);
            averageEstimationHoursFactorBE.BuildingCocFactor = TryToParse<decimal?>(dataRow["BUILD_COC_FACTOR_NBR"]);
            averageEstimationHoursFactorBE.ElectricalCocFactor = TryToParse<decimal?>(dataRow["ELCTR_COC_FACTOR_NBR"]);
            averageEstimationHoursFactorBE.MechanicalCocFactor = TryToParse<decimal?>(dataRow["MECH_COC_FACTOR_NBR"]);
            averageEstimationHoursFactorBE.PlumbingCocFactor = TryToParse<decimal?>(dataRow["PLUMB_COC_FACTOR_NBR"]);
            averageEstimationHoursFactorBE.BuildingSheetsFactor = TryToParse<decimal?>(dataRow["BUILD_SHEETS_FACTOR_NBR"]);
            averageEstimationHoursFactorBE.ElectricalSheetsFactor = TryToParse<decimal?>(dataRow["ELCTR_SHEETS_FACTOR_NBR"]);
            averageEstimationHoursFactorBE.MechanicalSheetsFactor = TryToParse<decimal?>(dataRow["MECH_SHEETS_FACTOR_NBR"]);
            averageEstimationHoursFactorBE.PlumbingSheetsFactor = TryToParse<decimal?>(dataRow["PLUMB_SHEETS_FACTOR_NBR"]);
            averageEstimationHoursFactorBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
            averageEstimationHoursFactorBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            averageEstimationHoursFactorBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
            averageEstimationHoursFactorBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);
            averageEstimationHoursFactorBE.ActiveInd = TryToParse<bool?>(dataRow["ACTIVE_IND"]);
            averageEstimationHoursFactorBE.ActiveDate = TryToParse<DateTime?>(dataRow["ACTIVE_DT"]);

            return averageEstimationHoursFactorBE;

        }

        #endregion

    }

    #endregion

}