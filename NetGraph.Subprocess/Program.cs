using CefSharp.BrowserSubprocess;
using CefSharp.RenderProcess;

namespace NetGraph.Subprocess
{
    internal class Program
	{
		static int Main(string[] args)
		{
			SubProcess.EnableHighDPISupport();
			//Add your own custom implementation of IRenderProcessHandler here
			IRenderProcessHandler handler = null;
			//The WcfBrowserSubprocessExecutable provides BrowserSubProcess functionality
			//specific to CefSharp, WCF support (required for Sync JSB) will optionally be
			//enabled if the CefSharpArguments.WcfEnabledArgument command line arg is present
			//For .Net Core use BrowserSubprocessExecutable as there is no WCF support
			var browserProcessExe = new WcfBrowserSubprocessExecutable();
			var result = browserProcessExe.Main(args, handler);
			return result;
		}
	}
}
