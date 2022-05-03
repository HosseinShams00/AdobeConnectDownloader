using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using AdobeConnectDownloader.Model;

namespace AdobeConnectDownloader.Application
{

    public delegate void PercentageChange(int percent, double byteRecive, double totalByte);
    public delegate void DownloadFileComplited(object? sender, System.ComponentModel.AsyncCompletedEventArgs e);

    public class WebManager
    {

        private WebClient webClient { get; set; } = null;

        public event PercentageChange PercentageChange;
        public event DownloadFileComplited DownloadFileComplited;
        public static VideoFileName GetDownloadUrl(string url)
        {
            var linkSplit = url.Split('/');
            string id = linkSplit[3];

            return new VideoFileName()
            {
                FileId = id,
                Url = $"{linkSplit[0]}//{linkSplit[2]}/{id}/output/{id}.zip?download=zip"
            };
        }

        public static string GetAssetsDownloadUrl(string url)
        {
            var linkSplit = url.Split('/');
            string id = linkSplit[3];

            return $"{linkSplit[0]}//{linkSplit[2]}/{id}/source/{id}.zip?download=zip";

        }

        public static Cookie? GetSessionCookieFrom(string url)
        {
            string sessionStr = "?session=";
            string sessionCookie = String.Empty;

            if (url.Contains(sessionStr))
            {
                int sessionIndex = url.IndexOf(sessionStr) + sessionStr.Length;
                if (url.IndexOf('&') == -1)
                    sessionCookie = url.Substring(sessionIndex);
                else
                    sessionCookie = url.Substring(sessionIndex, url.IndexOf('&') - sessionIndex);
            }

            if (sessionCookie == null)
                return null;
            else
            {
                string domain = url.Split('/')[2];
                return new Cookie("BREEZESESSION", sessionCookie, "/", domain);
            }
        }

