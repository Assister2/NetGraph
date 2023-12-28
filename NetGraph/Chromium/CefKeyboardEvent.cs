using CefSharp;

namespace CyConex.Chromium
{
    public class CefKeyboardEvent
	{
		public string KeyType { get; set; }
		public int WindowsKeyCode { get; set; }
		public int NativeKeyCode { get; set; }
		public CefEventFlags Modifiers { get; set; }
		public bool IsSystemKey { get; set; }

		public CefKeyboardEvent()
		{

		}

		public string ToJSON()
		{
			return Newtonsoft.Json.JsonConvert.SerializeObject(this);
		}
	}
}
