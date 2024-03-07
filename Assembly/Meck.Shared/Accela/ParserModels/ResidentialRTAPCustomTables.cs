using System.Collections.Generic;

namespace Meck.Shared.Accela.ParserModels
{
    public class ResidentialRTAPCustomTables
    {
        public int status { get; set; }

        public List<ResidentialRTAPResult> result { get; set; }

        public ResidentialRTAPCustomTables()
        {
            result = new List<ResidentialRTAPResult>();

            CustomTable_GEOGRAPHICINFORMATION mGeoCustomTable = new CustomTable_GEOGRAPHICINFORMATION();
            result.Add(mGeoCustomTable.myresult);

            CustomTable_REVIEWTASKACTIVATION mReviewTaskCustomTable = new CustomTable_REVIEWTASKACTIVATION();
            result.Add(mReviewTaskCustomTable.myresult);
        }

    }

    public class ResidentialRTAPResult
    {
        public string id { get; set; }
        public string text { get; set; }
        public int displayOrder { get; set; }
        public List<ResidentialRTAPField> fields { get; set; }

        public ResidentialRTAPResult()
        {
            fields = new List<ResidentialRTAPField>();
        }
    }

    public class ResidentialRTAPField
    {
        public string id { get; set; }
        public string text { get; set; }
        public string isReadonly { get; set; }
        public string fieldType { get; set; }
        public int maxLength { get; set; }
        public string isRequired { get; set; }
        public int displayOrder { get; set; }
        public string value { get; set; }
        public List<ResidentialRTAPOption> options { get; set; }

        public ResidentialRTAPField(string NameID, string TextValue, string ValueValue, string IsReadOnly, string FieldType, int MaxLength,
            string IsRequired, int DisplayOrder, List<ResidentialRTAPOption> OptionList)
        {
            id = NameID;
            text = TextValue;
            value = ValueValue;
            isReadonly = IsReadOnly;
            fieldType = fieldType;
            maxLength = maxLength;
            isRequired = isRequired;
            displayOrder = DisplayOrder;
            List<ResidentialRTAPOption> options = new List<ResidentialRTAPOption>();
            options = OptionList;
        }
    }

    public class ResidentialRTAPOption
    {
        public string value { get; set; }
        public string text { get; set; }
    }


    public class CustomTable_GEOGRAPHICINFORMATION
    {
        public ResidentialRTAPResult myresult { get; set; }

        public CustomTable_GEOGRAPHICINFORMATION()
        {
            myresult = new ResidentialRTAPResult();

            myresult.id = "CE_RRTAP-GEOGRAPHIC.cINFORMATION";
            myresult.text = "GEOGRAPHIC INFORMATION";
            myresult.displayOrder = 70;

            List<ResidentialRTAPField> fields = new List<ResidentialRTAPField>();

            ResidentialRTAPField tfield = new ResidentialRTAPField("Layer Name", "Layer Name", null, "N", "Text", 0, "N", 10, null);

            myresult.fields.Add(tfield);

            tfield = new ResidentialRTAPField("Attribute", "Attribute", null, "N", "Text", 0, "N", 20, null);

            myresult.fields.Add(tfield);

            tfield = new ResidentialRTAPField("Value", "Value", null, "N", "Text", 0, "N", 30, null);

            myresult.fields.Add(tfield);
        }
    }

    public partial class CustomTable_REVIEWTASKACTIVATION
    {
        public ResidentialRTAPResult myresult { get; set; }

        public CustomTable_REVIEWTASKACTIVATION()
        {
            myresult = new ResidentialRTAPResult();

            myresult.id = "CE_RRTAP-REVIEW.cTASK.cACTIVATION";
            myresult.text = "REVIEW TASK ACTIVATION";
            myresult.displayOrder = 90;

            List<ResidentialRTAPOption> mCommOPT = new List<ResidentialRTAPOption>();

            List<ResidentialRTAPField> fields = new List<ResidentialRTAPField>();

            ResidentialRTAPField tfield = new ResidentialRTAPField("Task Type", "Task Type", "Plan Review", "N", "DropDownList", 240, "Y", 10, mCommOPT);

            myresult.fields.Add(tfield);

            tfield = new ResidentialRTAPField("Task Name", "Task Name", null, "N", "DropDownList", 240, "N", 20, mCommOPT);

            myresult.fields.Add(tfield);

            tfield = new ResidentialRTAPField("Reviewer", "Reviewer", null, "N", "Text", 240, "N", 30, null);

            myresult.fields.Add(tfield);

            tfield = new ResidentialRTAPField("Due Date", "Due Date", null, "N", "Date", 240, "N", 40, null);

            myresult.fields.Add(tfield);

            tfield = new ResidentialRTAPField("Cycle #", "Cycle #", null, "N", "Number", 240, "N", 50, null);

            myresult.fields.Add(tfield);

            tfield = new ResidentialRTAPField("Date-Time Stamp", "Date-Time Stamp", null, "N", "Text", 240, "Y", 60, null);

            myresult.fields.Add(tfield);
            tfield = new ResidentialRTAPField("Processing Status", "Processing Status", null, "N", "DropDownList", 240, "N", 70, mCommOPT);

            myresult.fields.Add(tfield);
        }
    }

}
