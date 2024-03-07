using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Newtonsoft.Json.Serialization;


namespace Meck.Shared.Accela
{
    public class RecordApoCustomFormBE
    {
        public RecordApoCustomFormsModelBE RecordDetails { get; set; }


        public RecordApoCustomFormBE()
        {

            RecordDetails = default(RecordApoCustomFormsModelBE);

            RecordDetails.Addresses = new List<RecordAddressCustomFormsModelBE>();
            RecordDetails.Assets = new List<AssetMasterModelBE>();
            RecordDetails.ConditionOfApprovals = new List<CapConditionModel2BE>();
            RecordDetails.Conditions = new List<NoticeConditionModelBE>();
            RecordDetails.Contact = new List<RecordContactSimpleModelBE>();
            RecordDetails.CustomForms = new List<CustomAttributeModelBE>();
            RecordDetails.CustomTables = new List<TableModelBE>();
            RecordDetails.Owner = new List<RefOwnerModelBE>();
            RecordDetails.Parcel = new List<ParcelModel1BE>();
            RecordDetails.Professional = new List<LicenseProfessionalModelBE>();
            RecordDetails.StatusType = new List<string>();
        }
    }

    #region RecordApoCustomFormBE

    public partial class RecordApoCustomFormsModelBE
    {
        public RecordApoCustomFormsModelBE()
        {
            Addresses = new List<RecordAddressCustomFormsModelBE>();
            Assets = new List<AssetMasterModelBE>();
            ConditionOfApprovals = new List<CapConditionModel2BE>();
            Conditions = new List<NoticeConditionModelBE>();
            Contact = new List<RecordContactSimpleModelBE>();
            CustomForms = new List<CustomAttributeModelBE>();
            CustomTables = new List<TableModelBE>();
            Owner = new List<RefOwnerModelBE>();
            Parcel = new List<ParcelModel1BE>();
            Professional = new List<LicenseProfessionalModelBE>();
            StatusType = new List<string>();
        }

        /// <summary>
        /// Indictes whether or not the record was cloned.
        /// </summary>
        /// <value>Indictes whether or not the record was cloned.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum CreatedByCloningEnumBE
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

        /// <summary>
        /// Indictes whether or not the record was cloned.
        /// </summary>
        /// <value>Indictes whether or not the record was cloned.</value>
        [DataMember(Name = "createdByCloning", EmitDefaultValue = false)]
        public CreatedByCloningEnumBE? CreatedByCloning { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RecordAPOCustomFormsModel" /> class.
        /// </summary>
        /// <param name="actualProductionUnit">Estimated cost per production unit..</param>
        /// <param name="addresses">addresses.</param>
        /// <param name="appearanceDate">The date for a hearing appearance..</param>
        /// <param name="appearanceDayOfWeek">The day for a hearing appearance..</param>
        /// <param name="assets">assets.</param>
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
        /// <param name="conditionOfApprovals">conditionOfApprovals.</param>
        /// <param name="conditions">conditions.</param>
        /// <param name="constructionType">constructionType.</param>
        /// <param name="contact">contact.</param>
        /// <param name="costPerUnit">The cost for one unit associated to the record..</param>
        /// <param name="createdBy">The unique user id of the individual that created the entry..</param>
        /// <param name="createdByCloning">Indictes whether or not the record was cloned..</param>
        /// <param name="customForms">customForms.</param>
        /// <param name="customId">An ID based on a different numbering convention from the numbering convention used by the record ID (xxxxx-xx-xxxxx). Civic Platform auto-generates and applies an alternate ID value when you submit a new application..</param>
        /// <param name="customTables">customTables.</param>
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
        /// <param name="owner">owner.</param>
        /// <param name="parcel">parcel.</param>
        /// <param name="priority">priority.</param>
        /// <param name="professional">professional.</param>
        /// <param name="publicOwned">Indicates whether or not the record is for the public..</param>
        /// <param name="recordClass">General information about the record..</param>
        /// <param name="renewalInfo">renewalInfo.</param>
        /// <param name="reportedChannel">reportedChannel.</param>
        /// <param name="reportedDate">The date the complaint was reported..</param>
        /// <param name="reportedType">reportedType.</param>
        /// <param name="scheduledDate">The date when the inspection gets scheduled..</param>
        /// <param name="severity">severity.</param>
        /// <param name="shortNotes">A brief note about the record subject..</param>
        /// <param name="status">status.</param>
        /// <param name="statusDate">The date when the current status changed. .</param>
        /// <param name="statusReason">statusReason.</param>
        /// <param name="statusType">The record status type..</param>
        /// <param name="totalFee">The total amount of the fees invoiced to the record..</param>
        /// <param name="totalJobCost">The combination of work order assignments (labor) and costs..</param>
        /// <param name="totalPay">The total amount of pay..</param>
        /// <param name="trackingId">The application tracking number (IVR tracking number)..</param>
        /// <param name="type">type.</param>
        /// <param name="undistributedCost">The undistributed costs for this work order..</param>
        /// <param name="updateDate">The last update date..</param>
        /// <param name="value">The record value..</param>
        public RecordApoCustomFormsModelBE(double? actualProductionUnit = default(double?),
            List<RecordAddressCustomFormsModelBE> addresses = default(List<RecordAddressCustomFormsModelBE>),
            DateTime? appearanceDate = default(DateTime?), string appearanceDayOfWeek = default(string),
            List<AssetMasterModelBE> assets = default(List<AssetMasterModelBE>),
            DateTime? assignedDate = default(DateTime?), string assignedToDepartment = default(string),
            string assignedUser = default(string), double? balance = default(double?), bool? booking = default(bool?),
            string closedByDepartment = default(string), string closedByUser = default(string),
            DateTime? closedDate = default(DateTime?), DateTime? completeDate = default(DateTime?),
            string completedByDepartment = default(string), string completedByUser = default(string),
            List<CapConditionModel2BE> conditionOfApprovals = default(List<CapConditionModel2BE>),
            List<NoticeConditionModelBE> conditions = default(List<NoticeConditionModelBE>),
            RecordAPOCustomFormsModelConstructionTypeBE constructionType =
                default(RecordAPOCustomFormsModelConstructionTypeBE),
            List<RecordContactSimpleModelBE> contact = default(List<RecordContactSimpleModelBE>),
            double? costPerUnit = default(double?), string createdBy = default(string),
            CreatedByCloningEnumBE? createdByCloning = default(CreatedByCloningEnumBE?),
            List<CustomAttributeModelBE> customForms = default(List<CustomAttributeModelBE>),
            string customId = default(string), List<TableModelBE> customTables = default(List<TableModelBE>),
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
            List<RefOwnerModelBE> owner = default(List<RefOwnerModelBE>),
            List<ParcelModel1BE> parcel = default(List<ParcelModel1BE>),
            RecordAPOCustomFormsModelPriorityBE priority = default(RecordAPOCustomFormsModelPriorityBE),
            List<LicenseProfessionalModelBE> professional = default(List<LicenseProfessionalModelBE>),
            bool? publicOwned = default(bool?), string recordClass = default(string),
            RecordExpirationModelBE renewalInfo = default(RecordExpirationModelBE),
            RecordAPOCustomFormsModelReportedChannelBE reportedChannel =
                default(RecordAPOCustomFormsModelReportedChannelBE), DateTime? reportedDate = default(DateTime?),
            RecordAPOCustomFormsModelReportedTypeBE reportedType = default(RecordAPOCustomFormsModelReportedTypeBE),
            DateTime? scheduledDate = default(DateTime?),
            RecordAPOCustomFormsModelSeverityBE severity = default(RecordAPOCustomFormsModelSeverityBE),
            string shortNotes = default(string),
            RecordAPOCustomFormsModelStatusBE status = default(RecordAPOCustomFormsModelStatusBE),
            DateTime? statusDate = default(DateTime?),
            RecordAPOCustomFormsModelStatusReasonBE statusReason = default(RecordAPOCustomFormsModelStatusReasonBE),
            List<string> statusType = default(List<string>), double? totalFee = default(double?),
            double? totalJobCost = default(double?), double? totalPay = default(double?),
            long? trackingId = default(long?), RecordTypeModelBE type = default(RecordTypeModelBE),
            double? undistributedCost = default(double?), DateTime? updateDate = default(DateTime?),
            string value = default(string))
        {
            this.ActualProductionUnit = actualProductionUnit;
            this.Addresses = addresses;
            this.AppearanceDate = appearanceDate;
            this.AppearanceDayOfWeek = appearanceDayOfWeek;
            this.Assets = assets;
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
            this.ConditionOfApprovals = conditionOfApprovals;
            this.Conditions = conditions;
            this.ConstructionType = constructionType;
            this.Contact = contact;
            this.CostPerUnit = costPerUnit;
            this.CreatedBy = createdBy;
            this.CreatedByCloning = createdByCloning;
            this.CustomForms = customForms;
            this.CustomId = customId;
            this.CustomTables = customTables;
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
            this.Owner = owner;
            this.Parcel = parcel;
            this.Priority = priority;
            this.Professional = professional;
            this.PublicOwned = publicOwned;
            this.RecordClass = recordClass;
            this.RenewalInfo = renewalInfo;
            this.ReportedChannel = reportedChannel;
            this.ReportedDate = reportedDate;
            this.ReportedType = reportedType;
            this.ScheduledDate = scheduledDate;
            this.Severity = severity;
            this.ShortNotes = shortNotes;
            this.Status = status;
            this.StatusDate = statusDate;
            this.StatusReason = statusReason;
            this.StatusType = statusType;
            this.TotalFee = totalFee;
            this.TotalJobCost = totalJobCost;
            this.TotalPay = totalPay;
            this.TrackingId = trackingId;
            this.Type = type;
            this.UndistributedCost = undistributedCost;
            this.UpdateDate = updateDate;
            this.Value = value;
        }

        /// <summary>
        /// Estimated cost per production unit.
        /// </summary>
        /// <value>Estimated cost per production unit.</value>
        [DataMember(Name = "actualProductionUnit", EmitDefaultValue = false)]
        public double? ActualProductionUnit { get; set; }

        /// <summary>
        /// Gets or Sets Addresses
        /// </summary>
        [DataMember(Name = "addresses", EmitDefaultValue = false)]
        public List<RecordAddressCustomFormsModelBE> Addresses { get; set; }

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
        /// Gets or Sets Assets
        /// </summary>
        [DataMember(Name = "assets", EmitDefaultValue = false)]
        public List<AssetMasterModelBE> Assets { get; set; }

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
        /// Gets or Sets ConditionOfApprovals
        /// </summary>
        [DataMember(Name = "conditionOfApprovals", EmitDefaultValue = false)]
        public List<CapConditionModel2BE> ConditionOfApprovals { get; set; }

        /// <summary>
        /// Gets or Sets Conditions
        /// </summary>
        [DataMember(Name = "conditions", EmitDefaultValue = false)]
        public List<NoticeConditionModelBE> Conditions { get; set; }

        /// <summary>
        /// Gets or Sets ConstructionType
        /// </summary>
        [DataMember(Name = "constructionType", EmitDefaultValue = false)]
        public RecordAPOCustomFormsModelConstructionTypeBE ConstructionType { get; set; }

        /// <summary>
        /// Gets or Sets Contact
        /// </summary>
        [DataMember(Name = "contact", EmitDefaultValue = false)]
        public List<RecordContactSimpleModelBE> Contact { get; set; }

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
        /// Gets or Sets CustomForms
        /// </summary>
        [DataMember(Name = "customForms", EmitDefaultValue = false)]
        public List<CustomAttributeModelBE> CustomForms { get; set; }

        /// <summary>
        /// An ID based on a different numbering convention from the numbering convention used by the record ID (xxxxx-xx-xxxxx). Civic Platform auto-generates and applies an alternate ID value when you submit a new application.
        /// </summary>
        /// <value>An ID based on a different numbering convention from the numbering convention used by the record ID (xxxxx-xx-xxxxx). Civic Platform auto-generates and applies an alternate ID value when you submit a new application.</value>
        [DataMember(Name = "customId", EmitDefaultValue = false)]
        public string CustomId { get; set; }

        /// <summary>
        /// Gets or Sets CustomTables
        /// </summary>
        [DataMember(Name = "customTables", EmitDefaultValue = false)]
        public List<TableModelBE> CustomTables { get; set; }

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
        /// Gets or Sets Owner
        /// </summary>
        [DataMember(Name = "owner", EmitDefaultValue = false)]
        public List<RefOwnerModelBE> Owner { get; set; }

        /// <summary>
        /// Gets or Sets Parcel
        /// </summary>
        [DataMember(Name = "parcel", EmitDefaultValue = false)]
        public List<ParcelModel1BE> Parcel { get; set; }

        /// <summary>
        /// Gets or Sets Priority
        /// </summary>
        [DataMember(Name = "priority", EmitDefaultValue = false)]
        public RecordAPOCustomFormsModelPriorityBE Priority { get; set; }

        /// <summary>
        /// Gets or Sets Professional
        /// </summary>
        [DataMember(Name = "professional", EmitDefaultValue = false)]
        public List<LicenseProfessionalModelBE> Professional { get; set; }

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
        public RecordExpirationModelBE RenewalInfo { get; set; }

        /// <summary>
        /// Gets or Sets ReportedChannel
        /// </summary>
        [DataMember(Name = "reportedChannel", EmitDefaultValue = false)]
        public RecordAPOCustomFormsModelReportedChannelBE ReportedChannel { get; set; }

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
        public RecordAPOCustomFormsModelReportedTypeBE ReportedType { get; set; }

        /// <summary>
        /// The date when the inspection gets scheduled.
        /// </summary>
        /// <value>The date when the inspection gets scheduled.</value>
        [DataMember(Name = "scheduledDate", EmitDefaultValue = false)]
        public DateTime? ScheduledDate { get; set; }

        /// <summary>
        /// Gets or Sets Severity
        /// </summary>
        [DataMember(Name = "severity", EmitDefaultValue = false)]
        public RecordAPOCustomFormsModelSeverityBE Severity { get; set; }

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
        public RecordAPOCustomFormsModelStatusBE Status { get; set; }

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
        public RecordAPOCustomFormsModelStatusReasonBE StatusReason { get; set; }

        /// <summary>
        /// The record status type.
        /// </summary>
        /// <value>The record status type.</value>
        [DataMember(Name = "statusType", EmitDefaultValue = false)]
        public List<string> StatusType { get; set; }

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
        public RecordTypeModelBE Type { get; set; }

        /// <summary>
        /// The undistributed costs for this work order.
        /// </summary>
        /// <value>The undistributed costs for this work order.</value>
        [DataMember(Name = "undistributedCost", EmitDefaultValue = false)]
        public double? UndistributedCost { get; set; }

        /// <summary>
        /// The last update date.
        /// </summary>
        /// <value>The last update date.</value>
        [DataMember(Name = "updateDate", EmitDefaultValue = false)]
        public DateTime? UpdateDate { get; set; }

        /// <summary>
        /// The record value.
        /// </summary>
        /// <value>The record value.</value>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }
    }

    #endregion

    #region AssetMasterModelBE

    /// <summary>
    /// AssetMasterModel
    /// </summary>
    [DataContract]
    public partial class AssetMasterModelBE
    {
        public AssetMasterModelBE()
        {

        }

        /// <summary>
        /// Indicates whether or not the parent asset is dependent on this asset.
        /// </summary>
        /// <value>Indicates whether or not the parent asset is dependent on this asset.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum DependentFlagEnum
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

        /// <summary>
        /// Indicates whether or not the parent asset is dependent on this asset.
        /// </summary>
        /// <value>Indicates whether or not the parent asset is dependent on this asset.</value>
        [DataMember(Name = "dependentFlag", EmitDefaultValue = false)]
        public DependentFlagEnum? DependentFlag { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AssetMasterModel" /> class.
        /// </summary>
        /// <param name="assetId">The unique alpha-numeric asset ID in an asset group.  **Added in Civic Platform version**: 9.2.0 .</param>
        /// <param name="classType">A Class Type is how Civic Platform groups objects that an agency owns or maintains. The five class types are component, linear, node-link linear, point, and polygon. Asset class types provide the ability to assign or group multiple asset types together. .</param>
        /// <param name="comments">comments.</param>
        /// <param name="currentValue">The current value of the asset..</param>
        /// <param name="dateOfService">The date the asset was initially placed into service..</param>
        /// <param name="dependentFlag">Indicates whether or not the parent asset is dependent on this asset..</param>
        /// <param name="depreciationAmount">The decline in the asset value by the asset depreciation calculation..</param>
        /// <param name="depreciationEndDate">The end date for the asset depreciation calculation. This field is used in the asset depreciation calculation..</param>
        /// <param name="depreciationStartDate">The start date for the asset depreciation calculation. This field is used in the asset depreciation calculation..</param>
        /// <param name="depreciationValue">The asset value after the asset depreciation calculation, which is based on the start value, depreciation start and end dates, useful life, and salvage value..</param>
        /// <param name="description">description.</param>
        /// <param name="endId">The ending point asset ID..</param>
        /// <param name="gisObjects">gisObjects.</param>
        /// <param name="id">The asset system id assigned by the Civic Platform server..</param>
        /// <param name="name">name.</param>
        /// <param name="number">The unique, alpha-numeric asset ID..</param>
        /// <param name="salvageValue">The residual value of the asset at the end of itâ€™s useful life..</param>
        /// <param name="serviceProviderCode">The unique agency identifier..</param>
        /// <param name="size">A positive numeric value for the asset size..</param>
        /// <param name="sizeUnit">The unit of measure corresponding to the asset size..</param>
        /// <param name="startId">The starting point asset ID..</param>
        /// <param name="startValue">The beginning value or purchase price of the asset..</param>
        /// <param name="status">status.</param>
        /// <param name="statusDate">The date the asset status changed..</param>
        /// <param name="type">type.</param>
        public AssetMasterModelBE(string assetId = default(string), string classType = default(string),
            AssetMasterModelCommentsBe comments = default(AssetMasterModelCommentsBe),
            double? currentValue = default(double?), DateTime? dateOfService = default(DateTime?),
            DependentFlagEnum? dependentFlag = default(DependentFlagEnum?),
            double? depreciationAmount = default(double?), DateTime? depreciationEndDate = default(DateTime?),
            DateTime? depreciationStartDate = default(DateTime?), double? depreciationValue = default(double?),
            AssetMasterModelDescriptionBe description = default(AssetMasterModelDescriptionBe),
            string endId = default(string), List<GisObjectModelBe> gisObjects = default(List<GisObjectModelBe>),
            long? id = default(long?), AssetMasterModelNameBe name = default(AssetMasterModelNameBe),
            string number = default(string), double? salvageValue = default(double?),
            string serviceProviderCode = default(string), double? size = default(double?),
            string sizeUnit = default(string), string startId = default(string), double? startValue = default(double?),
            AssetMasterModelStatusBe status = default(AssetMasterModelStatusBe),
            DateTime? statusDate = default(DateTime?),
            AssetMasterModelTypeBe type = default(AssetMasterModelTypeBe))
        {
            this.AssetId = assetId;
            this.ClassType = classType;
            this.Comments = comments;
            this.CurrentValue = currentValue;
            this.DateOfService = dateOfService;
            this.DependentFlag = dependentFlag;
            this.DepreciationAmount = depreciationAmount;
            this.DepreciationEndDate = depreciationEndDate;
            this.DepreciationStartDate = depreciationStartDate;
            this.DepreciationValue = depreciationValue;
            this.Description = description;
            this.EndId = endId;
            this.GisObjects = gisObjects;
            this.Id = id;
            this.Name = name;
            this.Number = number;
            this.SalvageValue = salvageValue;
            this.ServiceProviderCode = serviceProviderCode;
            this.Size = size;
            this.SizeUnit = sizeUnit;
            this.StartId = startId;
            this.StartValue = startValue;
            this.Status = status;
            this.StatusDate = statusDate;
            this.Type = type;
        }

        /// <summary>
        /// The unique alpha-numeric asset ID in an asset group.  **Added in Civic Platform version**: 9.2.0 
        /// </summary>
        /// <value>The unique alpha-numeric asset ID in an asset group.  **Added in Civic Platform version**: 9.2.0 </value>
        [DataMember(Name = "assetId", EmitDefaultValue = false)]
        public string AssetId { get; set; }

        /// <summary>
        /// A Class Type is how Civic Platform groups objects that an agency owns or maintains. The five class types are component, linear, node-link linear, point, and polygon. Asset class types provide the ability to assign or group multiple asset types together. 
        /// </summary>
        /// <value>A Class Type is how Civic Platform groups objects that an agency owns or maintains. The five class types are component, linear, node-link linear, point, and polygon. Asset class types provide the ability to assign or group multiple asset types together. </value>
        [DataMember(Name = "classType", EmitDefaultValue = false)]
        public string ClassType { get; set; }

        /// <summary>
        /// Gets or Sets Comments
        /// </summary>
        [DataMember(Name = "comments", EmitDefaultValue = false)]
        public AssetMasterModelCommentsBe Comments { get; set; }

        /// <summary>
        /// The current value of the asset.
        /// </summary>
        /// <value>The current value of the asset.</value>
        [DataMember(Name = "currentValue", EmitDefaultValue = false)]
        public double? CurrentValue { get; set; }

        /// <summary>
        /// The date the asset was initially placed into service.
        /// </summary>
        /// <value>The date the asset was initially placed into service.</value>
        [DataMember(Name = "dateOfService", EmitDefaultValue = false)]
        public DateTime? DateOfService { get; set; }


        /// <summary>
        /// The decline in the asset value by the asset depreciation calculation.
        /// </summary>
        /// <value>The decline in the asset value by the asset depreciation calculation.</value>
        [DataMember(Name = "depreciationAmount", EmitDefaultValue = false)]
        public double? DepreciationAmount { get; set; }

        /// <summary>
        /// The end date for the asset depreciation calculation. This field is used in the asset depreciation calculation.
        /// </summary>
        /// <value>The end date for the asset depreciation calculation. This field is used in the asset depreciation calculation.</value>
        [DataMember(Name = "depreciationEndDate", EmitDefaultValue = false)]
        public DateTime? DepreciationEndDate { get; set; }

        /// <summary>
        /// The start date for the asset depreciation calculation. This field is used in the asset depreciation calculation.
        /// </summary>
        /// <value>The start date for the asset depreciation calculation. This field is used in the asset depreciation calculation.</value>
        [DataMember(Name = "depreciationStartDate", EmitDefaultValue = false)]
        public DateTime? DepreciationStartDate { get; set; }

        /// <summary>
        /// The asset value after the asset depreciation calculation, which is based on the start value, depreciation start and end dates, useful life, and salvage value.
        /// </summary>
        /// <value>The asset value after the asset depreciation calculation, which is based on the start value, depreciation start and end dates, useful life, and salvage value.</value>
        [DataMember(Name = "depreciationValue", EmitDefaultValue = false)]
        public double? DepreciationValue { get; set; }

        /// <summary>
        /// Gets or Sets Description
        /// </summary>
        [DataMember(Name = "description", EmitDefaultValue = false)]
        public AssetMasterModelDescriptionBe Description { get; set; }

        /// <summary>
        /// The ending point asset ID.
        /// </summary>
        /// <value>The ending point asset ID.</value>
        [DataMember(Name = "endID", EmitDefaultValue = false)]
        public string EndId { get; set; }

        /// <summary>
        /// Gets or Sets GisObjects
        /// </summary>
        [DataMember(Name = "gisObjects", EmitDefaultValue = false)]
        public List<GisObjectModelBe> GisObjects { get; set; }

        /// <summary>
        /// The asset system id assigned by the Civic Platform server.
        /// </summary>
        /// <value>The asset system id assigned by the Civic Platform server.</value>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public long? Id { get; set; }

        /// <summary>
        /// Gets or Sets Name
        /// </summary>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public AssetMasterModelNameBe Name { get; set; }

        /// <summary>
        /// The unique, alpha-numeric asset ID.
        /// </summary>
        /// <value>The unique, alpha-numeric asset ID.</value>
        [DataMember(Name = "number", EmitDefaultValue = false)]
        public string Number { get; set; }

        /// <summary>
        /// The residual value of the asset at the end of itâ€™s useful life.
        /// </summary>
        /// <value>The residual value of the asset at the end of itâ€™s useful life.</value>
        [DataMember(Name = "salvageValue", EmitDefaultValue = false)]
        public double? SalvageValue { get; set; }

        /// <summary>
        /// The unique agency identifier.
        /// </summary>
        /// <value>The unique agency identifier.</value>
        [DataMember(Name = "serviceProviderCode", EmitDefaultValue = false)]
        public string ServiceProviderCode { get; set; }

        /// <summary>
        /// A positive numeric value for the asset size.
        /// </summary>
        /// <value>A positive numeric value for the asset size.</value>
        [DataMember(Name = "size", EmitDefaultValue = false)]
        public double? Size { get; set; }

        /// <summary>
        /// The unit of measure corresponding to the asset size.
        /// </summary>
        /// <value>The unit of measure corresponding to the asset size.</value>
        [DataMember(Name = "sizeUnit", EmitDefaultValue = false)]
        public string SizeUnit { get; set; }

        /// <summary>
        /// The starting point asset ID.
        /// </summary>
        /// <value>The starting point asset ID.</value>
        [DataMember(Name = "startID", EmitDefaultValue = false)]
        public string StartId { get; set; }

        /// <summary>
        /// The beginning value or purchase price of the asset.
        /// </summary>
        /// <value>The beginning value or purchase price of the asset.</value>
        [DataMember(Name = "startValue", EmitDefaultValue = false)]
        public double? StartValue { get; set; }

        /// <summary>
        /// Gets or Sets Status
        /// </summary>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public AssetMasterModelStatusBe Status { get; set; }

        /// <summary>
        /// The date the asset status changed.
        /// </summary>
        /// <value>The date the asset status changed.</value>
        [DataMember(Name = "statusDate", EmitDefaultValue = false)]
        public DateTime? StatusDate { get; set; }

        /// <summary>
        /// Gets or Sets Type
        /// </summary>
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public AssetMasterModelTypeBe Type { get; set; }
    }

