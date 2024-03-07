using AccelaRecords.Model;
using Meck.Shared;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using DocumentModelCategory = AccelaDocuments.Model.DocumentModelCategory;

namespace AION.Accela.Engine.BusinessEntities.AccelaResponseBE
{
    public class RecordWrapperBE
    {
        [JsonProperty("status")] public int StatusCode { get; set; }

        [JsonProperty("result")] public List<RecordBE> Records { get; set; }
        public List<RecordDocumentBE> RecordDocuments { get; set; }

        public RecordApoCustomFormsModelBE RecordAPOFormBE { get; set; }

        public SimpleRecordModelBE recorddetail { get; set; }

        public List<SimpleRecordModelBE> SimpleRecModels { get; set; }

        public RecordWrapperBE()
        {

            RecordDocuments = new List<RecordDocumentBE>();
            SimpleRecModels = new List<SimpleRecordModelBE>();
        }
    }

    public class RecordTypeBE
    {
        [JsonProperty("module")] public string Module { get; set; }
        [JsonProperty("value")] public string Value { get; set; }
        [JsonProperty("type")] public string Type { get; set; }
        [JsonProperty("category")] public string Category { get; set; }
        [JsonProperty("text")] public string Text { get; set; }
        [JsonProperty("subType")] public string SubType { get; set; }
        [JsonProperty("group")] public string Group { get; set; }
        [JsonProperty("alias")] public string Alias { get; set; }
        [JsonProperty("id")] public string Id { get; set; }

    }

    public class ReportedChannelBE
    {
        [JsonProperty("value")] public string Value { get; set; }
        [JsonProperty("text")] public string Text { get; set; }
    }

    public class RecordBE
    {
        [JsonProperty("name")] public string Name { get; set; }
        [JsonProperty("id")] public string Id { get; set; }
        [JsonProperty("type")] public RecordTypeBE RecordType { get; set; }
        [JsonProperty("description")] public string Description { get; set; }
        [JsonProperty("module")] public string Module { get; set; }
        [JsonProperty("updateDate")] public string UpdateDate { get; set; }
        [JsonProperty("openedDate")] public string OpenedDate { get; set; }
        [JsonProperty("customId")] public string CustomId { get; set; }
        [JsonProperty("trackingId")] public long TrackingId { get; set; }
        [JsonProperty("jobValue")] public double JobValue { get; set; }
        [JsonProperty("totalJobCost")] public double TotalJobCost { get; set; }
        [JsonProperty("serviceProviderCode")] public string ServiceProviderCode { get; set; }
        [JsonProperty("statusDate")] public string StatusDate { get; set; }
        [JsonProperty("createdBy")] public string CreatedBy { get; set; }
        [JsonProperty("reportedDate")] public string ReportedDate { get; set; }
        [JsonProperty("recordClass")] public string RecordClass { get; set; }
        [JsonProperty("initiatedProduct")] public string InitiatedProduct { get; set; }
        [JsonProperty("reportedChannel")] public ReportedChannelBE ReportedChannel { get; set; }
        [JsonProperty("undistributedCost")] public double UndistributedCost { get; set; }

        [JsonProperty("estimatedProductionUnit")]
        public double EstimatedProductionUnit { get; set; }

        [JsonProperty("actualProductionUnit")] public double ActualProductionUnit { get; set; }
        [JsonProperty("value")] public string Value { get; set; }
        [JsonProperty("totalFee")] public double TotalFee { get; set; }
        [JsonProperty("totalPay")] public double TotalPay { get; set; }
        [JsonProperty("balance")] public double Balance { get; set; }
        [JsonProperty("booking")] public bool Booking { get; set; }
        [JsonProperty("infraction")] public bool Infraction { get; set; }
        [JsonProperty("misdemeanor")] public bool Misdemeanor { get; set; }
        [JsonProperty("offenseWitnessed")] public bool OffenseWitnessed { get; set; }
        [JsonProperty("defendantSignature")] public bool DefendantSignature { get; set; }
        [JsonProperty("publicOwned")] public bool PublicOwned { get; set; }
        [JsonProperty("constructionType")] public string ConstructionType { get; set; }
        [JsonProperty("occupancyType")] public string OccupancyType { get; set; }
        [JsonProperty("sqrFt")] public int SqrFt { get; set; }
        [JsonProperty("costOfConstruction")] public double CostOfConstruction { get; set; }
        [JsonProperty("numberofSheets")] public int NumberofSheets { get; set; }

        [JsonProperty("isPreliminaryMtgRequested")] public bool isPreliminaryMtgRequested { get; set; }
        [JsonProperty("isPreliminaryMtgComplete")] public bool IsPreliminaryMtgComplete { get; set; }

        [JsonProperty("AccelaPreliminaryProjectRefId")]
        public string AccelaPreliminaryProjectRefId { get; set; }

        [JsonProperty("isRTAP")] public bool IsRTAP { get; set; }

        [JsonProperty("AccelaRTAPProjectRefId")]
        public string AccelaRTAPProjectRefId { get; set; }

        [JsonProperty("accelaProjectDisplayInfo")]
        public AccelaProjectDisplayInfo DisplayInformation { get; set; }

        [JsonProperty("projectAgencyList")] public List<AgencyInfo> ProjectAgencyList { get; set; }
        [JsonProperty("projectStatusCodeRef")] public string ProjectStatusCodeRef { get; set; }

        [JsonProperty("requestedReviewerName")]
        public string RequestedReviewerName { get; set; }

        [JsonProperty("ProjectRegion")] public string ProjectRegion { get; set; }
        [JsonProperty("Addresses")] public AddressWrapperBE Addresses { get; set; }
        [JsonProperty("Professionals")] public List<ProfessionalBE> Professionals { get; set; }
        [JsonProperty("Contacts")] public ContactWrapperBE Contacts { get; set; }
        [JsonProperty("projectTradesList")] public List<TradeInfo> ProjectTradesList { get; set; }

    }

