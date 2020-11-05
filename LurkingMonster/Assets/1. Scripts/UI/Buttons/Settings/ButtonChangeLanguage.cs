using Enums;
using UnityEngine;
using UnityEngine.UI;
using VDFramework;

namespace UI.Buttons.Settings
{
	public class ButtonChangeLanguage : BetterMonoBehaviour
	{
		[SerializeField]
		private Language languageToSet = Language.NL;
		
		private void Awake()
		{
			GetComponent<Button>().onClick.AddListener(SetLanguage);
		}

		private void SetLanguage()
		{
			LanguageSettings.Language = languageToSet;
		}
	}
}