using CefSharp;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace CyConex
{
    internal static class Program
	{
		public static bool DebugMode;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{	
			DebugMode = Debugger.IsAttached || args.Contains("debug");
			LoadResolver();
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjA0Njc5QDMyMzAyZTMxMmUzMGx0b1R5TkdWQ1o0VlZRbVNFY3pGakkvV2dLR2MvY0xGdHhTTi9zcURTRFU9;NjA0NjgwQDMyMzAyZTMxMmUzMG9CSlk0M210Zk9qZVVhYzRrYjRwSjVkaVEvY2xBSVE4MHg3V1ZLSE04S009;NjA0NjgxQDMyMzAyZTMxMmUzMEUvYUdtWjFHWTBuS21KWVZRRk9EMEtQRjJKaGo5ZmtERGpYbEpwUExoVW89");
			Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjA0Njk1QDMxMzkyZTM0MmUzMEI5czRPaEpLWXBhWWlGdjZENlBqM0VZd3B6UERjYlJHMDZUQnNqTlcwMmM9;NjA0Njk2QDMxMzkyZTM0MmUzMEFBb1hoVVdIc09ZMXBMRVFuNnByLzVrd3lCU3NtbCtmMHM2Uy9uTlg3U1E9;NjA0Njk3QDMxMzkyZTM0MmUzMGZiSjk5MktWUVMvSmxTTTBpYWtkeTJmMklxS1hPV3pia1pudi9DRnRrdHc9");
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTAzNjk1NUAzMjMwMmUzNDJlMzBUYUtnTWxlWWVkVmoyaXJONWFFUURNQU5PZ2ptMkxZNlBTV0ljbWpIVk9VPQ ==");
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBaFt+QHJqUU1hXk5Hd0BLVGpAblJ3T2ZQdVt5ZDU7a15RRnVfR1xjSX1RdkZiX3dedA==;Mgo+DSMBPh8sVXJ1S0R+WFpFdEBBXHxAd1p/VWJYdVt5flBPcDwsT3RfQF5jTH9TdkZhWXxfdn1QQg==;ORg4AjUWIQA/Gnt2VFhiQlVPd11dXmJWd1p/THNYflR1fV9DaUwxOX1dQl9gSXtSdkVgW39dcXNUQGQ=;MTk5MTYxNkAzMjMxMmUzMjJlMzRpRnFpV2k0SFZGVFoyZjVGdTNMWmwrUUt1d0Y3dkRIM1FueXNXOE9saHMwPQ==;MTk5MTYxN0AzMjMxMmUzMjJlMzRoWjFPb01xaWtuWWZPczBWeldJb0ZadXFxOXV5anVuSStRdnNYUzAzWk44PQ==;NRAiBiAaIQQuGjN/V0d+Xk9AfV5AQmBIYVp/TGpJfl96cVxMZVVBJAtUQF1hSn5Wd0VjWn1ec3VSTmZY;MTk5MTYxOUAzMjMxMmUzMjJlMzRnNXA5OC90REpUREV0RUJ1bUxtM3BIUUhsRXhYU2lvWWJYN21lbkFsaVF3PQ==;MTk5MTYyMEAzMjMxMmUzMjJlMzRLc0xVbFY5VmVkRFlpM3F3Q0dSQWxLdjBXRFNBOEdhOXoyUm50SjZ4RHBvPQ==;Mgo+DSMBMAY9C3t2VFhiQlVPd11dXmJWd1p/THNYflR1fV9DaUwxOX1dQl9gSXtSdkVgW39dcXxSRmQ=;MTk5MTYyMkAzMjMxMmUzMjJlMzRnclZDWXBLcW9iWFo4cUYwQzhncGRHM3YxRTEzRU96NndDckcvdERxMzh3PQ==;MTk5MTYyM0AzMjMxMmUzMjJlMzRKTDUweDdPUU1IN2NWUWRISXBJQXFMMDZ1R0ZCck1KOXpRRml2WjdIc2lvPQ==;MTk5MTYyNEAzMjMxMmUzMjJlMzRnNXA5OC90REpUREV0RUJ1bUxtM3BIUUhsRXhYU2lvWWJYN21lbkFsaVF3PQ==");
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NGaF1cWmhIfEx1RHxQdld5ZFRHallYTnNWUj0eQnxTdEZjUX9ZcHdQTmNcU0x2Xw==");

            Application.Run(new MainForm());
			Cef.Shutdown();
		}

		/// <summary>
		/// Embed library resolves
		/// </summary>;
		private static void LoadResolver()
		{
			//AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolver);
		}

		/// <summary>
		/// Resolve embed DLL's
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		/// <returns></returns>
		/// 

		//	//String resourceName = Assembly.GetExecutingAssembly().FullName.Split(',').First() + "." + new AssemblyName(args.Name).Name + ".dll";
		//	//using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
		//	//{
		//	//	if (stream != null)
		//	//	{
		//	//		Byte[] assemblyData = new Byte[stream.Length];
		//	//		stream.Read(assemblyData, 0, assemblyData.Length);
		//	//		return Assembly.Load(assemblyData);
		//	//	}
		//	//}
		//	//return null;
		//}

	}
}
