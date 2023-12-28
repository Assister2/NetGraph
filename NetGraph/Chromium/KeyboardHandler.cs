using CefSharp;

namespace CyConex.Chromium
{
    public class KeyboardHandler : IKeyboardHandler
	{
			
		public bool OnKeyEvent(IWebBrowser chromiumWebBrowser, IBrowser browser, KeyType type, int windowsKeyCode, int nativeKeyCode, CefEventFlags modifiers, bool isSystemKey)
		{
			CefKeyboardEvent cefKeyboardEvent = new CefKeyboardEvent()
			{
				KeyType = type.ToString(),
				WindowsKeyCode = windowsKeyCode,
				NativeKeyCode = nativeKeyCode,
				IsSystemKey = isSystemKey,
				Modifiers = modifiers
			};
			browser.ExecuteScriptAsync($"bound.eventHandler('main', 'onkey', '{cefKeyboardEvent.ToJSON()}');");
			return false;
			//throw new NotImplementedException();
		}

		public bool OnPreKeyEvent(IWebBrowser chromiumWebBrowser, IBrowser browser, KeyType type, int windowsKeyCode, int nativeKeyCode, CefEventFlags modifiers, bool isSystemKey, ref bool isKeyboardShortcut)
		{
			CefKeyboardEvent cefKeyboardEvent = new CefKeyboardEvent()
			{
				KeyType = type.ToString(),
				WindowsKeyCode = windowsKeyCode,
				NativeKeyCode = nativeKeyCode,
				IsSystemKey = isSystemKey,
				Modifiers = modifiers
			};
			browser.ExecuteScriptAsync($"bound.eventHandler('main', 'prekey', '{cefKeyboardEvent.ToJSON()}');");
			return false;
			//throw new NotImplementedException();
		}
	}
}
