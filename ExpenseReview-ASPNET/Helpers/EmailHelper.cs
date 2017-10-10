using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace ReimbursementApp.Helpers
{
    public static class EmailHelper
    {
        //TODO: Email configuation reqd
        public static async Task SendEmailAsync(string email, string subject, string message)
        {
            var emailMessage = new MimeMessage();
            //TODO:- Read FROM and TO from the workflow
            emailMessage.From.Add(new MailboxAddress("Rahul Sahay", "rahul.sahay@kdi.kongsberg.com"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart("plain") { Text = message };

            using (var client = new SmtpClient())
            {
                //Not Required
                // client.LocalDomain = "some.domain.com";
                //await client.ConnectAsync("smtp.relay.uri", 25, SecureSocketOptions.None).ConfigureAwait(false);
                await client.ConnectAsync("157.237.41.230", 25, SecureSocketOptions.None).ConfigureAwait(false);
                await client.SendAsync(emailMessage).ConfigureAwait(false);
                await client.DisconnectAsync(true).ConfigureAwait(false);
            }
        }
    }
}
