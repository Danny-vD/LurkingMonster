using System;
using Enums;
using Events;
using Gameplay.WeatherEvent;
using UnityEngine;
using VDFramework;

namespace Aesthetics
{
	[RequireComponent(typeof(Light))]
	public class LightWeatherHandler : BetterMonoBehaviour
	{
		[Header("Material Settings"), SerializeField]
		private Color normal;

		[SerializeField]
		private Color rainfall;
		
		[SerializeField]
		private Color storm;

		[SerializeField]
		private Color drought;

		private Light lightSource;
		private bool isModified;

		private void Awake()
		{
			lightSource = GetComponent<Light>();
			SetLightSetting(normal);

			RandomWeatherEvent.Listeners += ReactToWeather;
		}
		
		private void ReactToWeather(RandomWeatherEvent WeatherEvent)
		{
			switch (WeatherEvent.AbstractWeatherEvent.WeatherType)
			{
				case WeatherEventType.Drought:
					SetLightSetting(drought);
					break;
				case WeatherEventType.HeavyRain:
					SetLightSetting(rainfall);
					break;
				case WeatherEventType.Earthquake:
					SetLightSetting(normal);
					break;
				case WeatherEventType.Storm:
					SetLightSetting(storm);
					break;
				case WeatherEventType.GasWinning:
					SetLightSetting(normal);
					break;
				case WeatherEventType.BuildingTunnels:
					SetLightSetting(normal);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void Update()
		{
			if (isModified && !WeatherEventManager.WeatherEventActive)
			{
				SetLightSetting(normal);
			}
		}

		private void SetLightSetting(Color color)
		{
			isModified = !color.Equals(normal);
			
			lightSource.color = color;
		}

		[ContextMenu("Normal")]
		private void SetNormal()
		{
			SetLightSetting(normal);
		}
	}
}
