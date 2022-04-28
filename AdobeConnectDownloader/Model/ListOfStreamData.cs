using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdobeConnectDownloader.Model
{
    public class ListOfStreamData
    {
        public List<StreamData> AudioStreamData { get; set; } = new List<StreamData>();
        public List<StreamData> ScreenStreamData { get; set; } = new List<StreamData>();

    }
}
