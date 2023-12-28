using System;
using System.Security.Cryptography.X509Certificates;
using CefSharp;

namespace CyConex.Chromium
{
    public class ResourceDownloadedEventArgs : EventArgs
	{
		public string Resource;
		public int StatusCode;
		public CefErrorCode ErrorCode;
		public string StatusText;

		public ResourceDownloadedEventArgs()
		{
			Resource = String.Empty;
			StatusCode = 0;
			ErrorCode = CefErrorCode.None;
			StatusText = String.Empty;
		}
	}

	public class CustomRequestHandler : IRequestHandler
	{

		private ApplicationSettings _settings;
		public event EventHandler<ResourceDownloadedEventArgs> OnResourceDownloadedEvent;

		public CustomRequestHandler(ApplicationSettings settings) : base()
		{
			_settings = settings;
		}

		public bool GetAuthCredentials(IWebBrowser browserControl, IBrowser browser, IFrame frame, bool isProxy, string host, int port, string realm, string scheme, IAuthCallback callback)
		{
			return true;
		}

		public IResponseFilter GetResourceResponseFilter(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IResponse response)
		{
			return null;
		}

		public CefReturnValue OnBeforeResourceLoad(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IRequestCallback callback)
		{
			var headers = request.Headers;
			headers.Add("Accept-Language", "fr-CA,en,en-US");
			return CefReturnValue.Continue;
		}

		public bool OnCertificateError(IWebBrowser browserControl, IBrowser browser, CefErrorCode errorCode, string requestUrl, ISslInfo sslInfo, IRequestCallback callback)
		{
			return true;
		}

		public bool OnOpenUrlFromTab(IWebBrowser browserControl, IBrowser browser, IFrame frame, string targetUrl, WindowOpenDisposition targetDisposition, bool userGesture)
		{
			return false;
		}

		public void OnPluginCrashed(IWebBrowser browserControl, IBrowser browser, string pluginPath)
		{

		}

		public bool OnProtocolExecution(IWebBrowser browserControl, IBrowser browser, string url)
		{
			return true;
		}

		public bool OnQuotaRequest(IWebBrowser browserControl, IBrowser browser, string originUrl, long newSize, IRequestCallback callback)
		{
			return true;
		}

		public void OnRenderProcessTerminated(IWebBrowser browserControl, IBrowser browser, CefTerminationStatus status)
		{
		}

		public void OnRenderViewReady(IWebBrowser browserControl, IBrowser browser)
		{
		}

		public void OnResourceLoadComplete(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IResponse response, UrlRequestStatus status, long receivedContentLength)
		{
			
			OnResourceDownloadedEvent?.Invoke(this, new ResourceDownloadedEventArgs()
			{
				Resource = request.Url,
				StatusCode = response.StatusCode,
				ErrorCode = response.ErrorCode,
				StatusText = response.StatusText,
			});
		}

		public bool OnResourceResponse(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IResponse response)
		{
			return false;
		}

		public bool OnSelectClientCertificate(IWebBrowser browserControl, IBrowser browser, bool isProxy, string host, int port, X509Certificate2Collection certificates, ISelectClientCertificateCallback callback)
		{
			callback.Dispose();
			return false;
		}

		public void OnResourceRedirect(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IResponse response, ref string newUrl)
		{
		}

		public bool CanGetCookies(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request)
		{
			return true;
		}

		public bool CanSetCookie(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, Cookie cookie)
		{
			return true;
		}

		public bool OnBeforeBrowse(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, bool userGesture, bool isRedirect)
		{
			return false;
		}

		public void OnDocumentAvailableInMainFrame(IWebBrowser chromiumWebBrowser, IBrowser browser)
		{
			
		}

		public IResourceRequestHandler GetResourceRequestHandler(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, bool isNavigation, bool isDownload, string requestInitiator, ref bool disableDefaultHandling)
		{
			return null;
		}

		public bool GetAuthCredentials(IWebBrowser chromiumWebBrowser, IBrowser browser, string originUrl, bool isProxy, string host, int port, string realm, string scheme, IAuthCallback callback)
		{
			return false;
		}
	}
}
