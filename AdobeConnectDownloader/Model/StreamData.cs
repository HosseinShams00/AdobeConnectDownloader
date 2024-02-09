using System;
using AdobeConnectDownloader.Enums;

namespace AdobeConnectDownloader.Model
{
    public class StreamData
    {
        public string FileNames { get; set; } = string.Empty;
        public string Extension { get; set; } = String.Empty;
        public uint StartFilesTime { get; set; }
        public uint EndFilesTime { get; set; }
        public uint Length { get; set; }
        public StreamContentTypeEnum StreamContentTypeEnum { get; set; } = StreamContentTypeEnum.Empty;

    }
}
