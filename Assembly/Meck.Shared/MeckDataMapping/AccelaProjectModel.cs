using Meck.Shared.Accela;
using Meck.Shared.PosseToAccela;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Meck.Shared.MeckDataMapping
{

    #region Accela Project model    
    /// <summary>
    /// Project details from Accela
    /// </summary>
    public class AccelaProjectModel : ExternalBase
    {
        /* The details as per Accela. This could be used as mapping with AION Project Status.*/

        /// <summary>
        /// This is the error object for data from the record parsing.
        /// </summary>
        public string MappingError { get; set; }
        /// <summary>
        /// Project ID from Accela
        /// </summary>
        public string ProjectIDRef { get; set; }

        public string ProjectNumber { get; set; }
        public string RecordId { get; set; }
        public string RecordType { get; set; }
        public string AccelaRTAPProjectRefId { get; set; }
        public string AccelaPreliminaryProjectRefId { get; set; }
        public AgencyInfo Agencyinfo { get; set; }
        public List<contactDetail> Contacts { get; set; }
        public List<professionalDetail> Professionals { get; set; }
        /// <summary>
        /// Project Status from Accela
        /// </summary>
        public string ProjectStatusCodeRef { get; set; }
        /// <summary>
        /// Preliminary Meeting RequestedbyCustomer
        /// </summary>
        public bool IsPreliminaryMeetingRequested { get; set; }
        /// <summary>
        /// Preliminary Meeting completed from Accela
        /// </summary>
        public bool IsPreliminaryMeetingCompleted { get; set; }

        public bool IsPreliminaryMeetingCancelled { get; set; }

        /// <summary>
        /// Project is RTAP from Accela
        /// </summary>
        public bool IsProjectRTAP { get; set; }

        /// <summary>
        /// Project Gate Accepted from Accela
        /// </summary>
        public bool IsGateAccepted { get; set; }

        public bool IsFifo { get; set; }
        public int CycleNumber { get; set; }

        public DateTime? FifoDueDt { get; set; }

        public DateTime? FifoDueAccelaDt { get; set; }

        /// <summary>
        /// Building Contractor Name from Accela - for FIFO
        /// </summary>
        public string BuildingContractorName { get; set; }

        /// <summary>
        ///Building Contractor Account Number from Accela - for FIFO
        /// </summary>
        public string BuildingContractorAcctNo { get; set; }
        /// <summary>
        /// Project Manager Name from Accela
        /// </summary>
        public string ProjectManagerName { get; set; }
        /// <summary>
        /// Project Manager Phone from Accela
        /// </summary>
        public string projectManagerPhone { get; set; }
        /// <summary>
        /// Project Manager Email from Accela
        /// </summary>
        public string projectManagerEmail { get; set; }
        /// <summary>
        /// Project region from Accela. Use constants from "DepartmentRegionExternalRef"
        /// </summary>
        public string ProjectRegionExternalRef { get; set; }
        /// <summary>
        /// Review Type (Express, On Schedule) from Accela
        /// </summary>
        public string ReviewTypeRef { get; set; }
        /// <summary>
        /// Project is MMF, Townhomes, FIFO, etc from Accela
        /// </summary>
        public string PropertyTypeRef { get; set; }
        /// <summary>
        /// Agencies for the project (Fire, Zoning, etc) from Accela
        /// </summary>
        public List<AgencyInfo> ProjectAgencyList { get; set; }
        /// <summary>
        /// Trades for the project(building, electrical, etc) from Accela
        /// </summary>
        public List<TradeInfo> ProjectTradesList { get; set; }
        /// <summary>
        /// Notes from the application from Accela
        /// </summary>
        public List<string> ApplicationNotes { get; set; }
        /// <summary>
        /// UI information from Accela
        /// </summary>
        public AccelaProjectDisplayInfo DisplayOnlyInformation { get; set; }
        public string ConstructionType { get; set; }
        public List<RequestedExpressDateBE> RequestedExpressDates { get; set; }
        public bool IsExpress { get; set; }
        public string OccupancyType { get; set; }
        public string SquareFootageToBeReviewed { get; set; }
        public string SquareFootageOfOverallBuilding { get; set; }
        public string NumberOfStories { get; set; }
        public bool IsHighRise { get; set; }
        public string CostOfConstruction { get; set; }
        public string NumberofSheets { get; set; }
        public string TotalJobCost { get; set; }
        public string ProjectName { get; set; }
        public DateTime? PlansReadyOnDate { get; set; }
        public string GateDate { get; set; }
        public List<string> FloorList { get; set; }
        public List<string> PreliminaryDepts { get; set; }
        public List<TaskActivation> TaskActivations { get; set; }
        public List<AccelaMeeting> Meetings { get; set; }
        public List<GateResponse> GateResponses { get; set; }

        public PrelimProjectSummaryObj PrelimProjectSummary { get; set; }
        public PrelimGeneralInfoObj PrelimGeneralInfo { get; set; }
        public PrelimMeetingAgendaObj PrelimMeetingAgenda { get; set; }
        public PrelimProposedWorkObj PrelimProposedWork { get; set; }
        public PrelimSystemInfoObj PrelimSystemInfo { get; set; }
        public PrelimBIMProjectDeliveryObj PrelimBIMProjectDelivery { get; set; }
        public PrelimTypeOfWorkObj PrelimTypeOfWork { get; set; }
        public PrelimMeetingDetailObj PrelimMeetingDetail { get; set; }
        public PrelimMeetingTradesAndReviewerObj PrelimMeetingTradesAndReviewer { get; set; }

        public bool IsPaidStatus { get; set; }

        //Meeting Request record type information - only used on Meeting Request record
        public string MeetingType { get; set; }
        public int NumberOfAttendees { get; set; }
        public DateTime RequestedMeetingDate { get; set; }
        public string EstimatedFee { get; set; }

        //RTAP original project type id
        public int AIONOriginalProjectTypeId { get; set; }


        /// <summary>
        /// A&E Grading Grade
        /// </summary>
        public string TeamGrade { get; set; }
        /// <summary>
        /// A&E Grading Score
        /// </summary>
        public string TeamScore { get; set; }

        public AccelaProjectModel()
        {
            ApplicationNotes = new List<string>();
            Contacts = new List<contactDetail>();
            Professionals = new List<professionalDetail>();
            FloorList = new List<string>();
            TaskActivations = new List<TaskActivation>();
            Meetings = new List<AccelaMeeting>();
            GateResponses = new List<GateResponse>();
            PrelimBIMProjectDelivery = new PrelimBIMProjectDeliveryObj();
            PrelimGeneralInfo = new PrelimGeneralInfoObj();
            PrelimMeetingAgenda = new PrelimMeetingAgendaObj();
            PrelimMeetingDetail = new PrelimMeetingDetailObj();
            PrelimMeetingTradesAndReviewer = new PrelimMeetingTradesAndReviewerObj();
            PrelimProjectSummary = new PrelimProjectSummaryObj();
            PrelimProposedWork = new PrelimProposedWorkObj();
            PrelimSystemInfo = new PrelimSystemInfoObj();
            PrelimTypeOfWork = new PrelimTypeOfWorkObj();

        }
    }
    #endregion

    /// <summary>
    /// Agency (Fire, Zoning, etc) from Accela
    /// </summary>
    ///  This is mapped to AgencyInfo in the Accela Engine via the UserWrapper loading.
    ///  AgencyInfo mAgencyInfo = new AgencyInfo();
    ///   mAgencyInfo.AccelaDepartmentDivisionRef = tresult.Agency;
    ///   mAgencyInfo.AccelaDepartmentRegionRef = tresult.Text;
    ///
    public class AgencyInfo : DepartmentInfo
    {

    }

    /// <summary>
    /// Trade (Building, electrical, etc) from Accela
    /// </summary>
    public class TradeInfo : DepartmentInfo
    {
        /// <summary>
        /// Project Hours from Accela
        /// </summary>
        public long EstimationHours { get; set; }
        public int NumberOfSheets { get; set; }

    }

    public class TaskActivation
    {
        public DateTime StartDate { get; set; }
        public string Assignee { get; set; }
        public string TaskType { get; set; }
        public DateTime DateTimeStamp { get; set; }
        public string Comments { get; set; }
        public string TaskName { get; set; }
        public int Cycle { get; set; }
        public string ProcessingStatus { get; set; }
        public bool PoolReview { get; set; }
        public double EstimatedReviewTime { get; set; }
        public DateTime DueDate { get; set; }
        public int id { get; set; }
    }


    public class AccelaMeeting
    {
        public string Status { get; set; }
        public string Requester { get; set; }
        public string MeetingType { get; set; }
        public string AttendeesList { get; set; }
        public string MeetingDate { get; set; }
        public int Cycle { get; set; }
        public string MeetingTime { get; set; }
        public string Notes { get; set; }
        public string id { get; set; }
    }

    public class GateResponse
    {
        public string Reason { get; set; }
        public string Comments { get; set; }
        public int Cycle { get; set; }
        public DateTime DateTimeStamp { get; set; }
        public string id { get; set; }
    }



    #region DisplayInfo
    /// <summary>
    /// Project UI info from Accela
    /// </summary>
    public class AccelaProjectDisplayInfo
    {
        public string ProjectName { get; set; }
        public string ProjectAddress { get; set; }
        public string ProjectNumber { get; set; }
        public string RecordId { get; set; }
        public string BuildingCodeVersion { get; set; }
        public string TypeOfWork { get; set; }
        public string TypeOfConstruction { get; set; }
        public string Occupancy { get; set; }
        public string PrimaryOccupancy { get; set; }
        public string SecondaryOccupancy { get; set; }
        public string SquareFootage { get; set; }
        public string NumofSheets { get; set; }
        public string SealHolders { get; set; }
        public string Designers { get; set; }
        public string FireDetails { get; set; }
        public List<string> FloorList { get; set; }
        public string ScopeOfWorkOverall { get; set; }
        public string ScopeOfWorkMechanical { get; set; }
        public string ScopeOfWorkElectrical { get; set; }
        public string ScopeOfWorkPlumbing { get; set; }
        public string ScopeOfWorkCivil { get; set; }
        public string ZoningOfSite { get; set; }
        public string ChangeOfUse { get; set; }
        public string IsConditionalPermitApproval { get; set; }
        public string PreviousBusinessType { get; set; }
        public string CityOfC { get; set; }
        public string ProposedBusinessType { get; set; }
        public string CodeSummary { get; set; }
        public List<contactDetail> Contacts { get; set; }
        public string BackflowApplictnDet { get; set; }
        public string WaterSewerDetails { get; set; }
        public string HealthDeptDetails { get; set; }

        public string DayCare { get; set; }
        public string ProposedOutdoorUndergroundPiping { get; set; }
        public string ProposedFireSprinklerPiping { get; set; }
        public string IsInstallingCMUDBackflowPreventer { get; set; }
        public string ExtendingPublicWaterSewer { get; set; }
        public string GradeModificationWaterSewerEasement { get; set; }
        public string ProposedEncroachmentWaterSewerEasement { get; set; }

        //=====Preliminary Estimation===========
        public string ParcelNumber { get; set; }
        public string IsAffordableHousing { get; set; }
        public string ExactAddress { get; set; }
        public string DeliveryMethod { get; set; }
        public string IsBIM { get; set; }
        public string BIMDesignDiscipline { get; set; }
        public string NumOfAttendees { get; set; }
        public string PreviousPreliminaryReview { get; set; }
        public string ProjectNumberPrevPrelimReview { get; set; }
        public string IsSameReviewTeam { get; set; }
        public string PropertyOwnerName { get; set; }
        public string PropertyOwnerAddress { get; set; }
        public string PropertyOwnerEmail { get; set; }
        public string PropertyOwnerPhone { get; set; }
        public string PropertyOwnerAutoEmail { get; set; }
        public string PropertyManagerName { get; set; }
        public string PropertyManagerPhone { get; set; }
        public string PropertyManagerEmail { get; set; }
        public string PropertyManagerEmail2 { get; set; }
        public string ArchDesContactName { get; set; }
        public string ArchDesContactPhone { get; set; }
        public string ArchDesContactEmail { get; set; }
        public string ArchDesAutoEmail { get; set; }
        public string IsArchDrawingsSealed { get; set; }
        public string ArchDesLicenseNum { get; set; }
        public string ArchDesLicenseBoard { get; set; }
        public string IsArchDesEmployee { get; set; }
        public string PermitNumber { get; set; }
        public decimal? TotalFee { get; set; }
        public string GateDate { get; set; }
        public bool IsPaidStatus { get; set; }
        public string RequestedMeetingTime { get; set; }
        public string Agenda { get; set; }
        public bool DayCareCheckBox { get; set; }

        // LES-3407 additons for CommercialRTAP
        public string RTAPAffordableUnitChange { get; set; }
        public string RTAPAffordableUnitsRemove { get; set; }
        public string RTAPAffordableWorkforceUnitsAdd { get; set; }
        public string RTAPWorkforceAdd { get; set; }
        public string RTAPWorkforceRemove { get; set; }
        public string EquuipmentCost { get; set; }
        public List<ProfessionalDetail> Professional { get; set; }
        public string AccountNumber { get; set; }
        public string EquipmentCost { get; set; }
        public string PrepaidFeePaymentType { get; set; }
        // end of LUE-3407 changes 

        public AccelaProjectDisplayInfo()
        {
            FloorList = new List<string>();
            SealHolders = "";
            Designers = "";
            BackflowApplictnDet = "";
            FireDetails = "";
            WaterSewerDetails = "";
            HealthDeptDetails = "";
            DayCare = "";
            CityOfC = "";
            Professional = new List<ProfessionalDetail>();
        }
    }

    #endregion

    /// <summary>
    /// Department from Accela
    /// </summary>
    public abstract class DepartmentInfo : ExternalBase
    {
        /// <summary>
        /// Requested Reviewer Name from Accela
        /// </summary>
        public string RequestedReviewerName { get; set; }

        /// <summary>
        /// use constants from Meck.Shared.CityNamesEnumConst
        /// </summary>
        public string AccelaDepartmentDivisionRef { get; set; }

        /// <summary>
        /// use constants from Meck.Shared.PropertyTypeEnumConst
        /// </summary>
        public string AccelaDepartmentRegionRef { get; set; }
        /// <summary>
        /// dept was requested for preliminary meeting
        /// </summary>
        public bool IsDeptRequested { get; set; }
    }

    #region Preliminary Meeting 

    public class PrelimProjectSummaryObj
    {
        [JsonProperty("IncludesAfforableOrWorkforceHousing")]
        public string IncludesAfforableOrWorkforceHousing { get; set; }
        [JsonProperty("WorkforceHousingUnits")]
        public string WorkforceHousingUnits { get; set; }
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("AfforableHousingUnits")]
        public string AfforableHousingUnits { get; set; }

        public string To_String()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("IncludesAfforableOrWorkforceHousing|" + IncludesAfforableOrWorkforceHousing + ";");
            sb.Append("WorkforceHousingUnits|" + WorkforceHousingUnits + ";");
            sb.Append("AfforableHousingUnits|" + AfforableHousingUnits + ";");
            return sb.ToString();
        }
    }

    public class PrelimGeneralInfoObj
    {
        public string BuildingCode { get; set; }
        public string PropertyType { get; set; }
        public string id { get; set; }
        public string ReviewType { get; set; }

        public string To_String()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("BuildingCode|" + BuildingCode + ";");
            sb.Append("PropertyType|" + PropertyType + ";");
            sb.Append("ReviewType|" + ReviewType + ";");
            return sb.ToString();
        }
    }

    public class PrelimMeetingAgendaObj
    {
        public string AgendaAmenities { get; set; }
        public string AgendaAmenitiesLocation { get; set; }
        public string AgendaUpfitType { get; set; }
        public string AgendaElectricalSystemType { get; set; }
        public string AgendaPlumbingType { get; set; }
        public string AgendaAmenitiesDesign { get; set; }
        public string AgendaParkingGarage { get; set; }
        public string Agenda { get; set; }
        public string id { get; set; }
        public string AgendaSpecialWaste { get; set; }

        public string To_String()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("AgendaAmenities|" + AgendaAmenities + ";");
            sb.Append("AgendaUpfitType|" + AgendaUpfitType + ";");
            sb.Append("AgendaElectricalSystemType|" + AgendaElectricalSystemType + ";");
            sb.Append("AgendaPlumbingType|" + AgendaPlumbingType + ";");
            sb.Append("AgendaAmenitiesDesign|" + AgendaAmenitiesDesign + ";");
            sb.Append("AgendaParkingGarage|" + AgendaParkingGarage + ";");
            sb.Append("Agenda|" + Agenda + ";");
            sb.Append("AgendaSpecialWaste|" + AgendaSpecialWaste + ";");
            return sb.ToString();
        }
    }

    public class PrelimProposedWorkObj
    {
        public string ProposedScopeOfWork { get; set; }
        public string id { get; set; }

        public string To_String()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("ProposedScopeOfWork |" + ProposedScopeOfWork + ";");
            return sb.ToString();
        }
    }

    public class PrelimSystemInfoObj
    {
        public int CurrentReviewCycle { get; set; }
        public string ClonedFromProjectNumber { get; set; }
        public string GISAddressCode { get; set; }
        public string IsRTAP { get; set; }
        public decimal EstimatedFees { get; set; }
        public string TaxJurisdiction { get; set; }
        public string id { get; set; }
        public string FIFO { get; set; }
        public string RecordLocation { get; set; }

        public string To_String()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("CurrentReviewCycle|" + CurrentReviewCycle.ToString() + ";");
            sb.Append("ClonedFromProjectNumber|" + ClonedFromProjectNumber + ";");
            sb.Append("GISAddressCode|" + GISAddressCode + ";");
            sb.Append("IsRTAP|" + IsRTAP + ";");
            sb.Append("EstimatedFees|" + EstimatedFees.ToString() + ";");
            sb.Append("TaxJurisdiction|" + TaxJurisdiction + ";");
            sb.Append("FIFO|" + FIFO + ";");
            sb.Append("RecordLocation|" + RecordLocation + ";");
            return sb.ToString();
        }
    }

    public class PrelimBIMProjectDeliveryObj
    {
        public string ProjectDeliveryMethodDesignBuild { get; set; }
        public string ProjectDeliveryMethodCMOwnerAgent { get; set; }
        public string BIMDisciplinesElec { get; set; }
        public string ProjectDeliveryMethodFastTrack { get; set; }
        public string ProjectDeliveryMethodOther { get; set; }
        public string ProjectDeliveryMethodDesignBidBuild { get; set; }
        public string ProjectDeliveryMethodIPDOrVariation { get; set; }
        public string PDMOtherDescription { get; set; }
        public string PDMDisciplinesDesignAssistMech { get; set; }
        public string PDMDisciplinesDesignBuildArchStruct { get; set; }
        public string BIMDisciplinesArch { get; set; }
        public string ProjectDeliveryMethodDesignAssist { get; set; }
        public string PDMDisciplinesDesignBuildMech { get; set; }
        public string PDMDisciplinesDesignBuildPlumb { get; set; }
        public string PDMDisciplinesDesignAssistArchStruct { get; set; }
        public string PDMDisciplinesDesignAssistElec { get; set; }
        public string ProjectIsBim { get; set; }
        public string PDMDisciplinesDesignBuildElec { get; set; }
        public string id { get; set; }
        public string BIMDisciplinesStruct { get; set; }
        public string PDMDisciplinesDesignAssistPlumb { get; set; }
        public string BIMDisciplinesMech { get; set; }
        public string BIMDisciplinesPlumb { get; set; }

        public string To_String()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("ProjectDeliveryMethodDesignBuild|" + ProjectDeliveryMethodDesignBuild + ";");
            sb.Append("ProjectDeliveryMethodCMOwnerAgent|" + ProjectDeliveryMethodCMOwnerAgent + ";");
            sb.Append("BIMDisciplinesElec|" + BIMDisciplinesElec + ";");
            sb.Append("ProjectDeliveryMethodFastTrack|" + ProjectDeliveryMethodFastTrack + ";");
            sb.Append("ProjectDeliveryMethodOther|" + ProjectDeliveryMethodOther + ";");
            sb.Append("ProjectDeliveryMethodDesignBidBuild|" + ProjectDeliveryMethodDesignBidBuild + ";");
            sb.Append("ProjectDeliveryMethodIPDOrVariation|" + ProjectDeliveryMethodIPDOrVariation + ";");
            sb.Append("PDMOtherDescription|" + PDMOtherDescription + ";");
            sb.Append("PDMDisciplinesDesignAssistMech|" + PDMDisciplinesDesignAssistMech + ";");
            sb.Append("PDMDisciplinesDesignBuildArchStruct|" + PDMDisciplinesDesignBuildArchStruct + ";");
            sb.Append("BIMDisciplinesArch|" + BIMDisciplinesArch + ";");
            sb.Append("ProjectDeliveryMethodDesignAssist|" + ProjectDeliveryMethodDesignAssist + ";");
            sb.Append("PDMDisciplinesDesignBuildMech|" + PDMDisciplinesDesignBuildMech + ";");
            sb.Append("PDMDisciplinesDesignBuildPlumb|" + PDMDisciplinesDesignBuildPlumb + ";");
            sb.Append("PDMDisciplinesDesignAssistArchStruct|" + PDMDisciplinesDesignAssistArchStruct + ";");
            sb.Append("PDMDisciplinesDesignAssistElec|" + PDMDisciplinesDesignAssistElec + ";");
            sb.Append("ProjectIsBim|" + ProjectIsBim + ";");
            sb.Append("PDMDisciplinesDesignBuildElec|" + PDMDisciplinesDesignBuildElec + ";");
            sb.Append("BIMDisciplinesStruct|" + BIMDisciplinesStruct + ";");
            sb.Append("PDMDisciplinesDesignAssistPlumb |" + PDMDisciplinesDesignAssistPlumb + ";");
            sb.Append("BIMDisciplinesMech|" + BIMDisciplinesMech + ";");
            sb.Append("BIMDisciplinesPlumb|" + BIMDisciplinesPlumb + ";");
            return sb.ToString();
        }

    }

    public class PrelimTypeOfWorkObj
    {

        public string TypeOfWorkUpfit { get; set; }
        public string TypeOfWorkNewConFull { get; set; }
        public string TypeOfWorkPreEngMetalBldgOption { get; set; }
        public string TypeOfWorkDayCare { get; set; }
        public string TypeOfWorkNewConShellFootFoundPrev { get; set; }
        public string TypeOfWorkNewConShellFootFoundCore { get; set; }
        public string TypeOfWorkProCert { get; set; }
        public string TypeOfWorkChangeOfUse { get; set; }
        public string TypeOfWorkNewConFootFound { get; set; }
        public string TypeOfWorkNewConShellFootFound { get; set; }
        public string TypeOfWorkPreEngMetalBldg { get; set; }
        public string TypeOfWorkNewConsShellFootFoundCorePrev { get; set; }
        public string TypeOfWorkAddition { get; set; }
        public string id { get; set; }
        public string TypeOfWorkPrevOccupiedBldg { get; set; }

        public string To_String()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("TypeOfWorkUpfit|" + TypeOfWorkUpfit + ";");
            sb.Append("TypeOfWorkNewConFull|" + TypeOfWorkNewConFull + ";");
            sb.Append("TypeOfWorkPreEngMetalBldgOption|" + TypeOfWorkPreEngMetalBldgOption + ";");
            sb.Append("TypeOfWorkDayCare|" + TypeOfWorkDayCare + ";");
            sb.Append("TypeOfWorkNewConShellFootFoundPrev|" + TypeOfWorkNewConShellFootFoundPrev + ";");
            sb.Append("TypeOfWorkNewConShellFootFoundCore|" + TypeOfWorkNewConShellFootFoundCore + ";");
            sb.Append("TypeOfWorkProCert|" + TypeOfWorkProCert + ";");
            sb.Append("TypeOfWorkChangeOfUse|" + TypeOfWorkChangeOfUse + ";");
            sb.Append("TypeOfWorkNewConFootFound|" + TypeOfWorkNewConFootFound + ";");
            sb.Append("TypeOfWorkNewConShellFootFound|" + TypeOfWorkNewConShellFootFound + ";");
            sb.Append("TypeOfWorkPreEngMetalBldg|" + TypeOfWorkPreEngMetalBldg + ";");
            sb.Append("TypeOfWorkNewConsShellFootFoundCorePrev|" + TypeOfWorkNewConsShellFootFoundCorePrev + ";");
            sb.Append("TypeOfWorkAddition|" + TypeOfWorkAddition + ";");
            sb.Append("TypeOfWorkPrevOccupiedBldg|" + TypeOfWorkPrevOccupiedBldg + ";");

            return sb.ToString();
        }
    }

    public class PrelimMeetingDetailObj
    {
        public DateTime RequestedEndDateRange { get; set; }
        public DateTime RequestedBeginDateRange { get; set; }
        public string id { get; set; }
        public int NumberOfAttendees { get; set; }
        public string To_String()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("RequestedBeginDateRange|" + RequestedBeginDateRange.ToString() + ";");
            sb.Append("RequestedEndDateRange|" + RequestedEndDateRange.ToString() + ";");
            sb.Append("NumberOfAttendees|" + NumberOfAttendees.ToString() + ";");
            return sb.ToString();
        }
    }

    public class PrelimMeetingTradesAndReviewerObj
    {
        public string PrelimTradeBackflow { get; set; }
        public string PrelimTradeMechanicalReviewers { get; set; }
        public string PrelimTradeZoning { get; set; }
        public string PrelimTradeHealthReviewers { get; set; }
        public string PrelimTradeElectricalReviewers { get; set; }
        public string PrelimTradeHealth { get; set; }
        public string PrelimTradeMechanical { get; set; }
        public string PrelimTradeBuildingReviewers { get; set; }
        public string PrelimTradeZoningReviewers { get; set; }
        public string PreviousPrelimProjectNumber { get; set; }
        public string PrelimTradePlumbingReviewers { get; set; }
        public string PreviousPrelimReview { get; set; }
        public string PrelimTradeElectrical { get; set; }
        public string PrelimTradeFire { get; set; }
        public string id { get; set; }
        public string PrelimTradeBuilding { get; set; }
        public string PrelimTradePlumbing { get; set; }
        public string PrelimTradeBackflowReviewers { get; set; }
        public string PrelimTradeFireReviewers { get; set; }
        public string To_String()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("PrelimTradeBackflow|" + PrelimTradeBackflow + ";");
            sb.Append("PrelimTradeMechanicalReviewers|" + PrelimTradeMechanicalReviewers + ";");
            sb.Append("PrelimTradeZoning|" + PrelimTradeZoning + ";");
            sb.Append("PrelimTradeHealthReviewers |" + PrelimTradeHealthReviewers + ";");
            sb.Append("PrelimTradeElectricalReviewers|" + PrelimTradeElectricalReviewers + ";");
            sb.Append("PrelimTradeHealth|" + PrelimTradeHealth + ";");
            sb.Append("PrelimTradeMechanical|" + PrelimTradeMechanical + ";");
            sb.Append("PrelimTradeBuildingReviewers|" + PrelimTradeBuildingReviewers + ";");
            sb.Append("PrelimTradeZoningReviewers|" + PrelimTradeZoningReviewers + ";");
            sb.Append("PreviousPrelimProjectNumber|" + PreviousPrelimProjectNumber + ";");
            sb.Append("PrelimTradePlumbingReviewers|" + PrelimTradePlumbingReviewers + ";");
            sb.Append("PreviousPrelimReview|" + PreviousPrelimReview + ";");
            sb.Append("PrelimTradeElectrical|" + PrelimTradeElectrical + ";");
            sb.Append("PrelimTradeFire|" + PrelimTradeFire + ";");
            sb.Append("PrelimTradeBuilding|" + PrelimTradeBuilding + ";");
            sb.Append("PrelimTradePlumbing|" + PrelimTradePlumbing + ";");
            sb.Append("PrelimTradeBackflowReviewers|" + PrelimTradeBackflowReviewers + ";");
            sb.Append("PrelimTradeFireReviewers|" + PrelimTradeFireReviewers + ";");

            return sb.ToString();
        }
    }
    #endregion

    #region ProfessionalDetail 
    public class ProfessionalDetail
    {
        public string id { get; set; }
        public string fullName { get; set; }
        public string addressLine1 { get; set; }
        public string isPrimary { get; set; }
        public string referenceLicenseId { get; set; }
        public string phone2 { get; set; }
        public string updateOnUI { get; set; }
        public string licenseNumber { get; set; }
        public string lastName { get; set; }
        public string city { get; set; }
        public string email { get; set; }
        public string postalCode { get; set; }
        public LicenseType licenseType { get; set; }
        public State state { get; set; }

        public string To_String()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("id|" + id + ";");
            sb.Append("fullName|" + fullName + ";");
            sb.Append("addressLine1|" + addressLine1 + ";");
            sb.Append("isPrimary|" + isPrimary + ";");
            sb.Append("referenceLicenseId|" + referenceLicenseId + ";");
            sb.Append("phone2|" + phone2 + ";");
            sb.Append("updateOnUI|" + updateOnUI + ";");
            sb.Append("licenseNumber|" + licenseNumber + ";");
            sb.Append("lastName|" + lastName + ";");
            sb.Append("city|" + city + ";");
            sb.Append("email|" + email + ";");
            sb.Append("postalCode|" + postalCode + ";");
            sb.Append("LicenseType|" + licenseType.value + ";");
            sb.Append("State|" + state.value + ";");
            return sb.ToString();
        }
    }

    public class LicenseType
    {
        public string value { get; set; }
        public string text { get; set; }

        public LicenseType(string Text, string Value)
        {
            text = Text;
            value = Value;
        }



    }
    public class State
    {
        public string value { get; set; }
        public string text { get; set; }

        public State(string Text, string Value)
        {
            text = Text;
            value = Value;
        }
    }
    #endregion

}
