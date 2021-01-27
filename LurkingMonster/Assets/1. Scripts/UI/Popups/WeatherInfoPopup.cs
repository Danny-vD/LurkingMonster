using Enums;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utility;
using VDFramework;

namespace UI.Popups
{
	public class WeatherInfoPopup : BetterMonoBehaviour
	{
		[SerializeField]
		private TMP_Text typeLabel;

		[SerializeField]
		private TMP_Text infoLabel;

		[SerializeField]
		private Image icon;

		[SerializeField]
		private SerializableEnumDictionary<WeatherEventType, string> typeKeys;
		
		[SerializeField]
		private SerializableEnumDictionary<WeatherEventType, string> infoKeys;
		
		[SerializeField]
		private SerializableEnumDictionary<WeatherEventType, Sprite> weatherIcons;
		
		public void EnablePopup(WeatherEventType weatherEventType)
		{
			CachedGameObject.SetActive(true);
			
			SetUI(weatherEventType);
		}

		private void SetUI(WeatherEventType weatherEventType)
		{
			string typeKey = typeKeys[weatherEventType];
			typeLabel.text = LanguageUtil.GetJsonString(typeKey);
			
			string infoKey = infoKeys[weatherEventType];
			infoLabel.text = LanguageUtil.GetJsonString(infoKey);

			icon.sprite = weatherIcons[weatherEventType];
		}
	}
}