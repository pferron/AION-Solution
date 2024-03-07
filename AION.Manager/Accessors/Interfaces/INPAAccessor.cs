using AION.BL.Models;
using AION.Estimator.Engine.BusinessEntities;
using System;
using System.Collections.Generic;

namespace AION.Manager.Accessors
{
    public interface INPAAccessor
    {
        bool DeleteNPA(List<int> scheduleIdList, bool cancelRecurringflag);
        List<NPASearchResult> GetNPAList();
        List<NPASearchResult> GetEndingSoonList();
        void SendExistingNPACalendarAppts(UserDepartmentXref item);
        List<NPASearchResult> SearchNPAs(int? type, int? reviewerId, string searchtxt, DateTime? startdate, DateTime? enddate);
        List<NPASearchResult> SearchNPAs_v2(int? type, int? reviewerId, string searchtxt, DateTime? startdate, DateTime? enddate);
        /// <summary>
        /// Processes Npas to determine if user needs to be added to all inclusive appts.
        /// </summary>
        /// <param name="items">List of users and business ref ids</param>
        /// <param name="updatedUserId">user id updating</param>
        /// <param name="timeStamp">date time for update</param>
        /// <returns>success</returns>
        bool ProcessNpas(List<UserBusinessRelationshipBE> items, int updatedUserId, DateTime timeStamp);
    }
}