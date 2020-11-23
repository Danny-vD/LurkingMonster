using System;
using Enums;
using Events;
using Gameplay.WeatherEvent;
using VDFramework;

namespace Aesthetics
{
	public class RainEnabler : BetterMonoBehaviour
	{
		private bool active;
		
		private void Start()
		{
			RandomWeatherEvent.Listeners += AddListener;
			Disable();
		}

		private void AddListener(RandomWeatherEvent randomWeatherEvent)
		{
			switch (randomWeatherEvent.AbstractWeatherEvent.WeatherType)
			{
				case WeatherEventType.Drought:
					Disable();
					break;
				case WeatherEventType.HeavyRain:
					Enable();
					break;
				case WeatherEventType.Earthquake:
					Disable();
					break;
				case WeatherEventType.Storm:
					Enable();
					break;
				case WeatherEventType.GasWinning:
					Disable();
					break;
				case WeatherEventType.BuildingTunnels:
					Disable();
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
		
		private void Update()
		{
			if (!WeatherEventManager.WeatherEventActive && active)
			{
				Disable();
			}
		}

		private void Enable()
		{
			active = true;
			CachedGameObject.SetActive(true);
		}

		private void Disable()
		{
			active = false;
			CachedGameObject.SetActive(false);
		}
	}
}
