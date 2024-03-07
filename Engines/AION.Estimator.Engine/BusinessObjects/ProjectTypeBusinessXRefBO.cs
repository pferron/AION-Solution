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

    //#region BusinessObject - ProjectTypeBusinessXRefBO

    //public class ProjectTypeBusinessXRefBO : BaseBO
    //{

    //    #region Private Members

    //    private enum ActionType { Create, Delete, GetById, GetDataSet, GetList, Update };

    //    private string _errorMsg;

    //    private ProjectTypeBusinessXRefBE _projectTypeBusinessXRefBE;

    //    private int _id;

    //    #endregion

    //    #region Public Methods

    //    public int Create(int BusinessRefId,int ProjectTypeRefId)
    //    {
    //        int id;

    //        if (!this.Validate(ActionType.Create))
    //            throw (new Exception(_errorMsg));

    //        try
    //        {
    //            SqlParameter[] sqlParameters = new SqlParameter[3];

    //            sqlParameters[0] = new SqlParameter("@BUSINESS_REF_ID", BusinessRefId);
    //            sqlParameters[1] = new SqlParameter("@PROJECT_TYP_REF_ID", ProjectTypeRefId);

    //            sqlParameters[2] = new SqlParameter("@ReturnValue", 0);
    //            sqlParameters[2].Direction = ParameterDirection.Output;

    //            id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_project_type_business_x_ref", base.ConnectionString, ref sqlParameters);

    //        }
    //        catch (Exception ex)
    //        {
    //            throw (ex);
    //        }

    //        return id;
    //    }

    //    public int Delete(int BusinessRefId, int ProjectTypeRefId)
    //    {

    //        int id;

    //        if (!this.Validate(ActionType.Create))
    //            throw (new Exception(_errorMsg));

    //        try
    //        {
    //            SqlParameter[] sqlParameters = new SqlParameter[3];

    //            sqlParameters[0] = new SqlParameter("@BUSINESS_REF_ID", BusinessRefId);
    //            sqlParameters[1] = new SqlParameter("@PROJECT_TYP_REF_ID", ProjectTypeRefId);

    //            sqlParameters[2] = new SqlParameter("@ReturnValue", 0);
    //            sqlParameters[2].Direction = ParameterDirection.Output;

    //            id = SqlWrapper.RunSPReturnInteger("usp_delete_aion_project_type_business_x_ref", base.ConnectionString, ref sqlParameters);

    //        }
    //        catch (Exception ex)
    //        {
    //            throw (ex);
    //        }

    //        return id;
    //    }

    //    public ProjectTypeBusinessXRefBE GetById(int id)
    //    {
    //        _id = id;

    //        if (!this.Validate(ActionType.GetById))
    //            throw (new Exception(_errorMsg));

    //        ProjectTypeBusinessXRefBE projectTypeBusinessXRefBE = new ProjectTypeBusinessXRefBE();
    //        DataSet dataSet;

    //        try
    //        {
    //            SqlParameter[] sqlParameters = new SqlParameter[1];

    //            sqlParameters[0] = new SqlParameter("@identity", id);

    //            dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_project_type_business_x_ref_get_by_id", base.ConnectionString, ref sqlParameters);

    //        }
    //        catch (Exception ex)
    //        {
    //            throw (ex);
    //        }

    //        if (dataSet.Tables[0].Rows.Count > 0)
    //        {
    //            projectTypeBusinessXRefBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
    //        }

    //        return projectTypeBusinessXRefBE;
    //    }

    //    public DataSet GetDataSet(int id)
    //    {
    //        _id = id;

    //        if (!this.Validate(ActionType.GetDataSet))
    //            throw (new Exception(_errorMsg));

    //        DataSet dataSet;

    //        try
    //        {
    //            SqlParameter[] sqlParameters = new SqlParameter[1];

    //            sqlParameters[0] = new SqlParameter("@identity", id);

    //            dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_project_type_business_x_ref_get_list", base.ConnectionString, ref sqlParameters);

    //        }
    //        catch (Exception ex)
    //        {
    //            throw (ex);
    //        }

    //        return dataSet;
    //    }

    //    public List<ProjectTypeBusinessXRefBE> GetList()
    //    {

    //        if (!this.Validate(ActionType.GetList))
    //            throw (new Exception(_errorMsg));

    //        DataSet dataSet;
    //        List<ProjectTypeBusinessXRefBE> projectTypeBusinessXRefBEList = new List<ProjectTypeBusinessXRefBE>();

    //        try
    //        {

    //            dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_project_type_business_x_ref_get_list", base.ConnectionString);

    //            foreach (DataRow dataRow in dataSet.Tables[0].Rows)
    //            {
    //                projectTypeBusinessXRefBEList.Add(this.ConvertDataRowToBE(dataRow));
    //            }

    //        }
    //        catch (Exception ex)
    //        {
    //            throw (ex);
    //        }

    //        return projectTypeBusinessXRefBEList;

    //    }

    //    public int Update(ProjectTypeBusinessXRefBE projectTypeBusinessXRefBE)
    //    {
    //        int rows;
    //        _projectTypeBusinessXRefBE = projectTypeBusinessXRefBE;

    //        if (!this.Validate(ActionType.Update))
    //            throw (new Exception(_errorMsg));

    //        try
    //        {
    //            SqlParameter[] sqlParameters = new SqlParameter[5];

    //            sqlParameters[0] = new SqlParameter("@BUSINESS_REF_ID", projectTypeBusinessXRefBE.BusinessRefId);
    //            sqlParameters[1] = new SqlParameter("@PROJECT_TYP_REF_ID", projectTypeBusinessXRefBE.ProjectTypeRefId);
    //            sqlParameters[2] = new SqlParameter("@PROJECT_TYP_BUSINESS_CROSS_REF_ID", projectTypeBusinessXRefBE.ProjectTypeBusinessCrossRefId);

    //            sqlParameters[3] = new SqlParameter("@WKR_ID_TXT", projectTypeBusinessXRefBE.UserId);

    //            sqlParameters[4] = new SqlParameter("@ReturnValue", 0);
    //            sqlParameters[4].Direction = ParameterDirection.Output;

    //            rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_project_type_business_x_ref", base.ConnectionString, ref sqlParameters);

    //        }
    //        catch (Exception ex)
    //        {
    //            throw (ex);
    //        }

    //        return rows;
    //    }

    //    #endregion

    //    #region Private Methods

    //    private bool Validate(ActionType actionType)
    //    {
    //        // TODO: Add validation rules as necessary.

    //        _errorMsg = String.Empty;

    //        switch (actionType)
    //        {
    //            case ActionType.Create:
    //                return (_errorMsg == String.Empty);

    //            case ActionType.Delete:
    //                return (_errorMsg == String.Empty);

    //            case ActionType.GetById:
    //                return (_errorMsg == String.Empty);

    //            case ActionType.GetDataSet:
    //                return (_errorMsg == String.Empty);

    //            case ActionType.GetList:
    //                return (_errorMsg == String.Empty);

    //            case ActionType.Update:
    //                return (_errorMsg == String.Empty);

    //            default:
    //                break;
    //        }

    //        return true;

    //    }

    //    private ProjectTypeBusinessXRefBE ConvertDataRowToBE(DataRow dataRow)
    //    {
    //        ProjectTypeBusinessXRefBE projectTypeBusinessXRefBE = new ProjectTypeBusinessXRefBE();

    //        projectTypeBusinessXRefBE.BusinessRefId = TryToParse<int?>(dataRow["BUSINESS_REF_ID"]);
    //        projectTypeBusinessXRefBE.ProjectTypeRefId = TryToParse<int?>(dataRow["PROJECT_TYP_REF_ID"]);
    //        projectTypeBusinessXRefBE.ProjectTypeBusinessCrossRefId = TryToParse<int?>(dataRow["PROJECT_TYP_BUSINESS_CROSS_REF_ID"]);

    //        return projectTypeBusinessXRefBE;

    //    }

    //    #endregion

    //}

    //#endregion

}