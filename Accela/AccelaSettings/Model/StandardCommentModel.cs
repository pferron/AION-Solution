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
    /// StandardCommentModel
    /// </summary>
    [DataContract]
    public partial class StandardCommentModel :  IEquatable<StandardCommentModel>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StandardCommentModel" /> class.
        /// </summary>
        /// <param name="comments">Additional notes about the comment..</param>
        /// <param name="group">group.</param>
        /// <param name="text">The comment text..</param>
        /// <param name="type">type.</param>
        /// <param name="value">The comment value..</param>
        public StandardCommentModel(string comments = default(string), StandardCommentModelGroup group = default(StandardCommentModelGroup), string text = default(string), StandardCommentModelType type = default(StandardCommentModelType), string value = default(string))
        {
            this.Comments = comments;
            this.Group = group;
            this.Text = text;
            this.Type = type;
            this.Value = value;
        }
        
        /// <summary>
        /// Additional notes about the comment.
        /// </summary>
        /// <value>Additional notes about the comment.</value>
        [DataMember(Name="comments", EmitDefaultValue=false)]
        public string Comments { get; set; }

        /// <summary>
        /// Gets or Sets Group
        /// </summary>
        [DataMember(Name="group", EmitDefaultValue=false)]
        public StandardCommentModelGroup Group { get; set; }

        /// <summary>
        /// The comment text.
        /// </summary>
        /// <value>The comment text.</value>
        [DataMember(Name="text", EmitDefaultValue=false)]
        public string Text { get; set; }

        /// <summary>
        /// Gets or Sets Type
        /// </summary>
        [DataMember(Name="type", EmitDefaultValue=false)]
        public StandardCommentModelType Type { get; set; }

        /// <summary>
        /// The comment value.
        /// </summary>
        /// <value>The comment value.</value>
        [DataMember(Name="value", EmitDefaultValue=false)]
        public string Value { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class StandardCommentModel {\n");
            sb.Append("  Comments: ").Append(Comments).Append("\n");
            sb.Append("  Group: ").Append(Group).Append("\n");
            sb.Append("  Text: ").Append(Text).Append("\n");
            sb.Append("  Type: ").Append(Type).Append("\n");
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
            return this.Equals(input as StandardCommentModel);
        }

        /// <summary>
        /// Returns true if StandardCommentModel instances are equal
        /// </summary>
        /// <param name="input">Instance of StandardCommentModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(StandardCommentModel input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Comments == input.Comments ||
                    (this.Comments != null &&
                    this.Comments.Equals(input.Comments))
                ) && 
                (
                    this.Group == input.Group ||
                    (this.Group != null &&
                    this.Group.Equals(input.Group))
                ) && 
                (
                    this.Text == input.Text ||
                    (this.Text != null &&
                    this.Text.Equals(input.Text))
                ) && 
                (
                    this.Type == input.Type ||
                    (this.Type != null &&
                    this.Type.Equals(input.Type))
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
                if (this.Comments != null)
                    hashCode = hashCode * 59 + this.Comments.GetHashCode();
                if (this.Group != null)
                    hashCode = hashCode * 59 + this.Group.GetHashCode();
                if (this.Text != null)
                    hashCode = hashCode * 59 + this.Text.GetHashCode();
                if (this.Type != null)
                    hashCode = hashCode * 59 + this.Type.GetHashCode();
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