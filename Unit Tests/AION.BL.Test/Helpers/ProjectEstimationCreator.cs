using AION.BL.Models;
using System.Collections.Generic;

namespace AION.BL.Test.Helpers
{
    public static class ProjectEstimationCreator
    {
        public static ProjectEstimation CreatePE(PropertyTypeEnums propertyType, int projectHours, string projectLvlTxt)
        {

            ProjectEstimation project = new ProjectEstimation();

            //    Building
            ProjectTrade b = new ProjectTrade
            {
                DepartmentInfo = DepartmentNameEnums.Building,
                DepartmentDivision = DepartmentDivisionEnum.Building,
                DepartmentTypeEnum = DepartmentTypeEnum.Trade,
                EstimationHours = projectHours,
                ExcludedPlanReviewers = new List<int>()

            };

            //    Electrical
            ProjectTrade e = new ProjectTrade
            {
                DepartmentInfo = DepartmentNameEnums.Electrical,
                DepartmentDivision = DepartmentDivisionEnum.Electrical,
                DepartmentTypeEnum = DepartmentTypeEnum.Trade,
                EstimationHours = projectHours,
                ExcludedPlanReviewers = new List<int>()

            };

            //    Mechanical
            ProjectTrade m = new ProjectTrade
            {
                DepartmentInfo = DepartmentNameEnums.Mechanical,
                DepartmentDivision = DepartmentDivisionEnum.Mechanical,
                DepartmentTypeEnum = DepartmentTypeEnum.Trade,
                EstimationHours = projectHours,
                ExcludedPlanReviewers = new List<int>()

            };

            //    Plumbing
            ProjectTrade p = new ProjectTrade
            {
                DepartmentInfo = DepartmentNameEnums.Plumbing,
                DepartmentDivision = DepartmentDivisionEnum.Plumbing,
                DepartmentTypeEnum = DepartmentTypeEnum.Trade,
                EstimationHours = projectHours,
                ExcludedPlanReviewers = new List<int>()

            };

            //    Zoning County
            ProjectAgency z_county = new ProjectAgency
            {
                DepartmentInfo = DepartmentNameEnums.Zone_County,
                DepartmentDivision = DepartmentDivisionEnum.Zoning,
                DepartmentTypeEnum = DepartmentTypeEnum.Agency,
                EstimationHours = projectHours,
                ExcludedPlanReviewers = new List<int>()

            };
            //    Zoning City Of Charlotte
            ProjectAgency z_charlotte = new ProjectAgency
            {
                DepartmentInfo = DepartmentNameEnums.Zone_Cty_Chrlt,
                DepartmentDivision = DepartmentDivisionEnum.Zoning,
                DepartmentTypeEnum = DepartmentTypeEnum.Agency,
                EstimationHours = projectHours,
                ExcludedPlanReviewers = new List<int>()

            };
            //    Zoning Mint Hill
            ProjectAgency z_minthill = new ProjectAgency
            {
                DepartmentInfo = DepartmentNameEnums.Zone_Mint_Hill,
                DepartmentDivision = DepartmentDivisionEnum.Zoning,
                DepartmentTypeEnum = DepartmentTypeEnum.Agency,
                EstimationHours = projectHours,
                ExcludedPlanReviewers = new List<int>()

            };

            //    Zoning Huntersville
            ProjectAgency z_hville = new ProjectAgency
            {
                DepartmentInfo = DepartmentNameEnums.Zone_Huntersville,
                DepartmentDivision = DepartmentDivisionEnum.Zoning,
                DepartmentTypeEnum = DepartmentTypeEnum.Agency,
                EstimationHours = projectHours,
                ExcludedPlanReviewers = new List<int>()

            };

            //    Fire City Of Charlotte
            ProjectAgency f_charlotte = new ProjectAgency
            {
                DepartmentInfo = DepartmentNameEnums.Fire_Cty_Chrlt,
                DepartmentDivision = DepartmentDivisionEnum.Fire,
                DepartmentTypeEnum = DepartmentTypeEnum.Agency,
                EstimationHours = projectHours,
                ExcludedPlanReviewers = new List<int>()

            };

            //    Fire County
            ProjectAgency f_county = new ProjectAgency
            {
                DepartmentInfo = DepartmentNameEnums.Fire_County,
                DepartmentDivision = DepartmentDivisionEnum.Fire,
                DepartmentTypeEnum = DepartmentTypeEnum.Agency,
                EstimationHours = projectHours,
                ExcludedPlanReviewers = new List<int>()

            };
            //    Backflow
            ProjectAgency backflow = new ProjectAgency
            {
                DepartmentInfo = DepartmentNameEnums.Backflow,
                DepartmentDivision = DepartmentDivisionEnum.Backflow,
                DepartmentTypeEnum = DepartmentTypeEnum.Agency,
                EstimationHours = projectHours,
                ExcludedPlanReviewers = new List<int>()

            };
            //    Food Service
            ProjectAgency ehsfood = new ProjectAgency
            {
                DepartmentInfo = DepartmentNameEnums.EH_Food,
                DepartmentDivision = DepartmentDivisionEnum.Environmental,
                DepartmentTypeEnum = DepartmentTypeEnum.Agency,
                EstimationHours = projectHours,
                ExcludedPlanReviewers = new List<int>()

            };

            //    Public Pool
            ProjectAgency ehspool = new ProjectAgency
            {
                DepartmentInfo = DepartmentNameEnums.EH_Pool,
                DepartmentDivision = DepartmentDivisionEnum.Environmental,
                DepartmentTypeEnum = DepartmentTypeEnum.Agency,
                EstimationHours = projectHours,
                ExcludedPlanReviewers = new List<int>()

            };

            //    Facility Lodging
            ProjectAgency ehsfacility = new ProjectAgency
            {
                DepartmentInfo = DepartmentNameEnums.EH_Facilities,
                DepartmentDivision = DepartmentDivisionEnum.Environmental,
                DepartmentTypeEnum = DepartmentTypeEnum.Agency,
                EstimationHours = projectHours,
                ExcludedPlanReviewers = new List<int>()

            };

            //    Day Care
            ProjectAgency ehsdaycare = new ProjectAgency
            {
                DepartmentInfo = DepartmentNameEnums.EH_Day_Care,
                DepartmentDivision = DepartmentDivisionEnum.Environmental,
                DepartmentTypeEnum = DepartmentTypeEnum.Trade,
                EstimationHours = projectHours,
                ExcludedPlanReviewers = new List<int>()

            };
            List<ProjectTrade> trades = new List<ProjectTrade>();
            trades.Add(b);
            trades.Add(e);
            trades.Add(m);
            trades.Add(p);

            List<ProjectAgency> agencies = new List<ProjectAgency>();
            agencies.Add(backflow);
            agencies.Add(ehsdaycare);
            agencies.Add(ehspool);
            agencies.Add(ehsfacility);
            agencies.Add(ehsfood);

            agencies.Add(z_charlotte);

            agencies.Add(z_county);

            agencies.Add(z_hville);

            agencies.Add(z_minthill);

            agencies.Add(f_charlotte);

            agencies.Add(f_county);

            project.Trades = trades;
            project.Agencies = agencies;
            project.AccelaPropertyType = propertyType;
            project.AionPropertyType = propertyType;
            project.ProjectLvlTxt = projectLvlTxt;

            return project;
        }
    }
}
