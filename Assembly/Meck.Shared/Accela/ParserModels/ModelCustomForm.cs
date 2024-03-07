using System.Collections.Generic;

namespace Meck.Shared.Accela.ParserModels
{
    public class ModelCustomForms
    {
        public int status { get; set; }
        public List<AccelaFormResult> formResults { get; set; }

        public ModelCustomForms()
        {
            formResults = new List<AccelaFormResult>();
        }
        public ModelCustomForms(List<AccelaFormResult> customForms)
        {
            formResults = new List<AccelaFormResult>();
            formResults = customForms;
        }
    }

    public class AccelaFormResult
    {
         public string Id { get; set; }
        public List<AccelaFormField> fields { get; set; }

        public AccelaFormResult()
        {
            fields = new List<AccelaFormField>();
        }
    }

    public class AccelaFormField
    {
        public string id { get; set; }
        public string text { get; set; }
        public List<AccelaFormsFieldOption> options { get; set; }
        public string value { get; set; }

        public AccelaFormField(string NameID, string TextValue, string ValueValue, List<AccelaFormsFieldOption> OptionArray)
        {
            id = NameID;
            text = TextValue;
            value = ValueValue;
            List<AccelaFormsFieldOption> options = new List<AccelaFormsFieldOption>();
            options = OptionArray;
        }
    }

    public class AccelaFormsFieldOption
    {
        public string value { get; set; }
        public string text { get; set; }
    }




}
