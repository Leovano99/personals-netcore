using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VDI.Demo.TAXDB;

namespace VDI.Demo.EntityFrameworkCore
{
    public class TAXDbContext : AbpDbContext
    {        
        public virtual DbSet<FP_TR_FPHeader> FP_TR_FPHeader { get; set; }

        public virtual DbSet<FP_TR_FPDetail> FP_TR_FPDetail { get; set; }

        public virtual DbSet<FP_LK_FPTransCode> FP_LK_FPTransCode { get; set; }

        public virtual DbSet<msBatchPajakStock> msBatchPajakStock { get; set; }

        public TAXDbContext(DbContextOptions<TAXDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FP_TR_FPHeader>()
                .HasKey(c => new { c.entityCode, c.coCode, c.FPCode });

            modelBuilder.Entity<FP_TR_FPDetail>()
                .HasKey(c => new { c.entityCode, c.coCode, c.FPCode, c.transNo });

            modelBuilder.Entity<msBatchPajakStock>()
                .HasKey(c => new { c.BatchID, c.CoCode, c.FPBranchCode, c.FPYear, c.FPNo });

            //modelBuilder.Entity<FP_TR_FPHeader>()
            //    .HasMany(e => e.FP_TR_FPDetail)
            //    .WithOne(e => e.FP_TR_FPHeader)
            //    .HasForeignKey(e => new { e.entityCode, e.coCode, e.FPCode });
            //.HasConstraintName("ForeignKey_FP_TR_FPHeader_FP_TR_FPDetail");

            //modelBuilder.Entity<MS_Account>()
            //            .HasIndex(b => b.accCode)
            //            .IsUnique()
            //            .HasName("accCodeUnique");



            ////No Delete On Action
            //modelBuilder.Entity<MS_BankBranch>()
            //    .HasOne(t => t.LK_BankLevel)
            //    .WithMany(w => w.MS_BankBranch)
            //    .HasForeignKey(d => d.bankBranchTypeID)
            //    .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
            //Database.SetInitializer<PropertySystemDbContext>(null);
        }
    }
}
