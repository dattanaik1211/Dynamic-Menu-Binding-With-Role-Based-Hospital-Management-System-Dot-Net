using HospitalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.ViewModel
{
    public class RoleVM
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage ="Role Name Is required")]
        public string RoleName { get; set; }

        public static RoleVM GetDTO(Role role)
        {
            RoleVM roleVM = null;
            if (role != null)
            {
                roleVM = new RoleVM()
                {
                    Id = role.Id,
                    RoleName = role.RoleName
                };
            }
            return roleVM;
        }
    }
}