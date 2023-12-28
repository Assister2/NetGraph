using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using CyConex.Helpers;

namespace CyConex.Chromium
{
    public static class CEFHelper
	{

		/// <summary>
		/// Initialize CEF
		/// </summary>
		/// <param name="dummy"></param>
		public async static void InitCEF(ApplicationSettings appSettings)
		{
			Directory.SetCurrentDirectory(Application.StartupPath);
			//Cef.EnableHighDPISupport();
			CefSettings settings = new CefSettings()
			{
				CachePath = Path.Combine(Application.StartupPath, "cache"),
				//CachePath = null,
				LogSeverity = LogSeverity.Disable,
				Locale = "en-US",
				AcceptLanguageList = "en-US",
				LocalesDirPath = Path.Combine(Application.StartupPath, "locales"),
				BrowserSubprocessPath = Path.Combine(Application.StartupPath, "CefSharp.BrowserSubprocess.exe")
			};
			CefSharpSettings.ShutdownOnExit = true;
			//CefSharpSettings.WcfTimeout = TimeSpan.FromSeconds(2);
			//CefSharpSettings.LegacyJavascriptBindingEnabled = true;
			string selectedUserAgent = Constants.UserAgent;
			settings.UserAgent = selectedUserAgent;
			//Turn off flash
			settings.CefCommandLineArgs.Add(@"ppapi-flash-path", Path.Combine(Application.StartupPath, String.Empty)); //Set empty flash folder
			settings.MultiThreadedMessageLoop = true;
			settings.PersistSessionCookies = false;
			settings.PersistUserPreferences = false;
			settings.Locale = "en-US";
			settings.CefCommandLineArgs.Add("disable-extensions", "1");
			settings.CefCommandLineArgs.Add("disable-local-storage", "1");
			settings.CefCommandLineArgs.Add("disable-pdf-extension", "1");
			settings.CefCommandLineArgs.Add("allow-universal-access-from-files");
			settings.CefCommandLineArgs.Add("allow-file-access-from-files");
			settings.CefCommandLineArgs.Add("no_sandbox", "1");
			settings.CefCommandLineArgs.Add("uncaught-exception-stack-size", "5");
			settings.PersistSessionCookies = true;
			settings.CefCommandLineArgs.Add(@"enable-npapi", "0");
			if (settings.CefCommandLineArgs.Any(item => item.Key == "enable-system-flash"))
			{
				settings.CefCommandLineArgs["enable-system-flash"] = "0";
			}
			else
			{
				settings.CefCommandLineArgs.Add("enable-system-flash", "0");
			}
			settings.CefCommandLineArgs.Add("allow-running-insecure-content", "allow-running-insecure-content");
			settings.CefCommandLineArgs.Add("access-control-allow-origin", "*");
			settings.CefCommandLineArgs.Add("disable-web-security", "disable-web-security");
			//
			settings.DisableGpuAcceleration();
			//Register own theme for maps
			settings.RegisterScheme(new CefCustomScheme
			{
				SchemeName = SchemeHandlerFactory.SchemeName,
				SchemeHandlerFactory = new SchemeHandlerFactory(appSettings),
				IsDisplayIsolated = false,
				IsLocal = false,
			});
			List<string> dependencies = DependencyChecker.CheckDependencies(true, false, Application.StartupPath, Application.StartupPath, Path.Combine(Application.StartupPath, "CefSharp.BrowserSubprocess.exe")); 
			if (dependencies.Count != 0)
			{
				throw new Exception($"Missing CEF dependency: \"{dependencies[0]}\"");
			}
			BrowserProcessHandler browserProcessHandler = new BrowserProcessHandler();
			if (!Cef.Initialize(settings, true, browserProcessHandler))
			{
				throw new Exception("Unable to initialize CefBrowser");
			}
			//Small delay after CEF initialize
			await GeneralHelpers.TaskDelay(500);
		}

		internal static void ExecScriptAsync(this ChromiumWebBrowser browser, string script)
		{
			if (browser != null && !browser.IsDisposed && browser.IsBrowserInitialized)
			{
				browser.ExecuteScriptAsync(script);
			}
		}


		internal static void EvalScriptAsync(this ChromiumWebBrowser browser, string script)
		{
			if (browser != null && !browser.IsDisposed)
			{
				browser.EvaluateScriptAsync(script);
			}
		}

	}
}
