using Singletons;
using UnityEngine;
using UnityEngine.UI;
using VDFramework;

namespace UI.Buttons.Settings
{
	public class ButtonVibrate : BetterMonoBehaviour
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
			button.onClick.AddListener(ToggleVibrate);
			onOff = GetComponent<Image>();

			ChangeSprites();
		}

		private void ToggleVibrate()
		{
			UserSettings.GameData.Vibrate ^= true;
			ChangeSprites();
		}

		private void ChangeSprites()
		{
			image.sprite = UserSettings.GameData.Vibrate ? spritesVolume[1] : spritesVolume[0];
			onOff.sprite = UserSettings.GameData.Vibrate ? onOffButtons[1] : onOffButtons[0];
		}
	}
}