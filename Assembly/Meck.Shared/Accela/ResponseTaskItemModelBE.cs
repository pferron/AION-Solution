using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Meck.Shared.Accela
{

    /// <summary>
    /// ResponseTaskItemModel
    /// </summary>
    [DataContract]
    public partial class ResponseTaskItemModelBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseTaskItemModel" /> class.
        /// </summary>
        /// <param name="result">result.</param>
        /// <param name="status">The HTTP return status..</param>
        public ResponseTaskItemModelBE(TaskItemModelBE result = default(TaskItemModelBE), int? status = default(int?))
        {
            this.Result = result;
            this.Status = status;
        }

        /// <summary>
        /// Gets or Sets Result
        /// </summary>
        [DataMember(Name = "result", EmitDefaultValue = false)]
        public TaskItemModelBE Result { get; set; }

        /// <summary>
        /// The HTTP return status.
        /// </summary>
        /// <value>The HTTP return status.</value>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public int? Status { get; set; }
    }

    /// <summary>
    /// TaskItemModel
    /// </summary>
    [DataContract]
    public partial class TaskItemModelBE
    {
        /// <summary>
        /// Indicates whether or not the item is billable.
        /// </summary>
        /// <value>Indicates whether or not the item is billable.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum BillableEnum
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
        /// Indicates whether or not the item is billable.
        /// </summary>
        /// <value>Indicates whether or not the item is billable.</value>
        [DataMember(Name = "billable", EmitDefaultValue = false)]
        public BillableEnum? Billable { get; set; }

        /// <summary>
        /// Indicates whether or not the workflow task is active.
        /// </summary>
        /// <value>Indicates whether or not the workflow task is active.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum IsActiveEnum
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
        /// Indicates whether or not the workflow task is active.
        /// </summary>
        /// <value>Indicates whether or not the workflow task is active.</value>
        [DataMember(Name = "isActive", EmitDefaultValue = false)]
        public IsActiveEnum? IsActive { get; set; }

        /// <summary>
        /// Indicates whether or not the workflow task is completed.
        /// </summary>
        /// <value>Indicates whether or not the workflow task is completed.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum IsCompletedEnum
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
        /// Indicates whether or not the workflow task is completed.
        /// </summary>
        /// <value>Indicates whether or not the workflow task is completed.</value>
        [DataMember(Name = "isCompleted", EmitDefaultValue = false)]
        public IsCompletedEnum? IsCompleted { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskItemModel" /> class.
        /// </summary>
        /// <param name="actionbyDepartment">actionbyDepartment.</param>
        /// <param name="actionbyUser">actionbyUser.</param>
        /// <param name="approval">Used to indicate supervisory approval of an adhoc task..</param>
        /// <param name="assignEmailDisplay">Indicates whether or not to display the agency employeeâ€™s email address in ACA. Public users can then click the e-mail hyperlink and send an e-mail to the agency employee. â€œYâ€ : display the email address. â€œNâ€ : hide the email address..</param>
        /// <param name="assignedDate">The date of the assigned action..</param>
        /// <param name="assignedToDepartment">assignedToDepartment.</param>
        /// <param name="assignedUser">assignedUser.</param>
        /// <param name="billable">Indicates whether or not the item is billable..</param>
        /// <param name="comment">Comments or notes about the current context..</param>
        /// <param name="commentDisplay">Indicates whether or not Accela Citizen Access users can view the inspection results comments. .</param>
        /// <param name="commentPublicVisible">Specifies the type of user who can view the inspection result comments. &lt;br/&gt;\&quot;All ACA Users\&quot; - Both registered and anonymous Accela Citizen Access users can view the comments for inspection results. &lt;br/&gt;\&quot;Record Creator Only\&quot; - the user who created the record can see the comments for the inspection results. &lt;br/&gt;\&quot;Record Creator and Licensed Professional\&quot; - The user who created the record and the licensed professional associated with the record can see the comments for the inspection results..</param>
        /// <param name="currentTaskId">The ID of the current workflow task..</param>
        /// <param name="daysDue">The amount of time to complete a task (measured in days)..</param>
        /// <param name="description">The description of the record or item..</param>
        /// <param name="dispositionNote">A note describing the disposition of the current task..</param>
        /// <param name="dueDate">The desired completion date of the task..</param>
        /// <param name="endTime">The time the workflow task was completed..</param>
        /// <param name="estimatedDueDate">The estimated date of completion..</param>
        /// <param name="estimatedHours">The estimated hours necessary to complete this task..</param>
        /// <param name="hoursSpent">Number of hours used for a workflow or workflow task..</param>
        /// <param name="id">The workflow task system id assigned by the Civic Platform server..</param>
        /// <param name="inPossessionTime">The application level in possession time of the time tracking feature..</param>
        /// <param name="isActive">Indicates whether or not the workflow task is active..</param>
        /// <param name="isCompleted">Indicates whether or not the workflow task is completed..</param>
        /// <param name="lastModifiedDate">The date when the task item was last changed..</param>
        /// <param name="nextTaskId">The id of the next task in a workflow..</param>
        /// <param name="overTime">A labor cost factor that indicates time worked beyond a worker&#39;s regular working hours..</param>
        /// <param name="processCode">The process code for the next task in a workflow..</param>
        /// <param name="recordId">recordId.</param>
        /// <param name="serviceProviderCode">The unique agency identifier..</param>
        /// <param name="startTime">The time the workflow task started..</param>
        /// <param name="status">status.</param>
        /// <param name="statusDate">The date when the current status changed..</param>
        /// <param name="trackStartDate">The date that time tracking is set to begin..</param>
        public TaskItemModelBE(
            RecordConditionModelActionbyDepartmentBE actionbyDepartment = default(RecordConditionModelActionbyDepartmentBE),
            RecordConditionModelActionbyUserBE actionbyUser = default(RecordConditionModelActionbyUserBE),
            string approval = default(string), string assignEmailDisplay = default(string),
            DateTime? assignedDate = default(DateTime?),
            RecordConditionModelActionbyDepartmentBE assignedToDepartment =
                default(RecordConditionModelActionbyDepartmentBE),
            TaskItemModelAssignedUserBE assignedUser = default(TaskItemModelAssignedUserBE),
            BillableEnum? billable = default(BillableEnum?), string comment = default(string),
            string commentDisplay = default(string), List<string> commentPublicVisible = default(List<string>),
            string currentTaskId = default(string), long? daysDue = default(long?),
            string description = default(string), string dispositionNote = default(string),
            DateTime? dueDate = default(DateTime?), DateTime? endTime = default(DateTime?),
            DateTime? estimatedDueDate = default(DateTime?), double? estimatedHours = default(double?),
            double? hoursSpent = default(double?), string id = default(string),
            double? inPossessionTime = default(double?), IsActiveEnum? isActive = default(IsActiveEnum?),
            IsCompletedEnum? isCompleted = default(IsCompletedEnum?), DateTime? lastModifiedDate = default(DateTime?),
            string nextTaskId = default(string), string overTime = default(string),
            string processCode = default(string), RecordIdModelBE recordId = default(RecordIdModelBE),
            string serviceProviderCode = default(string), DateTime? startTime = default(DateTime?),
            TaskItemModelStatusBE status = default(TaskItemModelStatusBE), DateTime? statusDate = default(DateTime?),
            DateTime? trackStartDate = default(DateTime?))
        {
            this.ActionbyDepartment = actionbyDepartment;
            this.ActionbyUser = actionbyUser;
            this.Approval = approval;
            this.AssignEmailDisplay = assignEmailDisplay;
            this.AssignedDate = assignedDate;
            this.AssignedToDepartment = assignedToDepartment;
            this.AssignedUser = assignedUser;
            this.Billable = billable;
            this.Comment = comment;
            this.CommentDisplay = commentDisplay;
            this.CommentPublicVisible = commentPublicVisible;
            this.CurrentTaskId = currentTaskId;
            this.DaysDue = daysDue;
            this.Description = description;
            this.DispositionNote = dispositionNote;
            this.DueDate = dueDate;
            this.EndTime = endTime;
            this.EstimatedDueDate = estimatedDueDate;
            this.EstimatedHours = estimatedHours;
            this.HoursSpent = hoursSpent;
            this.Id = id;
            this.InPossessionTime = inPossessionTime;
            this.IsActive = isActive;
            this.IsCompleted = isCompleted;
            this.LastModifiedDate = lastModifiedDate;
            this.NextTaskId = nextTaskId;
            this.OverTime = overTime;
            this.ProcessCode = processCode;
            this.RecordId = recordId;
            this.ServiceProviderCode = serviceProviderCode;
            this.StartTime = startTime;
            this.Status = status;
            this.StatusDate = statusDate;
            this.TrackStartDate = trackStartDate;
        }

        /// <summary>
        /// Gets or Sets ActionbyDepartment
        /// </summary>
        [DataMember(Name = "actionbyDepartment", EmitDefaultValue = false)]
        public RecordConditionModelActionbyDepartmentBE ActionbyDepartment { get; set; }

        /// <summary>
        /// Gets or Sets ActionbyUser
        /// </summary>
        [DataMember(Name = "actionbyUser", EmitDefaultValue = false)]
        public RecordConditionModelActionbyUserBE ActionbyUser { get; set; }

        /// <summary>
        /// Used to indicate supervisory approval of an adhoc task.
        /// </summary>
        /// <value>Used to indicate supervisory approval of an adhoc task.</value>
        [DataMember(Name = "approval", EmitDefaultValue = false)]
        public string Approval { get; set; }

        /// <summary>
        /// Indicates whether or not to display the agency employeeâ€™s email address in ACA. Public users can then click the e-mail hyperlink and send an e-mail to the agency employee. â€œYâ€ : display the email address. â€œNâ€ : hide the email address.
        /// </summary>
        /// <value>Indicates whether or not to display the agency employeeâ€™s email address in ACA. Public users can then click the e-mail hyperlink and send an e-mail to the agency employee. â€œYâ€ : display the email address. â€œNâ€ : hide the email address.</value>
        [DataMember(Name = "assignEmailDisplay", EmitDefaultValue = false)]
        public string AssignEmailDisplay { get; set; }

        /// <summary>
        /// The date of the assigned action.
        /// </summary>
        /// <value>The date of the assigned action.</value>
        [DataMember(Name = "assignedDate", EmitDefaultValue = false)]
        public DateTime? AssignedDate { get; set; }

        /// <summary>
        /// Gets or Sets AssignedToDepartment
        /// </summary>
        [DataMember(Name = "assignedToDepartment", EmitDefaultValue = false)]
        public RecordConditionModelActionbyDepartmentBE AssignedToDepartment { get; set; }

        /// <summary>
        /// Gets or Sets AssignedUser
        /// </summary>
        [DataMember(Name = "assignedUser", EmitDefaultValue = false)]
        public TaskItemModelAssignedUserBE AssignedUser { get; set; }


        /// <summary>
        /// Comments or notes about the current context.
        /// </summary>
        /// <value>Comments or notes about the current context.</value>
        [DataMember(Name = "comment", EmitDefaultValue = false)]
        public string Comment { get; set; }

        /// <summary>
        /// Indicates whether or not Accela Citizen Access users can view the inspection results comments. 
        /// </summary>
        /// <value>Indicates whether or not Accela Citizen Access users can view the inspection results comments. </value>
        [DataMember(Name = "commentDisplay", EmitDefaultValue = false)]
        public string CommentDisplay { get; set; }

        /// <summary>
        /// Specifies the type of user who can view the inspection result comments. &lt;br/&gt;\&quot;All ACA Users\&quot; - Both registered and anonymous Accela Citizen Access users can view the comments for inspection results. &lt;br/&gt;\&quot;Record Creator Only\&quot; - the user who created the record can see the comments for the inspection results. &lt;br/&gt;\&quot;Record Creator and Licensed Professional\&quot; - The user who created the record and the licensed professional associated with the record can see the comments for the inspection results.
        /// </summary>
        /// <value>Specifies the type of user who can view the inspection result comments. &lt;br/&gt;\&quot;All ACA Users\&quot; - Both registered and anonymous Accela Citizen Access users can view the comments for inspection results. &lt;br/&gt;\&quot;Record Creator Only\&quot; - the user who created the record can see the comments for the inspection results. &lt;br/&gt;\&quot;Record Creator and Licensed Professional\&quot; - The user who created the record and the licensed professional associated with the record can see the comments for the inspection results.</value>
        [DataMember(Name = "commentPublicVisible", EmitDefaultValue = false)]
        public List<string> CommentPublicVisible { get; set; }

        /// <summary>
        /// The ID of the current workflow task.
        /// </summary>
        /// <value>The ID of the current workflow task.</value>
        [DataMember(Name = "currentTaskId", EmitDefaultValue = false)]
        public string CurrentTaskId { get; set; }

        /// <summary>
        /// The amount of time to complete a task (measured in days).
        /// </summary>
        /// <value>The amount of time to complete a task (measured in days).</value>
        [DataMember(Name = "daysDue", EmitDefaultValue = false)]
        public long? DaysDue { get; set; }

        /// <summary>
        /// The description of the record or item.
        /// </summary>
        /// <value>The description of the record or item.</value>
        [DataMember(Name = "description", EmitDefaultValue = false)]
        public string Description { get; set; }

        /// <summary>
        /// A note describing the disposition of the current task.
        /// </summary>
        /// <value>A note describing the disposition of the current task.</value>
        [DataMember(Name = "dispositionNote", EmitDefaultValue = false)]
        public string DispositionNote { get; set; }

        /// <summary>
        /// The desired completion date of the task.
        /// </summary>
        /// <value>The desired completion date of the task.</value>
        [DataMember(Name = "dueDate", EmitDefaultValue = false)]
        public DateTime? DueDate { get; set; }

        /// <summary>
        /// The time the workflow task was completed.
        /// </summary>
        /// <value>The time the workflow task was completed.</value>
        [DataMember(Name = "endTime", EmitDefaultValue = false)]
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// The estimated date of completion.
        /// </summary>
        /// <value>The estimated date of completion.</value>
        [DataMember(Name = "estimatedDueDate", EmitDefaultValue = false)]
        public DateTime? EstimatedDueDate { get; set; }

        /// <summary>
        /// The estimated hours necessary to complete this task.
        /// </summary>
        /// <value>The estimated hours necessary to complete this task.</value>
        [DataMember(Name = "estimatedHours", EmitDefaultValue = false)]
        public double? EstimatedHours { get; set; }

        /// <summary>
        /// Number of hours used for a workflow or workflow task.
        /// </summary>
        /// <value>Number of hours used for a workflow or workflow task.</value>
        [DataMember(Name = "hoursSpent", EmitDefaultValue = false)]
        public double? HoursSpent { get; set; }

        /// <summary>
        /// The workflow task system id assigned by the Civic Platform server.
        /// </summary>
        /// <value>The workflow task system id assigned by the Civic Platform server.</value>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }

        /// <summary>
        /// The application level in possession time of the time tracking feature.
        /// </summary>
        /// <value>The application level in possession time of the time tracking feature.</value>
        [DataMember(Name = "inPossessionTime", EmitDefaultValue = false)]
        public double? InPossessionTime { get; set; }



        /// <summary>
        /// The date when the task item was last changed.
        /// </summary>
        /// <value>The date when the task item was last changed.</value>
        [DataMember(Name = "lastModifiedDate", EmitDefaultValue = false)]
        public DateTime? LastModifiedDate { get; set; }

        /// <summary>
        /// The id of the next task in a workflow.
        /// </summary>
        /// <value>The id of the next task in a workflow.</value>
        [DataMember(Name = "nextTaskId", EmitDefaultValue = false)]
        public string NextTaskId { get; set; }

        /// <summary>
        /// A labor cost factor that indicates time worked beyond a worker&#39;s regular working hours.
        /// </summary>
        /// <value>A labor cost factor that indicates time worked beyond a worker&#39;s regular working hours.</value>
        [DataMember(Name = "overTime", EmitDefaultValue = false)]
        public string OverTime { get; set; }

        /// <summary>
        /// The process code for the next task in a workflow.
        /// </summary>
        /// <value>The process code for the next task in a workflow.</value>
        [DataMember(Name = "processCode", EmitDefaultValue = false)]
        public string ProcessCode { get; set; }

        /// <summary>
        /// Gets or Sets RecordId
        /// </summary>
        [DataMember(Name = "recordId", EmitDefaultValue = false)]
        public RecordIdModelBE RecordId { get; set; }

        /// <summary>
        /// The unique agency identifier.
        /// </summary>
        /// <value>The unique agency identifier.</value>
        [DataMember(Name = "serviceProviderCode", EmitDefaultValue = false)]
        public string ServiceProviderCode { get; set; }

        /// <summary>
        /// The time the workflow task started.
        /// </summary>
        /// <value>The time the workflow task started.</value>
        [DataMember(Name = "startTime", EmitDefaultValue = false)]
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// Gets or Sets Status
        /// </summary>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public TaskItemModelStatusBE Status { get; set; }

        /// <summary>
        /// The date when the current status changed.
        /// </summary>
        /// <value>The date when the current status changed.</value>
        [DataMember(Name = "statusDate", EmitDefaultValue = false)]
        public DateTime? StatusDate { get; set; }

        /// <summary>
        /// The date that time tracking is set to begin.
        /// </summary>
        /// <value>The date that time tracking is set to begin.</value>
        [DataMember(Name = "trackStartDate", EmitDefaultValue = false)]
        public DateTime? TrackStartDate { get; set; }
    }

    /// <summary>
    /// The staff member responsible for the action.
    /// </summary>
    [DataContract]
    public partial class TaskItemModelAssignedUserBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaskItemModelAssignedUser" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public TaskItemModelAssignedUserBE(string text = default(string), string value = default(string))
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

}