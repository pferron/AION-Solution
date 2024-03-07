using System;
using System.Collections.Generic;

namespace AION.BL
{
    public class ProjectInfo : ModelBase
    {

        public ProjectInfo()
        {
            Notes = new List<Note>();
            AIONProjectStatus = new ProjectStatus();
            AccelaProjectCreatedBy = new UserIdentity();
            AccelaProjectLastUpdatedBy = new UserIdentity();
            AccelaApplicationNotes = new List<Note>();
        }


        /// <summary>
        /// Notes from UI AION
        /// </summary>
        public List<Note> Notes { get; set; }

        /// <summary>
        /// Project Name from AION
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// Project Reference ID from Accela
        /// </summary>
        public string AccelaProjectRefId { get; set; }

        public string AccelaRTAPProjectRefId { get; set; }

        public string AccelaPreliminaryProjectRefId { get; set; }

        /// <summary>
        /// Project Address from Accela Display Info UI
        /// </summary>
        public string ProjectAddress { get; set; }

        /// <summary>
        /// Bool indicating if meeting is Requested by Customer
        /// </summary>
        public bool IsPreliminaryMeetingRequested { get; set; }
        /// <summary>
        /// Bool indicating if meeting is completed from Accela
        /// </summary>
        public bool IsPreliminaryMeetingCompleted { get; set; }

        public bool IsPreliminaryMeetingCancelled { get; set; }

        /// <summary>
        /// bool indicating if the project is RTAP from Accela
        /// </summary>
        public bool IsProjectRTAP { get; set; }

        public string PMName { get; set; }
        public string PMPhone { get; set; }
        public string PMEmail { get; set; }

        public bool IsProjectPreliminary { get; set; }

        /// <summary>
        /// Building Code Version from UI Display info only
        /// </summary>
        public string BuildingCodeVersion { get; set; }

        /// <summary>
        /// Indicates Express, On Schedule from AION
        /// </summary>
        public ReviewTypeEnum ReviewType { get; set; }

        /// <summary>
        /// indicates MMF, Townhomes, FIFO, etc from AION
        /// </summary>
        public PropertyTypeEnums PropertyType { get; set; }

        /// <summary>
        /// Assigned Estimator Reference id/info/name from Accela
        /// </summary>
        public string AssignedEstimatorRefInfo;

        /// <summary>
        /// Assigned Estimator from AION
        /// </summary>
        public int AssignedEstimator { get; set; }

        /// <summary>
        /// Assigned Facilitator id/info/name from Accela
        /// </summary>
        public string AssignedFacilitatorRefInfo { get; set; }


        //public UserIdentity AssignedFacilitator { get; set; }
        /// <summary>
        /// Assigned Facilitator from AION
        //
        /// </summary>
        public int? AssignedFacilitator { get; set; }

        /// <summary>
        /// Project Status from AION 
        /// </summary>
        public ProjectStatus AIONProjectStatus { get; set; }

        /// <summary>
        /// Workflow Status from AION
        /// </summary>
        public ProjectStatus AIONProjectWorkFlowStatus { get; set; }

        /// <summary>
        /// Project Status from Accela
        /// </summary>
        public string AccelaProjectStatus { get; set; }

        /// <summary>
        /// Project Create DAte from Accela
        /// </summary>
        public DateTime? AccelaProjectCreatedDate { get; set; }

        /// <summary>
        /// Project create by user from Accela
        /// </summary>
        public UserIdentity AccelaProjectCreatedBy { get; set; }
        /// <summary>
        /// Project create by user from Accela
        /// </summary>
        public string AccelaProjectCreatedByRefId { get; set; }
        /// <summary>
        /// Project last update date from Accela
        /// </summary>
        public DateTime? AccelaProjectLastUpdatedDate { get; set; }

        /// <summary>
        /// Project last update by user from Accela
        /// </summary>
        public UserIdentity AccelaProjectLastUpdatedBy { get; set; }
        /// <summary>
        /// Project last update by user from Accela
        /// </summary>
        public string AccelaProjectLastUpdatedByRefId { get; set; }
        /// <summary>
        /// Application Notes from input application in Accela
        /// </summary>
        public List<Note> AccelaApplicationNotes { get; set; }

        public string AccelaConstructionType { get; set; }
        public string AccelaOccupancyType { get; set; }
        public int AccelaSqrFtToBeReviewed { get; set; }
        public int? AccelaSqrFtOfOverallBuilding { get; set; }
        public double AccelaCostOfConstruction { get; set; }
        public int AccelaNumberofSheets { get; set; }
    }

}
