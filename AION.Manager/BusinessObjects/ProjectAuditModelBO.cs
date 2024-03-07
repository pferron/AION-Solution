using AION.Base;
using AION.BL;
using AION.BL.BusinessObjects;
using AION.BL.Models.Base;
using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using AION.Manager.Models;
using Meck.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace AION.Manager.BusinessObjects
{
    public class ProjectAuditModelBO : BaseAdapter
    {
        private ProjectAuditBO _projectAuditBO;
        private ProjectAuditBE _projectAuditBE;
        private ProjectAudit _projectAudit;
        public bool Create(ProjectAudit projectAudit)
        {
            _projectAudit = projectAudit;
            int projectauditid = 0;
            try
            {
                _projectAuditBO = new ProjectAuditBO();
                ConvertProjectAuditToBE();
                projectauditid = _projectAuditBO.Create(_projectAuditBE);
            }
            catch (System.Exception ex)
            {

                string errorMessage = "Error in Create - data: " + JsonConvert.SerializeObject(projectAudit) + " - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
            }

            return (projectauditid != 0);
        }
        internal static ProjectAudit CreateInstance(int projectId, string auditActionDetailsTxt, string auditUserId, DateTime auditDt, int auditActionRefId)
        {
            ProjectAudit projectAudit = new ProjectAudit
            {
                AuditActionDetailsTxt = auditActionDetailsTxt,
                AuditActionRefId = auditActionRefId,
                AuditDt = auditDt,
                AuditUserId = auditUserId,
                ProjectId = projectId
            };
            return projectAudit;
        }
        public bool InsertProjectAudit(int projectId, string auditActionDetailsTxt, string auditUserId, AuditActionEnum auditAction)
        {
            return Create(CreateInstance(projectId, auditActionDetailsTxt, auditUserId, DateTime.Now, (int)auditAction));
        }
        public bool InsertProjectAudit(int projectId, string auditActionDetailsTxt, string auditUserId, DateTime auditDt, int auditActionRefId)
        {
            int projectauditid = 0;
            try
            {
                ProjectAuditBO projectAuditBO = new ProjectAuditBO();
                //updating user is always system 1
                ProjectAuditBE projectAuditBE = new ProjectAuditBE
                {
                    AuditActionDetailsTxt = auditActionDetailsTxt,
                    AuditActionRefId = auditActionRefId,
                    AuditDt = auditDt,
                    AuditUserId = auditUserId,
                    ProjectId = projectId,
                    UserId = "1"
                };

                projectauditid = projectAuditBO.Create(projectAuditBE);
            }
            catch (System.Exception ex)
            {

                string errorMessage = "Error in InsertProjectAudit:  - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
            }
            return (projectauditid != 0);

        }

        /// <summary>
        /// If the project is not_scheduled and the audit action comes from estimation, 
        /// insert completed row
        /// </summary>
        /// <param name="project"></param>
        /// <returns></returns>
        public bool InsertProjectAudit_AllEstimationsCompleted(Project project)
        {
            ProjectStatus completestatus = new ProjectStatusModelBO().GetInstance(ProjectStatusEnum.Not_Scheduled);
            if (project.AIONProjectStatus.ProjectStatusEnum == completestatus.ProjectStatusEnum
                && project.AuditAction == AuditActionEnum.Estimation_Change)
            {
                StringBuilder txt = new StringBuilder();
                string auditUserNm = project.UpdatedUser.ID.ToString();
                AuditActionEnum auditAction = AuditActionEnum.All_Estimations_Completed;
                txt.Append(AuditActionEnum.All_Estimations_Completed.ToStringValue());
                txt.Append(Environment.NewLine);
                //get the hours
                foreach (ProjectTrade trade in project.Trades)
                {
                    //division name
                    string division = Enum.GetName(typeof(DepartmentDivisionEnum), (int)trade.DepartmentDivision);
                    txt.Append(division);
                    txt.Append(" : ");
                    if (trade.EstimationNotApplicable)
                    {
                        txt.Append("Not Required");
                    }
                    else
                    {
                        txt.Append("Estimated Hours: ");
                        txt.Append(trade.EstimationHours.ToString());

                    }
                    txt.Append(Environment.NewLine);
                }
                foreach (ProjectAgency agency in project.Agencies)
                {
                    //division name
                    string division = Enum.GetName(typeof(DepartmentDivisionEnum), (int)agency.DepartmentDivision);
                    string agencyname = agency.DepartmentInfo.ToStringValue();
                    txt.Append(division);
                    txt.Append(" - ");
                    txt.Append(agencyname);
                    txt.Append(" : ");
                    if (agency.EstimationNotApplicable)
                    {
                        txt.Append("Not Required");
                    }
                    else
                    {
                        txt.Append("Estimated Hours: ");
                        txt.Append(agency.EstimationHours.ToString());

                    }
                    txt.Append(Environment.NewLine);
                }
                return Create(CreateInstance(project.ID, txt.ToString(), auditUserNm, DateTime.Now, (int)auditAction));
            }
            return true;
        }

        /// <summary>
        /// only do this when the dept is being estimated
        /// 
        /// </summary>
        /// <param name="projectid"></param>
        /// <param name="dept"></param>
        /// <param name="auditUserId"></param>
        /// <returns></returns>
        internal static bool InsertProjectDeptAudit(int projectid, ProjectDepartment dept, string auditUserId)
        {
            if (dept.AuditAction != AuditActionEnum.Estimation_Change) return false;

            StringBuilder txt = new StringBuilder();

            //string division = Enum.GetName(typeof(DepartmentDivisionEnum), (int)dept.DepartmentDivision);
            //string agencyname = dept.DepartmentInfo.ToStringValue();
            ActionTag actionTag = ActionTag.NA;

            if (dept.EstimationNotApplicable) { actionTag = ActionTag.Not_Required; }
            else
            {
                switch (dept.DepartmentStatusRef.ProjectStatusEnum)
                {
                    case ProjectStatusEnum.Preliminary_Meeting_Required:
                    case ProjectStatusEnum.Scope_Drawings_Required:
                    case ProjectStatusEnum.Information_Required:
                        actionTag = ActionTag.Pending;
                        break;
                    default:
                        actionTag = ActionTag.Completed;
                        break;
                }

            }

            //get 'pending', 'completed', 'not required' for each trade/agency
            AuditActionEnum auditActionEnum = AuditActionRefModelBO.AuditActionByDeptByAction(dept.DepartmentDivision, actionTag, dept.DepartmentInfo);

            txt.Append(auditActionEnum.ToStringValue());
            txt.Append(" : ");
            txt.Append("Estimated Hours: ");
            txt.Append(dept.EstimationHours.ToString());

            return new ProjectAuditModelBO().Create(CreateInstance(projectid, txt.ToString(), auditUserId, DateTime.Now, (int)auditActionEnum));

        }

        /// <summary>
        /// Insert EMA, FMA, PMA project audit
        /// </summary>
        /// <param name="attendees"></param>
        /// <param name="projectId"></param>
        /// <param name="auditActionDetailsTxt"></param>
        /// <param name="auditUserId"></param>
        /// <param name="statusEnum"></param>
        /// <returns></returns>
        public bool InsertProjectAudit(List<AttendeeInfo> attendees, int projectId, string meetinginfo, string auditUserId, AppointmentResponseStatusEnum statusEnum)
        {
            //choose audittypeenum by statusenum
            AuditActionEnum auditAction = AuditActionEnum.NA;
            switch (statusEnum)
            {
                case AppointmentResponseStatusEnum.Reject:
                    break;
                case AppointmentResponseStatusEnum.Self_Schedule:
                    break;
                case AppointmentResponseStatusEnum.No_Reply:
                    break;
                case AppointmentResponseStatusEnum.Not_Scheduled:
                    break;
                case AppointmentResponseStatusEnum.Scheduled:
                    break;
                case AppointmentResponseStatusEnum.Accept:
                    auditAction = AuditActionEnum.Review_Date_Accepted;
                    break;
                case AppointmentResponseStatusEnum.Cancelled:
                    auditAction = AuditActionEnum.Appointment_Cancelled;
                    break;
                case AppointmentResponseStatusEnum.Closed:
                    break;
                case AppointmentResponseStatusEnum.Tentatively_Scheduled:
                    auditAction = AuditActionEnum.Review_Tentatively_Scheduled;
                    break;
                default:
                    break;
            }
            if (auditAction != AuditActionEnum.NA)
            {
                //Trade, Reviewer, Appointment
                StringBuilder adtdtls = new StringBuilder();
                foreach (AttendeeInfo info in attendees)
                {
                    string trade = ((DepartmentNameEnums)info.DeptNameEnumId).ToStringValue();
                    string date = meetinginfo;
                    string name = info.FirstName + " " + info.LastName;
                    adtdtls.Append(trade);
                    adtdtls.Append(" ");
                    adtdtls.Append(date);
                    adtdtls.Append(" ");
                    adtdtls.Append(name);
                    adtdtls.Append(" ");
                }
                InsertProjectAudit(projectId, adtdtls.ToString(), auditUserId, DateTime.Now, (int)auditAction);

            }
            return true;
        }

        /// <summary>
        /// Plan Review is sPeciAl
        /// </summary>
        /// <param name="attendees"></param>
        /// <param name="projectId"></param>
        /// <param name="auditActionDetailsTxt"></param>
        /// <param name="auditUserId"></param>
        /// <param name="statusEnum"></param>
        /// <returns></returns>
        public bool InsertProjectAuditPR(List<AttendeeInfo> attendees, int projectId, string auditActionDetailsTxt, string auditUserId, AppointmentResponseStatusEnum statusEnum)
        {
            //choose audittypeenum by statusenum
            AuditActionEnum auditAction = AuditActionEnum.NA;
            switch (statusEnum)
            {
                case AppointmentResponseStatusEnum.Reject:
                    break;
                case AppointmentResponseStatusEnum.Self_Schedule:
                    break;
                case AppointmentResponseStatusEnum.No_Reply:
                    break;
                case AppointmentResponseStatusEnum.Not_Scheduled:
                    break;
                case AppointmentResponseStatusEnum.Scheduled:
                    break;
                case AppointmentResponseStatusEnum.Accept:
                    auditAction = AuditActionEnum.Review_Date_Accepted;
                    break;
                case AppointmentResponseStatusEnum.Cancelled:
                    auditAction = AuditActionEnum.Appointment_Cancelled;
                    break;
                case AppointmentResponseStatusEnum.Closed:
                    break;
                case AppointmentResponseStatusEnum.Tentatively_Scheduled:
                    auditAction = AuditActionEnum.Review_Tentatively_Scheduled;
                    break;
                default:
                    break;
            }
            if (auditAction != AuditActionEnum.NA)
            {
                //Trade, Reviewer, Appointment
                StringBuilder adtdtls = new StringBuilder();
                adtdtls.Append(auditActionDetailsTxt);
                foreach (AttendeeInfo info in attendees)
                {
                    string trade = ((DepartmentNameEnums)info.DeptNameEnumId).ToStringValue();
                    string date = info.MeetingInfo;
                    string name = info.FirstName + " " + info.LastName;
                    adtdtls.Append(" ");
                    adtdtls.Append(trade);
                    adtdtls.Append(" ");
                    adtdtls.Append(date);
                    adtdtls.Append(" ");
                    adtdtls.Append(name);
                    adtdtls.Append(" ");
                }
                InsertProjectAudit(projectId, adtdtls.ToString(), auditUserId, DateTime.Now, (int)auditAction);
            }
            return true;
        }

        public void InsertProjectStatusAuditLog(int projectId, int newProjectStatusEnumId, string userId)
        {
            ProjectStatus newprojectStatus = new ProjectStatusModelBO().GetInstance((ProjectStatusEnum)newProjectStatusEnumId);
            InsertProjectAudit(projectId, newprojectStatus.ProjectStatusEnum.ToStringValue(), userId, AuditActionEnum.Status_Changed);

        }
        /// <summary>
        /// LES-3809 - add project audit for auto scheduling
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="userId">updating user</param>
        /// <param name="autoscheduleYN">Yes/No values</param>
        public static void InsertAutoScheduledAudit(int projectId, string userId, string autoscheduleYN)
        {
            new ProjectAuditModelBO().InsertProjectAudit(projectId, autoscheduleYN, userId, AuditActionEnum.Auto_Schedule);

        }
        private void ConvertProjectAuditToBE()
        {
            _projectAuditBE = new ProjectAuditBE
            {
                ProjectId = _projectAudit.ProjectId,
                AuditActionDetailsTxt = _projectAudit.AuditActionDetailsTxt,
                AuditActionRefId = _projectAudit.AuditActionRefId,
                AuditDt = _projectAudit.AuditDt,
                AuditUserId = _projectAudit.AuditUserId,
                ProjectAuditId = _projectAudit.ProjectAuditId,
                UserId = "1"//always system updated

            };
        }

    }
}
