using System.ComponentModel;

namespace AION.BL
{

    /*
     *  --These Enums do not have a related do not have [ENUM_MAPPING_VAL_NBR] in [AION].[PROJECT_TYPE_REF] table. So This is mapped directly to PK. 
     *  CAUTION: This may change between envionments and if that happens then need to change the PK in DB to fix it.
     */
    public enum PropertyTypeEnums
    {
        [Description("Express")]
        Express = 1,

        [Description("Commercial")]
        Commercial = 2,

        [Description("Mega Multi-Family")]
        Mega_Multi_Family = 3,

        [Description("Special Projects Team")]
        Special_Projects_Team = 4,

        [Description("Townhomes")]
        Townhomes = 5,

        [Description("FIFO: Small Commercial")]
        FIFO_Small_Commercial = 6,

        [Description("FIFO: Single Family Homes")]
        FIFO_Single_Family_Homes = 7,

        [Description("FIFO: Master Plans")]
        FIFO_Master_Plans = 8,

        [Description("FIFO: Addition/Renovation Single Family Home")]
        FIFO_Addition_Renovation_Single_Family_Home = 9,

        [Description("Fire Shop Drawings")]
        County_Fire_Shop_Drawings = 10,

        [Description("Residential")]
        Residential = 11,

        NA = -1
    }


    public enum DepartmentNameEnums
    {
        Building = 1,
        Electrical = 2,
        Mechanical = 3,
        Plumbing = 4,
        [Description("Zone - Davidson")]
        Zone_Davidson = 5,
        [Description("Zone - Cornelius")]
        Zone_Cornelius = 6,
        [Description("Zone - Pineville")]
        Zone_Pineville = 7,
        [Description("Zone - Matthews")]
        Zone_Matthews = 8,
        [Description("Zone - Mint Hill")]
        Zone_Mint_Hill = 9,
        [Description("Zone - Huntersville")]
        Zone_Huntersville = 10,
        [Description("Zone - Unincorporated Mecklenburg County")]
        Zone_UMC = 11,
        [Description("Zone - City Of Charlotte")]
        Zone_Cty_Chrlt = 12,
        [Description("Fire - Davidson")]
        Fire_Davidson = 13,
        [Description("Fire - Cornelius")]
        Fire_Cornelius = 14,
        [Description("Fire - Pineville")]
        Fire_Pineville = 15,
        [Description("Fire - Matthews")]
        Fire_Matthews = 16,
        [Description("Fire - Mint Hill")]
        Fire_Mint_Hill = 17,
        [Description("Fire - Huntersville")]
        Fire_Huntersville = 18,
        [Description("Fire - Unincorporated Mecklenburg County")]
        Fire_UMC = 19,
        [Description("Fire - City Of Charlotte")]
        Fire_Cty_Chrlt = 20,
        [Description("Environmental Health Services - Day Care")]
        EH_Day_Care = 21,
        [Description("Environmental Health Services - Food")]
        EH_Food = 22,
        [Description("Environmental Health Services - Pool")]
        EH_Pool = 23,
        [Description("Environmental Health Services - Facilities/Lodging")]
        EH_Facilities = 24,
        [Description("Charlotte Water Backflow")]
        Backflow = 25,
        [Description("Zone - County")]
        Zone_County = 26,
        [Description("Fire - County")]
        Fire_County = 27,
        NA = -1
    }
    public enum ShortDepartmentNameEnums
    {
        Building = 1,
        Electrical = 2,
        Mechanical = 3,
        Plumbing = 4,
        [Description("Zone - Davidson")]
        Zone_Davidson = 5,
        [Description("Zone - Cornelius")]
        Zone_Cornelius = 6,
        [Description("Zone - Pineville")]
        Zone_Pineville = 7,
        [Description("Zone - Matthews")]
        Zone_Matthews = 8,
        [Description("Zone - Mint Hill")]
        Zone_Mint_Hill = 9,
        [Description("Zone - Huntersville")]
        Zone_Huntersville = 10,
        [Description("Zone - UMC")]
        Zone_UMC = 11,
        [Description("Zone - City")]
        Zone_Cty_Chrlt = 12,
        [Description("Fire - Davidson")]
        Fire_Davidson = 13,
        [Description("Fire - Cornelius")]
        Fire_Cornelius = 14,
        [Description("Fire - Pineville")]
        Fire_Pineville = 15,
        [Description("Fire - Matthews")]
        Fire_Matthews = 16,
        [Description("Fire - Mint Hill")]
        Fire_Mint_Hill = 17,
        [Description("Fire - Huntersville")]
        Fire_Huntersville = 18,
        [Description("Fire - UMC")]
        Fire_UMC = 19,
        [Description("Fire - City")]
        Fire_Cty_Chrlt = 20,
        [Description("EHS - Day Care")]
        EH_Day_Care = 21,
        [Description("EHS - Food")]
        EH_Food = 22,
        [Description("EHS - Pool")]
        EH_Pool = 23,
        [Description("EHS - Fac/Lodging")]
        EH_Facilities = 24,
        [Description("CW Backflow")]
        Backflow = 25,
        [Description("Zone - County")]
        Zone_County = 26,
        [Description("Fire - County")]
        Fire_County = 27,
        NA = -1
    }

