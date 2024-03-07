using AION.Base;
using System;

namespace AION.BL.BusinessObjects
{
    public class ModelBaseModelBO : BaseAdapter
    {
        protected bool InjectBaseObjects(ModelBase inheritedObject, int _ID, DateTime createdDate, DateTime updatedDate, string createdUserID, string updatedUserID)
        {
            ModelBase ret = inheritedObject;
            UserIdentity systemUser = new UserIdentity { ID = 1 };
            int _createdUserID, _updatedUserID;
            ret.ID = _ID;
            if (int.TryParse(createdUserID, out _createdUserID) == true)
                ret.CreatedUser = new UserIdentity { ID = _createdUserID };
            else
                ret.CreatedUser = systemUser; //if user id is invalid then defaults it to system;
            if (int.TryParse(updatedUserID, out _updatedUserID) == true)
                ret.UpdatedUser = new UserIdentity { ID = _updatedUserID };
            else
                ret.UpdatedUser = systemUser; //if user id is invalid then defaults it to system;
            ret.CreatedDate = createdDate;
            ret.UpdatedDate = updatedDate;
            return true;
        }
    }
}
