using Meck.Shared.Accela;
using Meck.Shared.MeckDataMapping;
using System;
using System.Collections.Generic;

namespace AION.BL
{
    public abstract class Project : ModelBase
    {

        public Project()
        {
            Notes = new List<Note>();
            AIONProjectStatus = new ProjectStatus();

            AccelaProjectCreatedBy = new UserIdentity();
            AccelaProjectLastUpdatedBy = new UserIdentity();
            AccelaApplicationNotes = new List<Note>();
            Trades = new List<ProjectTrade>();
            Agencies = new List<ProjectAgency>();
            IsFacilitatorMeeting = false;
            TradesAccela = new List<Meck.Shared.MeckDataMapping.TradeInfo>();
            AgenciesAccela = new List<Meck.Shared.MeckDataMapping.AgencyInfo>();
            PrelimBIMProjectDelivery = new PrelimBIMProjectDeliveryObj();
            PrelimGeneralInfo = new PrelimGeneralInfoObj();
            PrelimMeetingAgenda = new PrelimMeetingAgendaObj();
            PrelimMeetingDetail = new PrelimMeetingDetailObj();
            PrelimMeetingTradesAndReviewer = new PrelimMeetingTradesAndReviewerObj();
            PrelimProjectSummary = new PrelimProjectSummaryObj();
            PrelimProposedWork = new PrelimProposedWorkObj();
            PrelimSystemInfo = new PrelimSystemInfoObj();
            PrelimTypeOfWork = new PrelimTypeOfWorkObj();
            DisplayOnlyInformation = new AccelaProjectDisplayInfo();

        }


        /// <summary>
        /// Notes from UI AION
        /// </summary>
        public List<Note> Notes { get; set; }

        /// <summary>
        /// Project Name from AION
        /// </summary>
        public string ProjectName { get; set; }


        /// <summary>
        /// Project Reference ID from Accela
        /// </summary>
        public string AccelaProjectRefId { get; set; }

        public string AccelaRTAPProjectRefId { get; set; }

        public string AccelaPreliminaryProjectRefId { get; set; }

        /// <summary>
        /// Project Address from Accela Display Info UI
        /// </summary>
        public string ProjectAddress { get; set; }

        /// <summary>
        /// Bool indicating if meeting is Requested by Customer
        /// </summary>
        public bool IsPreliminaryMeetingRequested { get; set; }
        /// <summary>
        /// Bool indicating if meeting is completed from Accela
        /// </summary>
        public bool IsPreliminaryMeetingCompleted { get; set; }

        public bool IsPreliminaryMeetingCancelled { get; set; }

        /// <summary>
        /// bool indicating if the project is RTAP from Accela
        /// </summary>
        public bool IsProjectRTAP { get; set; }
        public bool IsGateAccepted { get; set; }

        public string PMName { get; set; }
        public string PMFirstName { get; set; }
        public string PMLastName { get; set; }
        public string PMPhone { get; set; }
        public string PMEmail { get; set; }
        public int? PMId { get; set; }
        public bool IsProjectPreliminary { get; set; }
        public bool IsFacilitatorMeeting { get; set; }

        /// <summary>
        /// Building Code Version from UI Display info only
        /// </summary>
        public string BuildingCodeVersion { get; set; }

        /// <summary>
        /// Indicates Express, On Schedule from AION
        /// </summary>
        public ReviewTypeEnum ReviewType { get; set; }

        public string ReviewTypRefDesc { get; set; }  // for displaying the Accela Property Type Ref

        /// <summary>
        /// indicates MMF, Townhomes, FIFO, etc from Accela
        /// </summary>
        public PropertyTypeEnums AccelaPropertyType { get; set; }

        /// <summary>
        /// indicates MMF, Townhomes, FIFO, etc from AION
        /// </summary>
        public PropertyTypeEnums AionPropertyType { get; set; }

        /// <summary>
        /// Assigned Estimator Reference id/info/name from Accela
        /// </summary>
        public string AssignedEstimatorRefInfo;

        /// <summary>
        /// Assigned Estimator from AION
        /// </summary>
        public int AssignedEstimator { get; set; }

        /// <summary>
        /// Assigned Facilitator id/info/name from Accela
        /// </summary>
        public string AssignedFacilitatorRefInfo { get; set; }


        //public UserIdentity AssignedFacilitator { get; set; }
        /// <summary>
        /// Assigned Facilitator from AION
        //
        /// </summary>
        public int? AssignedFacilitator { get; set; }

        /// <summary>
        /// Project Status from AION
        /// </summary>
        public ProjectStatus AIONProjectStatus { get; set; }

        /// <summary>
        /// Workflow Status from AION
        /// </summary>
        public ProjectStatus AIONProjectWorkFlowStatus { get; set; }

        /// <summary>
        /// Project Status from Accela
        /// </summary>
        public string AccelaProjectStatus { get; set; }

        /// <summary>
        /// Project Create DAte from Accela
        /// </summary>
        public DateTime? AccelaProjectCreatedDate { get; set; }

        /// <summary>
        /// Project create by user from Accela
        /// </summary>
        public UserIdentity AccelaProjectCreatedBy { get; set; }
        /// <summary>
        /// Project create by user from Accela
        /// </summary>
        public string AccelaProjectCreatedByRefId { get; set; }
        /// <summary>
        /// Project last update date from Accela
        /// </summary>
        public DateTime? AccelaProjectLastUpdatedDate { get; set; }

