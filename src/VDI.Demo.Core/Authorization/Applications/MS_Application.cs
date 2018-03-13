using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VDI.Demo.Authorization.Applications
{
    public class MS_Application : CreationAuditedEntity<int>
    {
        [Required, StringLength(50)]
        public string AppName { get; set; }
        [Required, StringLength(500)]
        public string AppDesc { get; set; }
        public int? ParentId { get; set; }
    }
}
