using HospitalManagementSystem.Framework.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    [Table("ROLE")]
    public class Role : MasterEntity
    {
        public string RoleName { get; set; }

        public Role()
        {

        }

        public Role(string name)
        {
            this.RoleName = name;
        }

        public static Role Create(string name)
        {
            return new Role(name);
        }
    }
}