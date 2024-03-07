using AION.BL;

namespace AION.Manager.BusinessObjects
{
    public class ProjectInfoModelBO
    {
        public ProjectInfo ConvertProjectToProjectInfo(Project project)
        {
            ProjectInfo projectinfo = new ProjectInfo();
            projectinfo.IsPreliminaryMeetingRequested = project.IsPreliminaryMeetingRequested;
            projectinfo.IsPreliminaryMeetingCompleted = project.IsPreliminaryMeetingCompleted;
            projectinfo.IsProjectRTAP = project.IsProjectRTAP;
            projectinfo.AssignedFacilitator = project.AssignedFacilitator;
            projectinfo.AccelaRTAPProjectRefId = project.AccelaRTAPProjectRefId;
            projectinfo.AccelaPreliminaryProjectRefId = project.AccelaPreliminaryProjectRefId;
            projectinfo.AccelaOccupancyType = project.AccelaOccupancyType;
            projectinfo.AccelaConstructionType = project.AccelaConstructionType;
            projectinfo.AccelaCostOfConstruction = project.AccelaCostOfConstruction;
            projectinfo.AccelaNumberofSheets = project.AccelaNumberofSheets;
            projectinfo.AccelaSqrFtToBeReviewed = project.AccelaSqrFtToBeReviewed;
            projectinfo.AccelaSqrFtOfOverallBuilding = project.AccelaSqrFtOfOverallBuilding;
            projectinfo.BuildingCodeVersion = project.BuildingCodeVersion;
            projectinfo.AccelaProjectRefId = project.AccelaProjectRefId;
            projectinfo.PropertyType = project.AccelaPropertyType;
            projectinfo.AccelaProjectCreatedDate = project.AccelaProjectCreatedDate;
            projectinfo.AccelaProjectLastUpdatedDate = project.AccelaProjectLastUpdatedDate;
            projectinfo.ProjectName = project.ProjectName;
            projectinfo.IsProjectPreliminary = project.IsProjectPreliminary;
            projectinfo.ID = project.ID;
            projectinfo.ReviewType = project.ReviewType;
            projectinfo.CreatedDate = project.CreatedDate;
            projectinfo.UpdatedDate = project.UpdatedDate;
            projectinfo.AIONProjectStatus = project.AIONProjectStatus;
            projectinfo.ReviewType = project.ReviewType;
            projectinfo.AIONProjectStatus = project.AIONProjectStatus;
            projectinfo.CreatedDate = project.CreatedDate;
            projectinfo.CreatedUser = project.CreatedUser;
            return projectinfo;
        }


    }
}