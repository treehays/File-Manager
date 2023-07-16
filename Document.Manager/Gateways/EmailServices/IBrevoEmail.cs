using Document.Manager.Models.DTOs.EmailDTOs;
using Document.Manager.Models.DTOs.UserDTOs;

namespace Document.Manager.Gateways.EmailServices;

public interface IBrevoEmail
{
    Task<bool> SendEmailWithAttachment(SendAtDTO model);

}
