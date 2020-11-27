using Audio;
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
			UserSettings.GameData.MasterMute ^= true;
			AudioParameterManager.SetMasterMute(UserSettings.GameData.MasterMute);
			ChangeSprites();
		}

		private void ChangeSprites()
		{
			image.sprite = UserSettings.GameData.MasterMute ? spritesVolume[0] : spritesVolume[1];
			onOff.sprite = UserSettings.GameData.MasterMute ? onOffButtons[1] : onOffButtons[0];
		}
	}
}