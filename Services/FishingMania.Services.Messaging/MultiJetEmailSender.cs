namespace FishingMania.Services.Messaging
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Mailjet.Client;
    using Mailjet.Client.Resources;
    using Mailjet.Client.TransactionalEmails;
    using SendGrid;
    using SendGrid.Helpers.Mail.Model;

    public class MultiJetEmailSender : IEmailSender
    {
        private readonly MailjetClient client;

        public MultiJetEmailSender(string apiKey, string apiSecret)
        {
            this.client = new MailjetClient(apiKey, apiSecret);
        }

        public async Task<bool> SendEmailAsync(string from, string fromName, string to, string subject, string htmlContent, IEnumerable<EmailAttachment> attachments = null)
        {
            try
            {
                MailjetRequest request = new MailjetRequest
                {
                    Resource = Send.Resource
                };

                var content = await FormatEmailTemplate(from, subject, to, htmlContent);

                var email = new TransactionalEmailBuilder()
                       .WithFrom(new SendContact(from))
                .WithSubject(subject)
                       .WithHtmlPart(content)
                       .WithTo(new SendContact(to))
                       .Build();

                var response = await client.SendTransactionalEmailAsync(email);
                var message = response.Messages[0];

                bool result = message.Status.ToLower() == "success";

                Console.WriteLine("Email sent successfully.");

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString(), "Error sending email.");

                return false;
            }
        }

        public async Task<string> FormatEmailTemplate(string fromEmail, string subject, string toEmail, string message)
        {
            string filePath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, $"Files/email.html"));
            string template = File.ReadAllText($"{filePath}");

            template = template.Replace("{fromEmail}", fromEmail);
            template = template.Replace("{subject}", subject);
            template = template.Replace("{message}", message);

            return template;
        }
    }
}
