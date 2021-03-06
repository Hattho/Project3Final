﻿using System;
using System.Collections.Generic;
using System.Text;
using TrainTravel.ApplicationLogic.Abstractions;
using System.Linq;

namespace TrainTravel.DataAcess
{
    public class BaseRepository<T> : IRepository<T> where T : class, new()
    {
        protected readonly TrainTravelDbContext dbContext;

        public BaseRepository(TrainTravelDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public T Add(T itemToAdd)
        {
            var entity = dbContext.Add<T>(itemToAdd);
            dbContext.SaveChanges();
            return entity.Entity;
        }

        public T GetById(Guid Id)
        {
            return dbContext.Set<T>()
                            .Find(Id);
        }

        public bool Delete(T itemToDelete)
        {
            dbContext.Remove<T>(itemToDelete);
            dbContext.SaveChanges();
            return true;

        }

        public IEnumerable<T> GetAll()
        {
            return dbContext.Set<T>()
                            .AsEnumerable();
        }

        public T Update(T itemToUpdate)
        {
            var entity = dbContext.Update<T>(itemToUpdate);
            dbContext.SaveChanges();
            return entity.Entity;
        }

    }
}
