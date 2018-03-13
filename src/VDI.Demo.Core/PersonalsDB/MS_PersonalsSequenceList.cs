using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PersonalsDB
{
    [Table("MS_PersonalsSequenceList")]
    class MS_PersonalsSequenceList : AuditedEntity<string>
    {
        [NotMapped]
        public override string Id
        {
            get
            {
                return SourceName;
            }
            set { /* nothing */ }
        }

        [Key]
        [Column(Order = 0)]
        [StringLength(1)]
        public int EntityCode { get; set; }

        [Key]
        [Column(Order = 0)]
        [StringLength(1)]
        public int StepSequence { get; set; }

        public string SourceName { get; set; }

        [Required]
        [StringLength(150)]
        public string SpCount { get; set; }

        [Required]
        [StringLength(150)]
        public string SpList { get; set; }


        [Required]
        [StringLength(150)]
        public string SpGetPIC { get; set; }
    }
}
