using AION.BL.BusinessObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AION.BL.Test.BusinessObjects
{
    [TestClass]
    public class UiSettingsModelBOTests
    {
        [TestMethod]
        public void UiSettingsModelBOSetsObjWithJsonString()
        {
            string jsonstring = $@"{{
                    ""estimationDashboard"":{{
                        ""columnsFilter"": ""th0,th01,th02,th03,th04,th05,th06,th07,th08,th09,th10,th11,th12,th13,th14,th15,th16,th17,th18,th19,th20,th21,th22,th23""
                    }},
                    ""meetingDashboard"":{{
                        ""columnsFilter"": ""th0,th01,th02,th03,th04,th05,th06,th07,th08,th09,th10,th11,th12,th13,th14,th15""
                    }},
                    ""schedulingDashboard"":{{
                        ""columnsFilter"": ""th0,th01,th02,th03,th04,th05,th06,th07,th08,th09,th10,th11,th12,th13,th14,th15""
                    }}
                }}";

            UiSettingsModelBO bo = new UiSettingsModelBO();
            bo.JsonString = jsonstring;
            var uisettingsobj = bo.UiSettings;

            Assert.IsNotNull(uisettingsobj);
        }
    }
}
