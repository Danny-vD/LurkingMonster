using System;
using Enums;
using UnityEngine;
using UnityEngine.UI;
using VDFramework;

namespace UI.Buttons
{
	public class ButtonChangeLanguage : BetterMonoBehaviour
	{
		[SerializeField]
		private Language languageToSet;
		
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