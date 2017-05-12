using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace SendEmailsTest
{
    public class EmailGenerator
    {
        private Encoding _encode;
        private string _lineBreak;
        private string _mailFrom;
        private string _mailTo;
        private string _subject;
        private string _body;
        private bool _isHtmlBody;
        private TransferEncoding _transferEncoding = TransferEncoding.Base64;

        public EmailGenerator()
        {
            _mailFrom = Faker.Internet.Email(Faker.Name.FullName());
            _mailTo = Faker.Internet.Email(Faker.Name.FullName());
            _subject = Faker.Lorem.Sentence(3);
            _body = string.Join(_lineBreak, Faker.Lorem.Paragraphs(10));
            _encode = Encoding.UTF8;
            _lineBreak = "\r\n";
        }

        public EmailGenerator(string to) : this()
        {
            _mailTo = to;
        }

        public EmailGenerator(string to, string subject) : this(to)
        {
            _subject = subject;
        }

        public EmailGenerator(string to, string subject, string textBody, bool isHtml) : this(to, subject)
        {
            _body = textBody;
            _isHtmlBody = isHtml;
        }

        public void SetEncoding(Encoding encode)
        {
            _encode = encode;
        }

        public void SetTransferEncoding(TransferEncoding transferEncoding)
        {
            _transferEncoding = transferEncoding;
        }

        public void AppendTextToSubject(string textToAppend)
        {
            _subject += textToAppend;
        }

        public void AppendTextToBody(string textToAppend)
        {
            _body += textToAppend;
        }

        public void SendEmail(SmtpServerConfig config)
        {
            var message = new MailMessage(_mailFrom, _mailTo)
            {
                Subject = _subject,
                IsBodyHtml = _isHtmlBody,
                BodyEncoding = _encode,
                HeadersEncoding = _encode,
                SubjectEncoding = _encode
            };
            using (AlternateView body = AlternateView.CreateAlternateViewFromString(
                    _body,
                    message.BodyEncoding,
                    message.IsBodyHtml ? "text/html" : "text/plain"
            ))
            {
                body.TransferEncoding = _transferEncoding;
                message.AlternateViews.Add(body);
                config.Send(message);
            }
        }
    }
}
