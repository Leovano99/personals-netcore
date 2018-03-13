﻿using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.LippoMaster
{
    [Table("TR_BookingDetailAddDisc")]
    public class TR_BookingDetailAddDisc : AuditedEntity
    {
        public int entityID { get; set; }

        //[StringLength(20)]
        //public string bookCode { get; set; }

        [ForeignKey("TR_BookingDetail")]
        public int bookingDetailID { get; set; }
        public virtual TR_BookingDetail TR_BookingDetail { get; set; }

        //public short refNo { get; set; }

        public short addDiscNo { get; set; }

        public double pctAddDisc { get; set; }

        [Column(TypeName = "money")]
        public decimal amtAddDisc { get; set; }

        public bool isAmount { get; set; }

        [Required]
        [StringLength(500)]
        public string addDiscDesc { get; set; }

    }
}
