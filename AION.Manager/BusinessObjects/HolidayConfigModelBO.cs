using AION.Estimator.Engine.BusinessEntities;
using AION.Estimator.Engine.BusinessObjects;
using System;
using System.Collections.Generic;

namespace AION.BL.BusinessObjects
{
    public class HolidayConfigModelBO: IHolidayConfigBO
    {
        public int InsertHolidayConfig(HolidayConfig holidayConfig)
        {
            HolidayConfigBO bo = new HolidayConfigBO();
            HolidayConfigBE be = new HolidayConfigBE();
            be.HolidayDate = holidayConfig.HolidayDate;
            be.HolidayNm = holidayConfig.HolidayNm;
            be.HolidayAnnualRecurInd = holidayConfig.HolidayAnnualRecurInd;
            be.IsActive = holidayConfig.IsActive;
            int retValue = bo.Create(be);
            return retValue;
        }

        public List<HolidayConfig> GetHolidayConfigList()
        {
            List<HolidayConfig> ret = new List<HolidayConfig>();
            HolidayConfigBO bo = new HolidayConfigBO();
            List<HolidayConfigBE> be = bo.GetList();
            if (be == null || be.Count == 0)
                return new List<HolidayConfig>();
            foreach (var item in be)
            {
                HolidayConfig holidayConfig = new HolidayConfig();
                holidayConfig.HolidayConfigId = item.HolidayConfigId;
                holidayConfig.HolidayNm = item.HolidayNm;
                holidayConfig.HolidayDate = item.HolidayDate;
                holidayConfig.HolidayAnnualRecurInd = item.HolidayAnnualRecurInd;
                ret.Add(holidayConfig);
            }
            return ret;

        }

        public List<DateTime> GetHolidayConfigDateList()
        {
            List<DateTime> ret = new List<DateTime>();
            HolidayConfigBO bo = new HolidayConfigBO();
            List<DateTime> be = bo.GetDateList();
            if (be == null || be.Count == 0)
                return new List<DateTime>();
            foreach (var item in be)
            {
                ret.Add(item);
            }
            return ret;
        }

        public int InactivateHolidayConfig(IEnumerable<int> HolidayIds)
        {
            HolidayConfigBO bo = new HolidayConfigBO();
            int rows=0;
            foreach (var holidayId in HolidayIds)
            {
                bo.Inactivate(holidayId);
                rows++;
            }
           
            return rows;
        }


    }

    public interface IHolidayConfigBO
    {

     //   int InsertHolidayConfig(HolidayConfig holidayConfig);

        List<HolidayConfig> GetHolidayConfigList();
      
       
     
    }
}
