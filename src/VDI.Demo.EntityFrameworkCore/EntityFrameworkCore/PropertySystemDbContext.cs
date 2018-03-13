using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VDI.Demo.PropertySystemDB;
using VDI.Demo.PropertySystemDB.LippoMaster;
using VDI.Demo.PropertySystemDB.MasterPlan.Project;
using VDI.Demo.PropertySystemDB.MasterPlan.Unit;
using VDI.Demo.PropertySystemDB.OnlineBooking.PPOnline;
using VDI.Demo.PropertySystemDB.OnlineBooking.ProjectInfo;
using VDI.Demo.PropertySystemDB.OnlineBooking.PropertySystem;
using VDI.Demo.PropertySystemDB.Pricing;

namespace VDI.Demo.EntityFrameworkCore
{
    public class PropertySystemDbContext : AbpDbContext
    {
        public virtual DbSet<MS_Position> MS_Position { get; set; }

        public virtual DbSet<MS_Officer> MS_Officer { get; set; }

        public virtual DbSet<MS_Department> MS_Department { get; set; }

        public virtual DbSet<MS_Bank> MS_Bank { get; set; }

        public virtual DbSet<LK_BankLevel> MS_BankType { get; set; }

        public virtual DbSet<MS_BankBranch> MS_BankBranch { get; set; }

        public virtual DbSet<MS_Entity> MS_Entity { get; set; }

        public virtual DbSet<MS_Area> MS_Area { get; set; }

        public virtual DbSet<MS_City> MS_City { get; set; }

        public virtual DbSet<MS_County> MS_County { get; set; }

        public virtual DbSet<MS_Territory> MS_Territory { get; set; }

        public virtual DbSet<MS_Country> MS_Country { get; set; }

        public virtual DbSet<MS_Town> MS_Town { get; set; }

        public virtual DbSet<MS_PostCode> MS_PostCode { get; set; }

        public virtual DbSet<MS_Account> MS_Account { get; set; }

        public virtual DbSet<MS_Company> MS_Company { get; set; }

        public virtual DbSet<MS_JenisKantorBank> MS_JenisKantorBank { get; set; }

        public virtual DbSet<MP_BankBranch_JKB> MP_BankBranch_JKB { get; set; }

        public virtual DbSet<MS_Category> MS_Category { get; set; }

        public virtual DbSet<LK_Item> LK_Item { get; set; }

        public virtual DbSet<LK_UnitStatus> LK_UnitStatus { get; set; }

        public virtual DbSet<MS_Product> MS_Product { get; set; }

        public virtual DbSet<LK_Facing> LK_Facing { get; set; }

        public virtual DbSet<MS_Zoning> MS_Zoning { get; set; }

        public virtual DbSet<MS_Project> MS_Project { get; set; }

        public virtual DbSet<MP_CompanyProject> MP_CompanyProject { get; set; }

        public virtual DbSet<MP_OfficerProject> MP_OfficerProject { get; set; }

        public virtual DbSet<MS_Renovation> MS_Renovation { get; set; }

        public virtual DbSet<MS_UnitCode> MS_UnitCode { get; set; }

        public virtual DbSet<MS_Cluster> MS_Cluster { get; set; }

        public virtual DbSet<MS_Unit> MS_Unit { get; set; }

        public virtual DbSet<MS_UnitItem> MS_UnitItem { get; set; }

        public virtual DbSet<MS_Discount> MS_Discount { get; set; }

        public virtual DbSet<TR_BasePrice> TR_BasePrice { get; set; }

        public virtual DbSet<MS_ProjectProduct> MS_ProjectProduct { get; set; }

        public virtual DbSet<MS_Term> MS_Term { get; set; }

        public virtual DbSet<MS_TermAddDisc> MS_TermAddDisc { get; set; }

        public virtual DbSet<MS_TermDP> MS_TermDP { get; set; }

        public virtual DbSet<MS_TermMain> MS_TermMain { get; set; }

        public virtual DbSet<MS_TermPmt> MS_TermPmt { get; set; }

        public virtual DbSet<LK_FinType> LK_FinType { get; set; }

        public virtual DbSet<MS_GroupTerm> MS_GroupTerm { get; set; }

        public virtual DbSet<MS_FormulaCode> MS_FormulaCode { get; set; }

        public virtual DbSet<MS_MappingFormula> MS_MappingFormula { get; set; }

        public virtual DbSet<MS_UnitItemPrice> MS_UnitItemPrice { get; set; }

