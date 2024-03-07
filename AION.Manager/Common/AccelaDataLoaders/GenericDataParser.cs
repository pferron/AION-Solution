using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace AION.BL.Common.AccelaDataLoaders
{

    public interface IAIONGenericParserInterface
    {

        ReturnLookUpValue GetSingleLookupFromList<T>(List<T> mAccelaListObj, string Accela_Loc_Desc, string searchValue, string matchValue, bool emulateTestData = false);

        ReturnLookUpValue GetDictionaryValueByKey(Dictionary<String, object> dictSource, string keyValue, bool emulateTestData = false);

        ReturnLookUpValue GetSingleLookupFromListWithKey<T>(List<T> mAccelaListObj, string Accela_Loc_Desc, string searchValue, string matchValue, bool emulateTestData = false);

        ReturnLookUpValue GetArrayListValueByKey(ArrayList tValue, string searchValue, string matchValue, bool emulateTestData = false);

        /// <summary>
        /// GetListOfSingleLookupFromList  placed details (keyValue bypass ID value) into a List<string> for packaging into ReturnLookupValue</string>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mAccelaListObj"></param>
        /// <param name="Accela_Loc_Desc"></param>
        /// <param name="searchValue"></param>
        /// <param name="matchValue"></param>
        /// <returns></returns>
        ReturnLookUpValue GetListOfSingleLookupFromList<T>(List<T> mAccelaListObj, string Accela_Loc_Desc, string searchValue, string matchValue, bool emulateTestData = false);


        /// <summary>
        /// Uses KeyValue check of MatchValue if found returns fieldToLoad
        /// </summary>
        /// <param name="dictSource"></param>
        /// <param name="keyValueToCheck"></param>
        /// <param name="matchtoValue"></param>
        /// <param name="fieldToLoad"></param>
        /// <returns></returns>
        ReturnLookUpValue GetDictionaryValueUsingKeyWithLookUp(List<Dictionary<string, object>> dictSources, string keyValueToCheck,
            string matchtoValue, string fieldToLoad);

    }

    public struct ReturnLookUpValue
    {
        public bool lookUpStatus { get; set; }
        public object lookUpValue { get; set; }
    }

    public partial class AIONGenericDataParser : IAIONGenericParserInterface
    {
        public AIONGenericDataParser()
        {
            //   ReturnLookUpValue mReturnVal;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dictSource"></param>
        /// <param name="valueToCheck"></param>
        /// <param name="matchtoValue"></param>
        /// <param name="fieldToLoad"></param>
        /// <returns></returns>
        public ReturnLookUpValue GetDictionaryValueUsingKeyWithLookUp(List<Dictionary<string, object>> dictSources, string keyValueToCheck, string matchtoValue, string fieldToLoad)
        {
            ReturnLookUpValue mReturnVal = new ReturnLookUpValue();

            foreach (var dictSource in dictSources)
            {
                object objMatchToValue;
              //  object searchValue;  
                dictSource.TryGetValue(keyValueToCheck, out objMatchToValue);

                if (objMatchToValue.ToString().Contains(matchtoValue))
                {
                    object mDictObjectOut = null;
                    dictSource.TryGetValue(fieldToLoad, out mDictObjectOut);
                    
                    mReturnVal.lookUpStatus = true;
                    mReturnVal.lookUpValue = mDictObjectOut;

                    return mReturnVal;
                }
            }

            return mReturnVal;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dictSource"></param>
        /// <param name="keyValue"></param>
        /// <param name="emulateTestData"></param>
        /// <returns></returns>
        public ReturnLookUpValue GetDictionaryValueByKey(Dictionary<String, object> dictSource, string keyValue, bool emulateTestData = false)
        {
            ReturnLookUpValue mReturnVal = new ReturnLookUpValue();

            try
            {
                object mDictObjectOut = null;

                dictSource.TryGetValue(keyValue, out mDictObjectOut);


                mReturnVal.lookUpStatus = true;
                mReturnVal.lookUpValue = mDictObjectOut;
                if (emulateTestData && mDictObjectOut == null)
                {
                    mReturnVal.lookUpValue = "emulatedTestData";
                }



                return mReturnVal;
            }

            catch (Exception ex)
            {
                string objname = dictSource.GetType().Name.ToString();

                string ErrMsg = "Error Extracting Accela Value from Record " + dictSource.GetType().Name.ToString() + "KeyValue: " + keyValue + " - " + ex.Message + " - " + DateTime.Now;

                LoggingWrapper.staticBLLogMessage(MethodBase.GetCurrentMethod(), ErrMsg, ex);

                return mReturnVal;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mAccelaListObj"></param>
        /// <param name="Accela_Loc_Desc"></param>
        /// <param name="searchValue"></param>
        /// <param name="matchValue"></param>
        /// <param name="emulateTestData"></param>
        /// <returns></returns>
        public ReturnLookUpValue GetSingleLookupFromList<T>(List<T> mAccelaListObj, string Accela_Loc_Desc, string searchValue, string matchValue, bool emulateTestData = false)
        {
            ReturnLookUpValue mReturnVal = new ReturnLookUpValue();

            string matchedKey = string.Empty;

            //  Object mResult;
            foreach (var ObjValue in mAccelaListObj)
            {
                string varTypeName = ObjValue.GetType().Name.ToString();

                if (varTypeName.Contains("Dictionary"))
                {
                    Dictionary<string, object> mDictDef = ObjValue as Dictionary<string, object>;

                    if (mDictDef.ContainsKey("id"))
                    {
                        object objId;

                        mDictDef.TryGetValue("id", out objId);

                        if (Accela_Loc_Desc.Trim() == (string)objId)
                        {
                            foreach (var key in mDictDef.Keys)
                            {
                                if (key.ToUpper() == searchValue.Trim().ToUpper())
                                {
                                    matchedKey = key;

                                    mReturnVal = GetDictionaryValueByKey(mDictDef, matchedKey, emulateTestData);

                                    if (mReturnVal.lookUpStatus)
                                    {
                                        mReturnVal.lookUpStatus = true;
                                        return mReturnVal;
                                    }
                                }
                            }
                            break;
                        }
                    }

                    if (varTypeName.Contains("ArrayList"))
                    {
                        ArrayList mArrayListDef = ObjValue as ArrayList;

                        if (string.IsNullOrWhiteSpace(Accela_Loc_Desc) || mArrayListDef.Contains(Accela_Loc_Desc))
                        {
                            mReturnVal = GetArrayListValueByKey(mArrayListDef, searchValue, matchValue, emulateTestData);
                            if (mReturnVal.lookUpStatus)
                            {
                                return mReturnVal;
                            }
                        }
                    }
                }
            }
            return mReturnVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mAccelaListObj"></param>
        /// <param name="searchValue"></param>
        /// <param name="matchValue"></param>
        /// <returns></returns>
        public ReturnLookUpValue GetListOfSingleLookupFromList<T>(List<T> mAccelaListObj, string Accela_Loc_Desc, string searchValue, string matchValue, bool emulateTestData = false)
        {
            ReturnLookUpValue mReturnVal = new ReturnLookUpValue();

            //   Object mResult;

            // OUTPUTAREA
            List<string> mObjString = new List<string>();

            string mTempLine;

            string matchedKey = string.Empty;

            // LOOP AND PROCESS HERE 
            foreach (var ObjValue in mAccelaListObj)
            {
                string varTypeName = ObjValue.GetType().Name.ToString();

                if (varTypeName.Contains("Dictionary"))
                {
                    Dictionary<string, object> mDictDef = ObjValue as Dictionary<string, object>;
                    if (mDictDef.ContainsValue(Accela_Loc_Desc))
                    {
                        foreach (var key in mDictDef.Keys)
                        {
                            if (key.ToUpper() == searchValue.ToUpper())
                            {
                                matchedKey = key;
                            }
                        }

                        if (matchedKey != string.Empty && mDictDef.ContainsValue(matchedKey))
                        {
                            foreach (var altkey in mDictDef.Keys)
                            {
                                object mtvalue;

                                if (altkey != "id")
                                {
                                    // add code for multiple searchValues
                                    // use empty searchvalue to get all values, else part key contains part of value
                                    if (String.IsNullOrWhiteSpace(searchValue))
                                    {
                                        mDictDef.TryGetValue(altkey, out mtvalue);

                                        if (mtvalue is null)
                                        {
                                            mTempLine = altkey + " : " + null;
                                            mObjString.Add(mTempLine);
                                        }
                                        else
                                        {
                                            mTempLine = altkey + " : " + mtvalue.ToString();
                                            mObjString.Add(mTempLine);
                                            if (emulateTestData && mtvalue == null)
                                            {
                                                mTempLine = altkey + " : \" emulatedTestData\"";
                                                mObjString.Add(mTempLine);
                                            }
                                        }

                                    }
                                    else
                                    {
                                        if (altkey.Contains(matchedKey))
                                        {
                                            mDictDef.TryGetValue(altkey, out mtvalue);

                                            if (mtvalue is null)
                                            {
                                                mTempLine = altkey + " : " + null;
                                                mObjString.Add(mTempLine);
                                            }
                                            else
                                            {
                                                mTempLine = altkey + " : " + mtvalue.ToString();
                                                mObjString.Add(mTempLine);
                                                if (emulateTestData && mtvalue == null)
                                                {
                                                    mTempLine = altkey + " : \"emulatedTestData\" ";
                                                    mObjString.Add(mTempLine);
                                                }
                                            }
                                        }

                                    }
                                }
                            }
                        }

                        mReturnVal.lookUpStatus = true;
                        mReturnVal.lookUpValue = mObjString;
                        return mReturnVal;
                    }
                    else
                    {
                        if (mDictDef.ContainsKey(matchedKey))
                        {
                            mReturnVal = GetDictionaryValueByKey(mDictDef, matchedKey, emulateTestData);

                            if (mReturnVal.lookUpStatus)
                            {
                                mReturnVal.lookUpStatus = true;
                                return mReturnVal;
                            }
                        }
                    }
                }

                if (varTypeName.Contains("ArrayList"))
                {
                    ArrayList mArrayListDef = ObjValue as ArrayList;

                    if (string.IsNullOrWhiteSpace(Accela_Loc_Desc) || mArrayListDef.Contains(Accela_Loc_Desc))
                    {
                        mReturnVal = GetArrayListValueByKey(mArrayListDef, searchValue, matchValue, emulateTestData);
                        if (mReturnVal.lookUpStatus)
                        {
                            return mReturnVal;
                        }
                    }
                }
            }

            return mReturnVal;
        }


        public ReturnLookUpValue GetSingleLookupFromListWithKey<T>(List<T> mAccelaListObj, string Accela_Loc_Desc, string searchValue, string matchValue, bool emulateTestData = false)
        {
            ReturnLookUpValue mReturnVal = new ReturnLookUpValue();

            //   Object mResult;

            string matchKey = string.Empty;

            foreach (var tValue in mAccelaListObj)
            {
                string varTypeName = tValue.GetType().Name.ToString();

                switch (varTypeName)
                {
                    case "Dictionary":

                        Dictionary<string, object> mDictDef = tValue as Dictionary<String, object>;

                        if (mDictDef.ContainsValue(Accela_Loc_Desc))
                        {
                            mReturnVal = GetDictionaryValueByKey(mDictDef, searchValue, emulateTestData);

                            if (mReturnVal.lookUpStatus)
                            {
                                return mReturnVal;
                            }
                        }

                        break;

                    case "ArrayList":

                        ArrayList mArrayListDef = tValue as ArrayList;

                        mReturnVal = GetArrayListValueByKey(mArrayListDef, searchValue, matchValue, emulateTestData);
                        if (mReturnVal.lookUpStatus)
                        {
                            return mReturnVal;
                        }

                        break;

                    default:

                        break;
                }

            }

            return mReturnVal;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tValue"></param>
        /// <param name="searchValue"></param>
        /// <param name="matchValue"></param>
        /// <returns></returns>
        public ReturnLookUpValue GetArrayListValueByKey(ArrayList tValue, string searchValue, string matchValue, bool emulateTestData = false)
        {
            ReturnLookUpValue mReturnVal = new ReturnLookUpValue();
            // Similar code used in AccelaEngine.RecordParser for Professionals parsing from Json. 
            /* Model is this:
                    "table value":[
                                   {
                                    "inner value" :[
                                          {
                                           "text" = string 
                                           "value" = string, int, bool  may be another dictionary layer 
                                          }
                                     ]        
                                  }
                               ]          
             */
            object mResult = new object();

            if (tValue.Contains(searchValue))
            {

                foreach (var tDictSource in tValue)
                {
                    // Parcel mParcel = new Parcel();
                    Dictionary<string, Object>
                        mDictSource = (Dictionary<string, Object>)tDictSource;

                    mResult = GetDictionaryValueByKey(mDictSource, searchValue, emulateTestData);
                }
                if (mReturnVal.lookUpStatus)
                {
                    return mReturnVal;
                }
            }
            else
            {
                return mReturnVal;
            }
            return mReturnVal;
        }

    }

}


