using Meck.Shared.MeckDataMapping;
using Meck.Shared.PosseToAccela;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AION.Manager.AccelaBusinessObjects
{
    public class ProfessionalDetailBO : AccelaBusinessObjectBase
    {
        public professionalDetail ParserRecordProfessional(Dictionary<string, object> professionalDetailObject, List<MeckAccelaDataMap> mPosseProfessionals, bool emulateTestData = false)
        {
            professionalDetail mProfessional = new professionalDetail();
            System.Type t = mProfessional.GetType();

            PropertyInfo[] professionalDetailProps = t.GetProperties();

            List<MeckAccelaDataMap> mMapFields =
                mPosseProfessionals.FindAll(x => x.ACCELA_OBJ_TYP_DESC.ToUpper() == "PROFESSIONALS" && (!String.IsNullOrEmpty(x.ACCELA_FIELD_NM)));

            foreach (var mapField in mMapFields)
            {

                string mapkey;

                if (String.IsNullOrWhiteSpace(mapField.ACCELA_LOOKUP_FIELD_NM))
                {
                    mapkey = mapField.ACCELA_FIELD_NM;
                }
                else
                {
                    mapkey = mapField.ACCELA_LOOKUP_FIELD_NM;
                }

                if (professionalDetailObject.ContainsKey(mapkey))
                {
                    object professionalValue;
                    professionalDetailObject.TryGetValue(mapkey, out professionalValue);
                    //   1st level dictionary map

                    if (professionalValue.GetType().Name.Contains("Dictionary"))
                    {
                        object professionalValueDictObj = professionalValue;
                        Dictionary<string, Object> mtrec = (Dictionary<string, object>)professionalValueDictObj;
                        //  Object contactSubObject = null; // used for dicionary values

                        mtrec.TryGetValue(mapField.ACCELA_FIELD_NM, out professionalValueDictObj);
                        if (mapField.ACCELA_LOOKUP_FIELD_NM != string.Empty)
                        {
                            // code for map name = "state" which is sublayer dictionary.
                            // if (mapField.ACCELA_LOOKUP_FIELD_NM == "address" || mapField.ACCELA_LOOKUP_VAL_DESC == "type")
                            // {
                            switch (mapField.ACCELA_LOOKUP_FIELD_NM)
                            {
                                case "state":
                                case "licenseType":
                                    //  is a single level dictionary
                                    if (professionalValue.GetType().Name.Contains("Dictionary"))
                                    {
                                        mProfessional = LoadValueToProfessionalDetail(mapField, mProfessional, professionalDetailProps, professionalValueDictObj);
                                        //object mContactTypeValue;
                                        // Dictionary<string, Object> mContactValueType = (Dictionary<string, object>)contactValueDictObj;

                                        // mContactValueType.TryGetValue("value", out mContactTypeValue);
                                    }
                                    break;
                                default:
                                    mProfessional = LoadValueToProfessionalDetail(mapField, mProfessional, professionalDetailProps, professionalValueDictObj);
                                    break;
                            }
                        }
                    }
                    else
                    {
                        mProfessional = LoadValueToProfessionalDetail(mapField, mProfessional, professionalDetailProps, professionalValue);
                    }
                }
            }
            return mProfessional;
        }


        // used for Commercial RTAPS only 
        public ProfessionalDetail ParserRecordDisplayProfessional(Dictionary<string, object> professionalDetailObject)
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


        public static string ConvertToCSV(List<ProfessionalDetail> professionals, string objectseparator)
        {
            if (professionals == null)
                return "";

            var ret = professionals.Select(x => x.To_String() + objectseparator).ToList();
            StringBuilder sb = new StringBuilder();
            foreach (string val in ret)
            {
                sb.Append(val);
            }

            return sb.ToString();
        }

        public static List<ProfessionalDetail> ConvertCSVToList(string professionals, char objectseparator)
        {
            List<ProfessionalDetail> professionalsList = new List<ProfessionalDetail>();
            if (string.IsNullOrWhiteSpace(professionals))
                return professionalsList;

            string[] inputList = professionals.Split(objectseparator);

            foreach (string professional in inputList)
            {
                professionalsList.Add(BuildProfessionalDetail(professional));
            }

            return professionalsList;
        }

        public static ProfessionalDetail BuildProfessionalDetail(string input)
        {
            ProfessionalDetail obj = new ProfessionalDetail();

            if (string.IsNullOrWhiteSpace(input)) return obj;

            string[] inputList = input.Split(';');

            foreach (string task in inputList)
            {
                string[] keypair = task.Split('|');

                switch (keypair[0].Trim())
                {
                    case "id":
                        obj.id = keypair[1];
                        break;
                    case "fullName":
                        obj.fullName = keypair[1];
                        break;
                    case "addressLine1":
                        obj.addressLine1 = keypair[1];
                        break;
                    case "isPrimary":
                        obj.isPrimary = keypair[1];
                        break;
                    case "referenceLicenseId":
                        obj.referenceLicenseId = keypair[1];
                        break;
                    case "phone2":
                        obj.phone2 = keypair[1];
                        break;
                    case "updateOnUI":
                        obj.updateOnUI = keypair[1];
                        break;
                    case "licenseNumber":
                        obj.licenseNumber = keypair[1];
                        break;
                    case "lastName":
                        obj.lastName = keypair[1];
                        break;
                    case "city":
                        obj.city = keypair[1];
                        break;
                    case "email":
                        obj.email = keypair[1];
                        break;
                    case "postalCode":
                        obj.postalCode = keypair[1];
                        break;
                    case "LicenseType":
                        obj.licenseType = new LicenseType(keypair[1], keypair[1]);
                        break;
                    case "State":
                        obj.state = new State(keypair[1], keypair[1]);
                        break;
                    //case "DrawingSealed":
                    default:
                        break;
                }
            }

            return obj;
        }

    }
}