using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Meck.Shared.Accela
{
    public class RequestTaskModelBE
    {
        public RequestTaskItemModelBE TaskItemUpDate { get; set; }

        public RequestTaskModelBE()
        {
            TaskItemUpDate = new RequestTaskItemModelBE();
        }
    }
    /// <summary>
    /// RequestTaskItemModel
    /// </summary>
    [DataContract]
        public partial class RequestTaskItemModelBE
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
            /// Initializes a new instance of the <see cref="RequestTaskItemModel" /> class.
            /// </summary>
            /// <param name="actionbyDepartment">actionbyDepartment.</param>
            /// <param name="actionbyUser">actionbyUser.</param>
            /// <param name="approval">Used to indicate supervisory approval of an adhoc task..</param>
            /// <param name="assignEmailDisplay">Indicates whether or not to display the agency employeeâ€™s email address in ACA. Public users can then click the e-mail hyperlink and send an e-mail to the agency employee. â€œYâ€ : display the email address. â€œNâ€ : hide the email address..</param>
            /// <param name="billable">Indicates whether or not the item is billable..</param>
            /// <param name="comment">Comments or notes about the current context..</param>
            /// <param name="commentDisplay">Indicates whether or not Accela Citizen Access users can view the inspection results comments. .</param>
            /// <param name="commentPublicVisible">Specifies the type of user who can view the inspection result comments. &lt;br/&gt;\&quot;All ACA Users\&quot; - Both registered and anonymous Accela Citizen Access users can view the comments for inspection results. &lt;br/&gt;\&quot;Record Creator Only\&quot; - the user who created the record can see the comments for the inspection results. &lt;br/&gt;\&quot;Record Creator and Licensed Professional\&quot; - The user who created the record and the licensed professional associated with the record can see the comments for the inspection results..</param>
            /// <param name="dueDate">The desired completion date of the task..</param>
            /// <param name="endTime">The time the workflow task was completed..</param>
            /// <param name="hoursSpent">Number of hours used for a workflow or workflow task..</param>
            /// <param name="overTime">A labor cost factor that indicates time worked beyond a worker&#39;s regular working hours..</param>
            /// <param name="startTime">The time the workflow task started..</param>
            /// <param name="status">status.</param>
            /// <param name="statusDate">The date when the current status changed..</param>
            public RequestTaskItemModelBE(
                RecordConditionModelActionbyDepartmentBE actionbyDepartment =
                    default(RecordConditionModelActionbyDepartmentBE),
                RecordConditionModelActionbyUserBE actionbyUser = default(RecordConditionModelActionbyUserBE),
                string approval = default(string), string assignEmailDisplay = default(string),
                BillableEnum? billable = default(BillableEnum?), string comment = default(string),
                string commentDisplay = default(string), List<string> commentPublicVisible = default(List<string>),
                DateTime? dueDate = default(DateTime?), DateTime? endTime = default(DateTime?),
                double? hoursSpent = default(double?), string overTime = default(string),
                DateTime? startTime = default(DateTime?), TaskItemModelStatusBE status = default(TaskItemModelStatusBE),
                DateTime? statusDate = default(DateTime?))
            {
                this.actionbyDepartment = actionbyDepartment;
                this.ActionbyUser = actionbyUser;
                this.approval = approval;
                this.assignEmailDisplay = assignEmailDisplay;
                this.Billable = billable;
                this.comment = comment;
                this.commentDisplay = commentDisplay;
                this.commentPublicVisible = commentPublicVisible;
                this.dueDate = dueDate;
                this.endTime = endTime;
                this.hoursSpent = hoursSpent;
                this.overTime = overTime;
                this.startTime = startTime;
                this.status = status;
                this.statusDate = statusDate;
            }

            /// <summary>
            /// Gets or Sets ActionbyDepartment
            /// </summary>
            [DataMember(Name = "actionbyDepartment", EmitDefaultValue = false)]
            public RecordConditionModelActionbyDepartmentBE actionbyDepartment { get; set; }

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
            public string approval { get; set; }

            /// <summary>
            /// Indicates whether or not to display the agency employeeâ€™s email address in ACA. Public users can then click the e-mail hyperlink and send an e-mail to the agency employee. â€œYâ€ : display the email address. â€œNâ€ : hide the email address.
            /// </summary>
            /// <value>Indicates whether or not to display the agency employeeâ€™s email address in ACA. Public users can then click the e-mail hyperlink and send an e-mail to the agency employee. â€œYâ€ : display the email address. â€œNâ€ : hide the email address.</value>
            [DataMember(Name = "assignEmailDisplay", EmitDefaultValue = false)]
            public string assignEmailDisplay { get; set; }


            /// <summary>
            /// Comments or notes about the current context.
            /// </summary>
            /// <value>Comments or notes about the current context.</value>
            [DataMember(Name = "comment", EmitDefaultValue = false)]
            public string comment { get; set; }

            /// <summary>
            /// Indicates whether or not Accela Citizen Access users can view the inspection results comments. 
            /// </summary>
            /// <value>Indicates whether or not Accela Citizen Access users can view the inspection results comments. </value>
            [DataMember(Name = "commentDisplay", EmitDefaultValue = false)]
            public string commentDisplay { get; set; }

            /// <summary>
            /// Specifies the type of user who can view the inspection result comments. &lt;br/&gt;\&quot;All ACA Users\&quot; - Both registered and anonymous Accela Citizen Access users can view the comments for inspection results. &lt;br/&gt;\&quot;Record Creator Only\&quot; - the user who created the record can see the comments for the inspection results. &lt;br/&gt;\&quot;Record Creator and Licensed Professional\&quot; - The user who created the record and the licensed professional associated with the record can see the comments for the inspection results.
            /// </summary>
            /// <value>Specifies the type of user who can view the inspection result comments. &lt;br/&gt;\&quot;All ACA Users\&quot; - Both registered and anonymous Accela Citizen Access users can view the comments for inspection results. &lt;br/&gt;\&quot;Record Creator Only\&quot; - the user who created the record can see the comments for the inspection results. &lt;br/&gt;\&quot;Record Creator and Licensed Professional\&quot; - The user who created the record and the licensed professional associated with the record can see the comments for the inspection results.</value>
            [DataMember(Name = "commentPublicVisible", EmitDefaultValue = false)]
            public List<string> commentPublicVisible { get; set; }

            /// <summary>
            /// The desired completion date of the task.
            /// </summary>
            /// <value>The desired completion date of the task.</value>
            [DataMember(Name = "dueDate", EmitDefaultValue = false)]
            public DateTime? dueDate { get; set; }

            /// <summary>
            /// The time the workflow task was completed.
            /// </summary>
            /// <value>The time the workflow task was completed.</value>
            [DataMember(Name = "endTime", EmitDefaultValue = false)]
            public DateTime? endTime { get; set; }

            /// <summary>
            /// Number of hours used for a workflow or workflow task.
            /// </summary>
            /// <value>Number of hours used for a workflow or workflow task.</value>
            [DataMember(Name = "hoursSpent", EmitDefaultValue = false)]
            public double? hoursSpent { get; set; }

            /// <summary>
            /// A labor cost factor that indicates time worked beyond a worker&#39;s regular working hours.
            /// </summary>
            /// <value>A labor cost factor that indicates time worked beyond a worker&#39;s regular working hours.</value>
            [DataMember(Name = "overTime", EmitDefaultValue = false)]
            public string overTime { get; set; }

            /// <summary>
            /// The time the workflow task started.
            /// </summary>
            /// <value>The time the workflow task started.</value>
            [DataMember(Name = "startTime", EmitDefaultValue = false)]
            public DateTime? startTime { get; set; }

            /// <summary>
            /// Gets or Sets Status
            /// </summary>
            [DataMember(Name = "status", EmitDefaultValue = false)]
            public TaskItemModelStatusBE status { get; set; }

            /// <summary>
            /// The date when the current status changed.
            /// </summary>
            /// <value>The date when the current status changed.</value>
            [DataMember(Name = "statusDate", EmitDefaultValue = false)]
            public DateTime? statusDate { get; set; }
        }
  

    /// <summary>
    /// The department responsible for the action.
    /// </summary>
    [DataContract]
    public partial class RecordConditionModelActionbyDepartmentBE 
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordConditionModelActionbyDepartment" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public RecordConditionModelActionbyDepartmentBE(string text = default(string), string value = default(string))
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

    /// <summary>
    /// The individual responsible for the action.
    /// </summary>
    [DataContract]
    public partial class RecordConditionModelActionbyUserBE
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordConditionModelActionbyUser" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public RecordConditionModelActionbyUserBE(string text = default(string), string value = default(string))
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

    /// <summary>
    /// The workflow task status.
    /// </summary>
    [DataContract]
    public partial class TaskItemModelStatusBE 
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TaskItemModelStatus" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public TaskItemModelStatusBE(string text = default(string), string value = default(string))
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
