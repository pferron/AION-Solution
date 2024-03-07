using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AION.Accela.Engine;
using AION.Accela.Engine.BusinessObjects;
using Meck.Azure;
using Meck.Shared.Accela.ParserModels;
using Meck.Shared.MeckDataMapping;
using Meck.Shared.PosseToAccela;
using Newtonsoft.Json;

namespace DemoInterface.Adapters
{
   public class SampleParsing
    {

        public SampleParsing()
        {
            Meck.Shared.Globals.AccelaAuthBaseUrl = GetConfigValue("AccelaAuthbaseUrl");
            Meck.Shared.Globals.AccelaAgency = GetConfigValue("AccelaAuthAgency");
            Meck.Shared.Globals.AccelaEnvironment = GetConfigValue("AccelaEnvironment");
            Meck.Shared.Globals.AccelaClientId = KeyVaultUtility.GetSecret("AccelaClientID");
            Meck.Shared.Globals.AccelaScope = GetConfigValue("AccelaScope");
            Meck.Shared.Globals.AIONConnectionString = KeyVaultUtility.GetSecret("KeyVaultAIONConnectionstring");
            Meck.Shared.Globals.AccelaClientSecret = KeyVaultUtility.GetSecret("AccelaClientSecret");
            Meck.Shared.Globals.AccelaUserName = KeyVaultUtility.GetSecret("AccelaUserID");
            Meck.Shared.Globals.AccelaPassword = KeyVaultUtility.GetSecret("AccelaPassword");

            //   string PosseDevTest = KeyVaultUtility.GetSecret("KeyVaultAIONDevConnectionString");


            //  _moqAccelaEngine = new Mock<IAccelaEngine>();
        }



        IAccelaEngine mAccelaEngine = new AccelaApiBO();

        private List<ProfessionalDetail> mProfess = new List<ProfessionalDetail>();

        private string GetConfigValue(string settingname)
        {
            return ConfigurationManager.AppSettings[settingname];
        }


        public List<ProfessionalDetail>  GetProfessionDestailobject(string recordid)
        {
            IAccelaEngine mAccelaEngine = new AccelaApiBO();


            
            var result = GetTestRecord(recordid);

            foreach (var recresult in result.result)
            {

                foreach (var profs in recresult.professionals)
                {
                    var parseresult = ParserRecordDisplayProfessional(profs, null, false); 
                    
                    mProfess.Add(parseresult);
                }
                 
            }
            return mProfess; 
        }



        public AccelaRecordModel GetTestRecord(string recordId)
        {

             
            var result = mAccelaEngine.GetAccelaRecord(recordId);
            var recInfo = result.Result;
            return recInfo; 
        }
        

        public ProfessionalDetail ParserRecordDisplayProfessional(Dictionary<string, object> professionalDetailObject, List<MeckAccelaDataMap> mPosseProfessionals, bool emulateTestData = false)
        {   
               ProfessionalDetail mProfessional = new ProfessionalDetail();
               
               var details = JsonConvert.SerializeObject(professionalDetailObject);
            
                var tdetails = JsonConvert.DeserializeObject<ProfessionalDetail>(details);
                   mProfessional = tdetails; 
            
            return mProfessional;
        }

        public professionalDetail LoadValueToProfessionalDetail(MeckAccelaDataMap mapField, professionalDetail mProfessional, PropertyInfo[] professionalDetailProps, object professionalValue, bool emulateTestData = false)
        {
            for (int indx = 0; indx < professionalDetailProps.Length; indx++)
            {
                if (professionalDetailProps[indx].Name == mapField.Meck_FIELD_NM.Trim())
                {
                    PropertyInfo professionalDetailProp = professionalDetailProps[indx];

                    string professionalValueType = mapField.Meck_DATA_TYP_DESC.ToUpper();

                    string[] professionalValueTypeSplit = professionalValueType.Split('(');

                    switch (professionalValueTypeSplit[0])
                    {
                        case "STRING":

                            if (emulateTestData)
                            {
                                professionalDetailProp.SetValue(mProfessional, "emulatedTestData-Type");
                            }
                            else
                            {

                                professionalDetailProp.SetValue(mProfessional, (string)professionalValue);
                            }

                            break;

                        case "DATETIME":
                            if (emulateTestData)
                            {
                                professionalDetailProp.SetValue(mProfessional, DateTime.Now.AddHours(-12));
                            }
                            else
                            {
                                professionalDetailProp.SetValue(mProfessional, (DateTime)professionalValue);
                            }

                            break;

                        case "DECIMAL":
                            if (emulateTestData)
                            {
                                professionalDetailProp.SetValue(mProfessional, 9999.99m);
                            }
                            else
                            {
                                professionalDetailProp.SetValue(mProfessional, (decimal)professionalValue);
                            }

                            break;

                        case "NUMBER":
                            if (professionalValueType.Contains("(") && professionalValueType.Contains(")"))
                            {
                                if (emulateTestData)
                                {
                                    professionalDetailProp.SetValue(mProfessional, 999999.99m);
                                }
                                else
                                {
                                    professionalDetailProp.SetValue(mProfessional, (decimal)professionalValue);
                                }
                            }
                            else
                            {
                                if (emulateTestData)
                                {
                                    professionalDetailProp.SetValue(mProfessional, 999999);
                                }
                                else
                                {

                                    professionalDetailProp.SetValue(mProfessional, (Int32)professionalValue);
                                }
                            }
                            break;
                        default:

                            // string errorTrap;

                            break;
                    }

                    break;
                }
            }
            return mProfessional;
        }


    }
}
