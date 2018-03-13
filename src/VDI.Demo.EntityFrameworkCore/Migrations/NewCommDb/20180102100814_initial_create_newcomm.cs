using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.NewCommDb
{
    public partial class initial_create_newcomm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MS_Flag",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    entityID = table.Column<int>(nullable: false),
                    flagCode = table.Column<string>(maxLength: 1, nullable: false),
                    flagDesc = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_Flag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MS_Schema",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    accACDPeriod = table.Column<int>(nullable: false),
                    accACDPeriodType = table.Column<string>(maxLength: 4, nullable: false),
                    accCDPeriod = table.Column<int>(nullable: false),
                    accCDPeriodType = table.Column<string>(maxLength: 4, nullable: false),
                    accPeriod = table.Column<int>(nullable: false),
                    accPeriodType = table.Column<string>(maxLength: 4, nullable: false),
                    budgetPct = table.Column<double>(nullable: true),
                    digitMemberCode = table.Column<string>(maxLength: 2, nullable: false),
                    document = table.Column<string>(nullable: true),
                    dueDateComm = table.Column<int>(nullable: false),
                    entityCode = table.Column<string>(maxLength: 1, nullable: true),
                    entityID = table.Column<int>(nullable: false),
                    isACDGetComm = table.Column<bool>(nullable: false),
                    isAcc = table.Column<bool>(nullable: false),
                    isAccACD = table.Column<bool>(nullable: false),
                    isAccCD = table.Column<bool>(nullable: false),
                    isActive = table.Column<bool>(nullable: false),
                    isAutomaticMemberStatus = table.Column<bool>(nullable: false),
                    isBudget = table.Column<bool>(nullable: false),
                    isCDGetComm = table.Column<bool>(nullable: false),
                    isCapacity = table.Column<bool>(nullable: false),
                    isClub = table.Column<bool>(name: "isClub$", nullable: false),
                    isCommHold = table.Column<bool>(nullable: false),
                    isComplete = table.Column<bool>(nullable: false),
                    isFix = table.Column<bool>(nullable: false),
                    isFixACD = table.Column<bool>(nullable: false),
                    isFixCD = table.Column<bool>(nullable: false),
                    isHaveACD = table.Column<bool>(nullable: false),
                    isHaveCD = table.Column<bool>(nullable: false),
                    isOverRiding = table.Column<bool>(nullable: false),
                    isPointCalc = table.Column<bool>(nullable: false),
                    isSendSMSPaid = table.Column<bool>(nullable: false),
                    isTeam = table.Column<bool>(nullable: false),
                    scmCode = table.Column<string>(maxLength: 3, nullable: false),
                    scmName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_Schema", x => x.Id);
                    table.UniqueConstraint("scmCodeUnique", x => x.scmCode);
                });

            migrationBuilder.CreateTable(
                name: "TR_BudgetPayment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    bookNo = table.Column<string>(maxLength: 20, nullable: false),
                    budgetAmount = table.Column<decimal>(type: "money", nullable: false),
                    budgetPPh = table.Column<decimal>(type: "money", nullable: false),
                    budgetPaidDate = table.Column<DateTime>(nullable: true),
                    budgetPayOrderDate = table.Column<DateTime>(nullable: true),
                    budgetVAT = table.Column<decimal>(type: "money", nullable: false),
                    devCode = table.Column<string>(maxLength: 5, nullable: true),
                    entityCode = table.Column<string>(maxLength: 1, nullable: true),
                    isPostFakturPajak = table.Column<bool>(nullable: false),
                    memberCode = table.Column<string>(maxLength: 12, nullable: false),
                    oracleInvoiceID = table.Column<long>(nullable: false),
                    payOrderNo = table.Column<string>(maxLength: 30, nullable: false),
                    propCode = table.Column<string>(maxLength: 5, nullable: true),
                    reqNo = table.Column<byte>(nullable: false),
                    scmCode = table.Column<string>(maxLength: 3, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_BudgetPayment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TR_SoldUnitFlag",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    bookNo = table.Column<string>(maxLength: 20, nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    flagCode = table.Column<string>(maxLength: 1, nullable: true),
                    flagDate = table.Column<DateTime>(nullable: false),
                    flagID = table.Column<int>(nullable: false),
                    remarks = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_SoldUnitFlag", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TR_SoldUnitFlag_MS_Flag_flagID",
                        column: x => x.flagID,
                        principalTable: "MS_Flag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LK_CommType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    commTypeCode = table.Column<string>(maxLength: 3, nullable: false),
                    commTypeName = table.Column<string>(maxLength: 30, nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    isComplete = table.Column<bool>(nullable: false),
                    schemaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LK_CommType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LK_CommType_MS_Schema_schemaID",
                        column: x => x.schemaID,
                        principalTable: "MS_Schema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LK_PointType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    entityID = table.Column<int>(nullable: false),
                    isComplete = table.Column<bool>(nullable: false),
                    pointTypeCode = table.Column<string>(maxLength: 3, nullable: false),
                    pointTypeName = table.Column<string>(maxLength: 30, nullable: false),
                    schemaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LK_PointType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LK_PointType_MS_Schema_schemaID",
                        column: x => x.schemaID,
                        principalTable: "MS_Schema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LK_Upline",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    entityID = table.Column<int>(nullable: false),
                    isComplete = table.Column<bool>(nullable: false),
                    schemaID = table.Column<int>(nullable: false),
                    uplineNo = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LK_Upline", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LK_Upline_MS_Schema_schemaID",
                        column: x => x.schemaID,
                        principalTable: "MS_Schema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MS_BobotComm",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    clusterID = table.Column<int>(nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    isActive = table.Column<bool>(nullable: false),
                    isComplete = table.Column<bool>(nullable: false),
                    pctBobot = table.Column<double>(nullable: false),
                    projectID = table.Column<int>(nullable: false),
                    schemaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_BobotComm", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MS_BobotComm_MS_Schema_schemaID",
                        column: x => x.schemaID,
                        principalTable: "MS_Schema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MS_GroupSchema",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    clusterID = table.Column<int>(nullable: false),
                    documentGrouping = table.Column<string>(nullable: true),
                    entityID = table.Column<int>(nullable: false),
                    groupSchemaCode = table.Column<string>(maxLength: 4, nullable: false),
                    groupSchemaName = table.Column<string>(maxLength: 50, nullable: false),
                    isActive = table.Column<bool>(nullable: false),
                    isComplete = table.Column<bool>(nullable: false),
                    isStandard = table.Column<bool>(nullable: false),
                    projectID = table.Column<int>(nullable: false),
                    schemaID = table.Column<int>(nullable: false),
                    validFrom = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_GroupSchema", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MS_GroupSchema_MS_Schema_schemaID",
                        column: x => x.schemaID,
                        principalTable: "MS_Schema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MS_PPhRange",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    TAX_CODE = table.Column<string>(maxLength: 15, nullable: true),
                    TAX_CODE_NON_NPWP = table.Column<string>(maxLength: 15, nullable: true),
                    entityID = table.Column<int>(nullable: false),
                    isActive = table.Column<bool>(nullable: false),
                    isComplete = table.Column<bool>(nullable: false),
                    pphRangeHighBound = table.Column<decimal>(nullable: false),
                    pphRangePct = table.Column<double>(nullable: false),
                    pphRangePct_NON_NPWP = table.Column<string>(maxLength: 15, nullable: true),
                    pphYear = table.Column<int>(nullable: false),
                    schemaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_PPhRange", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MS_PPhRange_MS_Schema_schemaID",
                        column: x => x.schemaID,
                        principalTable: "MS_Schema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MS_PPhRangeIns",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    TAX_CODE = table.Column<string>(maxLength: 15, nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    isActive = table.Column<bool>(nullable: false),
                    isComplete = table.Column<bool>(nullable: false),
                    pphRangePct = table.Column<double>(nullable: false),
                    schemaID = table.Column<int>(nullable: false),
                    validDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_PPhRangeIns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MS_PPhRangeIns_MS_Schema_schemaID",
                        column: x => x.schemaID,
                        principalTable: "MS_Schema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MS_Property",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    entityID = table.Column<int>(nullable: false),
                    isComplete = table.Column<bool>(nullable: false),
                    propCode = table.Column<string>(maxLength: 5, nullable: false),
                    propDesc = table.Column<string>(maxLength: 100, nullable: false),
                    propName = table.Column<string>(maxLength: 30, nullable: false),
                    schemaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_Property", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MS_Property_MS_Schema_schemaID",
                        column: x => x.schemaID,
                        principalTable: "MS_Schema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MS_SchemaRequirement",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    entityID = table.Column<int>(nullable: false),
                    isComplete = table.Column<bool>(nullable: false),
                    orPctPaid = table.Column<double>(nullable: false),
                    pctPaid = table.Column<double>(nullable: false),
                    reqDesc = table.Column<string>(maxLength: 40, nullable: false),
                    reqNo = table.Column<short>(nullable: false),
                    schemaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_SchemaRequirement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MS_SchemaRequirement_MS_Schema_schemaID",
                        column: x => x.schemaID,
                        principalTable: "MS_Schema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MS_StatusMember",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    entityID = table.Column<int>(nullable: false),
                    isComplete = table.Column<bool>(nullable: false),
                    pointMin = table.Column<decimal>(nullable: false),
                    pointToKeepStatus = table.Column<decimal>(nullable: false),
                    reviewStartMonth = table.Column<int>(nullable: false),
                    reviewTimeYear = table.Column<int>(nullable: false),
                    schemaID = table.Column<int>(nullable: false),
                    statusCode = table.Column<string>(maxLength: 5, nullable: false),
                    statusName = table.Column<string>(maxLength: 30, nullable: false),
                    statusStar = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_StatusMember", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MS_StatusMember_MS_Schema_schemaID",
                        column: x => x.schemaID,
                        principalTable: "MS_Schema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MS_GroupSchemaRequirement",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    entityID = table.Column<int>(nullable: false),
                    groupSchemaID = table.Column<int>(nullable: false),
                    isComplete = table.Column<bool>(nullable: false),
                    orPctPaid = table.Column<double>(nullable: false),
                    pctPaid = table.Column<double>(nullable: false),
                    reqDesc = table.Column<string>(maxLength: 40, nullable: false),
                    reqNo = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_GroupSchemaRequirement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MS_GroupSchemaRequirement_MS_GroupSchema_groupSchemaID",
                        column: x => x.groupSchemaID,
                        principalTable: "MS_GroupSchema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MS_Developer_Schema",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    bankAccountName = table.Column<string>(maxLength: 50, nullable: false),
                    bankBranchName = table.Column<string>(maxLength: 50, nullable: false),
                    bankCode = table.Column<string>(maxLength: 5, nullable: false),
                    devCode = table.Column<string>(maxLength: 5, nullable: false),
                    devName = table.Column<string>(maxLength: 50, nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    isActive = table.Column<bool>(nullable: false),
                    isComplete = table.Column<bool>(nullable: false),
                    propertyID = table.Column<int>(nullable: false),
                    schemaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_Developer_Schema", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MS_Developer_Schema_MS_Property_propertyID",
                        column: x => x.propertyID,
                        principalTable: "MS_Property",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MS_Developer_Schema_MS_Schema_schemaID",
                        column: x => x.schemaID,
                        principalTable: "MS_Schema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MS_CommPct",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    asUplineNo = table.Column<byte>(nullable: false),
                    commPctHold = table.Column<double>(nullable: false),
                    commPctPaid = table.Column<double>(nullable: true),
                    commTypeID = table.Column<int>(nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    isComplete = table.Column<bool>(nullable: false),
                    maxAmt = table.Column<decimal>(type: "money", nullable: false),
                    minAmt = table.Column<decimal>(type: "money", nullable: false),
                    nominal = table.Column<decimal>(type: "money", nullable: true),
                    schemaID = table.Column<int>(nullable: false),
                    statusMemberID = table.Column<int>(nullable: false),
                    validDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_CommPct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MS_CommPct_LK_CommType_commTypeID",
                        column: x => x.commTypeID,
                        principalTable: "LK_CommType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MS_CommPct_MS_Schema_schemaID",
                        column: x => x.schemaID,
                        principalTable: "MS_Schema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MS_CommPct_MS_StatusMember_statusMemberID",
                        column: x => x.statusMemberID,
                        principalTable: "MS_StatusMember",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MS_GroupCommPct",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    asUplineNo = table.Column<byte>(nullable: false),
                    commPctHold = table.Column<double>(nullable: false),
                    commPctPaid = table.Column<double>(nullable: true),
                    commTypeID = table.Column<int>(nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    groupSchemaID = table.Column<int>(nullable: false),
                    isComplete = table.Column<bool>(nullable: false),
                    maxAmt = table.Column<decimal>(type: "money", nullable: false),
                    minAmt = table.Column<decimal>(type: "money", nullable: false),
                    nominal = table.Column<decimal>(type: "money", nullable: true),
                    statusMemberID = table.Column<int>(nullable: false),
                    validDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_GroupCommPct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MS_GroupCommPct_LK_CommType_commTypeID",
                        column: x => x.commTypeID,
                        principalTable: "LK_CommType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MS_GroupCommPct_MS_GroupSchema_groupSchemaID",
                        column: x => x.groupSchemaID,
                        principalTable: "MS_GroupSchema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MS_GroupCommPct_MS_StatusMember_statusMemberID",
                        column: x => x.statusMemberID,
                        principalTable: "MS_StatusMember",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MS_GroupCommPctNonStd",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    asUplineNo = table.Column<byte>(nullable: false),
                    commPctHold = table.Column<double>(nullable: false),
                    commPctPaid = table.Column<double>(nullable: true),
                    commTypeID = table.Column<int>(nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    groupSchemaID = table.Column<int>(nullable: false),
                    isComplete = table.Column<bool>(nullable: false),
                    maxAmt = table.Column<decimal>(type: "money", nullable: false),
                    minAmt = table.Column<decimal>(type: "money", nullable: false),
                    nominal = table.Column<decimal>(type: "money", nullable: true),
                    statusMemberID = table.Column<int>(nullable: false),
                    validDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_GroupCommPctNonStd", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MS_GroupCommPctNonStd_LK_CommType_commTypeID",
                        column: x => x.commTypeID,
                        principalTable: "LK_CommType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MS_GroupCommPctNonStd_MS_GroupSchema_groupSchemaID",
                        column: x => x.groupSchemaID,
                        principalTable: "MS_GroupSchema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MS_GroupCommPctNonStd_MS_StatusMember_statusMemberID",
                        column: x => x.statusMemberID,
                        principalTable: "MS_StatusMember",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MS_PointPct",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    asUplineNo = table.Column<byte>(nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    isActive = table.Column<bool>(nullable: false),
                    isComplete = table.Column<bool>(nullable: false),
                    pointKonvert = table.Column<decimal>(nullable: false),
                    pointPct = table.Column<double>(nullable: false),
                    pointTypeID = table.Column<int>(nullable: false),
                    schemaID = table.Column<int>(nullable: false),
                    statusMemberID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_PointPct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MS_PointPct_LK_PointType_pointTypeID",
                        column: x => x.pointTypeID,
                        principalTable: "LK_PointType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MS_PointPct_MS_Schema_schemaID",
                        column: x => x.schemaID,
                        principalTable: "MS_Schema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MS_PointPct_MS_StatusMember_statusMemberID",
                        column: x => x.statusMemberID,
                        principalTable: "MS_StatusMember",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MS_ManagementPct",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    bankAccountName = table.Column<string>(maxLength: 50, nullable: false),
                    bankBranchName = table.Column<string>(maxLength: 50, nullable: false),
                    bankCode = table.Column<string>(maxLength: 5, nullable: false),
                    developerSchemaID = table.Column<int>(nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    isActive = table.Column<bool>(nullable: false),
                    isComplete = table.Column<bool>(nullable: false),
                    managementPct = table.Column<double>(nullable: false),
                    schemaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_ManagementPct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MS_ManagementPct_MS_Developer_Schema_developerSchemaID",
                        column: x => x.developerSchemaID,
                        principalTable: "MS_Developer_Schema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MS_ManagementPct_MS_Schema_schemaID",
                        column: x => x.schemaID,
                        principalTable: "MS_Schema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TR_CommPayment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    NPWP = table.Column<string>(maxLength: 30, nullable: false),
                    PPNAmount = table.Column<decimal>(type: "money", nullable: false),
                    amount = table.Column<decimal>(type: "money", nullable: false),
                    asUplineNo = table.Column<short>(nullable: false),
                    bankAccName = table.Column<string>(maxLength: 50, nullable: false),
                    bankAccNo = table.Column<string>(maxLength: 50, nullable: false),
                    bankBranchName = table.Column<string>(maxLength: 50, nullable: false),
                    bankCode = table.Column<string>(maxLength: 5, nullable: false),
                    bankType = table.Column<string>(maxLength: 1, nullable: false),
                    bookNo = table.Column<string>(maxLength: 20, nullable: false),
                    commNo = table.Column<short>(nullable: false),
                    commPayCode = table.Column<string>(maxLength: 30, nullable: false),
                    commTypeCode = table.Column<string>(maxLength: 3, nullable: true),
                    commTypeID = table.Column<int>(nullable: false),
                    desc = table.Column<string>(maxLength: 100, nullable: false),
                    developerSchemaID = table.Column<int>(nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    isAutoCalc = table.Column<bool>(nullable: false),
                    isHold = table.Column<string>(maxLength: 1, nullable: false),
                    isInstitusi = table.Column<bool>(nullable: false),
                    memberCode = table.Column<string>(maxLength: 12, nullable: false),
                    memberName = table.Column<string>(maxLength: 50, nullable: false),
                    oracleInvoiceID = table.Column<long>(nullable: false),
                    paidDate = table.Column<DateTime>(nullable: true),
                    paidNo = table.Column<string>(maxLength: 32, nullable: true),
                    payOrderDate = table.Column<DateTime>(nullable: true),
                    payOrderNo = table.Column<string>(maxLength: 30, nullable: false),
                    pointValue = table.Column<decimal>(type: "money", nullable: false),
                    pphAmount = table.Column<decimal>(type: "money", nullable: true),
                    pphProcessDate = table.Column<DateTime>(nullable: true),
                    pphYear = table.Column<short>(nullable: true),
                    reqNo = table.Column<byte>(nullable: false),
                    schedDate = table.Column<DateTime>(nullable: false),
                    schemaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_CommPayment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TR_CommPayment_LK_CommType_commTypeID",
                        column: x => x.commTypeID,
                        principalTable: "LK_CommType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TR_CommPayment_MS_Developer_Schema_developerSchemaID",
                        column: x => x.developerSchemaID,
                        principalTable: "MS_Developer_Schema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TR_CommPayment_MS_Schema_schemaID",
                        column: x => x.schemaID,
                        principalTable: "MS_Schema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TR_CommPaymentPph",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AWT_GROUP_NAME = table.Column<string>(maxLength: 15, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    PPHRange = table.Column<double>(nullable: false),
                    amount = table.Column<decimal>(type: "money", nullable: false),
                    asUplineNo = table.Column<short>(nullable: false),
                    bookNo = table.Column<string>(maxLength: 20, nullable: false),
                    commNo = table.Column<short>(nullable: false),
                    commTypeID = table.Column<int>(nullable: false),
                    developerSchemaID = table.Column<int>(nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    isHold = table.Column<string>(maxLength: 1, nullable: false),
                    isInst = table.Column<bool>(nullable: false),
                    isPKP = table.Column<bool>(nullable: false),
                    memberCode = table.Column<string>(maxLength: 12, nullable: false),
                    pphNo = table.Column<short>(nullable: false),
                    reqNo = table.Column<byte>(nullable: false),
                    schemaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_CommPaymentPph", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TR_CommPaymentPph_LK_CommType_commTypeID",
                        column: x => x.commTypeID,
                        principalTable: "LK_CommType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TR_CommPaymentPph_MS_Developer_Schema_developerSchemaID",
                        column: x => x.developerSchemaID,
                        principalTable: "MS_Developer_Schema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TR_CommPaymentPph_MS_Schema_schemaID",
                        column: x => x.schemaID,
                        principalTable: "MS_Schema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TR_CommPct",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    asUplineNo = table.Column<short>(nullable: false),
                    bookNo = table.Column<string>(maxLength: 20, nullable: false),
                    commPctPaid = table.Column<double>(nullable: false),
                    commTypeID = table.Column<int>(nullable: false),
                    developerSchemaID = table.Column<int>(nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    memberCodeN = table.Column<string>(maxLength: 20, nullable: false),
                    memberCodeR = table.Column<string>(maxLength: 20, nullable: false),
                    pointTypeID = table.Column<int>(nullable: false),
                    pphRangeID = table.Column<int>(nullable: false),
                    pphRangeInsID = table.Column<int>(nullable: false),
                    statusMemberID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_CommPct", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TR_CommPct_LK_CommType_commTypeID",
                        column: x => x.commTypeID,
                        principalTable: "LK_CommType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TR_CommPct_MS_Developer_Schema_developerSchemaID",
                        column: x => x.developerSchemaID,
                        principalTable: "MS_Developer_Schema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TR_CommPct_LK_PointType_pointTypeID",
                        column: x => x.pointTypeID,
                        principalTable: "LK_PointType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TR_CommPct_MS_PPhRange_pphRangeID",
                        column: x => x.pphRangeID,
                        principalTable: "MS_PPhRange",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TR_CommPct_MS_PPhRangeIns_pphRangeInsID",
                        column: x => x.pphRangeInsID,
                        principalTable: "MS_PPhRangeIns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TR_CommPct_MS_StatusMember_statusMemberID",
                        column: x => x.statusMemberID,
                        principalTable: "MS_StatusMember",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TR_ManagementFee",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    bookNo = table.Column<string>(maxLength: 20, nullable: false),
                    developerSchemaID = table.Column<int>(nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    isPostFakturPajak = table.Column<bool>(nullable: false),
                    memberCode = table.Column<string>(maxLength: 12, nullable: false),
                    mgmtFee = table.Column<decimal>(type: "money", nullable: false),
                    mgmtPPh = table.Column<decimal>(type: "money", nullable: false),
                    mgmtPaidDate = table.Column<DateTime>(nullable: true),
                    mgmtPayOrderDate = table.Column<DateTime>(nullable: true),
                    mgmtVAT = table.Column<decimal>(type: "money", nullable: false),
                    oracleInvoiceID = table.Column<long>(nullable: false),
                    payOrderNo = table.Column<string>(maxLength: 30, nullable: false),
                    reqNo = table.Column<int>(nullable: false),
                    schemaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_ManagementFee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TR_ManagementFee_MS_Developer_Schema_developerSchemaID",
                        column: x => x.developerSchemaID,
                        principalTable: "MS_Developer_Schema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TR_ManagementFee_MS_Schema_schemaID",
                        column: x => x.schemaID,
                        principalTable: "MS_Schema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TR_SoldUnit",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ACDCode = table.Column<string>(maxLength: 12, nullable: false),
                    CDCode = table.Column<string>(maxLength: 12, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    PPJBDate = table.Column<DateTime>(nullable: true),
                    Remarks = table.Column<string>(maxLength: 100, nullable: true),
                    batchNo = table.Column<string>(maxLength: 11, nullable: false),
                    bookDate = table.Column<DateTime>(nullable: false),
                    bookNo = table.Column<string>(maxLength: 20, nullable: false),
                    calculateUseMaster = table.Column<bool>(nullable: false),
                    cancelDate = table.Column<DateTime>(nullable: true),
                    changeDealClosureReason = table.Column<string>(maxLength: 100, nullable: true),
                    developerSchemaID = table.Column<int>(nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    holdDate = table.Column<DateTime>(nullable: true),
                    holdReason = table.Column<string>(maxLength: 100, nullable: true),
                    memberCode = table.Column<string>(maxLength: 12, nullable: false),
                    netNetPrice = table.Column<decimal>(type: "money", nullable: false),
                    pctBobot = table.Column<double>(nullable: false),
                    pctComm = table.Column<double>(nullable: false),
                    roadCode = table.Column<string>(maxLength: 20, nullable: false),
                    roadName = table.Column<string>(maxLength: 50, nullable: false),
                    schemaID = table.Column<int>(nullable: false),
                    termRemarks = table.Column<string>(maxLength: 40, nullable: false),
                    unitBuildArea = table.Column<float>(nullable: false),
                    unitID = table.Column<int>(nullable: false),
                    unitLandArea = table.Column<float>(nullable: false),
                    unitNo = table.Column<string>(maxLength: 10, nullable: false),
                    unitPrice = table.Column<decimal>(type: "money", nullable: false),
                    xprocessDate = table.Column<DateTime>(nullable: true),
                    xreqInstPayDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_SoldUnit", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TR_SoldUnit_MS_Developer_Schema_developerSchemaID",
                        column: x => x.developerSchemaID,
                        principalTable: "MS_Developer_Schema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TR_SoldUnit_MS_Schema_schemaID",
                        column: x => x.schemaID,
                        principalTable: "MS_Schema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TR_SoldUnitRequirement",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    bookNo = table.Column<string>(maxLength: 20, nullable: false),
                    developerSchemaID = table.Column<int>(nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    orPctPaid = table.Column<double>(nullable: true),
                    pctPaid = table.Column<double>(nullable: false),
                    processDate = table.Column<DateTime>(nullable: true),
                    reqDate = table.Column<DateTime>(nullable: true),
                    reqDesc = table.Column<string>(maxLength: 40, nullable: false),
                    reqNo = table.Column<byte>(nullable: false),
                    schemaID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_SoldUnitRequirement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TR_SoldUnitRequirement_MS_Developer_Schema_developerSchemaID",
                        column: x => x.developerSchemaID,
                        principalTable: "MS_Developer_Schema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TR_SoldUnitRequirement_MS_Schema_schemaID",
                        column: x => x.schemaID,
                        principalTable: "MS_Schema",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LK_CommType_schemaID",
                table: "LK_CommType",
                column: "schemaID");

            migrationBuilder.CreateIndex(
                name: "IX_LK_PointType_schemaID",
                table: "LK_PointType",
                column: "schemaID");

            migrationBuilder.CreateIndex(
                name: "IX_LK_Upline_schemaID",
                table: "LK_Upline",
                column: "schemaID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_BobotComm_schemaID",
                table: "MS_BobotComm",
                column: "schemaID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_CommPct_commTypeID",
                table: "MS_CommPct",
                column: "commTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_CommPct_schemaID",
                table: "MS_CommPct",
                column: "schemaID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_CommPct_statusMemberID",
                table: "MS_CommPct",
                column: "statusMemberID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_Developer_Schema_propertyID",
                table: "MS_Developer_Schema",
                column: "propertyID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_Developer_Schema_schemaID",
                table: "MS_Developer_Schema",
                column: "schemaID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_GroupCommPct_commTypeID",
                table: "MS_GroupCommPct",
                column: "commTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_GroupCommPct_groupSchemaID",
                table: "MS_GroupCommPct",
                column: "groupSchemaID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_GroupCommPct_statusMemberID",
                table: "MS_GroupCommPct",
                column: "statusMemberID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_GroupCommPctNonStd_commTypeID",
                table: "MS_GroupCommPctNonStd",
                column: "commTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_GroupCommPctNonStd_groupSchemaID",
                table: "MS_GroupCommPctNonStd",
                column: "groupSchemaID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_GroupCommPctNonStd_statusMemberID",
                table: "MS_GroupCommPctNonStd",
                column: "statusMemberID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_GroupSchema_schemaID",
                table: "MS_GroupSchema",
                column: "schemaID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_GroupSchemaRequirement_groupSchemaID",
                table: "MS_GroupSchemaRequirement",
                column: "groupSchemaID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_ManagementPct_developerSchemaID",
                table: "MS_ManagementPct",
                column: "developerSchemaID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_ManagementPct_schemaID",
                table: "MS_ManagementPct",
                column: "schemaID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_PointPct_pointTypeID",
                table: "MS_PointPct",
                column: "pointTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_PointPct_schemaID",
                table: "MS_PointPct",
                column: "schemaID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_PointPct_statusMemberID",
                table: "MS_PointPct",
                column: "statusMemberID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_PPhRange_schemaID",
                table: "MS_PPhRange",
                column: "schemaID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_PPhRangeIns_schemaID",
                table: "MS_PPhRangeIns",
                column: "schemaID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_Property_schemaID",
                table: "MS_Property",
                column: "schemaID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_SchemaRequirement_schemaID",
                table: "MS_SchemaRequirement",
                column: "schemaID");

            migrationBuilder.CreateIndex(
                name: "IX_MS_StatusMember_schemaID",
                table: "MS_StatusMember",
                column: "schemaID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_CommPayment_commTypeID",
                table: "TR_CommPayment",
                column: "commTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_CommPayment_developerSchemaID",
                table: "TR_CommPayment",
                column: "developerSchemaID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_CommPayment_schemaID",
                table: "TR_CommPayment",
                column: "schemaID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_CommPaymentPph_commTypeID",
                table: "TR_CommPaymentPph",
                column: "commTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_CommPaymentPph_developerSchemaID",
                table: "TR_CommPaymentPph",
                column: "developerSchemaID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_CommPaymentPph_schemaID",
                table: "TR_CommPaymentPph",
                column: "schemaID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_CommPct_commTypeID",
                table: "TR_CommPct",
                column: "commTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_CommPct_developerSchemaID",
                table: "TR_CommPct",
                column: "developerSchemaID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_CommPct_pointTypeID",
                table: "TR_CommPct",
                column: "pointTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_CommPct_pphRangeID",
                table: "TR_CommPct",
                column: "pphRangeID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_CommPct_pphRangeInsID",
                table: "TR_CommPct",
                column: "pphRangeInsID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_CommPct_statusMemberID",
                table: "TR_CommPct",
                column: "statusMemberID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_ManagementFee_developerSchemaID",
                table: "TR_ManagementFee",
                column: "developerSchemaID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_ManagementFee_schemaID",
                table: "TR_ManagementFee",
                column: "schemaID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_SoldUnit_developerSchemaID",
                table: "TR_SoldUnit",
                column: "developerSchemaID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_SoldUnit_schemaID",
                table: "TR_SoldUnit",
                column: "schemaID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_SoldUnitFlag_flagID",
                table: "TR_SoldUnitFlag",
                column: "flagID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_SoldUnitRequirement_developerSchemaID",
                table: "TR_SoldUnitRequirement",
                column: "developerSchemaID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_SoldUnitRequirement_schemaID",
                table: "TR_SoldUnitRequirement",
                column: "schemaID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LK_Upline");

            migrationBuilder.DropTable(
                name: "MS_BobotComm");

            migrationBuilder.DropTable(
                name: "MS_CommPct");

            migrationBuilder.DropTable(
                name: "MS_GroupCommPct");

            migrationBuilder.DropTable(
                name: "MS_GroupCommPctNonStd");

            migrationBuilder.DropTable(
                name: "MS_GroupSchemaRequirement");

            migrationBuilder.DropTable(
                name: "MS_ManagementPct");

            migrationBuilder.DropTable(
                name: "MS_PointPct");

            migrationBuilder.DropTable(
                name: "MS_SchemaRequirement");

            migrationBuilder.DropTable(
                name: "TR_BudgetPayment");

            migrationBuilder.DropTable(
                name: "TR_CommPayment");

            migrationBuilder.DropTable(
                name: "TR_CommPaymentPph");

            migrationBuilder.DropTable(
                name: "TR_CommPct");

            migrationBuilder.DropTable(
                name: "TR_ManagementFee");

            migrationBuilder.DropTable(
                name: "TR_SoldUnit");

            migrationBuilder.DropTable(
                name: "TR_SoldUnitFlag");

            migrationBuilder.DropTable(
                name: "TR_SoldUnitRequirement");

            migrationBuilder.DropTable(
                name: "MS_GroupSchema");

            migrationBuilder.DropTable(
                name: "LK_CommType");

            migrationBuilder.DropTable(
                name: "LK_PointType");

            migrationBuilder.DropTable(
                name: "MS_PPhRange");

            migrationBuilder.DropTable(
                name: "MS_PPhRangeIns");

            migrationBuilder.DropTable(
                name: "MS_StatusMember");

            migrationBuilder.DropTable(
                name: "MS_Flag");

            migrationBuilder.DropTable(
                name: "MS_Developer_Schema");

            migrationBuilder.DropTable(
                name: "MS_Property");

            migrationBuilder.DropTable(
                name: "MS_Schema");
        }
    }
}
