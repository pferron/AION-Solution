using AION.BL;
using System;
using System.Collections.Generic;

namespace AION.Manager.Models
{
    /// <summary>
    /// Used in Schedule Capacity Popup
    /// </summary>
    public class ScheduleCapacitySearchResult : ModelBase
    {

        public int UserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserName { get; set; }

        public DateTime ScheduleDate { get; set; }

        public string PlanReviewHours { get; set; }

        public List<Meeting> Meetings { get; set; }

        public List<Meeting> NpaMeetings { get; set; }

        public List<Meeting> ExpressReservations { get; set; }

        public List<Meeting> ExpressMeetings { get; set; }
        public List<Meeting> PlanReviews { get; set; }

        public List<Meeting> FIFOPlanReview { get; set; }

        public bool IsAvailable { get; set; }
        public double TotalHours { get; set; }
        public double MaxHours { get; set; }
    }

    public class ScheduleCapacitySearchResultPlanReview : ScheduleCapacitySearchResult
    {
        public ScheduleCapacitySearchResultPlanReview(ScheduleCapacitySearchResult result)
        {
            this.UserId = result.UserId;
            this.FirstName = result.FirstName;
            this.LastName = result.LastName;
            this.UserName = result.UserName;
            this.ScheduleDate = result.ScheduleDate;
            this.PlanReviewHours = result.PlanReviewHours;
            this.FIFOPlanReview = result.FIFOPlanReview;
            this.Meetings = result.Meetings;
            this.NpaMeetings = result.NpaMeetings;
            this.ExpressReservations = result.ExpressReservations;
            this.ExpressMeetings = result.ExpressMeetings;
            this.PlanReviews = result.PlanReviews;
            this.IsAvailable = result.IsAvailable;
            this.TotalHours = result.TotalHours;
            this.MaxHours = result.MaxHours;
        }
        public double SearchHours { get; set; }
        public DateTime SearchStartDate { get; set; }
        public DateTime SearchEndDate { get; set; }
    }
}