    /// <summary>
    /// Only used in the User Admin for the Trade/Agency checkboxes
    /// BEMP, Backflow, EHS are the same enum values as DepartmentNameEnums
    /// Zoning and Fire need to be mapped for the jurisdiction
    /// </summary>
    public enum UserAdminViewModelDeptNameEnum
    {
        Building = 1,
        Electrical = 2,
        Mechanical = 3,
        Plumbing = 4,
        Zoning = 5,
        Fire = 6,
        [Description("Day Care")]
        EH_Day_Care = 21,
        [Description("Food Service")]
        EH_Food = 22,
        [Description("Public Pool")]
        EH_Pool = 23,
        [Description("Facility/Lodging")]
        EH_Facilities = 24,
        Backflow = 25,
        NA = -1
    }
    public enum ExternalSystemEnum
    {
        AION = 1,
        Accela = 2,
        NA = -1
    }

    public enum ReviewTypeEnum
    {
        Express = 1,
        OnSchdule = 2,
        NA = -1
    }

    public enum ProjectStatusEnum
    {
        /* These values are connected with database table values, 
         * [AION].[CATALOG_REF].SUB_KEY  and [AION].[PROJECT_STATUS_REF]. 
         * DO NOT CHANGE IT WITH OUT CROSS VERIFYING THE VALUES.*/
        [Description("Application Submitted")]
        Application_Submitted = 1,

        [Description("Auto Estimation Pending")]
        Auto_Estimation_Pending = 2,

        [Description("Auto Estimation in Progress")]
        Auto_Estimation_In_Progress = 3,

        [Description("Ready for Estimator")]
        Ready_For_Estimator = 4,

        [Description("Estimation In Progress")]
        Estimation_In_Progress = 5,

        [Description("Not Scheduled")]
        Not_Scheduled = 6,

        [Description("PROD Not Known")]
        PROD_Not_Known = 7,

        [Description("Abort Package")]
        Abort_Package = 8,

        [Description("Suspended Fees Due")]
        Suspended_Fees_Due = 9,

        [Description("Tentatively Scheduled")]
        Tentatively_Scheduled = 10,

        [Description("Awaiting Meeting Documents")]
        Awaiting_Meeting_Documents = 11,

        [Description("Scheduled")]
        Scheduled = 12,

        [Description("Awaiting Minutes")]
        Awaiting_Minutes = 13,

        [Description("Waiting For Summary Document")]
        Waiting_For_Summary_Document = 14,

        [Description("Review Meeting Minutes")]
        Review_Meeting_Minutes = 15,

        [Description("Awaiting Revised Minutes")]
        Awaiting_Revised_Minutes = 16,

        [Description("Awaiting Docs")]
        Awaiting_Docs = 17,

        [Description("Review Docs")]
        Review_Docs = 18,

        [Description("Rev Prepermitting Agreement Letter")]
        Rev_Prepermitting_Agreement_Letter = 19,

        [Description("Review Phasing Document")]
        Review_Phasing_Document = 20,

        [Description("Complete")]
        Complete = 21,

        [Description("Awaiting Cancellation")]
        Awaiting_Cancellation = 22,

        [Description("Pending Abandonment")]
        Pending_Abandonment = 23,

        [Description("Pending Cancellation - Awaiting Cancellation Fees")]
        Pending_Cancellation_Awaiting_Cancellation_Fees = 24,

        [Description("Cancelled")]
        Cancelled = 25,

        /// <summary>
        /// This is linked in Catalog_ref table
        /// If enum value is changed then the subkey 
        /// needs to be changed in the catalog table
        /// </summary>
        // Estimation Pending Reason Mapping
        [Description("Preliminary Meeting Required")]
        Preliminary_Meeting_Required = 26,

        /// <summary>
        /// This is linked in Catalog_ref table
        /// If enum value is changed then the subkey 
        /// needs to be changed in the catalog table
        /// </summary>
        // Estimation Pending Reason Mapping
        [Description("Scope Drawings Required")]
        Scope_Drawings_Required = 27,

        /// <summary>
        /// This is linked in Catalog_ref table
        /// If enum value is changed then the subkey 
        /// needs to be changed in the catalog table
        /// </summary>
        // Estimation Pending Reason Mapping
        [Description("Information Required")]
        Information_Required = 28,

        [Description("Abandoned")]
        Abandoned = 29,

        [Description("NA")]
        NA = -1
    }

    public enum NoteTypeEnum
    {
        [Description("Internal")]
        InternalNotes = 1,

        [Description("Gate")]
        GateNotes = 2,

        [Description("Pending")]
        PendingNotes = 3,

        [Description("Accela")]
        AccelaProjectNotes = 4,

        [Description("Exit Meeting")]
        ExitMeetingNotes = 5,

        [Description("Meeting Doc")]
        MeetingDocNotes = 6,

        [Description("Estimation Standard")]
        EstimationStandardNotes = 7,

        [Description("Scheduling Standard")]
        SchedulingStandardNotes = 8,

        [Description("Scheduling Mandatory")]
        SchedulingMandatoryNotes = 9,

        [Description("Scheduling")]
        SchedulingNotes = 10,

        [Description("NA")]
        NA = -1
    }

    public enum DepartmentTypeEnum
    {
        Agency = 1,
        Trade = 2,
        NA = -1
    }


    public enum DepartmentDivisionEnum
    {
        Building = 3,
        Electrical = 4,
        Mechanical = 5,
        Plumbing = 6,
        Zoning = 7,
        Fire = 8,
        Environmental = 9,
        Backflow = 10,
        NA = -1
    }

    /// <summary>
    /// Business_Type_Ref table
    /// </summary>
    public enum DepartmentRegionEnum
    {
        Davidson = 11,
        Cornelius = 12,
        Pineville = 13,
        Matthews = 14,
        Mint_Hill = 15,
        Huntersville = 16,
        UN_County = 17,
        Charlotte_City = 18,
        Day_Care = 19,
        Food_Service = 20,
        Public_Pool = 21,
        Facilities_Lodging = 22,
        County = 23,
        NA = -1
    }

