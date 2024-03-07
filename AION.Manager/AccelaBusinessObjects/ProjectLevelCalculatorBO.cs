using AION.BL;
using AION.Manager.Models;

namespace AION.Manager.AccelaBusinessObjects
{
    public class ProjectLevelCalculatorBO
    {
        private PropertyTypeEnums _propertyType;
        private bool? _isHighRise;
        private int? _sqrFootage;
        private int? _numberOfStories;
        private string _occupancyType;

        public ProjectLevelCalculatorBO(ProjectLevelCalculatorParms parms)
        {
            _propertyType = parms.PropertyType;
            _sqrFootage = parms.SqrFootage;
            _isHighRise = parms.IsHighRise;
            _numberOfStories = parms.NumberOfStories;
            _occupancyType = parms.OccupancyType;
        }

        public string SetProjectLevel()
        {
            switch (_propertyType)
            {
                case PropertyTypeEnums.Townhomes:
                case PropertyTypeEnums.FIFO_Single_Family_Homes:
                case PropertyTypeEnums.FIFO_Master_Plans:
                case PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home:
                    return "1";

                case PropertyTypeEnums.County_Fire_Shop_Drawings:
                    return "3";

                case PropertyTypeEnums.Express:
                case PropertyTypeEnums.Commercial:
                case PropertyTypeEnums.Mega_Multi_Family:
                case PropertyTypeEnums.Special_Projects_Team:
                case PropertyTypeEnums.FIFO_Small_Commercial:
                    return CalculateProjectLevel();

                default:
                    return "1";
            }
        }

        private string CalculateProjectLevel()
        {
            if (ShouldDefaultForNullValues())
            {
                return "3";
            }

            if (_isHighRise != null && _isHighRise == true)
            {
                return "3";
            }

            if ((_isHighRise == null || _isHighRise == false) && _sqrFootage == null && _numberOfStories == null)
            {
                return "3";
            }

            if ((_isHighRise == null || _isHighRise == false) && _sqrFootage > 0 && _numberOfStories == null)
            {
                return "3";
            }

            if ((_isHighRise == null || _isHighRise == false) && _sqrFootage == null && _numberOfStories > 0)
            {
                return "3";
            }

            if (!string.IsNullOrEmpty(_occupancyType))
            {
                switch (_occupancyType.ToUpper())
                {
                    case "BUSINESS":
                        return CalculateProjectLevel(20000, 60000, 4);

                    case "INSTITUTIONAL":
                        return CalculateProjectLevel(7500, 10000, 3);

                    case "RESIDENTIAL":
                        return CalculateProjectLevel(7500, int.MaxValue, 3);

                    case "ASSEMBLY":
                        return CalculateProjectLevel(7500, 20000, null);

                    case "STORAGE":
                        return CalculateProjectLevel(20000, 60000, 4);

                    case "EDUCATIONAL":
                        return CalculateProjectLevel(7500, 20000, 2);

                    case "UTILITYMISCELLANEOUS":
                        return "1";

                    case "MERCANTILE":
                        return CalculateProjectLevel(20000, 60000, 4);

                    case "HAZARDOUS":
                        return CalculateProjectLevel(3000, 20000, 2);

                    case "FACTORY INDUSTRIAL":
                        return CalculateProjectLevel(20000, 60000, 4);

                    case "1/2 FAMILY DWELLINGS, TOWNHOMES":
                        return "1";

                    default:
                        return "1";
                }
            }
            else
            {
                return "1";
            }
        }

        private string CalculateProjectLevel(
            int levelOneSqrFootageMax,
            int levelTwoSqrFootageMax,
            int? multiStoryMax)
        {
            string projectLevel = "3";

            if (_numberOfStories == 1
                && _sqrFootage <= levelOneSqrFootageMax
                && (multiStoryMax == null || _numberOfStories <= multiStoryMax))
            {
                projectLevel = "1";
            }
            if (((int)_sqrFootage > levelOneSqrFootageMax && (int)_sqrFootage <= levelTwoSqrFootageMax)
                && (multiStoryMax == null || (_numberOfStories >= 1 && _numberOfStories <= multiStoryMax)))
            {
                projectLevel = "2";
            }

            return projectLevel;
        }

        private bool ShouldDefaultForNullValues()
        {
            if (_isHighRise == null && _sqrFootage == null && _numberOfStories == null)
            {
                return true;
            }

            return false;
        }
    }
}