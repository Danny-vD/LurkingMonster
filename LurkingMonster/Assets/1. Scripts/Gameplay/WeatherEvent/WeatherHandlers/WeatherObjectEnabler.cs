using System.Collections.Generic;
using Enums;
using ScriptableObjects;
using UnityEngine;

namespace Gameplay.WeatherEvent.WeatherHandlers
{
	public class WeatherObjectEnabler : AbstractWeatherHandler
	{
		[SerializeField]
		private List<WeatherEventType> enableOnEvent = new List<WeatherEventType>() {WeatherEventType.HeavyRain, WeatherEventType.Storm};

		protected override void StartWeather(WeatherEventType type, WeatherEventData data)
		{
			if (enableOnEvent.Contains(type))
			{
				Enable();
			}
		}

		protected override void SetToDefault()
		{
			Disable();
		}

		private void Enable()
		{
			CachedGameObject.SetActive(true);
		}

		private void Disable()
		{
			CachedGameObject.SetActive(false);
		}
	}
}