    //THIS ENUM IS HARWIRED TO TABLE COLUMN [AION].[SYSTEM_ROLE].[ENUM_MAPPING_VAL_NBR]
    public enum SystemRoleEnum
    {
        [Description("NA")]
        NA = -1,
        [Description("Estimator")]
        Estimator = 1,
        [Description("Plan Reviewer")]
        Plan_Reviewer = 2,
        [Description("Facilitator")]
        Facilitator = 3,
        [Description("Manager")]
        Manager = 32,
        [Description("View Only")]
        View_Only = 45,
        [Description("System Administrator")]
        Sys_Admin = 46,
        [Description("External Project Manager (Customer)")]
        External_Project_Manager = 47
    }
    public enum TradeEnums
    {
        Building = 1,
        Electrical = 2,
        Mechanical = 3,
        Plumbing = 4
    }
    public enum AgencyEnums
    {
        Zone_Davidson = 5,
        Zone_Cornelius = 6,
        Zone_Pineville = 7,
        Zone_Matthews = 8,
        Zone_Mint_Hill = 9,
        Zone_Huntersville = 10,
        Zone_UMC = 11,
        Zone_Cty_Chrlt = 12,
        Fire_Davidson = 13,
        Fire_Cornelius = 14,
        Fire_Pineville = 15,
        Fire_Matthews = 16,
        Fire_Mint_Hill = 17,
        Fire_Huntersville = 18,
        Fire_UMC = 19,
        Fire_Cty_Chrlt = 20,
        EH_Day_Care = 21,
        EH_Food = 22,
        EH_Pool = 23,
        EH_Facilities = 24,
        Backflow = 25,
        Zone_County = 26,
        Fire_County = 27,
        NA = -1
    }
    public enum StandardNoteGroupEnums
    {
        First_Cycle_Packaging_Instructions,
        ReReview_Packaging_Instructions,
        RTAP_Packaging_Instructions,
        Express_Review_Packaging_Instructions,
        Charlotte_City_Zoning,
        Towns,
        County_Engineering,
        Charlotte_City_Engineering,
        Commercial_Plan_Review_Link,
        Health_Department,
        CMUD_Backflow,
        Mandatory
    }

    public enum PlanReviewHourTypes
    {
        [Description("Plan Reviewer - Non MMF")]
        HoursPlanReviewerMMF = 1,
        [Description("Mega Multi Family")]
        HoursMMF = 2,
        [Description("County Fire")]
        HoursCountyFire = 3,
        [Description("Express")]
        HoursExpress = 4,
        [Description("FIFO: Small Commercial")]
        HoursFIFOSmComm = 5,
        [Description("FIFO: Single Family Home")]
        HoursFIFOSingleFH = 6,
        [Description("FIFO: Master Plan")]
        HoursFIFOMsPln = 7,
        [Description("FIFO: Add/Reno SFH")]
        HoursFIFOAddRenSFH = 8,
    }

    /// <summary>
    /// Used in the table ProjectSchedule to determine what type of appointment is required
    /// The text of this enum is the field "PMA", "NPA", etc
    /// </summary>
    public enum ProjectScheduleRefEnum
    {
        //Non project appointment
        NPA,
        //Preliminary Meeting appointment
        PMA,
        //Express Reservations
        EXP,
        //Plan Review
        PR,
        //Express Meeting Appointment
        EMA,
        //Facilitator Meeting Appointment
        FMA,
        //Fifo
        FIFO
    }
    public enum AppointmentRecurrenceRefEnum
    {
        NA = -1,
        Once = 1,
        First_Monday = 2,
        First_Tuesday = 3,
        First_Wednesday = 4,
        First_Thursday = 5,
        First_Friday = 6,
        Second_Monday = 7,
        Second_Tuesday = 8,
        Second_Wednesday = 9,
        Second_Thursday = 10,
        Second_Friday = 11,
        Third_Monday = 12,
        Third_Tuesday = 13,
        Third_Wednesday = 14,
        Third_Thursday = 15,
        Third_Friday = 16,
        Fourth_Monday = 17,
        Fourth_Tuesday = 18,
        Fourth_Wednesday = 19,
        Fourth_Thursday = 20,
        Fourth_Friday = 21,
        Last_Monday = 22,
        Last_Tuesday = 23,
        Last_Wednesday = 24,
        Last_Thursday = 25,
        Last_Friday = 26,
        Weekly_Monday = 27,
        Weekly_Tuesday = 28,
        Weekly_Wednesday = 29,
        Weekly_Thursday = 30,
        Weekly_Friday = 31,
        Daily = 32,
        Yearly = 33
    }

    public enum RecurrenceEnum
    {
        Once = -1,
        First = 1,
        Second = 2,
        Third = 3,
        Fourth = 4,
        Last = 5,
        Weekly = 6,
        Yearly = 7,
        Daily = 8
    }

    /// <summary>
    /// Used for drop down lists in UI
    /// </summary>
    public struct DepartmentNameList
    {
        public const string Building = "Building";
        public const string Electrical = "Electrical";
        public const string Mechanical = "Mechanical";
        public const string Plumbing = "Plumbing";
        public const string Zoning = "Zoning";
        public const string Zoning_Charlotte = "Zoning - City Of Charlotte";
        public const string Zoning_MintHill = "Zoning - Mint Hill";
        public const string Zoning_Huntersville = "Zoning - Huntersville";
        public const string Fire = "Fire";
        public const string Fire_Charlotte = "Fire - City Of Charlotte";
        public const string Backflow = "Backflow";
        public const string Food_Service = "Food Service";
        public const string Public_Pool = "Public Pool";
        public const string Facility_Lodging = "Facility/Lodging";
        public const string Day_Care = "Day Care";
    }

