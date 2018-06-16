using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Models
{
    public class Tenant
    {
        public Guid TenantId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Occupation { get; set; }

        public string ReferenceName { get; set; }
        public string ReferencedEmail { get; set; }
        public string ReferencePhone { get; set; }

        public DateTime StayStartDate { get; set; }
        public bool IsOnWaitList { get; set; }

        public DateTime CreateDate { get; set; }

        public Guid? HouseId { get; set; }
        public virtual House House { get; set; }
    }
}
