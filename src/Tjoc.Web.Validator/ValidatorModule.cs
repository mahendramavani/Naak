using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Web;

namespace Tjoc.Web.Validator
{
	public class ValidatorModule : IHttpModule
	{
		// TODO - lots!
		// 1. Have a querystring marker that turns it on/off for a page.
		// 2. Move the output so as not to invalidate the source.
		// 3. Consider adding an Email mode.

		private HttpApplication _application;
		private CaptureStream _decoratorStream;
		private bool _validExtension;
		// setup the appropriate implementation of IValidationRenderer
		private IValidationRenderer _renderer;

		internal static ValidatorConfigHandler Config
		{
			get { return (ValidatorConfigHandler) ConfigurationManager.GetSection("validatorModule"); }
		}

		public void Init(HttpApplication context)
		{
			_application = context;
			// wire up the endrequest method
			context.EndRequest += context_EndRequest;
			// add the stream decorator
			context.BeginRequest += context_BeginRequest;
		}

		private void context_BeginRequest(object sender, EventArgs e)
		{
			if (Config.Enabled)
			{
				_validExtension = IsValidExtenstion();

				if (_validExtension)
				{
					_decoratorStream = new CaptureStream(_application.Response.Filter);
					_application.Response.Filter = _decoratorStream;

					// setup the specified implementation of renderer, default to 
					// HTML so it is clearly seen and not left on by accident.
					switch (Config.Mode)
					{
						case Mode.Custom:
							SetupCustomRenderer();
							break;

						case Mode.Comments:
							_renderer = new InlineCommentRenderer();
							break;

						case Mode.HtmlFloat:
							_renderer = new FloatingHtmlRenderer();
							break;

						default: // default to HTML
							_renderer = new InlineHtmlRenderer();
							break;
					}
				}
			}
		}

		private void SetupCustomRenderer()
		{
			if (String.IsNullOrEmpty(Config.CustomRenderer))
			{
				throw new ArgumentException("In Custom mode a customRenderer type must be specified");
			}

			Type rendererType = Type.GetType(Config.CustomRenderer, true);
			_renderer = Activator.CreateInstance(rendererType) as IValidationRenderer;
			// If the renderer is null then it's not of the correct type
			if (_renderer == null)
			{
				throw new ArgumentException(string.Format(
				                            	"The specified custom renderer type '{0}' must implement the '{1}' interface",
				                            	Config.CustomRenderer,
				                            	typeof (IValidationRenderer).FullName));
			}
		}

		private void context_EndRequest(object sender, EventArgs e)
		{
			if (Config.Enabled && _validExtension && IsValidContentType())
			{
				DateTime start = DateTime.Now;

				_decoratorStream.CopyStream.Position = 0;

				string html;

				using (var reader = new StreamReader(_decoratorStream.CopyStream))
				{
					html = reader.ReadToEnd();
				}

				var validator = new XhtmlValidator(html);
				Collection<ValidationRecord> _errors = validator.Validate();

				// calculate the duration.
				TimeSpan duration = DateTime.Now - start;

				// hand the rendering over to the renderer...
				_renderer.Render(_application.Response, _errors, duration, html);
			}
		}

		public void Dispose()
		{
			// do nothing...
		}

		private bool IsValidExtenstion()
		{
			bool validExtension = false;

			foreach (StringElement element in Config.PageExtensions)
			{
				if (element.Key != null &&
				    _application.Request.Path.EndsWith(element.Key, true, CultureInfo.InvariantCulture))
				{
					validExtension = true;
					break;
				}
			}
 
			return validExtension;
		}

		private bool IsValidContentType()
		{
			bool validContentType = false;

			foreach (StringElement element in Config.ContentTypes)
			{
				if (!string.IsNullOrEmpty(element.Key) &&
				    string.Compare(_application.Response.ContentType, element.Key, true, CultureInfo.InvariantCulture) == 0)
				{
					validContentType = true;
					break;
				}
			}

			return validContentType;
		}
	}
}