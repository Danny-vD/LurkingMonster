using System;
using Enums;
using VDFramework;

namespace UI.Buttons
{
	public class ButtonChangeLanguage : BetterMonoBehaviour
	{
		public void ChangeLanguageToEnglish()
		{
			LanguageSettings.Language = Language.EN;
		}
		
		public void ChangeLanguageToDutch()
		{
			LanguageSettings.Language = Language.NL;
		}
	}
}