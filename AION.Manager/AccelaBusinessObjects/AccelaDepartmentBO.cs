using AION.BL;
using AION.BL.BusinessObjects;
using Meck.Shared.MeckDataMapping;
using System.Collections.Generic;
using System.Linq;

namespace AION.Manager.AccelaBusinessObjects
{
    public class AccelaDepartmentBO : AccelaBusinessObjectBase
    {
        public int MapAccelaDepartment(string department)
        {
            int BusinessRefId = -1;
            DepartmentModelBO dept_bo = new DepartmentModelBO();

            switch (department)
            {
                case "Commercial Building":
                case "Residential Building":
                    BusinessRefId = dept_bo.GetInstance(DepartmentNameEnums.Building).ID;
                    break;
                case "Commercial Electrical":
                    BusinessRefId = dept_bo.GetInstance(DepartmentNameEnums.Electrical).ID;
                    break;
                case "Commercial Mechanical":
                    BusinessRefId = dept_bo.GetInstance(DepartmentNameEnums.Mechanical).ID;
                    break;
                case "Commercial Plumbing":
                    BusinessRefId = dept_bo.GetInstance(DepartmentNameEnums.Plumbing).ID;
                    break;
                case "Commercial City Fire":
                case "Commercial County Fire":
                case "Residential City Fire":
                case "Residential County Fire":
                    BusinessRefId = dept_bo.GetInstance(DepartmentNameEnums.Fire_Cornelius).ID;
                    break;
                case "Commercial City Zoning":
                case "Commercial County Zoning":
                case "Residential Charlotte Zoning":
                case "Residential County Zoning":
                case "Residential TH Zoning":
                    BusinessRefId = dept_bo.GetInstance(DepartmentNameEnums.Zone_Cornelius).ID;
                    break;
                case "CLTWTR Backflow Prevention":
                    BusinessRefId = dept_bo.GetInstance(DepartmentNameEnums.Backflow).ID;
                    break;
                case "Commercial EHS Public Pool":
                case "EHS Residential Pools":
                    BusinessRefId = dept_bo.GetInstance(DepartmentNameEnums.EH_Pool).ID;
                    break;
                case "Commercial EHS Food Service":
                    BusinessRefId = dept_bo.GetInstance(DepartmentNameEnums.EH_Food).ID;
                    break;
                case "Commercial EHS Facility Lodging":
                    BusinessRefId = dept_bo.GetInstance(DepartmentNameEnums.EH_Facilities).ID;
                    break;
                case "Commercial EHS Day Care":
                    BusinessRefId = dept_bo.GetInstance(DepartmentNameEnums.EH_Day_Care).ID;
                    break;
                default:
                    break;
            }

            return BusinessRefId;
        }

        public string MapAionDepartment(int businessRefId)
        {
            DepartmentModelBO dept_bo = new DepartmentModelBO();
            Department department = dept_bo.GetInstance(businessRefId);
            switch (department.DepartmentEnum)
            {
                case DepartmentNameEnums.Building:
                    return "Commercial Building";
                case DepartmentNameEnums.Electrical:
                    return "Commercial Electrical";
                case DepartmentNameEnums.Mechanical:
                    return "Commercial Mechanical";
                case DepartmentNameEnums.Plumbing:
                    return "Commercial Plumbing";
                case DepartmentNameEnums.Backflow:
                    return "CLTWTR Backflow Prevention";
                case DepartmentNameEnums.EH_Food:
                    return "Commercial EHS Food Service";
                case DepartmentNameEnums.EH_Facilities:
                    return "Commercial EHS Facility Lodging";
                case DepartmentNameEnums.EH_Day_Care:
                    return "Commercial EHS Day Care";
                case DepartmentNameEnums.Fire_Cornelius:
                case DepartmentNameEnums.Fire_County:
                case DepartmentNameEnums.Fire_Cty_Chrlt:
                case DepartmentNameEnums.Fire_Davidson:
                case DepartmentNameEnums.Fire_Huntersville:
                case DepartmentNameEnums.Fire_Matthews:
                case DepartmentNameEnums.Fire_Mint_Hill:
                case DepartmentNameEnums.Fire_Pineville:
                case DepartmentNameEnums.Fire_UMC:
                    return "Fire";
                case DepartmentNameEnums.Zone_Cornelius:
                case DepartmentNameEnums.Zone_County:
                case DepartmentNameEnums.Zone_Cty_Chrlt:
                case DepartmentNameEnums.Zone_Davidson:
                case DepartmentNameEnums.Zone_Huntersville:
                case DepartmentNameEnums.Zone_Matthews:
                case DepartmentNameEnums.Zone_Mint_Hill:
                case DepartmentNameEnums.Zone_Pineville:
                case DepartmentNameEnums.Zone_UMC: return "Zoning";
                default: return string.Empty;
            }
        }

