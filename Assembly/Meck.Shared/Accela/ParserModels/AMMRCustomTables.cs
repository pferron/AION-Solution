using System.Collections.Generic;

namespace Meck.Shared.Accela.ParserModels
{
    public class AMMRCustomTables
    {
        public int status { get; set; }
        public List<AMMRResult> result { get; set; }

        public AMMRCustomTables()
        {
            result = new List<AMMRResult>();
            
            AMMRGeoInfomation  mGeoInfo = new AMMRGeoInfomation();
            result.Add(mGeoInfo.myresult);

            AMMRTaskInfomation mTaskInfo = new AMMRTaskInfomation();
            result.Add(mTaskInfo.myresult);
        }

    }

    public class AMMRResult
    {
        public string id { get; set; }
        public string text { get; set; }
        public int displayOrder { get; set; }
        public List<AMMRField> fields { get; set; }

        public AMMRResult()
        {
            fields = new List<AMMRField>();
        }
    }

    public class AMMRField
    {
        public string id { get; set; }
        public string text { get; set; }
        public string isReadonly { get; set; }
        public string fieldType { get; set; }
        public int maxLength { get; set; }
        public string isRequired { get; set; }
        public int displayOrder { get; set; }
        public string value { get; set; }
        public List<AMMROption> options { get; set; }
        public AMMRField(string NameID, string TextValue, string ValueValue, string IsReadOnly, string FieldType,
            int MaxLength,
            string IsRequired, int DisplayOrder, List<AMMROption> OptionArray)
        {
            id = NameID;
            text = TextValue;
            value = ValueValue;
            isReadonly = IsReadOnly;
            fieldType = fieldType;
            maxLength = maxLength;
            isRequired = isRequired;
            displayOrder = DisplayOrder;
            List<AMMROption> options = new List<AMMROption>();
            options = OptionArray;
        }
    }

    public class AMMROption
    {
        public string value { get; set; }
        public string text { get; set; }
    }

    public class AMMRGeoInfomation
    {
        public AMMRResult myresult { get; set; }

        public AMMRGeoInfomation()
        {
            myresult = new AMMRResult();

            myresult.id = "CE_AMMR-GEOGRAPHIC.cINFORMATION";
            myresult.text = "GEOGRAPHIC INFORMATION";
            myresult.displayOrder = 80;
            // build the list of fields//

            myresult.fields = new List<AMMRField>();

            AMMRField mpfield = new AMMRField("Layer Name", "Layer Name", null, "N", "Text", 0, "N", 10, null);
            myresult.fields.Add(mpfield);

            mpfield = new AMMRField("Attribute", "Attribute", null, "N", "Text", 240, "N", 20, null);
            myresult.fields.Add(mpfield);

            mpfield = new AMMRField("Value", "Value", null, "N", "Text", 0, "N", 30, null);
            myresult.fields.Add(mpfield);

        }
    }

    public class AMMRTaskInfomation
    {
        public AMMRResult myresult { get; set; }

        public AMMRTaskInfomation()
        {
            myresult = new AMMRResult();

            myresult.id = "CE_AMMR-REVIEW.cTASK.cACTIVATION";
            myresult.text = "REVIEW TASK ACTIVATION";
            myresult.displayOrder = 90;
            // build the list of fields//

            myresult.fields = new List<AMMRField>();

            List<AMMROption> mCommOpts = new List<AMMROption>();

            AMMROption mopt = new AMMROption();
            mopt.text = string.Empty;
            mopt.value = string.Empty;

            mCommOpts.Add(mopt);

            AMMRField mpfield = new AMMRField("Task Type", "Task Type", "Plan Review", "N", "DropDownList", 0, "N", 10, mCommOpts);
            myresult.fields.Add(mpfield);

            mpfield = new AMMRField("Task Name", "Task Name", null, "N", "Text", 0, "N", 30, mCommOpts);
            myresult.fields.Add(mpfield);

            mpfield = new AMMRField("Reviewer", "Reviewer", null, "N", "Text", 240, "N", 30, null);
            myresult.fields.Add(mpfield);

            mpfield = new AMMRField("Due Date", "Due Date", null, "N", "Date", 240, "N", 40, null);
            myresult.fields.Add(mpfield);

            mpfield = new AMMRField("Cycle #", "Cycle #", null, "N", "Number", 240, "N", 50, null);
            myresult.fields.Add(mpfield);

            mpfield = new AMMRField("Date-Time Stamp", "Date-Time Stamp", null, "N", "Text", 240, "y", 60, null);
            myresult.fields.Add(mpfield);

            mpfield = new AMMRField("Processing Status", "Processing Status", null, "N", "DropDownListr", 240, "N", 70, mCommOpts);
            myresult.fields.Add(mpfield);
        }
    }

}
