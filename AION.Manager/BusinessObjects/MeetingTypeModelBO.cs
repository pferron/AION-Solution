using AION.Base;
using AION.Engine.BusinessEntities;
using AIONEstimator.Engine.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AION.BL.BusinessObjects
{
    public class MeetingTypeModelBO : ModelBaseModelBO
    {
        public bool RefreshList()
        {
            LocalStorage<List<MeetingType>>.Delete("MeetingType_List");
            return true;
        }

        public MeetingType GetInstance(int ID)
        {
            var t = BaseList.Where(x => x.ID == ID).FirstOrDefault();
            return t;
        }

        public MeetingType GetInstance(string meetingTypeDesc)
        {
            var t = BaseList.Where(x => x.MeetingTypeDesc == meetingTypeDesc).FirstOrDefault();
            return t;
        }

        public List<MeetingType> CreateInstance()
        {
            List<MeetingType> ret = new List<MeetingType>();
            MeetingTypeRefBO bo = new MeetingTypeRefBO();
            List<MeetingTypeRefBE> be = bo.GetList();
            foreach (var item in be)
            {
                ret.Add(ConvertData(item));
            }
            return ret;
        }
        public MeetingType ConvertData(MeetingTypeRefBE be)
        {
            MeetingType ret = new MeetingType();
            InjectBaseObjects(ret, be.MeetingTypRefId.Value, be.CreatedDate.Value, be.UpdatedDate.Value, be.CreatedByWkrId, be.UpdatedByWkrId);
            ret.MeetingTypeDesc = be.MeetingTypDesc;
            if (be.EnumMappingValNbr != null)
                ret.MeetingTypeEnum = (MeetingTypeEnum)be.EnumMappingValNbr;
            ret.ID = be.MeetingTypRefId.Value;
            ret.MeetingTypeRefId = be.MeetingTypRefId;
            return ret;
        }

        public List<MeetingType> BaseList
        {
            get
            {
                List<MeetingType> ret = LocalStorage<List<MeetingType>>.GetValue("MeetingType_List");
                if (ret == null)
                {
                    ret = CreateInstance();
                    LocalStorage<List<MeetingType>>.Add("MeetingType_List", ret);
                }
                return ret;
            }
        }
    }
}