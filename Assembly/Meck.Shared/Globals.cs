using System;

namespace Meck.Shared
{
    public class Globals
    {
        public static Meck.Shared.Accela.AccelaTokenBE AccelaToken { get; set; }

        public static string AccelaUser { get; set; }

        public static string AccelaClientSecret { get; set; }  //= KeyVaultUtility.GetSecret("AccelaClientSecret"),
        public static string AccelaUserName { get; set; }  //= KeyVaultUtility.GetSecret("AccelaUserID"),
        public static string AccelaPassword { get; set; }  //=   KeyVaultUtility.GetSecret("AccelaPassword")

        public static string AccelaAuthBaseUrl { get; set; }  // = GetConfigValue("AccellaAuthbaseUrl"),
        public static string AccelaAgency { get; set; }  // = GetConfigValue("AccelaAuthAgency"),
        public static string AccelaEnvironment { get; set; } //= GetConfigValue("AccelaEnvironment"),
        public static string AccelaClientId { get; set; } // = GetConfigValue("AccelaClientID"),

        public static string AccelaScope { get; set; }   // = GetConfigValue("AccelaScope"),

         public static string AIONConnectionString {get;set;}  // AION database connection 
          

    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class OrderAttribute : Attribute
    {
        public OrderAttribute(int position)
        {
            this.Position = position;
        }

        public int Position { get; private set; }

    }



}