    #endregion

    #region AssetMasterModelTypeBE

    /// <summary>
    /// The type of asset.
    /// </summary>
    [DataContract]
    public partial class AssetMasterModelTypeBe
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssetMasterModelType" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public AssetMasterModelTypeBe(string text = default(string), string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// The localized display value.
        /// </summary>
        /// <value>The localized display value.</value>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        public string Text { get; set; }

        /// <summary>
        /// The data value.
        /// </summary>
        /// <value>The data value.</value>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }
    }

    #endregion

    #region AssetMasterModelStatusBE

    /// <summary>
    /// The status of the asset.
    /// </summary>
    [DataContract]
    public partial class AssetMasterModelStatusBe
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssetMasterModelStatus" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public AssetMasterModelStatusBe(string text = default(string), string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// The localized display value.
        /// </summary>
        /// <value>The localized display value.</value>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        public string Text { get; set; }

        /// <summary>
        /// The data value.
        /// </summary>
        /// <value>The data value.</value>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }
    }

    #endregion

    #region AssetMaseterModelCommentsBE

    /// <summary>
    /// General comments about the asset.
    /// </summary>
    [DataContract]
    public partial class AssetMasterModelCommentsBe
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssetMasterModelComments" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public AssetMasterModelCommentsBe(string text = default(string), string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// The localized display value.
        /// </summary>
        /// <value>The localized display value.</value>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        public string Text { get; set; }

