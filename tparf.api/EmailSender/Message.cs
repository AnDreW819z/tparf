using MimeKit;

namespace tparf.api.EmailSender
{
    public class Message
    {
        public List<MailboxAddress> To { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public Message(IEnumerable<string> to, string subject, string contetn)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(x => new MailboxAddress("Торгово-Промышленное Агентство", x)));
            Subject = subject;
            Content = contetn;
        }
    }
}
