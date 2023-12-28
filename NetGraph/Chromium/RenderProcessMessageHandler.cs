using CefSharp;

namespace CyConex.Chromium
{
    public class RenderProcessMessageHandler : IRenderProcessMessageHandler
	{
		public void OnContextCreated(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame)
		{
			frame.EvaluateScriptAsync("(async function() {await CefSharp.BindObjectAsync('bound');})();");
		}

		public void OnContextReleased(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame)
		{
			
		}

		public void OnFocusedNodeChanged(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IDomNode node)
		{
			
		}

		public void OnUncaughtException(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, JavascriptException exception)
		{
			
		}
	}
}
