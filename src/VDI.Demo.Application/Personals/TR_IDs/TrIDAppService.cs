using Abp.Domain.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Personals.TR_IDs.Dto;
using VDI.Demo.PersonalsDB;
using System.Data;
using Abp.UI;
using Abp.AutoMapper;
using VDI.Demo.Authorization;
using Abp.Authorization;

namespace VDI.Demo.Personals.TR_IDs
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_TrId)]
    public class TrIDAppService : DemoAppServiceBase, ITrIDAppService
    {
        private readonly IRepository<TR_ID, string> _trIDRepo;

        public TrIDAppService(IRepository<TR_ID, string> trIDRepo)
        {
            _trIDRepo = trIDRepo;
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_TrId_Delete)]
        public void DeleteTrID(DeleteTrIDInputDto input)
        {
            var getTrID = (from x in _trIDRepo.GetAll()
                           where x.entityCode == "1" && x.psCode == input.psCode && x.refID == input.refID
                           select x).FirstOrDefault();
            try
            {
                _trIDRepo.Delete(getTrID);
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

        [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_TrId_Edit)]
        public void UpdateTrID(UpdateTrIDInputDto input)
        {
            var getTrID = (from x in _trIDRepo.GetAll()
                           where x.entityCode == "1" && x.psCode == input.psCode && x.refID == input.refID
                           select x).FirstOrDefault();

            var data = getTrID.MapTo<TR_ID>();

            data.idType = input.idType;
            data.idNo = input.idNo;
            data.expiredDate = input.expiredDate;

            try
            {
                _trIDRepo.Update(data);
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
