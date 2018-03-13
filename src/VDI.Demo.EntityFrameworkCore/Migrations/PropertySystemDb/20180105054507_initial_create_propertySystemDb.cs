using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PropertySystemDb
{
    public partial class initial_create_propertySystemDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LK_BankLevel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    bankLevelCode = table.Column<string>(maxLength: 3, nullable: false),
                    bankLevelName = table.Column<string>(maxLength: 20, nullable: false),
                    isBankBranchType = table.Column<bool>(nullable: false),
                    isBankType = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LK_BankLevel", x => x.Id);
                    table.UniqueConstraint("bankLevelCodeUnique", x => x.bankLevelCode);
                });

            migrationBuilder.CreateTable(
                name: "LK_DPCalc",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DPCalcDesc = table.Column<string>(maxLength: 100, nullable: false),
                    DPCalcType = table.Column<string>(maxLength: 5, nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LK_DPCalc", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LK_Facing",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    facingCode = table.Column<string>(maxLength: 3, nullable: false),
                    facingName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LK_Facing", x => x.Id);
                    table.UniqueConstraint("facingCodeUnique", x => x.facingCode);
                });

            migrationBuilder.CreateTable(
                name: "LK_FinType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    finTimes = table.Column<short>(nullable: false),
                    finTypeCode = table.Column<string>(maxLength: 5, nullable: false),
                    finTypeDesc = table.Column<string>(maxLength: 50, nullable: false),
                    isCashStd = table.Column<bool>(nullable: false),
                    isCommStd = table.Column<bool>(nullable: false),
                    oldFinTypeCode = table.Column<string>(maxLength: 3, nullable: false),
                    pctComm = table.Column<double>(nullable: false),
                    pctCommLC = table.Column<double>(nullable: false),
                    pctCommTB = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LK_FinType", x => x.Id);
                    table.UniqueConstraint("finTypeCodeUnique", x => x.finTypeCode);
                });

            migrationBuilder.CreateTable(
                name: "LK_Item",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    isOption = table.Column<bool>(nullable: false),
                    itemCode = table.Column<string>(maxLength: 2, nullable: false),
                    itemName = table.Column<string>(maxLength: 40, nullable: false),
                    optionSort = table.Column<int>(nullable: false),
                    shortName = table.Column<string>(maxLength: 15, nullable: false),
                    sortNo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LK_Item", x => x.Id);
                    table.UniqueConstraint("itemCodeUnique", x => x.itemCode);
                });

            migrationBuilder.CreateTable(
                name: "LK_UnitStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    unitStatusCode = table.Column<string>(maxLength: 1, nullable: false),
                    unitStatusName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LK_UnitStatus", x => x.Id);
                    table.UniqueConstraint("unitStatusCodeUnique", x => x.unitStatusCode);
                });

            migrationBuilder.CreateTable(
                name: "MS_Block",
                columns: table => new
                {
                    blockCode = table.Column<string>(maxLength: 10, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    blockName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_Block", x => x.blockCode);
                });

            migrationBuilder.CreateTable(
                name: "MS_Category",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    areaField = table.Column<string>(maxLength: 30, nullable: false),
                    categoryCode = table.Column<string>(maxLength: 5, nullable: false),
                    categoryField = table.Column<string>(maxLength: 30, nullable: false),
                    categoryName = table.Column<string>(maxLength: 30, nullable: false),
                    clusterField = table.Column<string>(maxLength: 30, nullable: false),
                    detailField = table.Column<string>(maxLength: 30, nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    facingField = table.Column<string>(maxLength: 30, nullable: false),
                    kavNoField = table.Column<string>(maxLength: 30, nullable: false),
                    productField = table.Column<string>(maxLength: 30, nullable: false),
                    projectField = table.Column<string>(maxLength: 30, nullable: false),
                    roadField = table.Column<string>(maxLength: 30, nullable: false),
                    zoningField = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_Category", x => x.Id);
                    table.UniqueConstraint("categoryCodeUnique", x => x.categoryCode);
                });

            migrationBuilder.CreateTable(
                name: "MS_Cluster",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    clusterCode = table.Column<string>(maxLength: 5, nullable: false),
                    clusterName = table.Column<string>(maxLength: 35, nullable: false),
                    dueDateDevelopment = table.Column<DateTime>(nullable: true),
                    dueDateRemarks = table.Column<string>(maxLength: 500, nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    gracePeriod = table.Column<string>(maxLength: 100, nullable: true),
                    handOverPeriod = table.Column<string>(maxLength: 100, nullable: true),
                    sortNo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_Cluster", x => x.Id);
                    table.UniqueConstraint("clusterCodeUnique", x => x.clusterCode);
                });

            migrationBuilder.CreateTable(
                name: "MS_Country",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    countryCode = table.Column<string>(maxLength: 5, nullable: false),
                    countryName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_Country", x => x.Id);
                    table.UniqueConstraint("countryCodeUnique", x => x.countryCode);
                });

            migrationBuilder.CreateTable(
                name: "MS_Department",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    departmentCode = table.Column<string>(maxLength: 5, nullable: false),
                    departmentEmail = table.Column<string>(nullable: true),
                    departmentName = table.Column<string>(maxLength: 50, nullable: false),
                    departmentWhatsapp = table.Column<string>(maxLength: 20, nullable: true),
                    isActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_Department", x => x.Id);
                    table.UniqueConstraint("departmentCodeUnique", x => x.departmentCode);
                });

            migrationBuilder.CreateTable(
                name: "MS_Discount",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    discountCode = table.Column<string>(nullable: false),
                    discountName = table.Column<string>(nullable: false),
                    isActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_Discount", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MS_Entity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    entityCode = table.Column<string>(maxLength: 1, nullable: false),
                    entityName = table.Column<string>(maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_Entity", x => x.Id);
                    table.UniqueConstraint("entityCodeUnique", x => x.entityCode);
                });

            migrationBuilder.CreateTable(
                name: "MS_Facade",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    entityID = table.Column<int>(nullable: false),
                    facadeCode = table.Column<string>(maxLength: 5, nullable: false),
                    facadeName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_Facade", x => x.Id);
                    table.UniqueConstraint("facadeCodeUnique", x => x.facadeCode);
                });

            migrationBuilder.CreateTable(
                name: "MS_FormulaCode",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    formula = table.Column<string>(maxLength: 100, nullable: true),
                    formulaCode = table.Column<int>(nullable: false),
                    formulaName = table.Column<string>(maxLength: 20, nullable: true),
                    formulaNo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_FormulaCode", x => x.Id);
                    table.UniqueConstraint("formulaCodeUnique", x => x.formulaCode);
                });

            migrationBuilder.CreateTable(
                name: "MS_GroupTerm",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    GroupTermCode = table.Column<string>(maxLength: 5, nullable: false),
                    GroupTermDesc = table.Column<string>(maxLength: 20, nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    entityID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_GroupTerm", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MS_JenisKantorBank",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    JKBCode = table.Column<string>(maxLength: 5, nullable: false),
                    JKBName = table.Column<string>(maxLength: 100, nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_JenisKantorBank", x => x.Id);
                    table.UniqueConstraint("JKBCodeUnique", x => x.JKBCode);
                });

            migrationBuilder.CreateTable(
                name: "MS_Product",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    entityID = table.Column<int>(nullable: false),
                    productCode = table.Column<string>(maxLength: 5, nullable: false),
                    productName = table.Column<string>(maxLength: 30, nullable: false),
                    sortNo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_Product", x => x.Id);
                    table.UniqueConstraint("productCodeUnique", x => x.productCode);
                });

            migrationBuilder.CreateTable(
                name: "MS_Project",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BIZ_UNIT_ID = table.Column<string>(maxLength: 2, nullable: true),
                    BusinessGroup = table.Column<string>(maxLength: 5, nullable: true),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DIV_ID = table.Column<string>(maxLength: 3, nullable: false),
                    DMT_ProjectGroupCode = table.Column<string>(nullable: true),
                    DMT_ProjectGroupName = table.Column<string>(nullable: true),
                    FinanceContact = table.Column<string>(maxLength: 200, nullable: true),
                    FinanceEmail = table.Column<string>(maxLength: 200, nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    ORG_ID = table.Column<string>(maxLength: 5, nullable: true),
                    OperationalGroup = table.Column<string>(maxLength: 5, nullable: true),
                    PGContact = table.Column<string>(maxLength: 500, nullable: true),
                    PGEmail = table.Column<string>(maxLength: 200, nullable: true),
                    PGManagerID = table.Column<int>(nullable: true),
                    PGPhone = table.Column<string>(maxLength: 100, nullable: true),
                    PGStaffID = table.Column<int>(nullable: true),
                    PROJECT_ID = table.Column<string>(maxLength: 3, nullable: true),
                    SADBM = table.Column<string>(maxLength: 30, nullable: false),
                    SADBMID = table.Column<int>(nullable: true),
                    SADContact = table.Column<string>(maxLength: 500, nullable: false),
                    SADEmail = table.Column<string>(maxLength: 200, nullable: true),
                    SADFax = table.Column<string>(maxLength: 100, nullable: false),
                    SADManager = table.Column<string>(maxLength: 200, nullable: false),
                    SADManagerID = table.Column<int>(nullable: true),
                    SADPhone = table.Column<string>(maxLength: 100, nullable: false),
                    SADStaff = table.Column<string>(maxLength: 550, nullable: false),
                    SADStaffID = table.Column<int>(nullable: true),
                    TaxGroup = table.Column<string>(maxLength: 200, nullable: true),
                    bankRelationManagerID = table.Column<int>(nullable: true),
                    bankRelationStaffID = table.Column<int>(nullable: true),
                    callCenterManagerID = table.Column<int>(nullable: true),
                    callCenterStaffID = table.Column<int>(nullable: true),
                    entityID = table.Column<int>(nullable: false),
                    financeManagerID = table.Column<int>(nullable: true),
                    financeStaffID = table.Column<int>(nullable: true),
                    image = table.Column<string>(maxLength: 300, nullable: true),
                    isBookingCashier = table.Column<bool>(nullable: false),
                    isBookingSMS = table.Column<bool>(nullable: false),
                    isConfirmSP = table.Column<bool>(nullable: false),
                    isDMT = table.Column<bool>(nullable: false),
                    isPublish = table.Column<bool>(nullable: false),
                    parentProjectName = table.Column<string>(maxLength: 50, nullable: false),
                    penaltyRate = table.Column<double>(nullable: false),
                    projectCode = table.Column<string>(maxLength: 5, nullable: false),
                    projectName = table.Column<string>(maxLength: 40, nullable: false),
                    startPenaltyDay = table.Column<int>(nullable: false),
                    unitNoGroupLength = table.Column<short>(nullable: false),
                    webImage = table.Column<string>(maxLength: 300, nullable: true),
                    webSort = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_Project", x => x.Id);
                    table.UniqueConstraint("projectCodeUnique", x => x.projectCode);
                });

            migrationBuilder.CreateTable(
                name: "MS_TermMain",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BFAmount = table.Column<decimal>(type: "money", nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    entityID = table.Column<int>(nullable: false),
                    famDiscCode = table.Column<string>(maxLength: 5, nullable: false),
                    termCode = table.Column<string>(maxLength: 5, nullable: true),
                    termDesc = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_TermMain", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MS_Territory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    territoryName = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_Territory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MS_Tower",
                columns: table => new
                {
                    towerCode = table.Column<string>(maxLength: 10, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    towerName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_Tower", x => x.towerCode);
                });

            migrationBuilder.CreateTable(
                name: "MS_UnitCode",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    entityID = table.Column<int>(nullable: false),
                    unitCode = table.Column<string>(maxLength: 20, nullable: true),
                    unitName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_UnitCode", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MS_UnitType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    area = table.Column<double>(nullable: true),
                    dueDate = table.Column<DateTime>(nullable: true),
                    isMultiple = table.Column<bool>(nullable: false),
                    jumlahKamarMandi = table.Column<int>(nullable: true),
                    jumlahKamarTidur = table.Column<int>(nullable: true),
                    remarks = table.Column<string>(maxLength: 150, nullable: true),
                    unitTypeCode = table.Column<string>(maxLength: 5, nullable: false),
                    unitTypeName = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_UnitType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MS_Zoning",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    entityID = table.Column<int>(nullable: false),
                    zoningCode = table.Column<string>(maxLength: 8, nullable: false),
                    zoningName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_Zoning", x => x.Id);
                    table.UniqueConstraint("zoningCodeUnique", x => x.zoningCode);
                });

            migrationBuilder.CreateTable(
                name: "TR_BasePrice",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    projectCode = table.Column<string>(maxLength: 5, nullable: true),
                    roadCode = table.Column<string>(maxLength: 20, nullable: true),
                    unitBasePrice = table.Column<double>(nullable: false),
                    unitCode = table.Column<string>(maxLength: 20, nullable: true),
                    unitNo = table.Column<string>(maxLength: 8, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_BasePrice", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TR_BookingDetailSchedule",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    allocCode = table.Column<string>(maxLength: 3, nullable: false),
                    bookCode = table.Column<string>(maxLength: 20, nullable: true),
                    dueDate = table.Column<DateTime>(nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    netAmt = table.Column<decimal>(type: "money", nullable: false),
                    netOut = table.Column<decimal>(type: "money", nullable: false),
                    refNo = table.Column<short>(nullable: false),
                    remarks = table.Column<string>(maxLength: 100, nullable: false),
                    schedNo = table.Column<short>(nullable: false),
                    vatAmt = table.Column<decimal>(type: "money", nullable: false),
                    vatOut = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_BookingDetailSchedule", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TR_BookingDocument",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    bookCode = table.Column<string>(maxLength: 20, nullable: true),
                    docCode = table.Column<string>(maxLength: 5, nullable: true),
                    docDate = table.Column<DateTime>(nullable: false),
                    docNo = table.Column<string>(maxLength: 50, nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    inputTime = table.Column<DateTime>(nullable: false),
                    inputUN = table.Column<string>(maxLength: 40, nullable: false),
                    modifTime = table.Column<DateTime>(nullable: false),
                    modifUN = table.Column<string>(maxLength: 40, nullable: false),
                    remarks = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_BookingDocument", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TR_BookingHeader",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BFPayTypeCode = table.Column<string>(maxLength: 3, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DPCalcType = table.Column<string>(maxLength: 1, nullable: false),
                    KPRBankCode = table.Column<string>(maxLength: 5, nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    NUP = table.Column<string>(maxLength: 20, nullable: false),
                    PPJBDue = table.Column<short>(nullable: false),
                    SADStatus = table.Column<string>(maxLength: 1, nullable: false),
                    bankName = table.Column<string>(maxLength: 50, nullable: false),
                    bankNo = table.Column<string>(maxLength: 30, nullable: false),
                    bankRekeningPemilik = table.Column<string>(maxLength: 50, nullable: true),
                    bookCode = table.Column<string>(maxLength: 20, nullable: false),
                    bookDate = table.Column<DateTime>(nullable: false),
                    cancelDate = table.Column<DateTime>(nullable: true),
                    discBFCalcType = table.Column<string>(maxLength: 1, nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    facadeCode = table.Column<string>(maxLength: 5, nullable: true),
                    inputTime = table.Column<DateTime>(nullable: false),
                    inputUN = table.Column<string>(maxLength: 40, nullable: false),
                    isPenaltyStop = table.Column<bool>(nullable: false),
                    isSK = table.Column<bool>(nullable: false),
                    isSMS = table.Column<bool>(nullable: false),
                    memberCode = table.Column<string>(maxLength: 12, nullable: false),
                    memberName = table.Column<string>(maxLength: 100, nullable: false),
                    modifTime = table.Column<DateTime>(nullable: false),
                    modifUN = table.Column<string>(maxLength: 40, nullable: false),
                    netPriceComm = table.Column<decimal>(type: "money", nullable: false),
                    nomorRekeningPemilik = table.Column<string>(maxLength: 50, nullable: true),
                    promotionCode = table.Column<string>(maxLength: 50, nullable: false),
                    psCode = table.Column<string>(maxLength: 8, nullable: false),
                    remarks = table.Column<string>(maxLength: 1500, nullable: false),
                    salesEvent = table.Column<string>(maxLength: 5, nullable: false),
                    scmCode = table.Column<string>(maxLength: 3, nullable: false),
                    shopBusinessCode = table.Column<string>(maxLength: 3, nullable: false),
                    shopName = table.Column<string>(maxLength: 50, nullable: false),
                    sumberDanaCode = table.Column<string>(maxLength: 3, nullable: true),
                    termCode = table.Column<string>(maxLength: 5, nullable: false),
                    termNo = table.Column<short>(nullable: false),
                    termRemarks = table.Column<string>(maxLength: 200, nullable: false),
                    transCode = table.Column<string>(maxLength: 5, nullable: false),
                    tujuanTransaksiCode = table.Column<string>(maxLength: 3, nullable: true),
                    unitID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_BookingHeader", x => x.Id);
                    table.UniqueConstraint("bookCodeUnique", x => x.bookCode);
                });

            migrationBuilder.CreateTable(
                name: "TR_PaymentDetailSchedule",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    allocCode = table.Column<string>(maxLength: 3, nullable: false),
                    bookCode = table.Column<string>(maxLength: 20, nullable: true),
                    dueDate = table.Column<DateTime>(nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    netAmt = table.Column<decimal>(type: "money", nullable: false),
                    netOut = table.Column<decimal>(type: "money", nullable: false),
                    refNo = table.Column<short>(nullable: false),
                    remarks = table.Column<string>(maxLength: 100, nullable: false),
                    schedNo = table.Column<short>(nullable: false),
                    vatAmt = table.Column<decimal>(type: "money", nullable: false),
                    vatOut = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_PaymentDetailSchedule", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MS_Bank",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    SWIFTCode = table.Column<string>(maxLength: 11, nullable: true),
                    address = table.Column<string>(maxLength: 200, nullable: false),
                    att = table.Column<string>(maxLength: 40, nullable: false),
                    bankCode = table.Column<string>(maxLength: 5, nullable: false),
                    bankName = table.Column<string>(maxLength: 50, nullable: false),
                    bankTermId = table.Column<int>(nullable: true),
                    bankTypeID = table.Column<int>(nullable: false),
                    deputyName1 = table.Column<string>(maxLength: 40, nullable: false),
                    deputyName2 = table.Column<string>(maxLength: 40, nullable: false),
                    divertToRO = table.Column<bool>(nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    fax = table.Column<string>(maxLength: 20, nullable: false),
                    groupBankCode = table.Column<string>(maxLength: 20, nullable: true),
                    headName = table.Column<string>(maxLength: 50, nullable: false),
                    isActive = table.Column<bool>(nullable: false),
                    parentBankCode = table.Column<string>(maxLength: 5, nullable: false),
                    phone = table.Column<string>(maxLength: 20, nullable: false),
                    relationOfficerEmail = table.Column<string>(maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_Bank", x => x.Id);
                    table.UniqueConstraint("bankCodeUnique", x => x.bankCode);
                    table.ForeignKey(
                        name: "FK_MS_Bank_LK_BankLevel_bankTypeID",
                        column: x => x.bankTypeID,
                        principalTable: "LK_BankLevel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MS_Town",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    countryID = table.Column<int>(nullable: false),
                    townCode = table.Column<string>(maxLength: 5, nullable: false),
                    townName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_Town", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MS_Town_MS_Country_countryID",
                        column: x => x.countryID,
                        principalTable: "MS_Country",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MS_Position",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    departmentID = table.Column<int>(nullable: false),
                    isActive = table.Column<bool>(nullable: false),
                    positionCode = table.Column<string>(maxLength: 5, nullable: false),
                    positionName = table.Column<string>(maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_Position", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MS_Position_MS_Department_departmentID",
                        column: x => x.departmentID,
                        principalTable: "MS_Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MS_MappingFormula",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    categoryID = table.Column<int>(nullable: false),
                    formulaCodeID = table.Column<int>(nullable: false),
                    productID = table.Column<int>(nullable: false),
                    projectID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_MappingFormula", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MS_MappingFormula_MS_Category_categoryID",
                        column: x => x.categoryID,
                        principalTable: "MS_Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MS_MappingFormula_MS_FormulaCode_formulaCodeID",
                        column: x => x.formulaCodeID,
                        principalTable: "MS_FormulaCode",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MS_MappingFormula_MS_Product_productID",
                        column: x => x.productID,
                        principalTable: "MS_Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MS_MappingFormula_MS_Project_projectID",
                        column: x => x.projectID,
                        principalTable: "MS_Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MS_ProjectProduct",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    productID = table.Column<int>(nullable: false),
                    projectID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_ProjectProduct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MS_ProjectProduct_MS_Product_productID",
                        column: x => x.productID,
                        principalTable: "MS_Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MS_ProjectProduct_MS_Project_projectID",
                        column: x => x.projectID,
                        principalTable: "MS_Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MS_Renovation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    detail = table.Column<string>(maxLength: 300, nullable: true),
                    isActive = table.Column<bool>(nullable: false),
                    projectID = table.Column<int>(nullable: false),
                    renovationCode = table.Column<string>(maxLength: 5, nullable: false),
                    renovationName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_Renovation", x => x.Id);
                    table.UniqueConstraint("renovationCodeUnique", x => x.renovationCode);
                    table.ForeignKey(
                        name: "FK_MS_Renovation_MS_Project_projectID",
                        column: x => x.projectID,
                        principalTable: "MS_Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MS_Term",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DPCalcType = table.Column<string>(maxLength: 1, nullable: false),
                    GroupTermCode = table.Column<string>(maxLength: 5, nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    PPJBDue = table.Column<short>(nullable: false),
                    discBFCalcType = table.Column<string>(maxLength: 1, nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    isActive = table.Column<bool>(nullable: false),
                    projectID = table.Column<int>(nullable: false),
                    remarks = table.Column<string>(maxLength: 200, nullable: false),
                    termCode = table.Column<string>(maxLength: 5, nullable: true),
                    termInstallment = table.Column<int>(nullable: true),
                    termMainID = table.Column<int>(nullable: false),
                    termNo = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_Term", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MS_Term_MS_Project_projectID",
                        column: x => x.projectID,
                        principalTable: "MS_Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MS_Term_MS_TermMain_termMainID",
                        column: x => x.termMainID,
                        principalTable: "MS_TermMain",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MS_County",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    countyName = table.Column<string>(nullable: false),
                    territoryID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_County", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MS_County_MS_Territory_territoryID",
                        column: x => x.territoryID,
                        principalTable: "MS_Territory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MS_UnitItemPrice",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    entityID = table.Column<int>(nullable: false),
                    execMode = table.Column<string>(maxLength: 50, nullable: true),
                    execTime = table.Column<DateTime>(nullable: true),
                    execUN = table.Column<string>(maxLength: 50, nullable: true),
                    grossPrice = table.Column<decimal>(type: "money", nullable: false),
                    itemID = table.Column<int>(nullable: false),
                    renovID = table.Column<int>(nullable: false),
                    termNo = table.Column<short>(nullable: false),
                    unitCodeID = table.Column<int>(nullable: false),
                    unitNo = table.Column<string>(maxLength: 8, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_UnitItemPrice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MS_UnitItemPrice_LK_Item_itemID",
                        column: x => x.itemID,
                        principalTable: "LK_Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MS_UnitItemPrice_MS_UnitCode_unitCodeID",
                        column: x => x.unitCodeID,
                        principalTable: "MS_UnitCode",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MS_LayoutUnitType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    layout = table.Column<string>(maxLength: 300, nullable: true),
                    unitTypeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_LayoutUnitType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MS_LayoutUnitType_MS_UnitType_unitTypeID",
                        column: x => x.unitTypeID,
                        principalTable: "MS_UnitType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TR_BookingDetail",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BFAmount = table.Column<decimal>(type: "money", nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    TR_BookingHeaderId = table.Column<int>(nullable: true),
                    adjArea = table.Column<double>(nullable: false),
                    adjPrice = table.Column<decimal>(type: "money", nullable: false),
                    amount = table.Column<decimal>(type: "money", nullable: false),
                    amountComm = table.Column<decimal>(type: "money", nullable: false),
                    amountMKT = table.Column<decimal>(type: "money", nullable: false),
                    area = table.Column<double>(nullable: false),
                    bookCode = table.Column<string>(maxLength: 20, nullable: true),
                    bookNo = table.Column<int>(nullable: false),
                    coCode = table.Column<string>(maxLength: 5, nullable: false),
                    combineCode = table.Column<string>(maxLength: 1, nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    finStartDue = table.Column<short>(nullable: false),
                    finTypeCode = table.Column<string>(maxLength: 5, nullable: true),
                    inputTime = table.Column<DateTime>(nullable: false),
                    inputUN = table.Column<string>(maxLength: 40, nullable: false),
                    itemCode = table.Column<string>(maxLength: 2, nullable: false),
                    modifTime = table.Column<DateTime>(nullable: false),
                    modifUN = table.Column<string>(maxLength: 40, nullable: false),
                    netNetPrice = table.Column<decimal>(type: "money", nullable: false),
                    netPrice = table.Column<decimal>(type: "money", nullable: false),
                    netPriceCash = table.Column<decimal>(type: "money", nullable: true),
                    netPriceComm = table.Column<decimal>(type: "money", nullable: false),
                    netPriceMKT = table.Column<decimal>(type: "money", nullable: false),
                    pctDisc = table.Column<double>(nullable: false),
                    pctTax = table.Column<double>(nullable: false),
                    refNo = table.Column<short>(nullable: false),
                    termNo = table.Column<int>(nullable: false),
                    trType = table.Column<string>(maxLength: 2, nullable: false),
                    unitID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_BookingDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TR_BookingDetail_TR_BookingHeader_TR_BookingHeaderId",
                        column: x => x.TR_BookingHeaderId,
                        principalTable: "TR_BookingHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TR_PaymentHeader",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    SMSSentTime = table.Column<DateTime>(nullable: true),
                    accCode = table.Column<string>(maxLength: 5, nullable: true),
                    apvTime = table.Column<DateTime>(nullable: true),
                    apvUN = table.Column<string>(maxLength: 50, nullable: true),
                    bookingHeaderID = table.Column<int>(nullable: false),
                    clearDate = table.Column<DateTime>(nullable: true),
                    combineCode = table.Column<string>(maxLength: 1, nullable: false),
                    controlNo = table.Column<string>(maxLength: 18, nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    hadmail = table.Column<bool>(nullable: true),
                    isSMS = table.Column<bool>(nullable: false),
                    ket = table.Column<string>(maxLength: 300, nullable: false),
                    mailTime = table.Column<DateTime>(nullable: true),
                    payForCode = table.Column<string>(maxLength: 3, nullable: false),
                    paymentDate = table.Column<DateTime>(nullable: false),
                    transNo = table.Column<string>(maxLength: 18, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_PaymentHeader", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TR_PaymentHeader_TR_BookingHeader_bookingHeaderID",
                        column: x => x.bookingHeaderID,
                        principalTable: "TR_BookingHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MS_BankBranch",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    PICName = table.Column<string>(maxLength: 50, nullable: true),
                    PICPosition = table.Column<string>(maxLength: 50, nullable: true),
                    bankBranchCode = table.Column<string>(maxLength: 5, nullable: false),
                    bankBranchName = table.Column<string>(maxLength: 50, nullable: false),
                    bankBranchType = table.Column<string>(nullable: true),
                    bankBranchTypeID = table.Column<int>(nullable: false),
                    bankID = table.Column<int>(nullable: false),
                    bankRekNo = table.Column<string>(maxLength: 20, nullable: false),
                    clusterCode = table.Column<string>(maxLength: 5, nullable: true),
                    email = table.Column<string>(maxLength: 50, nullable: true),
                    entityID = table.Column<int>(nullable: false),
                    isActive = table.Column<bool>(nullable: false),
                    phone = table.Column<string>(maxLength: 20, nullable: true),
                    projectCode = table.Column<string>(maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_BankBranch", x => x.Id);
                    table.UniqueConstraint("bankBranchCodeUnique", x => x.bankBranchCode);
                    table.ForeignKey(
                        name: "FK_MS_BankBranch_LK_BankLevel_bankBranchTypeID",
                        column: x => x.bankBranchTypeID,
                        principalTable: "LK_BankLevel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MS_BankBranch_MS_Bank_bankID",
                        column: x => x.bankID,
                        principalTable: "MS_Bank",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MS_PostCode",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    district = table.Column<string>(maxLength: 100, nullable: false),
                    postCode = table.Column<string>(maxLength: 5, nullable: false),
                    province = table.Column<string>(maxLength: 100, nullable: false),
                    regency = table.Column<string>(maxLength: 100, nullable: false),
                    townID = table.Column<int>(nullable: false),
                    village = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_PostCode", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MS_PostCode_MS_Town_townID",
                        column: x => x.townID,
                        principalTable: "MS_Town",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MS_Officer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    departmentID = table.Column<int>(nullable: false),
                    isActive = table.Column<bool>(nullable: false),
                    officerEmail = table.Column<string>(maxLength: 50, nullable: false),
                    officerName = table.Column<string>(maxLength: 25, nullable: false),
                    officerPhone = table.Column<string>(maxLength: 20, nullable: false),
                    positionID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_Officer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MS_Officer_MS_Department_departmentID",
                        column: x => x.departmentID,
                        principalTable: "MS_Department",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MS_Officer_MS_Position_positionID",
                        column: x => x.positionID,
                        principalTable: "MS_Position",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MS_TermAddDisc",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    addDiscAmt = table.Column<decimal>(nullable: false),
                    addDiscNo = table.Column<short>(nullable: false),
                    addDiscPct = table.Column<double>(nullable: false),
                    discountID = table.Column<int>(nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    termID = table.Column<int>(nullable: false),
                    termNo = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_TermAddDisc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MS_TermAddDisc_MS_Discount_discountID",
                        column: x => x.discountID,
                        principalTable: "MS_Discount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MS_TermAddDisc_MS_Term_termID",
                        column: x => x.termID,
                        principalTable: "MS_Term",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MS_TermDP",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DPAmount = table.Column<decimal>(type: "money", nullable: false),
                    DPNo = table.Column<short>(nullable: false),
                    DPPct = table.Column<double>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    daysDue = table.Column<short>(nullable: false),
                    daysDueNewKP = table.Column<int>(nullable: true),
                    entityID = table.Column<int>(nullable: false),
                    termCode = table.Column<string>(maxLength: 5, nullable: true),
                    termID = table.Column<int>(nullable: false),
                    termNo = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_TermDP", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MS_TermDP_MS_Term_termID",
                        column: x => x.termID,
                        principalTable: "MS_Term",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MS_TermPmt",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    entityID = table.Column<int>(nullable: false),
                    finStartDue = table.Column<short>(nullable: false),
                    finTypeID = table.Column<int>(nullable: false),
                    termID = table.Column<int>(nullable: false),
                    termNo = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_TermPmt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MS_TermPmt_LK_FinType_finTypeID",
                        column: x => x.finTypeID,
                        principalTable: "LK_FinType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MS_TermPmt_MS_Term_termID",
                        column: x => x.termID,
                        principalTable: "MS_Term",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MS_City",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    cityName = table.Column<string>(nullable: false),
                    countyID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_City", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MS_City_MS_County_countyID",
                        column: x => x.countyID,
                        principalTable: "MS_County",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TR_PaymentDetail",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    accCode = table.Column<string>(maxLength: 5, nullable: true),
                    bankName = table.Column<string>(maxLength: 30, nullable: false),
                    chequeNo = table.Column<string>(maxLength: 30, nullable: false),
                    dueDate = table.Column<DateTime>(nullable: true),
                    ket = table.Column<string>(maxLength: 300, nullable: false),
                    othersTypeCode = table.Column<string>(maxLength: 3, nullable: false),
                    payNo = table.Column<int>(nullable: false),
                    payTypeCode = table.Column<string>(maxLength: 3, nullable: false),
                    paymentHeaderID = table.Column<int>(nullable: false),
                    status = table.Column<string>(maxLength: 1, nullable: false),
                    transNo = table.Column<string>(maxLength: 18, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_PaymentDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TR_PaymentDetail_TR_PaymentHeader_paymentHeaderID",
                        column: x => x.paymentHeaderID,
                        principalTable: "TR_PaymentHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MP_BankBranch_JKB",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    JKBID = table.Column<int>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    bankBranchID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MP_BankBranch_JKB", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MP_BankBranch_JKB_MS_JenisKantorBank_JKBID",
                        column: x => x.JKBID,
                        principalTable: "MS_JenisKantorBank",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MP_BankBranch_JKB_MS_BankBranch_bankBranchID",
                        column: x => x.bankBranchID,
                        principalTable: "MS_BankBranch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MS_Company",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    APLogin = table.Column<string>(maxLength: 6, nullable: false),
                    APServer = table.Column<string>(maxLength: 20, nullable: false),
                    APcoCode = table.Column<string>(maxLength: 6, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    KPP_Name = table.Column<string>(maxLength: 50, nullable: false),
                    KPP_TTD = table.Column<string>(maxLength: 100, nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    NPWP = table.Column<string>(maxLength: 30, nullable: false),
                    NPWPAddress = table.Column<string>(maxLength: 300, nullable: false),
                    ORG_ID = table.Column<string>(maxLength: 5, nullable: true),
                    PKP = table.Column<string>(maxLength: 30, nullable: false),
                    PKPDate = table.Column<DateTime>(nullable: false),
                    PPATK_PBJ_code = table.Column<string>(maxLength: 10, nullable: true),
                    accountNo = table.Column<string>(maxLength: 50, nullable: false),
                    address = table.Column<string>(maxLength: 500, nullable: false),
                    bankBranch = table.Column<string>(maxLength: 50, nullable: false),
                    bankName = table.Column<string>(maxLength: 50, nullable: false),
                    centerManager = table.Column<string>(maxLength: 50, nullable: true),
                    city = table.Column<string>(maxLength: 20, nullable: false),
                    coCode = table.Column<string>(maxLength: 5, nullable: false),
                    coCodeParent = table.Column<string>(maxLength: 5, nullable: false),
                    coName = table.Column<string>(maxLength: 100, nullable: false),
                    companyEmail = table.Column<string>(nullable: true),
                    country = table.Column<string>(nullable: true),
                    entityID = table.Column<int>(nullable: false),
                    faxNo = table.Column<string>(maxLength: 30, nullable: false),
                    image = table.Column<string>(nullable: true),
                    isActive = table.Column<bool>(nullable: false),
                    isCA = table.Column<bool>(nullable: false),
                    leasingManager = table.Column<string>(maxLength: 50, nullable: true),
                    mailAddress = table.Column<string>(maxLength: 200, nullable: false),
                    phoneNo = table.Column<string>(maxLength: 30, nullable: false),
                    postCodeID = table.Column<int>(nullable: false),
                    website = table.Column<string>(maxLength: 100, nullable: true),
                    workHour = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_Company", x => x.Id);
                    table.UniqueConstraint("coCodeUnique", x => x.coCode);
                    table.ForeignKey(
                        name: "FK_MS_Company_MS_PostCode_postCodeID",
                        column: x => x.postCodeID,
                        principalTable: "MS_PostCode",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MP_OfficerProject",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    officerID = table.Column<int>(nullable: false),
                    phoneNo = table.Column<string>(nullable: true),
                    projectID = table.Column<int>(nullable: false),
                    whatsappNo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MP_OfficerProject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MP_OfficerProject_MS_Officer_officerID",
                        column: x => x.officerID,
                        principalTable: "MS_Officer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MP_OfficerProject_MS_Project_projectID",
                        column: x => x.projectID,
                        principalTable: "MS_Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MS_Area",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    areaCode = table.Column<string>(maxLength: 5, nullable: false),
                    cityID = table.Column<int>(nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    regionName = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_Area", x => x.Id);
                    table.UniqueConstraint("areaCodeUnique", x => x.areaCode);
                    table.ForeignKey(
                        name: "FK_MS_Area_MS_City_cityID",
                        column: x => x.cityID,
                        principalTable: "MS_City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TR_PaymentDetailAlloc",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    accCode = table.Column<string>(maxLength: 5, nullable: true),
                    bookCode = table.Column<string>(maxLength: 20, nullable: true),
                    entityID = table.Column<string>(maxLength: 1, nullable: true),
                    netAmt = table.Column<decimal>(type: "money", nullable: false),
                    payNo = table.Column<int>(nullable: false),
                    paymentDetailID = table.Column<int>(nullable: false),
                    schedNo = table.Column<short>(nullable: false),
                    transNo = table.Column<string>(maxLength: 18, nullable: true),
                    vatAmt = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_PaymentDetailAlloc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TR_PaymentDetailAlloc_TR_PaymentDetail_paymentDetailID",
                        column: x => x.paymentDetailID,
                        principalTable: "TR_PaymentDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MP_CompanyProject",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    companyID = table.Column<int>(nullable: false),
                    mainStatus = table.Column<bool>(nullable: false),
                    projectID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MP_CompanyProject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MP_CompanyProject_MS_Company_companyID",
                        column: x => x.companyID,
                        principalTable: "MS_Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MP_CompanyProject_MS_Project_projectID",
                        column: x => x.projectID,
                        principalTable: "MS_Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MS_Account",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    NATURE_ACCOUNT_BANK = table.Column<string>(maxLength: 10, nullable: true),
                    NATURE_ACCOUNT_DEP = table.Column<string>(maxLength: 10, nullable: true),
                    ORG_ID = table.Column<string>(maxLength: 5, nullable: true),
                    PROVINCE_ID = table.Column<string>(maxLength: 2, nullable: true),
                    accCode = table.Column<string>(maxLength: 5, nullable: false),
                    accName = table.Column<string>(maxLength: 60, nullable: false),
                    accNo = table.Column<string>(maxLength: 40, nullable: false),
                    bankBranchID = table.Column<int>(nullable: false),
                    bankID = table.Column<int>(nullable: false),
                    coID = table.Column<int>(nullable: false),
                    devCode = table.Column<string>(maxLength: 5, nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    isActive = table.Column<bool>(nullable: false),
                    projectID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_Account", x => x.Id);
                    table.UniqueConstraint("accCodeUnique", x => x.accCode);
                    table.ForeignKey(
                        name: "FK_MS_Account_MS_BankBranch_bankBranchID",
                        column: x => x.bankBranchID,
                        principalTable: "MS_BankBranch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MS_Account_MS_Bank_bankID",
                        column: x => x.bankID,
                        principalTable: "MS_Bank",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MS_Account_MS_Company_coID",
                        column: x => x.coID,
                        principalTable: "MS_Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MS_Account_MS_Project_projectID",
                        column: x => x.projectID,
                        principalTable: "MS_Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MS_Unit",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CombinedUnitNo = table.Column<string>(maxLength: 8, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    TokenNo = table.Column<int>(nullable: true),
                    areaID = table.Column<int>(nullable: false),
                    categoryID = table.Column<int>(nullable: false),
                    clusterID = table.Column<int>(nullable: false),
                    detailID = table.Column<int>(nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    facingID = table.Column<int>(nullable: false),
                    prevUnitNo = table.Column<string>(maxLength: 8, nullable: false),
                    productID = table.Column<int>(nullable: false),
                    projectID = table.Column<int>(nullable: false),
                    remarks = table.Column<string>(maxLength: 100, nullable: false),
                    rentalStatusID = table.Column<int>(nullable: false),
                    termMainID = table.Column<int>(nullable: false),
                    unitCertCode = table.Column<string>(maxLength: 1, nullable: false),
                    unitCodeID = table.Column<int>(nullable: false),
                    unitNo = table.Column<string>(maxLength: 8, nullable: false),
                    unitStatusID = table.Column<int>(nullable: false),
                    zoningID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_Unit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MS_Unit_MS_Area_areaID",
                        column: x => x.areaID,
                        principalTable: "MS_Area",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MS_Unit_MS_Category_categoryID",
                        column: x => x.categoryID,
                        principalTable: "MS_Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MS_Unit_MS_Cluster_clusterID",
                        column: x => x.clusterID,
                        principalTable: "MS_Cluster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MS_Unit_LK_Facing_facingID",
                        column: x => x.facingID,
                        principalTable: "LK_Facing",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MS_Unit_MS_Product_productID",
                        column: x => x.productID,
                        principalTable: "MS_Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MS_Unit_MS_Project_projectID",
                        column: x => x.projectID,
                        principalTable: "MS_Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MS_Unit_MS_TermMain_termMainID",
                        column: x => x.termMainID,
                        principalTable: "MS_TermMain",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MS_Unit_MS_UnitCode_unitCodeID",
                        column: x => x.unitCodeID,
                        principalTable: "MS_UnitCode",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MS_Unit_LK_UnitStatus_unitStatusID",
                        column: x => x.unitStatusID,
                        principalTable: "LK_UnitStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MS_Unit_MS_Zoning_zoningID",
                        column: x => x.zoningID,
                        principalTable: "MS_Zoning",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MS_UnitItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    amount = table.Column<decimal>(type: "money", nullable: false),
                    area = table.Column<double>(nullable: false),
                    coCode = table.Column<string>(maxLength: 5, nullable: false),
                    dimension = table.Column<string>(maxLength: 50, nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    itemID = table.Column<int>(nullable: false),
                    pctDisc = table.Column<double>(nullable: false),
                    pctTax = table.Column<double>(nullable: false),
                    unitID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_UnitItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MS_UnitItem_LK_Item_itemID",
                        column: x => x.itemID,
                        principalTable: "LK_Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MS_UnitItem_MS_Unit_unitID",
                        column: x => x.unitID,
                        principalTable: "MS_Unit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MP_BankBranch_JKB_JKBID",
                table: "MP_BankBranch_JKB",
                column: "JKBID");

            migrationBuilder.CreateIndex(
                name: "IX_MP_BankBranch_JKB_bankBranchID",
                table: "MP_BankBranch_JKB",
                column: "bankBranchID");

            migrationBuilder.CreateIndex(
                name: "IX_MP_CompanyProject_companyID",
                table: "MP_CompanyProject",
                column: "companyID");

            migrationBuilder.CreateIndex(
                name: "IX_MP_CompanyProject_projectID",
                table: "MP_CompanyProject",
                column: "projectID");

            migrationBuilder.CreateIndex(
                name: "IX_MP_OfficerProject_officerID",
                table: "MP_OfficerProject",
                column: "officerID");

            migrationBuilder.CreateIndex(
                name: "IX_MP_OfficerProject_projectID",
                table: "MP_OfficerProject",
                column: "projectID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_Account_bankBranchID",
                table: "MS_Account",
                column: "bankBranchID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_Account_bankID",
                table: "MS_Account",
                column: "bankID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_Account_coID",
                table: "MS_Account",
                column: "coID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_Account_projectID",
                table: "MS_Account",
                column: "projectID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_Area_cityID",
                table: "MS_Area",
                column: "cityID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_Bank_bankTypeID",
                table: "MS_Bank",
                column: "bankTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_BankBranch_bankBranchTypeID",
                table: "MS_BankBranch",
                column: "bankBranchTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_BankBranch_bankID",
                table: "MS_BankBranch",
                column: "bankID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_City_countyID",
                table: "MS_City",
                column: "countyID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_Company_postCodeID",
                table: "MS_Company",
                column: "postCodeID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_County_territoryID",
                table: "MS_County",
                column: "territoryID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_LayoutUnitType_unitTypeID",
                table: "MS_LayoutUnitType",
                column: "unitTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_MappingFormula_categoryID",
                table: "MS_MappingFormula",
                column: "categoryID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_MappingFormula_formulaCodeID",
                table: "MS_MappingFormula",
                column: "formulaCodeID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_MappingFormula_productID",
                table: "MS_MappingFormula",
                column: "productID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_MappingFormula_projectID",
                table: "MS_MappingFormula",
                column: "projectID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_Officer_departmentID",
                table: "MS_Officer",
                column: "departmentID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_Officer_positionID",
                table: "MS_Officer",
                column: "positionID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_Position_departmentID",
                table: "MS_Position",
                column: "departmentID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_PostCode_townID",
                table: "MS_PostCode",
                column: "townID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_ProjectProduct_productID",
                table: "MS_ProjectProduct",
                column: "productID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_ProjectProduct_projectID",
                table: "MS_ProjectProduct",
                column: "projectID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_Renovation_projectID",
                table: "MS_Renovation",
                column: "projectID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_Term_projectID",
                table: "MS_Term",
                column: "projectID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_Term_termMainID",
                table: "MS_Term",
                column: "termMainID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_TermAddDisc_discountID",
                table: "MS_TermAddDisc",
                column: "discountID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_TermAddDisc_termID",
                table: "MS_TermAddDisc",
                column: "termID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_TermDP_termID",
                table: "MS_TermDP",
                column: "termID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_TermPmt_finTypeID",
                table: "MS_TermPmt",
                column: "finTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_TermPmt_termID",
                table: "MS_TermPmt",
                column: "termID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_Town_countryID",
                table: "MS_Town",
                column: "countryID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_Unit_areaID",
                table: "MS_Unit",
                column: "areaID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_Unit_categoryID",
                table: "MS_Unit",
                column: "categoryID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_Unit_clusterID",
                table: "MS_Unit",
                column: "clusterID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_Unit_facingID",
                table: "MS_Unit",
                column: "facingID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_Unit_productID",
                table: "MS_Unit",
                column: "productID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_Unit_projectID",
                table: "MS_Unit",
                column: "projectID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_Unit_termMainID",
                table: "MS_Unit",
                column: "termMainID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_Unit_unitCodeID",
                table: "MS_Unit",
                column: "unitCodeID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_Unit_unitStatusID",
                table: "MS_Unit",
                column: "unitStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_Unit_zoningID",
                table: "MS_Unit",
                column: "zoningID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_UnitItem_itemID",
                table: "MS_UnitItem",
                column: "itemID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_UnitItem_unitID",
                table: "MS_UnitItem",
                column: "unitID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_UnitItemPrice_itemID",
                table: "MS_UnitItemPrice",
                column: "itemID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_UnitItemPrice_unitCodeID",
                table: "MS_UnitItemPrice",
                column: "unitCodeID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_BookingDetail_TR_BookingHeaderId",
                table: "TR_BookingDetail",
                column: "TR_BookingHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_TR_PaymentDetail_paymentHeaderID",
                table: "TR_PaymentDetail",
                column: "paymentHeaderID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_PaymentDetailAlloc_paymentDetailID",
                table: "TR_PaymentDetailAlloc",
                column: "paymentDetailID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_PaymentHeader_bookingHeaderID",
                table: "TR_PaymentHeader",
                column: "bookingHeaderID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LK_DPCalc");

            migrationBuilder.DropTable(
                name: "MP_BankBranch_JKB");

            migrationBuilder.DropTable(
                name: "MP_CompanyProject");

            migrationBuilder.DropTable(
                name: "MP_OfficerProject");

            migrationBuilder.DropTable(
                name: "MS_Account");

            migrationBuilder.DropTable(
                name: "MS_Block");

            migrationBuilder.DropTable(
                name: "MS_Entity");

            migrationBuilder.DropTable(
                name: "MS_Facade");

            migrationBuilder.DropTable(
                name: "MS_GroupTerm");

            migrationBuilder.DropTable(
                name: "MS_LayoutUnitType");

            migrationBuilder.DropTable(
                name: "MS_MappingFormula");

            migrationBuilder.DropTable(
                name: "MS_ProjectProduct");

            migrationBuilder.DropTable(
                name: "MS_Renovation");

            migrationBuilder.DropTable(
                name: "MS_TermAddDisc");

            migrationBuilder.DropTable(
                name: "MS_TermDP");

            migrationBuilder.DropTable(
                name: "MS_TermPmt");

            migrationBuilder.DropTable(
                name: "MS_Tower");

            migrationBuilder.DropTable(
                name: "MS_UnitItem");

            migrationBuilder.DropTable(
                name: "MS_UnitItemPrice");

            migrationBuilder.DropTable(
                name: "TR_BasePrice");

            migrationBuilder.DropTable(
                name: "TR_BookingDetail");

            migrationBuilder.DropTable(
                name: "TR_BookingDetailSchedule");

            migrationBuilder.DropTable(
                name: "TR_BookingDocument");

            migrationBuilder.DropTable(
                name: "TR_PaymentDetailAlloc");

            migrationBuilder.DropTable(
                name: "TR_PaymentDetailSchedule");

            migrationBuilder.DropTable(
                name: "MS_JenisKantorBank");

            migrationBuilder.DropTable(
                name: "MS_Officer");

            migrationBuilder.DropTable(
                name: "MS_BankBranch");

            migrationBuilder.DropTable(
                name: "MS_Company");

            migrationBuilder.DropTable(
                name: "MS_UnitType");

            migrationBuilder.DropTable(
                name: "MS_FormulaCode");

            migrationBuilder.DropTable(
                name: "MS_Discount");

            migrationBuilder.DropTable(
                name: "LK_FinType");

            migrationBuilder.DropTable(
                name: "MS_Term");

            migrationBuilder.DropTable(
                name: "MS_Unit");

            migrationBuilder.DropTable(
                name: "LK_Item");

            migrationBuilder.DropTable(
                name: "TR_PaymentDetail");

            migrationBuilder.DropTable(
                name: "MS_Position");

            migrationBuilder.DropTable(
                name: "MS_Bank");

            migrationBuilder.DropTable(
                name: "MS_PostCode");

            migrationBuilder.DropTable(
                name: "MS_Area");

            migrationBuilder.DropTable(
                name: "MS_Category");

            migrationBuilder.DropTable(
                name: "MS_Cluster");

            migrationBuilder.DropTable(
                name: "LK_Facing");

            migrationBuilder.DropTable(
                name: "MS_Product");

            migrationBuilder.DropTable(
                name: "MS_Project");

            migrationBuilder.DropTable(
                name: "MS_TermMain");

            migrationBuilder.DropTable(
                name: "MS_UnitCode");

            migrationBuilder.DropTable(
                name: "LK_UnitStatus");

            migrationBuilder.DropTable(
                name: "MS_Zoning");

            migrationBuilder.DropTable(
                name: "TR_PaymentHeader");

            migrationBuilder.DropTable(
                name: "MS_Department");

            migrationBuilder.DropTable(
                name: "LK_BankLevel");

            migrationBuilder.DropTable(
                name: "MS_Town");

            migrationBuilder.DropTable(
                name: "MS_City");

            migrationBuilder.DropTable(
                name: "TR_BookingHeader");

            migrationBuilder.DropTable(
                name: "MS_Country");

            migrationBuilder.DropTable(
                name: "MS_County");

            migrationBuilder.DropTable(
                name: "MS_Territory");
        }
    }
}