        public void GetPrelimRequestedReviewers(List<ProjectTrade> trades, List<ProjectAgency> agencies, PrelimMeetingTradesAndReviewerObj tradesAndReviewers)
        {
            foreach (ProjectTrade trade in trades)
            {
                switch (trade.DepartmentInfo)
                {
                    case DepartmentNameEnums.Building:
                        if (IsChecked(tradesAndReviewers.PrelimTradeBuilding))
                        {
                            trade.IsDeptRequested = true;
                            trade.ProposedPlanReviewer = GetProposedReviewer(tradesAndReviewers.PrelimTradeBuildingReviewers);
                        }
                        break;
                    case DepartmentNameEnums.Electrical:
                        if (IsChecked(tradesAndReviewers.PrelimTradeElectrical))
                        {
                            trade.IsDeptRequested = true;
                            trade.ProposedPlanReviewer = GetProposedReviewer(tradesAndReviewers.PrelimTradeElectricalReviewers);
                        }

                        break;
                    case DepartmentNameEnums.Mechanical:
                        if (IsChecked(tradesAndReviewers.PrelimTradeMechanical))
                        {
                            trade.IsDeptRequested = true;
                            trade.ProposedPlanReviewer = GetProposedReviewer(tradesAndReviewers.PrelimTradeMechanicalReviewers);
                        }

                        break;
                    case DepartmentNameEnums.Plumbing:
                        if (IsChecked(tradesAndReviewers.PrelimTradePlumbing))
                        {
                            trade.IsDeptRequested = true;
                            trade.ProposedPlanReviewer = GetProposedReviewer(tradesAndReviewers.PrelimTradePlumbingReviewers);
                        }

                        break;
                }

            }

            foreach (ProjectAgency agency in agencies)
            {
                switch (agency.DepartmentInfo)
                {
                    case DepartmentNameEnums.Zone_Davidson:
                    case DepartmentNameEnums.Zone_Cornelius:
                    case DepartmentNameEnums.Zone_Pineville:
                    case DepartmentNameEnums.Zone_Matthews:
                    case DepartmentNameEnums.Zone_Mint_Hill:
                    case DepartmentNameEnums.Zone_Huntersville:
                    case DepartmentNameEnums.Zone_UMC:
                    case DepartmentNameEnums.Zone_Cty_Chrlt:
                    case DepartmentNameEnums.Zone_County:
                        if (IsChecked(tradesAndReviewers.PrelimTradeZoning))
                        {
                            agency.IsDeptRequested = true;
                            agency.ProposedPlanReviewer = GetProposedReviewer(tradesAndReviewers.PrelimTradeZoningReviewers);
                        }

                        break;
                    case DepartmentNameEnums.Fire_Davidson:
                    case DepartmentNameEnums.Fire_Cornelius:
                    case DepartmentNameEnums.Fire_Pineville:
                    case DepartmentNameEnums.Fire_Matthews:
                    case DepartmentNameEnums.Fire_Mint_Hill:
                    case DepartmentNameEnums.Fire_Huntersville:
                    case DepartmentNameEnums.Fire_UMC:
                    case DepartmentNameEnums.Fire_Cty_Chrlt:
                    case DepartmentNameEnums.Fire_County:
                        if (IsChecked(tradesAndReviewers.PrelimTradeFire))
                        {
                            agency.IsDeptRequested = true;
                            agency.ProposedPlanReviewer = GetProposedReviewer(tradesAndReviewers.PrelimTradeFireReviewers);
                        }

                        break;
                    case DepartmentNameEnums.EH_Day_Care:
                    case DepartmentNameEnums.EH_Food:
                    case DepartmentNameEnums.EH_Pool:
                    case DepartmentNameEnums.EH_Facilities:
                        if (IsChecked(tradesAndReviewers.PrelimTradeHealth))
                        {
                            agency.IsDeptRequested = true;
                            agency.ProposedPlanReviewer = GetProposedReviewer(tradesAndReviewers.PrelimTradeHealthReviewers);
                        }

                        break;
                    case DepartmentNameEnums.Backflow:
                        if (IsChecked(tradesAndReviewers.PrelimTradeBackflow))
                        {
                            agency.IsDeptRequested = true;
                            agency.ProposedPlanReviewer = GetProposedReviewer(tradesAndReviewers.PrelimTradeBackflowReviewers);
                        }
                        break;
                }
            }
        }

