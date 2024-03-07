using System.Collections.Generic;

namespace Meck.Shared.Accela.ParserModels
{
    public class SpecialEventCustomTables
    {
        public int status { get; set; }
        public List<SpecialEventResult> result { get; set; }

        public SpecialEventCustomTables()
        {
            result = new List<SpecialEventResult>();
            SpecialEventGENERATORcLOCATIONS  mGens = new SpecialEventGENERATORcLOCATIONS();
            result.Add(mGens.myResult);

            SpecialEventGEOGRAPHICINFORMATION mGeoInfo = new SpecialEventGEOGRAPHICINFORMATION();
            result.Add(mGeoInfo.myResult);

            SpecialEventREVIEWTASKACTIVATION mTask = new SpecialEventREVIEWTASKACTIVATION();
            
            result.Add(mTask.myResult);
        }
    }

    public class SpecialEventResult
    {
        public string id { get; set; }
        public string text { get; set; }
        public int displayOrder { get; set; }
        public List<SpecialEventField> fields { get; set; }

        public SpecialEventResult()
        {
            fields = new List<SpecialEventField>();
        }

    }

    public class SpecialEventField
    {
        public string id { get; set; }
        public string text { get; set; }
        public string isReadonly { get; set; }
        public string fieldType { get; set; }
        public int maxLength { get; set; }
        public string isRequired { get; set; }
        public int displayOrder { get; set; }
        public List<SpecialEventOption> options { get; set; }
        public string value { get; set; }

        public SpecialEventField(string NameID, string TextValue, string ValueValue, string IsReadOnly,
            string FieldType, int MaxLength, string IsRequired, int DisplayOrder, List<SpecialEventOption> OptionArray)
        {
            id = NameID;
            text = TextValue;
            value = ValueValue;
            isReadonly = IsReadOnly;
            fieldType = fieldType;
            maxLength = maxLength;
            isRequired = isRequired;
            displayOrder = DisplayOrder;
            List<SpecialEventOption> options = new List<SpecialEventOption>();
            options = OptionArray;
        }
    }

    public class SpecialEventOption
    {
        public string value { get; set; }
        public string text { get; set; }
    }

    public class SpecialEventGENERATORcLOCATIONS
    {
        public SpecialEventResult myResult { get; set; }

        public SpecialEventGENERATORcLOCATIONS()
        {
            myResult = new SpecialEventResult();

            myResult.id = "CE_SPEV-GENERATOR.cLOCATIONS";
            myResult.text = "GENERATOR LOCATIONS";
            myResult.displayOrder = 70;

            List<SpecialEventField> fields = new List<SpecialEventField>();

            SpecialEventField tfield = new SpecialEventField("GeneratorID", "GeneratorID", null, "N", "Text", 240,
                "N", 10, null);

            myResult.fields.Add(tfield);

            tfield = new SpecialEventField("GeneratorLocation", "GeneratorLocation", null, "N", "Text", 240, "N",
                20, null);

            myResult.fields.Add(tfield);
        }
    }

    public class SpecialEventVendors
    {
        public SpecialEventResult myResult { get; set; }

        public SpecialEventVendors()
        {
            myResult = new SpecialEventResult();

            myResult.id = "CE_SPEV-VENDORS";
            myResult.text = "GENERATOR LOCATIONS";
            myResult.displayOrder = 70;

            List<SpecialEventOption> mCommOpt = new List<SpecialEventOption>();

            List<SpecialEventField> fields = new List<SpecialEventField>();

            SpecialEventField tfield = new SpecialEventField("VendorType", "VendorType", null, "N", "DropdownList",
                240, "N", 10, mCommOpt);
            myResult.fields.Add(tfield);

            tfield = new SpecialEventField("VendorName", "VendorName", null, "N", "Text", 240, "N", 20, null);
            myResult.fields.Add(tfield);
            tfield = new SpecialEventField("VendorPhoneNumber", "VendorPhoneNumber", null, "N", "Text", 240, "N",
                30, null);
            myResult.fields.Add(tfield);
        }
    }


    public class SpecialEventGEOGRAPHICINFORMATION
    {
        public SpecialEventResult myResult { get; set; }

        public SpecialEventGEOGRAPHICINFORMATION()
        {
            myResult = new SpecialEventResult();

            myResult.id = "CE_SPEV-GEOGRAPHIC.cINFORMATION";
            myResult.text = "GEOGRAPHIC INFORMATION";
            myResult.displayOrder = 70;

            List<SpecialEventField> fields = new List<SpecialEventField>();

            SpecialEventField tfield =
                new SpecialEventField("Layer Name", "Layer Name", null, "N", "Text", 0, "N", 10, null);

            myResult.fields.Add(tfield);

            tfield = new SpecialEventField("Attribute", "Attribute", null, "N", "Text", 0, "N", 20, null);

            myResult.fields.Add(tfield);

            tfield = new SpecialEventField("Value", "Value", null, "N", "Text", 0, "N", 30, null);

            myResult.fields.Add(tfield);
        }
    }

    public partial class SpecialEventREVIEWTASKACTIVATION
    {
        public SpecialEventResult myResult { get; set; }


        public SpecialEventREVIEWTASKACTIVATION()
        {
            myResult = new SpecialEventResult();

            myResult.id = "CE_SPEV-REVIEW.cTASK.cACTIVATION";
            myResult.text = "REVIEW TASK ACTIVATION";
            myResult.displayOrder = 90;

            List<SpecialEventOption> mCommOPT = new List<SpecialEventOption>();

            List<SpecialEventField> fields = new List<SpecialEventField>();

            SpecialEventField tfield = new SpecialEventField("Task Type", "Task Type", "Plan Review", "N",
                "DropDownList", 240, "Y", 10, mCommOPT);

            myResult.fields.Add(tfield);

            tfield = new SpecialEventField("Task Name", "Task Name", null, "N", "DropDownList", 240, "N", 20,
                mCommOPT);

            myResult.fields.Add(tfield);

            tfield = new SpecialEventField("Reviewer", "Reviewer", null, "N", "Text", 240, "N", 30, null);

            myResult.fields.Add(tfield);

            tfield = new SpecialEventField("Due Date", "Due Date", null, "N", "Date", 240, "N", 40, null);

            myResult.fields.Add(tfield);

            tfield = new SpecialEventField("Cycle #", "Cycle #", null, "N", "Number", 240, "N", 50, null);

            myResult.fields.Add(tfield);

            tfield = new SpecialEventField("Date-Time Stamp", "Date-Time Stamp", null, "N", "Text", 240, "Y", 60,
                null);

            myResult.fields.Add(tfield);
            tfield = new SpecialEventField("Processing Status", "Processing Status", null, "N", "DropDownList", 240,
                "N", 70, mCommOPT);

            myResult.fields.Add(tfield);
        }
    }

}
 