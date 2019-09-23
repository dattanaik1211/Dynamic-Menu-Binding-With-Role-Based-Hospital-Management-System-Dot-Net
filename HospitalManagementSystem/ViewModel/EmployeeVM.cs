using HospitalManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.ViewModel
{
    public class EmployeeVM: AggregateVM
    {
        [Display(Name ="Role")]
        [Required(ErrorMessage = "Role Is required")]
        public Guid RoleId { get; set; }

        [Required(ErrorMessage = "First Name Is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name Is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email Is required")]
        [EmailAddress]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "MobileNo Is required")]
        public string MobileNo { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public double Salary { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MMM/yyyy}")]
        public DateTime DateOfJoining { get; set; }
        public RoleVM RoleVM { get; set; }

        public static EmployeeVM GetDTO(Employee employee)
        {
            EmployeeVM employeeVM = null;
            if (employee != null)
            {
                employeeVM = new EmployeeVM()
                {
                    Id = employee.Id,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    EmailId = employee.EmailId,
                    MobileNo = employee.MobileNo,
                    DateOfJoining = employee.DateOfJoining,
                    Password=employee.Password,
                    Salary=employee.Salary,
                    RoleVM=RoleVM.GetDTO(employee.Role)
                };
            }
            return employeeVM;
        }
    }
}