using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.ViewModel
{
    public class DisplayMenuVM
    {
        public MenuVM Menu { get; set; }
        public List<MenuVM> Submenu { get; set; }

        public DisplayMenuVM()
        {
            Submenu = new List<MenuVM>();
        }
    }
}