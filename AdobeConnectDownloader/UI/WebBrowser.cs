using System;
using System.Collections.Generic;
using System.Net;
using System.Windows.Forms;
using iTextSharp.text;
using Microsoft.Web.WebView2.WinForms;

namespace AdobeConnectDownloader.UI;

public partial class WebBrowser : Form
{

    public WebView2? WebView { get; set; } = null;
    public List<Cookie> ExtractedCookies { get; set; } = new List<Cookie>();

    public WebBrowser()
    {
        InitializeComponent();
    }

    private async void BrowsUrlButton_Click(object sender, EventArgs e)
    {
        if (WebView is null)
        {
            WebView = new WebView2();
            WebView.Dock = DockStyle.Fill;
            BrowserGroupBox.Controls.Add(WebView);
            await WebView.EnsureCoreWebView2Async();
        }

        WebView.CoreWebView2.Navigate(UrlTextBox.Text.Trim());
    }

    private async void CollectDataButton_Click(object sender, EventArgs e)
    {
        if (WebView is null)
        {
            MessageBox.Show("you must in first search a url then collect data");
            return;
        }

        if (MessageBox.Show("are you sure ?", "confirm box", MessageBoxButtons.YesNo) != DialogResult.Yes)
            return;

        var cookieList = await WebView.CoreWebView2.CookieManager.GetCookiesAsync(UrlTextBox.Text.Trim());
        foreach (var c in cookieList)
        {
            var sysCookie = c.ToSystemNetCookie();
            if (sysCookie is not null)
            {
                ExtractedCookies.Add(sysCookie);
            }
        }

        this.Close();
    }
}