using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PropertySystemDb
{
    public partial class Add_table_lippomaster : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LK_Alloc",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    allocCode = table.Column<string>(maxLength: 3, nullable: false),
                    allocDesc = table.Column<string>(maxLength: 30, nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    isVAT = table.Column<bool>(nullable: false),
                    payForCode = table.Column<string>(maxLength: 3, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LK_Alloc", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LK_BookingTrType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    bookingTrType = table.Column<string>(maxLength: 2, nullable: false),
                    ket = table.Column<string>(maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LK_BookingTrType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LK_LetterStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    entityID = table.Column<int>(nullable: false),
                    letterStatusCode = table.Column<string>(maxLength: 3, nullable: false),
                    letterStatusDesc = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LK_LetterStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LK_PayType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    entityID = table.Column<int>(nullable: false),
                    isBooking = table.Column<bool>(nullable: false),
                    isIncome = table.Column<bool>(nullable: false),
                    isInventory = table.Column<bool>(nullable: false),
                    payTypeCode = table.Column<string>(maxLength: 3, nullable: false),
                    payTypeDesc = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LK_PayType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LK_Promotion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    promotionCode = table.Column<string>(maxLength: 3, nullable: false),
                    promotionDesc = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LK_Promotion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LK_Reason",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    entityID = table.Column<int>(nullable: false),
                    isActive = table.Column<bool>(nullable: true),
                    reasonCode = table.Column<string>(maxLength: 2, nullable: false),
                    reasonDesc = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LK_Reason", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LK_SADStatus",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    statusCode = table.Column<string>(maxLength: 1, nullable: false),
                    statusDesc = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LK_SADStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MS_Corres",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    corresCode = table.Column<string>(maxLength: 5, nullable: false),
                    corresName = table.Column<string>(maxLength: 50, nullable: false),
                    entityID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_Corres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MS_Document",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    docCode = table.Column<string>(maxLength: 5, nullable: false),
                    docName = table.Column<string>(maxLength: 50, nullable: false),
                    entityID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_Document", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MS_MappingDocument",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    documentType = table.Column<string>(maxLength: 25, nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    payMethod = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_MappingDocument", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MS_SalesEvent",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    EntityID = table.Column<int>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    eventCode = table.Column<string>(maxLength: 5, nullable: false),
                    eventName = table.Column<string>(maxLength: 50, nullable: false),
                    sortNo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_SalesEvent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MS_ShopBusiness",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    entityID = table.Column<int>(nullable: false),
                    shopBusinessCode = table.Column<string>(maxLength: 3, nullable: false),
                    shopBusinessName = table.Column<string>(maxLength: 50, nullable: false),
                    sort = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_ShopBusiness", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MS_Transform",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    entityID = table.Column<int>(nullable: false),
                    parentTransCode = table.Column<string>(maxLength: 5, nullable: false),
                    transCode = table.Column<string>(maxLength: 5, nullable: false),
                    transDesc = table.Column<string>(maxLength: 100, nullable: false),
                    transName = table.Column<string>(maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_Transform", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TR_SSPHeader",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    batchNo = table.Column<string>(maxLength: 20, nullable: false),
                    coCode = table.Column<string>(maxLength: 5, nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    generateDate = table.Column<DateTime>(nullable: false),
                    period = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_SSPHeader", x => x.Id);
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
                    SADStatusID = table.Column<int>(nullable: false),
                    TR_TransformId = table.Column<int>(nullable: true),
                    bankName = table.Column<string>(maxLength: 50, nullable: false),
                    bankNo = table.Column<string>(maxLength: 30, nullable: false),
                    bankRekeningPemilik = table.Column<string>(maxLength: 50, nullable: true),
                    bookCode = table.Column<string>(maxLength: 20, nullable: false),
                    bookDate = table.Column<DateTime>(nullable: false),
                    cancelDate = table.Column<DateTime>(nullable: true),
                    discBFCalcType = table.Column<string>(maxLength: 1, nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    eventID = table.Column<int>(nullable: false),
                    facadeID = table.Column<int>(nullable: true),
                    isPenaltyStop = table.Column<bool>(nullable: false),
                    isSK = table.Column<bool>(nullable: true),
                    isSMS = table.Column<bool>(nullable: false),
                    memberCode = table.Column<string>(maxLength: 12, nullable: false),
                    memberName = table.Column<string>(maxLength: 100, nullable: false),
                    netPriceComm = table.Column<decimal>(type: "money", nullable: false),
                    nomorRekeningPemilik = table.Column<string>(maxLength: 50, nullable: true),
                    promotionID = table.Column<int>(nullable: false),
                    psCode = table.Column<string>(maxLength: 8, nullable: false),
                    remarks = table.Column<string>(maxLength: 1500, nullable: false),
                    schemaID = table.Column<int>(nullable: false),
                    shopBusinessID = table.Column<int>(nullable: false),
                    sumberDanaCode = table.Column<string>(maxLength: 3, nullable: true),
                    termID = table.Column<int>(nullable: false),
                    termRemarks = table.Column<string>(maxLength: 200, nullable: false),
                    transID = table.Column<int>(nullable: false),
                    tujuanTransaksiCode = table.Column<string>(maxLength: 3, nullable: true),
                    unitID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_BookingHeader", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TR_BookingHeader_LK_SADStatus_SADStatusID",
                        column: x => x.SADStatusID,
                        principalTable: "LK_SADStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TR_BookingHeader_MS_Transform_TR_TransformId",
                        column: x => x.TR_TransformId,
                        principalTable: "MS_Transform",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TR_BookingHeader_MS_SalesEvent_eventID",
                        column: x => x.eventID,
                        principalTable: "MS_SalesEvent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TR_BookingHeader_MS_Facade_facadeID",
                        column: x => x.facadeID,
                        principalTable: "MS_Facade",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TR_BookingHeader_LK_Promotion_promotionID",
                        column: x => x.promotionID,
                        principalTable: "LK_Promotion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TR_BookingHeader_MS_ShopBusiness_shopBusinessID",
                        column: x => x.shopBusinessID,
                        principalTable: "MS_ShopBusiness",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TR_BookingHeader_MS_Term_termID",
                        column: x => x.termID,
                        principalTable: "MS_Term",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TR_BookingHeader_MS_Unit_unitID",
                        column: x => x.unitID,
                        principalTable: "MS_Unit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TR_BookingCancel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    bookingHeaderID = table.Column<int>(nullable: false),
                    cancelDate = table.Column<DateTime>(nullable: true),
                    entityID = table.Column<int>(nullable: false),
                    lostAmount = table.Column<decimal>(type: "money", nullable: false),
                    newBookCode = table.Column<string>(maxLength: 20, nullable: false),
                    reasonID = table.Column<int>(nullable: false),
                    refundAmount = table.Column<decimal>(type: "money", nullable: false),
                    remarks = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_BookingCancel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TR_BookingCancel_TR_BookingHeader_bookingHeaderID",
                        column: x => x.bookingHeaderID,
                        principalTable: "TR_BookingHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TR_BookingCancel_LK_Reason_reasonID",
                        column: x => x.reasonID,
                        principalTable: "LK_Reason",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TR_BookingChangeOwner",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ADDNDate = table.Column<DateTime>(nullable: true),
                    ADDNNo = table.Column<string>(maxLength: 30, nullable: false),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    bookingHeaderID = table.Column<int>(nullable: false),
                    costAmt = table.Column<decimal>(type: "money", nullable: false),
                    costPct = table.Column<double>(nullable: false),
                    docNo = table.Column<string>(maxLength: 30, nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    jumlahSetoran = table.Column<decimal>(type: "money", nullable: true),
                    newBankCode = table.Column<string>(maxLength: 5, nullable: false),
                    newFinType = table.Column<string>(maxLength: 4, nullable: false),
                    newPsCode = table.Column<string>(maxLength: 8, nullable: false),
                    nilaiJualObjekPajakBangunan = table.Column<decimal>(type: "money", nullable: true),
                    nilaiJualObjekPajakTanah = table.Column<decimal>(type: "money", nullable: true),
                    nilaiPengalihan = table.Column<decimal>(type: "money", nullable: true),
                    noObjekPajak = table.Column<string>(maxLength: 50, nullable: true),
                    noTandaPenerimaanNegara = table.Column<string>(maxLength: 50, nullable: true),
                    oldBankCode = table.Column<string>(maxLength: 5, nullable: false),
                    oldFinType = table.Column<string>(maxLength: 4, nullable: false),
                    oldPsCode = table.Column<string>(maxLength: 8, nullable: false),
                    remarks = table.Column<string>(maxLength: 100, nullable: false),
                    seqNo = table.Column<int>(nullable: false),
                    tanggalPenyetoran = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_BookingChangeOwner", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TR_BookingChangeOwner_TR_BookingHeader_bookingHeaderID",
                        column: x => x.bookingHeaderID,
                        principalTable: "TR_BookingHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TR_BookingCorres",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    bookingHeaderID = table.Column<int>(nullable: false),
                    corresDate = table.Column<DateTime>(nullable: false),
                    corresId = table.Column<int>(nullable: false),
                    corresNo = table.Column<short>(nullable: false),
                    dueDate = table.Column<DateTime>(nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    mailDate = table.Column<DateTime>(nullable: false),
                    recepient = table.Column<string>(maxLength: 100, nullable: false),
                    refNo = table.Column<string>(maxLength: 40, nullable: false),
                    remarks = table.Column<string>(maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_BookingCorres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TR_BookingCorres_TR_BookingHeader_bookingHeaderID",
                        column: x => x.bookingHeaderID,
                        principalTable: "TR_BookingHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TR_BookingCorres_MS_Corres_corresId",
                        column: x => x.corresId,
                        principalTable: "MS_Corres",
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
                    adjArea = table.Column<double>(nullable: false),
                    adjPrice = table.Column<decimal>(type: "money", nullable: false),
                    amount = table.Column<decimal>(type: "money", nullable: false),
                    amountComm = table.Column<decimal>(type: "money", nullable: false),
                    amountMKT = table.Column<decimal>(type: "money", nullable: false),
                    area = table.Column<double>(nullable: false),
                    bookNo = table.Column<int>(nullable: false),
                    bookingHeaderID = table.Column<int>(nullable: false),
                    bookingTrTypeID = table.Column<int>(nullable: false),
                    coCode = table.Column<string>(maxLength: 5, nullable: false),
                    combineCode = table.Column<string>(maxLength: 1, nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    finTypeID = table.Column<int>(nullable: false),
                    itemID = table.Column<int>(nullable: false),
                    netNetPrice = table.Column<decimal>(type: "money", nullable: false),
                    netPrice = table.Column<decimal>(type: "money", nullable: false),
                    netPriceCash = table.Column<decimal>(type: "money", nullable: false),
                    netPriceComm = table.Column<decimal>(type: "money", nullable: false),
                    netPriceMKT = table.Column<decimal>(type: "money", nullable: false),
                    pctDisc = table.Column<double>(nullable: false),
                    pctTax = table.Column<double>(nullable: false),
                    refNo = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_BookingDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TR_BookingDetail_TR_BookingHeader_bookingHeaderID",
                        column: x => x.bookingHeaderID,
                        principalTable: "TR_BookingHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TR_BookingDetail_LK_BookingTrType_bookingTrTypeID",
                        column: x => x.bookingTrTypeID,
                        principalTable: "LK_BookingTrType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TR_BookingDetail_LK_Item_itemID",
                        column: x => x.itemID,
                        principalTable: "LK_Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    bookingHeaderID = table.Column<int>(nullable: false),
                    docCode = table.Column<string>(maxLength: 5, nullable: false),
                    docDate = table.Column<DateTime>(nullable: false),
                    docNo = table.Column<string>(maxLength: 50, nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    remarks = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_BookingDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TR_BookingDocument_TR_BookingHeader_bookingHeaderID",
                        column: x => x.bookingHeaderID,
                        principalTable: "TR_BookingHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TR_BookingItemPrice",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    bookingHeaderID = table.Column<int>(nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    grossPrice = table.Column<decimal>(type: "money", nullable: false),
                    itemID = table.Column<int>(nullable: false),
                    renovCode = table.Column<string>(maxLength: 2, nullable: false),
                    termNo = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_BookingItemPrice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TR_BookingItemPrice_TR_BookingHeader_bookingHeaderID",
                        column: x => x.bookingHeaderID,
                        principalTable: "TR_BookingHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TR_BookingItemPrice_LK_Item_itemID",
                        column: x => x.itemID,
                        principalTable: "LK_Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TR_BookingSalesDisc",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    bookingHeaderID = table.Column<int>(nullable: false),
                    itemID = table.Column<int>(nullable: false),
                    pctDisc = table.Column<double>(nullable: false),
                    pctTax = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_BookingSalesDisc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TR_BookingSalesDisc_TR_BookingHeader_bookingHeaderID",
                        column: x => x.bookingHeaderID,
                        principalTable: "TR_BookingHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TR_BookingSalesDisc_LK_Item_itemID",
                        column: x => x.itemID,
                        principalTable: "LK_Item",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    accountID = table.Column<int>(nullable: false),
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
                    transNo = table.Column<string>(maxLength: 18, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_PaymentHeader", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TR_PaymentHeader_MS_Account_accountID",
                        column: x => x.accountID,
                        principalTable: "MS_Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TR_PaymentHeader_TR_BookingHeader_bookingHeaderID",
                        column: x => x.bookingHeaderID,
                        principalTable: "TR_BookingHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TR_PenaltySchedule",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    ScheduleAllocCode = table.Column<string>(maxLength: 50, nullable: true),
                    ScheduleNetAmount = table.Column<decimal>(type: "money", nullable: true),
                    ScheduleTerm = table.Column<int>(nullable: false),
                    SeqNo = table.Column<int>(nullable: false),
                    bookingHeader_ID = table.Column<int>(nullable: false),
                    createdBy = table.Column<string>(maxLength: 100, nullable: true),
                    createdOn = table.Column<DateTime>(nullable: true),
                    entityID = table.Column<int>(nullable: false),
                    penaltyAging = table.Column<int>(nullable: true),
                    penaltyAmount = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_PenaltySchedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TR_PenaltySchedule_TR_BookingHeader_bookingHeader_ID",
                        column: x => x.bookingHeader_ID,
                        principalTable: "TR_BookingHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TR_ReminderLetter",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    bankBranchCode = table.Column<string>(maxLength: 5, nullable: false),
                    bankRekNo = table.Column<string>(maxLength: 20, nullable: false),
                    bookingHeaderID = table.Column<int>(nullable: false),
                    clearDate = table.Column<DateTime>(nullable: false),
                    coCode = table.Column<string>(maxLength: 5, nullable: false),
                    dueDate = table.Column<DateTime>(nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    letterDate = table.Column<DateTime>(nullable: false),
                    letterNo = table.Column<string>(maxLength: 25, nullable: false),
                    letterStatusID = table.Column<int>(nullable: false),
                    letterType = table.Column<string>(maxLength: 3, nullable: false),
                    mailDate = table.Column<DateTime>(nullable: false),
                    outAmt = table.Column<decimal>(type: "money", nullable: false),
                    overDue = table.Column<decimal>(type: "money", nullable: false),
                    payedAmt = table.Column<decimal>(type: "money", nullable: false),
                    penAge = table.Column<int>(nullable: false),
                    penAmt = table.Column<decimal>(type: "money", nullable: false),
                    printDate = table.Column<DateTime>(nullable: false),
                    receiveDate = table.Column<DateTime>(nullable: false),
                    remarks = table.Column<string>(maxLength: 150, nullable: false),
                    sadOfficer1 = table.Column<string>(maxLength: 50, nullable: false),
                    sadOfficer2 = table.Column<string>(maxLength: 50, nullable: false),
                    sadPosition1 = table.Column<string>(maxLength: 50, nullable: false),
                    sadPosition2 = table.Column<string>(maxLength: 50, nullable: false),
                    totAmt = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_ReminderLetter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TR_ReminderLetter_TR_BookingHeader_bookingHeaderID",
                        column: x => x.bookingHeaderID,
                        principalTable: "TR_BookingHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TR_ReminderLetter_LK_LetterStatus_letterStatusID",
                        column: x => x.letterStatusID,
                        principalTable: "LK_LetterStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TR_BookingDetailAddDisc",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    addDiscDesc = table.Column<string>(maxLength: 500, nullable: false),
                    addDiscNo = table.Column<short>(nullable: false),
                    bookingDetailID = table.Column<int>(nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    pctAddDisc = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_BookingDetailAddDisc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TR_BookingDetailAddDisc_TR_BookingDetail_bookingDetailID",
                        column: x => x.bookingDetailID,
                        principalTable: "TR_BookingDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TR_BookingDetailAdjustment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    adjDate = table.Column<DateTime>(nullable: false),
                    adjNo = table.Column<int>(nullable: false),
                    area = table.Column<DateTime>(nullable: false),
                    bookingDetailID = table.Column<int>(nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    remarks = table.Column<string>(maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_BookingDetailAdjustment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TR_BookingDetailAdjustment_TR_BookingDetail_bookingDetailID",
                        column: x => x.bookingDetailID,
                        principalTable: "TR_BookingDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TR_BookingDetailDP",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    DPAmount = table.Column<decimal>(type: "money", nullable: false),
                    DPPct = table.Column<double>(nullable: false),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    bookingDetailID = table.Column<int>(nullable: false),
                    daysDue = table.Column<short>(nullable: false),
                    dpNo = table.Column<byte>(nullable: false),
                    entityID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_BookingDetailDP", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TR_BookingDetailDP_TR_BookingDetail_bookingDetailID",
                        column: x => x.bookingDetailID,
                        principalTable: "TR_BookingDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TR_BookingDetailSchedule",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    allocID = table.Column<int>(nullable: false),
                    bookingDetailID = table.Column<int>(nullable: false),
                    dueDate = table.Column<DateTime>(nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    netAmt = table.Column<decimal>(type: "money", nullable: false),
                    netOut = table.Column<decimal>(type: "money", nullable: false),
                    remarks = table.Column<string>(maxLength: 200, nullable: false),
                    schedNo = table.Column<short>(nullable: false),
                    vatAmt = table.Column<decimal>(type: "money", nullable: false),
                    vatOut = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_BookingDetailSchedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TR_BookingDetailSchedule_LK_Alloc_allocID",
                        column: x => x.allocID,
                        principalTable: "LK_Alloc",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TR_BookingDetailSchedule_TR_BookingDetail_bookingDetailID",
                        column: x => x.bookingDetailID,
                        principalTable: "TR_BookingDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TR_BookingDetailScheduleOtorisasi",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    allocID = table.Column<int>(nullable: false),
                    bookingDetailID = table.Column<int>(nullable: false),
                    dueDate = table.Column<DateTime>(nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    netAmt = table.Column<decimal>(type: "money", nullable: false),
                    schedNo = table.Column<short>(nullable: false),
                    vatAmt = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_BookingDetailScheduleOtorisasi", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TR_BookingDetailScheduleOtorisasi_LK_Alloc_allocID",
                        column: x => x.allocID,
                        principalTable: "LK_Alloc",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TR_BookingDetailScheduleOtorisasi_TR_BookingDetail_bookingDetailID",
                        column: x => x.bookingDetailID,
                        principalTable: "TR_BookingDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TR_CommAddDisc",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    TR_BookingHeaderId = table.Column<int>(nullable: true),
                    addDisc = table.Column<double>(nullable: false),
                    addDiscDesc = table.Column<string>(maxLength: 500, nullable: true),
                    addDiscNo = table.Column<byte>(nullable: false),
                    bookNo = table.Column<int>(nullable: false),
                    bookingDetailID = table.Column<int>(nullable: false),
                    coCode = table.Column<string>(maxLength: 5, nullable: false),
                    entityID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_CommAddDisc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TR_CommAddDisc_TR_BookingHeader_TR_BookingHeaderId",
                        column: x => x.TR_BookingHeaderId,
                        principalTable: "TR_BookingHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TR_CommAddDisc_TR_BookingDetail_bookingDetailID",
                        column: x => x.bookingDetailID,
                        principalTable: "TR_BookingDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TR_SSPDetail",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    SSPHeaderID = table.Column<int>(nullable: false),
                    accountID = table.Column<int>(nullable: false),
                    bookingDetailID = table.Column<int>(nullable: false),
                    buildArea = table.Column<double>(nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    landArea = table.Column<double>(nullable: false),
                    netAmt = table.Column<decimal>(type: "money", nullable: false),
                    pphAmt = table.Column<decimal>(type: "money", nullable: false),
                    psCity = table.Column<string>(maxLength: 50, nullable: false),
                    psCode = table.Column<string>(maxLength: 8, nullable: false),
                    psName = table.Column<string>(maxLength: 100, nullable: false),
                    unitDesc = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_SSPDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TR_SSPDetail_TR_SSPHeader_SSPHeaderID",
                        column: x => x.SSPHeaderID,
                        principalTable: "TR_SSPHeader",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TR_SSPDetail_MS_Account_accountID",
                        column: x => x.accountID,
                        principalTable: "MS_Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TR_SSPDetail_TR_BookingDetail_bookingDetailID",
                        column: x => x.bookingDetailID,
                        principalTable: "TR_BookingDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    MS_AccountId = table.Column<int>(nullable: true),
                    bankName = table.Column<string>(maxLength: 30, nullable: false),
                    chequeNo = table.Column<string>(maxLength: 30, nullable: false),
                    dueDate = table.Column<DateTime>(nullable: false),
                    entityID = table.Column<int>(nullable: false),
                    ket = table.Column<string>(maxLength: 300, nullable: false),
                    othersTypeCode = table.Column<string>(maxLength: 3, nullable: false),
                    payNo = table.Column<int>(nullable: false),
                    payTypeID = table.Column<int>(nullable: false),
                    paymentHeaderID = table.Column<int>(nullable: false),
                    status = table.Column<string>(maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_PaymentDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TR_PaymentDetail_MS_Account_MS_AccountId",
                        column: x => x.MS_AccountId,
                        principalTable: "MS_Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TR_PaymentDetail_LK_PayType_payTypeID",
                        column: x => x.payTypeID,
                        principalTable: "LK_PayType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TR_PaymentDetail_TR_PaymentHeader_paymentHeaderID",
                        column: x => x.paymentHeaderID,
                        principalTable: "TR_PaymentHeader",
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
                    MS_AccountId = table.Column<int>(nullable: true),
                    entityID = table.Column<int>(nullable: false),
                    netAmt = table.Column<decimal>(type: "money", nullable: false),
                    paymentDetailID = table.Column<int>(nullable: false),
                    schedNo = table.Column<short>(nullable: false),
                    vatAmt = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_PaymentDetailAlloc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TR_PaymentDetailAlloc_MS_Account_MS_AccountId",
                        column: x => x.MS_AccountId,
                        principalTable: "MS_Account",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TR_PaymentDetailAlloc_TR_PaymentDetail_paymentDetailID",
                        column: x => x.paymentDetailID,
                        principalTable: "TR_PaymentDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "allocCodeUnique",
                table: "LK_Alloc",
                column: "allocCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "letterStatusCodeUnique",
                table: "LK_LetterStatus",
                column: "letterStatusCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "payTypeCodeUnique",
                table: "LK_PayType",
                column: "payTypeCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "promotionCodeUnique",
                table: "LK_Promotion",
                column: "promotionCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "reasonCodeUnique",
                table: "LK_Reason",
                column: "reasonCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "statusCodeUnique",
                table: "LK_SADStatus",
                column: "statusCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "docCodeCodeUnique",
                table: "MS_Document",
                column: "docCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "eventCodeUnique",
                table: "MS_SalesEvent",
                column: "eventCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "shopBusinessCodeUnique",
                table: "MS_ShopBusiness",
                column: "shopBusinessCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "transCodeUnique",
                table: "MS_Transform",
                column: "transCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TR_BookingCancel_bookingHeaderID",
                table: "TR_BookingCancel",
                column: "bookingHeaderID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_BookingCancel_reasonID",
                table: "TR_BookingCancel",
                column: "reasonID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_BookingChangeOwner_bookingHeaderID",
                table: "TR_BookingChangeOwner",
                column: "bookingHeaderID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_BookingCorres_bookingHeaderID",
                table: "TR_BookingCorres",
                column: "bookingHeaderID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_BookingCorres_corresId",
                table: "TR_BookingCorres",
                column: "corresId");

            migrationBuilder.CreateIndex(
                name: "IX_TR_BookingDetail_bookingHeaderID",
                table: "TR_BookingDetail",
                column: "bookingHeaderID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_BookingDetail_bookingTrTypeID",
                table: "TR_BookingDetail",
                column: "bookingTrTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_BookingDetail_itemID",
                table: "TR_BookingDetail",
                column: "itemID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_BookingDetailAddDisc_bookingDetailID",
                table: "TR_BookingDetailAddDisc",
                column: "bookingDetailID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_BookingDetailAdjustment_bookingDetailID",
                table: "TR_BookingDetailAdjustment",
                column: "bookingDetailID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_BookingDetailDP_bookingDetailID",
                table: "TR_BookingDetailDP",
                column: "bookingDetailID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_BookingDetailSchedule_allocID",
                table: "TR_BookingDetailSchedule",
                column: "allocID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_BookingDetailSchedule_bookingDetailID",
                table: "TR_BookingDetailSchedule",
                column: "bookingDetailID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_BookingDetailScheduleOtorisasi_allocID",
                table: "TR_BookingDetailScheduleOtorisasi",
                column: "allocID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_BookingDetailScheduleOtorisasi_bookingDetailID",
                table: "TR_BookingDetailScheduleOtorisasi",
                column: "bookingDetailID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_BookingDocument_bookingHeaderID",
                table: "TR_BookingDocument",
                column: "bookingHeaderID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_BookingHeader_SADStatusID",
                table: "TR_BookingHeader",
                column: "SADStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_BookingHeader_TR_TransformId",
                table: "TR_BookingHeader",
                column: "TR_TransformId");

            migrationBuilder.CreateIndex(
                name: "bookCodeUnique",
                table: "TR_BookingHeader",
                column: "bookCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TR_BookingHeader_eventID",
                table: "TR_BookingHeader",
                column: "eventID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_BookingHeader_facadeID",
                table: "TR_BookingHeader",
                column: "facadeID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_BookingHeader_promotionID",
                table: "TR_BookingHeader",
                column: "promotionID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_BookingHeader_shopBusinessID",
                table: "TR_BookingHeader",
                column: "shopBusinessID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_BookingHeader_termID",
                table: "TR_BookingHeader",
                column: "termID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_BookingHeader_unitID",
                table: "TR_BookingHeader",
                column: "unitID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_BookingItemPrice_bookingHeaderID",
                table: "TR_BookingItemPrice",
                column: "bookingHeaderID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_BookingItemPrice_itemID",
                table: "TR_BookingItemPrice",
                column: "itemID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_BookingSalesDisc_bookingHeaderID",
                table: "TR_BookingSalesDisc",
                column: "bookingHeaderID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_BookingSalesDisc_itemID",
                table: "TR_BookingSalesDisc",
                column: "itemID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_CommAddDisc_TR_BookingHeaderId",
                table: "TR_CommAddDisc",
                column: "TR_BookingHeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_TR_CommAddDisc_bookingDetailID",
                table: "TR_CommAddDisc",
                column: "bookingDetailID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_PaymentDetail_MS_AccountId",
                table: "TR_PaymentDetail",
                column: "MS_AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_TR_PaymentDetail_payTypeID",
                table: "TR_PaymentDetail",
                column: "payTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_PaymentDetail_paymentHeaderID",
                table: "TR_PaymentDetail",
                column: "paymentHeaderID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_PaymentDetailAlloc_MS_AccountId",
                table: "TR_PaymentDetailAlloc",
                column: "MS_AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_TR_PaymentDetailAlloc_paymentDetailID",
                table: "TR_PaymentDetailAlloc",
                column: "paymentDetailID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_PaymentHeader_accountID",
                table: "TR_PaymentHeader",
                column: "accountID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_PaymentHeader_bookingHeaderID",
                table: "TR_PaymentHeader",
                column: "bookingHeaderID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_PenaltySchedule_bookingHeader_ID",
                table: "TR_PenaltySchedule",
                column: "bookingHeader_ID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_ReminderLetter_bookingHeaderID",
                table: "TR_ReminderLetter",
                column: "bookingHeaderID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_ReminderLetter_letterStatusID",
                table: "TR_ReminderLetter",
                column: "letterStatusID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_SSPDetail_SSPHeaderID",
                table: "TR_SSPDetail",
                column: "SSPHeaderID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_SSPDetail_accountID",
                table: "TR_SSPDetail",
                column: "accountID");

            migrationBuilder.CreateIndex(
                name: "IX_TR_SSPDetail_bookingDetailID",
                table: "TR_SSPDetail",
                column: "bookingDetailID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MS_Document");

            migrationBuilder.DropTable(
                name: "MS_MappingDocument");

            migrationBuilder.DropTable(
                name: "TR_BookingCancel");

            migrationBuilder.DropTable(
                name: "TR_BookingChangeOwner");

            migrationBuilder.DropTable(
                name: "TR_BookingCorres");

            migrationBuilder.DropTable(
                name: "TR_BookingDetailAddDisc");

            migrationBuilder.DropTable(
                name: "TR_BookingDetailAdjustment");

            migrationBuilder.DropTable(
                name: "TR_BookingDetailDP");

            migrationBuilder.DropTable(
                name: "TR_BookingDetailSchedule");

            migrationBuilder.DropTable(
                name: "TR_BookingDetailScheduleOtorisasi");

            migrationBuilder.DropTable(
                name: "TR_BookingDocument");

            migrationBuilder.DropTable(
                name: "TR_BookingItemPrice");

            migrationBuilder.DropTable(
                name: "TR_BookingSalesDisc");

            migrationBuilder.DropTable(
                name: "TR_CommAddDisc");

            migrationBuilder.DropTable(
                name: "TR_PaymentDetailAlloc");

            migrationBuilder.DropTable(
                name: "TR_PenaltySchedule");

            migrationBuilder.DropTable(
                name: "TR_ReminderLetter");

            migrationBuilder.DropTable(
                name: "TR_SSPDetail");

            migrationBuilder.DropTable(
                name: "LK_Reason");

            migrationBuilder.DropTable(
                name: "MS_Corres");

            migrationBuilder.DropTable(
                name: "LK_Alloc");

            migrationBuilder.DropTable(
                name: "TR_PaymentDetail");

            migrationBuilder.DropTable(
                name: "LK_LetterStatus");

            migrationBuilder.DropTable(
                name: "TR_SSPHeader");

            migrationBuilder.DropTable(
                name: "TR_BookingDetail");

            migrationBuilder.DropTable(
                name: "LK_PayType");

            migrationBuilder.DropTable(
                name: "TR_PaymentHeader");

            migrationBuilder.DropTable(
                name: "LK_BookingTrType");

            migrationBuilder.DropTable(
                name: "TR_BookingHeader");

            migrationBuilder.DropTable(
                name: "LK_SADStatus");

            migrationBuilder.DropTable(
                name: "MS_Transform");

            migrationBuilder.DropTable(
                name: "MS_SalesEvent");

            migrationBuilder.DropTable(
                name: "LK_Promotion");

            migrationBuilder.DropTable(
                name: "MS_ShopBusiness");
        }
    }
}
