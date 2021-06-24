using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace PokeApi.AppService.Framework
{
    public static class ReportFileResponseExtensions
    {
        public static HttpResponseMessage GetFileAsHttpResponseMessage(this ReportFileResponse reportFileResponse)
        {
            var httpResult = new HttpResponseMessage(HttpStatusCode.OK);

            string fileName;

            if (reportFileResponse != null)
            {
                httpResult.Content = new ByteArrayContent(reportFileResponse.FileContent);

                fileName = reportFileResponse.Filename;

                httpResult.Content.Headers.ContentType = new MediaTypeHeaderValue(reportFileResponse.ContentType);
            }
            else
            {
                fileName = "Descarga de Archivo Vacio";

                fileName += ".txt";

                httpResult.Content.Headers.ContentType = new MediaTypeHeaderValue("text/plain");
            }

            httpResult.Content.Headers.ContentDisposition =
                new ContentDispositionHeaderValue("attachment")
                {
                    FileName = fileName
                };

            httpResult.Content.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return httpResult;
        }
    }
}
