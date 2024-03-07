using System.Collections.Generic;

namespace Meck.Shared.PosseToAccela
{
    public class PosseAccelaRecords
    {
        public List<MappedObj> thisMapping { get; set; }

        public string MappingError { get; set; }

        public PosseAccelaRecord thisRecordMap { get; set; }


        public PosseAccelaRecords()
        {
            thisMapping = new List<MappedObj>();

            thisRecordMap = new PosseAccelaRecord();
        }

    }

    public class MappedObj
    {
        private object target { get; set; }
        private object value { get; set; }

        public MappedObj(object mTarget, object mValue)
        {
            target = mTarget;
            value = mValue;
        }
    }

    public class PosseAccelaRecord
    {
        [Order(1)]
        public string RecordId { get; set; }
        [Order(2)]
        public string PlanReviewType { get; set; }
        [Order(3)]
        public string PlanReviewCategory { get; set; }
        [Order(4)]
        public string ProjectNumber { get; set; }
        [Order(5)]
        public string ProjectName { get; set; }
        [Order(6)]
        public string AccelaProjectStatus { get; set; }
        [Order(7)]
        public string AddressID { get; set; }
        [Order(8)]
        public string CoordinatorID { get; set; }
        [Order(9)]
        public string NumStoriesOverallBldg { get; set; }
        [Order(10)]
        public bool ProfessionalCertification { get; set; }
        [Order(11)]
        public bool IsHighRiseResidential { get; set; }
        [Order(12)]
        public bool SIRequired { get; set; }
        [Order(13)]
        public bool PublicSecurityBldg { get; set; }
        [Order(14)]
        public string CodeReviewYear { get; set; }
        [Order(15)]
        public string OriginalProjectNumber { get; set; }
        [Order(16)]
        public string OriginalPermitNumber { get; set; }
        [Order(17)]
        public string ClonedFromProjectNumber { get; set; }
        [Order(18)]
        public string HeatedSquareFeet { get; set; }
        [Order(19)]
        public string UnheatedSquareFeet { get; set; }
        [Order(20)]
        public string DeckArea { get; set; }
        [Order(21)]
        public string CnvrtUnhtdToHtdSqFt { get; set; }
        [Order(22)]
        public string RenovatedArea { get; set; }
        [Order(23)]
        public string EquipmentCost { get; set; }
        [Order(24)]
        public string ProjectCost { get; set; }
        [Order(25)]
        public string TypeOfWork { get; set; }
        [Order(26)]
        public string PrepaidFeePaymentType { get; set; }
        [Order(27)]
        public string ContractorID { get; set; }
        [Order(28)]
        public string Occupancy { get; set; }
        [Order(29)]
        public string ConstructionType { get; set; }

        //public string StructureActivity { get; set; }
        //public string StructureType { get; set; }
        [Order(30)]
        public string UseClass { get; set; }
        [Order(31)]
        public bool IsHomeowner { get; set; }
        [Order(32)]
        public string ConditionID { get; set; }
        [Order(33)]
        public string BuildingPermitsRequired { get; set; }
        [Order(34)]
        public string ElectricalPermitsRequired { get; set; }
        [Order(35)]
        public string MechanicalPermitsRequired { get; set; }
        [Order(36)]
        public string PlumbingPermitsRequired { get; set; }
        [Order(37)]
        public string ZoningCode { get; set; }
        [Order(38)]
        public decimal ZoningFrontSetback { get; set; }
        [Order(39)]
        public decimal ZoningRearSetback { get; set; }
        [Order(40)]
        public decimal ZoningLeftSetback { get; set; }
        [Order(41)]
        public decimal ZoningRightSetback { get; set; }
        [Order(42)]
        public List<contactDetail> Contacts { get; set; }
        [Order(43)]
        public List<PlanReviewFee> PlanReviewFees { get; set; }
        [Order(44)]
        public List<RTAPProjectNumber> RTAPProjectNumbers { get; set; }
        [Order(45)]
        public List<AANHold> AANHolds { get; set; }


        public PosseAccelaRecord()
        {
            Contacts = new List<contactDetail>();
            PlanReviewFees = new List<PlanReviewFee>();
            RTAPProjectNumbers = new List<RTAPProjectNumber>();
            AANHolds = new List<AANHold>();
        }
    }

    public class contactDetail
    {
         public long id { get; set; }
        public string ContactType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string BusinessName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneNumberExt { get; set; }
        public string HomePhone { get; set; }
        public string MobilePhone { get; set; }
        public string AddressCustomer { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCodeFirstPart { get; set; }
        public string HomeownerUserId { get; set; }
       public string Notify { get; set; }
       public string RequestorAssociation { get; set; }
       public string RequestorAssociationOther { get; set; }
       public string Grade { get; set; }

        public contactDetail()
        {

        }
    }

    public class professionalDetail
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string LicenseNumber { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string BusinessName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string BusinessLicense { get; set; }
        public string FirstName { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string LicenseType { get; set; }
        public string ReferenceLicenseId { get; set; }
        public string State { get; set; }
        public string IsArchDrawingsSealed { get; set; }

        public professionalDetail()
        {

        }
    }

    public class PlanReviewFee
    {
        public string ProjectNumber { get; set; }
        public string FeeName { get; set; }
        public decimal TotalFee { get; set; }
        public string Remarks { get; set; }

        public PlanReviewFee()
        {

        }
    }

    public class RTAPProjectNumber
    {
        public string RTapNumber { get; set; }

        public RTAPProjectNumber()
        {

        }
    }

    public class AANHold
    {
        public string AccessGroup { get; set; }
        public string HoldNote { get; set; }
    }


}




