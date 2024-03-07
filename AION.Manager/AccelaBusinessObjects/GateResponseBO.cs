using Meck.Shared.MeckDataMapping;
using System;
using System.Collections;
using System.Collections.Generic;

namespace AION.Manager.AccelaBusinessObjects
{
    public class GateResponseBO : AccelaBusinessObjectBase
    {
        public List<GateResponse> MakeAIONGateRejectionReasonsList(List<KeyValuePair<string, object>> newValue)
        {

            List<GateResponse> mGateResponses = new List<GateResponse>();
            string Errloc = "MakeGateRejectionReasonList-0";
            try
            {
                for (int indx = 0; indx < newValue.Count; indx++)
                {
                    Errloc = "MakeGateRejectionReasonList-1";
                    if (newValue[indx].Value.GetType().Name.Contains("ArrayList"))
                    {
                        ArrayList mDataArray = (ArrayList)newValue[indx].Value;

                        GateResponse mGateResponse = new GateResponse();

                        for (int indxarray = 0; indxarray < mDataArray.Count; indxarray++)
                        {
                            Errloc = "MakeGateRejectionReasonList-2";
                            ArrayList mArrayDataElement = new ArrayList { mDataArray[indxarray] };

                            Dictionary<string, object> mNewElement = (Dictionary<string, object>)mArrayDataElement[0];



                            foreach (var tElementTopLayer in mNewElement)
                            {
                                Errloc = "MakeGateRejectionReasonList-3";


                                if (!tElementTopLayer.Value.GetType().Name.Contains("Dictionary"))
                                {
                                    mGateResponse.id = tElementTopLayer.Value.ToString();
                                }
                                else
                                {
                                    Dictionary<string, object> tElement =
                                        (Dictionary<string, object>)tElementTopLayer.Value;

                                    foreach (var telement in tElement)
                                    {
                                        Errloc = "MakeGateRejectionReasonList-4";
                                        object saveObject;
                                        switch (telement.Key.ToUpper())
                                        {
                                            case "REASON":
                                                {
                                                    tElement.TryGetValue("Reason", out saveObject);
                                                    if (saveObject != null)
                                                    {
                                                        mGateResponse.Reason = saveObject.ToString();
                                                    }
                                                }
                                                break;
                                            case "COMMENTS":
                                                {
                                                    tElement.TryGetValue("Comments", out saveObject);
                                                    if (saveObject != null)
                                                    {
                                                        mGateResponse.Comments = saveObject.ToString();
                                                    }
                                                }
                                                break;
                                            case "CYCLE #":
                                                {
                                                    tElement.TryGetValue("Cycle #", out saveObject);
                                                    if (saveObject != null)
                                                    {
                                                        mGateResponse.Cycle = Convert.ToInt32(saveObject.ToString());
                                                    }
                                                }
                                                break;
                                            case "DATE-TIME STAMP":
                                                {
                                                    tElement.TryGetValue("Date-Time Stamp", out saveObject);
                                                    if (saveObject != null)
                                                    {
                                                        mGateResponse.DateTimeStamp =
                                                            Convert.ToDateTime(saveObject.ToString());
                                                    }
                                                }
                                                break;
                                            case "id":
                                                {
                                                    tElement.TryGetValue("id", out saveObject);
                                                    if (saveObject != null)
                                                    {
                                                        mGateResponse.id = saveObject.ToString();
                                                    }
                                                }
                                                break;
                                            default:
                                                {
                                                    tElement.TryGetValue(telement.Key, out saveObject);
                                                    string badvalue = string.Empty;
                                                    if (saveObject != null)
                                                    {
                                                        badvalue = saveObject.ToString();
                                                    }

                                                    sbAionMapTrap.Append(
                                                        "Gate Rejection Reason creation Accela error took default path:" +
                                                        telement.Key.ToUpper() + " Value:" + badvalue);
                                                }
                                                break;
                                        }
                                    }
                                }
                            }
                            mGateResponses.Add(mGateResponse);
                        }

                    }
                }
                return mGateResponses;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in MakeTaskList fromRecord : " + Errloc + "-" + ex.InnerException);
            }
        }

    }
}