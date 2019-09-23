using HospitalManagementSystem.Framework;
using HospitalManagementSystem.Framework.SearchSpecification;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HospitalManagementSystem.Controllers
{

    public class HomeController : Controller
    {

        private HMSContext _context;
        private IRepository<MenuRoleMap> _menuRoleMapRepository;

        public HomeController()
        {
            _context = new HMSContext();
            _menuRoleMapRepository = new EFRepository<MenuRoleMap>(_context);
        }

        // GET: Home
        public ActionResult Index()
        {
            if (this.User.Identity.IsAuthenticated && Session["MenuMaster"] == null)
            {
                var criteria = new MenuRoleMapCriteria { RoleId = new Guid("eb05d6ba-edf8-4fda-97e7-1409c4b14ea1"), IsActive = true };
                var specification = new MenuRoleMapSpecification(criteria);
                var menus = _menuRoleMapRepository.Find(specification).OrderBy(x => x.Menu.Order).ToList();

                List<MenuVM> listOfMenu = new List<MenuVM>();

                foreach (var menu in menus)
                {
                    if (menu.Menu.IsParent)
                    {
                        var submenus = menus.Where(x => x.Menu.MainMenu != null && x.Menu.MainMenu.Id.Equals(menu.Menu.Id)).Select(x => x.Menu).ToList();
                        if (submenus.Count() != 0)
                        {
                            var mainMenuVM = MenuVM.GetDTO(menu.Menu);
                            foreach (var submenu in submenus)
                            {
                                mainMenuVM.SubMenuList.Add(MenuVM.GetDTO(submenu));
                            }
                            listOfMenu.Add(mainMenuVM);
                        }
                    }
                    else if (!menu.Menu.IsParent && menu.Menu.MainMenu == null)
                    {
                        listOfMenu.Add(MenuVM.GetDTO(menu.Menu));
                    }
                    else { continue; }
                }
                Session["MenuMaster"] = listOfMenu;

            }

            return View();
        }
    }
}