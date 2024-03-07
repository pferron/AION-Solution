namespace AION.Email.Engine.BusinessEntities
{
    public class SendEmailBE
    {
        public enum MessageType : int
        {
            ErrorMessage = 0,
            SuccessMessage = 1,
            InformationMessage = 2,
            Formatting = 3
        }
    }
}