    /// <summary>
    /// Permission List, db connected enum vals, do not change
    /// </summary>
    public enum PermissionEnum
    {
        [Description("Add Project Files")]
        Add_Project_Files = 1,
        [Description("Preliminary Meeting Estimate Trades")]
        Prlim_Estimat_Trads = 2,
        [Description("Preliminary Meeting Estimate Zoning")]
        Prlim_Estimat_Zoning = 3,
        [Description("Preliminary Meeting Estimate Fire")]
        Prlim_Estimat_Fire = 4,
        [Description("Preliminary Meeting Estimate Backflow")]
        Prlim_Estimat_Bkflow = 5,
        [Description("Preliminary Meeting Estimate EHS")]
        Prlim_Estimat_EHS = 6,
        [Description("Schedule Preliminary Meeting (auto)")]
        Schdul_Prlim_Mtng_Auto = 7,
        [Description("Schedule Preliminary Meeting (manual)")]
        Schdul_Prlim_Mtng_Man = 8,
        [Description("Add Preliminary Meeting Participants")]
        Add_Prlim_Mtng_Prtcpnt = 9,
        [Description("Cancel Preliminary Meeting")]
        Cancel_Prlim_Mtng = 10,
        [Description("Approve Meeting Minutes")]
        Apprv_Mtng_Minuts = 11,
        [Description("Upload Minutes")]
        Upload_Minuts = 12,
        [Description("Estimate Trades")]
        Estimat_Trads = 13,
        [Description("Estimate Zoning")]
        Estimat_Zoning = 14,
        [Description("Estimate Fire")]
        Estimat_Fire = 15,
        [Description("Estimate Backflow")]
        Estimat_Bkflow = 16,
        [Description("Estimate EHS")]
        Estimat_EHS = 17,
        [Description("View Facilitator Workload Summary")]
        Vw_Fclttor_Wrkload = 18,
        [Description("Edit Facilitator")]
        E_Fclttor = 19,
        [Description("Resend Notifications")]
        Resend_Notif = 20,
        [Description("Edit Plans Ready Date")]
        E_Plns_Rdy_Dt = 21,
        [Description("Accept/Reject Review Date")]
        Accpt_Rjct_Rview_Dt = 22,
        [Description("Schedule Review Plan Review (auto)")]
        Schdul_Rview_Pln_Rview_Auto = 23,
        [Description("Schedule Review Plan Review (manual)")]
        Schdul_Rview_Pln_Rview_Man = 24,
        [Description("Activate NA Review")]
        Activt_NA_Rview = 25,
        [Description("View Schedule Capacity")]
        Vw_Schdul_Cpcty = 26,
        [Description("Schedule Next Cycle")]
        Schdul_Nxt_Cycl = 27,
        [Description("Assign to Me")]
        Assign_To_Me = 28,
        [Description("Scheduling Notes Selection")]
        Schdul_Notes_Sel = 29,
        [Description("View Management Reports")]
        Vw_Mngmnt_Rprts = 30,
        [Description("Request Meeting")]
        Reqst_Mtng = 31,
        [Description("Reopen Meeting")]
        Reopen_Mtng = 32,
        [Description("Close Meeting")]
        Close_Mtng = 33,
        [Description("Cancel Meeting")]
        Cancel_Mtng = 34,
        [Description("Exit Meeting Notes for Customer")]
        Exit_Mtng_Notes_For_Cstmr = 35,
        [Description("Enter Meeting Participants")]
        Enter_Mtng_Prtcpnt = 36,
        [Description("Configure Express")]
        Configure_Express = 37,
        [Description("Modify Express Reservations")]
        E_Express_Rsrvtions = 38,
        [Description("Modify Scheduled Express")]
        E_Schdul_Express = 39,
        [Description("Manually Reserve Express Time")]
        Man_Reserve_Express_Time = 40,
        [Description("Schedule Express (auto)")]
        Schdul_Express_Auto = 41,
        [Description("Schedule Express (manual)")]
        Schdul_Express_Man = 42,
        [Description("Use Express")]
        Use_Express = 43,
        [Description("Schedule Collaborative Meeting")]
        Schdul_Collab_Mtng = 44,
        [Description("Schedule PrePermitting Meeting")]
        Schdul_PrePermitting_Mtng = 45,
        [Description("Schedule Phasing Meeting")]
        Schdul_Phasing_Mtng = 46,
        [Description("Schedule Exit Meeting")]
        Schdul_Exit_Mtng = 47,
        [Description("Create NPA")]
        Create_NPA = 48,
        [Description("Modify NPA")]
        Modify_NPA = 49,
        [Description("Holiday Configuration")]
        Holday_Config = 50,
        [Description("Schedule Meeting")]
        Schdul_Mtng = 51,
        [Description("Scheduling Lead Time For Projects")]
        Management_Report_1 = 52,
        [Description("Reviewer Scheduling Summary")]
        Management_Report_2 = 53,
        [Description("Reviewer Scheduling Details")]
        Management_Report_3 = 54,
        [Description("Scheduling Event Analysis")]
        Management_Report_4 = 55,

    }
    /// <summary>
    /// Module - permission matrix module - linked to db do not change
    /// </summary>
    public enum PermissionModuleEnum
    {
        [Description("Preliminary Meeting")]
        Prlim_Estimation = 1,
        [Description("Estimation")]
        Estimation = 2,
        [Description("Scheduling")]
        Scheduling = 3,
        [Description("Reports")]
        Reports = 4,
        [Description("Meeting")]
        Meeting = 5,
        [Description("Express")]
        Express = 6,
        [Description("NPA")]
        NPA = 7,
        [Description("Add Project Files")]
        Add_Project_Files = 8,
        [Description("Admin")]
        Admin = 9,
        [Description("Deprecated")]
        Deprecated = 10
    }

