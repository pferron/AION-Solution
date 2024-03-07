using AION.BL.Adapters;
using AION.BL.Helpers;
using AION.BL.Models;
using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using AION.Manager.AccelaBusinessObjects;
using Meck.Shared.MeckDataMapping;
using System.Collections.Generic;
using System.Linq;

namespace AION.BL.BusinessObjects
{
    public class ProjectEstimationModelBO : ProjectModelBaseBO, IProjectEstimationBO
    {
        public ProjectEstimation GetInstance(AccelaProjectModel accelaProjectInfo)
        {
            ProjectEstimation ret = (ProjectEstimation)InjectBaseObjects(new ProjectEstimation(), accelaProjectInfo);
            return ret;
        }

        public ProjectEstimation GetInstanceFromAION(int id)
        {
            ProjectBE projectBE = new ProjectBO().GetById(id);

            ProjectEstimation ret = ConvertFromBE(projectBE);

            bool isExpressProject = (ret.AionPropertyType == PropertyTypeEnums.Express);
            bool isFifo = (ret.AionPropertyType == PropertyTypeEnums.FIFO_Small_Commercial ||
                ret.AionPropertyType == PropertyTypeEnums.FIFO_Single_Family_Homes ||
                ret.AionPropertyType == PropertyTypeEnums.FIFO_Master_Plans ||
                ret.AionPropertyType == PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home);

            List<Reviewer> reviewers = new List<Reviewer>();
            if (ret.IsProjectPreliminary)
            {
                reviewers = new UserAdapter().GetReviewers(0, -1, isExpressProject);

                reviewers = reviewers.Where(x => x.IsPrelimMeetingAllowed == true).ToList();
            }
            else
            {
                reviewers = new UserAdapter().GetReviewers((int)ret.AionPropertyType, -1, isExpressProject);

            }

            ret.Reviewers = reviewers;
            return ret;
        }

        public ProjectEstimation GetInstanceFromAION(ProjectBE projectBE, bool includeReviewers = true)
        {
            ProjectEstimation ret = ConvertFromBE(projectBE);

            bool isExpressProject = (ret.AionPropertyType == PropertyTypeEnums.Express);
            bool isFifo = (ret.AionPropertyType == PropertyTypeEnums.FIFO_Small_Commercial ||
                ret.AionPropertyType == PropertyTypeEnums.FIFO_Single_Family_Homes ||
                ret.AionPropertyType == PropertyTypeEnums.FIFO_Master_Plans ||
                ret.AionPropertyType == PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home);

            if (includeReviewers)
            {
                List<Reviewer> reviewers = new List<Reviewer>();
                if (ret.IsProjectPreliminary)
                {
                    reviewers = new UserAdapter().GetReviewers(0, -1, isExpressProject);

                    reviewers = reviewers.Where(x => x.IsPrelimMeetingAllowed == true).ToList();
                }
                else
                {
                    reviewers = new UserAdapter().GetReviewers((int)ret.AionPropertyType, -1, isExpressProject);
                }

                ret.Reviewers = reviewers;
            }

            return ret;
        }

        public ProjectEstimation GetInstanceFromAIONAllDepartmentReviewers(ProjectBE projectBE)
        {
            ProjectEstimation ret = ConvertFromBE(projectBE);

            List<Reviewer> reviewers = new UserAdapter().GetReviewers((int)PropertyTypeEnums.NA, (int)DepartmentNameEnums.NA, false);

            ret.Reviewers = reviewers;

            return ret;
        }

