using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using CefSharp;
using CefSharp.Callback;

namespace CyConex.Chromium
{
    internal class NetGraphSchemeHandler : IResourceHandler
	{
		private string _mimeType;
		private MemoryStream _stream;
		private ApplicationSettings _settings;

		public NetGraphSchemeHandler(ApplicationSettings settings)
		{
			_settings = settings;
		}

		public void Cancel()
		{

		}

		public bool CanGetCookie(CefSharp.Cookie cookie)
		{
			return true;
		}

		public bool CanSetCookie(CefSharp.Cookie cookie)
		{
			return true;
		}

		public void Dispose()
		{
			_stream?.Dispose();
		}

		public void GetResponseHeaders(IResponse response, out long responseLength, out string redirectUrl)
		{
			responseLength = _stream == null ? 0 : _stream.Length;
			redirectUrl = null;
			response.StatusCode = (int)HttpStatusCode.OK;
			response.StatusText = "OK";
			response.MimeType = _mimeType;
			NameValueCollection nameval = new NameValueCollection
			{
				{ "Access-Control-Allow-Origin", "*" }
			};
			response.Headers = nameval;
		}

		public bool Open(IRequest request, out bool handleRequest, ICallback callback)
		{
			//TODO: Made new flow for processing request
			//For backward compatibility
			handleRequest = false;
			return false;
		}

