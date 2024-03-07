
using AION.Accela.Engine;
using AION.Accela.Engine.BusinessObjects;
using AION.AIONDB.Engine;
using AION.AIONDB.Engine.BusinessObjects;
using AION.BL.Common.AccelaDataLoaders;
using AION.BL.Models;
using DemoInterface.Common;
using Meck.Shared.Accela.ParserModels;
using Meck.Shared.MeckDataMapping;
using Meck.Shared.PosseToAccela;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;



namespace DemoInterface.Adapters
{

    /// <summary>
    /// This class works as an adapter between AION and Accela API interfaces. All the calls from AION to Accelal should be done in this class.
    /// </summary>
    public class AAAEstimationAccelaMappingAdapter : LoggingWrapper, IEstimationAccelaMappingAdapter
    {
        private IAccelaEngine _acceladataconn;

        //   private List<SystemRole> _usersystemroles;
        private enum ListType { Reviewer, Estimator, Facilitator };
        //    private ListType _listtype;

        //  private ProjectEstimationModelBO _projectestimationmodelbo;

        //  private AIONProjectModel mAccelaProjectModel = new AIONProjectModel();

        private AccelaProjectModel mAccelaProjectModel = new AccelaProjectModel();



        StringBuilder sbAionMapTrap = new StringBuilder();

        public AAAEstimationAccelaMappingAdapter()
        {
            _acceladataconn = new AccelaApiBO();

        }

        public AccelaProjectModel GetProjectDetailsLoad(ProjectParms parms)
        {
            //  AIONProjectModel mMappedAccelaProjectMode = new AIONProjectModel();

            AccelaProjectModel mAccelaProjectModel = new AccelaProjectModel();

            try
            {
                var result = GetProjectDetails(parms);

                var AccelaRec = result;

                var mapresult = Task.Run(() => GetAccelaAIONMapByAccelaRecordType(AccelaRec.result[0].ParseRecType));
                mapresult.Wait();
                var mAccelaAONMap = mapresult.Result;

                mAccelaProjectModel = ConvertAccelaToAionMappingAccelaProjectModel(result, mAccelaAONMap);

                // remove items we can't process , undefined in Mapping  



                return mAccelaProjectModel;
            }
            catch (Exception ex)
            {
                string ErrMsg = DateTime.Now + "Exception trapped while running AccelaMapping RecordId " + parms.ProjectId + "check Azure Logging " + ex.Message;

                //     BLLogMessage(MethodBase.GetCurrentMethod(), ErrMsg, ex);

                //  throw new Exception(ErrMsg);
            }
            return mAccelaProjectModel;
        }

        /// <summary>
        /// GetProjectDetailsForAIONDisplayInfo
        /// </summary>
        /// <param name="parms"></param>
        /// <param name="mCurrentProjectModel"> Requires current Active AccelProjectModel, and AccelaProjectDisplayInfo is replaced.</param>
        /// <returns></returns>
        public AccelaProjectDisplayInfo GetProjectDetailsForAIONDisplayInfo(ProjectParms parms, AccelaProjectModel mCurrentProjectModel)
        {
            AccelaProjectDisplayInfo mAccelaDisplayInfo = new AccelaProjectDisplayInfo();
            try
            {
                if (parms.ProjectId != mCurrentProjectModel.ProjectIDRef)
                {
                    string errorMisMatch =
                        "Current Project model does not match to Parms projectId. Unable to Get Display Information";
                    throw new Exception(errorMisMatch);
                }

                var getRecordResult = GetProjectDetails(parms);

                var mAccelaGetRecordResult = getRecordResult.result;

                var mapresult = GetAccelaAIONMap();
                mapresult.Wait();
                var mAccelaAONMap = mapresult.Result;

                if (mAccelaGetRecordResult.Count != 1)
                {
                    throw new Exception("A requested record was not returned when processing Accela Project Display Information");
                }

                foreach (var result in mAccelaGetRecordResult)
                {
                    mAccelaDisplayInfo = ConvertAccelaToAIONProjectDisplayInfo(parms.ProjectId, result, mAccelaAONMap);

                    mCurrentProjectModel.DisplayOnlyInformation = mAccelaDisplayInfo;

                    // only process one result 
                    break;
                }


            }
            catch (Exception ex)
            {
                string ErrMsg = DateTime.Now +
                                "Exception has been logged, check Azure Logging for additional details." + ex.Message;

                //    sbAionMapTrap.AppendLine(ErrMsg);

                //    mCurrentProjectModel.MappingError = ErrMsg;

                //   BLLogMessage(MethodBase.GetCurrentMethod(), ErrMsg, ex);

                //  throw new Exception(ErrMsg);
            }
            return mAccelaDisplayInfo;
        }

