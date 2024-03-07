using System;
namespace ReadAIONDb.Dtos
{
    internal class PROJECT_AUDITtESTDto
    {
        public int? PROJECT_ID { get; set; }
        public int? AUDIT_USER_NM { get; set; }
        public DateTime? AUDIT_DT { get; set; } 
        public int? AUDIT_ACTION_REF_ID { get; set; }
    }
}
