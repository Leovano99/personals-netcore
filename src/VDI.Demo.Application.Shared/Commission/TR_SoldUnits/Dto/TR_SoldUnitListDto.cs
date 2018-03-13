using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VDI.Demo.Commission.TR_SoldUnits.Dto
{
    public class TR_SoldUnitListDto : AuditedEntityDto
    {
        [Required]
        [StringLength(20)]
        public string bookNo { get; set; }

        [Required]
        [StringLength(11)]
        public string batchNo { get; set; }

        [Required]
        [StringLength(12)]
        public string memberCode { get; set; }

        [Required]
        [StringLength(12)]
        public string CDCode { get; set; }

        [Required]
        [StringLength(12)]
        public string ACDCode { get; set; }

        [Required]
        [StringLength(20)]
        public string roadCode { get; set; }

        [Required]
        [StringLength(50)]
        public string roadName { get; set; }

        [Required]
        [StringLength(10)]
        public string unitNo { get; set; }

        public DateTime bookDate { get; set; }

        public float unitLandArea { get; set; }

        public float unitBuildArea { get; set; }
        
        public decimal netNetPrice { get; set; }
        
        public decimal unitPrice { get; set; }

        public double pctComm { get; set; }

        public double pctBobot { get; set; }

        public DateTime? PPJBDate { get; set; }

        public DateTime? xreqInstPayDate { get; set; }

        public DateTime? xprocessDate { get; set; }

        public DateTime? cancelDate { get; set; }

        [StringLength(100)]
        public string Remarks { get; set; }

        public DateTime? holdDate { get; set; }

        public bool calculateUseMaster { get; set; }

        [Required]
        [StringLength(40)]
        public string termRemarks { get; set; }

        [StringLength(100)]
        public string holdReason { get; set; }

        [StringLength(100)]
        public string changeDealClosureReason { get; set; }
        
        public int schemaID { get; set; }
        
        public int developerSchemaID { get; set; }

        public int unitID { get; set; }

        public int entityID { get; set; }
    }
}