        /// <summary>
        /// GetAccelaAIONMap
        /// </summary>
        /// <returns>List<AccelaAionMap> </AAccelAionMap></returns>
        private async Task<List<MeckAccelaDataMap>> GetAccelaAIONMap()
        {
            IAIONDBEngine thisengine = new AIONEngineCrudApiBO();
            var result = thisengine.SelectAccelaAionMap();

            List<MeckAccelaDataMap> mAccelaAIONMap = result;

            return mAccelaAIONMap;
        }

        /// <summary>
        /// GetAccelaAIONMap
        /// </summary>
        /// <returns>List<AccelaAionMap> </AAccelAionMap></returns>
        private async Task<List<MeckAccelaDataMap>> GetAccelaAIONMapByAccelaRecordType(string AccelaRecordType)
        {
            IAIONDBEngine thisengine = new AIONEngineCrudApiBO();
            var result = Task.Run(() => thisengine.SelectAccelaAionMapByRecordType(AccelaRecordType));
            result.Wait();
            List<MeckAccelaDataMap> mAccelaAIONMap = result.Result;

            // remove records we can't process 
            List<MeckAccelaDataMap> mMeckAccelaDataMaps = new List<MeckAccelaDataMap>();

            mMeckAccelaDataMaps = mAccelaAIONMap.FindAll(x => x.ACCELA_FIELD_NM != "no distinct value" && x.ACCELA_FIELD_NM != string.Empty && x.ACCELA_DATA_TYP_DESC != string.Empty).ToList();

            mMeckAccelaDataMaps = mMeckAccelaDataMaps.FindAll(X => X.ACCELA_OBJ_TYP_DESC.ToUpper() != "VOID").ToList();

            return mMeckAccelaDataMaps;
        }


        private AccelaRecordModel GetProjectDetails(ProjectParms parms)
        {
            _acceladataconn = new AccelaApiBO();

            var task = _acceladataconn.GetAccelaRecord(parms.ProjectId);
            task.Wait();
            var accelaRecordModel = task.Result;


            var intermediate = JsonConvert.SerializeObject(accelaRecordModel);

            //   var result = JsonConvert.DeserializeObject<RecordWrapperBE>(JsonConvert.SerializeObject(jsonresult.Result));

            return accelaRecordModel;
        }

