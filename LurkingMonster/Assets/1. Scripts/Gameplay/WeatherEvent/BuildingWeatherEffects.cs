using System;
using CameraScripts;
using Enums;
using Events;
using Gameplay.Buildings;
using ScriptableObjects;
using UnityEngine;
using VDFramework;
using VDFramework.EventSystem;

namespace Gameplay
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
			
			switch (abstractWeatherEvent.type)
			{
				case WeatherEventType.Drought:
					abstractWeatherEvent.RegisterListener(DroughtEffects);
					break;
				case WeatherEventType.HeavyRain:
					abstractWeatherEvent.RegisterListener(EarthquakeEffects);
					break;
				case WeatherEventType.Earthquake:
					abstractWeatherEvent.RegisterListener(EarthquakeEffects);
					break;
				case WeatherEventType.Storm:
					abstractWeatherEvent.RegisterListener(EarthquakeEffects);
					break;
				case WeatherEventType.GasWinning:
					abstractWeatherEvent.RegisterListener(EarthquakeEffects);
					break;
				case WeatherEventType.BuildingTunnels:
					abstractWeatherEvent.RegisterListener(EarthquakeEffects);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void EarthquakeEffects(WeatherEventData data)
		{
			health.DamageBuilding(data.BuildingTime);
			health.DamageFoundation(data.FoundationTime);
			health.DamageSoil(data.SoilTime);
			print("Earthquake");
		}
		
		private void DroughtEffects(WeatherEventData data)
		{
			health.DamageBuilding(data.BuildingTime);
			health.DamageFoundation(data.FoundationTime);
			health.DamageSoil(data.SoilTime);
			print("Drought");
		}
	}
}