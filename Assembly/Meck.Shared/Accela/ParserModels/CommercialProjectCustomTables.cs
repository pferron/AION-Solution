using System.Collections.Generic;


namespace Meck.Shared.Accela.ParserModels
{

    public class CommercialProjectCustomTables
    {
        public int status { get; set; }
        public List<CommercialProjectCustomTableResult> result { get; set; }


        public CommercialProjectCustomTables()
        {
            result = new List<CommercialProjectCustomTableResult>();

            CommercialProjectSquareFootage msqft = new CommercialProjectSquareFootage();

            result.Add(msqft.myresult);

            CommercialProjectMultiFamily mMultiFamily = new CommercialProjectMultiFamily();
            result.Add(mMultiFamily.myresult);

            CommercialProjectGeoInfomation mGeoInfo = new CommercialProjectGeoInfomation();
            result.Add(mGeoInfo.myresult);
        }
    }
    public class CommercialProjectCustomTableResult
    {
        public string id { get; set; }
        public string text { get; set; }
        public int displayOrder { get; set; }
        public List<CommercialProjectField> fields { get; set; }

        public CommercialProjectCustomTableResult()
        {
            fields = new List<CommercialProjectField>();
        }
    }
    public class CommercialProjectSquareFootage
    {
        public CommercialProjectCustomTableResult myresult { get; set; }

        public CommercialProjectSquareFootage()
        {
            myresult = new CommercialProjectCustomTableResult();

            myresult.id = "CE_COM-SQUARE.cFOOTAGE";
            myresult.text = "SQUARE FOOTAGE";
            myresult.displayOrder = 40;
            // build the list of fields//

            myresult.fields = new List<CommercialProjectField>();
            CommercialProjectField mpfield = new CommercialProjectField("Floor", "Floor", null, "N", "Text", 0, "N", 10, null);
            myresult.fields.Add(mpfield);

            mpfield = new CommercialProjectField("Existing", "Existing", null, "N", "Number", 0, "N", 20, null);
            myresult.fields.Add(mpfield);
            mpfield = new CommercialProjectField("Renovation", "Renovation", null, "N", "Number", 0, "N", 30, null);
            myresult.fields.Add(mpfield);
            mpfield = new CommercialProjectField("New", "New", null, "N", "Number", 0, "N", 40, null);
            myresult.fields.Add(mpfield);
            mpfield = new CommercialProjectField("Renovation", "Renovation", null, "N", "Number", 0, "N", 30, null);
            myresult.fields.Add(mpfield);
        }
    }

    public class CommercialProjectField
    {
        public string id { get; set; }
        public string text { get; set; }
        public string isReadonly { get; set; }
        public string fieldType { get; set; }
        public int maxLength { get; set; }
        public string isRequired { get; set; }
        public int displayOrder { get; set; }
        public string value { get; set; }
        public List<CommercialProjectOption> options { get; set; }

        public CommercialProjectField(string NameID, string TextValue, string ValueValue, string IsReadOnly, string FieldType,
            int MaxLength,
            string IsRequired, int DisplayOrder, List<CommercialProjectOption> OptionArray)
        {
            id = NameID;
            text = TextValue;
            value = ValueValue;
            isReadonly = IsReadOnly;
            fieldType = fieldType;
            maxLength = maxLength;
            isRequired = isRequired;
            displayOrder = DisplayOrder;
            List<CommercialProjectOption> options = new List<CommercialProjectOption>();
        }
    }
    public class CommercialProjectMultiFamily
    {
        public CommercialProjectCustomTableResult myresult { get; set; }

        public CommercialProjectMultiFamily()
        {
            myresult = new CommercialProjectCustomTableResult();

            myresult.id = "CE_COM-MULTI.1FAMILY.cUNITS";
            myresult.text = "MULTI-FAMILY UNITS";
            myresult.displayOrder = 70;
            // build the list of fields//

            myresult.fields = new List<CommercialProjectField>();

            CommercialProjectField mpfield = new CommercialProjectField("Building Number Description", "Building Number Description", null, "N", "Text", 0, "N", 10, null);
            myresult.fields.Add(mpfield);

            mpfield = new CommercialProjectField("Number of Units", "Number of Units", null, "N", "Number", 240, "N", 20, null);
            myresult.fields.Add(mpfield);
        }
    }
    public class CommercialProjectOption
    {
        public string value { get; set; }
        public string text { get; set; }
    }

    public class CommercialProjectGeoInfomation
    {
        public CommercialProjectCustomTableResult myresult { get; set; }

        public CommercialProjectGeoInfomation()
        {
            myresult = new CommercialProjectCustomTableResult();

            myresult.id = "CE_COM-GEOGRAPHIC.cINFORMATION";
            myresult.text = "GEOGRAPHIC INFORMATION";
            myresult.displayOrder = 80;
            // build the list of fields//

            myresult.fields = new List<CommercialProjectField>();

            CommercialProjectField mpfield = new CommercialProjectField("Layer Name", "Layer Name", null, "N", "Text", 0, "N", 10, null);
            myresult.fields.Add(mpfield);

            mpfield = new CommercialProjectField("Attribute", "Attribute", null, "N", "Text", 240, "N", 20, null);
            myresult.fields.Add(mpfield);

            mpfield = new CommercialProjectField("Value", "Value", null, "N", "Text", 0, "N", 30, null);
            myresult.fields.Add(mpfield);

        }
    }

    public class CommercialProjectTaskInfomation
    {
        public CommercialProjectCustomTableResult myresult { get; set; }

        public CommercialProjectTaskInfomation()
        { 
            myresult = new CommercialProjectCustomTableResult();
            
            myresult.id = "CE_COM-REVIEW.cTASK.cACTIVATION";
            myresult.text = "REVIEW TASK ACTIVATION";
            myresult.displayOrder = 80;
            // build the list of fields//

            myresult.fields = new List<CommercialProjectField>();

            List<CommercialProjectOption> mCommOpts = new List<CommercialProjectOption>();

            CommercialProjectOption mopt = new CommercialProjectOption();
            mopt.text = string.Empty;
            mopt.value = string.Empty;

            mCommOpts.Add(mopt);

            CommercialProjectField mpfield = new CommercialProjectField("Task Type", "Task Type", "Plan Review", "N", "DropDownList", 0, "N", 10, mCommOpts);
            myresult.fields.Add(mpfield);

            mpfield = new CommercialProjectField("Task Name", "Task Name", null, "N", "Text", 0, "N", 30, mCommOpts);
            myresult.fields.Add(mpfield);

            mpfield = new CommercialProjectField("Reviewer", "Reviewer", null, "N", "Text", 240, "N", 30, null);
            myresult.fields.Add(mpfield);

            mpfield = new CommercialProjectField("Due Date", "Due Date", null, "N", "Date", 240, "N", 40, null);
            myresult.fields.Add(mpfield);

            mpfield = new CommercialProjectField("Cycle #", "Cycle #", null, "N", "Number", 240, "N", 50, null);
            myresult.fields.Add(mpfield);

            mpfield = new CommercialProjectField("Date-Time Stamp", "Date-Time Stamp", null, "N", "Text", 240, "y", 60, null);
            myresult.fields.Add(mpfield);

            mpfield = new CommercialProjectField("Processing Status", "Processing Status", null, "N", "DropDownListr", 240, "N", 70, mCommOpts);
            myresult.fields.Add(mpfield);
        }
    }
}