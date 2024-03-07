using AION.BL.BusinessObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AION.BL.Test
{
    [TestClass]
    public class DepartmentModelBOTests
    {
        private DepartmentTypeEnum _departmentType;
        private DepartmentDivisionEnum _divisionenum;
        private DepartmentRegionEnum _regionenum;
        private string _accelaDivisionRef;
        private string _accelaRegionRef;

        private DepartmentNameEnums _departmentNameEnum;
        private Mock<DepartmentModelBO> _departmentmodelbo;
        [TestInitialize]
        public void TestInitialize()
        {
            _departmentType = DepartmentTypeEnum.Agency;
            _accelaDivisionRef = "Fire";
            _accelaRegionRef = "Huntersville";
            _divisionenum = DepartmentDivisionEnum.Backflow;
            _regionenum = DepartmentRegionEnum.Huntersville;

            _departmentNameEnum = DepartmentNameEnums.Backflow;
            _departmentmodelbo = new Mock<DepartmentModelBO>();
        }
        //[TestMethod]
        public void GetInstanceByEnumsIsNotNull()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;
            var department = _departmentmodelbo.Object.GetInstance(_departmentType, _divisionenum, _regionenum);

            Assert.IsNotNull(department);

        }
        //[TestMethod]
        public void GetInstanceByDepartmentNameEnumIsNotNull()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;
            var department = _departmentmodelbo.Object.GetInstance(_departmentNameEnum);

            Assert.IsNotNull(department);

        }
        //[TestMethod]
        public void GetInstanceIsNotNull()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;
            var department = _departmentmodelbo.Object.GetInstance(_departmentType, _accelaDivisionRef, _accelaRegionRef);

            Assert.IsNotNull(department);

        }
    }
}
