using HospitalManagementSystem.Models;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace HospitalManagementSystem.Framework.SearchSpecification
{

    public class MenuCriteria
    {
        public string MenuName { get; set; }
        public string FMenuName { get; set; }
        public string FController { get; set; }
        public string FAction { get; set; }
        public int Order { get; set; }
    }

    public class MenuSpecification : ISpecification<Menu>
    {
        private MenuCriteria _criteria;

        public MenuSpecification(MenuCriteria criteria)
        {
            _criteria = criteria;
        }

        public Expression<Func<Menu, bool>> Expression {
            get
            {
                var builder = PredicateBuilder.True<Menu>();

                if (!string.IsNullOrEmpty(_criteria.MenuName))
                    builder = builder.And(x=>x.MenuName.ToLower().Contains(_criteria.MenuName.ToLower()));

                if (!string.IsNullOrEmpty(_criteria.FMenuName))
                    builder = builder.And(x => x.MenuName.Equals(_criteria.FMenuName));

                if (!string.IsNullOrEmpty(_criteria.FController))
                    builder = builder.And(x => x.Controller.Equals(_criteria.FController));

                if (!string.IsNullOrEmpty(_criteria.FAction))
                    builder = builder.And(x => x.Action.Equals(_criteria.FAction));

                if (_criteria.Order!=0)
                    builder = builder.And(x => x.Order==_criteria.Order);

                return builder;
            }
        }
    }
}