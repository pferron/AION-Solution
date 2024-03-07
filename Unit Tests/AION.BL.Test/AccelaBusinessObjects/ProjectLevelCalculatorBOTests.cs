using AION.Manager.AccelaBusinessObjects;
using AION.Manager.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AION.BL.Test.AccelaBusinessObjects
{
    [TestClass]
    public class ProjectLevelCalculatorBOTests
    {
        [DataTestMethod]
        [DataRow(PropertyTypeEnums.Commercial, 20000, true, 3, "Business", "3")]
        [DataRow(PropertyTypeEnums.Commercial, 20000, true, 0, "Business", "3")]
        [DataRow(PropertyTypeEnums.Commercial, 0, true, 5, "Business", "3")]
        [DataRow(PropertyTypeEnums.Townhomes, 50000, true, 3, "Business", "1")]
        [DataRow(PropertyTypeEnums.FIFO_Single_Family_Homes, 20000, false, 3, "Business", "1")]
        [DataRow(PropertyTypeEnums.FIFO_Master_Plans, 20000, false, 3, "Business", "1")]
        [DataRow(PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home, 20000, false, 3, "Business", "1")]
        [DataRow(PropertyTypeEnums.Express, 6000, false, 1, "Assembly", "1")]
        [DataRow(PropertyTypeEnums.Express, 15000, false, 1, "Assembly", "2")]
        [DataRow(PropertyTypeEnums.Express, 25000, false, 1, "Assembly", "3")]
        [DataRow(PropertyTypeEnums.Commercial, 10000, false, 1, "Business", "1")]
        [DataRow(PropertyTypeEnums.Commercial, 40000, false, 2, "Business", "2")]
        [DataRow(PropertyTypeEnums.Commercial, 70000, false, 1, "Business", "3")]
        [DataRow(PropertyTypeEnums.Commercial, 40000, false, 5, "Business", "3")]
        [DataRow(PropertyTypeEnums.Mega_Multi_Family, 3000, false, 1, "Residential", "1")]
        [DataRow(PropertyTypeEnums.Mega_Multi_Family, 10000, false, 3, "Residential", "2")]
        [DataRow(PropertyTypeEnums.Mega_Multi_Family, 80000, false, 3, "Residential", "2")]
        [DataRow(PropertyTypeEnums.Mega_Multi_Family, 80000, false, 5, "Residential", "3")]
        [DataRow(PropertyTypeEnums.Mega_Multi_Family, 80000, false, null, "Residential", "3")]
        [DataRow(PropertyTypeEnums.Mega_Multi_Family, null, false, 5, "Residential", "3")]
        [DataRow(PropertyTypeEnums.Mega_Multi_Family, null, false, null, "Residential", "3")]
        public void ValidateThatCalculatorDeterminesTheCorrectProjectLevel(PropertyTypeEnums propertyType, int sqrFootage, bool isHighRise, int numberOfStories, string occupancyType, string expectedLevel)
        {
            var projectLevelCalculatorParms = new ProjectLevelCalculatorParms()
            {
                PropertyType = propertyType,
                SqrFootage = sqrFootage,
                IsHighRise = isHighRise,
                NumberOfStories = numberOfStories,
                OccupancyType = occupancyType
            };

            var projectLevelCalculator = new ProjectLevelCalculatorBO(projectLevelCalculatorParms);
            string actualLevel = projectLevelCalculator.SetProjectLevel();

            Assert.AreEqual(expectedLevel, actualLevel);
        }
    }

}
