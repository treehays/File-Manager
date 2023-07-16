using Document.Manager.Models.DTOs.EmailDTOs;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;

namespace Document.Manager.Gateways.EmailServices;

public class BrevoEmail : IBrevoEmail
{
    private readonly IConfiguration _configuration;

    public BrevoEmail(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<bool> SendEmailWithAttachment(SendAtDTO model)
    {
        var key = _configuration.GetSection("SendinblueAPIKey")["APIKey"];
        var senderName = _configuration.GetSection("SendinblueAPIKey")["SenderName"];
        var senderEmail = _configuration.GetSection("SendinblueAPIKey")["SenderEmail"];
        Configuration.Default.ApiKey.Clear();
        Configuration.Default.ApiKey.Add("api-key", key);
        var apiInstance = new TransactionalEmailsApi();

        var emailSender = new SendSmtpEmailSender(senderName, senderEmail);

        var emailReciever = new SendSmtpEmailTo(model.ReceiverEmail, model.ReceiverEmail);

        var emailRecievers = new List<SendSmtpEmailTo>();
        emailRecievers.Add(emailReciever);

        var replyTo = new SendSmtpEmailReplyTo(senderEmail, "Do not reply");

        var subject = $"My Sample {model.Subject}";


        var htmlContent = $"< html >< body >< h1 > This testing Document Message {model.Body} </ h1 > <h4>Thanks for trying it</h4> </ body ></ html > ";

        //var stringInBase64 = "aGVsbG8gdGhpcyBpcyB0ZXN0";
        //var content = Convert.FromBase64String(stringInBase64);
        //var attachment = new List<SendSmtpEmailAttachment>();
        //foreach (var item in model.Files)
        //{
        //    var attachmentContent = new SendSmtpEmailAttachment
        //    {
        //        Content = item,
        //        Name = model.FIleName,
        //        Url = "http://go.com",
        //    };
        //    attachment.Add(attachmentContent);
        //}
        var sendSmtpEmail = new SendSmtpEmail
        {
            Sender = emailSender,
            HtmlContent = htmlContent,
            Subject = subject,
            ReplyTo = replyTo,
            To = emailRecievers,
            Attachment = model.EmailAttachment,
        };
        try
        {
            var result = await apiInstance.SendTransacEmailAsync(sendSmtpEmail);

            return true;
        }
        catch (Exception)
        {

            return false;
        }
    }
}