        public List<TradeInfo> GetAccelaTradeInfoList(PrelimMeetingTradesAndReviewerObj tradesAndReviewers)
        {
            //     public string PrelimTradeBackflow { get; set; }
            //public string PrelimTradeMechanicalReviewers { get; set; }
            //public string PrelimTradeZoning { get; set; }
            //public string PrelimTradeHealthReviewers { get; set; }
            //public string PrelimTradeElectricalReviewers { get; set; }
            //public string PrelimTradeHealth { get; set; }
            //public string PrelimTradeMechanical { get; set; }
            //public string PrelimTradeBuildingReviewers { get; set; }
            //public string PrelimTradeZoningReviewers { get; set; }
            //public string PreviousPrelimProjectNumber { get; set; }
            //public string PrelimTradePlumbingReviewers { get; set; }
            //public string PreviousPrelimReview { get; set; }
            //public string PrelimTradeElectrical { get; set; }
            //public string PrelimTradeFire { get; set; }
            //public string id { get; set; }
            //public string PrelimTradeBuilding { get; set; }
            //public string PrelimTradePlumbing { get; set; }
            //public string PrelimTradeBackflowReviewers { get; set; }
            //public string PrelimTradeFireReviewers { get; set; }

            List<TradeInfo> trades = new List<TradeInfo>();
            if (tradesAndReviewers == null)
            {
                return trades;
            }

            if (IsChecked(tradesAndReviewers.PrelimTradeBuilding))
            {
                trades.Add(GetTradeInfo(
                    Meck.Shared.DepartmentDivisionExternalRef.Building,
                    Meck.Shared.DepartmentRegionExternalRef.NA,
                    tradesAndReviewers.PrelimTradeBuildingReviewers));
            }
            if (IsChecked(tradesAndReviewers.PrelimTradeElectrical))
            {
                trades.Add(GetTradeInfo(
                   Meck.Shared.DepartmentDivisionExternalRef.Electrical,
                   Meck.Shared.DepartmentRegionExternalRef.NA,
                   tradesAndReviewers.PrelimTradeElectricalReviewers));

            }

            if (IsChecked(tradesAndReviewers.PrelimTradeMechanical))
            {
                trades.Add(GetTradeInfo(
                   Meck.Shared.DepartmentDivisionExternalRef.Mechanical,
                   Meck.Shared.DepartmentRegionExternalRef.NA,
                   tradesAndReviewers.PrelimTradeMechanicalReviewers));

            }

            if (IsChecked(tradesAndReviewers.PrelimTradePlumbing))
            {
                trades.Add(GetTradeInfo(
                   Meck.Shared.DepartmentDivisionExternalRef.Plumbing,
                   Meck.Shared.DepartmentRegionExternalRef.NA,
                   tradesAndReviewers.PrelimTradePlumbingReviewers));

            }

            return trades;
        }

