using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace VDI.Demo.Migrations.PersonalsNewDb
{
    public partial class initialpersonalsDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LK_AddrType",
                columns: table => new
                {
                    addrType = table.Column<string>(maxLength: 1, nullable: false),
                    inputTime = table.Column<DateTime>(nullable: false),
                    inputUN = table.Column<long>(nullable: true),
                    modifTime = table.Column<DateTime>(nullable: true),
                    modifUN = table.Column<long>(nullable: true),
                    addrTypeName = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LK_AddrType", x => x.addrType);
                });

            migrationBuilder.CreateTable(
                name: "LK_BankType",
                columns: table => new
                {
                    bankType = table.Column<string>(maxLength: 1, nullable: false),
                    inputTime = table.Column<DateTime>(nullable: false),
                    inputUN = table.Column<long>(nullable: true),
                    modifTime = table.Column<DateTime>(nullable: true),
                    modifUN = table.Column<long>(nullable: true),
                    bankTypeName = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LK_BankType", x => x.bankType);
                });

            migrationBuilder.CreateTable(
                name: "LK_Blood",
                columns: table => new
                {
                    bloodCode = table.Column<string>(maxLength: 1, nullable: false),
                    inputTime = table.Column<DateTime>(nullable: false),
                    inputUN = table.Column<long>(nullable: true),
                    modifTime = table.Column<DateTime>(nullable: true),
                    modifUN = table.Column<long>(nullable: true),
                    bloodName = table.Column<string>(maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LK_Blood", x => x.bloodCode);
                });

            migrationBuilder.CreateTable(
                name: "LK_Country",
                columns: table => new
                {
                    country = table.Column<string>(maxLength: 100, nullable: false),
                    inputTime = table.Column<DateTime>(nullable: false),
                    inputUN = table.Column<long>(nullable: true),
                    modifTime = table.Column<DateTime>(nullable: true),
                    modifUN = table.Column<long>(nullable: true),
                    ppatkCountryCode = table.Column<string>(maxLength: 3, nullable: true),
                    urut = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LK_Country", x => x.country);
                });

            migrationBuilder.CreateTable(
                name: "LK_FamilyStatus",
                columns: table => new
                {
                    famStatus = table.Column<string>(maxLength: 1, nullable: false),
                    inputTime = table.Column<DateTime>(nullable: false),
                    inputUN = table.Column<long>(nullable: true),
                    modifTime = table.Column<DateTime>(nullable: true),
                    modifUN = table.Column<long>(nullable: true),
                    famStatusName = table.Column<string>(maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LK_FamilyStatus", x => x.famStatus);
                });

            migrationBuilder.CreateTable(
                name: "LK_Grade",
                columns: table => new
                {
                    gradeCode = table.Column<string>(maxLength: 1, nullable: false),
                    inputTime = table.Column<DateTime>(nullable: false),
                    inputUN = table.Column<long>(nullable: true),
                    modifTime = table.Column<DateTime>(nullable: true),
                    modifUN = table.Column<long>(nullable: true),
                    gradeName = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LK_Grade", x => x.gradeCode);
                });

            migrationBuilder.CreateTable(
                name: "LK_IDType",
                columns: table => new
                {
                    idType = table.Column<string>(maxLength: 1, nullable: false),
                    inputTime = table.Column<DateTime>(nullable: false),
                    inputUN = table.Column<long>(nullable: true),
                    modifTime = table.Column<DateTime>(nullable: true),
                    modifUN = table.Column<long>(nullable: true),
                    idTypeName = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LK_IDType", x => x.idType);
                });

            migrationBuilder.CreateTable(
                name: "LK_KeyPeople",
                columns: table => new
                {
                    keyPeopleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    inputTime = table.Column<DateTime>(nullable: false),
                    inputUN = table.Column<long>(nullable: true),
                    modifTime = table.Column<DateTime>(nullable: true),
                    modifUN = table.Column<long>(nullable: true),
                    keyPeopleDesc = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LK_KeyPeople", x => x.keyPeopleId);
                });

            migrationBuilder.CreateTable(
                name: "LK_MarStatus",
                columns: table => new
                {
                    marStatus = table.Column<string>(maxLength: 1, nullable: false),
                    inputTime = table.Column<DateTime>(nullable: false),
                    inputUN = table.Column<long>(nullable: true),
                    modifTime = table.Column<DateTime>(nullable: true),
                    modifUN = table.Column<long>(nullable: true),
                    marStatusName = table.Column<string>(maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LK_MarStatus", x => x.marStatus);
                });

            migrationBuilder.CreateTable(
                name: "LK_PhonePrefix",
                columns: table => new
                {
                    prefix = table.Column<string>(maxLength: 10, nullable: false),
                    phoneType = table.Column<string>(maxLength: 1, nullable: false),
                    inputTime = table.Column<DateTime>(nullable: false),
                    inputUN = table.Column<long>(nullable: true),
                    modifTime = table.Column<DateTime>(nullable: true),
                    modifUN = table.Column<long>(nullable: true),
                    description = table.Column<string>(maxLength: 50, nullable: false),
                    length = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LK_PhonePrefix", x => new { x.prefix, x.phoneType });
                    table.UniqueConstraint("AK_LK_PhonePrefix_phoneType_prefix", x => new { x.phoneType, x.prefix });
                });

            migrationBuilder.CreateTable(
                name: "LK_PhoneType",
                columns: table => new
                {
                    phoneType = table.Column<string>(maxLength: 1, nullable: false),
                    inputTime = table.Column<DateTime>(nullable: false),
                    inputUN = table.Column<long>(nullable: true),
                    modifTime = table.Column<DateTime>(nullable: true),
                    modifUN = table.Column<long>(nullable: true),
                    phoneTypeName = table.Column<string>(maxLength: 30, nullable: false),
                    phoneTypeNameProInt = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LK_PhoneType", x => x.phoneType);
                });

            migrationBuilder.CreateTable(
                name: "LK_Religion",
                columns: table => new
                {
                    relCode = table.Column<string>(maxLength: 1, nullable: false),
                    inputTime = table.Column<DateTime>(nullable: false),
                    inputUN = table.Column<long>(nullable: true),
                    modifTime = table.Column<DateTime>(nullable: true),
                    modifUN = table.Column<long>(nullable: true),
                    relName = table.Column<string>(maxLength: 20, nullable: false),
                    relNameProInt = table.Column<string>(maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LK_Religion", x => x.relCode);
                });

            migrationBuilder.CreateTable(
                name: "LK_Spec",
                columns: table => new
                {
                    specCode = table.Column<string>(maxLength: 1, nullable: false),
                    inputTime = table.Column<DateTime>(nullable: false),
                    inputUN = table.Column<long>(nullable: true),
                    modifTime = table.Column<DateTime>(nullable: true),
                    modifUN = table.Column<long>(nullable: true),
                    specName = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LK_Spec", x => x.specCode);
                });

            migrationBuilder.CreateTable(
                name: "MS_Bank",
                columns: table => new
                {
                    bankCode = table.Column<string>(maxLength: 5, nullable: false),
                    inputTime = table.Column<DateTime>(nullable: false),
                    inputUN = table.Column<long>(nullable: true),
                    modifTime = table.Column<DateTime>(nullable: true),
                    modifUN = table.Column<long>(nullable: true),
                    bankName = table.Column<string>(maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_Bank", x => x.bankCode);
                });

            migrationBuilder.CreateTable(
                name: "MS_County",
                columns: table => new
                {
                    countyCode = table.Column<string>(maxLength: 5, nullable: false),
                    countyDesc = table.Column<string>(maxLength: 50, nullable: false),
                    inputTime = table.Column<DateTime>(nullable: false),
                    inputUN = table.Column<long>(nullable: true),
                    modifTime = table.Column<DateTime>(nullable: true),
                    modifUN = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_County", x => new { x.countyCode, x.countyDesc });
                });

            migrationBuilder.CreateTable(
                name: "MS_Document",
                columns: table => new
                {
                    documentType = table.Column<string>(maxLength: 25, nullable: false),
                    inputTime = table.Column<DateTime>(nullable: false),
                    inputUN = table.Column<long>(nullable: true),
                    modifTime = table.Column<DateTime>(nullable: true),
                    modifUN = table.Column<long>(nullable: true),
                    documentName = table.Column<string>(maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_Document", x => x.documentType);
                });

            migrationBuilder.CreateTable(
                name: "MS_FranchiseGroup",
                columns: table => new
                {
                    franchiseID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    inputTime = table.Column<DateTime>(nullable: false),
                    inputUN = table.Column<long>(nullable: true),
                    modifTime = table.Column<DateTime>(nullable: true),
                    modifUN = table.Column<long>(nullable: true),
                    franchiseGroupName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_FranchiseGroup", x => x.franchiseID);
                });

            migrationBuilder.CreateTable(
                name: "MS_Group",
                columns: table => new
                {
                    entityCode = table.Column<string>(maxLength: 1, nullable: false),
                    groupCode = table.Column<string>(maxLength: 5, nullable: false),
                    inputTime = table.Column<DateTime>(nullable: false),
                    inputUN = table.Column<long>(nullable: true),
                    modifTime = table.Column<DateTime>(nullable: true),
                    modifUN = table.Column<long>(nullable: true),
                    groupName = table.Column<string>(maxLength: 50, nullable: false),
                    groupParentCode = table.Column<string>(maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_Group", x => new { x.entityCode, x.groupCode });
                });

            migrationBuilder.CreateTable(
                name: "MS_JobTitle",
                columns: table => new
                {
                    jobTitleID = table.Column<string>(maxLength: 3, nullable: false),
                    inputTime = table.Column<DateTime>(nullable: false),
                    inputUN = table.Column<long>(nullable: true),
                    modifTime = table.Column<DateTime>(nullable: true),
                    modifUN = table.Column<long>(nullable: true),
                    jobTitleName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_JobTitle", x => x.jobTitleID);
                });

            migrationBuilder.CreateTable(
                name: "MS_Nation",
                columns: table => new
                {
                    entityCode = table.Column<string>(maxLength: 1, nullable: false),
                    nationID = table.Column<string>(maxLength: 3, nullable: false),
                    inputTime = table.Column<DateTime>(nullable: false),
                    inputUN = table.Column<long>(nullable: true),
                    modifTime = table.Column<DateTime>(nullable: true),
                    modifUN = table.Column<long>(nullable: true),
                    nationality = table.Column<string>(maxLength: 50, nullable: false),
                    ppatkNationCode = table.Column<string>(maxLength: 3, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_Nation", x => new { x.entityCode, x.nationID });
                });

            migrationBuilder.CreateTable(
                name: "MS_Occupation",
                columns: table => new
                {
                    entityCode = table.Column<string>(maxLength: 1, nullable: false),
                    occID = table.Column<string>(maxLength: 3, nullable: false),
                    inputTime = table.Column<DateTime>(nullable: false),
                    inputUN = table.Column<long>(nullable: true),
                    modifTime = table.Column<DateTime>(nullable: true),
                    modifUN = table.Column<long>(nullable: true),
                    occDesc = table.Column<string>(maxLength: 50, nullable: false),
                    ppatkOccCode = table.Column<string>(maxLength: 3, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_Occupation", x => new { x.entityCode, x.occID });
                });

            migrationBuilder.CreateTable(
                name: "MS_PriorityPass",
                columns: table => new
                {
                    PPNo = table.Column<string>(maxLength: 10, nullable: false),
                    inputTime = table.Column<DateTime>(nullable: false),
                    inputUN = table.Column<long>(nullable: true),
                    modifTime = table.Column<DateTime>(nullable: true),
                    modifUN = table.Column<long>(nullable: true),
                    custName = table.Column<string>(maxLength: 50, nullable: false),
                    dealCloserID = table.Column<string>(maxLength: 50, nullable: false),
                    dealCloserName = table.Column<string>(maxLength: 100, nullable: false),
                    dealCloserPhone = table.Column<string>(maxLength: 100, nullable: false),
                    queueNo = table.Column<int>(nullable: false),
                    scmName = table.Column<string>(maxLength: 100, nullable: false),
                    statusCode = table.Column<string>(maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_PriorityPass", x => x.PPNo);
                });

            migrationBuilder.CreateTable(
                name: "MS_Province",
                columns: table => new
                {
                    provinceCode = table.Column<string>(maxLength: 5, nullable: false),
                    provinceName = table.Column<string>(maxLength: 50, nullable: false),
                    inputTime = table.Column<DateTime>(nullable: false),
                    inputUN = table.Column<long>(nullable: true),
                    modifTime = table.Column<DateTime>(nullable: true),
                    modifUN = table.Column<long>(nullable: true),
                    ppatkProvinceCode = table.Column<string>(maxLength: 3, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_Province", x => new { x.provinceCode, x.provinceName });
                });

            migrationBuilder.CreateTable(
                name: "MS_Regency",
                columns: table => new
                {
                    regencyCode = table.Column<string>(maxLength: 5, nullable: false),
                    regencyName = table.Column<string>(maxLength: 50, nullable: false),
                    inputTime = table.Column<DateTime>(nullable: false),
                    inputUN = table.Column<long>(nullable: true),
                    modifTime = table.Column<DateTime>(nullable: true),
                    modifUN = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_Regency", x => new { x.regencyCode, x.regencyName });
                });

            migrationBuilder.CreateTable(
                name: "MS_RelationResident",
                columns: table => new
                {
                    kkCode = table.Column<string>(maxLength: 10, nullable: false),
                    RefID = table.Column<string>(maxLength: 10, nullable: false),
                    inputTime = table.Column<DateTime>(nullable: false),
                    inputUN = table.Column<long>(nullable: true),
                    modifTime = table.Column<DateTime>(nullable: true),
                    modifUN = table.Column<long>(nullable: true),
                    psCode = table.Column<string>(maxLength: 10, nullable: true),
                    remark = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_RelationResident", x => new { x.kkCode, x.RefID });
                });

            migrationBuilder.CreateTable(
                name: "MS_Village",
                columns: table => new
                {
                    villageCode = table.Column<string>(maxLength: 5, nullable: false),
                    villageName = table.Column<string>(maxLength: 50, nullable: false),
                    cityCode = table.Column<string>(maxLength: 5, nullable: false),
                    countyCode = table.Column<string>(maxLength: 5, nullable: false),
                    regencyCode = table.Column<string>(maxLength: 5, nullable: false),
                    provinceCode = table.Column<string>(maxLength: 5, nullable: false),
                    inputTime = table.Column<DateTime>(nullable: false),
                    inputUN = table.Column<long>(nullable: true),
                    modifTime = table.Column<DateTime>(nullable: true),
                    modifUN = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_Village", x => new { x.villageCode, x.villageName, x.cityCode, x.countyCode, x.regencyCode, x.provinceCode });
                    table.UniqueConstraint("AK_MS_Village_cityCode_countyCode_provinceCode_regencyCode_villageCode_villageName", x => new { x.cityCode, x.countyCode, x.provinceCode, x.regencyCode, x.villageCode, x.villageName });
                });

            migrationBuilder.CreateTable(
                name: "PERSONALS",
                columns: table => new
                {
                    entityCode = table.Column<string>(maxLength: 1, nullable: false),
                    psCode = table.Column<string>(maxLength: 8, nullable: false),
                    inputTime = table.Column<DateTime>(nullable: false),
                    inputUN = table.Column<long>(nullable: true),
                    FPTransCode = table.Column<string>(maxLength: 2, nullable: false),
                    modifTime = table.Column<DateTime>(nullable: true),
                    modifUN = table.Column<long>(nullable: true),
                    NPWP = table.Column<string>(maxLength: 30, nullable: false),
                    UploadContentImage = table.Column<string>(nullable: true),
                    UploadContentType = table.Column<string>(maxLength: 50, nullable: true),
                    birthDate = table.Column<DateTime>(nullable: true),
                    birthPlace = table.Column<string>(maxLength: 30, nullable: false),
                    bloodCode = table.Column<string>(maxLength: 1, nullable: false),
                    familyStatus = table.Column<string>(maxLength: 1, nullable: false),
                    grade = table.Column<string>(maxLength: 1, nullable: false),
                    isActive = table.Column<bool>(nullable: false),
                    isInstitute = table.Column<bool>(nullable: false),
                    mailGroup = table.Column<string>(maxLength: 10, nullable: false),
                    marCode = table.Column<string>(maxLength: 1, nullable: false),
                    name = table.Column<string>(maxLength: 100, nullable: false),
                    nationID = table.Column<string>(maxLength: 3, nullable: false),
                    occID = table.Column<string>(maxLength: 3, nullable: true),
                    parentPSCode = table.Column<string>(maxLength: 8, nullable: false),
                    relCode = table.Column<string>(maxLength: 1, nullable: false),
                    remarks = table.Column<string>(maxLength: 500, nullable: true),
                    sex = table.Column<string>(maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PERSONALS", x => new { x.entityCode, x.psCode });
                });

            migrationBuilder.CreateTable(
                name: "SYS_Counter",
                columns: table => new
                {
                    entityCode = table.Column<string>(maxLength: 1, nullable: false),
                    psCode = table.Column<string>(maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYS_Counter", x => new { x.entityCode, x.psCode });
                });

            migrationBuilder.CreateTable(
                name: "SYS_CounterMember",
                columns: table => new
                {
                    entityCode = table.Column<string>(maxLength: 1, nullable: false),
                    scmCode = table.Column<string>(maxLength: 3, nullable: false),
                    memberDigit = table.Column<string>(maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYS_CounterMember", x => new { x.entityCode, x.scmCode });
                    table.UniqueConstraint("AK_SYS_CounterMember_entityCode_memberDigit_scmCode", x => new { x.entityCode, x.memberDigit, x.scmCode });
                });

            migrationBuilder.CreateTable(
                name: "SYS_RolesAddr",
                columns: table => new
                {
                    entityCode = table.Column<string>(maxLength: 1, nullable: false),
                    rolesname = table.Column<string>(maxLength: 50, nullable: false),
                    addrType = table.Column<string>(maxLength: 1, nullable: false),
                    inputTime = table.Column<DateTime>(nullable: false),
                    inputUN = table.Column<long>(nullable: true),
                    modifTime = table.Column<DateTime>(nullable: true),
                    modifUN = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYS_RolesAddr", x => new { x.entityCode, x.rolesname, x.addrType });
                    table.UniqueConstraint("AK_SYS_RolesAddr_addrType_entityCode_rolesname", x => new { x.addrType, x.entityCode, x.rolesname });
                });

            migrationBuilder.CreateTable(
                name: "SYS_UserGroup",
                columns: table => new
                {
                    entityCode = table.Column<string>(maxLength: 1, nullable: false),
                    userName = table.Column<string>(maxLength: 50, nullable: false),
                    groupCode = table.Column<string>(maxLength: 5, nullable: false),
                    inputTime = table.Column<DateTime>(nullable: false),
                    inputUN = table.Column<long>(nullable: true),
                    modifTime = table.Column<DateTime>(nullable: true),
                    modifUN = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYS_UserGroup", x => new { x.entityCode, x.userName, x.groupCode });
                    table.UniqueConstraint("AK_SYS_UserGroup_entityCode_groupCode_userName", x => new { x.entityCode, x.groupCode, x.userName });
                });

            migrationBuilder.CreateTable(
                name: "TR_Address",
                columns: table => new
                {
                    entityCode = table.Column<string>(maxLength: 1, nullable: false),
                    psCode = table.Column<string>(maxLength: 8, nullable: false),
                    refID = table.Column<int>(nullable: false),
                    addrType = table.Column<string>(maxLength: 1, nullable: false),
                    inputTime = table.Column<DateTime>(nullable: false),
                    inputUN = table.Column<long>(nullable: true),
                    Kecamatan = table.Column<string>(maxLength: 250, nullable: true),
                    Kelurahan = table.Column<string>(maxLength: 250, nullable: true),
                    modifTime = table.Column<DateTime>(nullable: true),
                    modifUN = table.Column<long>(nullable: true),
                    address = table.Column<string>(maxLength: 500, nullable: false),
                    city = table.Column<string>(maxLength: 50, nullable: false),
                    country = table.Column<string>(maxLength: 50, nullable: false),
                    postCode = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_Address", x => new { x.entityCode, x.psCode, x.refID, x.addrType });
                    table.UniqueConstraint("AK_TR_Address_addrType_entityCode_psCode_refID", x => new { x.addrType, x.entityCode, x.psCode, x.refID });
                });

            migrationBuilder.CreateTable(
                name: "TR_BankAccount",
                columns: table => new
                {
                    entityCode = table.Column<string>(maxLength: 1, nullable: false),
                    psCode = table.Column<string>(maxLength: 8, nullable: false),
                    refID = table.Column<int>(nullable: false),
                    BankCode = table.Column<string>(maxLength: 5, nullable: false),
                    AccountName = table.Column<string>(maxLength: 200, nullable: true),
                    AccountNo = table.Column<string>(maxLength: 50, nullable: false),
                    inputTime = table.Column<DateTime>(nullable: false),
                    inputUN = table.Column<long>(nullable: true),
                    modifTime = table.Column<DateTime>(nullable: true),
                    modifUN = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_BankAccount", x => new { x.entityCode, x.psCode, x.refID, x.BankCode });
                    table.UniqueConstraint("AK_TR_BankAccount_BankCode_entityCode_psCode_refID", x => new { x.BankCode, x.entityCode, x.psCode, x.refID });
                });

            migrationBuilder.CreateTable(
                name: "TR_Company",
                columns: table => new
                {
                    entityCode = table.Column<string>(maxLength: 1, nullable: false),
                    psCode = table.Column<string>(maxLength: 8, nullable: false),
                    refID = table.Column<int>(nullable: false),
                    inputTime = table.Column<DateTime>(nullable: false),
                    inputUN = table.Column<long>(nullable: true),
                    modifTime = table.Column<DateTime>(nullable: true),
                    modifUN = table.Column<long>(nullable: true),
                    coAddress = table.Column<string>(maxLength: 500, nullable: false),
                    coCity = table.Column<string>(maxLength: 50, nullable: false),
                    coCountry = table.Column<string>(maxLength: 50, nullable: false),
                    coName = table.Column<string>(maxLength: 200, nullable: false),
                    coPostCode = table.Column<string>(maxLength: 10, nullable: false),
                    coType = table.Column<string>(maxLength: 100, nullable: false),
                    jobTitle = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_Company", x => new { x.entityCode, x.psCode, x.refID });
                });

            migrationBuilder.CreateTable(
                name: "TR_Document",
                columns: table => new
                {
                    entityCode = table.Column<string>(maxLength: 1, nullable: false),
                    psCode = table.Column<string>(maxLength: 8, nullable: false),
                    documentType = table.Column<string>(maxLength: 25, nullable: false),
                    inputTime = table.Column<DateTime>(nullable: false),
                    inputUN = table.Column<long>(nullable: true),
                    modifTime = table.Column<DateTime>(nullable: true),
                    modifUN = table.Column<long>(nullable: true),
                    documentBinary = table.Column<string>(nullable: true),
                    documentPicType = table.Column<string>(maxLength: 10, nullable: true),
                    documentRef = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_Document", x => new { x.entityCode, x.psCode, x.documentType });
                    table.UniqueConstraint("AK_TR_Document_documentType_entityCode_psCode", x => new { x.documentType, x.entityCode, x.psCode });
                });

            migrationBuilder.CreateTable(
                name: "TR_Email",
                columns: table => new
                {
                    entityCode = table.Column<string>(maxLength: 1, nullable: false),
                    psCode = table.Column<string>(maxLength: 8, nullable: false),
                    refID = table.Column<int>(nullable: false),
                    inputTime = table.Column<DateTime>(nullable: false),
                    inputUN = table.Column<long>(nullable: true),
                    modifTime = table.Column<DateTime>(nullable: true),
                    modifUN = table.Column<long>(nullable: true),
                    email = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_Email", x => new { x.entityCode, x.psCode, x.refID });
                });

            migrationBuilder.CreateTable(
                name: "TR_EmailInvalid",
                columns: table => new
                {
                    entityCode = table.Column<string>(maxLength: 1, nullable: false),
                    psCode = table.Column<string>(maxLength: 8, nullable: false),
                    refID = table.Column<int>(nullable: false),
                    email = table.Column<string>(maxLength: 100, nullable: false),
                    inputTime = table.Column<DateTime>(nullable: false),
                    inputUN = table.Column<long>(nullable: true),
                    modifTime = table.Column<DateTime>(nullable: true),
                    modifUN = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_EmailInvalid", x => new { x.entityCode, x.psCode, x.refID, x.email });
                    table.UniqueConstraint("AK_TR_EmailInvalid_email_entityCode_psCode_refID", x => new { x.email, x.entityCode, x.psCode, x.refID });
                });

            migrationBuilder.CreateTable(
                name: "TR_Family",
                columns: table => new
                {
                    entityCode = table.Column<string>(maxLength: 1, nullable: false),
                    psCode = table.Column<string>(maxLength: 8, nullable: false),
                    refID = table.Column<int>(nullable: false),
                    inputTime = table.Column<DateTime>(nullable: false),
                    inputUN = table.Column<long>(nullable: true),
                    modifTime = table.Column<DateTime>(nullable: true),
                    modifUN = table.Column<long>(nullable: true),
                    birthDate = table.Column<DateTime>(nullable: true),
                    familyName = table.Column<string>(maxLength: 100, nullable: false),
                    familyStatus = table.Column<string>(maxLength: 50, nullable: false),
                    occID = table.Column<string>(maxLength: 3, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_Family", x => new { x.entityCode, x.psCode, x.refID });
                });

            migrationBuilder.CreateTable(
                name: "TR_IDFamily",
                columns: table => new
                {
                    psCode = table.Column<string>(maxLength: 8, nullable: false),
                    familyRefID = table.Column<int>(nullable: false),
                    refID = table.Column<int>(nullable: false),
                    inputTime = table.Column<DateTime>(nullable: false),
                    inputUN = table.Column<long>(nullable: true),
                    modifTime = table.Column<DateTime>(nullable: true),
                    modifUN = table.Column<long>(nullable: true),
                    expiredDate = table.Column<DateTime>(nullable: true),
                    idNo = table.Column<string>(maxLength: 50, nullable: false),
                    idType = table.Column<string>(maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_IDFamily", x => new { x.psCode, x.familyRefID, x.refID });
                    table.UniqueConstraint("AK_TR_IDFamily_familyRefID_psCode_refID", x => new { x.familyRefID, x.psCode, x.refID });
                });

            migrationBuilder.CreateTable(
                name: "MS_City",
                columns: table => new
                {
                    entityCode = table.Column<string>(maxLength: 1, nullable: false),
                    cityCode = table.Column<string>(maxLength: 4, nullable: false),
                    inputTime = table.Column<DateTime>(nullable: false),
                    inputUN = table.Column<long>(nullable: true),
                    modifTime = table.Column<DateTime>(nullable: true),
                    modifUN = table.Column<long>(nullable: true),
                    cityAbbreviation = table.Column<string>(maxLength: 10, nullable: true),
                    cityName = table.Column<string>(maxLength: 50, nullable: false),
                    country = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_City", x => new { x.entityCode, x.cityCode });
                    table.UniqueConstraint("AK_MS_City_cityCode_entityCode", x => new { x.cityCode, x.entityCode });
                    table.ForeignKey(
                        name: "FK_MS_City_LK_Country_country",
                        column: x => x.country,
                        principalTable: "LK_Country",
                        principalColumn: "country",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TR_ID",
                columns: table => new
                {
                    entityCode = table.Column<string>(maxLength: 1, nullable: false),
                    psCode = table.Column<string>(maxLength: 8, nullable: false),
                    refID = table.Column<int>(nullable: false),
                    inputTime = table.Column<DateTime>(nullable: false),
                    inputUN = table.Column<long>(nullable: true),
                    modifTime = table.Column<DateTime>(nullable: true),
                    modifUN = table.Column<long>(nullable: true),
                    expiredDate = table.Column<DateTime>(nullable: true),
                    idNo = table.Column<string>(maxLength: 50, nullable: false),
                    idType = table.Column<string>(maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_ID", x => new { x.entityCode, x.psCode, x.refID });
                    table.ForeignKey(
                        name: "FK_TR_ID_LK_IDType_idType",
                        column: x => x.idType,
                        principalTable: "LK_IDType",
                        principalColumn: "idType",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TR_KeyPeople",
                columns: table => new
                {
                    trKeyPeopleId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    inputTime = table.Column<DateTime>(nullable: false),
                    inputUN = table.Column<long>(nullable: true),
                    modifTime = table.Column<DateTime>(nullable: true),
                    modifUN = table.Column<long>(nullable: true),
                    isAcive = table.Column<bool>(nullable: false),
                    keyPeopleId = table.Column<int>(nullable: false),
                    keyPeopleName = table.Column<string>(maxLength: 100, nullable: true),
                    keyPeoplePSCode = table.Column<string>(maxLength: 8, nullable: true),
                    psCode = table.Column<string>(maxLength: 8, nullable: true),
                    refID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_KeyPeople", x => x.trKeyPeopleId);
                    table.ForeignKey(
                        name: "FK_TR_KeyPeople_LK_KeyPeople_keyPeopleId",
                        column: x => x.keyPeopleId,
                        principalTable: "LK_KeyPeople",
                        principalColumn: "keyPeopleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TR_Phone",
                columns: table => new
                {
                    entityCode = table.Column<string>(maxLength: 1, nullable: false),
                    psCode = table.Column<string>(maxLength: 8, nullable: false),
                    refID = table.Column<int>(nullable: false),
                    inputTime = table.Column<DateTime>(nullable: false),
                    inputUN = table.Column<long>(nullable: true),
                    modifTime = table.Column<DateTime>(nullable: true),
                    modifUN = table.Column<long>(nullable: true),
                    number = table.Column<string>(maxLength: 30, nullable: false),
                    phoneType = table.Column<string>(maxLength: 1, nullable: false),
                    remarks = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_Phone", x => new { x.entityCode, x.psCode, x.refID });
                    table.ForeignKey(
                        name: "FK_TR_Phone_LK_PhoneType_phoneType",
                        column: x => x.phoneType,
                        principalTable: "LK_PhoneType",
                        principalColumn: "phoneType",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PERSONALS_MEMBER",
                columns: table => new
                {
                    entityCode = table.Column<string>(maxLength: 1, nullable: false),
                    psCode = table.Column<string>(maxLength: 8, nullable: false),
                    ACDCode = table.Column<string>(maxLength: 12, nullable: false),
                    CDCode = table.Column<string>(maxLength: 12, nullable: false),
                    inputTime = table.Column<DateTime>(nullable: false),
                    inputUN = table.Column<long>(nullable: true),
                    modifTime = table.Column<DateTime>(nullable: true),
                    modifUN = table.Column<long>(nullable: true),
                    PTName = table.Column<string>(maxLength: 50, nullable: false),
                    PrincName = table.Column<string>(maxLength: 50, nullable: false),
                    bankAccName = table.Column<string>(maxLength: 50, nullable: false),
                    bankAccNo = table.Column<string>(maxLength: 30, nullable: false),
                    bankBranchName = table.Column<string>(maxLength: 50, nullable: false),
                    bankCode = table.Column<string>(maxLength: 5, nullable: false),
                    bankType = table.Column<string>(maxLength: 1, nullable: false),
                    franchiseGroup = table.Column<string>(maxLength: 50, nullable: true),
                    id_role = table.Column<int>(nullable: true),
                    isACD = table.Column<bool>(nullable: false),
                    isActive = table.Column<bool>(nullable: false),
                    isActiveEmail = table.Column<bool>(nullable: false),
                    isCD = table.Column<bool>(nullable: false),
                    isInstitusi = table.Column<bool>(nullable: false),
                    isMember = table.Column<bool>(nullable: false),
                    isPKP = table.Column<bool>(nullable: false),
                    joinDate = table.Column<DateTime>(nullable: false),
                    memberCode = table.Column<string>(maxLength: 12, nullable: false),
                    memberStatusCode = table.Column<string>(maxLength: 5, nullable: false),
                    mothName = table.Column<string>(maxLength: 50, nullable: false),
                    parentMemberCode = table.Column<string>(maxLength: 20, nullable: false),
                    password = table.Column<string>(maxLength: 20, nullable: false),
                    regDate = table.Column<DateTime>(nullable: false),
                    remarks1 = table.Column<string>(maxLength: 200, nullable: false),
                    remarks2 = table.Column<string>(maxLength: 200, nullable: false),
                    remarks3 = table.Column<string>(maxLength: 200, nullable: false),
                    scmCode = table.Column<string>(maxLength: 3, nullable: false),
                    specCode = table.Column<string>(maxLength: 1, nullable: false),
                    spouName = table.Column<string>(maxLength: 50, nullable: false),
                    userName = table.Column<string>(maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PERSONALS_MEMBER", x => new { x.entityCode, x.psCode });
                    table.UniqueConstraint("AK_PERSONALS_MEMBER_entityCode_memberCode_psCode_scmCode", x => new { x.entityCode, x.memberCode, x.psCode, x.scmCode });
                    table.ForeignKey(
                        name: "FK_PERSONALS_MEMBER_MS_Bank_bankCode",
                        column: x => x.bankCode,
                        principalTable: "MS_Bank",
                        principalColumn: "bankCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PERSONALS_MEMBER_LK_BankType_bankType",
                        column: x => x.bankType,
                        principalTable: "LK_BankType",
                        principalColumn: "bankType",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PERSONALS_MEMBER_LK_Spec_specCode",
                        column: x => x.specCode,
                        principalTable: "LK_Spec",
                        principalColumn: "specCode",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TR_Group",
                columns: table => new
                {
                    entityCode = table.Column<string>(maxLength: 1, nullable: false),
                    groupCode = table.Column<string>(maxLength: 5, nullable: false),
                    psCode = table.Column<string>(maxLength: 8, nullable: false),
                    inputTime = table.Column<DateTime>(nullable: false),
                    inputUN = table.Column<long>(nullable: true),
                    modifTime = table.Column<DateTime>(nullable: true),
                    modifUN = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TR_Group", x => new { x.entityCode, x.groupCode, x.psCode });
                    table.ForeignKey(
                        name: "FK_TR_Group_MS_Group_entityCode_groupCode",
                        columns: x => new { x.entityCode, x.groupCode },
                        principalTable: "MS_Group",
                        principalColumns: new[] { "entityCode", "groupCode" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MS_PostCode",
                columns: table => new
                {
                    entityCode = table.Column<string>(maxLength: 1, nullable: false),
                    cityCode = table.Column<string>(maxLength: 4, nullable: false),
                    postCode = table.Column<string>(maxLength: 50, nullable: false),
                    inputTime = table.Column<DateTime>(nullable: false),
                    inputUN = table.Column<long>(nullable: true),
                    modifTime = table.Column<DateTime>(nullable: true),
                    modifUN = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_PostCode", x => new { x.entityCode, x.cityCode, x.postCode });
                    table.UniqueConstraint("AK_MS_PostCode_cityCode_entityCode_postCode", x => new { x.cityCode, x.entityCode, x.postCode });
                    table.ForeignKey(
                        name: "FK_MS_PostCode_MS_City_entityCode_cityCode",
                        columns: x => new { x.entityCode, x.cityCode },
                        principalTable: "MS_City",
                        principalColumns: new[] { "entityCode", "cityCode" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MS_Street",
                columns: table => new
                {
                    entityCode = table.Column<string>(maxLength: 1, nullable: false),
                    cityCode = table.Column<string>(maxLength: 4, nullable: false),
                    postCode = table.Column<string>(maxLength: 50, nullable: false),
                    streetNo = table.Column<int>(nullable: false),
                    inputTime = table.Column<DateTime>(nullable: false),
                    inputUN = table.Column<long>(nullable: true),
                    modifTime = table.Column<DateTime>(nullable: true),
                    modifUN = table.Column<long>(nullable: true),
                    streetName = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MS_Street", x => new { x.entityCode, x.cityCode, x.postCode, x.streetNo });
                    table.UniqueConstraint("AK_MS_Street_cityCode_entityCode_postCode_streetNo", x => new { x.cityCode, x.entityCode, x.postCode, x.streetNo });
                    table.ForeignKey(
                        name: "FK_MS_Street_MS_PostCode_entityCode_cityCode_postCode",
                        columns: x => new { x.entityCode, x.cityCode, x.postCode },
                        principalTable: "MS_PostCode",
                        principalColumns: new[] { "entityCode", "cityCode", "postCode" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MS_City_country",
                table: "MS_City",
                column: "country");

            migrationBuilder.CreateIndex(
                name: "IX_PERSONALS_MEMBER_bankCode",
                table: "PERSONALS_MEMBER",
                column: "bankCode");

            migrationBuilder.CreateIndex(
                name: "IX_PERSONALS_MEMBER_bankType",
                table: "PERSONALS_MEMBER",
                column: "bankType");

            migrationBuilder.CreateIndex(
                name: "IX_PERSONALS_MEMBER_specCode",
                table: "PERSONALS_MEMBER",
                column: "specCode");

            migrationBuilder.CreateIndex(
                name: "IX_TR_ID_idType",
                table: "TR_ID",
                column: "idType");

            migrationBuilder.CreateIndex(
                name: "IX_TR_KeyPeople_keyPeopleId",
                table: "TR_KeyPeople",
                column: "keyPeopleId");

            migrationBuilder.CreateIndex(
                name: "IX_TR_Phone_phoneType",
                table: "TR_Phone",
                column: "phoneType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LK_AddrType");

            migrationBuilder.DropTable(
                name: "LK_Blood");

            migrationBuilder.DropTable(
                name: "LK_FamilyStatus");

            migrationBuilder.DropTable(
                name: "LK_Grade");

            migrationBuilder.DropTable(
                name: "LK_MarStatus");

            migrationBuilder.DropTable(
                name: "LK_PhonePrefix");

            migrationBuilder.DropTable(
                name: "LK_Religion");

            migrationBuilder.DropTable(
                name: "MS_County");

            migrationBuilder.DropTable(
                name: "MS_Document");

            migrationBuilder.DropTable(
                name: "MS_FranchiseGroup");

            migrationBuilder.DropTable(
                name: "MS_JobTitle");

            migrationBuilder.DropTable(
                name: "MS_Nation");

            migrationBuilder.DropTable(
                name: "MS_Occupation");

            migrationBuilder.DropTable(
                name: "MS_PriorityPass");

            migrationBuilder.DropTable(
                name: "MS_Province");

            migrationBuilder.DropTable(
                name: "MS_Regency");

            migrationBuilder.DropTable(
                name: "MS_RelationResident");

            migrationBuilder.DropTable(
                name: "MS_Street");

            migrationBuilder.DropTable(
                name: "MS_Village");

            migrationBuilder.DropTable(
                name: "PERSONALS");

            migrationBuilder.DropTable(
                name: "PERSONALS_MEMBER");

            migrationBuilder.DropTable(
                name: "SYS_Counter");

            migrationBuilder.DropTable(
                name: "SYS_CounterMember");

            migrationBuilder.DropTable(
                name: "SYS_RolesAddr");

            migrationBuilder.DropTable(
                name: "SYS_UserGroup");

            migrationBuilder.DropTable(
                name: "TR_Address");

            migrationBuilder.DropTable(
                name: "TR_BankAccount");

            migrationBuilder.DropTable(
                name: "TR_Company");

            migrationBuilder.DropTable(
                name: "TR_Document");

            migrationBuilder.DropTable(
                name: "TR_Email");

            migrationBuilder.DropTable(
                name: "TR_EmailInvalid");

            migrationBuilder.DropTable(
                name: "TR_Family");

            migrationBuilder.DropTable(
                name: "TR_Group");

            migrationBuilder.DropTable(
                name: "TR_ID");

            migrationBuilder.DropTable(
                name: "TR_IDFamily");

            migrationBuilder.DropTable(
                name: "TR_KeyPeople");

            migrationBuilder.DropTable(
                name: "TR_Phone");

            migrationBuilder.DropTable(
                name: "MS_PostCode");

            migrationBuilder.DropTable(
                name: "MS_Bank");

            migrationBuilder.DropTable(
                name: "LK_BankType");

            migrationBuilder.DropTable(
                name: "LK_Spec");

            migrationBuilder.DropTable(
                name: "MS_Group");

            migrationBuilder.DropTable(
                name: "LK_IDType");

            migrationBuilder.DropTable(
                name: "LK_KeyPeople");

            migrationBuilder.DropTable(
                name: "LK_PhoneType");

            migrationBuilder.DropTable(
                name: "MS_City");

            migrationBuilder.DropTable(
                name: "LK_Country");
        }
    }
}
