using Enums;
using Gameplay.WeatherEvent;
using UnityEngine;

namespace Gameplay
{
	public class Storm : AbstractWeatherEvent
	{
		public override WeatherEventType WeatherType => WeatherEventType.Storm;

		private float timer;

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