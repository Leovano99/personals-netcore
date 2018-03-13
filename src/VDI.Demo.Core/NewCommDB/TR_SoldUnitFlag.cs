using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace VDI.Demo.NewCommDB
{
    [Table("TR_SoldUnitFlag")]
    public class TR_SoldUnitFlag : Entity<string>
    {
        [NotMapped]
        public override string Id
        {
            get
            {
                return entityCode +
                    "-" + devCode +
                    "-" + bookNo;
            }
            set { /* nothing */ }
        }
        [Key]
        [Column(Order = 0)]
        [StringLength(1)]
        public string entityCode { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(5)]
        public string devCode { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(20)]
        public string bookNo { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(1)]
        public string flagCode { get; set; }

        public DateTime flagDate { get; set; }

        [Required]
        [StringLength(100)]
        public string remarks { get; set; }

        public DateTime modifTime { get; set; }

        [Required]
        [StringLength(50)]
        public string modifUN { get; set; }

        public DateTime inputTime { get; set; }

        [Required]
        [StringLength(50)]
        public string inputUN { get; set; }
    }
}
