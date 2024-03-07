using AION.BL;
using AION.BL.Models;
using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using Meck.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AION.Manager.Adapters
{
    public class ProjectTypeAdapter : BaseManagerAdapter
    {
        public bool CheckConfigurationForAutoAssignedFacilitator(PropertyTypeEnums propertyTypeEnum)
        {
            try
            {
                //LES-4519
                // get the autoassignfacilitator bool for the property type
                ProjectTypeRefBO projectTypeRefBO = new ProjectTypeRefBO();
                int projectTypeRefId = (int)propertyTypeEnum;
                ProjectTypeRefBE projectTypeRefBE = projectTypeRefBO.GetById(projectTypeRefId);
                return projectTypeRefBE.AutoAssignFacilitator.HasValue ? projectTypeRefBE.AutoAssignFacilitator.Value : false;

            }
            catch (System.Exception ex)
            {

                string errorMessage = "Error in ProjectTypeAdapter CheckConfigurationForAutoAssignedFacilitator - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }

        }

        public List<ProjectType> GetProjectTypeList()
        {
            try
            {
                ProjectTypeRefBO projectTypeRefBO = new ProjectTypeRefBO();
                return projectTypeRefBO.GetList().Select(x => ConvertBE(x)).ToList();

            }
            catch (System.Exception ex)
            {

                string errorMessage = "Error in ProjectTypeAdapter GetProjectTypeList - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }

        }

        public bool UpdateAutoAssignFacilitator(string projectTypeRefIdCsvList, bool autoAssignFacilitator, string wkrId)
        {
            try
            {
                ProjectTypeRefBO projectTypeRefBO = new ProjectTypeRefBO();
                int rows = projectTypeRefBO.UpdateAutoAssignFacilitator(projectTypeRefIdCsvList, autoAssignFacilitator, wkrId);
                return true;
            }
            catch (System.Exception ex)
            {
                string errorMessage = "Error in ProjectTypeAdapter UpdateAutoAssignFacilitator - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
        }

        public ProjectType ConvertBE(ProjectTypeRefBE projectTypeRefBE)
        {
            return new ProjectType
            {
                AutoAssignFacilitator = projectTypeRefBE.AutoAssignFacilitator.HasValue ? projectTypeRefBE.AutoAssignFacilitator.Value : false,
                ExternalSystemRefId = projectTypeRefBE.ExternalSystemRefId,
                ProjectTypRefDisplayNm = projectTypeRefBE.ProjectTypRefDisplayNm,
                ProjectTypRefId = projectTypeRefBE.ProjectTypRefId,
                ProjectTypRefNm = projectTypeRefBE.ProjectTypRefNm,
                SrcSystemValueTxt = projectTypeRefBE.SrcSystemValueTxt,
                ProjectTypeEnum = (PropertyTypeEnums)projectTypeRefBE.ProjectTypRefId
            };
        }
    }
}