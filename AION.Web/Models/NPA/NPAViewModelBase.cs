using AION.BL;
using AION.BL.Models;
using AION.Manager.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace AION.Web.Models
{
    public class NPAViewModelBase : ViewModelBase
    {
        private List<SelectListItem> _allReviewersSelectList;
        private List<ApptAttendeeManagerModel> _allReviewersVm;

        public List<SelectListItem> AllReviewersSelectList
        {
            get
            {
                return PlanReviewersListViewModel;
            }
        }
        public List<ApptAttendeeManagerModel> AllReviewersVm
        {
            get
            {
                _allReviewersVm = BuildAllReviewersVm();
                return _allReviewersVm;
            }
        }

        public List<ApptAttendeeManagerModel> AllReviewersVmWSOI
        {
            get
            {
                _allReviewersVm = BuildAllReviewersVmWSOI();
                return _allReviewersVm;
            }
        }
        #region Private Methods

        private List<ApptAttendeeManagerModel> BuildAllReviewersVm()
        {
            //Building = 7,
            //Electrical = 8,
            //Mechanical = 9,
            //Plumbing = 10,
            //Zone_County = 11,
            //Zone_Cty_Chrlt = 18,
            //Fire_County = 19,
            //Fire_Cty_Chrlt = 26,
            //EH_Day_Care = 27,
            //EH_Food = 28,
            //EH_Pool = 29,
            //EH_Facilities = 30,
            //Backflow = 31,
            Reviewer reviewer;
            List<ApptAttendeeManagerModel> items = new List<ApptAttendeeManagerModel>();
            int deptnameenumid = 0;
            int businessrefid = 0;
            string deptnamelistid = string.Empty;
            foreach (SelectListItem item in BldgPersonSelectList)
            {
                deptnameenumid = (int)DepartmentNameEnums.Building;
                businessrefid = (int)DepartmentNameEnums.Building;
                deptnamelistid = DepartmentNameList.Building;
                int id = int.Parse(item.Value);
                //get the reviewer
                reviewer = AllReviewers.Where(x => x.ID == id).FirstOrDefault();
                items.Add(new ApptAttendeeManagerModel
                {
                    AttendeeId = id,
                    BusinessRefId = businessrefid,
                    DeptNameEnumId = deptnameenumid,
                    DeptNameListId = deptnamelistid,
                    FirstName = reviewer.FirstName,
                    LastName = reviewer.LastName
                });
            }
            foreach (SelectListItem item in ElecPersonSelectList)
            {
                deptnameenumid = (int)DepartmentNameEnums.Electrical;
                businessrefid = (int)DepartmentNameEnums.Electrical;
                deptnamelistid = DepartmentNameList.Electrical;
                int id = int.Parse(item.Value);
                //get the reviewer
                reviewer = AllReviewers.Where(x => x.ID == id).FirstOrDefault();
                items.Add(new ApptAttendeeManagerModel
                {
                    AttendeeId = id,
                    BusinessRefId = businessrefid,
                    DeptNameEnumId = deptnameenumid,
                    DeptNameListId = deptnamelistid,
                    FirstName = reviewer.FirstName,
                    LastName = reviewer.LastName
                });
            }
            foreach (SelectListItem item in MechPersonSelectList)
            {
                deptnameenumid = (int)DepartmentNameEnums.Mechanical;
                businessrefid = (int)DepartmentNameEnums.Mechanical;
                deptnamelistid = DepartmentNameList.Mechanical;
                int id = int.Parse(item.Value);
                //get the reviewer
                reviewer = AllReviewers.Where(x => x.ID == id).FirstOrDefault();
                items.Add(new ApptAttendeeManagerModel
                {
                    AttendeeId = id,
                    BusinessRefId = businessrefid,
                    DeptNameEnumId = deptnameenumid,
                    DeptNameListId = deptnamelistid,
                    FirstName = reviewer.FirstName,
                    LastName = reviewer.LastName
                });
            }
            foreach (SelectListItem item in PlumPersonSelectList)
            {
                deptnameenumid = (int)DepartmentNameEnums.Plumbing;
                businessrefid = (int)DepartmentNameEnums.Plumbing;
                deptnamelistid = DepartmentNameList.Plumbing;
                int id = int.Parse(item.Value);
                //get the reviewer
                reviewer = AllReviewers.Where(x => x.ID == id).FirstOrDefault();
                items.Add(new ApptAttendeeManagerModel
                {
                    AttendeeId = id,
                    BusinessRefId = businessrefid,
                    DeptNameEnumId = deptnameenumid,
                    DeptNameListId = deptnamelistid,
                    FirstName = reviewer.FirstName,
                    LastName = reviewer.LastName
                });
            }
            foreach (SelectListItem item in BackPersonSelectList)
            {
                deptnameenumid = (int)DepartmentNameEnums.Backflow;
                businessrefid = (int)DepartmentNameEnums.Backflow;
                deptnamelistid = DepartmentNameList.Backflow;
                int id = int.Parse(item.Value);
                //get the reviewer
                reviewer = AllReviewers.Where(x => x.ID == id).FirstOrDefault();
                items.Add(new ApptAttendeeManagerModel
                {
                    AttendeeId = id,
                    BusinessRefId = businessrefid,
                    DeptNameEnumId = deptnameenumid,
                    DeptNameListId = deptnamelistid,
                    FirstName = reviewer.FirstName,
                    LastName = reviewer.LastName
                });
            }
            foreach (SelectListItem item in DayCPersonSelectList)
            {
                deptnameenumid = (int)DepartmentNameEnums.EH_Day_Care;
                businessrefid = (int)DepartmentNameEnums.EH_Day_Care;
                deptnamelistid = DepartmentNameList.Day_Care;
                int id = int.Parse(item.Value);
                //get the reviewer
                reviewer = AllReviewers.Where(x => x.ID == id).FirstOrDefault();
                items.Add(new ApptAttendeeManagerModel
                {
                    AttendeeId = id,
                    BusinessRefId = businessrefid,
                    DeptNameEnumId = deptnameenumid,
                    DeptNameListId = deptnamelistid,
                    FirstName = reviewer.FirstName,
                    LastName = reviewer.LastName
                });
            }
            foreach (SelectListItem item in FaciPersonSelectList)
            {
                deptnameenumid = (int)DepartmentNameEnums.EH_Facilities;
                businessrefid = (int)DepartmentNameEnums.EH_Facilities;
                deptnamelistid = DepartmentNameList.Facility_Lodging;
                int id = int.Parse(item.Value);
                //get the reviewer
                reviewer = AllReviewers.Where(x => x.ID == id).FirstOrDefault();
                items.Add(new ApptAttendeeManagerModel
                {
                    AttendeeId = id,
                    BusinessRefId = businessrefid,
                    DeptNameEnumId = deptnameenumid,
                    DeptNameListId = deptnamelistid,
                    FirstName = reviewer.FirstName,
                    LastName = reviewer.LastName
                });
            }
            foreach (SelectListItem item in FirePersonSelectList)
            {
                deptnameenumid = (int)DepartmentNameEnums.Fire_Davidson;
                businessrefid = (int)DepartmentNameEnums.Fire_Davidson;
                deptnamelistid = DepartmentNameList.Fire;
                int id = int.Parse(item.Value);
                //get the reviewer
                reviewer = AllReviewers.Where(x => x.ID == id).FirstOrDefault();
                items.Add(new ApptAttendeeManagerModel
                {
                    AttendeeId = id,
                    BusinessRefId = businessrefid,
                    DeptNameEnumId = deptnameenumid,
                    DeptNameListId = deptnamelistid,
                    FirstName = reviewer.FirstName,
                    LastName = reviewer.LastName
                });
            }
            foreach (SelectListItem item in FoodPersonSelectList)
            {
                deptnameenumid = (int)DepartmentNameEnums.EH_Food;
                businessrefid = (int)DepartmentNameEnums.EH_Food;
                deptnamelistid = DepartmentNameList.Food_Service;
                int id = int.Parse(item.Value);
                //get the reviewer
                reviewer = AllReviewers.Where(x => x.ID == id).FirstOrDefault();
                items.Add(new ApptAttendeeManagerModel
                {
                    AttendeeId = id,
                    BusinessRefId = businessrefid,
                    DeptNameEnumId = deptnameenumid,
                    DeptNameListId = deptnamelistid,
                    FirstName = reviewer.FirstName,
                    LastName = reviewer.LastName
                });
            }
            foreach (SelectListItem item in PoolPersonSelectList)
            {
                deptnameenumid = (int)DepartmentNameEnums.EH_Pool;
                businessrefid = (int)DepartmentNameEnums.EH_Pool;
                deptnamelistid = DepartmentNameList.Public_Pool;
                int id = int.Parse(item.Value);
                //get the reviewer
                reviewer = AllReviewers.Where(x => x.ID == id).FirstOrDefault();
                items.Add(new ApptAttendeeManagerModel
                {
                    AttendeeId = id,
                    BusinessRefId = businessrefid,
                    DeptNameEnumId = deptnameenumid,
                    DeptNameListId = deptnamelistid,
                    FirstName = reviewer.FirstName,
                    LastName = reviewer.LastName
                });
            }
            foreach (SelectListItem item in ZoniPersonSelectList)
            {
                deptnameenumid = (int)DepartmentNameEnums.Zone_Davidson;
                businessrefid = (int)DepartmentNameEnums.Zone_Davidson;
                deptnamelistid = DepartmentNameList.Zoning;
                int id = int.Parse(item.Value);
                //get the reviewer
                reviewer = AllReviewers.Where(x => x.ID == id).FirstOrDefault();
                items.Add(new ApptAttendeeManagerModel
                {
                    AttendeeId = id,
                    BusinessRefId = businessrefid,
                    DeptNameEnumId = deptnameenumid,
                    DeptNameListId = deptnamelistid,
                    FirstName = reviewer.FirstName,
                    LastName = reviewer.LastName
                });
            }
            return items;
        }
        private List<ApptAttendeeManagerModel> BuildAllReviewersVmWSOI()
        {
            //Building = 7,
            //Electrical = 8,
            //Mechanical = 9,
            //Plumbing = 10,
            //Zone_County = 11,
            //Zone_Cty_Chrlt = 18,
            //Fire_County = 19,
            //Fire_Cty_Chrlt = 26,
            //EH_Day_Care = 27,
            //EH_Food = 28,
            //EH_Pool = 29,
            //EH_Facilities = 30,
            //Backflow = 31,
            Reviewer reviewer;
            List<ApptAttendeeManagerModel> items = new List<ApptAttendeeManagerModel>();
            int deptnameenumid = 0;
            int businessrefid = 0;
            string deptnamelistid = string.Empty;
            foreach (SelectListItem item in BldgPersonSelectList)
            {
                deptnameenumid = (int)DepartmentNameEnums.Building;
                businessrefid = (int)DepartmentNameEnums.Building;
                deptnamelistid = DepartmentNameList.Building;
                int id = int.Parse(item.Value);
                //get the reviewer
                reviewer = AllReviewers.Where(x => x.ID == id).FirstOrDefault();
                items.Add(new ApptAttendeeManagerModel
                {
                    AttendeeId = id,
                    BusinessRefId = businessrefid,
                    DeptNameEnumId = deptnameenumid,
                    DeptNameListId = deptnamelistid,
                    FirstName = reviewer.FirstName,
                    LastName = reviewer.LastName
                });
            }
            foreach (SelectListItem item in ElecPersonSelectList)
            {
                deptnameenumid = (int)DepartmentNameEnums.Electrical;
                businessrefid = (int)DepartmentNameEnums.Electrical;
                deptnamelistid = DepartmentNameList.Electrical;
                int id = int.Parse(item.Value);
                //get the reviewer
                reviewer = AllReviewers.Where(x => x.ID == id).FirstOrDefault();
                items.Add(new ApptAttendeeManagerModel
                {
                    AttendeeId = id,
                    BusinessRefId = businessrefid,
                    DeptNameEnumId = deptnameenumid,
                    DeptNameListId = deptnamelistid,
                    FirstName = reviewer.FirstName,
                    LastName = reviewer.LastName
                });
            }
            foreach (SelectListItem item in MechPersonSelectList)
            {
                deptnameenumid = (int)DepartmentNameEnums.Mechanical;
                businessrefid = (int)DepartmentNameEnums.Mechanical;
                deptnamelistid = DepartmentNameList.Mechanical;
                int id = int.Parse(item.Value);
                //get the reviewer
                reviewer = AllReviewers.Where(x => x.ID == id).FirstOrDefault();
                items.Add(new ApptAttendeeManagerModel
                {
                    AttendeeId = id,
                    BusinessRefId = businessrefid,
                    DeptNameEnumId = deptnameenumid,
                    DeptNameListId = deptnamelistid,
                    FirstName = reviewer.FirstName,
                    LastName = reviewer.LastName
                });
            }
            foreach (SelectListItem item in PlumPersonSelectList)
            {
                deptnameenumid = (int)DepartmentNameEnums.Plumbing;
                businessrefid = (int)DepartmentNameEnums.Plumbing;
                deptnamelistid = DepartmentNameList.Plumbing;
                int id = int.Parse(item.Value);
                //get the reviewer
                reviewer = AllReviewers.Where(x => x.ID == id).FirstOrDefault();
                items.Add(new ApptAttendeeManagerModel
                {
                    AttendeeId = id,
                    BusinessRefId = businessrefid,
                    DeptNameEnumId = deptnameenumid,
                    DeptNameListId = deptnamelistid,
                    FirstName = reviewer.FirstName,
                    LastName = reviewer.LastName
                });
            }
            foreach (SelectListItem item in BackPersonSelectList)
            {
                deptnameenumid = (int)DepartmentNameEnums.Backflow;
                businessrefid = (int)DepartmentNameEnums.Backflow;
                deptnamelistid = DepartmentNameList.Backflow;
                int id = int.Parse(item.Value);
                //get the reviewer
                reviewer = AllReviewers.Where(x => x.ID == id).FirstOrDefault();
                items.Add(new ApptAttendeeManagerModel
                {
                    AttendeeId = id,
                    BusinessRefId = businessrefid,
                    DeptNameEnumId = deptnameenumid,
                    DeptNameListId = deptnamelistid,
                    FirstName = reviewer.FirstName,
                    LastName = reviewer.LastName
                });
            }
            foreach (SelectListItem item in DayCPersonSelectList)
            {
                deptnameenumid = (int)DepartmentNameEnums.EH_Day_Care;
                businessrefid = (int)DepartmentNameEnums.EH_Day_Care;
                deptnamelistid = DepartmentNameList.Day_Care;
                int id = int.Parse(item.Value);
                //get the reviewer
                reviewer = AllReviewers.Where(x => x.ID == id).FirstOrDefault();
                items.Add(new ApptAttendeeManagerModel
                {
                    AttendeeId = id,
                    BusinessRefId = businessrefid,
                    DeptNameEnumId = deptnameenumid,
                    DeptNameListId = deptnamelistid,
                    FirstName = reviewer.FirstName,
                    LastName = reviewer.LastName
                });
            }
            foreach (SelectListItem item in FaciPersonSelectList)
            {
                deptnameenumid = (int)DepartmentNameEnums.EH_Facilities;
                businessrefid = (int)DepartmentNameEnums.EH_Facilities;
                deptnamelistid = DepartmentNameList.Facility_Lodging;
                int id = int.Parse(item.Value);
                //get the reviewer
                reviewer = AllReviewers.Where(x => x.ID == id).FirstOrDefault();
                items.Add(new ApptAttendeeManagerModel
                {
                    AttendeeId = id,
                    BusinessRefId = businessrefid,
                    DeptNameEnumId = deptnameenumid,
                    DeptNameListId = deptnamelistid,
                    FirstName = reviewer.FirstName,
                    LastName = reviewer.LastName
                });
            }
            foreach (SelectListItem item in FirePersonSelectList)
            {
                deptnameenumid = (int)DepartmentNameEnums.Fire_Davidson;
                businessrefid = (int)DepartmentNameEnums.Fire_Davidson;
                deptnamelistid = DepartmentNameList.Fire;
                int id = int.Parse(item.Value);
                //get the reviewer
                reviewer = AllReviewers.Where(x => x.ID == id).FirstOrDefault();
                items.Add(new ApptAttendeeManagerModel
                {
                    AttendeeId = id,
                    BusinessRefId = businessrefid,
                    DeptNameEnumId = deptnameenumid,
                    DeptNameListId = deptnamelistid,
                    FirstName = reviewer.FirstName,
                    LastName = reviewer.LastName
                });
            }
            foreach (SelectListItem item in FireCityPersonSelectList)
            {
                deptnameenumid = (int)DepartmentNameEnums.Fire_Cty_Chrlt;
                businessrefid = (int)DepartmentNameEnums.Fire_Cty_Chrlt;
                deptnamelistid = DepartmentNameList.Fire_Charlotte;
                int id = int.Parse(item.Value);
                //get the reviewer
                reviewer = AllReviewers.Where(x => x.ID == id).FirstOrDefault();
                items.Add(new ApptAttendeeManagerModel
                {
                    AttendeeId = id,
                    BusinessRefId = businessrefid,
                    DeptNameEnumId = deptnameenumid,
                    DeptNameListId = deptnamelistid,
                    FirstName = reviewer.FirstName,
                    LastName = reviewer.LastName
                });
            }
            foreach (SelectListItem item in FoodPersonSelectList)
            {
                deptnameenumid = (int)DepartmentNameEnums.EH_Food;
                businessrefid = (int)DepartmentNameEnums.EH_Food;
                deptnamelistid = DepartmentNameList.Food_Service;
                int id = int.Parse(item.Value);
                //get the reviewer
                reviewer = AllReviewers.Where(x => x.ID == id).FirstOrDefault();
                items.Add(new ApptAttendeeManagerModel
                {
                    AttendeeId = id,
                    BusinessRefId = businessrefid,
                    DeptNameEnumId = deptnameenumid,
                    DeptNameListId = deptnamelistid,
                    FirstName = reviewer.FirstName,
                    LastName = reviewer.LastName
                });
            }
            foreach (SelectListItem item in PoolPersonSelectList)
            {
                deptnameenumid = (int)DepartmentNameEnums.EH_Pool;
                businessrefid = (int)DepartmentNameEnums.EH_Pool;
                deptnamelistid = DepartmentNameList.Public_Pool;
                int id = int.Parse(item.Value);
                //get the reviewer
                reviewer = AllReviewers.Where(x => x.ID == id).FirstOrDefault();
                items.Add(new ApptAttendeeManagerModel
                {
                    AttendeeId = id,
                    BusinessRefId = businessrefid,
                    DeptNameEnumId = deptnameenumid,
                    DeptNameListId = deptnamelistid,
                    FirstName = reviewer.FirstName,
                    LastName = reviewer.LastName
                });
            }
            foreach (SelectListItem item in ZoniCountyPersonSelectList)
            {
                deptnameenumid = (int)DepartmentNameEnums.Zone_Davidson;
                businessrefid = (int)DepartmentNameEnums.Zone_Davidson;
                deptnamelistid = DepartmentNameList.Zoning;
                int id = int.Parse(item.Value);
                //get the reviewer
                reviewer = AllReviewers.Where(x => x.ID == id).FirstOrDefault();
                items.Add(new ApptAttendeeManagerModel
                {
                    AttendeeId = id,
                    BusinessRefId = businessrefid,
                    DeptNameEnumId = deptnameenumid,
                    DeptNameListId = deptnamelistid,
                    FirstName = reviewer.FirstName,
                    LastName = reviewer.LastName
                });
            }
            foreach (SelectListItem item in ZoniCityPersonSelectList)
            {
                deptnameenumid = (int)DepartmentNameEnums.Zone_Cty_Chrlt;
                businessrefid = (int)DepartmentNameEnums.Zone_Cty_Chrlt;
                deptnamelistid = DepartmentNameList.Zoning_Charlotte;
                int id = int.Parse(item.Value);
                //get the reviewer
                reviewer = AllReviewers.Where(x => x.ID == id).FirstOrDefault();
                items.Add(new ApptAttendeeManagerModel
                {
                    AttendeeId = id,
                    BusinessRefId = businessrefid,
                    DeptNameEnumId = deptnameenumid,
                    DeptNameListId = deptnamelistid,
                    FirstName = reviewer.FirstName,
                    LastName = reviewer.LastName
                });
            }
            foreach (SelectListItem item in ZoniHuntersvillePersonSelectList)
            {
                deptnameenumid = (int)DepartmentNameEnums.Zone_Huntersville;
                businessrefid = (int)DepartmentNameEnums.Zone_Huntersville;
                deptnamelistid = DepartmentNameList.Zoning_Huntersville;
                int id = int.Parse(item.Value);
                //get the reviewer
                reviewer = AllReviewers.Where(x => x.ID == id).FirstOrDefault();
                items.Add(new ApptAttendeeManagerModel
                {
                    AttendeeId = id,
                    BusinessRefId = businessrefid,
                    DeptNameEnumId = deptnameenumid,
                    DeptNameListId = deptnamelistid,
                    FirstName = reviewer.FirstName,
                    LastName = reviewer.LastName
                });
            }
            foreach (SelectListItem item in ZoniMintHillPersonSelectList)
            {
                deptnameenumid = (int)DepartmentNameEnums.Zone_Mint_Hill;
                businessrefid = (int)DepartmentNameEnums.Zone_Mint_Hill;
                deptnamelistid = DepartmentNameList.Zoning_MintHill;
                int id = int.Parse(item.Value);
                //get the reviewer
                reviewer = AllReviewers.Where(x => x.ID == id).FirstOrDefault();
                items.Add(new ApptAttendeeManagerModel
                {
                    AttendeeId = id,
                    BusinessRefId = businessrefid,
                    DeptNameEnumId = deptnameenumid,
                    DeptNameListId = deptnamelistid,
                    FirstName = reviewer.FirstName,
                    LastName = reviewer.LastName
                });
            }
            return items;
        }
        #endregion Private Methods
    }
}