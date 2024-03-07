using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Meck.Shared.Accela
{
    /// <summary>
    ///  Interface model to define a Custom Table row containing fields to be used for adding, updating or deleting fields for end user use only. This is not for Defining new fields. 
    /// </summary>
    public class RequestCustomTablesTasksBE
    {

        /// <summary>
        /// This is the actual recordId that all updates are for, not the Accela record  alias id. 
        /// </summary>
        public string recordId { get; set; }

        /// <summary>
        /// the array of multiple Custom tables to be processed.
        /// </summary>
        public List<TableRowsBE> array { get; set; }

        public RequestCustomTablesTasksBE()
        {
            array = new List<TableRowsBE>();
        }


    }

    /// <summary>
    ///  TableRowsBe  each row holds list fields , and the ID for the CustomTable
    /// </summary>
    public class TableRowsBE
    {
        /// <summary>
        /// string name of the Custom Table
        /// </summary>
        [JsonProperty("id")]
        public string id { get; set; }

        // <summary>
        ///  List of Rows  - contains the Table Id and the Field names and values 
        /// </summary>
        [JsonProperty("rows")]
        public List<TableRowBE> Rows { get; set; }

        /// <summary>
        /// 
        /// </summary>
        //public TableRowsBE()
        //{
        //    Rows = new List<TableRowBE>();
        //     Rows.Add( new TableRowBE( ));
        //}

        /// <summary>
        ///  Iniializer for TableRowsBE requires CustomTablename, ActionEnum and list of newfields. 
        /// </summary>
        /// <param name="CustomTableName"></param>
        /// <param name="toDoAction"></param>
        /// <param name="newFields"></param>
        public TableRowsBE(string CustomTableName, TableRowBE.ActionEnum toDoAction, List<TableFieldBE> newFields)
        {
            id = CustomTableName;
            Rows = new List<TableRowBE>();
            Rows.Add(new TableRowBE(toDoAction, newFields)); ;
        }

        /// <summary>
        ///  This will build out the complete json object table details needed for the AccelaConstruct UpdateWorkFlowCustomTable 
        /// </summary>
        /// <param name="mJasondata"></param>
        /// <returns></returns>
        public string ToJasonString(TableRowsBE mJasondata)
        {
            const string rowspace = "\t \t";

            const string fieldspace = "\t \t \t";
            const string fielddetail = "\t \t \t \t";

            StringBuilder sbFields = new StringBuilder();

            /* used to create Id at end of fields data */

            int outRowsCnt = 0;
            // neededfor the counter Id 

            sbFields.AppendLine("[");
            sbFields.AppendLine(" {");
            sbFields.AppendLine(string.Format("\t \"{0}\":\"{1}\",", "id", mJasondata.id));

            sbFields.AppendLine(string.Format("\t \"{0}\"{1}", "rows", ": ["));
            sbFields.AppendLine(string.Format("{0} {1} ", rowspace, "{"));

            foreach (var taskTableRow in mJasondata.Rows)
            {
                outRowsCnt++;
                sbFields.AppendLine(string.Format("{0} \"{1}\":\"{2}\" ,", fieldspace, "action", taskTableRow.action));
                sbFields.AppendLine(string.Format("{0} \"{1}\"{2}", fieldspace, "fields", ":"));
                sbFields.AppendLine(string.Format("{0} {1} ", fieldspace, "{"));

                for (int indx = 0; indx <= taskTableRow.fields.Count - 1; indx++)
                {
                    if (indx == taskTableRow.fields.Count - 1)
                    {
                        sbFields.AppendLine(string.Format("{0} \"{1}\":\"{2}\"", fielddetail, taskTableRow.fields[indx].text,
                            taskTableRow.fields[indx].value));
                    }
                    else
                    {
                        sbFields.AppendLine(string.Format("{0}\"{1}\":\"{2}\",", fielddetail, taskTableRow.fields[indx].text,
                            taskTableRow.fields[indx].value));
                    }

                }
                // close thefields
                sbFields.AppendLine(string.Format("{0} {1},", fieldspace, "}"));
                sbFields.AppendLine(string.Format("{0}\"{1}\":\"{2}\"", fieldspace, "id", outRowsCnt));
                sbFields.AppendLine(string.Format("{0} {1}", rowspace, "}"));
                sbFields.AppendLine(string.Format("\t ] "));
            }

            // close the rows
            //   sbFields.AppendLine("\t }");
            sbFields.AppendLine(string.Format("{0}", "  }"));
            sbFields.AppendLine(string.Format("{0}", "]"));
            return sbFields.ToString();
        }
    }

    /// <summary>
    /// Contents of one field properties 
    /// </summary>
    public class TableRowBE
    {
        public enum ActionEnum
        {

            /// <summary>
            /// Enum Add for value: add
            /// </summary>
            [EnumMember(Value = "add")] Add = 1,

            /// <summary>
            /// Enum Update for value: update
            /// </summary>
            [EnumMember(Value = "update")] Update = 2,

            /// <summary>
            /// Enum Delete for value: delete
            /// </summary>
            [EnumMember(Value = "delete")]
            Delete = 3,
            
            /// <summary>
            /// Nullvalue used for space holding. 
            /// </summary>
                [EnumMember(Value = null)] NullValue = 4

        }
        /// <summary>
        /// Action to be performed ( Add, Update  or Delete)
        /// </summary>
        [JsonProperty("action")]
        public ActionEnum action { get; set; }
        /// <summary>
        ///  List of field for the table 
        /// </summary>
        [JsonProperty("fields")]
        public List<TableFieldBE> fields { get; set; }

        //public TableRowBE()
        //{
        //    fields = new List<TableFieldBE>();
        //}
        /// <summary>
        /// creates new TableRowBE object ToDoaction is Enum ActionEnum, new fields is the list of new fields to be added to the Table Row 
        /// </summary>
        /// <param name="toDoAction"></param>
        /// <param name="newFields"></param>
        public TableRowBE(ActionEnum toDoAction, List<TableFieldBE> newFields)
        {
            action = toDoAction;
            fields = new List<TableFieldBE>();
            fields = newFields;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class TableFieldBE
    {
        /// <summary>
        ///  id name of the CustomTable 
        /// </summary>
        public string id { get; set; }
        /// <summary>
        ///  Text name of the field
        /// </summary>
        public string text { get; set; }
        /// <summary>
        ///  Value of the Field
        /// </summary>
        public object value { get; set; }
        /// <summary>
        /// If a dropdown field the possible values to be selected
        /// </summary>
        public List<FieldOptionBE> options { get; set; }

        /// <summary>
        /// 
        /// </summary>
        //public TableFieldBE()
        //{
        //    List<FieldOptionBE> options = new List<FieldOptionBE>();
        //}

        /// <summary>
        /// Values used to create one Field detail 
        /// </summary>
        /// <param name="NameID"> name of the Custom Table (optional) </param>
        /// <param name="TextValue"> text name ofthe field (required) </param>
        /// <param name="ValueValue"> value of the field  (use null if not provided)</param>
        /// <param name="OptionList">List ofOptions for the field (used when field type = dropdown) </param>
        public TableFieldBE(string NameID, string TextValue, object ValueValue, List<FieldOptionBE> OptionList = default(List<FieldOptionBE>))
        {
            id = NameID;
            text = TextValue;
            value = ValueValue;
            List<FieldOptionBE> options = new List<FieldOptionBE>();
            options = OptionList;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class FieldOptionBE
    {
        /// <summary>
        ///  Text name of the field Option 
        /// </summary>
        public string text { get; set; }
        /// <summary>
        /// Value of the field option 
        /// </summary>
        public string value { get; set; }
    }
}

