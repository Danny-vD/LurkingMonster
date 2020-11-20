using System;
using Enums;
using Gameplay;
using ScriptableObjects;
using UnityEngine;

namespace Utility
{
	public static class InstanceCreator
	{
		public static AbstractWeatherEvent CreateInstance(WeatherEventType type, WeatherEventData weatherEventData)
		{
			GameObject gameObject = new GameObject(type.ToString());
			AbstractWeatherEvent abstractWeatherEvent;
			
			switch (type)
			{
				case WeatherEventType.Drought:
					abstractWeatherEvent = gameObject.AddComponent<Earthquake>();
					break;
				case WeatherEventType.HeavyRain:
					abstractWeatherEvent = gameObject.AddComponent<Earthquake>();
					break;
				case WeatherEventType.Earthquake:
					abstractWeatherEvent = gameObject.AddComponent<Earthquake>();
					break;
				case WeatherEventType.Storm:
					abstractWeatherEvent = gameObject.AddComponent<Earthquake>();
					break;
				case WeatherEventType.GasWinning:
					abstractWeatherEvent = gameObject.AddComponent<Earthquake>();
					break;
				case WeatherEventType.BuildingTunnels:
					abstractWeatherEvent = gameObject.AddComponent<Earthquake>();
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(type), type, null);
			}
			
			abstractWeatherEvent.WeatherEventData = weatherEventData;
			return abstractWeatherEvent;
		}
	}
}