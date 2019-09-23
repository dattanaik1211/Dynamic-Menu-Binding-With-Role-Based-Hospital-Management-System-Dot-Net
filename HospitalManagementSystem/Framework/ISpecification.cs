using HospitalManagementSystem.Framework.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem.Framework
{
    public interface ISpecification<T> where T:AggregateEntity
    {
        Expression<Func<T, bool>> Expression { get; }
    }
}
