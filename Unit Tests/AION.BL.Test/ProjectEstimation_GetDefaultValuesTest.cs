using AION.BL.Adapters;
using AION.BL.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;

namespace AION.BL.Test
{
    [TestClass]
    public class ProjectEstimation_GetDefaultValuesTest
    {
        Mock<ProjectEstimationAdapter> _estimation;
        ProjectEstimation _project;

        [TestInitialize]
        public void TestInitialize()
        {
            //
            _estimation = new Mock<ProjectEstimationAdapter>();
            _project = new ProjectEstimation
            {
                Trades = new List<ProjectTrade>(),
                Agencies = new List<ProjectAgency>(),
                AccelaProjectRefId = "1"
            };
        }

        [TestMethod]
        [Ignore]
        public void ProjectGetAgencyDefaultHoursExpressFireCityOfCharlotte()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;
            _project = new ProjectEstimation
            {
                Trades = new List<ProjectTrade>(),
                Agencies = new List<ProjectAgency>(),
                AccelaProjectRefId = "1"
            };
            var expectedhours = 1.5M;
            decimal? result = 0.0M;
            var propertytype = PropertyTypeEnums.Express;
            var departmentname = DepartmentNameEnums.Fire_Cty_Chrlt;
            _project.AccelaPropertyType = propertytype;
            _project.Agencies.Add(new ProjectAgency
            {
                DepartmentInfo = departmentname
            });
            _estimation.Object.PerformAutoEstimation(_project);
            result = _project.Agencies[0].EstimationHours;

            Assert.IsTrue(result == expectedhours);
        }

        //[TestMethod]
        public void ProjectGetAgencyDefaultHoursOnScheduleFireCityOfCharlotte()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;
            _project = new ProjectEstimation
            {
                Trades = new List<ProjectTrade>(),
                Agencies = new List<ProjectAgency>(),
                AccelaProjectRefId = "1"
            };
            var expectedhours = 1.5M;
            decimal? result = 0.0M;
            var propertytype = PropertyTypeEnums.Commercial;
            var departmentname = DepartmentNameEnums.Fire_Cty_Chrlt;
            _project.AccelaPropertyType = propertytype;
            _project.Agencies.Add(new ProjectAgency
            {
                DepartmentInfo = departmentname
            });
            _estimation.Object.PerformAutoEstimation(_project);
            result = _project.Agencies[0].EstimationHours;

