using Abp.Domain.Entities.Auditing;
using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Authorization.Applications;
using VDI.Demo.NewCommDB;

namespace VDI.Demo.EntityFrameworkCore
{
    public class NewCommDbContext : AbpDbContext
    {
        /* Define an DbSet for each entity of the application */

        public virtual DbSet<MS_Schema> MS_Schema { get; set; }

        //public virtual DbSet<MS_GroupSchema> MS_GroupSchema { get; set; }

        public virtual DbSet<MS_SchemaRequirement> MS_SchemaRequirement { get; set; }

        public virtual DbSet<MS_Property> MS_Property { get; set; }

        public virtual DbSet<MS_Developer_Schema> MS_Developer_Schema { get; set; }

        public virtual DbSet<LK_Upline> LK_Upline { get; set; }

        public virtual DbSet<MS_PPhRange> MS_PPHRange { get; set; }

        public virtual DbSet<MS_PPhRangeIns> MS_PPHRangeIns { get; set; }

        public virtual DbSet<LK_PointType> LK_PointType { get; set; }

        public virtual DbSet<MS_PointPct> MS_PointPct { get; set; }

        public virtual DbSet<LK_CommType> LK_CommType { get; set; }

        public virtual DbSet<MS_CommPct> MS_CommPct { get; set; }

        //public virtual DbSet<MS_GroupCommPct> MS_GroupCommPct { get; set; }

        //public virtual DbSet<MS_GroupCommPctNonStd> MS_GroupCommPctNonStd { get; set; }

        //public virtual DbSet<MS_GroupSchemaRequirement> MS_GroupSchemaRequirement { get; set; }

        public virtual DbSet<MS_StatusMember> MS_StatusMember { get; set; }

        public virtual DbSet<MS_BobotComm> MS_BobotComm { get; set; }

        public virtual DbSet<TR_CommPayment> TR_CommPayment { get; set; }

        public virtual DbSet<TR_SoldUnit> TR_SoldUnit { get; set; }

        public virtual DbSet<TR_SoldUnitRequirement> TR_SoldUnitRequirement { get; set; }

        public virtual DbSet<TR_ManagementFee> TR_ManagementFee { get; set; }

        public virtual DbSet<TR_CommPaymentPph> TR_CommPaymentPph { get; set; }

        public virtual DbSet<TR_CommPct> TR_CommPct { get; set; }

        public virtual DbSet<TR_SoldUnitFlag> TR_SoldUnitFlag { get; set; }

        public virtual DbSet<TR_BudgetPayment> TR_BudgetPayment { get; set; }

        //public virtual DbSet<MS_ManagementPct> MS_ManagementPct { get; set; }

        public virtual DbSet<MS_Flag> MS_Flag { get; set; }

        
        public NewCommDbContext(DbContextOptions<NewCommDbContext> options)
            : base(options)
        {

        }

        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Database.SetInitializer<NewCommDbContext>(null);

            //modelBuilder
            //    .Entity<MS_Schema>()
            //    .Property(t => t.scmCode)
            //    .HasAnnotation(
            //    "Index",
            //    new IndexAnnotation(new IndexAttribute("scmCodeUnique") { IsUnique = true }));

            modelBuilder.Entity<MS_Schema>()
                        .HasIndex(b => b.scmCode)
                        .IsUnique()
                        .HasName("scmCodeUnique");

            modelBuilder.Entity<LK_CommType>()
                .HasKey(c => new { c.entityCode, c.scmCode, c.commTypeCode });

            modelBuilder.Entity<LK_PointType>()
                .HasKey(c => new { c.entityCode, c.scmCode, c.pointTypeCode });

            modelBuilder.Entity<LK_Upline>()
                .HasKey(c => new { c.entityCode, c.scmCode, c.uplineNo });

            modelBuilder.Entity<MS_BobotComm>()
                .HasKey(c => new { c.entityCode, c.projectCode, c.clusterCode, c.scmCode });

            modelBuilder.Entity<MS_Developer_Schema>()
                .HasKey(c => new { c.entityCode, c.propCode, c.devCode, c.scmCode });

            modelBuilder.Entity<MS_Flag>()
                .HasKey(c => new { c.entityCode, c.flagCode });

            modelBuilder.Entity<MS_PointPct>()
                .HasKey(c => new { c.entityCode, c.scmCode, c.statusCode, c.asUplineNo });

            modelBuilder.Entity<MS_PPhRange>()
                .HasKey(c => new { c.entityCode, c.scmCode, c.PPhYear, c.PPhRangeHighBound });

            modelBuilder.Entity<MS_Property>()
                .HasKey(c => new { c.entityCode, c.scmCode, c.propCode});

            modelBuilder.Entity<MS_Schema>()
                .HasKey(c => new { c.entityCode, c.scmCode});

            modelBuilder.Entity<MS_SchemaRequirement>()
                .HasKey(c => new { c.entityCode, c.scmCode, c.reqNo });

            modelBuilder.Entity<MS_StatusMember>()
                .HasKey(c => new { c.entityCode, c.scmCode, c.statusCode});

            modelBuilder.Entity<TR_BudgetPayment>()
                .HasKey(c => new { c.entityCode, c.scmCode, c.propCode, c.devCode, c.bookNo, c.reqNo });

