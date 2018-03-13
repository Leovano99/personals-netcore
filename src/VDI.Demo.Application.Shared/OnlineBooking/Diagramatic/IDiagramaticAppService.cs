using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VDI.Demo.OnlineBooking.Diagramatic.Dto;

namespace VDI.Demo.OnlineBooking.Diagramatic
{
    public interface IDiagramaticAppService : IApplicationService
    {
        List<GetDiagramaticResultDto> GetListDiagramatic(DiagramaticMobileInputDto input);

        List<GetDiagramaticForWebListDto> GetListDiagramaticWeb(DiagramaticInputDto input);

        List<ListRenovationResultDto> GetListRenovation(int unitID);

        List<ListTermResultDto> GetListTerm(int unitID);

        GetUnitSelectionDetailDto GetUnitSelectionDetail(int unitID);

        ListResultDto<ListPriceResultDto> ListPrice(int unitCodeID, int renovId);

        GrossPriceDto GetGrossPrice(GetGrossPriceInputDto input);

        ListResultDto<ListTowerResultDto> GetListTowerByProjectID(int projectId);

        List<ListBedroomResultDto> GetListBedroom(int projectID, int clusterID);

        List<ListZoningResultDto> GetListZoning(int projectID, int clusterID);

        List<ListSumberDanaResultDto> GetListSumberDana();

        List<ListTujuanTransaksiResultDto> GetListTujuanTransaksi();

        ListDetailDiagramaticWebResultDto GetDetailDiagramatic(int unitID);

        List<ListPaymentTypeResultDto> GetPaymentType();

        GetUnitSelectionDetailDto GetUnitSelectionDetailMobile(int unitID);

    }
}
