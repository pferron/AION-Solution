using AION.Engine.BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AION.BL.BusinessObjects
{
    public class ProjectScheduleModelBO
    {
        public ProjectSchedule ConvertBEToModel(ProjectScheduleBE be)
        {
            ProjectSchedule ret = new ProjectSchedule();

            ret.ProjectScheduleID = be.ProjectScheduleID.Value;
            ret.AppointmentID = be.AppoinmentID.Value;
            ret.ProjectScheduleTypeRef = be.ProjectScheduleTypeRef;
            ret.RecurringApptDt = be.RecurringApptDt.Value;

            return ret;
        }
    }
}