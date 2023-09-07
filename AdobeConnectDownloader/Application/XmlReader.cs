using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using AdobeConnectDownloader.Model;


namespace AdobeConnectDownloader.Application
{
    public class XmlReader
    {
        public static ListOfStreamData FindTimesOfFiles(List<string> fileNames, string xmlFileData, uint endTimeOfRoom)
        {
            List<StreamData> AudioStreamData = new List<StreamData>();
            List<StreamData> ScreenStreamData = new List<StreamData>();

            foreach (var item in fileNames)
            {
                if (item.StartsWith("cameraVoip_"))
                {
                    string filename = item.Substring(0, item.Length - 4);
                    var x = FindStreamTimes(xmlFileData, filename, endTimeOfRoom);
                    AudioStreamData.Add(x);

                }
                else if (item.StartsWith("screenshare_"))
                {
                    string filename = item.Substring(0, item.Length - 4);
                    var x = FindStreamTimes(xmlFileData, filename, endTimeOfRoom);
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

        public static ListOfStreamData FindTimesOfFilesV2(string xmlFileData)
        {
            var audioStreamData = new List<StreamData>();
            var screenStreamData = new List<StreamData>();

            var streamDatas = FindStreamsFileTime_V2(xmlFileData);
            foreach (var streamData in streamDatas)
            {
                if (streamData.FileNames.StartsWith("cameraVoip_"))
                {
                    audioStreamData.Add(streamData);
                }
                else if (streamData.FileNames.StartsWith("screenshare_"))
                {
                    screenStreamData.Add(streamData);
                }
            }

            audioStreamData = audioStreamData.OrderBy(i => i.StartFilesTime).ToList();

            if (screenStreamData.Count != 0 || screenStreamData.Count != 1)
                screenStreamData = screenStreamData.OrderBy(i => i.StartFilesTime).ToList();

            return new ListOfStreamData()
            {
                AudioStreamData = audioStreamData,
                ScreenStreamData = screenStreamData
            };

        }

        private static StreamData FindStreamTimes(string xmlFileData, string searchData, uint endTimeOfRoom)
        {
            string streamNameXml = $"<streamName><![CDATA[/{searchData}]]></streamName>";
            string startTimeString = "<time><![CDATA[";
            string endStartTimestring = "]]></time>";
            uint EndFileTime = endTimeOfRoom;

            int index1 = xmlFileData.IndexOf(streamNameXml) + streamNameXml.Length;

            int startTimeIndex = xmlFileData.IndexOf(startTimeString, index1) + startTimeString.Length;
            int endTimeIndex = xmlFileData.IndexOf(endStartTimestring, startTimeIndex);
            string startTime = xmlFileData.Substring(startTimeIndex, (endTimeIndex - startTimeIndex));


            int index2 = xmlFileData.IndexOf(streamNameXml, endTimeIndex);

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

        private static List<StreamData> FindStreamsFileTime_V2(string xmlFileData)
        {
            var str = XElement.Parse(xmlFileData);
            var streamDatas = str.Elements("Message").Where(x =>
                {
                    var isRemoveStream = x.Element("String")?.Value.Equals("streamRemoved") ?? false;
                    var xElementsArray = x.Elements("Array") ?? new List<XElement>(1);
                    var xElements = xElementsArray.Elements("Object") ?? new List<XElement>(1);
                    return isRemoveStream && xElementsArray.Any() && xElements.Any() && xElements.Elements("startTime").Any();
                })
                .Select(x =>
                {
                    var objectElement = x.Element("Array")?.Element("Object");
                    var startTime = uint.Parse(objectElement?.Element("startTime")?.Value ?? "0");
                    var streamName = objectElement?.Element("streamName")?.Value.Trim('/');
                    var endTime = uint.Parse(x.Element("Object")?.Element("time")?.Value ?? "0");
                    return new StreamData
                    {
                        StartFilesTime = startTime,
                        FileNames = streamName ?? string.Empty,
                        EndFilesTime = endTime,
                        Length = endTime - startTime,
                        Extension = ".flv"
                    };
                })
                .ToList();


            return streamDatas;
        }

        public static uint FindEndOfTime(string xmlFileData)
        {
            string endTimeString = "<String><![CDATA[__stop__]]></String>";
            string numberStr = "<Number><![CDATA[";
            string endNumberStr = "]]></Number>";

            string endItemString = "<Message time=\"";
            string timeValue = string.Empty;
            int indexStop = xmlFileData.IndexOf(endTimeString);
            if (indexStop != -1)
            {
                indexStop += endTimeString.Length;
                int indexData1 = xmlFileData.IndexOf(numberStr, indexStop) + numberStr.Length;
                int indexData2 = xmlFileData.IndexOf(endNumberStr, indexData1);
                timeValue = xmlFileData.Substring(indexData1, indexData2 - indexData1);
            }
            else
            {
                int lastTimeIndex = xmlFileData.LastIndexOf(endItemString) + endItemString.Length;
                int indexData1 = xmlFileData.IndexOf("\"", lastTimeIndex);
                timeValue = xmlFileData.Substring(lastTimeIndex, indexData1 - lastTimeIndex);

            }

            uint data = uint.Parse(timeValue);

            return data;
        }

        public static uint FindEndOfTimeV2(string xmlFileData)
        {
            var str = XElement.Parse(xmlFileData);
            var timeValue = str.XPathSelectElements("Message//String").FirstOrDefault(x => x.Value.Equals("__stop__"))
                ?.Parent?.Attribute("time").Value ?? "0";
            return uint.Parse(timeValue);
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
                resultWitoutDuplicateValue[i] = $"{hotst}{value1}data/";
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

            if (xMaxSize.IndexOf(".") != -1)
                pdfDetail.XSize = int.Parse(xMaxSize.Split('.')[0]) + 1;
            else
                pdfDetail.XSize = int.Parse(xMaxSize);


            string yValueStr = "yMax=\"";
            int indexYvalue = xmlData.IndexOf(yValueStr, index2) + yValueStr.Length;
            index2 = xmlData.IndexOf("\"", indexYvalue);
            string yMaxSize = xmlData.Substring(indexYvalue, index2 - indexYvalue);

            if (xMaxSize.IndexOf(".") != -1)
                pdfDetail.YSize = int.Parse(yMaxSize.Split('.')[0]) + 1;
            else
                pdfDetail.YSize = int.Parse(yMaxSize);


            return pdfDetail;
        }

        public static List<string> GetFilesDownloadLink(string xmlFileData, string baseUrl)
        {
            List<string> files = new List<string>();
            string theUrlStringStart = "<theUrl><![CDATA[/_a7/";
            string theUrlStringEnd = "]]></theUrl>";
            int index1 = 0;

            while (true)
            {

                index1 = xmlFileData.IndexOf(theUrlStringStart, index1);

                if (index1 == -1)
                    break;

                index1 += theUrlStringStart.Length;
                int index2 = xmlFileData.IndexOf(theUrlStringEnd, index1);
                files.Add(baseUrl + xmlFileData.Substring(index1, index2 - index1));
                index1 = index2;
            }

            if (files.Count != 0)
                files = files.Distinct().ToList();

            return files;

        }


        public class Test
        {
            public static Dictionary<string, object> ExtractDataStreamTimeStamps(string xmlFile)
            {
                Dictionary<string, object> tempSession = new Dictionary<string, object>();
                List<Dictionary<string, object>> dataStreamList = new List<Dictionary<string, object>>();

                try
                {
                    XmlDocument indexStream = new XmlDocument();
                    indexStream.LoadXml(xmlFile);

                    XmlNodeList messages = indexStream.GetElementsByTagName("Message");

                    foreach (XmlNode messageNode in messages)
                    {
                        XmlElement messageEl = (XmlElement)messageNode;
                        string name = GetFirstElementByTagName(messageEl, "name")?.InnerText ?? "";
                        string status = GetFirstElementByTagName(messageEl, "String")?.InnerText ?? "";

                        if (name == "StreamManagerId_Mainstream" && status == "streamRemoved")
                        {
                            Dictionary<string, object> tds = new Dictionary<string, object>();

                            tds["endTimeStamp"] = GetFirstElementByTagName(messageEl, "time")?.InnerText;
                            tds["startTimeStamp"] = GetFirstElementByTagName(messageEl, "startTime")?.InnerText;
                            tds["name"] = GetFirstElementByTagName(messageEl, "streamName")?.InnerText;
                            tds["senderId"] = GetFirstElementByTagName(messageEl, "streamPublisherID")?.InnerText;
                            tds["type"] = GetFirstElementByTagName(messageEl, "streamType")?.InnerText;

                            dataStreamList.Add(tds);
                        }
                        else if (status == "__stop__")
                        {
                            tempSession["duration"] = GetFirstElementByTagName(messageEl, "Number")?.InnerText;
                        }
                    }

                    tempSession["dataStreamList"] = dataStreamList;
                    // Assuming mSessions is a class-level variable, you can modify this accordingly
                    mSessions.Add(tempSession);
                    
                }
                catch (IOException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }

                return tempSession;
            }

            private static XmlNode GetFirstElementByTagName(XmlElement parentElement, string tagName)
            {
                XmlNodeList elements = parentElement.GetElementsByTagName(tagName);
                return elements.Count > 0 ? elements[0] : null;
            }

            private static List<Dictionary<string, object>> mSessions = new List<Dictionary<string, object>>();
        }
    }
}
