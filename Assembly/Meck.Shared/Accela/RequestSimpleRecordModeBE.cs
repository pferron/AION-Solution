﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Meck.Shared.Accela
{
    /// <summary>
    /// RequestSimpleRecordModel
    /// </summary>

    public partial class RequestSimpleRecordModelBE  
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequestSimpleRecordModel" /> class.
        /// </summary>
        /// <param name="actualProductionUnit">Estimated cost per production unit..</param>
        /// <param name="appearanceDate">The date for a hearing appearance..</param>
        /// <param name="appearanceDayOfWeek">The day for a hearing appearance..</param>
        /// <param name="assignedDate">The date the application was assigned..</param>
        /// <param name="assignedToDepartment">The department responsible for the action. See [Get All Departments](./api-settings.html#operation/v4.get.settings.departments)..</param>
        /// <param name="balance">The amount due..</param>
        /// <param name="booking">Indicates whether or not there was a booking in addition to a citation..</param>
        /// <param name="closedByDepartment">The department responsible for closing the record. See [Get All Departments](./api-settings.html#operation/v4.get.settings.departments)..</param>
        /// <param name="closedDate">The date the application was closed..</param>
        /// <param name="completeDate">The date the application was completed..</param>
        /// <param name="completedByDepartment">The department responsible for completion. See [Get All Departments](./api-settings.html#operation/v4.get.settings.departments)..</param>
        /// <param name="defendantSignature">Indicates whether or not a defendant&#39;s signature has been obtained..</param>
        /// <param name="description">The description of the record or item..</param>
        /// <param name="enforceDepartment">The name of the department responsible for enforcement. See [Get All Departments](./api-settings.html#operation/v4.get.settings.departments)..</param>
        /// <param name="estimatedProductionUnit">The estimated number of production units..</param>
        /// <param name="estimatedTotalJobCost">The estimated cost of the job..</param>
        /// <param name="firstIssuedDate">The first issued date for license.</param>
        /// <param name="infraction">Indicates whether or not an infraction occurred..</param>
        /// <param name="inspectorDepartment">The name of the department where the inspector works. See [Get All Departments](./api-settings.html#operation/v4.get.settings.departments)..</param>
        /// <param name="inspectorId">The ID number of the inspector. See [Get All Inspectors](./api-inspections.html#operation/v4.get.inspectors)..</param>
        /// <param name="inspectorName">The name of the inspector. See [Get All Inspectors](./api-inspections.html#operation/v4.get.inspectors)..</param>
        /// <param name="jobValue">The value of the job..</param>
        /// <param name="misdemeanor">Indicates whether or not a misdemeanor occurred..</param>
        /// <param name="name">The name associated to the record..</param>
        /// <param name="offenseWitnessed">Indicates whether or not  there was a witness to the alleged offense..</param>
        /// <param name="priority">priority.</param>
        /// <param name="publicOwned">Indicates whether or not the record is for the public..</param>
        /// <param name="renewalInfo">renewalInfo.</param>
        /// <param name="reportedChannel">reportedChannel.</param>
        /// <param name="reportedDate">The date the complaint was reported..</param>
        /// <param name="reportedType">reportedType.</param>
        /// <param name="scheduledDate">The date when the inspection gets scheduled..</param>
        /// <param name="severity">severity.</param>
        /// <param name="shortNotes">A brief note about the record subject..</param>
        /// <param name="status">status.</param>
        /// <param name="statusReason">statusReason.</param>
        /// <param name="totalFee">The total amount of the fees invoiced to the record..</param>
        /// <param name="totalPay">The total amount of pay..</param>
        public RequestSimpleRecordModelBE(double? actualProductionUnit = default(double?), DateTime? appearanceDate = default(DateTime?), string appearanceDayOfWeek = default(string), DateTime? assignedDate = default(DateTime?), string assignedToDepartment = default(string), double? balance = default(double?), bool? booking = default(bool?), string closedByDepartment = default(string), DateTime? closedDate = default(DateTime?), DateTime? completeDate = default(DateTime?), string completedByDepartment = default(string), bool? defendantSignature = default(bool?), string description = default(string), string enforceDepartment = default(string), double? estimatedProductionUnit = default(double?), double? estimatedTotalJobCost = default(double?), DateTime? firstIssuedDate = default(DateTime?), bool? infraction = default(bool?), string inspectorDepartment = default(string), string inspectorId = default(string), string inspectorName = default(string), double? jobValue = default(double?), bool? misdemeanor = default(bool?), string name = default(string), bool? offenseWitnessed = default(bool?), RecordAPOCustomFormsModelPriorityBE priority = default(RecordAPOCustomFormsModelPriorityBE), bool? publicOwned = default(bool?), RecordExpirationModelBE renewalInfo = default(RecordExpirationModelBE), RecordAPOCustomFormsModelReportedChannelBE reportedChannel = default(RecordAPOCustomFormsModelReportedChannelBE), DateTime? reportedDate = default(DateTime?), RecordAPOCustomFormsModelReportedTypeBE reportedType = default(RecordAPOCustomFormsModelReportedTypeBE), DateTime? scheduledDate = default(DateTime?), RecordAPOCustomFormsModelSeverityBE severity = default(RecordAPOCustomFormsModelSeverityBE), string shortNotes = default(string), RecordAPOCustomFormsModelStatusBE status = default(RecordAPOCustomFormsModelStatusBE), RecordAPOCustomFormsModelStatusReasonBE statusReason = default(RecordAPOCustomFormsModelStatusReasonBE), double? totalFee = default(double?), double? totalPay = default(double?))
            
        {
            this.ActualProductionUnit = actualProductionUnit;
            this.AppearanceDate = appearanceDate;
            this.AppearanceDayOfWeek = appearanceDayOfWeek;
            this.AssignedDate = assignedDate;
            this.AssignedToDepartment = assignedToDepartment;
            this.Balance = balance;
            this.Booking = booking;
            this.ClosedByDepartment = closedByDepartment;
            this.ClosedDate = closedDate;
            this.CompleteDate = completeDate;
            this.CompletedByDepartment = completedByDepartment;
            this.DefendantSignature = defendantSignature;
            this.Description = description;
            this.EnforceDepartment = enforceDepartment;
            this.EstimatedProductionUnit = estimatedProductionUnit;
            this.EstimatedTotalJobCost = estimatedTotalJobCost;
            this.FirstIssuedDate = firstIssuedDate;
            this.Infraction = infraction;
            this.InspectorDepartment = inspectorDepartment;
            this.InspectorId = inspectorId;
            this.InspectorName = inspectorName;
            this.JobValue = jobValue;
            this.Misdemeanor = misdemeanor;
            this.Name = name;
            this.OffenseWitnessed = offenseWitnessed;
            this.Priority = priority;
            this.PublicOwned = publicOwned;
            this.RenewalInfo = renewalInfo;
            this.ReportedChannel = reportedChannel;
            this.ReportedDate = reportedDate;
            this.ReportedType = reportedType;
            this.ScheduledDate = scheduledDate;
            this.Severity = severity;
            this.ShortNotes = shortNotes;
            this.Status = status;
            this.StatusReason = statusReason;
            this.TotalFee = totalFee;
            this.TotalPay = totalPay;
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
        /// Indicates whether or not an infraction occurred.
        /// </summary>
        /// <value>Indicates whether or not an infraction occurred.</value>
        [DataMember(Name = "infraction", EmitDefaultValue = false)]
        public bool? Infraction { get; set; }

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
        /// The name associated to the record.
        /// </summary>
        /// <value>The name associated to the record.</value>
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }

        /// <summary>
        /// Indicates whether or not  there was a witness to the alleged offense.
        /// </summary>
        /// <value>Indicates whether or not  there was a witness to the alleged offense.</value>
        [DataMember(Name = "offenseWitnessed", EmitDefaultValue = false)]
        public bool? OffenseWitnessed { get; set; }

        /// <summary>
        /// Gets or Sets Priority
        /// </summary>
        [DataMember(Name = "priority", EmitDefaultValue = false)]
        public RecordAPOCustomFormsModelPriorityBE Priority { get; set; }

        /// <summary>
        /// Indicates whether or not the record is for the public.
        /// </summary>
        /// <value>Indicates whether or not the record is for the public.</value>
        [DataMember(Name = "publicOwned", EmitDefaultValue = false)]
        public bool? PublicOwned { get; set; }

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
        /// Gets or Sets StatusReason
        /// </summary>
        [DataMember(Name = "statusReason", EmitDefaultValue = false)]
        public RecordAPOCustomFormsModelStatusReasonBE StatusReason { get; set; }

        /// <summary>
        /// The total amount of the fees invoiced to the record.
        /// </summary>
        /// <value>The total amount of the fees invoiced to the record.</value>
        [DataMember(Name = "totalFee", EmitDefaultValue = false)]
        public double? TotalFee { get; set; }

        /// <summary>
        /// The total amount of pay.
        /// </summary>
        /// <value>The total amount of pay.</value>
        [DataMember(Name = "totalPay", EmitDefaultValue = false)]
        public double? TotalPay { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class RequestSimpleRecordModel {\n");
            sb.Append("  ActualProductionUnit: ").Append(ActualProductionUnit).Append("\n");
            sb.Append("  AppearanceDate: ").Append(AppearanceDate).Append("\n");
            sb.Append("  AppearanceDayOfWeek: ").Append(AppearanceDayOfWeek).Append("\n");
            sb.Append("  AssignedDate: ").Append(AssignedDate).Append("\n");
            sb.Append("  AssignedToDepartment: ").Append(AssignedToDepartment).Append("\n");
            sb.Append("  Balance: ").Append(Balance).Append("\n");
            sb.Append("  Booking: ").Append(Booking).Append("\n");
            sb.Append("  ClosedByDepartment: ").Append(ClosedByDepartment).Append("\n");
            sb.Append("  ClosedDate: ").Append(ClosedDate).Append("\n");
            sb.Append("  CompleteDate: ").Append(CompleteDate).Append("\n");
            sb.Append("  CompletedByDepartment: ").Append(CompletedByDepartment).Append("\n");
            sb.Append("  DefendantSignature: ").Append(DefendantSignature).Append("\n");
            sb.Append("  Description: ").Append(Description).Append("\n");
            sb.Append("  EnforceDepartment: ").Append(EnforceDepartment).Append("\n");
            sb.Append("  EstimatedProductionUnit: ").Append(EstimatedProductionUnit).Append("\n");
            sb.Append("  EstimatedTotalJobCost: ").Append(EstimatedTotalJobCost).Append("\n");
            sb.Append("  FirstIssuedDate: ").Append(FirstIssuedDate).Append("\n");
            sb.Append("  Infraction: ").Append(Infraction).Append("\n");
            sb.Append("  InspectorDepartment: ").Append(InspectorDepartment).Append("\n");
            sb.Append("  InspectorId: ").Append(InspectorId).Append("\n");
            sb.Append("  InspectorName: ").Append(InspectorName).Append("\n");
            sb.Append("  JobValue: ").Append(JobValue).Append("\n");
            sb.Append("  Misdemeanor: ").Append(Misdemeanor).Append("\n");
            sb.Append("  Name: ").Append(Name).Append("\n");
            sb.Append("  OffenseWitnessed: ").Append(OffenseWitnessed).Append("\n");
            sb.Append("  Priority: ").Append(Priority).Append("\n");
            sb.Append("  PublicOwned: ").Append(PublicOwned).Append("\n");
            sb.Append("  RenewalInfo: ").Append(RenewalInfo).Append("\n");
            sb.Append("  ReportedChannel: ").Append(ReportedChannel).Append("\n");
            sb.Append("  ReportedDate: ").Append(ReportedDate).Append("\n");
            sb.Append("  ReportedType: ").Append(ReportedType).Append("\n");
            sb.Append("  ScheduledDate: ").Append(ScheduledDate).Append("\n");
            sb.Append("  Severity: ").Append(Severity).Append("\n");
            sb.Append("  ShortNotes: ").Append(ShortNotes).Append("\n");
            sb.Append("  Status: ").Append(Status).Append("\n");
            sb.Append("  StatusReason: ").Append(StatusReason).Append("\n");
            sb.Append("  TotalFee: ").Append(TotalFee).Append("\n");
            sb.Append("  TotalPay: ").Append(TotalPay).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as RequestSimpleRecordModelBE);
        }

        /// <summary>
        /// Returns true if RequestSimpleRecordModel instances are equal
        /// </summary>
        /// <param name="input">Instance of RequestSimpleRecordModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(RequestSimpleRecordModelBE input)
        {
            if (input == null)
                return false;

            return
                (
                    this.ActualProductionUnit == input.ActualProductionUnit ||
                    (this.ActualProductionUnit != null &&
                    this.ActualProductionUnit.Equals(input.ActualProductionUnit))
                ) &&
                (
                    this.AppearanceDate == input.AppearanceDate ||
                    (this.AppearanceDate != null &&
                    this.AppearanceDate.Equals(input.AppearanceDate))
                ) &&
                (
                    this.AppearanceDayOfWeek == input.AppearanceDayOfWeek ||
                    (this.AppearanceDayOfWeek != null &&
                    this.AppearanceDayOfWeek.Equals(input.AppearanceDayOfWeek))
                ) &&
                (
                    this.AssignedDate == input.AssignedDate ||
                    (this.AssignedDate != null &&
                    this.AssignedDate.Equals(input.AssignedDate))
                ) &&
                (
                    this.AssignedToDepartment == input.AssignedToDepartment ||
                    (this.AssignedToDepartment != null &&
                    this.AssignedToDepartment.Equals(input.AssignedToDepartment))
                ) &&
                (
                    this.Balance == input.Balance ||
                    (this.Balance != null &&
                    this.Balance.Equals(input.Balance))
                ) &&
                (
                    this.Booking == input.Booking ||
                    (this.Booking != null &&
                    this.Booking.Equals(input.Booking))
                ) &&
                (
                    this.ClosedByDepartment == input.ClosedByDepartment ||
                    (this.ClosedByDepartment != null &&
                    this.ClosedByDepartment.Equals(input.ClosedByDepartment))
                ) &&
                (
                    this.ClosedDate == input.ClosedDate ||
                    (this.ClosedDate != null &&
                    this.ClosedDate.Equals(input.ClosedDate))
                ) &&
                (
                    this.CompleteDate == input.CompleteDate ||
                    (this.CompleteDate != null &&
                    this.CompleteDate.Equals(input.CompleteDate))
                ) &&
                (
                    this.CompletedByDepartment == input.CompletedByDepartment ||
                    (this.CompletedByDepartment != null &&
                    this.CompletedByDepartment.Equals(input.CompletedByDepartment))
                ) &&
                (
                    this.DefendantSignature == input.DefendantSignature ||
                    (this.DefendantSignature != null &&
                    this.DefendantSignature.Equals(input.DefendantSignature))
                ) &&
                (
                    this.Description == input.Description ||
                    (this.Description != null &&
                    this.Description.Equals(input.Description))
                ) &&
                (
                    this.EnforceDepartment == input.EnforceDepartment ||
                    (this.EnforceDepartment != null &&
                    this.EnforceDepartment.Equals(input.EnforceDepartment))
                ) &&
                (
                    this.EstimatedProductionUnit == input.EstimatedProductionUnit ||
                    (this.EstimatedProductionUnit != null &&
                    this.EstimatedProductionUnit.Equals(input.EstimatedProductionUnit))
                ) &&
                (
                    this.EstimatedTotalJobCost == input.EstimatedTotalJobCost ||
                    (this.EstimatedTotalJobCost != null &&
                    this.EstimatedTotalJobCost.Equals(input.EstimatedTotalJobCost))
                ) &&
                (
                    this.FirstIssuedDate == input.FirstIssuedDate ||
                    (this.FirstIssuedDate != null &&
                    this.FirstIssuedDate.Equals(input.FirstIssuedDate))
                ) &&
                (
                    this.Infraction == input.Infraction ||
                    (this.Infraction != null &&
                    this.Infraction.Equals(input.Infraction))
                ) &&
                (
                    this.InspectorDepartment == input.InspectorDepartment ||
                    (this.InspectorDepartment != null &&
                    this.InspectorDepartment.Equals(input.InspectorDepartment))
                ) &&
                (
                    this.InspectorId == input.InspectorId ||
                    (this.InspectorId != null &&
                    this.InspectorId.Equals(input.InspectorId))
                ) &&
                (
                    this.InspectorName == input.InspectorName ||
                    (this.InspectorName != null &&
                    this.InspectorName.Equals(input.InspectorName))
                ) &&
                (
                    this.JobValue == input.JobValue ||
                    (this.JobValue != null &&
                    this.JobValue.Equals(input.JobValue))
                ) &&
                (
                    this.Misdemeanor == input.Misdemeanor ||
                    (this.Misdemeanor != null &&
                    this.Misdemeanor.Equals(input.Misdemeanor))
                ) &&
                (
                    this.Name == input.Name ||
                    (this.Name != null &&
                    this.Name.Equals(input.Name))
                ) &&
                (
                    this.OffenseWitnessed == input.OffenseWitnessed ||
                    (this.OffenseWitnessed != null &&
                    this.OffenseWitnessed.Equals(input.OffenseWitnessed))
                ) &&
                (
                    this.Priority == input.Priority ||
                    (this.Priority != null &&
                    this.Priority.Equals(input.Priority))
                ) &&
                (
                    this.PublicOwned == input.PublicOwned ||
                    (this.PublicOwned != null &&
                    this.PublicOwned.Equals(input.PublicOwned))
                ) &&
                (
                    this.RenewalInfo == input.RenewalInfo ||
                    (this.RenewalInfo != null &&
                    this.RenewalInfo.Equals(input.RenewalInfo))
                ) &&
                (
                    this.ReportedChannel == input.ReportedChannel ||
                    (this.ReportedChannel != null &&
                    this.ReportedChannel.Equals(input.ReportedChannel))
                ) &&
                (
                    this.ReportedDate == input.ReportedDate ||
                    (this.ReportedDate != null &&
                    this.ReportedDate.Equals(input.ReportedDate))
                ) &&
                (
                    this.ReportedType == input.ReportedType ||
                    (this.ReportedType != null &&
                    this.ReportedType.Equals(input.ReportedType))
                ) &&
                (
                    this.ScheduledDate == input.ScheduledDate ||
                    (this.ScheduledDate != null &&
                    this.ScheduledDate.Equals(input.ScheduledDate))
                ) &&
                (
                    this.Severity == input.Severity ||
                    (this.Severity != null &&
                    this.Severity.Equals(input.Severity))
                ) &&
                (
                    this.ShortNotes == input.ShortNotes ||
                    (this.ShortNotes != null &&
                    this.ShortNotes.Equals(input.ShortNotes))
                ) &&
                (
                    this.Status == input.Status ||
                    (this.Status != null &&
                    this.Status.Equals(input.Status))
                ) &&
                (
                    this.StatusReason == input.StatusReason ||
                    (this.StatusReason != null &&
                    this.StatusReason.Equals(input.StatusReason))
                ) &&
                (
                    this.TotalFee == input.TotalFee ||
                    (this.TotalFee != null &&
                    this.TotalFee.Equals(input.TotalFee))
                ) &&
                (
                    this.TotalPay == input.TotalPay ||
                    (this.TotalPay != null &&
                    this.TotalPay.Equals(input.TotalPay))
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashCode = 41;
                if (this.ActualProductionUnit != null)
                    hashCode = hashCode * 59 + this.ActualProductionUnit.GetHashCode();
                if (this.AppearanceDate != null)
                    hashCode = hashCode * 59 + this.AppearanceDate.GetHashCode();
                if (this.AppearanceDayOfWeek != null)
                    hashCode = hashCode * 59 + this.AppearanceDayOfWeek.GetHashCode();
                if (this.AssignedDate != null)
                    hashCode = hashCode * 59 + this.AssignedDate.GetHashCode();
                if (this.AssignedToDepartment != null)
                    hashCode = hashCode * 59 + this.AssignedToDepartment.GetHashCode();
                if (this.Balance != null)
                    hashCode = hashCode * 59 + this.Balance.GetHashCode();
                if (this.Booking != null)
                    hashCode = hashCode * 59 + this.Booking.GetHashCode();
                if (this.ClosedByDepartment != null)
                    hashCode = hashCode * 59 + this.ClosedByDepartment.GetHashCode();
                if (this.ClosedDate != null)
                    hashCode = hashCode * 59 + this.ClosedDate.GetHashCode();
                if (this.CompleteDate != null)
                    hashCode = hashCode * 59 + this.CompleteDate.GetHashCode();
                if (this.CompletedByDepartment != null)
                    hashCode = hashCode * 59 + this.CompletedByDepartment.GetHashCode();
                if (this.DefendantSignature != null)
                    hashCode = hashCode * 59 + this.DefendantSignature.GetHashCode();
                if (this.Description != null)
                    hashCode = hashCode * 59 + this.Description.GetHashCode();
                if (this.EnforceDepartment != null)
                    hashCode = hashCode * 59 + this.EnforceDepartment.GetHashCode();
                if (this.EstimatedProductionUnit != null)
                    hashCode = hashCode * 59 + this.EstimatedProductionUnit.GetHashCode();
                if (this.EstimatedTotalJobCost != null)
                    hashCode = hashCode * 59 + this.EstimatedTotalJobCost.GetHashCode();
                if (this.FirstIssuedDate != null)
                    hashCode = hashCode * 59 + this.FirstIssuedDate.GetHashCode();
                if (this.Infraction != null)
                    hashCode = hashCode * 59 + this.Infraction.GetHashCode();
                if (this.InspectorDepartment != null)
                    hashCode = hashCode * 59 + this.InspectorDepartment.GetHashCode();
                if (this.InspectorId != null)
                    hashCode = hashCode * 59 + this.InspectorId.GetHashCode();
                if (this.InspectorName != null)
                    hashCode = hashCode * 59 + this.InspectorName.GetHashCode();
                if (this.JobValue != null)
                    hashCode = hashCode * 59 + this.JobValue.GetHashCode();
                if (this.Misdemeanor != null)
                    hashCode = hashCode * 59 + this.Misdemeanor.GetHashCode();
                if (this.Name != null)
                    hashCode = hashCode * 59 + this.Name.GetHashCode();
                if (this.OffenseWitnessed != null)
                    hashCode = hashCode * 59 + this.OffenseWitnessed.GetHashCode();
                if (this.Priority != null)
                    hashCode = hashCode * 59 + this.Priority.GetHashCode();
                if (this.PublicOwned != null)
                    hashCode = hashCode * 59 + this.PublicOwned.GetHashCode();
                if (this.RenewalInfo != null)
                    hashCode = hashCode * 59 + this.RenewalInfo.GetHashCode();
                if (this.ReportedChannel != null)
                    hashCode = hashCode * 59 + this.ReportedChannel.GetHashCode();
                if (this.ReportedDate != null)
                    hashCode = hashCode * 59 + this.ReportedDate.GetHashCode();
                if (this.ReportedType != null)
                    hashCode = hashCode * 59 + this.ReportedType.GetHashCode();
                if (this.ScheduledDate != null)
                    hashCode = hashCode * 59 + this.ScheduledDate.GetHashCode();
                if (this.Severity != null)
                    hashCode = hashCode * 59 + this.Severity.GetHashCode();
                if (this.ShortNotes != null)
                    hashCode = hashCode * 59 + this.ShortNotes.GetHashCode();
                if (this.Status != null)
                    hashCode = hashCode * 59 + this.Status.GetHashCode();
                if (this.StatusReason != null)
                    hashCode = hashCode * 59 + this.StatusReason.GetHashCode();
                if (this.TotalFee != null)
                    hashCode = hashCode * 59 + this.TotalFee.GetHashCode();
                if (this.TotalPay != null)
                    hashCode = hashCode * 59 + this.TotalPay.GetHashCode();
                return hashCode;
            }
        }

        
    }

}