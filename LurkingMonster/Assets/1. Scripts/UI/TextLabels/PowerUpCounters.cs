using System;
using Enums;
using Events;
using Singletons;
using TMPro;
using UnityEngine;
using VDFramework;
using VDFramework.EventSystem;

namespace UI.TextLabels
{
	public class PowerUpCounters : BetterMonoBehaviour
	{
		[SerializeField]
		private PowerUpType powerUpType;

		private TextMeshProUGUI counterText;

		private void Start()
		{
			counterText = GetComponentInChildren<TextMeshProUGUI>();
			SetCountersText();
			EventManager.Instance.AddListener<PowerUpIncreaseEvent>(SetCountersText);
			EventManager.Instance.AddListener<PowerUpActivateEvent>(SetCountersText);
		}

		private void SetCountersText()
		{
			switch (powerUpType)
			{
				case PowerUpType.AvoidMonster:
					counterText.text = PowerUpManager.Instance.AvoidMonsters.ToString();
					break;
				case PowerUpType.FixProblems:
					counterText.text = PowerUpManager.Instance.FixProblems.ToString();
					break;
				case PowerUpType.AvoidWeatherEvent:
					counterText.text = PowerUpManager.Instance.AvoidWeather.ToString();
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}