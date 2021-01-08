using System;
using Events;
using Singletons;
using TMPro;
using UnityEngine;
using VDFramework;
using VDFramework.EventSystem;

namespace UI.TextScrips
{
	public class CityNameText : BetterMonoBehaviour
	{
		private TextMeshPro text;
		
		private void Awake()
		{
			text = GetComponent<TextMeshPro>();
			EventManager.Instance.AddListener<InputChangedEvent>(ChangeCityName);
		}

		private void ChangeCityName()
		{
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