        /// <summary>
        /// The data value.
        /// </summary>
        /// <value>The data value.</value>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }
    }

    #endregion

    #region AssetMasetModelDescriptionBE

    /// <summary>
    /// The description of the asset.
    /// </summary>
    [DataContract]
    public partial class AssetMasterModelDescriptionBe
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssetMasterModelDescription" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public AssetMasterModelDescriptionBe(string text = default(string), string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// The localized display value.
        /// </summary>
        /// <value>The localized display value.</value>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        public string Text { get; set; }

        /// <summary>
        /// The data value.
        /// </summary>
        /// <value>The data value.</value>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }
    }

    #endregion

    #region AssetMasterModelNameBE

    /// <summary>
    /// The descriptive name of the asset.
    /// </summary>
    [DataContract]
    public partial class AssetMasterModelNameBe
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AssetMasterModelName" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public AssetMasterModelNameBe(string text = default(string), string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// The localized display value.
        /// </summary>
        /// <value>The localized display value.</value>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        public string Text { get; set; }

        /// <summary>
        /// The data value.
        /// </summary>
        /// <value>The data value.</value>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }
    }

    #endregion

    #region CapConditionModel2BE

    /// <summary>
    /// CapConditionModel2
    /// </summary>
    [DataContract]
    public partial class CapConditionModel2BE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CapConditionModel2" /> class.
        /// </summary>
        /// <param name="actionbyDepartment">actionbyDepartment.</param>
        /// <param name="actionbyUser">actionbyUser.</param>
        /// <param name="activeStatus">activeStatus.</param>
        /// <param name="additionalInformation">additionalInformation.</param>
        /// <param name="additionalInformationPlainText">additionalInformationPlainText.</param>
        /// <param name="agencyListSql">agencyListSQL.</param>
        /// <param name="appliedDate">appliedDate.</param>
        /// <param name="appliedbyDepartment">appliedbyDepartment.</param>
        /// <param name="appliedbyUser">appliedbyUser.</param>
        /// <param name="dispAdditionalInformationPlainText">dispAdditionalInformationPlainText.</param>
        /// <param name="displayNoticeInAgency">displayNoticeInAgency.</param>
        /// <param name="displayNoticeInCitizens">displayNoticeInCitizens.</param>
        /// <param name="displayNoticeInCitizensFee">displayNoticeInCitizensFee.</param>
        /// <param name="displayOrder">displayOrder.</param>
        /// <param name="effectiveDate">effectiveDate.</param>
        /// <param name="expirationDate">expirationDate.</param>
        /// <param name="group">group.</param>
        /// <param name="id">id.</param>
        /// <param name="inheritable">inheritable.</param>
        /// <param name="isIncludeNameInNotice">isIncludeNameInNotice.</param>
        /// <param name="isIncludeShortCommentsInNotice">isIncludeShortCommentsInNotice.</param>
        /// <param name="longComments">longComments.</param>
        /// <param name="name">name.</param>
        /// <param name="priority">priority.</param>
        /// <param name="publicDisplayMessage">publicDisplayMessage.</param>
        /// <param name="recordId">recordId.</param>
        /// <param name="resAdditionalInformationPlainText">resAdditionalInformationPlainText.</param>
        /// <param name="resolutionAction">resolutionAction.</param>
        /// <param name="serviceProviderCode">serviceProviderCode.</param>
        /// <param name="serviceProviderCodes">serviceProviderCodes.</param>
        /// <param name="severity">severity.</param>
        /// <param name="shortComments">shortComments.</param>
        /// <param name="status">status.</param>
        /// <param name="statusDate">statusDate.</param>
        /// <param name="statusType">statusType.</param>
        /// <param name="type">type.</param>
        public CapConditionModel2BE(IdentifierModelBE actionbyDepartment = default(IdentifierModelBE),
            IdentifierModelBE actionbyUser = default(IdentifierModelBE),
            IdentifierModelBE activeStatus = default(IdentifierModelBE), string additionalInformation = default(string),
            string additionalInformationPlainText = default(string), string agencyListSql = default(string),
            DateTime? appliedDate = default(DateTime?),
            IdentifierModelBE appliedbyDepartment = default(IdentifierModelBE),
            IdentifierModelBE appliedbyUser = default(IdentifierModelBE),
            string dispAdditionalInformationPlainText = default(string), bool? displayNoticeInAgency = default(bool?),
            bool? displayNoticeInCitizens = default(bool?), bool? displayNoticeInCitizensFee = default(bool?),
            long? displayOrder = default(long?), DateTime? effectiveDate = default(DateTime?),
            DateTime? expirationDate = default(DateTime?), IdentifierModelBE group = default(IdentifierModelBE),
            long? id = default(long?), IdentifierModelBE inheritable = default(IdentifierModelBE),
            bool? isIncludeNameInNotice = default(bool?), bool? isIncludeShortCommentsInNotice = default(bool?),
            string longComments = default(string), string name = default(string),
            IdentifierModelBE priority = default(IdentifierModelBE), string publicDisplayMessage = default(string),
            CapIDModelBE recordId = default(CapIDModelBE), string resAdditionalInformationPlainText = default(string),
            string resolutionAction = default(string), string serviceProviderCode = default(string),
            string serviceProviderCodes = default(string), IdentifierModelBE severity = default(IdentifierModelBE),
            string shortComments = default(string), IdentifierModelBE status = default(IdentifierModelBE),
            DateTime? statusDate = default(DateTime?), string statusType = default(string),
            IdentifierModelBE type = default(IdentifierModelBE))
        {
            this.ActionbyDepartment = actionbyDepartment;
            this.ActionbyUser = actionbyUser;
            this.ActiveStatus = activeStatus;
            this.AdditionalInformation = additionalInformation;
            this.AdditionalInformationPlainText = additionalInformationPlainText;
            this.AgencyListSql = agencyListSql;
            this.AppliedDate = appliedDate;
            this.AppliedbyDepartment = appliedbyDepartment;
            this.AppliedbyUser = appliedbyUser;
            this.DispAdditionalInformationPlainText = dispAdditionalInformationPlainText;
            this.DisplayNoticeInAgency = displayNoticeInAgency;
            this.DisplayNoticeInCitizens = displayNoticeInCitizens;
            this.DisplayNoticeInCitizensFee = displayNoticeInCitizensFee;
            this.DisplayOrder = displayOrder;
            this.EffectiveDate = effectiveDate;
            this.ExpirationDate = expirationDate;
            this.Group = group;
            this.Id = id;
            this.Inheritable = inheritable;
            this.IsIncludeNameInNotice = isIncludeNameInNotice;
            this.IsIncludeShortCommentsInNotice = isIncludeShortCommentsInNotice;
            this.LongComments = longComments;
            this.Name = name;
            this.Priority = priority;
            this.PublicDisplayMessage = publicDisplayMessage;
            this.RecordId = recordId;
            this.ResAdditionalInformationPlainText = resAdditionalInformationPlainText;
            this.ResolutionAction = resolutionAction;
            this.ServiceProviderCode = serviceProviderCode;
            this.ServiceProviderCodes = serviceProviderCodes;
            this.Severity = severity;
            this.ShortComments = shortComments;
            this.Status = status;
            this.StatusDate = statusDate;
            this.StatusType = statusType;
            this.Type = type;
        }

        /// <summary>
        /// Gets or Sets ActionbyDepartment
        /// </summary>
        [DataMember(Name = "actionbyDepartment", EmitDefaultValue = false)]
        public IdentifierModelBE ActionbyDepartment { get; set; }

        /// <summary>
        /// Gets or Sets ActionbyUser
        /// </summary>
        [DataMember(Name = "actionbyUser", EmitDefaultValue = false)]
        public IdentifierModelBE ActionbyUser { get; set; }

        /// <summary>
        /// Gets or Sets ActiveStatus
        /// </summary>
        [DataMember(Name = "activeStatus", EmitDefaultValue = false)]
        public IdentifierModelBE ActiveStatus { get; set; }

        /// <summary>
        /// Gets or Sets AdditionalInformation
        /// </summary>
        [DataMember(Name = "additionalInformation", EmitDefaultValue = false)]
        public string AdditionalInformation { get; set; }

        /// <summary>
        /// Gets or Sets AdditionalInformationPlainText
        /// </summary>
        [DataMember(Name = "additionalInformationPlainText", EmitDefaultValue = false)]
        public string AdditionalInformationPlainText { get; set; }

        /// <summary>
        /// Gets or Sets AgencyListSQL
        /// </summary>
        [DataMember(Name = "agencyListSQL", EmitDefaultValue = false)]
        public string AgencyListSql { get; set; }

        /// <summary>
        /// Gets or Sets AppliedDate
        /// </summary>
        [DataMember(Name = "appliedDate", EmitDefaultValue = false)]
        public DateTime? AppliedDate { get; set; }

        /// <summary>
        /// Gets or Sets AppliedbyDepartment
        /// </summary>
        [DataMember(Name = "appliedbyDepartment", EmitDefaultValue = false)]
        public IdentifierModelBE AppliedbyDepartment { get; set; }

        /// <summary>
        /// Gets or Sets AppliedbyUser
        /// </summary>
        [DataMember(Name = "appliedbyUser", EmitDefaultValue = false)]
        public IdentifierModelBE AppliedbyUser { get; set; }

        /// <summary>
        /// Gets or Sets DispAdditionalInformationPlainText
        /// </summary>
        [DataMember(Name = "dispAdditionalInformationPlainText", EmitDefaultValue = false)]
        public string DispAdditionalInformationPlainText { get; set; }

        /// <summary>
        /// Gets or Sets DisplayNoticeInAgency
        /// </summary>
        [DataMember(Name = "displayNoticeInAgency", EmitDefaultValue = false)]
        public bool? DisplayNoticeInAgency { get; set; }

        /// <summary>
        /// Gets or Sets DisplayNoticeInCitizens
        /// </summary>
        [DataMember(Name = "displayNoticeInCitizens", EmitDefaultValue = false)]
        public bool? DisplayNoticeInCitizens { get; set; }

        /// <summary>
        /// Gets or Sets DisplayNoticeInCitizensFee
        /// </summary>
        [DataMember(Name = "displayNoticeInCitizensFee", EmitDefaultValue = false)]
        public bool? DisplayNoticeInCitizensFee { get; set; }

        /// <summary>
        /// Gets or Sets DisplayOrder
        /// </summary>
        [DataMember(Name = "displayOrder", EmitDefaultValue = false)]
        public long? DisplayOrder { get; set; }

        /// <summary>
        /// Gets or Sets EffectiveDate
        /// </summary>
        [DataMember(Name = "effectiveDate", EmitDefaultValue = false)]
        public DateTime? EffectiveDate { get; set; }

        /// <summary>
        /// Gets or Sets ExpirationDate
        /// </summary>
        [DataMember(Name = "expirationDate", EmitDefaultValue = false)]
        public DateTime? ExpirationDate { get; set; }

        /// <summary>
        /// Gets or Sets Group
        /// </summary>
        [DataMember(Name = "group", EmitDefaultValue = false)]
        public IdentifierModelBE Group { get; set; }

        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public long? Id { get; set; }

        /// <summary>
        /// Gets or Sets Inheritable
        /// </summary>
        [DataMember(Name = "inheritable", EmitDefaultValue = false)]
        public IdentifierModelBE Inheritable { get; set; }

        /// <summary>
        /// Gets or Sets IsIncludeNameInNotice
        /// </summary>
        [DataMember(Name = "isIncludeNameInNotice", EmitDefaultValue = false)]
        public bool? IsIncludeNameInNotice { get; set; }

        /// <summary>
        /// Gets or Sets IsIncludeShortCommentsInNotice
        /// </summary>
        [DataMember(Name = "isIncludeShortCommentsInNotice", EmitDefaultValue = false)]
        public bool? IsIncludeShortCommentsInNotice { get; set; }

        /// <summary>
        /// Gets or Sets LongComments
        /// </summary>
        [DataMember(Name = "longComments", EmitDefaultValue = false)]
        public string LongComments { get; set; }

        /// <summary>
        /// Gets or Sets Name
        /// </summary>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets Priority
        /// </summary>
        [DataMember(Name = "priority", EmitDefaultValue = false)]
        public IdentifierModelBE Priority { get; set; }

        /// <summary>
        /// Gets or Sets PublicDisplayMessage
        /// </summary>
        [DataMember(Name = "publicDisplayMessage", EmitDefaultValue = false)]
        public string PublicDisplayMessage { get; set; }

        /// <summary>
        /// Gets or Sets RecordId
        /// </summary>
        [DataMember(Name = "recordId", EmitDefaultValue = false)]
        public CapIDModelBE RecordId { get; set; }

        /// <summary>
        /// Gets or Sets ResAdditionalInformationPlainText
        /// </summary>
        [DataMember(Name = "resAdditionalInformationPlainText", EmitDefaultValue = false)]
        public string ResAdditionalInformationPlainText { get; set; }

        /// <summary>
        /// Gets or Sets ResolutionAction
        /// </summary>
        [DataMember(Name = "resolutionAction", EmitDefaultValue = false)]
        public string ResolutionAction { get; set; }

        /// <summary>
        /// Gets or Sets ServiceProviderCode
        /// </summary>
        [DataMember(Name = "serviceProviderCode", EmitDefaultValue = false)]
        public string ServiceProviderCode { get; set; }

        /// <summary>
        /// Gets or Sets ServiceProviderCodes
        /// </summary>
        [DataMember(Name = "serviceProviderCodes", EmitDefaultValue = false)]
        public string ServiceProviderCodes { get; set; }

        /// <summary>
        /// Gets or Sets Severity
        /// </summary>
        [DataMember(Name = "severity", EmitDefaultValue = false)]
        public IdentifierModelBE Severity { get; set; }

        /// <summary>
        /// Gets or Sets ShortComments
        /// </summary>
        [DataMember(Name = "shortComments", EmitDefaultValue = false)]
        public string ShortComments { get; set; }

        /// <summary>
        /// Gets or Sets Status
        /// </summary>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public IdentifierModelBE Status { get; set; }

        /// <summary>
        /// Gets or Sets StatusDate
        /// </summary>
        [DataMember(Name = "statusDate", EmitDefaultValue = false)]
        public DateTime? StatusDate { get; set; }

        /// <summary>
        /// Gets or Sets StatusType
        /// </summary>
        [DataMember(Name = "statusType", EmitDefaultValue = false)]
        public string StatusType { get; set; }

        /// <summary>
        /// Gets or Sets Type
        /// </summary>
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public IdentifierModelBE Type { get; set; }
    }

    #endregion

    #region CapIdModelBE

    /// <summary>
    /// CapIDModel
    /// </summary>
    [DataContract]
    public partial class CapIDModelBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CapIDModel" /> class.
        /// </summary>
        /// <param name="customId">customId.</param>
        /// <param name="id">id.</param>
        /// <param name="serviceProviderCode">serviceProviderCode.</param>
        /// <param name="trackingId">trackingId.</param>
        /// <param name="value">value.</param>
        public CapIDModelBE(string customId = default(string), string id = default(string),
            string serviceProviderCode = default(string), long? trackingId = default(long?),
            string value = default(string))
        {
            this.CustomId = customId;
            this.Id = id;
            this.ServiceProviderCode = serviceProviderCode;
            this.TrackingId = trackingId;
            this.Value = value;
        }

        /// <summary>
        /// Gets or Sets CustomId
        /// </summary>
        [DataMember(Name = "customId", EmitDefaultValue = false)]
        public string CustomId { get; set; }

        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }

        /// <summary>
        /// Gets or Sets ServiceProviderCode
        /// </summary>
        [DataMember(Name = "serviceProviderCode", EmitDefaultValue = false)]
        public string ServiceProviderCode { get; set; }

        /// <summary>
        /// Gets or Sets TrackingId
        /// </summary>
        [DataMember(Name = "trackingId", EmitDefaultValue = false)]
        public long? TrackingId { get; set; }

        /// <summary>
        /// Gets or Sets Value
        /// </summary>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }
    }

    #endregion

    #region IdentifierModelBE

    /// <summary>
    /// IdentifierModel
    /// </summary>
    [DataContract]
    public partial class IdentifierModelBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IdentifierModel" /> class.
        /// </summary>
        /// <param name="text">text.</param>
        /// <param name="value">value.</param>
        public IdentifierModelBE(string text = default(string), string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// Gets or Sets Text
        /// </summary>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        public string Text { get; set; }

        /// <summary>
        /// Gets or Sets Value
        /// </summary>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }
    }

    #endregion

    #region GISObjectModelBE

    /// <summary>
    /// GISObjectModel
    /// </summary>
    [DataContract]
    public partial class GisObjectModelBe
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GISObjectModel" /> class.
        /// </summary>
        /// <param name="gisId">The GIS object id..</param>
        /// <param name="layerId">The map layer id..</param>
        /// <param name="serviceId">The map service id..</param>
        public GisObjectModelBe(string gisId = default(string), string layerId = default(string),
            string serviceId = default(string))
        {
            this.GisId = gisId;
            this.LayerId = layerId;
            this.ServiceId = serviceId;
        }

        /// <summary>
        /// The GIS object id.
        /// </summary>
        /// <value>The GIS object id.</value>
        [DataMember(Name = "gisId", EmitDefaultValue = false)]
        public string GisId { get; set; }

        /// <summary>
        /// The map layer id.
        /// </summary>
        /// <value>The map layer id.</value>
        [DataMember(Name = "layerId", EmitDefaultValue = false)]
        public string LayerId { get; set; }

        /// <summary>
        /// The map service id.
        /// </summary>
        /// <value>The map service id.</value>
        [DataMember(Name = "serviceId", EmitDefaultValue = false)]
        public string ServiceId { get; set; }
    }

    #endregion

    #region NoticeConditionModelBE

    /// <summary>
    /// NoticeConditionModel
    /// </summary>
    [DataContract]
    public partial class NoticeConditionModelBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NoticeConditionModel" /> class.
        /// </summary>
        /// <param name="actionbyDepartment">actionbyDepartment.</param>
        /// <param name="actionbyUser">actionbyUser.</param>
        /// <param name="activeStatus">activeStatus.</param>
        /// <param name="additionalInformation">additionalInformation.</param>
        /// <param name="additionalInformationPlainText">additionalInformationPlainText.</param>
        /// <param name="agencyListSQL">agencyListSQL.</param>
        /// <param name="appliedDate">appliedDate.</param>
        /// <param name="appliedbyDepartment">appliedbyDepartment.</param>
        /// <param name="appliedbyUser">appliedbyUser.</param>
        /// <param name="dispAdditionalInformationPlainText">dispAdditionalInformationPlainText.</param>
        /// <param name="displayNoticeInAgency">displayNoticeInAgency.</param>
        /// <param name="displayNoticeInCitizens">displayNoticeInCitizens.</param>
        /// <param name="displayNoticeInCitizensFee">displayNoticeInCitizensFee.</param>
        /// <param name="displayOrder">displayOrder.</param>
        /// <param name="effectiveDate">effectiveDate.</param>
        /// <param name="expirationDate">expirationDate.</param>
        /// <param name="group">group.</param>
        /// <param name="id">id.</param>
        /// <param name="inheritable">inheritable.</param>
        /// <param name="isIncludeNameInNotice">isIncludeNameInNotice.</param>
        /// <param name="isIncludeShortCommentsInNotice">isIncludeShortCommentsInNotice.</param>
        /// <param name="longComments">longComments.</param>
        /// <param name="name">name.</param>
        /// <param name="priority">priority.</param>
        /// <param name="publicDisplayMessage">publicDisplayMessage.</param>
        /// <param name="recordId">recordId.</param>
        /// <param name="resAdditionalInformationPlainText">resAdditionalInformationPlainText.</param>
        /// <param name="resolutionAction">resolutionAction.</param>
        /// <param name="serviceProviderCode">serviceProviderCode.</param>
        /// <param name="serviceProviderCodes">serviceProviderCodes.</param>
        /// <param name="severity">severity.</param>
        /// <param name="shortComments">shortComments.</param>
        /// <param name="status">status.</param>
        /// <param name="statusDate">statusDate.</param>
        /// <param name="statusType">statusType.</param>
        /// <param name="type">type.</param>
        public NoticeConditionModelBE(IdentifierModelBE actionbyDepartment = default(IdentifierModelBE),
            IdentifierModelBE actionbyUser = default(IdentifierModelBE),
            IdentifierModelBE activeStatus = default(IdentifierModelBE), string additionalInformation = default(string),
            string additionalInformationPlainText = default(string), string agencyListSQL = default(string),
            DateTime? appliedDate = default(DateTime?),
            IdentifierModelBE appliedbyDepartment = default(IdentifierModelBE),
            IdentifierModelBE appliedbyUser = default(IdentifierModelBE),
            string dispAdditionalInformationPlainText = default(string), bool? displayNoticeInAgency = default(bool?),
            bool? displayNoticeInCitizens = default(bool?), bool? displayNoticeInCitizensFee = default(bool?),
            long? displayOrder = default(long?), DateTime? effectiveDate = default(DateTime?),
            DateTime? expirationDate = default(DateTime?), IdentifierModelBE group = default(IdentifierModelBE),
            long? id = default(long?), IdentifierModelBE inheritable = default(IdentifierModelBE),
            bool? isIncludeNameInNotice = default(bool?), bool? isIncludeShortCommentsInNotice = default(bool?),
            string longComments = default(string), string name = default(string),
            IdentifierModelBE priority = default(IdentifierModelBE), string publicDisplayMessage = default(string),
            CapIDModelBE recordId = default(CapIDModelBE), string resAdditionalInformationPlainText = default(string),
            string resolutionAction = default(string), string serviceProviderCode = default(string),
            string serviceProviderCodes = default(string), IdentifierModelBE severity = default(IdentifierModelBE),
            string shortComments = default(string), IdentifierModelBE status = default(IdentifierModelBE),
            DateTime? statusDate = default(DateTime?), string statusType = default(string),
            IdentifierModelBE type = default(IdentifierModelBE))
        {
            this.ActionbyDepartment = actionbyDepartment;
            this.ActionbyUser = actionbyUser;
            this.ActiveStatus = activeStatus;
            this.AdditionalInformation = additionalInformation;
            this.AdditionalInformationPlainText = additionalInformationPlainText;
            this.AgencyListSQL = agencyListSQL;
            this.AppliedDate = appliedDate;
            this.AppliedbyDepartment = appliedbyDepartment;
            this.AppliedbyUser = appliedbyUser;
            this.DispAdditionalInformationPlainText = dispAdditionalInformationPlainText;
            this.DisplayNoticeInAgency = displayNoticeInAgency;
            this.DisplayNoticeInCitizens = displayNoticeInCitizens;
            this.DisplayNoticeInCitizensFee = displayNoticeInCitizensFee;
            this.DisplayOrder = displayOrder;
            this.EffectiveDate = effectiveDate;
            this.ExpirationDate = expirationDate;
            this.Group = group;
            this.Id = id;
            this.Inheritable = inheritable;
            this.IsIncludeNameInNotice = isIncludeNameInNotice;
            this.IsIncludeShortCommentsInNotice = isIncludeShortCommentsInNotice;
            this.LongComments = longComments;
            this.Name = name;
            this.Priority = priority;
            this.PublicDisplayMessage = publicDisplayMessage;
            this.RecordId = recordId;
            this.ResAdditionalInformationPlainText = resAdditionalInformationPlainText;
            this.ResolutionAction = resolutionAction;
            this.ServiceProviderCode = serviceProviderCode;
            this.ServiceProviderCodes = serviceProviderCodes;
            this.Severity = severity;
            this.ShortComments = shortComments;
            this.Status = status;
            this.StatusDate = statusDate;
            this.StatusType = statusType;
            this.Type = type;
        }

        /// <summary>
        /// Gets or Sets ActionbyDepartment
        /// </summary>
        [DataMember(Name = "actionbyDepartment", EmitDefaultValue = false)]
        public IdentifierModelBE ActionbyDepartment { get; set; }

        /// <summary>
        /// Gets or Sets ActionbyUser
        /// </summary>
        [DataMember(Name = "actionbyUser", EmitDefaultValue = false)]
        public IdentifierModelBE ActionbyUser { get; set; }

        /// <summary>
        /// Gets or Sets ActiveStatus
        /// </summary>
        [DataMember(Name = "activeStatus", EmitDefaultValue = false)]
        public IdentifierModelBE ActiveStatus { get; set; }

        /// <summary>
        /// Gets or Sets AdditionalInformation
        /// </summary>
        [DataMember(Name = "additionalInformation", EmitDefaultValue = false)]
        public string AdditionalInformation { get; set; }

        /// <summary>
        /// Gets or Sets AdditionalInformationPlainText
        /// </summary>
        [DataMember(Name = "additionalInformationPlainText", EmitDefaultValue = false)]
        public string AdditionalInformationPlainText { get; set; }

        /// <summary>
        /// Gets or Sets AgencyListSQL
        /// </summary>
        [DataMember(Name = "agencyListSQL", EmitDefaultValue = false)]
        public string AgencyListSQL { get; set; }

        /// <summary>
        /// Gets or Sets AppliedDate
        /// </summary>
        [DataMember(Name = "appliedDate", EmitDefaultValue = false)]
        public DateTime? AppliedDate { get; set; }

        /// <summary>
        /// Gets or Sets AppliedbyDepartment
        /// </summary>
        [DataMember(Name = "appliedbyDepartment", EmitDefaultValue = false)]
        public IdentifierModelBE AppliedbyDepartment { get; set; }

        /// <summary>
        /// Gets or Sets AppliedbyUser
        /// </summary>
        [DataMember(Name = "appliedbyUser", EmitDefaultValue = false)]
        public IdentifierModelBE AppliedbyUser { get; set; }

        /// <summary>
        /// Gets or Sets DispAdditionalInformationPlainText
        /// </summary>
        [DataMember(Name = "dispAdditionalInformationPlainText", EmitDefaultValue = false)]
        public string DispAdditionalInformationPlainText { get; set; }

        /// <summary>
        /// Gets or Sets DisplayNoticeInAgency
        /// </summary>
        [DataMember(Name = "displayNoticeInAgency", EmitDefaultValue = false)]
        public bool? DisplayNoticeInAgency { get; set; }

        /// <summary>
        /// Gets or Sets DisplayNoticeInCitizens
        /// </summary>
        [DataMember(Name = "displayNoticeInCitizens", EmitDefaultValue = false)]
        public bool? DisplayNoticeInCitizens { get; set; }

        /// <summary>
        /// Gets or Sets DisplayNoticeInCitizensFee
        /// </summary>
        [DataMember(Name = "displayNoticeInCitizensFee", EmitDefaultValue = false)]
        public bool? DisplayNoticeInCitizensFee { get; set; }

        /// <summary>
        /// Gets or Sets DisplayOrder
        /// </summary>
        [DataMember(Name = "displayOrder", EmitDefaultValue = false)]
        public long? DisplayOrder { get; set; }

        /// <summary>
        /// Gets or Sets EffectiveDate
        /// </summary>
        [DataMember(Name = "effectiveDate", EmitDefaultValue = false)]
        public DateTime? EffectiveDate { get; set; }

        /// <summary>
        /// Gets or Sets ExpirationDate
        /// </summary>
        [DataMember(Name = "expirationDate", EmitDefaultValue = false)]
        public DateTime? ExpirationDate { get; set; }

        /// <summary>
        /// Gets or Sets Group
        /// </summary>
        [DataMember(Name = "group", EmitDefaultValue = false)]
        public IdentifierModelBE Group { get; set; }

        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public long? Id { get; set; }

        /// <summary>
        /// Gets or Sets Inheritable
        /// </summary>
        [DataMember(Name = "inheritable", EmitDefaultValue = false)]
        public IdentifierModelBE Inheritable { get; set; }

        /// <summary>
        /// Gets or Sets IsIncludeNameInNotice
        /// </summary>
        [DataMember(Name = "isIncludeNameInNotice", EmitDefaultValue = false)]
        public bool? IsIncludeNameInNotice { get; set; }

        /// <summary>
        /// Gets or Sets IsIncludeShortCommentsInNotice
        /// </summary>
        [DataMember(Name = "isIncludeShortCommentsInNotice", EmitDefaultValue = false)]
        public bool? IsIncludeShortCommentsInNotice { get; set; }

        /// <summary>
        /// Gets or Sets LongComments
        /// </summary>
        [DataMember(Name = "longComments", EmitDefaultValue = false)]
        public string LongComments { get; set; }

        /// <summary>
        /// Gets or Sets Name
        /// </summary>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets Priority
        /// </summary>
        [DataMember(Name = "priority", EmitDefaultValue = false)]
        public IdentifierModelBE Priority { get; set; }

        /// <summary>
        /// Gets or Sets PublicDisplayMessage
        /// </summary>
        [DataMember(Name = "publicDisplayMessage", EmitDefaultValue = false)]
        public string PublicDisplayMessage { get; set; }

        /// <summary>
        /// Gets or Sets RecordId
        /// </summary>
        [DataMember(Name = "recordId", EmitDefaultValue = false)]
        public CapIDModelBE RecordId { get; set; }

        /// <summary>
        /// Gets or Sets ResAdditionalInformationPlainText
        /// </summary>
        [DataMember(Name = "resAdditionalInformationPlainText", EmitDefaultValue = false)]
        public string ResAdditionalInformationPlainText { get; set; }

        /// <summary>
        /// Gets or Sets ResolutionAction
        /// </summary>
        [DataMember(Name = "resolutionAction", EmitDefaultValue = false)]
        public string ResolutionAction { get; set; }

        /// <summary>
        /// Gets or Sets ServiceProviderCode
        /// </summary>
        [DataMember(Name = "serviceProviderCode", EmitDefaultValue = false)]
        public string ServiceProviderCode { get; set; }

        /// <summary>
        /// Gets or Sets ServiceProviderCodes
        /// </summary>
        [DataMember(Name = "serviceProviderCodes", EmitDefaultValue = false)]
        public string ServiceProviderCodes { get; set; }

        /// <summary>
        /// Gets or Sets Severity
        /// </summary>
        [DataMember(Name = "severity", EmitDefaultValue = false)]
        public IdentifierModelBE Severity { get; set; }

        /// <summary>
        /// Gets or Sets ShortComments
        /// </summary>
        [DataMember(Name = "shortComments", EmitDefaultValue = false)]
        public string ShortComments { get; set; }

        /// <summary>
        /// Gets or Sets Status
        /// </summary>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public IdentifierModelBE Status { get; set; }

        /// <summary>
        /// Gets or Sets StatusDate
        /// </summary>
        [DataMember(Name = "statusDate", EmitDefaultValue = false)]
        public DateTime? StatusDate { get; set; }

        /// <summary>
        /// Gets or Sets StatusType
        /// </summary>
        [DataMember(Name = "statusType", EmitDefaultValue = false)]
        public string StatusType { get; set; }

        /// <summary>
        /// Gets or Sets Type
        /// </summary>
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public IdentifierModelBE Type { get; set; }
    }

    #endregion

    #region RecordAddressCustomFormsModelBE

    public partial class RecordAddressCustomFormsModelBE
    {

        public RecordAddressCustomFormsModelBE()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RecordAddressCustomFormsModel" /> class.
        /// </summary>
        /// <param name="addressLine1">The first line of the address..</param>
        /// <param name="addressLine2">The second line of the address..</param>
        /// <param name="addressTypeFlag">addressTypeFlag.</param>
        /// <param name="city">The name of the city..</param>
        /// <param name="country">country.</param>
        /// <param name="crossStreetNameStart">The beginning intersecting street name for searching. Added in Civic Platform version: 9.2.0 .</param>
        /// <param name="crossStreetNameEnd">The ending intersecting street name for searching. Added in Civic Platform version: 9.2.0 .</param>
        /// <param name="county">The name of the county..</param>
        /// <param name="customForms">customForms.</param>
        /// <param name="description">A description of the address..</param>
        /// <param name="direction">direction.</param>
        /// <param name="distance">The distance from another landmark used to locate the address..</param>
        /// <param name="houseAlphaStart">The beginning alphabetic unit in street address..</param>
        /// <param name="houseAlphaEnd">The ending alphabetic unit in street address..</param>
        /// <param name="houseFractionStart">houseFractionStart.</param>
        /// <param name="houseFractionEnd">houseFractionEnd.</param>
        /// <param name="id">The unique address id assigned by the Civic Platform server..</param>
        /// <param name="inspectionDistrict">The inspection district where the address is located..</param>
        /// <param name="inspectionDistrictPrefix">The prefix for the inspection district where the address is located..</param>
        /// <param name="isPrimary">Indicates whether or not to designate the address as the primary address. Only one address can be primary at any given time..</param>
        /// <param name="levelEnd">The ending level number (floor number) that makes up the address within a complex..</param>
        /// <param name="levelPrefix">The prefix for the level numbers (floor numbers) that make up the address..</param>
        /// <param name="levelStart">The starting level number (floor number) that makes up the address within a complex..</param>
        /// <param name="locationType">The type of location used for Right of Way Management. The valid values are configured with the LOCATION_TYPE standard choice in Civic Platform Administration. Added in Civic Platform version: 9.2.0 .</param>
        /// <param name="neighborhood">The neighborhood where the address is located..</param>
        /// <param name="neighborhoodPrefix">The prefix for neighborhood where the address is located..</param>
        /// <param name="postalCode">The postal ZIP code for the address..</param>
        /// <param name="recordId">recordId.</param>
        /// <param name="refAddressId">The reference address id..</param>
        /// <param name="secondaryStreet">This field (along with the Secondary Road Number field) displays an extra description for the location when two roads that cross or a street with two names makes up the address of the location..</param>
        /// <param name="secondaryStreetNumber">This field (along with the Secondary Road field) displays an extra description for the location when two roads that cross or a street with two names makes up the address of the location..</param>
        /// <param name="serviceProviderCode">The unique agency identifier..</param>
        /// <param name="state">state.</param>
        /// <param name="status">status.</param>
        /// <param name="streetAddress">The street address..</param>
        /// <param name="streetEnd">The ending number of a street address range..</param>
        /// <param name="streetEndFrom">The beginning number of a street end address range..</param>
        /// <param name="streetEndTo">The ending number of a street end address range..</param>
        /// <param name="streetName">The name of the street..</param>
        /// <param name="streetNameStart">The beginning street name for searching. Added in Civic Platform version: 9.2.0 .</param>
        /// <param name="streetNameEnd">The ending street name for searching. Added in Civic Platform version: 9.2.0 .</param>
        /// <param name="streetPrefix">Any part of an address that appears before a street name or number. For example, if the address is 123 West Main, \&quot;West\&quot; is the street prefix..</param>
        /// <param name="streetStart">The starting number of a street address range..</param>
        /// <param name="streetStartFrom">The beginning number of a street start address range..</param>
        /// <param name="streetStartTo">The ending number of a street start address range..</param>
        /// <param name="streetSuffix">streetSuffix.</param>
        /// <param name="streetSuffixDirection">streetSuffixDirection.</param>
        /// <param name="type">type.</param>
        /// <param name="unitStart">The starting value of a range of unit numbers..</param>
        /// <param name="unitEnd">The ending value of a range of unit numbers..</param>
        /// <param name="unitType">unitType.</param>
        /// <param name="xCoordinate">The longitudinal coordinate for this address..</param>
        /// <param name="yCoordinate">The latitudinal coordinate for this address..</param>
        public RecordAddressCustomFormsModelBE(string addressLine1 = default(string),
            string addressLine2 = default(string),
            RecordAddressModelAddressTypeFlagBE addressTypeFlag = default(RecordAddressModelAddressTypeFlagBE),
            string city = default(string), RecordAddressModelCountryBE country = default(RecordAddressModelCountryBE),
            string crossStreetNameStart = default(string), string crossStreetNameEnd = default(string),
            string county = default(string),
            List<CustomAttributeModelBE> customForms = default(List<CustomAttributeModelBE>),
            string description = default(string),
            RecordAddressModelDirectionBE direction = default(RecordAddressModelDirectionBE),
            double? distance = default(double?), string houseAlphaStart = default(string),
            string houseAlphaEnd = default(string),
            RecordAddressModelHouseFractionStartBE houseFractionStart = default(RecordAddressModelHouseFractionStartBE),
            RecordAddressModelHouseFractionEndBE houseFractionEnd = default(RecordAddressModelHouseFractionEndBE),
            long? id = default(long?), string inspectionDistrict = default(string),
            string inspectionDistrictPrefix = default(string), string isPrimary = default(string),
            string levelEnd = default(string), string levelPrefix = default(string),
            string levelStart = default(string), string locationType = default(string),
            string neighborhood = default(string), string neighborhoodPrefix = default(string),
            string postalCode = default(string), RecordIdModelBE recordId = default(RecordIdModelBE),
            long? refAddressId = default(long?), string secondaryStreet = default(string),
            decimal? secondaryStreetNumber = default(decimal?), string serviceProviderCode = default(string),
            RecordAddressModelStateBE state = default(RecordAddressModelStateBE),
            RecordAddressModelStatusBE status = default(RecordAddressModelStatusBE),
            string streetAddress = default(string),
            decimal? streetEnd = default(decimal?), long? streetEndFrom = default(long?),
            long? streetEndTo = default(long?), string streetName = default(string),
            string streetNameStart = default(string), string streetNameEnd = default(string),
            string streetPrefix = default(string), decimal? streetStart = default(decimal?),
            long? streetStartFrom = default(long?), long? streetStartTo = default(long?),
            RecordAddressModelStreetSuffixBE streetSuffix = default(RecordAddressModelStreetSuffixBE),
            RecordAddressModelStreetSuffixDirectionBE streetSuffixDirection =
                default(RecordAddressModelStreetSuffixDirectionBE),
            RecordAddressModelTypeBE type = default(RecordAddressModelTypeBE), string unitStart = default(string),
            string unitEnd = default(string),
            RecordAddressModelUnitTypeBE unitType = default(RecordAddressModelUnitTypeBE),
            double? xCoordinate = default(double?), double? yCoordinate = default(double?))
        {
            this.AddressLine1 = addressLine1;
            this.AddressLine2 = addressLine2;
            this.AddressTypeFlag = addressTypeFlag;
            this.City = city;
            this.Country = country;
            this.CrossStreetNameStart = crossStreetNameStart;
            this.CrossStreetNameEnd = crossStreetNameEnd;
            this.County = county;
            this.CustomForms = customForms;
            this.Description = description;
            this.Direction = direction;
            this.Distance = distance;
            this.HouseAlphaStart = houseAlphaStart;
            this.HouseAlphaEnd = houseAlphaEnd;
            this.HouseFractionStart = houseFractionStart;
            this.HouseFractionEnd = houseFractionEnd;
            this.Id = id;
            this.InspectionDistrict = inspectionDistrict;
            this.InspectionDistrictPrefix = inspectionDistrictPrefix;
            this.IsPrimary = isPrimary;
            this.LevelEnd = levelEnd;
            this.LevelPrefix = levelPrefix;
            this.LevelStart = levelStart;
            this.LocationType = locationType;
            this.Neighborhood = neighborhood;
            this.NeighborhoodPrefix = neighborhoodPrefix;
            this.PostalCode = postalCode;
            this.RecordId = recordId;
            this.RefAddressId = refAddressId;
            this.SecondaryStreet = secondaryStreet;
            this.SecondaryStreetNumber = secondaryStreetNumber;
            this.ServiceProviderCode = serviceProviderCode;
            this.State = state;
            this.Status = status;
            this.StreetAddress = streetAddress;
            this.StreetEnd = streetEnd;
            this.StreetEndFrom = streetEndFrom;
            this.StreetEndTo = streetEndTo;
            this.StreetName = streetName;
            this.StreetNameStart = streetNameStart;
            this.StreetNameEnd = streetNameEnd;
            this.StreetPrefix = streetPrefix;
            this.StreetStart = streetStart;
            this.StreetStartFrom = streetStartFrom;
            this.StreetStartTo = streetStartTo;
            this.StreetSuffix = streetSuffix;
            this.StreetSuffixDirection = streetSuffixDirection;
            this.Type = type;
            this.UnitStart = unitStart;
            this.UnitEnd = unitEnd;
            this.UnitType = unitType;
            this.XCoordinate = xCoordinate;
            this.YCoordinate = yCoordinate;
        }

        /// <summary>
        /// The first line of the address.
        /// </summary>
        /// <value>The first line of the address.</value>
        [DataMember(Name = "addressLine1", EmitDefaultValue = false)]
        public string AddressLine1 { get; set; }

        /// <summary>
        /// The second line of the address.
        /// </summary>
        /// <value>The second line of the address.</value>
        [DataMember(Name = "addressLine2", EmitDefaultValue = false)]
        public string AddressLine2 { get; set; }

        /// <summary>
        /// Gets or Sets AddressTypeFlag
        /// </summary>
        [DataMember(Name = "addressTypeFlag", EmitDefaultValue = false)]
        public RecordAddressModelAddressTypeFlagBE AddressTypeFlag { get; set; }

        /// <summary>
        /// The name of the city.
        /// </summary>
        /// <value>The name of the city.</value>
        [DataMember(Name = "city", EmitDefaultValue = false)]
        public string City { get; set; }

        /// <summary>
        /// Gets or Sets Country
        /// </summary>
        [DataMember(Name = "country", EmitDefaultValue = false)]
        public RecordAddressModelCountryBE Country { get; set; }

        /// <summary>
        /// The beginning intersecting street name for searching. Added in Civic Platform version: 9.2.0 
        /// </summary>
        /// <value>The beginning intersecting street name for searching. Added in Civic Platform version: 9.2.0 </value>
        [DataMember(Name = "crossStreetNameStart", EmitDefaultValue = false)]
        public string CrossStreetNameStart { get; set; }

        /// <summary>
        /// The ending intersecting street name for searching. Added in Civic Platform version: 9.2.0 
        /// </summary>
        /// <value>The ending intersecting street name for searching. Added in Civic Platform version: 9.2.0 </value>
        [DataMember(Name = "crossStreetNameEnd", EmitDefaultValue = false)]
        public string CrossStreetNameEnd { get; set; }

        /// <summary>
        /// The name of the county.
        /// </summary>
        /// <value>The name of the county.</value>
        [DataMember(Name = "county", EmitDefaultValue = false)]
        public string County { get; set; }

        /// <summary>
        /// Gets or Sets CustomForms
        /// </summary>
        [DataMember(Name = "customForms", EmitDefaultValue = false)]
        public List<CustomAttributeModelBE> CustomForms { get; set; }

        /// <summary>
        /// A description of the address.
        /// </summary>
        /// <value>A description of the address.</value>
        [DataMember(Name = "description", EmitDefaultValue = false)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or Sets Direction
        /// </summary>
        [DataMember(Name = "direction", EmitDefaultValue = false)]
        public RecordAddressModelDirectionBE Direction { get; set; }

        /// <summary>
        /// The distance from another landmark used to locate the address.
        /// </summary>
        /// <value>The distance from another landmark used to locate the address.</value>
        [DataMember(Name = "distance", EmitDefaultValue = false)]
        public double? Distance { get; set; }

        /// <summary>
        /// The beginning alphabetic unit in street address.
        /// </summary>
        /// <value>The beginning alphabetic unit in street address.</value>
        [DataMember(Name = "houseAlphaStart", EmitDefaultValue = false)]
        public string HouseAlphaStart { get; set; }

        /// <summary>
        /// The ending alphabetic unit in street address.
        /// </summary>
        /// <value>The ending alphabetic unit in street address.</value>
        [DataMember(Name = "houseAlphaEnd", EmitDefaultValue = false)]
        public string HouseAlphaEnd { get; set; }

        /// <summary>
        /// Gets or Sets HouseFractionStart
        /// </summary>
        [DataMember(Name = "houseFractionStart", EmitDefaultValue = false)]
        public RecordAddressModelHouseFractionStartBE HouseFractionStart { get; set; }

        /// <summary>
        /// Gets or Sets HouseFractionEnd
        /// </summary>
        [DataMember(Name = "houseFractionEnd", EmitDefaultValue = false)]
        public RecordAddressModelHouseFractionEndBE HouseFractionEnd { get; set; }

        /// <summary>
        /// The unique address id assigned by the Civic Platform server.
        /// </summary>
        /// <value>The unique address id assigned by the Civic Platform server.</value>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public long? Id { get; set; }

        /// <summary>
        /// The inspection district where the address is located.
        /// </summary>
        /// <value>The inspection district where the address is located.</value>
        [DataMember(Name = "inspectionDistrict", EmitDefaultValue = false)]
        public string InspectionDistrict { get; set; }

        /// <summary>
        /// The prefix for the inspection district where the address is located.
        /// </summary>
        /// <value>The prefix for the inspection district where the address is located.</value>
        [DataMember(Name = "inspectionDistrictPrefix", EmitDefaultValue = false)]
        public string InspectionDistrictPrefix { get; set; }

        /// <summary>
        /// Indicates whether or not to designate the address as the primary address. Only one address can be primary at any given time.
        /// </summary>
        /// <value>Indicates whether or not to designate the address as the primary address. Only one address can be primary at any given time.</value>
        [DataMember(Name = "isPrimary", EmitDefaultValue = false)]
        public string IsPrimary { get; set; }

        /// <summary>
        /// The ending level number (floor number) that makes up the address within a complex.
        /// </summary>
        /// <value>The ending level number (floor number) that makes up the address within a complex.</value>
        [DataMember(Name = "levelEnd", EmitDefaultValue = false)]
        public string LevelEnd { get; set; }

        /// <summary>
        /// The prefix for the level numbers (floor numbers) that make up the address.
        /// </summary>
        /// <value>The prefix for the level numbers (floor numbers) that make up the address.</value>
        [DataMember(Name = "levelPrefix", EmitDefaultValue = false)]
        public string LevelPrefix { get; set; }

        /// <summary>
        /// The starting level number (floor number) that makes up the address within a complex.
        /// </summary>
        /// <value>The starting level number (floor number) that makes up the address within a complex.</value>
        [DataMember(Name = "levelStart", EmitDefaultValue = false)]
        public string LevelStart { get; set; }

        /// <summary>
        /// The type of location used for Right of Way Management. The valid values are configured with the LOCATION_TYPE standard choice in Civic Platform Administration. Added in Civic Platform version: 9.2.0 
        /// </summary>
        /// <value>The type of location used for Right of Way Management. The valid values are configured with the LOCATION_TYPE standard choice in Civic Platform Administration. Added in Civic Platform version: 9.2.0 </value>
        [DataMember(Name = "locationType", EmitDefaultValue = false)]
        public string LocationType { get; set; }

        /// <summary>
        /// The neighborhood where the address is located.
        /// </summary>
        /// <value>The neighborhood where the address is located.</value>
        [DataMember(Name = "neighborhood", EmitDefaultValue = false)]
        public string Neighborhood { get; set; }

        /// <summary>
        /// The prefix for neighborhood where the address is located.
        /// </summary>
        /// <value>The prefix for neighborhood where the address is located.</value>
        [DataMember(Name = "neighborhoodPrefix", EmitDefaultValue = false)]
        public string NeighborhoodPrefix { get; set; }

        /// <summary>
        /// The postal ZIP code for the address.
        /// </summary>
        /// <value>The postal ZIP code for the address.</value>
        [DataMember(Name = "postalCode", EmitDefaultValue = false)]
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or Sets RecordId
        /// </summary>
        [DataMember(Name = "recordId", EmitDefaultValue = false)]
        public RecordIdModelBE RecordId { get; set; }

        /// <summary>
        /// The reference address id.
        /// </summary>
        /// <value>The reference address id.</value>
        [DataMember(Name = "refAddressId", EmitDefaultValue = false)]
        public long? RefAddressId { get; set; }

        /// <summary>
        /// This field (along with the Secondary Road Number field) displays an extra description for the location when two roads that cross or a street with two names makes up the address of the location.
        /// </summary>
        /// <value>This field (along with the Secondary Road Number field) displays an extra description for the location when two roads that cross or a street with two names makes up the address of the location.</value>
        [DataMember(Name = "secondaryStreet", EmitDefaultValue = false)]
        public string SecondaryStreet { get; set; }

        /// <summary>
        /// This field (along with the Secondary Road field) displays an extra description for the location when two roads that cross or a street with two names makes up the address of the location.
        /// </summary>
        /// <value>This field (along with the Secondary Road field) displays an extra description for the location when two roads that cross or a street with two names makes up the address of the location.</value>
        [DataMember(Name = "secondaryStreetNumber", EmitDefaultValue = false)]
        public decimal? SecondaryStreetNumber { get; set; }

        /// <summary>
        /// The unique agency identifier.
        /// </summary>
        /// <value>The unique agency identifier.</value>
        [DataMember(Name = "serviceProviderCode", EmitDefaultValue = false)]
        public string ServiceProviderCode { get; set; }

        /// <summary>
        /// Gets or Sets State
        /// </summary>
        [DataMember(Name = "state", EmitDefaultValue = false)]
        public RecordAddressModelStateBE State { get; set; }

        /// <summary>
        /// Gets or Sets Status
        /// </summary>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public RecordAddressModelStatusBE Status { get; set; }

        /// <summary>
        /// The street address.
        /// </summary>
        /// <value>The street address.</value>
        [DataMember(Name = "streetAddress", EmitDefaultValue = false)]
        public string StreetAddress { get; set; }

        /// <summary>
        /// The ending number of a street address range.
        /// </summary>
        /// <value>The ending number of a street address range.</value>
        [DataMember(Name = "streetEnd", EmitDefaultValue = false)]
        public decimal? StreetEnd { get; set; }

        /// <summary>
        /// The beginning number of a street end address range.
        /// </summary>
        /// <value>The beginning number of a street end address range.</value>
        [DataMember(Name = "streetEndFrom", EmitDefaultValue = false)]
        public long? StreetEndFrom { get; set; }

        /// <summary>
        /// The ending number of a street end address range.
        /// </summary>
        /// <value>The ending number of a street end address range.</value>
        [DataMember(Name = "streetEndTo", EmitDefaultValue = false)]
        public long? StreetEndTo { get; set; }

        /// <summary>
        /// The name of the street.
        /// </summary>
        /// <value>The name of the street.</value>
        [DataMember(Name = "streetName", EmitDefaultValue = false)]
        public string StreetName { get; set; }

        /// <summary>
        /// The beginning street name for searching. Added in Civic Platform version: 9.2.0 
        /// </summary>
        /// <value>The beginning street name for searching. Added in Civic Platform version: 9.2.0 </value>
        [DataMember(Name = "streetNameStart", EmitDefaultValue = false)]
        public string StreetNameStart { get; set; }

        /// <summary>
        /// The ending street name for searching. Added in Civic Platform version: 9.2.0 
        /// </summary>
        /// <value>The ending street name for searching. Added in Civic Platform version: 9.2.0 </value>
        [DataMember(Name = "streetNameEnd", EmitDefaultValue = false)]
        public string StreetNameEnd { get; set; }

        /// <summary>
        /// Any part of an address that appears before a street name or number. For example, if the address is 123 West Main, \&quot;West\&quot; is the street prefix.
        /// </summary>
        /// <value>Any part of an address that appears before a street name or number. For example, if the address is 123 West Main, \&quot;West\&quot; is the street prefix.</value>
        [DataMember(Name = "streetPrefix", EmitDefaultValue = false)]
        public string StreetPrefix { get; set; }

        /// <summary>
        /// The starting number of a street address range.
        /// </summary>
        /// <value>The starting number of a street address range.</value>
        [DataMember(Name = "streetStart", EmitDefaultValue = false)]
        public decimal? StreetStart { get; set; }

        /// <summary>
        /// The beginning number of a street start address range.
        /// </summary>
        /// <value>The beginning number of a street start address range.</value>
        [DataMember(Name = "streetStartFrom", EmitDefaultValue = false)]
        public long? StreetStartFrom { get; set; }

        /// <summary>
        /// The ending number of a street start address range.
        /// </summary>
        /// <value>The ending number of a street start address range.</value>
        [DataMember(Name = "streetStartTo", EmitDefaultValue = false)]
        public long? StreetStartTo { get; set; }

        /// <summary>
        /// Gets or Sets StreetSuffix
        /// </summary>
        [DataMember(Name = "streetSuffix", EmitDefaultValue = false)]
        public RecordAddressModelStreetSuffixBE StreetSuffix { get; set; }

        /// <summary>
        /// Gets or Sets StreetSuffixDirection
        /// </summary>
        [DataMember(Name = "streetSuffixDirection", EmitDefaultValue = false)]
        public RecordAddressModelStreetSuffixDirectionBE StreetSuffixDirection { get; set; }

        /// <summary>
        /// Gets or Sets Type
        /// </summary>
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public RecordAddressModelTypeBE Type { get; set; }

        /// <summary>
        /// The starting value of a range of unit numbers.
        /// </summary>
        /// <value>The starting value of a range of unit numbers.</value>
        [DataMember(Name = "unitStart", EmitDefaultValue = false)]
        public string UnitStart { get; set; }

        /// <summary>
        /// The ending value of a range of unit numbers.
        /// </summary>
        /// <value>The ending value of a range of unit numbers.</value>
        [DataMember(Name = "unitEnd", EmitDefaultValue = false)]
        public string UnitEnd { get; set; }

        /// <summary>
        /// Gets or Sets UnitType
        /// </summary>
        [DataMember(Name = "unitType", EmitDefaultValue = false)]
        public RecordAddressModelUnitTypeBE UnitType { get; set; }

        /// <summary>
        /// The longitudinal coordinate for this address.
        /// </summary>
        /// <value>The longitudinal coordinate for this address.</value>
        [DataMember(Name = "xCoordinate", EmitDefaultValue = false)]
        public double? XCoordinate { get; set; }

        /// <summary>
        /// The latitudinal coordinate for this address.
        /// </summary>
        /// <value>The latitudinal coordinate for this address.</value>
        [DataMember(Name = "yCoordinate", EmitDefaultValue = false)]
        public double? YCoordinate { get; set; }

    }

    #endregion

    #region RecordAddressModelUnitTypeBE

    /// <summary>
    /// The unit type designation of the address.
    /// </summary>
    [DataContract]
    public partial class RecordAddressModelUnitTypeBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordAddressModelUnitType" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public RecordAddressModelUnitTypeBE(string text = default(string), string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// The localized display value.
        /// </summary>
        /// <value>The localized display value.</value>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        public string Text { get; set; }

        /// <summary>
        /// The data value.
        /// </summary>
        /// <value>The data value.</value>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }

    }

    #endregion

    #region RecordAdressModelTypeBE

    /// <summary>
    /// The address type.
    /// </summary>
    [DataContract]
    public partial class RecordAddressModelTypeBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordAddressModelType" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public RecordAddressModelTypeBE(string text = default(string), string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// The localized display value.
        /// </summary>
        /// <value>The localized display value.</value>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        public string Text { get; set; }

        /// <summary>
        /// The data value.
        /// </summary>
        /// <value>The data value.</value>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }
    }

    #endregion

    #region RecordAddressModelAddressTypeFlagBE

    /// <summary>
    /// A code name or an abbreviation of the address type.
    /// </summary>
    [DataContract]
    public partial class RecordAddressModelAddressTypeFlagBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordAddressModelAddressTypeFlag" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public RecordAddressModelAddressTypeFlagBE(string text = default(string), string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// The localized display value.
        /// </summary>
        /// <value>The localized display value.</value>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        public string Text { get; set; }

        /// <summary>
        /// The data value.
        /// </summary>
        /// <value>The data value.</value>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }
    }

    #endregion

    #region RecordAddressModelDirectionBE

    /// <summary>
    /// The street direction of the primary address associated with the application.
    /// </summary>
    [DataContract]
    public partial class RecordAddressModelDirectionBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordAddressModelDirection" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public RecordAddressModelDirectionBE(string text = default(string), string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// The localized display value.
        /// </summary>
        /// <value>The localized display value.</value>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        public string Text { get; set; }

        /// <summary>
        /// The data value.
        /// </summary>
        /// <value>The data value.</value>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }
    }

    #endregion

    #region CompactAddressModelStateBE

    /// <summary>
    /// The address state.
    /// </summary>
    [DataContract]
    public partial class CompactAddressModelStateBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompactAddressModelState" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public CompactAddressModelStateBE(string text = default(string), string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// The localized display value.
        /// </summary>
        /// <value>The localized display value.</value>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        public string Text { get; set; }

        /// <summary>
        /// The data value.
        /// </summary>
        /// <value>The data value.</value>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }
    }

    #endregion

    #region CompactAddressModelBE

    /// <summary>
    /// CompactAddressModel  CompactAddressModelBE
    /// </summary>
    [DataContract]
    public partial class CompactAddressModelBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompactAddressModel" /> class.
        /// </summary>
        /// <param name="addressLine1">The first line of the address..</param>
        /// <param name="addressLine2">The second line of the address..</param>
        /// <param name="addressLine3">The third line of the address..</param>
        /// <param name="city">The name of the city..</param>
        /// <param name="country">country.</param>
        /// <param name="postalCode">The postal ZIP code for the address..</param>
        /// <param name="state">state.</param>
        public CompactAddressModelBE(string addressLine1 = default(string), string addressLine2 = default(string),
            string addressLine3 = default(string), string city = default(string),
            CompactAddressModelCountryBE country = default(CompactAddressModelCountryBE),
            string postalCode = default(string), CompactAddressModelStateBE state = default(CompactAddressModelStateBE))
        {
            this.AddressLine1 = addressLine1;
            this.AddressLine2 = addressLine2;
            this.AddressLine3 = addressLine3;
            this.City = city;
            this.Country = country;
            this.PostalCode = postalCode;
            this.State = state;
        }

        /// <summary>
        /// The first line of the address.
        /// </summary>
        /// <value>The first line of the address.</value>
        [DataMember(Name = "addressLine1", EmitDefaultValue = false)]
        public string AddressLine1 { get; set; }

        /// <summary>
        /// The second line of the address.
        /// </summary>
        /// <value>The second line of the address.</value>
        [DataMember(Name = "addressLine2", EmitDefaultValue = false)]
        public string AddressLine2 { get; set; }

        /// <summary>
        /// The third line of the address.
        /// </summary>
        /// <value>The third line of the address.</value>
        [DataMember(Name = "addressLine3", EmitDefaultValue = false)]
        public string AddressLine3 { get; set; }

        /// <summary>
        /// The name of the city.
        /// </summary>
        /// <value>The name of the city.</value>
        [DataMember(Name = "city", EmitDefaultValue = false)]
        public string City { get; set; }

        /// <summary>
        /// Gets or Sets Country
        /// </summary>
        [DataMember(Name = "country", EmitDefaultValue = false)]
        public CompactAddressModelCountryBE Country { get; set; }

        /// <summary>
        /// The postal ZIP code for the address.
        /// </summary>
        /// <value>The postal ZIP code for the address.</value>
        [DataMember(Name = "postalCode", EmitDefaultValue = false)]
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or Sets State
        /// </summary>
        [DataMember(Name = "state", EmitDefaultValue = false)]
        public CompactAddressModelStateBE State { get; set; }
    }

    #endregion

    #region CustomAttributeModelBE

    /// <summary>
    /// Contains a custom form consisting of the custom form id and custom field name and value pairs. For example in JSON, \&quot;My Custom Field\&quot;: \&quot;My Custom Value\&quot;. The custom field name and its data type are defined in Civic Platform custom forms or custom tables: &lt;br/&gt;**For a Text field**, the maximum length is 256.  &lt;br/&gt;**For a Number field**, any numeric form is allowed, including negative numbers.  &lt;br/&gt;**For a Date field**, the format is MM/dd/yyyy.  &lt;br/&gt;**For a Time field**, the format is hh:mm.  &lt;br/&gt;**For a TextArea field**, the maximum length is 4000 characters, and allows line return characters.  &lt;br/&gt;**For a DropdownList field**, the dropdown list values are in the options[] array.  &lt;br/&gt;**For a CheckBox field**, the (case-sensitive) valid values are \&quot;UNCHECKED\&quot; and \&quot;CHECKED\&quot;.  &lt;br/&gt;**For a Radio(Y/N) field**, the (case-sensitive) valid values are \&quot;Yes\&quot; and \&quot;No\&quot;.
    /// </summary>
    [DataContract]
    public partial class CustomAttributeModelBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomAttributeModel" /> class.
        /// </summary>
        /// <param name="id">The custom form id..</param>
        /// <param name="aCustomFieldName">The name of a custom field..</param>
        /// <param name="aCustomFieldValue">The value of a custom field..</param>
        public CustomAttributeModelBE(string id = default(string), string aCustomFieldName = default(string),
            string aCustomFieldValue = default(string))
        {
            this.Id = id;
            this.ACustomFieldName = aCustomFieldName;
            this.ACustomFieldValue = aCustomFieldValue;
        }

        /// <summary>
        /// The custom form id.
        /// </summary>
        /// <value>The custom form id.</value>
        [DataMember(Name = "id", EmitDefaultValue = true)]
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// The name of a custom field.
        /// </summary>
        /// <value>The name of a custom field.</value>
        [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
        public string ACustomFieldName { get; set; }

        /// <summary>
        /// The value of a custom field.
        /// </summary>
        /// <value>The value of a custom field.</value>
        [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
        public  string ACustomFieldValue { get; set; }
    }



    #endregion

    #region RecordAPOCustomFormsModelConstructionTypeBE

    /// <summary>
    /// The US Census Bureau construction type code. See [Get All Record Construction Types](./api-settings.html#operation/v4.get.settings.records.constructionTypes).
    /// </summary>
    [DataContract]
    public partial class RecordAPOCustomFormsModelConstructionTypeBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordAPOCustomFormsModelConstructionType" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public RecordAPOCustomFormsModelConstructionTypeBE(string text = default(string),
            string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// The localized display value.
        /// </summary>
        /// <value>The localized display value.</value>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        public string Text { get; set; }

        /// <summary>
        /// The data value.
        /// </summary>
        /// <value>The data value.</value>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }
    }

    #endregion

    # region RecordAPOCustomFormsModelStatusReasonBE

    /// <summary>
    ///  The reason for the status setting on the record.
    /// </summary>
    [DataContract]
    public partial class RecordAPOCustomFormsModelStatusReasonBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordAPOCustomFormsModelStatusReason" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public RecordAPOCustomFormsModelStatusReasonBE(string text = default(string), string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// The localized display value.
        /// </summary>
        /// <value>The localized display value.</value>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        public string Text { get; set; }

        /// <summary>
        /// The data value.
        /// </summary>
        /// <value>The data value.</value>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }
    }

    #endregion

    #region RecordAPOCustomFormsModelStatusBE

    /// <summary>
    /// The record status.
    /// </summary>
    [DataContract]
    public partial class RecordAPOCustomFormsModelStatusBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordAPOCustomFormsModelStatus" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public RecordAPOCustomFormsModelStatusBE(string text = default(string), string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// The localized display value.
        /// </summary>
        /// <value>The localized display value.</value>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        public string Text { get; set; }

        /// <summary>
        /// The data value.
        /// </summary>
        /// <value>The data value.</value>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }
    }

    #endregion

    #region RecordAPOCustomFormsModelSeverityBE

    /// <summary>
    /// Indicates the severity of the condition.
    /// </summary>
    [DataContract]
    public partial class RecordAPOCustomFormsModelSeverityBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordAPOCustomFormsModelSeverity" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public RecordAPOCustomFormsModelSeverityBE(string text = default(string), string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// The localized display value.
        /// </summary>
        /// <value>The localized display value.</value>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        public string Text { get; set; }

        /// <summary>
        /// The data value.
        /// </summary>
        /// <value>The data value.</value>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }
    }

    #endregion

    #region RecordAPOCustomFormsModelReportedTypeBE

    /// <summary>
    /// The type of complaint or incident being reported.
    /// </summary>
    [DataContract]
    public partial class RecordAPOCustomFormsModelReportedTypeBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordAPOCustomFormsModelReportedType" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public RecordAPOCustomFormsModelReportedTypeBE(string text = default(string), string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// The localized display value.
        /// </summary>
        /// <value>The localized display value.</value>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        public string Text { get; set; }

        /// <summary>
        /// The data value.
        /// </summary>
        /// <value>The data value.</value>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }
    }

    #endregion

    #region RecordAPOCustomFormsModelReportedChannelBE

    /// <summary>
    /// The incoming channel through which the applicant submitted the application.
    /// </summary>
    [DataContract]
    public partial class RecordAPOCustomFormsModelReportedChannelBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordAPOCustomFormsModelReportedChannel" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public RecordAPOCustomFormsModelReportedChannelBE(string text = default(string), string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// The localized display value.
        /// </summary>
        /// <value>The localized display value.</value>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        public string Text { get; set; }

        /// <summary>
        /// The data value.
        /// </summary>
        /// <value>The data value.</value>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }
    }

    #endregion

    #region RecordExpirationModelBE

    /// <summary>
    /// RecordExpirationModel
    /// </summary>
    [DataContract]
    public partial class RecordExpirationModelBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordExpirationModel" /> class.
        /// </summary>
        /// <param name="expirationDate">The date when the condition expires..</param>
        /// <param name="expirationStatus">expirationStatus.</param>
        public RecordExpirationModelBE(DateTime? expirationDate = default(DateTime?),
            RecordExpirationModelExpirationStatusBE expirationStatus = default(RecordExpirationModelExpirationStatusBE))
        {
            this.ExpirationDate = expirationDate;
            this.ExpirationStatus = expirationStatus;
        }

        /// <summary>
        /// The date when the condition expires.
        /// </summary>
        /// <value>The date when the condition expires.</value>
        [DataMember(Name = "expirationDate", EmitDefaultValue = false)]
        public DateTime? ExpirationDate { get; set; }

        /// <summary>
        /// Gets or Sets ExpirationStatus
        /// </summary>
        [DataMember(Name = "expirationStatus", EmitDefaultValue = false)]
        public RecordExpirationModelExpirationStatusBE ExpirationStatus { get; set; }
    }

    #endregion

    #region RecordExpirationModelExpirationStatusBE

    /// <summary>
    /// Indicates whether the expiration is enabled or disabled. See [Get All Record Expiration Statuses](./api-settings.html#operation/v4.get.settings.records.expirationStatuses).
    /// </summary>
    [DataContract]
    public partial class RecordExpirationModelExpirationStatusBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordExpirationModelExpirationStatus" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public RecordExpirationModelExpirationStatusBE(string text = default(string), string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// The localized display value.
        /// </summary>
        /// <value>The localized display value.</value>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        public string Text { get; set; }

        /// <summary>
        /// The data value.
        /// </summary>
        /// <value>The data value.</value>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }
    }

    #endregion

    #region LicenseProfessionalModelBE

    /// <summary>
    /// LicenseProfessionalModel
    /// </summary>
    [DataContract]
    public partial class LicenseProfessionalModelBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LicenseProfessionalModel" /> class.
        /// </summary>
        /// <param name="addressLine1">The first line of the address..</param>
        /// <param name="addressLine2">The second line of the address..</param>
        /// <param name="addressLine3">The third line of the address..</param>
        /// <param name="birthDate">The birth date of an individual..</param>
        /// <param name="businessLicense">The official business license number issued by an agency. A licensed professional can have the same license number assigned to multiple license types..</param>
        /// <param name="businessName">A business name for the applicable individual..</param>
        /// <param name="businessName2">A secondary business name for the applicable individual..</param>
        /// <param name="city">The name of the city..</param>
        /// <param name="comment">Comments or notes about the current context..</param>
        /// <param name="country">country.</param>
        /// <param name="email">The contact&#39;s email address..</param>
        /// <param name="expirationDate">The license expiration date..</param>
        /// <param name="fax">The fax number for the contact..</param>
        /// <param name="federalEmployerId">The Federal Employer Identification Number. It is used to identify a business for tax purposes..</param>
        /// <param name="firstName">The licensed professional&#39;s first name. .</param>
        /// <param name="fullName">The licensed professional&#39;s full name. .</param>
        /// <param name="gender">gender.</param>
        /// <param name="id">The licensed professional system id assigned by the Civic Platform server..</param>
        /// <param name="isPrimary">Indicates whether or not to designate the professional as the primary professional..</param>
        /// <param name="lastName">The licensed professional&#39;s last name. .</param>
        /// <param name="lastRenewalDate">The last date for a professionals renewal license..</param>
        /// <param name="licenseNumber">The licensed professional&#39;s license number..</param>
        /// <param name="licenseType">licenseType.</param>
        /// <param name="licensingBoard">licensingBoard.</param>
        /// <param name="middleName">The licensed professional&#39;s middle name. .</param>
        /// <param name="originalIssueDate">The original issuance date of license..</param>
        /// <param name="phone1">The primary phone number of the contact..</param>
        /// <param name="phone2">The secondary phone number of the contact..</param>
        /// <param name="phone3">The tertiary phone number for the contact. .</param>
        /// <param name="postOfficeBox">The post office box number..</param>
        /// <param name="postalCode">The postal ZIP code for the address..</param>
        /// <param name="recordId">recordId.</param>
        /// <param name="referenceLicenseId">The unique Id generated for a professional stored in the system..</param>
        /// <param name="salutation">salutation.</param>
        /// <param name="serviceProviderCode">The unique agency identifier..</param>
        /// <param name="state">state.</param>
        /// <param name="suffix">The licensed professional&#39;s name suffix..</param>
        /// <param name="title">The individual&#39;s professional title..</param>
        public LicenseProfessionalModelBE(string addressLine1 = default(string), string addressLine2 = default(string),
            string addressLine3 = default(string), DateTime? birthDate = default(DateTime?),
            string businessLicense = default(string), string businessName = default(string),
            string businessName2 = default(string), string city = default(string), string comment = default(string),
            CompactAddressModelCountryBE country = default(CompactAddressModelCountryBE),
            string email = default(string), DateTime? expirationDate = default(DateTime?), string fax = default(string),
            string federalEmployerId = default(string), string firstName = default(string),
            string fullName = default(string),
            RecordContactSimpleModelGenderBE gender = default(RecordContactSimpleModelGenderBE),
            string id = default(string), string isPrimary = default(string), string lastName = default(string),
            DateTime? lastRenewalDate = default(DateTime?), string licenseNumber = default(string),
            LicenseProfessionalModelLicenseTypeBE licenseType = default(LicenseProfessionalModelLicenseTypeBE),
            LicenseProfessionalModelLicensingBoardBE licensingBoard = default(LicenseProfessionalModelLicensingBoardBE),
            string middleName = default(string), DateTime? originalIssueDate = default(DateTime?),
            string phone1 = default(string), string phone2 = default(string), string phone3 = default(string),
            string postOfficeBox = default(string), string postalCode = default(string),
            RecordIdModelBE recordId = default(RecordIdModelBE), string referenceLicenseId = default(string),
            LicenseProfessionalModelSalutationBE salutation = default(LicenseProfessionalModelSalutationBE),
            string serviceProviderCode = default(string),
            LicenseProfessionalModelStateBE state = default(LicenseProfessionalModelStateBE),
            string suffix = default(string), string title = default(string))
        {
            this.AddressLine1 = addressLine1;
            this.AddressLine2 = addressLine2;
            this.AddressLine3 = addressLine3;
            this.BirthDate = birthDate;
            this.BusinessLicense = businessLicense;
            this.BusinessName = businessName;
            this.BusinessName2 = businessName2;
            this.City = city;
            this.Comment = comment;
            this.Country = country;
            this.Email = email;
            this.ExpirationDate = expirationDate;
            this.Fax = fax;
            this.FederalEmployerId = federalEmployerId;
            this.FirstName = firstName;
            this.FullName = fullName;
            this.Gender = gender;
            this.Id = id;
            this.IsPrimary = isPrimary;
            this.LastName = lastName;
            this.LastRenewalDate = lastRenewalDate;
            this.LicenseNumber = licenseNumber;
            this.LicenseType = licenseType;
            this.LicensingBoard = licensingBoard;
            this.MiddleName = middleName;
            this.OriginalIssueDate = originalIssueDate;
            this.Phone1 = phone1;
            this.Phone2 = phone2;
            this.Phone3 = phone3;
            this.PostOfficeBox = postOfficeBox;
            this.PostalCode = postalCode;
            this.RecordId = recordId;
            this.ReferenceLicenseId = referenceLicenseId;
            this.Salutation = salutation;
            this.ServiceProviderCode = serviceProviderCode;
            this.State = state;
            this.Suffix = suffix;
            this.Title = title;
        }

        /// <summary>
        /// The first line of the address.
        /// </summary>
        /// <value>The first line of the address.</value>
        [DataMember(Name = "addressLine1", EmitDefaultValue = false)]
        public string AddressLine1 { get; set; }

        /// <summary>
        /// The second line of the address.
        /// </summary>
        /// <value>The second line of the address.</value>
        [DataMember(Name = "addressLine2", EmitDefaultValue = false)]
        public string AddressLine2 { get; set; }

        /// <summary>
        /// The third line of the address.
        /// </summary>
        /// <value>The third line of the address.</value>
        [DataMember(Name = "addressLine3", EmitDefaultValue = false)]
        public string AddressLine3 { get; set; }

        /// <summary>
        /// The birth date of an individual.
        /// </summary>
        /// <value>The birth date of an individual.</value>
        [DataMember(Name = "birthDate", EmitDefaultValue = false)]
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// The official business license number issued by an agency. A licensed professional can have the same license number assigned to multiple license types.
        /// </summary>
        /// <value>The official business license number issued by an agency. A licensed professional can have the same license number assigned to multiple license types.</value>
        [DataMember(Name = "businessLicense", EmitDefaultValue = false)]
        public string BusinessLicense { get; set; }

        /// <summary>
        /// A business name for the applicable individual.
        /// </summary>
        /// <value>A business name for the applicable individual.</value>
        [DataMember(Name = "businessName", EmitDefaultValue = false)]
        public string BusinessName { get; set; }

        /// <summary>
        /// A secondary business name for the applicable individual.
        /// </summary>
        /// <value>A secondary business name for the applicable individual.</value>
        [DataMember(Name = "businessName2", EmitDefaultValue = false)]
        public string BusinessName2 { get; set; }

        /// <summary>
        /// The name of the city.
        /// </summary>
        /// <value>The name of the city.</value>
        [DataMember(Name = "city", EmitDefaultValue = false)]
        public string City { get; set; }

        /// <summary>
        /// Comments or notes about the current context.
        /// </summary>
        /// <value>Comments or notes about the current context.</value>
        [DataMember(Name = "comment", EmitDefaultValue = false)]
        public string Comment { get; set; }

        /// <summary>
        /// Gets or Sets Country
        /// </summary>
        [DataMember(Name = "country", EmitDefaultValue = false)]
        public CompactAddressModelCountryBE Country { get; set; }

        /// <summary>
        /// The contact&#39;s email address.
        /// </summary>
        /// <value>The contact&#39;s email address.</value>
        [DataMember(Name = "email", EmitDefaultValue = false)]
        public string Email { get; set; }

        /// <summary>
        /// The license expiration date.
        /// </summary>
        /// <value>The license expiration date.</value>
        [DataMember(Name = "expirationDate", EmitDefaultValue = false)]
        public DateTime? ExpirationDate { get; set; }

        /// <summary>
        /// The fax number for the contact.
        /// </summary>
        /// <value>The fax number for the contact.</value>
        [DataMember(Name = "fax", EmitDefaultValue = false)]
        public string Fax { get; set; }

        /// <summary>
        /// The Federal Employer Identification Number. It is used to identify a business for tax purposes.
        /// </summary>
        /// <value>The Federal Employer Identification Number. It is used to identify a business for tax purposes.</value>
        [DataMember(Name = "federalEmployerId", EmitDefaultValue = false)]
        public string FederalEmployerId { get; set; }

        /// <summary>
        /// The licensed professional&#39;s first name. 
        /// </summary>
        /// <value>The licensed professional&#39;s first name. </value>
        [DataMember(Name = "firstName", EmitDefaultValue = false)]
        public string FirstName { get; set; }

        /// <summary>
        /// The licensed professional&#39;s full name. 
        /// </summary>
        /// <value>The licensed professional&#39;s full name. </value>
        [DataMember(Name = "fullName", EmitDefaultValue = false)]
        public string FullName { get; set; }

        /// <summary>
        /// Gets or Sets Gender
        /// </summary>
        [DataMember(Name = "gender", EmitDefaultValue = false)]
        public RecordContactSimpleModelGenderBE Gender { get; set; }

        /// <summary>
        /// The licensed professional system id assigned by the Civic Platform server.
        /// </summary>
        /// <value>The licensed professional system id assigned by the Civic Platform server.</value>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }

        /// <summary>
        /// Indicates whether or not to designate the professional as the primary professional.
        /// </summary>
        /// <value>Indicates whether or not to designate the professional as the primary professional.</value>
        [DataMember(Name = "isPrimary", EmitDefaultValue = false)]
        public string IsPrimary { get; set; }

        /// <summary>
        /// The licensed professional&#39;s last name. 
        /// </summary>
        /// <value>The licensed professional&#39;s last name. </value>
        [DataMember(Name = "lastName", EmitDefaultValue = false)]
        public string LastName { get; set; }

        /// <summary>
        /// The last date for a professionals renewal license.
        /// </summary>
        /// <value>The last date for a professionals renewal license.</value>
        [DataMember(Name = "lastRenewalDate", EmitDefaultValue = false)]
        public DateTime? LastRenewalDate { get; set; }

        /// <summary>
        /// The licensed professional&#39;s license number.
        /// </summary>
        /// <value>The licensed professional&#39;s license number.</value>
        [DataMember(Name = "licenseNumber", EmitDefaultValue = false)]
        public string LicenseNumber { get; set; }

        /// <summary>
        /// Gets or Sets LicenseType
        /// </summary>
        [DataMember(Name = "licenseType", EmitDefaultValue = false)]
        public LicenseProfessionalModelLicenseTypeBE LicenseType { get; set; }

        /// <summary>
        /// Gets or Sets LicensingBoard
        /// </summary>
        [DataMember(Name = "licensingBoard", EmitDefaultValue = false)]
        public LicenseProfessionalModelLicensingBoardBE LicensingBoard { get; set; }

        /// <summary>
        /// The licensed professional&#39;s middle name. 
        /// </summary>
        /// <value>The licensed professional&#39;s middle name. </value>
        [DataMember(Name = "middleName", EmitDefaultValue = false)]
        public string MiddleName { get; set; }

        /// <summary>
        /// The original issuance date of license.
        /// </summary>
        /// <value>The original issuance date of license.</value>
        [DataMember(Name = "originalIssueDate", EmitDefaultValue = false)]
        public DateTime? OriginalIssueDate { get; set; }

        /// <summary>
        /// The primary phone number of the contact.
        /// </summary>
        /// <value>The primary phone number of the contact.</value>
        [DataMember(Name = "phone1", EmitDefaultValue = false)]
        public string Phone1 { get; set; }

        /// <summary>
        /// The secondary phone number of the contact.
        /// </summary>
        /// <value>The secondary phone number of the contact.</value>
        [DataMember(Name = "phone2", EmitDefaultValue = false)]
        public string Phone2 { get; set; }

        /// <summary>
        /// The tertiary phone number for the contact. 
        /// </summary>
        /// <value>The tertiary phone number for the contact. </value>
        [DataMember(Name = "phone3", EmitDefaultValue = false)]
        public string Phone3 { get; set; }

        /// <summary>
        /// The post office box number.
        /// </summary>
        /// <value>The post office box number.</value>
        [DataMember(Name = "postOfficeBox", EmitDefaultValue = false)]
        public string PostOfficeBox { get; set; }

        /// <summary>
        /// The postal ZIP code for the address.
        /// </summary>
        /// <value>The postal ZIP code for the address.</value>
        [DataMember(Name = "postalCode", EmitDefaultValue = false)]
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or Sets RecordId
        /// </summary>
        [DataMember(Name = "recordId", EmitDefaultValue = false)]
        public RecordIdModelBE RecordId { get; set; }

        /// <summary>
        /// The unique Id generated for a professional stored in the system.
        /// </summary>
        /// <value>The unique Id generated for a professional stored in the system.</value>
        [DataMember(Name = "referenceLicenseId", EmitDefaultValue = false)]
        public string ReferenceLicenseId { get; set; }

        /// <summary>
        /// Gets or Sets Salutation
        /// </summary>
        [DataMember(Name = "salutation", EmitDefaultValue = false)]
        public LicenseProfessionalModelSalutationBE Salutation { get; set; }

        /// <summary>
        /// The unique agency identifier.
        /// </summary>
        /// <value>The unique agency identifier.</value>
        [DataMember(Name = "serviceProviderCode", EmitDefaultValue = false)]
        public string ServiceProviderCode { get; set; }

        /// <summary>
        /// Gets or Sets State
        /// </summary>
        [DataMember(Name = "state", EmitDefaultValue = false)]
        public LicenseProfessionalModelStateBE State { get; set; }

        /// <summary>
        /// The licensed professional&#39;s name suffix.
        /// </summary>
        /// <value>The licensed professional&#39;s name suffix.</value>
        [DataMember(Name = "suffix", EmitDefaultValue = false)]
        public string Suffix { get; set; }

        /// <summary>
        /// The individual&#39;s professional title.
        /// </summary>
        /// <value>The individual&#39;s professional title.</value>
        [DataMember(Name = "title", EmitDefaultValue = false)]
        public string Title { get; set; }
    }

    #endregion

    #region LicenseProfessionalModelLicenseTypeBE

    /// <summary>
    /// The type of license held by the professional.
    /// </summary>
    [DataContract]
    public partial class LicenseProfessionalModelLicenseTypeBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LicenseProfessionalModelLicenseType" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public LicenseProfessionalModelLicenseTypeBE(string text = default(string), string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// The localized display value.
        /// </summary>
        /// <value>The localized display value.</value>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        public string Text { get; set; }

        /// <summary>
        /// The data value.
        /// </summary>
        /// <value>The data value.</value>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }
    }

    #endregion

    #region LicenseProfessionalModelLicensingBoardBE

    /// <summary>
    /// The name of the licensing board that issued the license.
    /// </summary>
    [DataContract]
    public partial class LicenseProfessionalModelLicensingBoardBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LicenseProfessionalModelLicensingBoard" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public LicenseProfessionalModelLicensingBoardBE(string text = default(string), string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// The localized display value.
        /// </summary>
        /// <value>The localized display value.</value>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        public string Text { get; set; }

        /// <summary>
        /// The data value.
        /// </summary>
        /// <value>The data value.</value>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }
    }

    #endregion

    #region LicenseProfessionalModelSalutationBE

    /// <summary>
    /// The salutation to be used when addressing the contact; for example Mr. or Ms. This field is active only when Contact Type &#x3D; Individual.
    /// </summary>
    [DataContract]
    public partial class LicenseProfessionalModelSalutationBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LicenseProfessionalModelSalutation" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public LicenseProfessionalModelSalutationBE(string text = default(string), string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// The localized display value.
        /// </summary>
        /// <value>The localized display value.</value>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        public string Text { get; set; }

        /// <summary>
        /// The data value.
        /// </summary>
        /// <value>The data value.</value>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }
    }

    #endregion

    #region LicenseProfessionalModelStateBE

    /// <summary>
    /// The state corresponding to the address on record.
    /// </summary>
    [DataContract]
    public partial class LicenseProfessionalModelStateBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LicenseProfessionalModelState" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public LicenseProfessionalModelStateBE(string text = default(string), string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// The localized display value.
        /// </summary>
        /// <value>The localized display value.</value>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        public string Text { get; set; }

        /// <summary>
        /// The data value.
        /// </summary>
        /// <value>The data value.</value>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }
    }

    #endregion

    #region RecordAPOCustomFormsModelPriorityBE

    /// <summary>
    /// The priority level assigned to the record. See [Get All Priorities](./api-settings.html#operation/v4.get.settings.priorities).
    /// </summary>
    [DataContract]
    public partial class RecordAPOCustomFormsModelPriorityBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordAPOCustomFormsModelPriority" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public RecordAPOCustomFormsModelPriorityBE(string text = default(string), string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// The localized display value.
        /// </summary>
        /// <value>The localized display value.</value>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        public string Text { get; set; }

        /// <summary>
        /// The data value.
        /// </summary>
        /// <value>The data value.</value>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }
    }

    #endregion

    #region ParcelModel1BE

    /// <summary>
    /// ParcelModel1
    /// </summary>
    [DataContract]
    public partial class ParcelModel1BE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParcelModel1" /> class.
        /// </summary>
        /// <param name="block">block.</param>
        /// <param name="book">book.</param>
        /// <param name="censusTract">censusTract.</param>
        /// <param name="councilDistrict">councilDistrict.</param>
        /// <param name="exemptionValue">exemptionValue.</param>
        /// <param name="gisSequenceNumber">gisSequenceNumber.</param>
        /// <param name="id">id.</param>
        /// <param name="improvedValue">improvedValue.</param>
        /// <param name="isPrimary">isPrimary.</param>
        /// <param name="landValue">landValue.</param>
        /// <param name="legalDescription">legalDescription.</param>
        /// <param name="lot">lot.</param>
        /// <param name="mapNumber">mapNumber.</param>
        /// <param name="mapReferenceInfo">mapReferenceInfo.</param>
        /// <param name="page">page.</param>
        /// <param name="parcel">parcel.</param>
        /// <param name="parcelArea">parcelArea.</param>
        /// <param name="parcelNumber">parcelNumber.</param>
        /// <param name="planArea">planArea.</param>
        /// <param name="range">range.</param>
        /// <param name="section">section.</param>
        /// <param name="status">status.</param>
        /// <param name="subdivision">subdivision.</param>
        /// <param name="supervisorDistrict">supervisorDistrict.</param>
        /// <param name="township">township.</param>
        /// <param name="tract">tract.</param>
        public ParcelModel1BE(string block = default(string), string book = default(string),
            string censusTract = default(string), string councilDistrict = default(string),
            double? exemptionValue = default(double?), long? gisSequenceNumber = default(long?),
            string id = default(string), double? improvedValue = default(double?), string isPrimary = default(string),
            double? landValue = default(double?), string legalDescription = default(string),
            string lot = default(string), string mapNumber = default(string), string mapReferenceInfo = default(string),
            string page = default(string), string parcel = default(string), double? parcelArea = default(double?),
            string parcelNumber = default(string), string planArea = default(string), string range = default(string),
            long? section = default(long?), IdentifierModelBE status = default(IdentifierModelBE),
            IdentifierModelBE subdivision = default(IdentifierModelBE), string supervisorDistrict = default(string),
            string township = default(string), string tract = default(string))
        {
            this.Block = block;
            this.Book = book;
            this.CensusTract = censusTract;
            this.CouncilDistrict = councilDistrict;
            this.ExemptionValue = exemptionValue;
            this.GisSequenceNumber = gisSequenceNumber;
            this.Id = id;
            this.ImprovedValue = improvedValue;
            this.IsPrimary = isPrimary;
            this.LandValue = landValue;
            this.LegalDescription = legalDescription;
            this.Lot = lot;
            this.MapNumber = mapNumber;
            this.MapReferenceInfo = mapReferenceInfo;
            this.Page = page;
            this.Parcel = parcel;
            this.ParcelArea = parcelArea;
            this.ParcelNumber = parcelNumber;
            this.PlanArea = planArea;
            this.Range = range;
            this.Section = section;
            this.Status = status;
            this.Subdivision = subdivision;
            this.SupervisorDistrict = supervisorDistrict;
            this.Township = township;
            this.Tract = tract;
        }

        /// <summary>
        /// Gets or Sets Block
        /// </summary>
        [DataMember(Name = "block", EmitDefaultValue = false)]
        public string Block { get; set; }

        /// <summary>
        /// Gets or Sets Book
        /// </summary>
        [DataMember(Name = "book", EmitDefaultValue = false)]
        public string Book { get; set; }

        /// <summary>
        /// Gets or Sets CensusTract
        /// </summary>
        [DataMember(Name = "censusTract", EmitDefaultValue = false)]
        public string CensusTract { get; set; }

        /// <summary>
        /// Gets or Sets CouncilDistrict
        /// </summary>
        [DataMember(Name = "councilDistrict", EmitDefaultValue = false)]
        public string CouncilDistrict { get; set; }

        /// <summary>
        /// Gets or Sets ExemptionValue
        /// </summary>
        [DataMember(Name = "exemptionValue", EmitDefaultValue = false)]
        public double? ExemptionValue { get; set; }

        /// <summary>
        /// Gets or Sets GisSequenceNumber
        /// </summary>
        [DataMember(Name = "gisSequenceNumber", EmitDefaultValue = false)]
        public long? GisSequenceNumber { get; set; }

        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }

        /// <summary>
        /// Gets or Sets ImprovedValue
        /// </summary>
        [DataMember(Name = "improvedValue", EmitDefaultValue = false)]
        public double? ImprovedValue { get; set; }

        /// <summary>
        /// Gets or Sets IsPrimary
        /// </summary>
        [DataMember(Name = "isPrimary", EmitDefaultValue = false)]
        public string IsPrimary { get; set; }

        /// <summary>
        /// Gets or Sets LandValue
        /// </summary>
        [DataMember(Name = "landValue", EmitDefaultValue = false)]
        public double? LandValue { get; set; }

        /// <summary>
        /// Gets or Sets LegalDescription
        /// </summary>
        [DataMember(Name = "legalDescription", EmitDefaultValue = false)]
        public string LegalDescription { get; set; }

        /// <summary>
        /// Gets or Sets Lot
        /// </summary>
        [DataMember(Name = "lot", EmitDefaultValue = false)]
        public string Lot { get; set; }

        /// <summary>
        /// Gets or Sets MapNumber
        /// </summary>
        [DataMember(Name = "mapNumber", EmitDefaultValue = false)]
        public string MapNumber { get; set; }

        /// <summary>
        /// Gets or Sets MapReferenceInfo
        /// </summary>
        [DataMember(Name = "mapReferenceInfo", EmitDefaultValue = false)]
        public string MapReferenceInfo { get; set; }

        /// <summary>
        /// Gets or Sets Page
        /// </summary>
        [DataMember(Name = "page", EmitDefaultValue = false)]
        public string Page { get; set; }

        /// <summary>
        /// Gets or Sets Parcel
        /// </summary>
        [DataMember(Name = "parcel", EmitDefaultValue = false)]
        public string Parcel { get; set; }

        /// <summary>
        /// Gets or Sets ParcelArea
        /// </summary>
        [DataMember(Name = "parcelArea", EmitDefaultValue = false)]
        public double? ParcelArea { get; set; }

        /// <summary>
        /// Gets or Sets ParcelNumber
        /// </summary>
        [DataMember(Name = "parcelNumber", EmitDefaultValue = false)]
        public string ParcelNumber { get; set; }

        /// <summary>
        /// Gets or Sets PlanArea
        /// </summary>
        [DataMember(Name = "planArea", EmitDefaultValue = false)]
        public string PlanArea { get; set; }

        /// <summary>
        /// Gets or Sets Range
        /// </summary>
        [DataMember(Name = "range", EmitDefaultValue = false)]
        public string Range { get; set; }

        /// <summary>
        /// Gets or Sets Section
        /// </summary>
        [DataMember(Name = "section", EmitDefaultValue = false)]
        public long? Section { get; set; }

        /// <summary>
        /// Gets or Sets Status
        /// </summary>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public IdentifierModelBE Status { get; set; }

        /// <summary>
        /// Gets or Sets Subdivision
        /// </summary>
        [DataMember(Name = "subdivision", EmitDefaultValue = false)]
        public IdentifierModelBE Subdivision { get; set; }

        /// <summary>
        /// Gets or Sets SupervisorDistrict
        /// </summary>
        [DataMember(Name = "supervisorDistrict", EmitDefaultValue = false)]
        public string SupervisorDistrict { get; set; }

        /// <summary>
        /// Gets or Sets Township
        /// </summary>
        [DataMember(Name = "township", EmitDefaultValue = false)]
        public string Township { get; set; }

        /// <summary>
        /// Gets or Sets Tract
        /// </summary>
        [DataMember(Name = "tract", EmitDefaultValue = false)]
        public string Tract { get; set; }
    }

    #endregion

    #region RecordAddressModelCountryBE

    /// <summary>
    /// The name of the country. See [Get All Address Countries](./api-settings.html#operation/v4.get.settings.addresses.countries).
    /// </summary>
    [DataContract]
    public partial class RecordAddressModelCountryBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordAddressModelCountry" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public RecordAddressModelCountryBE(string text = default(string), string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// The localized display value.
        /// </summary>
        /// <value>The localized display value.</value>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        public string Text { get; set; }

        /// <summary>
        /// The data value.
        /// </summary>
        /// <value>The data value.</value>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }
    }

    #endregion

    #region RecordAddressModelHouseFractionStartBE

    /// <summary>
    /// Beginning fraction value used in combination with the Street number fields.
    /// </summary>
    [DataContract]
    public partial class RecordAddressModelHouseFractionStartBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordAddressModelHouseFractionStart" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public RecordAddressModelHouseFractionStartBE(string text = default(string), string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// The localized display value.
        /// </summary>
        /// <value>The localized display value.</value>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        public string Text { get; set; }

        /// <summary>
        /// The data value.
        /// </summary>
        /// <value>The data value.</value>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }
    }

    #endregion

    #region RecordAddressModelHouseFractionEndBE

    /// <summary>
    /// Ending franction value used in combination with the Street number fields.
    /// </summary>
    [DataContract]
    public partial class RecordAddressModelHouseFractionEndBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordAddressModelHouseFractionEnd" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public RecordAddressModelHouseFractionEndBE(string text = default(string), string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// The localized display value.
        /// </summary>
        /// <value>The localized display value.</value>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        public string Text { get; set; }

        /// <summary>
        /// The data value.
        /// </summary>
        /// <value>The data value.</value>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }
    }

    #endregion

    #region RecordAddressModelStreetSuffixDirectionBE

    /// <summary>
    /// The direction appended to the street suffix. For example, if the address is 500 56th Avenue NW, \&quot;NW\&quot; is the street suffix direction.
    /// </summary>
    [DataContract]
    public partial class RecordAddressModelStreetSuffixDirectionBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordAddressModelStreetSuffixDirection" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public RecordAddressModelStreetSuffixDirectionBE(string text = default(string), string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// The localized display value.
        /// </summary>
        /// <value>The localized display value.</value>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        public string Text { get; set; }

        /// <summary>
        /// The data value.
        /// </summary>
        /// <value>The data value.</value>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }
    }

    #endregion

    #region RecordAddressModelStreetSuffixBE

    /// <summary>
    /// The type of street such as \&quot;Lane\&quot; or \&quot;Boulevard\&quot;.
    /// </summary>
    [DataContract]
    public partial class RecordAddressModelStreetSuffixBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordAddressModelStreetSuffix" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public RecordAddressModelStreetSuffixBE(string text = default(string), string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// The localized display value.
        /// </summary>
        /// <value>The localized display value.</value>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        public string Text { get; set; }

        /// <summary>
        /// The data value.
        /// </summary>
        /// <value>The data value.</value>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }
    }

    #endregion

    #region RecordAddressModelStatusBE

    /// <summary>
    /// The address status indicating whether the address is active or inactive.
    /// </summary>
    [DataContract]
    public partial class RecordAddressModelStatusBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordAddressModelStatus" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public RecordAddressModelStatusBE(string text = default(string), string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// The localized display value.
        /// </summary>
        /// <value>The localized display value.</value>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        public string Text { get; set; }

        /// <summary>
        /// The data value.
        /// </summary>
        /// <value>The data value.</value>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }
    }

    #endregion

    #region RecordAddressModelStateBE

    /// <summary>
    /// The name of the state.
    /// </summary>
    [DataContract]
    public partial class RecordAddressModelStateBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordAddressModelState" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public RecordAddressModelStateBE(string text = default(string), string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// The localized display value.
        /// </summary>
        /// <value>The localized display value.</value>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        public string Text { get; set; }

        /// <summary>
        /// The data value.
        /// </summary>
        /// <value>The data value.</value>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }
    }

    #endregion

    #region RecordContactSimpleModelGenderBE

    /// <summary>
    /// The gender (male or female) of the individual.
    /// </summary>
    [DataContract]
    public partial class RecordContactSimpleModelGenderBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordContactSimpleModelGender" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public RecordContactSimpleModelGenderBE(string text = default(string), string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// The localized display value.
        /// </summary>
        /// <value>The localized display value.</value>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        public string Text { get; set; }

        /// <summary>
        /// The data value.
        /// </summary>
        /// <value>The data value.</value>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }
    }

    #endregion

    #region RecordContactSimpleModelBirthCityBE

    /// <summary>
    /// The city of birth for an individual.
    /// </summary>
    [DataContract]
    public partial class RecordContactSimpleModelBirthCityBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordContactSimpleModelBirthCity" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public RecordContactSimpleModelBirthCityBE(string text = default(string), string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// The localized display value.
        /// </summary>
        /// <value>The localized display value.</value>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        public string Text { get; set; }

        /// <summary>
        /// The data value.
        /// </summary>
        /// <value>The data value.</value>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }
    }

    #endregion

    #region RecordContactSimpleModelBirthRegionBE

    /// <summary>
    /// The country of birth or region of birth for an individual.
    /// </summary>
    [DataContract]
    public partial class RecordContactSimpleModelBirthRegionBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordContactSimpleModelBirthRegion" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public RecordContactSimpleModelBirthRegionBE(string text = default(string), string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// The localized display value.
        /// </summary>
        /// <value>The localized display value.</value>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        public string Text { get; set; }

        /// <summary>
        /// The data value.
        /// </summary>
        /// <value>The data value.</value>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }

    }

    #endregion

    #region RecordContactSimpleModelBirthStateBE
    /// <summary>
    /// The state of birth for an individual.
    /// </summary>
    [DataContract]
    public partial class RecordContactSimpleModelBirthStateBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordContactSimpleModelBirthState" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public RecordContactSimpleModelBirthStateBE(string text = default(string), string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// The localized display value.
        /// </summary>
        /// <value>The localized display value.</value>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        public string Text { get; set; }

        /// <summary>
        /// The data value.
        /// </summary>
        /// <value>The data value.</value>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }
    }
    #endregion

    #region  RecordContactSimpleModelBE

    /// <summary>
    /// RecordContactSimpleModel
    /// </summary>
    [DataContract]
    public partial class RecordContactSimpleModelBE
    {
        /// <summary>
        /// Indicates whether or not to designate the contact as the primary contact Only one address can be primary at any given time.
        /// </summary>
        /// <value>Indicates whether or not to designate the contact as the primary contact Only one address can be primary at any given time.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum IsPrimaryEnum
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

        /// <summary>
        /// Indicates whether or not to designate the contact as the primary contact Only one address can be primary at any given time.
        /// </summary>
        /// <value>Indicates whether or not to designate the contact as the primary contact Only one address can be primary at any given time.</value>
        [DataMember(Name = "isPrimary", EmitDefaultValue = false)]
        public IsPrimaryEnum? IsPrimary { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RecordContactSimpleModel" /> class.
        /// </summary>
        /// <param name="address">address.</param>
        /// <param name="birthCity">birthCity.</param>
        /// <param name="birthDate">The birth date..</param>
        /// <param name="birthRegion">birthRegion.</param>
        /// <param name="birthState">birthState.</param>
        /// <param name="businessName">A secondary business name for the applicable individual..</param>
        /// <param name="comment">A comment about the inspection contact..</param>
        /// <param name="deceasedDate">The deceased date..</param>
        /// <param name="driverLicenseNumber">The driver&#39;s license number of the contact. This field is active only when the Contact Type selected is Individual..</param>
        /// <param name="driverLicenseState">driverLicenseState.</param>
        /// <param name="email">The contact&#39;s email address..</param>
        /// <param name="endDate">The date when the contact address ceases to be active..</param>
        /// <param name="fax">The fax number for the contact..</param>
        /// <param name="faxCountryCode">Fax Number Country Code.</param>
        /// <param name="federalEmployerId">The Federal Employer Identification Number. It is used to identify a business for tax purposes..</param>
        /// <param name="firstName">The contact&#39;s first name..</param>
        /// <param name="fullName">The contact&#39;s full name. .</param>
        /// <param name="gender">gender.</param>
        /// <param name="id">The contact system id assigned by the Civic Platform server..</param>
        /// <param name="individualOrOrganization">The organization to which the contact belongs. This field is only active when the Contact Type selected is Organization..</param>
        /// <param name="isPrimary">Indicates whether or not to designate the contact as the primary contact Only one address can be primary at any given time..</param>
        /// <param name="lastName">The last name (surname)..</param>
        /// <param name="middleName">The middle name. .</param>
        /// <param name="organizationName">The organization to which the contact belongs. This field is only active when the Contact Type selected is Organization..</param>
        /// <param name="passportNumber">The contact&#39;s passport number. This field is only active when the Contact Type selected is Individual..</param>
        /// <param name="phone1">The primary telephone number of the contact..</param>
        /// <param name="phone1CountryCode">Phone Number 1 Country Code.</param>
        /// <param name="phone2">The secondary telephone number of the contact..</param>
        /// <param name="phone2CountryCode">Phone Number 2 Country Code.</param>
        /// <param name="phone3">The tertiary telephone number for the contact..</param>
        /// <param name="phone3CountryCode">Phone Number 3 Country Code.</param>
        /// <param name="postOfficeBox">The post office box number..</param>
        /// <param name="preferredChannel">preferredChannel.</param>
        /// <param name="race">race.</param>
        /// <param name="recordId">recordId.</param>
        /// <param name="referenceContactId">The unique Id generated for a contact stored in the sytem..</param>
        /// <param name="relation">relation.</param>
        /// <param name="salutation">salutation.</param>
        /// <param name="socialSecurityNumber">The individual&#39;s social security number. This field is only active when the Contact Type selected is Individual..</param>
        /// <param name="startDate">The date the contact became active..</param>
        /// <param name="stateIdNumber">The contact&#39;s state ID number. This field is only active when the Contact Type selected is Individual..</param>
        /// <param name="status">status.</param>
        /// <param name="suffix">The contact name suffix..</param>
        /// <param name="title">The individual&#39;s business title..</param>
        /// <param name="tradeName">The contact&#39;s preferred business or trade name. This field is active only when the Contact Type selected is Organization..</param>
        /// <param name="type">type.</param>
        public RecordContactSimpleModelBE(CompactAddressModelBE address = default(CompactAddressModelBE),
            RecordContactSimpleModelBirthCityBE birthCity = default(RecordContactSimpleModelBirthCityBE),
            DateTime? birthDate = default(DateTime?),
            RecordContactSimpleModelBirthRegionBE birthRegion = default(RecordContactSimpleModelBirthRegionBE),
            RecordContactSimpleModelBirthStateBE birthState = default(RecordContactSimpleModelBirthStateBE),
            string businessName = default(string), string comment = default(string),
            DateTime? deceasedDate = default(DateTime?), string driverLicenseNumber = default(string),
            RecordContactSimpleModelDriverLicenseStateBE driverLicenseState =
                default(RecordContactSimpleModelDriverLicenseStateBE), string email = default(string),
            DateTime? endDate = default(DateTime?), string fax = default(string),
            string faxCountryCode = default(string), string federalEmployerId = default(string),
            string firstName = default(string), string fullName = default(string),
            RecordContactSimpleModelGenderBE gender = default(RecordContactSimpleModelGenderBE),
            string id = default(string), string individualOrOrganization = default(string),
            IsPrimaryEnum? isPrimary = default(IsPrimaryEnum?), string lastName = default(string),
            string middleName = default(string), string organizationName = default(string),
            string passportNumber = default(string), string phone1 = default(string),
            string phone1CountryCode = default(string), string phone2 = default(string),
            string phone2CountryCode = default(string), string phone3 = default(string),
            string phone3CountryCode = default(string), string postOfficeBox = default(string),
            RecordContactSimpleModelPreferredChannelBE preferredChannel =
                default(RecordContactSimpleModelPreferredChannelBE),
            RecordContactSimpleModelRaceBE race = default(RecordContactSimpleModelRaceBE),
            RecordIdModelBE recordId = default(RecordIdModelBE), string referenceContactId = default(string),
            RecordContactSimpleModelRelationBE relation = default(RecordContactSimpleModelRelationBE),
            RecordContactSimpleModelSalutationBE salutation = default(RecordContactSimpleModelSalutationBE),
            string socialSecurityNumber = default(string), DateTime? startDate = default(DateTime?),
            string stateIdNumber = default(string),
            RecordContactSimpleModelStatusBE status = default(RecordContactSimpleModelStatusBE),
            string suffix = default(string), string title = default(string), string tradeName = default(string),
            RecordContactSimpleModelTypeBE type = default(RecordContactSimpleModelTypeBE))
        {
            this.Address = address;
            this.BirthCity = birthCity;
            this.BirthDate = birthDate;
            this.BirthRegion = birthRegion;
            this.BirthState = birthState;
            this.BusinessName = businessName;
            this.Comment = comment;
            this.DeceasedDate = deceasedDate;
            this.DriverLicenseNumber = driverLicenseNumber;
            this.DriverLicenseState = driverLicenseState;
            this.Email = email;
            this.EndDate = endDate;
            this.Fax = fax;
            this.FaxCountryCode = faxCountryCode;
            this.FederalEmployerId = federalEmployerId;
            this.FirstName = firstName;
            this.FullName = fullName;
            this.Gender = gender;
            this.Id = id;
            this.IndividualOrOrganization = individualOrOrganization;
            this.IsPrimary = isPrimary;
            this.LastName = lastName;
            this.MiddleName = middleName;
            this.OrganizationName = organizationName;
            this.PassportNumber = passportNumber;
            this.Phone1 = phone1;
            this.Phone1CountryCode = phone1CountryCode;
            this.Phone2 = phone2;
            this.Phone2CountryCode = phone2CountryCode;
            this.Phone3 = phone3;
            this.Phone3CountryCode = phone3CountryCode;
            this.PostOfficeBox = postOfficeBox;
            this.PreferredChannel = preferredChannel;
            this.Race = race;
            this.RecordId = recordId;
            this.ReferenceContactId = referenceContactId;
            this.Relation = relation;
            this.Salutation = salutation;
            this.SocialSecurityNumber = socialSecurityNumber;
            this.StartDate = startDate;
            this.StateIdNumber = stateIdNumber;
            this.Status = status;
            this.Suffix = suffix;
            this.Title = title;
            this.TradeName = tradeName;
            this.Type = type;
        }

        /// <summary>
        /// Gets or Sets Address
        /// </summary>
        [DataMember(Name = "address", EmitDefaultValue = false)]
        public CompactAddressModelBE Address { get; set; }

        /// <summary>
        /// Gets or Sets BirthCity
        /// </summary>
        [DataMember(Name = "birthCity", EmitDefaultValue = false)]
        public RecordContactSimpleModelBirthCityBE BirthCity { get; set; }

        /// <summary>
        /// The birth date.
        /// </summary>
        /// <value>The birth date.</value>
        [DataMember(Name = "birthDate", EmitDefaultValue = false)]
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// Gets or Sets BirthRegion
        /// </summary>
        [DataMember(Name = "birthRegion", EmitDefaultValue = false)]
        public RecordContactSimpleModelBirthRegionBE BirthRegion { get; set; }

        /// <summary>
        /// Gets or Sets BirthState
        /// </summary>
        [DataMember(Name = "birthState", EmitDefaultValue = false)]
        public RecordContactSimpleModelBirthStateBE BirthState { get; set; }

        /// <summary>
        /// A secondary business name for the applicable individual.
        /// </summary>
        /// <value>A secondary business name for the applicable individual.</value>
        [DataMember(Name = "businessName", EmitDefaultValue = false)]
        public string BusinessName { get; set; }

        /// <summary>
        /// A comment about the inspection contact.
        /// </summary>
        /// <value>A comment about the inspection contact.</value>
        [DataMember(Name = "comment", EmitDefaultValue = false)]
        public string Comment { get; set; }

        /// <summary>
        /// The deceased date.
        /// </summary>
        /// <value>The deceased date.</value>
        [DataMember(Name = "deceasedDate", EmitDefaultValue = false)]
        public DateTime? DeceasedDate { get; set; }

        /// <summary>
        /// The driver&#39;s license number of the contact. This field is active only when the Contact Type selected is Individual.
        /// </summary>
        /// <value>The driver&#39;s license number of the contact. This field is active only when the Contact Type selected is Individual.</value>
        [DataMember(Name = "driverLicenseNumber", EmitDefaultValue = false)]
        public string DriverLicenseNumber { get; set; }

        /// <summary>
        /// Gets or Sets DriverLicenseState
        /// </summary>
        [DataMember(Name = "driverLicenseState", EmitDefaultValue = false)]
        public RecordContactSimpleModelDriverLicenseStateBE DriverLicenseState { get; set; }

        /// <summary>
        /// The contact&#39;s email address.
        /// </summary>
        /// <value>The contact&#39;s email address.</value>
        [DataMember(Name = "email", EmitDefaultValue = false)]
        public string Email { get; set; }

        /// <summary>
        /// The date when the contact address ceases to be active.
        /// </summary>
        /// <value>The date when the contact address ceases to be active.</value>
        [DataMember(Name = "endDate", EmitDefaultValue = false)]
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// The fax number for the contact.
        /// </summary>
        /// <value>The fax number for the contact.</value>
        [DataMember(Name = "fax", EmitDefaultValue = false)]
        public string Fax { get; set; }

        /// <summary>
        /// Fax Number Country Code
        /// </summary>
        /// <value>Fax Number Country Code</value>
        [DataMember(Name = "faxCountryCode", EmitDefaultValue = false)]
        public string FaxCountryCode { get; set; }

        /// <summary>
        /// The Federal Employer Identification Number. It is used to identify a business for tax purposes.
        /// </summary>
        /// <value>The Federal Employer Identification Number. It is used to identify a business for tax purposes.</value>
        [DataMember(Name = "federalEmployerId", EmitDefaultValue = false)]
        public string FederalEmployerId { get; set; }

        /// <summary>
        /// The contact&#39;s first name.
        /// </summary>
        /// <value>The contact&#39;s first name.</value>
        [DataMember(Name = "firstName", EmitDefaultValue = false)]
        public string FirstName { get; set; }

        /// <summary>
        /// The contact&#39;s full name. 
        /// </summary>
        /// <value>The contact&#39;s full name. </value>
        [DataMember(Name = "fullName", EmitDefaultValue = false)]
        public string FullName { get; set; }

        /// <summary>
        /// Gets or Sets Gender
        /// </summary>
        [DataMember(Name = "gender", EmitDefaultValue = false)]
        public RecordContactSimpleModelGenderBE Gender { get; set; }

        /// <summary>
        /// The contact system id assigned by the Civic Platform server.
        /// </summary>
        /// <value>The contact system id assigned by the Civic Platform server.</value>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }

        /// <summary>
        /// The organization to which the contact belongs. This field is only active when the Contact Type selected is Organization.
        /// </summary>
        /// <value>The organization to which the contact belongs. This field is only active when the Contact Type selected is Organization.</value>
        [DataMember(Name = "individualOrOrganization", EmitDefaultValue = false)]
        public string IndividualOrOrganization { get; set; }


        /// <summary>
        /// The last name (surname).
        /// </summary>
        /// <value>The last name (surname).</value>
        [DataMember(Name = "lastName", EmitDefaultValue = false)]
        public string LastName { get; set; }

        /// <summary>
        /// The middle name. 
        /// </summary>
        /// <value>The middle name. </value>
        [DataMember(Name = "middleName", EmitDefaultValue = false)]
        public string MiddleName { get; set; }

        /// <summary>
        /// The organization to which the contact belongs. This field is only active when the Contact Type selected is Organization.
        /// </summary>
        /// <value>The organization to which the contact belongs. This field is only active when the Contact Type selected is Organization.</value>
        [DataMember(Name = "organizationName", EmitDefaultValue = false)]
        public string OrganizationName { get; set; }

        /// <summary>
        /// The contact&#39;s passport number. This field is only active when the Contact Type selected is Individual.
        /// </summary>
        /// <value>The contact&#39;s passport number. This field is only active when the Contact Type selected is Individual.</value>
        [DataMember(Name = "passportNumber", EmitDefaultValue = false)]
        public string PassportNumber { get; set; }

        /// <summary>
        /// The primary telephone number of the contact.
        /// </summary>
        /// <value>The primary telephone number of the contact.</value>
        [DataMember(Name = "phone1", EmitDefaultValue = false)]
        public string Phone1 { get; set; }

        /// <summary>
        /// Phone Number 1 Country Code
        /// </summary>
        /// <value>Phone Number 1 Country Code</value>
        [DataMember(Name = "phone1CountryCode", EmitDefaultValue = false)]
        public string Phone1CountryCode { get; set; }

        /// <summary>
        /// The secondary telephone number of the contact.
        /// </summary>
        /// <value>The secondary telephone number of the contact.</value>
        [DataMember(Name = "phone2", EmitDefaultValue = false)]
        public string Phone2 { get; set; }

        /// <summary>
        /// Phone Number 2 Country Code
        /// </summary>
        /// <value>Phone Number 2 Country Code</value>
        [DataMember(Name = "phone2CountryCode", EmitDefaultValue = false)]
        public string Phone2CountryCode { get; set; }

        /// <summary>
        /// The tertiary telephone number for the contact.
        /// </summary>
        /// <value>The tertiary telephone number for the contact.</value>
        [DataMember(Name = "phone3", EmitDefaultValue = false)]
        public string Phone3 { get; set; }

        /// <summary>
        /// Phone Number 3 Country Code
        /// </summary>
        /// <value>Phone Number 3 Country Code</value>
        [DataMember(Name = "phone3CountryCode", EmitDefaultValue = false)]
        public string Phone3CountryCode { get; set; }

        /// <summary>
        /// The post office box number.
        /// </summary>
        /// <value>The post office box number.</value>
        [DataMember(Name = "postOfficeBox", EmitDefaultValue = false)]
        public string PostOfficeBox { get; set; }

        /// <summary>
        /// Gets or Sets PreferredChannel
        /// </summary>
        [DataMember(Name = "preferredChannel", EmitDefaultValue = false)]
        public RecordContactSimpleModelPreferredChannelBE PreferredChannel { get; set; }

        /// <summary>
        /// Gets or Sets Race
        /// </summary>
        [DataMember(Name = "race", EmitDefaultValue = false)]
        public RecordContactSimpleModelRaceBE Race { get; set; }

        /// <summary>
        /// Gets or Sets RecordId
        /// </summary>
        [DataMember(Name = "recordId", EmitDefaultValue = false)]
        public RecordIdModelBE RecordId { get; set; }

        /// <summary>
        /// The unique Id generated for a contact stored in the sytem.
        /// </summary>
        /// <value>The unique Id generated for a contact stored in the sytem.</value>
        [DataMember(Name = "referenceContactId", EmitDefaultValue = false)]
        public string ReferenceContactId { get; set; }

        /// <summary>
        /// Gets or Sets Relation
        /// </summary>
        [DataMember(Name = "relation", EmitDefaultValue = false)]
        public RecordContactSimpleModelRelationBE Relation { get; set; }

        /// <summary>
        /// Gets or Sets Salutation
        /// </summary>
        [DataMember(Name = "salutation", EmitDefaultValue = false)]
        public RecordContactSimpleModelSalutationBE Salutation { get; set; }

        /// <summary>
        /// The individual&#39;s social security number. This field is only active when the Contact Type selected is Individual.
        /// </summary>
        /// <value>The individual&#39;s social security number. This field is only active when the Contact Type selected is Individual.</value>
        [DataMember(Name = "socialSecurityNumber", EmitDefaultValue = false)]
        public string SocialSecurityNumber { get; set; }

        /// <summary>
        /// The date the contact became active.
        /// </summary>
        /// <value>The date the contact became active.</value>
        [DataMember(Name = "startDate", EmitDefaultValue = false)]
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// The contact&#39;s state ID number. This field is only active when the Contact Type selected is Individual.
        /// </summary>
        /// <value>The contact&#39;s state ID number. This field is only active when the Contact Type selected is Individual.</value>
        [DataMember(Name = "stateIdNumber", EmitDefaultValue = false)]
        public string StateIdNumber { get; set; }

        /// <summary>
        /// Gets or Sets Status
        /// </summary>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public RecordContactSimpleModelStatusBE Status { get; set; }

        /// <summary>
        /// The contact name suffix.
        /// </summary>
        /// <value>The contact name suffix.</value>
        [DataMember(Name = "suffix", EmitDefaultValue = false)]
        public string Suffix { get; set; }

        /// <summary>
        /// The individual&#39;s business title.
        /// </summary>
        /// <value>The individual&#39;s business title.</value>
        [DataMember(Name = "title", EmitDefaultValue = false)]
        public string Title { get; set; }

        /// <summary>
        /// The contact&#39;s preferred business or trade name. This field is active only when the Contact Type selected is Organization.
        /// </summary>
        /// <value>The contact&#39;s preferred business or trade name. This field is active only when the Contact Type selected is Organization.</value>
        [DataMember(Name = "tradeName", EmitDefaultValue = false)]
        public string TradeName { get; set; }

        /// <summary>
        /// Gets or Sets Type
        /// </summary>
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public RecordContactSimpleModelTypeBE Type { get; set; }
    }
    #endregion

    #region  RecordContactSimpleModelDriverLicenseStateBE 

    /// <summary>
    /// The state that issued the driver&#39;s license.
    /// </summary>
    [DataContract]
    public partial class RecordContactSimpleModelDriverLicenseStateBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordContactSimpleModelDriverLicenseState" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public RecordContactSimpleModelDriverLicenseStateBE(string text = default(string), string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// The localized display value.
        /// </summary>
        /// <value>The localized display value.</value>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        public string Text { get; set; }

        /// <summary>
        /// The data value.
        /// </summary>
        /// <value>The data value.</value>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }
    }
    #endregion

    #region RecordContactSimpleModelRelationBE 

    /// <summary>
    /// The contact&#39;s relationship to the application or service request.
    /// </summary>
    [DataContract]
    public partial class RecordContactSimpleModelRelationBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordContactSimpleModelRelation" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public RecordContactSimpleModelRelationBE(string text = default(string), string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// The localized display value.
        /// </summary>
        /// <value>The localized display value.</value>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        public string Text { get; set; }

        /// <summary>
        /// The data value.
        /// </summary>
        /// <value>The data value.</value>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }
    }
    #endregion

    #region RecordContactSimpleModelRaceBE 

    /// <summary>
    /// The contact&#39;s race or ethnicity. See [Get All Contact Races](./api-settings.html#operation/v4.get.settings.contacts.races).
    /// </summary>
    [DataContract]
    public partial class RecordContactSimpleModelRaceBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordContactSimpleModelRace" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public RecordContactSimpleModelRaceBE(string text = default(string), string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// The localized display value.
        /// </summary>
        /// <value>The localized display value.</value>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        public string Text { get; set; }

        /// <summary>
        /// The data value.
        /// </summary>
        /// <value>The data value.</value>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }
    }
    #endregion

    #region RecordContactSimpleModelTypeBE

    /// <summary>
    /// The contact type. See [Get All Contact Types](./api-settings.html#operation/v4.get.settings.contacts.types).
    /// </summary>
    [DataContract]
    public partial class RecordContactSimpleModelTypeBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordContactSimpleModelType" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public RecordContactSimpleModelTypeBE(string text = default(string), string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// The localized display value.
        /// </summary>
        /// <value>The localized display value.</value>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        public string Text { get; set; }

        /// <summary>
        /// The data value.
        /// </summary>
        /// <value>The data value.</value>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }
    }
    #endregion

    #region CompactAddressModelCountryBE

    /// <summary>
    /// The name of the country.
    /// </summary>
    [DataContract]
    public partial class CompactAddressModelCountryBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompactAddressModelCountry" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public CompactAddressModelCountryBE(string text = default(string), string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// The localized display value.
        /// </summary>
        /// <value>The localized display value.</value>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        public string Text { get; set; }

        /// <summary>
        /// The data value.
        /// </summary>
        /// <value>The data value.</value>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }
    }

    #endregion

    #region  OwnerAddressModelBE 

    /// <summary>
    /// OwnerAddressModel
    /// </summary>
    [DataContract]
    public partial class OwnerAddressModelBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OwnerAddressModel" /> class.
        /// </summary>
        /// <param name="addressLine1">addressLine1.</param>
        /// <param name="addressLine2">addressLine2.</param>
        /// <param name="addressLine3">addressLine3.</param>
        /// <param name="city">city.</param>
        /// <param name="country">country.</param>
        /// <param name="postalCode">postalCode.</param>
        /// <param name="state">state.</param>
        public OwnerAddressModelBE(string addressLine1 = default(string), string addressLine2 = default(string),
            string addressLine3 = default(string), string city = default(string),
            IdentifierModelBE country = default(IdentifierModelBE), string postalCode = default(string),
            IdentifierModelBE state = default(IdentifierModelBE))
        {
            this.AddressLine1 = addressLine1;
            this.AddressLine2 = addressLine2;
            this.AddressLine3 = addressLine3;
            this.City = city;
            this.Country = country;
            this.PostalCode = postalCode;
            this.State = state;
        }

        /// <summary>
        /// Gets or Sets AddressLine1
        /// </summary>
        [DataMember(Name = "addressLine1", EmitDefaultValue = false)]
        public string AddressLine1 { get; set; }

        /// <summary>
        /// Gets or Sets AddressLine2
        /// </summary>
        [DataMember(Name = "addressLine2", EmitDefaultValue = false)]
        public string AddressLine2 { get; set; }

        /// <summary>
        /// Gets or Sets AddressLine3
        /// </summary>
        [DataMember(Name = "addressLine3", EmitDefaultValue = false)]
        public string AddressLine3 { get; set; }

        /// <summary>
        /// Gets or Sets City
        /// </summary>
        [DataMember(Name = "city", EmitDefaultValue = false)]
        public string City { get; set; }

        /// <summary>
        /// Gets or Sets Country
        /// </summary>
        [DataMember(Name = "country", EmitDefaultValue = false)]
        public IdentifierModelBE Country { get; set; }

        /// <summary>
        /// Gets or Sets PostalCode
        /// </summary>
        [DataMember(Name = "postalCode", EmitDefaultValue = false)]
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or Sets State
        /// </summary>
        [DataMember(Name = "state", EmitDefaultValue = false)]
        public IdentifierModelBE State { get; set; }

    }

    #endregion

    #region RecordIdModelBE

    /// <summary>
    /// RecordIdModel
    /// </summary>
    [DataContract]
    public partial class RecordIdModelBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordIdModel" /> class.
        /// </summary>
        /// <param name="customId">An ID based on a different numbering convention from the numbering convention used by the record ID (xxxxx-xx-xxxxx). Accela Automation auto-generates and applies an alternate ID value when you submit a new application..</param>
        /// <param name="id">The record system id assigned by the Civic Platform server..</param>
        /// <param name="serviceProviderCode">The unique agency identifier..</param>
        /// <param name="trackingId">The application tracking number (IVR tracking number)..</param>
        /// <param name="value">The alphanumeric record id..</param>
        public RecordIdModelBE(string customId = default(string), string id = default(string),
            string serviceProviderCode = default(string), long? trackingId = default(long?),
            string value = default(string))
        {
            this.CustomId = customId;
            this.Id = id;
            this.ServiceProviderCode = serviceProviderCode;
            this.TrackingId = trackingId;
            this.Value = value;
        }

        /// <summary>
        /// An ID based on a different numbering convention from the numbering convention used by the record ID (xxxxx-xx-xxxxx). Accela Automation auto-generates and applies an alternate ID value when you submit a new application.
        /// </summary>
        /// <value>An ID based on a different numbering convention from the numbering convention used by the record ID (xxxxx-xx-xxxxx). Accela Automation auto-generates and applies an alternate ID value when you submit a new application.</value>
        [DataMember(Name = "customId", EmitDefaultValue = false)]
        public string CustomId { get; set; }

        /// <summary>
        /// The record system id assigned by the Civic Platform server.
        /// </summary>
        /// <value>The record system id assigned by the Civic Platform server.</value>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }

        /// <summary>
        /// The unique agency identifier.
        /// </summary>
        /// <value>The unique agency identifier.</value>
        [DataMember(Name = "serviceProviderCode", EmitDefaultValue = false)]
        public string ServiceProviderCode { get; set; }

        /// <summary>
        /// The application tracking number (IVR tracking number).
        /// </summary>
        /// <value>The application tracking number (IVR tracking number).</value>
        [DataMember(Name = "trackingId", EmitDefaultValue = false)]
        public long? TrackingId { get; set; }

        /// <summary>
        /// The alphanumeric record id.
        /// </summary>
        /// <value>The alphanumeric record id.</value>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }
    }

    #endregion

    #region RefOwnerModelBE

    /// <summary>
    /// RefOwnerModel
    /// </summary>
    [DataContract]
    public partial class RefOwnerModelBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RefOwnerModel" /> class.
        /// </summary>
        /// <param name="email">The contact&#39;s email address..</param>
        /// <param name="fax">The fax number for the contact..</param>
        /// <param name="firstName">The contact&#39;s first name. This field is only active when the Contact Type selected is Individual..</param>
        /// <param name="fullName">The contact&#39;s full name. This field is only active when the Contact Type selected is Individual..</param>
        /// <param name="id">The owner system id assigned by the Civic Platform server..</param>
        /// <param name="isPrimary">Indicates whether or not to designate the owner as the primary owner..</param>
        /// <param name="lastName">The last name (surname)..</param>
        /// <param name="mailAddress">mailAddress.</param>
        /// <param name="middleName">The contact&#39;s middle name..</param>
        /// <param name="parcelId">The unique Id generated for a parcel..</param>
        /// <param name="phone">The telephone number of the owner..</param>
        /// <param name="phoneCountryCode">The country code for the assoicated phone number..</param>
        /// <param name="recordId">recordId.</param>
        /// <param name="refOwnerId">The reference owner id..</param>
        /// <param name="status">status.</param>
        /// <param name="taxId">The owner&#39;s tax ID number..</param>
        /// <param name="title">The individual&#39;s business title..</param>
        /// <param name="type">The owner type..</param>
        public RefOwnerModelBE(string email = default(string), string fax = default(string),
            string firstName = default(string), string fullName = default(string), long? id = default(long?),
            string isPrimary = default(string), string lastName = default(string),
            OwnerAddressModelBE mailAddress = default(OwnerAddressModelBE), string middleName = default(string),
            string parcelId = default(string), string phone = default(string),
            string phoneCountryCode = default(string), RecordIdModelBE recordId = default(RecordIdModelBE),
            double? refOwnerId = default(double?), RefOwnerModelStatusBE status = default(RefOwnerModelStatusBE),
            string taxId = default(string), string title = default(string), string type = default(string))
        {
            this.Email = email;
            this.Fax = fax;
            this.FirstName = firstName;
            this.FullName = fullName;
            this.Id = id;
            this.IsPrimary = isPrimary;
            this.LastName = lastName;
            this.MailAddress = mailAddress;
            this.MiddleName = middleName;
            this.ParcelId = parcelId;
            this.Phone = phone;
            this.PhoneCountryCode = phoneCountryCode;
            this.RecordId = recordId;
            this.RefOwnerId = refOwnerId;
            this.Status = status;
            this.TaxId = taxId;
            this.Title = title;
            this.Type = type;
        }

        /// <summary>
        /// The contact&#39;s email address.
        /// </summary>
        /// <value>The contact&#39;s email address.</value>
        [DataMember(Name = "email", EmitDefaultValue = false)]
        public string Email { get; set; }

        /// <summary>
        /// The fax number for the contact.
        /// </summary>
        /// <value>The fax number for the contact.</value>
        [DataMember(Name = "fax", EmitDefaultValue = false)]
        public string Fax { get; set; }

        /// <summary>
        /// The contact&#39;s first name. This field is only active when the Contact Type selected is Individual.
        /// </summary>
        /// <value>The contact&#39;s first name. This field is only active when the Contact Type selected is Individual.</value>
        [DataMember(Name = "firstName", EmitDefaultValue = false)]
        public string FirstName { get; set; }

        /// <summary>
        /// The contact&#39;s full name. This field is only active when the Contact Type selected is Individual.
        /// </summary>
        /// <value>The contact&#39;s full name. This field is only active when the Contact Type selected is Individual.</value>
        [DataMember(Name = "fullName", EmitDefaultValue = false)]
        public string FullName { get; set; }

        /// <summary>
        /// The owner system id assigned by the Civic Platform server.
        /// </summary>
        /// <value>The owner system id assigned by the Civic Platform server.</value>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public long? Id { get; set; }

        /// <summary>
        /// Indicates whether or not to designate the owner as the primary owner.
        /// </summary>
        /// <value>Indicates whether or not to designate the owner as the primary owner.</value>
        [DataMember(Name = "isPrimary", EmitDefaultValue = false)]
        public string IsPrimary { get; set; }

        /// <summary>
        /// The last name (surname).
        /// </summary>
        /// <value>The last name (surname).</value>
        [DataMember(Name = "lastName", EmitDefaultValue = false)]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or Sets MailAddress
        /// </summary>
        [DataMember(Name = "mailAddress", EmitDefaultValue = false)]
        public OwnerAddressModelBE MailAddress { get; set; }

        /// <summary>
        /// The contact&#39;s middle name.
        /// </summary>
        /// <value>The contact&#39;s middle name.</value>
        [DataMember(Name = "middleName", EmitDefaultValue = false)]
        public string MiddleName { get; set; }

        /// <summary>
        /// The unique Id generated for a parcel.
        /// </summary>
        /// <value>The unique Id generated for a parcel.</value>
        [DataMember(Name = "parcelId", EmitDefaultValue = false)]
        public string ParcelId { get; set; }

        /// <summary>
        /// The telephone number of the owner.
        /// </summary>
        /// <value>The telephone number of the owner.</value>
        [DataMember(Name = "phone", EmitDefaultValue = false)]
        public string Phone { get; set; }

        /// <summary>
        /// The country code for the assoicated phone number.
        /// </summary>
        /// <value>The country code for the assoicated phone number.</value>
        [DataMember(Name = "phoneCountryCode", EmitDefaultValue = false)]
        public string PhoneCountryCode { get; set; }

        /// <summary>
        /// Gets or Sets RecordId
        /// </summary>
        [DataMember(Name = "recordId", EmitDefaultValue = false)]
        public RecordIdModelBE RecordId { get; set; }

        /// <summary>
        /// The reference owner id.
        /// </summary>
        /// <value>The reference owner id.</value>
        [DataMember(Name = "refOwnerId", EmitDefaultValue = false)]
        public double? RefOwnerId { get; set; }

        /// <summary>
        /// Gets or Sets Status
        /// </summary>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public RefOwnerModelStatusBE Status { get; set; }

        /// <summary>
        /// The owner&#39;s tax ID number.
        /// </summary>
        /// <value>The owner&#39;s tax ID number.</value>
        [DataMember(Name = "taxId", EmitDefaultValue = false)]
        public string TaxId { get; set; }

        /// <summary>
        /// The individual&#39;s business title.
        /// </summary>
        /// <value>The individual&#39;s business title.</value>
        [DataMember(Name = "title", EmitDefaultValue = false)]
        public string Title { get; set; }

        /// <summary>
        /// The owner type.
        /// </summary>
        /// <value>The owner type.</value>
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string Type { get; set; }
    }
    #endregion

    #region RefOwnerModelStatusBE

    /// <summary>
    /// The owner status.
    /// </summary>
    [DataContract]
    public partial class RefOwnerModelStatusBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RefOwnerModelStatus" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public RefOwnerModelStatusBE(string text = default(string), string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// The localized display value.
        /// </summary>
        /// <value>The localized display value.</value>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        public string Text { get; set; }

        /// <summary>
        /// The data value.
        /// </summary>
        /// <value>The data value.</value>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }
    }
    #endregion

    #region  RecordContactSimpleModelPreferredChannelBE 

    /// <summary>
    /// The method by which the contact prefers to be notified, by phone for example. See [Get All Contact Preferred Channels](./api-settings.html#operation/v4.get.settings.contacts.preferredChannels).
    /// </summary>
    [DataContract]
    public partial class RecordContactSimpleModelPreferredChannelBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordContactSimpleModelPreferredChannel" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public RecordContactSimpleModelPreferredChannelBE(string text = default(string), string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// The localized display value.
        /// </summary>
        /// <value>The localized display value.</value>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        public string Text { get; set; }

        /// <summary>
        /// The data value.
        /// </summary>
        /// <value>The data value.</value>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }

    }
    #endregion

    #region RecordContactSimpleModelStatusBE

    /// <summary>
    /// The contact status.
    /// </summary>
    [DataContract]
    public partial class RecordContactSimpleModelStatusBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordContactSimpleModelStatus" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public RecordContactSimpleModelStatusBE(string text = default(string), string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// The localized display value.
        /// </summary>
        /// <value>The localized display value.</value>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        public string Text { get; set; }

        /// <summary>
        /// The data value.
        /// </summary>
        /// <value>The data value.</value>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }
    }
    #endregion

    #region  RecordContactSimpleModelSalutationBE

    /// <summary>
    /// The salutation to be used when addressing the contact; for example Mr. oar Ms. This field is active only when Contact Type &#x3D; Individual. See [Get All Contact Salutations](./api-settings.html#operation/v4.get.settings.contacts.salutations).
    /// </summary>
    [DataContract]
    public partial class RecordContactSimpleModelSalutationBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordContactSimpleModelSalutation" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public RecordContactSimpleModelSalutationBE(string text = default(string), string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// The localized display value.
        /// </summary>
        /// <value>The localized display value.</value>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        public string Text { get; set; }

        /// <summary>
        /// The data value.
        /// </summary>
        /// <value>The data value.</value>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }
    }
    #endregion

    #region RowModelBE

    /// <summary>
    /// RowModel
    /// </summary>
   
    public partial class RowModelBE
    {
        /// <summary>
        /// The requested operation on the row.
        /// </summary>
        /// <value>The requested operation on the row.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum ActionEnum
        {

            /// <summary>
            /// Enum Add for value: add
            /// </summary>
            [EnumMember(Value = "add")] Add = 1,

            /// <summary>
            /// Enum Update for value: update
            /// </summary>
            [EnumMember(Value = "update")] Update = 2,

            /// <summary>
            /// Enum Delete for value: delete
            /// </summary>
            [EnumMember(Value = "delete")] Delete = 3
        }

        /// <summary>
        /// The requested operation on the row.
        /// </summary>
        /// <value>The requested operation on the row.</value>
        [DataMember(Name = "action", EmitDefaultValue = false)]
        public ActionEnum? Action { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RowModel" /> class.
        /// </summary>
        /// <param name="action">The requested operation on the row..</param>
        /// <param name="fields">fields.</param>
        /// <param name="id">The row id..</param>
        public RowModelBE(ActionEnum? action = default(ActionEnum?), CustomAttributeModelBE fields = default(CustomAttributeModelBE), string id = default(string))
        {
           this.Action = action;
           this.Fields = fields;
            this.Id = id;
        }


        /// <summary>
        /// Gets or Sets Fields
        /// </summary>
        [DataMember(Name = "fields", EmitDefaultValue = false)]
        public CustomAttributeModelBE Fields { get; set; }

        /// <summary>
        /// The row id.
        /// </summary>
        /// <value>The row id.</value>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }
    }

    #endregion

    #region RecordTypeModelBE

    /// <summary>
    /// RecordTypeModel
    /// </summary>
    [DataContract]
    public partial class RecordTypeModelBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordTypeModel" /> class.
        /// </summary>
        /// <param name="alias">The record type alias..</param>
        /// <param name="category">The 4th level in a 4-level record type structure (Group-Type-Subtype-Category)..</param>
        /// <param name="filterName">The name of the record type filter which defines the record types to be displayed for the citizen user..</param>
        /// <param name="group">The 1st level in a 4-level record type structure (Group-Type-Subtype-Category)..</param>
        /// <param name="id">The record type id..</param>
        /// <param name="module">The module the record type belongs to..</param>
        /// <param name="subType">The 3rd level in a 4-level record type structure (Group-Type-Subtype-Category)..</param>
        /// <param name="text">The localized display text..</param>
        /// <param name="type">The 2nd level in a 4-level record type structure (Group-Type-Subtype-Category)..</param>
        /// <param name="value">The stored value..</param>
        public RecordTypeModelBE(string alias = default(string), string category = default(string),
            string filterName = default(string), string group = default(string), string id = default(string),
            string module = default(string), string subType = default(string), string text = default(string),
            string type = default(string), string value = default(string))
        {
            this.Alias = alias;
            this.Category = category;
            this.FilterName = filterName;
            this.Group = group;
            this.Id = id;
            this.Module = module;
            this.SubType = subType;
            this.Text = text;
            this.Type = type;
            this.Value = value;
        }

        /// <summary>
        /// The record type alias.
        /// </summary>
        /// <value>The record type alias.</value>
        [DataMember(Name = "alias", EmitDefaultValue = false)]
        public string Alias { get; set; }

        /// <summary>
        /// The 4th level in a 4-level record type structure (Group-Type-Subtype-Category).
        /// </summary>
        /// <value>The 4th level in a 4-level record type structure (Group-Type-Subtype-Category).</value>
        [DataMember(Name = "category", EmitDefaultValue = false)]
        public string Category { get; set; }

        /// <summary>
        /// The name of the record type filter which defines the record types to be displayed for the citizen user.
        /// </summary>
        /// <value>The name of the record type filter which defines the record types to be displayed for the citizen user.</value>
        [DataMember(Name = "filterName", EmitDefaultValue = false)]
        public string FilterName { get; set; }

        /// <summary>
        /// The 1st level in a 4-level record type structure (Group-Type-Subtype-Category).
        /// </summary>
        /// <value>The 1st level in a 4-level record type structure (Group-Type-Subtype-Category).</value>
        [DataMember(Name = "group", EmitDefaultValue = false)]
        public string Group { get; set; }

        /// <summary>
        /// The record type id.
        /// </summary>
        /// <value>The record type id.</value>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }

        /// <summary>
        /// The module the record type belongs to.
        /// </summary>
        /// <value>The module the record type belongs to.</value>
        [DataMember(Name = "module", EmitDefaultValue = false)]
        public string Module { get; set; }

        /// <summary>
        /// The 3rd level in a 4-level record type structure (Group-Type-Subtype-Category).
        /// </summary>
        /// <value>The 3rd level in a 4-level record type structure (Group-Type-Subtype-Category).</value>
        [DataMember(Name = "subType", EmitDefaultValue = false)]
        public string SubType { get; set; }

        /// <summary>
        /// The localized display text.
        /// </summary>
        /// <value>The localized display text.</value>
        [DataMember(Name = "text", EmitDefaultValue = false)]
        public string Text { get; set; }

        /// <summary>
        /// The 2nd level in a 4-level record type structure (Group-Type-Subtype-Category).
        /// </summary>
        /// <value>The 2nd level in a 4-level record type structure (Group-Type-Subtype-Category).</value>
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public string Type { get; set; }

        /// <summary>
        /// The stored value.
        /// </summary>
        /// <value>The stored value.</value>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }
    }
    #endregion

    #region TableModelBE

    /// <summary>
    /// TableModel
    /// </summary>
    [DataContract]
    public partial class TableModelBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TableModel" /> class.
        /// </summary>
        /// <param name="id">The custom table id..</param>
        /// <param name="rows">A table row containing custom fields..</param>
        public TableModelBE(string id = default(string), List<RowModelBE> rows = default(List<RowModelBE>))
        {
            this.Id = id;
            this.Rows = rows;
        }

        /// <summary>
        /// The custom table id.
        /// </summary>
        /// <value>The custom table id.</value>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }

        /// <summary>
        /// A table row containing custom fields.
        /// </summary>
        /// <value>A table row containing custom fields.</value>
        [DataMember(Name = "rows", EmitDefaultValue = false)]
        public List<RowModelBE> Rows { get; set; }
    }
    #endregion
}
