using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodayWebAPi.DAL.Data.Models;
using TodayWebAPi.DAL.Repos.Generic;

namespace TodayWebAPi.DAL.Repos.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepo<TEntity> Repo<TEntity>() where TEntity : BaseClass;
        Task<int> Complete();
    }
}
