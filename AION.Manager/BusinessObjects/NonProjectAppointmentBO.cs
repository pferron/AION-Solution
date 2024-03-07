using AION.BL.Models;
using AION.Engine.BusinessEntities;
using AIONEstimator.Engine.BusinessObjects;
using System;
using System.Collections.Generic;

namespace AION.BL.BusinessObjects
{
    public class NonProjectAppointmentBO : INonProjectAppointmentBO
    {
        private NonProjectAppoinmentBO _itemBO;
        private NonProjectAppoinmentBE _itemBE;

        public List<NonProjectAppointment> GetInstance()
        {

            List<NonProjectAppointment> items = new List<NonProjectAppointment>();
            try
            {
                _itemBO = new NonProjectAppoinmentBO();
                List<NonProjectAppoinmentBE> beitems = _itemBO.GetList();
                foreach (NonProjectAppoinmentBE be in beitems)
                {
                    items.Add(ConvertItemBEToModel(be));
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return items;
        }

        public NonProjectAppointment GetInstance(int id)
        {
            _itemBO = new NonProjectAppoinmentBO();
            _itemBE = _itemBO.GetById(id);
            return ConvertItemBEToModel(_itemBE);
        }

        public int DeleteNPAById(int scheduleId, bool flag)
        {
            _itemBO = new NonProjectAppoinmentBO();
            return _itemBO.Delete(scheduleId, flag);
        }

        public List<UserIdentity> GetAttendeesByNPAId(int id)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// TODO: fix GetListByDateRange
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <returns></returns>
        public List<NonProjectAppointment> GetListByDateRange(DateTime startdate, DateTime enddate)
        {
            return SearchNPAs(0, 0, string.Empty, startdate, enddate);
        }

        public List<NonProjectAppointment> SearchNPAs(int? type, int? reviewerId, string searchtxt, DateTime? startdate, DateTime? enddate)
        {

            List<NonProjectAppointment> items = new List<NonProjectAppointment>();

            _itemBO = new NonProjectAppoinmentBO();
            List<NonProjectAppoinmentBE> beitems = _itemBO.Search(type, reviewerId, searchtxt, startdate, enddate);
            foreach (NonProjectAppoinmentBE be in beitems)
            {
                items.Add(ConvertItemBEToModel(be));
            }

            return items;
        }

        public List<NPASearchResultBE> SearchNPAs_v2(int? type, int? reviewerId, string searchtxt, DateTime? startdate, DateTime? enddate)
        {

            List<NPASearchResultBE> items = new List<NPASearchResultBE>();

            _itemBO = new NonProjectAppoinmentBO();
            List<NPASearchResultBE> beitems = _itemBO.Searchv2(type, reviewerId, searchtxt, startdate, enddate);

            return beitems;
        }
        public int InsertAttendee(int userId, int npaId)
        {
            throw new NotImplementedException();
        }

        public int InsertNPA(NonProjectAppointment npa)
        {
            throw new NotImplementedException();
        }

        public int RemoveAttendee(int userId, int npaId)
        {
            throw new NotImplementedException();
        }



        public int UpdateNPA(NonProjectAppointment npa)
        {
            throw new NotImplementedException();
        }

        #region Private Methods
        /// <summary>
        /// converts BE object to view model
        /// TODO: build object
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private NonProjectAppointment ConvertItemBEToModel(NonProjectAppoinmentBE item)
        {
            NonProjectAppointment npa = new NonProjectAppointment
            {
                AppoinmentName = item.AppoinmentName,
                AppoinmentRecurrenceRefID = item.AppoinmentRecurrenceRefID,
                AppointmentFrom = item.AppointmentFrom,
                AppointmentTo = item.AppointmentTo,
                Users = new List<UserIdentity>(),
                IsAllBackFlow = item.IsAllBackFlow,
                IsAllBuild = item.IsAllBuild,
                IsAllDay = item.IsAllDay,
                IsAllEhsDayCare = item.IsAllEhsDayCare,
                IsAllEhsFood = item.IsAllEhsFood,
                IsAllEhsLodge = item.IsAllEhsLodge,
                IsAllEhsPool = item.IsAllEhsPool,
                IsAllElectric = item.IsAllElectric,
                IsAllFire = item.IsAllFire,
                IsAllMech = item.IsAllMech,
                IsAllPlanReviewers = item.IsAllPlanReviewers,
                IsAllPlumb = item.IsAllPlumb,
                IsAllZoning = item.IsAllZoning,
                CreatedDate = item.CreatedDate.Value,
                UpdatedDate = item.UpdatedDate.Value,
                CreatedUser = new UserIdentityModelBO().GetInstance(int.Parse(item.CreatedByWkrId)),
                UpdatedUser = new UserIdentityModelBO().GetInstance(int.Parse(item.UpdatedByWkrId)),
                MeetingRoomRefId = item.MeetingRoomRefID,
                NonProjectAppointmentID = item.NonProjectAppointmentID,
                NPATypeRefID = item.NPATypeRefID,
                UserId = !string.IsNullOrWhiteSpace(item.UpdatedByWkrId) ? item.UpdatedByWkrId : "1"
            };
            return npa;
        }
        #endregion Private Methods
    }
    public interface INonProjectAppointmentBO
    {
        List<NonProjectAppointment> GetInstance();
        NonProjectAppointment GetInstance(int id);
        List<NonProjectAppointment> GetListByDateRange(DateTime startdate, DateTime enddate);
        int DeleteNPAById(int scheduleId, bool flag);
        List<NonProjectAppointment> SearchNPAs(int? type, int? reviewerId, string searchtxt, DateTime? startdate, DateTime? enddate);
        int InsertNPA(NonProjectAppointment npa);
        int UpdateNPA(NonProjectAppointment npa);
        int InsertAttendee(int userId, int npaId);
        int RemoveAttendee(int userId, int npaId);
        List<UserIdentity> GetAttendeesByNPAId(int id);
    }
}
