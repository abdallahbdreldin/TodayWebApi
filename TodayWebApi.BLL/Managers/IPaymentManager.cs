using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodayWebAPi.DAL.Data.Models;

namespace TodayWebApi.BLL.Managers
{
    public interface IPaymentManager
    {
        Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketId);
    }
}
