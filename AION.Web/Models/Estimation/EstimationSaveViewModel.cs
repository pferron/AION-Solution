using AION.BL;
using AION.BL.Models;
using System.Collections.Generic;

namespace AION.Web.Models
{
    public class EstimationSaveViewModel : ViewModelBase
    {
        public EstimationSaveViewModel()
        {
            LoggedInUser = new UserIdentity();
        }

        public string ActiveTab { get; set; }
        public ProjectEstimation Project { get; set; }
        public bool HrsNABuilding { get; set; }
        public bool HrsNAElectric { get; set; }
        public bool HrsNAMech { get; set; }
        public bool HrsNAPlumbing { get; set; }
        public bool HrsNAZone { get; set; }
        public bool HrsNAFire { get; set; }
        public bool HrsNABackFlow { get; set; }
        public bool HrsNAFood { get; set; }
        public bool HrsNAPool { get; set; }
        public bool HrsNAFacility { get; set; }
        public bool HrsNADayCare { get; set; }
        public decimal HoursBuilding { get; set; }
        public decimal HoursElectic { get; set; }
        public decimal HoursMech { get; set; }
        public decimal HoursPlumb { get; set; }
        public decimal HoursZoning { get; set; }
        public decimal HoursFire { get; set; }
        public decimal HoursBackFlow { get; set; }
        public decimal HoursFood { get; set; }
        public decimal HoursPool { get; set; }
        public decimal HoursLodge { get; set; }
        public decimal HoursDayCare { get; set; }
        public ApplicationNotes BEMPApplicationNotes { get; set; }
        public ApplicationNotes ZoningApplicationNotes { get; set; }

        public ApplicationNotes FireApplicationNotes { get; set; }

        public ApplicationNotes BackFlowApplicationNotes { get; set; }
        public ApplicationNotes EHSApplicationNotes { get; set; }

        public bool IsAllNAChecked { get; set; }
        public int AssignedEstimatorId { get; set; }
        public string AssignedFacilitator { get; set; }
        public string PrimaryReviewerBuilding { get; set; }
        public string SecondaryReviewerBuilding { get; set; }
        public string PrimaryReviewerElectrical { get; set; }
        public string SecondaryReviewerelectrical { get; set; }
        public string PrimaryReviewerMechanical { get; set; }
        public string SecondaryReviewerMechanical { get; set; }
        public string PrimaryReviewerPlumbing { get; set; }
        public string SecondaryReviewerPlumbing { get; set; }
        public string PrimaryReviewerZone { get; set; }
        public string SecondaryReviewerZone { get; set; }
        public string PrimaryReviewerFire { get; set; }
        public string PrimaryReviewerBackFlow { get; set; }
        public string SecondaryReviewerFire { get; set; }
        public string SecondaryReviewerBackFlow { get; set; }
        public string PrimaryReviewerFood { get; set; }
        public string SecondaryReviewerFood { get; set; }
        public string PrimaryReviewerPool { get; set; }
        public string SecondaryReviewerPool { get; set; }
        public string PrimaryReviewerFacilities { get; set; }
        public string SecondaryReviewerFacilities { get; set; }
        public string PrimaryReviewerDayCare { get; set; }
        public string SecondaryReviewerDayCare { get; set; }

        public List<string> ExcludedPlanReviewersBuild { get; set; }

        public List<string> ExcludedPlanReviewersElectric { get; set; }

        public List<string> ExcludedPlanReviewersMech { get; set; }

        public List<string> ExcludedPlanReviewersPlumb { get; set; }

        public List<string> ExcludedPlanReviewersZone { get; set; }

        public List<string> ExcludedPlanReviewersFire { get; set; }

        public List<string> ExcludedPlanReviewersBackFlow { get; set; }

        public List<string> ExcludedPlanReviewersFood { get; set; }

        public List<string> ExcludedPlanReviewersPool { get; set; }

        public List<string> ExcludedPlanReviewersLodge { get; set; }

        public List<string> ExcludedPlanReviewersDayCare { get; set; }
        public bool IsSubmit { get; set; }
        /// <summary>
        /// LES-782
        /// Linked to SaveTypeEnum
        /// </summary>
        public int SaveType { get; set; }
        /// <summary>
        /// LES-782
        /// Linked to DepartmentDivisionEnum
        /// </summary>
        public int PendingEmailType { get; set; }

    }
}