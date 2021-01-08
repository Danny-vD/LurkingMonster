using Enums;
using Events;
using UnityEngine;
using UnityEngine.UI;
using VDFramework;
using VDFramework.EventSystem;

namespace UI.Buttons.Settings
{
	public class ButtonChangeLanguage : BetterMonoBehaviour
	{
		[SerializeField]
		private Sprite selected;

		[SerializeField]
		private Sprite notSelected;

		[SerializeField]
		private Language languageToSet = Language.NL;

		private Image image;

		private void Awake()
		{
			GetComponent<Button>().onClick.AddListener(SetLanguage);
			image = GetComponent<Image>();

			LanguageChangedEvent.ParameterlessListeners += ChangeSprite;
		}

		private void Start()
		{
			ChangeSprite();
		}

		private void SetLanguage()
		{
			LanguageSettings.Language = languageToSet;
		}

		private void ChangeSprite()
		{
			image.sprite = LanguageSettings.Language == languageToSet ? selected : notSelected;
		}

		private void OnDestroy()
		{
			if (!EventManager.IsInitialized)
			{
				return;
			}

			LanguageChangedEvent.ParameterlessListeners -= ChangeSprite;
		}
	}
}