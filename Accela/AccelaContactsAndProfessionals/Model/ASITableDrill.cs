/* 
 * Contacts and Professionals
 *
 * The Contacts and Professionals APIs enable apps to manage reference contacts and licensed professionals.
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
using SwaggerDateConverter = AccelaContactsAndProfessionals.Client.SwaggerDateConverter;

namespace AccelaContactsAndProfessionals.Model
{
    /// <summary>
    /// Contains drilldown field information.
    /// </summary>
    [DataContract]
    public partial class ASITableDrill :  IEquatable<ASITableDrill>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ASITableDrill" /> class.
        /// </summary>
        /// <param name="children">children.</param>
        /// <param name="isRoot">isRoot.</param>
        public ASITableDrill(List<ChildDrill> children = default(List<ChildDrill>), bool? isRoot = default(bool?))
        {
            this.Children = children;
            this.IsRoot = isRoot;
        }
        
        /// <summary>
        /// Gets or Sets Children
        /// </summary>
        [DataMember(Name="children", EmitDefaultValue=false)]
        public List<ChildDrill> Children { get; set; }

        /// <summary>
        /// Gets or Sets IsRoot
        /// </summary>
        [DataMember(Name="isRoot", EmitDefaultValue=false)]
        public bool? IsRoot { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ASITableDrill {\n");
            sb.Append("  Children: ").Append(Children).Append("\n");
            sb.Append("  IsRoot: ").Append(IsRoot).Append("\n");
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
            return this.Equals(input as ASITableDrill);
        }

        /// <summary>
        /// Returns true if ASITableDrill instances are equal
        /// </summary>
        /// <param name="input">Instance of ASITableDrill to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ASITableDrill input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Children == input.Children ||
                    this.Children != null &&
                    this.Children.SequenceEqual(input.Children)
                ) && 
                (
                    this.IsRoot == input.IsRoot ||
                    (this.IsRoot != null &&
                    this.IsRoot.Equals(input.IsRoot))
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
                if (this.Children != null)
                    hashCode = hashCode * 59 + this.Children.GetHashCode();
                if (this.IsRoot != null)
                    hashCode = hashCode * 59 + this.IsRoot.GetHashCode();
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