    /// <summary>
    /// In DB as Jurisdiction_Ref
    /// </summary>
    public enum JurisdictionEnum
    {
        [Description("County")]
        County = 1,

        [Description("Charlotte")]
        Charlotte = 2,

        [Description("Cornelius")]
        Cornelius = 3,

        [Description("Davidson")]
        Davidson = 4,

        [Description("Huntersville")]
        Huntersville = 5,

        [Description("Matthews")]
        Matthews = 6,

        [Description("Mint Hill")]
        Mint_Hill = 7,

        [Description("Pineville")]
        Pineville = 8
    }

    public enum AppointmentResponseStatusEnum
    {
        [Description("Reject")]
        Reject = 1,

        [Description("Self Schedule")]
        Self_Schedule = 2,

        [Description("No Reply")]
        No_Reply = 3,

        [Description("Not Scheduled")]
        Not_Scheduled = 4,

        [Description("Scheduled")]
        Scheduled = 5,

        [Description("Accept")]
        Accept = 6,

        [Description("Cancelled")]
        Cancelled = 7,

        [Description("Closed")]
        Closed = 8,

        [Description("Tentatively Scheduled")]
        Tentatively_Scheduled = 9
    }

    /// <summary>
    /// This enum is used wherever an appointment is automatically cancelled
    /// by either no timely response to the appointment request or cancelled
    /// by Accela.
    /// </summary>
    public enum AppointmentCancellationEnum
    {
        [Description("No Reply from Customer")]
        No_Reply = 1,

        [Description("Cancelled by Accela")]
        Accela = 2,

        [Description("Rejected by Customer")] // customer explicitly reschedules dates
        Reject = 3,

        [Description("NA")]
        NA = -1
    }

    public enum PlanReviewResponseStatusEnum
    {
        [Description("Reject")]
        Reject = 1,

        [Description("Accept")]
        Accept = 2,

        [Description("Self Schedule")]
        Self_Schedule = 3
    }

    public enum ExpressMeetingAppointmentStatusEnum
    {
        [Description("Reject")] Reject = 1,
        [Description("Accept")] Accept = 2,
    }

    public enum MeetingTypeEnum
    {
        [Description("Exit")]
        Exit = 1,

        [Description("Project Challenges")]
        Project_Challenges = 2,

        [Description("Code Administration")]
        Code_Admin = 3,

        [Description("Legal Easement Agreement")]
        Legal_Easement = 4,

        [Description("Phasing")]
        Phasing = 5,

        [Description("Prepermitting")]
        Prepermitting = 6,

        [Description("Preliminary Meeting")]
        Preliminary = 7,

        [Description("Express")]
        Express = 8,

        [Description("NA")]
        NA = -1
    }

    public enum MeetingActionEnum
    {
        [Description("Close Meeting")]
        Close_Meeting = 1,
        [Description("Cancel Meeting")]
        Cancel_Meeting = 2,
        [Description("Mgr Reopen")]
        Mgr_Reopen = 3,
        [Description("Reschedule Meeting")]
        Reschedule_Meeting = 4,
        NA = -1
    }

    public enum ReserveExpressDayEnum
    {
        Monday = 1,
        Tuesday = 2,
        Wednesday = 3,
        Thursday = 4,
        Friday = 5
    }


    /// <summary>
    /// Used in saving Estimation and Preliminary Estimation
    /// to determine if emails need to be sent and if the project status should be changed
    /// </summary>
    public enum SaveTypeEnum
    {
        Submittal = 1,
        Save = 2,
        SendPendingEmail = 3
    }

    public enum MeetingRoomEnum
    {
        MintHill = 1,
        Matthews = 2,
        Pineville = 3,
        Hal_Marshall = 4,
        Hoffman = 5,
        Huntersville = 6,
        Woods = 7,
        Granite = 8,
        Dogwood = 9,
        Cardinal = 10
    }

    /// <summary>
    /// Email notification type descriptions
    /// Used for saving notifications
    /// </summary>
    public enum EmailNotifType
    {
        [Description("Meeting Accept/Reject email")]
        Meeting_Tentative_Scheduled,
        [Description("Plan Review Accept/Reject Email")]
        Plan_Review_Tentative_Scheduled,
        [Description("Preliminary Meeting Accept/Reject Email")]
        Preliminary_Tentative_Scheduled,
        [Description("Express Tentative Scheduled Email")]
        Express_Tentative_Scheduled,
        [Description("Pending Estimation")]
        Pending_Estimation,
        [Description("Pending Preliminary Estimation")]
        Pending_Preliminary_Estimation,
        [Description("Express Decision By Estimator")]
        Express_Decision_By_Estimator,
    }

    public enum UIStatusMessage
    {
        [Description("Insufficient Permission")]
        Insufficient_Permission,
        [Description("Please log in")]
        Not_Logged_In,
        [Description("Error. There is an issue with retrieving selected project information. Check the project is valid and if the issues still persists consider contacting your Adminstrator.")]
        Project_Not_Found,
        [Description("Project Error. Please contact system admin.")]
        Project_Load_Error,
        [Description("Need Project ID to continue")]
        Project_ID_Missing,
        [Description("Outdated Project - missing cycle information")]
        Project_Cycle_Missing,
        [Description("Logged Out")]
        Logged_Out,
        [Description("Saved Successfully")]
        Saved_Successfully,
        [Description("Incomplete Save")]
        Incomplete_Save,
        NA
    }
    /// <summary>
    /// linked to db id col, only edit if linked to correct db id
    /// </summary>
    public enum AuditActionEnum
    {
        [Description("NA")]
        NA = -1,

