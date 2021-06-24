using Microsoft.AspNetCore.Mvc;
using PokeApi.AppService.Framework;

namespace PokeApi.Framework
{
    public static class ReportFileResponseExtensions
    {
        public static FileContentResult GetFileAsFileContentResult(this ReportFileResponse reportFileResponse)
        {
            return new FileContentResult(reportFileResponse.FileContent, reportFileResponse.ContentType)
            {
                FileDownloadName = reportFileResponse.Filename
            };
        }
    }
}
