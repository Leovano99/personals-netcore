using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.UI;
using Microsoft.AspNetCore.Http;
using VDI.Demo.Authorization.Users;
using VDI.Demo.SqlExecuter;
using VDI.Demo.NewCommDB;
using VDI.Demo.Personals.Personals.Dto;
using VDI.Demo.PersonalsDB;
using Microsoft.EntityFrameworkCore;
using VDI.Demo.Files;
using VDI.Demo.Authorization;
using Abp.Authorization;
using Newtonsoft.Json.Linq;
using Abp.Extensions;
using static VDI.Demo.Personals.Personals.Dto.GetUniversalPersonalDto;
using System.Text.RegularExpressions;
using Visionet_Backend_NetCore.Komunikasi;
using Newtonsoft.Json;
using VDI.Demo.EntityFrameworkCore;
using System.Net.Mail;
using Microsoft.AspNetCore.Hosting;

namespace VDI.Demo.Personals.Personals
{
    //[AbpAuthorize(AppPermissions.Pages_Tenant_Personal)]
    public class PersonalAppService : DemoAppServiceBase, IPersonalAppService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRepository<PERSONALS, string> _personalRepo;
        private readonly ISqlExecuter _sqlExecuter;
        private readonly IRepository<TR_BankAccount, string> _bankAccountRepo;
        private readonly IRepository<TR_Company, string> _companyRepo;
        private readonly IRepository<TR_Document, string> _documentRepo;
        private readonly IRepository<TR_Family, string> _familyRepo;
        private readonly IRepository<TR_ID, string> _idRepo;
        private readonly IRepository<TR_Phone, string> _phoneRepo;
        private readonly IRepository<TR_Email, string> _emailRepo;
        private readonly IRepository<TR_Address, string> _addressRepo;
        private readonly IRepository<PERSONALS_MEMBER, string> _personalsMemberRepo;
        private readonly IRepository<TR_KeyPeople> _keyPeopleRepo;
        private readonly IRepository<MS_Occupation, string> _occRepo;
        private readonly IRepository<MS_Nation, string> _nationRepo;
        private readonly IRepository<MS_Schema, string> _schemaRepo;
        private readonly IRepository<MS_Document, string> _msDocumentRepo;
        private readonly IRepository<MS_BankPersonal, string> _bankRepo;
        private readonly IRepository<LK_IDType, string> _lkIDTypeRepo;
        private readonly IRepository<LK_AddrType, string> _lkAddrTypeRepo;
        private readonly IRepository<TR_EmailInvalid, string> _trEmailInvalidRepo;
        private readonly IRepository<User, long> _userRepository;
        private readonly IRepository<LK_KeyPeople> _lkKeyPeopleRepo;
        private readonly IRepository<SYS_Counter, string> _sysCounter;
        private readonly UserManager _userManager;
        private readonly string entityCode;
        private readonly FilesHelper _filesHelper;
        private readonly IRepository<LK_Country, string> _lkCountryRepo;
        private readonly IRepository<MS_Province, string> _msProvinceRepo;
        private readonly IRepository<MS_City, string> _msCityRepo;
        private readonly IRepository<MS_PostCode, string> _msPostCodeRepo;
        private readonly IRepository<MS_Schema, string> _msSchemaRepo;
        private readonly IRepository<PERSONALS_MEMBER, string> _personalMemberRepo;
        private readonly DemoDbContext _contextDemo;
        private readonly PersonalsNewDbContext _contextPers;
        private readonly IHostingEnvironment _hostingEnvironment;

        #region constructor
        public PersonalAppService
            (
                IHttpContextAccessor httpContextAccessor,
                ISqlExecuter sqlExecuter,
                IRepository<PERSONALS, string> personalRepo,
                IRepository<TR_BankAccount, string> bankAccountRepo,
                IRepository<TR_Company, string> companyRepo,
                IRepository<TR_Document, string> documentRepo,
                IRepository<TR_Family, string> familyRepo,
                IRepository<TR_ID, string> idRepo,
                IRepository<TR_Phone, string> phoneRepo,
                IRepository<TR_Email, string> emailRepo,
                IRepository<TR_Address, string> addressRepo,
                IRepository<PERSONALS_MEMBER, string> personalsMemberRepo,
                IRepository<TR_KeyPeople> keyPeopleRepo,
                IRepository<MS_Occupation, string> occRepo,
                IRepository<MS_Nation, string> nationRepo,
                IRepository<MS_Schema, string> schemaRepo,
                IRepository<MS_Document, string> msDocumentRepo,
                IRepository<MS_BankPersonal, string> bankRepo,
                IRepository<LK_IDType, string> lkIDTypeRepo,
                IRepository<LK_AddrType, string> lkAddrTypeRepo,
                IRepository<LK_KeyPeople> lkKeyPeopleRepo,
                IRepository<SYS_Counter, string> sysCounter,
                IRepository<TR_EmailInvalid, string> trEmailInvalidRepo,
                IRepository<User, long> userRepository,
                UserManager userManager,
                FilesHelper filesHelper,
                IRepository<LK_Country, string> lkCountryRepo,
                IRepository<MS_Province, string> msProvinceRepo,
                IRepository<MS_City, string> msCityRepo,
                IRepository<MS_PostCode, string> msPostCodeRepo,
                IRepository<MS_Schema, string> msSchemaRepo,
                IRepository<PERSONALS_MEMBER, string> personalMemberRepo,
                DemoDbContext contextDemo,
                PersonalsNewDbContext contextPers,
                IHostingEnvironment hostingEnvironment
            )
        {
            _httpContextAccessor = httpContextAccessor;
            _personalRepo = personalRepo;
            _bankAccountRepo = bankAccountRepo;
            _companyRepo = companyRepo;
            _documentRepo = documentRepo;
            _familyRepo = familyRepo;
            _idRepo = idRepo;
            _sqlExecuter = sqlExecuter;
            _phoneRepo = phoneRepo;
            _emailRepo = emailRepo;
            _addressRepo = addressRepo;
            _personalsMemberRepo = personalsMemberRepo;
            _keyPeopleRepo = keyPeopleRepo;
            _occRepo = occRepo;
            _nationRepo = nationRepo;
            _schemaRepo = schemaRepo;
            _msDocumentRepo = msDocumentRepo;
            _bankRepo = bankRepo;
            _lkIDTypeRepo = lkIDTypeRepo;
            _lkAddrTypeRepo = lkAddrTypeRepo;
            _lkKeyPeopleRepo = lkKeyPeopleRepo;
            _trEmailInvalidRepo = trEmailInvalidRepo;
            _sysCounter = sysCounter;
            _userManager = userManager;
            _userRepository = userRepository;
            entityCode = "1";
            _filesHelper = filesHelper;
            _lkCountryRepo = lkCountryRepo;
            _msProvinceRepo = msProvinceRepo;
            _msCityRepo = msCityRepo;
            _msPostCodeRepo = msPostCodeRepo;
            _msSchemaRepo = msSchemaRepo;
            _personalMemberRepo = personalMemberRepo;
            _contextDemo = contextDemo;
            _contextPers = contextPers;
            _hostingEnvironment = hostingEnvironment;
        }
        #endregion

        public List<GetPersonalsByKeywordList> GetPersonalsByAdvanceSearch(GetPersonalsByAdvanceSearchInputDto input)
        {
            DateTime inputBirthDate;
            if (input.birthDate != null)
            {
                DateTime inputBirthDateParse = DateTime.Parse(input.birthDate);
                inputBirthDate = inputBirthDateParse.Date;
            }
            else
            {
                inputBirthDate = new DateTime();
            }
            var getAllData = (from x in _personalRepo.GetAll()
                              join xx in _personalsMemberRepo.GetAll() on x.psCode equals xx.psCode into h
                              from xx in h.DefaultIfEmpty()
                              join y in _idRepo.GetAll() on x.psCode equals y.psCode into g
                              from yy in g.DefaultIfEmpty()
                              join z in _emailRepo.GetAll() on x.psCode equals z.psCode into m
                              from zz in m.DefaultIfEmpty()
                              select new
                              {
                                  psCode = x.psCode,
                                  name = x.name,
                                  birthDate = x.birthDate == null ? null : x.birthDate,
                                  email = zz.email,
                                  isInstitute = x.isInstitute,
                                  idNo = yy.idNo,
                                  idType = yy.idType,
                                  memberCode = xx.memberCode
                              })
                              .WhereIf(!input.keyword.IsNullOrWhiteSpace(), item => item.psCode.Equals(input.keyword) || item.name.Contains(input.keyword))
                              .WhereIf(!input.idNumber.IsNullOrWhiteSpace(), item => item.idType == "1" || item.idType == "2" && item.idNo == input.idNumber)
                              .WhereIf(!input.memberCode.IsNullOrWhiteSpace(), item => item.memberCode.Equals(input.memberCode))
                              .WhereIf(input.birthDate != null, item => item.birthDate == inputBirthDate)
                              .WhereIf(!input.email.IsNullOrWhiteSpace(), item => item.email.Equals(input.email))
                              .GroupBy(y => new
                              {
                                  y.psCode,
                                  y.name,
                                  y.birthDate,                               
                                  y.isInstitute
                              })
                              .Select(x => new GetPersonalsByKeywordList
                              {
                                  birthDate = x.Key.birthDate.ToString(),
                                  psCode = x.Key.psCode,
                                  isInstitute = x.Key.isInstitute,
                                  name = x.Key.name                               
                              }).ToList();

            return getAllData;
        }

        public async Task<PagedResultDto<GetPersonalsByKeywordList>> GetPersonalsByKeyword(GetPersonalsByKeywordInputDto input)
        {
            var getAllData = (from x in _personalRepo.GetAll()
                              where x.name.Contains(input.keyword) || x.psCode.Contains(input.keyword)
                              select new GetPersonalsByKeywordList
                              {
                                  psCode = x.psCode,
                                  name = x.name,
                                  birthDate = x.birthDate == null ? null : x.birthDate.ToString().Substring(0, 11),
                                  modifTime = x.LastModificationTime == null ? null : x.LastModificationTime.ToString().Substring(0, 11),
                                  isInstitute = x.isInstitute
                              });

            var dataCount = await getAllData.AsQueryable().CountAsync();

            var resultList = await getAllData.AsQueryable().OrderBy(input.Sorting).PageBy(input).ToListAsync();

            var listDtos = resultList;

            return new PagedResultDto<GetPersonalsByKeywordList>(
                dataCount,
                listDtos);
        }

