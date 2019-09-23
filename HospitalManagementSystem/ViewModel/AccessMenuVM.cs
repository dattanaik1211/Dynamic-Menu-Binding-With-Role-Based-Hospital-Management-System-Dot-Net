using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.ViewModel
{
    public class AccessMenuVM
    {
        public RoleVM Role { get; set; }
        public List<MenuRoleMapVM> Menus { get; set; }

        public AccessMenuVM()
        {
            Menus = new List<MenuRoleMapVM>();
        }
    }
}