using Enums;
using UnityEngine;

namespace Gameplay.WeatherEvent
{
	public class Earthquake : AbstractWeatherEvent
	{
		public override WeatherEventType WeatherType => WeatherEventType.Earthquake;

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

			if (ActivateEffects())
			{
				timer = WeatherEventData.interval;
			}
		}
	}
}