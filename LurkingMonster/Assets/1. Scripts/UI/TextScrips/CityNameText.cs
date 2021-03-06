﻿using Events;
using Singletons;
using TMPro;
using VDFramework;
using VDFramework.EventSystem;

namespace UI.TextScrips
{
	public class CityNameText : BetterMonoBehaviour
	{
		private TMP_Text text;
		
		private void Awake()
		{
			text = GetComponent<TMP_Text>();
			EventManager.Instance.AddListener<InputChangedEvent>(ChangeCityName);
			ChangeCityName();
		}

		private void ChangeCityName()
		{
			if (string.IsNullOrEmpty(UserSettings.GameData.CityName))
			{
				text.text = "Monstrocity";
				return;
			}
			
			text.text = UserSettings.GameData.CityName;
		}

		private void OnDestroy()
		{
			if (!EventManager.IsInitialized)
			{
				return;
			}
			
			EventManager.Instance.RemoveListener<InputChangedEvent>(ChangeCityName);
		}
	}
}