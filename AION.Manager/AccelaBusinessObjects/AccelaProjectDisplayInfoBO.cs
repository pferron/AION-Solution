using AION.BL.Common.AccelaDataLoaders;
using AION.Manager.Adapters;
using Meck.Shared.MeckDataMapping;
using Meck.Shared.PosseToAccela;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AION.Manager.AccelaBusinessObjects
{
    public class AccelaProjectDisplayInfoBO : AccelaBusinessObjectBase
    {

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

            // Get the Paid status for the current record

            mAccelaProjectDisplayInfo.IsPaidStatus = new AccelaBOAdapter().GetRecordPaidStatusFromConditons(mCurrentRecordId.ToString());

            List<Dictionary<string, object>> mCommonContacts = mIncomingAccelaRecord.Contacts;
            mAccelaProjectDisplayInfo.Contacts = new List<contactDetail>();

            foreach (var contactDictionary in mCommonContacts)
            {
                var nContactDetail = new ContactDetailBO().ParserRecordContact(mCurrentRecordId.ToString(), contactDictionary, mAccelaAIONMap, false);


                mAccelaProjectDisplayInfo.Contacts.Add(nContactDetail);
            }


            // changed to include Residental RTAp

            // var Professional = mAccelaAIONMap.FindAll(x => x.ACCELA_DATA_TYP_DESC == "List<ProfessionalsDetails").ToList(); ;

            var Professional = mAccelaAIONMap.FindAll(x => x.Meck_DATA_TYP_DESC == "List<Professionals>").ToList(); 

            if (Professional.Count == 1 && (mIncomingAccelaRecord.ParseRecType == "Commercial RTAP" || mIncomingAccelaRecord.ParseRecType == "Residential RTAP"))
            {
                List<Dictionary<string, object>> mLicensedProfessionals = mIncomingAccelaRecord.professionals;
                mAccelaProjectDisplayInfo.Professional = new List<ProfessionalDetail>();
                foreach (var professionalDictionary in mLicensedProfessionals)
                {
                    var nProfessionalDetail =
                        new ProfessionalDetailBO().ParserRecordDisplayProfessional(professionalDictionary);
                    mAccelaProjectDisplayInfo.Professional.Add(nProfessionalDetail);
                }

                //  var noprofmap = mAccelaAIONMap.FindAll(x => x.Meck_DATA_TYP_DESC != "List<ProfessionalsDetails")
                var noprofmap = mAccelaAIONMap.FindAll(x => x.ACCELA_DATA_TYP_DESC != "List<Professionals>")

                    .ToList();
                if (noprofmap.Count > 0)
                {
                    mAccelaAIONMap = noprofmap;
                }
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
                            Type tAccelaProjectModel = mAccelaProjectDisplayInfo.GetType();
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
                                                        if (maprecord.Meck_DATA_TYP_DESC.ToUpper().Contains("LIST") || maprecord.ACCELA_FIELD_NM.ToUpper().Equals("ID"))
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

                                case "PROFESSIONALS":
                                    List<Dictionary<string, object>> searchProfessionalsFormsResult =
                                        new List<Dictionary<string, object>>();

                                    if (!String.IsNullOrWhiteSpace(maprecord.Meck_FIELD_NM))
                                    {
                                        if (mIncomingAccelaRecord.professionals.Exists(x =>
                                            x.ContainsKey(maprecord.ACCELA_FIELD_NM)))
                                        {
                                            searchProfessionalsFormsResult = mIncomingAccelaRecord.professionals
                                                    .FindAll(x => x.ContainsKey(maprecord.ACCELA_FIELD_NM))
                                                    .ToList();

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

                    sbAionMapTrap.AppendLine(ErrMsg);
                }
            }


            return mAccelaProjectDisplayInfo;
        }


        public AccelaProjectDisplayInfo GenerateDisplayOnlyInformation(AccelaProjectModel accelaProjectModel)
        {
            string designers = string.Empty;
            foreach (var contact in accelaProjectModel.Contacts)
            {
                if (contact.ContactType == "Property Owner")
                {
                    accelaProjectModel.DisplayOnlyInformation.PropertyOwnerAddress = contact.AddressCustomer + " " + contact.City + ", " + contact.State + " " + contact.ZipCodeFirstPart;
                    accelaProjectModel.DisplayOnlyInformation.PropertyOwnerAutoEmail = "";
                    accelaProjectModel.DisplayOnlyInformation.PropertyOwnerEmail = contact.EmailAddress;
                    accelaProjectModel.DisplayOnlyInformation.PropertyOwnerName = contact.FirstName + " " + contact.LastName
                        + (string.IsNullOrWhiteSpace(contact.BusinessName) ? "" : ", Business Name - " + contact.BusinessName);
                    accelaProjectModel.DisplayOnlyInformation.PropertyOwnerPhone = contact.PhoneNumber + (string.IsNullOrWhiteSpace(contact.PhoneNumberExt) ? "" : " Ext." + contact.PhoneNumberExt);
                    accelaProjectModel.DisplayOnlyInformation.PropertyOwnerAutoEmail = string.IsNullOrWhiteSpace(contact.Notify) ? "N" : contact.Notify;

                }

                if (contact.ContactType == "Project Manager")
                {
                    accelaProjectModel.DisplayOnlyInformation.PropertyManagerEmail = contact.EmailAddress;
                    accelaProjectModel.DisplayOnlyInformation.PropertyManagerName = contact.FirstName + " " + contact.LastName
                        + (string.IsNullOrWhiteSpace(contact.BusinessName) ? "" : ", Business Name - " + contact.BusinessName);
                    accelaProjectModel.DisplayOnlyInformation.PropertyManagerPhone = contact.PhoneNumber + (string.IsNullOrWhiteSpace(contact.PhoneNumberExt) ? "" : " Ext." + contact.PhoneNumberExt);
                }

            }
            accelaProjectModel.DisplayOnlyInformation.Designers = designers;

            //string sealHolders = string.Empty;
            //foreach (var professional in accelaProjectModel.Professionals)
            //{
            //    string name = professional.FirstName + " " + professional.LastName;
            //    if (sealHolders.Contains(name) == false)
            //    {
            //        sealHolders += name + ";";
            //    }
            //}
            //accelaProjectModel.DisplayOnlyInformation.SealHolders = sealHolders;

            if (accelaProjectModel.IsPreliminaryMeetingRequested)
            {
                accelaProjectModel.DisplayOnlyInformation = GetPreliminaryMeetingDisplayInfo(accelaProjectModel);

            }
            accelaProjectModel.DisplayOnlyInformation = GetDesigners(accelaProjectModel.DisplayOnlyInformation, accelaProjectModel.Contacts);
            accelaProjectModel.DisplayOnlyInformation = GetArchitects(accelaProjectModel.DisplayOnlyInformation, accelaProjectModel.Professionals);

            return accelaProjectModel.DisplayOnlyInformation;
        }

        /// <summary>
        /// Exchange and add the data as needed for the display only info for Preliminary meeting application detail
        /// </summary>
        /// <param name="accelaProjectModel"></param>
        /// <returns></returns>
        public AccelaProjectDisplayInfo GetPreliminaryMeetingDisplayInfo(Meck.Shared.MeckDataMapping.AccelaProjectModel accelaProjectModel)
        {
            AccelaProjectDisplayInfo info = accelaProjectModel.DisplayOnlyInformation;

            info = GetPrelimProjectSummary(info, accelaProjectModel.PrelimProjectSummary);
            info = GetPrelimGeneralInfo(info, accelaProjectModel.PrelimGeneralInfo);
            info = GetPrelimMeetingAgenda(info, accelaProjectModel.PrelimMeetingAgenda);
            info = GetPrelimProposedWork(info, accelaProjectModel.PrelimProposedWork);
            info = GetPrelimSystemInfo(info, accelaProjectModel.PrelimSystemInfo);
            info = GetPrelimBIMProjectDelivery(info, accelaProjectModel.PrelimBIMProjectDelivery);
            info = GetPrelimTypeOfWork(info, accelaProjectModel.PrelimTypeOfWork);
            info = GetPrelimMeetingDetail(info, accelaProjectModel.PrelimMeetingDetail);

            return info;
        }

        public AccelaProjectDisplayInfo GetPrelimProjectSummary(AccelaProjectDisplayInfo info, PrelimProjectSummaryObj projectSummary)
        {
            //PrelimProjectSummaryObj 
            //public string IncludesAfforableOrWorkforceHousing { get; set; }
            //public string WorkforceHousingUnits { get; set; }
            //public string id { get; set; }
            //public string AfforableHousingUnits { get; set; }
            info.IsAffordableHousing = projectSummary.IncludesAfforableOrWorkforceHousing;

            return info;
        }

        public AccelaProjectDisplayInfo GetPrelimGeneralInfo(AccelaProjectDisplayInfo info, PrelimGeneralInfoObj generalInfo)
        {
            //PrelimGeneralInfoObj 
            //public string BuildingCode { get; set; }
            //public string PropertyType { get; set; }
            //public string id { get; set; }
            //public string ReviewType { get; set; }
            info.BuildingCodeVersion = generalInfo.BuildingCode;
            return info;
        }

        public AccelaProjectDisplayInfo GetPrelimMeetingAgenda(AccelaProjectDisplayInfo info, PrelimMeetingAgendaObj meetingAgenda)
        {
            //PrelimMeetingAgendaObj
            //     public string AgendaAmenities { get; set; }
            //public string AgendaAmenitiesLocation { get; set; }
            //public string AgendaUpfitType { get; set; }
            //public string AgendaElectricalSystemType { get; set; }
            //public string AgendaPlumbingType { get; set; }
            //public string AgendaAmenitiesDesign { get; set; }
            //public string AgendaParkingGarage { get; set; }
            //public string Agenda { get; set; }
            //public string id { get; set; }
            //public string AgendaSpecialWaste { get; set; }
            info.Agenda = meetingAgenda.Agenda;
            return info;
        }

        public AccelaProjectDisplayInfo GetPrelimProposedWork(AccelaProjectDisplayInfo info, PrelimProposedWorkObj proposedWork)
        {
            //PrelimProposedWorkObj
            //     public string ProposedScopeOfWork { get; set; }
            //public string id { get; set; }
            info.ScopeOfWorkOverall = proposedWork.ProposedScopeOfWork;
            return info;
        }

        public AccelaProjectDisplayInfo GetPrelimSystemInfo(AccelaProjectDisplayInfo info, PrelimSystemInfoObj systemInfo)
        {
            //PrelimSystemInfoObj
            //    public int CurrentReviewCycle { get; set; }
            //public string ClonedFromProjectNumber { get; set; }
            //public string GISAddressCode { get; set; }
            //public string IsRTAP { get; set; }
            //public decimal EstimatedFees { get; set; }
            //public string TaxJurisdiction { get; set; }
            //public string id { get; set; }
            //public string FIFO { get; set; }
            //public string RecordLocation { get; set; }
            return info;
        }

        public AccelaProjectDisplayInfo GetPrelimBIMProjectDelivery(AccelaProjectDisplayInfo info, PrelimBIMProjectDeliveryObj prelimBIMProjectDelivery)
        {
            //PrelimBIMProjectDeliveryObj
            //public string ProjectDeliveryMethodDesignBuild { get; set; }
            //public string ProjectDeliveryMethodCMOwnerAgent { get; set; }
            //public string BIMDisciplinesElec { get; set; }
            //public string ProjectDeliveryMethodFastTrack { get; set; }
            //public string ProjectDeliveryMethodOther { get; set; }
            //public string ProjectDeliveryMethodDesignBidBuild { get; set; }
            //public string ProjectDeliveryMethodIPDOrVariation { get; set; }
            //public string PDMOtherDescription { get; set; }
            //public string PDMDisciplinesDesignAssistMech { get; set; }
            //public string PDMDisciplinesDesignBuildArchStruct { get; set; }
            //public string BIMDisciplinesArch { get; set; }
            //public string ProjectDeliveryMethodDesignAssist { get; set; }
            //public string PDMDisciplinesDesignBuildMech { get; set; }
            //public string PDMDisciplinesDesignBuildPlumb { get; set; }
            //public string PDMDisciplinesDesignAssistArchStruct { get; set; }
            //public string PDMDisciplinesDesignAssistElec { get; set; }
            //public string ProjectIsBim { get; set; }
            //public string PDMDisciplinesDesignBuildElec { get; set; }
            //public string id { get; set; }
            //public string BIMDisciplinesStruct { get; set; }
            //public string PDMDisciplinesDesignAssistPlumb { get; set; }
            //public string BIMDisciplinesMech { get; set; }
            //public string BIMDisciplinesPlumb { get; set; }

            //TODO: jcl delivery method and BIM
            info.BIMDesignDiscipline = prelimBIMProjectDelivery.PDMDisciplinesDesignBuildArchStruct;
            info.IsBIM = prelimBIMProjectDelivery.ProjectIsBim;
            return info;
        }
        public AccelaProjectDisplayInfo GetPrelimTypeOfWork(AccelaProjectDisplayInfo info, PrelimTypeOfWorkObj prelimTypeOfWork)
        {
            //PrelimTypeOfWorkObj
            //     public string TypeOfWorkUpfit { get; set; }
            //public string TypeOfWorkNewConFull { get; set; }
            //public string TypeOfWorkPreEngMetalBldgOption { get; set; }
            //public string TypeOfWorkDayCare { get; set; }
            //public string TypeOfWorkNewConShellFootFoundPrev { get; set; }
            //public string TypeOfWorkNewConShellFootFoundCore { get; set; }
            //public string TypeOfWorkProCert { get; set; }
            //public string TypeOfWorkChangeOfUse { get; set; }
            //public string TypeOfWorkNewConFootFound { get; set; }
            //public string TypeOfWorkNewConShellFootFound { get; set; }
            //public string TypeOfWorkPreEngMetalBldg { get; set; }
            //public string TypeOfWorkNewConsShellFootFoundCorePrev { get; set; }
            //public string TypeOfWorkAddition { get; set; }
            //public string id { get; set; }
            //public string TypeOfWorkPrevOccupiedBldg { get; set; }
            info.TypeOfConstruction = "";
            info.TypeOfWork = "";

            return info;
        }
        public AccelaProjectDisplayInfo GetPrelimMeetingDetail(AccelaProjectDisplayInfo info, PrelimMeetingDetailObj prelimMeetingDetail)
        {
            //PrelimMeetingDetailObj
            //     public DateTime RequestedEndDateRange { get; set; }
            //public DateTime RequestedBeginDateRange { get; set; }
            //public string id { get; set; }
            //public int NumberOfAttendees { get; set; }
            info.RequestedMeetingTime = prelimMeetingDetail.RequestedBeginDateRange.ToShortDateString()
                + " - " + prelimMeetingDetail.RequestedEndDateRange.ToShortDateString();
            info.NumOfAttendees = prelimMeetingDetail.NumberOfAttendees.ToString();

            return info;
        }
        public AccelaProjectDisplayInfo GetDesigners(AccelaProjectDisplayInfo info, List<contactDetail> contacts)
        {
            string designers = string.Empty;
            string designersemail = string.Empty;
            string designersphone = string.Empty;
            string designerautoemail = string.Empty;

            foreach (contactDetail contact in contacts)
            {
                if (contact.ContactType.Contains("Designer"))
                {
                    string name = contact.FirstName + " " + contact.LastName;
                    if (designers.Contains(name) == false)
                    {
                        designers += name + ";";
                        designersemail += contact.EmailAddress + ";";
                        designersphone += contact.PhoneNumber + " " + contact.PhoneNumberExt + ";";
                        designerautoemail += string.IsNullOrWhiteSpace(contact.Notify) ? "N" : contact.Notify + ";";
                    }
                }
            }
            //info.Designers = designers;
            info.ArchDesContactName = designers;
            info.ArchDesContactEmail = designersemail;
            info.ArchDesContactPhone = designersphone;
            info.ArchDesAutoEmail = designerautoemail;
            return info;
        }

        public AccelaProjectDisplayInfo GetArchitects(AccelaProjectDisplayInfo info, List<professionalDetail> professionals)
        {
            string architects = string.Empty;
            string architectsemail = string.Empty;
            string architectsphone = string.Empty;
            string architectlicenseboard = string.Empty;
            string architectlicensenum = string.Empty;

            foreach (professionalDetail architect in professionals)
            {
                if (architect.LicenseType.Contains("Architect"))
                {
                    string name = architect.FirstName + " " + architect.LastName;
                    if (architects.Contains(name) == false)
                    {
                        architects += name + ";";
                        architectsemail += architect.Email + ";";
                        architectsphone += architect.Phone1 + ";";
                        architectlicenseboard += architect.State + ";";
                        architectlicensenum += architect.LicenseNumber + ";";

                    }
                }
            }
            info.ArchDesContactName += architects;
            info.ArchDesContactEmail += architectsemail;
            info.ArchDesContactPhone += architectsphone;
            info.ArchDesLicenseBoard = architectlicenseboard;
            info.ArchDesLicenseNum = architectlicensenum;
            return info;
        }

    }
}