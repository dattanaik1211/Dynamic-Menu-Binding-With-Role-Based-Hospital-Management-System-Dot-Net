using HospitalManagementSystem.Framework.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    [Table("MENU")]
    public class Menu : MasterEntity
    {
        public string MenuName { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public int Order { get; set; }
        public bool IsParent { get; set; }

        public virtual Menu MainMenu { get; set; }

        public Menu()
        {

        }

        public Menu(string menuName,string controller,string action,int order)
        {
            this.MenuName = menuName;
            this.Controller = controller;
            this.Action = action;
            this.Order = order;
        }
    }
}