using System;
using System.IO;
using System.Threading.Tasks;
using FunctionalService;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using ModelService;
using Serilog;

namespace EmailService
{
    public class EmailSvc : IEmailSvc
    {
        private readonly SendGridOptions _sendGridOptions;
        private readonly IFunctionalSvc _functionalSvc;
        private readonly SmtpOptions _smtpOptions;
        private readonly IHostingEnvironment _hostingEnvironment;

        public EmailSvc(IOptions<SendGridOptions> sendGridOptions,
           IFunctionalSvc functionalSvc,
           IOptions<SmtpOptions> smtpOptions, IHostingEnvironment hostingEnvironment)
        {
            _sendGridOptions = sendGridOptions.Value;
            _smtpOptions = smtpOptions.Value;
            _hostingEnvironment = hostingEnvironment;
            _functionalSvc = functionalSvc;
        }

        public Task SendEmailAsync(string email, string subject, string message, string template,string moreText)
        {
            var strMessageBody = BuildEmailBody(message, template, subject,moreText);

            // Check for Default emails Sending Options from App settings
            if (_sendGridOptions.IsDefault)
            {
                _functionalSvc.SendEmailBySendGridAsync(_sendGridOptions.SendGridKey, _sendGridOptions.FromEmail, _sendGridOptions.FromFullName, subject, strMessageBody, email).Wait();
            }

            if (!_smtpOptions.IsDefault) return Task.CompletedTask;

            if (!string.IsNullOrEmpty(strMessageBody))
            {
                //using (StreamWriter sw = new StreamWriter("C:\\Windo\\publish\\wwwroot\\text.txt", true))
                //{
                //    sw.WriteLine(email + " " + " before send");
                //}
                // Then we need to send email using SMTP
                _functionalSvc.SendEmailByGmailAsync(_smtpOptions.FromEmail,
                    _smtpOptions.FromFullName,
                    subject,
                    strMessageBody,
                    email.Trim(),
                    email.Trim(),
                    _smtpOptions.SmtpUserName,
                    _smtpOptions.SmtpPassword,
                    _smtpOptions.SmtpHost,
                    _smtpOptions.SmtpPort,
                    _smtpOptions.SmtpSsl).Wait();
            }
            //using (StreamWriter sw = new StreamWriter("C:\\Windo\\publish\\wwwroot\\text.txt", true))
            //{
            //    sw.WriteLine(email + " " + " after send");
            //}
            return Task.CompletedTask;
        }

        private string BuildEmailBody(string message, string templateName, string subject,string moreText)
        {
            var strMessage = "";

            try
            {
                var strTemplateFilePath = _hostingEnvironment.ContentRootPath + "/EmailTemplates/" + templateName;
                var reader = new StreamReader(strTemplateFilePath);
                strMessage = reader.ReadToEnd();
                reader.Close();
            }
            catch (Exception ex)
            {
                Log.Error("An error occurred while seeding the database  {Error} {StackTrace} {InnerException} {Source}",
                    ex.Message, ex.StackTrace, ex.InnerException, ex.Source);
            }
            strMessage = strMessage.Replace("[[[Title]]]", string.IsNullOrEmpty(subject) ? "הודעה => Windo.com" : subject);
            strMessage = strMessage.Replace("[[[message]]]", message);
            strMessage = strMessage.Replace("[[[moreText]]]", moreText);
            return strMessage;
        }
    }
}
