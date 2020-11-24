using System;
using Singletons;
using UnityEngine;
using UnityEngine.UI;
using VDFramework;

namespace UI.Buttons.Settings
{
	public class ButtonMasterVolume : BetterMonoBehaviour
	{
		[SerializeField]
		private Sprite[] spritesVolume;

		[SerializeField]
		private Sprite[] onOffButtons;
		
		private Button button;

		[SerializeField]
		private Image image;

		private Image onOff;

		private void Start()
		{
			button = GetComponent<Button>();
			button.onClick.AddListener(ChangeMasterVolume);
			onOff = GetComponent<Image>();

			ChangeSprites();
		}

		private void ChangeMasterVolume()
		{
			UserSettings.GameData.MasterVolume ^= true;
			ChangeSprites();
		}

		private void ChangeSprites()
		{
			image.sprite = UserSettings.GameData.MasterVolume ? spritesVolume[0] : spritesVolume[1];
			onOff.sprite = UserSettings.GameData.MasterVolume ? onOffButtons[1] : onOffButtons[0];
		}
	}
}