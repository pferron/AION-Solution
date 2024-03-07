using AION.Base;
using AION.BL.Adapters;
using AION.BL.BusinessObjects;
using AION.BL.Models;
using AION.BL.Models.Base;
using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using Meck.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AION.BL
{
    public class NoteTypeModelBO: INoteTypeModelBO
    {

        public NoteType GetInstance(int externalSysytemID, string noteTypeExternalRef)
        {
            var t = BaseList.Where(x => x.NotesExternalRef == noteTypeExternalRef && x.ExternalSystem.ID == externalSysytemID).FirstOrDefault();
            return t;
        }

        public bool RefreshList()
        {
            LocalStorage<List<NoteType>>.Delete("NoteType_List");
            return true;
        }

        public NoteType GetInstance(int ID)
        {
            var t = BaseList.Where(x => x.ID == ID).FirstOrDefault();
            return t;
        }

        public NoteType GetInstance(NoteTypeEnum NoteTypeEnum)
        {
            var t = BaseList.Where(x => x.Type == NoteTypeEnum).FirstOrDefault();
            return t;
        }

        NoteType IModelCollectionCreater<NoteType, NotesTypeRefBE>.ConvertData(NotesTypeRefBE be)
        {
            NoteType ret = new NoteType();
            ret.CreatedDate = be.CreatedDate.Value;
            ret.CreatedUser = new  UserIdentityModelBO().GetInstance(int.Parse(be.CreatedByWkrId));
            ret.ExternalSystem = new  ExternalSystemModelBO().GetInstance(be.ExternalSystemRefId.Value);
            ret.ID = be.NotesTypeRefId.Value;
            ret.NotesExternalRef = be.SrcSystemValTxt;
            ret.Type = new NoteTypeEnum().CreateInstance(be.SrcSystemValTxt);
            ret.TypeName = be.NotesTypeRefName;
            ret.UpdatedDate = be.UpdatedDate.Value;
            ret.UpdatedUser = new UserIdentityModelBO().GetInstance(int.Parse(be.UpdatedByWkrId));
            return ret;
        }

        public List<NoteType> BaseList
        {
            get
            {
                List<NoteType> ret = LocalStorage<List<NoteType>>.GetValue("NoteType_List");
                if (ret == null)
                {
                    ret = ((IModelCollectionCreater<NoteType, NotesTypeRefBE>)this).CreateInstance();
                    LocalStorage<List<NoteType>>.Add("NoteType_List", ret);
                }
                return ret;
            }
        }

        List<NoteType> IModelCollectionCreater<NoteType, NotesTypeRefBE>.CreateInstance()
        {
            List<NoteType> ret = new List<NoteType>();
            NotesTypeRefBO bo = new NotesTypeRefBO();
            List<NotesTypeRefBE> be = bo.GetAllList();
            foreach (var item in be)
            {
                ret.Add(((IModelCollectionCreater<NoteType, NotesTypeRefBE>)this).ConvertData(item));
            }
            return ret;
        }
    }

    public interface INoteTypeModelBO: IModelCollectionCreater<NoteType, NotesTypeRefBE>
    {
        NoteType GetInstance(int ID);

        NoteType GetInstance(int externalSysytemID, string noteTypeExternalRef);

        NoteType GetInstance(NoteTypeEnum NoteTypeEnum);
    }
}
