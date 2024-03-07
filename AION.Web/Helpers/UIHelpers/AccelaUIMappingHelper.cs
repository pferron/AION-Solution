using System;

namespace AION.Web.Helpers
{
    public class AccelaUIMappingHelper
    {
        public static string ChangeChecked(string input)
        {
            if (!string.IsNullOrWhiteSpace(input) && input.Equals("CHECKED", StringComparison.OrdinalIgnoreCase))
            {
                return "Yes";
            }
            return "No";
        }

        public static string FireDetails(string strFireDetails)
        {

            string businessRefList = "";

            if (string.IsNullOrWhiteSpace(strFireDetails)) return "None";

            string[] inputList = strFireDetails.Split(';');

            foreach (string task in inputList)
            {
                if (task.ToUpper().Trim().Contains("CFSHOPEXTINGUISHERSYSTEM"))
                {
                    string fireString = After(task, ":");
                    if (fireString.Trim() != string.Empty)
                    {
                        businessRefList += fireString + ";";
                    }
                }
                switch (task.ToUpper().Trim())
                {
                    case "CFSHOPPLANREVIEWTYPESPRINKLER:CHECKED":
                        businessRefList += "Sprinkler Plans;";
                        break;
                    case "CFSHOPPLANREVIEWTYPEFIREALARM:CHECKED":
                        businessRefList += "Fire Alarm Plans;";
                        break;
                    case "CFSHOPPLANREVIEWTYPEHYDRAULICCALCS:CHECKED":
                        businessRefList += "Hydraulic Calculations;";
                        break;
                    case "CFSHOPPLANREVIEWTYPEALTFIREEXTINGUISHER:CHECKED":
                        businessRefList += "Alternative Fire Extinguisher System;";
                        break;
                    //case "CFSHOPEXTINGUISHERSYSTEM:TEST":
                    //   businessRefList += "Extinguisher System;";
                    //   break;
                    case "FIREDRAWINGSINCLUDED:YES":
                        businessRefList += "Submittal includes fire alarm and or sprinkler shop drawings;";
                        break;
                    case "FIREBUILDINGSPRINKLED:YES":
                        businessRefList += "Building is sprinklered;";
                        break;
                    case "FIRESTANDPIPE:YES":
                        businessRefList += "Building has a standpipe;";
                        break;
                    case "FIREFIREPUMP:YES":
                        businessRefList += "Building has a fire pump;";
                        break;
                    case "FIREELEVATOR:YES":
                        businessRefList += "Building has an elevator;";
                        break;
                    case "FIRESMOKEDETECTOR:YES":
                        businessRefList += "Building has a smoke detection system;";
                        break;
                    case "FIREFIREALARM:YES":
                        businessRefList += "Building has a fire alarm system;";
                        break;
                    default:
                        break;
                }
            }
            if (businessRefList.Length == 0) businessRefList = "None";

            return businessRefList;
        }
        public static string After(string value, string a)
        {
            int posA = value.LastIndexOf(a);
            if (posA == -1)
            {
                return "";
            }
            int adjustedPosA = posA + a.Length;
            if (adjustedPosA >= value.Length)
            {
                return "";
            }
            return value.Substring(adjustedPosA);
        }

        public static string WaterSewerDetails(string strWaterSewerDetails)
        {

            string businessRefList = "";

            if (string.IsNullOrWhiteSpace(strWaterSewerDetails)) return "None";

            string[] inputList = strWaterSewerDetails.Split(';');

            foreach (string task in inputList)
            {
                switch (task.ToUpper().Trim())
                {
                    case "EXISTINGSEPTICTANK:CHECKED":
                        businessRefList += "Have an existing septic tank system;";
                        break;
                    case "NEWSEPTICTANK:CHECKED":
                        businessRefList += "Installing a new septic tank system;";
                        break;
                    case "EXISTINGPUBLICWATER:CHECKED":
                        businessRefList += "Have existing Public (City) Water;";
                        break;
                    case "NEWPUBLICWATER:CHECKED":
                        businessRefList += "Installing new Public (City) Water;";
                        break;
                    case "EXISTINGPUBLICSEWER:CHECKED":
                        businessRefList += "Have an existing Public (City) Sewer;";
                        break;
                    case "NEWPUBLICSEWER:CHECKED":
                        businessRefList += "Installing a new Public (City) Sewer;";
                        break;
                    case "EXISTINGWELL:CHECKED":
                        businessRefList += "Have an existing well;";
                        break;
                    case "NEWWELL:CHECKED":
                        businessRefList += "Installing a new well;";
                        break;
                    default:
                        break;
                }
            }
            if (businessRefList.Length == 0) businessRefList = "None";

            return businessRefList;
        }

