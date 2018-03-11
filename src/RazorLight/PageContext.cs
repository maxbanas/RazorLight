﻿using System.Dynamic;
using System.IO;
using RazorLight.ViewFeatures;

namespace RazorLight
{
    public class PageContext : IPageContext
    {
        private dynamic _viewBag;

        public PageContext()
        {
            _viewBag = new ExpandoObject();
            Writer = new StringWriter();
        }

        public PageContext(ExpandoObject viewBag)
        {
            _viewBag = viewBag ?? new ExpandoObject();
        }

        public TextWriter Writer { get; set; }

        public dynamic ViewBag => _viewBag;

        public string ExecutingPageKey { get; set; }

        public ModelTypeInfo ModelTypeInfo { get; set; }

		public FormContext FormContext { get; set; }

		public ViewFeatures.Rendering.Html5DateRenderingMode Html5DateRenderingMode { get; set; }
	}
}
