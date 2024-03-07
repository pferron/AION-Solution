using AION.Base;
using AION.BL;
using AION.BL.Adapters;
using AION.BL.Models;
using AION.Manager.Adapters;
using AION.Manager.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace AION.Manager.Controllers
{
    [Authorize]
    public class AdminController : BaseApiController
    {
        public AdminController()
        {

        }

        /// <summary>
        /// Get Admin Model for Admin
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(AdminModel))]
        [Route("api/Admin/GetAdminModel")]
        public IHttpActionResult GetAdminModel()
        {
            IAdminAdapter thisengine = new AdminAdapter();

            var result = thisengine.GetAdminModel();

            return Ok(result);
        }

        /// <summary>
        /// Get Holiday config list for Admin
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(List<HolidayConfig>))]
        [Route("api/Admin/GetHolidayConfigList")]
        public IHttpActionResult GetHolidayConfigList()
        {
            IHolidayConfigAdapter thisengine = new HolidayConfigAdapter();

            var result = thisengine.GetHolidayConfigList();

            return Ok(result);
        }

        /// <summary>
        /// Get Holiday date list for Admin
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(List<DateTime>))]
        [Route("api/Admin/GetHolidayDateList")]
        public IHttpActionResult GetHolidayDateList()
        {
            IHolidayConfigAdapter thisengine = new HolidayConfigAdapter();

            var result = thisengine.GetHolidayConfigDates();

            return Ok(result);
        }

        /// <summary>
        /// Delete Holiday Config in Admin
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(int))]
        [Route("api/Admin/DeleteHoliday")]
        public IHttpActionResult DeleteHoliday(HolidayConfigManagerModel obj)
        {
            IHolidayConfigAdapter thisengine = new HolidayConfigAdapter();

            var result = thisengine.DeleteHoliday(obj.HolidayIds);

            return Ok(result);
        }

        /// <summary>
        /// Inserts Holiday Config in Admin
        /// </summary>
        /// <param name="holidayConfig"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(int))]
        [Route("api/Admin/InsertHolidayConfig")]
        public IHttpActionResult InsertHolidayConfig(HolidayConfig holidayConfig)
        {
            IHolidayConfigAdapter thisengine = new HolidayConfigAdapter();

            var result = thisengine.InsertHolidayConfig(holidayConfig);

            return Ok(result);
        }

        /// <summary>
        /// GetAuditActionRefs
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(List<AuditActionRef>))]
        [Route("api/Admin/GetAuditActionRefs")]
        public IHttpActionResult GetAuditActionRefs()
        {
            IAdminAdapter thisengine = new AdminAdapter();

            var result = thisengine.GetAuditActionRefs();

            return Ok(result);
        }

        /// <summary>
        /// DefaultEstimationHourModelRefreshList
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(bool))]
        [Route("api/Admin/DefaultEstimationHourModelRefreshList")]
        public IHttpActionResult DefaultEstimationHourModelRefreshList()
        {
            IAdminAdapter thisengine = new AdminAdapter();

            var result = thisengine.DefaultEstimationHourModelRefreshList();

            return Ok(result);
        }

        [HttpPost]
        [ResponseType(typeof(List<UserIdentity>))]
        [Route("api/Admin/GetUsersByFilterModeUserManagement")]
        public IHttpActionResult GetUsersByFilterModeUserManagement(FiltersManagerModel obj)
        {
            AdminAdapter thisengine = new AdminAdapter();

            var result = thisengine.GetUsersByFilterModeUserManagement(obj.FilterString, obj.FilterMode);

            return Ok(result);
        }


        /// <summary>
        /// GetAllPlanReviewerHours
        /// </summary>
        /// <returns></returns>
        //List<PlanReviewerAvailableHour> GetAllPlanReviewerHours();
        [HttpGet]
        [ResponseType(typeof(List<PlanReviewerAvailableHour>))]
        [Route("api/Admin/GetAllPlanReviewerHours")]
        public IHttpActionResult GetAllPlanReviewerHours()
        {
            ISchedulerAdapter thisengine = new SchedulerAdapter();

            var result = thisengine.GetAllPlanReviewerHours();

            return Ok(result);
        }

        /// <summary>
        /// GetAllPlanReviewerTimes
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(List<PlanReviewerAvailableTime>))]
        [Route("api/Admin/GetAllPlanReviewerTimes")]
        public IHttpActionResult GetAllPlanReviewerTimes()
        {
            ISchedulerAdapter thisengine = new SchedulerAdapter();

            var result = thisengine.GetAllPlanReviewerTimes();

            return Ok(result);
        }

        /// <summary>
        /// UpdatePlanReviewAvailableHours
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        //bool UpdatePlanReviewAvailableHours(PlanReviewerAvailableHour value);
        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route("api/Admin/UpdatePlanReviewAvailableHours")]
        public IHttpActionResult UpdatePlanReviewAvailableHours(PlanReviewerAvailableHour obj)
        {
            ISchedulerAdapter thisengine = new SchedulerAdapter();

            var result = thisengine.UpdatePlanReviewAvailableHours(obj);

            return Ok(result);
        }

        /// <summary>
        /// UpdatePlanReviewAvailableTimes
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        //bool UpdatePlanReviewAvailableTimes(PlanReviewerAvailableTime value);
        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route("api/Admin/UpdatePlanReviewAvailableTimes")]
        public IHttpActionResult UpdatePlanReviewAvailableTimes(PlanReviewerAvailableTime obj)
        {
            ISchedulerAdapter thisengine = new SchedulerAdapter();

            var result = thisengine.UpdatePlanReviewAvailableTimes(obj);

            return Ok(result);
        }

        /// <summary>
        /// GetDefaultEstimationHour
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        //DefaultEstimationHour GetDefaultEstimationHour(DepartmentNameEnums departmentNameEnum, PropertyTypeEnums propertyTypeEnum)
        [HttpPost]
        [ResponseType(typeof(DefaultEstimationHour))]
        [Route("api/Admin/GetDefaultEstimationHour")]
        public IHttpActionResult GetDefaultEstimationHour(DefaultEstimationHourManagerModel obj)
        {
            IAdminAdapter thisengine = new AdminAdapter();

            var result = thisengine.GetDefaultEstimationHour(obj.DepartmentNameEnum, obj.PropertyTypeEnum);

            return Ok(result);
        }

        /// <summary>
        /// UpdateDefaultEstimationHour
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        //bool UpdateDefaultEstimationHour(DefaultEstimationHour data)
        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route("api/Admin/UpdateDefaultEstimationHour")]
        public IHttpActionResult UpdateDefaultEstimationHour(DefaultEstimationHour obj)
        {
            IAdminAdapter thisengine = new AdminAdapter();

            var result = thisengine.UpdateDefaultEstimationHour(obj);

            return Ok(result);
        }

        /// <summary>
        /// GetCatalogItems
        /// </summary>
        /// <param name="catalogGroupExternalRef"></param>
        /// <returns></returns>
        //List<CatalogItem> GetCatalogItems(string catalogGroupExternalRef)
        [HttpGet]
        [ResponseType(typeof(List<CatalogItem>))]
        [Route("api/Admin/GetCatalogItems")]
        public IHttpActionResult GetCatalogItems(string catalogGroupExternalRef)
        {
            IAdminAdapter thisengine = new AdminAdapter();

            var result = thisengine.GetCatalogItems(catalogGroupExternalRef);

            return Ok(result);
        }

        /// <summary>
        /// UpdateCatalogItem
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        //bool UpdateCatalogItem(CatalogItem data)
        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route("api/Admin/UpdateCatalogItem")]
        public IHttpActionResult UpdateCatalogItem(CatalogItem data)
        {
            IAdminAdapter thisengine = new AdminAdapter();

            var result = thisengine.UpdateCatalogItem(data);

            return Ok(result);
        }

        /// <summary>
        /// GetSystemRoles
        /// </summary>
        /// <returns></returns>
        //List<SystemRole> GetSystemRoles()
        [HttpGet]
        [ResponseType(typeof(List<SystemRole>))]
        [Route("api/Admin/GetSystemRoles")]
        public IHttpActionResult GetSystemRoles()
        {
            IAdminAdapter thisengine = new AdminAdapter();

            var result = thisengine.GetSystemRoles();

            return Ok(result);
        }

        [HttpGet]
        [ResponseType(typeof(bool))]
        [Route("api/Admin/InsertDefaultPermSysRoleXref")]
        public IHttpActionResult InsertDefaultPermSysRoleXref()
        {
            IPermissionAdapter thisengine = new PermissionAdapter();

            var result = thisengine.InsertDefaultRolePermissions();

            return Ok(result);
        }

        [HttpGet]
        [ResponseType(typeof(bool))]
        [Route("api/Admin/GetPermissionList")]
        public IHttpActionResult GetPermissionList()
        {
            IPermissionAdapter thisengine = new PermissionAdapter();

            var result = thisengine.GetPermissionList();

            return Ok(result);
        }

        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route("api/Admin/SaveSystemRole")]
        public IHttpActionResult SaveSystemRole(SystemRole systemRole)
        {
            IPermissionAdapter thisengine = new PermissionAdapter();
            var result = thisengine.SaveSystemRole(systemRole);
            return Ok(result);
        }

        /// <summary>
        /// Gets the System role list with Permissions populated
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(List<SystemRole>))]
        [Route("api/Admin/GetSystemRolesWithPermissions")]
        public IHttpActionResult GetSystemRolesWithPermissions()
        {
            IAdminAdapter thisengine = new AdminAdapter();

            var result = thisengine.GetSystemRolesWithPermissions();

            return Ok(result);
        }

        /// <summary>
        /// Gets the permissions list for a system role
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(List<Permission>))]
        [Route("api/Admin/GetSystemRolePermissionList")]
        public IHttpActionResult GetSystemRolePermissionList(int id)
        {
            IAdminAdapter thisengine = new AdminAdapter();

            var result = thisengine.GetSystemRolePermissionList(id);

            return Ok(result);
        }
        /// <summary>
        /// Gets the permissions list for user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(List<Permission>))]
        [Route("api/Admin/GetUserPermissionList")]
        public IHttpActionResult GetUserPermissionList(int id)
        {
            IAdminAdapter thisengine = new AdminAdapter();

            var result = thisengine.GetUserPermissionList(id);

            return Ok(result);
        }

        /// <summary>
        /// Saves the Permissions from the Admin page to the User Permission xref table
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route("api/Admin/SaveUserPermissions")]
        public IHttpActionResult SaveUserPermissions(UserPermissionsSaveModel model)
        {
            IPermissionAdapter thisengine = new PermissionAdapter();
            var result = thisengine.SaveUserPermissions(model);
            return Ok(result);
        }

        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route("api/Admin/SendInactivePlanReviewerEmail")]
        public IHttpActionResult SendInactivePlanReviewerEmail(int userId)
        {
            IAdminAdapter thisengine = new AdminAdapter();
            var result = thisengine.SendInactivePlanReviewerEmail(userId);
            return Ok(result);
        }

        /// <summary>
        /// Admin Misc Config -> Facilitator Assignment Designation
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route("api/Admin/UpdateRoleOptions")]
        public IHttpActionResult UpdateRoleOptions(SystemRoleOptionsManagerModel item)
        {
            IAdminAdapter thisengine = new AdminAdapter();
            var result = thisengine.UpdateRoleOptions(item.RoleId, item.OldRoleOptionsString, item.NewRoleOptionsString);
            return Ok(result);
        }

        /// <summary>
        /// Admin Message Configuration --> MessageTemplate by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(MessageTemplate))]
        [Route("api/Admin/GetMessageTemplateById")]
        public IHttpActionResult GetMessageTemplateById(int id)
        {
            IAdminAdapter thisengine = new AdminAdapter();
            var result = thisengine.GetMessageTemplateById(id);
            return Ok(result);
        }

        /// <summary>
        /// Admin Message Configuration --> Message Template Types List
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(List<MessageTemplateType>))]
        [Route("api/Admin/GetMessageTemplateTypes")]
        public IHttpActionResult GetMessageTemplateTypes()
        {
            IAdminAdapter thisengine = new AdminAdapter();
            var result = thisengine.GetMessageTemplateTypes();
            return Ok(result);
        }

        /// <summary>
        /// Admin Message Configuration --> Message Template List
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(List<MessageTemplate>))]
        [Route("api/Admin/GetMessageTemplatesByTypeId")]
        public IHttpActionResult GetMessageTemplatesByTypeId(int id)
        {
            IAdminAdapter thisengine = new AdminAdapter();
            var result = thisengine.GetMessageTemplatesByTypeId(id);
            return Ok(result);
        }

        /// <summary>
        /// Admin Message Configuration --> Message Template Data Elements List
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ResponseType(typeof(List<MessageTemplateDataElement>))]
        [Route("api/Admin/GetMessageTemplateDataElements")]
        public IHttpActionResult GetMessageTemplateDataElements()
        {
            IAdminAdapter thisengine = new AdminAdapter();
            var result = thisengine.GetMessageTemplateDataElements();
            return Ok(result);
        }

        /// <summary>
        /// Admin Message Configuration -> Save new template
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route("api/Admin/InsertMessageTemplate")]
        public IHttpActionResult InsertMessageTemplate(MessageTemplate item)
        {
            IAdminAdapter thisengine = new AdminAdapter();
            var result = thisengine.InsertMessageTemplate(item);
            return Ok(result);
        }

        /// <summary>
        /// Admin Message Configuration -> Update existing template
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route("api/Admin/UpdateMessageTemplate")]
        public IHttpActionResult UpdateMessageTemplate(MessageTemplate item)
        {
            IAdminAdapter thisengine = new AdminAdapter();
            var result = thisengine.UpdateMessageTemplate(item);
            return Ok(result);
        }

        [HttpGet]
        [ResponseType(typeof(List<ProjectType>))]
        [Route("api/Admin/GetProjectTypeList")]
        public IHttpActionResult GetProjectTypeList()
        {
            ProjectTypeAdapter thisengine = new ProjectTypeAdapter();
            var result = thisengine.GetProjectTypeList();
            return Ok(result);
        }

        [HttpGet]
        [ResponseType(typeof(bool))]
        [Route("api/Admin/UpdateAutoAssignFacilitator")]
        public IHttpActionResult UpdateAutoAssignFacilitator(string projectTypeRefIdCsvList, bool autoAssignFacilitator, string wkrId)
        {
            ProjectTypeAdapter thisengine = new ProjectTypeAdapter();
            var result = thisengine.UpdateAutoAssignFacilitator(projectTypeRefIdCsvList, autoAssignFacilitator, wkrId);
            return Ok(result);
        }


    }
}