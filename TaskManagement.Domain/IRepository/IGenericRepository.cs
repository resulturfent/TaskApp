using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagement.Domain.IRepository
{
    public interface IGenericRepository<MyEntity> where MyEntity : class
    {
        List<MyEntity> GetAll();//Hepsini listeler
        IQueryable<MyEntity> GetAllQuery(Expression<Func<MyEntity,bool>> predicate);//Hepsini listeler

        MyEntity GetByIdAsync(int id);
        bool Any(Expression<Func<MyEntity, bool>> predicate);      

        void Add(MyEntity entity);
        void Update(MyEntity entity);
        void Remove(MyEntity entity);

        IEnumerable<MyEntity> Find(Expression<Func<MyEntity, bool>> predicate);

    }
}
