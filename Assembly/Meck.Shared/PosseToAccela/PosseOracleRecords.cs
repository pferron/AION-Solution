namespace Meck.Shared.PosseToAccela
{
    public class PosseOracleRecord 
    {
        public string ProjectNum { get; set; }
        public string TaskName { get; set; }
        //public string Status { get; set; }
        public string XmlData { get; set; }
      //  public Int32 t_seq { get; set;  }

        public PosseOracleRecord( string projectNum, string taskName, string xmlData )
        {
            ProjectNum = projectNum;
            TaskName = taskName;
          //  Status = "Pending";
            XmlData = xmlData; 
        }
    }

}