        [Description("Application Submitted")]
        Application_Submitted = 1,

        [Description("Building Estimation Completed")]
        Building_Estimation_Completed = 2,

        [Description("Building Estimation Not Required")]
        Building_Estimation_Not_Required = 3,

        [Description("Building Estimation Pending")]
        Building_Estimation_Pending = 4,

        [Description("Backflow Estimation Completed")]
        Backflow_Estimation_Completed = 5,

        [Description("Backflow Estimation Not Required")]
        Backflow_Estimation_Not_Required = 6,

        [Description("Backflow Estimation Pending")]
        Backflow_Estimation_Pending = 7,

        [Description("Day Care Estimation Completed")]
        Commercial_Day_Care_Estimation_Completed = 8,

        [Description("Day Care Estimation Not Required")]
        Commercial_Day_Care_Estimation_Not_Required = 9,

        [Description("Day Care Estimation Pending")]
        Commercial_Day_Care_Estimation_Pending = 10,

        [Description("Facility/Lodging Estimation Completed")]
        EHS_Facility_Lodging_Estimation_Completed = 11,

        [Description("Facility/Lodging Estimation Not Required")]
        EHS_Facility_Lodging_Estimation_Not_Required = 12,

        [Description("Facility/Lodging Estimation Pending")]
        EHS_Facility_Lodging_Estimation_Pending = 13,

        [Description("Electrical Estimation Completed")]
        Electrical_Estimation_Completed = 14,

        [Description("Electrical Estimation Not Required")]
        Electrical_Estimation_Not_Required = 15,

        [Description("Electrical Estimation Pending")]
        Electrical_Estimation_Pending = 16,

        [Description("Fire Estimation Completed")]
        Fire_Estimation_Completed = 17,

        [Description("Fire Estimation Not Required")]
        Fire_Estimation_Not_Required = 18,

        [Description("Fire Estimation Pending")]
        Fire_Estimation_Pending = 19,

        [Description("Food Service Estimation Completed")]
        Food_Service_Estimation_Completed = 20,

        [Description("Food Service Estimation Not Required")]
        Food_Service_Estimation_Not_Required = 21,

        [Description("Food Service Estimation Pending")]
        Food_Service_Estimation_Pending = 22,

        [Description("Mechanical Estimation Completed")]
        Mechanical_Estimation_Completed = 23,

        [Description("Mechanical Estimation Not Required")]
        Mechanical_Estimation_Not_Required = 24,

        [Description("Mechanical Estimation Pending")]
        Mechanical_Estimation_Pending = 25,

        //[Description("Package Resubmitted")]
        //Package_Resubmitted = 26,

        //[Description("Package Submitted")]
        //Package_Submitted = 27,

        //[Description("Plan Review Fee Added")]
        //Plan_Review_Fee_Added = 28,

        //[Description("Plan Review Fee Updated")]
        //Plan_Review_Fee_Updated = 29,

        [Description("Plumbing Estimation Completed")]
        Plumbing_Estimation_Completed = 30,

        [Description("Plumbing Estimation Not Required")]
        Plumbing_Estimation_Not_Required = 31,

        [Description("Plumbing Estimation Pending")]
        Plumbing_Estimation_Pending = 32,

        //[Description("Potential Agencies for Permitting")]
        //Potential_Agencies_for_Permitting = 33,

        [Description("Facilitator Assigned")]
        Facilitator_Assigned = 34,

        //[Description("Project File Deleted")]
        //Project_File_Deleted = 35,

        [Description("Project Linked")]
        Project_Linked = 36,

        [Description("Public Pool Estimation Completed")]
        Public_Pool_Estimation_Completed = 37,

        [Description("Public Pool Estimation Not Required")]
        Public_Pool_Estimation_Not_Required = 38,

        [Description("Public Pool Estimation Pending")]
        Public_Pool_Estimation_Pending = 39,

        [Description("Zoning Estimation Completed")]
        Zoning_Estimation_Completed = 40,

        [Description("Zoning Estimation Not Required")]
        Zoning_Estimation_Not_Required = 41,

        [Description("Zoning Estimation Pending")]
        Zoning_Estimation_Pending = 42,

        [Description("All Estimations Completed")]
        All_Estimations_Completed = 43,

        [Description("Appointment Cancelled")]
        Appointment_Cancelled = 44,

        [Description("Appointment Created")]
        Appointment_Created = 45,

        [Description("Plans Ready On Date Entered")]
        Plans_Ready_On_Date_Entered = 46,

        [Description("Project Note Entered")]
        Project_Note_Entered = 47,

        [Description("Review Date Accepted")]
        Review_Date_Accepted = 48,

        [Description("Review Tentatively Scheduled")]
        Review_Tentatively_Scheduled = 49,

        [Description("Review Cancelled")]
        Review_Cancelled = 50,

        [Description("AION Status Changed")]
        Status_Changed = 51,

        [Description("Accela Status")]
        Accela_Status = 52,

        [Description("Auto Schedule")]
        Auto_Schedule = 53,

        [Description("Meeting Cancelled")]
        Meeting_Cancelled = 54,

        [Description("Review Date Rejected")]
        Review_Date_Rejected = 55,

