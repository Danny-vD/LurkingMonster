using System;
using Enums;
using Events;
using Gameplay.Buildings;
using ScriptableObjects;
using UnityEngine;
using VDFramework;
using VDFramework.EventSystem;

namespace Gameplay.WeatherEvent
{
	[RequireComponent(typeof(BuildingHealth))]
	public class BuildingWeatherEffects : BetterMonoBehaviour
	{
		private BuildingHealth health;

		private void Start()
		{
			EventManager.Instance.AddListener<RandomWeatherEvent>(AddListener);
			health = GetComponent<BuildingHealth>();
		}

		private void AddListener(RandomWeatherEvent randomWeatherEvent)
		{
			AbstractWeatherEvent abstractWeatherEvent = randomWeatherEvent.AbstractWeatherEvent;
			
			switch (abstractWeatherEvent.WeatherType)
			{
				case WeatherEventType.Drought:
					abstractWeatherEvent.RegisterListener(DroughtEffects);
					break;
				case WeatherEventType.HeavyRain:
					abstractWeatherEvent.RegisterListener(HeavyRainEffects);
					break;
				case WeatherEventType.Earthquake:
					abstractWeatherEvent.RegisterListener(EarthquakeEffects);
					break;
				case WeatherEventType.Storm:
					abstractWeatherEvent.RegisterListener(StormEffects);
					break;
				case WeatherEventType.GasWinning:
					abstractWeatherEvent.RegisterListener(GasWinningEffects);
					break;
				case WeatherEventType.BuildingTunnels:
					abstractWeatherEvent.RegisterListener(BuildingTunnelsEffects);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void DroughtEffects(WeatherEventData data)
		{
			DecreaseHealth(data);
		}

		private void HeavyRainEffects(WeatherEventData data)
		{
			DecreaseHealth(data);
		}
		
		private void EarthquakeEffects(WeatherEventData data)
		{
			DecreaseHealth(data);
		}

		private void StormEffects(WeatherEventData data)
		{
			DecreaseHealth(data);
		}
		
		private void GasWinningEffects(WeatherEventData data)
		{
			DecreaseHealth(data);
		}
		
		private void BuildingTunnelsEffects(WeatherEventData data)
		{
			DecreaseHealth(data);
		}

		private void DecreaseHealth(WeatherEventData data)
		{
			health.DamageBuilding(data.BuildingTime);
			health.DamageFoundation(data.FoundationTime);
			health.DamageSoil(data.SoilTime);
		}
	}
}