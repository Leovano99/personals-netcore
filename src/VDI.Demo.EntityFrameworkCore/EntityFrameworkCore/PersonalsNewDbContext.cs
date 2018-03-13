using Abp.EntityFrameworkCore;
using Abp.IdentityServer4;
using Microsoft.EntityFrameworkCore;
using VDI.Demo.PersonalsDB;

namespace VDI.Demo.EntityFrameworkCore
{
    public class PersonalsNewDbContext : AbpDbContext
    {
        /* Define an IDbSet for each entity of the application */

        public virtual DbSet<LK_AddrType> LK_AddrType { get; set; }
        public virtual DbSet<LK_BankType> LK_bankType { get; set; }
        public virtual DbSet<LK_Blood> LK_Bloods { get; set; }
        public virtual DbSet<LK_Country> LK_Country { get; set; }
        public virtual DbSet<LK_FamilyStatus> LK_FamilyStatus { get; set; }
        public virtual DbSet<LK_Grade> LK_Grade { get; set; }
        public virtual DbSet<LK_IDType> LK_IDType { get; set; }
        public virtual DbSet<LK_MarStatus> LK_MarStatus { get; set; }
        public virtual DbSet<LK_PhonePrefix> LK_PhonePrefix { get; set; }
        public virtual DbSet<LK_PhoneType> LK_PhoneType { get; set; }
        public virtual DbSet<LK_Religion> LK_Religion { get; set; }
        public virtual DbSet<LK_Spec> LK_Spec { get; set; }

        public virtual DbSet<MS_BankPersonal> MS_BankPersonal { get; set; }
        public virtual DbSet<MS_City> MS_City { get; set; }
        public virtual DbSet<MS_Document> MS_Document { get; set; }
        public virtual DbSet<MS_Group> MS_Group { get; set; }
        public virtual DbSet<MS_JobTitle> MS_JobTitle { get; set; }
        public virtual DbSet<MS_Nation> MS_Nation { get; set; }
        public virtual DbSet<MS_Occupation> MS_Occupation { get; set; }
        public virtual DbSet<MS_PostCode> MS_PostCode { get; set; }
        public virtual DbSet<MS_PriorityPass> MS_PriorityPass { get; set; }
        public virtual DbSet<MS_RelationResident> MS_RelationResident { get; set; }
        public virtual DbSet<MS_Street> MS_Street { get; set; }
        public virtual DbSet<MS_County> MS_County { get; set; }
        public virtual DbSet<MS_Province> MS_Province { get; set; }
        public virtual DbSet<MS_Regency> MS_Regency { get; set; }
        public virtual DbSet<MS_Village> MS_Village { get; set; }
        public virtual DbSet<MS_FranchiseGroup> MS_FranchiseGroup { get; set; }

        public virtual DbSet<TR_Address> TR_Address { get; set; }
        public virtual DbSet<TR_BankAccount> TR_BankAccount { get; set; }
        public virtual DbSet<TR_Company> TR_Company { get; set; }
        public virtual DbSet<TR_Email> TR_Email { get; set; }
        public virtual DbSet<TR_Family> TR_Family { get; set; }
        public virtual DbSet<TR_Group> TR_Group { get; set; }
        public virtual DbSet<TR_ID> TR_ID { get; set; }
        public virtual DbSet<TR_IDFamily> TR_IDFamily { get; set; }
        public virtual DbSet<TR_Phone> TR_Phone { get; set; }
        public virtual DbSet<TR_Document> TR_Document { get; set; }
        public virtual DbSet<TR_EmailInvalid> TR_EmailInvalid { get; set; }

        public virtual DbSet<PERSONALS> PERSONAL { get; set; }
        public virtual DbSet<PERSONALS_MEMBER> PERSONALS_MEMBER { get; set; }

        public virtual DbSet<SYS_RolesAddr> SYS_RolesAddr { get; set; }
        public virtual DbSet<SYS_UserGroup> SYS_UserGroup { get; set; }
        public virtual DbSet<SYS_Counter> SYS_Counter { get; set; }
        public virtual DbSet<SYS_CounterMember> SYS_CounterMember { get; set; }

