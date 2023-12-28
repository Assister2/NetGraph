using CefSharp;

namespace CyConex.Chromium
{
    public class SchemeHandlerFactory : ISchemeHandlerFactory
	{
		private ApplicationSettings _settings;
		public static string SchemeName = "netg";

		public SchemeHandlerFactory(ApplicationSettings settings) : base()
		{
			_settings = settings;
		}

		public IResourceHandler Create(IBrowser browser, IFrame frame, string schemeName, IRequest request)
		{
			return new NetGraphSchemeHandler(_settings);
		}
	}
}
