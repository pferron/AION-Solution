using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AION.BL.Test.MockRepositories
{
	public class MockLegacyProjectEstimationHoursRefBO : IDataContextLegacyProjectDataBO
	{
		public List<LegacyProjectEstimationHoursRefBE> LegacyProjectData { get; set; } =
			new List<LegacyProjectEstimationHoursRefBE>() {
			new LegacyProjectEstimationHoursRefBE() { 
				OccupancyTypRefId = 1, 
				ConstrTypTxt = "UPFITRTAP",
				TotalProjectsCnt = 1525,
				BuildHoursNbr = 2441.2m,
				ElectHoursNbr = 2350.5m,
				MechHoursNbr = 1595.2m,
				PlumbHoursNbr = 1247.1m,
				TotalSquareFootageCnt =  82312152,
				TotalSheetsCnt = 56671,
				TotalConstrCostAmt = 2342945097m
			},
			new LegacyProjectEstimationHoursRefBE() {
				OccupancyTypRefId = 1,
				ConstrTypTxt = "NEWCONSTRUCTION",
				TotalProjectsCnt = 85,
				BuildHoursNbr = 709.3m,
				ElectHoursNbr = 581.3m,
				MechHoursNbr = 266m,
				PlumbHoursNbr = 260m,
				TotalSquareFootageCnt =  36019522,
				TotalSheetsCnt = 14781,
				TotalConstrCostAmt = 3811632615m
			},
			new LegacyProjectEstimationHoursRefBE() {
				OccupancyTypRefId = 4,
				ConstrTypTxt = "UPFITRTAP",
				TotalProjectsCnt = 377,
				BuildHoursNbr = 503.3m,
				ElectHoursNbr = 550.8m,
				MechHoursNbr = 352.6m,
				PlumbHoursNbr = 301m,
				TotalSquareFootageCnt =  2390113,
				TotalSheetsCnt = 10569,
				TotalConstrCostAmt = 260648709m
			},
			new LegacyProjectEstimationHoursRefBE() {
				OccupancyTypRefId = 4,
				ConstrTypTxt = "NEWCONSTRUCTION",
				TotalProjectsCnt = 36,
				BuildHoursNbr = 156m,
				ElectHoursNbr = 134.3m,
				MechHoursNbr = 63.5m,
				PlumbHoursNbr = 65m,
				TotalSquareFootageCnt =  1069165,
				TotalSheetsCnt = 3583,
				TotalConstrCostAmt = 285440854m
			},
			new LegacyProjectEstimationHoursRefBE() {
				OccupancyTypRefId = 6,
				ConstrTypTxt = "UPFITRTAP",
				TotalProjectsCnt = 25,
				BuildHoursNbr = 29.5m,
				ElectHoursNbr = 40m,
				MechHoursNbr = 19.5m,
				PlumbHoursNbr =21m,
				TotalSquareFootageCnt =  3900951,
				TotalSheetsCnt = 960,
				TotalConstrCostAmt = 20322600m
			},
			new LegacyProjectEstimationHoursRefBE() {
				OccupancyTypRefId = 6,
				ConstrTypTxt = "NEWCONSTRUCTION",
				TotalProjectsCnt = 40,
				BuildHoursNbr = 491.8m,
				ElectHoursNbr = 450m,
				MechHoursNbr = 344.3m,
				PlumbHoursNbr = 336.8m,
				TotalSquareFootageCnt =  5614728,
				TotalSheetsCnt = 12405,
				TotalConstrCostAmt = 1433205000m
			},
			new LegacyProjectEstimationHoursRefBE() {
				OccupancyTypRefId = 10,
				ConstrTypTxt = "UPFITRTAP",
				TotalProjectsCnt = 29,
				BuildHoursNbr = 39.3m,
				ElectHoursNbr = 61.8m,
				MechHoursNbr = 27m,
				PlumbHoursNbr = 24.8m,
				TotalSquareFootageCnt =  5900692,
				TotalSheetsCnt = 1314,
				TotalConstrCostAmt = 116923938m
			},
			new LegacyProjectEstimationHoursRefBE() {
				OccupancyTypRefId = 10,
				ConstrTypTxt = "NEWCONSTRUCTION",
				TotalProjectsCnt = 3,
				BuildHoursNbr = 7m,
				ElectHoursNbr = 5.3m,
				MechHoursNbr = 4.5m,
				PlumbHoursNbr = 2.3m,
				TotalSquareFootageCnt =  669875,
				TotalSheetsCnt = 200,
				TotalConstrCostAmt = 16875000m
			},
			new LegacyProjectEstimationHoursRefBE() {
				OccupancyTypRefId = 2,
				ConstrTypTxt = "UPFITRTAP",
				TotalProjectsCnt = 392,
				BuildHoursNbr = 545m,
				ElectHoursNbr = 676.7m,
				MechHoursNbr = 363.5m,
				PlumbHoursNbr = 287.8m,
				TotalSquareFootageCnt =  6668962,
				TotalSheetsCnt = 12436,
				TotalConstrCostAmt = 421988173m
			},
			new LegacyProjectEstimationHoursRefBE() {
				OccupancyTypRefId = 2,
				ConstrTypTxt = "NEWCONSTRUCTION",
				TotalProjectsCnt = 28,
				BuildHoursNbr = 289m,
				ElectHoursNbr = 332.5m,
				MechHoursNbr = 153m,
				PlumbHoursNbr = 157.5m,
				TotalSquareFootageCnt =  2398602,
				TotalSheetsCnt = 7253,
				TotalConstrCostAmt = 957745746m
			},
			new LegacyProjectEstimationHoursRefBE() {
				OccupancyTypRefId = 8,
				ConstrTypTxt = "UPFITRTAP",
				TotalProjectsCnt = 76,
				BuildHoursNbr = 88.5m,
				ElectHoursNbr = 119m,
				MechHoursNbr = 59.3m,
				PlumbHoursNbr = 58.5m,
				TotalSquareFootageCnt =  1692493,
				TotalSheetsCnt = 2406,
				TotalConstrCostAmt = 16938534m
			},
			new LegacyProjectEstimationHoursRefBE() {
				OccupancyTypRefId = 8,
				ConstrTypTxt = "NEWCONSTRUCTION",
				TotalProjectsCnt = 6,
				BuildHoursNbr = 75m,
				ElectHoursNbr = 48m,
				MechHoursNbr = 31.8m,
				PlumbHoursNbr = 48.8m,
				TotalSquareFootageCnt =  1849733,
				TotalSheetsCnt = 1470,
				TotalConstrCostAmt = 141310000m
			},
			new LegacyProjectEstimationHoursRefBE() {
				OccupancyTypRefId = 3,
				ConstrTypTxt = "UPFITRTAP",
				TotalProjectsCnt = 401,
				BuildHoursNbr = 741.8m,
				ElectHoursNbr = 731.1m,
				MechHoursNbr = 442.3m,
				PlumbHoursNbr = 420.5m,
				TotalSquareFootageCnt =  64931802,
				TotalSheetsCnt = 19081,
				TotalConstrCostAmt = 826455159m
			},
			new LegacyProjectEstimationHoursRefBE() {
				OccupancyTypRefId = 3,
				ConstrTypTxt = "NEWCONSTRUCTION",
				TotalProjectsCnt = 515,
				BuildHoursNbr = 5400m,
				ElectHoursNbr = 4143.7m,
				MechHoursNbr = 3115.3m,
				PlumbHoursNbr = 3118.5m,
				TotalSquareFootageCnt =  190039133,
				TotalSheetsCnt = 141672,
				TotalConstrCostAmt = 15736470105m
			},
			new LegacyProjectEstimationHoursRefBE() {
				OccupancyTypRefId = 5,
				ConstrTypTxt = "UPFITRTAP",
				TotalProjectsCnt = 94,
				BuildHoursNbr = 117.5m,
				ElectHoursNbr = 112.3m,
				MechHoursNbr = 35.5m,
				PlumbHoursNbr = 43.8m,
				TotalSquareFootageCnt =  27621417,
				TotalSheetsCnt = 2997,
				TotalConstrCostAmt = 172668431m
			},
			new LegacyProjectEstimationHoursRefBE() {
				OccupancyTypRefId = 5,
				ConstrTypTxt = "NEWCONSTRUCTION",
				TotalProjectsCnt = 69,
				BuildHoursNbr = 193.5m,
				ElectHoursNbr = 148.5m,
				MechHoursNbr = 49.5m,
				PlumbHoursNbr = 89m,
				TotalSquareFootageCnt =  24989469,
				TotalSheetsCnt = 8760,
				TotalConstrCostAmt = 1534893366m
			},
			new LegacyProjectEstimationHoursRefBE() {
				OccupancyTypRefId = 7,
				ConstrTypTxt = "UPFITRTAP",
				TotalProjectsCnt = 13,
				BuildHoursNbr = 11.5m,
				ElectHoursNbr = 16.3m,
				MechHoursNbr = 5.8m,
				PlumbHoursNbr = 6.3m,
				TotalSquareFootageCnt =  102876,
				TotalSheetsCnt = 205,
				TotalConstrCostAmt = 2737515m
			},
			new LegacyProjectEstimationHoursRefBE() {
				OccupancyTypRefId = 7,
				ConstrTypTxt = "NEWCONSTRUCTION",
				TotalProjectsCnt = 13,
				BuildHoursNbr = 15.3m,
				ElectHoursNbr = 8.8m,
				MechHoursNbr = 4m,
				PlumbHoursNbr = 3.8m,
				TotalSquareFootageCnt =  857210,
				TotalSheetsCnt = 240,
				TotalConstrCostAmt = 14094826m
			},
		};

		public List<LegacyProjectEstimationHoursRefBE> GetList()
		{
			return LegacyProjectData;
		}
	}
}
