using AION.Base;
using AION.Engine.BusinessEntities;
using AIONEstimator.Engine.BusinessObjects;
using System.Collections.Generic;
using System.Linq;

namespace AION.BL.BusinessObjects
{
    public class AppointmentCancellationRefModelBO : ModelBaseModelBO
    {
        public bool RefreshList()
        {
            LocalStorage<List<AppointmentCancellationReason>>.Delete("AppointmentCancellationRef_List");
            return true;
        }

        public AppointmentCancellationReason GetInstance(int ID)
        {
            var t = BaseList.Where(x => x.ID == ID).FirstOrDefault();
            return t;
        }
        public AppointmentCancellationReason GetInstance(AppointmentCancellationEnum appointmentCancellationEnum)
        {
            var t = BaseList.Where(x => x.ApptCancellationEnum == appointmentCancellationEnum).FirstOrDefault();
            return t;
        }

        public List<AppointmentCancellationReason> CreateInstance()
        {
            List<AppointmentCancellationReason> ret = new List<AppointmentCancellationReason>();
            AppointmentCancellationRefBO bo = new AppointmentCancellationRefBO();
            List<AppointmentCancellationRefBE> be = bo.GetList();
            foreach (var item in be)
            {
                ret.Add(ConvertData(item));
            }
            return ret;
        }
        public AppointmentCancellationReason ConvertData(AppointmentCancellationRefBE be)
        {
            AppointmentCancellationReason ret = new AppointmentCancellationReason();
            InjectBaseObjects(ret, be.ApptCancellationRefId.Value, be.CreatedDate.Value, be.UpdatedDate.Value, be.CreatedByWkrId, be.UpdatedByWkrId);
            ret.ApptCancellationDesc = be.CancellationDesc;
            if (be.EnumMappingValNbr != null)
                ret.ApptCancellationEnum = (AppointmentCancellationEnum)be.EnumMappingValNbr;
            ret.ID = be.ApptCancellationRefId.Value;
            ret.ApptCancellationRefId = be.ApptCancellationRefId;
            return ret;
        }

        public List<AppointmentCancellationReason> BaseList
        {
            get
            {
                List<AppointmentCancellationReason> ret = LocalStorage<List<AppointmentCancellationReason>>.GetValue("AppointmentCancellationRef_List");
                if (ret == null)
                {
                    ret = CreateInstance();
                    LocalStorage<List<AppointmentCancellationReason>>.Add("AppointmentCancellationRef_List", ret);
                }
                return ret;
            }
        }
    }
}