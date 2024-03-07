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

    #region BusinessObject - OccupancyTypeRefBO

    public class OccupancyTypeRefBO : BaseBO
    {

        #region Private Members

        private enum ActionType { Create, Delete, GetById, GetDataSet, GetList, Update, GetByOccupancyTyp };

        private string _errorMsg;

        private OccupancyTypeRefBE _occupancyTypeRefBE;

        private int _id;

        #endregion

        #region Public Methods

        public int Create(OccupancyTypeRefBE occupancyTypeRefBE)
        {
            int id;
            _occupancyTypeRefBE = occupancyTypeRefBE;

            if (!this.Validate(ActionType.Create))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[3];

                sqlParameters[0] = new SqlParameter("@OCCUPANCY_TYP_NM", occupancyTypeRefBE.OccupancyTypName);

                sqlParameters[1] = new SqlParameter("@WKR_ID_TXT", occupancyTypeRefBE.UserId);

                sqlParameters[2] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[2].Direction = ParameterDirection.Output;

                id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_occupancy_type_ref", base.ConnectionString, ref sqlParameters);

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

                rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_occupancy_type_ref", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;

        }

        public OccupancyTypeRefBE GetById(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            OccupancyTypeRefBE occupancyTypeRefBE = new OccupancyTypeRefBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_occupancy_type_ref_get_by_id", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                occupancyTypeRefBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
            }

            return occupancyTypeRefBE;
        }
        public OccupancyTypeRefBE GetByProjectOccupancyTyp(string occupancytyp)
        {
            //_id = id;

            if (!this.Validate(ActionType.GetByOccupancyTyp))
                throw (new Exception(_errorMsg));

            OccupancyTypeRefBE occupancyTypeRefBE = new OccupancyTypeRefBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@PROJECT_OCCUPANCY_TYP_MAP_NM", occupancytyp);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_occupancy_type_ref_getbyprojectoccpncy", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                occupancyTypeRefBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
            }

            return occupancyTypeRefBE;
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

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_occupancy_type_ref_get_list", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return dataSet;
        }

        public List<OccupancyTypeRefBE> GetList()
        {
            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<OccupancyTypeRefBE> occupancyTypeRefBEList = new List<OccupancyTypeRefBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];             

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_occupancy_type_ref_get_list", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    occupancyTypeRefBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return occupancyTypeRefBEList;

        }

        public int Update(OccupancyTypeRefBE occupancyTypeRefBE)
        {
            int rows;
            _occupancyTypeRefBE = occupancyTypeRefBE;

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[5];

                sqlParameters[0] = new SqlParameter("@OCCUPANCY_TYP_REF_ID", occupancyTypeRefBE.OccupancyTypRefId);
                sqlParameters[1] = new SqlParameter("@OCCUPANCY_TYP_NM", occupancyTypeRefBE.OccupancyTypName);
                sqlParameters[2] = new SqlParameter("@UPDATED_DTTM", occupancyTypeRefBE.UpdatedDate);

                sqlParameters[3] = new SqlParameter("@WKR_ID_TXT", occupancyTypeRefBE.UserId);

                sqlParameters[4] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[4].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_occupancy_type_ref", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;
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
                case ActionType.GetByOccupancyTyp:
                    return (_errorMsg == String.Empty);
                default:
                    break;
            }

            return true;

        }

        private OccupancyTypeRefBE ConvertDataRowToBE(DataRow dataRow)
        {
            OccupancyTypeRefBE occupancyTypeRefBE = new OccupancyTypeRefBE();

            occupancyTypeRefBE.OccupancyTypRefId = TryToParse<int?>(dataRow["OCCUPANCY_TYP_REF_ID"]);
            occupancyTypeRefBE.OccupancyTypName = TryToParse<string>(dataRow["OCCUPANCY_TYP_NM"]);
            occupancyTypeRefBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
            occupancyTypeRefBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            occupancyTypeRefBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
            occupancyTypeRefBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);

            return occupancyTypeRefBE;

        }

        #endregion

    }

    #endregion

}