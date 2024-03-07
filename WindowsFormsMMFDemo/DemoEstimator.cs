using AION.BL;
using AION.BL.Adapters;
using AION.BL.Controller;
using AION.BL.Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Text;



namespace DemoInterface
{
    public class MMFEstimation
    {
        public string ProjectId { get; set; }
        public string ReviewSqFt { get; set; }
        public string CostOfConstruction { get; set; }
        public string NumberofSheets { get; set; }
        public string Trade { get; set; }
        public string OccupancyType { get; set; }

        public string EstimationHours { get; set; }
        public string Hours { get; set; }
    }

    public class DemoEstimator
    {
        private List<MMFEstimation> odemodEstimate;

        private ProjectAutoEstimationEngine _autocalc;
        //  private IProjectAutoEstimationEngine _projectadapter;

        private EstimationAccelaAdapter _api;
        private EstimationCRUDAdapter _crud;
        private ProjectEstimationAdapter _estimation;
        //  private AccelaProjectModel _accelaProjectModel;

        private ProjectParms _projectparms;
        //  public ProjectEstimation _projectestimation;
        //  private List<ProjectTrade> _trades;
        //  private List<ProjectAgency> _agencies;
        //  private float _hours;
        //   private float _costofconstruction;
        //   private int _noofsheets;
        //   private int _reviewsqft;
        //   private string _occupancytype;
        private string _projectid;


        public DemoEstimator()
        {
            _api = new EstimationAccelaAdapter();
            _crud = new EstimationCRUDAdapter();
            _estimation = new ProjectEstimationAdapter();
            _autocalc = new ProjectAutoEstimationEngine(); // _api, _crud, _estimation);
            //    _hours = 300;
            //     _costofconstruction = 20000;
            //     _noofsheets = 1;
            //     _reviewsqft = 2000;
            //     _occupancytype = "R4";
            _projectid = "1";
            _projectparms = new ProjectParms
            {
                ProjectId = _projectid
            };

        }


        public string ProjectEstimationDefaultsCreationTest()
        {
            odemodEstimate = new List<MMFEstimation>();




            ProjectEstimation aionProjectModel = new ProjectEstimation
            {
                Trades = new List<ProjectTrade>(),
                Agencies = new List<ProjectAgency>(),
                AccelaPropertyType = PropertyTypeEnums.Mega_Multi_Family,
                AccelaProjectRefId = "1",
                AccelaCostOfConstruction = 10.0,
                AccelaNumberofSheets = 12,
                AccelaOccupancyType = "R1",
                AccelaSqrFtToBeReviewed = 1200,

            };
            aionProjectModel.Trades.Add(new ProjectTrade
            {
                DepartmentTypeEnum = DepartmentTypeEnum.Trade,
                EstimationHours = 0,
                DepartmentInfo = DepartmentNameEnums.Building
            });
            aionProjectModel.Trades.Add(new ProjectTrade
            {
                DepartmentTypeEnum = DepartmentTypeEnum.Trade,
                EstimationHours = 0,
                DepartmentInfo = DepartmentNameEnums.Building
            });
            aionProjectModel.Agencies.Add(new ProjectAgency
            {
                DepartmentInfo = DepartmentNameEnums.Building
            });
            _estimation.PerformAutoEstimation(aionProjectModel);

            CreateDemoOutput(aionProjectModel);

            CreateCSVfile(odemodEstimate, "AutoEstimationMMF", false);

            return "Demo file created";
        }


        public void CreateDemoOutput(ProjectEstimation demooutput)
        {
            foreach (var otrade in demooutput.Trades)
            {
                MMFEstimation demodata = new MMFEstimation()
                {
                    ProjectId = demooutput.AccelaProjectRefId,
                    ReviewSqFt = demooutput.AccelaSqrFtToBeReviewed.ToString(),
                    CostOfConstruction = demooutput.AccelaCostOfConstruction.ToString(),
                    NumberofSheets = demooutput.AccelaNumberofSheets.ToString(),
                    OccupancyType = demooutput.AccelaOccupancyType.ToString(),
                    EstimationHours = otrade.EstimationHours.ToString()
                };
                odemodEstimate.Add(demodata);
            }
        }


