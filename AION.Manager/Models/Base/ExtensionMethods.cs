using AION.BL;
using Meck.Shared;

namespace System
{
    public static class PropertyTypeEnumsExtensions
    {
        public static PropertyTypeEnums CreateInstance(this PropertyTypeEnums duration, string ExternalRefID)
        {
            switch (ExternalRefID)
            {
                case PropertyTypeExternalRef.Express:
                    return PropertyTypeEnums.Express;
                case PropertyTypeExternalRef.Commercial:
                    return PropertyTypeEnums.Commercial;
                case PropertyTypeExternalRef.Mega_Multi_Family:
                    return PropertyTypeEnums.Mega_Multi_Family;
                case PropertyTypeExternalRef.Special_Projects_Team:
                    return PropertyTypeEnums.Special_Projects_Team;
                case PropertyTypeExternalRef.Townhomes:
                    return PropertyTypeEnums.Townhomes;
                case PropertyTypeExternalRef.FIFO_Small_Commercial:
                    return PropertyTypeEnums.FIFO_Small_Commercial;
                case PropertyTypeExternalRef.FIFO_Single_Family_Homes:
                    return PropertyTypeEnums.FIFO_Single_Family_Homes;
                case PropertyTypeExternalRef.FIFO_Master_Plans:
                    return PropertyTypeEnums.FIFO_Master_Plans;
                case PropertyTypeExternalRef.FIFO_Addition_Renovation_Single_Family_Home:
                    return PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home;
                case PropertyTypeExternalRef.County_Shop_Drawings:
                    return PropertyTypeEnums.County_Fire_Shop_Drawings;
                default:
                    return PropertyTypeEnums.NA;
            }
        }

        public static DepartmentRegionEnum CreateInstance(this DepartmentRegionEnum refobj, string departmentRegionExternalRef)
        {
            switch (departmentRegionExternalRef)
            {
                case DepartmentRegionExternalRef.Davidson:
                    return DepartmentRegionEnum.Davidson;
                case DepartmentRegionExternalRef.Cornelius:
                    return DepartmentRegionEnum.Cornelius;
                case DepartmentRegionExternalRef.Pineville:
                    return DepartmentRegionEnum.Pineville;
                case DepartmentRegionExternalRef.Matthews:
                    return DepartmentRegionEnum.Matthews;
                case DepartmentRegionExternalRef.Mint_Hill:
                    return DepartmentRegionEnum.Mint_Hill;
                case DepartmentRegionExternalRef.Huntersville:
                    return DepartmentRegionEnum.Huntersville;
                case DepartmentRegionExternalRef.UN_County:
                    return DepartmentRegionEnum.UN_County;
                case DepartmentRegionExternalRef.Charlotte_City:
                    return DepartmentRegionEnum.Charlotte_City;
                default:
                    return DepartmentRegionEnum.NA;
            }
        }

        public static ReviewTypeEnum CreateInstance(this ReviewTypeEnum duration, string ExternalRefID)
        {
            switch (ExternalRefID)
            {
                case ReviewTypeEnumExternalRef.Express:
                    return ReviewTypeEnum.Express;
                case ReviewTypeEnumExternalRef.Commercial:
                    return ReviewTypeEnum.OnSchdule;
                default:
                    return ReviewTypeEnum.NA;
            }
        }

        public static NoteTypeEnum CreateInstance(this NoteTypeEnum duration, string ExternalRefID)
        {
            switch (ExternalRefID)
            {
                case NoteTypeExternalRef.Accela_Project_Notes:
                    return NoteTypeEnum.AccelaProjectNotes;
                case NoteTypeExternalRef.Gate_Notes:
                    return NoteTypeEnum.GateNotes;
                case NoteTypeExternalRef.Internal_Notes:
                    return NoteTypeEnum.InternalNotes;
                case NoteTypeExternalRef.Pending_Notes:
                    return NoteTypeEnum.PendingNotes;
                case NoteTypeExternalRef.Exit_Meeting_Notes:
                    return NoteTypeEnum.ExitMeetingNotes;
                case NoteTypeExternalRef.Meeting_Doc_Notes:
                    return NoteTypeEnum.MeetingDocNotes;
                case NoteTypeExternalRef.EstimationStandardNotes:
                    return NoteTypeEnum.EstimationStandardNotes;
                case NoteTypeExternalRef.SchedulingStandardNotes:
                    return NoteTypeEnum.SchedulingStandardNotes;
                case NoteTypeExternalRef.SchedulingMandatoryNotes:
                    return NoteTypeEnum.SchedulingMandatoryNotes;
                case NoteTypeExternalRef.SchedulingNotes:
                    return NoteTypeEnum.SchedulingNotes;
                default:
                    return NoteTypeEnum.NA;
            }
        }

