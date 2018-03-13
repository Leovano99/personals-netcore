using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VDI.Demo.Commission.TR_SoldUnits.Dto
{
    public class TR_CommPctListDto : AuditedEntityDto
    {
        [Required]
        [StringLength(20)]
        public string bookNo { get; set; }

        [Required]
        [StringLength(20)]
        public string memberCodeR { get; set; }

        [Required]
        [StringLength(20)]
        public string memberCodeN { get; set; }

        public short asUplineNo { get; set; }

        public double commPctPaid { get; set; }
        
        public int developerSchemaID { get; set; }
        
        public int commTypeID { get; set; }
        
        public int statusMemberID { get; set; }
        
        public int pointTypeID { get; set; }
        
        public int pphRangeInsID { get; set; }
        
        public int pphRangeID { get; set; }

        public int entityID { get; set; }
    }
}
