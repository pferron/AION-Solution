using AION.BL;

namespace AION.Manager.Models.User
{
    public class UserJurisdictionXRef : ModelBase
    {
        public int? UserJurisdictionXRefId { get; set; }

        public int? UserId { get; set; }

        public int? JurisdictionRefId { get; set; }

        public JurisdictionEnum JurisdictionEnum { get; set; }
    }
}