        public static string DayCareDetails(string strDayCareDetails)
        {

            string businessRefList = "";

            if (string.IsNullOrWhiteSpace(strDayCareDetails)) return "None";

            string[] inputList = strDayCareDetails.Split(';');

            foreach (string task in inputList)
            {
                switch (task.ToUpper().Trim())
                {
                    case "HDADULTDAYCARE:CHECKED":
                        businessRefList += "Adult Day Care;";
                        break;
                    case "HDCHILDDAYCARE:CHECKED":
                        businessRefList += "Child Day Care;";
                        break;
                    default:
                        break;
                }
            }
            if (businessRefList.Length == 0) businessRefList = "None";

            return businessRefList;
        }

        public static string HealthDeptDetails(string strHealthDeptDetails)
        {

            string businessRefList = "";

            if (string.IsNullOrWhiteSpace(strHealthDeptDetails)) return "None";

            string[] inputList = strHealthDeptDetails.Split(';');

            foreach (string task in inputList)
            {
                switch (task.ToUpper().Trim())
                {
                    case "RESTAURANT:CHECKED":
                        businessRefList += "Restaurant;";
                        break;
                    case "BARSERVICE:CHECKED":
                        businessRefList += "Bar Service w/o Food;";
                        break;
                    case "HDSEAFOOD:CHECKED":
                        businessRefList += "Seafood/Deli;";
                        break;
                    case "HDLODGING:CHECKED":
                        businessRefList += "Lodging/Hotel;";
                        break;
                    case "HDADULTDAYCARE:CHECKED":
                        businessRefList += "Adult Day Care;";
                        break;
                    case "HDMEATMARKET:CHECKED":
                        businessRefList += "Meat Market;";
                        break;
                    case "HDWATERRECREATION_POOL:CHECKED":
                        businessRefList += "Water Recreation/Pool;";
                        break;
                    case "HDCHILDDAYCARE:CHECKED":
                        businessRefList += "Child Day Care;";
                        break;
                    case "HDOTHER:CHECKED":
                        businessRefList += "Other;";
                        break;
                    default:
                        break;
                }
            }
            if (businessRefList.Length == 0) businessRefList = "None";

            return businessRefList;
        }

        public static string CityOfCharlotteDetails(string cityOfCharlotteDetails)
        {
            string businessRefList = "";

            if (string.IsNullOrWhiteSpace(cityOfCharlotteDetails)) return "None";

            string[] inputList = cityOfCharlotteDetails.Split(';');

            foreach (string task in inputList)
            {
                switch (task.ToUpper().Trim())
                {
                    case "CITYADDINGTREEPLANTING:CHECKED":
                        businessRefList += "Adding tree planting.;";
                        break;
                    case "CITYCHANGINGDRIVEWAYETC:CHECKED":
                        businessRefList += "Changing or adding driveways, roadway storm drainage systems, detention system and stormwater BMP’s.;";
                        break;
                    case "CITYADJOINSPUBLICSTREET:CHECKED":
                        businessRefList += "Building on a site that adjoins a new public street.;";
                        break;
                    case "CITYNEWPUBLICORPRIVATESTREET:CHECKED":
                        businessRefList += "Building a new public or Private Street.;";
                        break;
                    case "CITYADDING11ORMOREPARKINGSPACE:CHECKED":
                        businessRefList += "Adding 11 or more parking spaces.;";
                        break;
                    case "CITYPLANNEDMULTIFAMILY:CHECKED":
                        businessRefList += "Building a Planned Multi-Family Project.;";
                        break;
                    case "CITYADDINGIMPERVIOUSAREA:CHECKED":
                        businessRefList += "Adding impervious areas for new buildings, parking lots, sidewalks.;";
                        break;
                    case "CITYZONEDURBAN:CHECKED":
                        businessRefList += "Site is zoned: UMUD, MUDD, TOD, PED, TS, RE-3.;";
                        break;
                    case "CITYCHANGINGFACADEOVER10PERCENT:CHECKED":
                        businessRefList += "Changing the building façade more that 10%.;";
                        break;
                    case "CITYGRADINGMORETHANONEACRE:CHECKED":
                        businessRefList += "Grading of sites involving one acre of land disturbance (grading permit required).;";
                        break;
                    case "CITYBUILDINGOVER1000SQFT:CHECKED":
                        businessRefList += "Building an addition or a new building that is over 1000 square feet.;";
                        break;
                    case "CITYBUILDINGOVER5PERCENTSQFT:CHECKED":
                        businessRefList += "Building an addition or a new building that is more than 5% of the existing square footage.;";
                        break;
                    case "CITYADDINGTREEPROTECTION:CHECKED":
                        businessRefList += "Adding tree protection to existing trees on site during construction.;";
                        break;
                    default:
                        break;
                }
            }
            if (businessRefList.Length == 0) businessRefList = "None";

            return businessRefList;
        }

