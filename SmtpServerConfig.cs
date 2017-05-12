using System;
using System.Net.Mail;

namespace SendEmailsTest
{
    public class SmtpServerConfig
    {
        private string _smtpServer;
        private int _smtpPort;
        private bool _enableSsl;
        private bool _useDefaultCredential;
        private string _userName;
        private string _password;
        private SmtpDeliveryMethod _deliveryMethod;
        private SmtpDeliveryFormat _deliveryFormat;
        private string _lastSentError;

        public SmtpServerConfig()
        {
            _smtpServer = "localhost";
            _smtpPort = 9025;
            _userName = "";
            _password = "";
            _deliveryMethod = SmtpDeliveryMethod.Network;
            _deliveryFormat = SmtpDeliveryFormat.International;
            _lastSentError = "";
        }

        public SmtpServerConfig(string smtpServer, int port) : this()
        {
            _smtpServer = smtpServer;
            _smtpPort = port;
        }

        public SmtpServerConfig(string smtpServer, int port, bool enableSsl) : this(smtpServer, port)
        {
            _enableSsl = enableSsl;
        }

        public SmtpServerConfig(string smtpServer, int port, bool enableSsl, bool useCredential, string username, string password) : this(smtpServer, port, enableSsl)
        {
            _useDefaultCredential = useCredential;
            _userName = username;
            _password = password;
        }

        public SmtpServerConfig(string smtpServer, int port, bool enableSsl, bool useCredential, string username, string password, SmtpDeliveryMethod deliveryMethod, SmtpDeliveryFormat deliveryFormat) : this(smtpServer, port, enableSsl, useCredential, username, password)
        {
            _deliveryMethod = deliveryMethod;
            _deliveryFormat = deliveryFormat;
        }

        public string GetLastError()
        {
            return _lastSentError;
        }

        public void ChangeDeliveryStructure(SmtpDeliveryMethod deliveryMethod, SmtpDeliveryFormat deliveryFormat)
        {
            _deliveryMethod = deliveryMethod;
            _deliveryFormat = deliveryFormat;
        }

        internal bool Send(MailMessage message)
        {
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
                return true;
            }
            catch (SmtpException ex)
            {
                Console.WriteLine("Erro ao enviar email. Ex:" + ex.Message);
                _lastSentError = ex.Message;
                return false;
            }
        }

        internal bool SendAsync(MailMessage message)
        {
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
                    smtp.SendAsync(message, null);
                }
                return true;
            }
            catch (SmtpException ex)
            {
                Console.WriteLine("Erro ao enviar email. Ex:" + ex.Message);
                _lastSentError = ex.Message;
                return false;
            }
        }
    }
}