using Meck.Shared.Accela.ParserModels;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Reflection;
using System.Text;
using System.Web.Script.Serialization;
using Contact = Meck.Shared.Accela.ParserModels.Contact;
using Country = Meck.Shared.Accela.ParserModels.Country;
using RecordId = Meck.Shared.Accela.ParserModels.RecordId;
using ReportedChannel = Meck.Shared.Accela.ParserModels.ReportedChannel;
using Result = Meck.Shared.Accela.ParserModels.Result;
using State = Meck.Shared.Accela.ParserModels.State;
using Status = Meck.Shared.Accela.ParserModels.Status;
using StreetSuffix = Meck.Shared.Accela.ParserModels.StreetSuffix;
using Type = System.Type;


namespace Posse.Accela.Engine.RecordParser
{
    public class AccelaRecordParser
    {
        // This model is reauired and PaseAccelaJson method need to return it. 
        // public AccelaProjectModel mProjModel;

        Result mCurrentRecord;

        private StringBuilder sberrors = new StringBuilder();


        private List<Result> AccelaRecordDetail = new List<Result>();


        public AccelaRecordParser()
        {
            //    mProjModel = new AccelaProjectModel();
            AccelaRecordDetail = new List<Result>();

        }

        /// <summary>
        /// ParseAccelaJson(string InPutJson)
        /// </summary>
        /// <param name="InPutJson"></param>
        public Result ParseAccelaJson(string InPutJson)

        {
            // remove  bad Accela data found in Conditions
            InPutJson = InPutJson.Replace("\\u0002", " ");
            var mParsingResult = dynamicParseAccelaJson(InPutJson);

            return mParsingResult;
        }

