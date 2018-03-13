using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VDI.Demo.Authorization.Users;

namespace VDI.Demo.PropertySystemDB.OnlineBooking.DemoDB
{
    public class MP_UserPersonals : AuditedEntity
    {
        public string psCode { get; set; }

        public int? userType { get; set; }

        [ForeignKey("User")]
        public long userID { get; set; }
        public virtual User User { get; set; }
    }
}
