using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.UI;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VDI.Demo.Pricing.TR_BasePrices.Dto;
using VDI.Demo.PropertySystemDB.Pricing;

namespace VDI.Demo.Pricing.TR_BasePrices
{
    public class TrBasePriceAppService : DemoAppServiceBase, ITrBasePriceAppService
    {
        private readonly IRepository<TR_BasePrice> _trBasePriceRepo;

        public TrBasePriceAppService(
            IRepository<TR_BasePrice> trBasePriceRepo

            )
        {
            _trBasePriceRepo = trBasePriceRepo;
        }

        public void UploadBasePrice(UploadBasePriceInputDto input)
        {
            if (input.categoryName.ToLower() == "highrise")
            {
                foreach (var basePrice in input.BasePrices)
                {
                    bool checkAvailable = checkAvailableHighriseUnit(input.projectCode, basePrice.unitCode, basePrice.unitNo);
                    if (!checkAvailable)
                    {
                        var data = new TR_BasePrice
                        {
                            projectCode = input.projectCode,
                            unitCode = basePrice.unitCode,
                            unitNo = basePrice.unitNo,
                            unitBasePrice = basePrice.unitBasePrice
                        };
                        _trBasePriceRepo.InsertAsync(data);
                    }
                }
            }
            else if (input.categoryName.ToLower() == "landed")
            {
                foreach (var basePrice in input.BasePrices)
                {
                    bool checkAvailable = checkAvailableLandedUnit(input.projectCode, basePrice.unitCode, basePrice.unitNo);
                    if (!checkAvailable)
                    {
                        var data = new TR_BasePrice
                        {
                            projectCode = input.projectCode,
                            roadCode = basePrice.unitCode,
                            unitNo = basePrice.unitNo,
                            unitBasePrice = basePrice.unitBasePrice
                        };
                        _trBasePriceRepo.InsertAsync(data);
                    }
                }
            }
        }

        private bool checkAvailableLandedUnit(string projectCode, string roadCode, string unitNo)
        {
            return (from basePrice in _trBasePriceRepo.GetAll()
                    where basePrice.projectCode == projectCode
                    && basePrice.roadCode == roadCode
                    && basePrice.unitNo == unitNo
                    select basePrice).Any();
        }

        private bool checkAvailableHighriseUnit(string projectCode, string unitCode, string unitNo)
        {
            return (from basePrice in _trBasePriceRepo.GetAll()
                    where basePrice.projectCode == projectCode
                    && basePrice.unitCode == unitCode
                    && basePrice.unitNo == unitNo
                    select basePrice).Any();
        }

        public async Task<PagedResultDto<GetAllBasePriceListDto>> GetAllTrBasePrice(GetBasePriceListInputDto input)
        {

            bool checkProjectCode = (from x in _trBasePriceRepo.GetAll()
                                     where x.projectCode == input.ProjectCode
                                     select x).Any();
            if (checkProjectCode)
            {
                var listResult = (from x in _trBasePriceRepo.GetAll()
                                  where x.projectCode == input.ProjectCode
                                  select new GetAllBasePriceListDto
                                  {
                                      basePriceID = x.Id,
                                      projectCode = x.projectCode,
                                      roadCode = x.roadCode,
                                      unitBasePrice = x.unitBasePrice,
                                      unitCode = x.unitCode,
                                      unitNo = x.unitNo
                                  }).WhereIf(
                                !input.Filter.IsNullOrWhiteSpace(),
                                u =>
                                    u.projectCode.Contains(input.Filter) ||
                                    u.roadCode.Contains(input.Filter) ||
                                    u.unitBasePrice.Equals(input.Filter) ||
                                    u.unitCode.Contains(input.Filter) ||
                                    u.unitNo.Contains(input.Filter)
                            );
                int dataCount = await listResult.AsQueryable().CountAsync();

                var resultList = await listResult.AsQueryable()
                    .OrderByDescending(s => s.basePriceID)
                    .PageBy(input)
                    .ToListAsync();

                var listDtos = resultList;
                return new PagedResultDto<GetAllBasePriceListDto>(
                    dataCount,
                    listDtos);
            }
            else
            {
                throw new UserFriendlyException("Project Code Is Not Exist !");
            }
        }
    }
}
