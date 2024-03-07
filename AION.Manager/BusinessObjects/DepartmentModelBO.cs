using AION.Base;
using AION.BL.Models.Base;
using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using Meck.Logging;
using Meck.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AION.BL.BusinessObjects
{
    public class DepartmentModelBO : ModelBaseModelBO, IDepartmentBO
    {

        //private List<string> _departmentstatuslist;
        public DepartmentModelBO()
        {
            //_departmentstatuslist = new List<string>();
            //_departmentstatuslist.Add(ProjectDisplayStatus.Pending);
            //_departmentstatuslist.Add(ProjectDisplayStatus.Late);
            //_departmentstatuslist.Add(ProjectDisplayStatus.Complete);
            //_departmentstatuslist.Add(ProjectDisplayStatus.CustomerResponded);
            //_departmentstatuslist.Add(ProjectDisplayStatus.AutoEstimationInProgress);
            //_departmentstatuslist.Add(ProjectDisplayStatus.AutoEstimationCompleteNA);
            //_departmentstatuslist.Add(ProjectDisplayStatus.AutoEstimationComplete);
        }
        public Department GetInstance(int departmentID)
        {
            try
            {
                var t = BaseList.Where(x => x.ID == departmentID).FirstOrDefault();
                return t;
            }

            catch (Exception ex)
            {
                string errorMessage = "An error occured while getting department instance: params" + departmentID + " - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);

                throw (new Exception("An error occured while getting department instance: params" + departmentID, ex));
            }
        }

        public Department GetInstance(DepartmentTypeEnum departmentType, DepartmentDivisionEnum division, DepartmentRegionEnum region)
        {
            try
            {
                //look for depts with 3 level of info this includes fire,zones etc..
                Department ret = BaseList.Where(x => x.DepartmentType.DepartmentType == departmentType &&
                    x.DepartmentDivision.DepartmentDivision == division &&
                    x.DepartmentRegion.DepartmentRegion == region).FirstOrDefault();
                //if the object is null then look for look for depts with level 2 info. this includes trades etc..
                if (ret == null)
                    ret = BaseList.Where(x => x.DepartmentType.DepartmentType == departmentType &&
                    x.DepartmentDivision.DepartmentDivision == division &&
                    x.DepartmentRegion.DepartmentRegion == DepartmentRegionEnum.NA).FirstOrDefault();
                //if still null then there is an issues with input. so return NA department.
                if (ret == null)
                    ret = BaseList.Where(x => x.DepartmentEnum == DepartmentNameEnums.NA).FirstOrDefault();
                return ret;
            }

            catch (Exception ex)
            {
                string errorMessage = "An error occured while getting department instance: params " + departmentType.ToString() + ", " + division.ToString() + ", " + region.ToString() + " - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw (new Exception("An error occured while getting department instance: params " + departmentType.ToString() + ", " + division.ToString() + ", " + region.ToString(), ex));
            }
        }

        public Department GetInstance(DepartmentTypeEnum departmentType, string accelaDivisionRef, string accelaRegionRef)
        {
            try
            {
                //look for depts with 3 level of info this includes fire,zones etc..
                Department ret = BaseList.Where(x => x.DepartmentType.DepartmentType == departmentType &&
                            x.DepartmentDivision.DepartmentTypeExternalRef == accelaDivisionRef && (x.DepartmentRegion == null ||
                            x.DepartmentRegion.DepartmentTypeExternalRef == accelaRegionRef)).FirstOrDefault();
                //if the object is null then look for look for depts with level 2 info. this includes trades etc..
                if (ret == null)
                    ret = BaseList.Where(x => x.DepartmentType.DepartmentType == departmentType &&
                            x.DepartmentDivision.DepartmentTypeExternalRef == accelaDivisionRef &&
                            x.DepartmentRegion.DepartmentTypeExternalRef == "NA").FirstOrDefault();
                //if still null then there is an issues with input. so return NA department.
                if (ret == null)
                    ret = BaseList.Where(x => x.DepartmentEnum == DepartmentNameEnums.NA).FirstOrDefault();
                return ret;
            }
            catch (Exception ex)
            {
                string errorMessage = "An error occurred while getting department instance: params " + departmentType.ToString() + ", " + accelaDivisionRef + ", " + accelaRegionRef + " - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw (new Exception("An error occurred while getting department instance: params " + departmentType.ToString() + ", " + accelaDivisionRef + ", " + accelaRegionRef, ex));
            }
        }

        public Department GetInstance(DepartmentNameEnums departmentNameEnum)
        {
            Department _department;
            _department = null;
            try
            {
                _department = BaseList.Where(x => x.ID == (int)departmentNameEnum).FirstOrDefault();
            }
            catch (Exception ex)
            {
                string errorMessage = "An error occured while getting department instance: " + departmentNameEnum.ToString() + " - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw (new Exception("An error occured while getting department instance: " + departmentNameEnum.ToString(), ex));
            }
            return _department;
        }

        public List<Department> GetAllDepartmentsForUser(int userID)
        {
            List<Department> _departmentlist;
            _departmentlist = new List<Department>();
            int _userid = userID;
            try
            {
                UserBusinessRelationshipBO bo = new UserBusinessRelationshipBO();
                var ret = bo.GetAllListByUserID(userID);
                foreach (var item in ret)
                {
                    _departmentlist.Add(GetInstance(item.BusinessRefId.Value));
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "An error occured while getting department instance: user:" + userID + " - " + ex.Message;

                var logging = Logger.LogMessageAsync(Enums.LoggingType.Exception, MethodBase.GetCurrentMethod(), errorMessage,
                    string.Empty, string.Empty, string.Empty);
                throw (new Exception("An error occured while getting department instance: user:" + userID, ex));
            }
            return _departmentlist;
        }

        public List<Department> BaseList
        {
            get
            {
                List<Department> val = LocalStorage<List<Department>>.GetValue("Department_List");
                if (val == null)
                {
                    val = (this as IModelCollectionCreater<Department, BusinessRefBE>).CreateInstance();
                    LocalStorage<List<Department>>.Add("Department_List", val);
                }
                return val;
            }
        }

        public bool RefreshList()
        {
            LocalStorage<List<Department>>.Delete("Department_List");
            return true;
        }

        List<Department> IModelCollectionCreater<Department, BusinessRefBE>.CreateInstance()
        {
            List<Department> val = new List<Department>();
            BusinessRefBO bo = new BusinessRefBO();
            List<BusinessRefBE> be = bo.GetAllList();
            val = new List<Department>();
            foreach (var item in be)
            {
                val.Add(((this as IModelCollectionCreater<Department, BusinessRefBE>).ConvertData(item)));
            }
            return val;
        }

        public List<BusinessRefBE> GetAllBusinessRefs()
        {
            BusinessRefBO bo = new BusinessRefBO();
            List<BusinessRefBE> be = bo.GetAllList();

            return be;
        }

        Department IModelCollectionCreater<Department, BusinessRefBE>.ConvertData(BusinessRefBE be)
        {
            Department ret = new Department();
            InjectBaseObjects(ret, be.BusinessRefId.Value, be.CreatedDate.Value, be.UpdatedDate.Value, be.CreatedByWkrId, be.UpdatedByWkrId);
            ret.DepartmentCd = be.BusinessName;
            ret.DepartmentName = be.BusinessShortDesc;
            ret.DepartmentEnum = (DepartmentNameEnums)be.EnumMappingNumber.Value;
            ret.DepartmentType = (DepartmentTypeInfo)new DepartmentTypeModelBO().GetInstance(be.BusinessTypeRefId.Value, "DepartmentTypeInfo");
            ret.DepartmentDivision = (DepartmentDivisionInfo)new DepartmentTypeModelBO().GetInstance(be.DisionRefId.Value, "DepartmentDivisionInfo");
            ret.DepartmentRegion = (DepartmentRegionInfo)new DepartmentTypeModelBO().GetInstance(be.RegionRefId.Value, "DepartmentRegionInfo");
            ret.DepartmentStatus = ProjectDisplayStatus.NewApplication;
            if (ret.DepartmentType == null)
                ret.DepartmentType = (DepartmentTypeInfo)new DepartmentTypeModelBO().GetInstance(-1, "DepartmentTypeInfo");
            if (ret.DepartmentDivision == null)
                ret.DepartmentDivision = (DepartmentDivisionInfo)new DepartmentTypeModelBO().GetInstance(-1, "DepartmentDivisionInfo");
            if (ret.DepartmentRegion == null)
                ret.DepartmentRegion = (DepartmentRegionInfo)new DepartmentTypeModelBO().GetInstance(-1, "DepartmentRegionInfo");
            ret.ExternalRefInfo = be.SourceSystemValueText;
            return ret;
        }
    }

    public interface IDepartmentBO : IModelCollectionCreater<Department, BusinessRefBE>
    {
        Department GetInstance(int departmentID);

        Department GetInstance(DepartmentTypeEnum departmentType, DepartmentDivisionEnum division, DepartmentRegionEnum region);

        Department GetInstance(DepartmentNameEnums departmentNameEnum);

    }


}
