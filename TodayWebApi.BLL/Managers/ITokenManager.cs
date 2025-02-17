using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodayWebAPi.DAL.Data.Identity;

namespace TodayWebApi.BLL.Managers
{
    public interface ITokenManager
    {
        Task<string> CreateToken(User user);
    }
}
