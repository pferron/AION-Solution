/* 
 * Records
 *
 * Construct APIs for transactional records and related record resources
 *
 * OpenAPI spec version: v4
 * 
 *  Hand Coded By Dave Dolson based on existing models 
 * 
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
using SwaggerDateConverter = AccelaRecords.Client.SwaggerDateConverter;

namespace AccelaRecords.Model
{
    /// <summary>
    /// Contains a custom form consisting of the custom form id and custom field name and value pairs. For example in JSON, \&quot;My Custom Field\&quot;: \&quot;My Custom Value\&quot;. The custom field name and its data type are defined in Civic Platform custom forms or custom tables: &lt;br/&gt;**For a Text field**, the maximum length is 256.  &lt;br/&gt;**For a Number field**, any numeric form is allowed, including negative numbers.  &lt;br/&gt;**For a Date field**, the format is MM/dd/yyyy.  &lt;br/&gt;**For a Time field**, the format is hh:mm.  &lt;br/&gt;**For a TextArea field**, the maximum length is 4000 characters, and allows line return characters.  &lt;br/&gt;**For a DropdownList field**, the dropdown list values are in the options[] array.  &lt;br/&gt;**For a CheckBox field**, the (case-sensitive) valid values are \&quot;UNCHECKED\&quot; and \&quot;CHECKED\&quot;.  &lt;br/&gt;**For a Radio(Y/N) field**, the (case-sensitive) valid values are \&quot;Yes\&quot; and \&quot;No\&quot;.
    /// </summary>
    [DataContract]
    public partial class ContactCustomFormAttributeModel :  IEquatable<ContactCustomFormAttributeModel>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContactCustomFormAttributeModel" /> class.
        /// </summary>
        /// <param name="id">The custom form id..</param>
        /// <param name="aCustomFieldName">The name of a custom field..</param>
        /// <param name="aCustomFieldValue">The value of a custom field..</param>
        public ContactCustomFormAttributeModel(string id = default(string), string notify = default(string), string requestorAssociation = default(string), string requestorAssociationOther = default(string) , string grade = default(string))
        {
            this.Id = id;
            this.Notify = notify;
            this.RequestorAssociation = requestorAssociation;
			this.Grade = grade;
            this.RequestorAssociationOther = requestorAssociationOther;
        }
        
        /// <summary>
        /// The custom form id.
        /// </summary>
        /// <value>The custom form id.</value>
        [DataMember(Name="id", EmitDefaultValue=false)]
        public string Id { get; set; }

        /// <summary>
        /// The name of a custom field.
        /// </summary>
        /// <value>The name of a custom field.</value>
        [DataMember(Name="RequestorAssociation", EmitDefaultValue=false)]
        public string RequestorAssociation { get; set; }

        /// <summary>
        /// The value of a custom field.
        /// </summary>
        /// <value>The value of a custom field.</value>
        [DataMember(Name="RequestorAssociationOther", EmitDefaultValue=false)]
        public string RequestorAssociationOther { get; set; }
		
         /// <summary>
        /// The name of a custom field.
        /// </summary>
        /// <value>The name of a custom field.</value>
        [DataMember(Name="Grade", EmitDefaultValue=false)]
        public string Grade { get; set; }

        /// <summary>
        /// The value of a custom field.
        /// </summary>
        /// <value>The value of a custom field.</value>
        [DataMember(Name="Notify", EmitDefaultValue=false)]
        public string Notify  { get; set; }
		
        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ContactCustomFormAttributeModel {\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  RequestorAssociation: ").Append(RequestorAssociation).Append("\n");
            sb.Append("  RequestorAssociationOther: ").Append(RequestorAssociationOther).Append("\n");
			sb.Append("  Grade: ").Append(Grade).Append("\n");
			sb.Append("  Notify: ").Append(Notify).Append("\n");
			
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
            return this.Equals(input as  ContactCustomFormAttributeModel);
        }

        /// <summary>
        /// Returns true if  ContactCustomFormAttributeModel instances are equal
        /// </summary>
        /// <param name="input">Instance of ContactCustomFormAttributeModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ContactCustomFormAttributeModel input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Id == input.Id ||
                    (this.Id != null &&
                    this.Id.Equals(input.Id))
                ) && 
                (
                    this.RequestorAssociation == input.RequestorAssociation||
                    (this.RequestorAssociation != null &&
					this.RequestorAssociation.Equals(input.RequestorAssociation))
                ) && 
                (
                    this.RequestorAssociationOther == input.RequestorAssociationOther ||
                    (this.RequestorAssociationOther != null &&
                    this.RequestorAssociationOther.Equals(input.RequestorAssociationOther))
                )&& 
                (
                    this.Grade == input.Grade ||
                    (this.Grade != null &&
                    this.Grade.Equals(input.Grade))
                ) && 
                (
                    this.Notify == input.Notify ||
                    (this.Notify != null &&
                    this.Notify.Equals(input.Notify))
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
                if (this.Id != null)
                    hashCode = hashCode * 59 + this.Id.GetHashCode();
                if (this.RequestorAssociation != null)
                    hashCode = hashCode * 59 + this.RequestorAssociation.GetHashCode();
              if (this.RequestorAssociationOther != null)
                    hashCode = hashCode * 59 + this.RequestorAssociationOther.GetHashCode();
			    if (this.Grade != null)
                    hashCode = hashCode * 59 + this.Grade.GetHashCode();
					  if (this.Notify != null)
                    hashCode = hashCode * 59 + this.Notify.GetHashCode();
					
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