        public AccelaProjectModel ConvertAccelaToAionMappingAccelaProjectModel(AccelaRecordModel recorddata, List<MeckAccelaDataMap> mAccelaAIONMap)
        {
            IAIONGenericParserInterface mAionGenericDataParser = new AIONGenericDataParser();

            List<TradeInfo> mTradeInfos = new List<TradeInfo>();

            TradeInfo mTradeInfo = new TradeInfo()
            {
                EstimationHours = 10,
                NumberOfSheets = 10
            };
            mTradeInfos.Add(mTradeInfo);

            // build the different objects that are contained in the  mAionGenericDataParser object 

            foreach (var mIncomingAccelaRecord in recorddata.result)
            {
                object mCurrentRecordId;

                mIncomingAccelaRecord.CommonFields.TryGetValue("id", out mCurrentRecordId);

                if (mCurrentRecordId is null)
                {
                    sbAionMapTrap.AppendLine("A data problem detected from Accela GetRecord-Unable to extract recordId from CommonFields. No recovery on this error contact support.");
                    throw new Exception(sbAionMapTrap.ToString());
                }

                List<Dictionary<string, object>> mCommonContacts = mIncomingAccelaRecord.Contacts;

                foreach (var contactDictionary in mCommonContacts)
                {
                    var nContactDetail = ParserRecordContact(contactDictionary, mAccelaAIONMap, false);
                    mAccelaProjectModel.Contacts = new List<contactDetail>();
                    mAccelaProjectModel.Contacts.Add(nContactDetail);
                }

                // Map the data for AccelaProjectDisplayInfo
                var mAccelaDisplayMap = mAccelaAIONMap.FindAll(x => x.AION_CLS_NM == "AccelaProjectDisplayInfo").ToList();
                mAccelaProjectModel.DisplayOnlyInformation = ConvertAccelaToAIONProjectDisplayInfo((string)mCurrentRecordId, mIncomingAccelaRecord, mAccelaDisplayMap);

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
                                System.Type tAccelaProjectModel = mAccelaProjectModel.GetType();
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
                                                            if (maprecord.Meck_DATA_TYP_DESC.ToUpper().Contains("LIST"))
                                                            {
                                                                customField.lookUpStatus = true;
                                                                customField.lookUpValue = searchFormsResult;
                                                                break;
                                                            }
                                                            else
                                                            {
                                                                object xvalue;
                                                                target.TryGetValue(maprecord.ACCELA_FIELD_NM, out xvalue);
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

                                    case "CUSTOMTABLES":

                                        //    int ExistsIndx = mIncomingAccelaRecord.CustomTables.FindIndex(x => x.ContainsKey(maprecord.ACCELA_FIELD_NM));

                                        if (mIncomingAccelaRecord.CustomTables.Exists(x => x.ContainsKey(maprecord.ACCELA_FIELD_NM)))
                                        {
                                            var searchTablesResult = mIncomingAccelaRecord.CustomTables
                                                .Find(x => x.ContainsKey(maprecord.ACCELA_FIELD_NM))
                                                .ToList();
                                            //ReturnLookUpValue customTablesField = new ReturnLookUpValue();

                                            if (searchTablesResult.Count > 0)
                                            {
                                                // we got a match pull it out

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
                                            //   mAionGenericDataParser.GetDictionaryValueByKey(mStatus, maprecord.ACCELA_FIELD_NM, emulateTestData);

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

                        //                      sbAionMapTrap.AppendLine(ErrMsg);
                        //                       BLLogMessage(MethodBase.GetCurrentMethod(), ErrMsg, ex);
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

            return mAccelaProjectModel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="RecordId"></param>
        /// <param name="mAccelaRecord"></param>
        /// <param name="mAccelaAIONMap"></param>
        /// <returns></returns>
        public AccelaProjectDisplayInfo ConvertAccelaToAIONProjectDisplayInfo(string RecordId, Meck.Shared.Accela.ParserModels.Result mIncomingAccelaRecord, List<MeckAccelaDataMap> mAccelaAIONMap)
        {
            AccelaProjectDisplayInfo mAccelaProjectDisplayInfo = new AccelaProjectDisplayInfo();

            IAIONGenericParserInterface mAionGenericDataParser = new AIONGenericDataParser();

            StringBuilder sbAionMapTrap = new StringBuilder();

            // build the different objects that are contained in the  mAionGenericDataParser object 

            // Sanity check to make sure we have the correct RecordId 

            object mCurrentRecordId;


            mIncomingAccelaRecord.CommonFields.TryGetValue("id", out mCurrentRecordId);

            // ReturnLookUpValue mReturnVal = new ReturnLookUpValue();

            if ((string)mCurrentRecordId != (string)RecordId)
            {
                sbAionMapTrap.AppendLine(
                    "A data problem detected with Accela record information, recordId for AIONProjectDisplayInfo is not available. Please contact support on this error.");
                throw new Exception(sbAionMapTrap.ToString());
            }
            // Process Contacts prior to other details . 

            List<Dictionary<string, object>> mCommonContacts = mIncomingAccelaRecord.Contacts;

            foreach (var contactDictionary in mCommonContacts)
            {
                var nContactDetail = ParserRecordContact(contactDictionary, mAccelaAIONMap, false);

                mAccelaProjectDisplayInfo.Contacts = new List<contactDetail>();

                mAccelaProjectDisplayInfo.Contacts.Add(nContactDetail);
            }

            // remove the Contacts 
            var mNewPosseAccelaMap = mAccelaAIONMap.FindAll(x => x.ACCELA_OBJ_TYP_DESC != "Contacts").ToList();

            foreach (var maprecord in mNewPosseAccelaMap)
            {
                try
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
                        List<AccelaFormResult> mCustomFormDetails = record.CustomFormDetails;
                        List<AccelaTableResult> mCustomTables = record.CustomTables;
                        List<Dictionary<string, object>> mOwners = record.Owners;
                        Dictionary<string, object> mCommonFields = record.CommonFields;
                        Dictionary<string, object> mType = record.type;
                        Status status = record.status;  
    
                     */

                    if ((String.IsNullOrWhiteSpace(maprecord.ACCELA_OBJ_TYP_DESC)))
                    {
                        sbAionMapTrap.AppendLine(DateTime.Now + " NO AccelaObject value for classname, field : " +
                                                 maprecord.AION_CLS_NM + "," + maprecord.Meck_FIELD_NM.Trim());
                    }
                    else
                    {

                        if (maprecord.AION_CLS_NM == "AccelaProjectDisplayInfo")
                        {
                            System.Type tAccelaProjectModel = mAccelaProjectDisplayInfo.GetType();
                            PropertyInfo piAccelaProjectModel =
                                tAccelaProjectModel.GetProperty(maprecord.Meck_FIELD_NM.Trim());

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
                                                .FindAll(x => x.ContainsKey(maprecord.ACCELA_FIELD_NM) && x.ContainsValue(maprecord.ACCELA_LOC_DESC.Trim()))
                                                .ToList();

                                            if (searchFormsResult.Count > 0)
                                            {
                                                // we got a match pull it out


                                                foreach (var target in searchFormsResult)
                                                {
                                                    if (target.ContainsKey(maprecord.ACCELA_FIELD_NM))
                                                    {
                                                        if (maprecord.Meck_DATA_TYP_DESC.ToUpper().Contains("LIST"))
                                                        {
                                                            customField.lookUpStatus = true;
                                                            customField.lookUpValue = searchFormsResult;
                                                            break;
                                                        }
                                                        else
                                                        {
                                                            object xvalue;
                                                            target.TryGetValue(maprecord.ACCELA_FIELD_NM, out xvalue);
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

                                case "CUSTOMTABLES":

                                    //    int ExistsIndx = mIncomingAccelaRecord.CustomTables.FindIndex(x => x.ContainsKey(maprecord.ACCELA_FIELD_NM));

                                    if (mIncomingAccelaRecord.CustomTables.Exists(x => x.ContainsKey(maprecord.ACCELA_FIELD_NM)))
                                    {
                                        var searchTablesResult = mIncomingAccelaRecord.CustomTables
                                            .Find(x => x.ContainsKey(maprecord.ACCELA_FIELD_NM))
                                            .ToList();
                                        //ReturnLookUpValue customTablesField = new ReturnLookUpValue();

                                        if (searchTablesResult.Count > 0)
                                        {
                                            // we got a match pull it out

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
                                        //   mAionGenericDataParser.GetDictionaryValueByKey(mStatus, maprecord.ACCELA_FIELD_NM, emulateTestData);

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
                                piAccelaProjectModel = SetValueUpdate(piAccelaProjectModel, mAccelaProjectDisplayInfo,
                                    maprecord.Meck_DATA_TYP_DESC, customField.lookUpValue);
                            }
                            else
                            {
                                //  don't set to null ; 
                                //  piAccelaProjectModel.SetValue(mPosseAccelaRecord, null);
                                //piAccelaProjectModel = SetValueUpdate(piAccelaProjectModel, mPosseAccelaRecord,
                                //  maprecord.POSSE_DATA_TYP_DESC, string.Empty);
                            }

                        }
                    }
                }

                catch (Exception ex)
                {
                    string ErrMsg = DateTime.Now + "Exception trapped " + maprecord.Meck_FIELD_NM + " Check Azure Logging for more information  : " +
                                    ex.Message;

                    //                      sbAionMapTrap.AppendLine(ErrMsg);
                    BLLogMessage(MethodBase.GetCurrentMethod(), ErrMsg, ex);
                }
            }


            return mAccelaProjectDisplayInfo;
        }

        /// <summary>
        ///  SetValueUpdate   used to update the primary object to complete mapping
        /// </summary>
        /// <param name="AIONObjectItem"></param>
        /// <param name="target"></param>
        /// <param name="AionDataFieldType"></param>
        /// <param name="newValue"></param>
        /// <returns></returns>
        private PropertyInfo SetValueUpdate(PropertyInfo AIONObjectItem, Object target, string AionDataFieldType, object newValue)
        {
            try
            {
                switch (AionDataFieldType)
                {
                    case "string":
                        AIONObjectItem.SetValue(target, newValue.ToString());
                        break;
                    case "bool":
                        if ((string)newValue == "YES")
                        {
                            AIONObjectItem.SetValue(target, true);
                        }
                        else
                        {
                            AIONObjectItem.SetValue(target, false);
                        }

                        break;
                    case "datetime":
                        AIONObjectItem.SetValue(target, (DateTime)newValue);
                        break;
                    case "double":
                        AIONObjectItem.SetValue(target, (double)newValue);
                        break;
                    case "int":
                        AIONObjectItem.SetValue(target, (Int32)newValue);
                        break;
                    case "decimal":
                        AIONObjectItem.SetValue(target, (decimal)newValue);
                        break;
                    case "long":
                        AIONObjectItem.SetValue(target, (Int64)newValue);
                        break;
                    case "List<AgencyInfo>":
                        AIONObjectItem.SetValue(target, (List<AgencyInfo>)newValue);
                        break;
                    case "List<TradeInfo>":
                        AIONObjectItem.SetValue(target, (List<TradeInfo>)newValue);
                        break;
                    case "List<string>":

                        List<string> mstring = new List<string>();

                        if (newValue.GetType().Name.Contains("List"))
                        {
                            foreach (var newelement in (List<Dictionary<string, Object>>)newValue)
                            {
                                foreach (var newitem in newelement)
                                {
                                    mstring.Add(newitem.Key + ":" + newitem.Value);

                                }
                            }
                            AIONObjectItem.SetValue(target, (List<string>)mstring);
                        }
                        else
                        {
                            foreach (var newitem in (Dictionary<string, object>)newValue)
                            {
                                mstring.Add(newitem.Key + ":" + newitem.Value);
                            }

                            AIONObjectItem.SetValue(target, mstring);
                        }

                        break;

                }

                return AIONObjectItem;
            }
            catch (Exception ex)
            {
                throw new Exception("Assign to Object Error: " + AIONObjectItem.Name + " - " + ex.Message, ex.InnerException);
            }
        }

        /// <summary>
        /// GetConstructionTypeValue
        /// </summary>
        /// <param name="constructiontype"></param>
        /// <returns></returns>
        private string GetConstructionTypeValue(string constructiontype)
        {
            if (constructiontype.ToLower().Contains("upfit") || constructiontype.ToLower().Contains("rtap"))
            {
                return "UPFITRTAP";
            }
            else
            {
                return constructiontype;
            }
        }

        public contactDetail ParserRecordContact(Dictionary<string, object> contactDetailObject, List<MeckAccelaDataMap> mPosseContacts, bool emulateTestData = false)
        {
            contactDetail mContact = new contactDetail();
            System.Type t = mContact.GetType();

            PropertyInfo[] contactDetailProps = t.GetProperties();

            List<MeckAccelaDataMap> mMapFields =
                mPosseContacts.FindAll(x => x.ACCELA_OBJ_TYP_DESC == "Contacts" && (!String.IsNullOrEmpty(x.ACCELA_FIELD_NM)));

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
            return mContact;
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
            for (int indx = 0; indx < contactDetailProps.Length - 1; indx++)
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