//using AccelaEngineTests.UnitTestData;
using AION.Accela.Engine;
using AION.Accela.Engine.BusinessObjects;
using AION.AIONDB.Engine;
using AION.AIONDB.Engine.BusinessObjects;
using AION.BL;
using AION.BL.Adapters;
using AION.BL.Models;
using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using AION.Manager.Adapters;
using AION.Manager.Models;
using Meck.Azure;
using Meck.Shared.Accela;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
// using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

// using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace AccelaEngineTests
{

    [TestClass]
    public class AccelaApiTests : System.Web.HttpApplication
    {
        //private TestData mUTData = new TestData();

        //  data In Unit Test Build
        public List<PlanReviewHistory> mPlanRviewHistoryTest = new List<PlanReviewHistory>();
        public List<RecordNotification> mPrepRecordNotificationTest = new List<RecordNotification>();

        [TestInitialize]
        public void SetupAccelaEngineTests()
        {
            Meck.Shared.Globals.AccelaAuthBaseUrl = GetConfigValue("AccellaAuthbaseUrl");
            Meck.Shared.Globals.AccelaAgency = GetConfigValue("AccelaAuthAgency");
            Meck.Shared.Globals.AccelaEnvironment = GetConfigValue("AccelaEnvironment");

            Meck.Shared.Globals.AccelaScope = GetConfigValue("AccelaScope");
            Meck.Shared.Globals.AccelaClientSecret = KeyVaultUtility.GetSecret("AccelaClientSecret");
            Meck.Shared.Globals.AccelaUser = KeyVaultUtility.GetSecret("AccelaUserID");
            Meck.Shared.Globals.AccelaUserName = KeyVaultUtility.GetSecret("AccelaUserID");
            Meck.Shared.Globals.AccelaPassword = KeyVaultUtility.GetSecret("AccelaPassword");
            Meck.Shared.Globals.AccelaClientId = KeyVaultUtility.GetSecret("AccelaClientID");

            Meck.Shared.Globals.AIONConnectionString = KeyVaultUtility.GetSecret("KeyVaultConnectionString");
        }

        private string GetConfigValue(string settingname)
        {
            return ConfigurationManager.AppSettings[settingname];

        }

        //  [TestMethod]
        //[Ignore]
        //  public void AccelaWebApiPingTest()
        // {
        //    string mIHttpActionResult = string.Empty;

        //     //  CancellationToken CanToken = new CancellationToken();

        //    DateTime moqresult = DateTime.Now;

        //     PingController mPingController = new PingController();

        //     var result = mPingController.PingTest();

        ////    Assert.IsFalse(result == null);

        //     Console.WriteLine("Test Completed");

        // }

        [TestMethod]
        [Ignore]
        public void InsertNewRecordTest()
        {


            IAIONDBEngine mAIONDBEngine = new AIONEngineCrudApiBO();

            //foreach (var mRecNotify in mUTData.mRecordNotificationTest)
            //{
            //   //  var recjson = JsonConvert.SerializeObject(mRecNotify);
            //   var result = mAIONDBEngine.InsertNewAIONRecord(mRecNotify);

            //   var newRecResult = result;

            //   Console.WriteLine(JsonConvert.SerializeObject(newRecResult));

            //   //  AIONRecordQueueResponse newRecordDetail = JsonConvert.DeserializeObject<AIONRecordQueueResponse>(newRecResult);

            //   Assert.IsTrue(newRecResult.errors.Length == 0);
            //}

            //mUTData.ClearDBTable(true, false);
        }

        [TestMethod]
        [Ignore]
        public void AccelaWebApiPingTest()
        {
            //var userId = ClaimsPrincipal.Current.FindFirst(ClaimTypes.NameIdentifier); // will give the user's userId
            //var userName = ClaimsPrincipal.Current.FindFirst(ClaimTypes.Name); // will give the user's userName

            int id = 0;
            RequestCustomTablesTasksBE mRequestCustomTablesTasksBe = new RequestCustomTablesTasksBE();

            List<TableFieldBE> mPRFieldBE = new List<TableFieldBE>();

            mPRFieldBE.Add(new TableFieldBE(null, "id", id));
            mPRFieldBE.Add(new TableFieldBE(null, "Task Type", "Plan Review"));
            mPRFieldBE.Add(new TableFieldBE(null, "Task Name", "test"));
            mPRFieldBE.Add(new TableFieldBE(null, "Pool Review", "No"));
            mPRFieldBE.Add(new TableFieldBE(null, "Assignee", "Tammy" + " " + "Smith"));
            mPRFieldBE.Add(new TableFieldBE(null, "Due Date", DateTime.Now));
            mPRFieldBE.Add(new TableFieldBE(null, "Cycle #", 1));
            mPRFieldBE.Add(new TableFieldBE(null, "StartDate", DateTime.Now));
            mPRFieldBE.Add(new TableFieldBE(null, "EstimatedReviewTime", Convert.ToInt32("2")));
            mPRFieldBE.Add(new TableFieldBE(null, "Date-Time Stamp", DateTime.Now));
            mPRFieldBE.Add(new TableFieldBE(null, "Processing Status", "Added By AION"));
            mPRFieldBE.Add(new TableFieldBE(null, "Comments", ""));

            TableRowsBE mPRTableRowsBE =
                     new TableRowsBE("CE_COM-REVIEW.cTASK.cACTIVATION", TableRowBE.ActionEnum.Add, mPRFieldBE);

            mRequestCustomTablesTasksBe.array.Add(mPRTableRowsBE);

            Project project = new EstimationCRUDAdapter().GetProjectDetailsByProjectId(424798);
            mRequestCustomTablesTasksBe.recordId = project.RecIdTxt;
            AccelaRecordBO mAccelaRecordBO = new AccelaRecordBO();
            var result = mAccelaRecordBO.TaskUpDateRecordCustomTables(mRequestCustomTablesTasksBe);




            //IAccelaEngine thisengine = new AccelaApiBO();

            //var result = await thisengine.UpDateRecordCustomTables(mRequestCustomTablesTasksBe);


            {

                var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;
                var claims = identity.Claims;

            }

        }
        [TestMethod]
        [Ignore]
        public void InsertNewRecordFailTest()
        {
            IAIONDBEngine mAIONDBEngine = new AIONEngineCrudApiBO();

            RecordNotification mRecordNortification = new RecordNotification();

            //   mRecordNortification.EstimatedRereviewHours = null;
            mRecordNortification.WorkFlowStepId = string.Empty;
            mRecordNortification.WorkFlowTaskName = "Similuated send to Aion";
            mRecordNortification.WorkFlowStatus = "SendToAION";
            mRecordNortification.recordID = "Unit_Test-00" + 000 + "-birds".Length;
            mRecordNortification.recordtype = "";
            mRecordNortification.status = "";
            mRecordNortification.statusdescription = "should be replaced";

            var result = mAIONDBEngine.InsertNewAIONRecord(mRecordNortification);
            var newRecResult = result;

            Console.WriteLine(newRecResult);

            Assert.IsTrue(newRecResult.errors.Length == 0);

        }


        [TestMethod]
        [Ignore]
        public void InsertNewPlanReviewHistoryTest()
        {
            //TestData mUTData = new TestData();

            IAIONDBEngine mAIONDBEngine = new AIONEngineCrudApiBO();

            //foreach (PlanReviewHistory mRecNotify in mUTData.mPlaneReviewHistoryTest)
            //{
            //   //  var recjson = JsonConvert.SerializeObject(mRecNotify);
            //   var resulttask = mAIONDBEngine.InsertNewAIONPlanReviewHistoryRecord(mRecNotify);
            //   var mAIONPlanHistoryResponse = resulttask;

            //   Console.WriteLine(JsonConvert.SerializeObject(mAIONPlanHistoryResponse));

            //   Assert.IsTrue(mAIONPlanHistoryResponse.errors == string.Empty);
            //}

            //mUTData.ClearDBTable(false, true, false);
        }
        public Tuple<string, string, string, string> TableValueTasksMapAionToAccela(PlanReview pr, Project project, string BusinessName)
        {

            string pool = "No";
            string taskName = string.Empty;
            string startDate = string.Empty;
            string dueDate = string.Empty;


            switch (BusinessName)
            {
                case "NA":
                case "Building":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                    || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                    || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                    || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team)
                    {
                        taskName = "Commercial Building";
                    }

                    else if (project.AccelaPropertyType == PropertyTypeEnums.Townhomes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Single_Family_Homes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Master_Plans)
                    {
                        taskName = "Residential Building";
                    }
                    pool = pr.BuildPool.HasValue ? (pr.BuildPool.Value ? "Yes" : "No") : "No";
                    if (pr.BuildStartDate.HasValue) startDate = pr.BuildStartDate.Value.ToString();
                    if (pr.BuildEndDate.HasValue) dueDate = pr.BuildEndDate.Value.ToString();
                    break;
                case "Electrical":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                    || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                    || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                    || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team)
                    {
                        taskName = "Commercial Electrical";
                    }
                    pool = pr.ElectPool.HasValue ? (pr.ElectPool.Value ? "Yes" : "No") : "No";
                    if (pr.ElectStartDate.HasValue) startDate = pr.ElectStartDate.Value.ToString();
                    if (pr.ElectEndDate.HasValue) dueDate = pr.ElectEndDate.Value.ToString();
                    break;
                case "Mechanical":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                    || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                    || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                    || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team)
                    {
                        taskName = "Commercial Mechanical";
                    }
                    pool = pr.MechaPool.HasValue ? (pr.MechaPool.Value ? "Yes" : "No") : "No";
                    if (pr.MechaStartDate.HasValue) startDate = pr.MechaStartDate.Value.ToString();
                    if (pr.MechaEndDate.HasValue) dueDate = pr.MechaEndDate.Value.ToString();
                    break;
                case "Plumbing":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                    || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                    || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                    || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team)
                    {
                        taskName = "Commercial Plumbing";
                    }
                    if (pr.PlumbStartDate.HasValue) startDate = pr.PlumbStartDate.Value.ToString();
                    if (pr.PlumbEndDate.HasValue) dueDate = pr.PlumbEndDate.Value.ToString();
                    break;
                case "Zone_Davidson":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                    || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                    || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                    || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team)
                    {
                        taskName = "Commercial County Zoning";
                    }
                    else if (project.AccelaPropertyType == PropertyTypeEnums.Townhomes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Single_Family_Homes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Master_Plans)
                    {
                        taskName = "Residential County Zoning";
                    }
                    if (pr.ZoneStartDate.HasValue) startDate = pr.ZoneStartDate.Value.ToString();
                    if (pr.ZoneEndDate.HasValue) dueDate = pr.ZoneEndDate.Value.ToString();
                    break;
                case "Zone_Cornelius":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                       || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team)
                    {
                        taskName = "Commercial County Zoning";
                    }
                    else if (project.AccelaPropertyType == PropertyTypeEnums.Townhomes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Single_Family_Homes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Master_Plans)
                    {
                        taskName = "Residential County Zoning";
                    }
                    if (pr.ZoneStartDate.HasValue) startDate = pr.ZoneStartDate.Value.ToString();
                    if (pr.ZoneEndDate.HasValue) dueDate = pr.ZoneEndDate.Value.ToString();
                    break;
                case "Zone_Pineville":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                       || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team)
                    {
                        taskName = "Commercial County Zoning";
                    }
                    else if (project.AccelaPropertyType == PropertyTypeEnums.Townhomes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Single_Family_Homes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Master_Plans)
                    {
                        taskName = "Residential County Zoning";
                    }
                    if (pr.ZoneStartDate.HasValue) startDate = pr.ZoneStartDate.Value.ToString();
                    if (pr.ZoneEndDate.HasValue) dueDate = pr.ZoneEndDate.Value.ToString();
                    break;
                case "Zone_Matthews":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                       || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team)
                    {
                        taskName = "Commercial County Zoning";
                    }
                    else if (project.AccelaPropertyType == PropertyTypeEnums.Townhomes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Single_Family_Homes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Master_Plans)
                    {
                        taskName = "Residential County Zoning";
                    }
                    if (pr.ZoneStartDate.HasValue) startDate = pr.ZoneStartDate.Value.ToString();
                    if (pr.ZoneEndDate.HasValue) dueDate = pr.ZoneEndDate.Value.ToString();
                    break;
                case "Zone_Huntersville":
                    taskName = "Huntersville Zoning";
                    if (pr.ZoneStartDate.HasValue) startDate = pr.ZoneStartDate.Value.ToString();
                    if (pr.ZoneEndDate.HasValue) dueDate = pr.ZoneEndDate.Value.ToString();
                    break;
                case "Zone_Mint_Hill":
                    taskName = "Mint Hill Zoning";
                    if (pr.ZoneStartDate.HasValue) startDate = pr.ZoneStartDate.Value.ToString();
                    if (pr.ZoneEndDate.HasValue) dueDate = pr.ZoneEndDate.Value.ToString();
                    break;
                case "Zone Unincorporated Mecklenburg County":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                       || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team)
                    {
                        taskName = "Commercial County Zoning";
                    }
                    if (pr.ZoneStartDate.HasValue) startDate = pr.ZoneStartDate.Value.ToString();
                    if (pr.ZoneEndDate.HasValue) dueDate = pr.ZoneEndDate.Value.ToString();
                    break;
                case "Zone_City Of Charlotte":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                    || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                    || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                    || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team)
                    {
                        taskName = "Commercial City Zoning";
                    }
                    else if (project.AccelaPropertyType == PropertyTypeEnums.Townhomes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Single_Family_Homes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Master_Plans)
                    {
                        taskName = "Residential Charlotte Zoning";
                    }
                    pool = pr.ZonePool.HasValue ? (pr.ZonePool.Value ? "Yes" : "No") : "No";
                    if (pr.ZoneStartDate.HasValue) startDate = pr.ZoneStartDate.Value.ToString();
                    if (pr.ZoneEndDate.HasValue) dueDate = pr.ZoneEndDate.Value.ToString();
                    break;
                case "Fire_Davidson":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                       || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team
                       || project.AccelaPropertyType == PropertyTypeEnums.County_Fire_Shop_Drawings)

                    {
                        taskName = "Commercial County Fire";
                    }
                    else if (project.AccelaPropertyType == PropertyTypeEnums.Townhomes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Single_Family_Homes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Master_Plans)
                    {
                        taskName = "Residential County Fire";
                    }
                    if (pr.FireStartDate.HasValue) startDate = pr.FireStartDate.Value.ToString();
                    if (pr.FireEndDate.HasValue) dueDate = pr.FireEndDate.Value.ToString();
                    break;
                case "Fire_Cornelius":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                       || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team
                       || project.AccelaPropertyType == PropertyTypeEnums.County_Fire_Shop_Drawings)
                    {
                        taskName = "Commercial County Fire";
                    }
                    else if (project.AccelaPropertyType == PropertyTypeEnums.Townhomes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Single_Family_Homes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Master_Plans)
                    {
                        taskName = "Residential County Fire";
                    }
                    if (pr.FireStartDate.HasValue) startDate = pr.FireStartDate.Value.ToString();
                    if (pr.FireEndDate.HasValue) dueDate = pr.FireEndDate.Value.ToString();
                    break;
                case "Fire_Pineville":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                       || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team
                       || project.AccelaPropertyType == PropertyTypeEnums.County_Fire_Shop_Drawings)
                    {
                        taskName = "Commercial County Fire";
                    }
                    else if (project.AccelaPropertyType == PropertyTypeEnums.Townhomes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Single_Family_Homes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Master_Plans)
                    {
                        taskName = "Residential County Fire";
                    }
                    if (pr.FireStartDate.HasValue) startDate = pr.FireStartDate.Value.ToString();
                    if (pr.FireEndDate.HasValue) dueDate = pr.FireEndDate.Value.ToString();
                    break;
                case "Fire_Matthews":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                       || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team
                       || project.AccelaPropertyType == PropertyTypeEnums.County_Fire_Shop_Drawings)
                    {
                        taskName = "Commercial County Fire";
                    }
                    else if (project.AccelaPropertyType == PropertyTypeEnums.Townhomes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Single_Family_Homes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Master_Plans)
                    {
                        taskName = "Residential County Fire";
                    }
                    if (pr.FireStartDate.HasValue) startDate = pr.FireStartDate.Value.ToString();
                    if (pr.FireEndDate.HasValue) dueDate = pr.FireEndDate.Value.ToString();
                    break;
                case "Fire_Huntersville":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                       || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team
                       || project.AccelaPropertyType == PropertyTypeEnums.County_Fire_Shop_Drawings)
                    {
                        taskName = "Commercial County Fire";
                    }
                    else if (project.AccelaPropertyType == PropertyTypeEnums.Townhomes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Single_Family_Homes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Master_Plans)
                    {
                        taskName = "Residential County Fire";
                    }
                    if (pr.FireStartDate.HasValue) startDate = pr.FireStartDate.Value.ToString();
                    if (pr.FireEndDate.HasValue) dueDate = pr.FireEndDate.Value.ToString();
                    break;
                //case "Fire Mint Hill":
                //   if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                //      || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                //      || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                //      || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team)
                //   {
                //      taskName = "Commercial County Fire";
                //   }
                //   else if (project.AccelaPropertyType == PropertyTypeEnums.Townhomes
                //      || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home
                //      || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Single_Family_Homes
                //      || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Master_Plans)
                //   {
                //      taskName = "Residential County Fire";
                //   }
                //   if (pr.FireStartDate.HasValue) startDate = pr.FireStartDate.Value.ToString();
                //   if (pr.FireEndDate.HasValue) dueDate = pr.FireEndDate.Value.ToString();
                //   break;
                case "Fire Unincorporated Mecklenburg County":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                       || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team
                       || project.AccelaPropertyType == PropertyTypeEnums.County_Fire_Shop_Drawings)
                    {
                        taskName = "Commercial County Fire";
                    }
                    else if (project.AccelaPropertyType == PropertyTypeEnums.Townhomes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Single_Family_Homes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Master_Plans)
                    {
                        taskName = "Residential County Fire";
                    }
                    if (pr.FireStartDate.HasValue) startDate = pr.FireStartDate.Value.ToString();
                    if (pr.FireEndDate.HasValue) dueDate = pr.FireEndDate.Value.ToString();
                    break;
                case "Fire_City Of Charlotte":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                       || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team
                       )
                    {
                        taskName = "Commercial City Fire";
                    }
                    else if (project.AccelaPropertyType == PropertyTypeEnums.County_Fire_Shop_Drawings)
                    {
                        taskName = "Commercial County Fire";
                    }

                    else if (project.AccelaPropertyType == PropertyTypeEnums.Townhomes
                            || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home
                            || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Single_Family_Homes
                            || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Master_Plans)
                    {
                        taskName = "Residential City Fire";
                    }

                    pool = pr.FirePool.HasValue ? (pr.FirePool.Value ? "Yes" : "No") : "No";
                    if (pr.FireStartDate.HasValue) startDate = pr.FireStartDate.Value.ToString();
                    if (pr.FireEndDate.HasValue) dueDate = pr.FireEndDate.Value.ToString();
                    break;
                case "Environmental Health: Day Care":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                       || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team)
                    { taskName = "Commercial EHS Day Care"; }
                    pool = pr.DaycPool.HasValue ? (pr.DaycPool.Value ? "Yes" : "No") : "No";
                    if (pr.DaycStartDate.HasValue) startDate = pr.DaycStartDate.Value.ToString();
                    if (pr.DaycEndDate.HasValue) dueDate = pr.DaycEndDate.Value.ToString();
                    break;
                case "Environmental Health: Food Service":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                       || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                       || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team)
                    { taskName = "Commercial EHS Food Service"; }

                    pool = pr.FoodPool.HasValue ? (pr.FoodPool.Value ? "Yes" : "No") : "No";
                    if (pr.FoodStartDate.HasValue) startDate = pr.FoodStartDate.Value.ToString();
                    if (pr.FoodEndDate.HasValue) dueDate = pr.FoodEndDate.Value.ToString();
                    break;
                case "Environmental Health: Public Pool":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                          || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                          || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                          || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team)
                    { taskName = "Commercial EHS Public Pool"; }
                    else if (project.AccelaPropertyType == PropertyTypeEnums.Townhomes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Single_Family_Homes
                       || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Master_Plans)
                    {
                        taskName = "EHS Residential Pools";
                    }
                    pool = pr.PoolPool.HasValue ? (pr.PoolPool.Value ? "Yes" : "No") : "No";
                    if (pr.PoolStartDate.HasValue) startDate = pr.PoolStartDate.Value.ToString();
                    if (pr.PoolEndDate.HasValue) dueDate = pr.PoolEndDate.Value.ToString();
                    break;
                case "Environmental Health: Facilities/Lodging":
                    if (project.AccelaPropertyType == PropertyTypeEnums.Commercial
                    || project.AccelaPropertyType == PropertyTypeEnums.FIFO_Small_Commercial
                    || project.AccelaPropertyType == PropertyTypeEnums.Mega_Multi_Family
                    || project.AccelaPropertyType == PropertyTypeEnums.Special_Projects_Team)
                    { taskName = "Commercial EHS Facility Lodging"; }

                    pool = pr.FacilPool.HasValue ? (pr.FacilPool.Value ? "Yes" : "No") : "No";
                    if (pr.FacilStartDate.HasValue) startDate = pr.FacilStartDate.Value.ToString();
                    if (pr.FacilEndDate.HasValue) dueDate = pr.FacilEndDate.Value.ToString();
                    break;
                case "Charlotte Water Backflow":
                    taskName = "CLTWTR Backflow Prevention";
                    pool = pr.BackfPool.HasValue ? (pr.BackfPool.Value ? "Yes" : "No") : "No";
                    if (pr.BackfStartDate.HasValue) startDate = pr.BackfStartDate.Value.ToString();
                    if (pr.BackfEndDate.HasValue) dueDate = pr.BackfEndDate.Value.ToString();
                    break;
                default:
                    pool = "No";
                    taskName = string.Empty;
                    startDate = string.Empty;
                    dueDate = string.Empty;
                    break;
            }


            var tableValues = Tuple.Create(pool, startDate, dueDate, taskName);
            return tableValues;
        }


        [TestMethod]
        [Ignore]
        public void SendTasksToAccelaTestPlanReview()
        {

            PlanReview planReview = new PlanReview();
            planReview.ID = 12885;
            DateTime now = DateTime.Today.AddDays(-1);
            DateTime end = DateTime.Today.AddDays(4);
            planReview.BackfStartDate = now;
            planReview.BackfEndDate = end;

            int id = 0;

            AccelaCustomTableTaskUpDateModelBE mAccelaCustomTableTaskUpDateModelBe = new AccelaCustomTableTaskUpDateModelBE();
            mAccelaCustomTableTaskUpDateModelBe.recordId = "REC21-00000-0014M";
            AccelaTaskUpDateDetail mAccelaTaskUpDateDetail;
            mAccelaTaskUpDateDetail = new AccelaTaskUpDateDetail();

            mAccelaCustomTableTaskUpDateModelBe.custTableTaskUpdateModel.Add(mAccelaTaskUpDateDetail);
            RequestCustomTablesTasksBE mRequestCustomTablesTasksBe = new RequestCustomTablesTasksBE();
            ProjectEstimation pf = new ProjectEstimation();
            pf.AccelaPropertyType = PropertyTypeEnums.County_Fire_Shop_Drawings;
            //  Project project = new EstimationCRUDAdapter().GetProjectDetailsByProjectId(12885);
            var tableValues = TableValueTasksMapAionToAccela(planReview, pf, "Fire_City Of Charlotte");
            List<TableFieldBE> mPRFieldBE = new List<TableFieldBE>();

            mPRFieldBE.Add(new TableFieldBE(null, "id", id++));
            mPRFieldBE.Add(new TableFieldBE(null, "Task Type", "Plan Review"));
            mPRFieldBE.Add(new TableFieldBE(null, "Task Name", tableValues.Item4));
            mPRFieldBE.Add(new TableFieldBE(null, "Pool Review", tableValues.Item1));
            mPRFieldBE.Add(new TableFieldBE(null, "Assignee", "Tammy" + " " + "Smith"));
            mPRFieldBE.Add(new TableFieldBE(null, "Due Date", tableValues.Item3));
            mPRFieldBE.Add(new TableFieldBE(null, "Cycle #", 1));
            mPRFieldBE.Add(new TableFieldBE(null, "StartDate", tableValues.Item2));
            mPRFieldBE.Add(new TableFieldBE(null, "EstimatedReviewTime", Convert.ToInt32("2")));
            mPRFieldBE.Add(new TableFieldBE(null, "Date-Time Stamp", DateTime.Now));
            mPRFieldBE.Add(new TableFieldBE(null, "Processing Status", "Added By AION"));
            mPRFieldBE.Add(new TableFieldBE(null, "Comments", ""));

            TableRowsBE mPRTableRowsBE =
                     new TableRowsBE("CE_COM-REVIEW.cTASK.cACTIVATION", TableRowBE.ActionEnum.Add, mPRFieldBE);

            mRequestCustomTablesTasksBe.array.Add(mPRTableRowsBE);




            mRequestCustomTablesTasksBe.recordId = "REC21-00000-000NE";



            //                var result = accelaApiBO.UpDateRecordCustomTables(mRequestCustomTablesTasksBe);
            //result.Wait();
            //                var response = result.Result;


            //mUTData.ClearDBTable(false, true, false);
        }

        //public PropertyTypeEnums MapAccelaPropertyType(Meck.Shared.MeckDataMapping.AccelaProjectModel model, Meck.Shared.MeckDataMapping.AccelaProjectDisplayInfo modelDisplay)
        //{
        //   PropertyTypeEnums ret = PropertyTypeEnums.NA;
        //   string RecordType = model.RecordType;
        //   string PropertyType = model.PropertyTypeRef;
        //   string ReviewType = model.ReviewTypeRef;
        //   string TypeOfWork = modelDisplay.TypeOfWork;
        //   switch (RecordType)
        //   {
        //      case "Commercial Project":

        //         switch (ReviewType)
        //         {
        //            case "Commercial Large":
        //               ret = PropertyTypeEnums.Commercial;
        //               break;
        //            case "Commercial Small":
        //               ret = PropertyTypeEnums.FIFO_Small_Commercial;
        //               break;
        //            case "Mega Multi Family":
        //               ret = PropertyTypeEnums.Mega_Multi_Family;
        //               break;
        //            case "Special Projects Team":
        //               ret = PropertyTypeEnums.Special_Projects_Team;
        //               break;
        //            default:
        //               ret = PropertyTypeEnums.NA;
        //               break;
        //         }
        //         break;
        //      case "Residential Project":
        //         switch (ReviewType)
        //         {
        //            case "Townhouse < 3.5 Stories":
        //               ret = PropertyTypeEnums.Townhomes;
        //               break;
        //            case "Single Family":
        //               if (TypeOfWork == "Addition (expand footprint)" ||
        //               TypeOfWork == "Addition (footprint not expanded)" ||
        //               TypeOfWork == "Upfit (interior completion)" ||
        //               TypeOfWork == "Upfit (interior renovation)")
        //                  ret = PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home;
        //               else ret = PropertyTypeEnums.FIFO_Single_Family_Homes;
        //               break;
        //            case "Masterplan":
        //               ret = PropertyTypeEnums.FIFO_Master_Plans;
        //               break;
        //            default:
        //               ret = PropertyTypeEnums.NA;
        //               break;
        //         }
        //         break;
        //      case "Commercial RTAP":
        //         switch (ReviewType)
        //         {
        //            case "Commercial Large":
        //               ret = PropertyTypeEnums.Commercial;
        //               break;
        //            case "Commercial Small":
        //               ret = PropertyTypeEnums.FIFO_Small_Commercial;
        //               break;
        //            default:
        //               ret = PropertyTypeEnums.NA;
        //               break;
        //         }
        //         break;
        //      case "Residential RTAP":
        //         switch (PropertyType)
        //         {
        //            case "Townhouse <3.5 stories":
        //               ret = PropertyTypeEnums.Townhomes;
        //               break;
        //            case "Single Family":
        //               ret = PropertyTypeEnums.FIFO_Single_Family_Homes;
        //               break;
        //            case "Masterplan":
        //               ret = PropertyTypeEnums.FIFO_Master_Plans;
        //               break;
        //            default:
        //               ret = PropertyTypeEnums.NA;
        //               break;
        //         }
        //         break;
        //      case "County Fire Shop Drawings":
        //         ret = PropertyTypeEnums.County_Shop_Drawings;
        //         break;
        //      case "Preliminary Meeting":
        //         switch (ReviewType)
        //         {
        //            case "Commercial":
        //            case "Commercial Large":
        //               ret = PropertyTypeEnums.Commercial;
        //               break;
        //            case "Commercial Small":
        //               ret = PropertyTypeEnums.FIFO_Small_Commercial;
        //               break;
        //            case "Mega Multi Family":
        //               ret = PropertyTypeEnums.Mega_Multi_Family;
        //               break;
        //            case "Special Projects Team":
        //               ret = PropertyTypeEnums.Special_Projects_Team;
        //               break;
        //            case "Townhouse < 3.5 Stories":
        //               ret = PropertyTypeEnums.Townhomes;
        //               break;
        //            case "Single Family":
        //               if (TypeOfWork == "Addition (expand footprint)" ||
        //               TypeOfWork == "Addition (footprint not expanded)" ||
        //               TypeOfWork == "Upfit (interior completion)" ||
        //               TypeOfWork == "Upfit (interior renovation)")
        //                  ret = PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home;
        //               else ret = PropertyTypeEnums.FIFO_Single_Family_Homes;
        //               break;
        //            case "Masterplan":
        //               ret = PropertyTypeEnums.FIFO_Master_Plans;
        //               break;
        //            case "County Fire Shop Drawings":
        //               ret = PropertyTypeEnums.County_Shop_Drawings;
        //               break;
        //            default:
        //               ret = PropertyTypeEnums.NA;
        //               break;
        //         }
        //         break;
        //      default:
        //         ret = PropertyTypeEnums.NA;
        //         break;
        //   }
        //   return ret;
        //}
        public string MapPropertyTypeForDisplayOnly(Meck.Shared.MeckDataMapping.AccelaProjectModel model, Meck.Shared.MeckDataMapping.AccelaProjectDisplayInfo modelDisplay)
        {
            string RecordType = model.RecordType;

            string propertyTypeForDisplayOnly;
            switch (RecordType)
            {
                case "Commercial Project":
                case "Commercial RTAP":
                case "Residential RTAP":
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
        [TestMethod]
        [Ignore]
        public void AionPropertyTypeTest()
        {
            Meck.Shared.MeckDataMapping.AccelaProjectModel model = new Meck.Shared.MeckDataMapping.AccelaProjectModel();
            Meck.Shared.MeckDataMapping.AccelaProjectDisplayInfo modelDisplay = new Meck.Shared.MeckDataMapping.AccelaProjectDisplayInfo();
            model.RecordType = "Residential RTAP";
            model.PropertyTypeRef = "Townhouse <3.5 stories";
            model.ReviewTypeRef = "Commercial Small";
            modelDisplay.TypeOfWork = "Addition (expand footprint)";

            //PropertyTypeEnums propertyType = MapAccelaPropertyType(model, modelDisplay);
            //var propertyTypetest = propertyType;

            //RecordRelatedModelBE relatedRecords = new EstimationAccelaAdapter().GetRelatedRecord("REC21-00000-0014X", RecordRelatedModelBE.RelationshipEnum.Parent);
            //var accelaprojectmodel = new EstimationAccelaAdapter().GetProjectDetailsLoad(new ProjectParms {ProjectId = relatedRecords.CustomId, RecIdTxt = relatedRecords.Id });
            Project project = new EstimationCRUDAdapter().GetProjectDetailsByProjectId(18172);

            if (project.IsProjectRTAP)
            {
                RecordRelatedModelBE relatedRecords = new EstimationAccelaAdapter().GetRelatedRecord(project.RecIdTxt, RecordRelatedModelBE.RelationshipEnum.Parent);
                var accelaprojectmodelRTAP = new EstimationAccelaAdapter().GetProjectDetailsLoad(new ProjectParms { ProjectId = relatedRecords.CustomId, RecIdTxt = relatedRecords.Id });
                //propertyType = accelaProjectModel.IsExpress ? PropertyTypeEnums.Express : accelaPropertyTypeBO.MapAccelaPropertyType(accelaprojectmodelRTAP);
                var ret = MapAccelaPropertyType(accelaprojectmodelRTAP);
                var recordid = accelaprojectmodelRTAP.ProjectIDRef;
                var projectnumber = accelaprojectmodelRTAP.ProjectNumber;
            }
            else
            {
                //propertyType = accelaProjectModel.IsExpress ? PropertyTypeEnums.Express : accelaPropertyTypeBO.MapAccelaPropertyType(accelaProjectModel);
                //var ret = MapAccelaPropertyType(accelaprojectmodel);
            }




            //var ret = MapAccelaPropertyType(accelaprojectmodel);

        }
        public PropertyTypeEnums MapAccelaPropertyType(Meck.Shared.MeckDataMapping.AccelaProjectModel model)
        {
            PropertyTypeEnums ret = PropertyTypeEnums.NA;
            string RecordType = model.RecordType;
            string PropertyType = model.PropertyTypeRef;
            string ReviewType = model.ReviewTypeRef;
            string TypeOfWork = model.DisplayOnlyInformation.TypeOfWork;
            switch (RecordType)
            {
                case "Commercial Project":

                    switch (ReviewType)
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
                    break;
                case "Residential Project":
                    switch (ReviewType)
                    {
                        case "Townhouse < 3.5 Stories":
                            ret = PropertyTypeEnums.Townhomes;
                            break;
                        case "Single Family":
                            if (TypeOfWork == "Addition (expand footprint)" ||
                            TypeOfWork == "Addition (footprint not expanded)" ||
                            TypeOfWork == "Upfit (interior completion)" ||
                            TypeOfWork == "Upfit (interior renovation)")
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
                    break;
                case "County Fire Shop Drawings":
                    ret = PropertyTypeEnums.County_Fire_Shop_Drawings;
                    break;
                case "Preliminary Meeting":
                    switch (ReviewType)
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
                        case "Special Projects Team":
                            ret = PropertyTypeEnums.Special_Projects_Team;
                            break;
                        case "Townhouse < 3.5 Stories":
                            ret = PropertyTypeEnums.Townhomes;
                            break;
                        case "Single Family":
                            if (TypeOfWork == "Addition (expand footprint)" ||
                            TypeOfWork == "Addition (footprint not expanded)" ||
                            TypeOfWork == "Upfit (interior completion)" ||
                            TypeOfWork == "Upfit (interior renovation)")
                                ret = PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home;
                            else ret = PropertyTypeEnums.FIFO_Single_Family_Homes;
                            break;
                        case "Masterplan":
                            ret = PropertyTypeEnums.FIFO_Master_Plans;
                            break;
                        case "County Fire Shop Drawings":
                            ret = PropertyTypeEnums.County_Fire_Shop_Drawings;
                            break;
                        default:
                            ret = PropertyTypeEnums.NA;
                            break;
                    }
                    break;
                default:
                    ret = PropertyTypeEnums.NA;
                    break;
            }
            return ret;
        }

        [TestMethod]
        [Ignore]
        public void SendTasksToAccelaTestFIFOSSchedule()
        {
            AccelaBOAdapter adapter = new AccelaBOAdapter();

            FIFOSchedule fifoschedule = new FIFOSchedule();
            fifoschedule.ID = 12885;
            DateTime now = DateTime.Today.AddDays(2);
            DateTime end = DateTime.Today.AddDays(4);
            fifoschedule.BackfStartDate = now;
            fifoschedule.BackfEndDate = end;

            int id = 0;

            AccelaCustomTableTaskUpDateModelBE mAccelaCustomTableTaskUpDateModelBe = new AccelaCustomTableTaskUpDateModelBE();
            mAccelaCustomTableTaskUpDateModelBe.recordId = "REC21-00000-000G4";
            AccelaTaskUpDateDetail mAccelaTaskUpDateDetail;
            mAccelaTaskUpDateDetail = new AccelaTaskUpDateDetail();

            mAccelaCustomTableTaskUpDateModelBe.custTableTaskUpdateModel.Add(mAccelaTaskUpDateDetail);
            RequestCustomTablesTasksBE mRequestCustomTablesTasksBe = new RequestCustomTablesTasksBE();
            ProjectEstimation pf = new ProjectEstimation();
            pf.AccelaPropertyType = PropertyTypeEnums.FIFO_Small_Commercial;
            //  Project project = new EstimationCRUDAdapter().GetProjectDetailsByProjectId(12885);
            var tableValues = adapter.TableValueTasksMapAionToAccela(fifoschedule, pf, "Charlotte Water Backflow");
            List<TableFieldBE> mPRFieldBE = new List<TableFieldBE>();

            mPRFieldBE.Add(new TableFieldBE(null, "id", id++));
            mPRFieldBE.Add(new TableFieldBE(null, "Task Type", "Plan Review"));
            mPRFieldBE.Add(new TableFieldBE(null, "Task Name", tableValues.Item4));
            mPRFieldBE.Add(new TableFieldBE(null, "Pool Review", tableValues.Item1));
            mPRFieldBE.Add(new TableFieldBE(null, "Assignee", "Tammy" + " " + "Smith"));
            mPRFieldBE.Add(new TableFieldBE(null, "Due Date", tableValues.Item3));
            mPRFieldBE.Add(new TableFieldBE(null, "Cycle #", 1));
            mPRFieldBE.Add(new TableFieldBE(null, "StartDate", tableValues.Item2));
            mPRFieldBE.Add(new TableFieldBE(null, "EstimatedReviewTime", Convert.ToInt32("2")));
            mPRFieldBE.Add(new TableFieldBE(null, "Date-Time Stamp", DateTime.Now));
            mPRFieldBE.Add(new TableFieldBE(null, "Processing Status", "Added By AION"));
            mPRFieldBE.Add(new TableFieldBE(null, "Comments", ""));

            TableRowsBE mPRTableRowsBE =
                     new TableRowsBE("CE_COM-REVIEW.cTASK.cACTIVATION", TableRowBE.ActionEnum.Add, mPRFieldBE);

            mRequestCustomTablesTasksBe.array.Add(mPRTableRowsBE);

            mRequestCustomTablesTasksBe.recordId = "REC21-00000-000G4";

            Assert.AreEqual(now.ToString(), tableValues.Item2);
            Assert.AreEqual(end.ToString(), tableValues.Item3);
            Assert.AreEqual("CLTWTR Backflow Prevention", tableValues.Item4);
            Assert.AreEqual("No", tableValues.Item1);
        }

        [TestMethod]
        [Ignore]
        public async Task UpdateRecordAddSingleTaskTest()
        {
            AccelaApiBO accelaApiBO = new AccelaApiBO();
            string loggingGuid = Guid.NewGuid().ToString();
            string message = loggingGuid;
            UserBE userBE = new UserBE();
            BusinessRefBO businessRefBO = new BusinessRefBO();
            BusinessRefBE businessRefBE = new BusinessRefBE();


            AccelaCustomTableTaskUpDateModelBE mAccelaCustomTableTaskUpDateModelBe = new AccelaCustomTableTaskUpDateModelBE();
            int currentCycle = 1;
            string startDate, gateDate, dueDate;
            AION.Manager.Adapters.PlanReviewAdapter prAdapter = new AION.Manager.Adapters.PlanReviewAdapter();
            List<PlanReview> prs = prAdapter.GetPlanReviewsByProjectId(424730);
            PlanReview pr = prs.FirstOrDefault(x => x.IsCurrentCycle == true);
            //424798, 424871 not working, 424757 424818 working


            gateDate = pr.GateDate.ToString();
            startDate = string.Empty;
            dueDate = string.Empty;

            string processingStatus = pr.IsReschedule ? "Change by AION" : "Added by AION";
            string taskId = string.Empty;
            int id = 0;

            RequestCustomTablesTasksBE mRequestCustomTablesTasksBe = new RequestCustomTablesTasksBE();


            Project project = new EstimationCRUDAdapter().GetProjectDetailsByProjectId(424730);

            if (project != null)
            {
                bool IsPreliminary = project.IsProjectPreliminary == true ? project.IsProjectPreliminary : false;
                //taskId = AccelaPropertyTypeToTaskId(project.AccelaPropertyType, project.IsProjectRTAP);
                taskId = AccelaPropertyTypeToTaskId(project.AccelaPropertyType, project.IsProjectRTAP, IsPreliminary);


            }
            if (project.AssignedFacilitator != null)
            {
                userBE = new UserBO().GetById(project.AssignedFacilitator.Value);
            }



            if (userBE != null)
            {
                if (userBE.UserName != null)
                {

                    var recordValues = GenerateGateAndFacilitatorRecords(currentCycle, project, taskId, gateDate);

                    mRequestCustomTablesTasksBe = recordValues.Item1;
                    id = recordValues.Item2;

                }
            }




            //TODO - consider ScheduleBusinessRelationship table for subsequent cycle hours
            List<ProjectBusinessRelationshipBE> projectBusinessRelationshipBEs = new ProjectBusinessRelationshipBO().GetListByProjectId(project.ID);

            //if (pr.Cycle == 1)
            if (currentCycle == 1)
            {
                foreach (ProjectBusinessRelationshipBE projectBusinessRelationshipBE in projectBusinessRelationshipBEs)
                {
                    if (projectBusinessRelationshipBE.AssignedPlanReviewerId != null && projectBusinessRelationshipBE.AssignedPlanReviewerId != -1)
                    {
                        userBE = new UserBO().GetById(projectBusinessRelationshipBE.AssignedPlanReviewerId.Value);  //what values is this for each task type?
                    }
                    else continue;

                    businessRefBE = new BusinessRefBO().GetById(projectBusinessRelationshipBE.BusinessRefId.Value);

                    //var tableValues = TableValueTasksMapAionToAccela(pr, project, businessRefBE.BusinessName);
                    var tableValues = TableValueTasksMapAionToAccela(pr, project, businessRefBE.BusinessName);

                    TableRowsBE mPRTableRowsBE = GeneratePlanReviewRecord(
                         id++,
                         tableValues.Item4,
                         tableValues.Item1,
                         userBE,
                         tableValues.Item3,
                         currentCycle,
                         tableValues.Item2,
                         projectBusinessRelationshipBE.EstimationHoursNbr.GetValueOrDefault(0m),
                         processingStatus,
                         taskId);

                    mRequestCustomTablesTasksBe.array.Add(mPRTableRowsBE);
                }
            }
            else
            {
                //List<ScheduleBusinessRelationship> scheduleBusinessRelationships = new SchedulerAdapter().GetScheduleBusinessRelationshipListByCycle(new ProjectParms { ProjectId = pr.AccelaProjectRefId, CycleNbr = pr.Cycle, RecIdTxt = project.RecIdTxt });
                ProjectCycle projectCycle = new PlanReviewAdapter().GetProjectCyclesByProjectId(project.ID).FirstOrDefault(x => x.CurrentCycleInd == true);

                List<ProjectCycleDetail> projectCycleDetails = new PlanReviewAdapter().GetProjectCycleDetailsByProjectCycleId(projectCycle.ID);
                foreach (ProjectCycleDetail sbr in projectCycleDetails)
                {
                    userBE = new UserBO().GetById(projectBusinessRelationshipBEs.Where(x => x.BusinessRefId == sbr.BusinessRefId).FirstOrDefault().AssignedPlanReviewerId.Value);  //what values is this for each task type?

                    businessRefBE = new BusinessRefBO().GetById(sbr.BusinessRefId.Value);

                    var tableValues = TableValueTasksMapAionToAccela(pr, project, businessRefBE.BusinessName);

                    TableRowsBE mPRTableRowsBE = GeneratePlanReviewRecord(
                    id++,
                    tableValues.Item4,
                    tableValues.Item1,
                    userBE,
                    tableValues.Item3,
                    currentCycle,
                    tableValues.Item2,
                    sbr.RereviewHoursNbr.GetValueOrDefault(0m),
                    processingStatus,
                    taskId);

                    mRequestCustomTablesTasksBe.array.Add(mPRTableRowsBE);
                }
            }

            mRequestCustomTablesTasksBe.recordId = project.RecIdTxt;


            IAccelaEngine thisengine = new AccelaApiBO();

            var result = await thisengine.UpDateRecordCustomTables(mRequestCustomTablesTasksBe);

            Assert.IsTrue(result.Count > 0);



        }

        public Tuple<RequestCustomTablesTasksBE, int> GenerateGateAndFacilitatorRecords(int cycle, Project project, string taskId, string gateDate)
        {
            string loggingGuid = Guid.NewGuid().ToString();
            string message = loggingGuid;
            UserBE userBE = new UserBE();

            AccelaCustomTableTaskUpDateModelBE mAccelaCustomTableTaskUpDateModelBe = new AccelaCustomTableTaskUpDateModelBE();

            string processingStatus = "Added by AION";

            RequestCustomTablesTasksBE mRequestCustomTablesTasksBe = new RequestCustomTablesTasksBE();
            int id = 0;

            try
            {
                if (project != null)
                {
                    if (string.IsNullOrEmpty(taskId))
                    {
                        //string errorMessage = "Error in AccelaBOAdapter ScheduleReview - Task ID is null";

                        //var logging = Logger.LogMessageAsync(Enums.LoggingType.Tracing, MethodBase.GetCurrentMethod(), errorMessage,
                        //    string.Empty, string.Empty, string.Empty);

                        throw new Exception();
                    }

                    mAccelaCustomTableTaskUpDateModelBe.recordId = project.RecIdTxt;
                    AccelaTaskUpDateDetail mAccelaTaskUpDateDetail;
                    mAccelaTaskUpDateDetail = new AccelaTaskUpDateDetail();

                    mAccelaCustomTableTaskUpDateModelBe.custTableTaskUpdateModel.Add(mAccelaTaskUpDateDetail);

                    if (project.AssignedFacilitator != null)
                    {
                        userBE = new UserBO().GetById(project.AssignedFacilitator.Value);
                    }

                    if (userBE != null)
                    {
                        if (userBE.UserName != null)
                        {
                            TableRowsBE mGateTableRowsBE = GenerateGateRecord(id, cycle, processingStatus, taskId, gateDate);
                            mRequestCustomTablesTasksBe.array.Add(mGateTableRowsBE);

                            id++;

                            TableRowsBE mFCTableRowsBE = GenerateFacilitatorRecord(id, userBE, cycle, processingStatus, taskId);
                            mRequestCustomTablesTasksBe.array.Add(mFCTableRowsBE);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in AccelaBOAdapter GenerateGateAndFacilitatorRecords - " + ex.Message;

                //var logging = Logger.LogMessageAsync(Enums.LoggingType.Tracing, MethodBase.GetCurrentMethod(), errorMessage,
                //    string.Empty, string.Empty, string.Empty);

                throw ex;
            }

            var tableValues = Tuple.Create(mRequestCustomTablesTasksBe, id);

            return tableValues;
        }


        private TableRowsBE GenerateGateRecord(int id, int cycle, string processingStatus, string taskId, string gateDate)
        {
            var timeUtc = DateTime.UtcNow;
            var easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            var today = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, easternZone);
            var todayFormat = today.ToString("ddd MMM dd HH:mm:ss EST yyyy");
            //var assigndHoursDec = assignedHours.ToString("0.0");
            List<TableFieldBE> mGateFieldBE = new List<TableFieldBE>();

            mGateFieldBE.Add(new TableFieldBE(null, "id", id));
            mGateFieldBE.Add(new TableFieldBE(null, "Task Type", "Control"));
            mGateFieldBE.Add(new TableFieldBE(null, "Task Name", "Gate"));
            mGateFieldBE.Add(new TableFieldBE(null, "Pool Review", "No"));
            mGateFieldBE.Add(new TableFieldBE(null, "Assignee", ""));
            mGateFieldBE.Add(new TableFieldBE(null, "Due Date", gateDate));
            mGateFieldBE.Add(new TableFieldBE(null, "Cycle #", cycle));
            mGateFieldBE.Add(new TableFieldBE(null, "StartDate", string.Empty));
            mGateFieldBE.Add(new TableFieldBE(null, "EstimatedReviewTime", "0"));
            mGateFieldBE.Add(new TableFieldBE(null, "Date-Time Stamp", todayFormat));
            mGateFieldBE.Add(new TableFieldBE(null, "Processing Status", processingStatus));
            mGateFieldBE.Add(new TableFieldBE(null, "Comments", ""));

            TableRowsBE mGateTableRowsBE =
                new TableRowsBE(taskId, TableRowBE.ActionEnum.Add, mGateFieldBE);

            return mGateTableRowsBE;
        }
        private TableRowsBE GenerateFacilitatorRecord(int id, UserBE userBE, int cycle, string processingStatus, string taskId)
        {
            var timeUtc = DateTime.UtcNow;
            var easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            var today = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, easternZone);
            var todayFormat = today.ToString("ddd MMM dd HH:mm:ss EST yyyy");
            var UserAD = userBE.ADName;
            //var assigndHoursDec = assignedHours.ToString("0.0");
            List<TableFieldBE> mFCFieldBE = new List<TableFieldBE>();

            mFCFieldBE.Add(new TableFieldBE(null, "id", id++));
            mFCFieldBE.Add(new TableFieldBE(null, "Task Type", "Control"));
            mFCFieldBE.Add(new TableFieldBE(null, "Task Name", "Facilitator Coordination"));
            mFCFieldBE.Add(new TableFieldBE(null, "Pool Review", "No"));
            mFCFieldBE.Add(new TableFieldBE(null, "Assignee", UserAD));
            //mFCFieldBE.Add(new TableFieldBE(null, "Assignee", userBE.FirstNm + " " + userBE.LastNm));
            mFCFieldBE.Add(new TableFieldBE(null, "Due Date", string.Empty));
            mFCFieldBE.Add(new TableFieldBE(null, "Cycle #", cycle));
            mFCFieldBE.Add(new TableFieldBE(null, "StartDate", string.Empty));
            mFCFieldBE.Add(new TableFieldBE(null, "EstimatedReviewTime", "0"));
            mFCFieldBE.Add(new TableFieldBE(null, "Date-Time Stamp", todayFormat));
            mFCFieldBE.Add(new TableFieldBE(null, "Processing Status", processingStatus));
            mFCFieldBE.Add(new TableFieldBE(null, "Comments", ""));

            TableRowsBE mFCTableRowsBE =
                new TableRowsBE(taskId, TableRowBE.ActionEnum.Add, mFCFieldBE);

            return mFCTableRowsBE;
        }

        private TableRowsBE GeneratePlanReviewRecord(int id, string taskName, string poolReview, UserBE userBE, string dueDate, int cycle, string startDate, decimal assignedHours, string processingStatus, string taskId)
        {
            var timeUtc = DateTime.UtcNow;
            var easternZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");
            var today = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, easternZone);
            var todayFormat = today.ToString("ddd MMM dd HH:mm:ss EST yyyy");
            var UserAD = userBE.ADName;
            var assigndHoursDec = assignedHours.ToString("0.0");

            List<TableFieldBE> mPRFieldBE = new List<TableFieldBE>();

            mPRFieldBE.Add(new TableFieldBE(null, "id", id++));
            mPRFieldBE.Add(new TableFieldBE(null, "Task Type", "Plan Review"));
            mPRFieldBE.Add(new TableFieldBE(null, "Task Name", taskName));
            mPRFieldBE.Add(new TableFieldBE(null, "Pool Review", poolReview));
            mPRFieldBE.Add(new TableFieldBE(null, "Assignee", UserAD));
            mPRFieldBE.Add(new TableFieldBE(null, "Due Date", dueDate));
            mPRFieldBE.Add(new TableFieldBE(null, "Cycle #", cycle));
            mPRFieldBE.Add(new TableFieldBE(null, "StartDate", startDate));
            mPRFieldBE.Add(new TableFieldBE(null, "EstimatedReviewTime", assigndHoursDec));
            mPRFieldBE.Add(new TableFieldBE(null, "Date-Time Stamp", todayFormat));
            mPRFieldBE.Add(new TableFieldBE(null, "Processing Status", processingStatus));
            mPRFieldBE.Add(new TableFieldBE(null, "Comments", ""));

            TableRowsBE mPRTableRowsBE =
                       new TableRowsBE(taskId, TableRowBE.ActionEnum.Add, mPRFieldBE);

            return mPRTableRowsBE;
        }

        public string AccelaPropertyTypeToTaskId(PropertyTypeEnums propertyType, bool IsProjectRTAP, bool IsPreliminary = false)
        {
            string taskID = string.Empty;

            if (IsPreliminary) return "CE_PRELIM-REVIEW.cTASK.cACTIVATION";

            switch (propertyType)
            {
                case PropertyTypeEnums.Express:
                    if (!IsProjectRTAP)
                    {
                        taskID = "CE_COM-REVIEW.cTASK.cACTIVATION";
                    }
                    else
                    {
                        taskID = "CE_CRTAP-REVIEW.cTASK.cACTIVATION";
                    }
                    break;

                case PropertyTypeEnums.Commercial:
                    if (!IsProjectRTAP)
                    {
                        taskID = "CE_COM-REVIEW.cTASK.cACTIVATION";
                    }
                    else
                    {
                        taskID = "CE_CRTAP-REVIEW.cTASK.cACTIVATION";
                    }
                    break;

                case PropertyTypeEnums.Residential:
                    if (!IsProjectRTAP)
                    {
                        taskID = "CE_RES-REVIEW.cTASK.cACTIVATION";
                    }
                    else
                    {
                        taskID = "CE_RRTAP-REVIEW.cTASK.cACTIVATION";
                    }
                    break;

                case PropertyTypeEnums.Mega_Multi_Family:
                    {
                        taskID = "CE_COM-REVIEW.cTASK.cACTIVATION";
                        break;
                    }
                case PropertyTypeEnums.Special_Projects_Team:
                    {
                        taskID = "CE_COM-REVIEW.cTASK.cACTIVATION";
                        break;
                    }
                case PropertyTypeEnums.FIFO_Small_Commercial:
                    {
                        taskID = "CE_COM-REVIEW.cTASK.cACTIVATION";
                        break;
                    }
                case PropertyTypeEnums.FIFO_Master_Plans:
                    {
                        taskID = "CE_RES-REVIEW.cTASK.cACTIVATION";
                        break;
                    }
                case PropertyTypeEnums.FIFO_Single_Family_Homes:
                    {
                        taskID = "CE_RES-REVIEW.cTASK.cACTIVATION";
                        break;
                    }
                case PropertyTypeEnums.FIFO_Addition_Renovation_Single_Family_Home:
                    {
                        taskID = "CE_RES-REVIEW.cTASK.cACTIVATION";
                        break;
                    }
                case PropertyTypeEnums.Townhomes:
                    {
                        taskID = "CE_RES-REVIEW.cTASK.cACTIVATION";
                        break;
                    }

                case PropertyTypeEnums.County_Fire_Shop_Drawings:
                    taskID = "CE_CFSD-REVIEW.cTASK.cACTIVATION";

                    break;

                default:
                    break;
            }
            return taskID;
        }

        [TestMethod]
        [Ignore]
        public void InsertAEDataTest()
        {
            IAIONDBEngine mAIONDBEngine = new AIONEngineCrudApiBO();

            AccelaAIONAEData _mAccelaAIONAEData = new AccelaAIONAEData()
            {
                ACCELA_AE_DATA_ID = 0,
                SYSTEM_USER_NM = "UnitTestUser",
                REC_ID_NUM = "Unit_Test_1",
                PLAN_REVIEW_TYP_DESC = "Unit TestDetail Type",
                CYCLE_NBR = 1,
                LICENSE_TYP_DESC = "Test License Desc",
                PROJECT_SCORE_DESC = "Project score",
                PROJECT_CREATED_DTTM = DateTime.Now,
                PASS_FAIL_IND = true,
                FAILURE_CAUSE_TXT = " test Fail;ure cause",
                FAILURE_REASON_TXT = " Test Failure Reason",
                WKR_ID_CREATED_TXT = "UnitTesTuser",
                CREATED_DTTM = DateTime.Now,
                WKR_ID_UPDATED_TXT = "iupdateuser",
                UPDATED_DTTM = DateTime.Now
            };
            var result = mAIONDBEngine.InsertNewAEData(_mAccelaAIONAEData);
            // result.Wait();

            var response = result;

            Console.WriteLine(JsonConvert.SerializeObject(response));

            Assert.IsTrue(response.errors == string.Empty);




            //var getResult = mAIONDBEngine.GetAEDataByRecordId(_mAccelaAIONAEData.REC_ID_NUM);
            //getResult.Wait();

            //var getResponse = getResult.Result;

            // Assert.IsTrue(result.Result );


            var deleteResult =
                Task.Run(() => mAIONDBEngine.DeleteAERecordsByRecordId(response.CurrentRecord.REC_ID_NUM));
            deleteResult.Wait();

            var deleteResponse = deleteResult.Result;

            Assert.IsTrue(deleteResponse);

            //mUTData.ClearDBTable(false, false, true);

        }






    }
}
