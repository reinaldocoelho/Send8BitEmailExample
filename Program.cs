﻿using System;
using System.Net.Mail;
using System.Net.Mime;

namespace Send8BitEmail
{
    class Program
    {
        static void Main(string[] args)
        {
            SendEmail8Bit();
        }

        public static void SendEmail8Bit()
        {
            var message = new MailMessage("ronaldo.lambda@gmail.com", "teste@foca.encontact.tmp.br")
            {
                Subject = "Assunto teste acento çãõáéíóú",
                IsBodyHtml = false,
                Body = null,
                BodyEncoding = System.Text.Encoding.UTF8
            };
            using (AlternateView body = AlternateView.CreateAlternateViewFromString(
                    "Assunto teste acento çãõáéíóú",
                    message.BodyEncoding,
                    message.IsBodyHtml ? "text/html" : null
                )
            )
            {
                body.TransferEncoding = TransferEncoding.EightBit;
                message.AlternateViews.Add(body);
                try
                {
                    using (var smtp = new SmtpClient("localhost", 25))
                    {
                        smtp.EnableSsl = false;
                        //smtp.UseDefaultCredentials = true;
                        //smtp.Credentials = new System.Net.NetworkCredential("ronaldo.lambda@gmail.com", "teste666");
                        smtp.Send(message);
                    }
                    Console.WriteLine("E-mail enviado com sucesso.");
                }
                catch (SmtpException ex)
                {
                    Console.WriteLine("Erro ao enviar email. Ex:" + ex.Message);
                    System.Diagnostics.Debug.WriteLine(
                        ex.Message);
                }
            }
        }
    }
}
