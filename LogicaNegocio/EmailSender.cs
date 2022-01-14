using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

namespace LogicaNegocio
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return null;
        }
    }
}
