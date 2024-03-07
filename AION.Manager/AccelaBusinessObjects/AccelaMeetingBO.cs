using Meck.Shared.MeckDataMapping;
using System;
using System.Collections;
using System.Collections.Generic;

namespace AION.Manager.AccelaBusinessObjects
{
    public class AccelaMeetingBO : AccelaBusinessObjectBase
    {
        public List<AccelaMeeting> MakeAIONAccelaMeetingsList(List<KeyValuePair<string, object>> newValue)
        {

            List<AccelaMeeting> mAccelaMeetings = new List<AccelaMeeting>();
            string Errloc = "MakeMeetingList-0";
            try
            {
                for (int indx = 0; indx < newValue.Count; indx++)
                {
                    Errloc = "MakeMeetingList-1";
                    if (newValue[indx].Value.GetType().Name.Contains("ArrayList"))
                    {
                        ArrayList mDataArray = (ArrayList)newValue[indx].Value;

                        AccelaMeeting mAccelaMeeting = new AccelaMeeting();

                        for (int indxarray = 0; indxarray < mDataArray.Count; indxarray++)
                        {
                            Errloc = "MakeMeetingList-2";
                            ArrayList mArrayDataElement = new ArrayList { mDataArray[indxarray] };

                            Dictionary<string, object> mNewElement = (Dictionary<string, object>)mArrayDataElement[0];

                            foreach (var tElementTopLayer in mNewElement)
                            {
                                Errloc = "MakeMeetingList-3";


                                if (!tElementTopLayer.Value.GetType().Name.Contains("Dictionary"))
                                {
                                    mAccelaMeeting.id = tElementTopLayer.Value.ToString();
                                }
                                else
                                {
                                    Dictionary<string, object> tElement =
                                        (Dictionary<string, object>)tElementTopLayer.Value;

                                    foreach (var telement in tElement)
                                    {
                                        Errloc = "MakeMeetingList-4";
                                        object saveObject;
                                        switch (telement.Key.ToUpper())
                                        {
                                            case "STATUS":
                                                {
                                                    tElement.TryGetValue("Status", out saveObject);
                                                    if (saveObject != null)
                                                    {
                                                        mAccelaMeeting.Status = saveObject.ToString();
                                                    }
                                                }
                                                break;
                                            case "REQUESTER":
                                                {
                                                    tElement.TryGetValue("Requester", out saveObject);
                                                    if (saveObject != null)
                                                    {
                                                        mAccelaMeeting.Requester = saveObject.ToString();
                                                    }
                                                }
                                                break;
                                            case "MEETING TYPE":
                                                {
                                                    tElement.TryGetValue("Meeting Type", out saveObject);
                                                    if (saveObject != null)
                                                    {
                                                        mAccelaMeeting.MeetingType = saveObject.ToString();
                                                    }
                                                }
                                                break;
                                            case "ATTENDEES LIST":
                                                {
                                                    tElement.TryGetValue("Attendees List", out saveObject);
                                                    if (saveObject != null)
                                                    {
                                                        mAccelaMeeting.AttendeesList = saveObject.ToString();
                                                    }
                                                }
                                                break;
                                            case "MEETING DATE":
                                                {
                                                    tElement.TryGetValue("Meeting Date", out saveObject);
                                                    if (saveObject != null)
                                                    {
                                                        mAccelaMeeting.MeetingDate = saveObject.ToString();
                                                    }
                                                }

                                                break;
                                            case "CYCLE #":
                                                {
                                                    tElement.TryGetValue("Cycle #", out saveObject);
                                                    if (saveObject != null)
                                                    {
                                                        mAccelaMeeting.Cycle = Convert.ToInt32(saveObject.ToString());
                                                    }
                                                }

                                                break;
                                            case "MEETING TIME":
                                                {
                                                    tElement.TryGetValue("Meeting Time", out saveObject);
                                                    if (saveObject != null)
                                                    {
                                                        mAccelaMeeting.MeetingTime = saveObject.ToString();
                                                    }
                                                }
                                                break;
                                            case "NOTES":
                                                {
                                                    tElement.TryGetValue("Notes", out saveObject);
                                                    if (saveObject != null)
                                                    {
                                                        mAccelaMeeting.Notes = saveObject.ToString();
                                                    }
                                                }
                                                break;
                                            case "id":
                                                {
                                                    tElement.TryGetValue("id", out saveObject);
                                                    if (saveObject != null)
                                                    {
                                                        mAccelaMeeting.id = saveObject.ToString();
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
                                                        "Meeting List creation Accela error took default path:" +
                                                        telement.Key.ToUpper() + " Value:" + badvalue);
                                                }
                                                break;
                                        }
                                    }
                                }
                            }
                            mAccelaMeetings.Add(mAccelaMeeting);
                        }

                    }
                }
                return mAccelaMeetings;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in MakeTaskList fromRecord : " + Errloc + "-" + ex.InnerException);
            }
        }

    }
}