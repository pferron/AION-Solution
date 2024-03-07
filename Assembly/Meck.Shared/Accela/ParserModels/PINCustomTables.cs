using System.Collections.Generic;

namespace Meck.Shared.Accela.ParserModels
{
    public class PINCustomTables
    {
        public int status { get; set; }
        public List<PinResult> result { get; set; }

        public PINCustomTables()
        {
            result =  new List<PinResult>();

            PINRECRECORDcMATCHcCRITERIA mPinMatcHcCriteria  = new PINRECRECORDcMATCHcCRITERIA();

             result.Add(mPinMatcHcCriteria.myresult);

             
        }
    }

    public class PinResult
    {
        public string id { get; set; }
        public string text { get; set; }
        public int displayOrder { get; set; }
        public List<PINCustomField> fields { get; set; }

        public PinResult()
        {
            fields = new List<PINCustomField>();
        }
    }

    public class PINCustomField
    {
        public string id { get; set; }
        public string text { get; set; }

        public string value { get; set; }
        public string isReadonly { get; set; }
        public string fieldType { get; set; }
        public int maxLength { get; set; }
        public string isRequired { get; set; }
        public int displayOrder { get; set; }

        public PINCustomField(string NameID, string TextValue, string valueValue, string IsReadOnly, string FieldType, int MaxLength,
            string IsRequired, int DisplayOrder)
        {
            id = NameID;
            text = TextValue;
            value = 
            isReadonly = IsReadOnly;
            fieldType = fieldType;
            maxLength = maxLength;
            isRequired = isRequired;
            displayOrder = DisplayOrder;

        }
    }

    public partial class PINRECRECORDcMATCHcCRITERIA
    {
        public PinResult myresult { get; set; }
   
        public PINRECRECORDcMATCHcCRITERIA()
        {
           myresult = new PinResult();
           myresult.id = "PIN_REC - RECORD.cMATCH.cCRITERIA";
           myresult.text = "RECORD MATCH CRITERIA";
           myresult.displayOrder = 0;

            List<PINCustomField> fields = new List<PINCustomField>();

            PINCustomField tfield = new PINCustomField("Record Number", "Record Number",null, "N", "Text", 0 , "N", 10);

           myresult.fields.Add(tfield);

            tfield = new PINCustomField("PIN #", "PIN #", null, "N", "Text", 240, "N", 20);

           myresult.fields.Add(tfield);
        }
    }
}
