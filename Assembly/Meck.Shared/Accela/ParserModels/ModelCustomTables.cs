using System.Collections;
using System.Collections.Generic;

namespace Meck.Shared.Accela.ParserModels
{
    public class ModelCustomTables
    {
        public int status { get; set; }

        public List<AccelaTableResult> customTablesResult { get; set; }

        public string ParseErrors { get; set; }
       public bool HasErrors { get; set; }
     

        public ModelCustomTables()
        {
            customTablesResult = new List<AccelaTableResult>();

       }

    }

    public class AccelaTableResult
    {
        public string  id { get; set; }

        public ArrayList  rows { get; set; }

        public AccelaTableResult( )
        {
            rows = new ArrayList();
        }

        public AccelaTableResult(string newId, ArrayList newRows )
        {
            if (! string.IsNullOrWhiteSpace(newId) )
            {
                id = newId;  
            }
            if  (newRows != null)
            {
                rows = new ArrayList();
                rows = newRows;
            }
        }

    } 

    public class Rows
  {

        public ArrayList fields  { get; set; }

      public Rows( ArrayList newFields)
      {
          fields = new ArrayList() ;
          fields = newFields;
      }
      
    }

    public class AccelaTableField
    {
        public string id { get; set; }
        public string text { get; set; }
        public object value { get; set; }
        public List<AccelaTableFieldOption> options { get; set; }

        public AccelaTableField()
        {
            List<AccelaTableFieldOption> options = new List<AccelaTableFieldOption>();
        }


        public AccelaTableField(string NameID, string TextValue, object ValueValue, List<AccelaTableFieldOption> OptionList)
        {
            id = NameID;
            text = TextValue;
            value = ValueValue;
            List<AccelaTableFieldOption> options = new List<AccelaTableFieldOption>();
            options = OptionList;
        }
    }

    public class AccelaTableFieldOption
    {
        public string value { get; set; }
        public string text { get; set; }
    }
}
