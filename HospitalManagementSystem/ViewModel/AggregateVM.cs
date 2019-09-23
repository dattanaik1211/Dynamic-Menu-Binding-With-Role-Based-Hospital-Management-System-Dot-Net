using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.ViewModel
{
    public class AggregateVM
    {
        public Guid Id { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public string RedirectUrl { get; set; }
    }
}