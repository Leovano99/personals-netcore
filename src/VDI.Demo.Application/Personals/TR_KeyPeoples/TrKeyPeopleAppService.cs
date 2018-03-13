using Abp.Domain.Repositories;
using Abp.UI;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Text;
using VDI.Demo.Personals.TR_KeyPeoples.Dto;
using VDI.Demo.PersonalsDB;
using Abp.AutoMapper;
using VDI.Demo.Authorization;
using Abp.Authorization;

namespace VDI.Demo.Personals.TR_KeyPeoples
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_TrKeyPeople)]
    public class TrKeyPeopleAppService : DemoAppServiceBase, ITrKeyPeopleAppService
    {
        private readonly IRepository<TR_KeyPeople> _trKeyPeopleRepo;

        public TrKeyPeopleAppService(IRepository<TR_KeyPeople> trKeyPeopleRepo)
        {
            _trKeyPeopleRepo = trKeyPeopleRepo;
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_TrKeyPeople_Delete)]
        public void DeleteTrKeyPeople(int trKeyPeopleID)
        {
            try
            {
                _trKeyPeopleRepo.Delete(trKeyPeopleID);
                CurrentUnitOfWork.SaveChanges();
            }
            
            // Handle data errors.
            catch (DataException exDb)
            {
                throw new UserFriendlyException("Database Error : {0}", exDb.Message);
            }
            // Handle all other exceptions.
            catch (Exception ex)
            {
                throw new UserFriendlyException("Error : {0}", ex.Message);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_TrKeyPeople_Edit)]
        public void UpdateTrJKeyPeople(UpdateTrJKeyPeopleInputDto input)
        {
            var getTrKeyPeople = (from x in _trKeyPeopleRepo.GetAll()
                                  where x.Id == input.Id
                                  select x).FirstOrDefault();

            var data = getTrKeyPeople.MapTo<TR_KeyPeople>();

            data.refID = input.refID;
            data.keyPeopleId = input.keyPeopleID;
            data.keyPeopleName = input.keyPeopleName;
            data.keyPeoplePSCode = input.keyPeoplePSCode;

            try
            {
                _trKeyPeopleRepo.Update(data);
                CurrentUnitOfWork.SaveChanges();
            }
            // Handle data errors.
            catch (DataException exDb)
            {
                throw new UserFriendlyException("Database Error : {0}", exDb.Message);
            }
            // Handle all other exceptions.
            catch (Exception ex)
            {
                throw new UserFriendlyException("Error : {0}", ex.Message);
            }
        }
    }
}