        [Description("Estimation Change")]
        Estimation_Change = 56,
    }
    public enum ActionTag
    {
        Completed,
        Pending,
        [Description("Not Required")]
        Not_Required,
        NA
    }

    public enum CalendarAppointmentAction
    {
        Create,
        Delete
    }

    /// <summary>
    /// Used in MessageTemplateEngine
    /// These are the enum_mapping_val_nbrs from the db
    /// </summary>
    public enum MessageTemplateTypeEnum
    {
        [Description("Meeting Accept/Reject email")] Meeting_AcceptReject_email = 1,
        [Description("Plan Review Accept/Reject Email")] Plan_Review_AcceptReject_Email = 2,
        [Description("Preliminary Meeting Accept/Reject Email")] Preliminary_Meeting_AcceptReject_Email = 3,
        [Description("Pending Estimation")] Pending_Estimation = 4,
        [Description("Pending Preliminary Estimation")] Pending_Preliminary_Estimation = 5,
        [Description("Express Decision By Estimator")] Express_Decision_By_Estimator = 6,
        [Description("Express Tentative Scheduled Email")] Express_Tentative_Scheduled_Email = 7,
        [Description("Mandatory Scheduling Notes")] Mandatory_Scheduling_Notes = 8,
        [Description("Re-Review Packaging Instructions")] ReReview_Packaging_Instructions = 9,
        [Description("RTAP Packaging Instructions")] RTAP_Packaging_Instructions = 10,
        [Description("First Cycle Packaging Instructions")] First_Cycle_Packaging_Instructions = 11,
        [Description("Submit Anytime")] Submit_Anytime = 12,
        [Description("Express Review Packaging Instructions")] Express_Review_Packaging_Instructions = 13,
        [Description("Charlotte City Zoning")] Charlotte_City_Zoning = 14,
        [Description("Towns")] Towns = 15,
        [Description("Huntersville Project Submittal")] Huntersville_Project_Submittal = 16,
        [Description("Huntersville Zoning Approval")] Huntersville_Zoning_Approval = 17,
        [Description("Cornelius Zoning Approval")] Cornelius_Zoning_Approval = 18,
        [Description("Davidson Zoning Approval")] Davidson_Zoning_Approval = 19,
        [Description("County Engineering")] County_Engineering = 20,
        [Description("Charlotte City Engineering")] Charlotte_City_Engineering = 21,
        [Description("Site Plan Requirement")] Site_Plan_Requirement = 22,
        [Description("Change of Use")] Change_of_Use = 23,
        [Description("Commercial Plan Review Link")] Commercial_Plan_Review_Link = 24,
        [Description("Health Department")] Health_Department = 25,
        //jcl these will need to change when we add in standard notes
        [Description("Food Service JD")] Food_Service_JD = 26,
        [Description("Food Service Herb")] Food_Service_Herb = 27,
        [Description("Food Service Troy")] Food_Service_Troy = 28,
        //jcl
        [Description("Well/Septic")] WellSeptic = 29,
        [Description("Swimming Pool Review")] Swimming_Pool_Review = 30,
        [Description("Health Department Fee")] Health_Department_Fee = 31,
        [Description("CMUD Backflow")] CMUD_Backflow = 32,
        [Description("NPA Calendar Text")] NPA_Calendar_Text = 33,
        [Description("Prelim cancel meeting email")] Prelim_cancel_meeting_email = 34,
        [Description("Plan review project cancelled email")] Plan_review_project_cancelled_email = 35,
        [Description("Meeting cancelled email")] Meeting_cancelled_email = 36,
        [Description("New Project Manager Email")] New_Project_Manager_Email = 37,
        [Description("Change project Manager email")] Change_project_Manager_email = 38,
        [Description("All estimation is NA email")] All_estimation_is_NA_email = 39,
        [Description("Plan Reviewer NA for express email")] Plan_Reviewer_NA_for_express_email = 40,
        [Description("Gate date rescheduled email")] Gate_date_rescheduled_email = 41,
        [Description("Express cancelled email")] Express_cancelled_email = 42,
        [Description("Plan Review Cancellation Message")] PR_Cancellation_Message = 43,
        [Description("New Cycle: Scheduling Not Required")] New_Cycle_Scheduling_Not_Required = 44,
        [Description("Accela Integration Failure")] Accela_Integration_Failure = 45,
        [Description("Scheduling Lead Time Report Data Available")] Scheduling_LeadTimeReport_Data_Available = 46,
        [Description("Function Adapter Sync Failure")] Function_Adapter_Sync_Failure = 47
    }

    /// <summary>
    /// Used in auto estimation engine
    /// </summary>
    public enum TypeOfWork
    {
        [Description("UPFITRTAP")]
        UpFitRTAP = 1,
        [Description("NEWCONSTRUCTION")]
        NewConstruction = 2
    }

    public enum AccelaAionQueueStatus
    {
        [Description("Not Found in Accela")]
        NotFound = 1,
        [Description("Processed")]
        Processed = 2,
        [Description("Received")]
        Received = 3,
        [Description("AccelaAPI - null project")]
        NullProject = 4,
        [Description("In Process")]
        InProcess = 5,
        [Description("Plans Received")]
        PlansReceived = 6,
        [Description("No Cycle Found - FIFO error")]
        NoCycleFoundErrProcessing = 7,
        [Description("AION Project Invalid Sync")]
        SyncErrorAIONProjectInvalid = 8,
        //second time in queue as received
        [Description("Received Queue")]
        ReceivedQueue = 9,
        //unspecified error when creating/updating project
        [Description("AION Project Error Sync")]
        SyncErrorProjectError = 10

    }

