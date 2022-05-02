using System.Collections.Generic;
using AdobeConnectDownloader.Model;


namespace AdobeConnectDownloader.Application
{
    public class AudioLineData
    {
        public List<StreamData> Result { get; set; } = new List<StreamData>();
        public List<StreamData> NewInput { get; set; } = new List<StreamData>();
    }
}
