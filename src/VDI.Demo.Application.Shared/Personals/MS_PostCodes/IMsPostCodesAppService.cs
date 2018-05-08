using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services;
using VDI.Demo.Personals.MS_PostCodes.Dto;

namespace VDI.Demo.Personals.MS_PostCodes
{
    public interface  IMsPostCodesAppService : IApplicationService
    {
        List<GetMsPostCodeListDto> GetPostCodeByCity(string cityCode); 
    }
}
