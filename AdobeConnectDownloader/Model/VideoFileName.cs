namespace AdobeConnectDownloader.Model
{
    public record VideoFileName
    {
        public string FileId { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
    }
}
