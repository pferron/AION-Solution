using Newtonsoft.Json;
using System;

namespace AION.Manager.Models
{
    public class ManualReservation
    {
        public int Id { get; set; }
        public DateTime ManualExpressDate { get; set; }
        public DateTime ManualExpressStartTime { get; set; }
        public DateTime ManualExpressEndTime { get; set; }
        public int MeetingRoomRefId { get; set; }
        public string BuildingReviewerSelected { get; set; }
        public string ElectricalReviewerSelected { get; set; }
        public string MechanicalReviewerSelected { get; set; }
        public string PlumbingReviewerSelected { get; set; }
        public string ZoniReviewerSelected { get; set; }
        public string FireReviewerSelected { get; set; }
        public string FireCityReviewerSelected { get; set; }
        public string BackflowReviewerSelected { get; set; }
        public string FoodServiceReviewerSelected { get; set; }
        public string PublicPoolReviewerSelected { get; set; }
        public string FaciLodReviewerSelected { get; set; }
        public string DayCareReviewerSelected { get; set; }
        public string ZoniCityReviewerSelected { get; set; }
        public string ZoniHuntersvilleReviewerSelected { get; set; }
        public string ZoniMintHillReviewerSelected { get; set; }

    }
}