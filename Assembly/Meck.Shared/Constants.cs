namespace Meck.Shared
{
    /*
     IMPORTANT NOTE: ALL THESE CONTSTANTS COULD BE HAVING A BINDING WITH DATABASE TABLE VALUES.
     SO IF YOU CHANGE THE VALUES, MAKE SURE THE AFFECTED TABLE COLUMN VALUES ARE ALSO CHANGED.
     */



    public struct DepartmentNameEnumExternalRef
    {
        public const string Building = "Building";
        public const string Electrical = "Electrical";
        public const string Mechanical = "Mechanical";
        public const string Plumbing = "Plumbing";
        public const string Zone_Davidson = "Zone_Davidson";
        public const string Zone_Cornelius = "Zone_Cornelius";
        public const string Zone_Pineville = "Zone_Pineville";
        public const string Zone_Matthews = "Zone_Matthews";
        public const string Zone_Mint_Hill = "Zone_Mint_Hill";
        public const string Zone_Huntersville = "Zone_Huntersville";
        public const string Zone_UMC = "Zone_UMC";
        public const string Zone_Cty_Chrlt = "Zone_Cty_Chrlt";
        public const string Fire_Davidson = "Fire_Davidson";
        public const string Fire_Cornelius = "Fire_Cornelius";
        public const string Fire_Pineville = "Fire_Pineville";
        public const string Fire_Matthews = "Fire_Matthews";
        public const string Fire_Mint_Hill = "Fire_Mint_Hill";
        public const string Fire_Huntersville = "Fire_Huntersville";
        public const string Fire_UMC = "Fire_UMC";
        public const string Fire_Cty_Chrlt = "Fire_Cty_Chrlt";
        public const string EH_Day_Care = "EH_Day_Care";
        public const string EH_Food = "EH_Food";
        public const string EH_Pool = "EH_Pool";
        public const string EH_Facilities = "EH_Facilities";
        public const string Backflow = "Backflow";
        public const string Scheduled = "Scheduled";
        public const string NA = "NA";
    }

    public struct ReviewTypeEnumExternalRef
    {
        public const string Express = "Express";
        public const string Commercial = "Commercial";
        public const string NA = "NA";
    }

    public struct PropertyTypeExternalRef
    {
        public const string Express = "Express";
        public const string Commercial = "Commercial";
        public const string Mega_Multi_Family = "Mega Multi-Family";
        public const string Special_Projects_Team = "Special Projects Team";
        public const string Townhomes = "Townhomes";
        public const string FIFO_Small_Commercial = "FIFO: Small Commercial";
        public const string FIFO_Single_Family_Homes = "FIFO: Single Family Homes";
        public const string FIFO_Master_Plans = "FIFO: Master Plans";
        public const string FIFO_Addition_Renovation_Single_Family_Home = "FIFO: Addition/Renovation Single Family Home";
        public const string County_Shop_Drawings = "County Shop Drawings";
        public const string NA = "NA";
    }

    public struct CityNamesEnumConst
    {

        public const string Mint_Hill = "Mint_Hill";
        public const string Huntersville = "Huntersville";
        public const string Davidson = "Davidson";
        public const string Cornelius = "Cornelius";
        public const string Pineville = "Pineville";
        public const string Matthews = "Matthews";
        public const string CIty_of_Charlotte = "CIty_of_Charlotte";
        public const string NA = "NA";
    }

    public struct ProjectStatusExternalRef
    {
        public const string Application_Submitted = "Application Submitted";
        public const string Auto_Estimation_Pending = "Auto Estimation Pending";
        public const string Auto_Estimation_in_Progress = "Auto Estimation in Progress";
        public const string Ready_for_Estimator = "Ready for Estimator";
        public const string Manual_Estimation_In_Progress = "Manual Estimation In Progress";
        public const string Estimation_Complete = "Estimation Complete";
        public const string Ready_for_Scheduling = "Ready for Scheduling";
        public const string Waiting_for_Customer_Response = "Waiting for Customer Response";
        public const string Preliminary_Meeting_Required = "Preliminary Meeting Required";
        public const string Preliminary_Meeting_Complete = "Preliminary Meeting Complete";
        public const string Preliminary_Estimation_Pending = "Preliminary Estimation Pending";
        public const string Preliminary_Estimation_Complete = "Preliminary Estimation Complete";
        public const string Scope_Drawings_Required = "Scope Drawings Required";
        public const string Information_Required = "Information Required";
        public const string NA = "NA";
    }


    public struct DepartmentDivisionExternalRef
    {
        public const string Building = "Building";
        public const string Electrical = "Electrical";
        public const string Mechanical = "Mechanical";
        public const string Plumbing = "Plumbing";
        public const string Zoning = "Zoning";
        public const string Fire = "Fire";
        public const string Environmental = "Environmental";
        public const string Backflow = "Charlotte Water Backflow";
        public const string NA = "NA";
    }

    public struct DepartmentRegionExternalRef
    {
        public const string Davidson = "Davidson";
        public const string Cornelius = "Cornelius";
        public const string Pineville = "Pineville";
        public const string Matthews = "Matthews";
        public const string Mint_Hill = "Mint Hill";
        public const string Huntersville = "Huntersville";
        public const string UN_County = "Mecklenburg";
        public const string Charlotte_City = "Charlotte";
        public const string Day_Care = "Day Care";
        public const string Food_Service = "Food Service";
        public const string Public_Pool = "Public Pool";
        public const string Facilities_Lodging = "Facilities-Lodging";
        public const string NA = "NA";
    }

    public struct SystemRoleExternalRef
    {
        public const string Estimator = "Estimator";
        public const string Plan_Reviewer = "Plan_Reviewer";
        public const string Facilitator = "Facilitator";
        public const string Customer = "Customer";
        public const string Agency_Estimator = "Agency_Estimator";
        public const string Agency_Reviewer = "Agency_Reviewer";
        public const string Trade_Estimator = "Trade_Estimator";
        public const string Manager = "Manager";
        public const string MMF_Estimator = "MMF_Estimator";
        public const string Backflow = "Backflow";
        public const string EH_Day_Care = "EH_Day_Care";
        public const string Fire_County = "Fire_County";
        public const string Zone_County = "Zone_County";
        public const string Fire_City = "Fire_City";
        public const string Zone_City = "Zone_City";
        public const string Building = "Building";
        public const string NA = "NA";
        public const string View_Only = "View_Only";
        public const string External_Project_Manager = "External_Project_Manager";
    }

    public struct NoteTypeExternalRef
    {
        public const string Internal_Notes = "Internal Notes";
        public const string Gate_Notes = "Gate Notes";
        public const string Pending_Notes = "Pending Notes";
        public const string Accela_Project_Notes = "Accela Project Notes";
        public const string Exit_Meeting_Notes = "Exit Meeting Notes";
        public const string Meeting_Doc_Notes = "Meeting Document Notes";
        public const string EstimationStandardNotes = "EstimationStandardNotes";
        public const string SchedulingStandardNotes = "SchedulingStandardNotes";
        public const string SchedulingMandatoryNotes = "SchedulingMandatoryNotes";
        public const string SchedulingNotes = "SchedulingNotes";
    }

    public struct ProjectDisplayStatus
    {
        public const string None = "";
        public const string Late = "L";
        public const string Pending = "P";
        public const string Complete = "C";
        public const string CustomerResponded = "CR";
        public const string Default = "D";
        public const string AutoEstimationInProgress = "A";
        public const string AutoEstimationComplete = "AC";// use this status to acheive L since we need to idenitfy between Estimation complete and Autoestimation complete
        public const string AutoEstimationCompleteNA = "ACNA";// use this status to acheive L since we need to idenitfy between Estimation complete and Autoestimation complete
        public const string NewApplication = "N"; //This status indicates the application just received from accela and saved into AION, but not auto estimation has run on this. Auto estimation will be run only when user opens this project in manual estimation page.
    }

    public struct ProjectDisplayStatusText
    {
        public const string None = "";
        public const string Late = "Late";
        public const string Pending = "Pending";
        public const string Complete = "Complete";
        public const string CustomerResponded = "Customer Responded";
        public const string Default = "Default";
        public const string AutoEstimationInProgress = "Auto Estimation In Progress";
        public const string AutoEstimationComplete = "Auto Estimation Complete";// use this status to acheive L since we need to idenitfy between Estimation complete and Autoestimation complete
        public const string AutoEstimationCompleteNA = "Auto Estimation Complete NA";// use this status to acheive L since we need to idenitfy between Estimation complete and Autoestimation complete
        public const string NewApplication = "New Application"; //This status indicates the application just received from accela and saved into AION, but not auto estimation has run on this. Auto estimation will be run only when user opens this project in manual estimation page.
    }

    /// <summary>
    /// TradeSelectOptionConsts Enum but with string type.
    /// </summary>
    public class TradeSelectOptionConsts
    {
        private TradeSelectOptionConsts(string value) { Value = value; }

        public string Value { get; set; }

        /// <summary>
        /// "NA"
        /// </summary>
        public static TradeSelectOptionConsts NA { get { return new TradeSelectOptionConsts("NA"); } }

        /// <summary>
        /// "Manual"
        /// </summary>
        public static TradeSelectOptionConsts Manual { get { return new TradeSelectOptionConsts("Manual"); } }

        /// <summary>
        /// "Default"
        /// </summary>
        public static TradeSelectOptionConsts Default { get { return new TradeSelectOptionConsts("Default"); } }


        /// <summary>
        /// "Auto"
        /// </summary>
        public static TradeSelectOptionConsts Auto { get { return new TradeSelectOptionConsts("Auto"); } }

        /// <summary>
        /// "County"
        /// </summary>
        public static TradeSelectOptionConsts County { get { return new TradeSelectOptionConsts("County"); } }

        /// <summary>
        /// "Town"
        /// </summary>
        public static TradeSelectOptionConsts Town { get { return new TradeSelectOptionConsts("Town"); } }

        public static explicit operator TradeSelectOptionConsts(string value) { return new TradeSelectOptionConsts(value); }

        public static explicit operator string(TradeSelectOptionConsts value) { return value.Value; }

        public static explicit operator int(TradeSelectOptionConsts value)
        {
            switch (value.Value)
            {
                case "NA": return -1;
                case "Manual": return 1;
                case "Default": return 2;
                case "Auto": return 3;
                case "County": return 4;
                case "Town": return 5;
                default: return -1;
            }
        }

        public static bool operator ==(TradeSelectOptionConsts obj1, TradeSelectOptionConsts obj2) { return obj1.Value == obj2.Value; }

        public static bool operator !=(TradeSelectOptionConsts obj1, TradeSelectOptionConsts obj2) { return obj1.Value != obj2.Value; }

        public static bool operator ==(TradeSelectOptionConsts obj1, string obj2) { return obj1.Value == obj2; }

        public static bool operator !=(TradeSelectOptionConsts obj1, string obj2) { return obj1.Value != obj2; }

        public static bool operator ==(string obj1, TradeSelectOptionConsts obj2) { return obj1 == obj2.Value; }

        public static bool operator !=(string obj1, TradeSelectOptionConsts obj2) { return obj1 != obj2.Value; }

        public override bool Equals(object obj) { return this.Value == ((TradeSelectOptionConsts)obj).Value; }
    }
}
