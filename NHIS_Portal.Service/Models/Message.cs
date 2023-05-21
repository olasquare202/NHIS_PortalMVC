using MimeKit;

namespace NHIS_Portal.Service.Models
{
    public class Message//This is request body for sending email in C#
    {
        public List<MailboxAddress> To { get; set; }
        public string? Subject { get; set; }
        public string? Content { get; set; }
        public Message(IEnumerable<string> to, string? subject, string? content)
        {
            To = new List<MailboxAddress>();
            To.AddRange(to.Select(x => new MailboxAddress("email", x)));
            Subject = subject;
            Content = content;
        }   
    }
}
