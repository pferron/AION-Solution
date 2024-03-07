using AION.BL;

namespace AION.Manager.AccelaBusinessObjects
{
    public class AccelaPropertyTypeBO
    {
        public PropertyTypeEnums MapAccelaPropertyType(Meck.Shared.MeckDataMapping.AccelaProjectModel model)
        {
            PropertyTypeEnums ret;
            string recordType = model.RecordType;
            string propertyType = model.PropertyTypeRef;
            string reviewType = model.ReviewTypeRef;
            string typeOfWork = model.DisplayOnlyInformation.TypeOfWork;
            //if rtap, look at the property for the project type, return the original project type
            if (model.IsProjectRTAP && model.AIONOriginalProjectTypeId > 0)
            {
                return (PropertyTypeEnums)model.AIONOriginalProjectTypeId;
            }
            switch (recordType)
            {
                case "Commercial Project":
                    ret = GetCommercialProjectProjectType(reviewType, typeOfWork);
                    break;
                case "Residential Project":
                    ret = GetResidentialProjectProjectType(reviewType, typeOfWork);
                    break;
                case "Fire Shop Drawings":
                case "County Fire Shop Drawings":
                    ret = PropertyTypeEnums.County_Fire_Shop_Drawings;
                    break;
                case "Preliminary Meeting":
                    ret = GetPreliminaryMeetingProjectType(reviewType, typeOfWork, propertyType);
                    break;
                default:
                    ret = PropertyTypeEnums.NA;
                    break;
            }
            return ret;
        }
        public static PropertyTypeEnums GetResidentialProjectProjectType(string reviewType, string typeOfWork)
        {
            PropertyTypeEnums ret;
            switch (reviewType)
            {
                case "Townhouse < 3.5 Stories":
                    ret = PropertyTypeEnums.Townhomes;
                    break;
                case "Single Family":
                    if (typeOfWork == "Addition (expand footprint)" ||
                    typeOfWork == "Addition (footprint not expanded)" ||
                    typeOfWork == "Upfit (interior completion)" ||
                    typeOfWork == "Upfit (interior renovation)")
                        ret = PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home;
                    else ret = PropertyTypeEnums.FIFO_Single_Family_Homes;
                    break;
                case "Masterplan":
                    ret = PropertyTypeEnums.FIFO_Master_Plans;
                    break;
                default:
                    ret = PropertyTypeEnums.NA;
                    break;
            }
            return ret;
        }
        public static PropertyTypeEnums GetCommercialProjectProjectType(string reviewType, string typeOfWork)
        {
            PropertyTypeEnums ret;
            switch (reviewType)
            {
                case "Commercial Large":
                    ret = PropertyTypeEnums.Commercial;
                    break;
                case "Commercial Small":
                    ret = PropertyTypeEnums.FIFO_Small_Commercial;
                    break;
                case "Mega Multi Family":
                    ret = PropertyTypeEnums.Mega_Multi_Family;
                    break;
                case "Special Projects Team":
                    ret = PropertyTypeEnums.Special_Projects_Team;
                    break;
                default:
                    ret = PropertyTypeEnums.NA;
                    break;
            }
            return ret;
        }
        public static PropertyTypeEnums GetPreliminaryMeetingProjectType(string reviewType, string typeOfWork, string propertyType)
        {
            PropertyTypeEnums ret;
            switch (reviewType)
            {
                case "Commercial":
                case "Commercial Large":
                    ret = PropertyTypeEnums.Commercial;
                    break;
                case "Commercial Small":
                    ret = PropertyTypeEnums.FIFO_Small_Commercial;
                    break;
                case "Mega Multi Family":
                    ret = PropertyTypeEnums.Mega_Multi_Family;
                    break;
                case "Special Events":
                case "Special Projects Team":
                    ret = PropertyTypeEnums.Special_Projects_Team;
                    break;
                case "Residential Townhouse":
                case "Townhouse < 3.5 Stories":
                    ret = PropertyTypeEnums.Townhomes;
                    break;
                case "Single Family":
                    if (typeOfWork == "Addition (expand footprint)" ||
                    typeOfWork == "Addition (footprint not expanded)" ||
                    typeOfWork == "Upfit (interior completion)" ||
                    typeOfWork == "Upfit (interior renovation)")
                        ret = PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home;
                    else ret = PropertyTypeEnums.FIFO_Single_Family_Homes;
                    break;
                case "Residential Masterplan":
                case "Masterplan":
                    ret = PropertyTypeEnums.FIFO_Master_Plans;
                    break;
                case "Fire Shop Drawings":
                case "County Fire Shop Drawings":
                    ret = PropertyTypeEnums.County_Fire_Shop_Drawings;
                    break;
                case "Residential":
                    ret = GetPreliminaryResidential(propertyType);
                    break;
                default:
                    ret = PropertyTypeEnums.NA;
                    break;
            }
            return ret;
        }

        public static PropertyTypeEnums GetPreliminaryResidential(string propertyType)
        {
            PropertyTypeEnums ret;
            switch (propertyType)
            {
                case "1-2 Family - => 3.5 Stories":
                    ret = PropertyTypeEnums.FIFO_Single_Family_Homes;
                    break;
                case "TH, LFS - => 3.5 Stories":
                    ret = PropertyTypeEnums.Townhomes;
                    break;
                default:
                    ret = PropertyTypeEnums.Residential;
                    break;
            }
            return ret;
        }
        public string MapPropertyTypeForDisplayOnly(Meck.Shared.MeckDataMapping.AccelaProjectModel model)
        {
            string RecordType = model.RecordType;

            string propertyTypeForDisplayOnly;
            switch (RecordType)
            {
                case "Commercial Project":
                case "Commercial RTAP":
                case "Residential RTAP":
                case "Fire Shop Drawings":
                case "County Fire Shop Drawings":
                case "Preliminary Meeting":
                    propertyTypeForDisplayOnly = model.PropertyTypeRef;
                    break;
                case "Residential Project":
                    propertyTypeForDisplayOnly = model.ReviewTypeRef;
                    break;
                default:
                    propertyTypeForDisplayOnly = model.PropertyTypeRef;
                    break;
            }

            return propertyTypeForDisplayOnly;
        }

    }
}