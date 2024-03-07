using AION.Base;
using AION.Engine.BusinessEntities;
using AIONEstimator.Engine.BusinessObjects;
using System.Collections.Generic;
using System.Linq;

namespace AION.BL.BusinessObjects
{
    public class AppointmentResponseStatusModelBO : ModelBaseModelBO
    {
        public bool RefreshList()
        {
            LocalStorage<List<AppointmentResponseStatus>>.Delete("AppointmentResponseStatus_List");
            return true;
        }

        public AppointmentResponseStatus GetInstance(int ID)
        {
            var t = BaseList.Where(x => x.ID == ID).FirstOrDefault();
            return t;
        }
        public AppointmentResponseStatus GetInstance(AppointmentResponseStatusEnum appointmentResponseStatusEnum)
        {
            var t = BaseList.Where(x => x.ApptResponseStatusEnum == appointmentResponseStatusEnum).FirstOrDefault();
            return t;
        }

        public List<AppointmentResponseStatus> CreateInstance()
        {
            List<AppointmentResponseStatus> ret = new List<AppointmentResponseStatus>();
            AppointmentResponseStatusRefBO bo = new AppointmentResponseStatusRefBO();
            List<AppointmentResponseStatusRefBE> be = bo.GetList();
            foreach (var item in be)
            {
                ret.Add(ConvertData(item));
            }
            return ret;
        }
        public AppointmentResponseStatus ConvertData(AppointmentResponseStatusRefBE be)
        {
            AppointmentResponseStatus ret = new AppointmentResponseStatus();
            InjectBaseObjects(ret, be.ApptResponseStatusRefId.Value, be.CreatedDate.Value, be.UpdatedDate.Value, be.CreatedByWkrId, be.UpdatedByWkrId);
            ret.ApptResponseStatusDesc = be.ApptResponseStatusDesc;
            if (be.EnumMappingValNbr != null)
                ret.ApptResponseStatusEnum = (AppointmentResponseStatusEnum)be.EnumMappingValNbr;
            ret.ID = be.ApptResponseStatusRefId.Value;
            ret.ApptResponseStatusRefId = be.ApptResponseStatusRefId;
            return ret;
        }

        public List<AppointmentResponseStatus> BaseList
        {
            get
            {
                List<AppointmentResponseStatus> ret = LocalStorage<List<AppointmentResponseStatus>>.GetValue("AppointmentResponseStatus_List");
                if (ret == null)
                {
                    ret = CreateInstance();
                    LocalStorage<List<AppointmentResponseStatus>>.Add("AppointmentResponseStatus_List", ret);
                }
                return ret;
            }
        }
    }
}