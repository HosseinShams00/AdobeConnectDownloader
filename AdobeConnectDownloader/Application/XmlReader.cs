using System.Collections.Generic;
using System.Linq;
using AdobeConnectDownloader.Model;


namespace AdobeConnectDownloader.Application
{
    public class XmlReader
    {
        public static ListOfStreamData GetTimesOfFiles(List<string> fileNames, string xmlFileData, uint endTimeOfRoom)
        {
            List<StreamData> AudioStreamData = new List<StreamData>();
            List<StreamData> ScreenStreamData = new List<StreamData>();

            foreach (var item in fileNames)
            {
                if (item.StartsWith("cameraVoip_"))
                {
                    string filename = item.Substring(0, item.Length - 4);
                    var x = GetStreamTimes(xmlFileData, filename, endTimeOfRoom);
                    AudioStreamData.Add(x);

                }
                else if (item.StartsWith("screenshare_"))
                {
                    string filename = item.Substring(0, item.Length - 4);
                    var x = GetStreamTimes(xmlFileData, filename, endTimeOfRoom);
                    ScreenStreamData.Add(x);
                }
            }

            AudioStreamData = AudioStreamData.OrderBy(i => i.StartFilesTime).ToList();

            if (ScreenStreamData.Count != 0 || ScreenStreamData.Count != 1)
                ScreenStreamData = ScreenStreamData.OrderBy(i => i.StartFilesTime).ToList();

            return new ListOfStreamData()
            {
                AudioStreamData = AudioStreamData,
                ScreenStreamData = ScreenStreamData
            };

        }

        private static StreamData GetStreamTimes(string xmlFileData, string searchData, uint endTimeOfRoom)
        {
            string streamNameXml = $"<streamName><![CDATA[/{searchData}]]></streamName>";
            string startTimeString = "<time><![CDATA[";
            string endStartTimestring = "]]></time>";
            uint EndFileTime = endTimeOfRoom;

            int index1 = xmlFileData.IndexOf(streamNameXml) + streamNameXml.Length;

            int startTimeIndex = xmlFileData.IndexOf(startTimeString, index1) + startTimeString.Length;
            int endTimeIndex = xmlFileData.IndexOf(endStartTimestring, startTimeIndex);
            string startTime = xmlFileData.Substring(startTimeIndex, (endTimeIndex - startTimeIndex));


            int index2 = xmlFileData.IndexOf(streamNameXml, index1);

            if (index2 != -1)
            {
                int endTimeValueIndex = xmlFileData.IndexOf(startTimeString, index2) + startTimeString.Length;
                int endTimeValueIndex2 = xmlFileData.IndexOf(endStartTimestring, endTimeValueIndex);
                string endTimeValue = xmlFileData.Substring(endTimeValueIndex, (endTimeValueIndex2 - endTimeValueIndex));
                EndFileTime = uint.Parse(endTimeValue);
            }

            return new StreamData()
            {
                FileNames = searchData,
                StartFilesTime = uint.Parse(startTime),
                EndFilesTime = EndFileTime
            };
        }

        public static uint GetEndOfTime(string xmlFileData)
        {
            string endTimeString = "<String><![CDATA[__stop__]]></String>";
            string numberStr = "<Number><![CDATA[";
            string endNumberStr = "]]></Number>";

            int indexStop = xmlFileData.IndexOf(endTimeString) + endTimeString.Length;
            int indexData1 = xmlFileData.IndexOf(numberStr, indexStop) + numberStr.Length;
            int indexData2 = xmlFileData.IndexOf(endNumberStr, indexData1);
            string timeValue = xmlFileData.Substring(indexData1, indexData2 - indexData1);

            uint data = uint.Parse(timeValue);

            return data;
        }

        public static List<string> GetDefaultPdfPathForDownload(string xmlFileData, string hotst)
        {
            string playbackContentOutputPathStart = "<playbackContentOutputPath><![CDATA[";
            string playbackContentOutputPathEnd = "]]></playbackContentOutputPath>";
            int counter = 1;

            List<string> results = new List<string>();

            while (true)
            {
                int indexData1 = xmlFileData.IndexOf(playbackContentOutputPathStart, counter);
                if (indexData1 == -1)
                    break;
                indexData1 += playbackContentOutputPathStart.Length;

                int indexData2 = xmlFileData.IndexOf(playbackContentOutputPathEnd, indexData1);
                string result = xmlFileData.Substring(indexData1, (indexData2 - indexData1));
                counter = indexData2 + playbackContentOutputPathEnd.Length;

                if (result.Trim() != "")
                {
                    results.Add(result);
                }

            }

            List<string> resultWitoutDuplicateValue = results.Distinct().ToList();
            for (int i = 0; i < resultWitoutDuplicateValue.Count; i++)
            {
                string value1 = resultWitoutDuplicateValue[i].Substring(5);
                resultWitoutDuplicateValue[i] = $"{hotst}{ value1}data/";
            }


            return resultWitoutDuplicateValue;

        }

        public static PdfDetail GetPdfDetail(string xmlData)
        {
            PdfDetail pdfDetail = new PdfDetail();

            string pageNumberStr = "<Pages Number=\"";
            int indexPageNumber = xmlData.IndexOf(pageNumberStr) + pageNumberStr.Length;
            int index2 = xmlData.IndexOf("\"", indexPageNumber);
            string pageNumber = xmlData.Substring(indexPageNumber, index2 - indexPageNumber);

            pdfDetail.PageNumber = int.Parse(pageNumber);

            string xValueStr = "<Page xMax=\"";
            int indexXvalue = xmlData.IndexOf(xValueStr, index2) + xValueStr.Length;
            index2 = xmlData.IndexOf("\"", indexXvalue);
            string xMaxSize = xmlData.Substring(indexXvalue, index2 - indexXvalue);

            pdfDetail.XSize = int.Parse(xMaxSize);

            string yValueStr = "yMax=\"";
            int indexYvalue = xmlData.IndexOf(yValueStr, index2) + yValueStr.Length;
            index2 = xmlData.IndexOf("\"", indexYvalue);
            string yMaxSize = xmlData.Substring(indexYvalue, index2 - indexYvalue);

            pdfDetail.YSize = int.Parse(yMaxSize);

            return pdfDetail;
        }

    }
}
