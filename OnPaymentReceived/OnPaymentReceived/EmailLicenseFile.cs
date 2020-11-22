using System;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using SendGrid.Helpers.Mail;

namespace OnPaymentReceived
{
    //Nuget --> Microsoft.Azure.WebJobs.Extensions.SendGrid
    public static class EmailLicenseFile
    {
        [FunctionName("EmailLicenseFile")]
        public static void Run(
            [BlobTrigger("licenses/{name}", Connection = "AzureWebJobsStorage")]string licenseFileContents,
            [SendGrid(ApiKey = "SendGridApiKey")] out SendGridMessage message,
            string name, 
            ILogger log)
        {
            var email = Regex.Match(input: licenseFileContents,
                        pattern: @"^Email\:\(.+)$", RegexOptions.Multiline).Groups[1].Value;

            log.LogInformation($"Got order from {email}\n License File name:{name}");
            
            message = new SendGridMessage();
            message.From = new EmailAddress(Environment.GetEnvironmentVariable("EmailSender"));
            message.AddTo(email);

            //Need to encode the license file in base64
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(licenseFileContents);
            var base64 = Convert.ToBase64String(plainTextBytes);

            //Add attachment containing the license file
            message.AddAttachment(filename: name, content: base64, type: "text/plain");
            message.Subject = "Your license file";
            message.HtmlContent = "Thank you for your order";
            //if (!email.EndsWith("@test.com"))
            //    sender.Add(message);
        }
    }
}
