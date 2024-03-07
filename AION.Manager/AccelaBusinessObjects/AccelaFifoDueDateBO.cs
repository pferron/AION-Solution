using Meck.Shared.Accela.ParserModels;
using Meck.Shared.MeckDataMapping;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AION.Manager.AccelaBusinessObjects
{
    public class AccelaFifoDueDateBO : AccelaBusinessObjectBase
    {
        public DateTime? GetFifoDueDateFromCustomTable(
            AccelaRecordModel recorddata,
            List<MeckAccelaDataMap> mAccelaAIONMap,
            int cycleNumber)
        {
            DateTime? fifoDueDate = null;

            sbAionMapTrap = new StringBuilder();
            AccelaProjectModel mAccelaProjectModel = new AccelaProjectModel();

            foreach (var mIncomingAccelaRecord in recorddata.result)
            {
                var mNewPosseAccelaMap = mAccelaAIONMap.FindAll(x => x.ACCELA_OBJ_TYP_DESC != "Contacts").ToList();

                var mNotAccelaDisplayMap = mNewPosseAccelaMap.FindAll(x => x.AION_CLS_NM != "AccelaProjectDisplayInfo").ToList();

                foreach (var maprecord in mNotAccelaDisplayMap)
                {
                    try
                    {
                        if (maprecord.AION_CLS_NM == "AccelaProjectModel")
                        {
                            if ((string.IsNullOrWhiteSpace(maprecord.ACCELA_OBJ_TYP_DESC)))
                            {
                                sbAionMapTrap.AppendLine(DateTime.Now + "NO AccelaObject value for classname, field : " +
                                                       maprecord.AION_CLS_NM + "," + maprecord.Meck_FIELD_NM);
                                throw new Exception(sbAionMapTrap.ToString());
                            }
                            else
                            {
                                switch (maprecord.ACCELA_OBJ_TYP_DESC.ToUpper())
                                {
                                    case "CUSTOMTABLES":
                                        foreach (var customTable in mIncomingAccelaRecord.CustomTables)
                                        {
                                            if (customTable.ContainsKey("id"))
                                            {
                                                object stringValue;

                                                customTable.TryGetValue("id", out stringValue);

                                                if ((string)stringValue == "CE_COM-REVIEW.cTASK.cACTIVATION"
                                                || (string)stringValue == "CE_CRTAP-REVIEW.cTASK.cACTIVATION"
                                                || (string)stringValue == "CE_CFSD-REVIEW.cTASK.cACTIVATION"
                                                || (string)stringValue == "CE_PRELIM-REVIEW.cTASK.cACTIVATION"
                                                || (string)stringValue == "CE_RES-REVIEW.cTASK.cACTIVATION"
                                                || (string)stringValue == "CE_RRTAP-REVIEW.cTASK.cACTIVATION"
                                             )
                                                {
                                                    if (customTable.GetType().Name.Contains("Dictionary"))
                                                    {
                                                        object customTableDictObj = customTable;
                                                        Dictionary<string, Object> customTableRows = (Dictionary<string, object>)customTableDictObj;

                                                        foreach (var customTableItem in customTableRows)
                                                        {
                                                            if (customTableItem.GetType().Name.Contains("KeyValuePair"))
                                                            {
                                                                object customTableItemDictObj = customTableItem;
                                                                KeyValuePair<string, Object> customTableItemRows = (KeyValuePair<string, object>)customTableItemDictObj;

                                                                if (customTableItemRows.Key == "rows")
                                                                {
                                                                    ArrayList customTableItemArray = (ArrayList)customTableItemRows.Value;

                                                                    foreach (var pair in customTableItemArray)
                                                                    {
                                                                        if (pair.GetType().Name.Contains("Dictionary"))
                                                                        {
                                                                            object pairDictObj = pair;
                                                                            Dictionary<string, Object> pairs = (Dictionary<string, object>)pairDictObj;

                                                                            foreach (var fieldPair in pairs)
                                                                            {
                                                                                if (fieldPair.GetType().Name.Contains("KeyValuePair"))
                                                                                {
                                                                                    object fieldPairDictObj = fieldPair;
                                                                                    KeyValuePair<string, Object> fieldPairItems = (KeyValuePair<string, object>)fieldPairDictObj;

                                                                                    if (fieldPairItems.Key == "fields")
                                                                                    {
                                                                                        Dictionary<string, Object> fieldPairs = (Dictionary<string, object>)fieldPairItems.Value;

                                                                                        string taskType = string.Empty;
                                                                                        int cycleNumberTask = 0;
                                                                                        string processingStatus = string.Empty;
                                                                                        DateTime? dueDateTask = null;

                                                                                        if (fieldPairs.ContainsKey("Task Type"))
                                                                                        {
                                                                                            fieldPairs.TryGetValue("Task Type", out object taskTypeObject);
                                                                                            if (taskTypeObject != null)
                                                                                            {
                                                                                                taskType = (string)taskTypeObject;
                                                                                            }
                                                                                        }

                                                                                        if (taskType != "Plan Review") continue;

                                                                                        if (fieldPairs.ContainsKey("Cycle #"))
                                                                                        {
                                                                                            fieldPairs.TryGetValue("Cycle #", out object cycleNumberObject);
                                                                                            string cycleNumberString = (string)cycleNumberObject;
                                                                                            if (cycleNumberObject != null)
                                                                                            {
                                                                                                cycleNumberTask = int.Parse(cycleNumberString);
                                                                                            }
                                                                                        }

                                                                                        if (cycleNumberTask != cycleNumber) continue;

                                                                                        if (fieldPairs.ContainsKey("Processing Status"))
                                                                                        {
                                                                                            fieldPairs.TryGetValue("Processing Status", out object processingStatusObject);
                                                                                            if (processingStatusObject != null)
                                                                                            {
                                                                                                processingStatus = (string)processingStatusObject;
                                                                                            }
                                                                                        }

                                                                                        if (processingStatus != "Activated in Accela") continue;

                                                                                        if (fieldPairs.ContainsKey("Due Date"))
                                                                                        {
                                                                                            fieldPairs.TryGetValue("Due Date", out object dueDateObject);
                                                                                            if (dueDateObject != null)
                                                                                            {
                                                                                                string dueDate = (string)dueDateObject;
                                                                                                if (!string.IsNullOrWhiteSpace(dueDate))
                                                                                                {
                                                                                                    dueDateTask = DateTime.Parse(dueDate);
                                                                                                }
                                                                                            }
                                                                                        }

                                                                                        fifoDueDate = dueDateTask;
                                                                                        break;
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                        break;

                                    default:
                                        break;
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

            return fifoDueDate;
        }
    }
}