using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Web;
using Glimpse.Core.Extensibility;
using Naak.HtmlRules.Impl;

namespace Naak.HtmlRules
{
    [GlimpsePlugin(ShouldSetupInInit = true)]
    public class Plugin : IGlimpsePlugin
    {
        public object GetData(HttpContextBase context)
        {
            var html = CurrentContentStream.ReadToEnd();
            var renderer = new AccessibilityRenderer();
            return renderer.Render(html, Rules);
        }

        [Import]
        private IEnumerable<IHtmlRule> Rules { get; set; }

        public void SetupInit()
        {
            HttpContext.Current.ApplicationInstance.BeginRequest += AddContentStream;
        }

        private void AddContentStream(object sender, EventArgs e)
        {
            var contentStream = new CaptureStream(HttpContext.Current.Request.Filter);
            HttpContext.Current.Request.Filter = contentStream;
            CurrentContentStream = contentStream;
        }

        private static CaptureStream CurrentContentStream
        {
            get
            {
                return (CaptureStream) HttpContext.Current.Items["naak_response"];
            }
            
            set
            {
                HttpContext.Current.Items["naak_response"] = value;
            }
        }

        public string Name
        {
            get { return "Naak"; }
        }
    }
}