        public Result dynamicParseAccelaJson(string InPutJson)
        {
            // Disctionary for determining record type
            Dictionary<String, Object> mRecType = new Dictionary<string, object>();

            var serializer = new JavaScriptSerializer();

            // serializer.RegisterConverters(new[] { new DynamicJsonConverter() });

            ExpandoObject results = JsonConvert.DeserializeObject<ExpandoObject>(InPutJson);

            //   string json = results[0].ToString(); // suppose `dynamicObject` is your input
            //   IDictionary<string, string> dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(results);

            IDictionary<string, object> mDictionary = results;

            //  added 
            var jss = new JavaScriptSerializer();
            var dict = jss.Deserialize<Dictionary<string, object>>(InPutJson);

            // var place = dict["8"]; // "foobar"

            //  Dictionary<string, string> mRecordProperties;

            foreach (var mtest in dict) // mDictionary)
            {
                object mTestForms = mtest;

                object mTestObj;

                if (mtest.Key != "status")
                {
                    mCurrentRecord = MakeNewResultObject();

                    dict.TryGetValue(mtest.Key, out mTestObj);
                    Object mFormProcess = mTestObj;

                    ArrayList mDataArray = (ArrayList)mTestObj;

                    ArrayList mDataElement0 = new ArrayList { mDataArray[0] };

                    Dictionary<string, object> mElements = (Dictionary<string, object>)mDataElement0[0];

                    // need to get type before process custom forms 
                    // find the Type segment and process it, we need to have the customforms identifed before starting parsing elements.

                    object mTTypeObj = MakeNewResultObject();

                    mElements.TryGetValue("type", out mTTypeObj);

                    mRecType = (Dictionary<string, Object>)mTTypeObj;

                    var mTypeforRecord = DetermineRecordType(mRecType);

                    mCurrentRecord.ParseRecType = mTypeforRecord;

                    // mCurrentRecord.type = mRecType;



                    if (mCurrentRecord.hasErrors)
                    {
                        mCurrentRecord.parsingError = sberrors.ToString();
                        Console.WriteLine("Parsing Errors : \n " + sberrors.ToString());
                        return mCurrentRecord;
                    }

                    // build recordId detail 

                    Type mSourceObj = mCurrentRecord.GetType();

                    foreach (var mTestBObj in mElements)
                    {
                        //if (mCurrentRecord.hasErrors)
                        //{
                        //    mCurrentRecord.parsingError = sberrors.ToString();
                        //    Console.WriteLine("Parsing Errors : \n " + sberrors.ToString());
                        //    return mCurrentRecord;
                        //}

                        if (!mTestBObj.Value.GetType().Name.Contains("ArrayList") &&
                            !mTestBObj.Value.GetType().Name.Contains("Dictionary"))
                        {

                            Object mTRootFieldValue = null;

                            mElements.TryGetValue(mTestBObj.Key, out mTRootFieldValue);

                            if (mTestBObj.Key.ToUpper() == "ID")
                            {
                                mTRootFieldValue = mTRootFieldValue.ToString().Replace("MECKLENBURG-", "");
                            }

                            mCurrentRecord.CommonFields.Add(mTestBObj.Key, mTRootFieldValue);

                            if (mTestBObj.Key == "customId")
                            {
                                mCurrentRecord.altId = (string)mTRootFieldValue;
                            }
                        }
                        else
                        {
                            //  Object mInitType = null;

                            switch (mTestBObj.Key.ToUpper())
                            {
                                case "TYPE":
                                    //    // We should have a valid type by now. 
                                    //    if (mCurrentRecord.CustomFormDetails.Count is null)
                                    //    {
                                    //        sberrors.AppendLine("Error 'type' section of this record is missing '" +
                                    //                            mTestBObj.Key + "' Processing: \n" + InPutJson);
                                    //        mCurrentRecord.hasErrors = true;
                                    //    }
                                    //  ArrayList mResulttypeDataArray = (ArrayList)mTestBObj.Value;

                                    mCurrentRecord.Resulttype = (Dictionary<string, object>)mTestBObj.Value;

                                    break;

                                case "ADDRESSES":

                                    Object mInitAddresses = mTestForms;

                                    ArrayList mDataElement1 = new ArrayList { mDataArray[0] };

                                    Dictionary<string, object> mElementsb =
                                        (Dictionary<string, object>)mDataElement1[0];

                                    foreach (var mAddrTestBObj in mElementsb)
                                    {

                                        if (mAddrTestBObj.Value is String)
                                        {
                                            //Console.WriteLine(" Element: " + mTestBObj.Key + "Value: " +
                                            //                  (string)mTestBObj.ToString());
                                        }

                                        else
                                        {
                                            if (mAddrTestBObj.Key == "addresses")
                                            {
                                                object mAddrTestObject = null;

                                                object mAddrInitType = null;

                                                mElementsb.TryGetValue(mAddrTestBObj.Key, out mAddrTestObject);

                                                mRecType = (Dictionary<string, Object>)mAddrInitType;

                                                ArrayList maddrDataArray = (ArrayList)mAddrTestObject;

                                                ArrayList maddrDataElement0 = new ArrayList { maddrDataArray[0] };

                                                Dictionary<string, object> maddrElements =
                                                    (Dictionary<string, object>)maddrDataElement0[0];

                                                mCurrentRecord.Addresses.Add(maddrElements);

                                                //ParsedAddress mParsedAddresses = PaserRecordAddress(maddrElements);
                                                // // mCurrentRecord.Addresses = new List<Address>();
                                                // mCurrentRecord.Addresses.Add(mParsedAddresses);
                                                // //   Console.WriteLine("Addresses: " + mParsedAddresses.addressLine1);
                                            }
                                        }
                                    }

                                    break;

                                case "CONDITIONS":
                                    ArrayList mConditionsDataArray = (ArrayList)mTestBObj.Value;

                                    for (int indx = 0; indx < mConditionsDataArray.Count; indx++)
                                    {
                                        mCurrentRecord.conditions.Add((Dictionary<string, object>)mConditionsDataArray[indx]);
                                    }

                                    break;
                                case "CONTACTS":
                                    List<Contact> mContacts = new List<Contact>();

                                    Contact mContact = new Contact();
                                    ArrayList mdContactDataArray = (ArrayList)mTestBObj.Value;

                                    foreach (var mdContact in (ArrayList)mTestBObj.Value)
                                    {
                                        Dictionary<string, Object>
                                            mtContact = (Dictionary<string, Object>)mdContact;
                                        mCurrentRecord.Contacts.Add(mtContact);
                                        //mContact = ParserRecordContact(mtContact);

                                        //mContacts.Add(mContact);
                                    }

                                    //if (mContacts.Count > 0)
                                    //{
                                    //    mCurrentRecord.Contacts = mContacts;
                                    //}

                                    break;

                                case "CUSTOMFORMS":

                                    ArrayList mdCustomFormDataArray = (ArrayList)mTestBObj.Value;

                                    ModelCustomForms mCustFormsDetail = new ModelCustomForms();

                                    for (int indx = 0; indx < mdCustomFormDataArray.Count; indx++)
                                    {
                                        mCurrentRecord.CustomForms.Add((Dictionary<string, object>)mdCustomFormDataArray[indx]);

                                        //AccelaFormResult mFormResult = new AccelaFormResult();

                                        //mFormResult =
                                        //    ParserRecordCustomForms(
                                        //        (Dictionary<string, object>)mdCustomFormDataArray[indx]);

                                        //mCurrentRecord.CustomFormDetails.Add(mFormResult);

                                    }

                                    break;
                                case "CUSTOMTABLES":
                                    ArrayList mdCustomTablesDataArray = (ArrayList)mTestBObj.Value;

                                    ModelCustomTables mCustTableDetail = new ModelCustomTables();

                                    for (int indx = 0; indx < mdCustomTablesDataArray.Count; indx++)
                                    {
                                        mCurrentRecord.CustomTables.Add((Dictionary<string, object>)mdCustomTablesDataArray[indx]);

                                        //AccelaTableResult mtCustTableDetail = new AccelaTableResult();

                                        //mtCustTableDetail =
                                        ParserRecordCustomTables(
                                         (Dictionary<string, object>)mdCustomTablesDataArray[indx]);

                                        //mCurrentRecord.CustomTables.Add(mtCustTableDetail);
                                    }

                                    break;

                                case "OWNERS":

                                    List<Owner> mOwners = new List<Owner>();

                                    ArrayList mOwnerDataArray = (ArrayList)mTestBObj.Value;

                                    foreach (var mtArrayElement in mOwnerDataArray)
                                    {
                                        Owner mOwner = new Owner();
                                        Dictionary<string, Object> mtdict =
                                            (Dictionary<string, object>)mtArrayElement;

                                        mCurrentRecord.Owners.Add(mtdict);

                                        //mOwner = ParseRecordOwner(mtdict);
                                        //mOwners.Add(mOwner);
                                    }

                                    //if (mOwners.Count > 0)
                                    //{
                                    //    mCurrentRecord.Owners = mOwners;
                                    //}

                                    break;

                                case "PARCELS":

                                    List<Parcel> mParcels = new List<Parcel>();

                                    ArrayList mdtParcelDataArray = (ArrayList)mTestBObj.Value;

                                    foreach (var tparcel in mdtParcelDataArray)
                                    {
                                        Parcel mParcel = new Parcel();
                                        Dictionary<string, Object>
                                            mtParcel = (Dictionary<string, Object>)tparcel;

                                        mCurrentRecord.Parcels.Add(mtParcel);

                                        //mParcel = ParcelRecordParcel(mtParcel);

                                        //mParcels.Add(mParcel);
                                    }

                                    //if (mParcels.Count > 0)
                                    //{
                                    //    mCurrentRecord.Parcels = mParcels;
                                    //}

                                    break;

                                case "PROFESSIONALS":
                                    List<Professional> mProfessionals = new List<Professional>();
                                    Professional mProfessional = new Professional();

                                    ArrayList mdtProfessionalArray = (ArrayList)mTestBObj.Value;

                                    foreach (var tProfessional in mdtProfessionalArray)
                                    {
                                        // Parcel mParcel = new Parcel();
                                        Dictionary<string, Object>
                                            mtProfessionalDetail = (Dictionary<string, Object>)tProfessional;

                                        mCurrentRecord.professionals.Add(mtProfessionalDetail);


                                        //mProfessional = ParseRecordProfessional(mtProfessionalDetail);

                                        // mProfessionals.Add(mProfessional);

                                    }

                                    //if (mProfessionals.Count > 0)
                                    //{
                                    //    mCurrentRecord.professionals = mProfessionals;
                                    //}

                                    break;

                                case "REPORTEDCHANNEL":
                                    object mReportedChannnel = new ReportedChannel();

                                    Dictionary<string, Object>
                                        mtReportedChannelDetail = (Dictionary<string, Object>)mTestBObj.Value;

                                    //ArrayList mdtStatusArray = (ArrayList)mTestBObj.Value;

                                    mReportedChannnel = ProcessDataByClass(mReportedChannnel.GetType(),
                                        mtReportedChannelDetail);

                                    mCurrentRecord.reportedChannel = (ReportedChannel)mReportedChannnel;

                                    break;

                                case "STATUS":
                                    //  Status

                                    Object mStatus = new Status();
                                    Dictionary<string, Object>
                                        mtStatusDetail = (Dictionary<string, Object>)mTestBObj.Value;

                                    //ArrayList mdtStatusArray = (ArrayList)mTestBObj.Value;

                                    mStatus = ProcessDataByClass(mStatus.GetType(), mtStatusDetail);

                                    mCurrentRecord.Resultstatus = (Status)mStatus;
                                    break;

                                default:
                                    sberrors.AppendLine("Default value for data type = '" + mTestBObj.Key +
                                                        "' \n Processing data \n" + InPutJson);
                                    mCurrentRecord.hasErrors = true;
                                    break;

                            }
                        }
                    }
                }

            }


            if ((sberrors.Length > 0) || (mCurrentRecord.hasErrors))
            {
                mCurrentRecord.parsingError = sberrors.ToString();
                // Console.WriteLine("Parsing Errors : \n " + sberrors.ToString());
                mCurrentRecord.hasErrors = true;
            }

            return mCurrentRecord;
        }

