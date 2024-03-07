using AION.BL;
using AION.BL.Adapters;
using AION.BL.BusinessObjects;
using AION.BL.Models;
using AION.Email.Engine.BusinessObjects;
using AION.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using AION.Manager.BusinessObjects;
using AION.Manager.Models;
using AIONEstimator.Engine.BusinessObjects;
using Meck.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;

namespace AION.Manager.Adapters
{
    public class AdminAdapter : BaseManagerAdapter, IAdminAdapter
    {
        public AdminModel GetAdminModel()
        {
            try
            {
                AdminModel model = new AdminModel();

                IHolidayConfigAdapter holidayConfigAdapter = new HolidayConfigAdapter();
                IPermissionAdapter permissionAdapter = new PermissionAdapter();
                ISchedulerAdapter schedulerAdapter = new SchedulerAdapter();
                IUserAdapter userAdapter = new UserAdapter();
                INPATypeAdapter npaTypeAdapter = new NPATypeAdapter();

                model.HolidayConfigList = holidayConfigAdapter.GetHolidayConfigList();
                model.SystemRolesWithPermissions = GetSystemRolesWithPermissions();
                model.PermissionsList = permissionAdapter.GetPermissionList();
                model.PlanReviewerAvailableHours = schedulerAdapter.GetAllPlanReviewerHours();
                model.PlanReviewerAvailableTimes = schedulerAdapter.GetAllPlanReviewerTimes();
                model.CatalogItems.AddRange(GetCatalogItems("ADMIN.MISC_CONFIG.SCHEDULE_DATE_CONFIGURATION"));
                model.CatalogItems.AddRange(GetCatalogItems("ADMIN.MISC_CONFIG.SAME_CONTRACTOR_DAYS"));
                model.CatalogItems.AddRange(GetCatalogItems("ADMIN.MISC_CONFIG.CANCELLATION_FEE_PER_HOUR"));
                model.CatalogItems.AddRange(GetCatalogItems("ADMIN.MISC_CONFIG.SCHEDULING_MULTIPLIER"));
                model.CatalogItems.AddRange(GetCatalogItems("COUNTY.ZONING.DEFAULTS"));
                model.AllReviewers = userAdapter.GetAllReviewers(false, true);
                model.NpaTypes = npaTypeAdapter.GetAll();
                model.MessageTemplateTypes = GetMessageTemplateTypes();
                model.MessageTemplateDataElements = GetMessageTemplateDataElements();

                int propertyType = 1;

                model.DefaultEstimationHours.Add(GetDefaultEstimationHour(DepartmentNameEnums.Building, (PropertyTypeEnums)propertyType));
                model.DefaultEstimationHours.Add(GetDefaultEstimationHour(DepartmentNameEnums.Electrical, (PropertyTypeEnums)propertyType));
                model.DefaultEstimationHours.Add(GetDefaultEstimationHour(DepartmentNameEnums.Mechanical, (PropertyTypeEnums)propertyType));
                model.DefaultEstimationHours.Add(GetDefaultEstimationHour(DepartmentNameEnums.Plumbing, (PropertyTypeEnums)propertyType));
                model.DefaultEstimationHours.Add(GetDefaultEstimationHour(DepartmentNameEnums.Zone_Cty_Chrlt, (PropertyTypeEnums)propertyType));
                model.DefaultEstimationHours.Add(GetDefaultEstimationHour(DepartmentNameEnums.Fire_County, (PropertyTypeEnums)propertyType));
                model.DefaultEstimationHours.Add(GetDefaultEstimationHour(DepartmentNameEnums.Fire_Cty_Chrlt, (PropertyTypeEnums)propertyType));
                model.DefaultEstimationHours.Add(GetDefaultEstimationHour(DepartmentNameEnums.EH_Day_Care, (PropertyTypeEnums)propertyType));
                model.DefaultEstimationHours.Add(GetDefaultEstimationHour(DepartmentNameEnums.EH_Food, (PropertyTypeEnums)propertyType));
                model.DefaultEstimationHours.Add(GetDefaultEstimationHour(DepartmentNameEnums.EH_Pool, (PropertyTypeEnums)propertyType));
                model.DefaultEstimationHours.Add(GetDefaultEstimationHour(DepartmentNameEnums.EH_Facilities, (PropertyTypeEnums)propertyType));
                model.DefaultEstimationHours.Add(GetDefaultEstimationHour(DepartmentNameEnums.Backflow, (PropertyTypeEnums)propertyType));
                model.DefaultEstimationHours.Add(GetDefaultEstimationHour(DepartmentNameEnums.Zone_County, (PropertyTypeEnums)propertyType));
                model.DefaultEstimationHours.Add(GetDefaultEstimationHour(DepartmentNameEnums.Zone_Davidson, (PropertyTypeEnums)propertyType));
                model.DefaultEstimationHours.Add(GetDefaultEstimationHour(DepartmentNameEnums.Zone_Huntersville, (PropertyTypeEnums)propertyType));
                model.DefaultEstimationHours.Add(GetDefaultEstimationHour(DepartmentNameEnums.Zone_Matthews, (PropertyTypeEnums)propertyType));
                model.DefaultEstimationHours.Add(GetDefaultEstimationHour(DepartmentNameEnums.Zone_Mint_Hill, (PropertyTypeEnums)propertyType));
                model.DefaultEstimationHours.Add(GetDefaultEstimationHour(DepartmentNameEnums.Zone_Pineville, (PropertyTypeEnums)propertyType));
                //LES-4519
                model.ProjectTypeList = new ProjectTypeAdapter().GetProjectTypeList();

                return model;
            }
            catch (System.Exception ex)
            {
                string errorMessage = "Error in GetAdminModel - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
        }

        public List<AuditActionRef> GetAuditActionRefs()
        {
            try
            {
                AuditActionRefModelBO bo = new AuditActionRefModelBO();
                return bo.AuditActionRefs;
            }
            catch (System.Exception ex)
            {
                string errorMessage = "Error in GetAuditActionRefs - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
        }

        public bool DefaultEstimationHourModelRefreshList()
        {
            try
            {
                DefaultEstimationHourModelBO bo = new DefaultEstimationHourModelBO();
                return bo.RefreshList();
            }
            catch (System.Exception ex)
            {
                string errorMessage = "Error in DefaultEstimationHourModelRefreshList - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
        }

        /// <summary>
        /// Gets the user list for the user management page only
        /// Fills designated roles
        /// </summary>
        /// <param name="filterString"></param>
        /// <param name="filterMode"></param>
        /// <returns></returns>
        public List<UserIdentity> GetUsersByFilterModeUserManagement(string filterString = "", string filterMode = "")
        {
            try
            {
                UserIdentityModelBO bo = new UserIdentityModelBO();

                List<UserIdentity> adminusers = bo.UserManagementSearchList(filterString, filterMode).ToList();
                return adminusers;


            }
            catch (System.Exception ex)
            {
                string errorMessage = "Error in GetUsersByFilterMode - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
        }

        public bool UpdateRoleOptions(int roleID, string oldRoleOptionsString, string newRoleOptionsString)
        {
            try
            {
                SystemRoleModelBO bo = new SystemRoleModelBO();
                return bo.UpdateRoleOptions(roleID, oldRoleOptionsString, newRoleOptionsString);
            }
            catch (System.Exception ex)
            {
                string errorMessage = "Error in UpdateRoleOptions - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
        }

        public DefaultEstimationHour GetDefaultEstimationHour(DepartmentNameEnums departmentNameEnum, PropertyTypeEnums propertyTypeEnum)
        {
            try
            {
                DefaultEstimationHourModelBO bo = new DefaultEstimationHourModelBO();
                return bo.GetInstance(departmentNameEnum, propertyTypeEnum);

            }
            catch (System.Exception ex)
            {
                string errorMessage = "Error in GetDefaultEstimationHour - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
        }

        public bool UpdateDefaultEstimationHour(DefaultEstimationHour data)
        {
            try
            {
                DefaultEstimationHourModelBO bo = new DefaultEstimationHourModelBO();
                return bo.UpdateInstance(data);
            }
            catch (System.Exception ex)
            {
                string errorMessage = "Error in UpdateDefaultEstimationHour - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
        }

        public List<CatalogItem> GetCatalogItems(string catalogGroupExternalRef)
        {
            try
            {
                CatalogItemModelBO bo = new CatalogItemModelBO();
                return bo.GetInstance(catalogGroupExternalRef);
            }
            catch (System.Exception ex)
            {
                string errorMessage = "Error in GetCatalogItems - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
        }

        public bool UpdateCatalogItem(CatalogItem data)
        {
            try
            {
                CatalogItemModelBO bo = new CatalogItemModelBO();
                return bo.UpdateInstance(data);
            }
            catch (System.Exception ex)
            {
                string errorMessage = "Error in UpdateCatalogItem - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
        }

        public List<SystemRole> GetSystemRolesWithPermissions()
        {
            try
            {
                SystemRoleModelBO bo = new SystemRoleModelBO();
                PermissionModelBO permissionModelBO = new PermissionModelBO();
                List<SystemRole> systemRoles = bo.CreateInstance().ToList();
                foreach (SystemRole systemRole in systemRoles)
                {
                    systemRole.Permissions = permissionModelBO.GetBySystemRoleID(systemRole.ID);
                }
                return systemRoles;
            }
            catch (System.Exception ex)
            {
                string errorMessage = "Error in GetSystemRoles - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
        }
        public List<SystemRole> GetSystemRoles()
        {
            try
            {
                SystemRoleModelBO bo = new SystemRoleModelBO();
                return bo.CreateInstance();
            }
            catch (System.Exception ex)
            {
                string errorMessage = "Error in GetSystemRoles - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
        }

        public List<Permission> GetSystemRolePermissionList(int systemRoleId)
        {
            try
            {
                PermissionModelBO bo = new PermissionModelBO();
                return bo.GetBySystemRoleID(systemRoleId);
            }
            catch (System.Exception ex)
            {
                string errorMessage = "Error in GetSystemRolePermissionList - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
        }

        public List<Permission> GetUserPermissionList(int userId)
        {
            try
            {
                //get all the permissions for all the roles this user is in
                PermissionModelBO bo = new PermissionModelBO();
                return bo.GetByUserID(userId);
            }
            catch (System.Exception ex)
            {
                string errorMessage = "Error in GetUserPermissionList - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
        }

        public int GetGateDateConfig()
        {
            try
            {
                var catalogItems = GetCatalogItems("ADMIN.MISC_CONFIG.SCHEDULE_DATE_CONFIGURATION");
                var scheduleDateConfigItem = catalogItems.FirstOrDefault(x => x.CatalogGroupRefName == "ADMIN.MISC_CONFIG.SCHEDULE_DATE_CONFIGURATION");

                var isNum = int.TryParse(scheduleDateConfigItem.Value, out int gateDateConfig);
                if (!isNum)
                {
                    throw new System.Exception();
                }
                return gateDateConfig;
            }
            catch (System.Exception ex)
            {
                string errorMessage = "Error in GetGateDateConfig - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
        }

        public bool SendInactivePlanReviewerEmail(int userId)
        {
            try
            {
                UserAdapter userAdapter = new UserAdapter();
                List<Reviewer> reviewers = userAdapter.GetAllReviewers();

                UserScheduleBO userScheduleBO = new UserScheduleBO();
                UserIdentityModelBO userBO = new UserIdentityModelBO();
                EmailMessageBO emailBO = new EmailMessageBO();
                UserIdentity userBE = userBO.GetInstance(userId);
                string planReviewerName = userBE.FirstName + " " + userBE.LastName;
                List<Facilitator> facilitators = new UserAdapter().GetAllFacilitators();
                List<UserScheduleBE> AssignedExpressReviews = userScheduleBO.GetDisablePlanReviewerAllocations(userId, "EXP");
                List<UserScheduleBE> ReservedExpressReviews = userScheduleBO.GetDisablePlanReviewerAllocations(userId, "EMA");
                if (AssignedExpressReviews.Count > 0 || ReservedExpressReviews.Count > 0)
                {
                    List<DateTime?> AssignedExpressReviewStartDate = AssignedExpressReviews.Select(x => x.StartDateTime).Distinct().ToList();
                    List<DateTime?> ReservedExpressReviewStartDate = ReservedExpressReviews.Select(x => x.StartDateTime).Distinct().ToList();
                    EmailAdapter emailAdapter = new EmailAdapter();
                    //get mail message defaults
                    MailMessage mailMessage = emailAdapter.GetMailMessage();
                    foreach (var item in facilitators)
                    {
                        if (!String.IsNullOrWhiteSpace(item.Email) && item.Email.Contains("@"))
                            mailMessage.To.Add(new MailAddress(item.Email));
                    }
                    string subject = "Plan Reviewer Not available for Express Plan Review - " + planReviewerName;
                    string htmlMessageBody = emailBO.CreatePlanReviewerNotAvailableForExpressMessageBody(planReviewerName, AssignedExpressReviewStartDate, ReservedExpressReviewStartDate);
                    mailMessage.Subject = subject;
                    mailMessage.Body = htmlMessageBody;
                    emailAdapter.SendEmailMessage(mailMessage);
                }
                return true;

            }
            catch (System.Exception ex)
            {
                string errorMessage = "Error in SendInactivePlanReviewerEmail - " + ex.Message;
                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
        }

        public List<MessageTemplate> GetMessageTemplatesByTypeId(int templateTypeId)
        {
            try
            {
                List<MessageTemplate> messageTemplates = new List<MessageTemplate>();
                List<MessageTemplateBE> messageTemplateBEs = new MessageTemplateBO().GetListByTypeId(templateTypeId);
                messageTemplates = messageTemplateBEs.Select(x => new MessageTemplate
                {
                    ActiveDt = x.ActiveDt,
                    ActiveInd = x.ActiveInd,
                    TemplateId = x.TemplateId,
                    TemplateName = x.TemplateName,
                    TemplateText = x.TemplateText,
                    TemplateTypeId = x.TemplateTypeId,
                    DisplayActiveDate = x.ActiveDt.HasValue ? x.ActiveDt.Value.ToShortDateString() : string.Empty,
                    DisplayActiveTime = x.ActiveDt.HasValue ? x.ActiveDt.Value.ToShortTimeString() : string.Empty
                }).ToList();
                return messageTemplates;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in GetMessageTemplatesByTypeId - " + ex.Message;
                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
        }

        public MessageTemplate GetMessageTemplateById(int id)
        {
            try
            {
                MessageTemplateBE messageTemplateBE = new MessageTemplateBO().GetById(id);
                MessageTemplate messageTemplate = new MessageTemplate
                {
                    ActiveDt = messageTemplateBE.ActiveDt,
                    ActiveInd = messageTemplateBE.ActiveInd,
                    TemplateId = messageTemplateBE.TemplateId,
                    TemplateName = messageTemplateBE.TemplateName,
                    TemplateText = messageTemplateBE.TemplateText,
                    TemplateTypeId = messageTemplateBE.TemplateTypeId,
                    DisplayActiveDate = messageTemplateBE.ActiveDt.HasValue ? messageTemplateBE.ActiveDt.Value.ToShortDateString() : string.Empty,
                    DisplayActiveTime = messageTemplateBE.ActiveDt.HasValue ? messageTemplateBE.ActiveDt.Value.ToShortTimeString() : string.Empty
                };
                return messageTemplate;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in GetMessageTemplateById - " + ex.Message;
                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
        }

        public List<MessageTemplateType> GetMessageTemplateTypes()
        {
            try
            {
                List<MessageTemplateType> messageTemplateTypes = new List<MessageTemplateType>();
                List<MessageTemplateTypeBE> messageTemplateBEs = new MessageTemplateTypeBO().GetList(1);
                messageTemplateTypes = messageTemplateBEs.Select(x => new MessageTemplateType
                {
                    TemplateTypeId = x.TemplateTypeId,
                    TemplateTypeName = x.TemplateTypeName,
                    TemplateTypeDesc = x.TemplateTypeDesc,
                    EnumMappingValNbr = x.EnumMappingValNbr,
                    IsEditable = x.IsEditable,
                    TemplateModuleId = x.TemplateModuleId
                }).ToList();
                return messageTemplateTypes;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in GetMessageTemplateTypes - " + ex.Message;
                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }

        }

        public List<MessageTemplateDataElement> GetMessageTemplateDataElements()
        {
            try
            {
                List<MessageTemplateDataElement> messageTemplateDataElements = new List<MessageTemplateDataElement>();
                List<MessageTemplateDataElementBE> messageTemplateDataElementBEs = new MessageTemplateDataElementBO().GetList();
                messageTemplateDataElements = messageTemplateDataElementBEs.Select(x => new MessageTemplateDataElement
                {
                    DataElementDesc = x.DataElementDesc,
                    DataElementId = x.DataElementId,
                    DataElementName = x.DataElementName,
                    DataElementValTxt = x.DataElementValTxt,
                    EnumMappingValNbr = x.EnumMappingValNbr
                }).ToList();
                return messageTemplateDataElements;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in GetMessageTemplateDataElements - " + ex.Message;
                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }

        }
        public bool InsertMessageTemplate(MessageTemplate item)
        {
            try
            {
                //TODO:decide status. if this message is active, then set all others of this type to inactive?
                //TODO: unencode the text
                string messagetxt = WebUtility.HtmlDecode(item.TemplateText);
                MessageTemplateBE messageTemplate = new MessageTemplateBE
                {
                    ActiveDt = item.ActiveDt,
                    ActiveInd = item.ActiveInd,
                    UserId = item.CreatedUser.ID.ToString(),
                    TemplateName = item.TemplateName,
                    TemplateText = messagetxt,
                    TemplateTypeId = item.TemplateTypeId,
                    IsActive = item.ActiveInd.HasValue ? item.ActiveInd.Value : false
                };
                return new MessageTemplateBO().Create(messageTemplate) > 0;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in InsertMessageTemplate - " + ex.Message;
                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;
            }
        }

        public bool UpdateMessageTemplate(MessageTemplate item) {
            try {
                MessageTemplate mt = GetMessageTemplateById(item.TemplateId.Value);
                string messagetxt = WebUtility.HtmlDecode(item.TemplateText);
                MessageTemplateBO bo = new MessageTemplateBO();
                MessageTemplateBE BE = bo.GetById(mt.TemplateId.Value);
                BE.ActiveDt = item.ActiveDt;
                BE.ActiveInd = item.ActiveInd;
                BE.TemplateName = item.TemplateName;
                BE.TemplateText = messagetxt;
                BE.UserId = item.UpdatedUser.ID.ToString();
                BE.TemplateTypeId = item.TemplateTypeId;
                BE.IsActive = item.ActiveInd.HasValue ? item.ActiveInd.Value : false;
                return new MessageTemplateBO().Update(BE) > 0;
            } catch (Exception ex) {
                string errorMessage = "Error in UpdateMessageTemplate - " + ex.Message;
                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw ex;

            }
        }
    }

    public interface IAdminAdapter
    {
        AdminModel GetAdminModel();
        bool UpdateRoleOptions(int roleID, string oldRoleOptionsString, string newRoleOptionsString);
        List<AuditActionRef> GetAuditActionRefs();
        bool DefaultEstimationHourModelRefreshList();
        DefaultEstimationHour GetDefaultEstimationHour(DepartmentNameEnums departmentNameEnum, PropertyTypeEnums propertyTypeEnum);
        bool UpdateDefaultEstimationHour(DefaultEstimationHour data);
        List<CatalogItem> GetCatalogItems(string catalogGroupExternalRef);
        bool UpdateCatalogItem(CatalogItem data);
        List<SystemRole> GetSystemRolesWithPermissions();
        List<SystemRole> GetSystemRoles();

        /// <summary>
        /// Used in AJAX call from Admin - Modify Role
        /// </summary>
        /// <param name="systemRoleId"></param>
        /// <returns></returns>
        List<Permission> GetSystemRolePermissionList(int systemRoleId);

        /// <summary>
        /// User in AJAX call from admin - Modify User permissions
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        List<Permission> GetUserPermissionList(int userid);
        int GetGateDateConfig();

        bool SendInactivePlanReviewerEmail(int userId);

        /// <summary>
        /// Admin--> Message Configuration : Get the Message Template by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        MessageTemplate GetMessageTemplateById(int id);

        /// <summary>
        /// Admin --> Message Configuration: Get the message templates by the template type id
        /// </summary>
        /// <param name="templateTypeId"></param>
        /// <returns></returns>
        List<MessageTemplate> GetMessageTemplatesByTypeId(int templateTypeId);

        /// <summary>
        /// Admin --> Message Configuration: Get the Template Types filter list
        /// </summary>
        /// <returns></returns>
        List<MessageTemplateType> GetMessageTemplateTypes();

        /// <summary>
        /// Admin --> Message Configuration: Get the Data Elements List
        /// </summary>
        /// <returns></returns>
        List<MessageTemplateDataElement> GetMessageTemplateDataElements();

        /// <summary>
        /// Admin --> Message Configuration: insert new message template
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool InsertMessageTemplate(MessageTemplate item);

        /// <summary>
        /// Admin --> Message Configuration: update existing message template
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool UpdateMessageTemplate(MessageTemplate item);
    }
}