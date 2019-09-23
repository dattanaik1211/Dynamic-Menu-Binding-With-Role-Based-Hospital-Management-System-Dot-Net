using HospitalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalManagementSystem.ViewModel
{
    public class ManageMenuVM
    {
        [Required(ErrorMessage ="Role id required")]
        [Display(Name ="Role")]
        public Guid RoleId { get; set; }
        public List<RoleVM> Roles { get; set; }
        
        public ManageMenuVM()
        {
            Roles = new List<RoleVM>();
        }
    }
}