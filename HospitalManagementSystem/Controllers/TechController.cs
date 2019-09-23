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
    [Authorize]
    public class TechController : Controller
    {
        private HMSContext _context;
        private IRepository<Role> _roleRepository;
        private IRepository<Menu> _menuRepository;
        private IRepository<MenuRoleMap> _menuRoleMapRepository;

        public TechController()
        {
            _context = new HMSContext();
            _roleRepository = new EFRepository<Role>(_context);
            _menuRepository = new EFRepository<Menu>(_context);
            _menuRoleMapRepository = new EFRepository<MenuRoleMap>(_context);
        }

        // GET: Tech
        public ActionResult Role()
        {
            var roles = _roleRepository.Get().OrderBy(x => x.RoleName).ToList();
            return View(roles.Select(x => RoleVM.GetDTO(x)));
        }

        public ActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateRole(RoleVM roleVM)
        {
            if (ModelState.IsValid)
            {
                var role = new Role(roleVM.RoleName);
                _roleRepository.Add(role);
                _roleRepository.Save();
                return RedirectToAction("Role");
            }
            return View(roleVM);
        }

        public ActionResult Menu()
        {
            var menus = _menuRepository.Get().OrderBy(x => x.Order).ToList();

            List<MenuVM> listOfMenuVM = new List<MenuVM>();
            foreach(var menu in menus)
            {
                var singleVM = MenuVM.GetDTO(menu);
                if (menu.MainMenu != null)
                {
                    singleVM.MainMenuVM = MenuVM.GetDTO(menu.MainMenu);
                }
                listOfMenuVM.Add(singleVM);
            }
            return View(listOfMenuVM);
        }

        [NonAction]
        public Menu GetSearchedMenu(MenuVM vm)
        {
            var criteria = new MenuCriteria { FMenuName = vm.MenuName, FController = vm.Controller, FAction = vm.Action };
            var specification = new MenuSpecification(criteria);
            var menu = _menuRepository.Find(specification).FirstOrDefault();

            return menu;
        }

        public ActionResult UpdateMenus()
        {

            MenuVM v1 = new MenuVM() { MenuName = "Home", Controller = "Home", Action = "Index", Order = 1, IsParent = false };
            MenuVM v2 = new MenuVM() { MenuName = "Role", Controller = "Tech", Action = "Role", Order = 2, IsParent = false };
            MenuVM v3 = new MenuVM() { MenuName = "Menu", Controller = "", Action = "", Order = 3, IsParent = true };
            MenuVM v4 = new MenuVM() { MenuName = "View", Controller = "Tech", Action = "Menu", Order = 4, IsParent = false };
            v4.MainMenuVM = v3;
            MenuVM v5 = new MenuVM() { MenuName = "Manage Menu", Controller = "Tech", Action = "ManageMenu", Order = 5, IsParent = false };
            v5.MainMenuVM = v3;
            MenuVM v6 = new MenuVM() { MenuName = "Employees", Controller = "Employee", Action = "Index", Order = 6, IsParent = false };

            List<MenuVM> menus = new List<MenuVM>()
            {
                v1,v2,v3,v4,v5,v6
            };

            foreach (var menu in menus)
            {
                var newMenu = GetSearchedMenu(menu);
                if (newMenu == null)
                {
                    newMenu = new Menu(menu.MenuName, menu.Controller, menu.Action, menu.Order);
                    _menuRepository.Add(newMenu);
                    _menuRepository.Save();
                }

                if (menu.MainMenuVM != null)
                {
                    var mainMenu = GetSearchedMenu(menu.MainMenuVM);
                    if (mainMenu == null)
                    {
                        mainMenu = new Menu(menu.MainMenuVM.MenuName, menu.MainMenuVM.Controller, menu.MainMenuVM.Action, menu.MainMenuVM.Order);
                        _menuRepository.Add(mainMenu);
                        _menuRepository.Save();
                    }
                    newMenu.MainMenu = mainMenu;
                    _menuRepository.Save();

                }
                newMenu.Order = menu.Order;
                newMenu.IsParent = menu.IsParent;
                _menuRepository.Save();
            }

            var roleList = _roleRepository.Get().ToList();
            var menuList = _menuRepository.Get().ToList();

            foreach (var role in roleList)
            {
                foreach (var menu in menuList)
                {
                    var criteria = new MenuRoleMapCriteria { RoleId = role.Id, MenuId = menu.Id };
                    var specification = new MenuRoleMapSpecification(criteria);
                    var menuRoleMap = _menuRoleMapRepository.Find(specification).FirstOrDefault();

                    if (menuRoleMap == null)
                    {
                        menuRoleMap = new MenuRoleMap();
                        menuRoleMap.Role = role;
                        menuRoleMap.Menu = menu;
                        _menuRoleMapRepository.Add(menuRoleMap);
                        _menuRoleMapRepository.Save();
                    }
                }
            }

            return RedirectToAction("Menu");
        }

        public ActionResult ManageMenu()
        {
            ManageMenuVM vm = new ManageMenuVM();
            vm.Roles = _roleRepository.Get().OrderBy(x => x.RoleName).ToList().Select(x => RoleVM.GetDTO(x)).ToList();
            return View(vm);
        }

        public ActionResult UpdateAccess(Guid? roleId = null)
        {
            var role = _roleRepository.GetById(roleId ?? Guid.Empty);

            if (role == null)
                return null;

            AccessMenuVM vm = new AccessMenuVM();
            vm.Role = RoleVM.GetDTO(role);

            var criteria = new MenuRoleMapCriteria { RoleId = role.Id };
            var specification = new MenuRoleMapSpecification(criteria);
            var menuMapList = _menuRoleMapRepository.Find(specification).OrderBy(x => x.Menu.Order).ToList();

            vm.Menus = menuMapList.Select(x => MenuRoleMapVM.GetDTO(x)).ToList();

            return PartialView("_AccessMenuView", vm);
        }

        [HttpPost]
        public ActionResult UpdateAccess(AccessMenuVM vm)
        {
            foreach (var menuRoleMapVM in vm.Menus)
            {
                var menuRoleMap = _menuRoleMapRepository.GetById(menuRoleMapVM.Id);
                menuRoleMap.IsActive = menuRoleMapVM.IsActive;
                _menuRoleMapRepository.Save();
            }
            return PartialView("_AccessMenuView", vm);
        }
    }
}