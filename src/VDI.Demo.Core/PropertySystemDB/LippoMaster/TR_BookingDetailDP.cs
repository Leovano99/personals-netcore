﻿using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VDI.Demo.PropertySystemDB.Pricing;

namespace VDI.Demo.PropertySystemDB.LippoMaster
{
    [Table("TR_BookingDetailDP")]
    public class TR_BookingDetailDP: AuditedEntity
    {
        public int entityID { get; set; }

        //[Required]
        //[StringLength(20)]
        //public string bookCode { get; set; }

        [ForeignKey("TR_BookingDetail")]
        public int bookingDetailID { get; set; }
        public virtual TR_BookingDetail TR_BookingDetail { get; set; }

        //[Required]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        //public int refNo { get; set; }

        public byte dpNo { get; set; }
        
        public short daysDue { get; set; }

        public short? monthsDue { get; set; }

        public double DPPct { get; set; }

        [Column(TypeName = "money")]
        public decimal DPAmount { get; set; }

        [ForeignKey("LK_DpCalc")]
        public int? dpCalcID { get; set; }
        public virtual LK_DPCalc LK_DpCalc { get; set; }

        [ForeignKey("LK_FormulaDP")]
        public int? formulaDPID { get; set; }
        public virtual LK_FormulaDP LK_FormulaDP { get; set; }

        public bool isSetting { get; set; }

    }
}
