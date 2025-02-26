using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodayWebApi.BLL.Managers
{
    public interface IEmailManager
    {
        Task SendEmailAsync(string toEmail, string subject, string message);
    }
}
