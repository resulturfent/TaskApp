using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.IRepository;

namespace TaskManagement.DataLayer.Repository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly TaskManagementDbContext _taskManagementDb;


        public GenericRepository(TaskManagementDbContext taskManagementDb)
        {
            //DI
            _taskManagementDb = taskManagementDb;

        }

        public void Add(TEntity entity)
        {
           _taskManagementDb.Add(entity);
            _taskManagementDb.SaveChanges();
        }

        public bool Any(Expression<Func<TEntity, bool>> predicate)
        {
           return _taskManagementDb.Set<TEntity>().Any(predicate);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public List<TEntity> GetAll()
        {
            return _taskManagementDb.Set<TEntity>().ToList();
        }

        public IQueryable<TEntity> GetAllQuery(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public TEntity GetQuery(Expression<Func<TEntity, bool>> predicate)
        {
            return _taskManagementDb.Set<TEntity>().Where(predicate).FirstOrDefault();
        }


        public TEntity GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(TEntity entity)
        {
           _taskManagementDb.Set<TEntity>().Remove(entity);
            _taskManagementDb.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
