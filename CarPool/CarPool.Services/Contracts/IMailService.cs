using CarPool.Services.Mapping.DTOs;
using System.Threading.Tasks;

namespace CarPool.Services.Contracts
{
    public interface IMailService
    {
        Task SendEmailAsync(MailDTO mailRequest);
    }
}