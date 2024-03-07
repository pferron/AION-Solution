using AION.BL.Models;
using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AION.BL.BusinessObjects
{
    //public class ProjectDeptEstimationReqBO
    //{
    //    private List<ProjectDeptEstimationReq> _reqs;
    //    private ProjectTypeBusinessXRefBO _projectTypeBusinessXRefBO;
    //    private List<ProjectTypeBusinessXRefBE> _reqlist;
    //    public ProjectDeptEstimationReqBO()
    //    {
    //        _projectTypeBusinessXRefBO = new ProjectTypeBusinessXRefBO();
    //        _reqlist = _projectTypeBusinessXRefBO.GetList();
    //        _reqs = _reqlist.Select(x => new ProjectDeptEstimationReq
    //        {
    //            DepartmentId = (int)x.BusinessRefId,
    //            ProjectTypRefId = (int)x.ProjectTypeRefId,
    //            Department = (DepartmentNameEnums)Enum.Parse(typeof(DepartmentNameEnums), x.BusinessRefId.ToString()),
    //            PropertyType = (PropertyTypeEnums)Enum.Parse(typeof(PropertyTypeEnums), x.ProjectTypeRefId.ToString())

    //        }).ToList();

    //    }
    //    public List<ProjectDeptEstimationReq> GetDeptsRequired(PropertyTypeEnums propertytype)
    //    {
    //        List<ProjectDeptEstimationReq> reqs = new List<ProjectDeptEstimationReq>();
    //        reqs = _reqs.Where(x => x.PropertyType == propertytype).ToList();
    //        if (reqs == null) reqs = new List<ProjectDeptEstimationReq>();
    //        return reqs;
    //    }
    //}
}
