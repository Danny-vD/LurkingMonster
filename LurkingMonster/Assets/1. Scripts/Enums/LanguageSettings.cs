using System.Linq;
using Events;
using UnityEngine;
using VDFramework.EventSystem;
using VDFramework.Extensions;

namespace Enums
{
	// ReSharper disable InconsistentNaming
	public enum Language
	{
		NL = SystemLanguage.Dutch,
		EN = SystemLanguage.English,
	}

	public static class LanguageSettings
	{
		private const Language DefaultLanguage = Language.EN;

		static LanguageSettings()
		{
			SystemLanguage = Application.systemLanguage;
		}

		private static Language language;

		public static Language Language
		{
			get => language;
			set
			{
				language = IsValidLanguage(value) ? value : DefaultLanguage;

				EventManager.Instance.RaiseEvent(new LanguageChangedEvent());
			}
		}

		/// <summary>
		/// Returns the current language of the application as a SystemLanguage
		/// </summary>
		public static SystemLanguage SystemLanguage
		{
			get => (SystemLanguage) Language;
			set => Language = (Language) value;
		}

		private static bool IsValidLanguage(Language checkLanguage)
		{
			return default(Language).GetNames().Contains(checkLanguage.ToString());
		}
	}
}