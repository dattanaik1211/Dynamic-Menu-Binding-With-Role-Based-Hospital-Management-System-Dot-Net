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
    public class EmployeeController : Controller
    {
        private HMSContext _context;
        private IRepository<Employee> _employeeReposiory;
        private IRepository<Role> _roleReposiory;

        public EmployeeController()
        {
            _context = new HMSContext();
            _employeeReposiory = new EFRepository<Employee>(_context);
            _roleReposiory = new EFRepository<Role>(_context);
            ViewBag.RoleList = _roleReposiory.Get().ToList();
        }

        // GET: Employee
        public ActionResult Index(Guid? roleId = null)
        {
            var criteria = new EmployeeCriteria { RoleId = roleId ?? Guid.Empty };
            var specification = new EmployeeSpecification(criteria);
            var employees = _employeeReposiory.Find(specification);

            List<EmployeeVM> listofEmployeeVM = new List<EmployeeVM>();
            foreach (var employee in employees)
            {
                listofEmployeeVM.Add(EmployeeVM.GetDTO(employee));
            }

            return View(listofEmployeeVM);
        }

        public ActionResult CreateEditEmployee(Guid? id = null)
        {
            EmployeeVM vm = new EmployeeVM();
            ViewBag.ModalTitle = "Add Employee";
            ViewBag.ModalButton = "Add";
            if (id.HasValue)
            {
                var employee = _employeeReposiory.GetById(id.Value);
                ViewBag.ModalTitle = "Edit Employee";
                ViewBag.ModalButton = "Update";
                vm = EmployeeVM.GetDTO(employee);
                vm.RoleId = vm.RoleVM.Id;
            }
            return PartialView(vm);
        }

        [HttpPost]
        public ActionResult CreateEditEmployee(EmployeeVM vm)
        {
            var role = _roleReposiory.GetById(vm.RoleId);
            Employee employee = null;

            if (ModelState.IsValid)
            {
                try
                {
                    if (vm.Id != Guid.Empty)
                    {
                        employee = _employeeReposiory.GetById(vm.Id);
                        employee.Update(vm.FirstName, vm.LastName, vm.EmailId, vm.MobileNo, vm.DateOfJoining, vm.Salary);
                        employee.Role = role;
                    }
                    else
                    {
                        employee = new Employee(vm.FirstName, vm.LastName, vm.EmailId, vm.MobileNo, vm.Password, vm.DateOfJoining, vm.Salary);
                        employee.Role = role;
                        _employeeReposiory.Add(employee);
                    }
                    _employeeReposiory.Save();

                    return Json(new { success = true, message = "Successfully Added" });
                }
                catch (Exception ex)
                {
                    return Json(new { error = true, message = ex.InnerException.Message });
                }
            }
            return PartialView(vm);
        }


    }
}