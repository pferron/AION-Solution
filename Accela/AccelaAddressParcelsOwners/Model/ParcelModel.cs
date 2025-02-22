/* 
 * Addresses, Parcels, Owners
 *
 * Use the Address-Parcel-Owner (\"APO\") API to get, create, and update reference information about addresses, parcels, and owners used in land or property management solutions. Because reference APO can be associated to multiple transactional records, a reference APO object cannot be deleted.
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
using SwaggerDateConverter = AccelaAddressParcelsOwners.Client.SwaggerDateConverter;

namespace AccelaAddressParcelsOwners.Model
{
    /// <summary>
    /// ParcelModel
    /// </summary>
    [DataContract]
    public partial class ParcelModel :  IEquatable<ParcelModel>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ParcelModel" /> class.
        /// </summary>
        /// <param name="block">The block number associated with the parcel..</param>
        /// <param name="book">A reference to the physical location of parcel information in the County Assessor&#39;s office..</param>
        /// <param name="censusTract">The unique number assigned by the Census Bureau that identifies the tract to which this parcel belongs..</param>
        /// <param name="councilDistrict">The council district to which the parcel belongs..</param>
        /// <param name="customForms">customForms.</param>
        /// <param name="exemptionValue">The total value of any tax exemptions that apply to the land within the parcel..</param>
        /// <param name="gisSequenceNumber">The GIS object ID of the parcel..</param>
        /// <param name="id">The system id of the parcel assigned by the Civic Platform server..</param>
        /// <param name="improvedValue">The total value of any improvements to the land within the parcel..</param>
        /// <param name="isPrimary">Indicates whether or not to designate the parcel as the primary parcel..</param>
        /// <param name="landValue">The total value of the land within the parcel..</param>
        /// <param name="legalDescription">The legal description of the parcel..</param>
        /// <param name="lot">The lot name..</param>
        /// <param name="mapNumber">The unique map number that identifies the map for this parcel..</param>
        /// <param name="mapReferenceInfo">The map reference for this parcel..</param>
        /// <param name="page">A reference to the physical location of the parcel information in the records of the County Assessor (or other responsible department)..</param>
        /// <param name="parcel">The official parcel name or number, as determined by the county assessor or other responsible department..</param>
        /// <param name="parcelArea">The total area of the parcel. Your agency determines the standard unit of measure..</param>
        /// <param name="parcelNumber">The alpha-numeric parcel number..</param>
        /// <param name="planArea">The total area of the parcel. Your agency determines the standard unit of measure..</param>
        /// <param name="range">When land is surveyed using the rectangular-survey system, range represents the measure of units east and west of the base line..</param>
        /// <param name="section">A piece of a township measuring 640 acres, one square mile, numbered with reference to the base line and meridian line..</param>
        /// <param name="status">status.</param>
        /// <param name="subdivision">subdivision.</param>
        /// <param name="supervisorDistrict">The supervisor district to which the parcel belongs..</param>
        /// <param name="township">When land is surveyed using the rectangular-survey system, township represents the measure of units North or South of the base line. Townships typically measure 6 miles to a side, or 36 square miles..</param>
        /// <param name="tract">The name of the tract associated with this application. A tract may contain one or more related parcels..</param>
        public ParcelModel(string block = default(string), string book = default(string), string censusTract = default(string), string councilDistrict = default(string), List<CustomAttributeModel> customForms = default(List<CustomAttributeModel>), double? exemptionValue = default(double?), long? gisSequenceNumber = default(long?), string id = default(string), double? improvedValue = default(double?), string isPrimary = default(string), double? landValue = default(double?), string legalDescription = default(string), string lot = default(string), string mapNumber = default(string), string mapReferenceInfo = default(string), string page = default(string), string parcel = default(string), double? parcelArea = default(double?), string parcelNumber = default(string), string planArea = default(string), string range = default(string), long? section = default(long?), ParcelModelStatus status = default(ParcelModelStatus), ParcelModelSubdivision subdivision = default(ParcelModelSubdivision), string supervisorDistrict = default(string), string township = default(string), string tract = default(string))
        {
            this.Block = block;
            this.Book = book;
            this.CensusTract = censusTract;
            this.CouncilDistrict = councilDistrict;
            this.CustomForms = customForms;
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
        /// The block number associated with the parcel.
        /// </summary>
        /// <value>The block number associated with the parcel.</value>
        [DataMember(Name="block", EmitDefaultValue=false)]
        public string Block { get; set; }

        /// <summary>
        /// A reference to the physical location of parcel information in the County Assessor&#39;s office.
        /// </summary>
        /// <value>A reference to the physical location of parcel information in the County Assessor&#39;s office.</value>
        [DataMember(Name="book", EmitDefaultValue=false)]
        public string Book { get; set; }

        /// <summary>
        /// The unique number assigned by the Census Bureau that identifies the tract to which this parcel belongs.
        /// </summary>
        /// <value>The unique number assigned by the Census Bureau that identifies the tract to which this parcel belongs.</value>
        [DataMember(Name="censusTract", EmitDefaultValue=false)]
        public string CensusTract { get; set; }

        /// <summary>
        /// The council district to which the parcel belongs.
        /// </summary>
        /// <value>The council district to which the parcel belongs.</value>
        [DataMember(Name="councilDistrict", EmitDefaultValue=false)]
        public string CouncilDistrict { get; set; }

        /// <summary>
        /// Gets or Sets CustomForms
        /// </summary>
        [DataMember(Name="customForms", EmitDefaultValue=false)]
        public List<CustomAttributeModel> CustomForms { get; set; }

        /// <summary>
        /// The total value of any tax exemptions that apply to the land within the parcel.
        /// </summary>
        /// <value>The total value of any tax exemptions that apply to the land within the parcel.</value>
        [DataMember(Name="exemptionValue", EmitDefaultValue=false)]
        public double? ExemptionValue { get; set; }

        /// <summary>
        /// The GIS object ID of the parcel.
        /// </summary>
        /// <value>The GIS object ID of the parcel.</value>
        [DataMember(Name="gisSequenceNumber", EmitDefaultValue=false)]
        public long? GisSequenceNumber { get; set; }

        /// <summary>
        /// The system id of the parcel assigned by the Civic Platform server.
        /// </summary>
        /// <value>The system id of the parcel assigned by the Civic Platform server.</value>
        [DataMember(Name="id", EmitDefaultValue=false)]
        public string Id { get; set; }

        /// <summary>
        /// The total value of any improvements to the land within the parcel.
        /// </summary>
        /// <value>The total value of any improvements to the land within the parcel.</value>
        [DataMember(Name="improvedValue", EmitDefaultValue=false)]
        public double? ImprovedValue { get; set; }

        /// <summary>
        /// Indicates whether or not to designate the parcel as the primary parcel.
        /// </summary>
        /// <value>Indicates whether or not to designate the parcel as the primary parcel.</value>
        [DataMember(Name="isPrimary", EmitDefaultValue=false)]
        public string IsPrimary { get; set; }

        /// <summary>
        /// The total value of the land within the parcel.
        /// </summary>
        /// <value>The total value of the land within the parcel.</value>
        [DataMember(Name="landValue", EmitDefaultValue=false)]
        public double? LandValue { get; set; }

        /// <summary>
        /// The legal description of the parcel.
        /// </summary>
        /// <value>The legal description of the parcel.</value>
        [DataMember(Name="legalDescription", EmitDefaultValue=false)]
        public string LegalDescription { get; set; }

        /// <summary>
        /// The lot name.
        /// </summary>
        /// <value>The lot name.</value>
        [DataMember(Name="lot", EmitDefaultValue=false)]
        public string Lot { get; set; }

        /// <summary>
        /// The unique map number that identifies the map for this parcel.
        /// </summary>
        /// <value>The unique map number that identifies the map for this parcel.</value>
        [DataMember(Name="mapNumber", EmitDefaultValue=false)]
        public string MapNumber { get; set; }

        /// <summary>
        /// The map reference for this parcel.
        /// </summary>
        /// <value>The map reference for this parcel.</value>
        [DataMember(Name="mapReferenceInfo", EmitDefaultValue=false)]
        public string MapReferenceInfo { get; set; }

        /// <summary>
        /// A reference to the physical location of the parcel information in the records of the County Assessor (or other responsible department).
        /// </summary>
        /// <value>A reference to the physical location of the parcel information in the records of the County Assessor (or other responsible department).</value>
        [DataMember(Name="page", EmitDefaultValue=false)]
        public string Page { get; set; }

        /// <summary>
        /// The official parcel name or number, as determined by the county assessor or other responsible department.
        /// </summary>
        /// <value>The official parcel name or number, as determined by the county assessor or other responsible department.</value>
        [DataMember(Name="parcel", EmitDefaultValue=false)]
        public string Parcel { get; set; }

        /// <summary>
        /// The total area of the parcel. Your agency determines the standard unit of measure.
        /// </summary>
        /// <value>The total area of the parcel. Your agency determines the standard unit of measure.</value>
        [DataMember(Name="parcelArea", EmitDefaultValue=false)]
        public double? ParcelArea { get; set; }

        /// <summary>
        /// The alpha-numeric parcel number.
        /// </summary>
        /// <value>The alpha-numeric parcel number.</value>
        [DataMember(Name="parcelNumber", EmitDefaultValue=false)]
        public string ParcelNumber { get; set; }

        /// <summary>
        /// The total area of the parcel. Your agency determines the standard unit of measure.
        /// </summary>
        /// <value>The total area of the parcel. Your agency determines the standard unit of measure.</value>
        [DataMember(Name="planArea", EmitDefaultValue=false)]
        public string PlanArea { get; set; }

        /// <summary>
        /// When land is surveyed using the rectangular-survey system, range represents the measure of units east and west of the base line.
        /// </summary>
        /// <value>When land is surveyed using the rectangular-survey system, range represents the measure of units east and west of the base line.</value>
        [DataMember(Name="range", EmitDefaultValue=false)]
        public string Range { get; set; }

        /// <summary>
        /// A piece of a township measuring 640 acres, one square mile, numbered with reference to the base line and meridian line.
        /// </summary>
        /// <value>A piece of a township measuring 640 acres, one square mile, numbered with reference to the base line and meridian line.</value>
        [DataMember(Name="section", EmitDefaultValue=false)]
        public long? Section { get; set; }

        /// <summary>
        /// Gets or Sets Status
        /// </summary>
        [DataMember(Name="status", EmitDefaultValue=false)]
        public ParcelModelStatus Status { get; set; }

        /// <summary>
        /// Gets or Sets Subdivision
        /// </summary>
        [DataMember(Name="subdivision", EmitDefaultValue=false)]
        public ParcelModelSubdivision Subdivision { get; set; }

        /// <summary>
        /// The supervisor district to which the parcel belongs.
        /// </summary>
        /// <value>The supervisor district to which the parcel belongs.</value>
        [DataMember(Name="supervisorDistrict", EmitDefaultValue=false)]
        public string SupervisorDistrict { get; set; }

        /// <summary>
        /// When land is surveyed using the rectangular-survey system, township represents the measure of units North or South of the base line. Townships typically measure 6 miles to a side, or 36 square miles.
        /// </summary>
        /// <value>When land is surveyed using the rectangular-survey system, township represents the measure of units North or South of the base line. Townships typically measure 6 miles to a side, or 36 square miles.</value>
        [DataMember(Name="township", EmitDefaultValue=false)]
        public string Township { get; set; }

        /// <summary>
        /// The name of the tract associated with this application. A tract may contain one or more related parcels.
        /// </summary>
        /// <value>The name of the tract associated with this application. A tract may contain one or more related parcels.</value>
        [DataMember(Name="tract", EmitDefaultValue=false)]
        public string Tract { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ParcelModel {\n");
            sb.Append("  Block: ").Append(Block).Append("\n");
            sb.Append("  Book: ").Append(Book).Append("\n");
            sb.Append("  CensusTract: ").Append(CensusTract).Append("\n");
            sb.Append("  CouncilDistrict: ").Append(CouncilDistrict).Append("\n");
            sb.Append("  CustomForms: ").Append(CustomForms).Append("\n");
            sb.Append("  ExemptionValue: ").Append(ExemptionValue).Append("\n");
            sb.Append("  GisSequenceNumber: ").Append(GisSequenceNumber).Append("\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  ImprovedValue: ").Append(ImprovedValue).Append("\n");
            sb.Append("  IsPrimary: ").Append(IsPrimary).Append("\n");
            sb.Append("  LandValue: ").Append(LandValue).Append("\n");
            sb.Append("  LegalDescription: ").Append(LegalDescription).Append("\n");
            sb.Append("  Lot: ").Append(Lot).Append("\n");
            sb.Append("  MapNumber: ").Append(MapNumber).Append("\n");
            sb.Append("  MapReferenceInfo: ").Append(MapReferenceInfo).Append("\n");
            sb.Append("  Page: ").Append(Page).Append("\n");
            sb.Append("  Parcel: ").Append(Parcel).Append("\n");
            sb.Append("  ParcelArea: ").Append(ParcelArea).Append("\n");
            sb.Append("  ParcelNumber: ").Append(ParcelNumber).Append("\n");
            sb.Append("  PlanArea: ").Append(PlanArea).Append("\n");
            sb.Append("  Range: ").Append(Range).Append("\n");
            sb.Append("  Section: ").Append(Section).Append("\n");
            sb.Append("  Status: ").Append(Status).Append("\n");
            sb.Append("  Subdivision: ").Append(Subdivision).Append("\n");
            sb.Append("  SupervisorDistrict: ").Append(SupervisorDistrict).Append("\n");
            sb.Append("  Township: ").Append(Township).Append("\n");
            sb.Append("  Tract: ").Append(Tract).Append("\n");
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
            return this.Equals(input as ParcelModel);
        }

        /// <summary>
        /// Returns true if ParcelModel instances are equal
        /// </summary>
        /// <param name="input">Instance of ParcelModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ParcelModel input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Block == input.Block ||
                    (this.Block != null &&
                    this.Block.Equals(input.Block))
                ) && 
                (
                    this.Book == input.Book ||
                    (this.Book != null &&
                    this.Book.Equals(input.Book))
                ) && 
                (
                    this.CensusTract == input.CensusTract ||
                    (this.CensusTract != null &&
                    this.CensusTract.Equals(input.CensusTract))
                ) && 
                (
                    this.CouncilDistrict == input.CouncilDistrict ||
                    (this.CouncilDistrict != null &&
                    this.CouncilDistrict.Equals(input.CouncilDistrict))
                ) && 
                (
                    this.CustomForms == input.CustomForms ||
                    this.CustomForms != null &&
                    this.CustomForms.SequenceEqual(input.CustomForms)
                ) && 
                (
                    this.ExemptionValue == input.ExemptionValue ||
                    (this.ExemptionValue != null &&
                    this.ExemptionValue.Equals(input.ExemptionValue))
                ) && 
                (
                    this.GisSequenceNumber == input.GisSequenceNumber ||
                    (this.GisSequenceNumber != null &&
                    this.GisSequenceNumber.Equals(input.GisSequenceNumber))
                ) && 
                (
                    this.Id == input.Id ||
                    (this.Id != null &&
                    this.Id.Equals(input.Id))
                ) && 
                (
                    this.ImprovedValue == input.ImprovedValue ||
                    (this.ImprovedValue != null &&
                    this.ImprovedValue.Equals(input.ImprovedValue))
                ) && 
                (
                    this.IsPrimary == input.IsPrimary ||
                    (this.IsPrimary != null &&
                    this.IsPrimary.Equals(input.IsPrimary))
                ) && 
                (
                    this.LandValue == input.LandValue ||
                    (this.LandValue != null &&
                    this.LandValue.Equals(input.LandValue))
                ) && 
                (
                    this.LegalDescription == input.LegalDescription ||
                    (this.LegalDescription != null &&
                    this.LegalDescription.Equals(input.LegalDescription))
                ) && 
                (
                    this.Lot == input.Lot ||
                    (this.Lot != null &&
                    this.Lot.Equals(input.Lot))
                ) && 
                (
                    this.MapNumber == input.MapNumber ||
                    (this.MapNumber != null &&
                    this.MapNumber.Equals(input.MapNumber))
                ) && 
                (
                    this.MapReferenceInfo == input.MapReferenceInfo ||
                    (this.MapReferenceInfo != null &&
                    this.MapReferenceInfo.Equals(input.MapReferenceInfo))
                ) && 
                (
                    this.Page == input.Page ||
                    (this.Page != null &&
                    this.Page.Equals(input.Page))
                ) && 
                (
                    this.Parcel == input.Parcel ||
                    (this.Parcel != null &&
                    this.Parcel.Equals(input.Parcel))
                ) && 
                (
                    this.ParcelArea == input.ParcelArea ||
                    (this.ParcelArea != null &&
                    this.ParcelArea.Equals(input.ParcelArea))
                ) && 
                (
                    this.ParcelNumber == input.ParcelNumber ||
                    (this.ParcelNumber != null &&
                    this.ParcelNumber.Equals(input.ParcelNumber))
                ) && 
                (
                    this.PlanArea == input.PlanArea ||
                    (this.PlanArea != null &&
                    this.PlanArea.Equals(input.PlanArea))
                ) && 
                (
                    this.Range == input.Range ||
                    (this.Range != null &&
                    this.Range.Equals(input.Range))
                ) && 
                (
                    this.Section == input.Section ||
                    (this.Section != null &&
                    this.Section.Equals(input.Section))
                ) && 
                (
                    this.Status == input.Status ||
                    (this.Status != null &&
                    this.Status.Equals(input.Status))
                ) && 
                (
                    this.Subdivision == input.Subdivision ||
                    (this.Subdivision != null &&
                    this.Subdivision.Equals(input.Subdivision))
                ) && 
                (
                    this.SupervisorDistrict == input.SupervisorDistrict ||
                    (this.SupervisorDistrict != null &&
                    this.SupervisorDistrict.Equals(input.SupervisorDistrict))
                ) && 
                (
                    this.Township == input.Township ||
                    (this.Township != null &&
                    this.Township.Equals(input.Township))
                ) && 
                (
                    this.Tract == input.Tract ||
                    (this.Tract != null &&
                    this.Tract.Equals(input.Tract))
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
                if (this.Block != null)
                    hashCode = hashCode * 59 + this.Block.GetHashCode();
                if (this.Book != null)
                    hashCode = hashCode * 59 + this.Book.GetHashCode();
                if (this.CensusTract != null)
                    hashCode = hashCode * 59 + this.CensusTract.GetHashCode();
                if (this.CouncilDistrict != null)
                    hashCode = hashCode * 59 + this.CouncilDistrict.GetHashCode();
                if (this.CustomForms != null)
                    hashCode = hashCode * 59 + this.CustomForms.GetHashCode();
                if (this.ExemptionValue != null)
                    hashCode = hashCode * 59 + this.ExemptionValue.GetHashCode();
                if (this.GisSequenceNumber != null)
                    hashCode = hashCode * 59 + this.GisSequenceNumber.GetHashCode();
                if (this.Id != null)
                    hashCode = hashCode * 59 + this.Id.GetHashCode();
                if (this.ImprovedValue != null)
                    hashCode = hashCode * 59 + this.ImprovedValue.GetHashCode();
                if (this.IsPrimary != null)
                    hashCode = hashCode * 59 + this.IsPrimary.GetHashCode();
                if (this.LandValue != null)
                    hashCode = hashCode * 59 + this.LandValue.GetHashCode();
                if (this.LegalDescription != null)
                    hashCode = hashCode * 59 + this.LegalDescription.GetHashCode();
                if (this.Lot != null)
                    hashCode = hashCode * 59 + this.Lot.GetHashCode();
                if (this.MapNumber != null)
                    hashCode = hashCode * 59 + this.MapNumber.GetHashCode();
                if (this.MapReferenceInfo != null)
                    hashCode = hashCode * 59 + this.MapReferenceInfo.GetHashCode();
                if (this.Page != null)
                    hashCode = hashCode * 59 + this.Page.GetHashCode();
                if (this.Parcel != null)
                    hashCode = hashCode * 59 + this.Parcel.GetHashCode();
                if (this.ParcelArea != null)
                    hashCode = hashCode * 59 + this.ParcelArea.GetHashCode();
                if (this.ParcelNumber != null)
                    hashCode = hashCode * 59 + this.ParcelNumber.GetHashCode();
                if (this.PlanArea != null)
                    hashCode = hashCode * 59 + this.PlanArea.GetHashCode();
                if (this.Range != null)
                    hashCode = hashCode * 59 + this.Range.GetHashCode();
                if (this.Section != null)
                    hashCode = hashCode * 59 + this.Section.GetHashCode();
                if (this.Status != null)
                    hashCode = hashCode * 59 + this.Status.GetHashCode();
                if (this.Subdivision != null)
                    hashCode = hashCode * 59 + this.Subdivision.GetHashCode();
                if (this.SupervisorDistrict != null)
                    hashCode = hashCode * 59 + this.SupervisorDistrict.GetHashCode();
                if (this.Township != null)
                    hashCode = hashCode * 59 + this.Township.GetHashCode();
                if (this.Tract != null)
                    hashCode = hashCode * 59 + this.Tract.GetHashCode();
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
