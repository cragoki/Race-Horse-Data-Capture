using Core.Models.Mail;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IMailService
    {
        Task SendEmailAsync(MailModel mailRequest);
    }
}