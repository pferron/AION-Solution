using Newtonsoft.Json;

namespace Meck.Shared.Accela
{
    public class ProfLicenseDocSignQuery
    {
        public string b1_per_id1;
        public string b1_per_id2;
        public string b1_per_id3;
        public string lic_nbr;

        public  ProfLicenseDocSignQuery(string part1, string part2, string part3, string licnum)
        {
            b1_per_id1 = part1;
            b1_per_id2 = part2;
            b1_per_id3 = part3;
            lic_nbr = licnum;

        }

        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}

