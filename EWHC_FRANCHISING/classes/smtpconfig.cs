using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Limilabs.Mail;
using Limilabs.Mail.Headers;
using Limilabs.Client.SMTP;
using System.Configuration;
using System.Security.Cryptography;
using System.Net.Configuration;
using System.Windows;
using Limilabs.Mail.MIME;

namespace EWHC_FRANCHISING.classes
{
    public class smtpconfig
    {
            
        public smtpconfig() { }

        public ISendMessageResult sendEmail(string[] from, 
                                            string[] to, 
                                            string subject, 
                                            string htmlBody, 
                                            string attachment
                                            )
        { 
            ISendMessageResult sendMessageResult;
            MailBuilder mailBuilder= new MailBuilder();
            SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");
            
            mailBuilder.From.Add(new MailBox(from[0], from[1]));
            foreach (string mail in to)  mailBuilder.To.Add(new MailBox(mail)); 

            mailBuilder.Subject = subject;
            mailBuilder.Html = htmlBody;
    
            if (attachment != "")
             mailBuilder.AddAttachment(@attachment);
            
            IMail emailMessage = mailBuilder.Create();
            using (Smtp smtp = new Smtp())
            {
                smtp.Connect(smtpSection.Network.Host, 
                             smtpSection.Network.Port,
                             true);
                smtp.UseBestLogin(smtpSection.Network.UserName, smtpSection.Network.Password);

                    sendMessageResult = smtp.SendMessage(emailMessage);
            }
                return sendMessageResult;

        }
    
    }
}
