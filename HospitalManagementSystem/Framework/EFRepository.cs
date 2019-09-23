using HospitalManagementSystem.Framework.Entity;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Framework
{
    public class EFRepository<T> : IRepository<T> where T : AggregateEntity
    {

        private HMSContext _context;
        public EFRepository(HMSContext context)
        {
            this._context = context;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(Guid entityId)
        {
            _context.Set<T>().Remove(GetById(entityId));
        }

        public IQueryable<T> Get()
        {
            var queryable = _context.Set<T>();
            return queryable;
        }

        public T GetById(Guid entityId)
        {
            var entity = _context.Set<T>().SingleOrDefault(x => x.Id == entityId);
            return entity;
        }

        public IQueryable<T> Find(ISpecification<T> specification)
        {
            var queryable = _context.Set<T>().AsExpandable().Where(specification.Expression);
            return queryable;
        }

        public virtual void Update(T entity)
        {
            //throw new NotImplementedException();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}