using Abp.Domain.Repositories;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Personals.TR_Phones.Dto;
using VDI.Demo.PersonalsDB;
using Abp.AutoMapper;
using System.Data;
using Abp.UI;
using VDI.Demo.Authorization;
using Abp.Authorization;

namespace VDI.Demo.Personals.TR_Phones
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_TrPhone)]
    public class TrPhoneAppService : DemoAppServiceBase, ITrPhoneAppService
    {
        #region constructor
        private readonly IRepository<TR_Phone, string> _trPhoneRepo;

        public TrPhoneAppService(
            IRepository<TR_Phone, string> trPhoneRepo
            )
        {
            _trPhoneRepo = trPhoneRepo;
        }
        #endregion

        [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_TrPhone_Edit)]
        public void UpdateTrPhone(List<GetUpdateTrPhoneInputDto> inputs)
        {
            foreach (var input in inputs)
            {
                var getSetPhone = (from phone in _trPhoneRepo.GetAll()
                                   where phone.entityCode == "1"
                                   && phone.psCode == input.psCode
                                   && phone.refID == input.refID
                                   select phone).FirstOrDefault();

                if (getSetPhone != null)
                {
                    var data = getSetPhone.MapTo<TR_Phone>();
                    data.number = input.number;

                    try
                    {
                        _trPhoneRepo.Update(data);
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
                else
                {
                    throw new UserFriendlyException("Phone that you search is not exist!");
                }
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_TrPhone_Delete)]
        public void DeleteTrPhone(GetDeleteTrPhoneInputDto input)
        {
            var getSetPhone = (from phone in _trPhoneRepo.GetAll()
                               where phone.entityCode == "1"
                               && phone.psCode == input.psCode
                               && phone.refID == input.refID
                               select phone).FirstOrDefault();
            if (getSetPhone != null)
            {
                try
                {
                    _trPhoneRepo.Delete(getSetPhone);
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
            else
            {
                throw new UserFriendlyException("Phone that you search is not exist!");
            }
        }

    }
}
