using HospitalManagementSystem.Models;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Framework.SearchSpecification
{
    public class MenuRoleMapCriteria
    {
        public Guid MenuId { get; set; }
        public Guid RoleId { get; set; }
        public bool? IsActive { get; set; }
    }

    public class MenuRoleMapSpecification : ISpecification<MenuRoleMap>
    {
        private MenuRoleMapCriteria _criteria;

        public MenuRoleMapSpecification(MenuRoleMapCriteria criteria)
        {
            _criteria = criteria;
        }

        public System.Linq.Expressions.Expression<Func<MenuRoleMap, bool>> Expression
        {
            get
            {
                var builder = PredicateBuilder.True<MenuRoleMap>();

                if (_criteria.MenuId != Guid.Empty)
                    builder = builder.And(x => x.Menu.Id == _criteria.MenuId);

                if (_criteria.RoleId != Guid.Empty)
                    builder = builder.And(x => x.Role.Id == _criteria.RoleId);

                if (_criteria.IsActive == true)
                    builder = builder.And(x => x.IsActive == true);

                return builder;
            }
        }
    }
}