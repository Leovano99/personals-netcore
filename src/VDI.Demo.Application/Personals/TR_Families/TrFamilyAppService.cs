using Abp.Domain.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Personals.TR_Families.Dto;
using VDI.Demo.PersonalsDB;
using System.Data;
using Abp.UI;
using Abp.AutoMapper;
using VDI.Demo.Authorization;
using Abp.Authorization;

namespace VDI.Demo.Personals.TR_Families
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_TrFamily)]
    public class TrFamilyAppService : DemoAppServiceBase, ITrFamilyAppService
    {
        private readonly IRepository<TR_Family, string> _trFamilyRepo;

        public TrFamilyAppService(
            IRepository<TR_Family, string> trFamilyRepo
            )
        {
            _trFamilyRepo = trFamilyRepo;
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_TrFamily_Delete)]
        public void DeleteTrFamily(string psCode, int refID)
        {
            var getFamily = (from families in _trFamilyRepo.GetAll()
                             where families.entityCode == "1"
                             && families.psCode == psCode
                             && families.refID == refID
                             select families).FirstOrDefault();

            try
            {
                _trFamilyRepo.Delete(getFamily);
                CurrentUnitOfWork.SaveChanges();
            }
            catch (DataException ex)
            {
                throw new UserFriendlyException("Db Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException("Error: " + ex.Message);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_TrFamily_Edit)]
        public void UpdateTrFamily(UpdateTrFamilyListDto input)
        {
            var getFamily = (from families in _trFamilyRepo.GetAll()
                             where families.entityCode == "1"
                             && families.psCode == input.psCode
                             && families.refID == input.refID
                             select families).FirstOrDefault();

            var updateFamily = getFamily.MapTo<TR_Family>();

            updateFamily.familyName = input.familyName;
            updateFamily.familyStatus = input.familyStatus;
            updateFamily.birthDate = input.birthDate;
            updateFamily.occID = input.occID;

            try
            {
                _trFamilyRepo.Update(updateFamily);
                CurrentUnitOfWork.SaveChanges(); //execution saved inside try
            }
            catch (DataException ex)
            {
                throw new UserFriendlyException("Db Error: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw new UserFriendlyException("Error: " + ex.Message);
            }
        }
    }
}

