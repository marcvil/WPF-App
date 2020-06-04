using System;
using Common.Lib.Infrastructure;

using System.Linq;

namespace Common.Lib.Core
{   
    public interface IRepository<T> where T : Entity
    {

        IQueryable<T> QueryAll();

        T Find(Guid id);

        SaveValidation<T> Add(T entity);

        SaveValidation<T> Update(T entity);

        DeleteValidation<T> Delete(T entity);
      
    }
}
