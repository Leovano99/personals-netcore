using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Commission.TR_SoldUnits.Dto;

namespace VDI.Demo.Commission.TR_SoldUnits
{
    public interface ITrSoldUnitAppService : IApplicationService
    {
        List<GetDataTasklistTRSoldUnitListDto> GetDataTasklistTRSoldUnit(int projectID);
        GetDataDealCloserDto GetSetCommissionDataDealCloser(string bookNo);
        GetDataDefineUnitListDto GetDataDefineUnit(int soldUnitID);
        List<GetDataSchemaRequirementListDto> GetDataSchemaRequirement(string bookNo);
        List<GetDataAllMemberListDto> GetSetCommissionDataMember(string bookNo);
        GetDataSetCommissionUniversalListDto GetSetCommissionUniversal(string bookNo);
        List<GetDataPropertyByProjectListDto> GetDataPropertyByProject(int projectID);
        List<TR_SoldUnitListDto> GetDataTrxToInsert(string unitCode, string unitNo);
        void BulkInsertTrxToSoldUnit(List<TR_SoldUnitListDto> data);
        List<string> GetPaymentTermDropdown(string termCode);
        List<GetUplineByMemberCodeListDto> GetUplineByMemberCode(string memberCode, int countAsUplineNo);
        List<GetDataMemberUplineInsertListDto> GetDataMemberUplineInsert(GetDataMemberUplineInsertInputDto input, int limitAsUplineNo);
        List<TR_CommPctListDto> GetIdForUpdateOrInsertMemberUplineTrCommPct(List<TR_CommPctListDto> input); //Untuk menambahkan ID untuk update, ID null untuk insert
        List<TR_CommPctListDto> GetMemberToInsert(List<TR_SoldUnitListDto> input, int limitAsUplineNo); //Update Deal Closer hanya insert ke Tr_CommPct

        List<GetMemberToInsertManualListDto> GetDataMemberToInsertManual(GetDataMemberToInsertManualInputDto input);
        void HoldSoldUnit(string bookNo, string holdReason);
        void CancelSoldUnit(string bookNo);
        string GetMemberByUnit(string roadCode, string unitNo);
        void UpdateAdjustment(string bookNo, string memberCode, double commPctPaid);
        void BulkInsertManual(BulkInsertManualInputDto input);
        void BulkInsertOrUpdateTRSoldUnitReq(List<TR_SoldRequirementListDto> input);

        List<TR_SoldRequirementListDto> GetDataRequirementToInsert(List<TR_SoldUnitListDto> input);

        void CreateTrCommPayment(List<CommPaymentInputDto> input);
        void BulkInsertOrUpdateMember(List<TR_CommPctListDto> input);
        void BulkInsertUniversal(int limitAsUplineNo);
    }
}
