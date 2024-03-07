namespace ReadAIONDb.Dtos
{
    public class ReadPROJECT_CYCLEDto
    {  
        public int id_user { get; set; }
        public int PROJECT_CYCLE_ID { get; set; }
        public int PROJECT_ID { get; set; }
        public int CYCLE_NBR { get; set; }        
        public bool CURRENT_CYCLE_IND { get; set; }
    }
}
