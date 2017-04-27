using System;
using System.Net.Mail;
using System.Net.Mime;

namespace Send8BitEmail
{
    class Program
    {
        private static string _mailFrom = "ronaldo.lambda@gmail.com";
        private static string _mailTo = "reinaldo.enkilabs@gmail.com";
        private static string _subject = "Assunto teste acento çãõáéíóú";
        private static string _body = "Corpo do e-mail de teste acento çãõáéíóú";
        private static SmtpDeliveryMethod _deliveryMethod = SmtpDeliveryMethod.Network;
        private static SmtpDeliveryFormat _deliveryFormat = SmtpDeliveryFormat.International;

        // SMTP SERVER LOCAL
        //private static string _smtpServer = "localhost";
        //private static int _smtpPort = 25;
        //private static bool _enableSsl = false;
        //private static bool _useDefaultCredential = false;
        
        // SMTP SERVER GMAIL
        private static string _smtpServer = "smtp.gmail.com";
        private static int _smtpPort = 587;
        private static bool _enableSsl = true;
        private static bool _useDefaultCredential = true;
        private static string _userName = "ronaldo.lambda@gmail.com";
        private static string _password = "teste666";


        static void Main(string[] args)
        {
            SendEmail8BitUtf8();
            SendEmail8BitIso88591();
        }

        public static void SendEmail8BitUtf8()
        {
            var message = new MailMessage(_mailFrom, _mailTo)
            {
                Subject = _subject,
                IsBodyHtml = false,
                BodyEncoding = System.Text.Encoding.UTF8,
                HeadersEncoding = System.Text.Encoding.UTF8,
                SubjectEncoding = System.Text.Encoding.UTF8
            };
            using (AlternateView body = AlternateView.CreateAlternateViewFromString(
                    _body,
                    message.BodyEncoding,
                    message.IsBodyHtml ? "text/html" : "text/plain"
                )
            )
            {
                body.TransferEncoding = TransferEncoding.EightBit;
                message.AlternateViews.Add(body);
                try
                {
                    using (var smtp = new SmtpClient(_smtpServer, _smtpPort))
                    {
                        smtp.DeliveryMethod = _deliveryMethod;
                        smtp.DeliveryFormat = _deliveryFormat;
                        smtp.EnableSsl = _enableSsl;
                        smtp.UseDefaultCredentials = _useDefaultCredential;
                        if (_useDefaultCredential)
                        {
                            smtp.Credentials = new System.Net.NetworkCredential(_userName, _password);
                        }
                        smtp.Send(message);
                    }
                    Console.WriteLine("E-mail UTF8 enviado com sucesso.");
                }
                catch (SmtpException ex)
                {
                    Console.WriteLine("Erro ao enviar email UTF8. Ex:" + ex.Message);
                    System.Diagnostics.Debug.WriteLine(
                        ex.Message);
                }
            }
        }
        public static void SendEmail8BitIso88591()
        {
            var message = new MailMessage(_mailFrom, _mailTo)
            {
                Subject = _subject,
                IsBodyHtml = false,
                BodyEncoding = System.Text.Encoding.GetEncoding("iso-8859-1"),
                HeadersEncoding = System.Text.Encoding.GetEncoding("iso-8859-1"),
                SubjectEncoding = System.Text.Encoding.GetEncoding("iso-8859-1")
            };
            using (AlternateView body = AlternateView.CreateAlternateViewFromString(
                    _body,
                    message.BodyEncoding,
                    message.IsBodyHtml ? "text/html" : "text/plain"
                )
            )
            {
                body.TransferEncoding = TransferEncoding.EightBit;
                message.AlternateViews.Add(body);
                try
                {
                    using (var smtp = new SmtpClient(_smtpServer, _smtpPort))
                    {
                        smtp.DeliveryMethod = _deliveryMethod;
                        smtp.DeliveryFormat = _deliveryFormat;
                        smtp.EnableSsl = _enableSsl;
                        smtp.UseDefaultCredentials = _useDefaultCredential;
                        if (_useDefaultCredential)
                        {
                            smtp.Credentials = new System.Net.NetworkCredential(_userName, _password);
                        }
                        smtp.Send(message);
                    }
                    Console.WriteLine("E-mail UTF8 enviado com sucesso.");
                }
                catch (SmtpException ex)
                {
                    Console.WriteLine("Erro ao enviar email ISO. Ex:" + ex.Message);
                    System.Diagnostics.Debug.WriteLine(
                        ex.Message);
                }
            }
        }
    }
}
