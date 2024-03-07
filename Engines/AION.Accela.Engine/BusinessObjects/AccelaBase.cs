using System;
using AION.Base;
using Meck.Shared.Accela;
using System.Collections.Generic;
using System.Configuration;

namespace AION.Accela.Engine.BusinessObjects
{

    public class AccelaBase : BaseBO
    {
        #region private variables and constants  #region Public Varibles

        public string AccelaUserId = string.Empty;
        public string AccelaPassword = string.Empty;

        public string _mAccelaToken;

        public int MLastHttpStatus;

        public string _mAccelaUrl;
        public string mAccelaScope;


        public AccelaTokenBE _mAccelaTokenBE;

        public string _mAccelaClientId;

        // <summary>Body contains a URL-encoded query string as per RFC 1867</summary>

        public const string ApplicationWwwFormUrlEncoded = "application/x-www-form-urlencoded";

        public const string AccelaContentHeaderEncoding = "application/x-www-form-urlencoded";

        public Dictionary<string, string> _thisheader = new Dictionary<string, string>();

        #endregion

        public AccelaBase()
        {
            _mAccelaUrl = Meck.Shared.Globals.AccelaAuthBaseUrl;

            _mAccelaTokenBE = Meck.Shared.Globals.AccelaToken;

            _mAccelaClientId = Meck.Shared.Globals.AccelaClientId;

            mAccelaScope = Meck.Shared.Globals.AccelaScope;
            if (!String.IsNullOrEmpty(mAccelaScope))
            {
                if (!mAccelaScope.Contains("run_emse_script"))
                {
                    if (mAccelaScope.Contains((",")))
                    {
                        if (mAccelaScope.Trim().EndsWith(","))
                        {
                            mAccelaScope = mAccelaScope + " run_emse_script,";
                        }
                        else
                        {
                            mAccelaScope = mAccelaScope + ", run_emse_script";
                        }
                    }
                    else
                    {
                        mAccelaScope = mAccelaScope + " run_emse_script ";
                    }
                }
            }

        }

        public string GetConfigValue(string settingname)
        {
            return ConfigurationManager.AppSettings[settingname];
        }
    }
}
