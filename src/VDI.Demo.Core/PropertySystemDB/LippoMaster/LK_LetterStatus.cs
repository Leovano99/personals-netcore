using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.LippoMaster
{
    [Table("LK_LetterStatus")]
    public class LK_LetterStatus : AuditedEntity
    {
        public int entityID { get; set; }

        [Required]
        [StringLength(3)]
        public string letterStatusCode { get; set; }

        [Required]
        [StringLength(20)]
        public string letterStatusDesc { get; set; }

        public ICollection<TR_ReminderLetter> TR_ReminderLetter { get; set; }
    }
}