        /// <summary>
        /// Project last update by user from Accela
        /// </summary>
        public UserIdentity AccelaProjectLastUpdatedBy { get; set; }
        /// <summary>
        /// Project last update by user from Accela
        /// </summary>
        public string AccelaProjectLastUpdatedByRefId { get; set; }
        /// <summary>
        /// Application Notes from input application in Accela
        /// </summary>
        public List<Note> AccelaApplicationNotes { get; set; }

        /// <summary>
        /// list of agencies (fire, zoning, etc) from Accela
        /// </summary>
        public List<ProjectAgency> Agencies { get; set; }

        public List<Meck.Shared.MeckDataMapping.AgencyInfo> AgenciesAccela { get; set; }

        /// <summary>
        /// list of trades (building, electrical, etc) from Accela
        /// </summary>
        public List<ProjectTrade> Trades { get; set; }

        public List<Meck.Shared.MeckDataMapping.TradeInfo> TradesAccela { get; set; }
        public string AccelaConstructionType { get; set; }
        public string AccelaOccupancyType { get; set; }
        public List<RequestedExpressDateBE> AccelaRequestedExpressDates { get; set; }

        public int AccelaSqrFtToBeReviewed { get; set; }
        public int? AccelaSqrFtOfOverallBuilding { get; set; }
        public int? AccelaNumberOfStories { get; set; }
        public bool? IsHighRise { get; set; }
        public double AccelaCostOfConstruction { get; set; }
        public int AccelaNumberofSheets { get; set; }

        /// <summary>
        /// Project Mode - Express, OnSchedule, NA
        /// </summary>
        public int? ProjectModeRefId { get; set; }
        public string ProjectLvlTxt { get; set; }
        public DateTime? GateDt { get; set; }
        public string ProjectOccupancyTypMapNm { get; set; }


        /// <summary>
        /// This indicates if this project was submitted or "saved" by the user
        /// </summary>
        public SaveTypeEnum SaveType { get; set; }

        public DateTime? PlansReadyOnDate { get; set; }

        public bool IsFifo { get; set; }

        public DateTime? FifoDueDt { get; set; }

        public DateTime? FifoDueAccelaDt { get; set; }

        public string BuildingContractorName { get; set; }

        public string BuildingContractorAcctNo { get; set; }

        public int? CycleNbr { get; set; }

        /// <summary>
        /// Record ID from Accela
        /// Used to link to Accela record
        /// </summary>
        public string RecIdTxt { get; set; }

        public string TeamGradeTxt { get; set; }

        /// <summary>
        /// CE_COM-PROJECT.cCOST  "ProjectCostTotal": "365000"
        /// </summary>
        public decimal? ProjectCostTotal { get; set; }

        //Preliminary mapping details to save in db and display if needed
        public string PrelimProjectSummaryObjDetails { get; set; }
        public string PrelimGeneralInfoObjDetails { get; set; }
        public string PrelimMeetingAgendaObjDetails { get; set; }
        public string PrelimProposedWorkObjDetails { get; set; }
        public string PrelimSystemInfoObjDetails { get; set; }
        public string PrelimBIMProjectDeliveryObjDetails { get; set; }
        public string PrelimTypeOfWorkObjDetails { get; set; }
        public string PrelimMeetingDetailObjDetails { get; set; }

        //jcl 8-13-21 preliminary objects for display in UI, this data is saved as text in the details fields in the Project class object
        public PrelimProjectSummaryObj PrelimProjectSummary { get; set; }
        public PrelimGeneralInfoObj PrelimGeneralInfo { get; set; }
        public PrelimMeetingAgendaObj PrelimMeetingAgenda { get; set; }
        public PrelimProposedWorkObj PrelimProposedWork { get; set; }
        public PrelimSystemInfoObj PrelimSystemInfo { get; set; }
        public PrelimBIMProjectDeliveryObj PrelimBIMProjectDelivery { get; set; }
        public PrelimTypeOfWorkObj PrelimTypeOfWork { get; set; }
        public PrelimMeetingDetailObj PrelimMeetingDetail { get; set; }
        public PrelimMeetingTradesAndReviewerObj PrelimMeetingTradesAndReviewer { get; set; }

        //LES-1945 jcl 8-16-2021
        public decimal? CancellationFee { get; set; }

        public bool? IsPaidStatus { get; set; }
        public string EstimatedFee { get; set; }

        //LES-3407 
        public string RTAPAffordableUnitChange { get; set; }
        public string RTAPAffordableUnitsRemove { get; set; }
        public string RTAPAffordableWorkforceUnitsAdd { get; set; }
        public string RTAPWorkforceAdd { get; set; }
        public string RTAPWorkforceRemove { get; set; }
        public string Professionals { get; set; }
        public List<ProfessionalDetail> ProfessionalsList { get; set; }
        public string AccountNumber { get; set; }
        public string EquipmentCost { get; set; }
        public string PrepaidFeePaymentType { get; set; }
        /// <summary>
        /// Project Accela UI information
        /// </summary>
        public AccelaProjectDisplayInfo DisplayOnlyInformation { get; set; }
        public DepartmentNameEnums Jurisdiction { get; set; }

        /// <summary>
        /// Audit Action for insert project audit processing
        /// 
        /// </summary>
        public AuditActionEnum AuditAction { get; set; }

    }

}
