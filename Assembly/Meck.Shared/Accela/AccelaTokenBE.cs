using System;

namespace Meck.Shared.Accela
{
    public class AccelaTokenBE
    {
        public string access_token { get; set; }

        public string token_type { get; set; }

        public int expires_in { get; set; }

        public string refresh_token { get; set; }

        public string scope { get; set; }

        public DateTime expiresAt { get; set; }

        public string  appid { get; set; }

        public AccelaTokenBE()
        {

        }
    }
}