        public virtual DbSet<MS_Facade> MS_Facade { get; set; }

        public virtual DbSet<LK_DPCalc> LK_DPCalc { get; set; }

        public virtual DbSet<MS_Detail> MS_Detail { get; set; }

        public virtual DbSet<LK_RentalStatus> LK_RentalStatus { get; set; }

        public virtual DbSet<MS_UnitRoom> MS_UnitRoom { get; set; }

        public virtual DbSet<MS_UnitTaskList> MS_UnitTaskList { get; set; }

        public virtual DbSet<MS_PriceTaskList> MS_PriceTaskList { get; set; }

        public virtual DbSet<MS_TermDiscOnlineBooking> MS_TermDiscOnlineBooking { get; set; }

        public virtual DbSet<MS_SumberDana> MS_SumberDana { get; set; }

        public virtual DbSet<MS_TujuanTransaksi> MS_TujuanTransaksi { get; set; }

        public virtual DbSet<MS_AccountEmail> MS_AccountEmail { get; set; }

        public virtual DbSet<LK_FormulaDP> LK_FormulaDP { get; set; }
        

        //=======OB PropertySystem=====

        public virtual DbSet<TR_PaymentOnlineBook> TR_PaymentOnlineBook { get; set; }

        public virtual DbSet<MS_DiscOnlineBooking> MS_DiscOnlineBooking { get; set; }

        public virtual DbSet<LK_BookingOnlineStatus> LK_BookingOnlineStatus { get; set; }


        //=======OB Project Info=====

        public virtual DbSet<MS_ProjectInfo> MS_ProjectInfo { get; set; }

        public virtual DbSet<MS_ProjectKeyFeaturesCollection> MS_ProjectKeyFeaturesCollection { get; set; }

        public virtual DbSet<MS_ProjectLocation> MS_ProjectLocation { get; set; }

        public virtual DbSet<TR_ProjectImageGallery> TR_ProjectImageGallery { get; set; }

        public virtual DbSet<TR_ProjectKeyFeatures> TR_ProjectKeyFeatures { get; set; }

        //=======OB PPOnline=====

        public virtual DbSet<LK_PaymentType> LK_PaymentType { get; set; }

        //=======LM========

        public virtual DbSet<TR_BookingHeader> TR_BookingHeader { get; set; }

        public virtual DbSet<TR_BookingDetail> TR_BookingDetail { get; set; }

        public virtual DbSet<TR_PaymentDetail> TR_PaymentDetail { get; set; }

        public virtual DbSet<TR_PaymentHeader> TR_PaymentHeader { get; set; }

        public virtual DbSet<TR_BookingHeaderTerm> TR_BookingHeaderTerm { get; set; }

        public virtual DbSet<TR_BookingDetailSchedule> TR_BookingDetailSchedule { get; set; }

        public virtual DbSet<TR_BookingDocument> TR_BookingDocument { get; set; }

        public virtual DbSet<TR_PaymentDetailAlloc> TR_PaymentDetailAlloc { get; set; }

        public virtual DbSet<LK_BookingTrType> LK_BookingTrType { get; set; }

        public virtual DbSet<MS_Corres> MS_Corres { get; set; }

        public virtual DbSet<LK_Alloc> LK_Alloc { get; set; }

        public virtual DbSet<LK_LetterStatus> LK_LetterStatus { get; set; }

        public virtual DbSet<LK_Promotion> LK_Promotion { get; set; }

        public virtual DbSet<LK_Reason> LK_Reason { get; set; }

        public virtual DbSet<LK_SADStatus> LK_SADStatus { get; set; }

        public virtual DbSet<MS_DocumentPS> MS_DocumentPS { get; set; }

        public virtual DbSet<MS_MappingDocument> MS_MappingDocument { get; set; }

        public virtual DbSet<MS_SalesEvent> MS_SalesEvent { get; set; }

        public virtual DbSet<TR_BookingCancel> TR_BookingCancel { get; set; }

        public virtual DbSet<TR_BookingDetailAddDisc> TR_BookingDetailAddDisc { get; set; }

        public virtual DbSet<TR_BookingDetailScheduleOtorisasi> TR_BookingDetailScheduleOtorisasi { get; set; }

        public virtual DbSet<TR_BookingItemPrice> TR_BookingItemPrice { get; set; }

        public virtual DbSet<LK_PayType> LK_PayType { get; set; }

        public virtual DbSet<LK_OthersType> LK_OthersType { get; set; }

