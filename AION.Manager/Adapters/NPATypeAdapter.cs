using AION.Base;
using AION.BL;
using AION.BL.Models;
using AION.Engine.BusinessEntities;
using AION.Manager.BusinessObjects;
using AIONEstimator.Engine.BusinessObjects;
using Meck.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace AION.Manager.Adapters
{
    public class NPATypeAdapter : BaseManagerAdapter, INPATypeAdapter
    {
        public bool UpdateNPAConfiguration(List<string> npaConfig)
        {
            NpaTypeRefBE be = new NpaTypeRefBE();
            NpaTypeRefBO bo = new NpaTypeRefBO();
            return true;
        }

        public List<NpaType> GetAll(bool includeOnlyActive = false)
        {
            List<NpaType> ret = new List<NpaType>();

            try
            {
                NpaTypeRefBO bo = new NpaTypeRefBO();
                List<NpaTypeRefBE> belst = bo.GetAllNPaTypes(includeOnlyActive);
                foreach (var item in belst)
                {
                    ret.Add(new NpaType()
                    {
                        ID = item.NpaTypeRefID.Value,
                        AppointmentTypeName = item.AppointmentTypeName,
                        IsActive = item.IsActive.Value,
                        CreatedDate = item.CreatedDate.Value,
                        UpdatedDate = item.UpdatedDate.Value,
                        CreatedUser = new UserIdentity() { ID = int.Parse(item.CreatedByWkrId) },
                        UpdatedUser = new UserIdentity() { ID = int.Parse(item.UpdatedByWkrId) },
                        TimeAllocationType = new TimeAllocationTypeRefModelBO().GetInstance(item.TimeAllocationTypeRefId.Value).TimeAllocationType,
                        TimeAllocationTypeRefId = item.TimeAllocationTypeRefId.Value
                    });
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in NPATypeAdapter GetAll - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }

            return ret;
        }

        public bool Insert(NpaType data)
        {
            bool success = false;
            try
            {
                NpaTypeRefBO bo = new NpaTypeRefBO();
                NpaTypeRefBE be = new NpaTypeRefBE();
                be.IsActive = data.IsActive;
                be.AppointmentTypeName = data.AppointmentTypeName;
                be.NpaTypeRefID = data.ID;
                be.CreatedByWkrId = data.CreatedUser == null ? "1" : data.CreatedUser.ID.ToString();
                be.CreatedDate = data.CreatedDate;
                be.UpdatedByWkrId = data.UpdatedUser == null ? "1" : data.UpdatedUser.ID.ToString();
                be.UpdatedDate = DateTime.Now;
                be.TimeAllocationTypeRefId = new TimeAllocationTypeRefModelBO().GetInstance(data.TimeAllocationType).ID;
                bo.Create(be);

                success = true;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in NPATypeAdapter GetAll - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }

            return success;
        }

        public bool MakeActive(NpaType data)
        {
            bool success = false;
            try
            {
                NpaTypeRefBO bo = new NpaTypeRefBO();
                NpaTypeRefBE be = new NpaTypeRefBE();
                be.IsActive = data.IsActive;
                be.NpaTypeRefID = data.ID;
                be.CreatedByWkrId = data.CreatedUser.ID.ToString();
                be.CreatedDate = data.CreatedDate;
                be.UpdatedByWkrId = data.UpdatedUser.ID.ToString();
                be.UpdatedDate = data.UpdatedDate;
                bo.Update(be);

                success = true;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in NPATypeAdapter GetAll - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }

            return success;
        }

        public bool MakeInActive(NpaType data)
        {
            bool success = false;
            try
            {
                NpaTypeRefBO bo = new NpaTypeRefBO();
                NpaTypeRefBE be = new NpaTypeRefBE();
                be.IsActive = data.IsActive;
                be.NpaTypeRefID = data.ID;
                be.CreatedByWkrId = data.CreatedUser.ID.ToString();
                be.CreatedDate = data.CreatedDate;
                be.UpdatedByWkrId = data.UpdatedUser.ID.ToString();
                be.UpdatedDate = data.UpdatedDate;
                bo.Update(be);

                success = true;
            }
            catch (Exception ex)
            {
                string errorMessage = "Error in NPATypeAdapter GetAll - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw ex;
            }

            return success;
        }
    }
}