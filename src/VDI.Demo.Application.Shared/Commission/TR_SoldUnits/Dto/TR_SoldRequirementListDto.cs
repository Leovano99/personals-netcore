using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VDI.Demo.Commission.TR_SoldUnits.Dto
{
    public class TR_SoldRequirementListDto : AuditedEntityDto
    {
        [Required]
        [StringLength(20)]
        public string bookNo { get; set; }

        public byte reqNo { get; set; }

        [Required]
        [StringLength(40)]
        public string reqDesc { get; set; }

        public double pctPaid { get; set; }

        public double? orPctPaid { get; set; }

        public DateTime? reqDate { get; set; }

        public DateTime? processDate { get; set; }
        
        public int schemaID { get; set; }
        
        public int developerSchemaID { get; set; }

        public int entityID { get; set; }
    }
}
