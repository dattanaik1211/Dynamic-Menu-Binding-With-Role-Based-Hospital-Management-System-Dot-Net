using HospitalManagementSystem.Framework.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    [Table("MENUROLEMAP")]
    public class MenuRoleMap:MasterEntity
    {
        public bool IsActive { get; set; }

        public virtual Menu Menu { get; set; }
        public virtual Role Role { get; set; }

        public MenuRoleMap()
        {

        }
    }
}