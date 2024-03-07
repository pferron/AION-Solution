namespace AION.BL.Models
{
    public class ProjectType : ModelBase
    {
        public int? ProjectTypRefId { get; set; }

        public string ProjectTypRefNm { get; set; }

        public string ProjectTypRefDisplayNm { get; set; }

        public int? ExternalSystemRefId { get; set; }

        public string SrcSystemValueTxt { get; set; }

        public bool? AutoAssignFacilitator { get; set; }

        public PropertyTypeEnums ProjectTypeEnum { get; set; }

    }
}
