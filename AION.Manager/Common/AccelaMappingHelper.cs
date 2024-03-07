using Meck.Shared.MeckDataMapping;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AION.BL.Helpers
{
    public class AccelaMappingHelper
    {
        public string AccelaPropertyTypeToTaskId(PropertyTypeEnums propertyType, bool isProjectRTAP, bool isPreliminary = false)
        {
            string taskID = string.Empty;

            if (isPreliminary) return "CE_PRELIM-REVIEW.cTASK.cACTIVATION";

            switch (propertyType)
            {
                case PropertyTypeEnums.Express:
                    if (isProjectRTAP)
                    {
                        taskID = "CE_CRTAP-REVIEW.cTASK.cACTIVATION";
                    }
                    else
                    {
                        taskID = "CE_COM-REVIEW.cTASK.cACTIVATION";
                    }
                    break;

                case PropertyTypeEnums.Commercial:
                    if (isProjectRTAP)
                    {
                        taskID = "CE_CRTAP-REVIEW.cTASK.cACTIVATION";
                    }
                    else
                    {
                        taskID = "CE_COM-REVIEW.cTASK.cACTIVATION";
                    }
                    break;

                case PropertyTypeEnums.Residential:
                    if (isProjectRTAP)
                    {
                        taskID = "CE_RRTAP-REVIEW.cTASK.cACTIVATION";
                    }
                    else
                    {
                        taskID = "CE_RES-REVIEW.cTASK.cACTIVATION";
                    }
                    break;

                case PropertyTypeEnums.Mega_Multi_Family:

                    if (isProjectRTAP)
                    {
                        taskID = "CE_CRTAP-REVIEW.cTASK.cACTIVATION";
                    }
                    else
                    {
                        taskID = "CE_COM-REVIEW.cTASK.cACTIVATION";

                    }
                    break;

                case PropertyTypeEnums.Special_Projects_Team:

                    if (isProjectRTAP)
                    {
                        taskID = "CE_CRTAP-REVIEW.cTASK.cACTIVATION";
                    }
                    else
                    {
                        taskID = "CE_COM-REVIEW.cTASK.cACTIVATION";

                    }
                    break;

                case PropertyTypeEnums.FIFO_Small_Commercial:

                    if (isProjectRTAP)
                    {
                        taskID = "CE_CRTAP-REVIEW.cTASK.cACTIVATION";
                    }
                    else
                    {
                        taskID = "CE_COM-REVIEW.cTASK.cACTIVATION";

                    }
                    break;

                case PropertyTypeEnums.FIFO_Master_Plans:

                    if (isProjectRTAP)
                    {
                        taskID = "CE_RRTAP-REVIEW.cTASK.cACTIVATION";
                    }
                    else
                    {
                        taskID = "CE_RES-REVIEW.cTASK.cACTIVATION";
                    }
                    break;

                case PropertyTypeEnums.FIFO_Single_Family_Homes:

                    if (isProjectRTAP)
                    {
                        taskID = "CE_RRTAP-REVIEW.cTASK.cACTIVATION";
                    }
                    else
                    {
                        taskID = "CE_RES-REVIEW.cTASK.cACTIVATION";
                    }
                    break;

                case PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home:

                    if (isProjectRTAP)
                    {
                        taskID = "CE_RRTAP-REVIEW.cTASK.cACTIVATION";
                    }
                    else
                    {
                        taskID = "CE_RES-REVIEW.cTASK.cACTIVATION";

                    }
                    break;

                case PropertyTypeEnums.Townhomes:

                    if (isProjectRTAP)
                    {
                        taskID = "CE_RRTAP-REVIEW.cTASK.cACTIVATION";
                    }
                    else
                    {
                        taskID = "CE_RES-REVIEW.cTASK.cACTIVATION";
                    }
                    break;


                case PropertyTypeEnums.County_Fire_Shop_Drawings:
                    if (isProjectRTAP)
                    {
                        taskID = "CE_CRTAP-REVIEW.cTASK.cACTIVATION";
                    }
                    else
                    {
                        taskID = "CE_CFSD-REVIEW.cTASK.cACTIVATION";

                    }

                    break;

                default:
                    break;
            }
            return taskID;
        }

        public string AccelaPropertyTypeToMeetingID(PropertyTypeEnums propertyType, bool IsProjectRTAP, bool IsPreliminary = false)
        {
            string taskID = string.Empty;

            if (IsPreliminary) return "CE_PRELIM-MEETINGS";

            switch (propertyType)
            {
                case PropertyTypeEnums.Express:
                    if (!IsProjectRTAP)
                    {
                        taskID = "CE_COM-MEETINGS";
                    }
                    else
                    {
                        taskID = "CE_CRTAP-MEETINGS";
                    }
                    break;

                case PropertyTypeEnums.Commercial:
                    if (!IsProjectRTAP)
                    {
                        taskID = "CE_COM-MEETINGS";
                    }
                    else
                    {
                        taskID = "CE_CRTAP-MEETINGS";
                    }
                    break;

                case PropertyTypeEnums.Mega_Multi_Family:
                    {
                        taskID = "CE_COM-MEETINGS";
                        break;
                    }

                case PropertyTypeEnums.Special_Projects_Team:
                    {
                        taskID = "CE_COM-MEETINGS";
                        break;
                    }

                case PropertyTypeEnums.FIFO_Small_Commercial:
                    {
                        taskID = "CE_COM-MEETINGS";
                        break;
                    }

                case PropertyTypeEnums.FIFO_Master_Plans:
                    {
                        taskID = "CE_RES-MEETINGS";
                        break;
                    }

                case PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home:
                    {
                        taskID = "CE_RES-MEETINGS";
                        break;
                    }

                case PropertyTypeEnums.FIFO_Single_Family_Homes:
                    {
                        taskID = "CE_RES-MEETINGS";
                        break;
                    }

                case PropertyTypeEnums.Townhomes:
                    {
                        if (!IsProjectRTAP)
                        {
                            taskID = "CE_RES-MEETINGS";

                        }
                        else
                        {
                            taskID = "CE_RRTAP-MEETINGS";
                        }
                        break;
                    }

                case PropertyTypeEnums.County_Fire_Shop_Drawings:
                    taskID = "CE_CFSD-MEETINGS";

                    break;

                default:
                    break;
            }

            return taskID;
        }

        public List<string> AccelaWorkFlowTaskToDepartment(string workFlowTasks)
        {
            //This can possible be turned into a struct or class, right now the list that comes from Accela DOES NOT SEND underscores between the department names according to the
            //documentation that was provided during development of LES-187

            List<string> businessRefList = new List<string>();
            List<String> listStrLineElements;
            listStrLineElements = workFlowTasks.Split(',').ToList();

            foreach (string task in listStrLineElements)
            {

                if ((task == "Commercial Building") || (task == "Residential Building"))
                {
                    businessRefList.Add("1");
                }
                else if ((task == "Commercial Electric"))
                {
                    businessRefList.Add("2");
                }
                else if ((task == "Commercial Mechanical"))
                {
                    businessRefList.Add("3");
                }
                else if ((task == "Commercial Plumbing"))
                {
                    businessRefList.Add("4");
                }
                else if ((task == "Davidson Zoning"))
                {
                    businessRefList.Add("5");
                }
                else if ((task == "Cornelius Zoning"))
                {
                    businessRefList.Add("6");
                }
                else if ((task == "Pineville Zoning"))
                {
                    businessRefList.Add("7");
                }
                else if ((task == "Matthews Zoning"))
                {
                    businessRefList.Add("8");
                }
                else if ((task == "Mint Hill Zoning"))
                {
                    businessRefList.Add("9");
                }
                else if ((task == "Huntersville Zoning"))
                {
                    businessRefList.Add("10");
                }
                else if ((task == "Unincorporated Mecklenburg County Zoning"))
                {
                    businessRefList.Add("11");
                }
                else if ((task == "City Of Charlotte Zoning"))
                {
                    businessRefList.Add("12");
                }
                else if ((task == "Commercial EHS Day Care"))
                {
                    businessRefList.Add("21");
                }
                else if ((task == "Commercial EHS Food Service"))
                {
                    businessRefList.Add("22");
                }
                else if ((task == "Commercial EHS Public Pool"))
                {
                    businessRefList.Add("23");
                }
                else if ((task == "Commercial EHS Facility Lodging"))
                {
                    businessRefList.Add("24");
                }
                else if ((task == "CLTWTR Backflow Prevention"))
                {
                    businessRefList.Add("25");
                }


            }
            return businessRefList;
        }

        /// <summary>
        /// Returns the correct lookup value for the Project Estimation Engine
        /// returns UPFITRTAP if not "new construction" in string
        /// UPFITRTAP
        /// NEWCONSTRUCTION
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string GetTypeOfWorkValue(string input)
        {

            if (!string.IsNullOrWhiteSpace(input) && input.ToLower().Contains("new construction"))
            {
                return TypeOfWork.NewConstruction.ToStringValue();
            }

            else
            {
                return TypeOfWork.UpFitRTAP.ToStringValue();
            }
        }

        public static PrelimTypeOfWorkObj BuildPrelimTypeOfWork(string input)
        {
            PrelimTypeOfWorkObj obj = new PrelimTypeOfWorkObj();

            if (string.IsNullOrWhiteSpace(input)) return obj;

            string[] inputList = input.Split(';');

            foreach (string task in inputList)
            {
                string[] keypair = task.Split('|');

                switch (keypair[0].Trim())
                {
                    case "TypeOfWorkUpfit":
                        obj.TypeOfWorkUpfit = keypair[1];
                        break;
                    case "TypeOfWorkNewConFull":
                        obj.TypeOfWorkNewConFull = keypair[1];
                        break;
                    case "TypeOfWorkPreEngMetalBldgOption":
                        obj.TypeOfWorkPreEngMetalBldgOption = keypair[1];
                        break;
                    case "TypeOfWorkDayCare":
                        obj.TypeOfWorkDayCare = keypair[1];
                        break;
                    case "TypeOfWorkNewConShellFootFoundPrev":
                        obj.TypeOfWorkNewConShellFootFoundPrev = keypair[1];
                        break;
                    case "TypeOfWorkNewConShellFootFoundCore":
                        obj.TypeOfWorkNewConShellFootFoundCore = keypair[1];
                        break;
                    case "TypeOfWorkProCert":
                        obj.TypeOfWorkProCert = keypair[1];
                        break;
                    case "TypeOfWorkChangeOfUse":
                        obj.TypeOfWorkChangeOfUse = keypair[1];
                        break;
                    case "TypeOfWorkNewConFootFound":
                        obj.TypeOfWorkNewConFootFound = keypair[1];
                        break;
                    case "TypeOfWorkNewConShellFootFound":
                        obj.TypeOfWorkNewConShellFootFound = keypair[1];
                        break;
                    case "TypeOfWorkPreEngMetalBldg":
                        obj.TypeOfWorkPreEngMetalBldg = keypair[1];
                        break;
                    case "TypeOfWorkNewConsShellFootFoundCorePrev":
                        obj.TypeOfWorkNewConsShellFootFoundCorePrev = keypair[1];
                        break;
                    case "TypeOfWorkAddition":
                        obj.TypeOfWorkAddition = keypair[1];
                        break;
                    case "TypeOfWorkPrevOccupiedBldg":
                        obj.TypeOfWorkPrevOccupiedBldg = keypair[1];
                        break;
                    default:
                        break;
                }
            }

            return obj;
        }

        public static PrelimMeetingAgendaObj BuildPrelimAgenda(string input)
        {
            PrelimMeetingAgendaObj obj = new PrelimMeetingAgendaObj();

            if (string.IsNullOrWhiteSpace(input)) return obj;

            string[] inputList = input.Split(';');

            foreach (string task in inputList)
            {
                string[] keypair = task.Split('|');

                switch (keypair[0].Trim())
                {
                    case "AgendaAmenities":
                        obj.AgendaAmenities = keypair[1];
                        break;
                    case "AgendaUpfitType":
                        obj.AgendaUpfitType = keypair[1];
                        break;
                    case "AgendaElectricalSystemType":
                        obj.AgendaElectricalSystemType = keypair[1];
                        break;
                    case "AgendaPlumbingType":
                        obj.AgendaPlumbingType = keypair[1];
                        break;
                    case "AgendaAmenitiesDesign":
                        obj.AgendaAmenitiesDesign = keypair[1];
                        break;
                    case "AgendaParkingGarage":
                        obj.AgendaParkingGarage = keypair[1];
                        break;
                    case "Agenda":
                        obj.Agenda = keypair[1];
                        break;
                    case "AgendaSpecialWaste":
                        obj.AgendaSpecialWaste = keypair[1];
                        break;
                    default:
                        break;
                }
            }

            return obj;
        }

        public static PrelimBIMProjectDeliveryObj BuildPrelimBIMPDM(string input)
        {
            PrelimBIMProjectDeliveryObj obj = new PrelimBIMProjectDeliveryObj();

            if (string.IsNullOrWhiteSpace(input)) return obj;

            string[] inputList = input.Split(';');

            foreach (string task in inputList)
            {
                string[] keypair = task.Split('|');

                switch (keypair[0].Trim())
                {
                    case "ProjectDeliveryMethodDesignBuild":
                        obj.ProjectDeliveryMethodDesignBuild = keypair[1];
                        break;
                    case "ProjectDeliveryMethodCMOwnerAgent":
                        obj.ProjectDeliveryMethodCMOwnerAgent = keypair[1];
                        break;
                    case "BIMDisciplinesElec":
                        obj.BIMDisciplinesElec = keypair[1];
                        break;
                    case "ProjectDeliveryMethodFastTrack":
                        obj.ProjectDeliveryMethodFastTrack = keypair[1];
                        break;
                    case "ProjectDeliveryMethodOther":
                        obj.ProjectDeliveryMethodOther = keypair[1];
                        break;
                    case "ProjectDeliveryMethodDesignBidBuild":
                        obj.ProjectDeliveryMethodDesignBidBuild = keypair[1];
                        break;
                    case "ProjectDeliveryMethodIPDOrVariation":
                        obj.ProjectDeliveryMethodIPDOrVariation = keypair[1];
                        break;
                    case "PDMDisciplinesDesignAssistMech":
                        obj.PDMDisciplinesDesignAssistMech = keypair[1];
                        break;
                    case "PDMDisciplinesDesignBuildArchStruct":
                        obj.PDMDisciplinesDesignBuildArchStruct = keypair[1];
                        break;
                    case "BIMDisciplinesArch":
                        obj.BIMDisciplinesArch = keypair[1];
                        break;
                    case "ProjectDeliveryMethodDesignAssist":
                        obj.ProjectDeliveryMethodDesignAssist = keypair[1];
                        break;
                    case "PDMDisciplinesDesignBuildMech":
                        obj.PDMDisciplinesDesignBuildMech = keypair[1];
                        break;
                    case "PDMDisciplinesDesignBuildPlumb":
                        obj.PDMDisciplinesDesignBuildPlumb = keypair[1];
                        break;
                    case "PDMDisciplinesDesignAssistArchStruct":
                        obj.PDMDisciplinesDesignAssistArchStruct = keypair[1];
                        break;
                    case "PDMDisciplinesDesignAssistElec":
                        obj.PDMDisciplinesDesignAssistElec = keypair[1];
                        break;
                    case "ProjectIsBim:Yes":
                        obj.ProjectIsBim = keypair[1];
                        break;
                    case "PDMDisciplinesDesignBuildElec":
                        obj.PDMDisciplinesDesignBuildElec = keypair[1];
                        break;
                    case "BIMDisciplinesStruct":
                        obj.BIMDisciplinesStruct = keypair[1];
                        break;
                    case "PDMDisciplinesDesignAssistPlumb":
                        obj.PDMDisciplinesDesignAssistPlumb = keypair[1];
                        break;
                    case "BIMDisciplinesMech":
                        obj.BIMDisciplinesMech = keypair[1];
                        break;
                    case "BIMDisciplinesPlumb":
                        obj.BIMDisciplinesPlumb = keypair[1];
                        break;
                    case "PDMOtherDescription":
                        obj.PDMOtherDescription = keypair[1];
                        break;

                    default:
                        break;
                }
            }

            return obj;
        }

        public static PrelimGeneralInfoObj BuildPrelimGeneralInfo(string input)
        {
            PrelimGeneralInfoObj obj = new PrelimGeneralInfoObj();

            if (string.IsNullOrWhiteSpace(input)) return obj;

            string[] inputList = input.Split(';');

            foreach (string task in inputList)
            {
                string[] keypair = task.Split('|');

                switch (keypair[0].Trim())
                {
                    case "BuildingCode":
                        obj.BuildingCode = keypair[1];
                        break;
                    case "PropertyType":
                        obj.PropertyType = keypair[1];
                        break;
                    case "ReviewType":
                        obj.ReviewType = keypair[1];
                        break;
                }
            }

            return obj;
        }

        public static PrelimMeetingDetailObj BuildPrelimMeetingDetail(string input)
        {
            PrelimMeetingDetailObj obj = new PrelimMeetingDetailObj();

            if (string.IsNullOrWhiteSpace(input)) return obj;

            string[] inputList = input.Split(';');

            foreach (string task in inputList)
            {
                string[] keypair = task.Split('|');

                switch (keypair[0].Trim())
                {
                    case "RequestedBeginDateRange":
                        obj.RequestedBeginDateRange = string.IsNullOrWhiteSpace(keypair[1]) ? DateTime.MinValue : DateTime.Parse(keypair[1]);
                        break;
                    case "RequestedEndDateRange":
                        obj.RequestedEndDateRange = string.IsNullOrWhiteSpace(keypair[1]) ? DateTime.MinValue : DateTime.Parse(keypair[1]);
                        break;
                    case "NumberOfAttendees":
                        obj.NumberOfAttendees = string.IsNullOrWhiteSpace(keypair[1]) ? 0 : int.Parse(keypair[1]);
                        break;
                }
            }

            return obj;
        }

        public static PrelimProjectSummaryObj BuildPrelimProjectSummary(string input)
        {
            PrelimProjectSummaryObj obj = new PrelimProjectSummaryObj();

            if (string.IsNullOrWhiteSpace(input)) return obj;

            string[] inputList = input.Split(';');

            foreach (string task in inputList)
            {
                string[] keypair = task.Split('|');

                switch (keypair[0].Trim())
                {
                    case "IncludesAfforableOrWorkforceHousing":
                        obj.IncludesAfforableOrWorkforceHousing = keypair[1];
                        break;
                    case "WorkforceHousingUnits":
                        obj.WorkforceHousingUnits = keypair[1];
                        break;
                    case "AfforableHousingUnits":
                        obj.AfforableHousingUnits = keypair[1];
                        break;
                }
            }

            return obj;
        }

        public static PrelimProposedWorkObj BuildPrelimProposedScopeOfWork(string input)
        {
            PrelimProposedWorkObj obj = new PrelimProposedWorkObj();

            if (string.IsNullOrWhiteSpace(input)) return obj;

            string[] inputList = input.Split(';');

            foreach (string task in inputList)
            {
                string[] keypair = task.Split('|');

                switch (keypair[0].Trim())
                {
                    case "ProposedScopeOfWork":
                        obj.ProposedScopeOfWork = keypair[1];
                        break;
                }
            }

            return obj;
        }

        public static PrelimSystemInfoObj BuildPrelimSystemInfo(string input)
        {
            PrelimSystemInfoObj obj = new PrelimSystemInfoObj();

            if (string.IsNullOrWhiteSpace(input)) return obj;

            string[] inputList = input.Split(';');

            foreach (string task in inputList)
            {
                string[] keypair = task.Split('|');

                switch (keypair[0].Trim())
                {
                    case "CurrentReviewCycle":
                        obj.CurrentReviewCycle = string.IsNullOrWhiteSpace(keypair[1]) ? 0 : int.Parse(keypair[1]);
                        break;
                    case "ClonedFromProjectNumber":
                        obj.ClonedFromProjectNumber = keypair[1];
                        break;
                    case "GISAddressCode":
                        obj.GISAddressCode = keypair[1];
                        break;
                    case "IsRTAP":
                        obj.IsRTAP = keypair[1];
                        break;
                    case "EstimatedFees":
                        obj.EstimatedFees = string.IsNullOrWhiteSpace(keypair[1]) ? 0.0M : decimal.Parse(keypair[1]);
                        break;
                    case "TaxJurisdiction":
                        obj.TaxJurisdiction = keypair[1];
                        break;
                    case "FIFO":
                        obj.FIFO = keypair[1];
                        break;
                    case "RecordLocation":
                        obj.RecordLocation = keypair[1];
                        break;
                }
            }

            return obj;
        }

    }
}

