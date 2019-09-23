using HospitalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.ViewModel
{
    public class MenuVM
    {
        public Guid Id { get; set; }
        public string MenuName { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public int Order { get; set; }
        public bool IsParent { get; set; }

        public MenuVM MainMenuVM { get; set; }
        public List<MenuVM> SubMenuList { get; set; }

        public MenuVM()
        {
            SubMenuList = new List<MenuVM>();
        }

        public static MenuVM GetDTO(Menu menu)
        {
            MenuVM menuVM = null;
            if (menu != null)
            {
                menuVM = new MenuVM()
                {
                    Id = menu.Id,
                    MenuName = menu.MenuName,
                    Controller = menu.Controller,
                    Action = menu.Action,
                    Order = menu.Order,
                    IsParent=menu.IsParent
                };
            }
            return menuVM;
        }

    }
}