namespace AION.BL.Models
{
    public class ProjectDeptEstimationReq
    {
        public int ProjectTypRefId { get; set; }
        public PropertyTypeEnums PropertyType { get; set; }
        public int DepartmentId { get; set; }
        public DepartmentNameEnums Department { get; set; }
    }
}
