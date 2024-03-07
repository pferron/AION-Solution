using System.Collections.Generic;

namespace Meck.Shared.Accela.ParserModels
{
    public class CommercialRTAPCustomTables
    {
        public int status { get; set; }
        public List<CommercialRTAPResult> result { get; set; }

        public CommercialRTAPCustomTables()
        {
            result = new List<CommercialRTAPResult>();


        }
    }

    public class CommercialRTAPResult
    {
        public string id { get; set; }
        public string text { get; set; }
        public int displayOrder { get; set; }
        public List<CommercialRTAPField> fields { get; set; }

        public CommercialRTAPResult()
        {
            fields = new List<CommercialRTAPField>();
        }
    }

    public class CommercialRTAPField
    {
        public string id { get; set; }
        public string text { get; set; }
        public string isReadonly { get; set; }
        public string fieldType { get; set; }
        public int maxLength { get; set; }
        public string isRequired { get; set; }
        public int displayOrder { get; set; }
        public string value { get; set; }
        public List<CommercialRTAPOption> options { get; set; }

        public CommercialRTAPField(string NameID, string TextValue, string ValueValue, string IsReadOnly,
            string FieldType,
            int MaxLength,
            string IsRequired, int DisplayOrder, List<CommercialRTAPOption> OptionArray)
        {
            id = NameID;
            text = TextValue;
            value = ValueValue;
            isReadonly = IsReadOnly;
            fieldType = fieldType;
            maxLength = maxLength;
            isRequired = isRequired;
            displayOrder = DisplayOrder;
            List<CommercialRTAPOption> options = new List<CommercialRTAPOption>();
            options = OptionArray;
        }
    }

    public class CommercialRTAPOption
    {
        public string value { get; set; }
        public string text { get; set; }
    }



    public class CommercialRTAPGEOGRAPHICINFORMATION
    {
        public CommercialRTAPResult myresult { get; set; }

        public CommercialRTAPGEOGRAPHICINFORMATION()
        {
            myresult = new CommercialRTAPResult();

            myresult.id = "CE_RRTAP-GEOGRAPHIC.cINFORMATION";
            myresult.text = "GEOGRAPHIC INFORMATION";
            myresult.displayOrder = 70;

            List<CommercialRTAPField> fields = new List<CommercialRTAPField>();

            CommercialRTAPField tfield =
                new CommercialRTAPField("Layer Name", "Layer Name", null, "N", "Text", 0, "N", 10, null);

            myresult.fields.Add(tfield);

            tfield = new CommercialRTAPField("Attribute", "Attribute", null, "N", "Text", 0, "N", 20, null);

            myresult.fields.Add(tfield);

            tfield = new CommercialRTAPField("Value", "Value", null, "N", "Text", 0, "N", 30, null);

            myresult.fields.Add(tfield);
        }
    }

    public class classCommercialRTAPREVIEWTASKACTIVATION
    {
        public CommercialRTAPResult myresult { get; set; }


        public classCommercialRTAPREVIEWTASKACTIVATION()
        {
            myresult = new CommercialRTAPResult();

            myresult.id = "CE_CRTAP-REVIEW.cTASK.cACTIVATION";
            myresult.text = "REVIEW TASK ACTIVATION";
            myresult.displayOrder = 90;

            List<CommercialRTAPOption> mCommOPT = new List<CommercialRTAPOption>();

            List<CommercialRTAPField> fields = new List<CommercialRTAPField>();

            CommercialRTAPField tfield = new CommercialRTAPField("Task Type", "Task Type", "Plan Review", "N",
                "DropDownList", 240, "Y", 10, mCommOPT);

            myresult.fields.Add(tfield);

            tfield = new CommercialRTAPField("Task Name", "Task Name", null, "N", "DropDownList", 240, "N", 20,
                mCommOPT);

            myresult.fields.Add(tfield);

            tfield = new CommercialRTAPField("Reviewer", "Reviewer", null, "N", "Text", 240, "N", 30, null);

            myresult.fields.Add(tfield);

            tfield = new CommercialRTAPField("Due Date", "Due Date", null, "N", "Date", 240, "N", 40, null);

            myresult.fields.Add(tfield);

            tfield = new CommercialRTAPField("Cycle #", "Cycle #", null, "N", "Number", 240, "N", 50, null);

            myresult.fields.Add(tfield);

            tfield = new CommercialRTAPField("Date-Time Stamp", "Date-Time Stamp", null, "N", "Text", 240, "Y", 60,
                null);

            myresult.fields.Add(tfield);
            tfield = new CommercialRTAPField("Processing Status", "Processing Status", null, "N", "DropDownList", 240,
                "N", 70, mCommOPT);

            myresult.fields.Add(tfield);
        }
    }

}


