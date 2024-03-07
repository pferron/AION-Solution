using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Meck.Shared.Accela
{
    public class DepartmentWrapperBE
    {
        [JsonProperty("status")]
        public int Status { get; set; }

        [JsonProperty("result")]
        public List<DepartmentBE> Departmentlist { get; set; }

        public DepartmentWrapperBE()
        {
            Departmentlist = new List<DepartmentBE>();

        }
    }

    public class DepartmentBE
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("serviceProviderCode")]
        public string ServiceProviderCode { get; set; }
        [JsonProperty("division")]
        public string Division { get; set; }
        [JsonProperty("section")]
        public string Section { get; set; }
        [JsonProperty("agency")]
        public string Agency { get; set; }
        [JsonProperty("bureau")]
        public string Bureau { get; set; }
        [JsonProperty("office")]
        public string Office { get; set; }
        [JsonProperty("group")]
        public string Group { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("DeptBEvalue")]
        public string Value { get; set; }
        [JsonProperty("Users")]
        public List<DepartmentUserBE> DepartmentUsers { get; set; }
        
        public DepartmentBE()
        {
            DepartmentUsers = new List<DepartmentUserBE>();
        }

    }

    public partial class DepartmentUserBE
    {

        public DepartmentUserBE()
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserModel" /> class.
        /// </summary>
        /// <param name="cashierId">The unique ID associated with the cashier..</param>
        /// <param name="department">department.</param>
        /// <param name="email">The individual&#39;s email address..</param>
        /// <param name="employeeId">The unique ID associated with the employee..</param>
        /// <param name="firstName">The individual&#39;s first name..</param>
        /// <param name="fullName">The individual&#39;s full name..</param>
        /// <param name="id">The ID of the individual..</param>
        /// <param name="initial">The individual initials..</param>
        /// <param name="lastName">The individual&#39;s last name..</param>
        /// <param name="middleName">The individual&#39;s middle name..</param>
        /// <param name="namesuffix">The suffix portion of the name of a department staff member, for example; junior, esquire..</param>
        /// <param name="password">The individual&#39;s password.</param>
        /// <param name="phone">The individual&#39;s phone number..</param>
        /// <param name="serviceProviderCode">The agency&#39;s unique identifier..</param>
        /// <param name="title">The individual&#39;s business title..</param>
        /// <param name="userGroups">userGroups.</param>
        /// <param name="value">The ID of the individual..</param>
        public DepartmentUserBE(string cashierId, DepartmentBE department, string email = default(string),
            string employeeId = default(string), string firstName = default(string), string fullName = default(string),
            string id = default(string), string initial = default(string), string lastName = default(string),
            string middleName = default(string), string namesuffix = default(string), string password = default(string),
            string phone = default(string), string serviceProviderCode = default(string),
            string title = default(string), List<AccelaUserGroupModelBE> userGroups = default(List<AccelaUserGroupModelBE>),
            string value = default(string))
        {
            this.CashierId = cashierId;
            this.Department = department;
            this.Email = email;
            this.EmployeeId = employeeId;
            this.FirstName = firstName;
            this.FullName = fullName;
            this.Id = id;
            this.Initial = initial;
            this.LastName = lastName;
            this.MiddleName = middleName;
            this.Namesuffix = namesuffix;
            this.Password = password;
            this.Phone = phone;
            this.ServiceProviderCode = serviceProviderCode;
            this.Title = title;
            this.UserGroups = userGroups;
            this.Value = value;
        }

        /// <summary>
        /// The unique ID associated with the cashier.
        /// </summary>
        /// <value>The unique ID associated with the cashier.</value>
        [DataMember(Name = "cashierId", EmitDefaultValue = false)]
        public string CashierId { get; set; }

        /// <summary>
        /// Gets or Sets Department
        /// </summary>
        [DataMember(Name = "department", EmitDefaultValue = false)]
        public DepartmentBE Department { get; set; }

        /// <summary>
        /// The individual&#39;s email address.
        /// </summary>
        /// <value>The individual&#39;s email address.</value>
        [DataMember(Name = "email", EmitDefaultValue = false)]
        public string Email { get; set; }

        /// <summary>
        /// The unique ID associated with the employee.
        /// </summary>
        /// <value>The unique ID associated with the employee.</value>
        [DataMember(Name = "employeeId", EmitDefaultValue = false)]
        public string EmployeeId { get; set; }

        /// <summary>
        /// The individual&#39;s first name.
        /// </summary>
        /// <value>The individual&#39;s first name.</value>
        [DataMember(Name = "firstName", EmitDefaultValue = false)]
        public string FirstName { get; set; }

        /// <summary>
        /// The individual&#39;s full name.
        /// </summary>
        /// <value>The individual&#39;s full name.</value>
        [DataMember(Name = "fullName", EmitDefaultValue = false)]
        public string FullName { get; set; }

        /// <summary>
        /// The ID of the individual.
        /// </summary>
        /// <value>The ID of the individual.</value>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }

        /// <summary>
        /// The individual initials.
        /// </summary>
        /// <value>The individual initials.</value>
        [DataMember(Name = "initial", EmitDefaultValue = false)]
        public string Initial { get; set; }

        /// <summary>
        /// The individual&#39;s last name.
        /// </summary>
        /// <value>The individual&#39;s last name.</value>
        [DataMember(Name = "lastName", EmitDefaultValue = false)]
        public string LastName { get; set; }

        /// <summary>
        /// The individual&#39;s middle name.
        /// </summary>
        /// <value>The individual&#39;s middle name.</value>
        [DataMember(Name = "middleName", EmitDefaultValue = false)]
        public string MiddleName { get; set; }

        /// <summary>
        /// The suffix portion of the name of a department staff member, for example; junior, esquire.
        /// </summary>
        /// <value>The suffix portion of the name of a department staff member, for example; junior, esquire.</value>
        [DataMember(Name = "namesuffix", EmitDefaultValue = false)]
        public string Namesuffix { get; set; }

        /// <summary>
        /// The individual&#39;s password
        /// </summary>
        /// <value>The individual&#39;s password</value>
        [DataMember(Name = "password", EmitDefaultValue = false)]
        public string Password { get; set; }

        /// <summary>
        /// The individual&#39;s phone number.
        /// </summary>
        /// <value>The individual&#39;s phone number.</value>
        [DataMember(Name = "phone", EmitDefaultValue = false)]
        public string Phone { get; set; }

        /// <summary>
        /// The agency&#39;s unique identifier.
        /// </summary>
        /// <value>The agency&#39;s unique identifier.</value>
        [DataMember(Name = "serviceProviderCode", EmitDefaultValue = false)]
        public string ServiceProviderCode { get; set; }

        /// <summary>
        /// The individual&#39;s business title.
        /// </summary>
        /// <value>The individual&#39;s business title.</value>
        [DataMember(Name = "title", EmitDefaultValue = false)]
        public string Title { get; set; }

        /// <summary>
        /// Gets or Sets UserGroups
        /// </summary>
        [DataMember(Name = "userGroups", EmitDefaultValue = false)]
        public List<AccelaUserGroupModelBE> UserGroups { get; set; }

        /// <summary>
        /// The ID of the individual.
        /// </summary>
        /// <value>The ID of the individual.</value>
        [DataMember(Name = "value", EmitDefaultValue = false)]
        public string Value { get; set; }

    }
}
