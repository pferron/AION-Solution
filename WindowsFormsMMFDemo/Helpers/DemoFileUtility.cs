using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.HtmlControls;
using AION.Accela.Engine;
using Meck.Azure;

namespace DemoInterface.Helpers
{
   public class DemoFileUtility
    {


        public void SetupAccelaEngineTests()
        {
            Meck.Shared.Globals.AccelaAuthBaseUrl = GetConfigValue("AccellaAuthbaseUrl");
            Meck.Shared.Globals.AccelaAgency = GetConfigValue("AccelaAuthAgency");
            Meck.Shared.Globals.AccelaEnvironment = GetConfigValue("AccelaEnvironment");
            Meck.Shared.Globals.AccelaClientId = KeyVaultUtility.GetSecret("AccelaClientID");
            Meck.Shared.Globals.AccelaScope = GetConfigValue("AccelaScope");

            Meck.Shared.Globals.AccelaClientSecret = KeyVaultUtility.GetSecret("AccelaClientSecret");
            Meck.Shared.Globals.AccelaUser = KeyVaultUtility.GetSecret("AccelaUserID");
            Meck.Shared.Globals.AccelaUserName = KeyVaultUtility.GetSecret("AccelaUserID");
            Meck.Shared.Globals.AccelaPassword = KeyVaultUtility.GetSecret("AccelaPassword");
            ;
        }
        private string GetConfigValue(string settingname)
        {
            return ConfigurationManager.AppSettings[settingname];

        }



        public void WriteFileWithDupeCheck(string folderPath, string customFileName, string fileDetails, bool checkDupes)
        {
            // now get the Custom tables data 
            ///   string recOutFolderPath = Path.GetTempPath() + folderPath;

            Directory.CreateDirectory(folderPath);

            string recOutFolderPathFileName = folderPath + customFileName;
            if (checkDupes)
            {
                if (File.Exists(recOutFolderPathFileName))
                {
                    File.Delete(recOutFolderPathFileName);
                }
            }

            StreamWriter recCustTablesfsw = new StreamWriter(recOutFolderPathFileName, false);
            recCustTablesfsw.Write(fileDetails);
            recCustTablesfsw.Flush();
            recCustTablesfsw.Close();

        }


    }
}
