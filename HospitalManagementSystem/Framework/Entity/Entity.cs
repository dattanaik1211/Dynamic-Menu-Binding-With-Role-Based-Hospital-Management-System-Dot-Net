using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Framework.Entity
{
    public class Entity
    {
        [Key]
        [Column(Order = 1)]
        public Guid Id { get; set; }
    }
}