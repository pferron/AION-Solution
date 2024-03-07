using Meck.Shared.MeckDataMapping;
using Meck.Shared.PosseToAccela;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using System.Threading.Tasks;
using AION.Accela.Engine.BusinessObjects;
using Meck.Shared.Accela.ParserModels;
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace AION.Manager.AccelaBusinessObjects
{
    public class ContactDetailBO : AccelaBusinessObjectBase
    {
        public contactDetail ParserRecordContact(string recordId, Dictionary<string, object> contactDetailObject, List<MeckAccelaDataMap> mPosseContacts, bool emulateTestData = false)
        {
            contactDetail mContact = new contactDetail();
            System.Type t = mContact.GetType();

            PropertyInfo[] contactDetailProps = t.GetProperties();

            List<MeckAccelaDataMap> mMapFields =
                mPosseContacts.FindAll(x => x.ACCELA_OBJ_TYP_DESC == "Contacts" && (!String.IsNullOrEmpty(x.ACCELA_FIELD_NM)));

            object IdObject; 
          if( contactDetailObject.TryGetValue("id", out IdObject))
          {
              mContact.id = Convert.ToInt32(IdObject.ToString());
          }

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

                if (contactDetailObject.ContainsKey(mapkey))
                {
                    object contactValue;
                    contactDetailObject.TryGetValue(mapkey, out contactValue);
                    //   1st level dictionary map

                    if (contactValue.GetType().Name.Contains("Dictionary"))
                    {
                        object contactValueDictObj = contactValue;
                        Dictionary<string, Object> mtrec = (Dictionary<string, object>)contactValueDictObj;
                        //  Object contactSubObject = null; // used for dicionary values

                        mtrec.TryGetValue(mapField.ACCELA_FIELD_NM, out contactValueDictObj);
                        if (mapField.ACCELA_LOOKUP_FIELD_NM != string.Empty)
                        {
                            // code for map name = "state" which is sublayer dictionary.
                            // if (mapField.ACCELA_LOOKUP_FIELD_NM == "address" || mapField.ACCELA_LOOKUP_VAL_DESC == "type")
                            // {
                            switch (mapField.ACCELA_LOOKUP_FIELD_NM)
                            {
                                case "address":
                                    object contactValueAddress;

                                    contactDetailObject.TryGetValue(mapField.ACCELA_LOOKUP_FIELD_NM, out contactValueAddress);

                                    if (contactValueAddress.GetType().Name.Contains("Dictionary"))
                                    {
                                        Dictionary<string, object> mAddressValueAddress = (Dictionary<string, object>)contactValueAddress;

                                        object mValueAddress;

                                        mAddressValueAddress.TryGetValue(mapField.ACCELA_FIELD_NM, out mValueAddress);

                                        if (contactValueAddress.GetType().Name.Contains("Dictionary"))
                                        {
                                            Dictionary<string, object> mAddressValueAddressDict = (Dictionary<string, object>)contactValueAddress;

                                            object mValueAddressDict;

                                            mAddressValueAddressDict.TryGetValue(mapField.ACCELA_FIELD_NM, out mValueAddressDict);

                                            if (mValueAddressDict is null)
                                            {
                                                break;
                                            }

                                            if (mapField.ACCELA_FIELD_NM == "state")
                                            {
                                                Dictionary<string, object> mValueAddressList = (Dictionary<string, object>)mValueAddressDict;

                                                object mValueAddressDictValues;
                                                mValueAddressList.TryGetValue("value", out mValueAddressDictValues);

                                                if (mValueAddressDictValues != null)
                                                {
                                                    mContact = LoadValueToContactDetail(mapField, mContact, contactDetailProps, mValueAddressDictValues);
                                                }
                                            }
                                            else
                                            {
                                                mContact = LoadValueToContactDetail(mapField, mContact, contactDetailProps, mValueAddressDict);
                                            }
                                        }
                                        else
                                        {
                                            mContact = LoadValueToContactDetail(mapField, mContact, contactDetailProps, mValueAddress);
                                        }
                                    }
                                    else
                                    {
                                        mContact = LoadValueToContactDetail(mapField, mContact, contactDetailProps, contactValueAddress);
                                    }
                                    break;
                                case "type":
                                    //  is a single level dictionary
                                    if (contactValue.GetType().Name.Contains("Dictionary"))
                                    {
                                        mContact = LoadValueToContactDetail(mapField, mContact, contactDetailProps, contactValueDictObj);
                                        //object mContactTypeValue;
                                        // Dictionary<string, Object> mContactValueType = (Dictionary<string, object>)contactValueDictObj;

                                        // mContactValueType.TryGetValue("value", out mContactTypeValue);
                                    }
                                    break;
                                default:
                                    mContact = LoadValueToContactDetail(mapField, mContact, contactDetailProps, contactValueDictObj);
                                    break;
                            }
                        }

                    }
                    else
                    {
                        mContact = LoadValueToContactDetail(mapField, mContact, contactDetailProps, contactValue);





                    }
                }
            }

            mContact = GetContactEmailControl(recordId, mContact);

            return mContact;
        }

        public contactDetail GetContactEmailControl(string recordId, contactDetail currentContact)
        {
            contactDetail cCurrentContact = currentContact;

            var result = Task.Run(() => new AccelaApiBO().GetContactCustomForm(recordId, currentContact.id));
            ContactCustomFormBO mCustFormBo = new ContactCustomFormBO();


            var mDictCustForm = JsonConvert.DeserializeObject<Dictionary<string, object>>(result.Result);

            //  added 
            var jss = new JavaScriptSerializer();
            var dict = jss.Deserialize<Dictionary<string, object>>(result.Result);

            System.Type t = mCustFormBo.GetType();
            PropertyInfo[] mCustFromprops = t.GetProperties();

            // Teh object coming back from the GetContctCustomFom will have
            // result as list of values even though the result is as singleton being returned at this time. 

            foreach (var valueset in dict)
            {
                // status plus result array 
                if (valueset.Key == "result")
                {
                    // break down result to dictionary 
                    object custformdetail = string.Empty;

                    // get the new dictionary  object 
                    dict.TryGetValue(valueset.Key, out custformdetail);

                    ArrayList mtarray = (ArrayList)custformdetail;

                    for (int indx = 0; indx < mtarray.Count; indx++)
                    {
                        Dictionary<string, object> mtdetail = (Dictionary<string, object>)mtarray[indx];

                        foreach (var detail in mtdetail)
                        {
                            Console.WriteLine();
                            object thisValue = string.Empty;

                            if (detail.Key != "id")
                            {
                                switch (detail.Key)
                                {
                                    case "Notify":
                                        if (mtdetail.TryGetValue(detail.Key, out thisValue))
                                        {
                                            cCurrentContact.Notify = thisValue.ToString();
                                        }
                                        break;

                                    case "RequestorAssociation":
                                        if (mtdetail.TryGetValue(detail.Key, out thisValue))
                                        {
                                            cCurrentContact.RequestorAssociation = thisValue.ToString();
                                        }

                                        break;
                                    case "RequestorAssociationOther":
                                        if (mtdetail.TryGetValue(detail.Key, out thisValue))
                                        {
                                            cCurrentContact.RequestorAssociationOther = thisValue.ToString();
                                        }

                                        break;
                                    case "Grade":
                                        if (mtdetail.TryGetValue(detail.Key, out thisValue))
                                        {
                                            cCurrentContact.Grade = thisValue.ToString();
                                        }

                                        break;
                                }
                            }

                        }
                    }
                }
            }

            return cCurrentContact;
        }



        /// <summary>
        ///
        /// </summary>
        /// <param name="mapField"></param>
        /// <param name="mContact"></param>
        /// <param name="contactDetailProps"></param>
        /// <param name="contactValue"></param>
        /// <returns></returns>
        public contactDetail LoadValueToContactDetail(MeckAccelaDataMap mapField, contactDetail mContact, PropertyInfo[] contactDetailProps, object contactValue, bool emulateTestData = false)
        {
            for (int indx = 0; indx < contactDetailProps.Length; indx++)
            {
                if (contactDetailProps[indx].Name == mapField.Meck_FIELD_NM.Trim())
                {
                    PropertyInfo contactDetailProp = contactDetailProps[indx];

                    string contactValueType = mapField.Meck_DATA_TYP_DESC.ToUpper();

                    string[] contactValueTypeSplit = contactValueType.Split('(');

                    switch (contactValueTypeSplit[0])
                    {
                        case "STRING":

                            if (emulateTestData)
                            {
                                contactDetailProp.SetValue(mContact, "emulatedTestData-Type");
                            }
                            else
                            {

                                contactDetailProp.SetValue(mContact, (string)contactValue);
                            }

                            break;

                        case "DATETIME":
                            if (emulateTestData)
                            {
                                contactDetailProp.SetValue(mContact, DateTime.Now.AddHours(-12));
                            }
                            else
                            {
                                contactDetailProp.SetValue(mContact, (DateTime)contactValue);
                            }

                            break;

                        case "DECIMAL":
                            if (emulateTestData)
                            {
                                contactDetailProp.SetValue(mContact, 9999.99m);
                            }
                            else
                            {
                                contactDetailProp.SetValue(mContact, (decimal)contactValue);
                            }

                            break;

                        case "NUMBER":
                            if (contactValueType.Contains("(") && contactValueType.Contains(")"))
                            {
                                if (emulateTestData)
                                {
                                    contactDetailProp.SetValue(mContact, 999999.99m);
                                }
                                else
                                {
                                    contactDetailProp.SetValue(mContact, (decimal)contactValue);
                                }
                            }
                            else
                            {
                                if (emulateTestData)
                                {
                                    contactDetailProp.SetValue(mContact, 999999);
                                }
                                else
                                {

                                    contactDetailProp.SetValue(mContact, (Int32)contactValue);
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
            return mContact;
        }

    }
}