        public static DepartmentTypeEnum CreateInstance(this DepartmentTypeEnum refobj, string BusinessRefID)
        {
            TradeEnums trade = new TradeEnums();
            AgencyEnums agency = new AgencyEnums();
            if (Enum.TryParse(BusinessRefID, true, out trade))
            {
                if (Enum.IsDefined(typeof(TradeEnums), trade))
                    return DepartmentTypeEnum.Trade;
            }
            if (Enum.TryParse(BusinessRefID, true, out agency))
            {
                if (Enum.IsDefined(typeof(AgencyEnums), agency))
                    return DepartmentTypeEnum.Agency;
            }
            return DepartmentTypeEnum.NA;

        }
        public static DepartmentDivisionEnum CreateInstance(this DepartmentDivisionEnum refobj, int BusinessRefID)
        {
            DepartmentNameEnums department = (DepartmentNameEnums)BusinessRefID;
            switch (department)
            {
                case DepartmentNameEnums.Building:
                    return DepartmentDivisionEnum.Building;
                case DepartmentNameEnums.Electrical:
                    return DepartmentDivisionEnum.Electrical;
                case DepartmentNameEnums.Mechanical:
                    return DepartmentDivisionEnum.Mechanical;
                case DepartmentNameEnums.Plumbing:
                    return DepartmentDivisionEnum.Plumbing;
                case DepartmentNameEnums.Zone_Davidson:
                case DepartmentNameEnums.Zone_Cornelius:
                case DepartmentNameEnums.Zone_Pineville:
                case DepartmentNameEnums.Zone_Matthews:
                case DepartmentNameEnums.Zone_Mint_Hill:
                case DepartmentNameEnums.Zone_Huntersville:
                case DepartmentNameEnums.Zone_UMC:
                case DepartmentNameEnums.Zone_Cty_Chrlt:
                case DepartmentNameEnums.Zone_County:
                    return DepartmentDivisionEnum.Zoning;
                case DepartmentNameEnums.Fire_Davidson:
                case DepartmentNameEnums.Fire_Cornelius:
                case DepartmentNameEnums.Fire_Pineville:
                case DepartmentNameEnums.Fire_Matthews:
                case DepartmentNameEnums.Fire_Mint_Hill:
                case DepartmentNameEnums.Fire_Huntersville:
                case DepartmentNameEnums.Fire_UMC:
                case DepartmentNameEnums.Fire_Cty_Chrlt:
                case DepartmentNameEnums.Fire_County:
                    return DepartmentDivisionEnum.Fire;
                case DepartmentNameEnums.EH_Day_Care:
                case DepartmentNameEnums.EH_Food:
                case DepartmentNameEnums.EH_Pool:
                case DepartmentNameEnums.EH_Facilities:
                    return DepartmentDivisionEnum.Environmental;
                case DepartmentNameEnums.Backflow:
                    return DepartmentDivisionEnum.Backflow;
            }
            return DepartmentDivisionEnum.NA;
        }
        public static AppointmentRecurrenceRefEnum CreateInstance(this AppointmentRecurrenceRefEnum refobj, string day, string week)
        {
            string item = "";
            if (week == "Daily" || week == "Yearly" || week == "Once")
                item = week;
            else
                item = week + "_" + day;
            AppointmentRecurrenceRefEnum rec = new AppointmentRecurrenceRefEnum();
            if (Enum.TryParse(item, true, out rec))
            {
                return rec;
            }
            return AppointmentRecurrenceRefEnum.NA;
        }
        public static RecurrenceEnum CreateInstance(this RecurrenceEnum refobj, AppointmentRecurrenceRefEnum refEnum)
        {
            switch (refEnum)
            {
                case AppointmentRecurrenceRefEnum.NA:
                case AppointmentRecurrenceRefEnum.Once:
                    return RecurrenceEnum.Once;
                case AppointmentRecurrenceRefEnum.First_Monday:
                case AppointmentRecurrenceRefEnum.First_Tuesday:
                case AppointmentRecurrenceRefEnum.First_Wednesday:
                case AppointmentRecurrenceRefEnum.First_Thursday:
                case AppointmentRecurrenceRefEnum.First_Friday:
                    return RecurrenceEnum.First;
                case AppointmentRecurrenceRefEnum.Second_Monday:
                case AppointmentRecurrenceRefEnum.Second_Tuesday:
                case AppointmentRecurrenceRefEnum.Second_Wednesday:
                case AppointmentRecurrenceRefEnum.Second_Thursday:
                case AppointmentRecurrenceRefEnum.Second_Friday:
                    return RecurrenceEnum.Second;
                case AppointmentRecurrenceRefEnum.Third_Monday:
                case AppointmentRecurrenceRefEnum.Third_Tuesday:
                case AppointmentRecurrenceRefEnum.Third_Wednesday:
                case AppointmentRecurrenceRefEnum.Third_Thursday:
                case AppointmentRecurrenceRefEnum.Third_Friday:
                    return RecurrenceEnum.Third;
                case AppointmentRecurrenceRefEnum.Fourth_Monday:
                case AppointmentRecurrenceRefEnum.Fourth_Tuesday:
                case AppointmentRecurrenceRefEnum.Fourth_Wednesday:
                case AppointmentRecurrenceRefEnum.Fourth_Thursday:
                case AppointmentRecurrenceRefEnum.Fourth_Friday:
                    return RecurrenceEnum.Fourth;
                case AppointmentRecurrenceRefEnum.Last_Monday:
                case AppointmentRecurrenceRefEnum.Last_Tuesday:
                case AppointmentRecurrenceRefEnum.Last_Wednesday:
                case AppointmentRecurrenceRefEnum.Last_Thursday:
                case AppointmentRecurrenceRefEnum.Last_Friday:
                    return RecurrenceEnum.Last;
                case AppointmentRecurrenceRefEnum.Weekly_Monday:
                case AppointmentRecurrenceRefEnum.Weekly_Tuesday:
                case AppointmentRecurrenceRefEnum.Weekly_Wednesday:
                case AppointmentRecurrenceRefEnum.Weekly_Thursday:
                case AppointmentRecurrenceRefEnum.Weekly_Friday:
                    return RecurrenceEnum.Weekly;
                case AppointmentRecurrenceRefEnum.Yearly:
                    return RecurrenceEnum.Yearly;
                case AppointmentRecurrenceRefEnum.Daily:
                    return RecurrenceEnum.Daily;
                default:
                    return RecurrenceEnum.Once;
            }
        }
        public static DayOfWeek? CreateInstance(this DayOfWeek refobj, AppointmentRecurrenceRefEnum refEnum)
        {
            switch (refEnum)
            {
                case AppointmentRecurrenceRefEnum.First_Monday:
                case AppointmentRecurrenceRefEnum.Second_Monday:
                case AppointmentRecurrenceRefEnum.Third_Monday:
                case AppointmentRecurrenceRefEnum.Fourth_Monday:
                case AppointmentRecurrenceRefEnum.Last_Monday:
                case AppointmentRecurrenceRefEnum.Weekly_Monday:
                    return DayOfWeek.Monday;
                case AppointmentRecurrenceRefEnum.First_Tuesday:
                case AppointmentRecurrenceRefEnum.Second_Tuesday:
                case AppointmentRecurrenceRefEnum.Third_Tuesday:
                case AppointmentRecurrenceRefEnum.Fourth_Tuesday:
                case AppointmentRecurrenceRefEnum.Last_Tuesday:
                case AppointmentRecurrenceRefEnum.Weekly_Tuesday:
                    return DayOfWeek.Tuesday;
                case AppointmentRecurrenceRefEnum.First_Wednesday:
                case AppointmentRecurrenceRefEnum.Second_Wednesday:
                case AppointmentRecurrenceRefEnum.Third_Wednesday:
                case AppointmentRecurrenceRefEnum.Fourth_Wednesday:
                case AppointmentRecurrenceRefEnum.Last_Wednesday:
                case AppointmentRecurrenceRefEnum.Weekly_Wednesday:
                    return DayOfWeek.Wednesday;
                case AppointmentRecurrenceRefEnum.First_Thursday:
                case AppointmentRecurrenceRefEnum.Second_Thursday:
                case AppointmentRecurrenceRefEnum.Third_Thursday:
                case AppointmentRecurrenceRefEnum.Fourth_Thursday:
                case AppointmentRecurrenceRefEnum.Last_Thursday:
                case AppointmentRecurrenceRefEnum.Weekly_Thursday:
                    return DayOfWeek.Thursday;
                case AppointmentRecurrenceRefEnum.First_Friday:
                case AppointmentRecurrenceRefEnum.Second_Friday:
                case AppointmentRecurrenceRefEnum.Third_Friday:
                case AppointmentRecurrenceRefEnum.Fourth_Friday:
                case AppointmentRecurrenceRefEnum.Last_Friday:
                case AppointmentRecurrenceRefEnum.Weekly_Friday:
                    return DayOfWeek.Friday;
                case AppointmentRecurrenceRefEnum.Once:
                case AppointmentRecurrenceRefEnum.Daily:
                case AppointmentRecurrenceRefEnum.Yearly:
                    return null;
                default:
                    return DayOfWeek.Monday;
            }
        }
    }

}
