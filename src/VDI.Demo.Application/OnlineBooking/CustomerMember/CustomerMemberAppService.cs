using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.UI;
using System;
using System.Linq;
using System.Collections.Generic;
using Abp.Authorization;
using Dapper;
using Abp.Domain.Uow;
using Abp.AutoMapper;
using System.Threading.Tasks;
using System.Text;
using System.Security.Cryptography;
using VDI.Demo.PersonalsDB;
using VDI.Demo.PropertySystemDB.OnlineBooking.PPOnline;
using VDI.Demo.SqlExecuter;
using VDI.Demo.OnlineBooking.CustomerMember.Dto;
using System.Data;
using VDI.Demo.PropertySystemDB.OnlineBooking.DemoDB;
using VDI.Demo.Authorization.Users;
using VDI.Demo.Authorization.Accounts;
using VDI.Demo.Authorization.Accounts.Dto;
using VDI.Demo.OnlineBooking.Transaction.Dto;
using VDI.Demo.Authorization;

namespace VDI.Demo.OnlineBooking.CustomerMember
{
    //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_CustomerMember)]
    public class CustomerMemberAppService : DemoAppServiceBase, ICustomerMemberAppService
    {
        private readonly IRepository<PERSONALS, string> _personals;
        private readonly IRepository<PERSONALS_MEMBER, string> _personalsMember;
        private readonly IRepository<TR_Phone, string> _trPhone;
        private readonly IRepository<TR_Email, string> _trEmail;
        private readonly IRepository<TR_Document, string> _trDocument;
        private readonly IRepository<TR_ID, string> _trId;
        private readonly IRepository<TR_IDFamily, string> _trIdFamily;
        private readonly IRepository<LK_IDType, string> _lkIdType;
        private readonly IRepository<User, long> _User;
        private readonly ISqlExecuter _sqlExecuter;
        private readonly IRepository<TR_Address, string> _trAddress;
        private readonly IRepository<MS_Nation, string> _msNation;
        private readonly IRepository<LK_Country, string> _lkCountry;
        private readonly IRepository<MS_City, string> _msCity;
        private readonly IRepository<MS_PostCode, string> _msPostCode;
        private readonly IRepository<SYS_Counter, string> _sysCounter;
        private readonly IRepository<MP_UserPersonals> _mpUserPersonals;
        private readonly IRepository<MS_Province, string> _msProvice;
        private readonly IAccountAppService _accountAppService;


        public CustomerMemberAppService(
            IRepository<PERSONALS, string> personals,
            IRepository<PERSONALS_MEMBER, string> personalsMember,
            IRepository<TR_Phone, string> trPhone,
            IRepository<TR_Email, string> trEmail,
            IRepository<TR_Document, string> trDocument,
            IRepository<TR_ID, string> trId,
            IRepository<TR_IDFamily, string> trIdFamily,
            IRepository<LK_IDType, string> lkIdType,
            IRepository<User, long> User,
            ISqlExecuter sqlExecuter,
            IRepository<TR_Address, string> trAddress,
            IRepository<MS_Nation, string> msNation,
            IRepository<LK_Country, string> lkCountry,
            IRepository<MS_City, string> msCity,
            IRepository<MS_PostCode, string> msPostCode,
            IRepository<SYS_Counter, string> sysCounter,
            IRepository<MP_UserPersonals> mpUserPersonals,
            IRepository<MS_Province, string> msProvince,
            IAccountAppService accountAppService
        )
        {
            _personals = personals;
            _personalsMember = personalsMember;
            _trPhone = trPhone;
            _trEmail = trEmail;
            _trDocument = trDocument;
            _trId = trId;
            _trIdFamily = trIdFamily;
            _lkIdType = lkIdType;
            _sqlExecuter = sqlExecuter;
            _trAddress = trAddress;
            _msNation = msNation;
            _lkCountry = lkCountry;
            _msCity = msCity;
            _msPostCode = msPostCode;
            _sysCounter = sysCounter;
            _mpUserPersonals = mpUserPersonals;
            _User = User;
            _accountAppService = accountAppService;
            _msProvice = msProvince;
        }