        public virtual DbSet<LK_PayFor> LK_PayFor { get; set; }

        public virtual DbSet<MS_ShopBusiness> MS_ShopBusiness { get; set; }

        public virtual DbSet<MS_TransFrom> MS_Transfrom { get; set; }

        public virtual DbSet<TR_BookingChangeOwner> TR_BookingChangeOwner { get; set; }

        public virtual DbSet<TR_BookingCorres> TR_BookingCorres { get; set; }

        public virtual DbSet<TR_BookingDetailAdjustment> TR_BookingDetailAdjustment { get; set; }

        public virtual DbSet<TR_BookingDetailDP> TR_BookingDetailDP { get; set; }

        public virtual DbSet<TR_BookingSalesDisc> TR_BookingSalesDisc { get; set; }

        public virtual DbSet<TR_UnitOrderHeader> TR_UnitOrderHeader { get; set; }

        public virtual DbSet<TR_UnitReserved> TR_UnitReserved { get; set; }

        public virtual DbSet<TR_UnitOrderDetail> TR_UnitOrderDetail { get; set; }

        public virtual DbSet<TR_CommAddDisc> TR_CommAddDisc { get; set; }

        public virtual DbSet<TR_PenaltySchedule> TR_PenaltySchedule { get; set; }

        public virtual DbSet<TR_ReminderLetter> TR_ReminderLetter { get; set; }

        public virtual DbSet<TR_SSPDetail> TR_SSPDetail { get; set; }

        public virtual DbSet<TR_SSPHeader> TR_SSPHeader { get; set; }

        public virtual DbSet<SYS_FinanceCounter> SYS_FinanceCounter { get; set; }

        public virtual DbSet<TR_DPHistory> TR_DPHistory { get; set; }

        public virtual DbSet<TR_MKTAddDisc> TR_MKTAddDisc { get; set; }

        public virtual DbSet<TR_CashAddDisc> TR_CashAddDisc { get; set; }

        public virtual DbSet<MS_TaxType> MS_TaxType { get; set; }

        public virtual DbSet<TR_BookingTax> TR_BookingTax { get; set; }

        public virtual DbSet<SYS_BookingCounter> SYS_BookingCounter { get; set; }

        public virtual DbSet<MS_KuasaDireksi> MS_KuasaDireksi { get; set; }

        public virtual DbSet<MS_KuasaDireksiPeople> MS_KuasaDireksiPeople { get; set; }

        public virtual DbSet<MS_DocTemplate> MS_DocTemplate { get; set; }

        public virtual DbSet<MS_MappingTemplate> MS_MappingTemplate { get; set; }

        public virtual DbSet<SYS_RolesOthersType> SYS_RolesOthersType { get; set; }

        public virtual DbSet<SYS_RolesPayFor> SYS_RolesPayFor { get; set; }

        public virtual DbSet<SYS_RolesPayType> SYS_RolesPayType { get; set; }

        public virtual DbSet<MS_PromoOnlineBooking> MS_PromoOnlineBooking { get; set; }

        public virtual DbSet<MS_BankOLBooking> MS_BankOLBooking { get; set; }
        
        public virtual DbSet<DocNo_Counter> DocNo_Counter { get; set; }

        public virtual DbSet<TR_BookingHeaderHistory> TR_BookingHeaderHistory { get; set; }

        public virtual DbSet<TR_BookingDetailAddDiscHistory> TR_BookingDetailAddDiscHistory { get; set; }

        public virtual DbSet<TR_PaymentBulk> TR_PaymentBulk { get; set; }

        public virtual DbSet<TR_MKTAddDiscHistory> TR_MKTAddDiscHistory { get; set; }

        public virtual DbSet<TR_CommAddDiscHistory> TR_CommAddDiscHistory { get; set; }

        public virtual DbSet<LK_MappingTax> LK_MappingTax { get; set; }


