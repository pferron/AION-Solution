/* 
 * Settings
 *
 * The Settings API provides configuration values that have been defined in Civic Platform Administration, typically as standard choice values. The Settings APIs are helpful when you need reference or custom-configured values in your API calls.
 *
 * OpenAPI spec version: v4
 * 
 * Generated by: https://github.com/swagger-api/swagger-codegen.git
 */

using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using SwaggerDateConverter = AccelaSettings.Client.SwaggerDateConverter;

namespace AccelaSettings.Model
{
    /// <summary>
    /// UserModel
    /// </summary>
    [DataContract]
    public partial class UserModel :  IEquatable<UserModel>, IValidatableObject
    {
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
        public UserModel(string cashierId = default(string), DepartmentModel department = default(DepartmentModel), string email = default(string), string employeeId = default(string), string firstName = default(string), string fullName = default(string), string id = default(string), string initial = default(string), string lastName = default(string), string middleName = default(string), string namesuffix = default(string), string password = default(string), string phone = default(string), string serviceProviderCode = default(string), string title = default(string), List<UserGroupModel> userGroups = default(List<UserGroupModel>), string value = default(string))
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
        [DataMember(Name="cashierId", EmitDefaultValue=false)]
        public string CashierId { get; set; }

        /// <summary>
        /// Gets or Sets Department
        /// </summary>
        [DataMember(Name="department", EmitDefaultValue=false)]
        public DepartmentModel Department { get; set; }

        /// <summary>
        /// The individual&#39;s email address.
        /// </summary>
        /// <value>The individual&#39;s email address.</value>
        [DataMember(Name="email", EmitDefaultValue=false)]
        public string Email { get; set; }

        /// <summary>
        /// The unique ID associated with the employee.
        /// </summary>
        /// <value>The unique ID associated with the employee.</value>
        [DataMember(Name="employeeId", EmitDefaultValue=false)]
        public string EmployeeId { get; set; }

        /// <summary>
        /// The individual&#39;s first name.
        /// </summary>
        /// <value>The individual&#39;s first name.</value>
        [DataMember(Name="firstName", EmitDefaultValue=false)]
        public string FirstName { get; set; }

        /// <summary>
        /// The individual&#39;s full name.
        /// </summary>
        /// <value>The individual&#39;s full name.</value>
        [DataMember(Name="fullName", EmitDefaultValue=false)]
        public string FullName { get; set; }

        /// <summary>
        /// The ID of the individual.
        /// </summary>
        /// <value>The ID of the individual.</value>
        [DataMember(Name="id", EmitDefaultValue=false)]
        public string Id { get; set; }

        /// <summary>
        /// The individual initials.
        /// </summary>
        /// <value>The individual initials.</value>
        [DataMember(Name="initial", EmitDefaultValue=false)]
        public string Initial { get; set; }

        /// <summary>
        /// The individual&#39;s last name.
        /// </summary>
        /// <value>The individual&#39;s last name.</value>
        [DataMember(Name="lastName", EmitDefaultValue=false)]
        public string LastName { get; set; }

        /// <summary>
        /// The individual&#39;s middle name.
        /// </summary>
        /// <value>The individual&#39;s middle name.</value>
        [DataMember(Name="middleName", EmitDefaultValue=false)]
        public string MiddleName { get; set; }

        /// <summary>
        /// The suffix portion of the name of a department staff member, for example; junior, esquire.
        /// </summary>
        /// <value>The suffix portion of the name of a department staff member, for example; junior, esquire.</value>
        [DataMember(Name="namesuffix", EmitDefaultValue=false)]
        public string Namesuffix { get; set; }

        /// <summary>
        /// The individual&#39;s password
        /// </summary>
        /// <value>The individual&#39;s password</value>
        [DataMember(Name="password", EmitDefaultValue=false)]
        public string Password { get; set; }

        /// <summary>
        /// The individual&#39;s phone number.
        /// </summary>
        /// <value>The individual&#39;s phone number.</value>
        [DataMember(Name="phone", EmitDefaultValue=false)]
        public string Phone { get; set; }

        /// <summary>
        /// The agency&#39;s unique identifier.
        /// </summary>
        /// <value>The agency&#39;s unique identifier.</value>
        [DataMember(Name="serviceProviderCode", EmitDefaultValue=false)]
        public string ServiceProviderCode { get; set; }

        /// <summary>
        /// The individual&#39;s business title.
        /// </summary>
        /// <value>The individual&#39;s business title.</value>
        [DataMember(Name="title", EmitDefaultValue=false)]
        public string Title { get; set; }

        /// <summary>
        /// Gets or Sets UserGroups
        /// </summary>
        [DataMember(Name="userGroups", EmitDefaultValue=false)]
        public List<UserGroupModel> UserGroups { get; set; }

