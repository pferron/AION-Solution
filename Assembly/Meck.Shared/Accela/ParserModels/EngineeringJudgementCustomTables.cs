using System.Collections.Generic;

namespace Meck.Shared.Accela.ParserModels
{
    public class EngineeringJudgementCustomTables
    {
        public int status { get; set; }
        public List<EngineeringJudgementResult> result { get; set; }

        public EngineeringJudgementCustomTables()
        {
            result = new List<EngineeringJudgementResult>();

            EngineerJudgementGEOGRAPHICINFORMATION mGeoInfo = new EngineerJudgementGEOGRAPHICINFORMATION();

            result.Add(mGeoInfo.myresult);

            EngineeringJudgementREVIEWTASKACTIVATION mTaskInfo = new EngineeringJudgementREVIEWTASKACTIVATION();
            result.Add(mTaskInfo.myresult);
        }

    }

    public class EngineeringJudgementResult
    {
        public string id { get; set; }
        public string text { get; set; }
        public int displayOrder { get; set; }
        public List<EngineeringJudgementField> fields { get; set; }

        public EngineeringJudgementResult()
        {
            fields = new List<EngineeringJudgementField>();
        }

    }

    public class EngineeringJudgementField
    {
        public string id { get; set; }
        public string text { get; set; }
        public string isReadonly { get; set; }
        public string fieldType { get; set; }
        public int maxLength { get; set; }
        public string isRequired { get; set; }
        public int displayOrder { get; set; }
        public string value { get; set; }
        public List<EngineeringJudgementOption> options { get; set; }

        public EngineeringJudgementField(string NameID, string TextValue, string ValueValue, string IsReadOnly, string FieldType,
            int MaxLength,
            string IsRequired, int DisplayOrder, List<EngineeringJudgementOption> OptionArray)
        {
            id = NameID;
            text = TextValue;
            value = ValueValue;
            isReadonly = IsReadOnly;
            fieldType = fieldType;
            maxLength = maxLength;
            isRequired = isRequired;
            displayOrder = DisplayOrder;
            List<EngineeringJudgementOption> options = new List<EngineeringJudgementOption>();
            options = OptionArray;
        }

    }

    public class EngineeringJudgementOption
    {
        public string value { get; set; }
        public string text { get; set; }
    }

    public class EngineerJudgementGEOGRAPHICINFORMATION
    {
        public EngineeringJudgementResult myresult { get; set; }

        public EngineerJudgementGEOGRAPHICINFORMATION()
        {
            myresult = new EngineeringJudgementResult();

            myresult.id = "CE_EJ-GEOGRAPHIC.cINFORMATION";
            myresult.text = "GEOGRAPHIC INFORMATION";
            myresult.displayOrder = 70;

            List<EngineeringJudgementField> fields = new List<EngineeringJudgementField>();

            EngineeringJudgementField tfield = new EngineeringJudgementField("Layer Name", "Layer Name", null, "N", "Text", 0, "N", 10, null);

            myresult.fields.Add(tfield);

            tfield = new EngineeringJudgementField("Attribute", "Attribute", null, "N", "Text", 0, "N", 20, null);

            myresult.fields.Add(tfield);

            tfield = new EngineeringJudgementField("Value", "Value", null, "N", "Text", 0, "N", 30, null);

            myresult.fields.Add(tfield);
        }
    }

    public partial class EngineeringJudgementREVIEWTASKACTIVATION
    {
        public EngineeringJudgementResult myresult { get; set; }


        public EngineeringJudgementREVIEWTASKACTIVATION()
        {
            myresult = new EngineeringJudgementResult();

            myresult.id = "CE_EJ-REVIEW.cTASK.cACTIVATION";
            myresult.text = "REVIEW TASK ACTIVATION";
            myresult.displayOrder = 90;

            List<EngineeringJudgementOption> mCommOPT = new List<EngineeringJudgementOption>();

            List<EngineeringJudgementField> fields = new List<EngineeringJudgementField>();

            EngineeringJudgementField tfield = new EngineeringJudgementField("Task Type", "Task Type", "Plan Review", "N", "DropDownList", 240, "Y", 10, mCommOPT);

            myresult.fields.Add(tfield);

            tfield = new EngineeringJudgementField("Task Name", "Task Name", null, "N", "DropDownList", 240, "N", 20, mCommOPT);

            myresult.fields.Add(tfield);

            tfield = new EngineeringJudgementField("Reviewer", "Reviewer", null, "N", "Text", 240, "N", 30, null);

            myresult.fields.Add(tfield);

            tfield = new EngineeringJudgementField("Due Date", "Due Date", null, "N", "Date", 240, "N", 40, null);

            myresult.fields.Add(tfield);

            tfield = new EngineeringJudgementField("Cycle #", "Cycle #", null, "N", "Number", 240, "N", 50, null);

            myresult.fields.Add(tfield);

            tfield = new EngineeringJudgementField("Date-Time Stamp", "Date-Time Stamp", null, "N", "Text", 240, "Y", 60, null);

            myresult.fields.Add(tfield);
            tfield = new EngineeringJudgementField("Processing Status", "Processing Status", null, "N", "DropDownList", 240, "N", 70, mCommOPT);

            myresult.fields.Add(tfield);
        }
    }

}
