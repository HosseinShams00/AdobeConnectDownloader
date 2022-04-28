namespace AdobeConnectDownloader.Model
{
    public class StreamData
    {
        public string FileNames { get; set; } = string.Empty;
        public uint StartFilesTime { get; set; }
        public uint EndFilesTime { get; set; }
        public uint Length { get; set; }

    }
}
