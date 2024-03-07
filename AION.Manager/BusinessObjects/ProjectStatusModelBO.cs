using AION.Base;
using AION.BL.Adapters;
using AION.BL.Models.Base;
using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using AION.Manager.Models;
using System.Collections.Generic;
using System.Linq;

namespace AION.BL.BusinessObjects
{
    public class ProjectStatusModelBO : ModelBaseModelBO, IProjectStatusBO
    {
        public bool RefreshList()
        {
            LocalStorage<List<SystemRole>>.Delete("ProjectStatus_List");
            return true;
        }

        public ProjectStatus GetInstance(int ID)
        {
            var t = BaseList.Where(x => x.ID == ID).FirstOrDefault();
            return t;
        }

        public ProjectStatus GetInstance(string ProjectStatusCode, int externalSystemRef)
        {
            var t = BaseList.Where(x => x.ProjectStatusCode == ProjectStatusCode &&
                    x.ExternalSystem.ID == externalSystemRef).FirstOrDefault();
            return t;
        }

        public ProjectStatus GetInstance(string projectStatusExternalRef, ExternalSystemEnum externalSystemEnum)
        {
            var t = BaseList.Where(x => x.ProjectStatusCode == projectStatusExternalRef &&
                  x.ExternalSystem.ExternalSystemEnum == externalSystemEnum).FirstOrDefault();
            return t;
        }

        public ProjectStatus GetInstance(ProjectStatusEnum ProjectStatusEnum)
        {
            var t = BaseList.Where(x => x.ProjectStatusEnum == ProjectStatusEnum).FirstOrDefault();
            return t;
        }

        public List<ProjectStatus> CreateInstance()
        {
            List<ProjectStatus> ret = new List<ProjectStatus>();
            ProjectStatusRefBO bo = new ProjectStatusRefBO();
            List<ProjectStatusRefBE> be = bo.GetAllList();
            foreach (var item in be)
            {
                ret.Add(ConvertData(item));
            }
            return ret;
        }

        public ProjectStatus ConvertData(ProjectStatusRefBE be)
        {
            ProjectStatus ret = new ProjectStatus();
            InjectBaseObjects(ret, be.ProjectStatusRefId.Value, be.CreatedDate.Value, be.UpdatedDate.Value, be.CreatedByWkrId, be.UpdatedByWkrId);
            ret.ExternalSystem = new ExternalSystemModelBO().GetInstance(be.ExternalSystemRefId.Value);
            ret.ProjectStatusExternalRef = be.SrcSystemValueTxt;
            ret.ProjectStatusCode = be.ProjectStatusRefNm;
            ret.ProjectStatusDetails = be.ProjectStatusRefDesc;
            ret.ProjectStatusExternalRef = be.SrcSystemValueTxt;
            ret.ProjectStatusEnum = (ProjectStatusEnum)be.EnumMappingValNbr;
            ret.ID = be.ProjectStatusRefId.Value;
            //TBD assign each department based onvalues coming in.
            return ret;
        }

        public List<ProjectStatus> BaseList
        {
            get
            {
                List<ProjectStatus> ret = LocalStorage<List<ProjectStatus>>.GetValue("ProjectStatus_List");
                if (ret == null)
                {
                    ret = CreateInstance();
                    LocalStorage<List<ProjectStatus>>.Add("ProjectStatus_List", ret);
                }
                return ret;
            }
        }
    }

    public interface IProjectStatusBO : IModelCollectionCreater<ProjectStatus, ProjectStatusRefBE>
    {
        ProjectStatus GetInstance(int ID);

        ProjectStatus GetInstance(string ProjectStatusCode, int externalSystemRef);

        ProjectStatus GetInstance(string projectStatusExternalRef, ExternalSystemEnum externalSystemEnum);

        ProjectStatus GetInstance(ProjectStatusEnum ProjectStatusEnum);

    }
}
