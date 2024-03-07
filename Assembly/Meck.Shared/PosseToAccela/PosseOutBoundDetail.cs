using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Meck.Shared.PosseToAccela
{
    // using System.Xml.Serialization;
    // XmlSerializer serializer = new XmlSerializer(typeof(ProjectData));
    // using (StringReader reader = new StringReader(xml))
    // {
    //    var test = (ProjectData)serializer.Deserialize(reader);
    // }

    [Serializable()]
    [XmlRoot(ElementName = "FieldRTAP", Namespace = "")]
    public class FieldRTAP
    {

        [XmlElement(ElementName = "RTAPNumber", Namespace = "")]
        public string RTAPNumber;

        [XmlElement(ElementName = "OriginalProjectNumber", Namespace = "")]
        public string OriginalProjectNumber;

        [XmlElement(ElementName = "OriginalPermitNumber", Namespace = "")]
        public string OriginalPermitNumber;

        [XmlElement(ElementName = "Inspector", Namespace = "")]
        public string Inspector;

        [XmlElement(ElementName = "Trade", Namespace = "")]
        public string Trade;

        [XmlElement(ElementName = "Notes", Namespace = "")]
        public string Notes;
    }

    [Serializable()]
    [XmlRoot(ElementName = "FieldRTAPs", Namespace = "")]
    public class FieldRTAPs
    {

        [XmlElement(ElementName = "FieldRTAP", Namespace = "")]
        public List<FieldRTAP> FieldRTAP { get; set; }
    }

    [Serializable()]
    [XmlRoot(ElementName = "TaskList", Namespace = "")]
    public class TaskList
    {

        [XmlElement(ElementName = "TaskType", Namespace = "")]
        public string TaskType;

        [XmlElement(ElementName = "AccessGroup", Namespace = "")]
        public string AccessGroup;

        [XmlElement(ElementName = "CreatedBy", Namespace = "")]
        public string CreatedBy;

        [XmlElement(ElementName = "CreatedDate", Namespace = "")]
        public string CreatedDate;

        [XmlElement(ElementName = "Comments", Namespace = "")]
        public string Comments; 
    }

    [Serializable()]
    [XmlRoot(ElementName = "TaskLists", Namespace = "")]
    public class TaskLists
    {
        [XmlElement(ElementName = "TaskList", Namespace = "")]
        public List<TaskList> TaskList { get; set; }
    }

    [Serializable()]
    [XmlRoot(ElementName = "FeeCondition", Namespace = "")]
    public class FeeCondition
    {

        [XmlElement(ElementName = "ConditionID", Namespace = "")]
        public int ConditionID { get; set; }

        [XmlElement(ElementName = "Type", Namespace = "")]
        public string Type; 

        [XmlElement(ElementName = "Group", Namespace = "")]
        public string Group;

        [XmlElement(ElementName = "Name", Namespace = "")]
        public string Name; 

        [XmlElement(ElementName = "ConditionStatuses", Namespace = "")]
        public string ConditionStatuses; 

        [XmlElement(ElementName = "PossePrepaidReviewFee", Namespace = "")]
        public string  PossePrepaidReviewFee;
    }

    [Serializable()]
    [XmlRoot(ElementName = "FeeConditions", Namespace = "")]
    public class FeeConditions
    {

        [XmlElement(ElementName = "FeeCondition", Namespace = "")]
        public List<FeeCondition> FeeCondition { get; set; }
    }

    [Serializable()]
    [XmlRoot(ElementName = "ProjectData", Namespace = "")]
    public class PosseOutBoundDetail
    {

        [XmlElement(ElementName = "RecordId", Namespace = "")]
        public string RecordId;

        [XmlElement(ElementName = "PlanReviewCategory", Namespace = "")]
        public string PlanReviewCategory;

        [XmlElement(ElementName = "ProcessingStatus", Namespace = "")]
        public string ProcessingStatus;

        [XmlElement(ElementName = "ProjectNumber", Namespace = "")]
        public string ProjectNumber;

        [XmlElement(ElementName = "AccelaProjectStatus", Namespace = "")]
        public string AccelaProjectStatus;

        [XmlElement(ElementName = "AddressID", Namespace = "")]
        public string AddressID;

        [XmlElement(ElementName = "PrepaidFeeAmount", Namespace = "")]
        public string  PrepaidFeeAmount;

        [XmlElement(ElementName = "FieldRTAPs", Namespace = "")]
        public FieldRTAPs FieldRTAPs; 

        [XmlElement(ElementName = "TaskLists", Namespace = "")]
        public TaskLists TaskLists; 

        [XmlElement(ElementName = "FeeConditions", Namespace = "")]
        public FeeConditions FeeConditions;
    }



}
