using Newtonsoft.Json;
using System;
using System.Collections.Generic;


namespace Meck.Shared.Accela.ParserModels
{

    public class AccelaRecordModel
    {

        [JsonProperty("result")]
        public List<Result> result { get; set; }

        [JsonProperty("status")]
        public int status { get; set; }


        public AccelaRecordModel()
        {
            result = new List<Result>();
            // result[0].CustomFormDetails = new CustomForm();
        }

    }

    public class Status
    {

        [JsonProperty("Status_value")]
        public string value { get; set; }

        [JsonProperty("Status_text")]
        public string text { get; set; }
    }

    public class Parcel
    {
        [JsonProperty("landValue")]
        public decimal landValue { get; set; }

        [JsonProperty("exemptionValue")]
        public decimal exemptionValue { get; set; }

        [JsonProperty("parcel")]
        public string parcel { get; set; }

        [JsonProperty("parcelArea")]
        public decimal parcelArea { get; set; }

        [JsonProperty("gisSequenceNumber")]
        public int gisSequenceNumber { get; set; }

        [JsonProperty("improvedValue")]
        public decimal improvedValue { get; set; }

        [JsonProperty("legalDescription")]
        public string legalDescription { get; set; }

        [JsonProperty("book")]
        public string book { get; set; }

        [JsonProperty("parcelNumber")]
        public string parcelNumber { get; set; }

        [JsonProperty("page")]
        public string page { get; set; }

        [JsonProperty("Parcel_id")]
        public string id { get; set; }

        [JsonProperty("isPrimary")]
        public string isPrimary { get; set; }

        [JsonProperty("status")]
        public Status status { get; set; }
    }

    public class RecordId
    {
        [JsonProperty("RecordId_id")]
        public string id { get; set; }

        [JsonProperty("serviceProviderCode")]
        public string serviceProviderCode { get; set; }

        [JsonProperty("trackingId")]
        public Int32? trackingId { get; set; }

        [JsonProperty("RecordIdvalue")]
        public string value { get; set; }
    }

    public class State
    {

        [JsonProperty("state_value")]
        public string value { get; set; }

        [JsonProperty("state_text")]
        public string text { get; set; }
    }

    public class StreetSuffix
    {

        [JsonProperty("streetsuffixvalue")]
        public string value { get; set; }

        [JsonProperty("streetsuffixtext")]
        public string text { get; set; }
    }

    public class StreetPrefix
    {

        [JsonProperty("streetPrefixValue")]
        public string value { get; set; }

        [JsonProperty("streetPrefixText")]
        public string text { get; set; }
    }


    public class ParsedAddress
    {

        [JsonProperty("xCoordinate")]
        public decimal xCoordinate { get; set; }

        [JsonProperty("yCoordinate")]
        public decimal yCoordinate { get; set; }

        [JsonProperty("refAddressId")]
        public int refAddressId { get; set; }

        [JsonProperty("id")]
        public int id { get; set; }

        [JsonProperty("addressLine1")]
        public string addressLine1 { get; set; }

        [JsonProperty("streetStart")]
        public int streetStart { get; set; }

        [JsonProperty("houseAlphaStart")]
        public string houseAlphaStart { get; set; }

        [JsonProperty("streetAddress")]
        public string streetAddress { get; set; }

        [JsonProperty("serviceProviderCode")]
        public string serviceProviderCode { get; set; }

        [JsonProperty("isPrimary")]
        public string isPrimary { get; set; }

        [JsonProperty("postalCode")]
        public string postalCode { get; set; }

        [JsonProperty("city")]
        public string city { get; set; }

        [JsonProperty("streetName")]
        public string streetName { get; set; }

        [JsonProperty("recordId")]
        public RecordId recordId { get; set; }

        [JsonProperty("status")]
        public Status status { get; set; }

        [JsonProperty("address_state")]
        public State state { get; set; }

        [JsonProperty("streetPrefix")]
        public string streetPrefix { get; set; }
        
        [JsonProperty("streetSuffix")]
        public StreetSuffix streetSuffix { get; set; }


        [JsonProperty("country")]
        public Country country { get; set; }
    }

    public class TypeResult
    {

        [JsonProperty("TypeResultvalue")]
        public string value { get; set; }

        [JsonProperty("TypeResulttext")]
        public string text { get; set; }


    }

    public class Country
    {

        [JsonProperty("Countryvalue")]
        public string value { get; set; }

        [JsonProperty("Countrytext")]
        public string text { get; set; }
    }

    public class address
    {

        [JsonProperty("postalCode")]
        public string postalCode { get; set; }

        [JsonProperty("city")]
        public string city { get; set; }

        [JsonProperty("addressLine1")]
        public string addressLine1 { get; set; }

        [JsonProperty("country")]
        public Country country { get; set; }

        [JsonProperty("state")]
        public State state { get; set; }
    }

    public class Contact
    {

        [JsonProperty("fullName")]
        public string fullName { get; set; }
        
        [JsonProperty("firstName")]
        public string firstName { get; set; }

        [JsonProperty("middleName")]
        public string middleName { get; set; }

        [JsonProperty("lastName")]
        public string lastName { get; set; }

        [JsonProperty("email")]
        public string email { get; set; }