        #region function create : createPersonal, createKeyPeople, createBankAccount, createCompany, createDocument, createIDNumber, createFamily, createMember
        /*
        public async Task CreatePersonal(CreatePersonalDto input)
        {
            Logger.Info("CreatePersonal() - Started.");
            PERSONALS personal = new PERSONALS()
            {
                entityCode = entityCode,
                psCode = input.psCode,
                parentPSCode = "-",
                name = input.name,
                sex = input.sex,
                birthDate = input.birthDate,
                birthPlace = input.birthPlace,
                marCode = String.IsNullOrEmpty(input.marCode) ? "0" : input.marCode,
                relCode = String.IsNullOrEmpty(input.relCode) ? "0" : input.relCode,
                bloodCode = String.IsNullOrEmpty(input.bloodCode) ? "0" : input.bloodCode,
                occID = String.IsNullOrEmpty(input.occID) ? "001" : input.occID,
                nationID = input.nationID,
                familyStatus = String.IsNullOrEmpty(input.familyStatus) ? "0" : input.familyStatus,
                NPWP = input.npwp,
                FPTransCode = String.IsNullOrEmpty(input.FPTransCode) ? "0" : input.FPTransCode,
                grade = String.IsNullOrEmpty(input.grade) ? "0" : input.grade,
                isActive = input.isActive,
                remarks = input.remarks,
                mailGroup = "-",
                isInstitute = input.isInstitute
            };

            try
            {
                Logger.DebugFormat("CreatePersonal() - Start insert Personal. Parameters sent:{0}" +
                        "entityCode = {1}{0}" +
                        "psCode = {2}{0}" +
                        "parentPSCode = {3}{0}" +
                        "name = {4}{0}" +
                        "sex = {5}{0}" +
                        "birthDate = {6}{0}" +
                        "birthPlace = {7}{0}" +
                        "marCode = {8}{0}" +
                        "relCode = {9}{0}" +
                        "bloodCode = {10}{0}" +
                        "occID = {11}{0}" +
                        "nationID = {12}{0}" +
                        "familyStatus = {13}{0}" +
                        "NPWP = {14}{0}" +
                        "FPTransCode = {15}{0}" +
                        "grade = {16}{0}" +
                        "isActive = {17}{0}" +
                        "remarks = {18}{0}" +
                        "mailGroup = {19}{0}" +
                        "isInstitute = {20}{0}"
                        , Environment.NewLine, entityCode, input.psCode, "-", input.name
                        , input.sex, input.birthDate, input.birthPlace, input.marCode, input.relCode, input.bloodCode
                        , input.occID, input.nationID, input.familyStatus, input.npwp, input.FPTransCode, input.grade
                        , input.isActive, input.remarks, "-", input.isInstitute);

                await _personalRepo.InsertAsync(personal);

                Logger.DebugFormat("CreatePersonal() - Ended insert Personal.");
            }
            catch (DataException ex)
            {
                Logger.ErrorFormat("CreatePersonal() - ERROR DataException. Result = {0}", ex.Message);
                throw new UserFriendlyException("Db Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("CreatePersonal() - ERROR Exception. Result = {0}", ex.Message);
                throw new UserFriendlyException("Error: " + ex.Message);
            }

            Logger.Info("CreatePersonal() - Finished.");
        }
        */
        public async Task CreatePersonal(CreatePersonalDto input)
        {
            Logger.Info("CreatePersonal() - Started.");

            PERSONALS personal = new PERSONALS()
            {
                entityCode = entityCode,
                psCode = input.psCode,
                parentPSCode = "",
                name = input.name,
                sex = input.sex,
                birthDate = input.birthDate,
                birthPlace = String.IsNullOrEmpty(input.birthPlace) ? "" : input.birthPlace,
                marCode = String.IsNullOrEmpty(input.marCode) ? "0" : input.marCode,
                relCode = String.IsNullOrEmpty(input.relCode) ? "0" : input.relCode,
                bloodCode = String.IsNullOrEmpty(input.bloodCode) ? "0" : input.bloodCode,
                occID = String.IsNullOrEmpty(input.occID) ? "001" : input.occID,
                nationID = input.nationID,
                familyStatus = String.IsNullOrEmpty(input.familyStatus) ? "0" : input.familyStatus,
                NPWP = input.npwp,
                FPTransCode = String.IsNullOrEmpty(input.FPTransCode) ? "0" : input.FPTransCode,
                grade = String.IsNullOrEmpty(input.grade) ? "0" : input.grade,
                isActive = input.isActive,
                remarks = input.remarks,
                isInstitute = input.isInstitute
            };

            try
            {
                Logger.DebugFormat("CreatePersonal() - Start insert Personal. Parameters sent:{0}" +
                        "entityCode = {1}{0}" +
                        "psCode = {2}{0}" +
                        "parentPSCode = {3}{0}" +
                        "name = {4}{0}" +
                        "sex = {5}{0}" +
                        "birthDate = {6}{0}" +
                        "birthPlace = {7}{0}" +
                        "marCode = {8}{0}" +
                        "relCode = {9}{0}" +
                        "bloodCode = {10}{0}" +
                        "occID = {11}{0}" +
                        "nationID = {12}{0}" +
                        "familyStatus = {13}{0}" +
                        "NPWP = {14}{0}" +
                        "FPTransCode = {15}{0}" +
                        "grade = {16}{0}" +
                        "isActive = {17}{0}" +
                        "remarks = {18}{0}" +
                        "mailGroup = {19}{0}" +
                        "isInstitute = {20}{0}"
                        , Environment.NewLine, entityCode, input.psCode, "", input.name
                        , input.sex, input.birthDate, input.birthPlace, input.marCode, input.relCode, input.bloodCode
                        , input.occID, input.nationID, input.familyStatus, input.npwp, input.FPTransCode, input.grade
                        , input.isActive, input.remarks, "", input.isInstitute);

                await _personalRepo.InsertAsync(personal);
                var data = new List<CreateIDNumberDto>();
                if (input.isInstitute)
                {                    
                    // to do insert TDP Id Number
                    var dataIDNumber = new CreateIDNumberDto
                    {
                        entityCode = entityCode,
                        psCode = input.psCode,
                        refID = 1,
                        idNo = input.idNo,
                        idType = "7", // Tanda Daftar Perusahaan
                        expiredDate = null
                    };

                    data.Add(dataIDNumber);
                    await CreateIDNumber(data);                    
                }
                else
                {
                    if (input.isKeyPeople)
                    {
                        // to do insert KTP Id Number
                        var dataIDNumber = new CreateIDNumberDto
                        {
                            entityCode = entityCode,
                            psCode = input.psCode,
                            refID = 1,
                            idNo = input.idNo,
                            idType = "1", // Kartu Tanda Penduduk
                            expiredDate = null
                        };

                        data.Add(dataIDNumber);
                        await CreateIDNumber(data);
                    }
                }

                Logger.DebugFormat("CreatePersonal() - Ended insert Personal.");
            }
            catch (DataException ex)
            {
                Logger.ErrorFormat("CreatePersonal() - ERROR DataException. Result = {0}", ex.Message);
                throw new UserFriendlyException("Db Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("CreatePersonal() - ERROR Exception. Result = {0}", ex.Message);
                throw new UserFriendlyException("Error: " + ex.Message);
            }

            Logger.Info("CreatePersonal() - Finished.");
        }

        public async Task CreateKeyPeople(List<CreateKeyPeopleDto> inputs)
        {
            Logger.Info("CreateKeyPeople() - Started.");
            foreach (var input in inputs)
            {
                TR_KeyPeople keyPeople = new TR_KeyPeople()
                {
                    psCode = input.psCode,
                    refID = input.refID,
                    keyPeopleId = input.keyPeopleId,
                    keyPeopleName = input.keyPeopleName,
                    keyPeoplePSCode = input.keyPeoplePSCode,
                    isActive = true
                };

                try
                {
                    Logger.DebugFormat("CreateKeyPeople() - Start insert Key People. Parameters sent:{0}" +
                            "psCode = {1}{0}" +
                            "refID = {2}{0}" +
                            "keyPeopleId = {3}{0}" +
                            "keyPeopleName = {4}{0}" +
                            "keyPeoplePSCode = {5}{0}" +
                            "isActive = {6}{0}"
                            , Environment.NewLine, input.psCode, input.refID
                            , input.keyPeopleId, input.keyPeopleName, input.keyPeoplePSCode, true);

                    await _keyPeopleRepo.InsertAsync(keyPeople);

                    Logger.DebugFormat("CreateKeyPeople() - Ended insert Key People.");
                }
                catch (DataException ex)
                {
                    Logger.ErrorFormat("CreateKeyPeople() - ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat("CreateKeyPeople() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }
            }
            Logger.Info("CreateKeyPeople() - Finished.");
        }
        
        /*
        public async Task CreateBankAccount0(List<CreateBankAccountDto> inputs)
        {
            Logger.Info("CreateBankAccount() - Started.");
            foreach (var input in inputs)
            {
                var checkBankNameNo = (from ba in _bankAccountRepo.GetAll()
                                       where ba.entityCode == "1"
                                       && ba.psCode == input.psCode
                                       && ba.BankCode == input.BankCode
                                       && (ba.AccountNo == input.AccountNo && (ba.AccountName == input.AccountName || ba.AccountNo == input.AccountNo))
                                       select ba).Any();

                if (!checkBankNameNo)
                {
                    TR_BankAccount bankAccount = new TR_BankAccount()
                    {
                        entityCode = entityCode,
                        psCode = input.psCode,
                        refID = input.refID,
                        BankCode = input.BankCode,
                        AccountNo = input.AccountNo,
                        AccountName = input.AccountName
                    };

                    try
                    {
                        Logger.DebugFormat("CreateBankAccount() - Start insert Bank Account. Parameters sent:{0}" +
                                "entityCode = {1}{0}" +
                                "psCode = {2}{0}" +
                                "refID = {3}{0}" +
                                "BankCode = {4}{0}" +
                                "AccountNo = {5}{0}" +
                                "AccountName = {6}{0}"
                                , Environment.NewLine, entityCode, input.psCode, input.refID
                                , input.BankCode, input.AccountNo, input.AccountName);
                        await _bankAccountRepo.InsertAsync(bankAccount);
                        Logger.DebugFormat("CreateBankAccount() - Ended insert Bank Account.");
                    }
                    catch (DataException ex)
                    {
                        Logger.ErrorFormat("CreateBankAccount() - ERROR DataException. Result = {0}", ex.Message);
                        throw new UserFriendlyException("Db Error: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Logger.ErrorFormat("CreateBankAccount() - ERROR Exception. Result = {0}", ex.Message);
                        throw new UserFriendlyException("Error: " + ex.Message);
                    }
                }
                else
                {
                    Logger.ErrorFormat("UpdateBankAccount() - ERROR Result = {0}.", "The bank name or code is already exist in this bank type!");
                    throw new UserFriendlyException("The bank name or code is already exist in this bank type!");
                }
            }
            Logger.Info("CreateBankAccount() - Finished.");
        }
        */

        public async Task CreateBankAccount(List<CreateBankAccountDto> inputs)
        {
            Logger.Info("CreateBankAccount() - Started.");
            foreach (var input in inputs)
            {
                var checkBankNameNo = (from ba in _bankAccountRepo.GetAll()
                                       where ba.entityCode == "1"
                                       && ba.psCode == input.psCode
                                       && ba.BankCode == input.BankCode
                                       && (ba.AccountNo == input.AccountNo && (ba.AccountName == input.AccountName || ba.AccountNo == input.AccountNo))
                                       select ba).Any();

                if (!checkBankNameNo)
                {
                    TR_BankAccount bankAccount = new TR_BankAccount()
                    {
                        entityCode = entityCode,
                        psCode = input.psCode,
                        refID = input.refID,
                        BankCode = input.BankCode,
                        AccountNo = input.AccountNo,
                        AccountName = input.AccountName,
                        isAutoDebit = input.isAutoDebit,
                        isMain = false,
                        BankBranchName = input.BankBranchName
                    };

                    try
                    {
                        Logger.DebugFormat("CreateBankAccount() - Start insert Bank Account. Parameters sent:{0}" +
                                "entityCode = {1}{0}" +
                                "psCode = {2}{0}" +
                                "refID = {3}{0}" +
                                "BankCode = {4}{0}" +
                                "AccountNo = {5}{0}" +
                                "AccountName = {6}{0}" +
                                "isAutoDebit = {7}{0}" +
                                "isMain = {8}{0}" +
                                "BankBranchName = {9}{0}"
                                , Environment.NewLine, entityCode, input.psCode, input.refID
                                , input.BankCode, input.AccountNo, input.AccountName, input.isAutoDebit, input.isMain, input.BankBranchName);
                        await _bankAccountRepo.InsertAsync(bankAccount);
                        Logger.DebugFormat("CreateBankAccount() - Ended insert Bank Account.");
                    }
                    catch (DataException ex)
                    {
                        Logger.ErrorFormat("CreateBankAccount() - ERROR DataException. Result = {0}", ex.Message);
                        throw new UserFriendlyException("Db Error: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Logger.ErrorFormat("CreateBankAccount() - ERROR Exception. Result = {0}", ex.Message);
                        throw new UserFriendlyException("Error: " + ex.Message);
                    }
                }
                else
                {
                    Logger.ErrorFormat("UpdateBankAccount() - ERROR Result = {0}.", "The bank name or code is already exist in this bank type!");
                    throw new UserFriendlyException("The bank name or code is already exist in this bank type!");
                }
            }
            Logger.Info("CreateBankAccount() - Finished.");
        }

        public async Task CreateCompany(List<CreateCompanyDto> inputs)
        {
            Logger.Info("CreateCompany() - Started.");
            foreach (var input in inputs)
            {
                TR_Company company = new TR_Company()
                {
                    entityCode = entityCode,
                    psCode = input.psCode,
                    refID = input.refID,
                    coName = input.coName,
                    coAddress = String.IsNullOrEmpty(input.coAddress) ? "-" : input.coAddress,
                    coCity = input.coCity,
                    coPostCode = String.IsNullOrEmpty(input.coPostCode) ? "-" : input.coPostCode,
                    coCountry = input.coCountry,
                    coType = String.IsNullOrEmpty(input.coType) ? "-" : input.coType,
                    jobTitle = input.jobTitle
                };
                try
                {
                    Logger.DebugFormat("CreateCompany() - Start insert Company. Parameters sent:{0}" +
                            "entityCode = {1}{0}" +
                            "psCode = {2}{0}" +
                            "refID = {3}{0}" +
                            "coName = {4}{0}" +
                            "coAddress = {5}{0}" +
                            "coCity = {6}{0}" +
                            "coPostCode = {7}{0}" +
                            "coCountry = {8}{0}" +
                            "coType = {9}{0}" +
                            "jobTitle = {10}{0}"
                            , Environment.NewLine, entityCode, input.psCode, input.refID
                            , input.coName, input.coAddress, input.coCity, input.coPostCode, input.coCountry
                            , input.coType, input.jobTitle);
                    await _companyRepo.InsertAsync(company);
                    Logger.DebugFormat("CreateCompany() - Ended insert Company.");
                }
                catch (DataException ex)
                {
                    Logger.ErrorFormat("CreateCompany() - ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat("CreateCompany() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }
            }
            Logger.Info("CreateCompany() - Finished.");
        }

        public async Task CreateDocument(List<CreateDocumentDto> inputs)
        {
            Logger.Info("CreateDocument() - Started.");
            foreach (var input in inputs)
            {
                //string uploadDocumentPath = UploadFile(input.documentBinary);

                TR_Document document = new TR_Document()
                {
                    entityCode = entityCode,
                    psCode = input.psCode,
                    documentType = input.documentType,
                    documentRef = 1,
                    documentBinary = input.documentBinary,
                    documentPicType = "-"
                };
                try
                {
                    Logger.DebugFormat("CreateDocument() - Start insert Document. Parameters sent:{0}" +
                            "entityCode = {1}{0}" +
                            "psCode = {2}{0}" +
                            "documentType = {3}{0}" +
                            "documentRef = {4}{0}" +
                            "documentBinary = {5}{0}" +
                            "documentPicType = {6}{0}"
                            , Environment.NewLine, entityCode, input.psCode, input.documentType
                            , 1, input.documentBinary, "-");
                    await _documentRepo.InsertAsync(document);
                    Logger.DebugFormat("CreateDocument() - Ended insert Document.");
                }
                catch (DataException ex)
                {
                    Logger.ErrorFormat("CreateDocument() - ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat("CreateDocument() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }
            }
            Logger.Info("CreateDocument() - Finished.");
        }

        public async Task CreateIDNumber(List<CreateIDNumberDto> inputs)
        {
            Logger.Info("CreateIDNumber() - Started.");
            foreach (var input in inputs)
            {
                TR_ID idNumber = new TR_ID()
                {
                    entityCode = entityCode,
                    psCode = input.psCode,
                    refID = input.refID,
                    idType = input.idType,
                    idNo = input.idNo,
                    expiredDate = input.expiredDate
                };

                var isAvailableData = (from ti in _contextPers.TR_ID
                                       where ti.refID == input.refID && ti.psCode == input.psCode
                                       select ti.psCode).Any();
                try
                {
                    if (!isAvailableData)
                    {
                        Logger.DebugFormat("CreateIDNumber() - Start insert ID Number. Parameters sent:{0}" +
                                "entityCode = {1}{0}" +
                                "psCode = {2}{0}" +
                                "refID = {3}{0}" +
                                "idType = {4}{0}" +
                                "idNo = {5}{0}" +
                                "expiredDate = {6}{0}"
                                , Environment.NewLine, entityCode, input.psCode, input.refID
                                , input.idType, input.idNo, input.expiredDate);
                        await _idRepo.InsertAsync(idNumber);
                        Logger.DebugFormat("CreateIDNumber() - Ended insert ID Number.");
                    }
                    else
                    {
                        Logger.DebugFormat("CreateIDNumber() - Start update ID Number. Parameters sent:{0}" +
                               "entityCode = {1}{0}" +
                               "psCode = {2}{0}" +
                               "refID = {3}{0}" +
                               "idType = {4}{0}" +
                               "idNo = {5}{0}" +
                               "expiredDate = {6}{0}"
                               , Environment.NewLine, entityCode, input.psCode, input.refID
                               , input.idType, input.idNo, input.expiredDate);
                        await _idRepo.UpdateAsync(idNumber);
                        Logger.DebugFormat("CreateIDNumber() - Ended update ID Number.");
                    }
                }
                catch (DataException ex)
                {
                    Logger.ErrorFormat("CreateIDNumber() - ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    SendConsole("" + ex.Message + "" + ex.StackTrace);
                    Logger.ErrorFormat("CreateIDNumber() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }
            }
            Logger.Info("CreateIDNumber() - Finished.");
        }

        public async Task CreateFamily(List<CreateFamilyDto> inputs)
        {
            Logger.Info("CreateFamily() - Started.");
            foreach (var input in inputs)
            {
                TR_Family family = new TR_Family()
                {
                    entityCode = entityCode,
                    psCode = input.psCode,
                    refID = input.refID,
                    familyName = input.familyName,
                    familyStatus = input.familyStatus,
                    birthDate = input.birthDate,
                    occID = input.occID
                };
                try
                {
                    Logger.DebugFormat("CreateFamily() - Start insert Family. Parameters sent:{0}" +
                            "entityCode = {1}{0}" +
                            "psCode = {2}{0}" +
                            "refID = {3}{0}" +
                            "familyName = {4}{0}" +
                            "familyStatus = {5}{0}" +
                            "birthDate = {6}{0}" +
                            "occID = {7}{0}"
                            , Environment.NewLine, entityCode, input.psCode, input.refID
                            , input.familyName, input.familyStatus, input.birthDate, input.occID);
                    await _familyRepo.InsertAsync(family);
                    Logger.DebugFormat("CreateFamily() - Ended insert Family.");
                }
                catch (DataException ex)
                {
                    Logger.ErrorFormat("CreateFamily() - ERROR DataException. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat("CreateFamily() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }
            }
            Logger.Info("CreateFamily() - Finished.");
        }

        [UnitOfWork(isTransactional: false)]
        public JObject CreateMember(CreateMemberDto input)
        {
            JObject obj = new JObject();
            try
            {
                Logger.Info("CreateMember() - Started.");
                CreateMemberDataDto memberData = input.memberData;
                CreateMemberActivationDto memberActivation = input.memberActivation;
                CreateMemberBankDataDto memberBankData = input.memberBankData;

                PERSONALS_MEMBER member = new PERSONALS_MEMBER()
                {
                    entityCode = entityCode,
                    psCode = memberData.psCode,
                    scmCode = memberData.scmCode,
                    memberCode = memberData.memberCode,
                    parentMemberCode = String.IsNullOrEmpty(memberData.parentMemberCode) ? "-" : memberData.parentMemberCode,
                    specCode = String.IsNullOrEmpty(memberData.specCode) ? "0" : memberData.specCode,
                    CDCode = String.IsNullOrEmpty(memberData.CDCode) ? "-" : memberData.CDCode,
                    ACDCode = String.IsNullOrEmpty(memberData.ACDCode) ? "-" : memberData.ACDCode,
                    PTName = String.IsNullOrEmpty(memberData.PTName) ? null : memberData.PTName,
                    PrincName = String.IsNullOrEmpty(memberData.PrincName) ? null : memberData.PrincName,
                    spouName = String.IsNullOrEmpty(memberData.spouName) ? null : memberData.spouName,
                    regDate = DateTime.Today, //unused
                    joinDate = DateTime.Today, //unused
                    remarks1 = memberData.remarks1 == null ? "-" : memberData.remarks1,
                    isCD = memberData.isCD,
                    isACD = memberData.isACD,
                    isInstitusi = memberData.isInstitusi,
                    isPKP = memberData.isPKP,
                    franchiseGroup = memberData.franchiseGroup,
                    isActiveEmail = false, //unused
                    memberStatusCode = String.IsNullOrEmpty(memberActivation.memberStatusCode) ? "-" : memberActivation.memberStatusCode,
                    isMember = memberActivation.isMember,
                    isActive = memberActivation.isActive,
                    password = String.IsNullOrEmpty(memberActivation.password) ? null : memberActivation.password,

                    bankType = String.IsNullOrEmpty(memberBankData.bankType) ? "0" : memberBankData.bankType,
                    bankCode = memberBankData.bankCode,
                    bankAccNo = String.IsNullOrEmpty(memberBankData.bankAccNo) ? null : memberBankData.bankAccNo,
                    bankAccName = String.IsNullOrEmpty(memberBankData.bankAccName) ? null : memberBankData.bankAccName,
                    bankBranchName = String.IsNullOrEmpty(memberBankData.bankBranchName) ? null : memberBankData.bankBranchName,
                    bankAccountRefID = memberBankData.bankAccountRefID,
                    CreationTime = DateTime.Now
                };

                try
                {
                    Logger.DebugFormat("CreateMember() - Start insert Member. Parameters sent:{0}" +
                           "entityCode       = {1}{0}" +
                           "psCode           = {2}{0}" +
                           "scmCode          = {3}{0}" +
                           "memberCode       = {4}{0}" +
                           "parentMemberCode = {5}{0}" +
                           "specCode         = {6}{0}" +
                           "CDCode           = {7}{0}" +
                           "ACDCode          = {8}{0}" +
                           "PTName           = {9}{0}" +
                           "PrincName        = {10}{0}" +
                           "mothName         = {11}{0}" +
                           "spouName         = {12}{0}" +
                           "regDate          = {13}{0}" +
                           "joinDate         = {14}{0}" +
                           "remarks1         = {15}{0}" +
                           "remarks2         = {16}{0}" +
                           "remarks3         = {17}{0}" +
                           "isCD             = {18}{0}" +
                           "isACD            = {19}{0}" +
                           "isInstitusi      = {20}{0}" +
                           "isPKP            = {21}{0}" +
                           "franchiseGroup   = {22}{0}" +
                           "isActiveEmail    = {23}{0}" +
                           "userName         = {24}{0}" +
                           "memberStatusCode = {25}{0}" +
                           "isMember         = {26}{0}" +
                           "isActive         = {27}{0}" +
                           "password         = {28}{0}" +
                           "bankType         = {29}{0}" +
                           "bankCode         = {30}{0}" +
                           "bankAccNo        = {31}{0}" +
                           "bankAccName      = {32}{0}" +
                           "bankBranchName   = {33}{0}" +
                           "bankAccountRefID = {33}{0}"
                           , Environment.NewLine, entityCode, memberData.psCode, memberData.scmCode
                           , memberData.memberCode, memberData.parentMemberCode, memberData.specCode, memberData.CDCode
                           , memberData.ACDCode, memberData.PTName, memberData.PrincName, ""
                           , memberData.spouName, DateTime.Today, DateTime.Today, memberData.remarks1
                           , "", "", memberData.isCD, memberData.isACD
                           , memberData.isInstitusi, memberData.isPKP, memberData.franchiseGroup, false
                           , "", memberActivation.memberStatusCode, memberActivation.isMember, memberActivation.isActive
                           , memberActivation.password, memberBankData.bankType, memberBankData.bankCode
                           , memberBankData.bankAccNo, memberBankData.bankAccName, memberBankData.bankBranchName, memberBankData.bankAccountRefID);

                    _contextPers.PERSONALS_MEMBER.Add(member);
                    _contextPers.SaveChanges();

                    obj.Add("message", "Member Successfully Saved");
                    Logger.DebugFormat("CreateMember() - Ended insert Member.");
                }
                catch (DataException ex)
                {
                    Logger.ErrorFormat("CreateMember() - ERROR DataException. Result = {0}", ex.Message);
                    SendConsole("" + ex.Message + " " + ex.StackTrace);
                    throw new UserFriendlyException("Db Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat("CreateMember() - ERROR Exception. Result = {0}", ex.Message);
                    SendConsole("" + ex.Message + " " + ex.StackTrace);
                    throw new UserFriendlyException("Error: " + ex.Message);
                }
                Logger.Info("CreateMember() - Finished.");
            }
            catch (Exception e)
            {
                SendConsole("" + e.Message + " " + e.StackTrace);
            }

            return obj;
        }
        #endregion

        [UnitOfWork(isTransactional: false)]
        public GetUniversalPersonalDto GetUniversalPersonal(string psCode)
        {
            GetUniversalPersonalDto result = null;
            try
            {
                List<GetUpdateInfo> getListUpdateInfo = new List<GetUpdateInfo>();
                GetUpdateInfo getUpdateInfo = new GetUpdateInfo();
                #region GetPersonalByPsCode
                var personal = GetPersonalByPsCode(psCode);
                Logger.DebugFormat("GetUniversalPersonal() -GetPersonalByPsCode() result: {0}" +
                            "psCode         = {1}{0}" +
                            "name           = {2}{0}" +
                            "registerTime   = {3}{0}" +
                            "updateTime     = {4}{0}"
                            , Environment.NewLine, personal.psCode, personal.name, personal.registerTime, personal.updateTime);

                if (personal != null)
                {
                    if (personal.updateTime != null)
                    {
                        getUpdateInfo.updateBy = personal.updatedBy;
                        getUpdateInfo.updateTime = DateTime.ParseExact(personal.updateTime, "dd/MM/yyyy", null);

                        Logger.DebugFormat("GetUniversalPersonal() - personal1. result: {0}" +
                           "personalupdatetime        = {1}{0}" +
                           "personalupdateby          = {2}"
                           , Environment.NewLine, getUpdateInfo.updateTime, getUpdateInfo.updateBy);

                        getListUpdateInfo.Add(getUpdateInfo);
                        getUpdateInfo = null;
                    }
                    else if (personal.registerTime != null)
                    {
                        getUpdateInfo.updateBy = personal.registeredBy;
                        getUpdateInfo.updateTime = DateTime.ParseExact(personal.registerTime, "dd/MM/yyyy", null);

                        Logger.DebugFormat("GetUniversalPersonal() - personal2. result: {0}" +
                           "personalupdatetime        = {1}{0}" +
                           "personalupdateby          = {2}"
                           , Environment.NewLine, getUpdateInfo.updateTime, getUpdateInfo.updateBy);

                        getListUpdateInfo.Add(getUpdateInfo);
                        getUpdateInfo = null;
                    }
                    else
                    {
                        getUpdateInfo = null;
                    }


                }
                #endregion
                #region GetKeyPeopleByPsCode
                var keyPeople = GetKeyPeopleByPsCode(psCode);

                if (keyPeople.Any())
                {
                    var keyPeopleDate = keyPeople.Select(item => new
                    {
                        updateTime = item.LastModificationTime != null ? DateTime.ParseExact(item.LastModificationTime, "dd/MM/yyyy", null) : DateTime.ParseExact(item.CreationTime, "dd/MM/yyyy", null),
                        updateBy = item.LastModifierUserId != null ? item.LastModifierUserId : item.CreatorUserId
                    })
                    .OrderByDescending(item => item.updateTime)
                    .Select(item => new GetUpdateInfo
                    {
                        updateBy = item.updateBy,
                        updateTime = item.updateTime
                    }).FirstOrDefault();

                    if (keyPeopleDate != null)
                    {
                        Logger.DebugFormat("GetUniversalPersonal() - GetKeyPeopleByPsCode() result: {0}" +
                            "keypeopleupdatetime        = {1}{0}" +
                            "keypeopleupdateby          = {2}"
                            , Environment.NewLine, keyPeopleDate.updateTime, keyPeopleDate.updateBy);
                        getListUpdateInfo.Add(keyPeopleDate);
                    }
                }
                else
                {
                    Logger.Info("Key People with pscode: " + psCode + "is null");
                }
                #endregion
                #region GetContactByPsCode
                var contact = GetContactByPsCode(psCode);
                if (contact != null)
                {
                    #region contact.getEmail
                    if (contact.getEmail.Any())
                    {
                        var getEmail = contact.getEmail.Select(A => new
                        {
                            updateTime = A.LastModificationTime != null ? DateTime.Parse(A.LastModificationTime) : DateTime.Parse(A.CreateTime),
                            updateBy = A.LastModificationBy != null ? A.LastModificationBy : A.CreatedBy
                        })
                        .OrderByDescending(item => item.updateTime)
                        .Select(item => new GetUpdateInfo
                        {
                            updateBy = item.updateBy,
                            updateTime = item.updateTime
                        }).FirstOrDefault();

                        if (getEmail != null)
                        {
                            Logger.DebugFormat("GetUniversalPersonal() - GetContactByPsCode() - Email. result: {0}" +
                            "emailupdatetime        = {1}{0}" +
                            "emailupdateby          = {2}"
                            , Environment.NewLine, getEmail.updateTime, getEmail.updateBy);

                            var getUserByUserId = GetUserNameByUserId(Int32.Parse(getEmail.updateBy));
                            getEmail.updateBy = getUserByUserId;

                            getListUpdateInfo.Add(getEmail);
                        }
                    }
                    else
                    {
                        Logger.Info("contact.getEmail with pscode: " + psCode + "is null");
                    }
                    #endregion
                    #region contact.getPhone
                    if (contact.getPhone.Any())
                    {
                        var getPhone = contact.getPhone.Select(A => new
                        {
                            updateTime = A.LastModificationTime != null ? DateTime.Parse(A.LastModificationTime) : DateTime.Parse(A.CreationTime),
                            updateBy = A.LastModifierUserId != null ? A.LastModifierUserId : A.CreatorUserId
                        })
                        .OrderByDescending(item => item.updateTime)
                        .Select(item => new GetUpdateInfo
                        {
                            updateBy = item.updateBy,
                            updateTime = item.updateTime
                        }).FirstOrDefault();

                        if (getPhone != null)
                        {
                            Logger.DebugFormat("GetUniversalPersonal() - GetContactByPsCode() - Phone. result: {0}" +
                            "phoneupdatetime        = {1}{0}" +
                            "phoneupdateby          = {2}"
                            , Environment.NewLine, getPhone.updateTime, getPhone.updateBy);

                            var getUserByUserId = !string.IsNullOrWhiteSpace("" + getPhone.updateBy) ? GetUserNameByUserId(Int32.Parse(getPhone.updateBy)) : null;
                            getPhone.updateBy = getUserByUserId;

                            getListUpdateInfo.Add(getPhone);
                        }
                    }
                    else
                    {
                        Logger.Info("contact.getPhone with pscode: " + psCode + "is null");
                    }
                    #endregion
                }
                else
                {
                    Logger.Info("contact with pscode: " + psCode + "is null");
                }
                #endregion
                #region GetAddressByPsCode
                var address = GetAddressByPsCode(psCode);
                if (address.Any())
                {
                    var getAddress = address.Select(A => new
                    {
                        updateTime = A.LastModificationTime != null ? DateTime.Parse(A.LastModificationTime) : DateTime.Parse(A.CreationTime),
                        updateBy = A.LastModifierUserId != null ? A.LastModifierUserId.ToString() : A.CreatorUserId.ToString()
                    })
                    .OrderByDescending(item => item.updateTime)
                    .Select(item => new GetUpdateInfo
                    {
                        updateBy = item.updateBy,
                        updateTime = item.updateTime
                    }).FirstOrDefault();

                    if (getAddress != null)
                    {
                        Logger.DebugFormat("GetUniversalPersonal() - GetAddressByPsCode() - Address. result: {0}" +
                            "addressupdatetime        = {1}{0}" +
                            "addressupdateby          = {2}"
                            , Environment.NewLine, getAddress.updateTime, getAddress.updateBy);

                        var getUserByUserId = !string.IsNullOrWhiteSpace("" + getAddress.updateBy) ? GetUserNameByUserId(Int32.Parse(getAddress.updateBy)) : null;
                        getAddress.updateBy = getUserByUserId;

                        getListUpdateInfo.Add(getAddress);
                    }
                }
                else
                {
                    Logger.Info("address with pscode: " + psCode + "is null");
                }
                #endregion
                #region GetBankAccountByPsCode
                var bankAccount = GetBankAccountByPsCode(psCode);
                if (bankAccount.Any())
                {
                    var getBankAccount = bankAccount.Select(A => new
                    {
                        updateTime = A.LastModificationTime != null ? DateTime.ParseExact(A.LastModificationTime, "dd/MM/yyyy", null) : DateTime.ParseExact(A.CreationTime, "dd/MM/yyyy", null),
                        updateBy = A.LastModifierUserId != null ? A.LastModifierUserId.ToString() : A.CreatorUserId.ToString()
                    })
                    .OrderByDescending(item => item.updateTime)
                    .Select(item => new GetUpdateInfo
                    {
                        updateBy = item.updateBy,
                        updateTime = item.updateTime
                    }).FirstOrDefault();

                    if (getBankAccount != null)
                    {
                        Logger.DebugFormat("GetUniversalPersonal() - GetBankAccountByPsCode() - bankaccount. result: {0}" +
                           "bankaccountupdatetime        = {1}{0}" +
                           "bankaccountupdateby          = {2}"
                           , Environment.NewLine, getBankAccount.updateTime, getBankAccount.updateBy);

                        getListUpdateInfo.Add(getBankAccount);
                    }
                }
                else
                {
                    Logger.Info("bankAccount with pscode: " + psCode + "is null");
                }
                #endregion
                #region GetCompanyByPsCode
                var company = GetCompanyByPsCode(psCode);
                if (company.Any())
                {
                    var getCompany = company.Select(A => new
                    {
                        updateTime = A.LastModificationTime != null ? DateTime.ParseExact(A.LastModificationTime, "dd/MM/yyyy", null) : DateTime.ParseExact(A.CreationTime, "dd/MM/yyyy", null),
                        updateBy = A.LastModifierUserId != null ? A.LastModifierUserId.ToString() : A.CreatorUserId.ToString()
                    })
                    .OrderByDescending(item => item.updateTime)
                    .Select(item => new GetUpdateInfo
                    {
                        updateBy = item.updateBy,
                        updateTime = item.updateTime
                    }).FirstOrDefault();

                    if (getCompany != null)
                    {
                        Logger.DebugFormat("GetUniversalPersonal() - GetCompanyByPsCode() - company. result: {0}" +
                           "companyupdatetime        = {1}{0}" +
                           "companyupdateby          = {2}"
                           , Environment.NewLine, getCompany.updateTime, getCompany.updateBy);

                        getListUpdateInfo.Add(getCompany);
                    }
                }
                else
                {
                    Logger.Info("company with pscode: " + psCode + "is null");
                }
                #endregion
                #region GetDocumentByPsCode
                var document = GetDocumentByPsCode(psCode);
                if (document.Any())
                {
                    var getDocument = document.Select(A => new
                    {
                        updateTime = A.LastModificationTime != null ? DateTime.ParseExact(A.LastModificationTime, "dd/MM/yyyy", null) : DateTime.ParseExact(A.CreationTime, "dd/MM/yyyy", null),
                        updateBy = A.LastModifierUserId != null ? A.LastModifierUserId.ToString() : A.CreatorUserId.ToString()
                    })
                                    .OrderByDescending(item => item.updateTime)
                                    .Select(item => new GetUpdateInfo
                                    {
                                        updateBy = item.updateBy,
                                        updateTime = item.updateTime
                                    }).FirstOrDefault();

                    if (getDocument != null)
                    {
                        Logger.DebugFormat("GetUniversalPersonal() - GetDocumentByPsCode() - document. result: {0}" +
                           "documentupdatetime        = {1}{0}" +
                           "documentupdateby          = {2}"
                           , Environment.NewLine, getDocument.updateTime, getDocument.updateBy);

                        getListUpdateInfo.Add(getDocument);
                    }
                }
                else
                {
                    Logger.Info("document with pscode: " + psCode + "is null");
                }
                #endregion
                #region GetIDNumberByPsCode
                var idNumber = GetIDNumberByPsCode(psCode);
                if (idNumber.Any())
                {
                    var getIdNumber = idNumber.Select(A => new
                    {
                        updateTime = A.LastModificationTime != null ? DateTime.ParseExact(A.LastModificationTime, "dd/MM/yyyy", null) : DateTime.ParseExact(A.CreationTime, "dd/MM/yyyy", null),
                        updateBy = A.LastModifierUserId != null ? A.LastModifierUserId.ToString() : A.CreatorUserId.ToString()
                    })
                                    .OrderByDescending(item => item.updateTime)
                                    .Select(item => new GetUpdateInfo
                                    {
                                        updateBy = item.updateBy,
                                        updateTime = item.updateTime
                                    }).FirstOrDefault();

                    if (getIdNumber != null)
                    {
                        Logger.DebugFormat("GetUniversalPersonal() - GetIDNumberByPsCode() - idNumber. result: {0}" +
                           "idNumberupdatetime        = {1}{0}" +
                           "idNumberupdateby          = {2}"
                           , Environment.NewLine, getIdNumber.updateTime, getIdNumber.updateBy);

                        getListUpdateInfo.Add(getIdNumber);
                    }
                }
                else
                {
                    Logger.Info("idNumber with pscode: " + psCode + "is null");
                }
                #endregion
                #region GetFamilyByPsCode
                var family = GetFamilyByPsCode(psCode);
                if (family.Any())
                {
                    var getFamily = family.Select(A => new
                    {
                        updateTime = A.LastModificationTime != null ? DateTime.ParseExact(A.LastModificationTime, "dd/MM/yyyy", null) : DateTime.ParseExact(A.CreationTime, "dd/MM/yyyy", null),
                        updateBy = A.LastModifierUserId != null ? A.LastModifierUserId.ToString() : A.CreatorUserId.ToString()
                    })
                                    .OrderByDescending(item => item.updateTime)
                                    .Select(item => new GetUpdateInfo
                                    {
                                        updateBy = item.updateBy,
                                        updateTime = item.updateTime
                                    }).FirstOrDefault();

                    if (getFamily != null)
                    {
                        Logger.DebugFormat("GetUniversalPersonal() - GetFamilyByPsCode() - family. result: {0}" +
                           "familyupdatetime        = {1}{0}" +
                           "familyupdateby          = {2}"
                           , Environment.NewLine, getFamily.updateTime, getFamily.updateBy);

                        getListUpdateInfo.Add(getFamily);
                    }
                }
                else
                {
                    Logger.Info("family with pscode: " + psCode + "is null");
                }
                #endregion
                #region GetMemberByPsCode
                var member = GetMemberByPsCode(psCode);
                if (member.Any())
                {
                    var getDataUpdateInfo = member.Select(ui => ui.UpdateInfo).ToList();
                    if (getDataUpdateInfo.Any())
                    {
                        var updateInfo = getDataUpdateInfo.OrderByDescending(item => item.updateTime)
                                            .Select(item => new GetUpdateInfo
                                            {
                                                updateBy = item.updateBy,
                                                updateTime = item.updateTime
                                            }).FirstOrDefault();

                        if (updateInfo != null)
                        {
                            Logger.DebugFormat("GetUniversalPersonal() - GetMemberByPsCode() - member. result: {0}" +
                           "memberupdatetime        = {1}{0}" +
                           "memberupdateby          = {2}"
                           , Environment.NewLine, updateInfo.updateTime, updateInfo.updateBy);

                            getListUpdateInfo.Add(updateInfo);
                        }
                    }
                }
                else
                {
                    Logger.Info("member with pscode: " + psCode + "is null");
                }
                #endregion

                var latestUpdate = getListUpdateInfo
                    .OrderByDescending(pp => pp.updateTime.Value.Day)
                    .ThenByDescending(pp => pp.updateTime.Value.Month)
                    .ThenByDescending(pp => pp.updateTime.Value.Year)
                    .ThenByDescending(pp => pp.updateTime.Value.TimeOfDay)
                    .FirstOrDefault();

                result = new GetUniversalPersonalDto()
                {
                    personal = personal,
                    keyPeople = keyPeople,
                    contact = contact,
                    address = address,
                    bankAccount = bankAccount,
                    company = company,
                    document = document,
                    idNumber = idNumber,
                    family = family,
                    member = member,
                    UpdateInfo = latestUpdate
                };
            }
            catch (Exception e)
            {
                SendConsole("" + e.Message + " " + e.StackTrace);
            }

            //if (result == null)
            //{
            //    throw new UserFriendlyException("No Result !");
            //}
            return result;
        }


        private string GetUserNameByUserId(long userId)
        {
            var user = (from A in _userRepository.GetAll()
                        where A.Id == userId
                        select A.UserName).FirstOrDefault();

            return user;
        }

        private string GetUserNameByUserIdTabPersonal(string psCode, string remarks, string type)
        {
            string query = null;
            //string user = null;

            //var getPersonal = (from p in _contextPers.PERSONAL
            //                   where p.psCode == psCode
            //                   select p
            //            );

            if (type == "creation")
            {
                query = @"SELECT
                        CASE WHEN a.remarks = @remarks THEN b.Name
                        ELSE c.Name 
                        END as name
                        FROM E3Personals..PERSONALS a
                        LEFT JOIN E3PropertySystemCore..Users b on a.inputUN = b.Id
                        LEFT JOIN E3PersonalsCore..Users c on a.inputUN = c.Id
                        WHERE a.psCode = @psCode";

                //user = (from p in getPersonal.ToList()
                //        join u1 in _contextDemo.Users on p.CreatorUserId equals u1.Id into user1
                //        from us1_ in user1.DefaultIfEmpty()
                //        where p.psCode == psCode
                //        select new
                //        {
                //            User = (us1_ == null || us1_.Name == null) ? null : (p.remarks == remarks ? us1_.Name : null)
                //        }
                //        ).Select(x => x.User).FirstOrDefault();
            }
            else
            {
                query = @"SELECT
                        CASE WHEN a.remarks = @remarks THEN b.Name
                        ELSE c.Name 
                        END as name
                        FROM E3Personals..PERSONALS a
                        LEFT JOIN E3PropertySystemCore..Users b on a.modifUN = b.Id
                        LEFT JOIN E3PersonalsCore..Users c on a.modifUN = c.Id
                        WHERE a.psCode = @psCode";

                //user = (from p in getPersonal.ToList()
                //        join u1 in _contextDemo.Users on p.LastModifierUserId equals u1.Id into user1
                //        from us1_ in user1.DefaultIfEmpty()
                //        where p.psCode == psCode
                //        select new
                //        {
                //            User = (us1_ == null || us1_.Name == null) ? null : (p.remarks == remarks ? us1_.Name : null)
                //        }
                //        ).Select(x => x.User).FirstOrDefault();
            }
            var user = _sqlExecuter.GetFromPersonals<string>(query, new { psCode = psCode, remarks = remarks }).FirstOrDefault();
            return user;
        }


        #region function get : getPersonal, getKeyPeople, getContact, getBankAccount, getCompany, getDocument, getIDNumber, getFamily, getMember
        [UnitOfWork(isTransactional: false)]
        public GetPersonalDto GetPersonalByPsCode(string psCode)
        {
            GetPersonalDto result = null;
            try
            {
                var getPersonal = (from x in _personalRepo.GetAll()
                                   join y in _occRepo.GetAll() on x.occID equals y.occID into occ
                                   from y in occ.DefaultIfEmpty()
                                   join z in _nationRepo.GetAll() on x.nationID equals z.nationID into nat
                                   from z in nat.DefaultIfEmpty()
                                   join a in _lkCountryRepo.GetAll() on x.nationID equals a.urut.ToString() into con
                                   from a in con.DefaultIfEmpty()

                                   where x.psCode.Equals(psCode)
                                   select new
                                   {                                      
                                       psCode = x.psCode,
                                       name = x.name,
                                       sex = x.sex,
                                       birthDate = x.birthDate,
                                       birthPlace = x.birthPlace,
                                       relCode = x.relCode,
                                       marCode = x.marCode,
                                       bloodCode = x.bloodCode,
                                       occID = x.occID,
                                       occDesc = y.occDesc,
                                       npwp = x.NPWP,
                                       nationID = x.nationID,
                                       nationality = z == null ? null : z.nationality,
                                       familyStatus = x.familyStatus,
                                       FPTransCode = x.FPTransCode,
                                       grade = x.grade,
                                       isActive = x.isActive,
                                       remarks = x.remarks,
                                       isInstitute = x.isInstitute,
                                       idNo = "1",
                                       isKeyPeople = false,
                                       updatedBy = x.LastModifierUserId,
                                       updateTime = x.LastModificationTime,
                                       registeredBy = x.CreatorUserId,
                                       registerTime = x.CreationTime,
                                       urutCountry = a == null ? 0 : a.urut,
                                       country = a == null ? null : a.country
                                   }).ToList();

                result = getPersonal.Select(x => new GetPersonalDto
                {
                    psCode = x.psCode,
                    name = x.name,
                    sex = x.sex,
                    birthDate = x.birthDate,
                    birthPlace = x.birthPlace,
                    relCode = x.relCode,
                    marCode = x.marCode,
                    bloodCode = x.bloodCode,
                    occID = x.occID,
                    occDesc = x.occDesc,
                    nationID = x.isInstitute == true ? Convert.ToString(x.urutCountry) : x.nationID,
                    npwp = x.npwp,
                    nationality = x.isInstitute == true ? x.country : x.nationality,
                    familyStatus = x.familyStatus,
                    FPTransCode = x.FPTransCode,
                    grade = x.grade,
                    isActive = x.isActive,
                    remarks = x.remarks,
                    isInstitute = x.isInstitute,
                    updatedBy = x.updatedBy == null ? GetUserNameByUserIdTabPersonal(x.psCode, x.remarks, "creation") : GetUserNameByUserIdTabPersonal(x.psCode, x.remarks, "modification"),
                    updateTime = x.updateTime == null ? x.registerTime.ToString("dd/MM/yyyy") : x.updateTime?.ToString("dd/MM/yyyy"),
                    registeredBy = GetUserNameByUserIdTabPersonal(x.psCode, x.remarks, "creation"),
                    registerTime = x.registerTime.ToString("dd/MM/yyyy")
                }).FirstOrDefault();
            }
            catch (Exception e)
            {
                SendConsole("" + e.Message + " " + e.StackTrace);
            }

            return result;
        }

        [UnitOfWork(isTransactional: false)]
        public List<GetKeyPeopleDto> GetKeyPeopleByPsCode(string psCode)
        {
            var getPeople = (from x in _keyPeopleRepo.GetAll()
                             join y in _lkKeyPeopleRepo.GetAll() on x.keyPeopleId equals y.Id
                             where x.psCode.Equals(psCode)
                             orderby x.refID ascending
                             select new
                             {
                                 Id = x.Id,
                                 psCode = x.psCode,
                                 refID = x.refID,
                                 keyPeopleId = x.keyPeopleId,
                                 keyPeopleDesc = y.keyPeopleDesc,
                                 keyPeopleName = x.keyPeopleName,
                                 keyPeoplePSCode = x.keyPeoplePSCode,
                                 isAcive = x.isActive,
                                 LastModificationTime = x.LastModificationTime,
                                 LastModifierUserId = x.LastModifierUserId,
                                 CreationTime = x.CreationTime,
                                 CreatorUserId = x.CreatorUserId,
                             }).ToList();

            var result = getPeople.Select(x => new GetKeyPeopleDto
            {
                Id = x.Id,
                psCode = x.psCode,
                refID = x.refID,
                keyPeopleId = x.keyPeopleId,
                keyPeopleDesc = x.keyPeopleDesc,
                keyPeopleName = x.keyPeopleName,
                keyPeoplePSCode = x.keyPeoplePSCode,
                isAcive = x.isAcive,
                LastModificationTime = x.LastModificationTime == null ? (x.CreationTime == null ? null : x.CreationTime.ToString("dd/MM/yyyy")) : Setting_variabel.ToString(x.LastModificationTime, "dd/MM/yyyy"),
                LastModifierUserId = x.LastModifierUserId == null ? (x.CreatorUserId == null ? null : GetIdName(x.CreatorUserId)) : GetIdName(x.LastModifierUserId),
                CreationTime = x.CreationTime == null ? null : x.CreationTime.ToString("dd/MM/yyyy"),
                CreatorUserId = x.CreatorUserId == null ? null : GetIdName(x.CreatorUserId)
            }).ToList();
            return new List<GetKeyPeopleDto>(result);
        }

        [UnitOfWork(isTransactional: false)]
        public GetContactDto GetContactByPsCode(string psCode)
        {
            var result = new GetContactDto()
            {
                getPhone = GetPhoneByPsCode(psCode),
                getEmail = GetEmailByPsCode(psCode)
            };

            return result;
        }

        [UnitOfWork(isTransactional: false)]
        public List<GetAddressDto> GetAddressByPsCode(string psCode)
        {
            var result = (from x in _addressRepo.GetAll()
                          join y in _lkAddrTypeRepo.GetAll() on x.addrType equals y.addrType
                          where x.psCode.Equals(psCode)
                          join z in _msProvinceRepo.GetAll() on x.province equals z.provinceName
                          join a in _msCityRepo.GetAll() on x.city equals a.cityName
                          orderby x.refID ascending
                          select new GetAddressDto
                          {
                              entityCode = x.entityCode,
                              psCode = x.psCode,
                              refID = x.refID,
                              addrType = x.addrType,
                              addrTypeName = y.addrTypeName,
                              address = x.address == "-" ? " " : x.address,
                              postCode = x.postCode,
                              city = x.city == "-" ? " " : x.city,
                              cityCode = a.cityCode,
                              country = x.country,
                              Kelurahan = x.Kelurahan,
                              Kecamatan = x.Kecamatan,
                              province = x.province != null ? x.province : null,
                              provinceCode = z.provinceCode,
                              CreationTime = x.CreationTime.ToString(),
                              CreatorUserId = x.CreatorUserId,
                              LastModificationTime = x.LastModificationTime != null ? x.LastModificationTime.ToString() : null,
                              LastModifierUserId = x.LastModifierUserId
                          }).ToList();
            return new List<GetAddressDto>(result);
        }

        [UnitOfWork(isTransactional: false)]
        public List<GetBankAccountDto> GetBankAccountByPsCode(string psCode)
        {
            List<GetBankAccountDto> result = null;
            try
            {
                result = (from x in _contextPers.TR_BankAccount
                          join y in _contextPers.MS_BankPersonal on x.BankCode equals y.bankCode
                          where x.psCode.Equals(psCode)
                          orderby x.refID ascending
                          select new GetBankAccountDto
                          {
                              entityCode = x.entityCode == null ? null : x.entityCode,
                              psCode = x.psCode == null ? null : x.psCode,
                              refID = x.refID,
                              BankCode = x.BankCode == null ? null : x.BankCode,
                              BankName = y.bankName == null ? null : y.bankName,
                              AccountNo = x.AccountNo == null ? null : x.AccountNo,
                              AccountName = x.AccountName == null ? null : x.AccountName,
                              isAutoDebit = x.isAutoDebit == null ? null : x.isAutoDebit,
                              isMain = x.isMain == null ? null : x.isMain,
                              BankBranchName = x.BankBranchName == null ? null : x.BankBranchName,
                              LastModificationTime = x.LastModificationTime == null ? (x.CreationTime == null ? null : x.CreationTime.ToString("dd/MM/yyyy")) : Setting_variabel.ToString(x.LastModificationTime, "dd/MM/yyyy"),
                              LastModifierUserId = x.LastModifierUserId == null ? (x.CreatorUserId == null ? null : GetIdName(x.CreatorUserId)) : GetIdName(x.LastModifierUserId),
                              CreationTime = x.CreationTime == null ? null : x.CreationTime.ToString("dd/MM/yyyy"),
                              CreatorUserId = x.CreatorUserId == null ? null : GetIdName(x.CreatorUserId)
                          }).ToList();
            }
            catch (Exception e)
            {
                SendConsole("" + e.Message + " " + e.StackTrace);
            }

            return new List<GetBankAccountDto>(result);
        }

        private string GetIdName(long? Id)
        {
            string getName = (from u in _userManager.Users
                              where u.Id == Id
                              select u.Name)
                       .DefaultIfEmpty("").First();

            return getName;
        }

        [UnitOfWork(isTransactional: false)]
        public List<GetCompanyDto> GetCompanyByPsCode(string psCode)
        {
            var getCompany = (from x in _companyRepo.GetAll()
                              where x.psCode.Equals(psCode)
                              join y in _msCityRepo.GetAll() on x.coCity equals y.cityName
                              join z in _msProvinceRepo.GetAll() on y.provinceCode equals z.provinceCode
                              orderby x.refID ascending
                              select new
                              {
                                  psCode = x.psCode,
                                  refID = x.refID,
                                  coName = x.coName,
                                  coAddress = x.coAddress,
                                  coCity = x.coCity,
                                  cityCode = y.cityCode,
                                  coPostCode = x.coPostCode,
                                  coCountry = x.coCountry,
                                  coProvince = z.provinceName,
                                  provinceCode = z.provinceCode,
                                  coType = x.coType,
                                  jobTitle = x.jobTitle,
                                  LastModificationTime = x.LastModificationTime,
                                  LastModifierUserId = x.LastModifierUserId,
                                  CreationTime = x.CreationTime,
                                  CreatorUserId = x.CreatorUserId
                              }).ToList();
            var result = getCompany.Select(x => new GetCompanyDto
            {
                psCode = x.psCode,
                refID = x.refID,
                coName = x.coName,
                coAddress = x.coAddress,
                coCity = x.coCity,
                cityCode = x.cityCode,
                coPostCode = x.coPostCode,
                coProvince = x.coProvince,
                provinceCode = x.provinceCode,
                coCountry = x.coCountry,
                coType = x.coType,
                jobTitle = x.jobTitle,
                LastModificationTime = x.LastModificationTime == null ? (x.CreationTime == null ? null : x.CreationTime.ToString("dd/MM/yyyy")) : Setting_variabel.ToString(x.LastModificationTime, "dd/MM/yyyy"),
                LastModifierUserId = x.LastModifierUserId == null ? (x.CreatorUserId == null ? null : GetIdName(x.CreatorUserId)) : GetIdName(x.LastModifierUserId),
                CreationTime = x.CreationTime == null ? null : x.CreationTime.ToString("dd/MM/yyyy"),
                CreatorUserId = x.CreatorUserId == null ? null : GetIdName(x.CreatorUserId)

            }).ToList();
            return new List<GetCompanyDto>(result);
        }

        [UnitOfWork(isTransactional: false)]
        public List<GetDocumentDto> GetDocumentByPsCode(string psCode)
        {
            var getDocument = (from x in _documentRepo.GetAll()
                               join y in _msDocumentRepo.GetAll() on x.documentType equals y.documentType
                               where x.psCode.Equals(psCode)
                               select new
                               {
                                   psCode = x.psCode,
                                   documentType = x.documentType,
                                   documentTypeName = y.documentName,
                                   documentBinary = x.documentBinary,
                                   LastModificationTime = x.LastModificationTime,
                                   LastModifierUserId = x.LastModifierUserId,
                                   CreationTime = x.CreationTime,
                                   CreatorUserId = x.CreatorUserId
                               }).ToList();

            var result = getDocument.Select(x => new GetDocumentDto
            {
                psCode = x.psCode,
                documentType = x.documentType,
                documentTypeName = x.documentTypeName,
                documentBinary = x.documentBinary, //TODO link + ip host
                //filename = (x != null && x.documentBinary != null) ? GetFileNameFromUrl(x.documentBinary) : null,
                LastModificationTime = x.LastModificationTime == null ? (x.CreationTime == null ? null : x.CreationTime.ToString("dd/MM/yyyy")) : Setting_variabel.ToString(x.LastModificationTime, "dd/MM/yyyy"),
                LastModifierUserId = x.LastModifierUserId == null ? (x.CreatorUserId == null ? null : GetIdName(x.CreatorUserId)) : GetIdName(x.LastModifierUserId),
                CreationTime = x.CreationTime == null ? null : x.CreationTime.ToString("dd/MM/yyyy"),
                CreatorUserId = x.CreatorUserId == null ? null : GetIdName(x.CreatorUserId)
            }).ToList();

            return new List<GetDocumentDto>(result);
        }

        [UnitOfWork(isTransactional: false)]
        public List<GetIDNumberDto> GetIDNumberByPsCode(string psCode)
        {
            List<GetIDNumberDto> result = null;
            try
            {
                var getIdNumber = (from x in _idRepo.GetAll()
                                   join y in _lkIDTypeRepo.GetAll() on x.idType equals y.idType
                                   where x.psCode.Equals(psCode)
                                   orderby x.refID ascending
                                   select new
                                   {
                                       psCode = x.psCode,
                                       refID = x.refID,
                                       idType = x.idType,
                                       idTypeName = y.idTypeName,
                                       idNo = x.idNo,
                                       expiredDate = x.expiredDate,
                                       LastModificationTime = x.LastModificationTime,
                                       LastModifierUserId = x.LastModifierUserId,
                                       CreationTime = x.CreationTime,
                                       CreatorUserId = x.CreatorUserId
                                   }).ToList();
                
                result = getIdNumber.Select(x => new GetIDNumberDto
                {
                    psCode = x.psCode,
                    refID = x.refID,
                    idType = x.idType,
                    idTypeName = x.idTypeName,
                    idNo = x.idNo,
                    expiredDate = x.expiredDate,
                    LastModificationTime = x.LastModificationTime == null ? (x.CreationTime == null ? null : x.CreationTime.ToString("dd/MM/yyyy")) : Setting_variabel.ToString(x.LastModificationTime, "dd/MM/yyyy"),
                    LastModifierUserId = x.LastModifierUserId == null ? (x.CreatorUserId == null ? null : GetIdName(x.CreatorUserId)) : GetIdName(x.LastModifierUserId),
                    CreationTime = x.CreationTime == null ? null : x.CreationTime.ToString("dd/MM/yyyy"),
                    CreatorUserId = x.CreatorUserId == null ? null : GetIdName(x.CreatorUserId)
                }).ToList();
            }
            catch (Exception e) {
                SendConsole(""+e.Message+" "+e.StackTrace);
            }

            return new List<GetIDNumberDto>(result);
        }

        [UnitOfWork(isTransactional: false)]
        public List<GetFamilyDto> GetFamilyByPsCode(string psCode)
        {
            var getFamily = (from x in _familyRepo.GetAll()
                             join y in _occRepo.GetAll() on x.occID equals y.occID into occ
                             from z in occ.DefaultIfEmpty()
                             where x.psCode.Equals(psCode)
                             orderby x.refID ascending
                             select new
                             {
                                 psCode = x.psCode,
                                 refID = x.refID,
                                 familyName = x.familyName,
                                 familyStatus = x.familyStatus,
                                 birthDate = x.birthDate,
                                 occID = x.occID,
                                 occDesc = z.occDesc,
                                 LastModificationTime = x.LastModificationTime,
                                 LastModifierUserId = x.LastModifierUserId,
                                 CreationTime = x.CreationTime,
                                 CreatorUserId = x.CreatorUserId
                             }).ToList();

            var result = getFamily.Select(x => new GetFamilyDto
            {
                psCode = x.psCode,
                refID = x.refID,
                familyName = x.familyName,
                familyStatus = x.familyStatus,
                birthDate = x.birthDate,
                occID = x.occID,
                occDesc = x.occDesc,
                LastModificationTime = x.LastModificationTime == null ? (x.CreationTime == null ? null : x.CreationTime.ToString("dd/MM/yyyy")) : Setting_variabel.ToString(x.LastModificationTime, "dd/MM/yyyy"),
                LastModifierUserId = x.LastModifierUserId == null ? (x.CreatorUserId == null ? null : GetIdName(x.CreatorUserId)) : GetIdName(x.LastModifierUserId),
                CreationTime = x.CreationTime == null ? null : x.CreationTime.ToString("dd/MM/yyyy"),
                CreatorUserId = x.CreatorUserId == null ? null : GetIdName(x.CreatorUserId)
            }).ToList();

            return new List<GetFamilyDto>(result);
        }

        [UnitOfWork(isTransactional: false)]
        public List<GetMemberDto> GetMemberByPsCode(string psCode)
        {
            SendConsole("Debug");
            List<GetMemberDto> members = new List<GetMemberDto>();
            try
            {
                #region old
                /*
                var ps = _personalRepo.GetAll();
                var pm = _personalsMemberRepo.GetAll();

                var result = (from x in _personalsMemberRepo.GetAll()
                              join y in _personalRepo.GetAll() on x.psCode equals y.psCode
                              where x.psCode.Equals(psCode)
                              select new GetMemberAllDto
                              {
                                  entityCode = entityCode,
                                  psCode = x.psCode,
                                  scmCode = x.scmCode,
                                  memberCode = x.memberCode,
                                  parentMemberCode = x.parentMemberCode,
                                  parentMemberName = (from z in pm
                                                      join q in ps on z.psCode equals q.psCode
                                                      where z.memberCode == x.parentMemberCode
                                                      select q.name).FirstOrDefault(),
                                  specCode = x.specCode,
                                  CDCode = x.CDCode,
                                  ACDCode = x.ACDCode,
                                  PTName = x.PTName,
                                  PrincName = x.PrincName,
                                  spouName = x.spouName,
                                  remarks1 = x.remarks1,
                                  isCD = x.isCD,
                                  isACD = x.isACD,
                                  isInstitusi = x.isInstitusi,
                                  isPKP = x.isPKP,
                                  franchiseGroup = x.franchiseGroup,

                                  memberStatusCode = x.memberStatusCode,
                                  isMember = x.isMember,
                                  isActive = x.isActive,
                                  password = x.password,

                                  bankType = x.bankType,
                                  bankCode = x.bankCode,
                                  bankAccNo = x.bankAccNo,
                                  bankAccName = x.bankAccName,
                                  bankBranchName = x.bankBranchName,
                                  createdTime = x.CreationTime,
                                  creatorUserId = x.CreatorUserId,
                                  updatedBy = x.LastModifierUserId,
                                  updatedTime = x.LastModificationTime
                              }).ToList();

                SendConsole("result:" + JsonConvert.SerializeObject(result));

                foreach (var data in result)
                {
                    var getSchema = (from x in _schemaRepo.GetAll()
                                     where x.entityCode == entityCode && x.scmCode == data.scmCode
                                     select new
                                     {
                                         scmName = x.scmName,
                                         isAutomaticMemberStatus = x.isAutomaticMemberStatus
                                     }).FirstOrDefault();

                    var memberData = new GetMemberDataDto()
                    {
                        psCode = data.psCode,
                        scmCode = data.scmCode,
                        scmName = getSchema.scmName,
                        isAutomaticMemberStatus = getSchema.isAutomaticMemberStatus,
                        memberCode = data.memberCode,
                        parentMemberCode = data.parentMemberCode,
                        parentMemberName = data.parentMemberName,
                        specCode = data.specCode,
                        CDCode = data.CDCode,
                        ACDCode = data.ACDCode,
                        PTName = data.PTName,
                        PrincName = data.PrincName,
                        spouName = data.spouName,
                        remarks1 = data.remarks1,
                        isCD = data.isCD,
                        isACD = data.isACD,
                        isPKP = data.isPKP,
                        isInstitusi = data.isInstitusi,
                        franchiseGroup = data.franchiseGroup,

                    };

                    var memberActivation = new GetMemberActivationDto()
                    {
                        memberStatusCode = data.memberStatusCode,
                        isMember = data.isMember,
                        isActive = data.isActive,
                        password = data.password
                    };

                    var memberBankData = new GetMemberBankDataDto()
                    {
                        bankType = data.bankType,
                        bankCode = data.bankCode,
                        bankAccNo = data.bankAccNo,
                        bankAccName = data.bankAccName,
                        bankBranchName = data.bankBranchName
                    };

                    var member = new GetMemberDto()
                    {
                        memberData = memberData,
                        memberActivation = memberActivation,
                        memberBankData = memberBankData
                    };

                    var updateInfo = new GetUpdateInfo()
                    {
                        updateBy = data.updatedBy != 0 ? data.updatedBy.ToString() : data.creatorUserId.ToString(),
                        updateTime = data.updatedTime != null ? DateTime.Parse(data.updatedTime.ToString()) : DateTime.Parse(data.createdTime.ToString())
                    };
                    member.UpdateInfo = updateInfo;

                    members.Add(member);
                }
                */
                #endregion

                var px = _personalRepo.GetAll().Join(_personalsMemberRepo.GetAll(), x => x.psCode, y => y.psCode, (query_personalRepo, query_personalMemberRepo) => new { query_personalRepo, query_personalMemberRepo })
                .Where(s => s.query_personalRepo.psCode.Equals(s.query_personalMemberRepo.psCode));

                var result = (from x in px
                              where x.query_personalRepo.psCode.Equals(psCode)
                              select new GetMemberAllDto
                              {
                                  entityCode = entityCode,
                                  psCode = x.query_personalMemberRepo.psCode,
                                  scmCode = x.query_personalMemberRepo.scmCode,
                                  memberCode = x.query_personalMemberRepo.memberCode,
                                  parentMemberCode = x.query_personalMemberRepo.parentMemberCode,
                                  parentMemberName = (from z in px
                                                      where z.query_personalMemberRepo.memberCode.Equals(x.query_personalMemberRepo.parentMemberCode)
                                                      select z.query_personalRepo.name).FirstOrDefault(),
                                  specCode = x.query_personalMemberRepo.specCode,
                                  CDCode = x.query_personalMemberRepo.CDCode,
                                  ACDCode = x.query_personalMemberRepo.ACDCode,
                                  PTName = x.query_personalMemberRepo.PTName,
                                  PrincName = x.query_personalMemberRepo.PrincName,
                                  spouName = x.query_personalMemberRepo.spouName,
                                  remarks1 = x.query_personalMemberRepo.remarks1,
                                  isCD = x.query_personalMemberRepo.isCD,
                                  isACD = x.query_personalMemberRepo.isACD,
                                  isInstitusi = x.query_personalMemberRepo.isInstitusi,
                                  isPKP = x.query_personalMemberRepo.isPKP,
                                  franchiseGroup = x.query_personalMemberRepo.franchiseGroup,

                                  memberStatusCode = x.query_personalMemberRepo.memberStatusCode,
                                  isMember = x.query_personalMemberRepo.isMember,
                                  isActive = x.query_personalMemberRepo.isActive,
                                  password = x.query_personalMemberRepo.password,

                                  bankType = x.query_personalMemberRepo.bankType,
                                  bankCode = x.query_personalMemberRepo.bankCode,
                                  bankAccNo = x.query_personalMemberRepo.bankAccNo,
                                  bankAccName = x.query_personalMemberRepo.bankAccName,
                                  bankBranchName = x.query_personalMemberRepo.bankBranchName,
                                  bankAccountRefID = x.query_personalMemberRepo.bankAccountRefID,

                                  createdTime = x.query_personalMemberRepo.CreationTime,
                                  creatorUserId = x.query_personalMemberRepo.CreatorUserId,
                                  updatedBy = x.query_personalMemberRepo.LastModifierUserId,
                                  updatedTime = x.query_personalMemberRepo.LastModificationTime
                              }).ToList();
                
                foreach (var data in result)
                {
                    var getSchema = (from x in _schemaRepo.GetAll()
                                     where x.entityCode == entityCode && x.scmCode == data.scmCode
                                     select new
                                     {
                                         scmName = x.scmName,
                                         isAutomaticMemberStatus = x.isAutomaticMemberStatus
                                     }).FirstOrDefault();

                    var memberData = new GetMemberDataDto() //disini
                    {
                        psCode = data.psCode == null ? null : data.psCode,
                        scmCode = data.scmCode == null ? null : data.scmCode,
                        scmName = getSchema == null || getSchema.scmName == null ? null : getSchema.scmName,
                        isAutomaticMemberStatus = getSchema == null ? false : getSchema.isAutomaticMemberStatus,
                        memberCode = data.memberCode == null ? null : data.memberCode,
                        parentMemberCode = data.parentMemberCode == null ? null : data.parentMemberCode,
                        parentMemberName = data.parentMemberName == null ? null : data.parentMemberName,
                        specCode = data.specCode == null ? null : data.specCode,
                        CDCode = data.CDCode == null ? null : data.CDCode,
                        ACDCode = data.ACDCode == null ? null : data.ACDCode,
                        PTName = data.PTName == null ? null : data.PTName,
                        PrincName = data.PrincName == null ? null : data.PrincName,
                        spouName = data.spouName == null ? null : data.spouName,
                        remarks1 = data.remarks1 == null ? null : data.remarks1,
                        isCD = data.isCD,
                        isACD = data.isACD,
                        isPKP = data.isPKP,
                        isInstitusi = data.isInstitusi,
                        franchiseGroup = data.franchiseGroup == null ? null : data.franchiseGroup,

                    };

                    var memberActivation = new GetMemberActivationDto()
                    {
                        memberStatusCode = data.memberStatusCode == null ? null : data.memberStatusCode,
                        isMember = data.isMember,
                        isActive = data.isActive,
                        password = data.password
                    };

                    var memberBankData = new GetMemberBankDataDto()
                    {
                        bankType = data.bankType == null ? null : data.bankType,
                        bankCode = data.bankCode == null ? null : data.bankCode,
                        bankAccNo = data.bankAccNo == null ? null : data.bankAccNo,
                        bankAccName = data.bankAccName == null ? null : data.bankAccName,
                        bankBranchName = data.bankBranchName == null ? null : data.bankBranchName,
                        bankAccountRefID = data.bankAccountRefID
                    };

                    var member = new GetMemberDto()
                    {
                        memberData = memberData,
                        memberActivation = memberActivation,
                        memberBankData = memberBankData
                    };

                    var updateInfo = new GetUpdateInfo()
                    {
                        updateBy = data.updatedBy != 0 ? data.updatedBy.ToString() : data.creatorUserId.ToString(),
                        updateTime = data.updatedTime != null ? DateTime.Parse(data.updatedTime.ToString()) : DateTime.Parse(data.createdTime.ToString())
                    };
                    member.UpdateInfo = updateInfo;

                    members.Add(member);
                }
            }
            catch (Exception e)
            {
                SendConsole("" + e.Message + " " + e.StackTrace);
            }

            return members;
        }
        #endregion

        #region function getContact : getPhone, getEmail, getAddress
        public List<GetPhoneDto> GetPhoneByPsCode(string psCode)
        {
            var result = (from x in _phoneRepo.GetAll()
                          where x.psCode.Equals(psCode)
                          orderby x.refID ascending
                          select new GetPhoneDto
                          {
                              entityCode = x.entityCode,
                              psCode = x.psCode,
                              refID = x.refID,
                              phoneType = x.phoneType,
                              number = x.number,
                              LastModificationTime = x.LastModificationTime != null ? x.LastModificationTime.ToString() : null,
                              LastModifierUserId = x.LastModifierUserId != null ? x.LastModifierUserId.ToString() : null,
                              CreationTime = x.CreationTime.ToString(),
                              CreatorUserId = x.CreatorUserId.ToString()

                          }).ToList();
            return new List<GetPhoneDto>(result);
        }

        public List<GetEmailDto> GetEmailByPsCode(string psCode)
        {
            var validEmail = (from x in _emailRepo.GetAll()
                              where x.psCode.Equals(psCode)
                              orderby x.refID ascending
                              select new GetEmailDto
                              {
                                  entityCode = x.entityCode,
                                  psCode = x.psCode,
                                  refID = x.refID,
                                  email = x.email,
                                  isValid = true, // valid email,
                                  CreatedBy = x.CreatorUserId.ToString(),
                                  CreateTime = x.CreationTime.ToString(),
                                  LastModificationBy = x.LastModifierUserId != null ? x.LastModifierUserId.ToString() : null,
                                  LastModificationTime = x.LastModificationTime != null ? x.LastModificationTime.ToString() : null
                              }).ToList();

            var result = (from x in _trEmailInvalidRepo.GetAll()
                          where x.psCode.Equals(psCode)
                          orderby x.refID ascending
                          select new GetEmailDto
                          {
                              entityCode = x.entityCode,
                              psCode = x.psCode,
                              refID = x.refID,
                              email = x.email,
                              isValid = false, // invalid email
                              LastModificationTime = x.LastModificationTime != null ? x.LastModificationTime.ToString() : null,
                              LastModificationBy = x.LastModifierUserId != null ? x.LastModifierUserId.ToString() : null,
                              CreatedBy = x.CreatorUserId.ToString(),
                              CreateTime = x.CreationTime.ToString()
                          }).ToList();

            result.AddRange(validEmail);

            return new List<GetEmailDto>(result);
        }

        #endregion

        #region function Update Personal
        public async Task UpdatePersonal(CreatePersonalDto input)
        {
            Logger.Info("UpdatePersonal() - Started.");

            Logger.DebugFormat("UpdatePersonal() - Start get data before update Personal. Parameters sent:{0}" +
                        "psCode = {1}{0}"
                        , Environment.NewLine, input.psCode);
            var getPersonal = (from x in _personalRepo.GetAll()
                               where x.entityCode == entityCode
                               && x.psCode == input.psCode
                               select x).FirstOrDefault();
            Logger.DebugFormat("UpdatePersonal() - Ended get data before update Personal.");

            var personal = getPersonal.MapTo<PERSONALS>();

            getPersonal.psCode = input.psCode;
            getPersonal.name = input.name;
            getPersonal.sex = input.sex;
            getPersonal.birthDate = input.birthDate;
            getPersonal.birthPlace = input.birthPlace;
            getPersonal.marCode = String.IsNullOrEmpty(input.marCode) ? "0" : input.marCode;
            getPersonal.relCode = String.IsNullOrEmpty(input.relCode) ? "0" : input.relCode;
            getPersonal.bloodCode = String.IsNullOrEmpty(input.bloodCode) ? "0" : input.bloodCode;
            getPersonal.occID = String.IsNullOrEmpty(input.occID) ? "001" : input.occID;
            getPersonal.NPWP = input.npwp;
            getPersonal.nationID = input.nationID;
            getPersonal.familyStatus = String.IsNullOrEmpty(input.familyStatus) ? "0" : input.familyStatus;
            getPersonal.FPTransCode = String.IsNullOrEmpty(input.FPTransCode) ? "0" : input.FPTransCode;
            getPersonal.grade = String.IsNullOrEmpty(input.grade) ? "0" : input.grade;
            getPersonal.isActive = input.isActive;
            getPersonal.remarks = input.remarks;
            getPersonal.isInstitute = input.isInstitute;

            try
            {
                Logger.DebugFormat("UpdatePersonal() - Start update Personal. Parameters sent:{0}" +
                        "psCode            = {1}{0}" +
                        "name              = {2}{0}" +
                        "sex               = {3}{0}" +
                        "birthDate         = {4}{0}" +
                        "birthPlace        = {5}{0}" +
                        "marCode           = {6}{0}" +
                        "relCode           = {7}{0}" +
                        "bloodCode         = {8}{0}" +
                        "occID             = {9}{0}" +
                        "NPWP              = {10}{0}" +
                        "nationID          = {11}{0}" +
                        "familyStatus      = {12}{0}" +
                        "FPTransCode       = {13}{0}" +
                        "grade             = {14}{0}" +
                        "isActive          = {15}{0}" +
                        "remarks           = {16}{0}" +
                        "isInstitute       = {17}{0}"
                        , Environment.NewLine, input.psCode, input.name
                        , input.sex, input.birthDate, input.birthPlace, input.marCode, input.relCode, input.bloodCode
                        , input.occID, input.npwp, input.nationID, input.familyStatus, input.FPTransCode, input.grade
                        , input.isActive, input.remarks, input.isInstitute);

                await _personalRepo.UpdateAsync(personal);

                CurrentUnitOfWork.SaveChanges();
                Logger.DebugFormat("UpdatePersonal() - Ended update Personal.");
            }
            catch (DataException ex)
            {
                Logger.ErrorFormat("UpdatePersonal() - ERROR DataException. Result = {0}", ex.Message);
                throw new UserFriendlyException("Db Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                Logger.ErrorFormat("UpdatePersonal() - ERROR Exception. Result = {0}", ex.Message);
                throw new UserFriendlyException("Error: " + ex.Message);
            }
            Logger.Info("UpdatePersonal() - Finished.");
        }
        #endregion

        #region function Create Or Update Contact : CreateOrUpdatePhone, CreateOrUpdateEmail, CreateOrUpdateAddress        

        public async Task CreateOrUpdatePhone(List<CreatePhoneDto> inputs)
        {
            Logger.Info("CreateOrUpdatePhone() - Started.");

            Logger.Info("Check duplicate phone number - Started.");
            var isDuplicatePhone = inputs.GroupBy(n => n.number).Any(c => c.Count() > 1);
            if (isDuplicatePhone)
            {
                throw new UserFriendlyException("Duplicate Phone Number");
            }
            Logger.Info("Check duplicate phone number - End.");

            foreach (var input in inputs)
            {
                Logger.DebugFormat("CreateOrUpdatePhone() - Start get data to know insert or update Phone. Parameters sent:{0}" +
                        "entityCode = {1}{0}" +
                        "psCode = {2}{0}" +
                        "refID = {3}{0}"
                        , Environment.NewLine, entityCode, input.psCode, input.refID);

                var isThere = (from x in _phoneRepo.GetAll()
                               where x.entityCode == entityCode
                               && x.psCode.Equals(input.psCode)
                               && x.refID.Equals(input.refID)
                               select x).Count();

                Logger.DebugFormat("CreateOrUpdatePhone() - Ended get data to know insert or update Phone.");

                TR_Phone phone = new TR_Phone()
                {
                    entityCode = entityCode,
                    psCode = input.psCode,
                    refID = input.refID,
                    phoneType = input.phoneType,
                    number = input.number,
                    remarks = input.remarks
                };

                if (isThere == 0)
                {
                    try
                    {
                        Logger.DebugFormat("CreateOrUpdatePhone() - Start insert Phone. Parameters sent:{0}" +
                            "entityCode = {1}{0}" +
                            "psCode = {2}{0}" +
                            "refID = {3}{0}" +
                            "phoneType = {4}{0}" +
                            "number = {5}{0}" +
                            "remarks = {6}{0}"
                            , Environment.NewLine, entityCode, input.psCode, input.refID
                            , input.phoneType, input.number, input.remarks);

                        var checkAvailablePhone = (from x in _phoneRepo.GetAll()
                                                   where x.number == input.number
                                                   select x).Any();

                        if (checkAvailablePhone)
                        {
                            throw new UserFriendlyException(input.number + "is exist!");
                        }

                        await _phoneRepo.InsertAsync(phone);
                        Logger.DebugFormat("CreateOrUpdatePhone() - Ended insert Phone.");
                    }
                    catch (DataException ex)
                    {
                        Logger.ErrorFormat("CreateOrUpdatePhone() - ERROR DataException. Result = {0}", ex.Message);
                        throw new UserFriendlyException("Db Error: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Logger.ErrorFormat("CreateOrUpdatePhone() - ERROR Exception. Result = {0}", ex.Message);
                        throw new UserFriendlyException("Error: " + ex.Message);
                    }
                }
                else
                {
                    try
                    {
                        Logger.DebugFormat("CreateOrUpdatePhone() - Start update Phone. Parameters sent:{0}" +
                            "entityCode = {1}{0}" +
                            "psCode = {2}{0}" +
                            "refID = {3}{0}" +
                            "phoneType = {4}{0}" +
                            "number = {5}{0}" +
                            "remarks = {6}{0}"
                            , Environment.NewLine, entityCode, input.psCode, input.refID
                            , input.phoneType, input.number, input.remarks);

                        var checkAvailablePhone = (from x in _phoneRepo.GetAll()
                                                   where x.psCode != input.psCode && x.refID != input.refID && x.number == input.number
                                                   select x).Any();

                        if (checkAvailablePhone)
                        {
                            throw new UserFriendlyException(input.number + "is exist!");
                        }

                        await _phoneRepo.UpdateAsync(phone);
                        Logger.DebugFormat("CreateOrUpdatePhone() - Ended update Phone.");
                    }
                    catch (DataException ex)
                    {
                        Logger.ErrorFormat("CreateOrUpdatePhone() - ERROR DataException. Result = {0}", ex.Message);
                        throw new UserFriendlyException("Db Error: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Logger.ErrorFormat("CreateOrUpdatePhone() - ERROR Exception. Result = {0}", ex.Message);
                        throw new UserFriendlyException("Error: " + ex.Message);
                    }

                }
            }
            Logger.Info("CreateOrUpdatePhone() - Finished.");
        }

        public async Task CreateOrUpdateEmail(List<CreateEmailDto> inputs)
        {
            Logger.Info("CreateOrUpdateEmail() - Started.");

            Logger.Info("Check duplicate email - Started.");
            var isDuplicateEmail = inputs.GroupBy(n => n.email).Any(c => c.Count() > 1);
            if (isDuplicateEmail)
            {
                throw new UserFriendlyException("Duplicate email data.");
            }
            Logger.Info("Check duplicate email - End.");

            foreach (var input in inputs)
            {
                if (input.isValid)
                {
                    Logger.DebugFormat("CreateOrUpdateEmail() - Start get data to know insert or update Phone. Parameters sent:{0}" +
                        "entityCode = {1}{0}" +
                        "psCode = {2}{0}" +
                        "refID = {3}{0}"
                        , Environment.NewLine, entityCode, input.psCode, input.refID);

                    var isThere = (from x in _emailRepo.GetAll()
                                   where x.entityCode == entityCode
                                   && x.psCode.Equals(input.psCode)
                                   && x.refID.Equals(input.refID)
                                   select x).Count();

                    Logger.DebugFormat("CreateOrUpdateEmail() - Ended get data to know insert or update Phone.");

                    TR_Email email = new TR_Email()
                    {
                        entityCode = entityCode,
                        psCode = input.psCode,
                        refID = input.refID,
                        email = input.email
                    };

                    if (isThere == 0)
                    {
                        try
                        {
                            Logger.DebugFormat("CreateOrUpdateEmail() - Start insert Email. Parameters sent:{0}" +
                                "entityCode = {1}{0}" +
                                "psCode = {2}{0}" +
                                "refID = {3}{0}" +
                                "email = {4}{0}"
                                , Environment.NewLine, entityCode, input.psCode, input.refID
                                , input.email);

                            var checkAvailableEmail = (from x in _emailRepo.GetAll()
                                                       where x.email == input.email
                                                       select x).Any();

                            if (checkAvailableEmail)
                            {
                                throw new UserFriendlyException(input.email + "is exist!");
                            }

                            await _emailRepo.InsertAsync(email);
                            Logger.DebugFormat("CreateOrUpdateEmail() - Ended insert Email.");
                        }
                        catch (DataException ex)
                        {
                            Logger.ErrorFormat("CreateOrUpdateEmail() - ERROR DataException. Result = {0}", ex.Message);
                            throw new UserFriendlyException("Db Error: " + ex.Message);
                        }
                        catch (Exception ex)
                        {
                            Logger.ErrorFormat("CreateOrUpdateEmail() - ERROR Exception. Result = {0}", ex.Message);
                            throw new UserFriendlyException("Error: " + ex.Message);
                        }
                    }
                    else
                    {
                        try
                        {
                            Logger.DebugFormat("CreateOrUpdateEmail() - Start update Email. Parameters sent:{0}" +
                                "entityCode = {1}{0}" +
                                "psCode = {2}{0}" +
                                "refID = {3}{0}" +
                                "email = {4}{0}"
                                , Environment.NewLine, entityCode, input.psCode, input.refID
                                , input.email);

                            var checkAvailableEmail = (from x in _emailRepo.GetAll()
                                                       where x.psCode != input.psCode && x.refID != input.refID && x.email == input.email
                                                       select x).Any();

                            if (checkAvailableEmail)
                            {
                                throw new UserFriendlyException(input.email + "is exist!");
                            }

                            await _emailRepo.UpdateAsync(email);
                            Logger.DebugFormat("CreateOrUpdateEmail() - Ended update Email.");
                        }
                        catch (DataException ex)
                        {
                            Logger.ErrorFormat("CreateOrUpdateEmail() - ERROR DataException. Result = {0}", ex.Message);
                            throw new UserFriendlyException("Db Error: " + ex.Message);
                        }
                        catch (Exception ex)
                        {
                            Logger.ErrorFormat("CreateOrUpdateEmail() - ERROR Exception. Result = {0}", ex.Message);
                            throw new UserFriendlyException("Error: " + ex.Message);
                        }
                    }
                }
                else
                {
                    Logger.DebugFormat("CreateOrUpdateEmail() - Start get data to know insert or update Email. Parameters sent:{0}" +
                        "entityCode = {1}{0}" +
                        "psCode = {2}{0}" +
                        "refID = {3}{0}" +
                        "email = {4}{0}"
                        , Environment.NewLine, entityCode, input.psCode, input.refID, input.email);

                    var isThere = (from x in _trEmailInvalidRepo.GetAll()
                                   where x.entityCode == entityCode
                                   && x.psCode.Equals(input.psCode)
                                   && x.refID.Equals(input.refID)
                                   && x.email.Equals(input.email)
                                   select x).Count();

                    Logger.DebugFormat("CreateOrUpdateEmail() - Ended get data to know insert or update Email.");


                    TR_EmailInvalid email = new TR_EmailInvalid()
                    {
                        entityCode = entityCode,
                        psCode = input.psCode,
                        refID = input.refID,
                        email = input.email
                    };

                    if (isThere == 0)
                    {
                        try
                        {
                            Logger.DebugFormat("CreateOrUpdateEmail() - Start insert Email Invalid. Parameters sent:{0}" +
                                "entityCode = {1}{0}" +
                                "psCode = {2}{0}" +
                                "refID = {3}{0}" +
                                "email = {4}{0}"
                                , Environment.NewLine, entityCode, input.psCode, input.refID
                                , input.email);

                            var checkAvailableEmail = (from x in _trEmailInvalidRepo.GetAll()
                                                       where x.email == input.email
                                                       select x).Any();

                            if (checkAvailableEmail)
                            {
                                throw new UserFriendlyException(input.email + "is exist!");
                            }

                            await _trEmailInvalidRepo.InsertAsync(email);

                            Logger.DebugFormat("CreateOrUpdateEmail() - Ended insert Email Invalid.");

                            Logger.DebugFormat("CreateOrUpdateEmail() - Start get data before delete Email. Parameters sent:{0}" +
                                "entityCode = {1}{0}" +
                                "psCode = {2}{0}" +
                                "refID = {3}{0}" +
                                "email = {4}{0}"
                                , Environment.NewLine, entityCode, input.psCode, input.refID, input.email);
                            var emailToDelete = (from x in _emailRepo.GetAll()
                                                 where x.entityCode == entityCode
                                                 && x.psCode.Equals(input.psCode)
                                                 && x.refID.Equals(input.refID)
                                                 && x.email.Equals(input.email)
                                                 select x).FirstOrDefault();

                            Logger.DebugFormat("CreateOrUpdateEmail() - Ended get data before delete Email.");

                            Logger.DebugFormat("CreateOrUpdateEmail() - Start delete Email. Parameters sent:{0}" +
                                "psCode = {1}{0}" +
                                "refID = {2}{0}" +
                                "email = {3}{0}"
                                , Environment.NewLine, emailToDelete.psCode, emailToDelete.refID, emailToDelete.email);
                            await _emailRepo.DeleteAsync(emailToDelete);
                            Logger.DebugFormat("CreateOrUpdateEmail() - Ended delete Email.");

                        }
                        catch (DataException ex)
                        {
                            Logger.ErrorFormat("CreateOrUpdateEmail() - ERROR DataException. Result = {0}", ex.Message);
                            throw new UserFriendlyException("Db Error: " + ex.Message);
                        }
                        catch (Exception ex)
                        {
                            Logger.ErrorFormat("CreateOrUpdateEmail() - ERROR Exception. Result = {0}", ex.Message);
                            throw new UserFriendlyException("Error: " + ex.Message);
                        }
                    }
                    else
                    {
                        try
                        {
                            Logger.DebugFormat("CreateOrUpdateEmail() - Start update Email Invalid. Parameters sent:{0}" +
                                "entityCode = {1}{0}" +
                                "psCode = {2}{0}" +
                                "refID = {3}{0}" +
                                "email = {4}{0}"
                                , Environment.NewLine, entityCode, input.psCode, input.refID
                                , input.email);

                            await _trEmailInvalidRepo.UpdateAsync(email);
                            Logger.DebugFormat("CreateOrUpdateEmail() - Ended update Email Invalid.");
                        }
                        catch (DataException ex)
                        {
                            Logger.ErrorFormat("CreateOrUpdateEmail() - ERROR DataException. Result = {0}", ex.Message);
                            throw new UserFriendlyException("Db Error: " + ex.Message);
                        }
                        catch (Exception ex)
                        {
                            Logger.ErrorFormat("CreateOrUpdateEmail() - ERROR Exception. Result = {0}", ex.Message);
                            throw new UserFriendlyException("Error: " + ex.Message);
                        }
                    }
                }
            }
            Logger.Info("CreateOrUpdateEmail() - Finished.");
        }

        public async Task CreateOrUpdateAddress(List<CreateAddressDto> inputs)
        {
            Logger.Info("CreateOrUpdateAddress() - Started.");
            foreach (var input in inputs)
            {
                Logger.DebugFormat("CreateOrUpdateAddress() - Start get data to know insert or update Address. Parameters sent:{0}" +
                        "entityCode = {1}{0}" +
                        "psCode = {2}{0}" +
                        "refID = {3}{0}" +
                        "addrType = {4}{0}"
                        , Environment.NewLine, entityCode, input.psCode, input.refID, input.addrType);
                var getAddress = (from x in _addressRepo.GetAll()
                                  where x.entityCode == entityCode
                                  && x.psCode.Equals(input.psCode)
                                  && x.refID.Equals(input.refID)
                                  && x.addrType.Equals(input.addrType)
                                  select x);
                Logger.DebugFormat("CreateOrUpdateAddress() - Ended to know insert or update Address.");

                if (getAddress.Any())
                {
                    SendConsole("getAddress any");
                    var address = getAddress.FirstOrDefault().MapTo<TR_Address>();

                    address.address = input.address;
                    address.postCode = input.postCode;
                    address.city = input.city;
                    address.country = input.country;
                    address.Kelurahan = input.Kelurahan;
                    address.Kecamatan = input.Kecamatan;
                    address.province = input.province;

                    try
                    {
                        Logger.DebugFormat("CreateOrUpdateAddress() - Start update Address. Parameters sent:{0}" +
                            "address = {1}{0}" +
                            "postCode = {2}{0}" +
                            "city = {3}{0}" +
                            "country = {4}{0}" +
                            "Kelurahan = {5}{0}" +
                            "Kecamatan = {6}{0}" +
                            "province = {7}{0}"
                            , Environment.NewLine, input.address, input.postCode, input.city
                            , input.country, input.Kelurahan, input.Kecamatan, input.province);
                        await _addressRepo.UpdateAsync(address);
                        Logger.DebugFormat("CreateOrUpdateAddress() - Ended update Address.");
                    }
                    catch (DataException ex)
                    {
                        Logger.ErrorFormat("CreateOrUpdateAddress() - ERROR DataException. Result = {0}", ex.Message);
                        throw new UserFriendlyException("Db Error: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Logger.ErrorFormat("CreateOrUpdateAddress() - ERROR Exception. Result = {0}", ex.Message);
                        throw new UserFriendlyException("Error: " + ex.Message);
                    }
                }
                else
                {
                    SendConsole("getAddress not");
                    var address = new TR_Address
                    {
                        psCode = input.psCode,
                        refID = input.refID,
                        addrType = input.addrType,
                        entityCode = entityCode,
                        address = input.address,
                        postCode = input.postCode,
                        city = input.city,
                        country = input.country,
                        Kelurahan = input.Kelurahan,
                        Kecamatan = input.Kecamatan,
                        province = input.province
                    };

                    try
                    {
                        Logger.DebugFormat("CreateOrUpdateAddress() - Start insert Address. Parameters sent:{0}" +
                            "psCode     = {1}{0}" +
                            "refID      = {2}{0}" +
                            "addrType   = {3}{0}" +
                            "entityCode = {4}{0}" +
                            "address    = {5}{0}" +
                            "postCode   = {6}{0}" +
                            "city       = {7}{0}" +
                            "country    = {8}{0}" +
                            "Kelurahan  = {9}{0}" +
                            "Kecamatan  = {10}{0}" +
                            "province   = {11}{0}"
                            , Environment.NewLine, input.psCode, input.refID, input.address, input.addrType, entityCode
                            , input.postCode, input.city, input.country, input.Kelurahan, input.Kecamatan, input.province);
                        await _addressRepo.InsertAsync(address);
                        Logger.DebugFormat("CreateOrUpdateAddress() - Ended insert Address.");
                    }
                    catch (DataException ex)
                    {
                        Logger.ErrorFormat("CreateOrUpdateAddress() - ERROR DataException. Result = {0}", ex.Message);
                        throw new UserFriendlyException("Db Error: " + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Logger.ErrorFormat("CreateOrUpdateAddress() - ERROR Exception. Result = {0}", ex.Message);
                        throw new UserFriendlyException("Error: " + ex.Message);
                    }
                }
            }

            Logger.Info("CreateOrUpdateAddress() - Finished.");
        }
        #endregion

        #region function helper : generatePsCode, UploadImage

        public JObject generatePsCode()
        {
            JObject obj = new JObject();
            string psCode = null;
            bool checkAvailablePsCode = false;

            try
            {
                do
                {
                    var query = @"update SYS_Counter 
                        set	  psCode = (Select right('000000'+convert(varchar(6),
                                            (Select convert(integer,convert(varchar(6),max(pscode)))+1 
                                                from SYS_Counter where entityCode = 1))+ 
                                            right(convert(varchar(12),convert(integer,round( 
                                                (((((convert(float,(Select convert(integer,convert(varchar(6),max(pscode)))+1 
                        from    SYS_Counter (nolock)))+76)*73)*80)-80)/79),0))),2),8)) 
                        select  psCode from SYS_Counter";

                    psCode = _sqlExecuter.GetFromPersonals<string>(query).FirstOrDefault();

                    checkAvailablePsCode = (from x in _personalRepo.GetAll()
                                            where x.entityCode == entityCode
                                            && x.psCode == psCode
                                            select x.psCode).Any();

                } while (checkAvailablePsCode);
            }
            catch (Exception e)
            {
                SendConsole("" + e.Message + " " + e.StackTrace);
            }
            obj.Add("pscode", psCode);

            return obj;
        }

        #region msschema
        public ListResultDto<GetAllMsSchemaDropdownList> GetAllMsSchemaDropdown()
        {
            var result = (from x in _msSchemaRepo.GetAll()
                          select new GetAllMsSchemaDropdownList
                          {
                              scmCode = x.scmCode,
                              scmName = x.scmName,
                              isAutomaticMemberStatus = x.isAutomaticMemberStatus
                          }).ToList();

            return new ListResultDto<GetAllMsSchemaDropdownList>(result);
        }

        public GetAvailableMemberSchemaByScmCodeAndPsCode GetAvailableMemberSchemaByScmCodeAndPsCode(GetAvailableMemberSchemaByScmCodeAndPsCode input)
        {
            var getMember = (from x in _personalMemberRepo.GetAll()
                             where x.psCode == input.psCode
                             && x.scmCode == input.scmCode
                             && x.isMember == true
                             select new GetAvailableMemberSchemaByScmCodeAndPsCode
                             {
                                 scmCode = x.scmCode,
                                 memberCode = x.memberCode,
                                 psCode = x.psCode
                             }).FirstOrDefault();

            return getMember;
        }
        #endregion

        private string UploadFile(string filename)
        {

            try
            {
                return _filesHelper.MoveFiles(filename, @"Temp\Downloads\PersonalFile\", @"Assets\Upload\PersonalFile\");
            }
            catch (Exception ex)
            {
                Logger.DebugFormat("test() - ERROR Exception. Result = {0}", ex.Message);
                throw new UserFriendlyException("Error : {0}", ex.Message);
            }
        }
        private void GetURLWithoutHost(string path, out string finalpath)
        {
            finalpath = path;
            try
            {
                Regex RegexObj = new Regex("[\\w\\W]*([\\/]Assets[\\w\\W\\s]*)");
                if (RegexObj.IsMatch(path))
                {
                    finalpath = RegexObj.Match(path).Groups[1].Value;
                }
            }
            catch (ArgumentException ex)
            {
            }
        }

        private string GetFileNameFromUrl(string path)
        {            
            if (string.IsNullOrEmpty(path))
                return path;
            path = path.Replace(@"\", "/");
            path = path.Substring(path.LastIndexOf(@"/") + 1);
            return path;
        }

        private string GetURLWithoutHost(string path)
        {            
            string finalpath = path;
            if (string.IsNullOrEmpty(path))
                return finalpath;

            path = path.Replace(@"\","/");
            if (!path[0].Equals('/'))
                path = "/" + path;

            try
            {
                Regex RegexObj = new Regex("[\\w\\W]*([\\/]Assets[\\w\\W\\s]*)");
                if (RegexObj.IsMatch(path))
                {
                    finalpath = RegexObj.Match(path).Groups[1].Value;
                }
            }
            catch (ArgumentException ex)
            {
            }
            return finalpath;
        }

        private string getAbsoluteUriWithoutTail()
        {
            var request = _httpContextAccessor.HttpContext.Request;
            UriBuilder uriBuilder = new UriBuilder();
            uriBuilder.Scheme = request.Scheme;
            uriBuilder.Host = request.Host.ToString();            
            var test = uriBuilder.ToString();
            var result = test.Replace("[", "").Replace("]", "");            
            int position = result.LastIndexOf('/');
            if (position > -1)
                result = result.Substring(0, result.Length - 1);

            if (request.PathBase != null)
            {
                if (!string.IsNullOrWhiteSpace(request.PathBase.Value))
                {
                    result += request.PathBase.Value;
                }
            }
            return result;
        }
        #endregion


        #region debug console
        protected void SendConsole(string msg)
        {
            if (Setting_variabel.enable_tcp_debug == true)
            {
                if (Setting_variabel.Komunikasi_TCPListener == null)
                {
                    Setting_variabel.Komunikasi_TCPListener = new Visionet_Backend_NetCore.Komunikasi.Komunikasi_TCPListener(17000);
                    Task.Run(() => StartListenerLokal());
                }

                if (Setting_variabel.Komunikasi_TCPListener != null)
                {
                    if (Setting_variabel.Komunikasi_TCPListener.IsRunning())
                    {
                        if (Setting_variabel.ConsoleBayangan != null)
                        {
                            Setting_variabel.ConsoleBayangan.Send("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "] " + msg);
                        }
                    }
                }
            }

        }
        #endregion

        #region listener tcp debug
        protected void StartListenerLokal()
        {
            if (Setting_variabel.Komunikasi_TCPListener != null)
                Setting_variabel.Komunikasi_TCPListener.StartListener();
        }

        protected void StopListenerLokal()
        {
            if (Setting_variabel.Komunikasi_TCPListener != null)
                Setting_variabel.Komunikasi_TCPListener.StopListener();
        }
        #endregion

        public string sendEmailActivationMember(string psCode, string memberCode)
        {
            var getEmailAddress = (from x in _personalRepo.GetAll()
                                   where x.psCode == psCode
                                   join y in _emailRepo.GetAll() on x.psCode equals y.psCode
                                   select new
                                   {
                                       personalName = x.name,
                                       personalEmail = y.email,
                                   }).FirstOrDefault();



            var webRoot = _hostingEnvironment.WebRootPath;
            var file = System.IO.Path.Combine(webRoot, "EmailTemplate/AktivasiMember.html");
            var lippoImg = System.IO.Path.Combine(webRoot, "Assets/lippohomes.jpg");
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(file))
            {
                body = reader.ReadToEnd();
            }
            //body = body.Replace("{logoLippo}", input.projectImage);
            body = body.Replace("{{personalName}}", getEmailAddress.personalName);
            body = body.Replace("{{memberCode}}", memberCode);
            body = body.Replace("{{email}}", getEmailAddress.personalEmail);

            using (MailMessage mailMessage = new MailMessage())
            {

                mailMessage.From = new MailAddress("denykalpar@gmail.com");
                mailMessage.Subject = "Aktivasi Account Member";
                mailMessage.Body = body; //Ini body
                mailMessage.IsBodyHtml = true;
                mailMessage.To.Add(new MailAddress(getEmailAddress.personalEmail));
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                NetworkCred.UserName = "denykalpar@gmail.com";
                NetworkCred.Password = "ikhlas13";
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = NetworkCred;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Port = 587;
                smtp.Send(mailMessage);
            }
            return "Success";
        }

    }
}
