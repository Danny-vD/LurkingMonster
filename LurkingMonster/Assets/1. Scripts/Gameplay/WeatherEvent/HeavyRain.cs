using Enums;
using Gameplay.WeatherEvent;
using UnityEngine;

namespace Gameplay
{
	public class HeavyRain : AbstractWeatherEvent
	{
		public override WeatherEventType WeatherType => WeatherEventType.HeavyRain;

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
			
			ActivateEffects();
			timer = WeatherEventData.interval;
		}
	}
}