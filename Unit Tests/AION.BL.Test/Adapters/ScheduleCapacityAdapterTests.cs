using AION.Manager.Adapters;
using AION.Manager.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace AION.BL.Test.Adapters
{
    [TestClass]
    public class ScheduleCapacityAdapterTests
    {
        [TestMethod]
        public void SearchReturnsNotNull()
        {
            ScheduleCapacityAdapter scheduleCapacityAdapter = new ScheduleCapacityAdapter();
            List<string> reviewers = new List<string>();
            reviewers.Add("96");
            //reviewers.Add("110");
            ScheduleCapacitySearch search = new ScheduleCapacitySearch
            {
                BeginDateTime = DateTime.Parse("2020-12-02 12:00AM"),
                EndDateTime = DateTime.Parse("2020-12-08 12:00AM"),
                ReviewerSearchList = reviewers
            };
            List<ScheduleCapacitySearchResult> results = scheduleCapacityAdapter.ScheduleCapacitySearch(search);
            Assert.IsNotNull(results);
        }
        [TestMethod]
        public void ReviewerCapacitySearchReturnsNotNull()
        {
            ScheduleCapacityAdapter scheduleCapacityAdapter = new ScheduleCapacityAdapter();
            List<ScheduleCapacitySearch> searchlist = new List<ScheduleCapacitySearch>();
            List<string> reviewers = new List<string>();
            reviewers.Add("39");
            reviewers.Add("110");
            ScheduleCapacitySearch search = new ScheduleCapacitySearch
            {
                BeginDateTime = DateTime.Now,
                EndDateTime = DateTime.Now.AddDays(7),
                ReviewerSearchList = reviewers
            };
            searchlist.Add(search);
            reviewers = new List<string>();
            reviewers.Add("58");
            reviewers.Add("60");
            search = new ScheduleCapacitySearch
            {
                BeginDateTime = DateTime.Now,
                EndDateTime = DateTime.Now.AddDays(3),
                ReviewerSearchList = reviewers
            };
            searchlist.Add(search);
            List<ScheduleCapacitySearchResult> results = scheduleCapacityAdapter.SearchReviewerCapacity(searchlist);
            Assert.IsNotNull(results);
        }
    }
}
