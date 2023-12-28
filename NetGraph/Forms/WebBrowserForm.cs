using CefSharp.WinForms;
using Syncfusion.WinForms.Controls;
using System;
using System.Windows.Forms;

namespace CyConex
{
    public partial class WebBrowserForm : SfForm
    {
        public ChromiumWebBrowser _browser;
        private IWin32Window parent;
        public WebBrowserForm()
        {
            InitializeComponent();
        }

        public void ShowBrowserForm(IWin32Window owner, string url, int width, int height)
        {
            parent = owner;
            this.Width = width;
            this.Height = height;
            Uri uri = new Uri(url );
            try
            {
                webView.Source = new Uri(url);
            }
            catch (System.UriFormatException)
            {
                return;
            }
            
            ShowDialog(owner);
            /*var request = (HttpWebRequest)WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();

            string str = response.CharacterSet;

            Encoding enc = Encoding.GetEncoding("UTF-8");


            Stream stream = response.GetResponseStream();
            StreamReader sr = new StreamReader(stream, enc);
            string content = sr.ReadToEnd();
            webBrowser.DocumentText = content;*/
            
        }

        public void VisibleBrowserForm(string url, int width, int height)
        {
            this.Width = width;
            this.Height = height;
            //Uri uri = new Uri(url );
            try
            {
               // webBrowser.Navigate(url);
            }
            catch (System.UriFormatException)
            {
                return;
            }
            //this.Visible = true;
        }

        private void webBrowser_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
           // ShowDialog(parent );
        }
    }
}