        [JsonProperty("isPrimary")]
        public string isPrimary { get; set; }

        [JsonProperty("individualOrOrganization")]
        public string individualOrOrganization { get; set; }

        [JsonProperty("phone3")]
        public string phone3 { get; set; }

        [JsonProperty("startDate")]
        public string startDate { get; set; }

        [JsonProperty("phone2")]
        public string phone2 { get; set; }

        [JsonProperty("phone1")]
        public string phone1 { get; set; }

        [JsonProperty("referenceContactId")]
        public string referenceContactId { get; set; }

        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("ContactrecordId")]
        public RecordId recordId { get; set; }

        [JsonProperty("Contactstatus")]
        public Status status { get; set; }

        [JsonProperty("Contacttype")]
        public TypeResult typeContact { get; set; }

        [JsonProperty("address")]
        public ParsedAddress address { get; set; }

        [JsonProperty("organizationName")]
        public string organizationName { get; set; }

        [JsonProperty("fax")]
        public string fax { get; set; }

    }


    public class MailAddress
    {

        [JsonProperty("postalCode")]
        public string postalCode { get; set; }

        [JsonProperty("city")]
        public string city { get; set; }

        [JsonProperty("addressLine1")]
        public string addressLine1 { get; set; }

        [JsonProperty("MailAddressstate")]
        public State state { get; set; }

        [JsonProperty("MailAddresscountry")]
        public Country country { get; set; }
    }

    public class Owner
    {

        [JsonProperty("refOwnerId")]
        public double refOwnerId { get; set; }

        [JsonProperty("Ownerid")]
        public int id { get; set; }

        [JsonProperty("fullName")]
        public string fullName { get; set; }

        [JsonProperty("isPrimary")]
        public string isPrimary { get; set; }

        [JsonProperty("mailAddress")]
        public MailAddress mailAddress { get; set; }

        [JsonProperty("Ownerstatus")]
        public Status status { get; set; }
    }

    public class ReportedChannel
    {
        [JsonProperty("ReportedChannelvalue")]
        public string value { get; set; }

        [JsonProperty("ReportedChanneltext")]
        public string text { get; set; }
    }

    public class Result
    {
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("HasError")]
        public bool hasErrors { get; set; }

        [JsonProperty("parsingErrors")]
        public string parsingError { get; set; }

        [JsonProperty("ParseRecType")]
        public string ParseRecType { get; set; }

        [JsonProperty("TaskActivator")]
        public string  TaskActivator { get; set; }

        [JsonProperty("AltId")]
        public string altId { get; set; }

        [JsonProperty("CommonFields")]
        public Dictionary<string, object> CommonFields { get; set; }

        [JsonProperty("Resulttype")]
        public Dictionary<string, object> Resulttype { get; set; }
        
        [JsonProperty("Resultstatus")]
        public Status Resultstatus { get; set; }

       [JsonProperty("parcels")]
        public List<Dictionary<string,object>> Parcels { get; set; }
        [JsonProperty("professionals")]
        public List<Dictionary<string,object>> professionals { get; set; }

        [JsonProperty("addresses")]
        public List<Dictionary<string,object>> Addresses { get; set; }

        [JsonProperty("conditions")]
        public List<Dictionary<string, object>> conditions { get; set; }

        [JsonProperty("contacts")]
        public List<Dictionary<string,object>> Contacts { get; set; }

        [JsonProperty("customForms")]
        public List<Dictionary<string, object>>  CustomForms { get; set; }

        [JsonProperty("customTables")]
        public List<Dictionary<string, object>> CustomTables { get; set; }

        [JsonProperty("owners")]
        public List<Dictionary<string,object>> Owners { get; set; }
        [JsonProperty("reportedChannel")]
        public ReportedChannel reportedChannel { get; set; }
    }


    public class CustomForm
    {
        public string formset { get; set; }

        public Object objectValues { get; set; }

        public CustomForm()
        {
            formset = null;
            objectValues = new object();
        }
    }

    public class CustomTable
    {
        public string formset { get; set; }

        public Object objectValues { get; set; }

        public string ParsingError { get; set; }

        public bool HasErrors { get; set; }

        public CustomTable()
        {
            formset = null;
            objectValues = new object();
        }
    }

    public class Professional
    {
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("fullname")]
        public string fullName { get; set; }
        [JsonProperty("businessName")]
        public string businessName { get; set; }
        [JsonProperty("UpdateOnUI")]
        public bool updateOnUI { get; set; }
        [JsonProperty("licenseNumber")]
        public string licenseNumber { get; set; }
        [JsonProperty("lastName")]
        public string lastName { get; set; }
        [JsonProperty("middleName")]
        public string middleName { get; set; }
        [JsonProperty("firstName")]
        public string firstName { get; set; }
        [JsonProperty("isPrimary")]
        public string isPrimary { get; set; }
        [JsonProperty("referenceLicenseId")]
        public string referenceLicenseId { get; set; }
        [JsonProperty("recordId")]
        public RecordId recordId { get; set; }
        [JsonProperty("LicenseType")]
        public Licensetype licenseType { get; set; }
        [JsonProperty("state")]
        public State state { get; set; }
    }
    public class Licensetype
    {
        public string value { get; set; }
        public string text { get; set; }
    }


}


