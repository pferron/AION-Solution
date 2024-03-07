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

    #region BusinessObject - StandardNoteBO

    public class StandardNoteBO : BaseBO
    {

        #region Private Members

        private enum ActionType { Create, Delete, GetById, GetDataSet, GetList, Update, GetListByType };

        private string _errorMsg;

        private StandardNoteBE _standardNoteBE;

        private int _id;
        private int _typeid;

        #endregion

        #region Public Methods

        public int Create(StandardNoteBE standardNoteBE)
        {
            int id;
            _standardNoteBE = standardNoteBE;

            if (!this.Validate(ActionType.Create))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[7];

                sqlParameters[0] = new SqlParameter("@STANDARD_NOTE_GRP_NM", standardNoteBE.StandardNoteGroupName);
                sqlParameters[1] = new SqlParameter("@STANDARD_NOTE_TYP_REF_ID", standardNoteBE.NoteTypeRefId);
                sqlParameters[2] = new SqlParameter("@STANDARD_NOTE_TXT", standardNoteBE.StandardNoteText);
                sqlParameters[3] = new SqlParameter("@STANDARD_NOTE_TITLE_TXT", standardNoteBE.StandardNoteTitleText);
                sqlParameters[4] = new SqlParameter("@WKR_ID_TXT", standardNoteBE.UserId);
                sqlParameters[5] = new SqlParameter("@PROJECT_TYP_REF_ID", standardNoteBE.ProjectTypRefId);

                sqlParameters[6] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[6].Direction = ParameterDirection.Output;

                id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_standard_note", base.ConnectionString, ref sqlParameters);

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

                rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_standard_note", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;

        }

        public StandardNoteBE GetById(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            StandardNoteBE standardNoteBE = new StandardNoteBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_standard_note_get_by_id", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                standardNoteBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
            }

            return standardNoteBE;
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

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_standard_note_get_list", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return dataSet;
        }

        public List<StandardNoteBE> GetList(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<StandardNoteBE> standardNoteBEList = new List<StandardNoteBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_standard_note_get_list", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    standardNoteBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return standardNoteBEList;

        }
        public List<StandardNoteBE> GetListByType(int typeid)
        {
            _typeid = typeid;

            if (!this.Validate(ActionType.GetListByType))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<StandardNoteBE> standardNoteBEList = new List<StandardNoteBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@STANDARD_NOTE_TYP_REF_ID", typeid);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_standard_note_get_list_bytype", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    standardNoteBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return standardNoteBEList;

        }

        public int Update(StandardNoteBE standardNoteBE)
        {
            int rows;
            _standardNoteBE = standardNoteBE;

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[9];

                sqlParameters[0] = new SqlParameter("@STANDARD_NOTE_ID", standardNoteBE.StandardNoteId);
                sqlParameters[1] = new SqlParameter("@STANDARD_NOTE_GRP_NM", standardNoteBE.StandardNoteGroupName);
                sqlParameters[2] = new SqlParameter("@STANDARD_NOTE_TYP_REF_ID", standardNoteBE.NoteTypeRefId);
                sqlParameters[3] = new SqlParameter("@UPDATED_DTTM", standardNoteBE.UpdatedDate);
                sqlParameters[4] = new SqlParameter("@STANDARD_NOTE_TXT", standardNoteBE.StandardNoteText);
                sqlParameters[5] = new SqlParameter("@STANDARD_NOTE_TITLE_TXT", standardNoteBE.StandardNoteTitleText);
                sqlParameters[6] = new SqlParameter("@PROJECT_TYP_REF_ID", standardNoteBE.ProjectTypRefId);

                sqlParameters[7] = new SqlParameter("@WKR_ID_TXT", standardNoteBE.UserId);

                sqlParameters[8] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[8].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_standard_note", base.ConnectionString, ref sqlParameters);

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

                default:
                    break;
            }

            return true;

        }

        private StandardNoteBE ConvertDataRowToBE(DataRow dataRow)
        {
            StandardNoteBE standardNoteBE = new StandardNoteBE();

            standardNoteBE.StandardNoteId = TryToParse<int?>(dataRow["STANDARD_NOTE_ID"]);
            standardNoteBE.StandardNoteGroupName = TryToParse<string>(dataRow["STANDARD_NOTE_GRP_NM"]);
            standardNoteBE.NoteTypeRefId = TryToParse<int?>(dataRow["STANDARD_NOTE_TYP_REF_ID"]);
            standardNoteBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
            standardNoteBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            standardNoteBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
            standardNoteBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);
            standardNoteBE.StandardNoteText = TryToParse<string>(dataRow["STANDARD_NOTE_TXT"]);
            standardNoteBE.StandardNoteTitleText = TryToParse<string>(dataRow["STANDARD_NOTE_TITLE_TXT"]);
            standardNoteBE.ProjectTypRefId = TryToParse<int?>(dataRow["PROJECT_TYP_REF_ID"]);
            return standardNoteBE;

        }

        #endregion

    }

    #endregion

}