#region Using

using AION.Base;
using AION.Estimator.Engine.BusinessEntities;
using Meck.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

#endregion

namespace AION.Estimator.Engine.BusinessObjects
{

    #region BusinessObject - ProjectBO

    public class ProjectBO : BaseBO, IDataContextProjectBO
    {

        #region Private Members

        private enum ActionType { Create, Delete, GetById, GetDataSet, GetList, Update };

        private string _errorMsg;

        private ProjectBE _projectBE;

        private int _id;

        #endregion

        #region Public Methods

        public int Create(ProjectBE projectBE)
        {
            int id;
            _projectBE = projectBE;

            if (!this.Validate(ActionType.Create))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[126];

                sqlParameters[0] = new SqlParameter("@PROJECT_NM", projectBE.ProjectNm);
                sqlParameters[1] = new SqlParameter("@EXTERNAL_SYSTEM_REF_ID", projectBE.ExternalSystemRefId);
                sqlParameters[2] = new SqlParameter("@PROJECT_STATUS_REF_ID", projectBE.ProjectStatusRefId);
                sqlParameters[3] = new SqlParameter("@PROJECT_TYP_REF_ID", projectBE.ProjectTypRefId);
                sqlParameters[4] = new SqlParameter("@SRC_SYSTEM_VAL_TXT", projectBE.SrcSystemValTxt);
                sqlParameters[5] = new SqlParameter("@TAG_CREATED_ID_NUM", projectBE.TagCreatedIdNum);
                sqlParameters[6] = new SqlParameter("@TAG_CREATED_BY_TS", projectBE.TagCreatedByTs);
                sqlParameters[7] = new SqlParameter("@TAG_UPDATED_BY_TS", projectBE.TagUpdatedByTs);
                sqlParameters[8] = new SqlParameter("@TAG_UPDATED_BY_ID_NUM", projectBE.TagUpdatedByIdNum);
                sqlParameters[9] = new SqlParameter("@ASSIGNED_ESTIMATOR_ID", projectBE.AssignedEstimatorId);
                sqlParameters[10] = new SqlParameter("@ASSIGNED_FACILITATOR_ID", projectBE.AssignedFacilitatorId);
                sqlParameters[11] = new SqlParameter("@PROJECT_MODE_REF_ID", projectBE.ProjectModeRefId);
                sqlParameters[12] = new SqlParameter("@WORKFLOW_STATUS_REF_ID", projectBE.WorkflowStatusRefId);
                sqlParameters[13] = new SqlParameter("@RTAP_IND", projectBE.RtapInd);
                sqlParameters[14] = new SqlParameter("@PRELIMINARY_IND", projectBE.PreliminaryInd);
                sqlParameters[15] = new SqlParameter("@PROJECT_LVL_TXT", projectBE.ProjectLvlTxt);
                sqlParameters[16] = new SqlParameter("@GATE_DT", projectBE.GateDt);
                sqlParameters[17] = new SqlParameter("@PROJECT_ADDR_TXT", projectBE.ProjectAddrTxt);
                sqlParameters[18] = new SqlParameter("@PROJECT_MANAGER_ID", projectBE.ProjectManagerId);
                sqlParameters[19] = new SqlParameter("@BUILD_CONTR_NM", projectBE.BuildContrNm);
                sqlParameters[20] = new SqlParameter("@BUILD_CONTR_ACCT_NUM", projectBE.BuildContrAcctNum);
                sqlParameters[21] = new SqlParameter("@GATE_ACCEPTED_IND", projectBE.GateAcceptedInd);
                sqlParameters[22] = new SqlParameter("@FIFO_DUE_DT", projectBE.FifoDueDt);
                sqlParameters[23] = new SqlParameter("@PLANS_READY_ON_DT", projectBE.PlansReadyOnDt);
                sqlParameters[24] = new SqlParameter("@CYCLE_NBR", projectBE.CycleNbr);
                sqlParameters[25] = new SqlParameter("@PRELIM_MEETING_COMPLETE_IND", projectBE.PrelimMeetingCompleteInd);
                sqlParameters[26] = new SqlParameter("@ACCELA_RTAP_PROJECT_REF_ID", projectBE.AccelaRtapProjectRefId);
                sqlParameters[27] = new SqlParameter("@ACCELA_PRELIM_PROJECT_REF_ID", projectBE.AccelaPrelimProjectRefId);
                sqlParameters[28] = new SqlParameter("@PROJECT_OCCUPANCY_TYP_MAP_NM", projectBE.ProjectOccupancyTypMapNm);
                sqlParameters[29] = new SqlParameter("@CONSTR_TYP_DESC", projectBE.ConstrTypDesc);
                sqlParameters[30] = new SqlParameter("@CONSTR_COST_AMT", projectBE.ConstrCostAmt);
                sqlParameters[31] = new SqlParameter("@SHEETS_CNT_DESC", projectBE.SheetsCntDesc);
                sqlParameters[32] = new SqlParameter("@SQUARE_FOOTAGE_TO_BE_REVIEWED_NBR", projectBE.SquareFootageToBeReviewedNbr);
                sqlParameters[33] = new SqlParameter("@SQUARE_FOOTAGE_OF_OVERALL_BUILD_NBR", projectBE.SquareFootageOfOverallBuildNbr);
                sqlParameters[34] = new SqlParameter("@STORIES_CNT", projectBE.StoriesCnt);
                sqlParameters[35] = new SqlParameter("@HIGH_RISE_IND", projectBE.HighRiseInd);
                sqlParameters[36] = new SqlParameter("@EXPRESS_IND", projectBE.ExpressInd);
                sqlParameters[37] = new SqlParameter("@REVIEW_TYP_REF_DESC", projectBE.ReviewTypRefDesc);
                sqlParameters[38] = new SqlParameter("@PRELIM_MEETING_CANCELLED_IND", projectBE.PrelimMeetingCancelledInd);
                sqlParameters[39] = new SqlParameter("@FIFO_IND", projectBE.FifoInd);
                sqlParameters[40] = new SqlParameter("@TOTAL_JOB_COST_AMT", projectBE.TotalJobCostAmt);
                sqlParameters[41] = new SqlParameter("@WORK_TYP_DESC", projectBE.WorkTypDesc);
                sqlParameters[42] = new SqlParameter("@OCCUPANCY_DESC", projectBE.OccupancyDesc);
                sqlParameters[43] = new SqlParameter("@PRI_OCCUPANCY_DESC", projectBE.PriOccupancyDesc);
                sqlParameters[44] = new SqlParameter("@SECONDARY_OCCUPANCY_DESC", projectBE.SecondaryOccupancyDesc);
                sqlParameters[45] = new SqlParameter("@SEAL_HOLDERS_DESC", projectBE.SealHoldersDesc);
                sqlParameters[46] = new SqlParameter("@DESIGNER_DESC", projectBE.DesignerDesc);
                sqlParameters[47] = new SqlParameter("@FIRE_DETAIL_DESC", projectBE.FireDetailDesc);
                sqlParameters[48] = new SqlParameter("@OVERALL_WORK_SCOPE_DESC", projectBE.OverallWorkScopeDesc);
                sqlParameters[49] = new SqlParameter("@MECH_WORK_SCOPE_DESC", projectBE.MechWorkScopeDesc);
                sqlParameters[50] = new SqlParameter("@ELCTR_WORK_SCOPE_DESC", projectBE.ElctrWorkScopeDesc);
                sqlParameters[51] = new SqlParameter("@PLUMB_WORK_SCOPE_DESC", projectBE.PlumbWorkScopeDesc);
                sqlParameters[52] = new SqlParameter("@CIVIL_WORK_SCOPE_DESC", projectBE.CivilWorkScopeDesc);
                sqlParameters[53] = new SqlParameter("@ZONING_OF_SITE_DESC", projectBE.ZoningOfSiteDesc);
                sqlParameters[54] = new SqlParameter("@CHG_OF_USE_DESC", projectBE.ChgOfUseDesc);
                sqlParameters[55] = new SqlParameter("@CONDITIONAL_PERMIT_APPROVAL_DESC", projectBE.ConditionalPermitApprovalDesc);
                sqlParameters[56] = new SqlParameter("@PREVIOUS_BUSINESS_TYP_DESC", projectBE.PreviousBusinessTypDesc);
                sqlParameters[57] = new SqlParameter("@CITY_OF_CHARLOTTE_DESC", projectBE.CityOfCharlotteDesc);
                sqlParameters[58] = new SqlParameter("@PROPOSED_BUSINESS_TYP_DESC", projectBE.ProposedBusinessTypDesc);
                sqlParameters[59] = new SqlParameter("@CODE_SUMMARY_DESC", projectBE.CodeSummaryDesc);
                sqlParameters[60] = new SqlParameter("@BACKFLOW_APPLICATION_DETAIL_DESC", projectBE.BackflowApplicationDetailDesc);
                sqlParameters[61] = new SqlParameter("@WATER_SEWER_DETAIL_DESC", projectBE.WaterSewerDetailDesc);
                sqlParameters[62] = new SqlParameter("@HEALTH_DEPT_DETAIL_DESC", projectBE.HealthDeptDetailDesc);
                sqlParameters[63] = new SqlParameter("@DAY_CARE_DESC", projectBE.DayCareDesc);
                sqlParameters[64] = new SqlParameter("@PROPOSED_OUTDOOR_UNDERGROUND_PIPING_DESC", projectBE.ProposedOutdoorUndergroundPipingDesc);
                sqlParameters[65] = new SqlParameter("@PROPOSED_FIRE_SPRINKLER_PIPING_DESC", projectBE.ProposedFireSprinklerPipingDesc);
                sqlParameters[66] = new SqlParameter("@INSTALL_CMUD_BACKFLOW_PREVENTER_DESC", projectBE.InstallCmudBackflowPreventerDesc);
                sqlParameters[67] = new SqlParameter("@EXTENDING_PUBLIC_WATER_SEWER_DESC", projectBE.ExtendingPublicWaterSewerDesc);
                sqlParameters[68] = new SqlParameter("@GRADE_MOD_WATER_SEWER_EASEMENT_DESC", projectBE.GradeModWaterSewerEasementDesc);
                sqlParameters[69] = new SqlParameter("@PROPOSED_ENCROACHMENT_WATER_SEWER_EASEMENT_DESC", projectBE.ProposedEncroachmentWaterSewerEasementDesc);
                sqlParameters[70] = new SqlParameter("@PARCEL_NUM", projectBE.ParcelNum);
                sqlParameters[71] = new SqlParameter("@AFFORDABLE_HOUSING_DESC", projectBE.AffordableHousingDesc);
                sqlParameters[72] = new SqlParameter("@EXACT_ADDR_TXT", projectBE.ExactAddrTxt);
                sqlParameters[73] = new SqlParameter("@DELIVERY_MTHD_DESC", projectBE.DeliveryMthdDesc);
                sqlParameters[74] = new SqlParameter("@BIM_DESC", projectBE.BimDesc);
                sqlParameters[75] = new SqlParameter("@BIM_DESIGN_DISCIPLINE_DESC", projectBE.BimDesignDisciplineDesc);
                sqlParameters[76] = new SqlParameter("@ATTENDEES_CNT_DESC", projectBE.AttendeesCntDesc);
                sqlParameters[77] = new SqlParameter("@PREVIOUS_PRELIM_REVIEW_DESC", projectBE.PreviousPrelimReviewDesc);
                sqlParameters[78] = new SqlParameter("@PROJECT_NUM_PREVIOUS_PRELIM_REVIEW_DESC", projectBE.ProjectNumPreviousPrelimReviewDesc);
                sqlParameters[79] = new SqlParameter("@SAME_REVIEW_TEAM_DESC", projectBE.SameReviewTeamDesc);
                sqlParameters[80] = new SqlParameter("@PROPERTY_OWNER_NM", projectBE.PropertyOwnerNm);
                sqlParameters[81] = new SqlParameter("@PROPERTY_OWNER_ADDR_TXT", projectBE.PropertyOwnerAddrTxt);
                sqlParameters[82] = new SqlParameter("@PROPERTY_OWNER_EMAIL_ADDR_TXT", projectBE.PropertyOwnerEmailAddrTxt);
                sqlParameters[83] = new SqlParameter("@PROPERTY_OWNER_PHONE_NUM", projectBE.PropertyOwnerPhoneNum);
                sqlParameters[84] = new SqlParameter("@PROPERTY_OWNER_AUTO_EMAIL_ADDR_TXT", projectBE.PropertyOwnerAutoEmailAddrTxt);
                sqlParameters[85] = new SqlParameter("@PROPERTY_MANAGER_NM", projectBE.PropertyManagerNm);
                sqlParameters[86] = new SqlParameter("@PROPERTY_MANAGER_EMAIL_ADDR_TXT", projectBE.PropertyManagerEmailAddrTxt);
                sqlParameters[87] = new SqlParameter("@PROPERTY_MANAGER_EMAIL_ADDR_2_TXT", projectBE.PropertyManagerEmailAddr2Txt);
                sqlParameters[88] = new SqlParameter("@ARCHITECT_DESIGNER_CNTCT_NM", projectBE.ArchitectDesignerCntctNm);
                sqlParameters[89] = new SqlParameter("@ARCHITECT_DESIGNER_CNTCT_PHONE_NUM", projectBE.ArchitectDesignerCntctPhoneNum);
                sqlParameters[90] = new SqlParameter("@ARCHITECT_DESIGNER_CNTCT_EMAIL_ADDR_TXT", projectBE.ArchitectDesignerCntctEmailAddrTxt);
                sqlParameters[91] = new SqlParameter("@ARCHITECT_DESIGNER_AUTO_EMAIL_ADDR_TXT", projectBE.ArchitectDesignerAutoEmailAddrTxt);
                sqlParameters[92] = new SqlParameter("@ARCHITECT_DRAWINGS_SEALED_DESC", projectBE.ArchitectDrawingsSealedDesc);
                sqlParameters[93] = new SqlParameter("@ARCHITECT_DESIGNER_LICENSE_NUM", projectBE.ArchitectDesignerLicenseNum);
                sqlParameters[94] = new SqlParameter("@ARCHITECT_DESIGNER_LICENSE_BOARD_DESC", projectBE.ArchitectDesignerLicenseBoardDesc);
                sqlParameters[95] = new SqlParameter("@ARCHITECT_DESIGNER_EMPLOYEE_DESC", projectBE.ArchitectDesignerEmployeeDesc);
                sqlParameters[96] = new SqlParameter("@PERMIT_NUM", projectBE.PermitNum);
                sqlParameters[97] = new SqlParameter("@TOTAL_FEE_AMT", projectBE.TotalFeeAmt);
                sqlParameters[98] = new SqlParameter("@BUILD_CODE_VERSION_DESC", projectBE.BuildCodeVersionDesc);
                sqlParameters[99] = new SqlParameter("@SQUARE_FOOTAGE_DESC", projectBE.SquareFootageDesc);
                sqlParameters[100] = new SqlParameter("@PROPERTY_MANAGER_PHONE_NUM", projectBE.PropertyManagerPhoneNum);
                sqlParameters[101] = new SqlParameter("@REC_ID_TXT", projectBE.RecIdTxt);
                sqlParameters[102] = new SqlParameter("@TEAM_GRADE_TXT", projectBE.TeamGradeTxt);
                sqlParameters[103] = new SqlParameter("@WKR_ID_TXT", projectBE.UserId);
                //jcl 8-11-21 LES-3431
                sqlParameters[104] = new SqlParameter("@PRELIM_PROJECT_SUMMARY_OBJ_DETAILS_TXT", projectBE.PrelimProjectSummaryObjDetails);
                sqlParameters[105] = new SqlParameter("@PRELIM_GEN_INFO_OBJ_DETAILS_TXT", projectBE.PrelimGeneralInfoObjDetails);
                sqlParameters[106] = new SqlParameter("@PRELIM_MEETING_AGENDA_OBJ_DETAILS_TXT", projectBE.PrelimMeetingAgendaObjDetails);
                sqlParameters[107] = new SqlParameter("@PRELIM_PROPOSED_WORK_OBJ_DETAILS_TXT", projectBE.PrelimProposedWorkObjDetails);
                sqlParameters[108] = new SqlParameter("@PRELIM_BIM_PROJECT_DELIVERY_OBJ_DETAILS_TXT", projectBE.PrelimBIMProjectDeliveryObjDetails);
                sqlParameters[109] = new SqlParameter("@PRELIM_WORK_TYP_OBJ_DETAILS_TXT", projectBE.PrelimTypeOfWorkObjDetails);
                sqlParameters[110] = new SqlParameter("@PRELIM_MEETING_DETAIL_OBJ_DETAILS_TXT", projectBE.PrelimMeetingDetailObjDetails);
                sqlParameters[111] = new SqlParameter("@PRELIM_SYSTEM_INFO_OBJ_DETAILS_TXT", projectBE.PrelimSystemInfoObjDetails);

                sqlParameters[112] = new SqlParameter("@CANCELLATION_FEE_AMT", projectBE.CancellationFee);
                sqlParameters[113] = new SqlParameter("@PAID_STATUS_IND", projectBE.IsPaidStatus);
                sqlParameters[114] = new SqlParameter("@ESTIMATED_FEE_DESC", projectBE.EstimatedFee);

                //LES-3047 RTAP fields
                sqlParameters[115] = new SqlParameter("@RTAP_AFFORDABLE_UNIT_CHG_DESC", projectBE.RTAPAffordableUnitChange);
                sqlParameters[116] = new SqlParameter("@RTAP_AFFORDABLE_UNITS_REMOVE_DESC", projectBE.RTAPAffordableUnitsRemove);
                sqlParameters[117] = new SqlParameter("@RTAP_AFFORDABLE_WORKFORCE_UNITS_ADD_DESC", projectBE.RTAPAffordableWorkforceUnitsAdd);
                sqlParameters[118] = new SqlParameter("@RTAP_WORKFORCE_ADD_DESC", projectBE.RTAPWorkforceAdd);
                sqlParameters[119] = new SqlParameter("@RTAP_WORKFORCE_REMOVE_DESC", projectBE.RTAPWorkforceRemove);
                sqlParameters[120] = new SqlParameter("@PROFESSIONALS_TXT", projectBE.Professionals);
                sqlParameters[121] = new SqlParameter("@ACCT_NUM", projectBE.AccountNumber);
                sqlParameters[122] = new SqlParameter("@EQUIP_COST_DESC", projectBE.EquipmentCost);
                sqlParameters[123] = new SqlParameter("@PREPAID_FEE_PAYMENT_TYP_DESC", projectBE.PrepaidFeePaymentType);
                sqlParameters[124] = new SqlParameter("@FIFO_DUE_ACCELA_DT", projectBE.FifoDueAccelaDt);

                sqlParameters[125] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[125].Direction = ParameterDirection.Output;

                id = SqlWrapper.RunSPReturnInteger("usp_insert_aion_project_v4", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return id;
        }

        public int Delete(int id)
        {
            int rows;
            _id = id;

            if (!this.Validate(ActionType.Delete))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[2];

                sqlParameters[0] = new SqlParameter("identity", id);

                sqlParameters[1] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[1].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_delete_aion_project", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;

        }

        public ProjectBE GetById(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            ProjectBE projectBE = new ProjectBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_project_get_by_id_v3", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                projectBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
            }

            return projectBE;
        }

