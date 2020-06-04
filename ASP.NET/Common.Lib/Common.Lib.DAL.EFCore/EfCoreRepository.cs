using Common.Lib.Core;

using Common.Lib.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Common.Lib.DAL.EFCore
{
    public class EfCoreRepository<T> : IRepository<T> where T : Entity
    {
        DbContext DbContext { get; set; }

        DbSet<T> DbSet
        {
            get
            {
                return DbContext.Set<T>();
            }
        }

        public EfCoreRepository()
        {

        }

        public EfCoreRepository(DbContext context)
        {
            DbContext = context;
        }

        public IQueryable<T> QueryAll()
        {
            return DbSet.AsQueryable();
        }
        public T Find(Guid id)
        {
            return DbSet.Find(id);
        }

        public virtual SaveValidation<T> Add(T entity)
        {
           
            var output = new SaveValidation<T>
            {
                SaveValidationSuccesful = true
            };

            if (entity.Id == default(Guid))
                entity.Id = Guid.NewGuid();

            if (DbSet.Any(x => x.Id == entity.Id))
            {
                output.SaveValidationSuccesful = false;
                output.Validation.Messages.Add("Ya existe una entity con ese id");
            }

            if (output.SaveValidationSuccesful)
            {
                DbSet.Add(entity);
                DbContext.SaveChanges();
            }

            return output;
        }

        public virtual SaveValidation<T> Update(T entity)
        {
            var output = new SaveValidation<T>
            {
                SaveValidationSuccesful = true
            };

            if (entity.Id == default(Guid))
            {
                output.SaveValidationSuccesful = false;
                output.Validation.Messages.Add("No se puede actualizar una entidad sin Id");
            }

            //if (entity.Id != default(Guid) && !DbSet.Any(x => x.Id == entity.Id))
            if (entity.Id != default(Guid) && DbSet.All(x => x.Id != entity.Id)) // esta es mejor porque tiene mejor performance
            {
                output.SaveValidationSuccesful = false;
                output.Validation.Messages.Add("No existe una entity con ese id");
            }

            if (output.SaveValidationSuccesful)
            {
                DbSet.Update(entity);
            }

            return output;
        }

        public virtual DeleteValidation<T> Delete(T entity)
        {
            var output = new DeleteValidation<T>()
            {
                DeleteValidationSuccesful = true
            };

            if (DbSet.All(x => x.Id != entity.Id))
            {
                output.DeleteValidationSuccesful = false;
                output.Validation.Messages.Add("No existe una entity con ese id");
            }

            if (output.DeleteValidationSuccesful)
            {
                DbSet.Remove(entity);
            }

            return output;
        }

    }
}
