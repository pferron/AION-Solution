using AION.BL;
using System;
using System.Collections.Generic;

namespace AION.Web.Models
{
    public class NPACreateViewModel
    {
        /// <summary>
        /// The log in email of the user
        /// </summary>
        public string LoggedInUserEmail { get; set; }
        /// <summary>
        /// User ID
        /// </summary>
        public int LoggedInUserID { get; set; }
        public int NPAID { get; set; }
        public string NPAName { get; set; }
        public int NPATypeSelected { get; set; }
        public string RecurrenceSelected { get; set; }
        public string DaySelected { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool AllDay { get; set; }
        public string YNSelected { get; set; }
        public string MeetingRoomSelected { get; set; }
        public List<string> BldgReviewerSelected { get; set; }
        public bool BldgSelectAll { get; set; }
        public bool ElecSelectAll { get; set; }
        public List<string> ElecReviewerSelected { get; set; }
        public bool MechSelectAll { get; set; }
        public List<string> MechReviewerSelected { get; set; }
        public bool PlumSelectAll { get; set; }
        public List<string> PlumReviewerSelected { get; set; }
        public bool ZoniSelectAll { get; set; }
        public List<string> ZoniReviewerSelected { get; set; }
        public bool FireSelectAll { get; set; }
        public List<string> FireReviewerSelected { get; set; }
        public bool BackSelectAll { get; set; }
        public List<string> BackReviewerSelected { get; set; }
        public bool FoodSelectAll { get; set; }
        public List<string> FoodReviewerSelected { get; set; }
        public bool PoolSelectAll { get; set; }
        public List<string> PoolReviewerSelected { get; set; }
        public bool FaciSelectAll { get; set; }
        public List<string> FaciReviewerSelected { get; set; }
        public bool DayCSelectAll { get; set; }
        public List<string> DayCReviewerSelected { get; set; }
        public string SelectedMeetingRoom { get; set; }
        public int MeetingRoomRefIDSelected { get; set; }
        public int MeetingRoomNameSelected { get; set; }
        /// <summary>
        /// this is used to pass the useridentity object to the schedule helper to build the NPA
        /// </summary>
        public UserIdentity UpdatingUser { get; set; }
    }
}