using Events;
using UnityEngine;
using VDFramework.EventSystem;

namespace Enums
{
    // ReSharper disable InconsistentNaming
    public enum Language : int
    {
        NL = 0,
        EN = 1,
        DE = 2,
    }

    public static class LanguageSettings
    {
		private static Language language = 0;

		public static Language Language
		{
			get => language;
			set
			{
				language = value; 
				EventManager.Instance.RaiseEvent(new LanguageChangedEvent());
			}
		}
	}
}