        public async Task<bool> DownloadFile(string url, string fileAddress, Cookie cookie)
        {
            WebClient client = new WebClient();
            string CookiesStr = $"{cookie.Name}={cookie.Value}";

            client.Headers.Add(HttpRequestHeader.Cookie, CookiesStr);

            client.DownloadProgressChanged += Client_DownloadProgressChanged;
            client.DownloadFileCompleted += Client_DownloadFileCompleted;
            webClient = client;

            try
            {
                await client.DownloadFileTaskAsync(new Uri(url), fileAddress);
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public void CancelDownload()
        {
            if (webClient != null)
                webClient.CancelAsync();
        }

        private void Client_DownloadFileCompleted(object? sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            DownloadFileComplited?.Invoke(sender, e);
        }

        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            double bytesIn = double.Parse(e.BytesReceived.ToString()) / (1024 * 1024);
            double totalBytes = double.Parse(e.TotalBytesToReceive.ToString()) / (1024 * 1024);
            int percentage = (int)(bytesIn / totalBytes * 100);
            PercentageChange?.Invoke(percentage, bytesIn, totalBytes);
        }

        public static List<Cookie> GetCookieForm(string url, Cookie cookie)
        {
            HttpWebRequest webRequest = WebRequest.CreateHttp(url);
            webRequest.Method = "HEAD";
            webRequest.CookieContainer = new CookieContainer();
            webRequest.CookieContainer.Add(cookie);

            List<Cookie> cookies = new List<Cookie>();

            var respose = (HttpWebResponse)webRequest.GetResponse();

            foreach (var x in respose.Cookies)
            {
                cookies.Add((Cookie)x);
            }

            return cookies;
        }

        public static string GetRequestUrl(string url, string session)
        {
            string originalURL = url.Split('?')[0];
            return $"{originalURL}?session={session}&proto=true";
        }

        public static bool IsUrlWrong(string url, List<Cookie> cookies)
        {
            HttpWebRequest webRequest = WebRequest.CreateHttp(url);
            webRequest.Method = "HEAD";
            webRequest.CookieContainer = new CookieContainer();
            foreach (var cookie in cookies)
            {
                webRequest.CookieContainer.Add(cookie);
            }
            var respose = (HttpWebResponse)webRequest.GetResponse();

            if (respose.ContentType != "application/zip")
                return true;
            else
                return false;
        }

        public static Stream GetStreamData(string url, List<Cookie> cookies, HttpContentType httpContentType)
        {
            HttpWebRequest httpWebRequest = WebRequest.CreateHttp(url);
            httpWebRequest.CookieContainer = new CookieContainer();
            foreach (var cookie in cookies)
            {
                httpWebRequest.CookieContainer.Add(cookie);
            }

            httpWebRequest.Accept = "*/*";
            httpWebRequest.Method = "GET";

            try
            {
                var response = (HttpWebResponse)httpWebRequest.GetResponse();
                if (httpContentType == HttpContentType.All)
                {
                    return response.GetResponseStream();
                }
                else if (response.ContentType == ConvertContentToString(httpContentType))
                    return response.GetResponseStream();
                else
                    return null;
            }
            catch
            {
                return null;
            }

        }

        public static bool GetStreamData(string url, List<Cookie> cookies, HttpContentType httpContentType, string fileAddress, bool overwrite)
        {
            Stream result = GetStreamData(url, cookies, httpContentType);

            if (result == null)
                return false;

            bool checkFileExist = File.Exists(fileAddress);

            if (checkFileExist == true && overwrite == false)
                return false;

            else if (checkFileExist == true)
                File.Delete(fileAddress);


            using (var file = File.Create(fileAddress))
            {
                result.CopyTo(file);
                result.Close();
                result.Dispose();
                file.Close();
            }
            return true;
        }

        public static void GetFiles(string baseUrl, string xmlFileData, List<Cookie> cookies, string workFolderPath)
        {
            var urls = XmlReader.GetFilesDownloadLink(xmlFileData, baseUrl);
            string filesFolder = Path.Combine(workFolderPath, "Files");
            if (Directory.Exists(filesFolder) == false)
                Directory.CreateDirectory(filesFolder);

            if (urls.Count != 0)
            {
                for (int i = 0; i < urls.Count; i++)
                {
                    string[] filename = urls[i].Split('/');
                    string fileNameDecoded = HttpUtility.UrlDecode(filename[filename.Length - 1]);
                    string fileAddres = Path.Combine(filesFolder, i + fileNameDecoded);
                    WebManager.GetStreamData(urls[i], cookies, WebManager.HttpContentType.All, fileAddres, true);
                }
            }
        }

        public static bool DownloadAssetsMethod1(string url, List<Cookie> cookies, string workFolderPath)
        {
            string assetUrl = WebManager.GetAssetsDownloadUrl(url);
            string fileAddress = Path.Combine(workFolderPath, "Assets.zip");

            var downloadResult = WebManager.GetStreamData(assetUrl, cookies, WebManager.HttpContentType.Zip, fileAddress, true);

            return downloadResult;
        }

        public static string GetDataForPdf(string defaultAddress, List<Cookie> cookies)
        {
            try
            {
                string xmlPdfFilesname = defaultAddress + "layout.xml";
                Stream layoutStreamData = WebManager.GetStreamData(xmlPdfFilesname, cookies, WebManager.HttpContentType.Xml);
                string response = string.Empty;

                using (var reader = new StreamReader(layoutStreamData))
                {
                    response = reader.ReadToEnd();
                }

                layoutStreamData.Dispose();
                return response;

            }
            catch
            {
                return null;
            }
        }

        private static string ConvertContentToString(HttpContentType httpContentType) => httpContentType switch
        {
            HttpContentType.Flash => "application/x-shockwave-flash",
            HttpContentType.Text => "text/plain",
            HttpContentType.Xml => "application/xml",
            HttpContentType.Zip => "application/zip",
            HttpContentType.All => "*/*"
        };

        public enum HttpContentType
        {
            Xml,
            Text,
            Zip,
            Flash,
            All
        }

    }
}
