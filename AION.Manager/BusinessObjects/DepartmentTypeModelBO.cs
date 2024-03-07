using AION.Base;
using AION.BL.Adapters;
using AION.BL.Models.Base;
using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AION.BL.BusinessObjects
{
    public class DepartmentTypeModelBO: ModelBaseModelBO, IDepartmentTypeBaseBO
    {
  
        public List<DepartmentTypeBase> BaseList
        {
            get
            {
                List<DepartmentTypeBase> val = LocalStorage<List<DepartmentTypeBase>>.GetValue("DepartmentType_List");
                if (val == null)
                {
                    val = (this as IModelCollectionCreater<DepartmentTypeBase, BusinessTypeRefBE>).CreateInstance();
                    LocalStorage<List<DepartmentTypeBase>>.Add("DepartmentType_List", val);
                }
                return val;
            }
        }

        public bool RefreshList()
        {
            LocalStorage<List<DepartmentTypeBase>>.Delete("DepartmentType_List");
            return true;
        }

        public DepartmentTypeBase GetInstance(int enumMappingval,string returntype)
        {
            var t = BaseList.Where(x => x.EnumMappingVal == enumMappingval && x.GetType().Name == returntype).FirstOrDefault();
            return t;
        }

        public DepartmentTypeBase GetInstance(string accelaDepartmentRef)
        {
            var t = BaseList.Where(x => x.DepartmentTypeExternalRef == accelaDepartmentRef).FirstOrDefault();
            return t;
        }

        public DepartmentTypeInfo GetInstance(DepartmentTypeEnum DeptTypeID)
        {
            DepartmentTypeInfo t = (DepartmentTypeInfo)BaseList.Where(x => x.GetType().Name == "DepartmentTypeInfo" 
                                        && ((DepartmentTypeInfo)x).DepartmentType == DeptTypeID).FirstOrDefault();
            t.DepartmentType = DeptTypeID;
            return t;
        }

        public DepartmentDivisionInfo GetInstance(DepartmentDivisionEnum deptDivisionID)
        {
            DepartmentDivisionInfo t = (DepartmentDivisionInfo)BaseList.Where(x => x.GetType().Name == "DepartmentDivisionInfo"
                                        &&  ((DepartmentDivisionInfo)x).DepartmentDivision == deptDivisionID).FirstOrDefault();

            t.DepartmentDivision = deptDivisionID;
            return (DepartmentDivisionInfo)t;
        }

        public DepartmentRegionInfo GetInstance(DepartmentRegionEnum deptRegionID)
        {
            DepartmentRegionInfo t = (DepartmentRegionInfo)BaseList.Where(x => x.GetType().Name == "DepartmentRegionInfo"
                                        && ((DepartmentRegionInfo)x).DepartmentRegion == deptRegionID).FirstOrDefault();
            t.DepartmentRegion = deptRegionID;
            return t;
        }

        List<DepartmentTypeBase> IModelCollectionCreater<DepartmentTypeBase, BusinessTypeRefBE>.CreateInstance()
        {
            List<DepartmentTypeBase> val = new List<DepartmentTypeBase>();
            BusinessTypeRefBO bo = new BusinessTypeRefBO();
            List<BusinessTypeRefBE> be = bo.GetAllList();
            foreach (var item in be)
            {
                if(item.BusinessRef_EnumMappingValNbr.Value == -1)
                {
                    //cannot convert this type implicitly since it need to be 1 each for each type. so do it manualy
                    CreateNADefaults(item, val);
                }
                else
                    val.Add(((this as IModelCollectionCreater<DepartmentTypeBase, BusinessTypeRefBE>).ConvertData(item)));
            }
            return val;
        }

        bool CreateNADefaults(BusinessTypeRefBE be, List<DepartmentTypeBase> collection)
        {
            DepartmentTypeBase objType, objDiv, objReg;

            DepartmentTypeInfo depttype = new DepartmentTypeInfo();
            depttype.DepartmentType = (DepartmentTypeEnum)be.BusinessRef_EnumMappingValNbr.Value;
            objType = depttype;
            base.InjectBaseObjects(objType, be.BusinessTypeRefId.Value, be.CreatedDate.Value, be.UpdatedDate.Value, be.CreatedByWkrId, be.UpdatedByWkrId);
            objType.DepartmentTypeCode = be.BusinessRefTypeShortDesc;
            objType.DepartmentTypeExternalRef = be.BusinessRef_SrcSystemValueText;
            objType.DepartmentTypeName = be.BusinessRefDisplayName;
            objType.EnumMappingVal = -1;
            objType.ExternalSystem = new ExternalSystemModelBO().GetInstance(be.ExternalSystemRefId.Value);
            collection.Add(objType);

            DepartmentDivisionInfo deptdiv = new DepartmentDivisionInfo();
            deptdiv.DepartmentDivision = (DepartmentDivisionEnum)be.BusinessRef_EnumMappingValNbr.Value;
            objDiv = deptdiv;
            base.InjectBaseObjects(objDiv, be.BusinessTypeRefId.Value, be.CreatedDate.Value, be.UpdatedDate.Value, be.CreatedByWkrId, be.UpdatedByWkrId);
            objDiv.DepartmentTypeCode = be.BusinessRefTypeShortDesc;
            objDiv.DepartmentTypeExternalRef = be.BusinessRef_SrcSystemValueText;
            objDiv.DepartmentTypeName = be.BusinessRefDisplayName;
            objDiv.EnumMappingVal = -1;
            objDiv.ExternalSystem = new ExternalSystemModelBO().GetInstance(be.ExternalSystemRefId.Value);
            collection.Add(objDiv);

            DepartmentRegionInfo deptreg = new DepartmentRegionInfo();
            deptreg.DepartmentRegion = (DepartmentRegionEnum)be.BusinessRef_EnumMappingValNbr.Value;
            objReg = deptreg;
            base.InjectBaseObjects(objReg, be.BusinessTypeRefId.Value, be.CreatedDate.Value, be.UpdatedDate.Value, be.CreatedByWkrId, be.UpdatedByWkrId);
            objReg.DepartmentTypeCode = be.BusinessRefTypeShortDesc;
            objReg.DepartmentTypeExternalRef = be.BusinessRef_SrcSystemValueText;
            objReg.DepartmentTypeName = be.BusinessRefDisplayName;
            objReg.EnumMappingVal = -1;
            objReg.ExternalSystem = new ExternalSystemModelBO().GetInstance(be.ExternalSystemRefId.Value);
            collection.Add(objReg);

            return true;
        }

        DepartmentTypeBase IModelCollectionCreater<DepartmentTypeBase, BusinessTypeRefBE>.ConvertData(BusinessTypeRefBE be)
        {
            DepartmentTypeBase ret;
            /*
             * All the department types are stored in one single class for making it flattend table design. this will help the extensibility of departments in future.
             * each department is mapped to its Enum values 
             */
            if (Enum.IsDefined(typeof(DepartmentTypeEnum), be.BusinessRef_EnumMappingValNbr.Value)) // DepartmentTypeEnum . This need to be directly mapped to table column value.
            {
                DepartmentTypeInfo depttype = new DepartmentTypeInfo();
                depttype.DepartmentType = (DepartmentTypeEnum)be.BusinessRef_EnumMappingValNbr.Value;
                ret = depttype;
            }
            else if (Enum.IsDefined(typeof(DepartmentDivisionEnum), be.BusinessRef_EnumMappingValNbr.Value)) // DepartmentDivisionEnum . This need to be directly mapped to table column value.
            {
                DepartmentDivisionInfo division = new DepartmentDivisionInfo();
                division.DepartmentDivision = (DepartmentDivisionEnum)be.BusinessRef_EnumMappingValNbr.Value;
                ret = division;
            }
            else if (Enum.IsDefined(typeof(DepartmentRegionEnum), be.BusinessRef_EnumMappingValNbr.Value))  // DepartmentRegionInfo . This need to be directly mapped to table column value.
            {
                DepartmentRegionInfo region = new DepartmentRegionInfo();
                region.DepartmentRegion =  (DepartmentRegionEnum)be.BusinessRef_EnumMappingValNbr.Value;
                ret = region;
            }
            else
                throw new Exception("Unexpected Department Type Detected. Contact your IT Adminstrator for further assistance.");

            base.InjectBaseObjects(ret, be.BusinessTypeRefId.Value, be.CreatedDate.Value, be.UpdatedDate.Value, be.CreatedByWkrId, be.UpdatedByWkrId);
            ret.DepartmentTypeCode = be.BusinessRefTypeShortDesc;
            ret.DepartmentTypeExternalRef = be.BusinessRef_SrcSystemValueText;
            ret.DepartmentTypeName = be.BusinessRefDisplayName;
            ret.ExternalSystem = new ExternalSystemModelBO().GetInstance(be.ExternalSystemRefId.Value);
            ret.EnumMappingVal = be.BusinessRef_EnumMappingValNbr.Value;
            return ret;
        }
    }

    public interface IDepartmentTypeBaseBO : IModelCollectionCreater<DepartmentTypeBase, BusinessTypeRefBE>
    {
        DepartmentTypeBase GetInstance(int ID,string returntype);

        DepartmentTypeInfo GetInstance(DepartmentTypeEnum value);

        DepartmentDivisionInfo GetInstance(DepartmentDivisionEnum value);

        DepartmentRegionInfo GetInstance(DepartmentRegionEnum value);
       
    }


}