        /// <summary>
        /// The ID of the individual.
        /// </summary>
        /// <value>The ID of the individual.</value>
        [DataMember(Name="value", EmitDefaultValue=false)]
        public string Value { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class UserModel {\n");
            sb.Append("  CashierId: ").Append(CashierId).Append("\n");
            sb.Append("  Department: ").Append(Department).Append("\n");
            sb.Append("  Email: ").Append(Email).Append("\n");
            sb.Append("  EmployeeId: ").Append(EmployeeId).Append("\n");
            sb.Append("  FirstName: ").Append(FirstName).Append("\n");
            sb.Append("  FullName: ").Append(FullName).Append("\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  Initial: ").Append(Initial).Append("\n");
            sb.Append("  LastName: ").Append(LastName).Append("\n");
            sb.Append("  MiddleName: ").Append(MiddleName).Append("\n");
            sb.Append("  Namesuffix: ").Append(Namesuffix).Append("\n");
            sb.Append("  Password: ").Append(Password).Append("\n");
            sb.Append("  Phone: ").Append(Phone).Append("\n");
            sb.Append("  ServiceProviderCode: ").Append(ServiceProviderCode).Append("\n");
            sb.Append("  Title: ").Append(Title).Append("\n");
            sb.Append("  UserGroups: ").Append(UserGroups).Append("\n");
            sb.Append("  Value: ").Append(Value).Append("\n");
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
            return this.Equals(input as UserModel);
        }

        /// <summary>
        /// Returns true if UserModel instances are equal
        /// </summary>
        /// <param name="input">Instance of UserModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(UserModel input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.CashierId == input.CashierId ||
                    (this.CashierId != null &&
                    this.CashierId.Equals(input.CashierId))
                ) && 
                (
                    this.Department == input.Department ||
                    (this.Department != null &&
                    this.Department.Equals(input.Department))
                ) && 
                (
                    this.Email == input.Email ||
                    (this.Email != null &&
                    this.Email.Equals(input.Email))
                ) && 
                (
                    this.EmployeeId == input.EmployeeId ||
                    (this.EmployeeId != null &&
                    this.EmployeeId.Equals(input.EmployeeId))
                ) && 
                (
                    this.FirstName == input.FirstName ||
                    (this.FirstName != null &&
                    this.FirstName.Equals(input.FirstName))
                ) && 
                (
                    this.FullName == input.FullName ||
                    (this.FullName != null &&
                    this.FullName.Equals(input.FullName))
                ) && 
                (
                    this.Id == input.Id ||
                    (this.Id != null &&
                    this.Id.Equals(input.Id))
                ) && 
                (
                    this.Initial == input.Initial ||
                    (this.Initial != null &&
                    this.Initial.Equals(input.Initial))
                ) && 
                (
                    this.LastName == input.LastName ||
                    (this.LastName != null &&
                    this.LastName.Equals(input.LastName))
                ) && 
                (
                    this.MiddleName == input.MiddleName ||
                    (this.MiddleName != null &&
                    this.MiddleName.Equals(input.MiddleName))
                ) && 
                (
                    this.Namesuffix == input.Namesuffix ||
                    (this.Namesuffix != null &&
                    this.Namesuffix.Equals(input.Namesuffix))
                ) && 
                (
                    this.Password == input.Password ||
                    (this.Password != null &&
                    this.Password.Equals(input.Password))
                ) && 
                (
                    this.Phone == input.Phone ||
                    (this.Phone != null &&
                    this.Phone.Equals(input.Phone))
                ) && 
                (
                    this.ServiceProviderCode == input.ServiceProviderCode ||
                    (this.ServiceProviderCode != null &&
                    this.ServiceProviderCode.Equals(input.ServiceProviderCode))
                ) && 
                (
                    this.Title == input.Title ||
                    (this.Title != null &&
                    this.Title.Equals(input.Title))
                ) && 
                (
                    this.UserGroups == input.UserGroups ||
                    this.UserGroups != null &&
                    this.UserGroups.SequenceEqual(input.UserGroups)
                ) && 
                (
                    this.Value == input.Value ||
                    (this.Value != null &&
                    this.Value.Equals(input.Value))
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
                if (this.CashierId != null)
                    hashCode = hashCode * 59 + this.CashierId.GetHashCode();
                if (this.Department != null)
                    hashCode = hashCode * 59 + this.Department.GetHashCode();
                if (this.Email != null)
                    hashCode = hashCode * 59 + this.Email.GetHashCode();
                if (this.EmployeeId != null)
                    hashCode = hashCode * 59 + this.EmployeeId.GetHashCode();
                if (this.FirstName != null)
                    hashCode = hashCode * 59 + this.FirstName.GetHashCode();
                if (this.FullName != null)
                    hashCode = hashCode * 59 + this.FullName.GetHashCode();
                if (this.Id != null)
                    hashCode = hashCode * 59 + this.Id.GetHashCode();
                if (this.Initial != null)
                    hashCode = hashCode * 59 + this.Initial.GetHashCode();
                if (this.LastName != null)
                    hashCode = hashCode * 59 + this.LastName.GetHashCode();
                if (this.MiddleName != null)
                    hashCode = hashCode * 59 + this.MiddleName.GetHashCode();
                if (this.Namesuffix != null)
                    hashCode = hashCode * 59 + this.Namesuffix.GetHashCode();
                if (this.Password != null)
                    hashCode = hashCode * 59 + this.Password.GetHashCode();
                if (this.Phone != null)
                    hashCode = hashCode * 59 + this.Phone.GetHashCode();
                if (this.ServiceProviderCode != null)
                    hashCode = hashCode * 59 + this.ServiceProviderCode.GetHashCode();
                if (this.Title != null)
                    hashCode = hashCode * 59 + this.Title.GetHashCode();
                if (this.UserGroups != null)
                    hashCode = hashCode * 59 + this.UserGroups.GetHashCode();
                if (this.Value != null)
                    hashCode = hashCode * 59 + this.Value.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }

}
