#region Using

using AION.Base;
using System;
using System.Runtime.Serialization;

#endregion

namespace AION.Estimator.Engine.BusinessEntities
{

    #region BusinessEntitiy - ProjectBE

    [DataContract]
    public class ProjectBE : BaseBE
    {

        #region Properties

        [DataMember]
        public int? ProjectId { get; set; }

        [DataMember]
        public string ProjectNm { get; set; }

        [DataMember]
        public int? ExternalSystemRefId { get; set; }

        [DataMember]
        public int? ProjectStatusRefId { get; set; }

        [DataMember]
        public int? ProjectTypRefId { get; set; }

        [DataMember]
        public string SrcSystemValTxt { get; set; }

        [DataMember]
        public string SrcProjectStatus { get; set; }

        [DataMember]
        public string TagCreatedIdNum { get; set; }

        [DataMember]
        public DateTime? TagCreatedByTs { get; set; }

        [DataMember]
        public DateTime? TagUpdatedByTs { get; set; }

        [DataMember]
        public string TagUpdatedByIdNum { get; set; }

        [DataMember]
        public int? AssignedEstimatorId { get; set; }

        [DataMember]
        public int? AssignedFacilitatorId { get; set; }

        [DataMember]
        public int? ProjectModeRefId { get; set; }

        [DataMember]
        public int? WorkflowStatusRefId { get; set; }

        [DataMember]
        public bool? RtapInd { get; set; }

        [DataMember]
        public bool? PreliminaryInd { get; set; }

        [DataMember]
        public string ProjectLvlTxt { get; set; }

        [DataMember]
        public DateTime? GateDt { get; set; }

        [DataMember]
        public string ProjectAddrTxt { get; set; }

        [DataMember]
        public int? ProjectManagerId { get; set; }

        [DataMember]
        public string BuildContrNm { get; set; }

        [DataMember]
        public string BuildContrAcctNum { get; set; }

        [DataMember]
        public bool? GateAcceptedInd { get; set; }

        [DataMember]
        public DateTime? FifoDueDt { get; set; }
        
        [DataMember]
        public DateTime? FifoDueAccelaDt { get; set; }

        [DataMember]
        public DateTime? PlansReadyOnDt { get; set; }

        [DataMember]
        public int? CycleNbr { get; set; }

        [DataMember]
        public bool? PrelimMeetingCompleteInd { get; set; }

        [DataMember]
        public string AccelaRtapProjectRefId { get; set; }

        [DataMember]
        public string AccelaPrelimProjectRefId { get; set; }

        [DataMember]
        public string ProjectOccupancyTypMapNm { get; set; }

        [DataMember]
        public string ConstrTypDesc { get; set; }

        [DataMember]
        public decimal? ConstrCostAmt { get; set; }

        [DataMember]
        public string SheetsCntDesc { get; set; }

        [DataMember]
        public int? SquareFootageToBeReviewedNbr { get; set; }

        [DataMember]
        public int? SquareFootageOfOverallBuildNbr { get; set; }

        [DataMember]
        public int? StoriesCnt { get; set; }

        [DataMember]
        public bool? HighRiseInd { get; set; }

        [DataMember]
        public bool? ExpressInd { get; set; }

        [DataMember]
        public string ReviewTypRefDesc { get; set; }

        [DataMember]
        public bool? PrelimMeetingCancelledInd { get; set; }

        [DataMember]
        public bool? FifoInd { get; set; }

        [DataMember]
        public decimal? TotalJobCostAmt { get; set; }

        [DataMember]
        public string WorkTypDesc { get; set; }

        [DataMember]
        public string OccupancyDesc { get; set; }

        [DataMember]
        public string PriOccupancyDesc { get; set; }

        [DataMember]
        public string SecondaryOccupancyDesc { get; set; }

        [DataMember]
        public string SealHoldersDesc { get; set; }

        [DataMember]
        public string DesignerDesc { get; set; }

        [DataMember]
        public string FireDetailDesc { get; set; }

        [DataMember]
        public string OverallWorkScopeDesc { get; set; }

        [DataMember]
        public string MechWorkScopeDesc { get; set; }

        [DataMember]
        public string ElctrWorkScopeDesc { get; set; }

        [DataMember]
        public string PlumbWorkScopeDesc { get; set; }

        [DataMember]
        public string CivilWorkScopeDesc { get; set; }

        [DataMember]
        public string ZoningOfSiteDesc { get; set; }

        [DataMember]
        public string ChgOfUseDesc { get; set; }

        [DataMember]
        public string ConditionalPermitApprovalDesc { get; set; }

        [DataMember]
        public string PreviousBusinessTypDesc { get; set; }

        [DataMember]
        public string CityOfCharlotteDesc { get; set; }

        [DataMember]
        public string ProposedBusinessTypDesc { get; set; }

        [DataMember]
        public string CodeSummaryDesc { get; set; }

        [DataMember]
        public string BackflowApplicationDetailDesc { get; set; }

        [DataMember]
        public string WaterSewerDetailDesc { get; set; }

        [DataMember]
        public string HealthDeptDetailDesc { get; set; }

        [DataMember]
        public string DayCareDesc { get; set; }

        [DataMember]
        public string ProposedOutdoorUndergroundPipingDesc { get; set; }

        [DataMember]
        public string ProposedFireSprinklerPipingDesc { get; set; }