        public virtual DbSet<LK_KeyPeople> LK_KeyPeople { get; set; }
        public virtual DbSet<TR_KeyPeople> TR_KeyPeople { get; set; }

        public PersonalsNewDbContext(DbContextOptions<PersonalsNewDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<LK_PhonePrefix>()
                .HasKey(c => new { c.prefix, c.phoneType });

            modelBuilder.Entity<MS_City>()
                .HasKey(c => new { c.entityCode, c.cityCode });

            modelBuilder.Entity<MS_County>()
                .HasKey(c => new { c.countyCode, c.countyDesc });

            modelBuilder.Entity<MS_Group>()
                .HasKey(c => new { c.entityCode, c.groupCode });

            modelBuilder.Entity<MS_Nation>()
                .HasKey(c => new { c.entityCode, c.nationID });

            modelBuilder.Entity<MS_Occupation>()
                .HasKey(c => new { c.entityCode, c.occID });

            modelBuilder.Entity<MS_PostCode>()
                .HasKey(c => new { c.entityCode, c.cityCode, c.postCode });

            modelBuilder.Entity<MS_Province>()
                .HasKey(c => new { c.provinceCode, c.provinceName });

            modelBuilder.Entity<MS_Regency>()
                .HasKey(c => new { c.regencyCode, c.regencyName });

            modelBuilder.Entity<MS_RelationResident>()
                .HasKey(c => new { c.kkCode, c.RefID });

            modelBuilder.Entity<MS_Street>()
                .HasKey(c => new { c.entityCode, c.cityCode, c.postCode, c.streetNo });

            modelBuilder.Entity<MS_Village>()
                .HasKey(c => new { c.villageCode, c.villageName, c.cityCode, c.countyCode, c.regencyCode, c.provinceCode });

            modelBuilder.Entity<PERSONALS>()
                .HasKey(c => new { c.entityCode, c.psCode });

            modelBuilder.Entity<PERSONALS_MEMBER>()
                .HasKey(c => new { c.entityCode, c.psCode, c.scmCode, c.memberCode });

            modelBuilder.Entity<SYS_Counter>()
                .HasKey(c => new { c.entityCode, c.psCode });

            modelBuilder.Entity<SYS_CounterMember>()
                .HasKey(c => new { c.entityCode, c.scmCode });

            modelBuilder.Entity<SYS_RolesAddr>()
                .HasKey(c => new { c.entityCode, c.rolesname, c.addrType });

            modelBuilder.Entity<SYS_UserGroup>()
                .HasKey(c => new { c.entityCode, c.userName, c.groupCode });

            modelBuilder.Entity<TR_Address>()
                .HasKey(c => new { c.entityCode, c.psCode, c.refID, c.addrType });

            modelBuilder.Entity<TR_BankAccount>()
                .HasKey(c => new { c.entityCode, c.psCode, c.refID, c.BankCode });

            modelBuilder.Entity<TR_Company>()
                .HasKey(c => new { c.entityCode, c.psCode, c.refID });

            modelBuilder.Entity<TR_Document>()
                .HasKey(c => new { c.entityCode, c.psCode, c.documentType });

            modelBuilder.Entity<TR_Email>()
                .HasKey(c => new { c.entityCode, c.psCode, c.refID });

            modelBuilder.Entity<TR_EmailInvalid>()
                .HasKey(c => new { c.entityCode, c.psCode, c.refID, c.email });

            modelBuilder.Entity<TR_Family>()
                .HasKey(c => new { c.entityCode, c.psCode, c.refID });

            modelBuilder.Entity<TR_Group>()
                .HasKey(c => new { c.entityCode, c.groupCode, c.psCode });

            modelBuilder.Entity<TR_ID>()
                .HasKey(c => new { c.entityCode, c.psCode, c.refID });

            modelBuilder.Entity<TR_IDFamily>()
                .HasKey(c => new { c.psCode, c.familyRefID, c.refID });

            modelBuilder.Entity<TR_Phone>()
                .HasKey(c => new { c.entityCode, c.psCode, c.refID });

            modelBuilder.ConfigurePersistedGrantEntity();
        }

    }
 }