        public List<AgencyInfo> GetAccelaAgencyInfoList(PrelimMeetingTradesAndReviewerObj tradesAndReviewers)
        {
            //     public string PrelimTradeBackflow { get; set; }
            //public string PrelimTradeMechanicalReviewers { get; set; }
            //public string PrelimTradeZoning { get; set; }
            //public string PrelimTradeHealthReviewers { get; set; }
            //public string PrelimTradeElectricalReviewers { get; set; }
            //public string PrelimTradeHealth { get; set; }
            //public string PrelimTradeMechanical { get; set; }
            //public string PrelimTradeBuildingReviewers { get; set; }
            //public string PrelimTradeZoningReviewers { get; set; }
            //public string PreviousPrelimProjectNumber { get; set; }
            //public string PrelimTradePlumbingReviewers { get; set; }
            //public string PreviousPrelimReview { get; set; }
            //public string PrelimTradeElectrical { get; set; }
            //public string PrelimTradeFire { get; set; }
            //public string id { get; set; }
            //public string PrelimTradeBuilding { get; set; }
            //public string PrelimTradePlumbing { get; set; }
            //public string PrelimTradeBackflowReviewers { get; set; }
            //public string PrelimTradeFireReviewers { get; set; }
            List<AgencyInfo> agencies = new List<AgencyInfo>();
            if (tradesAndReviewers == null)
            {
                return agencies;
            }
            if (IsChecked(tradesAndReviewers.PrelimTradeZoning))
            {
                agencies.Add(GetAgencyInfo(
                   Meck.Shared.DepartmentDivisionExternalRef.Zoning,
                   Meck.Shared.DepartmentRegionExternalRef.NA,
                   tradesAndReviewers.PrelimTradeZoningReviewers));

            }
            if (IsChecked(tradesAndReviewers.PrelimTradeFire))
            {
                agencies.Add(GetAgencyInfo(
                   Meck.Shared.DepartmentDivisionExternalRef.Fire,
                   Meck.Shared.DepartmentRegionExternalRef.NA,
                   tradesAndReviewers.PrelimTradeFireReviewers));

            }
            if (IsChecked(tradesAndReviewers.PrelimTradeBackflow))
            {
                agencies.Add(GetAgencyInfo(
                   Meck.Shared.DepartmentDivisionExternalRef.Backflow,
                   Meck.Shared.DepartmentRegionExternalRef.NA,
                   tradesAndReviewers.PrelimTradeBackflowReviewers));

            }
            if (IsChecked(tradesAndReviewers.PrelimTradeHealth))
            {
                agencies.Add(GetAgencyInfo(
                   Meck.Shared.DepartmentDivisionExternalRef.Environmental,
                   Meck.Shared.DepartmentRegionExternalRef.Food_Service,
                   tradesAndReviewers.PrelimTradeHealthReviewers));

                agencies.Add(GetAgencyInfo(
                  Meck.Shared.DepartmentDivisionExternalRef.Environmental,
                  Meck.Shared.DepartmentRegionExternalRef.Public_Pool,
                  tradesAndReviewers.PrelimTradeHealthReviewers));

                agencies.Add(GetAgencyInfo(
                  Meck.Shared.DepartmentDivisionExternalRef.Environmental,
                  Meck.Shared.DepartmentRegionExternalRef.Facilities_Lodging,
                  tradesAndReviewers.PrelimTradeHealthReviewers));

                agencies.Add(GetAgencyInfo(
                  Meck.Shared.DepartmentDivisionExternalRef.Environmental,
                  Meck.Shared.DepartmentRegionExternalRef.Day_Care,
                  tradesAndReviewers.PrelimTradeHealthReviewers));

            }

            return agencies;

        }

        private UserIdentity GetProposedReviewer(string reviewers)
        {
            string requestedReviewerName = reviewers.Substring(reviewers.IndexOf(")") + 1).Trim();
            if (requestedReviewerName.IndexOf(",") > 0)
            {
                return new UserIdentityModelBO().GetInstance(requestedReviewerName, "lastname,firstname").FirstOrDefault();
            }
            else
            {
                return new UserIdentity();
            }
        }

        private string GetRequestedReviewerName(string reviewername)
        {
            if (!string.IsNullOrWhiteSpace(reviewername) && reviewername.IndexOf(")") > 0 && reviewername.IndexOf(",") > 0)
            {
                return reviewername.Substring(reviewername.IndexOf(")") + 1).Trim();

            }
            else
            {
                return "";
            }

        }

        private TradeInfo GetTradeInfo(string division, string region, string reviewers)
        {
            TradeInfo trade = new TradeInfo
            {
                AccelaDepartmentDivisionRef = division,
                AccelaDepartmentRegionRef = region,
                RequestedReviewerName = GetRequestedReviewerName(reviewers),
                IsDeptRequested = true
            };

            return trade;
        }

        private AgencyInfo GetAgencyInfo(string division, string region, string reviewers)
        {
            AgencyInfo agency = new AgencyInfo
            {
                AccelaDepartmentDivisionRef = division,
                AccelaDepartmentRegionRef = region,
                RequestedReviewerName = GetRequestedReviewerName(reviewers),
                IsDeptRequested = true
            };

            return agency;
        }

    }
}