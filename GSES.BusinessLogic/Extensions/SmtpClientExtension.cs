﻿using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;

namespace GSES.BusinessLogic.Extensions
{
    public static class SmtpClientExtension
    {
        public static async Task SendMessagesOnEmails(this SmtpClient client, IEnumerable<string> emails, string subject, string message)
        {
            foreach (var email in emails)
            {
                await client.SendMailAsync(new MailMessage("", email, subject, message));
            }
        }
    }
}
