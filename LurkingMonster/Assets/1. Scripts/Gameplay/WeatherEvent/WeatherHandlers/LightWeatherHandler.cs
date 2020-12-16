using ScriptableObjects;
using UnityEngine;

namespace Gameplay.WeatherEvent.WeatherHandlers
{
	[RequireComponent(typeof(Light))]
	public class LightWeatherHandler : AbstractWeatherHandler
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

		protected override bool AddWeatherListener => false;

		private void Awake()
		{
			lightSource = GetComponent<Light>();
		}

		protected override void OnDroughtStart(WeatherEventData weatherData)
		{
			SetLightSetting(drought);
		}

		protected override void OnHeavyRainStart(WeatherEventData weatherData)
		{
			SetLightSetting(rainfall);
		}

		protected override void OnStormStart(WeatherEventData weatherData)
		{
			SetLightSetting(storm);
		}

		private void SetLightSetting(Color color)
		{
			lightSource.color = color;
		}

		protected override void SetToDefault()
		{
			SetLightSetting(normal);
		}
	}
}