    #region RecordDocumentBE

    public partial class RecordDocumentBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccelaDocuments.Model.DocumentModel" /> class.
        /// </summary>
        /// <param name="category">category.</param>
        /// <param name="deletable">deletable.</param>
        /// <param name="department">The name of the department the document belongs to..</param>
        /// <param name="description">The document description..</param>
        /// <param name="downloadable">downloadable.</param>
        /// <param name="entityId">The unique ID of the entity or record..</param>
        /// <param name="entityType">The type of entity..</param>
        /// <param name="fileName">The name of the file as it displays in the source location..</param>
        /// <param name="group">group.</param>
        /// <param name="id">The document id..</param>
        /// <param name="modifiedBy">The user account that last modified the document..</param>
        /// <param name="modifiedDate">The date the document was last modified..</param>
        /// <param name="serviceProviderCode">The unique agency identifier..</param>
        /// <param name="size">The file size of the document..</param>
        /// <param name="source">The name for your agency&#39;s electronic document management system..</param>
        /// <param name="status">status.</param>
        /// <param name="statusDate">The date when the current status changed..</param>
        /// <param name="titleViewable">titleViewable.</param>
        /// <param name="type">The document type..</param>
        /// <param name="uploadedBy">The user who uploaded the document to the record..</param>
        /// <param name="uploadedDate">The date when the document was uploaded..</param>
        /// <param name="virtualFolders">This is the virtual folder for storing the attachment. With virtual folders you can organize uploaded attachments in groups.</param>
        ///
        public RecordDocumentBE()
        {

        }


        /// <summary>
        /// RecordDocumentBE
        /// </summary>
        /// <param name="category"></param>
        /// <param name="deletable"></param>
        /// <param name="department"></param>
        /// <param name="description"></param>
        /// <param name="downloadable"></param>
        /// <param name="entityId"></param>
        /// <param name="entityType"></param>
        /// <param name="fileName"></param>
        /// <param name="group"></param>
        /// <param name="id"></param>
        /// <param name="modifiedBy"></param>
        /// <param name="modifiedDate"></param>
        /// <param name="serviceProviderCode"></param>
        /// <param name="size"></param>
        /// <param name="source"></param>
        /// <param name="status"></param>
        /// <param name="statusDate"></param>
        /// <param name="titleViewable"></param>
        /// <param name="type"></param>
        /// <param name="uploadedBy"></param>
        /// <param name="uploadedDate"></param>
        /// <param name="virtualFolders"></param>
        public RecordDocumentBE(DocumentModelCategory category = default(DocumentModelCategory),
            UserRolePrivilegeModel deletable = default(UserRolePrivilegeModel), string department = default(string),
            string description = default(string), UserRolePrivilegeModel downloadable = default(UserRolePrivilegeModel),
            string entityId = default(string), string entityType = default(string), string fileName = default(string),
            DocumentModelGroup group = default(DocumentModelGroup), long? id = default(long?),
            string modifiedBy = default(string), DateTime? modifiedDate = default(DateTime?),
            string serviceProviderCode = default(string), double? size = default(double?),
            string source = default(string), DocumentModelStatus status = default(DocumentModelStatus),
            DateTime? statusDate = default(DateTime?),
            UserRolePrivilegeModel titleViewable = default(UserRolePrivilegeModel), string type = default(string),
            string uploadedBy = default(string), DateTime? uploadedDate = default(DateTime?),
            string virtualFolders = default(string))
        {
            this.Category = category;
            this.Deletable = deletable;
            this.Department = department;
            this.Description = description;
            this.Downloadable = downloadable;
            this.EntityId = entityId;
            this.EntityType = entityType;
            this.FileName = fileName;
            this.Group = group;
            this.Id = id;
            this.ModifiedBy = modifiedBy;
            this.ModifiedDate = modifiedDate;
            this.ServiceProviderCode = serviceProviderCode;
            this.Size = size;
            this.Source = source;
            this.Status = status;
            this.StatusDate = statusDate;
            this.TitleViewable = titleViewable;
            this.Type = type;
            this.UploadedBy = uploadedBy;
            this.UploadedDate = uploadedDate;
            this.VirtualFolders = virtualFolders;
        }

        /// <summary>
        /// Gets or Sets Category
        /// </summary>
        [DataMember(Name = "category", EmitDefaultValue = false)]
        public DocumentModelCategory Category { get; set; }

        /// <summary>
        /// Gets or Sets Deletable
        /// </summary>
        [DataMember(Name = "deletable", EmitDefaultValue = false)]
        public UserRolePrivilegeModel Deletable { get; set; }

        /// <summary>
        /// The name of the department the document belongs to.
        /// </summary>
        /// <value>The name of the department the document belongs to.</value>
        [DataMember(Name = "department", EmitDefaultValue = false)]
        public string Department { get; set; }

