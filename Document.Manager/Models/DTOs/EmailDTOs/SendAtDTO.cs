using sib_api_v3_sdk.Model;

namespace Document.Manager.Models.DTOs.EmailDTOs;

public class SendAtDTO
{
    public string ReceiverName { get; set; }
    public string ReceiverEmail { get; set; }
    public string Body { get; set; }
    public string Subject { get; set; }
    //public string FilePath { get; set; }
    public List<SendSmtpEmailAttachment> EmailAttachment { get; set; }
    //public string FIleName { get; set; }
}
