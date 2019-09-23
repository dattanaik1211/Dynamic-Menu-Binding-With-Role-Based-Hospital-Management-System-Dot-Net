using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HospitalManagementSystem.Framework.Entity
{
    public class MasterEntity:AggregateEntity
    {
        private static TimeZoneInfo INDIAN_TIME = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

        public DateTime CreatedOn { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public Guid? LastModifiedBy { get; set; }

        public MasterEntity()
        {
            this.Id = Guid.NewGuid();
            this.CreatedOn = this.LastModifiedOn = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_TIME);
        }
    }
}