        public static string TypeOfWorkDetails(string input)
        {
            string businessRefList = "";

            if (string.IsNullOrWhiteSpace(input)) return "None";

            string[] inputList = input.Split(';');

            foreach (string task in inputList)
            {
                switch (task.Trim())
                {
                    case "TypeOfWorkUpfit|CHECKED":
                        businessRefList += "Upfit(Interior Completion);";
                        break;
                    case "TypeOfWorkNewConFull|CHECKED":
                        businessRefList += "New Construction(Full);";
                        break;
                    case "TypeOfWorkPreEngMetalBldgOption|CHECKED":
                        businessRefList += "Work on Previously Occupied Existing Building;";
                        break;
                    case "TypeOfWorkDayCare|CHECKED":
                        businessRefList += "Day care;";
                        break;
                    case "TypeOfWorkNewConShellFootFoundPrev|CHECKED":
                        businessRefList += "New Construction(Shell w/ Footing / Foundation previously approved);";
                        break;
                    case "TypeOfWorkNewConShellFootFoundCore|CHECKED":
                        businessRefList += "New Construction(Shell/ Core w / Footing / Foundation);";
                        break;
                    case "TypeOfWorkProCert|CHECKED":
                        businessRefList += "Professional Certification;";
                        break;
                    case "TypeOfWorkChangeOfUse|CHECKED":
                        businessRefList += "Change of Use;";
                        break;
                    case "TypeOfWorkNewConFootFound|CHECKED":
                        businessRefList += "New Construction(Footing/ Foundation);";
                        break;
                    case "TypeOfWorkNewConShellFootFound|CHECKED":
                        businessRefList += "New Construction(Shell w/ Footing / Foundation);";
                        break;
                    case "TypeOfWorkPreEngMetalBldg|CHECKED":
                        businessRefList += "Pre - Engineered Metal Buildings;";
                        break;
                    case "TypeOfWorkNewConsShellFootFoundCorePrev|CHECKED":
                        businessRefList += "/New Construction(Shell/ Core w / Footing / Foundation previously approved);";
                        break;
                    case "TypeOfWorkAddition|CHECKED":
                        businessRefList += "Addition;";
                        break;
                    case "TypeOfWorkPrevOccupiedBldg|CHECKED":
                        businessRefList += "Work on Previously Occupied Existing Building;";
                        break;
                    default:
                        break;
                }
            }
            if (businessRefList.Length == 0) businessRefList = "None";

            return businessRefList;
        }

        public static string AgendaDetails(string input)
        {

            string businessRefList = "";

            if (string.IsNullOrWhiteSpace(input)) return "None";

            string[] inputList = input.Split(';');

            foreach (string task in inputList)
            {
                string[] keypair = task.Split('|');

                switch (keypair[0].Trim())
                {
                    case "AgendaAmenities":
                        businessRefList += "Are there amenities? ";
                        businessRefList += keypair[1];
                        businessRefList += " ;";
                        break;
                    case "AgendaUpfitType":
                        businessRefList += "Is the upfit a renovation or is it an upfit completion? ";
                        businessRefList += keypair[1];
                        businessRefList += " ;";
                        break;
                    case "AgendaElectricalSystemType":
                        businessRefList += "Is there an emergency electrical system or a stand by electrical system? ";
                        businessRefList += keypair[1];
                        businessRefList += " ;";
                        break;
                    case "AgendaPlumbingType":
                        businessRefList += "Is there conventional or engineered plumbing? ";
                        businessRefList += keypair[1];
                        businessRefList += " ;";
                        break;
                    case "AgendaAmenitiesDesign":
                        businessRefList += "Is the design conceptual or schematic? ";
                        businessRefList += keypair[1];
                        businessRefList += " ;";
                        break;
                    case "AgendaParkingGarage":
                        businessRefList += "Is there a parking garage? ";
                        businessRefList += keypair[1];
                        businessRefList += " ;";
                        break;
                    case "Agenda":
                        businessRefList += "Agenda : ";
                        businessRefList += keypair[1];
                        businessRefList += " ;";
                        break;
                    case "AgendaSpecialWaste":
                        businessRefList += "Is there special waste? ";
                        businessRefList += keypair[1];
                        businessRefList += " ;";
                        break;
                    default:
                        break;
                }
            }
            if (businessRefList.Length == 0) businessRefList = "None";

            return businessRefList;
        }

