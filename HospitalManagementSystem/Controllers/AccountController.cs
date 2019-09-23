using HospitalManagementSystem.Framework;
using HospitalManagementSystem.Framework.SearchSpecification;
using HospitalManagementSystem.Models;
using HospitalManagementSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace HospitalManagementSystem.Controllers
{
    public class AccountController : Controller
    {

        private HMSContext _context;
        private IRepository<MenuRoleMap> _menuRoleMapRepository;
        private IRepository<Employee> _employeeRepository;

        public AccountController()
        {
            _context = new HMSContext();
            _menuRoleMapRepository = new EFRepository<MenuRoleMap>(_context);
            _employeeRepository = new EFRepository<Employee>(_context);
        }

        // GET: Account
        [AllowAnonymous]
        public ActionResult Login()
        {
            LoginVM vm = new LoginVM();
            if (Request.Cookies["Username"] != null && Request.Cookies["Password"] != null)
            {
                vm.Username = Request.Cookies["Username"].Value;
                vm.Password = Request.Cookies["Password"].Value;
                var cookie = FormsAuthentication.FormsCookieName;
            }
            return View(vm);
        }

        [HttpPost]
        public ActionResult Login(LoginVM loginVM)
        {
            if (ModelState.IsValid)
            {
                var employeeCriteria = new EmployeeCriteria { EmailId = loginVM.Username, Password = loginVM.Password };
                var employeeSpecification = new EmployeeSpecification(employeeCriteria);
                var employee = _employeeRepository.Find(employeeSpecification).FirstOrDefault();

                if (employee == null)
                {
                    ModelState.AddModelError("", "Invalid Username and Password");
                }
                else
                {
                    FormsAuthentication.SetAuthCookie(loginVM.Username, loginVM.RememberMe);
                    if (loginVM.RememberMe)
                    {
                        Response.Cookies["Username"].Value = loginVM.Username;
                        Response.Cookies["Password"].Value = loginVM.Password;
                        Response.Cookies["Username"].Expires = DateTime.UtcNow.AddMonths(12);
                        Response.Cookies["Password"].Expires = DateTime.UtcNow.AddMonths(12);
                    }
                    else
                    {
                        Response.Cookies["Username"].Expires = DateTime.UtcNow.AddMinutes(-1);
                        Response.Cookies["Password"].Expires = DateTime.UtcNow.AddMinutes(-1);
                    }

                    var criteria = new MenuRoleMapCriteria { RoleId = employee.Role.Id, IsActive = true };
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
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(loginVM);
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterVM registerVM)
        {
            if (ModelState.IsValid)
            {
                //return View();
                ModelState.AddModelError("", "Please enter valid username and password");

                return RedirectToAction("Login");
            }
            return View(registerVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            Session.Clear();
            Session.RemoveAll();
            return RedirectToAction("Index", "Home");
        }
    }
}