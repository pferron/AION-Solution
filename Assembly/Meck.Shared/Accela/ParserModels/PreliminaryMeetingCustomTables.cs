using System.Collections.Generic;

namespace Meck.Shared.Accela.ParserModels
{
    public class PreliminaryMeetingCustomTables
    {
        public int status { get; set; }
        public List<PreliminaryMeetingResult> result { get; set; }

        public PreliminaryMeetingCustomTables()
        {
            result = new List<PreliminaryMeetingResult>();

            PreliminaryMeetingGeoInfo mGeographicinformation = new PreliminaryMeetingGeoInfo(); 
            result.Add(mGeographicinformation.myresult);

            PreliminaryMeetingREVIEWTASKACTIVATION mReviewTask = new PreliminaryMeetingREVIEWTASKACTIVATION();
            result.Add(mReviewTask.myresult);
        }
    }

    public class PreliminaryMeetingResult
    {
        public string id { get; set; }
        public string text { get; set; }
        public int displayOrder { get; set; }
        public List<PreliminaryMeetingField> fields { get; set; }

        public PreliminaryMeetingResult()
        {
            fields = new List<PreliminaryMeetingField>();

        }
    }

    public class PreliminaryMeetingField
    {
        public string id { get; set; }
        public string text { get; set; }
        public string isReadonly { get; set; }
        public string fieldType { get; set; }
        public int maxLength { get; set; }
        public string isRequired { get; set; }
        public int displayOrder { get; set; }
        public string value { get; set; }
        public List<PreliminaryMeetingOption> options { get; set; }
        public PreliminaryMeetingField(string NameID, string TextValue, string ValueValue, string IsReadOnly, string FieldType, int MaxLength,
            string IsRequired, int DisplayOrder, List<PreliminaryMeetingOption> OptionList)
        {
            id = NameID;
            text = TextValue;
            value = ValueValue;
            isReadonly = IsReadOnly;
            fieldType = fieldType;
            maxLength = maxLength;
            isRequired = isRequired;
            displayOrder = DisplayOrder;
            List<PreliminaryMeetingOption> options = new List<PreliminaryMeetingOption>();
            options = OptionList;
        }
    }

    public class PreliminaryMeetingOption
    {
        public string value { get; set; }
        public string text { get; set; }
    }



    public class PreliminaryMeetingGeoInfo
    {

        public PreliminaryMeetingResult myresult { get; set; }

        public PreliminaryMeetingGeoInfo()
        {
            myresult = new PreliminaryMeetingResult();

            myresult.id = "CE_RRTAP-GEOGRAPHIC.cINFORMATION";
            myresult.text = "GEOGRAPHIC INFORMATION";
            myresult.displayOrder = 70;


            List<PreliminaryMeetingField> fields = new List<PreliminaryMeetingField>();

            PreliminaryMeetingField tfield = new PreliminaryMeetingField("Layer Name", "Layer Name", null, "N", "Text", 0, "N", 10, null);

            myresult.fields.Add(tfield);

            tfield = new PreliminaryMeetingField("Attribute", "Attribute", null, "N", "Text", 0, "N", 20, null);

            myresult.fields.Add(tfield);

            tfield = new PreliminaryMeetingField("Value", "Value", null, "N", "Text", 0, "N", 30, null);

            myresult.fields.Add(tfield);
        }
    }

    public partial class PreliminaryMeetingREVIEWTASKACTIVATION
    {
        public PreliminaryMeetingResult myresult { get; set; }

        public PreliminaryMeetingREVIEWTASKACTIVATION()
        {
            myresult = new PreliminaryMeetingResult();

            myresult.id = "CE_RRTAP-REVIEW.cTASK.cACTIVATION";
            myresult.text = "REVIEW TASK ACTIVATION";
            myresult.displayOrder = 90;

            List<PreliminaryMeetingOption> mCommOPT = new List<PreliminaryMeetingOption>();
            List<PreliminaryMeetingField> fields = new List<PreliminaryMeetingField>();

            PreliminaryMeetingField tfield = new PreliminaryMeetingField("Task Type", "Task Type", "Plan Review", "N", "DropDownList", 240, "Y", 10, mCommOPT);

            myresult.fields.Add(tfield);

            tfield = new PreliminaryMeetingField("Task Name", "Task Name", null, "N", "DropDownList", 240, "N", 20, mCommOPT);

            myresult.fields.Add(tfield);

            tfield = new PreliminaryMeetingField("Reviewer", "Reviewer", null, "N", "Text", 240, "N", 30, null);

            myresult.fields.Add(tfield);

            tfield = new PreliminaryMeetingField("Due Date", "Due Date", null, "N", "Date", 240, "N", 40, null);

            myresult.fields.Add(tfield);

            tfield = new PreliminaryMeetingField("Cycle #", "Cycle #", null, "N", "Number", 240, "N", 50, null);

            myresult.fields.Add(tfield);

            tfield = new PreliminaryMeetingField("Date-Time Stamp", "Date-Time Stamp", null, "N", "Text", 240, "Y", 60, null);

            myresult.fields.Add(tfield);
            tfield = new PreliminaryMeetingField("Processing Status", "Processing Status", null, "N", "DropDownList", 240, "N", 70, mCommOPT);

            myresult.fields.Add(tfield);
        }
    }
}