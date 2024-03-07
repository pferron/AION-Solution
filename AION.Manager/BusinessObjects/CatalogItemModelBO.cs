using AION.Engine.BusinessEntities;
using AION.Engine.BusinessObjects;
using System.Collections.Generic;
using System.Linq;

namespace AION.BL.BusinessObjects
{
    public class CatalogItemModelBO : ModelBaseModelBO, ICatalogItemModelBO
    {
        public List<CatalogItem> GetInstance(string catalogGroupExternalRef)
        {
            List<CatalogItem> ret = new List<CatalogItem>();
            CatalogRefBO cataloggrp = new CatalogRefBO();
            List<CatalogRefBE> beObj = cataloggrp.GetByExternalRef(catalogGroupExternalRef);
            foreach (var item in beObj)
            {
                ret.Add(ConvertBEtoModelObject(item, catalogGroupExternalRef));
            }
            return ret;
        }

        public List<CatalogItem> GetInstance(string catalogKey, string catalogSubKey)
        {
            List<CatalogItem> ret = new List<CatalogItem>();
            CatalogRefBO cataloggrp = new CatalogRefBO();
            //List<CatalogRefBE> beObj = cataloggrp.GetByExternalRef(catalogGroupExternalRef);
            //foreach (var item in beObj)
            //{
            //    ret.Add(ConvertBEtoModelObject(item, catalogGroupExternalRef));
            //}
            return ret;
        }

        private CatalogItem ConvertBEtoModelObject(CatalogRefBE item, string catalogGroupExternalRef)
        {
            CatalogItem ret = new CatalogItem();
            ret.CatalogGroupRefName = catalogGroupExternalRef;
            ret.CatalogGroupRefID = item.CatalogGroupRefID;
            ret.CreatedDate = item.CreatedDate.Value;
            ret.CreatedUser = new UserIdentityModelBO().GetInstance(int.Parse(item.CreatedByWkrId));
            ret.UpdatedDate = item.UpdatedDate.Value;
            ret.UpdatedUser = new UserIdentityModelBO().GetInstance(int.Parse(item.UpdatedByWkrId));
            ret.Value = item.Value;
            ret.Key = item.Key;
            ret.SubKey = item.SubKey;
            ret.ID = item.ID.Value;
            return ret;
        }

        public CatalogItem GetInstance(int ID)
        {
            return new CatalogItem();
        }

        public bool UpdateInstance(CatalogItem data)
        {
            CatalogRefBO bo = new CatalogRefBO();
            CatalogRefBE be = new CatalogRefBE();
            be.CatalogGroupRefID = data.CatalogGroupRefID;
            be.ID = data.ID;
            be.Key = data.Key;
            be.SubKey = data.SubKey;
            be.Value = data.Value;
            be.UpdatedByWkrId = data.UpdatedUser == null ? "1" : data.UpdatedUser.ID.ToString();
            be.UserId = be.UpdatedByWkrId = data.UpdatedUser == null ? "1" : data.UpdatedUser.ID.ToString();
            be.UpdatedDate = data.UpdatedDate;
            bo.Update(be);
            return true;
        }

        public static decimal? GetCancellationFeePerHour()
        {
            CatalogItemModelBO bo = new CatalogItemModelBO();
            List<CatalogItem> catalogItems = bo.GetInstance("ADMIN.MISC_CONFIG.CANCELLATION_FEE_PER_HOUR");
            string catalogValue = catalogItems.Where(x => x.Key == "ADMIN.MISC_CONFIG.CANCELLATION_FEE_PER_HOUR").FirstOrDefault().Value;
            if (string.IsNullOrWhiteSpace(catalogValue) == false)
            {
                decimal cancellationFee = 0;

                bool isDecimal = decimal.TryParse(catalogValue, out cancellationFee);

                if (isDecimal) return cancellationFee;
            }
            return 0;
        }

    }

    public interface ICatalogItemModelBO
    {
        List<CatalogItem> GetInstance(string catalogGroupExternalRef);

        CatalogItem GetInstance(int ID);
    }
}
