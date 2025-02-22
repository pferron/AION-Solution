/* 
 * Citizens
 *
 * The Citizens API include endpoints for citizen users to manage their own accounts, authorized users to manage other accounts, and manage citizen delegates, announcements, and invitations. 
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
using SwaggerDateConverter = AccelaCitizens.Client.SwaggerDateConverter;

namespace AccelaCitizens.Model
{
    /// <summary>
    /// ContactAddressModelV4GetCivicidCitizenaccessContacts
    /// </summary>
    [DataContract]
    public partial class ContactAddressModelV4GetCivicidCitizenaccessContacts :  IEquatable<ContactAddressModelV4GetCivicidCitizenaccessContacts>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ContactAddressModelV4GetCivicidCitizenaccessContacts" /> class.
        /// </summary>
        /// <param name="addressLine1">addressLine1.</param>
        /// <param name="addressLine2">addressLine2.</param>
        /// <param name="addressLine3">addressLine3.</param>
        /// <param name="city">city.</param>
        /// <param name="country">country.</param>
        /// <param name="effectiveDate">effectiveDate.</param>
        /// <param name="entityID">entityID.</param>
        /// <param name="expirationDate">expirationDate.</param>
        /// <param name="fax">fax.</param>
        /// <param name="faxCountryCode">faxCountryCode.</param>
        /// <param name="fullAddress">fullAddress.</param>
        /// <param name="houseNumberAlphaEnd">houseNumberAlphaEnd.</param>
        /// <param name="houseNumberAlphaStart">houseNumberAlphaStart.</param>
        /// <param name="houseNumberEnd">houseNumberEnd.</param>
        /// <param name="houseNumberStart">houseNumberStart.</param>
        /// <param name="id">id.</param>
        /// <param name="levelNumberEnd">levelNumberEnd.</param>
        /// <param name="levelNumberStart">levelNumberStart.</param>
        /// <param name="levelPrefix">levelPrefix.</param>
        /// <param name="phone">phone.</param>
        /// <param name="phoneCountryCode">phoneCountryCode.</param>
        /// <param name="postalCode">postalCode.</param>
        /// <param name="primary">primary.</param>
        /// <param name="recipient">recipient.</param>
        /// <param name="state">state.</param>
        /// <param name="status">status.</param>
        /// <param name="streetDirection">streetDirection.</param>
        /// <param name="streetName">streetName.</param>
        /// <param name="streetPrefix">streetPrefix.</param>
        /// <param name="streetSuffix">streetSuffix.</param>
        /// <param name="streetSuffixDirection">streetSuffixDirection.</param>
        /// <param name="type">type.</param>
        /// <param name="unitEnd">unitEnd.</param>
        /// <param name="unitStart">unitStart.</param>
        /// <param name="unitType">unitType.</param>
        /// <param name="validateFlag">validateFlag.</param>
        public ContactAddressModelV4GetCivicidCitizenaccessContacts(string addressLine1 = default(string), string addressLine2 = default(string), string addressLine3 = default(string), string city = default(string), CompactAddressModelRequestV4PostCitizenaccessRegisterCountry country = default(CompactAddressModelRequestV4PostCitizenaccessRegisterCountry), DateTime? effectiveDate = default(DateTime?), long? entityID = default(long?), DateTime? expirationDate = default(DateTime?), string fax = default(string), string faxCountryCode = default(string), string fullAddress = default(string), string houseNumberAlphaEnd = default(string), string houseNumberAlphaStart = default(string), long? houseNumberEnd = default(long?), long? houseNumberStart = default(long?), long? id = default(long?), string levelNumberEnd = default(string), string levelNumberStart = default(string), string levelPrefix = default(string), string phone = default(string), string phoneCountryCode = default(string), string postalCode = default(string), string primary = default(string), string recipient = default(string), CompactAddressModelRequestV4PostCitizenaccessRegisterCountry state = default(CompactAddressModelRequestV4PostCitizenaccessRegisterCountry), string status = default(string), CompactAddressModelRequestV4PostCitizenaccessRegisterCountry streetDirection = default(CompactAddressModelRequestV4PostCitizenaccessRegisterCountry), string streetName = default(string), string streetPrefix = default(string), CompactAddressModelRequestV4PostCitizenaccessRegisterCountry streetSuffix = default(CompactAddressModelRequestV4PostCitizenaccessRegisterCountry), CompactAddressModelRequestV4PostCitizenaccessRegisterCountry streetSuffixDirection = default(CompactAddressModelRequestV4PostCitizenaccessRegisterCountry), CompactAddressModelRequestV4PostCitizenaccessRegisterCountry type = default(CompactAddressModelRequestV4PostCitizenaccessRegisterCountry), string unitEnd = default(string), string unitStart = default(string), CompactAddressModelRequestV4PostCitizenaccessRegisterCountry unitType = default(CompactAddressModelRequestV4PostCitizenaccessRegisterCountry), string validateFlag = default(string))
        {
            this.AddressLine1 = addressLine1;
            this.AddressLine2 = addressLine2;
            this.AddressLine3 = addressLine3;
            this.City = city;
            this.Country = country;
            this.EffectiveDate = effectiveDate;
            this.EntityID = entityID;
            this.ExpirationDate = expirationDate;
            this.Fax = fax;
            this.FaxCountryCode = faxCountryCode;
            this.FullAddress = fullAddress;
            this.HouseNumberAlphaEnd = houseNumberAlphaEnd;
            this.HouseNumberAlphaStart = houseNumberAlphaStart;
            this.HouseNumberEnd = houseNumberEnd;
            this.HouseNumberStart = houseNumberStart;
            this.Id = id;
            this.LevelNumberEnd = levelNumberEnd;
            this.LevelNumberStart = levelNumberStart;
            this.LevelPrefix = levelPrefix;
            this.Phone = phone;
            this.PhoneCountryCode = phoneCountryCode;
            this.PostalCode = postalCode;
            this.Primary = primary;
            this.Recipient = recipient;
            this.State = state;
            this.Status = status;
            this.StreetDirection = streetDirection;
            this.StreetName = streetName;
            this.StreetPrefix = streetPrefix;
            this.StreetSuffix = streetSuffix;
            this.StreetSuffixDirection = streetSuffixDirection;
            this.Type = type;
            this.UnitEnd = unitEnd;
            this.UnitStart = unitStart;
            this.UnitType = unitType;
            this.ValidateFlag = validateFlag;
        }
        
        /// <summary>
        /// Gets or Sets AddressLine1
        /// </summary>
        [DataMember(Name="addressLine1", EmitDefaultValue=false)]
        public string AddressLine1 { get; set; }

        /// <summary>
        /// Gets or Sets AddressLine2
        /// </summary>
        [DataMember(Name="addressLine2", EmitDefaultValue=false)]
        public string AddressLine2 { get; set; }

        /// <summary>
        /// Gets or Sets AddressLine3
        /// </summary>
        [DataMember(Name="addressLine3", EmitDefaultValue=false)]
        public string AddressLine3 { get; set; }

        /// <summary>
        /// Gets or Sets City
        /// </summary>
        [DataMember(Name="city", EmitDefaultValue=false)]
        public string City { get; set; }

        /// <summary>
        /// Gets or Sets Country
        /// </summary>
        [DataMember(Name="country", EmitDefaultValue=false)]
        public CompactAddressModelRequestV4PostCitizenaccessRegisterCountry Country { get; set; }

        /// <summary>
        /// Gets or Sets EffectiveDate
        /// </summary>
        [DataMember(Name="effectiveDate", EmitDefaultValue=false)]
        public DateTime? EffectiveDate { get; set; }

        /// <summary>
        /// Gets or Sets EntityID
        /// </summary>
        [DataMember(Name="entityID", EmitDefaultValue=false)]
        public long? EntityID { get; set; }

        /// <summary>
        /// Gets or Sets ExpirationDate
        /// </summary>
        [DataMember(Name="expirationDate", EmitDefaultValue=false)]
        public DateTime? ExpirationDate { get; set; }

        /// <summary>
        /// Gets or Sets Fax
        /// </summary>
        [DataMember(Name="fax", EmitDefaultValue=false)]
        public string Fax { get; set; }

        /// <summary>
        /// Gets or Sets FaxCountryCode
        /// </summary>
        [DataMember(Name="faxCountryCode", EmitDefaultValue=false)]
        public string FaxCountryCode { get; set; }

        /// <summary>
        /// Gets or Sets FullAddress
        /// </summary>
        [DataMember(Name="fullAddress", EmitDefaultValue=false)]
        public string FullAddress { get; set; }

        /// <summary>
        /// Gets or Sets HouseNumberAlphaEnd
        /// </summary>
        [DataMember(Name="houseNumberAlphaEnd", EmitDefaultValue=false)]
        public string HouseNumberAlphaEnd { get; set; }

        /// <summary>
        /// Gets or Sets HouseNumberAlphaStart
        /// </summary>
        [DataMember(Name="houseNumberAlphaStart", EmitDefaultValue=false)]
        public string HouseNumberAlphaStart { get; set; }

        /// <summary>
        /// Gets or Sets HouseNumberEnd
        /// </summary>
        [DataMember(Name="houseNumberEnd", EmitDefaultValue=false)]
        public long? HouseNumberEnd { get; set; }

        /// <summary>
        /// Gets or Sets HouseNumberStart
        /// </summary>
        [DataMember(Name="houseNumberStart", EmitDefaultValue=false)]
        public long? HouseNumberStart { get; set; }

        /// <summary>
        /// Gets or Sets Id
        /// </summary>
        [DataMember(Name="id", EmitDefaultValue=false)]
        public long? Id { get; set; }

        /// <summary>
        /// Gets or Sets LevelNumberEnd
        /// </summary>
        [DataMember(Name="levelNumberEnd", EmitDefaultValue=false)]
        public string LevelNumberEnd { get; set; }

        /// <summary>
        /// Gets or Sets LevelNumberStart
        /// </summary>
        [DataMember(Name="levelNumberStart", EmitDefaultValue=false)]
        public string LevelNumberStart { get; set; }

        /// <summary>
        /// Gets or Sets LevelPrefix
        /// </summary>
        [DataMember(Name="levelPrefix", EmitDefaultValue=false)]
        public string LevelPrefix { get; set; }

        /// <summary>
        /// Gets or Sets Phone
        /// </summary>
        [DataMember(Name="phone", EmitDefaultValue=false)]
        public string Phone { get; set; }

        /// <summary>
        /// Gets or Sets PhoneCountryCode
        /// </summary>
        [DataMember(Name="phoneCountryCode", EmitDefaultValue=false)]
        public string PhoneCountryCode { get; set; }

        /// <summary>
        /// Gets or Sets PostalCode
        /// </summary>
        [DataMember(Name="postalCode", EmitDefaultValue=false)]
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or Sets Primary
        /// </summary>
        [DataMember(Name="primary", EmitDefaultValue=false)]
        public string Primary { get; set; }

        /// <summary>
        /// Gets or Sets Recipient
        /// </summary>
        [DataMember(Name="recipient", EmitDefaultValue=false)]
        public string Recipient { get; set; }

        /// <summary>
        /// Gets or Sets State
        /// </summary>
        [DataMember(Name="state", EmitDefaultValue=false)]
        public CompactAddressModelRequestV4PostCitizenaccessRegisterCountry State { get; set; }

        /// <summary>
        /// Gets or Sets Status
        /// </summary>
        [DataMember(Name="status", EmitDefaultValue=false)]
        public string Status { get; set; }

        /// <summary>
        /// Gets or Sets StreetDirection
        /// </summary>
        [DataMember(Name="streetDirection", EmitDefaultValue=false)]
        public CompactAddressModelRequestV4PostCitizenaccessRegisterCountry StreetDirection { get; set; }

        /// <summary>
        /// Gets or Sets StreetName
        /// </summary>
        [DataMember(Name="streetName", EmitDefaultValue=false)]
        public string StreetName { get; set; }

        /// <summary>
        /// Gets or Sets StreetPrefix
        /// </summary>
        [DataMember(Name="streetPrefix", EmitDefaultValue=false)]
        public string StreetPrefix { get; set; }

        /// <summary>
        /// Gets or Sets StreetSuffix
        /// </summary>
        [DataMember(Name="streetSuffix", EmitDefaultValue=false)]
        public CompactAddressModelRequestV4PostCitizenaccessRegisterCountry StreetSuffix { get; set; }

        /// <summary>
        /// Gets or Sets StreetSuffixDirection
        /// </summary>
        [DataMember(Name="streetSuffixDirection", EmitDefaultValue=false)]
        public CompactAddressModelRequestV4PostCitizenaccessRegisterCountry StreetSuffixDirection { get; set; }

        /// <summary>
        /// Gets or Sets Type
        /// </summary>
        [DataMember(Name="type", EmitDefaultValue=false)]
        public CompactAddressModelRequestV4PostCitizenaccessRegisterCountry Type { get; set; }

        /// <summary>
        /// Gets or Sets UnitEnd
        /// </summary>
        [DataMember(Name="unitEnd", EmitDefaultValue=false)]
        public string UnitEnd { get; set; }

        /// <summary>
        /// Gets or Sets UnitStart
        /// </summary>
        [DataMember(Name="unitStart", EmitDefaultValue=false)]
        public string UnitStart { get; set; }

        /// <summary>
        /// Gets or Sets UnitType
        /// </summary>
        [DataMember(Name="unitType", EmitDefaultValue=false)]
        public CompactAddressModelRequestV4PostCitizenaccessRegisterCountry UnitType { get; set; }

        /// <summary>
        /// Gets or Sets ValidateFlag
        /// </summary>
        [DataMember(Name="validateFlag", EmitDefaultValue=false)]
        public string ValidateFlag { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ContactAddressModelV4GetCivicidCitizenaccessContacts {\n");
            sb.Append("  AddressLine1: ").Append(AddressLine1).Append("\n");
            sb.Append("  AddressLine2: ").Append(AddressLine2).Append("\n");
            sb.Append("  AddressLine3: ").Append(AddressLine3).Append("\n");
            sb.Append("  City: ").Append(City).Append("\n");
            sb.Append("  Country: ").Append(Country).Append("\n");
            sb.Append("  EffectiveDate: ").Append(EffectiveDate).Append("\n");
            sb.Append("  EntityID: ").Append(EntityID).Append("\n");
            sb.Append("  ExpirationDate: ").Append(ExpirationDate).Append("\n");
            sb.Append("  Fax: ").Append(Fax).Append("\n");
            sb.Append("  FaxCountryCode: ").Append(FaxCountryCode).Append("\n");
            sb.Append("  FullAddress: ").Append(FullAddress).Append("\n");
            sb.Append("  HouseNumberAlphaEnd: ").Append(HouseNumberAlphaEnd).Append("\n");
            sb.Append("  HouseNumberAlphaStart: ").Append(HouseNumberAlphaStart).Append("\n");
            sb.Append("  HouseNumberEnd: ").Append(HouseNumberEnd).Append("\n");
            sb.Append("  HouseNumberStart: ").Append(HouseNumberStart).Append("\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  LevelNumberEnd: ").Append(LevelNumberEnd).Append("\n");
            sb.Append("  LevelNumberStart: ").Append(LevelNumberStart).Append("\n");
            sb.Append("  LevelPrefix: ").Append(LevelPrefix).Append("\n");
            sb.Append("  Phone: ").Append(Phone).Append("\n");
            sb.Append("  PhoneCountryCode: ").Append(PhoneCountryCode).Append("\n");
            sb.Append("  PostalCode: ").Append(PostalCode).Append("\n");
            sb.Append("  Primary: ").Append(Primary).Append("\n");
            sb.Append("  Recipient: ").Append(Recipient).Append("\n");
            sb.Append("  State: ").Append(State).Append("\n");
            sb.Append("  Status: ").Append(Status).Append("\n");
            sb.Append("  StreetDirection: ").Append(StreetDirection).Append("\n");
            sb.Append("  StreetName: ").Append(StreetName).Append("\n");
            sb.Append("  StreetPrefix: ").Append(StreetPrefix).Append("\n");
            sb.Append("  StreetSuffix: ").Append(StreetSuffix).Append("\n");
            sb.Append("  StreetSuffixDirection: ").Append(StreetSuffixDirection).Append("\n");
            sb.Append("  Type: ").Append(Type).Append("\n");
            sb.Append("  UnitEnd: ").Append(UnitEnd).Append("\n");
            sb.Append("  UnitStart: ").Append(UnitStart).Append("\n");
            sb.Append("  UnitType: ").Append(UnitType).Append("\n");
            sb.Append("  ValidateFlag: ").Append(ValidateFlag).Append("\n");
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
            return this.Equals(input as ContactAddressModelV4GetCivicidCitizenaccessContacts);
        }

        /// <summary>
        /// Returns true if ContactAddressModelV4GetCivicidCitizenaccessContacts instances are equal
        /// </summary>
        /// <param name="input">Instance of ContactAddressModelV4GetCivicidCitizenaccessContacts to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ContactAddressModelV4GetCivicidCitizenaccessContacts input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.AddressLine1 == input.AddressLine1 ||
                    (this.AddressLine1 != null &&
                    this.AddressLine1.Equals(input.AddressLine1))
                ) && 
                (
                    this.AddressLine2 == input.AddressLine2 ||
                    (this.AddressLine2 != null &&
                    this.AddressLine2.Equals(input.AddressLine2))
                ) && 
                (
                    this.AddressLine3 == input.AddressLine3 ||
                    (this.AddressLine3 != null &&
                    this.AddressLine3.Equals(input.AddressLine3))
                ) && 
                (
                    this.City == input.City ||
                    (this.City != null &&
                    this.City.Equals(input.City))
                ) && 
                (
                    this.Country == input.Country ||
                    (this.Country != null &&
                    this.Country.Equals(input.Country))
                ) && 
                (
                    this.EffectiveDate == input.EffectiveDate ||
                    (this.EffectiveDate != null &&
                    this.EffectiveDate.Equals(input.EffectiveDate))
                ) && 
                (
                    this.EntityID == input.EntityID ||
                    (this.EntityID != null &&
                    this.EntityID.Equals(input.EntityID))
                ) && 
                (
                    this.ExpirationDate == input.ExpirationDate ||
                    (this.ExpirationDate != null &&
                    this.ExpirationDate.Equals(input.ExpirationDate))
                ) && 
                (
                    this.Fax == input.Fax ||
                    (this.Fax != null &&
                    this.Fax.Equals(input.Fax))
                ) && 
                (
                    this.FaxCountryCode == input.FaxCountryCode ||
                    (this.FaxCountryCode != null &&
                    this.FaxCountryCode.Equals(input.FaxCountryCode))
                ) && 
                (
                    this.FullAddress == input.FullAddress ||
                    (this.FullAddress != null &&
                    this.FullAddress.Equals(input.FullAddress))
                ) && 
                (
                    this.HouseNumberAlphaEnd == input.HouseNumberAlphaEnd ||
                    (this.HouseNumberAlphaEnd != null &&
                    this.HouseNumberAlphaEnd.Equals(input.HouseNumberAlphaEnd))
                ) && 
                (
                    this.HouseNumberAlphaStart == input.HouseNumberAlphaStart ||
                    (this.HouseNumberAlphaStart != null &&
                    this.HouseNumberAlphaStart.Equals(input.HouseNumberAlphaStart))
                ) && 
                (
                    this.HouseNumberEnd == input.HouseNumberEnd ||
                    (this.HouseNumberEnd != null &&
                    this.HouseNumberEnd.Equals(input.HouseNumberEnd))
                ) && 
                (
                    this.HouseNumberStart == input.HouseNumberStart ||
                    (this.HouseNumberStart != null &&
                    this.HouseNumberStart.Equals(input.HouseNumberStart))
                ) && 
                (
                    this.Id == input.Id ||
                    (this.Id != null &&
                    this.Id.Equals(input.Id))
                ) && 
                (
                    this.LevelNumberEnd == input.LevelNumberEnd ||
                    (this.LevelNumberEnd != null &&
                    this.LevelNumberEnd.Equals(input.LevelNumberEnd))
                ) && 
                (
                    this.LevelNumberStart == input.LevelNumberStart ||
                    (this.LevelNumberStart != null &&
                    this.LevelNumberStart.Equals(input.LevelNumberStart))
                ) && 
                (
                    this.LevelPrefix == input.LevelPrefix ||
                    (this.LevelPrefix != null &&
                    this.LevelPrefix.Equals(input.LevelPrefix))
                ) && 
                (
                    this.Phone == input.Phone ||
                    (this.Phone != null &&
                    this.Phone.Equals(input.Phone))
                ) && 
                (
                    this.PhoneCountryCode == input.PhoneCountryCode ||
                    (this.PhoneCountryCode != null &&
                    this.PhoneCountryCode.Equals(input.PhoneCountryCode))
                ) && 
                (
                    this.PostalCode == input.PostalCode ||
                    (this.PostalCode != null &&
                    this.PostalCode.Equals(input.PostalCode))
                ) && 
                (
                    this.Primary == input.Primary ||
                    (this.Primary != null &&
                    this.Primary.Equals(input.Primary))
                ) && 
                (
                    this.Recipient == input.Recipient ||
                    (this.Recipient != null &&
                    this.Recipient.Equals(input.Recipient))
                ) && 
                (
                    this.State == input.State ||
                    (this.State != null &&
                    this.State.Equals(input.State))
                ) && 
                (
                    this.Status == input.Status ||
                    (this.Status != null &&
                    this.Status.Equals(input.Status))
                ) && 
                (
                    this.StreetDirection == input.StreetDirection ||
                    (this.StreetDirection != null &&
                    this.StreetDirection.Equals(input.StreetDirection))
                ) && 
                (
                    this.StreetName == input.StreetName ||
                    (this.StreetName != null &&
                    this.StreetName.Equals(input.StreetName))
                ) && 
                (
                    this.StreetPrefix == input.StreetPrefix ||
                    (this.StreetPrefix != null &&
                    this.StreetPrefix.Equals(input.StreetPrefix))
                ) && 
                (
                    this.StreetSuffix == input.StreetSuffix ||
                    (this.StreetSuffix != null &&
                    this.StreetSuffix.Equals(input.StreetSuffix))
                ) && 
                (
                    this.StreetSuffixDirection == input.StreetSuffixDirection ||
                    (this.StreetSuffixDirection != null &&
                    this.StreetSuffixDirection.Equals(input.StreetSuffixDirection))
                ) && 
                (
                    this.Type == input.Type ||
                    (this.Type != null &&
                    this.Type.Equals(input.Type))
                ) && 
                (
                    this.UnitEnd == input.UnitEnd ||
                    (this.UnitEnd != null &&
                    this.UnitEnd.Equals(input.UnitEnd))
                ) && 
                (
                    this.UnitStart == input.UnitStart ||
                    (this.UnitStart != null &&
                    this.UnitStart.Equals(input.UnitStart))
                ) && 
                (
                    this.UnitType == input.UnitType ||
                    (this.UnitType != null &&
                    this.UnitType.Equals(input.UnitType))
                ) && 
                (
                    this.ValidateFlag == input.ValidateFlag ||
                    (this.ValidateFlag != null &&
                    this.ValidateFlag.Equals(input.ValidateFlag))
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
                if (this.AddressLine1 != null)
                    hashCode = hashCode * 59 + this.AddressLine1.GetHashCode();
                if (this.AddressLine2 != null)
                    hashCode = hashCode * 59 + this.AddressLine2.GetHashCode();
                if (this.AddressLine3 != null)
                    hashCode = hashCode * 59 + this.AddressLine3.GetHashCode();
                if (this.City != null)
                    hashCode = hashCode * 59 + this.City.GetHashCode();
                if (this.Country != null)
                    hashCode = hashCode * 59 + this.Country.GetHashCode();
                if (this.EffectiveDate != null)
                    hashCode = hashCode * 59 + this.EffectiveDate.GetHashCode();
                if (this.EntityID != null)
                    hashCode = hashCode * 59 + this.EntityID.GetHashCode();
                if (this.ExpirationDate != null)
                    hashCode = hashCode * 59 + this.ExpirationDate.GetHashCode();
                if (this.Fax != null)
                    hashCode = hashCode * 59 + this.Fax.GetHashCode();
                if (this.FaxCountryCode != null)
                    hashCode = hashCode * 59 + this.FaxCountryCode.GetHashCode();
                if (this.FullAddress != null)
                    hashCode = hashCode * 59 + this.FullAddress.GetHashCode();
                if (this.HouseNumberAlphaEnd != null)
                    hashCode = hashCode * 59 + this.HouseNumberAlphaEnd.GetHashCode();
                if (this.HouseNumberAlphaStart != null)
                    hashCode = hashCode * 59 + this.HouseNumberAlphaStart.GetHashCode();
                if (this.HouseNumberEnd != null)
                    hashCode = hashCode * 59 + this.HouseNumberEnd.GetHashCode();
                if (this.HouseNumberStart != null)
                    hashCode = hashCode * 59 + this.HouseNumberStart.GetHashCode();
                if (this.Id != null)
                    hashCode = hashCode * 59 + this.Id.GetHashCode();
                if (this.LevelNumberEnd != null)
                    hashCode = hashCode * 59 + this.LevelNumberEnd.GetHashCode();
                if (this.LevelNumberStart != null)
                    hashCode = hashCode * 59 + this.LevelNumberStart.GetHashCode();
                if (this.LevelPrefix != null)
                    hashCode = hashCode * 59 + this.LevelPrefix.GetHashCode();
                if (this.Phone != null)
                    hashCode = hashCode * 59 + this.Phone.GetHashCode();
                if (this.PhoneCountryCode != null)
                    hashCode = hashCode * 59 + this.PhoneCountryCode.GetHashCode();
                if (this.PostalCode != null)
                    hashCode = hashCode * 59 + this.PostalCode.GetHashCode();
                if (this.Primary != null)
                    hashCode = hashCode * 59 + this.Primary.GetHashCode();
                if (this.Recipient != null)
                    hashCode = hashCode * 59 + this.Recipient.GetHashCode();
                if (this.State != null)
                    hashCode = hashCode * 59 + this.State.GetHashCode();
                if (this.Status != null)
                    hashCode = hashCode * 59 + this.Status.GetHashCode();
                if (this.StreetDirection != null)
                    hashCode = hashCode * 59 + this.StreetDirection.GetHashCode();
                if (this.StreetName != null)
                    hashCode = hashCode * 59 + this.StreetName.GetHashCode();
                if (this.StreetPrefix != null)
                    hashCode = hashCode * 59 + this.StreetPrefix.GetHashCode();
                if (this.StreetSuffix != null)
                    hashCode = hashCode * 59 + this.StreetSuffix.GetHashCode();
                if (this.StreetSuffixDirection != null)
                    hashCode = hashCode * 59 + this.StreetSuffixDirection.GetHashCode();
                if (this.Type != null)
                    hashCode = hashCode * 59 + this.Type.GetHashCode();
                if (this.UnitEnd != null)
                    hashCode = hashCode * 59 + this.UnitEnd.GetHashCode();
                if (this.UnitStart != null)
                    hashCode = hashCode * 59 + this.UnitStart.GetHashCode();
                if (this.UnitType != null)
                    hashCode = hashCode * 59 + this.UnitType.GetHashCode();
                if (this.ValidateFlag != null)
                    hashCode = hashCode * 59 + this.ValidateFlag.GetHashCode();
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
