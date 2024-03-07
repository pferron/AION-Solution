namespace AION.Web.Models.Admin
{
    public class PermissionViewModel : ViewModelBase
    {
        /// <summary>
        /// PermissionEnum
        /// </summary>
        /// 
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
        public bool Holday_Config { get; set; }
        public bool Schdul_Mtng { get; set; }

        //LES-305 3/30/22 add management reports
        public bool Management_Report_1 { get; set; }
        public bool Management_Report_2 { get; set; }
        public bool Management_Report_3 { get; set; }
        public bool Management_Report_4 { get; set; }

    }
}