        public static string BIMDetails(string input)
        {
            string businessRefList = "";

            if (string.IsNullOrWhiteSpace(input)) return "None";

            string[] inputList = input.Split(';');

            foreach (string task in inputList)
            {
                string[] keypair = task.Split('|');


                if (keypair[0].Equals("PDMOtherDescription"))
                {
                    businessRefList += "PDM Other Description: " + keypair[1] + ";";
                }

                switch (task.Trim())
                {
                    case "ProjectDeliveryMethodDesignBuild|CHECKED":
                        businessRefList += "Design / Build;";
                        break;
                    case "ProjectDeliveryMethodCMOwnerAgent|CHECKED":
                        businessRefList += "CM Owner Agent;";
                        break;
                    case "BIMDisciplinesElec|CHECKED":
                        businessRefList += "BIM - Electrical;";
                        break;
                    case "ProjectDeliveryMethodFastTrack|CHECKED":
                        businessRefList += "Fast Track;";
                        break;
                    case "ProjectDeliveryMethodOther|CHECKED":
                        businessRefList += "Other;";
                        break;
                    case "ProjectDeliveryMethodDesignBidBuild|CHECKED":
                        businessRefList += "Design / Bid / Build;";
                        break;
                    case "ProjectDeliveryMethodIPDOrVariation|CHECKED":
                        businessRefList += "IPD or a variation on it;";
                        break;
                    case "PDMDisciplinesDesignAssistMech|CHECKED":
                        businessRefList += "Design/Assist - Mechanical;";
                        break;
                    case "PDMDisciplinesDesignBuildArchStruct|CHECKED":
                        businessRefList += "Design/Build - Architectural/Structural ;";
                        break;
                    case "BIMDisciplinesArch|CHECKED":
                        businessRefList += "BIM - Architectural;";
                        break;
                    case "ProjectDeliveryMethodDesignAssist|CHECKED":
                        businessRefList += "Design/Assist;";
                        break;
                    case "PDMDisciplinesDesignBuildMech|CHECKED":
                        businessRefList += "Design/Build - Mechanical;";
                        break;
                    case "PDMDisciplinesDesignBuildPlumb|CHECKED":
                        businessRefList += "Design/Build - Plumbing;";
                        break;
                    case "PDMDisciplinesDesignAssistArchStruct|CHECKED":
                        businessRefList += "Design/Assist - Architectural/Structural;";
                        break;
                    case "PDMDisciplinesDesignAssistElec|CHECKED":
                        businessRefList += "Design/Assist - Electrical;";
                        break;
                    case "ProjectIsBim:Yes":
                        businessRefList += "Is any of the project being produced using BIM (Building Information Modelling)?;";
                        break;
                    case "PDMDisciplinesDesignBuildElec|CHECKED":
                        businessRefList += "Design/Build - Electrical;";
                        break;
                    case "BIMDisciplinesStruct|CHECKED":
                        businessRefList += "BIM - Structural;";
                        break;
                    case "PDMDisciplinesDesignAssistPlumb|CHECKED":
                        businessRefList += "Design/Assist - Plumbing;";
                        break;
                    case "BIMDisciplinesMech|CHECKED":
                        businessRefList += "BIM - Mechanical;";
                        break;
                    case "BIMDisciplinesPlumb|CHECKED":
                        businessRefList += "BIM - Plumbing;";
                        break;
                    default:
                        break;
                }
            }
            if (businessRefList.Length == 0) businessRefList = "None";

            return businessRefList;
        }

        public static string PrelimProjectSummaryDetails(string input)
        {

            string businessRefList = "";

            if (string.IsNullOrWhiteSpace(input)) return "None";

            string[] inputList = input.Split(';');

            foreach (string task in inputList)
            {
                string[] keypair = task.Split('|');

                switch (keypair[0].Trim())
                {
                    case "IncludesAfforableOrWorkforceHousing":
                        //just return this value to answer the question
                        //businessRefList += "IncludesAfforableOrWorkforceHousing? ";
                        businessRefList += keypair[1];
                        businessRefList += " ;";
                        break;
                    //case "WorkforceHousingUnits":
                    //    businessRefList += "WorkforceHousingUnits: ";
                    //    businessRefList += keypair[1];
                    //    businessRefList += " ;";
                    //    break;
                    //case "AfforableHousingUnits":
                    //    businessRefList += "AfforableHousingUnits: ";
                    //    businessRefList += keypair[1];
                    //    businessRefList += " ;";
                    //    break;

                    default:
                        break;
                }
            }
            if (businessRefList.Length == 0) businessRefList = "None";

            return businessRefList;
        }
    }
}