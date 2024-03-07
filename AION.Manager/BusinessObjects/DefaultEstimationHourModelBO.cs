using AION.Base;
using AION.BL.BusinessObjects;
using AION.BL.Models.Base;
using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using System.Collections.Generic;
using System.Linq;

namespace AION.BL
{
    public class DefaultEstimationHourModelBO : ModelBaseModelBO, IModelCollectionCreater<DefaultEstimationHour, DefaultEstimationHoursBE>
    {
        static List<DefaultEstimationHour> _BaseList = new List<DefaultEstimationHour>(); // created this as static to keep the value across all the instances so it can reduce calls to DB.

        int PropertyTypeID = -1;
        public DefaultEstimationHour GetInstance(int ID)
        {
            DefaultEstimationHour basereturnmodel = new DefaultEstimationHour
            {
                ID = ID
            };
            var t = ((IModelCollectionCreater<DefaultEstimationHour, DefaultEstimationHoursBE>)this).BaseList.Where(x => x.ID == ID).FirstOrDefault();
            return t != null ? t : basereturnmodel;
        }

        public DefaultEstimationHour GetInstance(DepartmentNameEnums departmentNameEnum, PropertyTypeEnums propertyTypeEnum)
        {
            return GetInstance((int)departmentNameEnum, (int)propertyTypeEnum);
        }

        public DefaultEstimationHour GetInstance(int departmentID, int propertyTypeID)
        {
            //this need to be first set since it will be used in BaseList property.
            PropertyTypeID = propertyTypeID;
            //return null if no default exists
            var t = ((IModelCollectionCreater<DefaultEstimationHour, DefaultEstimationHoursBE>)this).BaseList.Where(x => x.DepartmentID == departmentID && x.PropertyTypeID == propertyTypeID).FirstOrDefault();
            return t;
        }

        List<DefaultEstimationHour> IModelCollectionCreater<DefaultEstimationHour, DefaultEstimationHoursBE>.BaseList
        {
            get
            {
                if (_BaseList == null || _BaseList.Count == 0)
                {
                    List<DefaultEstimationHour> val = LocalStorage<List<DefaultEstimationHour>>.GetValue("DefaultEstimationHour_List");
                    if (val == null)
                    {
                        if (PropertyTypeID == -1) //if class is called by ID not property type.
                        {
                            val = (this as IModelCollectionCreater<DefaultEstimationHour, DefaultEstimationHoursBE>).CreateInstance();
                        }
                        else
                        {
                            val = CreateInstance(PropertyTypeID);
                        }
                        LocalStorage<List<DefaultEstimationHour>>.Add("DefaultEstimationHour_List", val);
                    }
                    _BaseList = val;
                }
                else
                {
                    //if the request is for specific property type and it is not there in cache, then grab it and add to the list.
                    if (PropertyTypeID != -1 && _BaseList.Where(x => x.PropertyTypeID == PropertyTypeID).Any() == false)
                    {
                        List<DefaultEstimationHour> newval = CreateInstance(PropertyTypeID);
                        foreach (var item in newval)
                        {
                            _BaseList.Add(item);
                        }
                        LocalStorage<List<DefaultEstimationHour>>.Delete("DefaultEstimationHour_List");
                        LocalStorage<List<DefaultEstimationHour>>.Add("DefaultEstimationHour_List", _BaseList);
                    }
                }
                return _BaseList;
            }
        }


        List<DefaultEstimationHour> IModelCollectionCreater<DefaultEstimationHour, DefaultEstimationHoursBE>.CreateInstance()
        {
            List<DefaultEstimationHour> ret = new List<DefaultEstimationHour>();
            DefaultEstimationHoursBO bo = new DefaultEstimationHoursBO();
            List<DefaultEstimationHoursBE> be = bo.GetAllList().ToList();
            ret = new List<DefaultEstimationHour>();
            foreach (var item in be)
            {
                ret.Add((this as IModelCollectionCreater<DefaultEstimationHour, DefaultEstimationHoursBE>).ConvertData(item));
            }
            return ret;
        }

        List<DefaultEstimationHour> CreateInstance(int? projectTypeRefID = null)
        {
            List<DefaultEstimationHour> ret = new List<DefaultEstimationHour>();
            DefaultEstimationHoursBO bo = new DefaultEstimationHoursBO();
            List<DefaultEstimationHoursBE> be = bo.GetAllList(projectTypeRefID);
            ret = new List<DefaultEstimationHour>();
            foreach (var item in be)
            {
                ret.Add((this as IModelCollectionCreater<DefaultEstimationHour, DefaultEstimationHoursBE>).ConvertData(item));
            }
            return ret;
        }

        DefaultEstimationHour IModelCollectionCreater<DefaultEstimationHour, DefaultEstimationHoursBE>.ConvertData(DefaultEstimationHoursBE be)
        {
            DefaultEstimationHour ret = new DefaultEstimationHour();
            InjectBaseObjects(ret, be.DefaultEstimationHoursId.Value, be.CreatedDate.Value, be.UpdatedDate.Value, be.CreatedByWkrId, be.UpdatedByWkrId);
            ret.DefaultHours = be.DefaultHoursNbr.HasValue == false ? 0 : be.DefaultHoursNbr.Value;
            ret.DepartmentID = be.BusinessRefId.Value;
            ret.PropertyTypeID = be.ProjectTypeRefId.Value;
            ret.IsEnabled = be.IsActive;
            ret.EstimationHoursMode = be.EstimationHrsTxt;
            ret.IsModelUpdated = false;
            return ret;
        }

        public bool RefreshList()
        {
            LocalStorage<List<DefaultEstimationHour>>.Delete("DefaultEstimationHour_List");
            _BaseList = new List<DefaultEstimationHour>();
            DefaultEstimationHour v = ((IModelCollectionCreater<DefaultEstimationHour, DefaultEstimationHoursBE>)this).BaseList[0];//force the reload.
            return true;
        }

        public bool UpdateInstance(DefaultEstimationHour data)
        {
            if (data.IsModelUpdated == false)
                return true;
            DefaultEstimationHoursBO bo = new DefaultEstimationHoursBO();
            DefaultEstimationHoursBE be = new DefaultEstimationHoursBE();

            be.DefaultEstimationHoursId = data.ID;
            be.UpdatedDate = data.UpdatedDate;
            be.UpdatedByWkrId = data.UpdatedUser.ID.ToString();
            be.BusinessRefId = data.DepartmentID;
            be.ProjectTypeRefId = data.PropertyTypeID;
            be.Enabled = data.IsEnabled == true ? 1 : 0;
            be.EstimationHrsTxt = data.EstimationHoursMode;
            be.DefaultHoursNbr = decimal.Parse(data.DefaultHours.ToString("0.###############"));
            be.CreatedByWkrId = data.CreatedUser.ID.ToString();
            be.CreatedDate = data.CreatedDate;
            bo.Update(be);
            data.IsModelUpdated = false;//resetting this reference incase it is reused.
            return true;
        }
    }
}
