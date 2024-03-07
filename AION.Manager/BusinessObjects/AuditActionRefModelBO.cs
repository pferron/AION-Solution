using AION.Base;
using AION.BL;
using AION.BL.Models;
using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using Meck.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AION.Manager.BusinessObjects
{
    public class AuditActionRefModelBO : BaseAdapter
    {
        private AuditActionRefBE _auditActionRefBE;
        private List<AuditActionRef> _auditActionRefs;
        public List<AuditActionRef> AuditActionRefs
        {
            get
            {
                if (_auditActionRefs == null)
                {
                    _auditActionRefs = new AuditActionRefBO().GetList().Select(x => new AuditActionRef
                    {
                        AuditActionName = x.AuditActionName,
                        AuditActionDesc = x.AuditActionDesc,
                        AuditActionRefId = x.AuditActionRefId
                    }).ToList();
                }
                return _auditActionRefs;
            }
        }
        public int GetAuditActionIdFromName(string auditActionName)
        {
            //TODO: if AuditAction doesn't exist,
            //if it does exist get the id
            int auditactionrefid = 0;
            try
            {
                _auditActionRefBE = new AuditActionRefBO().GetByName(auditActionName);
                if (_auditActionRefBE != null)
                    auditactionrefid = (int)_auditActionRefBE.AuditActionRefId;
            }
            catch (System.Exception ex)
            {
                string errorMessage = "Error - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw (ex);
            }
            return auditactionrefid;
        }
        internal static AuditActionEnum AuditActionByDeptByAction(DepartmentDivisionEnum departmentDivision, ActionTag actionTag, DepartmentNameEnums departmentName)
        {
            //send in the departmentdivisionenum and the action
            //get back the audit action
            switch (departmentDivision)
            {
                case DepartmentDivisionEnum.Building:
                    switch (actionTag)
                    {
                        case ActionTag.Completed:
                            return AuditActionEnum.Building_Estimation_Completed;
                        case ActionTag.Pending:
                            return AuditActionEnum.Building_Estimation_Pending;
                        case ActionTag.Not_Required:
                            return AuditActionEnum.Building_Estimation_Not_Required;
                        default:
                            break;
                    }
                    break;
                case DepartmentDivisionEnum.Electrical:
                    switch (actionTag)
                    {
                        case ActionTag.Completed:
                            return AuditActionEnum.Electrical_Estimation_Completed;
                        case ActionTag.Pending:
                            return AuditActionEnum.Electrical_Estimation_Pending;
                        case ActionTag.Not_Required:
                            return AuditActionEnum.Electrical_Estimation_Not_Required;
                        default:
                            break;
                    }
                    break;
                case DepartmentDivisionEnum.Mechanical:
                    switch (actionTag)
                    {
                        case ActionTag.Completed:
                            return AuditActionEnum.Mechanical_Estimation_Completed;
                        case ActionTag.Pending:
                            return AuditActionEnum.Mechanical_Estimation_Pending;
                        case ActionTag.Not_Required:
                            return AuditActionEnum.Mechanical_Estimation_Not_Required;
                        default:
                            break;
                    }
                    break;
                case DepartmentDivisionEnum.Plumbing:
                    switch (actionTag)
                    {
                        case ActionTag.Completed:
                            return AuditActionEnum.Plumbing_Estimation_Completed;
                        case ActionTag.Pending:
                            return AuditActionEnum.Plumbing_Estimation_Pending;
                        case ActionTag.Not_Required:
                            return AuditActionEnum.Plumbing_Estimation_Not_Required;
                        default:
                            break;
                    }
                    break;
                case DepartmentDivisionEnum.Zoning:
                    switch (actionTag)
                    {
                        case ActionTag.Completed:
                            return AuditActionEnum.Zoning_Estimation_Completed;
                        case ActionTag.Pending:
                            return AuditActionEnum.Zoning_Estimation_Pending;
                        case ActionTag.Not_Required:
                            return AuditActionEnum.Zoning_Estimation_Not_Required;
                        default:
                            break;
                    }
                    break;
                case DepartmentDivisionEnum.Fire:
                    switch (actionTag)
                    {
                        case ActionTag.Completed:
                            return AuditActionEnum.Fire_Estimation_Completed;
                        case ActionTag.Pending:
                            return AuditActionEnum.Fire_Estimation_Pending;
                        case ActionTag.Not_Required:
                            return AuditActionEnum.Fire_Estimation_Not_Required;
                        default:
                            break;
                    }
                    break;
                case DepartmentDivisionEnum.Environmental:
                    switch (departmentName)
                    {
                        case DepartmentNameEnums.EH_Day_Care:
                            switch (actionTag)
                            {
                                case ActionTag.Completed:
                                    return AuditActionEnum.Commercial_Day_Care_Estimation_Completed;
                                case ActionTag.Pending:
                                    return AuditActionEnum.Commercial_Day_Care_Estimation_Pending;
                                case ActionTag.Not_Required:
                                    return AuditActionEnum.Commercial_Day_Care_Estimation_Not_Required;
                                default:
                                    break;
                            }

                            break;
                        case DepartmentNameEnums.EH_Food:
                            switch (actionTag)
                            {
                                case ActionTag.Completed:
                                    return AuditActionEnum.Food_Service_Estimation_Completed;
                                case ActionTag.Pending:
                                    return AuditActionEnum.Food_Service_Estimation_Pending;
                                case ActionTag.Not_Required:
                                    return AuditActionEnum.Food_Service_Estimation_Not_Required;
                                default:
                                    break;
                            }

                            break;
                        case DepartmentNameEnums.EH_Pool:
                            switch (actionTag)
                            {
                                case ActionTag.Completed:
                                    return AuditActionEnum.Public_Pool_Estimation_Completed;
                                case ActionTag.Pending:
                                    return AuditActionEnum.Public_Pool_Estimation_Pending;
                                case ActionTag.Not_Required:
                                    return AuditActionEnum.Public_Pool_Estimation_Not_Required;
                                default:
                                    break;
                            }

                            break;
                        case DepartmentNameEnums.EH_Facilities:
                            switch (actionTag)
                            {
                                case ActionTag.Completed:
                                    return AuditActionEnum.EHS_Facility_Lodging_Estimation_Completed;
                                case ActionTag.Pending:
                                    return AuditActionEnum.EHS_Facility_Lodging_Estimation_Pending;
                                case ActionTag.Not_Required:
                                    return AuditActionEnum.EHS_Facility_Lodging_Estimation_Not_Required;
                                default:
                                    break;
                            }

                            break;
                    }
                    break;
                case DepartmentDivisionEnum.Backflow:
                    switch (actionTag)
                    {
                        case ActionTag.Completed:
                            return AuditActionEnum.Backflow_Estimation_Completed;
                        case ActionTag.Pending:
                            return AuditActionEnum.Backflow_Estimation_Pending;
                        case ActionTag.Not_Required:
                            return AuditActionEnum.Backflow_Estimation_Not_Required;
                        default:
                            break;
                    }
                    break;
                case DepartmentDivisionEnum.NA:
                default:
                    break;
            }
            return AuditActionEnum.NA;
        }

    }
}
