using HospitalManagementSystem.Framework.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Framework
{
    public interface IRepository<T> where T : AggregateEntity
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(Guid entityId);
        IQueryable<T> Get();
        T GetById(Guid entityId);
        IQueryable<T> Find(ISpecification<T> specification);
        void Save();
    }
}
