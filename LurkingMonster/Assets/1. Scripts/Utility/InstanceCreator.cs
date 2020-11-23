using System;
using _1._Scripts.Gameplay.WeatherEvent;
using Enums;
using Gameplay;
using Gameplay.WeatherEvent;
using ScriptableObjects;
using UnityEngine;

namespace Utility
{
	public static class WeatherEventInstanceCreator
	{
		public static AbstractWeatherEvent CreateInstance(WeatherEventType eventType, WeatherEventData weatherEventData)
		{
			GameObject gameObject = new GameObject(eventType.ToString());

			AbstractWeatherEvent abstractWeatherEvent = AddWeatherComponent(eventType, gameObject);
			
			abstractWeatherEvent.WeatherEventData = weatherEventData;
			return abstractWeatherEvent;
		}

		private static AbstractWeatherEvent AddWeatherComponent(WeatherEventType type, GameObject gameObject)
		{
			AbstractWeatherEvent abstractWeatherEvent;

			switch (type)
			{
				case WeatherEventType.Drought:
					abstractWeatherEvent = gameObject.AddComponent<Drought>();
					break;
				case WeatherEventType.HeavyRain:
					abstractWeatherEvent = gameObject.AddComponent<HeavyRain>();
					break;
				case WeatherEventType.Earthquake:
					abstractWeatherEvent = gameObject.AddComponent<Earthquake>();
					break;
				case WeatherEventType.Storm:
					abstractWeatherEvent = gameObject.AddComponent<Storm>();
					break;
				case WeatherEventType.GasWinning:
					abstractWeatherEvent = gameObject.AddComponent<GasWinning>();
					break;
				case WeatherEventType.BuildingTunnels:
					abstractWeatherEvent = gameObject.AddComponent<BuildingTunnels>();
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(type), type, null);
			}

			return abstractWeatherEvent;
		}
	}
}