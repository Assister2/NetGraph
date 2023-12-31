﻿using CefSharp;

namespace CyConex.Chromium
{
    internal class BrowserProcessHandler : IBrowserProcessHandler
	{
		/// <summary>
		/// The maximum number of milliseconds we're willing to wait between calls to OnScheduleMessagePumpWork().
		/// </summary>
		protected const int MaxTimerDelay = 1000 / 30;  // 30fps

		void IBrowserProcessHandler.OnContextInitialized()
		{
			//The Request Context has been initialized, you can now set preferences, like proxy server settings
			var cookieManager = Cef.GetGlobalCookieManager();
			//cookieManager.SetStoragePath("cookies", true);

			//Dispose of context when finished - preferable not to keep a reference if possible.
			using (var context = Cef.GetGlobalRequestContext())
			{
				//You can set most preferences using a `.` notation rather than having to create a complex set of dictionaries.
				//The default is true, you can change to false to disable
				context.SetPreference("webkit.webprefs.plugins_enabled", true, out string errorMessage);
			}
		}

		void IBrowserProcessHandler.OnScheduleMessagePumpWork(long delay)
		{
			//If the delay is greater than the Maximum then use MaxTimerDelay
			//instead - we do this to achieve a minimum number of FPS
			if (delay > MaxTimerDelay)
			{
				delay = MaxTimerDelay;
			}
			OnScheduleMessagePumpWork((int)delay);
		}

		protected virtual void OnScheduleMessagePumpWork(int delay)
		{
			//TODO: Schedule work on the UI thread - call Cef.DoMessageLoopWork
		}

		public virtual void Dispose()
		{

		}
	}
}
