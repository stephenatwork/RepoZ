﻿using RepoZ.Api.Common.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace RepoZ.App.Win.i18n
{
	public class ResourceDictionaryTranslationService : ITranslationService
	{
		private static ResourceDictionary _dictionary = null;

		public static ResourceDictionary ResourceDictionary
		{
			get
			{
				if (_dictionary == null)
					_dictionary = GetLocalResourceDictionary();

				return _dictionary;
			}
		}

		public string Translate(string value)
		{
			var translated = ResourceDictionary[value]?.ToString();
			return translated ?? value;
		}

        public string Translate(string value, params object[] args)
        {
            var translated = ResourceDictionary[value]?.ToString();
            if (string.IsNullOrEmpty(translated)) return string.Empty;
            else return string.Format(translated, args);
        }

        private static ResourceDictionary GetLocalResourceDictionary()
		{
			try
			{
				var dictionaryLocation = $"i18n\\{Thread.CurrentThread.CurrentUICulture}.xaml";
				return new ResourceDictionary { Source = new Uri(dictionaryLocation, UriKind.RelativeOrAbsolute) };
			}
			catch (IOException)
			{
				return new ResourceDictionary { Source = new Uri("i18n\\en-US.xaml", UriKind.RelativeOrAbsolute) };
			}
		}

	}
}
