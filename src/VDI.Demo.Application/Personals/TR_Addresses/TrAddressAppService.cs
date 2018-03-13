using System;
using System.Data;
using System.Linq;
using Abp.Authorization;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.UI;
using VDI.Demo.Authorization;
using VDI.Demo.Personals.TR_Addresses.Dto;
using VDI.Demo.PersonalsDB;

namespace VDI.Demo.Personals.TR_Addresses
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_TrAddress)]
    public class TrAddressAppService : DemoAppServiceBase, ITrAddressAppService
    {
        #region constructor
        private readonly IRepository<TR_Address, string> _trAddressRepo;

        public TrAddressAppService
            (
                IRepository<TR_Address, string> trAddressRepo
            )
        {
            _trAddressRepo = trAddressRepo;
        }
        #endregion

        [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_TrAddress_Edit)]
        public void UpdateAddress(GetUpdateAddressInputDto input)
        {
            var getSetAddress = (from address in _trAddressRepo.GetAll()
                                 where address.entityCode == "1"
                                 && address.psCode == input.psCode
                                 && address.refID == input.refID
                                 && address.addrType == input.addrType
                                 select address).FirstOrDefault();

            if (getSetAddress != null)
            {
                var data = getSetAddress.MapTo<TR_Address>();
                data.address = input.address;
                data.city = input.city;
                data.Kelurahan = input.kelurahan;
                data.Kecamatan = input.kecamatan;
                data.postCode = input.postCode;
                data.country = input.country;

                try
                {
                    _trAddressRepo.Update(data);
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
                throw new UserFriendlyException("Address that you looking for is not exist!");
            }
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_Personal_TrAddress_Delete)]
        public void DeleteAddress(GetDeleteAddressInputDto input)
        {
            var getSetAddress = (from address in _trAddressRepo.GetAll()
                                 where address.entityCode == "1"
                                 && address.psCode == input.psCode
                                 && address.refID == input.refID
                                 && address.addrType == input.addrType
                                 select address).FirstOrDefault();

            if (getSetAddress != null)
            {
                try
                {
                    _trAddressRepo.Delete(getSetAddress);
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
                throw new UserFriendlyException("Address that you looking for is not exist!");
            }
        }
    }
}

