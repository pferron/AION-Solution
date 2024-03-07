namespace AION.Web.BusinessEntities
{
    public class PermissionMapping
    {
        // TBD. Need to update this property to pick the user name and then update the status to yes or not based on other roles of the logged in user.
        public bool IsManager { get; set; }

        public bool IsFacilitator { get; set; }

        public bool IsCustomer { get; set; }

        public bool IsTradeEstimator { get; set; }

        public bool IsTradePlanReviewer { get; set; }

        public bool IsAgencyEstimator { get; set; }

        public bool IsAgencyPlanReviewer { get; set; }

        public bool IsZoneAllowed { get; set; }

        public bool IsFireAllowed { get; set; }

        public bool IsBackFlowAllowed { get; set; }

        public bool IsHealthAllowed { get; set; }

        public bool IsBEMPAllowed { get; set; }

        public bool ISMMFEstimator { get; set; }

        public bool Building { get; set; }

        public bool Electrical { get; set; }

        public bool Mechanical { get; set; }

        public bool Plumbing { get; set; }

        public bool ZoneDavidson { get; set; }

        public bool ZoneCornelius { get; set; }

        public bool ZonePineVille { get; set; }

        public bool ZoneMatthews { get; set; }

        public bool ZoneMintHill { get; set; }

        public bool ZoneHuntersVille { get; set; }

        public bool ZoneUNC { get; set; }

        public bool ZoneCityCharlotte { get; set; }

        public bool FireDavidson { get; set; }

        public bool FireCornelius { get; set; }

        public bool FirePineVille { get; set; }

        public bool FireMatthews { get; set; }

        public bool FireMintHill { get; set; }

        public bool FireHuntersVille { get; set; }

        public bool FireUNC { get; set; }

        public bool FireCityCharlotte { get; set; }

        public bool EHDayCare { get; set; }

        public bool EHFood { get; set; }

        public bool EHPool { get; set; }

        public bool EHFacilities { get; set; }

        public bool BackFlow { get; set; }

        public bool IsViewOnly { get; set; }
        public bool IsSysAdmin { get; set; }
        public bool IsTradeReviewer { get; set; }
        public bool IsAgencyReviewer { get; set; }

        /// <summary>
        /// PermissionEnum
        /// </summary>
        public bool Add_Project_Files { get; set; }
        public bool Prlim_Estimat_Trads { get; set; }
        public bool Prlim_Estimat_Zoning { get; set; }
        public bool Prlim_Estimat_Fire { get; set; }
        public bool Prlim_Estimat_Bkflow { get; set; }
        public bool Prlim_Estimat_EHS { get; set; }
        public bool Schdul_Prlim_Mtng_Auto { get; set; }
        public bool Schdul_Prlim_Mtng_Man { get; set; }
        public bool Add_Prlim_Mtng_Prtcpnt { get; set; }
        public bool Cancel_Prlim_Mtng { get; set; }
        public bool Apprv_Mtng_Minuts { get; set; }
        public bool Upload_Minuts { get; set; }
        public bool Estimat_Trads { get; set; }
        public bool Estimat_Zoning { get; set; }
        public bool Estimat_Fire { get; set; }
        public bool Estimat_Bkflow { get; set; }
        public bool Estimat_EHS { get; set; }
        public bool Vw_Fclttor_Wrkload { get; set; }
        public bool E_Fclttor { get; set; }
        public bool Resend_Notif { get; set; }
        public bool E_Plns_Rdy_Dt { get; set; }
        public bool Accpt_Rjct_Rview_Dt { get; set; }
        public bool Schdul_Rview_Pln_Rview_Auto { get; set; }
        public bool Schdul_Rview_Pln_Rview_Man { get; set; }
        public bool Activt_NA_Rview { get; set; }
        public bool Vw_Schdul_Cpcty { get; set; }
        public bool Schdul_Nxt_Cycl { get; set; }
        public bool Assign_To_Me { get; set; }
        public bool Schdul_Notes_Sel { get; set; }
        public bool Vw_Mngmnt_Rprts { get; set; }
        public bool Reqst_Mtng { get; set; }
        public bool Reopen_Mtng { get; set; }
        public bool Close_Mtng { get; set; }
        public bool Cancel_Mtng { get; set; }
        public bool Exit_Mtng_Notes_For_Cstmr { get; set; }
        public bool Enter_Mtng_Prtcpnt { get; set; }
        public bool Configure_Express { get; set; }
        public bool E_Express_Rsrvtions { get; set; }
        public bool E_Schdul_Express { get; set; }
        public bool Man_Reserve_Express_Time { get; set; }
        public bool Schdul_Express_Auto { get; set; }
        public bool Schdul_Express_Man { get; set; }
        public bool Use_Express { get; set; }
        public bool Schdul_Collab_Mtng { get; set; }
        public bool Schdul_PrePermitting_Mtng { get; set; }
        public bool Schdul_Phasing_Mtng { get; set; }
        public bool Schdul_Exit_Mtng { get; set; }
        public bool Create_NPA { get; set; }
        public bool Modify_NPA { get; set; }

        public bool Access_Estimation { get; set; }
        public bool Access_PrelimEstimation { get; set; }
        public bool Access_SchedulePR { get; set; }
        public bool Access_ScheduleExpress { get; set; }
        public bool Access_ReserveExpress { get; set; }
        public bool Access_ScheduleMeeting { get; set; }
        public bool Access_SchedulePrelim { get; set; }

        public bool Schdul_Mtng { get; set; }
        public bool Holday_Config { get; set; }

        //LES-305 3/30/22 add management reports
        public bool Management_Report_1 { get; set; }
        public bool Management_Report_2 { get; set; }
        public bool Management_Report_3 { get; set; }
        public bool Management_Report_4 { get; set; }
    }
}