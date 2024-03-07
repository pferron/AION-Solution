using System.Collections.Generic;

namespace Meck.Shared.Accela.ParserModels
{
    public class ResidentialProjectCustomTables
    {
            public int status { get; set; }
        public List<ResidentialProjectResult> result { get; set; }

        public ResidentialProjectCustomTables()
        {
            result = new List<ResidentialProjectResult>();
            
            ResidentialProjectTrades mTrades = new ResidentialProjectTrades();
            result.Add(mTrades.myresult);
            
            ResidentialProjectGEOGRAPHICINFORMATION mGeo = new ResidentialProjectGEOGRAPHICINFORMATION();
            result.Add(mGeo.myresult);

            ResidentialProjectREVIEWTASKACTIVATION  mTasks = new ResidentialProjectREVIEWTASKACTIVATION();
            result.Add(mTasks.myresult);

            
        }
    }

    public class ResidentialProjectResult
    {
        public string id { get; set; }
        public string text { get; set; }
        public int displayOrder { get; set; }
        public List<ResidentialProjectField> fields { get; set; }

        public ResidentialProjectResult()
        {
            fields = new List<ResidentialProjectField>();
        }
    }

    public class ResidentialProjectField
    {
        public string id { get; set; }
        public string text { get; set; }
        public string isReadonly { get; set; }
        public string fieldType { get; set; }
        public int maxLength { get; set; }
        public string isRequired { get; set; }
        public int displayOrder { get; set; }
        public List<ResidentialProjectOption> options { get; set; }
        public string value { get; set; }

        public ResidentialProjectField(string NameID, string TextValue, string ValueValue, string IsReadOnly, string FieldType,
            int MaxLength,
            string IsRequired, int DisplayOrder, List<ResidentialProjectOption> OptionArray)
        {
            id = NameID;
            text = TextValue;
            value = ValueValue;
            isReadonly = IsReadOnly;
            fieldType = fieldType;
            maxLength = maxLength;
            isRequired = isRequired;
            displayOrder = DisplayOrder;
            List<ResidentialProjectOption> options = new List<ResidentialProjectOption>();
        }

    }

    public class ResidentialProjectOption
    {
        public string value { get; set; }
        public string text { get; set; }
    }

    public class ResidentialProjectTrades
    {
        public ResidentialProjectResult myresult { get; set; }

        public ResidentialProjectTrades()
        {
            myresult = new ResidentialProjectResult();

            myresult.id = "CE_RES-REVIEW.cTASK.cACTIVATION";
            myresult.text = "SUB-TRADES";
            myresult.displayOrder = 70;

            List<ResidentialProjectOption> mCommOPT = new List<ResidentialProjectOption>();

            List<ResidentialProjectField> fields = new List<ResidentialProjectField>();

            ResidentialProjectField tfield = new ResidentialProjectField("Trade", "Trade", null, "N", "DropDownList", 0, "N", 10, mCommOPT);

            myresult.fields.Add(tfield);

            tfield = new ResidentialProjectField("ScopeOfWork", "ScopeOfWork", null, "N", "DropDownList", 0, "N", 20, mCommOPT);

            myresult.fields.Add(tfield);

            tfield = new ResidentialProjectField("ContractCost", "ContractCost", null, "N", "Money", 0, "N", 30, null);

            myresult.fields.Add(tfield);

            tfield = new ResidentialProjectField("ContractorID", "ContractorID", null, "N", "Text", 0, "N", 40, null);

            myresult.fields.Add(tfield);

            tfield = new ResidentialProjectField("ContractorName", "ContractorName", null, "N", "Text", 0, "N", 50, null);

            myresult.fields.Add(tfield);

            tfield = new ResidentialProjectField("DetailedScopeOfWork", "DetailedScopeOfWork", null, "N", "TestAreaMoney", 0, "N", 60, null);

            myresult.fields.Add(tfield);
        }
    }
    public class ResidentialProjectGEOGRAPHICINFORMATION
    {
        public ResidentialProjectResult myresult { get; set; }

        public ResidentialProjectGEOGRAPHICINFORMATION()
        {
            myresult = new ResidentialProjectResult();

            myresult.id = "CE_RES-GEOGRAPHIC.cINFORMATION";
            myresult.text = "GEOGRAPHIC INFORMATION";
            myresult.displayOrder = 70;

            List<ResidentialProjectField> fields = new List<ResidentialProjectField>();

            ResidentialProjectField tfield = new ResidentialProjectField("Layer Name", "Layer Name", null, "N", "Text", 0, "N", 10, null);

            myresult.fields.Add(tfield);

            tfield = new ResidentialProjectField("Attribute", "Attribute", null, "N", "Text", 0, "N", 20, null);

            myresult.fields.Add(tfield);

            tfield = new ResidentialProjectField("Value", "Value", null, "N", "Text", 0, "N", 30, null);

            myresult.fields.Add(tfield);
        }
    }

    public partial class ResidentialProjectREVIEWTASKACTIVATION
    {
        public ResidentialProjectResult myresult { get; set; }


        public ResidentialProjectREVIEWTASKACTIVATION()
        {
            myresult = new ResidentialProjectResult();

            myresult.id = "CE_RES-REVIEW.cTASK.cACTIVATION";
            myresult.text = "REVIEW TASK ACTIVATION";
            myresult.displayOrder = 90;

            List<ResidentialProjectOption> mCommOPT = new List<ResidentialProjectOption>();

            List<ResidentialProjectField> fields = new List<ResidentialProjectField>();

            ResidentialProjectField tfield = new ResidentialProjectField("Task Type", "Task Type", "Plan Review", "N", "DropDownList", 240, "Y", 10, mCommOPT);

            myresult.fields.Add(tfield);

            tfield = new ResidentialProjectField("Task Name", "Task Name", null, "N", "DropDownList", 240, "N", 20, mCommOPT);

            myresult.fields.Add(tfield);

            tfield = new ResidentialProjectField("Reviewer", "Reviewer", null, "N", "Text", 240, "N", 30, null);

            myresult.fields.Add(tfield);

            tfield = new ResidentialProjectField("Due Date", "Due Date", null, "N", "Date", 240, "N", 40, null);

            myresult.fields.Add(tfield);

            tfield = new ResidentialProjectField("Cycle #", "Cycle #", null, "N", "Number", 240, "N", 50, null);

            myresult.fields.Add(tfield);

            tfield = new ResidentialProjectField("Date-Time Stamp", "Date-Time Stamp", null, "N", "Text", 240, "Y", 60, null);

            myresult.fields.Add(tfield);
            tfield = new ResidentialProjectField("Processing Status", "Processing Status", null, "N", "DropDownList", 240, "N", 70, mCommOPT);

            myresult.fields.Add(tfield);
        }
    }
}
