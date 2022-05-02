using AdobeConnectDownloader.Model;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Diagnostics;
using System.IO;

namespace AdobeConnectDownloader.Application
{
    public class SwfManager
    {
        public Process CurrentProcess { get; set; }
        public string SwfToImageAddress { get; set; }
        public bool CancelProcess { get; set; } = false;

        public SwfManager(string swfToImageAddress)
        {
            SwfToImageAddress = swfToImageAddress;
        }

        public void ConvertSwfToPdf(PdfDetail pdfDetail, string swfFolder)
        {

            FileStream fileStream = new FileStream(pdfDetail.FileName, FileMode.Create);
            Document pdfDocument = new Document(new Rectangle(pdfDetail.XSize, pdfDetail.YSize), 0, 0, 0, 0);

            PdfWriter pdfWriter = PdfWriter.GetInstance(pdfDocument, fileStream);

            pdfDocument.Open();
            pdfDocument.AddAuthor("Adobe Connect Downloader");
            pdfDocument.AddCreationDate();

            string pngFolderAddress = Path.Combine(swfFolder, "pdf Files");

            if (Directory.Exists(pngFolderAddress) == false)
                Directory.CreateDirectory(pngFolderAddress);

            if (CancelProcess)
                return;

            // becuse we can not set persian address i must set address like this
            ConvertSwfToPng(swfFolder, pngFolderAddress);

            if (CancelProcess)
                return;

            var finalPngs = Directory.GetFiles(pngFolderAddress);


            foreach (var png in finalPngs)
            {
                var pngInstance = Image.GetInstance(png);
                pngInstance.SetAbsolutePosition(0, 0);
                pdfDocument.Add(pngInstance);
                pdfDocument.NewPage();
            }

            pdfDocument.Close();
            pdfWriter.Close();
            fileStream.Close();
            fileStream.Dispose();

        }

        private void ConvertSwfToPng(string swfFolder, string outputFolder)
        {
            var files = Directory.GetFiles(swfFolder);
            foreach (var file in files)
            {
                if (CancelProcess == false)
                {
                    string outputFileName = Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(file) + ".png");
                    string command = $"\"{file}\" -o \"{outputFileName}\"";
                    RunProcess(command);
                }
                else
                    break;
            }
        }

        private void RunProcess(string command)
        {
            ProcessStartInfo processStartInfo = new ProcessStartInfo();
            processStartInfo.FileName = SwfToImageAddress;
            processStartInfo.CreateNoWindow = true;
            processStartInfo.Arguments = command;
            var process = Process.Start(processStartInfo);
            CurrentProcess = process;
            process?.WaitForExit();
        }
    }
}