		public bool ProcessRequest(IRequest request, ICallback callback)
		{
			string content = String.Empty;
			Uri requestUri = new Uri(request.Url);
			switch (requestUri.Host)
			{
				case "main":
					_mimeType = @"text/html";
					content = LocalHelpers.ReadGraphPage();
					break;
				case @"js":
					_mimeType = @"text/javascript";
					switch (requestUri.LocalPath)
					{
						case "/cytoscape.min.js":
							content = LocalHelpers.ReadResource("CyConex.HTML.cytoscape.min.js");
							break;
						case "/jquery.js":
							content = LocalHelpers.ReadResource("CyConex.HTML.jquery.js");
							break;
						case "/lodash.js":
							content = LocalHelpers.ReadResource("CyConex.HTML.lodash.js");
							break;
						case "/cytoscape.euler.js":
							content = LocalHelpers.ReadResource("CyConex.HTML.cytoscape.euler.js");
							break;
						case "/cytoscape.layers.js":
							content = LocalHelpers.ReadResource("CyConex.HTML.cytoscape.layers.js");
							break;
						case "/cytoscape.bubbleset.js":
							content = LocalHelpers.ReadResource("CyConex.HTML.cytoscape.bubbleset.js");
							break;
						case "/cytoscape-node-html-label.js":
							content = LocalHelpers.ReadResource("CyConex.HTML.cytoscape-node-html-label.js");
							break;
						case "/cytoscape-undo-redo.js":
							content = LocalHelpers.ReadResource("CyConex.HTML.cytoscape-undo-redo.js");
							break;
						case "/copyImageClipboard.js":
							content = LocalHelpers.ReadResource("CyConex.HTML.copyImageClipboard.js");
							break;
						case "/cytoscape-grid-guide.js":
							content = LocalHelpers.ReadResource("CyConex.HTML.cytoscape-grid-guide.js");
							break;
						case "/cytoscape-popper.js":
							content = LocalHelpers.ReadResource("CyConex.HTML.cytoscape-popper.js");
							break;
						case "/popper.min.js":
							content = LocalHelpers.ReadResource("CyConex.HTML.popper.min.js");
							break;
						case "/tippy-bundle.umd.min.js":
							content = LocalHelpers.ReadResource("CyConex.HTML.tippy-bundle.umd.min.js");
							break;
						//Dagre
						case "/dagre.js":
							content = LocalHelpers.ReadResource("CyConex.HTML.dagre.js");
							break;
						case "/cytoscape-dagre.js":
							content = LocalHelpers.ReadResource("CyConex.HTML.cytoscape-dagre.js");
							break;
						//Cola
						case "/cola.min.js":
							content = LocalHelpers.ReadResource("CyConex.HTML.cola.min.js");
							break;
						case "/cytoscape-cola.js":
							content = LocalHelpers.ReadResource("CyConex.HTML.cytoscape-cola.js");
							break;
						//ELK
						case "/cytoscape-elk.js":
							content = LocalHelpers.ReadResource("CyConex.HTML.cytoscape-elk.js");
							break;
						case "/elk.bundled.js":
							content = LocalHelpers.ReadResource("CyConex.HTML.elk.bundled.js");
							break;
						//AVSDF
						case "/avsdf-base.js":
							content = LocalHelpers.ReadResource("CyConex.HTML.avsdf-base.js");
							break;
						case "/layout-base.js":
							content = LocalHelpers.ReadResource("CyConex.HTML.layout-base.js");
							break;
						case "/cytoscape-avsdf.js":
							content = LocalHelpers.ReadResource("CyConex.HTML.cytoscape-avsdf.js");
							break;
						case "/cytoscape-cise.js":
							content = LocalHelpers.ReadResource("CyConex.HTML.cytoscape-cise.js");
							break;
						case "/cytoscape-cose-bilkent.js":
							content = LocalHelpers.ReadResource("CyConex.HTML.cytoscape-cose-bilkent.js");
							break;
						case "/cose-base.js":
							content = LocalHelpers.ReadResource("CyConex.HTML.cose-base.js");
							break;
						case "/cytoscape-all-paths.js":
							content = LocalHelpers.ReadResource("CyConex.HTML.cytoscape-all-paths.js");
							break;
						case "/cytoscape-drag-and-drop.js":
							content = LocalHelpers.ReadResource("CyConex.HTML.cytoscape-drag-and-drop.js");
							break;
						case "/heatmap.js":
							content = LocalHelpers.ReadResource("CyConex.HTML.heatmap.js");
							break;
						case "/backgroundImage.js":
							content = LocalHelpers.ReadResource("CyConex.HTML.backgroundImage.js");
							break;
						case "/cytoscape-edgehandles.js":
							content = LocalHelpers.ReadResource("CyConex.HTML.cytoscape-edgehandles.js");
							break;
						case "/cytoscape-node-editing.js":
							content = LocalHelpers.ReadResource("CyConex.HTML.cytoscape-node-editing.js");
							break;
						case "/popper.min.js.map":
						case "/tippy-bundle.umd.min.js.map":
							break;
						default:
							_mimeType = @"text/html";
							content = $@"<html><body><h1>NetGraph scheme.</h1><p>Requested resource not found: {requestUri.LocalPath}</p></body></html>";
							_stream = new MemoryStream(Encoding.UTF8.GetBytes(content));
							break;

					}
					break;
				case @"css":
					_mimeType = @"text/css";
					switch (requestUri.LocalPath)
					{
						case "/backdrop.css":
							content = LocalHelpers.ReadResource("CyConex.HTML.backdrop.css");
							break;
						case "/light.css":
							content = LocalHelpers.ReadResource("CyConex.HTML.light.css");
							break;
					}
					break;
				case "images":
					_mimeType = @"image/png";
					break;
				case "cur":
					//It this case we need return cursors as binary stream and process it separately
					_mimeType = "image/vnd.microsoft.icon";
					switch (requestUri.LocalPath)
					{
						case "/dropper.cur":
							_stream = new MemoryStream(LocalHelpers.ReadResourceBytes("CyConex.HTML.dropper.cur"));
							callback.Continue();
							return true;
					}
					break;

			}
			using (callback)
			{
				_stream = new MemoryStream(Encoding.UTF8.GetBytes(content));
				callback.Continue();
				return true;
			}
		}

		public bool Read(Stream dataOut, out int bytesRead, IResourceReadCallback callback)
		{
			//For backward compatibility
			bytesRead = -1;
			return false;
		}

		public bool ReadResponse(Stream dataOut, out int bytesRead, ICallback callback)
		{
			callback.Dispose();
			if (_stream == null)
			{
				bytesRead = 0;
				return false;
			}
			//Data out represents an underlying buffer (typically 32kb in size).
			var buffer = new byte[dataOut.Length];
			bytesRead = _stream.Read(buffer, 0, buffer.Length);
			dataOut.Write(buffer, 0, buffer.Length);
			return bytesRead > 0;
		}

		public bool Skip(long bytesToSkip, out long bytesSkipped, IResourceSkipCallback callback)
		{
			throw new NotImplementedException();
		}
	}
}