        public PropertySystemDbContext(DbContextOptions<PropertySystemDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //ON DELETE CASCADE
            modelBuilder.Entity<TR_BookingHeader>()
              .HasOne(c => c.MS_Unit)
              .WithMany(m => m.TR_BookingHeader)
              .HasForeignKey(u => u.unitID)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TR_PaymentHeader>()
              .HasOne(c => c.TR_BookingHeader)
              .WithMany(m => m.TR_PaymentHeader)
              .HasForeignKey(u => u.bookingHeaderID)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TR_SSPDetail>()
              .HasOne(c => c.TR_BookingDetail)
              .WithMany(m => m.TR_SSPDetail)
              .HasForeignKey(u => u.bookingDetailID)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TR_BookingHeaderTerm>()
              .HasOne(c => c.MS_Term)
              .WithMany(m => m.TR_BookingHeaderTerm)
              .HasForeignKey(u => u.termID)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MS_UnitItemPrice>()
              .HasOne(c => c.MS_Unit)
              .WithMany(m => m.MS_UnitItemPrice)
              .HasForeignKey(u => u.unitID)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TR_PaymentBulk>()
              .HasOne(c => c.MS_Unit)
              .WithMany(m => m.TR_PaymentBulk)
              .HasForeignKey(u => u.unitID)
              .OnDelete(DeleteBehavior.Restrict);

            //

            modelBuilder.Entity<MS_Account>()
                        .HasIndex(b => b.accCode)
                        .IsUnique()
                        .HasName("accCodeUnique");
            
            modelBuilder.Entity<MS_Bank>()
                        .HasIndex(b => b.bankCode)
                        .IsUnique()
                        .HasName("bankCodeUnique");
            
            modelBuilder.Entity<MS_BankBranch>()
                        .HasIndex(b => b.bankBranchCode)
                        .IsUnique()
                        .HasName("bankBranchCodeUnique");
            
            modelBuilder.Entity<LK_BankLevel>()
                        .HasIndex(b => b.bankLevelCode)
                        .IsUnique()
                        .HasName("bankLevelCodeUnique");
            
            modelBuilder.Entity<MS_Company>()
                        .HasIndex(b => b.coCode)
                        .IsUnique()
                        .HasName("coCodeUnique");
            
            modelBuilder.Entity<MS_Country>()
                        .HasIndex(b => b.countryCode)
                        .IsUnique()
                        .HasName("countryCodeUnique");
            
            modelBuilder.Entity<MS_Department>()
                        .HasIndex(b => b.departmentCode)
                        .IsUnique()
                        .HasName("departmentCodeUnique");
            
            modelBuilder.Entity<MS_JenisKantorBank>()
                        .HasIndex(b => b.JKBCode)
                        .IsUnique()
                        .HasName("JKBCodeUnique");
            
            modelBuilder.Entity<MS_Project>()
                        .HasIndex(b => b.projectCode)
                        .IsUnique()
                        .HasName("projectCodeUnique");
            
            modelBuilder.Entity<MS_Area>()
                        .HasIndex(b => b.areaCode)
                        .IsUnique()
                        .HasName("areaCodeUnique");
            
            modelBuilder.Entity<LK_Facing>()
                        .HasIndex(b => b.facingCode)
                        .IsUnique()
                        .HasName("facingCodeUnique");
            
            modelBuilder.Entity<LK_Item>()
                        .HasIndex(b => b.itemCode)
                        .IsUnique()
                        .HasName("itemCodeUnique");
            
            modelBuilder.Entity<LK_UnitStatus>()
                        .HasIndex(b => b.unitStatusCode)
                        .IsUnique()
                        .HasName("unitStatusCodeUnique");
            
            modelBuilder.Entity<MS_Category>()
                        .HasIndex(b => b.categoryCode)
                        .IsUnique()
                        .HasName("categoryCodeUnique");
            
            modelBuilder.Entity<MS_Cluster>()
                        .HasIndex(b => b.clusterCode)
                        .IsUnique()
                        .HasName("clusterCodeUnique");
            
            modelBuilder.Entity<MS_Product>()
                        .HasIndex(b => b.productCode)
                        .IsUnique()
                        .HasName("productCodeUnique");
            
            modelBuilder.Entity<MS_Zoning>()
                        .HasIndex(b => b.zoningCode)
                        .IsUnique()
                        .HasName("zoningCodeUnique");

            modelBuilder.Entity<LK_FinType>()
                        .HasIndex(b => b.finTypeCode)
                        .IsUnique()
                        .HasName("finTypeCodeUnique");

            modelBuilder.Entity<MS_FormulaCode>()
                        .HasIndex(b => b.formulaCode)
                        .IsUnique()
                        .HasName("formulaCodeUnique");
            
            modelBuilder.Entity<MS_Entity>()
                        .HasIndex(b => b.entityCode)
                        .IsUnique()
                        .HasName("entityCodeUnique");
            
            modelBuilder.Entity<MS_Facade>()
                        .HasIndex(b => b.facadeCode)
                        .IsUnique()
                        .HasName("facadeCodeUnique");

            modelBuilder.Entity<TR_BookingHeader>()
                        .HasIndex(b => b.bookCode)
                        .IsUnique()
                        .HasName("bookCodeUnique");

            modelBuilder.Entity<LK_RentalStatus>()
                        .HasIndex(b => b.rentalStatusCode)
                        .IsUnique()
                        .HasName("rentalStatusCodeUnique");

            //LippoMaster
            modelBuilder.Entity<LK_Alloc>()
                        .HasIndex(b => b.allocCode)
                        .IsUnique()
                        .HasName("allocCodeUnique");

            modelBuilder.Entity<LK_LetterStatus>()
                        .HasIndex(b => b.letterStatusCode)
                        .IsUnique()
                        .HasName("letterStatusCodeUnique");

            modelBuilder.Entity<LK_PayType>()
                        .HasIndex(b => b.payTypeCode)
                        .IsUnique()
                        .HasName("payTypeCodeUnique");

            modelBuilder.Entity<LK_Promotion>()
                        .HasIndex(b => b.promotionCode)
                        .IsUnique()
                        .HasName("promotionCodeUnique");

            modelBuilder.Entity<LK_Reason>()
                        .HasIndex(b => b.reasonCode)
                        .IsUnique()
                        .HasName("reasonCodeUnique");

            modelBuilder.Entity<LK_SADStatus>()
                        .HasIndex(b => b.statusCode)
                        .IsUnique()
                        .HasName("statusCodeUnique");

            modelBuilder.Entity<MS_DocumentPS>()
                        .HasIndex(b => b.docCode)
                        .IsUnique()
                        .HasName("docCodeCodeUnique");

            modelBuilder.Entity<MS_SalesEvent>()
                        .HasIndex(b => b.eventCode)
                        .IsUnique()
                        .HasName("eventCodeUnique");

            modelBuilder.Entity<MS_ShopBusiness>()
                        .HasIndex(b => b.shopBusinessCode)
                        .IsUnique()
                        .HasName("shopBusinessCodeUnique");

            modelBuilder.Entity<MS_TransFrom>()
                        .HasIndex(b => b.transCode)
                        .IsUnique()
                        .HasName("transCodeUnique");

            //No Delete On Action
            modelBuilder.Entity<MS_BankBranch>()
                .HasOne(t => t.LK_BankLevel)
                .WithMany(w => w.MS_BankBranch)
                .HasForeignKey(d => d.bankBranchTypeID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MS_BankBranch>()
                .HasOne(t => t.MS_Bank)
                .WithMany(w => w.MS_BankBranch)
                .HasForeignKey(d => d.bankID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MS_Position>()
                .HasOne(t => t.MS_Department)
                .WithMany(w => w.MS_Position)
                .HasForeignKey(d => d.departmentID)
                .OnDelete(DeleteBehavior.Restrict);

            //OnlineBooking
            modelBuilder.Entity<TR_UnitOrderDetail>()
                .HasOne(t => t.MS_Term)
                .WithMany(w => w.TR_UnitOrderDetail)
                .HasForeignKey(d => d.termID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TR_UnitOrderDetail>()
                .HasOne(t => t.MS_Unit)
                .WithMany(w => w.TR_UnitOrderDetail)
                .HasForeignKey(d => d.unitID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TR_UnitReserved>()
                .HasOne(t => t.MS_Term)
                .WithMany(w => w.TR_UnitReserved)
                .HasForeignKey(d => d.termID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MS_Unit>()
                .HasOne(t => t.MS_TermMain)
                .WithMany(w => w.MS_Unit)
                .HasForeignKey(d => d.termMainID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MS_UnitItemPrice>()
                .HasOne(t => t.MS_Term)
                .WithMany(w => w.MS_UnitItemPrice)
                .HasForeignKey(d => d.termID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TR_UnitReserved>()
                .HasOne(t => t.MS_Renovation)
                .WithMany(w => w.TR_UnitReserved)
                .HasForeignKey(d => d.renovID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MS_Cluster>()
                .HasOne(t => t.MS_Project)
                .WithMany(w => w.MS_Cluster)
                .HasForeignKey(d => d.projectID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MS_UnitCode>()
                .HasOne(t => t.MS_Project)
                .WithMany(w => w.MS_UnitCode)
                .HasForeignKey(d => d.projectID)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
            //Database.SetInitializer<PropertySystemDbContext>(null);
        }
    }
}
