using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AION.BL.Test
{
    [TestClass]
    public class DefaultEstimationHourModelBOTests
    {
        private DefaultEstimationHourModelBO _bo = new DefaultEstimationHourModelBO();
        private int _id;
        private int _departmentid;
        private int _propertytypeid;
        private DepartmentNameEnums _departmentnameenum;
        private PropertyTypeEnums _propertytypeenums;

        [TestInitialize]
        public void TestInitialize()
        {
            _id = 0;
            _departmentid = 1;
            _propertytypeid = 3;
            _departmentnameenum = DepartmentNameEnums.Building;
            _propertytypeenums = PropertyTypeEnums.Mega_Multi_Family;
        }
        [TestMethod]
        public void GetInstance1ReturnsObjNotNull()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;
            var id = _id;
            var defaultestimationhour = _bo.GetInstance(id);
            Assert.IsNotNull(defaultestimationhour);
        }
        [TestMethod]
        public void GetInstance1ReturnsObjWithCorrectID()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;
            var id = _id;
            var defaultestimationhour = _bo.GetInstance(id);
            Assert.IsTrue(defaultestimationhour.ID == id);

        }
        [TestMethod]
        public void GetInstance2ReturnsObjNull()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;
            //GetInstance(DepartmentNameEnums departmentNameEnum, PropertyTypeEnums propertyTypeEnum)
            DepartmentNameEnums departmentnameenum = _departmentnameenum;
            PropertyTypeEnums propertytypeenums = _propertytypeenums;
            var defaultestimationhour = _bo.GetInstance(departmentnameenum, propertytypeenums);
            Assert.IsNotNull(defaultestimationhour);
        }

        [TestMethod]
        public void GetInstance3ReturnsObjNull()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;
            //DefaultEstimationHour GetInstance(int departmentID, int propertyTypeID)
            var departmentid = _departmentid;
            var propertytypeid = _propertytypeid;
            var defaultestimationhour = _bo.GetInstance(departmentid, propertytypeid);
            Assert.IsNotNull(defaultestimationhour);

        }

        [TestMethod]
        public void RefreshListIsTrue()
        {
            var isrefresh = _bo.RefreshList();

            Assert.IsTrue(isrefresh);
        }
    }
}