        public bool CreateCSVfile(IEnumerable outputobject, string filename, bool overwrite)
        {
            string _newOutputPath = ConfigurationManager.AppSettings["DemoCsvPath"];

            //using (var csv = new CsvWriter(new StreamWriter(_newOutputPath + filename + ".csv", overwrite)))
            //{
            //    csv.Configuration.ShouldQuote = (field, context) =>
            //    {
            //        var quote =
            //            new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 }.Contains(
            //                context.Record.Count);
            //        return quote;
            //    };

            //    csv.WriteRecords(outputobject);

            //    csv.Flush();

            //    csv.Dispose();
            //}

            Debug.WriteLine("*** CSV File Completed ");
            return true;
        }


        /// <summary>
        ///  RunJsonSamples()
        /// </summary>
        /// <returns></returns>
        public string RunJsonSamples()
        {
            odemodEstimate = new List<MMFEstimation>();



            string readContents;
            string filepath = AppContext.BaseDirectory + "MMFTestData.json";

            Console.WriteLine(" Reading file at {0}", filepath);
            //   File.SetAttributes(AppContext.BaseDirectory + ConfigurationManager.AppSettings["MappingFile"], FileAttributes.Normal);
            using (StreamReader streamReader = new StreamReader(filepath, Encoding.UTF8))
            {
                readContents = streamReader.ReadToEnd();
            }

            List<MMFInputs> mMMFProjects = JsonConvert.DeserializeObject<List<MMFInputs>>(readContents);


            StringBuilder sb = new StringBuilder();

            foreach (MMFInputs _mommfinputs in mMMFProjects)
            {
                sb.AppendLine(EPMBasedProjectEstimationDefaultsCreationTest(_mommfinputs));
            }

            CreateCSVfile(odemodEstimate, "EPMSampleDataAutoEstimationMMF", false);

            sb.AppendLine("EPM detail process to Excel");

            return sb.ToString();

        }

        /// <summary>
        ///  EPMBasedProjectEstimationDefaultsCreationTest
        /// </summary>
        /// <returns></returns>
        public string EPMBasedProjectEstimationDefaultsCreationTest(MMFInputs request)
        {
            //      var trade1esthrs = 3;
            //      var trade2esthrs = 3;
            //      var agency1esthrs = 0;
            ProjectEstimation aionProjectModel = new ProjectEstimation
            {
                Trades = new List<ProjectTrade>(),
                Agencies = new List<ProjectAgency>(),
                AccelaPropertyType = PropertyTypeEnums.Mega_Multi_Family,
                AccelaProjectRefId = request.ProjectId,
                AccelaCostOfConstruction = (double)request.CostOfConstruction,
                AccelaNumberofSheets = request.NumberofSheets,
                AccelaOccupancyType = "R1",
                AccelaSqrFtToBeReviewed = Convert.ToInt32(request.SqrFt),
            };
            aionProjectModel.Trades.Add(new ProjectTrade
            {
                DepartmentTypeEnum = DepartmentTypeEnum.Trade,
                EstimationHours = .0069M,
                DepartmentInfo = DepartmentNameEnums.Building
            });
            aionProjectModel.Trades.Add(new ProjectTrade
            {
                DepartmentTypeEnum = DepartmentTypeEnum.Trade,
                EstimationHours = .0075M,
                DepartmentInfo = DepartmentNameEnums.Building
            });

            aionProjectModel.Trades.Add(new ProjectTrade
            {
                DepartmentTypeEnum = DepartmentTypeEnum.Trade,
                EstimationHours = .0032M,
                DepartmentInfo = DepartmentNameEnums.Building
            });

            aionProjectModel.Trades.Add(new ProjectTrade
            {
                DepartmentTypeEnum = DepartmentTypeEnum.Trade,
                EstimationHours = .0045M,
                DepartmentInfo = DepartmentNameEnums.Building
            });


            aionProjectModel.Agencies.Add(new ProjectAgency
            {
                DepartmentInfo = DepartmentNameEnums.Building
            });
            _estimation.PerformAutoEstimation(aionProjectModel);

            CreateDemoOutput(aionProjectModel);
            return "Sample for " + request.ProjectId + " done";
        }
    }
}
