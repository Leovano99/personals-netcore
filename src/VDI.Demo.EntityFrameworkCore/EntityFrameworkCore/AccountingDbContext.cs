using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VDI.Demo.AccountingDB;

namespace VDI.Demo.EntityFrameworkCore
{
    public class AccountingDbContext : AbpDbContext
    {
        public virtual DbSet<MS_COA> MS_COA { get; set; }

        public virtual DbSet<MS_COAFIN> MS_COAFIN { get; set; }

        public virtual DbSet<MS_Mapping> MS_Mapping { get; set; }

        public virtual DbSet<MS_JournalType> MS_JournalType { get; set; }

        public virtual DbSet<TR_Journal> TR_Journal { get; set; }

        public virtual DbSet<TR_PaymentDetailJournal> TR_PaymentDetailJournal { get; set; }

        public AccountingDbContext(DbContextOptions<AccountingDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //ON DELETE CASCADE
            //modelBuilder.Entity<TR_BookingHeader>()
            //  .HasOne(c => c.MS_Unit)
            //  .WithMany(m => m.TR_BookingHeader)
            //  .HasForeignKey(u => u.unitID)
            //  .OnDelete(DeleteBehavior.Restrict);

            //

            //modelBuilder.Entity<MS_Account>()
            //            .HasIndex(b => b.accCode)
            //            .IsUnique()
            //            .HasName("accCodeUnique");


            modelBuilder.Entity<MS_COA>()
            .HasKey(c => new { c.entityCode, c.accCode, c.COACodeFIN});

            modelBuilder.Entity<MS_COAFIN>()
            .HasKey(c => new { c.entityCode, c.COACodeFIN });

            modelBuilder.Entity<MS_Mapping>()
            .HasKey(c => new { c.entityCode, c.journalTypeCode, c.othersTypeCode, c.payForCode, c.payTypeCode });

            modelBuilder.Entity<MS_JournalType>()
            .HasKey(c => new { c.entityCode, c.journalTypeCode, c.COACodeFIN});

            modelBuilder.Entity<TR_PaymentDetailJournal>()
            .HasKey(c => new { c.entityCode, c.accCode, c.transNo, c.payNo, c.bookCode });

            modelBuilder.Entity<TR_Journal>()
            .HasKey(c => new { c.entityCode, c.journalCode, c.COACodeFIN, c.COACodeAcc });


            base.OnModelCreating(modelBuilder);
            //Database.SetInitializer<PropertySystemDbContext>(null);
        }
    }
}
