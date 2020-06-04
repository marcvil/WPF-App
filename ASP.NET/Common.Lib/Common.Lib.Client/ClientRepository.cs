using Common.Lib.Core;
using Common.Lib.Infrastructure;
using System;
using System.Linq;

namespace Common.Lib.Client
{
    public class ClientRepository<T> : IRepository<T> where T : Entity
    {
        public virtual SaveValidation<T> Add()
        {

            // creo una llamada a la web y le paso el entity en formato json
            throw new NotImplementedException();
        }

        public SaveValidation<T> Add(T entity)
        {
            throw new NotImplementedException();
        }

        public DeleteValidation<T> Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public T Find(Guid id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> QueryAll()
        {
            throw new NotImplementedException();
        }

        public SaveValidation<T> Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
