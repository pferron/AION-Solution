﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Meck.Shared.Accela
{
    /// <summary>
    /// RecordContactModel
    /// </summary>
  
    public partial class RecordContactModelBE  
    {
        /// <summary>
        /// Indicates whether or not to designate the contact as the primary contact Only one address can be primary at any given time.
        /// </summary>
        /// <value>Indicates whether or not to designate the contact as the primary contact Only one address can be primary at any given time.</value>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum IsPrimaryEnum
        {

            /// <summary>
            /// Enum Y for value: Y
            /// </summary>
            [EnumMember(Value = "Y")]
            Y = 1,

            /// <summary>
            /// Enum N for value: N
            /// </summary>
            [EnumMember(Value = "N")]
            N = 2
        }

        /// <summary>
        /// Indicates whether or not to designate the contact as the primary contact Only one address can be primary at any given time.
        /// </summary>
        /// <value>Indicates whether or not to designate the contact as the primary contact Only one address can be primary at any given time.</value>
        [DataMember(Name = "isPrimary", EmitDefaultValue = false)]
        public IsPrimaryEnum? IsPrimary { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordContactModelBE" /> class.
        /// </summary>
        /// <param name="address">address.</param>
        /// <param name="birthCity">birthCity.</param>
        /// <param name="birthDate">The birth date..</param>
        /// <param name="birthRegion">birthRegion.</param>
        /// <param name="birthState">birthState.</param>
        /// <param name="businessName">A secondary business name for the applicable individual..</param>
        /// <param name="comment">A comment about the inspection contact..</param>
        /// <param name="deceasedDate">The deceased date..</param>
        /// <param name="driverLicenseNumber">The driver&#39;s license number of the contact. This field is active only when the Contact Type selected is Individual..</param>
        /// <param name="driverLicenseState">driverLicenseState.</param>
        /// <param name="email">The contact&#39;s email address..</param>
        /// <param name="endDate">The date when the contact address ceases to be active..</param>
        /// <param name="fax">The fax number for the contact..</param>
        /// <param name="faxCountryCode">Fax Number Country Code.</param>
        /// <param name="federalEmployerId">The Federal Employer Identification Number. It is used to identify a business for tax purposes..</param>
        /// <param name="firstName">The contact&#39;s first name..</param>
        /// <param name="fullName">The contact&#39;s full name. .</param>
        /// <param name="gender">gender.</param>
        /// <param name="id">The contact system id assigned by the Civic Platform server..</param>
        /// <param name="individualOrOrganization">The organization to which the contact belongs. This field is only active when the Contact Type selected is Organization..</param>
        /// <param name="isPrimary">Indicates whether or not to designate the contact as the primary contact Only one address can be primary at any given time..</param>
        /// <param name="lastName">The last name (surname)..</param>
        /// <param name="middleName">The middle name. .</param>
        /// <param name="organizationName">The organization to which the contact belongs. This field is only active when the Contact Type selected is Organization..</param>
        /// <param name="passportNumber">The contact&#39;s passport number. This field is only active when the Contact Type selected is Individual..</param>
        /// <param name="phone1">The primary telephone number of the contact..</param>
        /// <param name="phone1CountryCode">Phone Number 1 Country Code.</param>
        /// <param name="phone2">The secondary telephone number of the contact..</param>
        /// <param name="phone2CountryCode">Phone Number 2 Country Code.</param>
        /// <param name="phone3">The tertiary telephone number for the contact..</param>
        /// <param name="phone3CountryCode">Phone Number 3 Country Code.</param>
        /// <param name="postOfficeBox">The post office box number..</param>
        /// <param name="preferredChannel">preferredChannel.</param>
        /// <param name="race">race.</param>
        /// <param name="recordId">recordId.</param>
        /// <param name="referenceContactId">The unique Id generated for a contact stored in the sytem..</param>
        /// <param name="relation">relation.</param>
        /// <param name="salutation">salutation.</param>
        /// <param name="socialSecurityNumber">The individual&#39;s social security number. This field is only active when the Contact Type selected is Individual..</param>
        /// <param name="startDate">The date the contact became active..</param>
        /// <param name="stateIdNumber">The contact&#39;s state ID number. This field is only active when the Contact Type selected is Individual..</param>
        /// <param name="status">status.</param>
        /// <param name="suffix">The contact name suffix..</param>
        /// <param name="title">The individual&#39;s business title..</param>
        /// <param name="tradeName">The contact&#39;s preferred business or trade name. This field is active only when the Contact Type selected is Organization..</param>
        /// <param name="type">type.</param>
        public RecordContactModelBE(CompactAddressModelBE address = default(CompactAddressModelBE), RecordContactSimpleModelBirthCityBE birthCity = default(RecordContactSimpleModelBirthCityBE), DateTime? birthDate = default(DateTime?), RecordContactSimpleModelBirthRegionBE birthRegion = default(RecordContactSimpleModelBirthRegionBE), RecordContactSimpleModelBirthStateBE birthState = default(RecordContactSimpleModelBirthStateBE), string businessName = default(string), string comment = default(string), DateTime? deceasedDate = default(DateTime?), string driverLicenseNumber = default(string), RecordContactSimpleModelDriverLicenseStateBE driverLicenseState = default(RecordContactSimpleModelDriverLicenseStateBE), string email = default(string), DateTime? endDate = default(DateTime?), string fax = default(string), string faxCountryCode = default(string), string federalEmployerId = default(string), string firstName = default(string), string fullName = default(string), RecordContactSimpleModelGenderBE gender = default(RecordContactSimpleModelGenderBE), string id = default(string), string individualOrOrganization = default(string), IsPrimaryEnum? isPrimary = default(IsPrimaryEnum?), string lastName = default(string), string middleName = default(string), string organizationName = default(string), string passportNumber = default(string), string phone1 = default(string), string phone1CountryCode = default(string), string phone2 = default(string), string phone2CountryCode = default(string), string phone3 = default(string), string phone3CountryCode = default(string), string postOfficeBox = default(string), RecordContactSimpleModelPreferredChannelBE preferredChannel = default(RecordContactSimpleModelPreferredChannelBE), RecordContactSimpleModelRaceBE race = default(RecordContactSimpleModelRaceBE), RecordIdModelBE recordId = default(RecordIdModelBE), string referenceContactId = default(string), RecordContactSimpleModelRelationBE relation = default(RecordContactSimpleModelRelationBE), RecordContactSimpleModelSalutationBE salutation = default(RecordContactSimpleModelSalutationBE), string socialSecurityNumber = default(string), DateTime? startDate = default(DateTime?), string stateIdNumber = default(string), RecordContactSimpleModelStatusBE status = default(RecordContactSimpleModelStatusBE), string suffix = default(string), string title = default(string), string tradeName = default(string), RecordContactSimpleModelTypeBE type = default(RecordContactSimpleModelTypeBE))
        {
            this.Address = address;
            this.BirthCity = birthCity;
            this.BirthDate = birthDate;
            this.BirthRegion = birthRegion;
            this.BirthState = birthState;
            this.BusinessName = businessName;
            this.Comment = comment;
            this.DeceasedDate = deceasedDate;
            this.DriverLicenseNumber = driverLicenseNumber;
            this.DriverLicenseState = driverLicenseState;
            this.Email = email;
            this.EndDate = endDate;
            this.Fax = fax;
            this.FaxCountryCode = faxCountryCode;
            this.FederalEmployerId = federalEmployerId;
            this.FirstName = firstName;
            this.FullName = fullName;
            this.Gender = gender;
            this.Id = id;
            this.IndividualOrOrganization = individualOrOrganization;
            this.IsPrimary = isPrimary;
            this.LastName = lastName;
            this.MiddleName = middleName;
            this.OrganizationName = organizationName;
            this.PassportNumber = passportNumber;
            this.Phone1 = phone1;
            this.Phone1CountryCode = phone1CountryCode;
            this.Phone2 = phone2;
            this.Phone2CountryCode = phone2CountryCode;
            this.Phone3 = phone3;
            this.Phone3CountryCode = phone3CountryCode;
            this.PostOfficeBox = postOfficeBox;
            this.PreferredChannel = preferredChannel;
            this.Race = race;
            this.RecordId = recordId;
            this.ReferenceContactId = referenceContactId;
            this.Relation = relation;
            this.Salutation = salutation;
            this.SocialSecurityNumber = socialSecurityNumber;
            this.StartDate = startDate;
            this.StateIdNumber = stateIdNumber;
            this.Status = status;
            this.Suffix = suffix;
            this.Title = title;
            this.TradeName = tradeName;
            this.Type = type;
        }

        /// <summary>
        /// Gets or Sets Address
        /// </summary>
        [DataMember(Name = "address", EmitDefaultValue = false)]
        public CompactAddressModelBE Address { get; set; }

        /// <summary>
        /// Gets or Sets BirthCity
        /// </summary>
        [DataMember(Name = "birthCity", EmitDefaultValue = false)]
        public RecordContactSimpleModelBirthCityBE BirthCity { get; set; }

        /// <summary>
        /// The birth date.
        /// </summary>
        /// <value>The birth date.</value>
        [DataMember(Name = "birthDate", EmitDefaultValue = false)]
        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// Gets or Sets BirthRegion
        /// </summary>
        [DataMember(Name = "birthRegion", EmitDefaultValue = false)]
        public RecordContactSimpleModelBirthRegionBE BirthRegion { get; set; }

        /// <summary>
        /// Gets or Sets BirthState
        /// </summary>
        [DataMember(Name = "birthState", EmitDefaultValue = false)]
        public RecordContactSimpleModelBirthStateBE BirthState { get; set; }

        /// <summary>
        /// A secondary business name for the applicable individual.
        /// </summary>
        /// <value>A secondary business name for the applicable individual.</value>
        [DataMember(Name = "businessName", EmitDefaultValue = false)]
        public string BusinessName { get; set; }

        /// <summary>
        /// A comment about the inspection contact.
        /// </summary>
        /// <value>A comment about the inspection contact.</value>
        [DataMember(Name = "comment", EmitDefaultValue = false)]
        public string Comment { get; set; }

        /// <summary>
        /// The deceased date.
        /// </summary>
        /// <value>The deceased date.</value>
        [DataMember(Name = "deceasedDate", EmitDefaultValue = false)]
        public DateTime? DeceasedDate { get; set; }

        /// <summary>
        /// The driver&#39;s license number of the contact. This field is active only when the Contact Type selected is Individual.
        /// </summary>
        /// <value>The driver&#39;s license number of the contact. This field is active only when the Contact Type selected is Individual.</value>
        [DataMember(Name = "driverLicenseNumber", EmitDefaultValue = false)]
        public string DriverLicenseNumber { get; set; }

        /// <summary>
        /// Gets or Sets DriverLicenseState
        /// </summary>
        [DataMember(Name = "driverLicenseState", EmitDefaultValue = false)]
        public RecordContactSimpleModelDriverLicenseStateBE DriverLicenseState { get; set; }

        /// <summary>
        /// The contact&#39;s email address.
        /// </summary>
        /// <value>The contact&#39;s email address.</value>
        [DataMember(Name = "email", EmitDefaultValue = false)]
        public string Email { get; set; }

        /// <summary>
        /// The date when the contact address ceases to be active.
        /// </summary>
        /// <value>The date when the contact address ceases to be active.</value>
        [DataMember(Name = "endDate", EmitDefaultValue = false)]
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// The fax number for the contact.
        /// </summary>
        /// <value>The fax number for the contact.</value>
        [DataMember(Name = "fax", EmitDefaultValue = false)]
        public string Fax { get; set; }

        /// <summary>
        /// Fax Number Country Code
        /// </summary>
        /// <value>Fax Number Country Code</value>
        [DataMember(Name = "faxCountryCode", EmitDefaultValue = false)]
        public string FaxCountryCode { get; set; }

        /// <summary>
        /// The Federal Employer Identification Number. It is used to identify a business for tax purposes.
        /// </summary>
        /// <value>The Federal Employer Identification Number. It is used to identify a business for tax purposes.</value>
        [DataMember(Name = "federalEmployerId", EmitDefaultValue = false)]
        public string FederalEmployerId { get; set; }

        /// <summary>
        /// The contact&#39;s first name.
        /// </summary>
        /// <value>The contact&#39;s first name.</value>
        [DataMember(Name = "firstName", EmitDefaultValue = false)]
        public string FirstName { get; set; }

        /// <summary>
        /// The contact&#39;s full name. 
        /// </summary>
        /// <value>The contact&#39;s full name. </value>
        [DataMember(Name = "fullName", EmitDefaultValue = false)]
        public string FullName { get; set; }

        /// <summary>
        /// Gets or Sets Gender
        /// </summary>
        [DataMember(Name = "gender", EmitDefaultValue = false)]
        public RecordContactSimpleModelGenderBE Gender { get; set; }

        /// <summary>
        /// The contact system id assigned by the Civic Platform server.
        /// </summary>
        /// <value>The contact system id assigned by the Civic Platform server.</value>
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }

        /// <summary>
        /// The organization to which the contact belongs. This field is only active when the Contact Type selected is Organization.
        /// </summary>
        /// <value>The organization to which the contact belongs. This field is only active when the Contact Type selected is Organization.</value>
        [DataMember(Name = "individualOrOrganization", EmitDefaultValue = false)]
        public string IndividualOrOrganization { get; set; }


        /// <summary>
        /// The last name (surname).
        /// </summary>
        /// <value>The last name (surname).</value>
        [DataMember(Name = "lastName", EmitDefaultValue = false)]
        public string LastName { get; set; }

        /// <summary>
        /// The middle name. 
        /// </summary>
        /// <value>The middle name. </value>
        [DataMember(Name = "middleName", EmitDefaultValue = false)]
        public string MiddleName { get; set; }

        /// <summary>
        /// The organization to which the contact belongs. This field is only active when the Contact Type selected is Organization.
        /// </summary>
        /// <value>The organization to which the contact belongs. This field is only active when the Contact Type selected is Organization.</value>
        [DataMember(Name = "organizationName", EmitDefaultValue = false)]
        public string OrganizationName { get; set; }

        /// <summary>
        /// The contact&#39;s passport number. This field is only active when the Contact Type selected is Individual.
        /// </summary>
        /// <value>The contact&#39;s passport number. This field is only active when the Contact Type selected is Individual.</value>
        [DataMember(Name = "passportNumber", EmitDefaultValue = false)]
        public string PassportNumber { get; set; }

        /// <summary>
        /// The primary telephone number of the contact.
        /// </summary>
        /// <value>The primary telephone number of the contact.</value>
        [DataMember(Name = "phone1", EmitDefaultValue = false)]
        public string Phone1 { get; set; }

        /// <summary>
        /// Phone Number 1 Country Code
        /// </summary>
        /// <value>Phone Number 1 Country Code</value>
        [DataMember(Name = "phone1CountryCode", EmitDefaultValue = false)]
        public string Phone1CountryCode { get; set; }

        /// <summary>
        /// The secondary telephone number of the contact.
        /// </summary>
        /// <value>The secondary telephone number of the contact.</value>
        [DataMember(Name = "phone2", EmitDefaultValue = false)]
        public string Phone2 { get; set; }

        /// <summary>
        /// Phone Number 2 Country Code
        /// </summary>
        /// <value>Phone Number 2 Country Code</value>
        [DataMember(Name = "phone2CountryCode", EmitDefaultValue = false)]
        public string Phone2CountryCode { get; set; }

        /// <summary>
        /// The tertiary telephone number for the contact.
        /// </summary>
        /// <value>The tertiary telephone number for the contact.</value>
        [DataMember(Name = "phone3", EmitDefaultValue = false)]
        public string Phone3 { get; set; }

        /// <summary>
        /// Phone Number 3 Country Code
        /// </summary>
        /// <value>Phone Number 3 Country Code</value>
        [DataMember(Name = "phone3CountryCode", EmitDefaultValue = false)]
        public string Phone3CountryCode { get; set; }

        /// <summary>
        /// The post office box number.
        /// </summary>
        /// <value>The post office box number.</value>
        [DataMember(Name = "postOfficeBox", EmitDefaultValue = false)]
        public string PostOfficeBox { get; set; }

        /// <summary>
        /// Gets or Sets PreferredChannel
        /// </summary>
        [DataMember(Name = "preferredChannel", EmitDefaultValue = false)]
        public RecordContactSimpleModelPreferredChannelBE PreferredChannel { get; set; }

        /// <summary>
        /// Gets or Sets Race
        /// </summary>
        [DataMember(Name = "race", EmitDefaultValue = false)]
        public RecordContactSimpleModelRaceBE Race { get; set; }

        /// <summary>
        /// Gets or Sets RecordId
        /// </summary>
        [DataMember(Name = "recordId", EmitDefaultValue = false)]
        public RecordIdModelBE RecordId { get; set; }

        /// <summary>
        /// The unique Id generated for a contact stored in the sytem.
        /// </summary>
        /// <value>The unique Id generated for a contact stored in the sytem.</value>
        [DataMember(Name = "referenceContactId", EmitDefaultValue = false)]
        public string ReferenceContactId { get; set; }

        /// <summary>
        /// Gets or Sets Relation
        /// </summary>
        [DataMember(Name = "relation", EmitDefaultValue = false)]
        public RecordContactSimpleModelRelationBE Relation { get; set; }

        /// <summary>
        /// Gets or Sets Salutation
        /// </summary>
        [DataMember(Name = "salutation", EmitDefaultValue = false)]
        public RecordContactSimpleModelSalutationBE Salutation { get; set; }

        /// <summary>
        /// The individual&#39;s social security number. This field is only active when the Contact Type selected is Individual.
        /// </summary>
        /// <value>The individual&#39;s social security number. This field is only active when the Contact Type selected is Individual.</value>
        [DataMember(Name = "socialSecurityNumber", EmitDefaultValue = false)]
        public string SocialSecurityNumber { get; set; }

        /// <summary>
        /// The date the contact became active.
        /// </summary>
        /// <value>The date the contact became active.</value>
        [DataMember(Name = "startDate", EmitDefaultValue = false)]
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// The contact&#39;s state ID number. This field is only active when the Contact Type selected is Individual.
        /// </summary>
        /// <value>The contact&#39;s state ID number. This field is only active when the Contact Type selected is Individual.</value>
        [DataMember(Name = "stateIdNumber", EmitDefaultValue = false)]
        public string StateIdNumber { get; set; }

        /// <summary>
        /// Gets or Sets Status
        /// </summary>
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public RecordContactSimpleModelStatusBE Status { get; set; }

        /// <summary>
        /// The contact name suffix.
        /// </summary>
        /// <value>The contact name suffix.</value>
        [DataMember(Name = "suffix", EmitDefaultValue = false)]
        public string Suffix { get; set; }

        /// <summary>
        /// The individual&#39;s business title.
        /// </summary>
        /// <value>The individual&#39;s business title.</value>
        [DataMember(Name = "title", EmitDefaultValue = false)]
        public string Title { get; set; }

        /// <summary>
        /// The contact&#39;s preferred business or trade name. This field is active only when the Contact Type selected is Organization.
        /// </summary>
        /// <value>The contact&#39;s preferred business or trade name. This field is active only when the Contact Type selected is Organization.</value>
        [DataMember(Name = "tradeName", EmitDefaultValue = false)]
        public string TradeName { get; set; }

        /// <summary>
        /// Gets or Sets Type
        /// </summary>
        [DataMember(Name = "type", EmitDefaultValue = false)]
        public RecordContactSimpleModelTypeBE Type { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class RecordContactModel {\n");
            sb.Append("  Address: ").Append(Address).Append("\n");
            sb.Append("  BirthCity: ").Append(BirthCity).Append("\n");
            sb.Append("  BirthDate: ").Append(BirthDate).Append("\n");
            sb.Append("  BirthRegion: ").Append(BirthRegion).Append("\n");
            sb.Append("  BirthState: ").Append(BirthState).Append("\n");
            sb.Append("  BusinessName: ").Append(BusinessName).Append("\n");
            sb.Append("  Comment: ").Append(Comment).Append("\n");
            sb.Append("  DeceasedDate: ").Append(DeceasedDate).Append("\n");
            sb.Append("  DriverLicenseNumber: ").Append(DriverLicenseNumber).Append("\n");
            sb.Append("  DriverLicenseState: ").Append(DriverLicenseState).Append("\n");
            sb.Append("  Email: ").Append(Email).Append("\n");
            sb.Append("  EndDate: ").Append(EndDate).Append("\n");
            sb.Append("  Fax: ").Append(Fax).Append("\n");
            sb.Append("  FaxCountryCode: ").Append(FaxCountryCode).Append("\n");
            sb.Append("  FederalEmployerId: ").Append(FederalEmployerId).Append("\n");
            sb.Append("  FirstName: ").Append(FirstName).Append("\n");
            sb.Append("  FullName: ").Append(FullName).Append("\n");
            sb.Append("  Gender: ").Append(Gender).Append("\n");
            sb.Append("  Id: ").Append(Id).Append("\n");
            sb.Append("  IndividualOrOrganization: ").Append(IndividualOrOrganization).Append("\n");
            sb.Append("  IsPrimary: ").Append(IsPrimary).Append("\n");
            sb.Append("  LastName: ").Append(LastName).Append("\n");
            sb.Append("  MiddleName: ").Append(MiddleName).Append("\n");
            sb.Append("  OrganizationName: ").Append(OrganizationName).Append("\n");
            sb.Append("  PassportNumber: ").Append(PassportNumber).Append("\n");
            sb.Append("  Phone1: ").Append(Phone1).Append("\n");
            sb.Append("  Phone1CountryCode: ").Append(Phone1CountryCode).Append("\n");
            sb.Append("  Phone2: ").Append(Phone2).Append("\n");
            sb.Append("  Phone2CountryCode: ").Append(Phone2CountryCode).Append("\n");
            sb.Append("  Phone3: ").Append(Phone3).Append("\n");
            sb.Append("  Phone3CountryCode: ").Append(Phone3CountryCode).Append("\n");
            sb.Append("  PostOfficeBox: ").Append(PostOfficeBox).Append("\n");
            sb.Append("  PreferredChannel: ").Append(PreferredChannel).Append("\n");
            sb.Append("  Race: ").Append(Race).Append("\n");
            sb.Append("  RecordId: ").Append(RecordId).Append("\n");
            sb.Append("  ReferenceContactId: ").Append(ReferenceContactId).Append("\n");
            sb.Append("  Relation: ").Append(Relation).Append("\n");
            sb.Append("  Salutation: ").Append(Salutation).Append("\n");
            sb.Append("  SocialSecurityNumber: ").Append(SocialSecurityNumber).Append("\n");
            sb.Append("  StartDate: ").Append(StartDate).Append("\n");
            sb.Append("  StateIdNumber: ").Append(StateIdNumber).Append("\n");
            sb.Append("  Status: ").Append(Status).Append("\n");
            sb.Append("  Suffix: ").Append(Suffix).Append("\n");
            sb.Append("  Title: ").Append(Title).Append("\n");
            sb.Append("  TradeName: ").Append(TradeName).Append("\n");
            sb.Append("  Type: ").Append(Type).Append("\n");
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
            return this.Equals(input as RecordContactModelBE);
        }

        /// <summary>
        /// Returns true if RecordContactModel instances are equal
        /// </summary>
        /// <param name="input">Instance of RecordContactModel to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(RecordContactModelBE input)
        {
            if (input == null)
                return false;

            return
                (
                    this.Address == input.Address ||
                    (this.Address != null &&
                    this.Address.Equals(input.Address))
                ) &&
                (
                    this.BirthCity == input.BirthCity ||
                    (this.BirthCity != null &&
                    this.BirthCity.Equals(input.BirthCity))
                ) &&
                (
                    this.BirthDate == input.BirthDate ||
                    (this.BirthDate != null &&
                    this.BirthDate.Equals(input.BirthDate))
                ) &&
                (
                    this.BirthRegion == input.BirthRegion ||
                    (this.BirthRegion != null &&
                    this.BirthRegion.Equals(input.BirthRegion))
                ) &&
                (
                    this.BirthState == input.BirthState ||
                    (this.BirthState != null &&
                    this.BirthState.Equals(input.BirthState))
                ) &&
                (
                    this.BusinessName == input.BusinessName ||
                    (this.BusinessName != null &&
                    this.BusinessName.Equals(input.BusinessName))
                ) &&
                (
                    this.Comment == input.Comment ||
                    (this.Comment != null &&
                    this.Comment.Equals(input.Comment))
                ) &&
                (
                    this.DeceasedDate == input.DeceasedDate ||
                    (this.DeceasedDate != null &&
                    this.DeceasedDate.Equals(input.DeceasedDate))
                ) &&
                (
                    this.DriverLicenseNumber == input.DriverLicenseNumber ||
                    (this.DriverLicenseNumber != null &&
                    this.DriverLicenseNumber.Equals(input.DriverLicenseNumber))
                ) &&
                (
                    this.DriverLicenseState == input.DriverLicenseState ||
                    (this.DriverLicenseState != null &&
                    this.DriverLicenseState.Equals(input.DriverLicenseState))
                ) &&
                (
                    this.Email == input.Email ||
                    (this.Email != null &&
                    this.Email.Equals(input.Email))
                ) &&
                (
                    this.EndDate == input.EndDate ||
                    (this.EndDate != null &&
                    this.EndDate.Equals(input.EndDate))
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
                    this.FederalEmployerId == input.FederalEmployerId ||
                    (this.FederalEmployerId != null &&
                    this.FederalEmployerId.Equals(input.FederalEmployerId))
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
                    this.Gender == input.Gender ||
                    (this.Gender != null &&
                    this.Gender.Equals(input.Gender))
                ) &&
                (
                    this.Id == input.Id ||
                    (this.Id != null &&
                    this.Id.Equals(input.Id))
                ) &&
                (
                    this.IndividualOrOrganization == input.IndividualOrOrganization ||
                    (this.IndividualOrOrganization != null &&
                    this.IndividualOrOrganization.Equals(input.IndividualOrOrganization))
                ) &&
                (
                    this.IsPrimary == input.IsPrimary ||
                    (this.IsPrimary != null &&
                    this.IsPrimary.Equals(input.IsPrimary))
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
                    this.OrganizationName == input.OrganizationName ||
                    (this.OrganizationName != null &&
                    this.OrganizationName.Equals(input.OrganizationName))
                ) &&
                (
                    this.PassportNumber == input.PassportNumber ||
                    (this.PassportNumber != null &&
                    this.PassportNumber.Equals(input.PassportNumber))
                ) &&
                (
                    this.Phone1 == input.Phone1 ||
                    (this.Phone1 != null &&
                    this.Phone1.Equals(input.Phone1))
                ) &&
                (
                    this.Phone1CountryCode == input.Phone1CountryCode ||
                    (this.Phone1CountryCode != null &&
                    this.Phone1CountryCode.Equals(input.Phone1CountryCode))
                ) &&
                (
                    this.Phone2 == input.Phone2 ||
                    (this.Phone2 != null &&
                    this.Phone2.Equals(input.Phone2))
                ) &&
                (
                    this.Phone2CountryCode == input.Phone2CountryCode ||
                    (this.Phone2CountryCode != null &&
                    this.Phone2CountryCode.Equals(input.Phone2CountryCode))
                ) &&
                (
                    this.Phone3 == input.Phone3 ||
                    (this.Phone3 != null &&
                    this.Phone3.Equals(input.Phone3))
                ) &&
                (
                    this.Phone3CountryCode == input.Phone3CountryCode ||
                    (this.Phone3CountryCode != null &&
                    this.Phone3CountryCode.Equals(input.Phone3CountryCode))
                ) &&
                (
                    this.PostOfficeBox == input.PostOfficeBox ||
                    (this.PostOfficeBox != null &&
                    this.PostOfficeBox.Equals(input.PostOfficeBox))
                ) &&
                (
                    this.PreferredChannel == input.PreferredChannel ||
                    (this.PreferredChannel != null &&
                    this.PreferredChannel.Equals(input.PreferredChannel))
                ) &&
                (
                    this.Race == input.Race ||
                    (this.Race != null &&
                    this.Race.Equals(input.Race))
                ) &&
                (
                    this.RecordId == input.RecordId ||
                    (this.RecordId != null &&
                    this.RecordId.Equals(input.RecordId))
                ) &&
                (
                    this.ReferenceContactId == input.ReferenceContactId ||
                    (this.ReferenceContactId != null &&
                    this.ReferenceContactId.Equals(input.ReferenceContactId))
                ) &&
                (
                    this.Relation == input.Relation ||
                    (this.Relation != null &&
                    this.Relation.Equals(input.Relation))
                ) &&
                (
                    this.Salutation == input.Salutation ||
                    (this.Salutation != null &&
                    this.Salutation.Equals(input.Salutation))
                ) &&
                (
                    this.SocialSecurityNumber == input.SocialSecurityNumber ||
                    (this.SocialSecurityNumber != null &&
                    this.SocialSecurityNumber.Equals(input.SocialSecurityNumber))
                ) &&
                (
                    this.StartDate == input.StartDate ||
                    (this.StartDate != null &&
                    this.StartDate.Equals(input.StartDate))
                ) &&
                (
                    this.StateIdNumber == input.StateIdNumber ||
                    (this.StateIdNumber != null &&
                    this.StateIdNumber.Equals(input.StateIdNumber))
                ) &&
                (
                    this.Status == input.Status ||
                    (this.Status != null &&
                    this.Status.Equals(input.Status))
                ) &&
                (
                    this.Suffix == input.Suffix ||
                    (this.Suffix != null &&
                    this.Suffix.Equals(input.Suffix))
                ) &&
                (
                    this.Title == input.Title ||
                    (this.Title != null &&
                    this.Title.Equals(input.Title))
                ) &&
                (
                    this.TradeName == input.TradeName ||
                    (this.TradeName != null &&
                    this.TradeName.Equals(input.TradeName))
                ) &&
                (
                    this.Type == input.Type ||
                    (this.Type != null &&
                    this.Type.Equals(input.Type))
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
                if (this.Address != null)
                    hashCode = hashCode * 59 + this.Address.GetHashCode();
                if (this.BirthCity != null)
                    hashCode = hashCode * 59 + this.BirthCity.GetHashCode();
                if (this.BirthDate != null)
                    hashCode = hashCode * 59 + this.BirthDate.GetHashCode();
                if (this.BirthRegion != null)
                    hashCode = hashCode * 59 + this.BirthRegion.GetHashCode();
                if (this.BirthState != null)
                    hashCode = hashCode * 59 + this.BirthState.GetHashCode();
                if (this.BusinessName != null)
                    hashCode = hashCode * 59 + this.BusinessName.GetHashCode();
                if (this.Comment != null)
                    hashCode = hashCode * 59 + this.Comment.GetHashCode();
                if (this.DeceasedDate != null)
                    hashCode = hashCode * 59 + this.DeceasedDate.GetHashCode();
                if (this.DriverLicenseNumber != null)
                    hashCode = hashCode * 59 + this.DriverLicenseNumber.GetHashCode();
                if (this.DriverLicenseState != null)
                    hashCode = hashCode * 59 + this.DriverLicenseState.GetHashCode();
                if (this.Email != null)
                    hashCode = hashCode * 59 + this.Email.GetHashCode();
                if (this.EndDate != null)
                    hashCode = hashCode * 59 + this.EndDate.GetHashCode();
                if (this.Fax != null)
                    hashCode = hashCode * 59 + this.Fax.GetHashCode();
                if (this.FaxCountryCode != null)
                    hashCode = hashCode * 59 + this.FaxCountryCode.GetHashCode();
                if (this.FederalEmployerId != null)
                    hashCode = hashCode * 59 + this.FederalEmployerId.GetHashCode();
                if (this.FirstName != null)
                    hashCode = hashCode * 59 + this.FirstName.GetHashCode();
                if (this.FullName != null)
                    hashCode = hashCode * 59 + this.FullName.GetHashCode();
                if (this.Gender != null)
                    hashCode = hashCode * 59 + this.Gender.GetHashCode();
                if (this.Id != null)
                    hashCode = hashCode * 59 + this.Id.GetHashCode();
                if (this.IndividualOrOrganization != null)
                    hashCode = hashCode * 59 + this.IndividualOrOrganization.GetHashCode();
                if (this.IsPrimary != null)
                    hashCode = hashCode * 59 + this.IsPrimary.GetHashCode();
                if (this.LastName != null)
                    hashCode = hashCode * 59 + this.LastName.GetHashCode();
                if (this.MiddleName != null)
                    hashCode = hashCode * 59 + this.MiddleName.GetHashCode();
                if (this.OrganizationName != null)
                    hashCode = hashCode * 59 + this.OrganizationName.GetHashCode();
                if (this.PassportNumber != null)
                    hashCode = hashCode * 59 + this.PassportNumber.GetHashCode();
                if (this.Phone1 != null)
                    hashCode = hashCode * 59 + this.Phone1.GetHashCode();
                if (this.Phone1CountryCode != null)
                    hashCode = hashCode * 59 + this.Phone1CountryCode.GetHashCode();
                if (this.Phone2 != null)
                    hashCode = hashCode * 59 + this.Phone2.GetHashCode();
                if (this.Phone2CountryCode != null)
                    hashCode = hashCode * 59 + this.Phone2CountryCode.GetHashCode();
                if (this.Phone3 != null)
                    hashCode = hashCode * 59 + this.Phone3.GetHashCode();
                if (this.Phone3CountryCode != null)
                    hashCode = hashCode * 59 + this.Phone3CountryCode.GetHashCode();
                if (this.PostOfficeBox != null)
                    hashCode = hashCode * 59 + this.PostOfficeBox.GetHashCode();
                if (this.PreferredChannel != null)
                    hashCode = hashCode * 59 + this.PreferredChannel.GetHashCode();
                if (this.Race != null)
                    hashCode = hashCode * 59 + this.Race.GetHashCode();
                if (this.RecordId != null)
                    hashCode = hashCode * 59 + this.RecordId.GetHashCode();
                if (this.ReferenceContactId != null)
                    hashCode = hashCode * 59 + this.ReferenceContactId.GetHashCode();
                if (this.Relation != null)
                    hashCode = hashCode * 59 + this.Relation.GetHashCode();
                if (this.Salutation != null)
                    hashCode = hashCode * 59 + this.Salutation.GetHashCode();
                if (this.SocialSecurityNumber != null)
                    hashCode = hashCode * 59 + this.SocialSecurityNumber.GetHashCode();
                if (this.StartDate != null)
                    hashCode = hashCode * 59 + this.StartDate.GetHashCode();
                if (this.StateIdNumber != null)
                    hashCode = hashCode * 59 + this.StateIdNumber.GetHashCode();
                if (this.Status != null)
                    hashCode = hashCode * 59 + this.Status.GetHashCode();
                if (this.Suffix != null)
                    hashCode = hashCode * 59 + this.Suffix.GetHashCode();
                if (this.Title != null)
                    hashCode = hashCode * 59 + this.Title.GetHashCode();
                if (this.TradeName != null)
                    hashCode = hashCode * 59 + this.TradeName.GetHashCode();
                if (this.Type != null)
                    hashCode = hashCode * 59 + this.Type.GetHashCode();
                return hashCode;
            }
        }
        
    }

}
