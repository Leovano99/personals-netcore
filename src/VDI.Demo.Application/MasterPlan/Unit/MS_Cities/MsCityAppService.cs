using System;
using System.Data;
using System.Linq;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.UI;
using VDI.Demo.Authorization;
using VDI.Demo.MasterPlan.Unit.MS_Cities.Dto;
using VDI.Demo.PropertySystemDB.MasterPlan.Unit;

namespace VDI.Demo.MasterPlan.Unit.MS_Cities
{
    [AbpAuthorize(AppPermissions.Pages_Tenant_MasterCity)]
    public class MsCityAppService : DemoAppServiceBase, IMsCityAppService
    {
        private readonly IRepository<MS_City> _msCityRepo;

        public MsCityAppService(
            IRepository<MS_City> msCityRepo
            )
        {
            _msCityRepo = msCityRepo;
        }

        [AbpAuthorize(AppPermissions.Pages_Tenant_MasterCity_Create)]
        public void CreateMsCity(GetCreateMsCityInputDto input)
        {
            var cekCityName = (from A in _msCityRepo.GetAll()
                               where A.cityName == input.cityName && A.countyID == input.countyID
                               select A).FirstOrDefault();

            if (cekCityName == null)
            {
                var createMsCity = new MS_City
                {
                    cityName = input.cityName,
                    countyID = input.countyID
                };

                try
                {
                    _msCityRepo.Insert(createMsCity);
                    CurrentUnitOfWork.SaveChanges();
                }
                /*catch (DbEntityValidationException ex)
                {
                    var errorMessages = ex.EntityValidationErrors
                                            .SelectMany(x => x.ValidationErrors)
                                            .Select(x => x.ErrorMessage);
                    string fullErrorMessage = string.Join("; ", errorMessages);
                    string exceptionMessage = string.Concat("Validation Error: ", fullErrorMessage);
                    throw new UserFriendlyException(exceptionMessage);
                }*/
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
                throw new UserFriendlyException("City is exist!");
            }
        }

        public ListResultDto<GetMsCityListDto> GetAllDropdownMsCity(int countyID)
        {
            var dataCity = (from A in _msCityRepo.GetAll()
                            where A.countyID == countyID
                            select new GetMsCityListDto
                            {
                                cityID = A.Id,
                                cityName = A.cityName,
                                countyID = countyID
                            }).ToList();

            return new ListResultDto<GetMsCityListDto>(dataCity);
        }
    }
}
