using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SQLite;
using Tripplanner.Business.Services;

namespace Tripplanner.Business.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T : new()
    {
        private readonly IStorageService storageService;
        private readonly SQLiteConnection database;

        protected BaseRepository(IStorageService storageService)
        {
            this.storageService = storageService;
            database = new SQLiteConnection(Path.Combine(storageService.RootPath, Constants.DbName));
            database.CreateTable<T>();
        }
        public virtual bool Add(T entity)
        {
            var rowCount = database.Insert(entity);
            return rowCount > 0;
        }

        public virtual T Get(int id)
        {
            return database.Get<T>(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return database.Table<T>();
        }

        public virtual bool Delete(T entity)
        {
            var rowCount = database.Delete(entity);
            return rowCount > 0;
        }
    }
}