        [UnitOfWork(isTransactional: false)]
        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_CustomerMember_MemberActivation)]
        public async Task<MemberActivationResultDto> MemberActivation(SignUpMemberInputDto input)
        {
            if (input.password != input.confirmPassword)
            {
                var message = new MemberActivationResultDto
                {
                    message = "Password and Confirm Password is not match"
                };

                return message;
                //throw new UserFriendlyException("Password and Confirm Password is not match");
            }
            else
            {
                var birthdateConv = Convert.ToDateTime(input.birthDate);
                var member = (from a in _personals.GetAll()
                              join b in _personalsMember.GetAll() on a.psCode equals b.psCode
                              join c in _trEmail.GetAll() on b.psCode equals c.psCode
                              where b.memberCode == input.memberCode && c.email == input.email && a.birthDate.Value.Date == birthdateConv
                              select a.psCode);

                if (member.Count() == 0)
                {
                    var message = new MemberActivationResultDto
                    {
                        message = "Member Is Not Exist"
                    };

                    return message;
                    //throw new UserFriendlyException("Member Is Not Exist");
                }
                else
                {
                    var checkIsActive = (from b in _personalsMember.GetAll()
                                         where b.memberCode == input.memberCode
                                         select b).FirstOrDefault();

                    if (checkIsActive.isActive == false)
                    {
                        var getPsCode = member.FirstOrDefault();

                        var getUserPsCode = (from x in _User.GetAll()
                                             join y in _mpUserPersonals.GetAll()
                                             on x.Id equals y.userID
                                             where y.psCode == getPsCode && x.UserName == input.memberCode
                                             select x);

                        if (getUserPsCode.Count() == 1)
                        {
                            var updateIsActive = checkIsActive.MapTo<PERSONALS_MEMBER>();

                            updateIsActive.isActive = true;

                            await _personalsMember.UpdateAsync(updateIsActive);

                            var getData = getUserPsCode.FirstOrDefault();

                            var updatePass = getData.MapTo<User>();

                            updatePass.Password = input.password;

                            await _User.UpdateAsync(updatePass);

                            var message = new MemberActivationResultDto
                            {
                                message = "Member Is Already Active"
                            };
                            return message;
                        }
                        else
                        {
                            var updateIsActive = checkIsActive.MapTo<PERSONALS_MEMBER>();

                            updateIsActive.isActive = true;

                            await _personalsMember.UpdateAsync(updateIsActive);

                            //var getPsCodes = member.FirstOrDefault();
                            //var emailHash = Hash(input.email);
                            //var memberCode = input.memberCode;
                            //var password = input.password;
                            //var email = input.email;

                            //var sql = "INSERT INTO pponline_Ovia..ms_user select ISNULL(max(userref), 0) + 1 as userRef, @memberCode as userName, @password as pass, @emailHash as activationKey, GETDATE() as activeDate, '1' as userType, @getPsCodes as psCode, '' as domainLogin, getdate() as inputTime, @email as inputUN FROM pponline_Ovia..ms_user";

                            //var exec = _sqlExecuter.GetFromPPOnline<string>(sql, new { memberCode, password, emailHash, getPsCodes, email }).FirstOrDefault();

                            var inputAccount = new RegisterMemberInput
                            {
                                membercode = input.memberCode,
                                password = input.password
                            };

                            await _accountAppService.RegisterMember(inputAccount);

                            var getuserID = (from x in _User.GetAll()
                                             where x.UserName == input.memberCode && x.EmailAddress == input.email
                                             select x.Id).FirstOrDefault();

                            var insertUserPersonals = new MP_UserPersonals
                            {
                                psCode = getPsCode,
                                userID = getuserID,
                                userType = 1
                            };
                            _mpUserPersonals.Insert(insertUserPersonals);

                            var message = new MemberActivationResultDto
                            {
                                message = "Member Is Already Active"
                            };
                            return message;
                        }
                    }
                    else
                    {
                        var getPsCode = member.FirstOrDefault();

                        var getUserPsCode = (from x in _User.GetAll()
                                             join y in _mpUserPersonals.GetAll()
                                             on x.Id equals y.userID
                                             where y.psCode == getPsCode && x.UserName == input.memberCode
                                             select x);

                        if (getUserPsCode.Count() == 1)
                        {
                            var message = new MemberActivationResultDto
                            {
                                message = "Member Is Already Active"
                            };
                            return message;
                        }
                        else
                        {
                            var getPsCodes = member.FirstOrDefault();
                            var emailHash = Hash(input.email);
                            var memberCode = input.memberCode;
                            var password = input.password;
                            var email = input.email;

                            //var sql = "INSERT INTO pponline_Ovia..ms_user select ISNULL(max(userref), 0) + 1 as userRef, @memberCode as userName, @password as pass, @emailHash as activationKey, GETDATE() as activeDate, '1' as userType, @getPsCodes as psCode, '' as domainLogin, getdate() as inputTime, @email as inputUN FROM pponline_Ovia..ms_user";

                            //var exec = _sqlExecuter.GetFromPPOnline<string>(sql, new { memberCode, password, emailHash, getPsCodes, email }).FirstOrDefault();

                            var inputAccount = new RegisterMemberInput
                            {
                                membercode = input.memberCode,
                                password = input.password
                            };

                            await _accountAppService.RegisterMember(inputAccount);

                            var getuserID = (from x in _User.GetAll()
                                             where x.UserName == input.memberCode && x.EmailAddress == input.email
                                             select x.Id).FirstOrDefault();

                            var insertUserPersonals = new MP_UserPersonals
                            {
                                psCode = getPsCode,
                                userID = getuserID,
                                userType = 1
                            };
                            _mpUserPersonals.Insert(insertUserPersonals);

                            var message = new MemberActivationResultDto
                            {
                                message = "Member Is Already Active"
                            };
                            return message;
                        }
                    }
                }
            }
        }

        public string Hash(string input)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    if (sb.Length > 0)
                    {
                        sb.Append("-");
                    }

                    sb.Append(b.ToString("X2"));
                }

                return sb.ToString();
            }
        }

        [UnitOfWork(isTransactional: false)]
        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_CustomerMember_SignUpCustomer)]
        public async Task<SignUpCustomerResultDto> SignUpCustomer(SignUpCustomerInputDto input)
        {
            var entityCode = "1";
            var birtdateConv = Convert.ToDateTime(input.birthDate);

            var sql = "update  personalUtilities..sys_counter set psCode = (Select right('000000'+convert(varchar(6)," +
                      "(Select convert(integer,convert(varchar(6),max(pscode)))+1 from personalUtilities..sys_counter where entityCode = 1))+ " +
                      "right(convert(varchar(12),convert(integer,round( (((((convert(float,(Select convert(integer,convert(varchar(6)," +
                      "max(pscode)))+1 from personalUtilities..sys_counter (nolock)))+76)*73)*80)-80)/79),0))),2),8))";

            _sqlExecuter.GetFromPersonals<string>(sql).FirstOrDefault();

            var getPsCode = (from a in _sysCounter.GetAll()
                             select a.psCode).FirstOrDefault();

            var idTypeName = "";
            if (input.idType == "1")
            {
                idTypeName = "KTP";
            }
            else if (input.idType == "5")
            {
                idTypeName = "KITAS";
            }
            else if (input.idType == "7")
            {
                idTypeName = "Tanda Daftar Perusahaan";
            }

            if (input.idType != null && input.idNo != null && input.NPWP != null && input.name != null && input.number != null && input.email != null && input.marCode != null && input.birthPlace != null && input.nationID != null && input.birthDate != null)
            {
                var checkIdNo = (from a in _trId.GetAll()
                                 where a.idNo == input.idNo && a.idType == input.idType
                                 select a).Count();
                
                if (checkIdNo != 0)
                {
                    //throw new UserFriendlyException("KTP/KITAS is already exist, Please Check Your KTP!");
                    var messages = new SignUpCustomerResultDto
                    {
                        message = "KTP/KITAS is already exist, Please Check Your KTP!"
                    };

                    return messages;
                }
                else
                {
                    var getIdType = (from b in _lkIdType.GetAll()
                                     where b.idType == input.idType
                                     select b).Count();

                    if (getIdType == 0)
                    {
                        var dataPersonals = new PERSONALS
                        {
                            entityCode = entityCode,
                            psCode = getPsCode,
                            parentPSCode = "-",
                            name = input.name,
                            birthDate = birtdateConv,
                            NPWP = input.NPWP,
                            familyStatus = "0",
                            sex = input.sex,
                            birthPlace = input.birthPlace,
                            marCode = input.marCode,
                            relCode = "0",
                            bloodCode = "0",
                            occID = "001",
                            nationID = input.nationID,
                            FPTransCode = "-",
                            grade = "-",
                            isActive = true,
                            mailGroup = "-",
                            remarks = "Online Booking"
                        };

                        await _personals.InsertAsync(dataPersonals);

                        var dataEmail = new TR_Email
                        {
                            entityCode = entityCode,
                            psCode = getPsCode,
                            email = input.email
                        };

                        await _trEmail.InsertAsync(dataEmail);

                        var dataIdFamily = new TR_IDFamily()
                        {
                            psCode = getPsCode,
                            familyRefID = 1,
                            refID = 1,
                            idType = input.idType,
                            idNo = input.idNo
                        };

                        await _trIdFamily.InsertAsync(dataIdFamily);

                        var dataLkIdType = new LK_IDType
                        {
                            idType = input.idType,
                            idTypeName = idTypeName
                        };

                        await _lkIdType.InsertAsync(dataLkIdType);

                        var dataTrId = new TR_ID
                        {
                            entityCode = entityCode,
                            psCode = getPsCode,
                            refID = 1,
                            idType = input.idType,
                            idNo = input.idNo
                        };

                        await _trId.InsertAsync(dataTrId);

                        if (input.idType != "7")
                        {
                            var dataPhone = new TR_Phone
                            {
                                entityCode = entityCode,
                                psCode = getPsCode,
                                refID = 1,
                                phoneType = "3",
                                number = input.number
                            };

                            await _trPhone.InsertAsync(dataPhone);
                        }

                        var dataAddress = new TR_Address
                        {
                            entityCode = entityCode,
                            psCode = getPsCode,
                            refID = 1,
                            addrType = "3",
                            address = input.address,
                            country = input.country,
                            city = input.city,
                            postCode = input.postCode,
                            province = input.province
                        };

                        await _trAddress.InsertAsync(dataAddress);

                        foreach (var item in input.document)
                        {
                            var dtoDocument = new TR_Document
                            {
                                entityCode = entityCode,
                                psCode = getPsCode,
                                documentType = item.documentType,
                                documentBinary = item.documentBinary,
                                documentPicType = "Image",
                                documentRef = 1
                            };

                            await _trDocument.InsertAsync(dtoDocument);
                        }
                    }
                    else
                    {
                        var dataPersonals = new PERSONALS
                        {
                            entityCode = entityCode,
                            psCode = getPsCode,
                            parentPSCode = "-",
                            name = input.name,
                            birthDate = birtdateConv,
                            NPWP = input.NPWP,
                            familyStatus = "0",
                            sex = input.sex,
                            birthPlace = input.birthPlace,
                            marCode = input.marCode,
                            relCode = "0",
                            bloodCode = "0",
                            occID = "001",
                            nationID = input.nationID,
                            FPTransCode = "-",
                            grade = "-",
                            isActive = true,
                            mailGroup = "-",
                            remarks = "Online Booking"
                        };

                        _personals.Insert(dataPersonals);

                        var dataEmail = new TR_Email
                        {
                            entityCode = entityCode,
                            psCode = getPsCode,
                            email = input.email
                        };

                        await _trEmail.InsertAsync(dataEmail);

                        var dataIdFamily = new TR_IDFamily
                        {
                            psCode = getPsCode,
                            familyRefID = 1,
                            refID = 1,
                            idType = input.idType,
                            idNo = input.idNo
                        };

                        await _trIdFamily.InsertAsync(dataIdFamily);

                        var dataTrId = new TR_ID
                        {
                            entityCode = entityCode,
                            psCode = getPsCode,
                            refID = 1,
                            idType = input.idType,
                            idNo = input.idNo
                        };

                        await _trId.InsertAsync(dataTrId);

                        var dataAddress = new TR_Address
                        {
                            entityCode = entityCode,
                            psCode = getPsCode,
                            refID = 1,
                            addrType = "3",
                            address = input.address,
                            country = input.country,
                            city = input.city,
                            postCode = input.postCode,
                            province = input.province
                        };

                        await _trAddress.InsertAsync(dataAddress);

                        if (input.idType != "7")
                        {
                            var dataPhone = new TR_Phone
                            {
                                entityCode = entityCode,
                                psCode = getPsCode,
                                refID = 1,
                                phoneType = "1",
                                number = input.number
                            };

                            await _trPhone.InsertAsync(dataPhone);
                        }

                        foreach (var item in input.document)
                        {
                            var dtoDocument = new TR_Document
                            {
                                entityCode = entityCode,
                                psCode = getPsCode,
                                documentType = item.documentType,
                                documentBinary = item.documentBinary,
                                documentPicType = "Image",
                                documentRef = 1
                            };

                            await _trDocument.InsertAsync(dtoDocument);
                        }
                    }
                }

                var message = new SignUpCustomerResultDto
                {
                    psCode = getPsCode
                };

                return message;

                //return getPsCode;
            }
            else
            {
                //throw new UserFriendlyException("Some field is empty!");

                var message = new SignUpCustomerResultDto
                {
                    message = "Some field is empty!"
                };

                return message;
                //return "Some field is empty!";
            }
        }

        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_CustomerMember_GetListCustomer)]
        public ListResultDto<ListCustomerResultDto> GetListCustomer(string name, string birthdate, string idNo)
        {


            if (name == null || birthdate == null || idNo == null)
            {
                var error = new List<ListCustomerResultDto>();

                var message = new ListCustomerResultDto
                {
                    message = "Some field is empty!"
                };

                error.Add(message);
                //throw new UserFriendlyException("Some field is empty!");
                return new ListResultDto<ListCustomerResultDto>(error);
            }
            else
            {
                var birthdateConv = Convert.ToDateTime(birthdate);
                var getCustomer = (from a in _personals.GetAll()
                                   join b in _trId.GetAll()
                                   on a.psCode equals b.psCode
                                   join c in _trEmail.GetAll()
                                   on a.psCode equals c.psCode into tremail
                                   from c in tremail.DefaultIfEmpty()
                                   join d in _trPhone.GetAll()
                                   on a.psCode equals d.psCode into trPhone
                                   from d in trPhone.DefaultIfEmpty()
                                   where a.name.Contains(name) && a.birthDate.Value.Date == birthdateConv && b.idNo == idNo
                                   select new ListCustomerResultDto
                                   {
                                       psCode = a.psCode,
                                       name = a.name,
                                       birthDate = a.birthDate,
                                       KTP = b.idNo,
                                       email = c.email,
                                       phone = d.number
                                   }
                                 ).ToList();
                if (getCustomer.Count() > 0)
                {
                    return new ListResultDto<ListCustomerResultDto>(getCustomer);
                }
                else
                {
                    var error = new List<ListCustomerResultDto>();

                    var message = new ListCustomerResultDto
                    {
                        message = "Data Is not Exist, Please Check Your Data"
                    };

                    error.Add(message);

                    //throw new UserFriendlyException("Data Is not Exist, Please Check Your Data");
                    return new ListResultDto<ListCustomerResultDto>(error);
                }
            }
        }

        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_CustomerMember_GetNation)]
        public List<ListNationResultDto> GetNation()
        {
            var nationData = (from a in _msNation.GetAll()
                              select new ListNationResultDto
                              {
                                  nationID = a.nationID,
                                  nationality = a.nationality
                              }).ToList();

            return new List<ListNationResultDto>(nationData);
        }

        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_CustomerMember_GetPostCode)]
        public List<ListPostCodeResultDto> GetPostCode(string city)
        {
            var postCode = (from a in _msPostCode.GetAll()
                            where a.cityCode == city
                            select new ListPostCodeResultDto
                            {
                                postCode = a.postCode
                            }).ToList();

            return new List<ListPostCodeResultDto>(postCode);
        }

        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_CustomerMember_GetCountry)]
        public List<ListCountryResultDto> GetCountry()
        {
            var countryData = (from a in _lkCountry.GetAll()
                               select new ListCountryResultDto
                               {
                                   country = a.country,
                                   urut = a.urut
                               }).ToList();

            return new List<ListCountryResultDto>(countryData);
        }

        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_CustomerMember_GetCity)]
        public List<ListCityResultDto> GetCity(string country)
        {
            var cityData = (from a in _msCity.GetAll()
                            where a.country == country
                            select new ListCityResultDto
                            {
                                cityCode = a.cityCode,
                                cityName = a.cityName
                            }).ToList();

            return new List<ListCityResultDto>(cityData);
        }

        [UnitOfWork(isTransactional: false)]
        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_CustomerMember_GetDetailCustomer)]
        public DetailCustomerResultDto GetDetailCustomer(string psCode)
        {
            var getDetail = (from a in _personals.GetAll()
                             join b in _trEmail.GetAll() on a.psCode equals b.psCode into trEmail
                             from b in trEmail.DefaultIfEmpty()
                             join c in _trPhone.GetAll() on a.psCode equals c.psCode into trPhone
                             from c in trPhone.DefaultIfEmpty()
                             join e in _trIdFamily.GetAll() on a.psCode equals e.psCode into trIdFamily
                             from e in trIdFamily.DefaultIfEmpty()
                             join f in _trId.GetAll() on a.psCode equals f.psCode into trId
                             from f in trId.DefaultIfEmpty()
                             join g in _trAddress.GetAll() on a.psCode equals g.psCode into trAddress
                             from g in trAddress.DefaultIfEmpty()
                             where a.psCode == psCode
                             select new DetailCustomerResultDto
                             {
                                 psCode = a.psCode,
                                 name = a.name,
                                 birthDate = a.birthDate,
                                 NPWP = a.NPWP,
                                 sex = a.sex,
                                 birthPlace = a.birthPlace,
                                 marCode = a.marCode,
                                 nationID = a.nationID,
                                 email = b.email,
                                 phone = c.number,
                                 idType = f.idType,
                                 idNo = f.idNo,
                                 address = g.address,
                                 city = g.city,
                                 country = g.country,
                                 postCode = g.postCode
                             }).FirstOrDefault();

            if (getDetail != null)
            {
                var document = (from d in _trDocument.GetAll()
                                where d.psCode == psCode
                                select new DocumentImage
                                {
                                    documentType = d.documentType,
                                    documentImage = d.documentBinary

                                }).ToList();

                var getAddress = (from a in _trAddress.GetAll()
                                  where a.psCode == getDetail.psCode
                                  select a).FirstOrDefault();

                getDetail = new DetailCustomerResultDto
                {
                    psCode = getDetail.psCode,
                    name = getDetail.name,
                    birthDate = getDetail.birthDate,
                    NPWP = getDetail.NPWP,
                    sex = getDetail.sex,
                    birthPlace = getDetail.birthPlace,
                    marCode = getDetail.marCode,
                    nationID = getDetail.nationID,
                    email = getDetail.email,
                    idType = getDetail.idType,
                    idNo = getDetail.idNo,
                    documentImages = document,
                    address = getAddress != null ? getAddress.address : null,
                    city = getDetail.city,
                    country = getDetail.country,
                    phone = getDetail.phone,
                    postCode = getDetail.postCode
                };

                return getDetail;
            }
            else
            {
                return new DetailCustomerResultDto
                {
                    message = "data not found"
                };
            }
        }

        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_CustomerMember_UpdateCustomer)]
        public async Task<ResultMessageDto> UpdateCustomer(UpdateCustomerInputDto input)
        {
            var getPersonals = (from p in _personals.GetAll()
                                where p.psCode == input.psCode
                                select p).FirstOrDefault();

            var birthdateConv = Convert.ToDateTime(input.birthDate);
            var upPersonals = getPersonals.MapTo<PERSONALS>();

            var document = input.document.Count();

            upPersonals.name = input.name;
            upPersonals.sex = input.sex;
            upPersonals.birthPlace = input.birthPlace;
            upPersonals.birthDate = birthdateConv;
            upPersonals.marCode = input.marCode;
            upPersonals.nationID = input.nationID;
            upPersonals.NPWP = input.NPWP;
            upPersonals.remarks = "Online Booking";

            await _personals.UpdateAsync(upPersonals);

            var getEmail = (from e in _trEmail.GetAll()
                            where e.psCode == input.psCode
                            select e).FirstOrDefault();

            var upEmail = getEmail.MapTo<TR_Email>();

            upEmail.email = input.email;

            await _trEmail.UpdateAsync(upEmail);

            var getIDFam = (from f in _trIdFamily.GetAll()
                            where f.psCode == input.psCode
                            select f).FirstOrDefault();

            var upIDFam = getIDFam.MapTo<TR_IDFamily>();

            upIDFam.idType = input.idType;
            upIDFam.idNo = input.idNo;

            await _trIdFamily.UpdateAsync(upIDFam);

            var getID = (from f in _trId.GetAll()
                         where f.psCode == input.psCode
                         select f).FirstOrDefault();

            var upID = getID.MapTo<TR_ID>();

            upID.idType = input.idType;
            upID.idNo = input.idNo;

            await _trId.UpdateAsync(upID);

            if (input.idType != "7")
            {
                var getPhone = (from p in _trPhone.GetAll()
                                where p.psCode == input.psCode
                                select p).FirstOrDefault();

                var upPhone = getPhone.MapTo<TR_Phone>();

                upPhone.number = input.number;

                await _trPhone.UpdateAsync(upPhone);
            }

            var getAddress = (from a in _trAddress.GetAll()
                              where a.psCode == input.psCode
                              select a).FirstOrDefault();

            if (getAddress != null)
            {
                var upAddress = getAddress.MapTo<TR_Address>();

                upAddress.address = input.address;
                upAddress.postCode = input.postCode;
                upAddress.city = input.city;
                upAddress.country = input.country;
                upAddress.province = input.province;

                await _trAddress.UpdateAsync(upAddress);
            }

            else
            {
                var dataAddress = new TR_Address
                {
                    entityCode = "1",
                    psCode = input.psCode,
                    refID = 1,
                    addrType = "3",
                    address = input.address,
                    country = input.country,
                    city = input.city,
                    postCode = input.postCode,
                    province = input.province
                };

                await _trAddress.InsertAsync(dataAddress);
            }

            var getDoc = (from x in _trDocument.GetAll()
                          where x.psCode == input.psCode
                          select x).ToList();

            if (input.document.Where(x => x.documentType == "KTP").FirstOrDefault() == null)
            {
                var itemDoc = getDoc.Where(x => x.documentType == "KTP").FirstOrDefault();
                if (itemDoc != null)
                {
                    _trDocument.Delete(getDoc.Where(x => x.documentType == "KTP").FirstOrDefault());
                }
            }
            if (input.document.Where(x => x.documentType == "NPWP").FirstOrDefault() == null)
            {
                var itemDoc = getDoc.Where(x => x.documentType == "NPWP").FirstOrDefault();
                if (itemDoc != null)
                {
                    _trDocument.Delete(getDoc.Where(x => x.documentType == "NPWP").FirstOrDefault());
                }
            }
            if (input.document.Where(x => x.documentType == "KK").FirstOrDefault() == null)
            {
                var itemDoc = getDoc.Where(x => x.documentType == "KK").FirstOrDefault();
                if (itemDoc != null)
                {
                    _trDocument.Delete(getDoc.Where(x => x.documentType == "KK").FirstOrDefault());
                }
            }
            if (input.document.Where(x => x.documentType == "KTP Pasangan" || x.documentType == "KTPPasangan" || x.documentType == "KTPP").FirstOrDefault() == null)
            {
                var itemDoc = getDoc.Where(x => x.documentType == "KTP Pasangan" || x.documentType == "KTPPasangan" || x.documentType == "KTPP").FirstOrDefault();
                if (itemDoc != null)
                {
                    _trDocument.Delete(getDoc.Where(x => x.documentType == "KTP Pasangan" || x.documentType == "KTPPasangan" || x.documentType == "KTPP").FirstOrDefault());
                }
            }
            if (input.document.Where(x => x.documentType == "KITAS").FirstOrDefault() == null)
            {
                var itemDoc = getDoc.Where(x => x.documentType == "KITAS").FirstOrDefault();
                if (itemDoc != null)
                {
                    _trDocument.Delete(getDoc.Where(x => x.documentType == "KITAS").FirstOrDefault());
                }
            }
            if (input.document.Where(x => x.documentType == "Passport").FirstOrDefault() == null)
            {
                var itemDoc = getDoc.Where(x => x.documentType == "Passport").FirstOrDefault();
                if (itemDoc != null)
                {
                    _trDocument.Delete(getDoc.Where(x => x.documentType == "Passport").FirstOrDefault());
                }
            }

            foreach (var item in input.document)
            {
                var getDocument = (from a in _trDocument.GetAll()
                                   where a.psCode == input.psCode && a.documentType == item.documentType
                                   select a).FirstOrDefault();

                if (getDocument != null)
                {
                    var upDocument = getDocument.MapTo<TR_Document>();

                    upDocument.documentType = item.documentType;
                    upDocument.documentBinary = item.documentBinary;

                    await _trDocument.UpdateAsync(upDocument);
                }
                else
                {
                    var dtoDocument = new TR_Document
                    {
                        entityCode = "1",
                        psCode = input.psCode,
                        documentType = item.documentType,
                        documentBinary = item.documentBinary,
                        documentPicType = "Image",
                        documentRef = 1
                    };

                    await _trDocument.InsertAsync(dtoDocument);
                }
            }
            return new ResultMessageDto
            {
                message = "Success",
                result = true
            };

        }

        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_CustomerMember_GetDocumentByPsCode)]
        public List<ListDocumentResultDto> GetDocumentByPsCode(string psCode)
        {
            var doc = (from x in _trDocument.GetAll()
                       where x.psCode == psCode
                       select new ListDocumentDto
                       {
                           documentType = x.documentType,
                           documentBinary = x.documentBinary
                       }).ToList();

            if (doc != null)
            {
                var penampung = new List<ListDocumentResultDto>();

                foreach (var item in doc)
                {
                    var data = new ListDocumentResultDto
                    {
                        documentType = item.documentType,
                        document = item.documentBinary
                    };
                    penampung.Add(data);
                }

                return penampung;
            }
            else
            {
                var listMessage = new List<ListDocumentResultDto>();

                var message = new ListDocumentResultDto
                {
                    message = "data not found"
                };

                listMessage.Add(message);
                return listMessage;
            }
        }

        [UnitOfWork(isTransactional: false)]
        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_CustomerMember_GetDetailCustomerMobile)]
        public DetailCustomerMobileResultDto GetDetailCustomerMobile(string psCode)
        {
            var getDetail = (from p in _personals.GetAll()
                             join e in _trEmail.GetAll() on p.psCode equals e.psCode
                             join f in _trIdFamily.GetAll() on p.psCode equals f.psCode
                             join i in _trId.GetAll() on p.psCode equals i.psCode
                             where p.psCode == psCode
                             select new DetailCustomerMobileResultDto
                             {
                                 psCode = p.psCode,
                                 name = p.name,
                                 birthDate = p.birthDate,
                                 NPWP = p.NPWP,
                                 sex = p.sex,
                                 birthPlace = p.birthPlace,
                                 marCode = p.marCode,
                                 nationID = p.nationID,
                                 email = e.email,
                                 idType = i.idType,
                                 idNo = i.idNo
                             }).FirstOrDefault();

            if (getDetail != null)
            {
                var document = (from d in _trDocument.GetAll()
                                where d.psCode == psCode
                                select new DataByte
                                {
                                    documentType = d.documentType,
                                    documentBinarys = d.documentBinary

                                }).ToList();

                var penampung = new List<DocumentUpload>();

                foreach (var item in document)
                {
                    var getType = (from a in document
                                   where a.documentType == item.documentType
                                   select a).FirstOrDefault();
                    if (getDetail.idType == "1")
                    {

                        if (item.documentType == "KTP" && getType != null)
                        {
                            getDetail.documentTypeKTP = item.documentType;
                            getDetail.documentBinaryKTP = item.documentBinarys;
                        }
                        else if (item.documentType == "NPWP" && getType != null)
                        {
                            getDetail.documentTypeNPWP = item.documentType;
                            getDetail.documentBinaryNPWP = item.documentBinarys;
                        }
                        else if (item.documentType == "KTP Pasangan" && getType != null)
                        {
                            getDetail.documentTypeKTPPasangan = item.documentType;
                            getDetail.documentBinaryKTPPasangan = item.documentBinarys;
                        }
                        else if (item.documentType == "KK" && getType != null)
                        {
                            getDetail.documentTypeKK = item.documentType;
                            getDetail.documentBinaryKK = item.documentBinarys;
                        }
                        else if (item.documentType == "AKTE" && getType != null)
                        {
                            getDetail.documentTypeAkte = item.documentType;
                            getDetail.documentBinaryAkte = item.documentBinarys;
                        }

                    }
                    else if (getDetail.idType == "5")
                    {
                        if (item.documentType == "KITAS" && getType != null)
                        {
                            getDetail.documentTypeKTP = item.documentType;
                            getDetail.documentBinaryKTP = item.documentBinarys;
                        }
                        else if (item.documentType == "NPWP" && getType != null)
                        {
                            getDetail.documentTypeNPWP = item.documentType;
                            getDetail.documentBinaryNPWP = item.documentBinarys;
                        }
                        else if (item.documentType == "Passport" && getType != null)
                        {
                            getDetail.documentTypePassport = item.documentType;
                            getDetail.documentBinaryPassport = item.documentBinarys;
                        }
                    }

                    else if (getDetail.idType == "7")
                    {
                        if (item.documentType.ToLowerInvariant() == "ktp direksi pengurus" && getType != null)
                        {
                            getDetail.documentTypeKTPDireksi = item.documentType;
                            getDetail.documentBinaryKTPDireksi = item.documentBinarys;
                        }
                        else if (item.documentType == "Tanda Daftar Perusahaan" && getType != null)
                        {
                            getDetail.documentTypeTDP = item.documentType;
                            getDetail.documentBinaryTDP = item.documentBinarys;
                        }
                        else if (item.documentType == "NPWP" && getType != null)
                        {
                            getDetail.documentTypeNPWP = item.documentType;
                            getDetail.documentBinaryNPWP = item.documentBinarys;
                        }
                    }
                }

                if (getDetail.idType == "1")
                {
                    var getAddress = (from a in _trAddress.GetAll()
                                      where a.psCode == getDetail.psCode
                                      select a).FirstOrDefault();

                    var phone = (from p in _trPhone.GetAll()
                                 where p.psCode == psCode
                                 select p).FirstOrDefault();
                    if (getAddress != null)
                    {
                        var city = (from c in _msCity.GetAll()
                                    where c.cityCode == getAddress.city
                                    select c).FirstOrDefault();
                        if (phone != null)
                        {
                            getDetail = new DetailCustomerMobileResultDto
                            {
                                psCode = getDetail.psCode,
                                name = getDetail.name,
                                birthDate = getDetail.birthDate,
                                NPWP = getDetail.NPWP,
                                sex = getDetail.sex,
                                birthPlace = getDetail.birthPlace,
                                marCode = getDetail.marCode,
                                nationID = getDetail.nationID,
                                email = getDetail.email,
                                idType = getDetail.idType,
                                idNo = getDetail.idNo,
                                number = phone.number,
                                address = getAddress.address,
                                country = getAddress.country,
                                city = city == null ? null : city.cityCode,
                                province = getAddress.province,
                                postCode = getAddress.postCode,
                                documentTypeKTP = getDetail.documentTypeKTP,
                                documentBinaryKTP = getDetail.documentBinaryKTP,
                                documentTypeNPWP = getDetail.documentTypeNPWP,
                                documentBinaryNPWP = getDetail.documentBinaryNPWP,
                                documentTypeKTPPasangan = getDetail.documentTypeKTPPasangan,
                                documentBinaryKTPPasangan = getDetail.documentBinaryKTPPasangan,
                                documentTypeKK = getDetail.documentTypeKK,
                                documentBinaryKK = getDetail.documentBinaryKK,
                                documentTypeAkte = getDetail.documentTypeAkte,
                                documentBinaryAkte = getDetail.documentBinaryAkte
                            };
                        }
                        else
                        {
                            getDetail = new DetailCustomerMobileResultDto
                            {
                                psCode = getDetail.psCode,
                                name = getDetail.name,
                                birthDate = getDetail.birthDate,
                                NPWP = getDetail.NPWP,
                                sex = getDetail.sex,
                                birthPlace = getDetail.birthPlace,
                                marCode = getDetail.marCode,
                                nationID = getDetail.nationID,
                                email = getDetail.email,
                                idType = getDetail.idType,
                                idNo = getDetail.idNo,
                                number = null,
                                address = getAddress.address,
                                country = getAddress.country,
                                province = getAddress.province,
                                city = city == null ? null : city.cityCode,
                                postCode = getAddress.postCode,
                                documentTypeKTP = getDetail.documentTypeKTP,
                                documentBinaryKTP = getDetail.documentBinaryKTP,
                                documentTypeNPWP = getDetail.documentTypeNPWP,
                                documentBinaryNPWP = getDetail.documentBinaryNPWP,
                                documentTypeKTPPasangan = getDetail.documentTypeKTPPasangan,
                                documentBinaryKTPPasangan = getDetail.documentBinaryKTPPasangan,
                                documentTypeKK = getDetail.documentTypeKK,
                                documentBinaryKK = getDetail.documentBinaryKK,
                                documentTypeAkte = getDetail.documentTypeAkte,
                                documentBinaryAkte = getDetail.documentBinaryAkte
                            };
                        }

                    }
                    else
                    {
                        if (phone != null)
                        {
                            getDetail = new DetailCustomerMobileResultDto
                            {
                                psCode = getDetail.psCode,
                                name = getDetail.name,
                                birthDate = getDetail.birthDate,
                                NPWP = getDetail.NPWP,
                                sex = getDetail.sex,
                                birthPlace = getDetail.birthPlace,
                                marCode = getDetail.marCode,
                                nationID = getDetail.nationID,
                                email = getDetail.email,
                                idType = getDetail.idType,
                                idNo = getDetail.idNo,
                                number = phone.number,
                                address = null,
                                country = null,
                                city = null,
                                postCode = null,
                                province = null,
                                documentTypeKTP = getDetail.documentTypeKTP,
                                documentBinaryKTP = getDetail.documentBinaryKTP,
                                documentTypeNPWP = getDetail.documentTypeNPWP,
                                documentBinaryNPWP = getDetail.documentBinaryNPWP,
                                documentTypeKTPPasangan = getDetail.documentTypeKTPPasangan,
                                documentBinaryKTPPasangan = getDetail.documentBinaryKTPPasangan,
                                documentTypeKK = getDetail.documentTypeKK,
                                documentBinaryKK = getDetail.documentBinaryKK,
                                documentTypeAkte = getDetail.documentTypeAkte,
                                documentBinaryAkte = getDetail.documentBinaryAkte
                            };
                        }
                        else
                        {
                            getDetail = new DetailCustomerMobileResultDto
                            {
                                psCode = getDetail.psCode,
                                name = getDetail.name,
                                birthDate = getDetail.birthDate,
                                NPWP = getDetail.NPWP,
                                sex = getDetail.sex,
                                birthPlace = getDetail.birthPlace,
                                marCode = getDetail.marCode,
                                nationID = getDetail.nationID,
                                email = getDetail.email,
                                idType = getDetail.idType,
                                idNo = getDetail.idNo,
                                number = null,
                                address = null,
                                country = null,
                                city = null,
                                postCode = null,
                                province = null,
                                documentTypeKTP = getDetail.documentTypeKTP,
                                documentBinaryKTP = getDetail.documentBinaryKTP,
                                documentTypeNPWP = getDetail.documentTypeNPWP,
                                documentBinaryNPWP = getDetail.documentBinaryNPWP,
                                documentTypeKTPPasangan = getDetail.documentTypeKTPPasangan,
                                documentBinaryKTPPasangan = getDetail.documentBinaryKTPPasangan,
                                documentTypeKK = getDetail.documentTypeKK,
                                documentBinaryKK = getDetail.documentBinaryKK,
                                documentTypeAkte = getDetail.documentTypeAkte,
                                documentBinaryAkte = getDetail.documentBinaryAkte
                            };
                        }

                    }

                }
                else if (getDetail.idType == "7")
                {
                    var getAddress = (from a in _trAddress.GetAll()
                                      where a.psCode == getDetail.psCode
                                      select a).FirstOrDefault();

                    var phone = (from p in _trPhone.GetAll()
                                 where p.psCode == psCode
                                 select p).FirstOrDefault();
                    if (getAddress != null)
                    {
                        var city = (from c in _msCity.GetAll()
                                    where c.cityCode == getAddress.city
                                    select c).FirstOrDefault();
                        if (phone != null)
                        {
                            getDetail = new DetailCustomerMobileResultDto
                            {
                                psCode = getDetail.psCode,
                                name = getDetail.name,
                                birthDate = getDetail.birthDate,
                                NPWP = getDetail.NPWP,
                                sex = getDetail.sex,
                                birthPlace = getDetail.birthPlace,
                                marCode = getDetail.marCode,
                                nationID = getDetail.nationID,
                                email = getDetail.email,
                                idType = getDetail.idType,
                                idNo = getDetail.idNo,
                                number = phone.number,
                                address = getAddress.address,
                                country = getAddress.country,
                                province = getAddress.province,
                                city = city == null ? null : city.cityCode,
                                postCode = getAddress.postCode,
                                documentTypeKTPDireksi = getDetail.documentTypeKTPDireksi,
                                documentBinaryKTPDireksi = getDetail.documentBinaryKTPDireksi,
                                documentTypeNPWPPerusahaan = getDetail.documentTypeNPWP,
                                documentBinaryNPWPPerusahaan = getDetail.documentBinaryNPWP,
                                documentTypeTDP = getDetail.documentTypeTDP,
                                documentBinaryTDP = getDetail.documentBinaryTDP
                            };
                        }
                        else
                        {
                            getDetail = new DetailCustomerMobileResultDto
                            {
                                psCode = getDetail.psCode,
                                name = getDetail.name,
                                birthDate = getDetail.birthDate,
                                NPWP = getDetail.NPWP,
                                sex = getDetail.sex,
                                birthPlace = getDetail.birthPlace,
                                marCode = getDetail.marCode,
                                nationID = getDetail.nationID,
                                email = getDetail.email,
                                idType = getDetail.idType,
                                idNo = getDetail.idNo,
                                number = null,
                                address = getAddress.address,
                                country = getAddress.country,
                                province = getAddress.province,
                                city = city.cityCode,
                                postCode = getAddress.postCode,
                                documentTypeKTPDireksi = getDetail.documentTypeKTPDireksi,
                                documentBinaryKTPDireksi = getDetail.documentBinaryKTPDireksi,
                                documentTypeNPWPPerusahaan = getDetail.documentTypeNPWP,
                                documentBinaryNPWPPerusahaan = getDetail.documentBinaryNPWP,
                                documentTypeTDP = getDetail.documentTypeTDP,
                                documentBinaryTDP = getDetail.documentBinaryTDP
                            };
                        }

                    }
                    else
                    {
                        if (phone != null)
                        {
                            getDetail = new DetailCustomerMobileResultDto
                            {
                                psCode = getDetail.psCode,
                                name = getDetail.name,
                                birthDate = getDetail.birthDate,
                                NPWP = getDetail.NPWP,
                                sex = getDetail.sex,
                                birthPlace = getDetail.birthPlace,
                                marCode = getDetail.marCode,
                                nationID = getDetail.nationID,
                                email = getDetail.email,
                                idType = getDetail.idType,
                                idNo = getDetail.idNo,
                                number = phone.number,
                                address = null,
                                country = null,
                                city = null,
                                postCode = null,
                                province = null,
                                documentTypeKTPDireksi = getDetail.documentTypeKTPDireksi,
                                documentBinaryKTPDireksi = getDetail.documentBinaryKTPDireksi,
                                documentTypeNPWPPerusahaan = getDetail.documentTypeNPWP,
                                documentBinaryNPWPPerusahaan = getDetail.documentBinaryNPWP,
                                documentTypeTDP = getDetail.documentTypeTDP,
                                documentBinaryTDP = getDetail.documentBinaryTDP
                            };
                        }
                        else
                        {
                            getDetail = new DetailCustomerMobileResultDto
                            {
                                psCode = getDetail.psCode,
                                name = getDetail.name,
                                birthDate = getDetail.birthDate,
                                NPWP = getDetail.NPWP,
                                sex = getDetail.sex,
                                birthPlace = getDetail.birthPlace,
                                marCode = getDetail.marCode,
                                nationID = getDetail.nationID,
                                email = getDetail.email,
                                idType = getDetail.idType,
                                idNo = getDetail.idNo,
                                number = null,
                                address = null,
                                country = null,
                                city = null,
                                postCode = null,
                                province = null,
                                documentTypeKTPDireksi = getDetail.documentTypeKTPDireksi,
                                documentBinaryKTPDireksi = getDetail.documentBinaryKTPDireksi,
                                documentTypeNPWPPerusahaan = getDetail.documentTypeNPWP,
                                documentBinaryNPWPPerusahaan = getDetail.documentBinaryNPWP,
                                documentTypeTDP = getDetail.documentTypeTDP,
                                documentBinaryTDP = getDetail.documentBinaryTDP
                            };
                        }

                    }

                }
                else if (getDetail.idType == "5")
                {
                    var getAddress = (from a in _trAddress.GetAll()
                                      where a.psCode == getDetail.psCode
                                      select a).FirstOrDefault();

                    var phone = (from p in _trPhone.GetAll()
                                 where p.psCode == psCode
                                 select p).FirstOrDefault();

                    var city = (from c in _msCity.GetAll()
                                where c.cityCode == getAddress.city
                                select c).FirstOrDefault();

                    if (getAddress != null)
                    {
                        if (phone != null)
                        {
                            getDetail = new DetailCustomerMobileResultDto
                            {
                                psCode = getDetail.psCode,
                                name = getDetail.name,
                                birthDate = getDetail.birthDate,
                                NPWP = getDetail.NPWP,
                                sex = getDetail.sex,
                                birthPlace = getDetail.birthPlace,
                                marCode = getDetail.marCode,
                                nationID = getDetail.nationID,
                                email = getDetail.email,
                                idType = getDetail.idType,
                                idNo = getDetail.idNo,
                                number = phone.number,
                                address = getAddress.address,
                                country = getAddress.country,
                                province = getAddress.province,
                                city = city == null ? null : city.cityCode,
                                postCode = getAddress.postCode,
                                documentTypeKTP = getDetail.documentTypeKTP,
                                documentBinaryKTP = getDetail.documentBinaryKTP,
                                documentTypeNPWP = getDetail.documentTypeNPWP,
                                documentBinaryNPWP = getDetail.documentBinaryNPWP,
                                documentTypePassport = getDetail.documentTypePassport,
                                documentBinaryPassport = getDetail.documentBinaryPassport
                            };
                        }
                        else
                        {
                            getDetail = new DetailCustomerMobileResultDto
                            {
                                psCode = getDetail.psCode,
                                name = getDetail.name,
                                birthDate = getDetail.birthDate,
                                NPWP = getDetail.NPWP,
                                sex = getDetail.sex,
                                birthPlace = getDetail.birthPlace,
                                marCode = getDetail.marCode,
                                nationID = getDetail.nationID,
                                email = getDetail.email,
                                idType = getDetail.idType,
                                idNo = getDetail.idNo,
                                number = null,
                                address = getAddress.address,
                                country = getAddress.country,
                                province = getAddress.province,
                                city = city == null ? null : city.cityCode,
                                postCode = getAddress.postCode,
                                documentTypeKTP = getDetail.documentTypeKTP,
                                documentBinaryKTP = getDetail.documentBinaryKTP,
                                documentTypeNPWP = getDetail.documentTypeNPWP,
                                documentBinaryNPWP = getDetail.documentBinaryNPWP,
                                documentTypePassport = getDetail.documentTypePassport,
                                documentBinaryPassport = getDetail.documentBinaryPassport
                            };
                        }

                    }
                    else
                    {
                        if (phone != null)
                        {
                            getDetail = new DetailCustomerMobileResultDto
                            {
                                psCode = getDetail.psCode,
                                name = getDetail.name,
                                birthDate = getDetail.birthDate,
                                NPWP = getDetail.NPWP,
                                sex = getDetail.sex,
                                birthPlace = getDetail.birthPlace,
                                marCode = getDetail.marCode,
                                nationID = getDetail.nationID,
                                email = getDetail.email,
                                idType = getDetail.idType,
                                idNo = getDetail.idNo,
                                number = phone.number,
                                address = null,
                                country = null,
                                city = city == null ? null : city.cityCode,
                                postCode = null,
                                province = null,
                                documentTypeKTP = getDetail.documentTypeKTP,
                                documentBinaryKTP = getDetail.documentBinaryKTP,
                                documentTypeNPWP = getDetail.documentTypeNPWP,
                                documentBinaryNPWP = getDetail.documentBinaryNPWP,
                                documentTypePassport = getDetail.documentTypePassport,
                                documentBinaryPassport = getDetail.documentBinaryPassport
                            };
                        }
                        else
                        {
                            getDetail = new DetailCustomerMobileResultDto
                            {
                                psCode = getDetail.psCode,
                                name = getDetail.name,
                                birthDate = getDetail.birthDate,
                                NPWP = getDetail.NPWP,
                                sex = getDetail.sex,
                                birthPlace = getDetail.birthPlace,
                                marCode = getDetail.marCode,
                                nationID = getDetail.nationID,
                                email = getDetail.email,
                                idType = getDetail.idType,
                                idNo = getDetail.idNo,
                                number = null,
                                address = null,
                                country = null,
                                city = city == null ? null : city.cityCode,
                                postCode = null,
                                province = null,
                                documentTypeKTP = getDetail.documentTypeKTP,
                                documentBinaryKTP = getDetail.documentBinaryKTP,
                                documentTypeNPWP = getDetail.documentTypeNPWP,
                                documentBinaryNPWP = getDetail.documentBinaryNPWP,
                                documentTypePassport = getDetail.documentTypePassport,
                                documentBinaryPassport = getDetail.documentBinaryPassport
                            };
                        }

                    }
                }
                return getDetail;
            }
            else
            {
                return new DetailCustomerMobileResultDto
                {
                    message = "data not found"
                };


            }
        }

        //[AbpAuthorize(AppPermissions.Pages_Tenant_OnlineBooking_CustomerMember_GetProvince)]
        public List<ListProvinceResultDto> GetProvince()
        {
            var getProvince = (from a in _msProvice.GetAll()
                               select new ListProvinceResultDto
                               {
                                   provinceName = a.provinceName
                               }).ToList();

            return getProvince;
        }
    }
}
