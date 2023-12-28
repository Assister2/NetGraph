using System;

namespace CyConex.Chromium
{

    public class EventHandlerEventArgs : EventArgs
	{
		public string Source;
		public string Event;
		public string Data;

		public EventHandlerEventArgs(string source, string eventName, string data)
		{
			Source = source;
			Event = eventName;
			Data = data;
		}
	}

	public class BoundObjectV2
	{
		public event Action<string, string, object> OnEventFired;

		public void EventHandler(string eventSource, string eventName = "", object eventData = null)
		{
			OnEventFired?.Invoke(eventSource, eventName, eventData);
		}
	}

}
