using HospitalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.ViewModel
{
    public class MenuRoleMapVM
    {
        public Guid Id { get; set; }
        public bool IsActive { get; set; }
        public MenuVM MenuVM { get; set; }
        public RoleVM RoleVM { get; set; }

        public static MenuRoleMapVM GetDTO(MenuRoleMap menuRoleMap)
        {
            MenuRoleMapVM menuRoleMapVM = null;
            if (menuRoleMap != null)
            {
                menuRoleMapVM = new MenuRoleMapVM()
                {
                    Id = menuRoleMap.Id,
                    IsActive = menuRoleMap.IsActive,
                    RoleVM = RoleVM.GetDTO(menuRoleMap.Role),
                    MenuVM = MenuVM.GetDTO(menuRoleMap.Menu)
                };
            }
            return menuRoleMapVM;
        }
    }
}