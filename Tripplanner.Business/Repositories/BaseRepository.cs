using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using SQLite;
using Tripplanner.Business.Models;
using Tripplanner.Business.Services;

namespace Tripplanner.Business.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T : IUnique, ICopyable<T>, new()
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

        public virtual T GetById(int id)
        {
            return database.Get<T>(id);
        }

        public virtual T GetByPredicate(Func<T, bool> predicate)
        {
            return database.Get<T>(predicate);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return database.Table<T>();
        }

        public IEnumerable<T> Where(Expression<Func<T, bool>> filter)
        {
            return database.Table<T>().Where(filter);
        }

        public virtual bool Delete(T entity)
        {
            var rowCount = database.Delete(entity);
            return rowCount > 0;
        }

        public virtual bool Update(T entity)
        {
            var rowCount = database.Update(entity) > 0;
            return rowCount;
        }

        public virtual bool AddOrUpdate(IEnumerable<T> entities)
        {
            //var all = GetAll().ToList();
            try
            {
                database.BeginTransaction();
                foreach (var entity in entities)
                {
                    AddOrUpdateSingle(entity);
                }
                database.Commit();
                return true;
            }
            catch (Exception ex)
            {
                //TODO: Log...
                database.Rollback();
                return false;
            }
        }

        public virtual bool AddOrUpdateSingle(T entity)
        {
            var rowCount = database.InsertOrReplace(entity);
            return rowCount > 0;
        }
    }
}