        /// <summary>
        /// The document description.
        /// </summary>
        /// <value>The document description.</value>
        [DataMember(Name = "description", EmitDefaultValue = false)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or Sets Downloadable
        /// </summary>
        [DataMember(Name = "downloadable", EmitDefaultValue = false)]
        public UserRolePrivilegeModel Downloadable { get; set; }

        /// <summary>
        /// The unique ID of the entity or record.
        /// </summary>
        /// <value>The unique ID of the entity or record.</value>
        [DataMember(Name = "entityId", EmitDefaultValue = false)]
        public string EntityId { get; set; }

        /// <summary>
        /// The type of entity.
        /// </summary>
        /// <value>The type of entity.</value>
        [DataMember(Name = "entityType", EmitDefaultValue = false)]
        public string EntityType { get; set; }

        /// <summary>
        /// The name of the file as it displays in the source location.
        /// </summary>
        /// <value>The name of the file as it displays in the source location.</value>
        [DataMember(Name = "fileName", EmitDefaultValue = false)]
        public string FileName { get; set; }

        /// <summary>
        /// Gets or Sets Group
        /// </summary>
        [DataMember(Name = "group", EmitDefaultValue = false)]
        public DocumentModelGroup Group { get; set; }

        /// <summary>
        /// The document id.
        /// </summary>
        /// <value>The document id.</value>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public long? Id { get; set; }

        /// <summary>
        /// The user account that last modified the document.
        /// </summary>
        /// <value>The user account that last modified the document.</value>
        [DataMember(Name = "modifiedBy", EmitDefaultValue = false)]
        public string ModifiedBy { get; set; }

        /// <summary>
        /// The date the document was last modified.
        /// </summary>
        /// <value>The date the document was last modified.</value>
        [DataMember(Name = "modifiedDate", EmitDefaultValue = false)]
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// The unique agency identifier.
        /// </summary>
        /// <value>The unique agency identifier.</value>
        [DataMember(Name = "serviceProviderCode", EmitDefaultValue = false)]
        public string ServiceProviderCode { get; set; }

        /// <summary>
        /// The file size of the document.
        /// </summary>
        /// <value>The file size of the document.</value>
        [DataMember(Name = "size", EmitDefaultValue = false)]
        public double? Size { get; set; }

        /// <summary>
        /// The name for your agency&#39;s electronic document management system.
        /// </summary>
        /// <value>The name for your agency&#39;s electronic document management system.</value>
        [DataMember(Name = "source", EmitDefaultValue = false)]
        public string Source { get; set; }

        /// <summary>
        /// Gets or Sets Status
        /// </summary>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public DocumentModelStatus Status { get; set; }

        /// <summary>
        /// The date when the current status changed.
        /// </summary>
        /// <value>The date when the current status changed.</value>
        [DataMember(Name = "statusDate", EmitDefaultValue = false)]
        public DateTime? StatusDate { get; set; }

        /// <summary>
        /// Gets or Sets TitleViewable
        /// </summary>
        [DataMember(Name = "titleViewable", EmitDefaultValue = false)]
        public UserRolePrivilegeModel TitleViewable { get; set; }

        /// <summary>
        /// The document type.
        /// </summary>
        /// <value>The document type.</value>
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string Type { get; set; }

        /// <summary>
        /// The user who uploaded the document to the record.
        /// </summary>
        /// <value>The user who uploaded the document to the record.</value>
        [DataMember(Name = "uploadedBy", EmitDefaultValue = false)]
        public string UploadedBy { get; set; }

        /// <summary>
        /// The date when the document was uploaded.
        /// </summary>
        /// <value>The date when the document was uploaded.</value>
        [DataMember(Name = "uploadedDate", EmitDefaultValue = false)]
        public DateTime? UploadedDate { get; set; }

        /// <summary>
        /// This is the virtual folder for storing the attachment. With virtual folders you can organize uploaded attachments in groups
        /// </summary>
        /// <value>This is the virtual folder for storing the attachment. With virtual folders you can organize uploaded attachments in groups</value>
        [DataMember(Name = "virtualFolders", EmitDefaultValue = false)]
        public string VirtualFolders { get; set; }
    }
    #endregion

    #region SimpleRecordModel

    /// <summary>
    /// SimpleRecordModel
    /// </summary>
    [DataContract]
    public partial class SimpleRecordModelBE
    {
        /// <summary>
        /// Indictes whether or not the record was cloned.
        /// </summary>
        /// <value>Indictes whether or not the record was cloned.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum CreatedByCloningEnum
        {

            /// <summary>
            /// Enum Y for value: Y
            /// </summary>
            [EnumMember(Value = "Y")] Y = 1,

            /// <summary>
            /// Enum N for value: N
            /// </summary>
            [EnumMember(Value = "N")] N = 2
        }

        public SimpleRecordModelBE()
        {

        }

