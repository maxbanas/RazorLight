﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Html;

namespace RazorLight.ViewFeatures
{
	/// <summary>
	/// Information about the current &lt;form&gt;.
	/// </summary>
	/// <remarks>
	/// Literal &lt;form&gt; elements in a view will share the default <see cref="FormContext"/> instance unless tag
	/// helpers are enabled.
	/// </remarks>
	public class FormContext
	{
		private Dictionary<string, bool> _renderedFields;
		private Dictionary<string, object> _formData;
		private IList<IHtmlContent> _endOfFormContent;

		/// <summary>
		/// Gets a property bag for any information you wish to associate with a &lt;form/&gt; in an
		/// <see cref="Rendering.IHtmlHelper"/> implementation or extension method.
		/// </summary>
		public IDictionary<string, object> FormData
		{
			get
			{
				if (_formData == null)
				{
					_formData = new Dictionary<string, object>(StringComparer.Ordinal);
				}

				return _formData;
			}
		}

		/// <summary>
		/// Gets or sets an indication the current &lt;form&gt; element contains an antiforgery token. Do not use
		/// unless <see cref="CanRenderAtEndOfForm"/> is <c>true</c>.
		/// </summary>
		/// <value>
		/// <c>true</c> if the current &lt;form&gt; element contains an antiforgery token; <c>false</c> otherwise.
		/// </value>
		public bool HasAntiforgeryToken { get; set; }

		/// <summary>
		/// Gets an indication the <see cref="FormData"/> property bag has been used and likely contains entries.
		/// </summary>
		/// <value>
		/// <c>true</c> if the backing field for <see cref="FormData"/> is non-<c>null</c>; <c>false</c> otherwise.
		/// </value>
		public bool HasFormData => _formData != null;

		/// <summary>
		/// Gets an indication the <see cref="EndOfFormContent"/> collection has been used and likely contains entries.
		/// </summary>
		/// <value>
		/// <c>true</c> if the backing field for <see cref="EndOfFormContent"/> is non-<c>null</c>; <c>false</c>
		/// otherwise.
		/// </value>
		public bool HasEndOfFormContent => _endOfFormContent != null;

		/// <summary>
		/// Gets an <see cref="IHtmlContent"/> collection that should be rendered just prior to the next &lt;/form&gt;
		/// end tag. Do not use unless <see cref="CanRenderAtEndOfForm"/> is <c>true</c>.
		/// </summary>
		public IList<IHtmlContent> EndOfFormContent
		{
			get
			{
				if (_endOfFormContent == null)
				{
					_endOfFormContent = new List<IHtmlContent>();
				}

				return _endOfFormContent;
			}
		}

		/// <summary>
		/// Gets or sets an indication whether extra content can be rendered at the end of the content of this
		/// &lt;form&gt; element. That is, <see cref="EndOfFormContent"/> will be rendered just prior to the
		/// &lt;/form&gt; end tag.
		/// </summary>
		/// <value>
		/// <c>true</c> if the framework will render <see cref="EndOfFormContent"/>; <c>false</c> otherwise. In
		/// particular, <c>true</c> if the current &lt;form&gt; is associated with a tag helper or will be generated by
		/// an HTML helper; <c>false</c> when using the default <see cref="FormContext"/> instance.
		/// </value>
		public bool CanRenderAtEndOfForm { get; set; }

		/// <summary>
		/// Gets a dictionary mapping full HTML field names to indications that the named field has been rendered in
		/// this &lt;form&gt;.
		/// </summary>
		private Dictionary<string, bool> RenderedFields
		{
			get
			{
				if (_renderedFields == null)
				{
					_renderedFields = new Dictionary<string, bool>(StringComparer.Ordinal);
				}

				return _renderedFields;
			}
		}

		/// <summary>
		/// Returns an indication based on <see cref="RenderedFields"/> that the given <paramref name="fieldName"/> has
		/// been rendered in this &lt;form&gt;.
		/// </summary>
		/// <param name="fieldName">The full HTML name of a field that may have been rendered.</param>
		/// <returns>
		/// <c>true</c> if the given <paramref name="fieldName"/> has been rendered; <c>false</c> otherwise.
		/// </returns>
		public bool RenderedField(string fieldName)
		{
			if (fieldName == null)
			{
				throw new ArgumentNullException(nameof(fieldName));
			}

			bool result;
			RenderedFields.TryGetValue(fieldName, out result);

			return result;
		}

		/// <summary>
		/// Updates <see cref="RenderedFields"/> to indicate <paramref name="fieldName"/> has been rendered in this
		/// &lt;form&gt;.
		/// </summary>
		/// <param name="fieldName">The full HTML name of a field that may have been rendered.</param>
		/// <param name="value">If <c>true</c>, the given <paramref name="fieldName"/> has been rendered.</param>
		public void RenderedField(string fieldName, bool value)
		{
			if (fieldName == null)
			{
				throw new ArgumentNullException(nameof(fieldName));
			}

			RenderedFields[fieldName] = value;
		}
	}
}
