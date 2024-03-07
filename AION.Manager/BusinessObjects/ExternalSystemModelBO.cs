using AION.Base;
using AION.BL.Adapters;
using AION.BL.Models.Base;
using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AION.BL.BusinessObjects
{
    public class ExternalSystemModelBO : ModelBaseModelBO, IExternalSystemBO
    {
        public List<ExternalSystem> BaseList
        {
            get
            {
                List<ExternalSystem> ret = LocalStorage<List<ExternalSystem>>.GetValue("ExternalSystem_List");
                if (ret == null)
                {
                    ret = (this as IModelCollectionCreater<ExternalSystem, ExternalSystemRefBE>).CreateInstance();
                    LocalStorage<List<ExternalSystem>>.Add("ExternalSystem_List", ret);
                }
                return ret;
            }
        }


        public bool RefreshList()
        {
            LocalStorage<List<List<ExternalSystem>>>.Delete("ExternalSystem_List");
            return true;
        }

        public ExternalSystem GetInstance(int ID)
        {
            var t = BaseList.Where(x => x.ID == ID).FirstOrDefault();
            return t;
        }

        public ExternalSystem GetInstance(string SystemName)
        {
            var t = BaseList.Where(x => x.SystemName == SystemName).FirstOrDefault();
            return t;
        }

        public ExternalSystem GetInstance(ExternalSystemEnum SystemEnum)
        {
            var t = BaseList.Where(x => x.ExternalSystemEnum == SystemEnum).FirstOrDefault();
            return t;
        }

        List<ExternalSystem> IModelCollectionCreater<ExternalSystem, ExternalSystemRefBE>.CreateInstance()
        {
            List<ExternalSystem> ret = new List<ExternalSystem>();
            try
            {
                //List<ExternalSystemRefBE> be = new APIHelper().GetAllExternalSystemRefBOList();
                ExternalSystemRefBO bo = new ExternalSystemRefBO();
                List<ExternalSystemRefBE> be = bo.GetAllList();
                foreach (var item in be)
                {
                    ret.Add((this as IModelCollectionCreater<ExternalSystem, ExternalSystemRefBE>).ConvertData(item));
                }
                return ret;
            }
            catch (Exception)
            {
                return ret;
            }
        }

        ExternalSystem IModelCollectionCreater<ExternalSystem, ExternalSystemRefBE>.ConvertData(ExternalSystemRefBE be)
        {
            ExternalSystem ret = new ExternalSystem();
            InjectBaseObjects(ret, be.ExternalSystemRefId.Value, be.CreatedDate.Value, be.UpdatedDate.Value, be.CreatedByWkrId, be.UpdatedByWkrId);
            ret.SystemName = be.ExternalSystemName;
            ret.Description = be.ExternalSystemDesc;
            ret.ExternalSystemEnum = (ExternalSystemEnum)be.EnumMappingValNbr;
            ret.AdditionalInfo = be.AddlInformationText;
            return ret;
        }
    }

    public interface IExternalSystemBO : IModelCollectionCreater<ExternalSystem, ExternalSystemRefBE>
    {

        ExternalSystem GetInstance(int ID);

        ExternalSystem GetInstance(string SystemName);

        ExternalSystem GetInstance(ExternalSystemEnum SystemEnum);
    }
}