        [DataMember]
        public string InstallCmudBackflowPreventerDesc { get; set; }

        [DataMember]
        public string ExtendingPublicWaterSewerDesc { get; set; }

        [DataMember]
        public string GradeModWaterSewerEasementDesc { get; set; }

        [DataMember]
        public string ProposedEncroachmentWaterSewerEasementDesc { get; set; }

        [DataMember]
        public string ParcelNum { get; set; }

        [DataMember]
        public string AffordableHousingDesc { get; set; }

        [DataMember]
        public string ExactAddrTxt { get; set; }

        [DataMember]
        public string DeliveryMthdDesc { get; set; }

        [DataMember]
        public string BimDesc { get; set; }

        [DataMember]
        public string BimDesignDisciplineDesc { get; set; }

        [DataMember]
        public string AttendeesCntDesc { get; set; }

        [DataMember]
        public string PreviousPrelimReviewDesc { get; set; }

        [DataMember]
        public string ProjectNumPreviousPrelimReviewDesc { get; set; }

        [DataMember]
        public string SameReviewTeamDesc { get; set; }

        [DataMember]
        public string PropertyOwnerNm { get; set; }

        [DataMember]
        public string PropertyOwnerAddrTxt { get; set; }

        [DataMember]
        public string PropertyOwnerEmailAddrTxt { get; set; }

        [DataMember]
        public string PropertyOwnerPhoneNum { get; set; }

        [DataMember]
        public string PropertyOwnerAutoEmailAddrTxt { get; set; }

        [DataMember]
        public string PropertyManagerNm { get; set; }

        [DataMember]
        public string PropertyManagerEmailAddrTxt { get; set; }

        [DataMember]
        public string PropertyManagerEmailAddr2Txt { get; set; }

        [DataMember]
        public string ArchitectDesignerCntctNm { get; set; }

        [DataMember]
        public string ArchitectDesignerCntctPhoneNum { get; set; }

        [DataMember]
        public string ArchitectDesignerCntctEmailAddrTxt { get; set; }

        [DataMember]
        public string ArchitectDesignerAutoEmailAddrTxt { get; set; }

        [DataMember]
        public string ArchitectDrawingsSealedDesc { get; set; }

        [DataMember]
        public string ArchitectDesignerLicenseNum { get; set; }

        [DataMember]
        public string ArchitectDesignerLicenseBoardDesc { get; set; }

        [DataMember]
        public string ArchitectDesignerEmployeeDesc { get; set; }

        [DataMember]
        public string PermitNum { get; set; }

        [DataMember]
        public decimal? TotalFeeAmt { get; set; }

        [DataMember]
        public string BuildCodeVersionDesc { get; set; }

        [DataMember]
        public string SquareFootageDesc { get; set; }

        [DataMember]
        public string PropertyManagerPhoneNum { get; set; }

        //Used for the dashboard only
        [DataMember]
        public DateTime? TentativeStartDate { get; set; }

        [DataMember]
        public DateTime? AcceptanceDeadline { get; set; }

        [DataMember]
        public string WorkStepIDNum { get; set; }
        [DataMember]
        public int QueueID { get; set; }
        [DataMember]
        public string WorkFlowTaskName { get; set; }

        /// <summary>
        /// Record ID from Accela
        /// Used to link to Accela record
        /// </summary>
        [DataMember]
        public string RecIdTxt { get; set; }

        /// <summary>
        /// Team Grade from Accela
        /// </summary>
        [DataMember]
        public string TeamGradeTxt { get; set; }

        //Preliminary mapping details to save in db and display if needed
        [DataMember]
        public string PrelimProjectSummaryObjDetails { get; set; }
        [DataMember]
        public string PrelimGeneralInfoObjDetails { get; set; }
        [DataMember]
        public string PrelimMeetingAgendaObjDetails { get; set; }
        [DataMember]
        public string PrelimProposedWorkObjDetails { get; set; }
        [DataMember]
        public string PrelimSystemInfoObjDetails { get; set; }
        [DataMember]
        public string PrelimBIMProjectDeliveryObjDetails { get; set; }
        [DataMember]
        public string PrelimTypeOfWorkObjDetails { get; set; }
        [DataMember]
        public string PrelimMeetingDetailObjDetails { get; set; }

        //LES-1945 jcl 8-16-2021
        [DataMember]
        public decimal? CancellationFee { get; set; }

        [DataMember]
        public bool? IsPaidStatus { get; set; }

        [DataMember]
        public string EstimatedFee { get; set; }

        //RTAP LES-3407
        [DataMember]
        public string RTAPAffordableUnitChange { get; set; }

        [DataMember]
        public string RTAPAffordableUnitsRemove { get; set; }

        [DataMember]
        public string RTAPAffordableWorkforceUnitsAdd { get; set; }

        [DataMember]
        public string RTAPWorkforceAdd { get; set; }

        [DataMember]
        public string RTAPWorkforceRemove { get; set; }

        [DataMember]
        public string Professionals { get; set; }

        [DataMember]
        public string AccountNumber { get; set; }

        [DataMember]
        public string EquipmentCost { get; set; }

        [DataMember]
        public string PrepaidFeePaymentType { get; set; }

        /// <summary>
        /// Used for the Dashboard Adapter
        /// </summary>
        [DataMember]
        public DateTime? PlanReviewCreatedDate { get; set; }
        #endregion

    }

    #endregion

}