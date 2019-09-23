using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.ViewModel
{
    public class LoginVM
    {
        [Required(ErrorMessage ="Username is required")]
        public string Username { get; set; }
        [Required(ErrorMessage ="Password is Required")]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}