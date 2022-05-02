namespace AdobeConnectDownloader.Model
{
    public class PdfDetail
    {
        public string FileName { get; set; }
        public int PageNumber { get; set; }
        public int XSize { get; set; }
        public int YSize { get; set; }

        public bool NeedToRotate
        {
            get
            {
                return (XSize > YSize) ? true : false;
            }

        }
    }
}
