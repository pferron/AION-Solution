using System;

namespace AION.Accela.Engine.Models
{
    public class AccelaWorkFlowTaskUpdate
    {
        public   string recordId { get; set; }
        public  string TaskID { get; set; }
        public  string DepartmentName { get; set; }
        public   string Assignee { get; set; }
        public    DateTime DueDate { get; set; }
        public    string Status { get; set; }
        public   string HoursSpent { get; set; }
        public    string UserId { get; set; }
        public   string Comment { get; set; } 
        
        public AccelaWorkFlowTaskUpdate()
        {

        }
        
    }
}
