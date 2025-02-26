using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodayWebAPi.DAL.Data.Context;
using TodayWebAPi.DAL.Data.Models;
using TodayWebAPi.DAL.Repos.Generic;

namespace TodayWebAPi.DAL.Repos.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _Context;
        private Hashtable _Repositories;
        public UnitOfWork(StoreContext context)
        {
            _Context = context;
        }

        public async Task<int> Complete()
        {
            return await _Context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _Context.Dispose();
        }

        public IGenericRepo<TEntity> Repo<TEntity>() where TEntity : BaseClass
        {
            if(_Repositories == null) _Repositories = new Hashtable();

            var type = typeof(TEntity).Name;

            if (!_Repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepo<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)),_Context);

                _Repositories.Add(type, repositoryInstance);
            }

            return (IGenericRepo<TEntity>)_Repositories[type];
        }
    }
}