            Assert.IsTrue(result == expectedhours);
        }

        [TestMethod]
        public void ProjectGetAgencyDefaultHoursMMFZoneMintHill()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;
            _project = new ProjectEstimation
            {
                Trades = new List<ProjectTrade>(),
                Agencies = new List<ProjectAgency>(),
                AccelaProjectRefId = "1"
            };
            var expectedhours = 1.0M;
            decimal? result = 0.0M;
            var propertytype = PropertyTypeEnums.Mega_Multi_Family;
            var departmentname = DepartmentNameEnums.Zone_Mint_Hill;
            _project.AccelaPropertyType = propertytype;
            _project.Agencies.Add(new ProjectAgency
            {
                DepartmentInfo = departmentname

            });
            _project.DisplayOnlyInformation.TypeOfWork = "test";
            _estimation.Object.PerformAutoEstimation(_project);
            result = _project.Agencies[0].EstimationHours;

            Assert.IsTrue(result == expectedhours);
        }
        [TestMethod]
        [Ignore]

        public void ProjectGetAgencyDefaultHoursMMFZoneHuntersville()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;
            _project = new ProjectEstimation
            {
                Trades = new List<ProjectTrade>(),
                Agencies = new List<ProjectAgency>(),
                AccelaProjectRefId = "1"
            };
            var expectedhours = 1.0M;
            decimal? result = 0.0M;
            var propertytype = PropertyTypeEnums.Mega_Multi_Family;
            var departmentname = DepartmentNameEnums.Zone_Huntersville;
            _project.AccelaPropertyType = propertytype;
            _project.Agencies.Add(new ProjectAgency
            {
                DepartmentInfo = departmentname
            });
            _project.DisplayOnlyInformation.TypeOfWork = "test";
            _estimation.Object.PerformAutoEstimation(_project);
            result = _project.Agencies[0].EstimationHours;

            Assert.IsTrue(result == expectedhours);
        }
        //[TestMethod]
        public void ProjectGetAgencyDefaultHoursMMFFireCityOfCharlotte()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;
            _project = new ProjectEstimation
            {
                Trades = new List<ProjectTrade>(),
                Agencies = new List<ProjectAgency>(),
                AccelaProjectRefId = "1"
            };
            var expectedhours = 1.5M;
            decimal? result = 0.0M;
            var propertytype = PropertyTypeEnums.Mega_Multi_Family;
            var departmentname = DepartmentNameEnums.Fire_Cty_Chrlt;
            _project.AccelaPropertyType = propertytype;
            _project.Agencies.Add(new ProjectAgency
            {
                DepartmentInfo = departmentname
            });
            _project.DisplayOnlyInformation.TypeOfWork = "test";
            _estimation.Object.PerformAutoEstimation(_project);
            result = _project.Agencies[0].EstimationHours;

            Assert.IsTrue(result == expectedhours);
        }

        [TestMethod]
        public void ProjectGetAgencyDefaultHoursSpecialProjectsTeamZoneMintHill()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;
            _project = new ProjectEstimation
            {
                Trades = new List<ProjectTrade>(),
                Agencies = new List<ProjectAgency>(),
                AccelaProjectRefId = "1"
            };
            var expectedhours = 1.0M;
            decimal? result = 0.0M;
            var propertytype = PropertyTypeEnums.Special_Projects_Team;
            var departmentname = DepartmentNameEnums.Zone_Mint_Hill;
            _project.AccelaPropertyType = propertytype;
            _project.Agencies.Add(new ProjectAgency
            {
                DepartmentInfo = departmentname
            });
            _estimation.Object.PerformAutoEstimation(_project);
            result = _project.Agencies[0].EstimationHours;

            Assert.IsTrue(result == expectedhours);
        }
        [TestMethod]
        public void ProjectGetAgencyDefaultHoursSpecialProjectsTeamZoneHuntersville()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;
            _project = new ProjectEstimation
            {
                Trades = new List<ProjectTrade>(),
                Agencies = new List<ProjectAgency>(),
                AccelaProjectRefId = "1"
            };
            var expectedhours = 1.0M;
            decimal? result = 0.0M;
            var propertytype = PropertyTypeEnums.Special_Projects_Team;
            var departmentname = DepartmentNameEnums.Zone_Huntersville;
            _project.AccelaPropertyType = propertytype;
            _project.Agencies.Add(new ProjectAgency
            {
                DepartmentInfo = departmentname
            });
            _estimation.Object.PerformAutoEstimation(_project);
            result = _project.Agencies[0].EstimationHours;

            Assert.IsTrue(result == expectedhours);
        }
        [TestMethod]
        public void ProjectGetAgencyDefaultHoursTownhomesZoneMintHill()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;
            _project = new ProjectEstimation
            {
                Trades = new List<ProjectTrade>(),
                Agencies = new List<ProjectAgency>(),
                AccelaProjectRefId = "1"
            };
            var expectedhours = 1.0M;
            decimal? result = 0.0M;
            var propertytype = PropertyTypeEnums.Townhomes;
            var departmentname = DepartmentNameEnums.Zone_Mint_Hill;
            _project.AccelaPropertyType = propertytype;
            _project.Agencies.Add(new ProjectAgency
            {
                DepartmentInfo = departmentname
            });
            _estimation.Object.PerformAutoEstimation(_project);
            result = _project.Agencies[0].EstimationHours;

            Assert.IsTrue(result == expectedhours);
        }
        [TestMethod]
        public void ProjectGetAgencyDefaultHoursTownhomesZoneHuntersville()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;
            _project = new ProjectEstimation
            {
                Trades = new List<ProjectTrade>(),
                Agencies = new List<ProjectAgency>(),
                AccelaProjectRefId = "1"
            };
            var expectedhours = 1.0M;
            decimal? result = 0.0M;
            var propertytype = PropertyTypeEnums.Townhomes;
            var departmentname = DepartmentNameEnums.Zone_Huntersville;
            _project.AccelaPropertyType = propertytype;
            _project.Agencies.Add(new ProjectAgency
            {
                DepartmentInfo = departmentname
            });
            _estimation.Object.PerformAutoEstimation(_project);
            result = _project.Agencies[0].EstimationHours;

            Assert.IsTrue(result == expectedhours);
        }

        //[TestMethod]
        public void ProjectGetTradeDefaultHoursTownhomesBuilding()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;
            _project = new ProjectEstimation
            {
                Trades = new List<ProjectTrade>(),
                Agencies = new List<ProjectAgency>(),
                AccelaProjectRefId = "1"
            };
            var expectedhours = 3.0M;
            decimal? result = 0.0M;
            var propertytype = PropertyTypeEnums.Townhomes;
            var departmentname = DepartmentNameEnums.Building;
            _project.AccelaPropertyType = propertytype;
            _project.Trades.Add(new ProjectTrade
            {
                DepartmentInfo = departmentname
            });
            _estimation.Object.PerformAutoEstimation(_project);
            result = _project.Trades[0].EstimationHours;

            Assert.IsTrue(result == expectedhours);
        }

        [TestMethod]
        [Ignore]

        public void ProjectGetAgencyDefaultHoursFifoSmallCommZoneMintHill()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;
            _project = new ProjectEstimation
            {
                Trades = new List<ProjectTrade>(),
                Agencies = new List<ProjectAgency>(),
                AccelaProjectRefId = "1"
            };
            var expectedhours = 0.5M;
            decimal? result = 0.0M;
            var propertytype = PropertyTypeEnums.FIFO_Small_Commercial;
            var departmentname = DepartmentNameEnums.Zone_Mint_Hill;
            _project.AccelaPropertyType = propertytype;
            _project.Agencies.Add(new ProjectAgency
            {
                DepartmentInfo = departmentname
            });
            _estimation.Object.PerformAutoEstimation(_project);
            result = _project.Agencies[0].EstimationHours;

            Assert.IsTrue(result == expectedhours);
        }
        [TestMethod]
        [Ignore]
        public void ProjectGetAgencyDefaultHoursFifoSmallCommZoneHuntersville()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;
            _project = new ProjectEstimation
            {
                Trades = new List<ProjectTrade>(),
                Agencies = new List<ProjectAgency>(),
                AccelaProjectRefId = "1"
            };
            var expectedhours = 0.5M;
            decimal? result = 0.0M;
            var propertytype = PropertyTypeEnums.FIFO_Small_Commercial;
            var departmentname = DepartmentNameEnums.Zone_Huntersville;
            _project.AccelaPropertyType = propertytype;
            _project.Agencies.Add(new ProjectAgency
            {
                DepartmentInfo = departmentname
            });
            _estimation.Object.PerformAutoEstimation(_project);
            result = _project.Agencies[0].EstimationHours;

            Assert.IsTrue(result == expectedhours);
        }
        //[TestMethod]
        public void ProjectGetAgencyDefaultHoursFifoSmallCommZoneDavidson()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;
            _project = new ProjectEstimation
            {
                Trades = new List<ProjectTrade>(),
                Agencies = new List<ProjectAgency>(),
                AccelaProjectRefId = "1"
            };
            var expectedhours = 0.5M;
            decimal? result = 0.0M;
            var propertytype = PropertyTypeEnums.FIFO_Small_Commercial;
            var departmentname = DepartmentNameEnums.Zone_Davidson;
            _project.AccelaPropertyType = propertytype;
            _project.Agencies.Add(new ProjectAgency
            {
                DepartmentInfo = departmentname
            });
            _estimation.Object.PerformAutoEstimation(_project);
            result = _project.Agencies[0].EstimationHours;

            Assert.IsTrue(result == expectedhours);
        }
        //[TestMethod]
        public void ProjectGetAgencyDefaultHoursFifoSmallCommZoneCornelius()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;
            _project = new ProjectEstimation
            {
                Trades = new List<ProjectTrade>(),
                Agencies = new List<ProjectAgency>(),
                AccelaProjectRefId = "1"
            };
            var expectedhours = 0.5M;
            decimal? result = 0.0M;
            var propertytype = PropertyTypeEnums.FIFO_Small_Commercial;
            var departmentname = DepartmentNameEnums.Zone_Cornelius;
            _project.AccelaPropertyType = propertytype;
            _project.Agencies.Add(new ProjectAgency
            {
                DepartmentInfo = departmentname
            });
            _estimation.Object.PerformAutoEstimation(_project);
            result = _project.Agencies[0].EstimationHours;

            Assert.IsTrue(result == expectedhours);
        }
        //[TestMethod]
        public void ProjectGetAgencyDefaultHoursFifoSmallCommZonePineville()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;
            _project = new ProjectEstimation
            {
                Trades = new List<ProjectTrade>(),
                Agencies = new List<ProjectAgency>(),
                AccelaProjectRefId = "1"
            };
            var expectedhours = 0.5M;
            decimal? result = 0.0M;
            var propertytype = PropertyTypeEnums.FIFO_Small_Commercial;
            var departmentname = DepartmentNameEnums.Zone_Pineville;
            _project.AccelaPropertyType = propertytype;
            _project.Agencies.Add(new ProjectAgency
            {
                DepartmentInfo = departmentname
            });
            _estimation.Object.PerformAutoEstimation(_project);
            result = _project.Agencies[0].EstimationHours;

            Assert.IsTrue(result == expectedhours);
        }
        //[TestMethod]
        public void ProjectGetAgencyDefaultHoursFifoSmallCommZoneMatthews()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;
            _project = new ProjectEstimation
            {
                Trades = new List<ProjectTrade>(),
                Agencies = new List<ProjectAgency>(),
                AccelaProjectRefId = "1"
            };
            var expectedhours = 0.5M;
            decimal? result = 0.0M;
            var propertytype = PropertyTypeEnums.FIFO_Small_Commercial;
            var departmentname = DepartmentNameEnums.Zone_Matthews;
            _project.AccelaPropertyType = propertytype;
            _project.Agencies.Add(new ProjectAgency
            {
                DepartmentInfo = departmentname
            });
            _estimation.Object.PerformAutoEstimation(_project);
            result = _project.Agencies[0].EstimationHours;

            Assert.IsTrue(result == expectedhours);
        }
        //[TestMethod]
        public void ProjectGetAgencyDefaultHoursFifoSmallCommZoneUnincorporated()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;
            _project = new ProjectEstimation
            {
                Trades = new List<ProjectTrade>(),
                Agencies = new List<ProjectAgency>(),
                AccelaProjectRefId = "1"
            };
            var expectedhours = 0.5M;
            decimal? result = 0.0M;
            var propertytype = PropertyTypeEnums.FIFO_Small_Commercial;
            var departmentname = DepartmentNameEnums.Zone_UMC;
            _project.AccelaPropertyType = propertytype;
            _project.Agencies.Add(new ProjectAgency
            {
                DepartmentInfo = departmentname
            });
            _estimation.Object.PerformAutoEstimation(_project);
            result = _project.Agencies[0].EstimationHours;

            Assert.IsTrue(result == expectedhours);
        }
        //[TestMethod]
        public void ProjectGetAgencyDefaultHoursFifoSmallCommZoneCityOfCharlotte()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;
            _project = new ProjectEstimation
            {
                Trades = new List<ProjectTrade>(),
                Agencies = new List<ProjectAgency>(),
                AccelaProjectRefId = "1"
            };
            var expectedhours = 0.5M;
            decimal? result = 0.0M;
            var propertytype = PropertyTypeEnums.FIFO_Small_Commercial;
            var departmentname = DepartmentNameEnums.Zone_Cty_Chrlt;
            _project.AccelaPropertyType = propertytype;
            _project.Agencies.Add(new ProjectAgency
            {
                DepartmentInfo = departmentname
            });
            _estimation.Object.PerformAutoEstimation(_project);
            result = _project.Agencies[0].EstimationHours;

            Assert.IsTrue(result == expectedhours);
        }
        //[TestMethod]
        public void ProjectGetAgencyDefaultHoursFifoSmallCommFireMintHill()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;
            _project = new ProjectEstimation
            {
                Trades = new List<ProjectTrade>(),
                Agencies = new List<ProjectAgency>(),
                AccelaProjectRefId = "1"
            };
            var expectedhours = 0.5M;
            decimal? result = 0.0M;
            var propertytype = PropertyTypeEnums.FIFO_Small_Commercial;
            var departmentname = DepartmentNameEnums.Fire_Mint_Hill;
            _project.AccelaPropertyType = propertytype;
            _project.Agencies.Add(new ProjectAgency
            {
                DepartmentInfo = departmentname
            });
            _estimation.Object.PerformAutoEstimation(_project);
            result = _project.Agencies[0].EstimationHours;

            Assert.IsTrue(result == expectedhours);
        }
        //[TestMethod]
        public void ProjectGetAgencyDefaultHoursFifoSmallCommFireHuntersville()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;
            _project = new ProjectEstimation
            {
                Trades = new List<ProjectTrade>(),
                Agencies = new List<ProjectAgency>(),
                AccelaProjectRefId = "1"
            };
            var expectedhours = 0.5M;
            decimal? result = 0.0M;
            var propertytype = PropertyTypeEnums.FIFO_Small_Commercial;
            var departmentname = DepartmentNameEnums.Fire_Huntersville;
            _project.AccelaPropertyType = propertytype;
            _project.Agencies.Add(new ProjectAgency
            {
                DepartmentInfo = departmentname
            });
            _estimation.Object.PerformAutoEstimation(_project);
            result = _project.Agencies[0].EstimationHours;

            Assert.IsTrue(result == expectedhours);
        }
        //[TestMethod]
        public void ProjectGetAgencyDefaultHoursFifoSmallCommFireDavidson()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;
            _project = new ProjectEstimation
            {
                Trades = new List<ProjectTrade>(),
                Agencies = new List<ProjectAgency>(),
                AccelaProjectRefId = "1"
            };
            var expectedhours = 0.5M;
            decimal? result = 0.0M;
            var propertytype = PropertyTypeEnums.FIFO_Small_Commercial;
            var departmentname = DepartmentNameEnums.Fire_Davidson;
            _project.AccelaPropertyType = propertytype;
            _project.Agencies.Add(new ProjectAgency
            {
                DepartmentInfo = departmentname
            });
            _estimation.Object.PerformAutoEstimation(_project);
            result = _project.Agencies[0].EstimationHours;

            Assert.IsTrue(result == expectedhours);
        }
        //[TestMethod]
        public void ProjectGetAgencyDefaultHoursFifoSmallCommFireCornelius()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;
            _project = new ProjectEstimation
            {
                Trades = new List<ProjectTrade>(),
                Agencies = new List<ProjectAgency>(),
                AccelaProjectRefId = "1"
            };
            var expectedhours = 0.5M;
            decimal? result = 0.0M;
            var propertytype = PropertyTypeEnums.FIFO_Small_Commercial;
            var departmentname = DepartmentNameEnums.Fire_Cornelius;
            _project.AccelaPropertyType = propertytype;
            _project.Agencies.Add(new ProjectAgency
            {
                DepartmentInfo = departmentname
            });
            _estimation.Object.PerformAutoEstimation(_project);
            result = _project.Agencies[0].EstimationHours;

            Assert.IsTrue(result == expectedhours);
        }
        //[TestMethod]
        public void ProjectGetAgencyDefaultHoursFifoSmallCommFirePineville()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;
            _project = new ProjectEstimation
            {
                Trades = new List<ProjectTrade>(),
                Agencies = new List<ProjectAgency>(),
                AccelaProjectRefId = "1"
            };
            var expectedhours = 0.5M;
            decimal? result = 0.0M;
            var propertytype = PropertyTypeEnums.FIFO_Small_Commercial;
            var departmentname = DepartmentNameEnums.Fire_Pineville;
            _project.AccelaPropertyType = propertytype;
            _project.Agencies.Add(new ProjectAgency
            {
                DepartmentInfo = departmentname
            });
            _estimation.Object.PerformAutoEstimation(_project);
            result = _project.Agencies[0].EstimationHours;

            Assert.IsTrue(result == expectedhours);
        }
        //[TestMethod]
        public void ProjectGetAgencyDefaultHoursFifoSmallCommFireMatthews()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;
            _project = new ProjectEstimation
            {
                Trades = new List<ProjectTrade>(),
                Agencies = new List<ProjectAgency>(),
                AccelaProjectRefId = "1"
            };
            var expectedhours = 0.5M;
            decimal? result = 0.0M;
            var propertytype = PropertyTypeEnums.FIFO_Small_Commercial;
            var departmentname = DepartmentNameEnums.Fire_Matthews;
            _project.AccelaPropertyType = propertytype;
            _project.Agencies.Add(new ProjectAgency
            {
                DepartmentInfo = departmentname
            });
            _estimation.Object.PerformAutoEstimation(_project);
            result = _project.Agencies[0].EstimationHours;

            Assert.IsTrue(result == expectedhours);
        }
        //[TestMethod]
        public void ProjectGetAgencyDefaultHoursFifoSmallCommFireUnincorporated()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;
            _project = new ProjectEstimation
            {
                Trades = new List<ProjectTrade>(),
                Agencies = new List<ProjectAgency>(),
                AccelaProjectRefId = "1"
            };
            var expectedhours = 0.5M;
            decimal? result = 0.0M;
            var propertytype = PropertyTypeEnums.FIFO_Small_Commercial;
            var departmentname = DepartmentNameEnums.Fire_UMC;
            _project.AccelaPropertyType = propertytype;
            _project.Agencies.Add(new ProjectAgency
            {
                DepartmentInfo = departmentname
            });
            _estimation.Object.PerformAutoEstimation(_project);
            result = _project.Agencies[0].EstimationHours;

            Assert.IsTrue(result == expectedhours);
        }
        //[TestMethod]
        public void ProjectGetAgencyDefaultHoursFifoSmallCommFireCityOfCharlotte()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;
            _project = new ProjectEstimation
            {
                Trades = new List<ProjectTrade>(),
                Agencies = new List<ProjectAgency>(),
                AccelaProjectRefId = "1"
            };
            var expectedhours = 0.5M;
            decimal? result = 0.0M;
            var propertytype = PropertyTypeEnums.FIFO_Small_Commercial;
            var departmentname = DepartmentNameEnums.Fire_Cty_Chrlt;
            _project.AccelaPropertyType = propertytype;
            _project.Agencies.Add(new ProjectAgency
            {
                DepartmentInfo = departmentname
            });
            _estimation.Object.PerformAutoEstimation(_project);
            result = _project.Agencies[0].EstimationHours;

            Assert.IsTrue(result == expectedhours);
        }
        [TestMethod]
        public void ProjectGetTradeDefaultHoursFifoSmallCommBuilding()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;
            _project = new ProjectEstimation
            {
                Trades = new List<ProjectTrade>(),
                Agencies = new List<ProjectAgency>(),
                AccelaProjectRefId = "1"
            };
            var expectedhours = 0.5M;
            decimal? result = 0.0M;
            var propertytype = PropertyTypeEnums.FIFO_Small_Commercial;
            var departmentname = DepartmentNameEnums.Building;
            _project.AccelaPropertyType = propertytype;
            _project.Trades.Add(new ProjectTrade
            {
                DepartmentInfo = departmentname
            });
            _estimation.Object.PerformAutoEstimation(_project);
            result = _project.Trades[0].EstimationHours;

            Assert.IsTrue(result == expectedhours);
        }
        [TestMethod]
        public void ProjectGetTradeDefaultHoursFifoSmallCommElectrical()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;
            _project = new ProjectEstimation
            {
                Trades = new List<ProjectTrade>(),
                Agencies = new List<ProjectAgency>(),
                AccelaProjectRefId = "1"
            };
            var expectedhours = 0.5M;
            decimal? result = 0.0M;
            var propertytype = PropertyTypeEnums.FIFO_Small_Commercial;
            var departmentname = DepartmentNameEnums.Electrical;
            _project.AccelaPropertyType = propertytype;
            _project.Trades.Add(new ProjectTrade
            {
                DepartmentInfo = departmentname
            });
            _estimation.Object.PerformAutoEstimation(_project);
            result = _project.Trades[0].EstimationHours;

            Assert.IsTrue(result == expectedhours);
        }
        [TestMethod]
        public void ProjectGetTradeDefaultHoursFifoSmallCommMechanical()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;
            _project = new ProjectEstimation
            {
                Trades = new List<ProjectTrade>(),
                Agencies = new List<ProjectAgency>(),
                AccelaProjectRefId = "1"
            };
            var expectedhours = 0.5M;
            decimal? result = 0.0M;
            var propertytype = PropertyTypeEnums.FIFO_Small_Commercial;
            var departmentname = DepartmentNameEnums.Mechanical;
            _project.AccelaPropertyType = propertytype;
            _project.Trades.Add(new ProjectTrade
            {
                DepartmentInfo = departmentname
            });
            _estimation.Object.PerformAutoEstimation(_project);
            result = _project.Trades[0].EstimationHours;

            Assert.IsTrue(result == expectedhours);
        }
        [TestMethod]
        public void ProjectGetTradeDefaultHoursFifoSmallCommPlumbing()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;
            _project = new ProjectEstimation
            {
                Trades = new List<ProjectTrade>(),
                Agencies = new List<ProjectAgency>(),
                AccelaProjectRefId = "1"
            };
            var expectedhours = 0.5M;
            decimal? result = 0.0M;
            var propertytype = PropertyTypeEnums.FIFO_Small_Commercial;
            var departmentname = DepartmentNameEnums.Plumbing;
            _project.AccelaPropertyType = propertytype;
            _project.Trades.Add(new ProjectTrade
            {
                DepartmentInfo = departmentname
            });
            _estimation.Object.PerformAutoEstimation(_project);
            result = _project.Trades[0].EstimationHours;

            Assert.IsTrue(result == expectedhours);
        }

        [TestMethod]
        public void ProjectGetTradeDefaultHoursFifoSingleFamilyHomesBuilding()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;
            _project = new ProjectEstimation
            {
                Trades = new List<ProjectTrade>(),
                Agencies = new List<ProjectAgency>(),
                AccelaProjectRefId = "1"
            };
            var expectedhours = 1.5M;
            decimal? result = 0.0M;
            var propertytype = PropertyTypeEnums.FIFO_Single_Family_Homes;
            var departmentname = DepartmentNameEnums.Building;
            _project.AccelaPropertyType = propertytype;
            _project.Trades.Add(new ProjectTrade
            {
                DepartmentInfo = departmentname
            });
            _estimation.Object.PerformAutoEstimation(_project);
            result = _project.Trades[0].EstimationHours;

            Assert.IsTrue(result == expectedhours);
        }
        //[TestMethod]
        public void ProjectGetTradeDefaultHoursFifoMasterPlansBuilding()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;
            _project = new ProjectEstimation
            {
                Trades = new List<ProjectTrade>(),
                Agencies = new List<ProjectAgency>(),
                AccelaProjectRefId = "1"
            };
            var expectedhours = 3.0M;
            decimal? result = 0.0M;
            var propertytype = PropertyTypeEnums.FIFO_Master_Plans;
            var departmentname = DepartmentNameEnums.Building;
            _project.AccelaPropertyType = propertytype;
            _project.Trades.Add(new ProjectTrade
            {
                DepartmentInfo = departmentname
            });
            _estimation.Object.PerformAutoEstimation(_project);
            result = _project.Trades[0].EstimationHours;

            Assert.IsTrue(result == expectedhours);
        }
        [TestMethod]
        public void ProjectGetTradeDefaultHoursFifoAdditionRenovationSingleFamilyHomeBuilding()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;
            _project = new ProjectEstimation
            {
                Trades = new List<ProjectTrade>(),
                Agencies = new List<ProjectAgency>(),
                AccelaProjectRefId = "1"
            };
            var expectedhours = 1.0M;
            decimal? result = 0.0M;
            var propertytype = PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home;
            var departmentname = DepartmentNameEnums.Building;
            _project.AccelaPropertyType = propertytype;
            _project.Trades.Add(new ProjectTrade
            {
                DepartmentInfo = departmentname
            });
            _estimation.Object.PerformAutoEstimation(_project);
            result = _project.Trades[0].EstimationHours;

            Assert.IsTrue(result == expectedhours);
        }

        //[TestMethod]
        public void ProjectGetAgencyDefaultHoursCountyShopDrawingsFireMintHill()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;
            _project = new ProjectEstimation
            {
                Trades = new List<ProjectTrade>(),
                Agencies = new List<ProjectAgency>(),
                AccelaProjectRefId = "1"
            };
            var expectedhours = 1.0M;
            decimal? result = 0.0M;
            var propertytype = PropertyTypeEnums.County_Fire_Shop_Drawings;
            var departmentname = DepartmentNameEnums.Fire_Mint_Hill;
            _project.AccelaPropertyType = propertytype;
            _project.Agencies.Add(new ProjectAgency
            {
                DepartmentInfo = departmentname
            });
            _estimation.Object.PerformAutoEstimation(_project);
            result = _project.Agencies[0].EstimationHours;

            Assert.IsTrue(result == expectedhours);
        }
        //[TestMethod]
        public void ProjectGetAgencyDefaultHoursCountyShopDrawingsFireHuntersville()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;
            _project = new ProjectEstimation
            {
                Trades = new List<ProjectTrade>(),
                Agencies = new List<ProjectAgency>(),
                AccelaProjectRefId = "1"
            };
            var expectedhours = 1.0M;
            decimal? result = 0.0M;
            var propertytype = PropertyTypeEnums.County_Fire_Shop_Drawings;
            var departmentname = DepartmentNameEnums.Fire_Huntersville;
            _project.AccelaPropertyType = propertytype;
            _project.Agencies.Add(new ProjectAgency
            {
                DepartmentInfo = departmentname
            });
            _estimation.Object.PerformAutoEstimation(_project);
            result = _project.Agencies[0].EstimationHours;

            Assert.IsTrue(result == expectedhours);
        }
        //[TestMethod]
        public void ProjectGetAgencyDefaultHoursCountyShopDrawingsFireDavidson()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;
            _project = new ProjectEstimation
            {
                Trades = new List<ProjectTrade>(),
                Agencies = new List<ProjectAgency>(),
                AccelaProjectRefId = "1"
            };
            var expectedhours = 1.0M;
            decimal? result = 0.0M;
            var propertytype = PropertyTypeEnums.County_Fire_Shop_Drawings;
            var departmentname = DepartmentNameEnums.Fire_Davidson;
            _project.AccelaPropertyType = propertytype;
            _project.Agencies.Add(new ProjectAgency
            {
                DepartmentInfo = departmentname
            });
            _estimation.Object.PerformAutoEstimation(_project);
            result = _project.Agencies[0].EstimationHours;

            Assert.IsTrue(result == expectedhours);
        }
        //[TestMethod]
        public void ProjectGetAgencyDefaultHoursCountyShopDrawingsFireCornelius()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;
            _project = new ProjectEstimation
            {
                Trades = new List<ProjectTrade>(),
                Agencies = new List<ProjectAgency>(),
                AccelaProjectRefId = "1"
            };
            var expectedhours = 1.0M;
            decimal? result = 0.0M;
            var propertytype = PropertyTypeEnums.County_Fire_Shop_Drawings;
            var departmentname = DepartmentNameEnums.Fire_Cornelius;
            _project.AccelaPropertyType = propertytype;
            _project.Agencies.Add(new ProjectAgency
            {
                DepartmentInfo = departmentname
            });
            _estimation.Object.PerformAutoEstimation(_project);
            result = _project.Agencies[0].EstimationHours;

            Assert.IsTrue(result == expectedhours);
        }
        //[TestMethod]
        public void ProjectGetAgencyDefaultHoursCountyShopDrawingsFirePineville()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;
            _project = new ProjectEstimation
            {
                Trades = new List<ProjectTrade>(),
                Agencies = new List<ProjectAgency>(),
                AccelaProjectRefId = "1"
            };
            var expectedhours = 1.0M;
            decimal? result = 0.0M;
            var propertytype = PropertyTypeEnums.County_Fire_Shop_Drawings;
            var departmentname = DepartmentNameEnums.Fire_Pineville;
            _project.AccelaPropertyType = propertytype;
            _project.Agencies.Add(new ProjectAgency
            {
                DepartmentInfo = departmentname
            });
            _estimation.Object.PerformAutoEstimation(_project);
            result = _project.Agencies[0].EstimationHours;

            Assert.IsTrue(result == expectedhours);
        }
        //[TestMethod]
        public void ProjectGetAgencyDefaultHoursCountyShopDrawingsFireMatthews()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;
            _project = new ProjectEstimation
            {
                Trades = new List<ProjectTrade>(),
                Agencies = new List<ProjectAgency>(),
                AccelaProjectRefId = "1"
            };
            var expectedhours = 1.0M;
            decimal? result = 0.0M;
            var propertytype = PropertyTypeEnums.County_Fire_Shop_Drawings;
            var departmentname = DepartmentNameEnums.Fire_Matthews;
            _project.AccelaPropertyType = propertytype;
            _project.Agencies.Add(new ProjectAgency
            {
                DepartmentInfo = departmentname
            });
            _estimation.Object.PerformAutoEstimation(_project);
            result = _project.Agencies[0].EstimationHours;

            Assert.IsTrue(result == expectedhours);
        }
        //[TestMethod]
        public void ProjectGetAgencyDefaultHoursCountyShopDrawingsFireUnincorporated()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;
            _project = new ProjectEstimation
            {
                Trades = new List<ProjectTrade>(),
                Agencies = new List<ProjectAgency>(),
                AccelaProjectRefId = "1"
            };
            var expectedhours = 1.0M;
            decimal? result = 0.0M;
            var propertytype = PropertyTypeEnums.County_Fire_Shop_Drawings;
            var departmentname = DepartmentNameEnums.Fire_UMC;
            _project.AccelaPropertyType = propertytype;
            _project.Agencies.Add(new ProjectAgency
            {
                DepartmentInfo = departmentname
            });
            _estimation.Object.PerformAutoEstimation(_project);
            result = _project.Agencies[0].EstimationHours;

            Assert.IsTrue(result == expectedhours);
        }

        [TestMethod]
        public void ProjectGetDefaultHoursWithJson()
        {
            if (AION.BL.Test.Global.FreezeTesting == true) return;
            List<TestData> mMMFProjects = JsonReader<TestData>.GetData(@"\TestData\DefaultProjectEstimationValues.json");

            var tmp = mMMFProjects.Where((x) => x.TestMethod == "ProjectGetDefaultHoursWithJson");

            foreach (var item in tmp)
            {
                decimal expectedhours = decimal.Parse(item.ExpectedResult);
                decimal? result = 0.0M;
                var propertytype = item.PropertyType;
                var departmentname = item.DepartmentName;
                _project.AccelaPropertyType = propertytype;
                _project.Agencies.Add(new ProjectAgency
                {
                    DepartmentInfo = departmentname
                });
                _estimation.Object.PerformAutoEstimation(_project);
                result = _project.Agencies[0].EstimationHours;

                Assert.IsTrue(result == expectedhours);
            }
        }

        public class TestData
        {
            public string TestMethod { get; set; }

            public string ExpectedResult { get; set; }

            public PropertyTypeEnums PropertyType { get; set; }

            public DepartmentNameEnums DepartmentName { get; set; }
        }
    }


}
