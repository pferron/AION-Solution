using AION.BL.Common.AccelaDataLoaders;
using AION.Manager.Adapters;
using Meck.Shared.Accela.ParserModels;
using Meck.Shared.MeckDataMapping;
using Meck.Shared.PosseToAccela;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AION.Manager.AccelaBusinessObjects
{
    public class AccelaProjectModelBO : AccelaBusinessObjectBase
    {
        public AccelaProjectModel ConvertAccelaToAionMappingAccelaProjectModel(AccelaRecordModel recorddata, List<MeckAccelaDataMap> mAccelaAIONMap)
        {
            sbAionMapTrap = new StringBuilder();
            IAIONGenericParserInterface mAionGenericDataParser = new AIONGenericDataParser();
            List<TradeInfo> mTradeInfos = new List<TradeInfo>();
            List<AgencyInfo> mAgencyInfos = new List<AgencyInfo>();
            AccelaProjectModel mAccelaProjectModel = new AccelaProjectModel();

            // build the different objects that are contained in the  mAionGenericDataParser object

            foreach (var mIncomingAccelaRecord in recorddata.result)
            {
                object mCurrentRecordId;

                int mNumSheets = 0;
                mIncomingAccelaRecord.CommonFields.TryGetValue("id", out mCurrentRecordId);

                if (mCurrentRecordId is null)
                {
                    sbAionMapTrap.AppendLine("A data problem detected from Accela GetRecord-Unable to extract recordId from CommonFields. No recovery on this error contact support.");
                    throw new Exception(sbAionMapTrap.ToString());
                }

                // Get the Paid status for the current record

                mAccelaProjectModel.IsPaidStatus = new AccelaBOAdapter().GetRecordPaidStatusFromConditons(mCurrentRecordId.ToString());

                // get Contacts and rest of details.
                // 
                List<Dictionary<string, object>> mCommonContacts = mIncomingAccelaRecord.Contacts;
                mAccelaProjectModel.Contacts = new List<contactDetail>();
                foreach (var contactDictionary in mCommonContacts)
                {
                    var nContactDetail = new ContactDetailBO().ParserRecordContact(mCurrentRecordId.ToString(), contactDictionary, mAccelaAIONMap, false);
                    mAccelaProjectModel.Contacts.Add(nContactDetail);
                }

                List<Dictionary<string, object>> mLicensedProfessionals = mIncomingAccelaRecord.professionals;
                mAccelaProjectModel.Professionals = new List<professionalDetail>();
                foreach (var professionalDictionary in mLicensedProfessionals)
                {
                    var nProfessionalDetail = new ProfessionalDetailBO().ParserRecordProfessional(professionalDictionary, mAccelaAIONMap, false);
                    mAccelaProjectModel.Professionals.Add(nProfessionalDetail);
                }

                // Map the data for AccelaProjectDisplayInfo
                var mAccelaDisplayMap = mAccelaAIONMap.FindAll(x => x.AION_CLS_NM == "AccelaProjectDisplayInfo").ToList();
                mAccelaProjectModel.DisplayOnlyInformation = new AccelaProjectDisplayInfoBO().ConvertAccelaToAIONProjectDisplayInfo((string)mCurrentRecordId, mIncomingAccelaRecord, mAccelaDisplayMap);


                bool valSet = int.TryParse(mAccelaProjectModel.DisplayOnlyInformation.NumofSheets, out mNumSheets);
                TradeInfo mTradeInfo = new TradeInfo()
                {
                    EstimationHours = 10,
                    NumberOfSheets = mNumSheets,
                    AccelaDepartmentDivisionRef = "Building",
                    AccelaDepartmentRegionRef = "Matthews"
                };
                AgencyInfo mAgencyInfo = new AgencyInfo()
                {
                    AccelaDepartmentDivisionRef = "Building",
                    AccelaDepartmentRegionRef = "Matthews"
                };
                mAgencyInfos.Add(mAgencyInfo);
                mTradeInfos.Add(mTradeInfo);

                // remove the Contacts
                var mNewPosseAccelaMap = mAccelaAIONMap.FindAll(x => x.ACCELA_OBJ_TYP_DESC != "Contacts").ToList();

                // remove the Displaymap  used the removed contacts and remove the Display
                var mNotAccelaDisplayMap = mNewPosseAccelaMap.FindAll(x => x.AION_CLS_NM != "AccelaProjectDisplayInfo").ToList();

                // process the Accela t AION Map for
                foreach (var maprecord in mNotAccelaDisplayMap)
                {
                    // for each value from table
                    // 1. Get the Accela dataObject Name
                    // 2. do a Key value look up using the IAIONGenericParserInterface method type as the value if Dictionary or Array object.
                    //      a, pass in Accel field name  and return to the Aion object navalue name item
                    //       b. if needed passthe return object back up to here as needed.
                    // 3. move to next iotem to be found.
                    //   if the returned object is null that meas a there was no value , b was not in the record.
                    //    wonder if the return result can be struct of message and object for value?????
                    /*   Sub section of AccelaRecordModel
                         List<Dictionary<string, object>> mParcels = record.Parcels;
                        List<Dictionary<string, object>> mProfessionals = record.professionals;
                        List<Dictionary<string, object>> mAddresses = record.Addresses;
                        List<Dictionary<string, object>> mContacts = record.Contacts;
                        List<Dictionary<string, object>> mCustomFormDetails = record.CustomFormDetails;
                        List<Dictionary<string, object>> mCustomTables = record.CustomTables;
                        List<Dictionary<string, object>> mOwners = record.Owners;
                        Dictionary<string, object> mCommonFields = record.CommonFields;
                        Dictionary<string, object> mType = record.type;
                        Status status = record.status;
                     */
                    try
                    {
                        if (maprecord.AION_CLS_NM == "AccelaProjectModel")
                        {
                            if ((String.IsNullOrWhiteSpace(maprecord.ACCELA_OBJ_TYP_DESC)))
                            {
                                sbAionMapTrap.AppendLine(DateTime.Now + "NO AccelaObject value for classname, field : " +
                                                       maprecord.AION_CLS_NM + "," + maprecord.Meck_FIELD_NM);
                                throw new Exception(sbAionMapTrap.ToString());
                            }
                            else
                            {
                                Type tAccelaProjectModel = mAccelaProjectModel.GetType();
                                PropertyInfo piAccelaProjectModel =
                                    tAccelaProjectModel.GetProperty(maprecord.Meck_FIELD_NM);

                                // Struct  needs to be preset
                                ReturnLookUpValue customField = new ReturnLookUpValue();
                                customField.lookUpValue = null;
                                customField.lookUpStatus = false;


                                switch (maprecord.ACCELA_OBJ_TYP_DESC.ToUpper())
                                {
                                    case "COMMONFIELDS":

                                        if (maprecord.ACCELA_OBJ_TYP_DESC == "CommonFields")
                                        {
                                            Dictionary<string, object> mCommonFields = mIncomingAccelaRecord.CommonFields;

                                            customField = mAionGenericDataParser.GetDictionaryValueByKey(mCommonFields, maprecord.ACCELA_FIELD_NM);
                                        }

                                        if (maprecord.ACCELA_OBJ_TYP_DESC == "Resulttype")
                                        {
                                            Dictionary<string, object> mResulttype = mIncomingAccelaRecord.Resulttype; //.type;

                                            customField =
                                                mAionGenericDataParser.GetDictionaryValueByKey(mResulttype, maprecord.ACCELA_FIELD_NM);
                                        }

                                        break;

                                    case "CUSTOMFORMS":
                                        if (!String.IsNullOrWhiteSpace(maprecord.ACCELA_LOC_DESC))
                                        {
                                            if (mIncomingAccelaRecord.CustomForms.Exists(x => x.ContainsKey(maprecord.ACCELA_FIELD_NM)))
                                            {
                                                var searchFormsResult = mIncomingAccelaRecord.CustomForms
                                                    .FindAll(x => x.ContainsKey(maprecord.ACCELA_FIELD_NM) && x.ContainsValue(maprecord.ACCELA_LOC_DESC))
                                                    .ToList();

                                                if (searchFormsResult.Count > 0)
                                                {
                                                    // we got a match pull it out


                                                    foreach (var target in searchFormsResult)
                                                    {
                                                        if (target.ContainsKey(maprecord.ACCELA_FIELD_NM))
                                                        {
                                                            if (maprecord.Meck_DATA_TYP_DESC.ToUpper()
                                                                .Contains("PRELIM"))
                                                            {
                                                                customField.lookUpStatus = true;
                                                                customField.lookUpValue = searchFormsResult;
                                                            }
                                                            else
                                                            {
                                                                if (maprecord.Meck_DATA_TYP_DESC.ToUpper()
                                                                    .Contains("LIST"))
                                                                {
                                                                    customField.lookUpStatus = true;
                                                                    customField.lookUpValue = searchFormsResult;
                                                                    break;
                                                                }
                                                                else
                                                                {
                                                                    object xvalue;
                                                                    target.TryGetValue(maprecord.ACCELA_FIELD_NM,
                                                                        out xvalue);
                                                                    if (xvalue is null)
                                                                    {
                                                                        customField.lookUpStatus = true;
                                                                        customField.lookUpValue = String.Empty;
                                                                    }
                                                                    else
                                                                    {
                                                                        customField.lookUpStatus = true;
                                                                        customField.lookUpValue = xvalue;
                                                                    }

                                                                    break;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }

                                        }

                                        break;

                                    case "CUSTOMTABLES":
                                        if (mIncomingAccelaRecord.CustomTables.Exists(x =>
                                            x.ContainsKey(maprecord.ACCELA_FIELD_NM)))
                                        {
                                            if (maprecord.Meck_DATA_TYP_DESC.ToUpper().Contains("TASKACTIVATION") ||
                                                maprecord.Meck_DATA_TYP_DESC.ToUpper().Contains("ACCELAMEETING") ||
                                                maprecord.Meck_DATA_TYP_DESC.ToUpper().Contains("GATERESPONSE"))

                                            {
                                                var tsearchTablesResult = mIncomingAccelaRecord.CustomTables.Find(x =>
                                                    x.ContainsKey(maprecord.ACCELA_FIELD_NM) &&
                                                    x.ContainsValue(maprecord.ACCELA_LOC_DESC)).ToList();
                                                if (tsearchTablesResult.Count > 0)
                                                {
                                                    // we got a match pull it out
                                                    if (maprecord.Meck_DATA_TYP_DESC.ToUpper()
                                                        .Contains("TASKACTIVATION"))
                                                    {
                                                        customField.lookUpStatus = true;
                                                        customField.lookUpValue = tsearchTablesResult;

                                                    }

                                                    if (maprecord.Meck_DATA_TYP_DESC.ToUpper()
                                                        .Contains("ACCELAMEETING"))
                                                    {
                                                        customField.lookUpStatus = true;
                                                        customField.lookUpValue = tsearchTablesResult;

                                                    }

                                                    if (maprecord.Meck_DATA_TYP_DESC.ToUpper()
                                                        .Contains("GATERESPONSE"))
                                                    {
                                                        customField.lookUpStatus = true;
                                                        customField.lookUpValue = tsearchTablesResult;
                                                    }



                                                    break;
                                                }
                                            }
                                            else
                                            {
                                                var searchTablesResult = mIncomingAccelaRecord.CustomTables.Find(x =>
                                                    x.ContainsKey(maprecord.ACCELA_FIELD_NM)).ToList();

                                                //ReturnLookUpValue customTablesField = new ReturnLookUpValue();

                                                foreach (var target in searchTablesResult)
                                                {
                                                    if (target.Key == maprecord.ACCELA_FIELD_NM)
                                                    {
                                                        if (target.Value == null)
                                                        {
                                                            customField.lookUpStatus = true;
                                                            customField.lookUpValue = String.Empty;
                                                        }
                                                        else
                                                        {
                                                            customField.lookUpStatus = true;
                                                            customField.lookUpValue = target.Value;
                                                        }

                                                    }
                                                }
                                            }
                                        }

                                        break;

                                    case "PROFESSIONALS":
                                        List<Dictionary<string, object>> searchProfessionalsFormsResult =
                                            new List<Dictionary<string, object>>();

                                        if (!String.IsNullOrWhiteSpace(maprecord.ACCELA_LOC_DESC))
                                        {
                                            if (mIncomingAccelaRecord.professionals.Exists(x =>
                                                x.ContainsKey(maprecord.ACCELA_FIELD_NM)))
                                            {
                                                if (maprecord.ACCELA_LOC_DESC == null)
                                                {
                                                    searchProfessionalsFormsResult = mIncomingAccelaRecord.professionals
                                                        .FindAll(x => x.ContainsKey(maprecord.ACCELA_FIELD_NM))
                                                        .ToList();
                                                }
                                                else
                                                {
                                                    searchProfessionalsFormsResult = mIncomingAccelaRecord.professionals
                                                        .FindAll(x =>
                                                            x.ContainsKey(maprecord.ACCELA_FIELD_NM) &&
                                                            x.ContainsValue(maprecord.ACCELA_LOC_DESC))
                                                        .ToList();
                                                }


                                                if (searchProfessionalsFormsResult.Count > 0)
                                                {
                                                    // we got a match pull it out


                                                    foreach (var target in searchProfessionalsFormsResult)
                                                    {
                                                        if (target.ContainsKey(maprecord.ACCELA_FIELD_NM))
                                                        {
                                                            if (maprecord.Meck_DATA_TYP_DESC.ToUpper().Contains("LIST"))
                                                            {
                                                                customField.lookUpStatus = true;
                                                                customField.lookUpValue =
                                                                    searchProfessionalsFormsResult;
                                                                break;
                                                            }
                                                            else
                                                            {
                                                                object xvalue;
                                                                target.TryGetValue(maprecord.ACCELA_FIELD_NM,
                                                                    out xvalue);
                                                                if (xvalue is null)
                                                                {
                                                                    customField.lookUpStatus = true;
                                                                    customField.lookUpValue = String.Empty;
                                                                }
                                                                else
                                                                {
                                                                    customField.lookUpStatus = true;
                                                                    customField.lookUpValue = xvalue;
                                                                }

                                                                break;
                                                            }
                                                        }
                                                    }
                                                }
                                            }

                                        }

                                        break;

                                    case "ADDRESSES":
                                        var searchAddressesResult = mIncomingAccelaRecord.Addresses.Find(x => x.ContainsKey(maprecord.ACCELA_FIELD_NM))
                                            .ToList();

                                        if (searchAddressesResult.Count > 0)
                                        {
                                            // we got a match pull it out

                                            foreach (var target in searchAddressesResult)
                                            {
                                                if (target.Key == maprecord.ACCELA_FIELD_NM)
                                                {
                                                    if (target.Value == null)
                                                    {
                                                        customField.lookUpStatus = true;
                                                        customField.lookUpValue = String.Empty;
                                                    }
                                                    else
                                                    {
                                                        customField.lookUpStatus = true;
                                                        customField.lookUpValue = target.Value;
                                                    }

                                                    break;
                                                }
                                            }
                                        }

                                        break;

                                    case "OWNERS":
                                        var searchOwnersResult = mIncomingAccelaRecord.CustomTables.Find(x => x.ContainsKey(maprecord.ACCELA_FIELD_NM))
                                            .ToList();

                                        if (searchOwnersResult.Count > 0)
                                        {
                                            // we got a match pull it out

                                            foreach (var target in searchOwnersResult)
                                            {
                                                if (target.Key == maprecord.ACCELA_FIELD_NM)
                                                {
                                                    if (target.Value == null)
                                                    {
                                                        customField.lookUpStatus = true;
                                                        customField.lookUpValue = String.Empty;
                                                    }
                                                    else
                                                    {
                                                        customField.lookUpStatus = true;
                                                        customField.lookUpValue = target.Value;
                                                    }

                                                    break;
                                                }
                                            }
                                        }

                                        break;

                                    case "PARCELS":
                                        var searchParcelsResult = mIncomingAccelaRecord.Parcels.Find(x => x.ContainsKey(maprecord.ACCELA_FIELD_NM))
                                            .ToList();

                                        if (searchParcelsResult.Count > 0)
                                        {
                                            // we got a match pull it out

                                            foreach (var target in searchParcelsResult)
                                            {
                                                if (target.Key == maprecord.ACCELA_FIELD_NM)
                                                {
                                                    if (target.Value == null)
                                                    {
                                                        customField.lookUpStatus = true;
                                                        customField.lookUpValue = String.Empty;
                                                    }
                                                    else
                                                    {
                                                        customField.lookUpStatus = true;
                                                        customField.lookUpValue = target.Value;
                                                    }

                                                    break;
                                                }
                                            }
                                        }

                                        break;
                                    case "RESULTTYPE":

                                        foreach (var target in mIncomingAccelaRecord.Resulttype)
                                        {
                                            if (target.Key == maprecord.ACCELA_FIELD_NM)
                                            {
                                                if (target.Value == null)
                                                {
                                                    customField.lookUpStatus = true;
                                                    customField.lookUpValue = string.Empty;
                                                }
                                                else
                                                {
                                                    customField.lookUpStatus = true;
                                                    customField.lookUpValue = target.Value;
                                                }
                                            }
                                        }

                                        break;
                                    case "RESULTSTATUS":
                                        var mStatus = mIncomingAccelaRecord.Resultstatus; //.type;

                                        if (piAccelaProjectModel != null)
                                        {

                                            if (mStatus == null || mStatus.value == null)
                                            {
                                                customField.lookUpStatus = true;
                                                customField.lookUpValue = String.Empty;
                                            }
                                            else
                                            {
                                                customField.lookUpStatus = true;
                                                customField.lookUpValue = mStatus.value;
                                            }
                                        }

                                        break;
                                    case "CONDITIONS":
                                        List<Dictionary<string, object>> mConditions = mIncomingAccelaRecord.conditions;
                                        if (piAccelaProjectModel != null)
                                        {

                                            if (maprecord.ACCELA_LOOKUP_FIELD_NM == string.Empty)
                                            {
                                                customField = mAionGenericDataParser.GetSingleLookupFromList(mConditions,
                                                    maprecord.ACCELA_LOC_DESC, maprecord.ACCELA_FIELD_NM, "Value");
                                            }
                                            else
                                            {
                                                customField = mAionGenericDataParser.GetDictionaryValueUsingKeyWithLookUp(mConditions,
                                                    maprecord.ACCELA_LOOKUP_FIELD_NM, maprecord.ACCELA_LOOKUP_VAL_DESC, maprecord.ACCELA_FIELD_NM);
                                            }
                                        }

                                        break;

                                    default:
                                        sbAionMapTrap.AppendFormat("UNRESOLVED VALUE: {0} in {1}; ", maprecord.Meck_FIELD_NM, maprecord.ACCELA_LOC_DESC);
                                        break;
                                }

                                if (customField.lookUpStatus && customField.lookUpValue != null)
                                {
                                    piAccelaProjectModel = SetValueUpdate(piAccelaProjectModel, mAccelaProjectModel,
                                        maprecord.Meck_DATA_TYP_DESC, customField.lookUpValue);
                                }

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        string ErrMsg = DateTime.Now + "Exception trapped " + maprecord.AION_CLS_NM + "." + maprecord.Meck_FIELD_NM + " Check Azure Logging for more information  : " + ex.Message;

                    }
                }
            }

            // build error parsing info.
            if (!string.IsNullOrWhiteSpace(mAccelaProjectModel.MappingError))
            {
                sbAionMapTrap.Append(sbAionMapTrap.ToString());
            }

            mAccelaProjectModel.MappingError = sbAionMapTrap.ToString();

            mAccelaProjectModel.ProjectTradesList = mTradeInfos;
            mAccelaProjectModel.ProjectAgencyList = mAgencyInfos;

            return mAccelaProjectModel;
        }

    }
}