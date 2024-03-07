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

    #region BusinessObject - NotesBO

    public class NotesBO : BaseBO
    {

        #region Private Members

        private enum ActionType { Create, Delete, GetById, GetDataSet, GetList, Update };

        private string _errorMsg;

        private NotesBE _notesBE;

        private int _id;

        #endregion

        #region Public Methods

        public int Create(NotesBE notesBE)
        {
            int id;
            _notesBE = notesBE;

            if (!this.Validate(ActionType.Create))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[11];

                sqlParameters[0] = new SqlParameter("@NOTES_COMMENT", notesBE.NotesComment);
                sqlParameters[1] = new SqlParameter("@PROJECT_ID", notesBE.ProjectId);
                sqlParameters[2] = new SqlParameter("@NOTES_TYP_REF_ID", notesBE.NotesTypeRefId);

                sqlParameters[3] = new SqlParameter("@WKR_ID_TXT", notesBE.UserId);
                sqlParameters[4] = new SqlParameter("@WORKER_CREATED_BY_ID_NUM", notesBE.CreatedByWkrId);
                sqlParameters[5] = new SqlParameter("@WORKER_CREATED_BY_TS", notesBE.CreatedDate);
                sqlParameters[6] = new SqlParameter("@WORKER_UPDATED_BY_ID_NUM", notesBE.UpdatedByWkrId);
                sqlParameters[7] = new SqlParameter("@WORKER_UPDATED_BY_TS", notesBE.UpdatedDate);
                sqlParameters[8] = new SqlParameter("@PARENT_NOTES_ID", notesBE.ParentNoteID);
                sqlParameters[9] = new SqlParameter("@BUSINESS_REF_ID", notesBE.BusinessRefID);

                sqlParameters[10] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[10].Direction = ParameterDirection.Output;
                id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_notes", base.ConnectionString, ref sqlParameters);

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

                rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_notes", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;

        }

        public NotesBE GetById(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            NotesBE notesBE = new NotesBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_notes_get_by_id", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                notesBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
            }

            return notesBE;
        }

        public List<NotesBE> GetAllNotesByProject(int? projectId, int? noteTypeId, string projectNumber = "")
        {

            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<NotesBE> notesBEList = new List<NotesBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[3];

                sqlParameters[0] = new SqlParameter("@projectID", projectId);
                sqlParameters[1] = new SqlParameter("@noteTypeID", noteTypeId);
                sqlParameters[2] = new SqlParameter("@projectNumber", projectNumber);
                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_notes_get_list_by_Project", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    notesBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return notesBEList;

        }

        public int Update(NotesBE notesBE)
        {
            int rows;
            _notesBE = notesBE;

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[9];

                sqlParameters[0] = new SqlParameter("@NOTES_ID", notesBE.NotesId);
                sqlParameters[1] = new SqlParameter("@NOTES_COMMENT", notesBE.NotesComment);
                sqlParameters[2] = new SqlParameter("@UPDATED_DTTM", notesBE.UpdatedDate);
                sqlParameters[3] = new SqlParameter("@PROJECT_ID", notesBE.ProjectId);
                sqlParameters[4] = new SqlParameter("@NOTES_TYP_REF_ID", notesBE.NotesTypeRefId);

                sqlParameters[5] = new SqlParameter("@WKR_ID_TXT", notesBE.UserId);
                sqlParameters[6] = new SqlParameter("@PARENT_NOTES_ID", notesBE.ParentNoteID);
                sqlParameters[7] = new SqlParameter("@BUSINESS_REF_ID", notesBE.BusinessRefID);

                sqlParameters[8] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[8].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_notes", base.ConnectionString, ref sqlParameters);

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

        private NotesBE ConvertDataRowToBE(DataRow dataRow)
        {
            NotesBE notesBE = new NotesBE();

            notesBE.NotesId = TryToParse<int?>(dataRow["NOTES_ID"]);
            notesBE.NotesComment = TryToParse<string>(dataRow["NOTES_COMMENT"]);
            notesBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
            notesBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            notesBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
            notesBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);
            notesBE.ProjectId = TryToParse<int?>(dataRow["PROJECT_ID"]);
            notesBE.NotesTypeRefId = TryToParse<int?>(dataRow["NOTES_TYP_REF_ID"]);
            notesBE.ParentNoteID = TryToParse<int>(dataRow["PARENT_NOTES_ID"]);
            notesBE.BusinessRefID = TryToParse<int>(dataRow["BUSINESS_REF_ID"]);
            return notesBE;

        }

        #endregion

    }

    #endregion

}