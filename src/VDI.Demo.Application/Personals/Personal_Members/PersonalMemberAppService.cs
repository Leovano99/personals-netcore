using System;
using System.Linq;
using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using VDI.Demo.Personals.Personal_Members.Dto;
using VDI.Demo.Personals.Personals.Dto;
using VDI.Demo.PersonalsDB;
using Abp.AutoMapper;
using System.Data;
using Abp.UI;
using VDI.Demo.SqlExecuter;
using VDI.Demo.Authorization;
using Abp.Authorization;
using Newtonsoft.Json.Linq;

namespace VDI.Demo.Personals.Personal_Members
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_PersonalMember)]
    public class PersonalMemberAppService : DemoAppServiceBase, IPersonalMemberAppService
    {
        private readonly IRepository<PERSONALS, string> _personalRepo;
        private readonly IRepository<PERSONALS_MEMBER, string> _personalsMemberRepo;
        private readonly ISqlExecuter _sqlExecuter;

        public PersonalMemberAppService(
            IRepository<PERSONALS, string> personalRepo,
            IRepository<PERSONALS_MEMBER, string> personalsMemberRepo,
            ISqlExecuter sqlExecuter
        )
        {
            _personalRepo = personalRepo;
            _personalsMemberRepo = personalsMemberRepo;
            _sqlExecuter = sqlExecuter;
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_PersonalMember_GetAllPersonalMemberList)]
        public ListResultDto<GetAllPersonalMemberDto> GetAllPersonalMemberList(string keyword, string scmCode)
        {
            var getAllData = (from A in _personalRepo.GetAll()
                              join B in _personalsMemberRepo.GetAll() on A.psCode equals B.psCode
                              where (A.name.Contains(keyword) || B.memberCode.Contains(keyword)) && B.scmCode == scmCode
                              select new GetAllPersonalMemberDto
                              {
                                  memberCode = B.memberCode,
                                  name = A.name
                              }).ToList();

            return new ListResultDto<GetAllPersonalMemberDto>(getAllData);

            //var dataCount = await getAllData.AsQueryable().CountAsync();

            //var resultList = await getAllData.AsQueryable()
            //    .OrderBy(input.Sorting)
            //    .PageBy(input)
            //    .ToListAsync();

            //var listDtos = resultList;

            //return new PagedResultDto<GetAllPersonalMemberDto>(
            //    dataCount,
            //    listDtos);
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_PersonalMember_GenerateMemberCode)]
        public string GenerateMemberCode(string scmCode, bool isInstitute)
        {
            Logger.Info("GenerateMemberCode() - Started.");
            var query = "exec sp_GetNewMemberCode @entityCode, @scmCode, @isInstitute";

            Logger.DebugFormat("GenerateMemberCode() - Start Generate Member Code. Parameters sent:{0}" +
               "entityCode = {1}{0}" +
               "scmCode    = {2}{0}" +
               "isInstitute= {3}{0}",
               Environment.NewLine, 1, scmCode, isInstitute);

            var entityCode = "1";
            var result = _sqlExecuter.GetFromPersonals<string>(query, new { entityCode, scmCode, isInstitute }, System.Data.CommandType.StoredProcedure).FirstOrDefault();

            Logger.DebugFormat("GenerateMemberCode() - End Generate Member Code.");
            Logger.Info("GenerateMemberCode() - Finished.");

            return result;
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_PersonalMember_Edit)]
        public JObject UpdateMember(CreateMemberDto input)
        {
            Logger.Info("UpdateMember() - Started.");
            JObject obj = new JObject();

            Logger.DebugFormat("UpdateMember() - Start get data for update in Personals Member. Parameters sent:{0}" +
                        "entityCode = {1}{0}" +
                        "psCode     = {2}{0}" +
                        "scmCode    = {3}{0}" +
                        "memberCode = {4}{0}"
                        , Environment.NewLine, 1, input.memberData.psCode, input.memberData.scmCode, input.memberData.memberCode);

            var getSetMember = (from personalsMember in _personalsMemberRepo.GetAll()
                                where personalsMember.entityCode == "1"
                                && personalsMember.psCode == input.memberData.psCode
                                && personalsMember.scmCode == input.memberData.scmCode
                                && personalsMember.memberCode == input.memberData.memberCode
                                select personalsMember).FirstOrDefault();

            Logger.DebugFormat("UpdateMember() - End get data for update in Personals Member.");


            if (getSetMember != null)
            {

                if (input.memberActivation.isMember)
                {
                    var getUnavailableMemberScmCode = (from x in _personalsMemberRepo.GetAll()
                                                       where x.entityCode == "1" &&
                                                       x.psCode == input.memberData.psCode &&
                                                       x.scmCode == input.memberData.scmCode &&
                                                       x.memberCode != input.memberData.memberCode &&
                                                       x.isMember == true
                                                       select x).ToList();
                    foreach (var dataToEdit in getUnavailableMemberScmCode)
                    {
                        dataToEdit.isMember = false;
                        try
                        {
                            Logger.DebugFormat("DeleteMember() - Start delete member. Params sent: {0}" +
                            "isMember     = {1}{0}"
                            , Environment.NewLine, false);

                            _personalsMemberRepo.Update(dataToEdit);
                            CurrentUnitOfWork.SaveChanges();

                            Logger.DebugFormat("DeleteMember() - End delete member.");
                        }
                        // Handle data errors.
                        catch (DataException exDb)
                        {
                            Logger.ErrorFormat("DeleteMember() - ERROR DataException. Result = {0}", exDb.Message);
                            throw new UserFriendlyException("Database Error : {0}", exDb.Message);
                        }
                        // Handle all other exceptions.
                        catch (Exception ex)
                        {
                            Logger.ErrorFormat("DeleteMember() - ERROR Exception. Result = {0}", ex.Message);
                            throw new UserFriendlyException("Error : {0}", ex.Message);
                        }

                    }
                }

                var update = getSetMember.MapTo<PERSONALS_MEMBER>();

                #region member data
                update.parentMemberCode = String.IsNullOrEmpty(input.memberData.parentMemberCode) ? null : input.memberData.parentMemberCode;
                update.PTName = String.IsNullOrEmpty(input.memberData.PTName) ? null : input.memberData.PTName;
                update.PrincName = String.IsNullOrEmpty(input.memberData.PrincName) ? null : input.memberData.PrincName;
                update.spouName = String.IsNullOrEmpty(input.memberData.spouName) ? null : input.memberData.spouName;
                update.specCode = String.IsNullOrEmpty(input.memberData.specCode) ? "0" : input.memberData.specCode;
                update.franchiseGroup = String.IsNullOrEmpty(input.memberData.franchiseGroup) ? "-" : input.memberData.franchiseGroup;
                update.isInstitusi = input.memberData.isInstitusi;
                update.isPKP = input.memberData.isPKP;
                update.isCD = input.memberData.isCD;
                update.CDCode = String.IsNullOrEmpty(input.memberData.CDCode) ? "-" : input.memberData.CDCode;
                update.isACD = input.memberData.isACD;
                update.ACDCode = String.IsNullOrEmpty(input.memberData.ACDCode) ? "-" : input.memberData.ACDCode;
                update.remarks1 = input.memberData.remarks1 == null ? null : input.memberData.remarks1;
                #endregion

                #region activation
                update.memberStatusCode = String.IsNullOrEmpty(input.memberActivation.memberStatusCode) ? "-" : input.memberActivation.memberStatusCode;
                update.isActive = input.memberActivation.isActive;
                update.isMember = input.memberActivation.isMember;
                update.password = String.IsNullOrEmpty(input.memberActivation.password) ? null : input.memberActivation.password;
                #endregion

                #region bank data
                update.bankType = String.IsNullOrEmpty(input.memberBankData.bankType) ? "0" : input.memberBankData.bankType;
                update.bankCode = input.memberBankData.bankCode;
                update.bankBranchName = input.memberBankData.bankBranchName;
                update.bankAccNo = input.memberBankData.bankAccNo;
                update.bankAccName = input.memberBankData.bankAccName;
                update.bankAccountRefID = input.memberBankData.bankAccountRefID;
                #endregion

                try
                {
                    Logger.DebugFormat("UpdateMember() - Start update member. Params sent:{0}" +
                        "Data Member:{0}" +
                    "parentMemberCode	={2}{0}" +
                    "PTName	            ={3}{0}" +
                    "PrincName	        ={4}{0}" +
                    "spouName	        ={5}{0}" +
                    "specCode	        ={6}{0}" +
                    "franchiseGroup	    ={7}{0}" +
                    "isInstitusi	    ={8}{0}" +
                    "isCD	            ={9}{0}" +
                    "CDCode	            ={10}{0}" +
                    "isACD	            ={11}{0}" +
                    "ACDCode	        ={12}{0}" +
                    "remarks1	        ={13}{0}" +
                    "Data Activation:{0}" +
                    "memberStatusCode   ={14}{0}" +
                    "isActive           ={15}{0}" +
                    "isMember           ={16}{0}" +
                    "password           ={17}{0}" +
                    "Data Bank:{0}" +
                    "bankType           ={18}{0}" +
                    "bankCode           ={19}{0}" +
                    "bankBranchName     ={20}{0}" +
                    "bankAccNo          ={21}{0}" +
                    "bankAccName        ={1}{0}" +
                    "bankAccountRefID   ={22}{0}"
                    , Environment.NewLine, input.memberBankData.bankAccName, input.memberData.parentMemberCode, input.memberData.PTName, input.memberData.PrincName, input.memberData.spouName
                    , input.memberData.specCode, input.memberData.franchiseGroup, input.memberData.isInstitusi, input.memberData.isCD, input.memberData.CDCode
                    , input.memberData.isACD, input.memberData.ACDCode, input.memberData.remarks1
                    , input.memberActivation.memberStatusCode, input.memberActivation.isActive, input.memberActivation.isMember, input.memberActivation.password
                    , input.memberBankData.bankType, input.memberBankData.bankCode, input.memberBankData.bankBranchName, input.memberBankData.bankAccNo, input.memberBankData.bankAccountRefID);

                    _personalsMemberRepo.Update(update);
                    CurrentUnitOfWork.SaveChanges();
                    obj.Add("message", "Successfully Updated");

                    Logger.DebugFormat("UpdateMember() - End update member.");
                }
                catch (DataException exDb)
                {
                    Logger.ErrorFormat("UpdateMember() - ERROR DataException. Result = {0}", exDb.Message);
                    throw new UserFriendlyException("Database Error : {0}", exDb.Message);
                }
                catch (Exception ex)
                {
                    Logger.ErrorFormat("UpdateMember() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error : {0}", ex.Message);
                }
            }
            else
            {
                Logger.ErrorFormat("UpdateMember() - ERROR. Result = {0}", "The member is not exist!");
                throw new UserFriendlyException("The member is not exist!");
            }

            Logger.Info("UpdateMember() - Finished.");

            return obj;
        }

        /*
        public JObject UpdateMember(CreateMemberDto input)
        {
            Logger.Info("UpdateMember() - Started.");
            JObject obj = new JObject();

            Logger.DebugFormat("UpdateMember() - Start get data for update in Personals Member. Parameters sent:{0}" +
                       "entityCode = {1}{0}" +
                       "psCode     = {2}{0}" +
                       "scmCode    = {3}{0}" +
                       "memberCode = {4}{0}"
                       , Environment.NewLine, 1, input.memberData.psCode, input.memberData.scmCode, input.memberData.memberCode);

            var getSetMember = (from personalsMember in _personalsMemberRepo.GetAll()
                                where personalsMember.entityCode == "1"
                                && personalsMember.psCode == input.memberData.psCode
                                && personalsMember.scmCode == input.memberData.scmCode
                                && personalsMember.memberCode == input.memberData.memberCode
                                select personalsMember).FirstOrDefault();

            Logger.DebugFormat("UpdateMember() - End get data for update in Personals Member.");
            
            if (getSetMember != null)
            {
                if (input.memberActivation.isActive)
                {
                    var getUnavailableMemberScmCode = (from x in _personalsMemberRepo.GetAll()
                                                       where x.entityCode == "1" &&
                                                       x.psCode == input.memberData.psCode &&
                                                       x.scmCode == input.memberData.scmCode &&
                                                       x.memberCode != input.memberData.memberCode &&
                                                       x.isMember == true
                                                       select x).ToList();

                    foreach (var dataToEdit in getUnavailableMemberScmCode)
                    {
                        dataToEdit.isMember = false;
                        try
                        {
                            Logger.DebugFormat("DeleteMember() - Start delete member. Params sent: {0}" +
                            "isMember     = {1}{0}"
                            , Environment.NewLine, false);

                            _personalsMemberRepo.Update(dataToEdit);
                            CurrentUnitOfWork.SaveChanges();

                            Logger.DebugFormat("DeleteMember() - End delete member.");
                        }
                        // Handle data errors.
                        catch (DataException exDb)
                        {
                            Logger.ErrorFormat("DeleteMember() - ERROR DataException. Result = {0}", exDb.Message);
                            throw new UserFriendlyException("Database Error : {0}", exDb.Message);
                        }
                        // Handle all other exceptions.
                        catch (Exception ex)
                        {
                            Logger.ErrorFormat("DeleteMember() - ERROR Exception. Result = {0}", ex.Message);
                            throw new UserFriendlyException("Error : {0}", ex.Message);
                        }
                    }
                }

                var update = getSetMember.MapTo<PERSONALS_MEMBER>();

                #region member data
                update.parentMemberCode = String.IsNullOrEmpty(input.memberData.parentMemberCode) ? "-" : input.memberData.parentMemberCode;
                update.PTName = String.IsNullOrEmpty(input.memberData.PTName) ? "-" : input.memberData.PTName;
                update.PrincName = String.IsNullOrEmpty(input.memberData.PrincName) ? "-" : input.memberData.PrincName;
                update.spouName = String.IsNullOrEmpty(input.memberData.spouName) ? "-" : input.memberData.spouName;
                update.specCode = String.IsNullOrEmpty(input.memberData.specCode) ? "0" : input.memberData.specCode;
                update.franchiseGroup = String.IsNullOrEmpty(input.memberData.franchiseGroup) ? "-" : input.memberData.franchiseGroup;
                update.isInstitusi = input.memberData.isInstitusi;
                update.isPKP = input.memberData.isPKP;
                update.isCD = input.memberData.isCD;
                update.CDCode = String.IsNullOrEmpty(input.memberData.CDCode) ? "-" : input.memberData.CDCode;
                update.isACD = input.memberData.isACD;
                update.ACDCode = String.IsNullOrEmpty(input.memberData.ACDCode) ? "-" : input.memberData.ACDCode;
                update.remarks1 = input.memberData.remarks1 == null ? "-" : input.memberData.remarks1;
                #endregion

                #region activation
                update.memberStatusCode = String.IsNullOrEmpty(input.memberActivation.memberStatusCode) ? "-" : input.memberActivation.memberStatusCode;
                update.isActive = input.memberActivation.isActive;
                update.isMember = input.memberActivation.isMember;
                update.password = String.IsNullOrEmpty(input.memberActivation.password) ? "-" : input.memberActivation.password;
                #endregion

                #region bank data
                update.bankType = String.IsNullOrEmpty(input.memberBankData.bankType) ? "0" : input.memberBankData.bankType;
                update.bankCode = input.memberBankData.bankCode;
                update.bankBranchName = String.IsNullOrEmpty(input.memberBankData.bankBranchName) ? "-" : input.memberBankData.bankBranchName;
                update.bankAccNo = String.IsNullOrEmpty(input.memberBankData.bankAccNo) ? "-" : input.memberBankData.bankAccNo;
                update.bankAccName = String.IsNullOrEmpty(input.memberBankData.bankAccName) ? "-" : input.memberBankData.bankAccName;
                #endregion

                try
                {
                    Logger.DebugFormat("UpdateMember() - Start update member. Params sent:{0}" +
                           "Data Member:{0}" +
                       "parentMemberCode	={2}{0}" +
                       "PTName	            ={3}{0}" +
                       "PrincName	        ={4}{0}" +
                       "spouName	        ={5}{0}" +
                       "specCode	        ={6}{0}" +
                       "franchiseGroup	    ={7}{0}" +
                       "isInstitusi	    ={8}{0}" +
                       "isCD	            ={9}{0}" +
                       "CDCode	            ={10}{0}" +
                       "isACD	            ={11}{0}" +
                       "ACDCode	        ={12}{0}" +
                       "remarks1	        ={13}{0}" +
                           "Data Activation:{0}" +
                       "memberStatusCode   ={14}{0}" +
                       "isActive           ={15}{0}" +
                       "isMember           ={16}{0}" +
                       "password           ={17}{0}" +
                           "Data Bank:{0}" +
                       "bankType           ={18}{0}" +
                       "bankCode           ={19}{0}" +
                       "bankBranchName     ={20}{0}" +
                       "bankAccNo          ={21}{0}" +
                       "bankAccName        ={1}"
                       , Environment.NewLine, input.memberBankData.bankAccName, input.memberData.parentMemberCode, input.memberData.PTName, input.memberData.PrincName, input.memberData.spouName
                       , input.memberData.specCode, input.memberData.franchiseGroup, input.memberData.isInstitusi, input.memberData.isCD, input.memberData.CDCode
                       , input.memberData.isACD, input.memberData.ACDCode, input.memberData.remarks1
                       , input.memberActivation.memberStatusCode, input.memberActivation.isActive, input.memberActivation.isMember, input.memberActivation.password
                       , input.memberBankData.bankType, input.memberBankData.bankCode, input.memberBankData.bankBranchName, input.memberBankData.bankAccNo);

                    _personalsMemberRepo.Update(update);
                    CurrentUnitOfWork.SaveChanges();
                    
                    obj.Add("message", "Successfully Updated");
                }
                // Handle data errors.
                catch (DataException exDb)
                {
                    Logger.ErrorFormat("UpdateMember() - ERROR DataException. Result = {0}", exDb.Message);
                    throw new UserFriendlyException("Database Error : {0}", exDb.Message);
                }
                // Handle all other exceptions.
                catch (Exception ex)
                {
                    Logger.ErrorFormat("UpdateMember() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error : {0}", ex.Message);
                }
            }
            else
            {
                Logger.ErrorFormat("UpdateMember() - ERROR. Result = {0}", "The member is not exist!");
                throw new UserFriendlyException("The member is not exist!");
            }

            Logger.Info("UpdateMember() - Finished.");

            return obj;
        }
        */

        [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_PersonalMember_DeleteMember)]
        public void DeleteMember(GetDeleteInputDto input)
        {
            Logger.Info("DeleteMember() - Started.");

            Logger.DebugFormat("DeleteMember() - Start get data for delete in Personals Member. Parameters sent:{0}" +
                        "entityCode = {1}{0}" +
                        "psCode     = {2}{0}" +
                        "scmCode    = {3}{0}"
                        , Environment.NewLine, 1, input.psCode, input.scmCode);

            var getSetMember = (from personalsMember in _personalsMemberRepo.GetAll()
                                where personalsMember.entityCode == "1"
                                && personalsMember.psCode == input.psCode
                                && personalsMember.scmCode == input.scmCode
                                select personalsMember).ToList();

            Logger.DebugFormat("DeleteMember() - End get data for delete in Personals Member.");

            if (getSetMember.Any())
            {
                foreach (var dataToEdit in getSetMember)
                {
                    dataToEdit.isMember = false;
                    try
                    {
                        Logger.DebugFormat("DeleteMember() - Start delete member. Params sent: {0}" +
                        "isActive     = {1}{0}"
                        , Environment.NewLine, false);

                        _personalsMemberRepo.Update(dataToEdit);
                        CurrentUnitOfWork.SaveChanges();

                        Logger.DebugFormat("DeleteMember() - End delete member.");
                    }
                    // Handle data errors.
                    catch (DataException exDb)
                    {
                        Logger.ErrorFormat("DeleteMember() - ERROR DataException. Result = {0}", exDb.Message);
                        throw new UserFriendlyException("Database Error : {0}", exDb.Message);
                    }
                    // Handle all other exceptions.
                    catch (Exception ex)
                    {
                        Logger.ErrorFormat("DeleteMember() - ERROR Exception. Result = {0}", ex.Message);
                        throw new UserFriendlyException("Error : {0}", ex.Message);
                    }

                }
            }
            Logger.Info("DeleteMember() - Finished.");
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_PersonalMember_DeleteSingleMember)]
        public void DeleteSingleMember(GetDeleteInputDto input)
        {
            Logger.Info("DeleteSingleMember() - Started.");

            Logger.DebugFormat("DeleteSingleMember() - Start get data for delete in Personals Member. Parameters sent:{0}" +
                        "entityCode = {1}{0}" +
                        "psCode     = {2}{0}" +
                        "scmCode    = {3}{0}" +
                        "memberCode = {4}{0}"
                        , Environment.NewLine, 1, input.psCode, input.scmCode, input.memberCode);

            var getSetMember = (from personalsMember in _personalsMemberRepo.GetAll()
                                where personalsMember.entityCode == "1"
                                && personalsMember.psCode == input.psCode
                                && personalsMember.scmCode == input.scmCode
                                && personalsMember.memberCode == input.memberCode
                                select personalsMember).FirstOrDefault();

            Logger.DebugFormat("DeleteSingleMember() - End get data for delete in Personals Member.");

            if (getSetMember != null)
            {
                getSetMember.isMember = false;
                try
                {
                    Logger.DebugFormat("DeleteSingleMember() - Start delete member. Params sent: {0}" +
                    "isActive     = {1}{0}"
                    , Environment.NewLine, false);

                    _personalsMemberRepo.Update(getSetMember);
                    CurrentUnitOfWork.SaveChanges();

                    Logger.DebugFormat("DeleteSingleMember() - End delete member.");
                }
                // Handle data errors.
                catch (DataException exDb)
                {
                    Logger.ErrorFormat("DeleteSingleMember() - ERROR DataException. Result = {0}", exDb.Message);
                    throw new UserFriendlyException("Database Error : {0}", exDb.Message);
                }
                // Handle all other exceptions.
                catch (Exception ex)
                {
                    Logger.ErrorFormat("DeleteSingleMember() - ERROR Exception. Result = {0}", ex.Message);
                    throw new UserFriendlyException("Error : {0}", ex.Message);
                }
            }
            Logger.Info("DeleteSingleMember() - Finished.");
        }
    }
}

