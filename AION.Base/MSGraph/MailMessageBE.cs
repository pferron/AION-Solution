using System.Collections.Generic;

namespace AION.Base.MSGraph
{
    public class MailMessageBE
    {
        public MessageBE Message { get; set; } = new MessageBE();
        public bool SaveToSentItems { get; set; } = false;
    }

    public class MessageBE
    {
        public string Subject { get; set; }
        public MessageBodyBE Body { get; set; } = new MessageBodyBE();
        public List<MessageRecipientBE> ToRecipients { get; set; } = new List<MessageRecipientBE>();

    }

    public class MessageBodyBE
    {
        public string Content { get; set; }
        public string ContentType { get; set; }
    }
    public class MessageRecipientBE
    {
        public EmailAddressBE EmailAddress { get; set; } = new EmailAddressBE();
    }

    public class EmailAddressBE
    {
        public string Address { get; set; }
    }
}
