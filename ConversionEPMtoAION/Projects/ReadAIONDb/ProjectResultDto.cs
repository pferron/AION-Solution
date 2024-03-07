using System;
using System.ComponentModel.DataAnnotations;

namespace ReadAIONDb
{
    public class ProjectResultDto
    {
        public string REVIEW_TYP_REF_DESC { get; set; }
        public string CODE_SUMMARY_DESC { get; set; }
        public string CONSTR_TYP_DESC { get; set; }
        public string WORK_TYP_DESC { get; set; }
        public DateTime? UPDATED_DTTM { get; set; }

        [MaxLength]
        public string OVERALL_WORK_SCOPE_DESC { get; set; }

        public string PERMIT_NUM { get; set; }

        [MaxLength]
        public string ELCTR_WORK_SCOPE_DESC { get; set; }

        [MaxLength]
        public string MECH_WORK_SCOPE_DESC { get; set; }

        [MaxLength]
        public string PLUMB_WORK_SCOPE_DESC { get; set; }
        [MaxLength]
        public string CIVIL_WORK_SCOPE_DESC { get; set; }

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
    }
}
