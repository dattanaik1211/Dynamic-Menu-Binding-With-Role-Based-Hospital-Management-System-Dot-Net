using HospitalManagementSystem.Framework.Entity;
using HospitalManagementSystem.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Models
{
    public class Employee:MasterEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public DateTime DateOfJoining { get; set; }
        public double Salary { get; set; }
        public virtual Role Role { get; set; }

        public Employee()
        {

        }

        public Employee(string firstName, string lastName, string emailId, string mobileNo,string password,DateTime dateOfJoining,double salary)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.EmailId = emailId;
            this.MobileNo = mobileNo;
            this.Password = password;
            this.DateOfJoining = dateOfJoining;
            this.Salary = salary;
        }

        public void Update(string firstName, string lastName, string emailId, string mobileNo, DateTime dateOfJoining, double salary)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.EmailId = emailId;
            this.MobileNo = mobileNo;
            this.DateOfJoining = dateOfJoining;
            this.Salary = Salary;
        }
    }
}