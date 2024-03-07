using System.Collections.Generic;

namespace Meck.Shared.Accela.ParserModels
{
    public class CountyFireShopDrawingsCustomTables
    {
        public int status { get; set; }
        public List<FireShopResult> result { get; set; }
        public CountyFireShopDrawingsCustomTables()
        {
            result = new List<FireShopResult>(); 

            FireShopGeoInfo mGeoCustomTable = new FireShopGeoInfo();
            result.Add(mGeoCustomTable.myresult);

            FireShopREVIEWTASKACTIVATION mReviewTaskCustomTable = new FireShopREVIEWTASKACTIVATION();
            result.Add(mReviewTaskCustomTable.myresult);
        }

    }


    public class FireShopResult
    {
        public string id { get; set; }
        public string text { get; set; }
        public int displayOrder { get; set; }
        public List<FireShopField> fields { get; set; }

        public FireShopResult()
        {
            fields = new List<FireShopField>();
        }

    }

    public class FireShopField
    {
        public string id { get; set; }
        public string text { get; set; }
        public string isReadonly { get; set; }
        public string fieldType { get; set; }
        public int maxLength { get; set; }
        public string isRequired { get; set; }
        public int displayOrder { get; set; }
        public string value { get; set; }
        public List<FireShopOption> options { get; set; }

        public FireShopField(string NameID, string TextValue, string ValueValue, string IsReadOnly, string FieldType, int MaxLength,
            string IsRequired, int DisplayOrder, List<FireShopOption> OptionList)
        {
            id = NameID;
            text = TextValue;
            value = ValueValue;
            isReadonly = IsReadOnly;
            fieldType = fieldType;
            maxLength = maxLength;
            isRequired = isRequired;
            displayOrder = DisplayOrder;
            List<FireShopOption> options = new List<FireShopOption>();
            options = OptionList;
        }
    }

    public class FireShopOption
    {
        public string value { get; set; }
        public string text { get; set; }
    }


    public class FireShopGeoInfo
    {
        public FireShopResult myresult { get; set; }

        public FireShopGeoInfo()
        {
            myresult = new FireShopResult();

            myresult.id = "CE_CFSD - GEOGRAPHIC.cINFORMATION";
            myresult.text = "GEOGRAPHIC INFORMATION";
            myresult.displayOrder = 70;

            List<FireShopField> fields = new List<FireShopField>();
            
            FireShopField tfield = new FireShopField("Layer Name", "Layer Name", null, "N", "Text", 0, "N", 10, null);

            myresult.fields.Add(tfield);

            tfield = new FireShopField("Attribute", "Attribute", null, "N", "Text", 0, "N", 20, null);

            myresult.fields.Add(tfield);

            tfield = new FireShopField("Value", "Value", null, "N", "Text", 0, "N", 30, null);

            myresult.fields.Add(tfield);
        }


    }
    public partial class FireShopREVIEWTASKACTIVATION
    {
        public FireShopResult myresult { get; set; }


        public FireShopREVIEWTASKACTIVATION()
        {
            myresult = new FireShopResult();

            myresult.id = "CE_CFSD-REVIEW.cTASK.cACTIVATION";
            myresult.text = "REVIEW TASK ACTIVATION";
            myresult.displayOrder = 90;

            List<FireShopField> fields = new List<FireShopField>();

            FireShopField tfield = new FireShopField("Task Type", "Task Type", "Plan Review", "N", "DropDownList", 240, "Y", 10, null);

            myresult.fields.Add(tfield);

            tfield = new FireShopField("Task Name", "Task Name", null, "N", "DropDownList", 240, "N", 20, null);

            myresult.fields.Add(tfield);

            tfield = new FireShopField("Reviewer", "Reviewer", null, "N", "Text", 240, "N", 30, null);

            myresult.fields.Add(tfield);

            tfield = new FireShopField("Due Date", "Due Date", null, "N", "Date", 240, "N", 40, null);

            myresult.fields.Add(tfield);

            tfield = new FireShopField("Cycle #", "Cycle #", null, "N", "Number", 240, "N", 50, null);

            myresult.fields.Add(tfield);

            tfield = new FireShopField("Date-Time Stamp", "Date-Time Stamp", null, "N", "Text", 240, "Y", 60, null);

            myresult.fields.Add(tfield);
            tfield = new FireShopField("Processing Status", "Processing Status", null, "N", "DropDownList", 240, "N", 70, null);

            myresult.fields.Add(tfield);
        }
    }

}