            modelBuilder.Entity<TR_CommPayment>()
                .HasKey(c => new { c.devCode, c.bookNo, c.asUplineNo, c.isHold, c.commNo, c.memberCode, c.commTypeCode, c.reqNo });

            modelBuilder.Entity<TR_CommPaymentPph>()
                .HasKey(c => new { c.devCode, c.bookNo, c.asUplineNo, c.isHold, c.commNo, c.memberCode, c.commTypeCode, c.reqNo, c.pphNo });

            modelBuilder.Entity<TR_CommPct>()
                .HasKey(c => new { c.entityCode, c.devCode, c.bookNo, c.memberCodeR, c.asUplineNo });

            modelBuilder.Entity<TR_ManagementFee>()
                .HasKey(c => new { c.entityCode, c.scmCode, c.propCode, c.devCode, c.bookNo, c.reqNo });

            modelBuilder.Entity<TR_SoldUnit>()
                .HasKey(c => new { c.entityCode, c.scmCode, c.devCode, c.bookNo});

            modelBuilder.Entity<TR_SoldUnitFlag>()
                .HasKey(c => new { c.entityCode, c.flagCode, c.devCode, c.bookNo });

            modelBuilder.Entity<MS_CommPct>()
                .HasKey(c => new { c.entityCode, c.scmCode, c.statusCode, c.asUplineNo, c.validDate, c.minAmt });

            modelBuilder.Entity<MS_PPhRangeIns>()
                .HasKey(c => new { c.entityCode, c.scmCode, c.pphRangePct, c.validDate, c.modifTime, c.inputTime, c.inputUN, c.TAX_CODE });

            modelBuilder.Entity<TR_SoldUnitRequirement>()
                .HasKey(c => new { c.entityCode, c.devCode, c.bookNo, c.scmCode, c.reqNo });

            //=====ON DELETE NO ACTION=====

            //modelBuilder.Entity<MS_CommPct>()
            //    .HasOne(t => t.MS_Schema)
            //    .WithMany(w => w.MS_CommPct)
            //    .HasForeignKey(d => d.schemaID)
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<MS_StatusMember>()
            //    .HasOne(t => t.MS_Schema)
            //    .WithMany(w => w.MS_StatusMember)
            //    .HasForeignKey(d => d.schemaID)
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<LK_CommType>()
            //    .HasOne(t => t.MS_Schema)
            //    .WithMany(w => w.LK_CommType)
            //    .HasForeignKey(d => d.schemaID)
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<LK_PointType>()
            //    .HasOne(t => t.MS_Schema)
            //    .WithMany(w => w.LK_PointType)
            //    .HasForeignKey(d => d.schemaID)
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<MS_Developer_Schema>()
            //    .HasOne(t => t.MS_Schema)
            //    .WithMany(w => w.MS_Developer_Schema)
            //    .HasForeignKey(d => d.schemaID)
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<MS_PointPct>()
            //    .HasOne(t => t.MS_Schema)
            //    .WithMany(w => w.MS_PointPct)
            //    .HasForeignKey(d => d.schemaID)
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<MS_Property>()
            //    .HasOne(t => t.MS_Schema)
            //    .WithMany(w => w.MS_Property)
            //    .HasForeignKey(d => d.schemaID)
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<MS_PPhRange>()
            //    .HasOne(t => t.MS_Schema)
            //    .WithMany(w => w.MS_PPhRange)
            //    .HasForeignKey(d => d.schemaID)
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<MS_PPhRangeIns>()
            //    .HasOne(t => t.MS_Schema)
            //    .WithMany(w => w.MS_PPhRangeIns)
            //    .HasForeignKey(d => d.schemaID)
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<TR_CommPayment>()
            //    .HasOne(t => t.MS_Schema)
            //    .WithMany(w => w.TR_CommPayment)
            //    .HasForeignKey(d => d.schemaID)
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<TR_CommPaymentPph>()
            //    .HasOne(t => t.MS_Schema)
            //    .WithMany(w => w.TR_CommPaymentPph)
            //    .HasForeignKey(d => d.schemaID)
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<TR_ManagementFee>()
            //    .HasOne(t => t.MS_Schema)
            //    .WithMany(w => w.TR_ManagementFee)
            //    .HasForeignKey(d => d.schemaID)
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<TR_SoldUnit>()
            //    .HasOne(t => t.MS_Schema)
            //    .WithMany(w => w.TR_SoldUnit)
            //    .HasForeignKey(d => d.schemaID)
            //    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<TR_SoldUnitRequirement>()
            //    .HasOne(t => t.MS_Schema)
            //    .WithMany(w => w.TR_SoldUnitRequirement)
            //    .HasForeignKey(d => d.schemaID)
            //    .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    base.OnConfiguring(optionsBuilder);
        //    var builder = new ConfigurationBuilder()
        //        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        //    IConfigurationRoot config = builder.Build();
            
        //    optionsBuilder.UseSqlServer(config.GetConnectionString("NewCommDbContext"));
        //}

        //public NewCommDbContext(string nameOrConnectionString)
        //    : base("NewCommDbContext")
        //{

        //}

        //public NewCommDbContext(DbConnection existingConnection)
        //   : base(existingConnection, false)
        //{

        //}

        //public NewCommDbContext(DbConnection existingConnection, bool contextOwnsConnection)
        //    : base(existingConnection, contextOwnsConnection)
        //{

        //}
    }
}
