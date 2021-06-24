namespace PokeApi.AppService.Framework
{
    public class ReportFileResponse
    {
        public string ContentType { get; set; } = "application/octet-stream";
        public string Filename { get; set; }
        public byte[] FileContent { get; set; }
    }
}
