namespace AION.BL
{
    public class ProjectStatus : ModelBase
    {
        public string ProjectStatusCode { get; set; } //[PROJECT_STATUS_REF_NM]

        public ProjectStatusEnum ProjectStatusEnum { get; set; } // ENUM_MAPPING_VAL

        public string ProjectStatusDetails { get; set; } // [PROJECT_STATUS_REF_DESC]

        public string ProjectStatusExternalRef { get; set; } //[SRC_SYSTEM_VALUE_TXT]

        public ExternalSystem ExternalSystem { get; set; } // [EXTERNAL_SYSTEM_REF_ID]

        /// <summary>
        /// jcl added to hold the ProjectStatusRef string for processing
        /// </summary>
        public string AccelaProjectStatus { get; set; }

    }
}
