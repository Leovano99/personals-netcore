using Abp.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace VDI.Demo.Files
{
    public interface IFilesHelper: IApplicationService
    {
        string MoveFiles(string filename, string oldPath, string newPath);
        string MoveFilesLegalDoc(string filename, string oldPath, string newPath);
        string ConvertIdToCode(int? Id);
        string ConvertDocIdToDocCode(int Id);
    }
}
