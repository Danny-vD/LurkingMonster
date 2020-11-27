using Gameplay.Buildings;
using ScriptableObjects;
using UnityEngine;

namespace Gameplay.WeatherEvent.WeatherHandlers
{
	[RequireComponent(typeof(BuildingHealth))]
	public class BuildingWeatherEffects : AbstractWeatherHandler
	{
		private BuildingHealth health;

		private void Awake()
		{
			health = GetComponent<BuildingHealth>();
		}

		protected override void OnDrought(WeatherEventData weatherData)
		{
			DecreaseHealth(weatherData);
		}

		protected override void OnHeavyRain(WeatherEventData weatherData)
		{
			DecreaseHealth(weatherData);
		}

		protected override void OnEarthQuake(WeatherEventData weatherData)
		{
			DecreaseHealth(weatherData);
		}

		protected override void OnStorm(WeatherEventData weatherData)
		{
			DecreaseHealth(weatherData);
		}

		protected override void OnGasWinning(WeatherEventData weatherData)
		{
			DecreaseHealth(weatherData);
		}

		protected override void OnBuildingTunnels(WeatherEventData weatherData)
		{
			DecreaseHealth(weatherData);
		}

		private void DecreaseHealth(WeatherEventData data)
		{
			health.DamageBuilding(data.BuildingTime);
			health.DamageFoundation(data.FoundationTime);
			health.DamageSoil(data.SoilTime);
		}
	}
}