    /// <summary>
    /// Used in Admin Configuration History
    /// </summary>
    public enum ConfigurationHistoryTable
    {
        [Description("User Management")]
        UserManagement = 1,
        [Description("NPA Configuration")]
        NPAConfiguration = 2,
        [Description("Holiday Configuration")]
        HolidayConfiguration = 3,
        [Description("Default Hours Configuration")]
        DefaultHoursConfiguration = 4,
        [Description("Misc Configuration")]
        MiscConfiguration = 5,
        [Description("Message Configuration")]
        MessageConfiguration = 6,
        [Description("Create Role")]
        CreateRole = 7,
        [Description("Modify Role")]
        ModifyRole = 8

    }

    /// <summary>
    /// Used in Admin Configuration History
    /// </summary>
    public enum SearchRange
    {
        [Description("24 Hours")]
        Hours24 = 1,
        [Description("3 Days")]
        Days3 = 2,
        [Description("14 Days")]
        Days14 = 3
    }

    /// <summary>
    /// LES-3767
    /// Used to display correct verbage for accepted/rejected items
    /// </summary>
    public enum AppointmentResponseStatusDisplay
    {
        [Description("Rejected")]
        Reject = 1,

        [Description("Self Scheduled")]
        Self_Schedule = 2,

        [Description("No Reply")]
        No_Reply = 3,

        [Description("Not Scheduled")]
        Not_Scheduled = 4,

        [Description("Scheduled")]
        Scheduled = 5,

        [Description("Accepted")]
        Accept = 6,

        [Description("Cancelled")]
        Cancelled = 7,

        [Description("Closed")]
        Closed = 8,

        [Description("Tentatively Scheduled")]
        Tentatively_Scheduled = 9
    }

    public struct AccelaAionDepartments
    {
        public const string Commercial_Building = "Commercial Building";
        public const string Commercial_Electrical = "Commercial Electrical";
        public const string Commercial_Mechanical = "Commercial Mechanical";
        public const string Commercial_Plumbing = "Commercial Plumbing";
        public const string Commercial_City_Fire = "Commercial City Fire";
        public const string Commercial_County_Fire = "Commercial County Fire";
        public const string Commercial_City_Zoning = "Commercial City Zoning";
        public const string Commercial_County_Zoning = "Commercial County Zoning";
        public const string CLTWTR_Backflow_Prevention = "CLTWTR Backflow Prevention";
        public const string Commercial_EHS_Food_Service = "Commercial EHS Food Service";
        public const string Commercial_EHS_Public_Pool = "Commercial EHS Public Pool";
        public const string Commercial_EHS_Facility_Lodging = "Commercial EHS Facility Lodging";
        public const string Commercial_EHS_Day_Care = "Commercial EHS Day Care";
        public const string Huntersville_Zoning = "Huntersville Zoning";
        public const string Mint_Hill_Zoning = "Mint Hill Zoning";
        public const string EHS_Residential_Pools = "EHS Residential Pools";
        public const string Residential_Building = "Residential Building";
        public const string Residential_City_Fire = "Residential City Fire";
        public const string Residential_County_Fire = "Residential County Fire";
        public const string Residential_Charlotte_Zoning = "Residential Charlotte Zoning";
        public const string Residential_County_Zoning = "Residential County Zoning";
        public const string Residential_TH_Zoning = "Residential TH Zoning";
    }

    public enum TimeAllocationType
    {
        NA = 0,
        Project = 1,
        Holiday = 2,
        WeekEnd = 3,
        Break = 4,
        [Description("NPA - Other")]
        NPA = 5,
        [Description("NPA - Personal Time")]
        NPA_PersonalTime = 6,
        [Description("NPA - Staff Meeting")]
        NPA_Staff_Meeting = 7,
        Project_Express_Blocked = 8,
        Project_Express_Reserved = 9,
        Project_Prelim = 10,
        Project_PlanReview = 11,
        Project_Facilitator_Meeting = 12
    }

    public enum BusinessDivisionRef
    {
        [Description("NA")] NA = -1,
        [Description("Building")] Building = 1,
        [Description("Electrical")] Electrical = 2,
        [Description("Mechanical")] Mechanical = 3,
        [Description("Plumbing")] Plumbing = 4,
        [Description("Zoning - County")] Zoning_County = 5,
        [Description("Zoning - City of Charlotte")] Zoning_City_of_Charlotte = 6,
        [Description("Zoning - Mint Hill")] Zoning_Mint_Hill = 7,
        [Description("Zoning - Huntersville")] Zoning_Huntersville = 8,
        [Description("Fire - County")] Fire_County = 9,
        [Description("Fire - City of Charlotte")] Fire_City_of_Charlotte = 10,
        [Description("Environmental Health: Day Care")] EHS_Day_Care = 11,
        [Description("Environmental Health: Food Service")] EHS_Food_Service = 12,
        [Description("Environmental Health: Public Pool")] EHS_Public_Pool = 13,
        [Description("Environmental Health: Facilities//Lodging")] EHS_Facilities_Lodging = 14,
        [Description("Charlotte Water Backflow")] Backflow = 15,
    }

    /// <summary>
    /// enumerates the levels for project type  scheduling
    /// cast the enum to string for the required values ("level1","level2","level3")
    ///     which are sent by Accela for each project
    /// Used by the Scheduling Lead Time report to enumerate
    /// </summary>
    public enum ProjectLevel
    {
        [Description("Level 1")] Level1 = 1,
        [Description("Level 2")] Level2 = 2,
        [Description("Level 3")] Level3 = 3
    }

}
