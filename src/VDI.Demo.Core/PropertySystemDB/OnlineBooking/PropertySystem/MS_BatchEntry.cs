using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.PropertySystemDB.OnlineBooking.PropertySystem
{
    [Table("MS_BatchEntry")]
    public class MS_BatchEntry : Entity<int>
    {
        [NotMapped]
        public override int Id
        {
            get
            {
                return batchSeq;
            }
            set
            {
            }
        }

        [Key]
        public int batchSeq { get; set; }

        [Required]
        [StringLength(3)]
        public string batchCode { get; set; }

        public int batchStartNum { get; set; }

        public int? batchMaxNum { get; set; }

        public int maxTopupFromOldBatch { get; set; }

        public int isTopupOnly { get; set; }

        public int? isBookingFee { get; set; }

        public int? isConvertOnly { get; set; }

        public int? isSellOnly { get; set; }

        [StringLength(10)]
        public string projectCode { get; set; }

        public int minToken { get; set; }

        public int maxToken { get; set; }

        [Required]
        public decimal priorityPassPrice { get; set; }

        public int isActive { get; set; }

        //[Column("inputTime")]
        //public override DateTime CreationTime { get; set; }

        //[Column("inputUN")]
        //public override long? CreatorUserId { get; set; }

        [StringLength(50)]
        public string FloorSize { get; set; }

        public decimal? FloorSizeMath { get; set; }

        [StringLength(100)]
        public string ClusterName { get; set; }

        [StringLength(10)]
        public string ClusterCode { get; set; }

        public bool isRunOut { get; set; }

        public ICollection<TR_PriorityPass> TR_PriorityPass { get; set; }

    }
}