        /// <summary>
        /// Get the Project from the AION table by the Accela Project ID
        /// </summary>
        /// <param name="externalRefInfo"></param>
        /// <returns></returns>
        public ProjectBE GetByExternalRefInfo(string externalRefInfo)
        {
            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            ProjectBE projectBE = new ProjectBE();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@externalRefInfo", externalRefInfo);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_project_get_by_externalRefInfo_v3", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (new Exception("Error: connection string " + base.ConnectionString + ", externalRefInfo: " + externalRefInfo, ex));
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                projectBE = this.ConvertDataRowToBE(dataSet.Tables[0].Rows[0]);
            }

            return projectBE;
        }


        public List<ProjectBE> GetListByExternalRefInfo(string externalRefInfo)
        {
            if (!this.Validate(ActionType.GetById))
                throw (new Exception(_errorMsg));

            List<ProjectBE> projectBEs = new List<ProjectBE>();
            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@externalRefInfo", externalRefInfo);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_project_get_by_externalRefInfo", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (new Exception("Error: connection string " + base.ConnectionString + ", externalRefInfo: " + externalRefInfo, ex));
            }

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                projectBEs.Add(this.ConvertExternalProjectDataRowToBE(dataSet.Tables[0].Rows[0]));
            }

            return projectBEs;
        }

        public DataSet GetDataSet(int id)
        {
            _id = id;

            if (!this.Validate(ActionType.GetDataSet))
                throw (new Exception(_errorMsg));

            DataSet dataSet;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@identity", id);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_project_get_list", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return dataSet;
        }

        public int GetAssignedFacilitator()
        {
            ProjectBE projectBE = new ProjectBE();
            int assignedFacilitator = -1;
            DataSet dataSet;

            try
            {

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_assigned_facilitator", base.ConnectionString);
                if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0 && dataSet.Tables[0].Rows[0]["USER_ID"].ToString() != "")
                    assignedFacilitator = Convert.ToInt32(dataSet.Tables[0].Rows[0]["USER_ID"]);
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return assignedFacilitator;
        }
        /// <summary>
        /// Get project list by Project Manager ID
        /// </summary>
        /// <param name="projectManagerId"></param>
        /// <returns></returns>
        public List<ProjectBE> GetList(int projectManagerId)
        {

            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<ProjectBE> projectBEList = new List<ProjectBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("@identity", projectManagerId);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_project_get_list", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    projectBEList.Add(this.ConvertDataRowToDashboardBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return projectBEList;

        }

        public List<ProjectBE> GetListByStatusIds(string prjStatusEnums)
        {
            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<ProjectBE> projectBEList = new List<ProjectBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("@prjStatusEnums", prjStatusEnums);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_project_get_list_by_status_ids", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    projectBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return projectBEList;

        }

        public List<ProjectBE> GetListByProjectIds(string projectIds)
        {
            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<ProjectBE> projectBEList = new List<ProjectBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];
                sqlParameters[0] = new SqlParameter("@projectIds", projectIds);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_project_get_list_byprojectids", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    projectBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return projectBEList;

        }

        public List<ProjectBE> GetListByFIFODueDate()
        {

            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<ProjectBE> projectBEList = new List<ProjectBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_project_get_list_by_fifo_due_dt", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    projectBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return projectBEList;
        }

        public int Update(ProjectBE projectBE)
        {
            int rows;
            _projectBE = projectBE;

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[128];

                sqlParameters[0] = new SqlParameter("@PROJECT_ID", projectBE.ProjectId);
                sqlParameters[1] = new SqlParameter("@PROJECT_NM", projectBE.ProjectNm);
                sqlParameters[2] = new SqlParameter("@EXTERNAL_SYSTEM_REF_ID", projectBE.ExternalSystemRefId);
                sqlParameters[3] = new SqlParameter("@UPDATED_DTTM", projectBE.UpdatedDate);
                sqlParameters[4] = new SqlParameter("@PROJECT_STATUS_REF_ID", projectBE.ProjectStatusRefId);
                sqlParameters[5] = new SqlParameter("@PROJECT_TYP_REF_ID", projectBE.ProjectTypRefId);
                sqlParameters[6] = new SqlParameter("@SRC_SYSTEM_VAL_TXT", projectBE.SrcSystemValTxt);
                sqlParameters[7] = new SqlParameter("@TAG_CREATED_ID_NUM", projectBE.TagCreatedIdNum);
                sqlParameters[8] = new SqlParameter("@TAG_CREATED_BY_TS", projectBE.TagCreatedByTs);
                sqlParameters[9] = new SqlParameter("@TAG_UPDATED_BY_TS", projectBE.TagUpdatedByTs);
                sqlParameters[10] = new SqlParameter("@TAG_UPDATED_BY_ID_NUM", projectBE.TagUpdatedByIdNum);
                sqlParameters[11] = new SqlParameter("@ASSIGNED_ESTIMATOR_ID", projectBE.AssignedEstimatorId);
                sqlParameters[12] = new SqlParameter("@ASSIGNED_FACILITATOR_ID", projectBE.AssignedFacilitatorId);
                sqlParameters[13] = new SqlParameter("@PROJECT_MODE_REF_ID", projectBE.ProjectModeRefId);
                sqlParameters[14] = new SqlParameter("@WORKFLOW_STATUS_REF_ID", projectBE.WorkflowStatusRefId);
                sqlParameters[15] = new SqlParameter("@RTAP_IND", projectBE.RtapInd);
                sqlParameters[16] = new SqlParameter("@PRELIMINARY_IND", projectBE.PreliminaryInd);
                sqlParameters[17] = new SqlParameter("@PROJECT_LVL_TXT", projectBE.ProjectLvlTxt);
                sqlParameters[18] = new SqlParameter("@GATE_DT", projectBE.GateDt);
                sqlParameters[19] = new SqlParameter("@PROJECT_ADDR_TXT", projectBE.ProjectAddrTxt);
                sqlParameters[20] = new SqlParameter("@PROJECT_MANAGER_ID", projectBE.ProjectManagerId);
                sqlParameters[21] = new SqlParameter("@BUILD_CONTR_NM", projectBE.BuildContrNm);
                sqlParameters[22] = new SqlParameter("@BUILD_CONTR_ACCT_NUM", projectBE.BuildContrAcctNum);
                sqlParameters[23] = new SqlParameter("@GATE_ACCEPTED_IND", projectBE.GateAcceptedInd);
                sqlParameters[24] = new SqlParameter("@FIFO_DUE_DT", projectBE.FifoDueDt);
                sqlParameters[25] = new SqlParameter("@PLANS_READY_ON_DT", projectBE.PlansReadyOnDt);
                sqlParameters[26] = new SqlParameter("@CYCLE_NBR", projectBE.CycleNbr);
                sqlParameters[27] = new SqlParameter("@PRELIM_MEETING_COMPLETE_IND", projectBE.PrelimMeetingCompleteInd);
                sqlParameters[28] = new SqlParameter("@ACCELA_RTAP_PROJECT_REF_ID", projectBE.AccelaRtapProjectRefId);
                sqlParameters[29] = new SqlParameter("@ACCELA_PRELIM_PROJECT_REF_ID", projectBE.AccelaPrelimProjectRefId);
                sqlParameters[30] = new SqlParameter("@PROJECT_OCCUPANCY_TYP_MAP_NM", projectBE.ProjectOccupancyTypMapNm);
                sqlParameters[31] = new SqlParameter("@CONSTR_TYP_DESC", projectBE.ConstrTypDesc);
                sqlParameters[32] = new SqlParameter("@CONSTR_COST_AMT", projectBE.ConstrCostAmt);
                sqlParameters[33] = new SqlParameter("@SHEETS_CNT_DESC", projectBE.SheetsCntDesc);
                sqlParameters[34] = new SqlParameter("@SQUARE_FOOTAGE_TO_BE_REVIEWED_NBR", projectBE.SquareFootageToBeReviewedNbr);
                sqlParameters[35] = new SqlParameter("@SQUARE_FOOTAGE_OF_OVERALL_BUILD_NBR", projectBE.SquareFootageOfOverallBuildNbr);
                sqlParameters[36] = new SqlParameter("@STORIES_CNT", projectBE.StoriesCnt);
                sqlParameters[37] = new SqlParameter("@HIGH_RISE_IND", projectBE.HighRiseInd);
                sqlParameters[38] = new SqlParameter("@EXPRESS_IND", projectBE.ExpressInd);
                sqlParameters[39] = new SqlParameter("@REVIEW_TYP_REF_DESC", projectBE.ReviewTypRefDesc);
                sqlParameters[40] = new SqlParameter("@PRELIM_MEETING_CANCELLED_IND", projectBE.PrelimMeetingCancelledInd);
                sqlParameters[41] = new SqlParameter("@FIFO_IND", projectBE.FifoInd);
                sqlParameters[42] = new SqlParameter("@TOTAL_JOB_COST_AMT", projectBE.TotalJobCostAmt);
                sqlParameters[43] = new SqlParameter("@WORK_TYP_DESC", projectBE.WorkTypDesc);
                sqlParameters[44] = new SqlParameter("@OCCUPANCY_DESC", projectBE.OccupancyDesc);
                sqlParameters[45] = new SqlParameter("@PRI_OCCUPANCY_DESC", projectBE.PriOccupancyDesc);
                sqlParameters[46] = new SqlParameter("@SECONDARY_OCCUPANCY_DESC", projectBE.SecondaryOccupancyDesc);
                sqlParameters[47] = new SqlParameter("@SEAL_HOLDERS_DESC", projectBE.SealHoldersDesc);
                sqlParameters[48] = new SqlParameter("@DESIGNER_DESC", projectBE.DesignerDesc);
                sqlParameters[49] = new SqlParameter("@FIRE_DETAIL_DESC", projectBE.FireDetailDesc);
                sqlParameters[50] = new SqlParameter("@OVERALL_WORK_SCOPE_DESC", projectBE.OverallWorkScopeDesc);
                sqlParameters[51] = new SqlParameter("@MECH_WORK_SCOPE_DESC", projectBE.MechWorkScopeDesc);
                sqlParameters[52] = new SqlParameter("@ELCTR_WORK_SCOPE_DESC", projectBE.ElctrWorkScopeDesc);
                sqlParameters[53] = new SqlParameter("@PLUMB_WORK_SCOPE_DESC", projectBE.PlumbWorkScopeDesc);
                sqlParameters[54] = new SqlParameter("@CIVIL_WORK_SCOPE_DESC", projectBE.CivilWorkScopeDesc);
                sqlParameters[55] = new SqlParameter("@ZONING_OF_SITE_DESC", projectBE.ZoningOfSiteDesc);
                sqlParameters[56] = new SqlParameter("@CHG_OF_USE_DESC", projectBE.ChgOfUseDesc);
                sqlParameters[57] = new SqlParameter("@CONDITIONAL_PERMIT_APPROVAL_DESC", projectBE.ConditionalPermitApprovalDesc);
                sqlParameters[58] = new SqlParameter("@PREVIOUS_BUSINESS_TYP_DESC", projectBE.PreviousBusinessTypDesc);
                sqlParameters[59] = new SqlParameter("@CITY_OF_CHARLOTTE_DESC", projectBE.CityOfCharlotteDesc);
                sqlParameters[60] = new SqlParameter("@PROPOSED_BUSINESS_TYP_DESC", projectBE.ProposedBusinessTypDesc);
                sqlParameters[61] = new SqlParameter("@CODE_SUMMARY_DESC", projectBE.CodeSummaryDesc);
                sqlParameters[62] = new SqlParameter("@BACKFLOW_APPLICATION_DETAIL_DESC", projectBE.BackflowApplicationDetailDesc);
                sqlParameters[63] = new SqlParameter("@WATER_SEWER_DETAIL_DESC", projectBE.WaterSewerDetailDesc);
                sqlParameters[64] = new SqlParameter("@HEALTH_DEPT_DETAIL_DESC", projectBE.HealthDeptDetailDesc);
                sqlParameters[65] = new SqlParameter("@DAY_CARE_DESC", projectBE.DayCareDesc);
                sqlParameters[66] = new SqlParameter("@PROPOSED_OUTDOOR_UNDERGROUND_PIPING_DESC", projectBE.ProposedOutdoorUndergroundPipingDesc);
                sqlParameters[67] = new SqlParameter("@PROPOSED_FIRE_SPRINKLER_PIPING_DESC", projectBE.ProposedFireSprinklerPipingDesc);
                sqlParameters[68] = new SqlParameter("@INSTALL_CMUD_BACKFLOW_PREVENTER_DESC", projectBE.InstallCmudBackflowPreventerDesc);
                sqlParameters[69] = new SqlParameter("@EXTENDING_PUBLIC_WATER_SEWER_DESC", projectBE.ExtendingPublicWaterSewerDesc);
                sqlParameters[70] = new SqlParameter("@GRADE_MOD_WATER_SEWER_EASEMENT_DESC", projectBE.GradeModWaterSewerEasementDesc);
                sqlParameters[71] = new SqlParameter("@PROPOSED_ENCROACHMENT_WATER_SEWER_EASEMENT_DESC", projectBE.ProposedEncroachmentWaterSewerEasementDesc);
                sqlParameters[72] = new SqlParameter("@PARCEL_NUM", projectBE.ParcelNum);
                sqlParameters[73] = new SqlParameter("@AFFORDABLE_HOUSING_DESC", projectBE.AffordableHousingDesc);
                sqlParameters[74] = new SqlParameter("@EXACT_ADDR_TXT", projectBE.ExactAddrTxt);
                sqlParameters[75] = new SqlParameter("@DELIVERY_MTHD_DESC", projectBE.DeliveryMthdDesc);
                sqlParameters[76] = new SqlParameter("@BIM_DESC", projectBE.BimDesc);
                sqlParameters[77] = new SqlParameter("@BIM_DESIGN_DISCIPLINE_DESC", projectBE.BimDesignDisciplineDesc);
                sqlParameters[78] = new SqlParameter("@ATTENDEES_CNT_DESC", projectBE.AttendeesCntDesc);
                sqlParameters[79] = new SqlParameter("@PREVIOUS_PRELIM_REVIEW_DESC", projectBE.PreviousPrelimReviewDesc);
                sqlParameters[80] = new SqlParameter("@PROJECT_NUM_PREVIOUS_PRELIM_REVIEW_DESC", projectBE.ProjectNumPreviousPrelimReviewDesc);
                sqlParameters[81] = new SqlParameter("@SAME_REVIEW_TEAM_DESC", projectBE.SameReviewTeamDesc);
                sqlParameters[82] = new SqlParameter("@PROPERTY_OWNER_NM", projectBE.PropertyOwnerNm);
                sqlParameters[83] = new SqlParameter("@PROPERTY_OWNER_ADDR_TXT", projectBE.PropertyOwnerAddrTxt);
                sqlParameters[84] = new SqlParameter("@PROPERTY_OWNER_EMAIL_ADDR_TXT", projectBE.PropertyOwnerEmailAddrTxt);
                sqlParameters[85] = new SqlParameter("@PROPERTY_OWNER_PHONE_NUM", projectBE.PropertyOwnerPhoneNum);
                sqlParameters[86] = new SqlParameter("@PROPERTY_OWNER_AUTO_EMAIL_ADDR_TXT", projectBE.PropertyOwnerAutoEmailAddrTxt);
                sqlParameters[87] = new SqlParameter("@PROPERTY_MANAGER_NM", projectBE.PropertyManagerNm);
                sqlParameters[88] = new SqlParameter("@PROPERTY_MANAGER_EMAIL_ADDR_TXT", projectBE.PropertyManagerEmailAddrTxt);
                sqlParameters[89] = new SqlParameter("@PROPERTY_MANAGER_EMAIL_ADDR_2_TXT", projectBE.PropertyManagerEmailAddr2Txt);
                sqlParameters[90] = new SqlParameter("@ARCHITECT_DESIGNER_CNTCT_NM", projectBE.ArchitectDesignerCntctNm);
                sqlParameters[91] = new SqlParameter("@ARCHITECT_DESIGNER_CNTCT_PHONE_NUM", projectBE.ArchitectDesignerCntctPhoneNum);
                sqlParameters[92] = new SqlParameter("@ARCHITECT_DESIGNER_CNTCT_EMAIL_ADDR_TXT", projectBE.ArchitectDesignerCntctEmailAddrTxt);
                sqlParameters[93] = new SqlParameter("@ARCHITECT_DESIGNER_AUTO_EMAIL_ADDR_TXT", projectBE.ArchitectDesignerAutoEmailAddrTxt);
                sqlParameters[94] = new SqlParameter("@ARCHITECT_DRAWINGS_SEALED_DESC", projectBE.ArchitectDrawingsSealedDesc);
                sqlParameters[95] = new SqlParameter("@ARCHITECT_DESIGNER_LICENSE_NUM", projectBE.ArchitectDesignerLicenseNum);
                sqlParameters[96] = new SqlParameter("@ARCHITECT_DESIGNER_LICENSE_BOARD_DESC", projectBE.ArchitectDesignerLicenseBoardDesc);
                sqlParameters[97] = new SqlParameter("@ARCHITECT_DESIGNER_EMPLOYEE_DESC", projectBE.ArchitectDesignerEmployeeDesc);
                sqlParameters[98] = new SqlParameter("@PERMIT_NUM", projectBE.PermitNum);
                sqlParameters[99] = new SqlParameter("@TOTAL_FEE_AMT", projectBE.TotalFeeAmt);
                sqlParameters[100] = new SqlParameter("@BUILD_CODE_VERSION_DESC", projectBE.BuildCodeVersionDesc);
                sqlParameters[101] = new SqlParameter("@SQUARE_FOOTAGE_DESC", projectBE.SquareFootageDesc);
                sqlParameters[102] = new SqlParameter("@PROPERTY_MANAGER_PHONE_NUM", projectBE.PropertyManagerPhoneNum);
                sqlParameters[103] = new SqlParameter("@REC_ID_TXT", projectBE.RecIdTxt);
                sqlParameters[104] = new SqlParameter("@TEAM_GRADE_TXT", projectBE.TeamGradeTxt);
                sqlParameters[105] = new SqlParameter("@WKR_ID_TXT", projectBE.UserId);
                //jcl 8-11-21 LES-3431
                sqlParameters[106] = new SqlParameter("@PRELIM_PROJECT_SUMMARY_OBJ_DETAILS_TXT", projectBE.PrelimProjectSummaryObjDetails);
                sqlParameters[107] = new SqlParameter("@PRELIM_GEN_INFO_OBJ_DETAILS_TXT", projectBE.PrelimGeneralInfoObjDetails);
                sqlParameters[108] = new SqlParameter("@PRELIM_MEETING_AGENDA_OBJ_DETAILS_TXT", projectBE.PrelimMeetingAgendaObjDetails);
                sqlParameters[109] = new SqlParameter("@PRELIM_PROPOSED_WORK_OBJ_DETAILS_TXT", projectBE.PrelimProposedWorkObjDetails);
                sqlParameters[110] = new SqlParameter("@PRELIM_BIM_PROJECT_DELIVERY_OBJ_DETAILS_TXT", projectBE.PrelimBIMProjectDeliveryObjDetails);
                sqlParameters[111] = new SqlParameter("@PRELIM_WORK_TYP_OBJ_DETAILS_TXT", projectBE.PrelimTypeOfWorkObjDetails);
                sqlParameters[112] = new SqlParameter("@PRELIM_MEETING_DETAIL_OBJ_DETAILS_TXT", projectBE.PrelimMeetingDetailObjDetails);
                sqlParameters[113] = new SqlParameter("@PRELIM_SYSTEM_INFO_OBJ_DETAILS_TXT", projectBE.PrelimSystemInfoObjDetails);

                sqlParameters[114] = new SqlParameter("@CANCELLATION_FEE_AMT", projectBE.CancellationFee);
                sqlParameters[115] = new SqlParameter("@PAID_STATUS_IND", projectBE.IsPaidStatus);
                sqlParameters[116] = new SqlParameter("@ESTIMATED_FEE_DESC", projectBE.EstimatedFee);

                //LES-3047 RTAP fields
                sqlParameters[117] = new SqlParameter("@RTAP_AFFORDABLE_UNIT_CHG_DESC", projectBE.RTAPAffordableUnitChange);
                sqlParameters[118] = new SqlParameter("@RTAP_AFFORDABLE_UNITS_REMOVE_DESC", projectBE.RTAPAffordableUnitsRemove);
                sqlParameters[119] = new SqlParameter("@RTAP_AFFORDABLE_WORKFORCE_UNITS_ADD_DESC", projectBE.RTAPAffordableWorkforceUnitsAdd);
                sqlParameters[120] = new SqlParameter("@RTAP_WORKFORCE_ADD_DESC", projectBE.RTAPWorkforceAdd);
                sqlParameters[121] = new SqlParameter("@RTAP_WORKFORCE_REMOVE_DESC", projectBE.RTAPWorkforceRemove);
                sqlParameters[122] = new SqlParameter("@PROFESSIONALS_TXT", projectBE.Professionals);
                sqlParameters[123] = new SqlParameter("@ACCT_NUM", projectBE.AccountNumber);
                sqlParameters[124] = new SqlParameter("@EQUIP_COST_DESC", projectBE.EquipmentCost);
                sqlParameters[125] = new SqlParameter("@PREPAID_FEE_PAYMENT_TYP_DESC", projectBE.PrepaidFeePaymentType);
                sqlParameters[126] = new SqlParameter("@FIFO_DUE_ACCELA_DT", projectBE.FifoDueAccelaDt);

                sqlParameters[127] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[127].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_project_v4", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;
        }

        public List<ProjectSearchResultBE> Search(DateTime? startDate, DateTime? endDate, string projectNumber, string projectName, string projectAddress,
            string customerName, string planReviewer, int? projectStatus = null, int? estimatorId = null, int? facilitatorId = null, int? meetingType = null)
        {
            DataSet dataSet;
            List<ProjectSearchResultBE> projectSearchResultBEList = new List<ProjectSearchResultBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[11];

                if (startDate == null || startDate == DateTime.MinValue)
                {
                    sqlParameters[0] = new SqlParameter("@START_DATE", DBNull.Value);
                }
                else
                {
                    sqlParameters[0] = new SqlParameter("@START_DATE", ((DateTime)startDate).ToString("yyyy-MM-dd"));
                }

                if (endDate == null || endDate == DateTime.MinValue)
                {
                    sqlParameters[1] = new SqlParameter("@END_DATE", DBNull.Value);
                }
                else
                {
                    sqlParameters[1] = new SqlParameter("@END_DATE", ((DateTime)endDate).ToString("yyyy-MM-dd"));
                }

                sqlParameters[2] = new SqlParameter("@PROJECT_NUMBER", projectNumber);
                sqlParameters[3] = new SqlParameter("@PROJECT_NAME", projectName);
                sqlParameters[4] = new SqlParameter("@PROJECT_ADDRESS", projectAddress);
                sqlParameters[5] = new SqlParameter("@CUSTOMER_NAME", customerName);
                sqlParameters[6] = new SqlParameter("@PLAN_REVIEWER", planReviewer);
                sqlParameters[7] = new SqlParameter("@PROJECT_STATUS", projectStatus);
                sqlParameters[8] = new SqlParameter("@ESTIMATOR_ID", estimatorId);
                sqlParameters[9] = new SqlParameter("@FACILITATOR_ID", facilitatorId);
                sqlParameters[10] = new SqlParameter("@MEETING_TYPE", meetingType);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_project_search", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    projectSearchResultBEList.Add(this.ConvertDataRowToProjectSearchResultBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return projectSearchResultBEList;
        }

        public int Cancel(int id, int user)
        {
            int rows;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[3];

                sqlParameters[0] = new SqlParameter("@PROJECT_ID", id);
                sqlParameters[1] = new SqlParameter("@USER_ID", user);
                sqlParameters[2] = new SqlParameter("@RETURN_VALUE", 0);
                sqlParameters[2].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_cancel_project_by_id", base.ConnectionString, ref sqlParameters);
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;
        }

        public int CancelEntitiesNotInJsonObject(string excludedProjectIds, string excludedMeetingIds)
        {
            int rows;

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[1];

                sqlParameters[0] = new SqlParameter("@excludedProjectIds", excludedProjectIds);

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_cancel_entities_not_in_json_object", base.ConnectionString, ref sqlParameters);
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;
        }

        public List<ProjectBE> GetProjectListByBuildContr(string buildContrName, string buildContrAcctNo, DateTime createdDate)
        {


            if (!this.Validate(ActionType.GetList))
                throw (new Exception(_errorMsg));

            DataSet dataSet;
            List<ProjectBE> projectBEList = new List<ProjectBE>();

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[3];

                sqlParameters[0] = new SqlParameter("@BUILD_CONTR_NM", buildContrName);
                sqlParameters[1] = new SqlParameter("@BUILD_CONTR_ACCT_NUM", buildContrAcctNo);
                sqlParameters[2] = new SqlParameter("@TAG_CREATED_BY_TS", createdDate.Date);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_project_get_list_by_BuildContr", base.ConnectionString, ref sqlParameters);

                foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                {
                    projectBEList.Add(this.ConvertDataRowToBE(dataRow));
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return projectBEList;

        }

        /// <summary>
        /// This updates the project status by the project id
        /// Required: ProjectID, WkrId, Status Ref id, current updatedDate
        /// </summary>
        /// <param name="projectBE"></param>
        /// <returns></returns>
        public int UpdateProjectStatus(ProjectBE projectBE)
        {
            int rows;
            _projectBE = projectBE;

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[5];

                sqlParameters[0] = new SqlParameter("@PROJECT_ID", projectBE.ProjectId);
                sqlParameters[1] = new SqlParameter("@UPDATED_DTTM", projectBE.UpdatedDate);
                sqlParameters[2] = new SqlParameter("@PROJECT_STATUS_REF_ID", projectBE.ProjectStatusRefId);
                sqlParameters[3] = new SqlParameter("@WKR_ID_TXT", projectBE.UserId);

                sqlParameters[4] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[4].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_project_status", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;
        }

        /// <summary>
        /// This updates the facilitator by project id
        /// Required: ProjectID, UserId(WkrId), FacilitatorId, current UpdatedDate
        /// </summary>
        /// <param name="projectBE"></param>
        /// <returns></returns>
        public int UpdateProjectFacilitator(ProjectBE projectBE)
        {
            int rows;
            _projectBE = projectBE;

            if (!this.Validate(ActionType.Update))
                throw (new Exception(_errorMsg));

            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[5];

                sqlParameters[0] = new SqlParameter("@PROJECT_ID", projectBE.ProjectId);
                sqlParameters[1] = new SqlParameter("@UPDATED_DTTM", projectBE.UpdatedDate);
                sqlParameters[2] = new SqlParameter("@ASSIGNED_FACILITATOR_ID", projectBE.AssignedFacilitatorId);
                sqlParameters[3] = new SqlParameter("@WKR_ID_TXT", projectBE.UserId);

                sqlParameters[4] = new SqlParameter("@ReturnValue", 0);
                sqlParameters[4].Direction = ParameterDirection.Output;

                rows = SqlWrapper.RunSPReturnInteger("usp_update_aion_project_facilitator", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return rows;
        }

        public List<ProjectBE> GetMMFProjectsComplete(DateTime startdate, DateTime enddate)
        {
            List<ProjectBE> list = new List<ProjectBE>();
            DataSet dataSet;
            try
            {
                SqlParameter[] sqlParameters = new SqlParameter[2];

                sqlParameters[0] = new SqlParameter("@startdate", startdate);
                sqlParameters[1] = new SqlParameter("@enddate", enddate);

                dataSet = SqlWrapper.RunSPReturnDS("usp_select_aion_get_projects_mmf_complete", base.ConnectionString, ref sqlParameters);

            }
            catch (Exception ex)
            {
                throw (ex);
            }
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                list.Add(this.ConvertExternalProjectDataRowToBE(dataSet.Tables[0].Rows[0]));
            }

            return list;
        }

        #endregion

        #region Private Methods

        private bool Validate(ActionType actionType)
        {
            // TODO: Add validation rules as necessary.

            _errorMsg = String.Empty;

            switch (actionType)
            {
                case ActionType.Create:
                    return (_errorMsg == String.Empty);

                case ActionType.Delete:
                    return (_errorMsg == String.Empty);

                case ActionType.GetById:
                    return (_errorMsg == String.Empty);

                case ActionType.GetDataSet:
                    return (_errorMsg == String.Empty);

                case ActionType.GetList:
                    return (_errorMsg == String.Empty);

                case ActionType.Update:
                    return (_errorMsg == String.Empty);

                default:
                    break;
            }

            return true;

        }

        private ProjectBE ConvertDataRowToBE(DataRow dataRow)
        {
            ProjectBE projectBE = new ProjectBE();

            projectBE.ProjectId = TryToParse<int?>(dataRow["PROJECT_ID"]);
            projectBE.ProjectNm = TryToParse<string>(dataRow["PROJECT_NM"]);
            projectBE.ExternalSystemRefId = TryToParse<int?>(dataRow["EXTERNAL_SYSTEM_REF_ID"]);
            projectBE.CreatedByWkrId = TryToParse<string>(dataRow["WKR_ID_CREATED_TXT"]);
            projectBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            projectBE.UpdatedByWkrId = TryToParse<string>(dataRow["WKR_ID_UPDATED_TXT"]);
            projectBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);
            projectBE.ProjectStatusRefId = TryToParse<int?>(dataRow["PROJECT_STATUS_REF_ID"]);
            projectBE.ProjectTypRefId = TryToParse<int?>(dataRow["PROJECT_TYP_REF_ID"]);
            projectBE.SrcSystemValTxt = TryToParse<string>(dataRow["SRC_SYSTEM_VAL_TXT"]);
            projectBE.TagCreatedIdNum = TryToParse<string>(dataRow["TAG_CREATED_ID_NUM"]);
            projectBE.TagCreatedByTs = TryToParse<DateTime?>(dataRow["TAG_CREATED_BY_TS"]);
            projectBE.TagUpdatedByTs = TryToParse<DateTime?>(dataRow["TAG_UPDATED_BY_TS"]);
            projectBE.TagUpdatedByIdNum = TryToParse<string>(dataRow["TAG_UPDATED_BY_ID_NUM"]);
            projectBE.AssignedEstimatorId = TryToParse<int?>(dataRow["ASSIGNED_ESTIMATOR_ID"]);
            projectBE.AssignedFacilitatorId = TryToParse<int?>(dataRow["ASSIGNED_FACILITATOR_ID"]);
            projectBE.ProjectModeRefId = TryToParse<int?>(dataRow["PROJECT_MODE_REF_ID"]);
            projectBE.WorkflowStatusRefId = TryToParse<int?>(dataRow["WORKFLOW_STATUS_REF_ID"]);
            projectBE.RtapInd = TryToParse<bool?>(dataRow["RTAP_IND"]);
            projectBE.PreliminaryInd = TryToParse<bool?>(dataRow["PRELIMINARY_IND"]);
            projectBE.ProjectLvlTxt = TryToParse<string>(dataRow["PROJECT_LVL_TXT"]);
            projectBE.GateDt = TryToParse<DateTime?>(dataRow["GATE_DT"]);
            projectBE.ProjectAddrTxt = TryToParse<string>(dataRow["PROJECT_ADDR_TXT"]);
            projectBE.ProjectManagerId = TryToParse<int?>(dataRow["PROJECT_MANAGER_ID"]);
            projectBE.BuildContrNm = TryToParse<string>(dataRow["BUILD_CONTR_NM"]);
            projectBE.BuildContrAcctNum = TryToParse<string>(dataRow["BUILD_CONTR_ACCT_NUM"]);
            projectBE.GateAcceptedInd = TryToParse<bool?>(dataRow["GATE_ACCEPTED_IND"]);
            projectBE.FifoDueDt = TryToParse<DateTime?>(dataRow["FIFO_DUE_DT"]);
            projectBE.PlansReadyOnDt = TryToParse<DateTime?>(dataRow["PLANS_READY_ON_DT"]);
            projectBE.CycleNbr = TryToParse<int?>(dataRow["CYCLE_NBR"]);
            projectBE.PrelimMeetingCompleteInd = TryToParse<bool?>(dataRow["PRELIM_MEETING_COMPLETE_IND"]);
            projectBE.AccelaRtapProjectRefId = TryToParse<string>(dataRow["ACCELA_RTAP_PROJECT_REF_ID"]);
            projectBE.AccelaPrelimProjectRefId = TryToParse<string>(dataRow["ACCELA_PRELIM_PROJECT_REF_ID"]);
            projectBE.ProjectOccupancyTypMapNm = TryToParse<string>(dataRow["PROJECT_OCCUPANCY_TYP_MAP_NM"]);
            projectBE.ConstrTypDesc = TryToParse<string>(dataRow["CONSTR_TYP_DESC"]);
            projectBE.ConstrCostAmt = TryToParse<decimal?>(dataRow["CONSTR_COST_AMT"]);
            projectBE.SheetsCntDesc = TryToParse<string>(dataRow["SHEETS_CNT_DESC"]);
            projectBE.SquareFootageToBeReviewedNbr = TryToParse<int?>(dataRow["SQUARE_FOOTAGE_TO_BE_REVIEWED_NBR"]);
            projectBE.SquareFootageOfOverallBuildNbr = TryToParse<int?>(dataRow["SQUARE_FOOTAGE_OF_OVERALL_BUILD_NBR"]);
            projectBE.StoriesCnt = TryToParse<int?>(dataRow["STORIES_CNT"]);
            projectBE.HighRiseInd = TryToParse<bool?>(dataRow["HIGH_RISE_IND"]);
            projectBE.ExpressInd = TryToParse<bool?>(dataRow["EXPRESS_IND"]);
            projectBE.ReviewTypRefDesc = TryToParse<string>(dataRow["REVIEW_TYP_REF_DESC"]);
            projectBE.PrelimMeetingCancelledInd = TryToParse<bool?>(dataRow["PRELIM_MEETING_CANCELLED_IND"]);
            projectBE.FifoInd = TryToParse<bool?>(dataRow["FIFO_IND"]);
            projectBE.TotalJobCostAmt = TryToParse<decimal?>(dataRow["TOTAL_JOB_COST_AMT"]);
            projectBE.WorkTypDesc = TryToParse<string>(dataRow["WORK_TYP_DESC"]);
            projectBE.OccupancyDesc = TryToParse<string>(dataRow["OCCUPANCY_DESC"]);
            projectBE.PriOccupancyDesc = TryToParse<string>(dataRow["PRI_OCCUPANCY_DESC"]);
            projectBE.SecondaryOccupancyDesc = TryToParse<string>(dataRow["SECONDARY_OCCUPANCY_DESC"]);
            projectBE.SealHoldersDesc = TryToParse<string>(dataRow["SEAL_HOLDERS_DESC"]);
            projectBE.DesignerDesc = TryToParse<string>(dataRow["DESIGNER_DESC"]);
            projectBE.FireDetailDesc = TryToParse<string>(dataRow["FIRE_DETAIL_DESC"]);
            projectBE.OverallWorkScopeDesc = TryToParse<string>(dataRow["OVERALL_WORK_SCOPE_DESC"]);
            projectBE.MechWorkScopeDesc = TryToParse<string>(dataRow["MECH_WORK_SCOPE_DESC"]);
            projectBE.ElctrWorkScopeDesc = TryToParse<string>(dataRow["ELCTR_WORK_SCOPE_DESC"]);
            projectBE.PlumbWorkScopeDesc = TryToParse<string>(dataRow["PLUMB_WORK_SCOPE_DESC"]);
            projectBE.CivilWorkScopeDesc = TryToParse<string>(dataRow["CIVIL_WORK_SCOPE_DESC"]);
            projectBE.ZoningOfSiteDesc = TryToParse<string>(dataRow["ZONING_OF_SITE_DESC"]);
            projectBE.ChgOfUseDesc = TryToParse<string>(dataRow["CHG_OF_USE_DESC"]);
            projectBE.ConditionalPermitApprovalDesc = TryToParse<string>(dataRow["CONDITIONAL_PERMIT_APPROVAL_DESC"]);
            projectBE.PreviousBusinessTypDesc = TryToParse<string>(dataRow["PREVIOUS_BUSINESS_TYP_DESC"]);
            projectBE.CityOfCharlotteDesc = TryToParse<string>(dataRow["CITY_OF_CHARLOTTE_DESC"]);
            projectBE.ProposedBusinessTypDesc = TryToParse<string>(dataRow["PROPOSED_BUSINESS_TYP_DESC"]);
            projectBE.CodeSummaryDesc = TryToParse<string>(dataRow["CODE_SUMMARY_DESC"]);
            projectBE.BackflowApplicationDetailDesc = TryToParse<string>(dataRow["BACKFLOW_APPLICATION_DETAIL_DESC"]);
            projectBE.WaterSewerDetailDesc = TryToParse<string>(dataRow["WATER_SEWER_DETAIL_DESC"]);
            projectBE.HealthDeptDetailDesc = TryToParse<string>(dataRow["HEALTH_DEPT_DETAIL_DESC"]);
            projectBE.DayCareDesc = TryToParse<string>(dataRow["DAY_CARE_DESC"]);
            projectBE.ProposedOutdoorUndergroundPipingDesc = TryToParse<string>(dataRow["PROPOSED_OUTDOOR_UNDERGROUND_PIPING_DESC"]);
            projectBE.ProposedFireSprinklerPipingDesc = TryToParse<string>(dataRow["PROPOSED_FIRE_SPRINKLER_PIPING_DESC"]);
            projectBE.InstallCmudBackflowPreventerDesc = TryToParse<string>(dataRow["INSTALL_CMUD_BACKFLOW_PREVENTER_DESC"]);
            projectBE.ExtendingPublicWaterSewerDesc = TryToParse<string>(dataRow["EXTENDING_PUBLIC_WATER_SEWER_DESC"]);
            projectBE.GradeModWaterSewerEasementDesc = TryToParse<string>(dataRow["GRADE_MOD_WATER_SEWER_EASEMENT_DESC"]);
            projectBE.ProposedEncroachmentWaterSewerEasementDesc = TryToParse<string>(dataRow["PROPOSED_ENCROACHMENT_WATER_SEWER_EASEMENT_DESC"]);
            projectBE.ParcelNum = TryToParse<string>(dataRow["PARCEL_NUM"]);
            projectBE.AffordableHousingDesc = TryToParse<string>(dataRow["AFFORDABLE_HOUSING_DESC"]);
            projectBE.ExactAddrTxt = TryToParse<string>(dataRow["EXACT_ADDR_TXT"]);
            projectBE.DeliveryMthdDesc = TryToParse<string>(dataRow["DELIVERY_MTHD_DESC"]);
            projectBE.BimDesc = TryToParse<string>(dataRow["BIM_DESC"]);
            projectBE.BimDesignDisciplineDesc = TryToParse<string>(dataRow["BIM_DESIGN_DISCIPLINE_DESC"]);
            projectBE.AttendeesCntDesc = TryToParse<string>(dataRow["ATTENDEES_CNT_DESC"]);
            projectBE.PreviousPrelimReviewDesc = TryToParse<string>(dataRow["PREVIOUS_PRELIM_REVIEW_DESC"]);
            projectBE.ProjectNumPreviousPrelimReviewDesc = TryToParse<string>(dataRow["PROJECT_NUM_PREVIOUS_PRELIM_REVIEW_DESC"]);
            projectBE.SameReviewTeamDesc = TryToParse<string>(dataRow["SAME_REVIEW_TEAM_DESC"]);
            projectBE.PropertyOwnerNm = TryToParse<string>(dataRow["PROPERTY_OWNER_NM"]);
            projectBE.PropertyOwnerAddrTxt = TryToParse<string>(dataRow["PROPERTY_OWNER_ADDR_TXT"]);
            projectBE.PropertyOwnerEmailAddrTxt = TryToParse<string>(dataRow["PROPERTY_OWNER_EMAIL_ADDR_TXT"]);
            projectBE.PropertyOwnerPhoneNum = TryToParse<string>(dataRow["PROPERTY_OWNER_PHONE_NUM"]);
            projectBE.PropertyOwnerAutoEmailAddrTxt = TryToParse<string>(dataRow["PROPERTY_OWNER_AUTO_EMAIL_ADDR_TXT"]);
            projectBE.PropertyManagerNm = TryToParse<string>(dataRow["PROPERTY_MANAGER_NM"]);
            projectBE.PropertyManagerEmailAddrTxt = TryToParse<string>(dataRow["PROPERTY_MANAGER_EMAIL_ADDR_TXT"]);
            projectBE.PropertyManagerEmailAddr2Txt = TryToParse<string>(dataRow["PROPERTY_MANAGER_EMAIL_ADDR_2_TXT"]);
            projectBE.ArchitectDesignerCntctNm = TryToParse<string>(dataRow["ARCHITECT_DESIGNER_CNTCT_NM"]);
            projectBE.ArchitectDesignerCntctPhoneNum = TryToParse<string>(dataRow["ARCHITECT_DESIGNER_CNTCT_PHONE_NUM"]);
            projectBE.ArchitectDesignerCntctEmailAddrTxt = TryToParse<string>(dataRow["ARCHITECT_DESIGNER_CNTCT_EMAIL_ADDR_TXT"]);
            projectBE.ArchitectDesignerAutoEmailAddrTxt = TryToParse<string>(dataRow["ARCHITECT_DESIGNER_AUTO_EMAIL_ADDR_TXT"]);
            projectBE.ArchitectDrawingsSealedDesc = TryToParse<string>(dataRow["ARCHITECT_DRAWINGS_SEALED_DESC"]);
            projectBE.ArchitectDesignerLicenseNum = TryToParse<string>(dataRow["ARCHITECT_DESIGNER_LICENSE_NUM"]);
            projectBE.ArchitectDesignerLicenseBoardDesc = TryToParse<string>(dataRow["ARCHITECT_DESIGNER_LICENSE_BOARD_DESC"]);
            projectBE.ArchitectDesignerEmployeeDesc = TryToParse<string>(dataRow["ARCHITECT_DESIGNER_EMPLOYEE_DESC"]);
            projectBE.PermitNum = TryToParse<string>(dataRow["PERMIT_NUM"]);
            projectBE.TotalFeeAmt = TryToParse<decimal?>(dataRow["TOTAL_FEE_AMT"]);
            projectBE.BuildCodeVersionDesc = TryToParse<string>(dataRow["BUILD_CODE_VERSION_DESC"]);
            projectBE.SquareFootageDesc = TryToParse<string>(dataRow["SQUARE_FOOTAGE_DESC"]);
            projectBE.PropertyManagerPhoneNum = TryToParse<string>(dataRow["PROPERTY_MANAGER_PHONE_NUM"]);
            projectBE.RecIdTxt = TryToParse<string>(dataRow["REC_ID_TXT"]);
            projectBE.TeamGradeTxt = TryToParse<string>(dataRow["TEAM_GRADE_TXT"]);
            projectBE.ReviewTypRefDesc = TryToParse<string>(dataRow["REVIEW_TYP_REF_DESC"]);
            projectBE.ProjectOccupancyTypMapNm = TryToParse<string>(dataRow["PROJECT_OCCUPANCY_TYP_MAP_NM"]);
            //jcl 8-11-21 LES-3431
            projectBE.PrelimBIMProjectDeliveryObjDetails = TryToParse<string>(dataRow["PRELIM_BIM_PROJECT_DELIVERY_OBJ_DETAILS_TXT"]);
            projectBE.PrelimGeneralInfoObjDetails = TryToParse<string>(dataRow["PRELIM_GEN_INFO_OBJ_DETAILS_TXT"]);
            projectBE.PrelimMeetingAgendaObjDetails = TryToParse<string>(dataRow["PRELIM_MEETING_AGENDA_OBJ_DETAILS_TXT"]);
            projectBE.PrelimMeetingDetailObjDetails = TryToParse<string>(dataRow["PRELIM_MEETING_DETAIL_OBJ_DETAILS_TXT"]);
            projectBE.PrelimProjectSummaryObjDetails = TryToParse<string>(dataRow["PRELIM_PROJECT_SUMMARY_OBJ_DETAILS_TXT"]);
            projectBE.PrelimProposedWorkObjDetails = TryToParse<string>(dataRow["PRELIM_PROPOSED_WORK_OBJ_DETAILS_TXT"]);
            projectBE.PrelimSystemInfoObjDetails = TryToParse<string>(dataRow["PRELIM_SYSTEM_INFO_OBJ_DETAILS_TXT"]);
            projectBE.PrelimTypeOfWorkObjDetails = TryToParse<string>(dataRow["PRELIM_WORK_TYP_OBJ_DETAILS_TXT"]);

            projectBE.CancellationFee = TryToParse<decimal?>(dataRow["CANCELLATION_FEE_AMT"]);
            projectBE.IsPaidStatus = TryToParse<bool?>(dataRow["PAID_STATUS_IND"]);
            projectBE.EstimatedFee = TryToParse<string>(dataRow["ESTIMATED_FEE_DESC"]);

            //LES-3047 RTAP fields
            projectBE.RTAPAffordableUnitChange = TryToParse<string>(dataRow["RTAP_AFFORDABLE_UNIT_CHG_DESC"]);
            projectBE.RTAPAffordableUnitsRemove = TryToParse<string>(dataRow["RTAP_AFFORDABLE_UNITS_REMOVE_DESC"]);
            projectBE.RTAPAffordableWorkforceUnitsAdd = TryToParse<string>(dataRow["RTAP_AFFORDABLE_WORKFORCE_UNITS_ADD_DESC"]);
            projectBE.RTAPWorkforceAdd = TryToParse<string>(dataRow["RTAP_WORKFORCE_ADD_DESC"]);
            projectBE.RTAPWorkforceRemove = TryToParse<string>(dataRow["RTAP_WORKFORCE_REMOVE_DESC"]);
            projectBE.Professionals = TryToParse<string>(dataRow["PROFESSIONALS_TXT"]);
            projectBE.AccountNumber = TryToParse<string>(dataRow["ACCT_NUM"]);
            projectBE.EquipmentCost = TryToParse<string>(dataRow["EQUIP_COST_DESC"]);
            projectBE.PrepaidFeePaymentType = TryToParse<string>(dataRow["PREPAID_FEE_PAYMENT_TYP_DESC"]);

            projectBE.FifoDueAccelaDt = TryToParse<DateTime?>(dataRow["FIFO_DUE_ACCELA_DT"]);


            return projectBE;
        }

        private ProjectBE ConvertDataRowToDashboardBE(DataRow dataRow)
        {
            ProjectBE projectBE = new ProjectBE();

            projectBE.ProjectId = TryToParse<int?>(dataRow["PROJECT_ID"]);
            projectBE.ProjectNm = TryToParse<string>(dataRow["PROJECT_NM"]);
            projectBE.ExternalSystemRefId = TryToParse<int?>(dataRow["EXTERNAL_SYSTEM_REF_ID"]);
            projectBE.ProjectStatusRefId = TryToParse<int?>(dataRow["PROJECT_STATUS_REF_ID"]);
            projectBE.ProjectTypRefId = TryToParse<int?>(dataRow["PROJECT_TYP_REF_ID"]);
            projectBE.SrcSystemValTxt = TryToParse<string>(dataRow["SRC_SYSTEM_VAL_TXT"]);
            projectBE.TagCreatedIdNum = TryToParse<string>(dataRow["TAG_CREATED_ID_NUM"]);
            projectBE.TagCreatedByTs = TryToParse<DateTime?>(dataRow["TAG_CREATED_BY_TS"]);
            projectBE.TagUpdatedByTs = TryToParse<DateTime?>(dataRow["TAG_UPDATED_BY_TS"]);
            projectBE.TagUpdatedByIdNum = TryToParse<string>(dataRow["TAG_UPDATED_BY_ID_NUM"]);
            projectBE.AssignedEstimatorId = TryToParse<int?>(dataRow["ASSIGNED_ESTIMATOR_ID"]);
            projectBE.AssignedFacilitatorId = TryToParse<int?>(dataRow["ASSIGNED_FACILITATOR_ID"]);
            projectBE.ProjectModeRefId = TryToParse<int?>(dataRow["PROJECT_MODE_REF_ID"]);
            projectBE.ProjectLvlTxt = TryToParse<string>(dataRow["PROJECT_LVL_TXT"]);
            projectBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            projectBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);
            projectBE.GateDt = TryToParse<DateTime?>(dataRow["GATE_DT"]);
            projectBE.ProjectAddrTxt = TryToParse<string>(dataRow["PROJECT_ADDR_TXT"]);
            projectBE.ProjectManagerId = TryToParse<int?>(dataRow["PROJECT_MANAGER_ID"]);
            projectBE.RtapInd = TryToParse<bool?>(dataRow["RTAP_IND"]);
            projectBE.PlansReadyOnDt = TryToParse<DateTime?>(dataRow["PLANS_READY_ON_DT"]);

            projectBE.PreliminaryInd = TryToParse<bool?>(dataRow["PRELIMINARY_IND"]);
            projectBE.BuildContrNm = TryToParse<string>(dataRow["BUILD_CONTR_NM"]);
            projectBE.BuildContrAcctNum = TryToParse<string>(dataRow["BUILD_CONTR_ACCT_NUM"]);
            projectBE.GateAcceptedInd = TryToParse<bool?>(dataRow["GATE_ACCEPTED_IND"]);
            projectBE.FifoDueDt = TryToParse<DateTime?>(dataRow["FIFO_DUE_DT"]);

            projectBE.TentativeStartDate = TryToParse<DateTime?>(dataRow["TENTATIVE_STARTDT"]);
            projectBE.RecIdTxt = TryToParse<string>(dataRow["REC_ID_TXT"]);
            projectBE.IsPaidStatus = TryToParse<bool?>(dataRow["PAID_STATUS_IND"]);
            projectBE.PlanReviewCreatedDate = TryToParse<DateTime?>(dataRow["PLAN_REVIEW_CREATED_DTTM"]);
            return projectBE;

        }

        private ProjectBE ConvertExternalProjectDataRowToBE(DataRow dataRow)
        {
            ProjectBE projectBE = new ProjectBE();

            projectBE.ProjectId = TryToParse<int?>(dataRow["PROJECT_ID"]);
            projectBE.ProjectNm = TryToParse<string>(dataRow["PROJECT_NM"]);
            projectBE.ExternalSystemRefId = TryToParse<int?>(dataRow["EXTERNAL_SYSTEM_REF_ID"]);
            projectBE.ProjectStatusRefId = TryToParse<int?>(dataRow["PROJECT_STATUS_REF_ID"]);
            projectBE.ProjectTypRefId = TryToParse<int?>(dataRow["PROJECT_TYP_REF_ID"]);
            projectBE.SrcSystemValTxt = TryToParse<string>(dataRow["SRC_SYSTEM_VAL_TXT"]);
            projectBE.TagCreatedIdNum = TryToParse<string>(dataRow["TAG_CREATED_ID_NUM"]);
            projectBE.TagCreatedByTs = TryToParse<DateTime?>(dataRow["TAG_CREATED_BY_TS"]);
            projectBE.TagUpdatedByTs = TryToParse<DateTime?>(dataRow["TAG_UPDATED_BY_TS"]);
            projectBE.TagUpdatedByIdNum = TryToParse<string>(dataRow["TAG_UPDATED_BY_ID_NUM"]);
            projectBE.AssignedEstimatorId = TryToParse<int?>(dataRow["ASSIGNED_ESTIMATOR_ID"]);
            projectBE.AssignedFacilitatorId = TryToParse<int?>(dataRow["ASSIGNED_FACILITATOR_ID"]);
            projectBE.ProjectModeRefId = TryToParse<int?>(dataRow["PROJECT_MODE_REF_ID"]);
            projectBE.ProjectLvlTxt = TryToParse<string>(dataRow["PROJECT_LVL_TXT"]);
            projectBE.CreatedDate = TryToParse<DateTime?>(dataRow["CREATED_DTTM"]);
            projectBE.UpdatedDate = TryToParse<DateTime?>(dataRow["UPDATED_DTTM"]);
            projectBE.GateDt = TryToParse<DateTime?>(dataRow["GATE_DT"]);
            projectBE.FifoDueDt = TryToParse<DateTime?>(dataRow["FIFO_DUE_DT"]);
            projectBE.ProjectAddrTxt = TryToParse<string>(dataRow["PROJECT_ADDR_TXT"]);
            projectBE.ProjectManagerId = TryToParse<int?>(dataRow["PROJECT_MANAGER_ID"]);
            projectBE.RtapInd = TryToParse<bool?>(dataRow["RTAP_IND"]);
            projectBE.PlansReadyOnDt = TryToParse<DateTime?>(dataRow["PLANS_READY_ON_DT"]);
            projectBE.SrcProjectStatus = TryToParse<string>(dataRow["SrcProjectStatus"]);
            return projectBE;

        }
     
        private ProjectSearchResultBE ConvertDataRowToProjectSearchResultBE(DataRow dataRow)
        {
            ProjectSearchResultBE projectSearchResultBE = new ProjectSearchResultBE();

            projectSearchResultBE.DateOfApplication = (DateTime)(dataRow["DATE_OF_APPLICATION"]);
            projectSearchResultBE.ProjectNumber = TryToParse<string>(dataRow["PROJECT_NUMBER"]);
            projectSearchResultBE.ProjectName = TryToParse<string>(dataRow["PROJECT_NAME"]);
            projectSearchResultBE.ProjectType = TryToParse<string>(dataRow["PROJECT_TYPE"]);
            projectSearchResultBE.CustomerName = TryToParse<string>(dataRow["CUSTOMER_NAME"]);
            projectSearchResultBE.FacilitatorName = TryToParse<string>(dataRow["FACILITATOR_NAME"]);
            projectSearchResultBE.ProjectStatus = TryToParse<string>(dataRow["PROJECT_STATUS"]);
            projectSearchResultBE.MeetingType = TryToParse<string>(dataRow["MEETING_TYPE"]);
            projectSearchResultBE.RecIdTxt = TryToParse<string>(dataRow["REC_ID_TXT"]);
            return projectSearchResultBE;
        }

    

        #endregion

    }

    #endregion

}