        // <summary>
        /// Indictes whether or not the record was cloned.
        /// </summary>
        /// <value>Indictes whether or not the record was cloned.</value>
        [DataMember(Name = "createdByCloning", EmitDefaultValue = false)]
        public AccelaRecords.Model.SimpleRecordModel.CreatedByCloningEnum? CreatedByCloning { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AccelaRecords.Model.SimpleRecordModel" /> class.
        /// </summary>
        /// <param name="actualProductionUnit">Estimated cost per production unit..</param>
        /// <param name="appearanceDate">The date for a hearing appearance..</param>
        /// <param name="appearanceDayOfWeek">The day for a hearing appearance..</param>
        /// <param name="assignedDate">The date the application was assigned..</param>
        /// <param name="assignedToDepartment">The department responsible for the action. See [Get All Departments](./api-settings.html#operation/v4.get.settings.departments)..</param>
        /// <param name="assignedUser">The staff member responsible for the action..</param>
        /// <param name="balance">The amount due..</param>
        /// <param name="booking">Indicates whether or not there was a booking in addition to a citation..</param>
        /// <param name="closedByDepartment">The department responsible for closing the record. See [Get All Departments](./api-settings.html#operation/v4.get.settings.departments)..</param>
        /// <param name="closedByUser">The staff member responsible for closure..</param>
        /// <param name="closedDate">The date the application was closed..</param>
        /// <param name="completeDate">The date the application was completed..</param>
        /// <param name="completedByDepartment">The department responsible for completion. See [Get All Departments](./api-settings.html#operation/v4.get.settings.departments)..</param>
        /// <param name="completedByUser">The staff member responsible for completion..</param>
        /// <param name="constructionType">constructionType.</param>
        /// <param name="costPerUnit">The cost for one unit associated to the record..</param>
        /// <param name="createdBy">The unique user id of the individual that created the entry..</param>
        /// <param name="createdByCloning">Indictes whether or not the record was cloned..</param>
        /// <param name="customId">An ID based on a different numbering convention from the numbering convention used by the record ID (xxxxx-xx-xxxxx). Civic Platform auto-generates and applies an alternate ID value when you submit a new application..</param>
        /// <param name="defendantSignature">Indicates whether or not a defendant&#39;s signature has been obtained..</param>
        /// <param name="description">The description of the record or item..</param>
        /// <param name="enforceDepartment">The name of the department responsible for enforcement. See [Get All Departments](./api-settings.html#operation/v4.get.settings.departments)..</param>
        /// <param name="enforceUser">Name of the enforcement officer..</param>
        /// <param name="enforceUserId">ID number of the enforcement officer..</param>
        /// <param name="estimatedCostPerUnit">The estimated cost per unit..</param>
        /// <param name="estimatedDueDate">The estimated date of completion..</param>
        /// <param name="estimatedProductionUnit">The estimated number of production units..</param>
        /// <param name="estimatedTotalJobCost">The estimated cost of the job..</param>
        /// <param name="firstIssuedDate">The first issued date for license.</param>
        /// <param name="housingUnits">The number of housing units..</param>
        /// <param name="id">The record system id assigned by the Civic Platform server..</param>
        /// <param name="inPossessionTime">The application level in possession time of the time tracking feature..</param>
        /// <param name="infraction">Indicates whether or not an infraction occurred..</param>
        /// <param name="initiatedProduct">The Civic Platform product  where the application is submitted: \&quot;AA\&quot; : Classic Accela Automation. \&quot;ACA\&quot; : Accela Citizen Access. \&quot;AIVR\&quot; : Accela Integrated Voice Response. \&quot;AMO\&quot; : Accela Mobile Office. \&quot;AV360\&quot; : Accela Asset Management, Accela Land Management..</param>
        /// <param name="inspectorDepartment">The name of the department where the inspector works. See [Get All Departments](./api-settings.html#operation/v4.get.settings.departments)..</param>
        /// <param name="inspectorId">The ID number of the inspector. See [Get All Inspectors](./api-inspections.html#operation/v4.get.inspectors)..</param>
        /// <param name="inspectorName">The name of the inspector. See [Get All Inspectors](./api-inspections.html#operation/v4.get.inspectors)..</param>
        /// <param name="jobValue">The value of the job..</param>
        /// <param name="misdemeanor">Indicates whether or not a misdemeanor occurred..</param>
        /// <param name="module">The module the record belongs to. See [Get All Modules](./api-settings.html#operation/v4.get.settings.modules)..</param>
        /// <param name="name">The name associated to the record..</param>
        /// <param name="numberOfBuildings">The number of buildings..</param>
        /// <param name="offenseWitnessed">Indicates whether or not  there was a witness to the alleged offense..</param>
        /// <param name="openedDate">The date the application was opened..</param>
        /// <param name="overallApplicationTime">The amount of elapsed time from the time tracking start date to the completion of the application..</param>
        /// <param name="priority">priority.</param>
        /// <param name="publicOwned">Indicates whether or not the record is for the public..</param>
        /// <param name="recordClass">General information about the record..</param>
        /// <param name="renewalInfo">renewalInfo.</param>
        /// <param name="reportedChannel">reportedChannel.</param>
        /// <param name="reportedDate">The date the complaint was reported..</param>
        /// <param name="reportedType">reportedType.</param>
        /// <param name="scheduledDate">The date when the inspection gets scheduled..</param>
        /// <param name="serviceProviderCode">The unique agency identifier,.</param>
        /// <param name="severity">severity.</param>
        /// <param name="shortNotes">A brief note about the record subject..</param>
        /// <param name="status">status.</param>
        /// <param name="statusDate">The date when the current status changed. .</param>
        /// <param name="statusReason">statusReason.</param>
        /// <param name="totalFee">The total amount of the fees invoiced to the record..</param>
        /// <param name="totalJobCost">The combination of work order assignments (labor) and costs..</param>
        /// <param name="totalPay">The total amount of pay..</param>
        /// <param name="trackingId">The application tracking number (IVR tracking number)..</param>
        /// <param name="type">type.</param>
        /// <param name="undistributedCost">The undistributed costs for this work order..</param>
        /// <param name="value">The record value..</param>
        public SimpleRecordModelBE(double? actualProductionUnit = default(double?),
            DateTime? appearanceDate = default(DateTime?), string appearanceDayOfWeek = default(string),
            DateTime? assignedDate = default(DateTime?), string assignedToDepartment = default(string),
            string assignedUser = default(string), double? balance = default(double?), bool? booking = default(bool?),
            string closedByDepartment = default(string), string closedByUser = default(string),
            DateTime? closedDate = default(DateTime?), DateTime? completeDate = default(DateTime?),
            string completedByDepartment = default(string), string completedByUser = default(string),
            RecordAPOCustomFormsModelConstructionType constructionType =
                default(RecordAPOCustomFormsModelConstructionType), double? costPerUnit = default(double?),
            string createdBy = default(string),
            AccelaRecords.Model.SimpleRecordModel.CreatedByCloningEnum? createdByCloning =
                default(AccelaRecords.Model.SimpleRecordModel.CreatedByCloningEnum?), string customId = default(string),
            bool? defendantSignature = default(bool?), string description = default(string),
            string enforceDepartment = default(string), string enforceUser = default(string),
            string enforceUserId = default(string), double? estimatedCostPerUnit = default(double?),
            DateTime? estimatedDueDate = default(DateTime?), double? estimatedProductionUnit = default(double?),
            double? estimatedTotalJobCost = default(double?), DateTime? firstIssuedDate = default(DateTime?),
            long? housingUnits = default(long?), string id = default(string),
            double? inPossessionTime = default(double?), bool? infraction = default(bool?),
            string initiatedProduct = default(string), string inspectorDepartment = default(string),
            string inspectorId = default(string), string inspectorName = default(string),
            double? jobValue = default(double?), bool? misdemeanor = default(bool?), string module = default(string),
            string name = default(string), long? numberOfBuildings = default(long?),
            bool? offenseWitnessed = default(bool?), DateTime? openedDate = default(DateTime?),
            double? overallApplicationTime = default(double?),
            RecordAPOCustomFormsModelPriority priority = default(RecordAPOCustomFormsModelPriority),
            bool? publicOwned = default(bool?), string recordClass = default(string),
            AccelaRecords.Model.RecordExpirationModel renewalInfo = default(AccelaRecords.Model.RecordExpirationModel),
            RecordAPOCustomFormsModelReportedChannel reportedChannel =
                default(RecordAPOCustomFormsModelReportedChannel), DateTime? reportedDate = default(DateTime?),
            RecordAPOCustomFormsModelReportedType reportedType = default(RecordAPOCustomFormsModelReportedType),
            DateTime? scheduledDate = default(DateTime?), string serviceProviderCode = default(string),
            RecordAPOCustomFormsModelSeverity severity = default(RecordAPOCustomFormsModelSeverity),
            string shortNotes = default(string),
            RecordAPOCustomFormsModelStatus status = default(RecordAPOCustomFormsModelStatus),
            DateTime? statusDate = default(DateTime?),
            RecordAPOCustomFormsModelStatusReason statusReason = default(RecordAPOCustomFormsModelStatusReason),
            double? totalFee = default(double?), double? totalJobCost = default(double?),
            double? totalPay = default(double?), long? trackingId = default(long?),
            AccelaRecords.Model.RecordTypeModel type = default(AccelaRecords.Model.RecordTypeModel),
            double? undistributedCost = default(double?), string value = default(string))
        {
            this.ActualProductionUnit = actualProductionUnit;
            this.AppearanceDate = appearanceDate;
            this.AppearanceDayOfWeek = appearanceDayOfWeek;
            this.AssignedDate = assignedDate;
            this.AssignedToDepartment = assignedToDepartment;
            this.AssignedUser = assignedUser;
            this.Balance = balance;
            this.Booking = booking;
            this.ClosedByDepartment = closedByDepartment;
            this.ClosedByUser = closedByUser;
            this.ClosedDate = closedDate;
            this.CompleteDate = completeDate;
            this.CompletedByDepartment = completedByDepartment;
            this.CompletedByUser = completedByUser;
            this.ConstructionType = constructionType;
            this.CostPerUnit = costPerUnit;
            this.CreatedBy = createdBy;
            this.CreatedByCloning = createdByCloning;
            this.CustomId = customId;
            this.DefendantSignature = defendantSignature;
            this.Description = description;
            this.EnforceDepartment = enforceDepartment;
            this.EnforceUser = enforceUser;
            this.EnforceUserId = enforceUserId;
            this.EstimatedCostPerUnit = estimatedCostPerUnit;
            this.EstimatedDueDate = estimatedDueDate;
            this.EstimatedProductionUnit = estimatedProductionUnit;
            this.EstimatedTotalJobCost = estimatedTotalJobCost;
            this.FirstIssuedDate = firstIssuedDate;
            this.HousingUnits = housingUnits;
            this.Id = id;
            this.InPossessionTime = inPossessionTime;
            this.Infraction = infraction;
            this.InitiatedProduct = initiatedProduct;
            this.InspectorDepartment = inspectorDepartment;
            this.InspectorId = inspectorId;
            this.InspectorName = inspectorName;
            this.JobValue = jobValue;
            this.Misdemeanor = misdemeanor;
            this.Module = module;
            this.Name = name;
            this.NumberOfBuildings = numberOfBuildings;
            this.OffenseWitnessed = offenseWitnessed;
            this.OpenedDate = openedDate;
            this.OverallApplicationTime = overallApplicationTime;
            this.Priority = priority;
            this.PublicOwned = publicOwned;
            this.RecordClass = recordClass;
            this.RenewalInfo = renewalInfo;
            this.ReportedChannel = reportedChannel;
            this.ReportedDate = reportedDate;
            this.ReportedType = reportedType;
            this.ScheduledDate = scheduledDate;
            this.ServiceProviderCode = serviceProviderCode;
            this.Severity = severity;
            this.ShortNotes = shortNotes;
            this.Status = status;
            this.StatusDate = statusDate;
            this.StatusReason = statusReason;
            this.TotalFee = totalFee;
            this.TotalJobCost = totalJobCost;
            this.TotalPay = totalPay;
            this.TrackingId = trackingId;
            this.Type = type;
            this.UndistributedCost = undistributedCost;
            this.Value = value;
        }

        /// <summary>
        /// Estimated cost per production unit.
        /// </summary>
        /// <value>Estimated cost per production unit.</value>
        [DataMember(Name = "actualProductionUnit", EmitDefaultValue = false)]
        public double? ActualProductionUnit { get; set; }

        /// <summary>
        /// The date for a hearing appearance.
        /// </summary>
        /// <value>The date for a hearing appearance.</value>
        [DataMember(Name = "appearanceDate", EmitDefaultValue = false)]
        public DateTime? AppearanceDate { get; set; }

        /// <summary>
        /// The day for a hearing appearance.
        /// </summary>
        /// <value>The day for a hearing appearance.</value>
        [DataMember(Name = "appearanceDayOfWeek", EmitDefaultValue = false)]
        public string AppearanceDayOfWeek { get; set; }

        /// <summary>
        /// The date the application was assigned.
        /// </summary>
        /// <value>The date the application was assigned.</value>
        [DataMember(Name = "assignedDate", EmitDefaultValue = false)]
        public DateTime? AssignedDate { get; set; }

        /// <summary>
        /// The department responsible for the action. See [Get All Departments](./api-settings.html#operation/v4.get.settings.departments).
        /// </summary>
        /// <value>The department responsible for the action. See [Get All Departments](./api-settings.html#operation/v4.get.settings.departments).</value>
        [DataMember(Name = "assignedToDepartment", EmitDefaultValue = false)]
        public string AssignedToDepartment { get; set; }

        /// <summary>
        /// The staff member responsible for the action.
        /// </summary>
        /// <value>The staff member responsible for the action.</value>
        [DataMember(Name = "assignedUser", EmitDefaultValue = false)]
        public string AssignedUser { get; set; }

        /// <summary>
        /// The amount due.
        /// </summary>
        /// <value>The amount due.</value>
        [DataMember(Name = "balance", EmitDefaultValue = false)]
        public double? Balance { get; set; }

        /// <summary>
        /// Indicates whether or not there was a booking in addition to a citation.
        /// </summary>
        /// <value>Indicates whether or not there was a booking in addition to a citation.</value>
        [DataMember(Name = "booking", EmitDefaultValue = false)]
        public bool? Booking { get; set; }

        /// <summary>
        /// The department responsible for closing the record. See [Get All Departments](./api-settings.html#operation/v4.get.settings.departments).
        /// </summary>
        /// <value>The department responsible for closing the record. See [Get All Departments](./api-settings.html#operation/v4.get.settings.departments).</value>
        [DataMember(Name = "closedByDepartment", EmitDefaultValue = false)]
        public string ClosedByDepartment { get; set; }

        /// <summary>
        /// The staff member responsible for closure.
        /// </summary>
        /// <value>The staff member responsible for closure.</value>
        [DataMember(Name = "closedByUser", EmitDefaultValue = false)]
        public string ClosedByUser { get; set; }

        /// <summary>
        /// The date the application was closed.
        /// </summary>
        /// <value>The date the application was closed.</value>
        [DataMember(Name = "closedDate", EmitDefaultValue = false)]
        public DateTime? ClosedDate { get; set; }

        /// <summary>
        /// The date the application was completed.
        /// </summary>
        /// <value>The date the application was completed.</value>
        [DataMember(Name = "completeDate", EmitDefaultValue = false)]
        public DateTime? CompleteDate { get; set; }

        /// <summary>
        /// The department responsible for completion. See [Get All Departments](./api-settings.html#operation/v4.get.settings.departments).
        /// </summary>
        /// <value>The department responsible for completion. See [Get All Departments](./api-settings.html#operation/v4.get.settings.departments).</value>
        [DataMember(Name = "completedByDepartment", EmitDefaultValue = false)]
        public string CompletedByDepartment { get; set; }

        /// <summary>
        /// The staff member responsible for completion.
        /// </summary>
        /// <value>The staff member responsible for completion.</value>
        [DataMember(Name = "completedByUser", EmitDefaultValue = false)]
        public string CompletedByUser { get; set; }

        /// <summary>
        /// Gets or Sets ConstructionType
        /// </summary>
        [DataMember(Name = "constructionType", EmitDefaultValue = false)]
        public RecordAPOCustomFormsModelConstructionType ConstructionType { get; set; }

        /// <summary>
        /// The cost for one unit associated to the record.
        /// </summary>
        /// <value>The cost for one unit associated to the record.</value>
        [DataMember(Name = "costPerUnit", EmitDefaultValue = false)]
        public double? CostPerUnit { get; set; }

        /// <summary>
        /// The unique user id of the individual that created the entry.
        /// </summary>
        /// <value>The unique user id of the individual that created the entry.</value>
        [DataMember(Name = "createdBy", EmitDefaultValue = false)]
        public string CreatedBy { get; set; }


        /// <summary>
        /// An ID based on a different numbering convention from the numbering convention used by the record ID (xxxxx-xx-xxxxx). Civic Platform auto-generates and applies an alternate ID value when you submit a new application.
        /// </summary>
        /// <value>An ID based on a different numbering convention from the numbering convention used by the record ID (xxxxx-xx-xxxxx). Civic Platform auto-generates and applies an alternate ID value when you submit a new application.</value>
        [DataMember(Name = "customId", EmitDefaultValue = false)]
        public string CustomId { get; set; }

        /// <summary>
        /// Indicates whether or not a defendant&#39;s signature has been obtained.
        /// </summary>
        /// <value>Indicates whether or not a defendant&#39;s signature has been obtained.</value>
        [DataMember(Name = "defendantSignature", EmitDefaultValue = false)]
        public bool? DefendantSignature { get; set; }

        /// <summary>
        /// The description of the record or item.
        /// </summary>
        /// <value>The description of the record or item.</value>
        [DataMember(Name = "description", EmitDefaultValue = false)]
        public string Description { get; set; }

        /// <summary>
        /// The name of the department responsible for enforcement. See [Get All Departments](./api-settings.html#operation/v4.get.settings.departments).
        /// </summary>
        /// <value>The name of the department responsible for enforcement. See [Get All Departments](./api-settings.html#operation/v4.get.settings.departments).</value>
        [DataMember(Name = "enforceDepartment", EmitDefaultValue = false)]
        public string EnforceDepartment { get; set; }

        /// <summary>
        /// Name of the enforcement officer.
        /// </summary>
        /// <value>Name of the enforcement officer.</value>
        [DataMember(Name = "enforceUser", EmitDefaultValue = false)]
        public string EnforceUser { get; set; }

        /// <summary>
        /// ID number of the enforcement officer.
        /// </summary>
        /// <value>ID number of the enforcement officer.</value>
        [DataMember(Name = "enforceUserId", EmitDefaultValue = false)]
        public string EnforceUserId { get; set; }

        /// <summary>
        /// The estimated cost per unit.
        /// </summary>
        /// <value>The estimated cost per unit.</value>
        [DataMember(Name = "estimatedCostPerUnit", EmitDefaultValue = false)]
        public double? EstimatedCostPerUnit { get; set; }

        /// <summary>
        /// The estimated date of completion.
        /// </summary>
        /// <value>The estimated date of completion.</value>
        [DataMember(Name = "estimatedDueDate", EmitDefaultValue = false)]
        public DateTime? EstimatedDueDate { get; set; }

        /// <summary>
        /// The estimated number of production units.
        /// </summary>
        /// <value>The estimated number of production units.</value>
        [DataMember(Name = "estimatedProductionUnit", EmitDefaultValue = false)]
        public double? EstimatedProductionUnit { get; set; }

        /// <summary>
        /// The estimated cost of the job.
        /// </summary>
        /// <value>The estimated cost of the job.</value>
        [DataMember(Name = "estimatedTotalJobCost", EmitDefaultValue = false)]
        public double? EstimatedTotalJobCost { get; set; }

        /// <summary>
        /// The first issued date for license
        /// </summary>
        /// <value>The first issued date for license</value>
        [DataMember(Name = "firstIssuedDate", EmitDefaultValue = false)]
        public DateTime? FirstIssuedDate { get; set; }

        /// <summary>
        /// The number of housing units.
        /// </summary>
        /// <value>The number of housing units.</value>
        [DataMember(Name = "housingUnits", EmitDefaultValue = false)]
        public long? HousingUnits { get; set; }

        /// <summary>
        /// The record system id assigned by the Civic Platform server.
        /// </summary>
        /// <value>The record system id assigned by the Civic Platform server.</value>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }

        /// <summary>
        /// The application level in possession time of the time tracking feature.
        /// </summary>
        /// <value>The application level in possession time of the time tracking feature.</value>
        [DataMember(Name = "inPossessionTime", EmitDefaultValue = false)]
        public double? InPossessionTime { get; set; }

        /// <summary>
        /// Indicates whether or not an infraction occurred.
        /// </summary>
        /// <value>Indicates whether or not an infraction occurred.</value>
        [DataMember(Name = "infraction", EmitDefaultValue = false)]
        public bool? Infraction { get; set; }

        /// <summary>
        /// The Civic Platform product  where the application is submitted: \&quot;AA\&quot; : Classic Accela Automation. \&quot;ACA\&quot; : Accela Citizen Access. \&quot;AIVR\&quot; : Accela Integrated Voice Response. \&quot;AMO\&quot; : Accela Mobile Office. \&quot;AV360\&quot; : Accela Asset Management, Accela Land Management.
        /// </summary>
        /// <value>The Civic Platform product  where the application is submitted: \&quot;AA\&quot; : Classic Accela Automation. \&quot;ACA\&quot; : Accela Citizen Access. \&quot;AIVR\&quot; : Accela Integrated Voice Response. \&quot;AMO\&quot; : Accela Mobile Office. \&quot;AV360\&quot; : Accela Asset Management, Accela Land Management.</value>
        [DataMember(Name = "initiatedProduct", EmitDefaultValue = false)]
        public string InitiatedProduct { get; set; }

        /// <summary>
        /// The name of the department where the inspector works. See [Get All Departments](./api-settings.html#operation/v4.get.settings.departments).
        /// </summary>
        /// <value>The name of the department where the inspector works. See [Get All Departments](./api-settings.html#operation/v4.get.settings.departments).</value>
        [DataMember(Name = "inspectorDepartment", EmitDefaultValue = false)]
        public string InspectorDepartment { get; set; }

        /// <summary>
        /// The ID number of the inspector. See [Get All Inspectors](./api-inspections.html#operation/v4.get.inspectors).
        /// </summary>
        /// <value>The ID number of the inspector. See [Get All Inspectors](./api-inspections.html#operation/v4.get.inspectors).</value>
        [DataMember(Name = "inspectorId", EmitDefaultValue = false)]
        public string InspectorId { get; set; }

        /// <summary>
        /// The name of the inspector. See [Get All Inspectors](./api-inspections.html#operation/v4.get.inspectors).
        /// </summary>
        /// <value>The name of the inspector. See [Get All Inspectors](./api-inspections.html#operation/v4.get.inspectors).</value>
        [DataMember(Name = "inspectorName", EmitDefaultValue = false)]
        public string InspectorName { get; set; }

        /// <summary>
        /// The value of the job.
        /// </summary>
        /// <value>The value of the job.</value>
        [DataMember(Name = "jobValue", EmitDefaultValue = false)]
        public double? JobValue { get; set; }

        /// <summary>
        /// Indicates whether or not a misdemeanor occurred.
        /// </summary>
        /// <value>Indicates whether or not a misdemeanor occurred.</value>
        [DataMember(Name = "misdemeanor", EmitDefaultValue = false)]
        public bool? Misdemeanor { get; set; }

        /// <summary>
        /// The module the record belongs to. See [Get All Modules](./api-settings.html#operation/v4.get.settings.modules).
        /// </summary>
        /// <value>The module the record belongs to. See [Get All Modules](./api-settings.html#operation/v4.get.settings.modules).</value>
        [DataMember(Name = "module", EmitDefaultValue = false)]
        public string Module { get; set; }

        /// <summary>
        /// The name associated to the record.
        /// </summary>
        /// <value>The name associated to the record.</value>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// The number of buildings.
        /// </summary>
        /// <value>The number of buildings.</value>
        [DataMember(Name = "numberOfBuildings", EmitDefaultValue = false)]
        public long? NumberOfBuildings { get; set; }

        /// <summary>
        /// Indicates whether or not  there was a witness to the alleged offense.
        /// </summary>
        /// <value>Indicates whether or not  there was a witness to the alleged offense.</value>
        [DataMember(Name = "offenseWitnessed", EmitDefaultValue = false)]
        public bool? OffenseWitnessed { get; set; }

        /// <summary>
        /// The date the application was opened.
        /// </summary>
        /// <value>The date the application was opened.</value>
        [DataMember(Name = "openedDate", EmitDefaultValue = false)]
        public DateTime? OpenedDate { get; set; }

        /// <summary>
        /// The amount of elapsed time from the time tracking start date to the completion of the application.
        /// </summary>
        /// <value>The amount of elapsed time from the time tracking start date to the completion of the application.</value>
        [DataMember(Name = "overallApplicationTime", EmitDefaultValue = false)]
        public double? OverallApplicationTime { get; set; }

        /// <summary>
        /// Gets or Sets Priority
        /// </summary>
        [DataMember(Name = "priority", EmitDefaultValue = false)]
        public RecordAPOCustomFormsModelPriority Priority { get; set; }

        /// <summary>
        /// Indicates whether or not the record is for the public.
        /// </summary>
        /// <value>Indicates whether or not the record is for the public.</value>
        [DataMember(Name = "publicOwned", EmitDefaultValue = false)]
        public bool? PublicOwned { get; set; }

        /// <summary>
        /// General information about the record.
        /// </summary>
        /// <value>General information about the record.</value>
        [DataMember(Name = "recordClass", EmitDefaultValue = false)]
        public string RecordClass { get; set; }

        /// <summary>
        /// Gets or Sets RenewalInfo
        /// </summary>
        [DataMember(Name = "renewalInfo", EmitDefaultValue = false)]
        public AccelaRecords.Model.RecordExpirationModel RenewalInfo { get; set; }

        /// <summary>
        /// Gets or Sets ReportedChannel
        /// </summary>
        [DataMember(Name = "reportedChannel", EmitDefaultValue = false)]
        public RecordAPOCustomFormsModelReportedChannel ReportedChannel { get; set; }

        /// <summary>
        /// The date the complaint was reported.
        /// </summary>
        /// <value>The date the complaint was reported.</value>
        [DataMember(Name = "reportedDate", EmitDefaultValue = false)]
        public DateTime? ReportedDate { get; set; }

        /// <summary>
        /// Gets or Sets ReportedType
        /// </summary>
        [DataMember(Name = "reportedType", EmitDefaultValue = false)]
        public RecordAPOCustomFormsModelReportedType ReportedType { get; set; }

        /// <summary>
        /// The date when the inspection gets scheduled.
        /// </summary>
        /// <value>The date when the inspection gets scheduled.</value>
        [DataMember(Name = "scheduledDate", EmitDefaultValue = false)]
        public DateTime? ScheduledDate { get; set; }

        /// <summary>
        /// The unique agency identifier,
        /// </summary>
        /// <value>The unique agency identifier,</value>
        [DataMember(Name = "serviceProviderCode", EmitDefaultValue = false)]
        public string ServiceProviderCode { get; set; }

        /// <summary>
        /// Gets or Sets Severity
        /// </summary>
        [DataMember(Name = "severity", EmitDefaultValue = false)]
        public RecordAPOCustomFormsModelSeverity Severity { get; set; }

        /// <summary>
        /// A brief note about the record subject.
        /// </summary>
        /// <value>A brief note about the record subject.</value>
        [DataMember(Name = "shortNotes", EmitDefaultValue = false)]
        public string ShortNotes { get; set; }

        /// <summary>
        /// Gets or Sets Status
        /// </summary>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public RecordAPOCustomFormsModelStatus Status { get; set; }

        /// <summary>
        /// The date when the current status changed. 
        /// </summary>
        /// <value>The date when the current status changed. </value>
        [DataMember(Name = "statusDate", EmitDefaultValue = false)]
        public DateTime? StatusDate { get; set; }

        /// <summary>
        /// Gets or Sets StatusReason
        /// </summary>
        [DataMember(Name = "statusReason", EmitDefaultValue = false)]
        public RecordAPOCustomFormsModelStatusReason StatusReason { get; set; }

        /// <summary>
        /// The total amount of the fees invoiced to the record.
        /// </summary>
        /// <value>The total amount of the fees invoiced to the record.</value>
        [DataMember(Name = "totalFee", EmitDefaultValue = false)]
        public double? TotalFee { get; set; }

        /// <summary>
        /// The combination of work order assignments (labor) and costs.
        /// </summary>
        /// <value>The combination of work order assignments (labor) and costs.</value>
        [DataMember(Name = "totalJobCost", EmitDefaultValue = false)]
        public double? TotalJobCost { get; set; }

        /// <summary>
        /// The total amount of pay.
        /// </summary>
        /// <value>The total amount of pay.</value>
        [DataMember(Name = "totalPay", EmitDefaultValue = false)]
        public double? TotalPay { get; set; }

        /// <summary>
        /// The application tracking number (IVR tracking number).
        /// </summary>
        /// <value>The application tracking number (IVR tracking number).</value>
        [DataMember(Name = "trackingId", EmitDefaultValue = false)]
        public long? TrackingId { get; set; }

        /// <summary>
        /// Gets or Sets Type
        /// </summary>
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public AccelaRecords.Model.RecordTypeModel Type { get; set; }

        /// <summary>
        /// The undistributed costs for this work order.
        /// </summary>
        /// <value>The undistributed costs for this work order.</value>
        [DataMember(Name = "undistributedCost", EmitDefaultValue = false)]
        public double? UndistributedCost { get; set; }

        /// <summary>
        /// The record value.
        /// </summary>
        /// <value>The record value.</value>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }
    }

    #endregion


}