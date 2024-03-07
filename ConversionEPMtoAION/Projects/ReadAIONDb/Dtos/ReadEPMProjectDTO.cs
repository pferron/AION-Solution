using System;

namespace ReadAIONDb.Dtos
{
    public class ReadEPMProjectDTO
    {
        public string PROJECT_ID { get; set; }
        public string PROJECT_NM { get; set; }
        public int EXTERNAL_SYSTEM_REF_ID { get; set; }
        public int PROJECT_STATUS_REF_ID { get; set; }
        public int PROJECT_TYP_REF_ID { get; set; }
        public string ASSIGNED_ESTIMATOR_ID { get; set; }
        public int PROJECT_MODE_REF_ID { get; set; }
        public int RTAP_IND { get; set; }
        public string PROJECT_ADDR_TXT { get; set; }
        public string TOTAL_FEE_AMT { get; set; }
        public string PROJECT_MANAGER_ID { get; set; }
        public string SRC_SYSTEM_VAL_TXT { get; set; }
        public DateTime? TAG_CREATED_BY_TS { get; set; }
        public DateTime? TAG_UPDATED_BY_TS { get; set; }
        public int PRELIMINARY_IND { get; set; }
        public int GATE_ACCEPTED_IND { get; set; }
        public int FIFO_IND { get; set; }
        public DateTime? PLANS_READY_ON_DT { get; set; }
        public string TEAM_GRADE_TXT { get; set; }
        public DateTime? GATE_DT { get; set; }
        public string BUILD_CODE_VERSION_DESC { get; set; }
        public string REVIEW_TYP_REF_DESC { get; set; }
        public string CODE_SUMMARY_DESC { get; set; }
        public string CONSTR_TYP_DESC { get; set; }
        public string WORK_TYP_DESC { get; set; }
        public string OVERALL_WORK_SCOPE_DESC { get; set; }
        public string ELCTR_WORK_SCOPE_DESC { get; set; }
        public string MECH_WORK_SCOPE_DESC { get; set; }
        public string PLUMB_WORK_SCOPE_DESC { get; set; }
        public string CIVIL_WORK_SCOPE_DESC { get; set; }
        public string PERMIT_NUM { get; set; }
        public string SHEETS_CNT_DESC { get; set; }
        public string DESIGNER_DESC { get; set; }
        public string SEAL_HOLDERS_DESC { get; set; }
        public string FIRE_DETAIL_DESC { get; set; }
        public string OCCUPANCY_DESC { get; set; }
        public string PRI_OCCUPANCY_DESC { get; set; }
        public string SECONDARY_OCCUPANCY_DESC { get; set; }
        public string SQUARE_FOOTAGE_DESC { get; set; }
        public string ZONING_OF_SITE_DESC { get; set; }
        public string CHG_OF_USE_DESC { get; set; }
        public string PREVIOUS_BUSINESS_TYP_DESC { get; set; }
        public string PROPOSED_BUSINESS_TYP_DESC { get; set; }
        public string CONDITIONAL_PERMIT_APPROVAL_DESC { get; set; }
        public string CITY_OF_CHARLOTTE_DESC { get; set; }
        public string WATER_SEWER_DETAIL_DESC { get; set; }
        public string PROPOSED_OUTDOOR_UNDERGROUND_PIPING_DESC { get; set; }
        public string PROPOSED_FIRE_SPRINKLER_PIPING_DESC { get; set; }
        public string INSTALL_CMUD_BACKFLOW_PREVENTER_DESC { get; set; }
        public string EXTENDING_PUBLIC_WATER_SEWER_DESC { get; set; }
        public string GRADE_MOD_WATER_SEWER_EASEMENT_DESC { get; set; }
        public string PROPOSED_ENCROACHMENT_WATER_SEWER_EASEMENT_DESC { get; set; }
        public string DAY_CARE_DESC { get; set; }
        public string HEALTH_DEPT_DETAIL_DESC { get; set; }
        public string TOTAL_JOB_COST_AMT { get; set; }
        public string txt_SOI { get; set; }
        public decimal? id_project_coordinator { get; set; }
    }
}
