namespace Meck.Shared.Accela
{
   public class DocumentStatusWrapperBE
    {
        public int? Status { get; set; }
        public string FilePath { get; set; }

        public string DocumentID { get; set; }
        

        public DocumentStatusWrapperBE( int? newStatus, string newFilePath)
        {
            Status = newStatus;
            FilePath = newFilePath;
        }

    }
}
