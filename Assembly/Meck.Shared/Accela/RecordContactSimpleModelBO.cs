using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Meck.Shared.Accela
{

    public partial class RecordContactSimpleModelBO
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
            [EnumMember(Value = "Y")] Y = 1,

            /// <summary>
            /// Enum N for value: N
            /// </summary>
            [EnumMember(Value = "N")] N = 2
        }

        /// <summary>
        /// Indicates whether or not to designate the contact as the primary contact Only one address can be primary at any given time.
        /// </summary>
        /// <value>Indicates whether or not to designate the contact as the primary contact Only one address can be primary at any given time.</value>
        [DataMember(Name = "isPrimary", EmitDefaultValue = false)]
        public IsPrimaryEnum? IsPrimary { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="RecordContactSimpleModel" /> class.
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
        public RecordContactSimpleModelBO(CompactAddressModelBO address = default(CompactAddressModelBO),
            RecordContactSimpleModelBirthCityBO birthCity = default(RecordContactSimpleModelBirthCityBO),
            DateTime? birthDate = default(DateTime?),
            RecordContactSimpleModelBirthRegionBO birthRegion = default(RecordContactSimpleModelBirthRegionBO),
            RecordContactSimpleModelBirthStateBO birthState = default(RecordContactSimpleModelBirthStateBO),
            string businessName = default(string), string comment = default(string),
            DateTime? deceasedDate = default(DateTime?), string driverLicenseNumber = default(string),
            RecordContactSimpleModelDriverLicenseStateBO driverLicenseState =
                default(RecordContactSimpleModelDriverLicenseStateBO), string email = default(string),
            DateTime? endDate = default(DateTime?), string fax = default(string),
            string faxCountryCode = default(string), string federalEmployerId = default(string),
            string firstName = default(string), string fullName = default(string),
            RecordContactSimpleModelGenderBO gender = default(RecordContactSimpleModelGenderBO),
            string id = default(string), string individualOrOrganization = default(string),
            IsPrimaryEnum? isPrimary = default(IsPrimaryEnum?), string lastName = default(string),
            string middleName = default(string), string organizationName = default(string),
            string passportNumber = default(string), string phone1 = default(string),
            string phone1CountryCode = default(string), string phone2 = default(string),
            string phone2CountryCode = default(string), string phone3 = default(string),
            string phone3CountryCode = default(string), string postOfficeBox = default(string),
            RecordContactSimpleModelPreferredChannelBO preferredChannel = default(RecordContactSimpleModelPreferredChannelBO),
            RecordContactSimpleModelRaceBO race = default(RecordContactSimpleModelRaceBO),
            RecordIdModelBO recordId = default(RecordIdModelBO), 
            string referenceContactId = default(string),
            RecordContactSimpleModelRelationBO relation = default(RecordContactSimpleModelRelationBO),
            RecordContactSimpleModelSalutationBO salutation = default(RecordContactSimpleModelSalutationBO),
            string socialSecurityNumber = default(string),
            DateTime? startDate = default(DateTime?),
            string stateIdNumber = default(string),
            RecordContactSimpleModelStatusBO status = default(RecordContactSimpleModelStatusBO),
            string suffix = default(string), 
            string title = default(string), 
            string tradeName = default(string),
            RecordContactSimpleModelTypeBO type = default(RecordContactSimpleModelTypeBO))
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

        public CompactAddressModelBO Address { get; set; }

        /// <summary>
        /// Gets or Sets BirthCity
        /// </summary>

        public RecordContactSimpleModelBirthCityBO BirthCity { get; set; }

        /// <summary>
        /// The birth date.
        /// </summary>
        /// <value>The birth date.</value>

        public DateTime? BirthDate { get; set; }

        /// <summary>
        /// Gets or Sets BirthRegion
        /// </summary>

        public RecordContactSimpleModelBirthRegionBO BirthRegion { get; set; }

        /// <summary>
        /// Gets or Sets BirthState
        /// </summary>
        public RecordContactSimpleModelBirthStateBO BirthState { get; set; }

        /// <summary>
        /// A secondary business name for the applicable individual.
        /// </summary>
        /// <value>A secondary business name for the applicable individual.</value>

        public string BusinessName { get; set; }

        /// <summary>
        /// A comment about the inspection contact.
        /// </summary>
        /// <value>A comment about the inspection contact.</value>

        public string Comment { get; set; }

        /// <summary>
        /// The deceased date.
        /// </summary>
        /// <value>The deceased date.</value>

        public DateTime? DeceasedDate { get; set; }

        /// <summary>
        /// The driver&#39;s license number of the contact. This field is active only when the Contact Type selected is Individual.
        /// </summary>
        /// <value>The driver&#39;s license number of the contact. This field is active only when the Contact Type selected is Individual.</value>

        public string DriverLicenseNumber { get; set; }

        /// <summary>
        /// Gets or Sets DriverLicenseState
        /// </summary>

        public RecordContactSimpleModelDriverLicenseStateBO DriverLicenseState { get; set; }

        /// <summary>
        /// The contact&#39;s email address.
        /// </summary>
        /// <value>The contact&#39;s email address.</value>

        public string Email { get; set; }

        /// <summary>
        /// The date when the contact address ceases to be active.
        /// </summary>
        /// <value>The date when the contact address ceases to be active.</value>

        public DateTime? EndDate { get; set; }

        /// <summary>
        /// The fax number for the contact.
        /// </summary>
        /// <value>The fax number for the contact.</value>

        public string Fax { get; set; }

        /// <summary>
        /// Fax Number Country Code
        /// </summary>
        /// <value>Fax Number Country Code</value>

        public string FaxCountryCode { get; set; }

        /// <summary>
        /// The Federal Employer Identification Number. It is used to identify a business for tax purposes.
        /// </summary>
        /// <value>The Federal Employer Identification Number. It is used to identify a business for tax purposes.</value>

        public string FederalEmployerId { get; set; }

        /// <summary>
        /// The contact&#39;s first name.
        /// </summary>
        /// <value>The contact&#39;s first name.</value>

        public string FirstName { get; set; }

        /// <summary>
        /// The contact&#39;s full name. 
        /// </summary>
        /// <value>The contact&#39;s full name. </value>

        public string FullName { get; set; }

        /// <summary>
        /// Gets or Sets Gender
        /// </summary>

        public RecordContactSimpleModelGenderBO Gender { get; set; }

        /// <summary>
        /// The contact system id assigned by the Civic Platform server.
        /// </summary>
        /// <value>The contact system id assigned by the Civic Platform server.</value>

        public string Id { get; set; }

        /// <summary>
        /// The organization to which the contact belongs. This field is only active when the Contact Type selected is Organization.
        /// </summary>
        /// <value>The organization to which the contact belongs. This field is only active when the Contact Type selected is Organization.</value>

        public string IndividualOrOrganization { get; set; }


        /// <summary>
        /// The last name (surname).
        /// </summary>
        /// <value>The last name (surname).</value>

        public string LastName { get; set; }

        /// <summary>
        /// The middle name. 
        /// </summary>
        /// <value>The middle name. </value>

        public string MiddleName { get; set; }

        /// <summary>
        /// The organization to which the contact belongs. This field is only active when the Contact Type selected is Organization.
        /// </summary>
        /// <value>The organization to which the contact belongs. This field is only active when the Contact Type selected is Organization.</value>

        public string OrganizationName { get; set; }

        /// <summary>
        /// The contact&#39;s passport number. This field is only active when the Contact Type selected is Individual.
        /// </summary>
        /// <value>The contact&#39;s passport number. This field is only active when the Contact Type selected is Individual.</value>

        public string PassportNumber { get; set; }

        /// <summary>
        /// The primary telephone number of the contact.
        /// </summary>
        /// <value>The primary telephone number of the contact.</value>
        public string Phone1 { get; set; }

        /// <summary>
        /// Phone Number 1 Country Code
        /// </summary>
        /// <value>Phone Number 1 Country Code</value>
        public string Phone1CountryCode { get; set; }

        /// <summary>
        /// The secondary telephone number of the contact.
        /// </summary>
        /// <value>The secondary telephone number of the contact.</value>
        public string Phone2 { get; set; }

        /// <summary>
        /// Phone Number 2 Country Code
        /// </summary>
        /// <value>Phone Number 2 Country Code</value>
        public string Phone2CountryCode { get; set; }

        /// <summary>
        /// The tertiary telephone number for the contact.
        /// </summary>
        /// <value>The tertiary telephone number for the contact.</value>

        public string Phone3 { get; set; }

        /// <summary>
        /// Phone Number 3 Country Code
        /// </summary>
        /// <value>Phone Number 3 Country Code</value>

        public string Phone3CountryCode { get; set; }

        /// <summary>
        /// The post office box number.
        /// </summary>
        /// <value>The post office box number.</value>

        public string PostOfficeBox { get; set; }

        /// <summary>
        /// Gets or Sets PreferredChannel
        /// </summary>

        public RecordContactSimpleModelPreferredChannelBO PreferredChannel { get; set; }

        /// <summary>
        /// Gets or Sets Race
        /// </summary>

        public RecordContactSimpleModelRaceBO Race { get; set; }

        /// <summary>
        /// Gets or Sets RecordId
        /// </summary>

        public RecordIdModelBO RecordId { get; set; }

        /// <summary>
        /// The unique Id generated for a contact stored in the sytem.
        /// </summary>
        /// <value>The unique Id generated for a contact stored in the sytem.</value>

        public string ReferenceContactId { get; set; }

        /// <summary>
        /// Gets or Sets Relation
        /// </summary>

        public RecordContactSimpleModelRelationBO Relation { get; set; }

        /// <summary>
        /// Gets or Sets Salutation
        /// </summary>

        public RecordContactSimpleModelSalutationBO Salutation { get; set; }

        /// <summary>
        /// The individual&#39;s social security number. This field is only active when the Contact Type selected is Individual.
        /// </summary>
        /// <value>The individual&#39;s social security number. This field is only active when the Contact Type selected is Individual.</value>

        public string SocialSecurityNumber { get; set; }

        /// <summary>
        /// The date the contact became active.
        /// </summary>
        /// <value>The date the contact became active.</value>

        public DateTime? StartDate { get; set; }

        /// <summary>
        /// The contact&#39;s state ID number. This field is only active when the Contact Type selected is Individual.
        /// </summary>
        /// <value>The contact&#39;s state ID number. This field is only active when the Contact Type selected is Individual.</value>

        public string StateIdNumber { get; set; }

        /// <summary>
        /// Gets or Sets Status
        /// </summary>
        public RecordContactSimpleModelStatusBO Status { get; set; }

        /// <summary>
        /// The contact name suffix.
        /// </summary>
        /// <value>The contact name suffix.</value>
        public string Suffix { get; set; }

        /// <summary>
        /// The individual&#39;s business title.
        /// </summary>
        /// <value>The individual&#39;s business title.</value>
        public string Title { get; set; }

        /// <summary>
        /// The contact&#39;s preferred business or trade name. This field is active only when the Contact Type selected is Organization.
        /// </summary>
        /// <value>The contact&#39;s preferred business or trade name. This field is active only when the Contact Type selected is Organization.</value>

        public string TradeName { get; set; }

        /// <summary>
        /// Gets or Sets Type
        /// </summary>
        public RecordContactSimpleModelTypeBO Type { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class RecordContactSimpleModel {\n");
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

    }

    public partial class RecordContactSimpleModelBirthCityBO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordContactSimpleModelBirthCity" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public RecordContactSimpleModelBirthCityBO(string text = default(string), string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// The localized display value.
        /// </summary>
        /// <value>The localized display value.</value>
        public string Text { get; set; }

        /// <summary>
        /// The data value.
        /// </summary>
        /// <value>The data value.</value>
        public string Value { get; set; }
    }

    public partial class RecordContactSimpleModelBirthRegionBO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordContactSimpleModelBirthRegion" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public RecordContactSimpleModelBirthRegionBO(string text = default(string), string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// The localized display value.
        /// </summary>
        /// <value>The localized display value.</value>
        public string Text { get; set; }

        /// <summary>
        /// The data value.
        /// </summary>
        /// <value>The data value.</value>
        public string Value { get; set; }
    }

    public partial class RecordContactSimpleModelBirthStateBO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordContactSimpleModelBirthState" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public RecordContactSimpleModelBirthStateBO(string text = default(string), string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// The localized display value.
        /// </summary>
        /// <value>The localized display value.</value>
        public string Text { get; set; }

        /// <summary>
        /// The data value.
        /// </summary>
        /// <value>The data value.</value>
        public string Value { get; set; }
    }

    public partial class RecordContactSimpleModelDriverLicenseStateBO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordContactSimpleModelDriverLicenseState" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public RecordContactSimpleModelDriverLicenseStateBO(string text = default(string),
            string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// The localized display value.
        /// </summary>
        /// <value>The localized display value.</value>
        public string Text { get; set; }

        /// <summary>
        /// The data value.
        /// </summary>
        /// <value>The data value.</value>
        public string Value { get; set; }
    }

    public partial class RecordContactSimpleModelGenderBO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordContactSimpleModelGender" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public RecordContactSimpleModelGenderBO(string text = default(string), string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// The localized display value.
        /// </summary>
        /// <value>The localized display value.</value>
        public string Text { get; set; }

        /// <summary>
        /// The data value.
        /// </summary>
        /// <value>The data value.</value>
        public string Value { get; set; }
    }

    public partial class RecordContactSimpleModelPreferredChannelBO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordContactSimpleModelPreferredChannel" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public RecordContactSimpleModelPreferredChannelBO(string text = default(string), string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// The localized display value.
        /// </summary>
        /// <value>The localized display value.</value>
        public string Text { get; set; }

        /// <summary>
        /// The data value.
        /// </summary>
        /// <value>The data value.</value>
        public string Value { get; set; }
    }

    public partial class RecordContactSimpleModelRaceBO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordContactSimpleModelRace" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public RecordContactSimpleModelRaceBO(string text = default(string), string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// The localized display value.
        /// </summary>
        /// <value>The localized display value.</value>
        public string Text { get; set; }

        /// <summary>
        /// The data value.
        /// </summary>
        /// <value>The data value.</value>
        public string Value { get; set; }
    }

    public partial class RecordContactSimpleModelRelationBO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordContactSimpleModelRelation" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public RecordContactSimpleModelRelationBO(string text = default(string), string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// The localized display value.
        /// </summary>
        /// <value>The localized display value.</value>
        public string Text { get; set; }

        /// <summary>
        /// The data value.
        /// </summary>
        /// <value>The data value.</value>
        public string Value { get; set; }
    }

    public partial class RecordIdModelBO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordIdModel" /> class.
        /// </summary>
        /// <param name="customId">An ID based on a different numbering convention from the numbering convention used by the record ID (xxxxx-xx-xxxxx). Accela Automation auto-generates and applies an alternate ID value when you submit a new application..</param>
        /// <param name="id">The record system id assigned by the Civic Platform server..</param>
        /// <param name="serviceProviderCode">The unique agency identifier..</param>
        /// <param name="trackingId">The application tracking number (IVR tracking number)..</param>
        /// <param name="value">The alphanumeric record id..</param>
        public RecordIdModelBO(string customId = default(string), string id = default(string),
            string serviceProviderCode = default(string), long? trackingId = default(long?),
            string value = default(string))
        {
            this.CustomId = customId;
            this.Id = id;
            this.ServiceProviderCode = serviceProviderCode;
            this.TrackingId = trackingId;
            this.Value = value;
        }

        /// <summary>
        /// An ID based on a different numbering convention from the numbering convention used by the record ID (xxxxx-xx-xxxxx). Accela Automation auto-generates and applies an alternate ID value when you submit a new application.
        /// </summary>
        /// <value>An ID based on a different numbering convention from the numbering convention used by the record ID (xxxxx-xx-xxxxx). Accela Automation auto-generates and applies an alternate ID value when you submit a new application.</value>
        public string CustomId { get; set; }

        /// <summary>
        /// The record system id assigned by the Civic Platform server.
        /// </summary>
        /// <value>The record system id assigned by the Civic Platform server.</value>
        public string Id { get; set; }

        /// <summary>
        /// The unique agency identifier.
        /// </summary>
        /// <value>The unique agency identifier.</value>
        public string ServiceProviderCode { get; set; }

        /// <summary>
        /// The application tracking number (IVR tracking number).
        /// </summary>
        /// <value>The application tracking number (IVR tracking number).</value>
        public long? TrackingId { get; set; }

        /// <summary>
        /// The alphanumeric record id.
        /// </summary>
        /// <value>The alphanumeric record id.</value>

        public string Value { get; set; }
    }

    public partial class RecordContactSimpleModelSalutationBO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordContactSimpleModelSalutation" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public RecordContactSimpleModelSalutationBO(string text = default(string), string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// The localized display value.
        /// </summary>
        /// <value>The localized display value.</value>
        public string Text { get; set; }

        /// <summary>
        /// The data value.
        /// </summary>
        /// <value>The data value.</value>
        public string Value { get; set; }
    }
    public partial class RecordContactSimpleModelStatusBO
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecordContactSimpleModelStatus" /> class.
        /// </summary>
        /// <param name="text">The localized display value..</param>
        /// <param name="value">The data value..</param>
        public RecordContactSimpleModelStatusBO(string text = default(string), string value = default(string))
        {
            this.Text = text;
            this.Value = value;
        }

        /// <summary>
        /// The localized display value.
        /// </summary>
        /// <value>The localized display value.</value>
        public string Text { get; set; }

        /// <summary>
        /// The data value.
        /// </summary>
        /// <value>The data value.</value>
        public string Value { get; set; }
    }

}