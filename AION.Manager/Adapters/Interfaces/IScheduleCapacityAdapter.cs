using AION.Manager.Models;
using System.Collections.Generic;

namespace AION.Manager.Adapters
{
    public interface IScheduleCapacityAdapter
    {

        /// <summary>
        /// Used in Schedule Capacity Pop up in Scheduling module
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        List<ScheduleCapacitySearchResult> ScheduleCapacitySearch(ScheduleCapacitySearch search);

        /// <summary>
        /// Used in plan review page to find capacity for reviewers while scheduling
        /// Filters for time
        /// Begin and End datetimes should have inclusive values
        /// ex. 01/01/2099 8am to 01/01/2099 5pm
        /// ex. for between two dates irrespective of time, 01/01/2099 00:00 to 01/01/2099 23:59
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        List<ScheduleCapacitySearchResult> SearchReviewerCapacity(List<ScheduleCapacitySearch> search);
    }
}
