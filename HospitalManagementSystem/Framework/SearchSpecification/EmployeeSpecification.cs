using HospitalManagementSystem.Models;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Framework.SearchSpecification
{
    public class EmployeeCriteria
    {
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public string Password { get; set; }
    }

    public class EmployeeSpecification : ISpecification<Employee>
    {
        private EmployeeCriteria _criteria;

        public EmployeeSpecification(EmployeeCriteria criteria)
        {
            _criteria = criteria;
        }

        public System.Linq.Expressions.Expression<Func<Employee, bool>> Expression
        {
            get
            {
                var builder = PredicateBuilder.True<Employee>();

                if (_criteria.Id != Guid.Empty)
                    builder = builder.And(x => x.Id == _criteria.Id);

                if (_criteria.RoleId != Guid.Empty)
                    builder = builder.And(x => x.Role.Id == _criteria.RoleId);

                if (!string.IsNullOrEmpty(_criteria.EmailId))
                    builder = builder.And(x => x.EmailId.ToLower().Equals(_criteria.EmailId.ToLower()));

                if (!string.IsNullOrEmpty(_criteria.MobileNo))
                    builder = builder.And(x => x.MobileNo.Equals(_criteria.MobileNo));

                if (!string.IsNullOrEmpty(_criteria.Password))
                    builder = builder.And(x => x.Password.Equals(_criteria.Password));

                return builder;
            }
        }
    }
}