        public ProjectEstimation ConvertFromBE(ProjectBE projectBE)
        {
            ProjectEstimation ret = new ProjectEstimation();
            UserIdentityModelBO userIdentityModelBO = new UserIdentityModelBO();

            ProjectStatus projectstatus = new ProjectStatusModelBO().GetInstance(projectBE.ProjectStatusRefId.Value);

            if (projectstatus == null) projectstatus = new ProjectStatus();

            ret.ID = projectBE.ProjectId.Value;
            ret.ProjectName = projectBE.ProjectNm;
            ret.CreatedUser = projectBE.CreatedByWkrId != null ? userIdentityModelBO.GetInstance(int.Parse(projectBE.CreatedByWkrId)) : null;
            ret.CreatedDate = projectBE.CreatedDate.Value;
            ret.UpdatedUser = projectBE.UpdatedByWkrId != null ? userIdentityModelBO.GetInstance(int.Parse(projectBE.UpdatedByWkrId)) : null;
            ret.UpdatedDate = projectBE.UpdatedDate.Value;
            ret.AIONProjectStatus = projectstatus;
            ret.AionPropertyType = projectBE.ProjectTypRefId.HasValue ? (PropertyTypeEnums)projectBE.ProjectTypRefId.Value : PropertyTypeEnums.NA;
            ret.AccelaPropertyType = (PropertyTypeEnums)projectBE.ProjectTypRefId;
            ret.AccelaProjectRefId = projectBE.SrcSystemValTxt;
            ret.AccelaProjectCreatedDate = projectBE.TagCreatedByTs;
            ret.AccelaProjectCreatedByRefId = projectBE.TagCreatedIdNum;
            ret.AccelaProjectLastUpdatedDate = projectBE.TagUpdatedByTs;
            ret.AccelaProjectLastUpdatedByRefId = projectBE.TagUpdatedByIdNum;
            ret.ProjectLvlTxt = projectBE.ProjectLvlTxt;
            ret.ProjectAddress = projectBE.ProjectAddrTxt;
            ret.PMId = projectBE.ProjectManagerId;
            ret.IsProjectRTAP = projectBE.RtapInd.Value;
            ret.IsGateAccepted = projectBE.GateAcceptedInd.Value;
            ret.GateDt = projectBE.GateDt;
            ret.AssignedFacilitator = projectBE.AssignedFacilitatorId.HasValue ? projectBE.AssignedFacilitatorId.Value : -1;
            ret.ReviewType = (ReviewTypeEnum)projectBE.ProjectModeRefId;
            ret.AssignedEstimator = projectBE.AssignedEstimatorId.HasValue ? projectBE.AssignedEstimatorId.Value : -1;
            ret.PlansReadyOnDate = projectBE.PlansReadyOnDt;
            ret.FifoDueDt = projectBE.FifoDueDt;
            ret.FifoDueAccelaDt = projectBE.FifoDueAccelaDt;
            ret.IsFifo = projectBE.FifoInd.Value;
            ret.BuildingContractorName = projectBE.BuildContrNm;
            ret.BuildingContractorAcctNo = projectBE.BuildContrAcctNum;
            ret.IsPreliminaryMeetingRequested = projectBE.PreliminaryInd.Value;
            ret.IsProjectPreliminary = projectBE.PreliminaryInd.Value;

            ret.ProjectOccupancyTypMapNm = projectBE.ProjectOccupancyTypMapNm;

            ret.AccelaConstructionType = projectBE.ConstrTypDesc;
            ret.AccelaCostOfConstruction = projectBE.ConstrCostAmt.HasValue ? (double)projectBE.ConstrCostAmt : 0;

            int resultInt;
            ret.AccelaNumberofSheets = int.TryParse(projectBE.SheetsCntDesc, out resultInt) ? resultInt : 0;

            ret.AccelaNumberOfStories = projectBE.StoriesCnt;
            ret.AccelaOccupancyType = projectBE.OccupancyDesc;
            ret.AccelaPreliminaryProjectRefId = projectBE.AccelaPrelimProjectRefId;
            ret.AccelaRTAPProjectRefId = projectBE.AccelaRtapProjectRefId;

            //map display only fields
            ret.DisplayOnlyInformation.ArchDesAutoEmail = projectBE.ArchitectDesignerAutoEmailAddrTxt;
            ret.DisplayOnlyInformation.ProjectAddress = projectBE.ProjectAddrTxt;
            ret.DisplayOnlyInformation.BuildingCodeVersion = projectBE.BuildCodeVersionDesc;
            ret.DisplayOnlyInformation.TypeOfWork = projectBE.WorkTypDesc;
            ret.DisplayOnlyInformation.TypeOfConstruction = projectBE.ConstrTypDesc;
            ret.DisplayOnlyInformation.Occupancy = projectBE.OccupancyDesc;
            ret.DisplayOnlyInformation.PrimaryOccupancy = projectBE.PriOccupancyDesc;
            ret.DisplayOnlyInformation.SecondaryOccupancy = projectBE.SecondaryOccupancyDesc;
            ret.DisplayOnlyInformation.SquareFootage = projectBE.SquareFootageDesc;
            ret.DisplayOnlyInformation.NumofSheets = projectBE.SheetsCntDesc;
            ret.DisplayOnlyInformation.SealHolders = projectBE.SealHoldersDesc;
            ret.DisplayOnlyInformation.Designers = projectBE.DesignerDesc;
            ret.DisplayOnlyInformation.FireDetails = projectBE.FireDetailDesc;
            ret.DisplayOnlyInformation.ScopeOfWorkOverall = projectBE.OverallWorkScopeDesc;
            ret.DisplayOnlyInformation.ScopeOfWorkMechanical = projectBE.MechWorkScopeDesc;
            ret.DisplayOnlyInformation.ScopeOfWorkCivil = projectBE.CivilWorkScopeDesc;
            ret.DisplayOnlyInformation.ScopeOfWorkElectrical = projectBE.ElctrWorkScopeDesc;
            ret.DisplayOnlyInformation.ScopeOfWorkPlumbing = projectBE.PlumbWorkScopeDesc;
            ret.DisplayOnlyInformation.ZoningOfSite = projectBE.ZoningOfSiteDesc;
            ret.DisplayOnlyInformation.ChangeOfUse = projectBE.ChgOfUseDesc;
            ret.DisplayOnlyInformation.IsConditionalPermitApproval = projectBE.ConditionalPermitApprovalDesc;
            ret.DisplayOnlyInformation.PreviousBusinessType = projectBE.PreviousBusinessTypDesc;
            ret.DisplayOnlyInformation.CityOfC = projectBE.CityOfCharlotteDesc;
            ret.DisplayOnlyInformation.ProposedBusinessType = projectBE.ProposedBusinessTypDesc;
            ret.DisplayOnlyInformation.CodeSummary = projectBE.CodeSummaryDesc;
            ret.DisplayOnlyInformation.BackflowApplictnDet = projectBE.BackflowApplicationDetailDesc;
            ret.DisplayOnlyInformation.WaterSewerDetails = projectBE.WaterSewerDetailDesc;
            ret.DisplayOnlyInformation.HealthDeptDetails = projectBE.HealthDeptDetailDesc;
            ret.DisplayOnlyInformation.DayCare = projectBE.DayCareDesc;
            ret.DisplayOnlyInformation.ProposedOutdoorUndergroundPiping = projectBE.ProposedOutdoorUndergroundPipingDesc;
            ret.DisplayOnlyInformation.ProposedFireSprinklerPiping = projectBE.ProposedFireSprinklerPipingDesc;
            ret.DisplayOnlyInformation.IsInstallingCMUDBackflowPreventer = projectBE.InstallCmudBackflowPreventerDesc;
            ret.DisplayOnlyInformation.ExtendingPublicWaterSewer = projectBE.ExtendingPublicWaterSewerDesc;
            ret.DisplayOnlyInformation.GradeModificationWaterSewerEasement = projectBE.GradeModWaterSewerEasementDesc;
            ret.DisplayOnlyInformation.ProposedEncroachmentWaterSewerEasement = projectBE.ProposedEncroachmentWaterSewerEasementDesc;
            ret.DisplayOnlyInformation.ParcelNumber = projectBE.ParcelNum;
            ret.DisplayOnlyInformation.IsAffordableHousing = projectBE.AffordableHousingDesc;
            ret.DisplayOnlyInformation.ExactAddress = projectBE.ExactAddrTxt;
            ret.DisplayOnlyInformation.DeliveryMethod = projectBE.DeliveryMthdDesc;
            ret.DisplayOnlyInformation.NumOfAttendees = projectBE.AttendeesCntDesc;
            ret.DisplayOnlyInformation.PreviousPreliminaryReview = projectBE.PreviousPrelimReviewDesc;
            ret.DisplayOnlyInformation.IsSameReviewTeam = projectBE.SameReviewTeamDesc;
            ret.DisplayOnlyInformation.PropertyOwnerName = projectBE.PropertyOwnerNm;
            ret.DisplayOnlyInformation.PropertyOwnerAddress = projectBE.PropertyOwnerAddrTxt;
            ret.DisplayOnlyInformation.PropertyOwnerEmail = projectBE.PropertyOwnerEmailAddrTxt;
            ret.DisplayOnlyInformation.PropertyOwnerPhone = projectBE.PropertyOwnerPhoneNum;
            ret.DisplayOnlyInformation.PropertyManagerName = projectBE.PropertyManagerNm;
            ret.DisplayOnlyInformation.PropertyManagerPhone = projectBE.PropertyManagerPhoneNum;
            ret.DisplayOnlyInformation.PropertyManagerEmail = projectBE.PropertyManagerEmailAddrTxt;
            ret.DisplayOnlyInformation.PropertyManagerEmail2 = projectBE.PropertyManagerEmailAddr2Txt;
            ret.DisplayOnlyInformation.ArchDesContactName = projectBE.ArchitectDesignerCntctNm;
            ret.DisplayOnlyInformation.ArchDesContactPhone = projectBE.ArchitectDesignerCntctPhoneNum;
            ret.DisplayOnlyInformation.ArchDesContactEmail = projectBE.ArchitectDesignerCntctEmailAddrTxt;
            ret.DisplayOnlyInformation.ArchDesAutoEmail = projectBE.ArchitectDesignerAutoEmailAddrTxt;
            ret.DisplayOnlyInformation.IsArchDrawingsSealed = projectBE.ArchitectDrawingsSealedDesc;
            ret.DisplayOnlyInformation.ArchDesLicenseNum = projectBE.ArchitectDesignerLicenseNum;
            ret.DisplayOnlyInformation.ArchDesLicenseBoard = projectBE.ArchitectDesignerLicenseBoardDesc;
            ret.DisplayOnlyInformation.IsArchDesEmployee = projectBE.ArchitectDesignerEmployeeDesc;
            ret.DisplayOnlyInformation.PermitNumber = projectBE.PermitNum;
            ret.DisplayOnlyInformation.TotalFee = projectBE.TotalFeeAmt.HasValue ? projectBE.TotalFeeAmt : 0;

            ret.CycleNbr = projectBE.CycleNbr.HasValue ? projectBE.CycleNbr : 0;
            ret.RecIdTxt = projectBE.RecIdTxt;
            ret.TeamGradeTxt = projectBE.TeamGradeTxt;
            ret.ReviewTypRefDesc = projectBE.ReviewTypRefDesc;

            ret.ProjectCostTotal = projectBE.TotalJobCostAmt;

            //jcl 8-11-21 preliminary obj details 
            ret.PrelimBIMProjectDeliveryObjDetails = projectBE.PrelimBIMProjectDeliveryObjDetails;
            ret.PrelimGeneralInfoObjDetails = projectBE.PrelimGeneralInfoObjDetails;
            ret.PrelimMeetingAgendaObjDetails = projectBE.PrelimMeetingAgendaObjDetails;
            ret.PrelimMeetingDetailObjDetails = projectBE.PrelimMeetingDetailObjDetails;
            ret.PrelimProjectSummaryObjDetails = projectBE.PrelimProjectSummaryObjDetails;
            ret.PrelimProposedWorkObjDetails = projectBE.PrelimProposedWorkObjDetails;
            ret.PrelimSystemInfoObjDetails = projectBE.PrelimSystemInfoObjDetails;
            ret.PrelimTypeOfWorkObjDetails = projectBE.PrelimTypeOfWorkObjDetails;

            ret.PrelimBIMProjectDelivery = AccelaMappingHelper.BuildPrelimBIMPDM(projectBE.PrelimBIMProjectDeliveryObjDetails);
            ret.PrelimGeneralInfo = AccelaMappingHelper.BuildPrelimGeneralInfo(projectBE.PrelimGeneralInfoObjDetails);
            ret.PrelimMeetingAgenda = AccelaMappingHelper.BuildPrelimAgenda(projectBE.PrelimMeetingAgendaObjDetails);
            ret.PrelimMeetingDetail = AccelaMappingHelper.BuildPrelimMeetingDetail(projectBE.PrelimMeetingDetailObjDetails);
            ret.PrelimProjectSummary = AccelaMappingHelper.BuildPrelimProjectSummary(projectBE.PrelimProjectSummaryObjDetails);
            ret.PrelimProposedWork = AccelaMappingHelper.BuildPrelimProposedScopeOfWork(projectBE.PrelimProposedWorkObjDetails);
            ret.PrelimSystemInfo = AccelaMappingHelper.BuildPrelimSystemInfo(projectBE.PrelimSystemInfoObjDetails);
            ret.PrelimTypeOfWork = AccelaMappingHelper.BuildPrelimTypeOfWork(projectBE.PrelimTypeOfWorkObjDetails);

            ret.CancellationFee = projectBE.CancellationFee;
            ret.IsPaidStatus = projectBE.IsPaidStatus;
            ret.EstimatedFee = projectBE.EstimatedFee;

            //LES-3407 RTAP
            ret.RTAPAffordableUnitChange = projectBE.RTAPAffordableUnitChange;
            ret.RTAPAffordableUnitsRemove = projectBE.RTAPAffordableUnitsRemove;
            ret.RTAPAffordableWorkforceUnitsAdd = projectBE.RTAPAffordableWorkforceUnitsAdd;
            ret.RTAPWorkforceAdd = projectBE.RTAPWorkforceAdd;
            ret.RTAPWorkforceRemove = projectBE.RTAPWorkforceRemove;
            ret.Professionals = projectBE.Professionals;
            ret.ProfessionalsList = ProfessionalDetailBO.ConvertCSVToList(projectBE.Professionals, '~');
            ret.AccountNumber = projectBE.AccountNumber;
            ret.EquipmentCost = projectBE.EquipmentCost;
            ret.PrepaidFeePaymentType = projectBE.PrepaidFeePaymentType;

            if (projectBE.ProjectManagerId.HasValue && projectBE.ProjectManagerId > 0)
            {
                UserIdentity projectManager = projectBE.ProjectManagerId.HasValue ? userIdentityModelBO.GetInstance(projectBE.ProjectManagerId.Value) : null;
                ret.PMEmail = projectManager.Email;
                ret.PMName = projectManager.FirstName + " " + projectManager.LastName;
                ret.PMFirstName = projectManager.FirstName;
                ret.PMLastName = projectManager.LastName;
                ret.PMPhone = projectManager.Phone;
                ret.PMId = projectBE.ProjectManagerId.Value;
            }

            GetExistingProjectDepartments(ret);

            //Set the jurisdiction from the agency zoning 
            //Used by the UI to get the correct dropdowns for assigning reviewers
            if (ret.Agencies != null && ret.Agencies.Any())
                ret.Jurisdiction = ret.Agencies.Where(x => x.DepartmentDivision == DepartmentDivisionEnum.Zoning).FirstOrDefault().DepartmentInfo;

            return ret;
        }

        public void GetProjectDepartments(ProjectEstimation projectEstimation)
        {
            GetExistingProjectDepartments(projectEstimation);
        }

    }

    public interface IProjectEstimationBO
    {
        ProjectEstimation GetInstance(AccelaProjectModel accelaProjectInfo);
        ProjectEstimation GetInstanceFromAION(int id);
    }
}