        #region Utility Parsing Reoutines

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private Result MakeNewResultObject()
        {
            Result mnewCurrentRecord = new Result()
            {
                CommonFields = new Dictionary<string, object>(),
                Parcels = new List<Dictionary<string, object>>(),     //List<Parcels>();  
                professionals = new List<Dictionary<string, object>>(),
                Addresses = new List<Dictionary<string, object>>(), //List<ParsedAddress>(),
                Contacts = new List<Dictionary<string, object>>(),   // List<Contact>(),
                Owners = new List<Dictionary<string, object>>(),     // List<Owner>(),
                CustomForms = new List<Dictionary<string, object>>(),
                CustomTables = new List<Dictionary<string, object>>(),
                // type = new Dictionary<string, object>(),
                Resulttype = new Dictionary<string, object>(),
                conditions = new List<Dictionary<string, object>>()

            };

            return mnewCurrentRecord;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mInObject"></param>
        /// <returns></returns>
        private IDictionary<string, object> ParseObjectToDictionary(Dictionary<string, Object> mInObject)
        {
            //  IDictionary<string, Object> mNewDict  = mInObject;

            IDictionary<string, Object> mNewDict = mInObject;

            return mNewDict;

        }

        /// <summary>
        /// ProcessDataByClass(Type Clazz, Dictionary<string, object> DataSourceDictionary)
        /// </summary>
        /// <param name="Clazz"></param>
        /// <param name="DataSourceDictionary"></param>
        /// <returns>returns object of input type</returns>
        private object ProcessDataByClass(Type TargetedClass, Dictionary<string, object> DataSourceDictionary)
        {
            object theObject = Activator.CreateInstance(TargetedClass);

            System.Type myType = theObject.GetType();

            PropertyInfo[] props = myType.GetProperties();

            foreach (var prp in DataSourceDictionary)
            {
                Object mNewPropValue = new Object();

                DataSourceDictionary.TryGetValue(prp.Key, out mNewPropValue);

                if (mNewPropValue.GetType().Name.Contains("Dictionary"))
                {
                    Dictionary<String, Object> mtsubobj = (Dictionary<String, Object>)mNewPropValue;

                    String MatchKeyName = string.Empty;

                    Object mNewState = new Object();

                    // we need a dictionary value to proceed to parse sub types
                    if (mtsubobj.Count > 0)
                    {
                        switch (prp.Key)
                        {
                            case "state":
                                MatchKeyName = "state";
                                mNewState = new State();
                                mNewState = (State)ProcessDataByClass(mNewState.GetType(), mtsubobj);
                                break;
                            case "status":
                                MatchKeyName = "status";
                                mNewState = new Status();
                                mNewState = (Status)ProcessDataByClass(mNewState.GetType(), mtsubobj);
                                break;
                            case "streetsuffix":
                                MatchKeyName = "status";
                                mNewState = new StreetSuffix();
                                mNewState = (StreetSuffix)ProcessDataByClass(mNewState.GetType(), mtsubobj);
                                break;
                            case "mailAddress":
                                MatchKeyName = "mailAddress";
                                mNewState = new MailAddress();
                                mNewState = (MailAddress)ProcessDataByClass(mNewState.GetType(), mtsubobj);
                                break;
                            case "type":
                                MatchKeyName = "TypeResult";
                                mNewState = new TypeResult();
                                mNewState = (TypeResult)ProcessDataByClass(mNewState.GetType(), mtsubobj);
                                break;
                            case "country":
                                MatchKeyName = "country";
                                mNewState = new Country();
                                mNewState = (Country)ProcessDataByClass(mNewState.GetType(), mtsubobj);
                                break;
                        }

                        //Combine the PGP.Value dictionary details we have. 

                        System.Type mynewType = theObject.GetType();
                        PropertyInfo piNewInstance = mynewType.GetProperty(MatchKeyName);

                        if (!(piNewInstance is null))
                        {
                            piNewInstance.SetValue(theObject, mNewState);
                        }
                    }
                }
                else
                {
                    // process the current data as a key value pair

                    PropertyInfo piInstance = myType.GetProperty(prp.Key);
                    if (piInstance != null)
                    {

                        if (piInstance.PropertyType.Name == "String")
                        {
                            piInstance.SetValue(theObject, mNewPropValue.ToString());
                        }
                        else
                        {
                            piInstance.SetValue(theObject, mNewPropValue);
                        }

                    }
                    else
                    {
                        sberrors.AppendLine(TargetedClass + " Unknown field value for " + prp.Key + " Value:" +
                                            mNewPropValue.ToString());
                    }
                }
            }

            return theObject;
        }

        #endregion

        #region Determine Record type

        /// <summary>
        /// 
        /// </summary>
        /// <param name="InputDetail"></param>
        /// <returns></returns>
        private string DetermineRecordType(Dictionary<string, Object> InputDetail)
        {
            object mtRecType = null;
            object mtValueType = null;


            foreach (var mparserec in InputDetail)
            {
                /* 
                "type":
                {
                    "module": "CodeEnforcement",
                    "value": "CodeEnforcement/Plan Review/Revision/Commercial RTAP",
                    "type": "Plan Review",
                    "text": "Commercial RTAP",
                    "alias": "Commercial RTAP",
                    "group": "CodeEnforcement",
                    "category": "Commercial RTAP",
                    "subType": "Revision",
                    "id": "CodeEnforcement-Plan.cReview-Revision-Commercial.cRTAP"
                },
                */

                if (mparserec.Key == "text")
                {
                    InputDetail.TryGetValue(mparserec.Key, out mtRecType);

                    if (mtRecType is null)
                    {
                        mtRecType = "Type_Unknown_Error";
                    }
                }

                if (mparserec.Key == "value")
                {
                    InputDetail.TryGetValue(mparserec.Key, out mtValueType);
                    mCurrentRecord.name = (string)mtValueType;
                }
            }

            return mtRecType.ToString();
        }

        #endregion

        #region Parsing key values

        /// <summary>
        /// 
        /// </summary>
        /// <param name="indata"></param>
        /// <returns></returns>
        public AccelaTableResult CustomFormsLoadAndProcess(string indata)
        {
            try
            {
                var jss = new JavaScriptSerializer();
                ExpandoObject results = JsonConvert.DeserializeObject<ExpandoObject>(indata);
                IDictionary<string, object> mDictionary = results;
                var dict = jss.Deserialize<Dictionary<string, object>>(indata);

                AccelaTableResult mTable = new AccelaTableResult();

                foreach (var sample in dict)
                {
                    if (sample.Key == "result")
                    {
                        object newArray;

                        mTable.id = sample.Key;

                        dict.TryGetValue(sample.Key, out newArray);

                        ArrayList mdRows = (ArrayList)newArray;

                        Rows mRows = new Rows(mdRows);

                        mTable.rows.Add(mRows);
                        mTable.rows.Add(mRows);
                    }


                }
                return mTable;
            }
            catch (Exception ex)
            {
                throw new Exception(" parsing errors " + ex.Message + " additional errors found + " +
                                    mCurrentRecord.parsingError);
            }
        }

        /// <summary>
        /// ParserRecordCustomForms(Dictionary<string, object> customFormsDetail)
        /// </summary>
        /// <param name="CustomFormsDetail"></param>
        /// <returns></returns>
        public AccelaFormResult ParserRecordCustomForms(Dictionary<string, object> customFormsDetail)
        {
            //object mCustomForm = Activator.CreateInstance(mCurrentRecord.CustomFormDetails.objectValues.GetType());

            // ModelCustomForms mCustomForm = new ModelCustomForms();

            object keyId;

            try
            {

                customFormsDetail.TryGetValue("id", out keyId);

                AccelaFormResult mForm = new AccelaFormResult();
                mForm.Id = keyId.ToString();
                foreach (var prp in customFormsDetail)
                {
                    AccelaFormField newField;
                    // If all we have is an Id value  return it otherwise hold it. 
                    if (prp.Key == "id" && customFormsDetail.Count == 1)
                    {
                        newField = new AccelaFormField(prp.Key, prp.Key, (string)prp.Value, null);
                    }
                    else
                    {
                        if (prp.Value == null)
                        {
                            newField = new AccelaFormField(prp.Key, prp.Key, (string)prp.Value, null);
                        }
                        else
                        {
                            if (!prp.Value.GetType().ToString().Contains("Dictionary"))
                            {
                                newField = new AccelaFormField(prp.Key, prp.Key, (string)prp.Value, null);
                            }
                            else
                            {

                                // need an object 
                                AccelaTableField mWorkTasksAccelaTableField = new AccelaTableField();
                                List<AccelaFormsFieldOption> mWorkTasksAccelaTableFieldOptions =
                                    new List<AccelaFormsFieldOption>();

                                Dictionary<string, object> mCustomListObjects =
                                    (Dictionary<string, object>)(prp.Value);

                                // ProcessDataByClass(  mWorkTasksAccelaTableFieldOPtions.GetType(), Dictionary<string, object> DataSourceDictionary)
                                var optionsResult = ProcessDataByClass(mWorkTasksAccelaTableFieldOptions.GetType(),
                                    mCustomListObjects);
                                mWorkTasksAccelaTableFieldOptions = (List<AccelaFormsFieldOption>)optionsResult;

                                newField = new AccelaFormField(prp.Key, prp.Key, null,
                                    mWorkTasksAccelaTableFieldOptions);
                            }
                        }
                    }


                    mForm.fields.Add(newField);
                }

                return mForm;

            }
            catch (Exception ex)
            {
                throw new Exception("In ParserRecordCustomForms : " + ex, ex.InnerException);
            }

        }

        /// <summary>
        /// ParserRecordCustomTables
        /// </summary>
        /// <param name="customTablesDetail"></param>
        /// <returns></returns>
        private void ParserRecordCustomTables(Dictionary<string, object> customTablesDetail)
        {
            // This is using the MarkUp structure design used for Task updating for the row creation. 
            // ModelCustomTables mCustomTable = new ModelCustomTables();

            AccelaTableResult mTable = new AccelaTableResult();

            foreach (var prp in customTablesDetail)
            {
                if (prp.Key != "rows")
                {
                    object keyId;
                    customTablesDetail.TryGetValue(prp.Key, out keyId);
                    mTable.id = (string)keyId;
                }
                else
                {

                    // if count > 1 then object has "rows" then needs to be grab the 2nd element. 
                    // then use field object from markup. 
                    foreach (var prrp in customTablesDetail)
                    {
                        if (prrp.Key == "id")
                        {
                            object keyId;
                            customTablesDetail.TryGetValue(prrp.Key, out keyId);
                            mTable.id = (string)keyId;
                        }
                        if (prrp.Key == "rows")
                        {
                            List<AccelaTableField> mAccelaTableFields = new List<AccelaTableField>();
                            List<AccelaTableFieldOption> mAccelaTableFieldOptions = new List<AccelaTableFieldOption>();
                            Object mArrayPart;
                            customTablesDetail.TryGetValue(prrp.Key, out mArrayPart);
                            ArrayList mdRows = (ArrayList)mArrayPart;
                            //for (int Rindx = 0; indx < mdRows.Count; indx++)
                            //{
                            //   object  mdrowsdetail = (Dictionary<string,object>) mdRows[Rindx];
                            Rows mRows = new Rows(mdRows);
                            mTable.rows.Add(mRows);
                            // }
                        }
                    }
                }

            }

            if (mTable.id.Contains("cTASK.cACTIVATION"))
            {
                mCurrentRecord.TaskActivator = mTable.id;
            }

        }

        private Professional ParseRecordProfessional(Dictionary<string, Object> mProfessionalDetail)
        {
            Professional mProfessional = new Professional();
            System.Type t = mProfessional.GetType();
            PropertyInfo[] props = t.GetProperties();
            Type[] mCurrentTypes = t.GetNestedTypes();

            foreach (var prp in mProfessionalDetail)
            {
                PropertyInfo piFieldInstance = t.GetProperty(prp.Key);
                if (prp.Value.GetType().Name.Contains("Dictionary"))
                {
                    object recid = prp.Value;
                    Dictionary<string, Object> mtrec = (Dictionary<string, object>)recid;
                    Object mTProfObject = null;
                    Object mTProfPartial = new Object();
                    switch (prp.Key)
                    {
                        case "recordId":
                            mTProfObject = new RecordId();
                            mTProfPartial = ProcessDataByClass(mTProfObject.GetType(), mtrec);
                            mProfessional.recordId = (RecordId)mTProfPartial;
                            break;

                        case "licenseType":
                            mTProfObject = new Licensetype();
                            mTProfPartial = ProcessDataByClass(mTProfObject.GetType(), mtrec);
                            mProfessional.licenseType = (Licensetype)mTProfPartial;
                            break;
                        case "state":
                            mTProfObject = new State();
                            mTProfPartial = ProcessDataByClass(mTProfObject.GetType(), mtrec);
                            mProfessional.state = (State)mTProfPartial;
                            break;
                    }
                }
                else
                {
                    Object mNewDetail = null;
                    mProfessionalDetail.TryGetValue(prp.Key, out mNewDetail);
                    PropertyInfo piInstance = t.GetProperty(prp.Key);

                    if (piInstance != null)
                    {
                        piInstance.SetValue(mProfessional, mNewDetail);
                    }
                    else
                    {
                        sberrors.AppendLine("Professional parser Unknown field value for " + prp.Key);
                    }
                }
            }
            return mProfessional;
        }


        private Parcel ParcelRecordParcel(Dictionary<string, Object> mParcelDetail)
        {
            Parcel mParcel = new Parcel();
            System.Type t = mParcel.GetType();
            PropertyInfo[] props = t.GetProperties();
            Type[] mCurrentTypes = t.GetNestedTypes();

            foreach (var prp in mParcelDetail)
            {
                PropertyInfo piFieldInstance = t.GetProperty(prp.Key);
                if (prp.Value.GetType().Name.Contains("Dictionary"))
                {
                    object recid = prp.Value;
                    Dictionary<string, Object> mtrec = (Dictionary<string, object>)recid;
                    Object mTParcelObject = null;
                    Object mTParcelPartial = new Object();
                    switch (prp.Key)
                    {
                        case "status":
                            mTParcelObject = new Status();
                            mTParcelPartial = ProcessDataByClass(mTParcelObject.GetType(), mtrec);
                            mParcel.status = (Status)mTParcelPartial;
                            break;
                    }
                }
                else
                {
                    Object mNewDetail = null;
                    mParcelDetail.TryGetValue(prp.Key, out mNewDetail);
                    PropertyInfo piInstance = t.GetProperty(prp.Key);

                    if (piInstance != null)
                    {
                        piInstance.SetValue(mParcel, mNewDetail);
                    }
                    else
                    {
                        sberrors.AppendLine(" Parcel parser Unknown field value for " + prp.Key);
                    }
                }
            }
            return mParcel;
        }


        private Owner ParseRecordOwner(Dictionary<string, Object> OwnerDetail)
        {
            Owner mOwner = new Owner();

            System.Type t = mOwner.GetType();
            PropertyInfo[] props = t.GetProperties();

            Type[] mCurrentTypes = t.GetNestedTypes();

            foreach (var prp in OwnerDetail)
            {
                PropertyInfo piFieldInstance = t.GetProperty(prp.Key);

                if (prp.Value.GetType().Name.Contains("Dictionary"))
                {
                    object recid = prp.Value;
                    Dictionary<string, Object> mtrec = (Dictionary<string, object>)recid;
                    Object mTOwnerObject = null;
                    Object mtOwnerPartial = new Object();

                    switch (prp.Key)
                    {
                        case "mailAddress":
                            mTOwnerObject = new MailAddress();
                            mOwner.mailAddress = (MailAddress)ProcessDataByClass(mTOwnerObject.GetType(), mtrec);
                            break;

                        case "status":
                            mTOwnerObject = new Status();
                            mOwner.status = (Status)ProcessDataByClass(mTOwnerObject.GetType(), mtrec);
                            break;

                            //case "state":
                            //    mTOwnerObject = new State();
                            //    mOwner.mailAddress.State = (State) ProcessDataByClass(mTOwnerObject.GetType(), mtrec);
                            //    break;
                    }
                }
                else
                {
                    Object mNewDetail = null;
                    OwnerDetail.TryGetValue(prp.Key, out mNewDetail);
                    PropertyInfo piInstance = t.GetProperty(prp.Key);

                    if (piInstance != null)
                    {
                        piInstance.SetValue(mOwner, mNewDetail);
                    }
                    else
                    {
                        sberrors.AppendLine("OWner Parser Unknown field value for " + prp.Key);
                    }
                }
            }
            return mOwner;
        }

        /// <summary>
        /// PaserRecordAddress(Dictionary<string, Object> AddressDetail)
        /// </summary>
        /// <param name="AddressDetail"></param>
        /// <returns></returns>
        private ParsedAddress PaserRecordAddress(Dictionary<string, Object> AddressDetail)
        {
            ParsedAddress mAddresses = new ParsedAddress();

            System.Type t = mAddresses.GetType();
            PropertyInfo[] props = t.GetProperties();

            foreach (var prp in AddressDetail)
            {
                PropertyInfo piFieldInstance = t.GetProperty(prp.Key);

                if (prp.Value.GetType().Name.Contains("Dictionary"))
                {
                    object recid = prp.Value;
                    Dictionary<string, Object> mtrec = (Dictionary<string, object>)recid;
                    Object mTOwnerObject = null;
                    Object mtOwnerPartial = new Object();

                    switch (prp.Key)
                    {
                        case "recordId":
                            mTOwnerObject = new RecordId();
                            mtOwnerPartial = ProcessDataByClass(mTOwnerObject.GetType(), mtrec);
                            break;

                        case "status":
                            mTOwnerObject = new Status();
                            mtOwnerPartial = ProcessDataByClass(mTOwnerObject.GetType(), mtrec);
                            break;

                        case "state":
                            mTOwnerObject = new State();
                            mtOwnerPartial = ProcessDataByClass(mTOwnerObject.GetType(), mtrec);
                            break;

                        case "streetSuffix":
                            mTOwnerObject = new StreetSuffix();
                            mtOwnerPartial = ProcessDataByClass(mTOwnerObject.GetType(), mtrec);
                            break;

                        case "country":
                            mTOwnerObject = new Country();
                            mtOwnerPartial = ProcessDataByClass(mTOwnerObject.GetType(), mtrec);
                            break;

                    }

                    PropertyInfo piInstance = t.GetProperty(prp.Key);

                    if (piInstance != null)
                    {
                        piInstance.SetValue(mAddresses, mtOwnerPartial);
                    }
                    else
                    {
                        sberrors.AppendLine("Address parser Unknown field value for " + prp.Key + " Value:" + (string)mtOwnerPartial.ToString());
                    }
                }
                else
                {
                    Object mNewDetail = new Object();
                    AddressDetail.TryGetValue(prp.Key, out mNewDetail);
                    PropertyInfo piInstance = t.GetProperty(prp.Key);

                    if (piInstance != null)
                    {
                        piInstance.SetValue(mAddresses, mNewDetail);
                    }
                    else
                    {
                        sberrors.AppendLine("Addresses parser Unknown field value for " + prp.Key + " Value:" + mNewDetail.ToString());
                    }
                }
            }

            return mAddresses;
        }

        /// <summary>
        /// ContactDetail
        /// </summary>
        /// <param name="ContactDetail"></param>
        /// <returns></returns>
        private Contact ParserRecordContact(Dictionary<string, Object> ContactDetail)
        {
            Contact mContact = new Contact();

            System.Type t = mContact.GetType();
            PropertyInfo[] props = t.GetProperties();

            foreach (var prp in ContactDetail)
            {
                PropertyInfo piFieldInstance = t.GetProperty(prp.Key);

                if (prp.Value.GetType().Name.Contains("Dictionary"))
                {
                    object recid = prp.Value;
                    Dictionary<string, Object> mtrec = (Dictionary<string, object>)recid;
                    Object mTOwnerObject = null;
                    Object mtOwnerPartial = new Object();

                    switch (prp.Key)
                    {
                        case "type":
                            mTOwnerObject = new TypeResult();
                            mContact.typeContact = (TypeResult)ProcessDataByClass(mTOwnerObject.GetType(), mtrec);
                            break;
                        case "recordId":
                            mTOwnerObject = new RecordId();
                            mContact.recordId = (RecordId)ProcessDataByClass(mTOwnerObject.GetType(), mtrec);
                            //  mContact.recordId = (RecordId)mtOwnerPartial;
                            break;

                        case "address":
                            mTOwnerObject = new ParsedAddress();
                            mtOwnerPartial = ProcessDataByClass(mTOwnerObject.GetType(), mtrec);
                            mContact.address = (ParsedAddress)mtOwnerPartial;
                            break;

                        case "status":
                            mTOwnerObject = new Status();
                            mContact.status = (Status)ProcessDataByClass(mTOwnerObject.GetType(), mtrec);

                            break;
                    }
                }
                else
                {
                    Object mNewDetail = new Object();
                    ContactDetail.TryGetValue(prp.Key, out mNewDetail);
                    PropertyInfo piInstance = t.GetProperty(prp.Key);

                    if (piInstance != null)
                    {
                        piInstance.SetValue(mContact, mNewDetail);
                    }
                    else
                    {
                        sberrors.AppendLine("Contact Unknown field value for " + prp.Key + " Value:" + mNewDetail.ToString());
                    }
                }
            }
            return mContact;
        }

        #endregion

    }

}

