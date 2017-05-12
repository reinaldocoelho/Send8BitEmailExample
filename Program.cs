using SendEmailsTest;
using System;
using System.Text;

namespace Send8BitEmail
{
    public class Program
    {
        // SMTP SERVER LOCAL
        private static string _smtpServer = "smtp.gmail.com";
        private static int _smtpPort = 587;
        private static bool _enableSsl = true;
        private static bool _useDefaultCredential = true;
        private static string _userName = "ronaldo.lambda@gmail.com";
        private static string _password = "xxxxxxx";
        // Email data
        //private static string _mailFrom = "teste@testmail.com";
        private static string _mailFrom = "ronaldo.lambda@gmail.com";
        private static string _mailTo = "reinaldo@sorridents.encontact.tmp.br";
        private static string _subject = "Test accent subject çãõáéíóú";
        private static string _body = "Body with special char çãõáéíóú";

        static void Main(string[] args)
        {
            var loopQuantity = 1;
            if(args.Length > 0 && args[0].StartsWith("-q"))
            {
                if(!Int32.TryParse(args[0].TrimStart('-', 'q'), out loopQuantity))
                {
                    Console.WriteLine("ERROR: Parameter -q must be numeric. Using only one loop quantity.");
                }
            }
            
            for(int i = 0; i < loopQuantity; i++)
            {
                SendEmail8BitIso88591();
                SendEmail8BitUtf8();
                SendEmailBase64Encoded();
                SendEmailQuotedPrintableEncoded();
                SendEmailSevenBitASCIIOnlyEncoded();
            }
        }

        private static void SendEmail8BitIso88591()
        {
            var email = new EmailGenerator(_mailTo);
            email.AppendTextToSubject(_subject);
            email.AppendTextToBody(_body);
            email.SetTransferEncoding(System.Net.Mime.TransferEncoding.EightBit);
            email.SetEncoding(Encoding.GetEncoding("iso-8859-1"));
            var server = new SmtpServerConfig(_smtpServer, _smtpPort, _enableSsl, _useDefaultCredential, _userName, _password);
            email.SendEmail(server);
        }

        private static void SendEmail8BitUtf8()
        {
            var email = new EmailGenerator(_mailTo);
            email.AppendTextToSubject(_subject);
            email.AppendTextToBody(_body);
            email.SetTransferEncoding(System.Net.Mime.TransferEncoding.EightBit);
            email.SetEncoding(Encoding.UTF8);
            var server = new SmtpServerConfig(_smtpServer, _smtpPort, _enableSsl, _useDefaultCredential, _userName, _password);
            email.SendEmail(server);
        }

        private static void SendEmailBase64Encoded()
        {
            var email = new EmailGenerator(_mailTo);
            email.AppendTextToSubject(_subject);
            email.AppendTextToBody(_body);
            email.SetTransferEncoding(System.Net.Mime.TransferEncoding.Base64);
            var server = new SmtpServerConfig(_smtpServer, _smtpPort, _enableSsl, _useDefaultCredential, _userName, _password);
            email.SendEmail(server);
        }

        private static void SendEmailQuotedPrintableEncoded()
        {
            var email = new EmailGenerator(_mailTo);
            email.AppendTextToSubject(_subject);
            email.AppendTextToBody(_body);
            email.SetTransferEncoding(System.Net.Mime.TransferEncoding.QuotedPrintable);
            var server = new SmtpServerConfig(_smtpServer, _smtpPort, _enableSsl, _useDefaultCredential, _userName, _password);
            email.SendEmail(server);
        }

        private static void SendEmailSevenBitASCIIOnlyEncoded()
        {
            var email = new EmailGenerator(_mailTo);
            email.AppendTextToSubject(_subject);
            email.AppendTextToBody(_body);
            email.SetTransferEncoding(System.Net.Mime.TransferEncoding.SevenBit);
            var server = new SmtpServerConfig(_smtpServer, _smtpPort, _enableSsl, _useDefaultCredential, _userName, _password);
            email.SendEmail(server);
        }
    }
}
