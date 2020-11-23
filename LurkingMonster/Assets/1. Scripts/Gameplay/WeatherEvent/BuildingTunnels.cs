using Enums;
using UnityEngine;

namespace Gameplay
{
	public class BuildingTunnels : AbstractWeatherEvent
	{
		public override WeatherEventType type => WeatherEventType.BuildingTunnels;

		private float timer;

		private void Start()
		{
			timer = WeatherEventData.interval;
		}

		private void Update()
		{
			timer -= Time.deltaTime;

			if (timer > 0)
			{
				return;
			}
			
			action?.Invoke(WeatherEventData);
			timer = WeatherEventData.interval;
		}
	}
}