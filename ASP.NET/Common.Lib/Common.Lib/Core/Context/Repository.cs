﻿

using Common.Lib.Core;
using Common.Lib.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Lib.Core
{
    public abstract class Repository<T> : IRepository<T> where T : Entity
    {
        public static Dictionary<Guid, T> DbSet { get; set; } = new Dictionary<Guid, T>();

        public IQueryable<T> QueryAll()
        {
            return DbSet.Values.AsQueryable<T>();
        }

        public T Find(Guid id)
        {
            return DbSet[id];

        }

        public virtual SaveValidation<T> Add(T entity)
        {
            var output = new SaveValidation<T>(true);

            if (entity.Id == Guid.Empty)
            {
                entity.Id = Guid.NewGuid();
            }
            if (DbSet.ContainsKey(entity.Id))
            {
                output.SaveValidationSuccesful = false;
                output.SaveValidationMessages.Add("Ya existe con este GUID");
            }

            if (output.SaveValidationSuccesful)
            {
                DbSet.Add(entity.Id, entity);

            }

            return output;
        }

        public virtual SaveValidation<T> Update(T entity)
        {
            var output = new SaveValidation<T>();

            if (entity.Id == default(Guid))
            {
                output.SaveValidationSuccesful = false;
                output.SaveValidationMessages.Add("Cannot update an entity without GUID.");
            }
            if (entity.Id != default(Guid) && !DbSet.ContainsKey(entity.Id))
            {
                output.SaveValidationSuccesful = false;
                output.SaveValidationMessages.Add("An entity with this GUID doesn't exist");
            }

            if (output.SaveValidationSuccesful)
            {
                DbSet[entity.Id] = entity;
            }

            return output;
        }
        public virtual DeleteValidation<T> Delete(T entity)
        {
            throw new NotImplementedException();
        }

        
    }
}
