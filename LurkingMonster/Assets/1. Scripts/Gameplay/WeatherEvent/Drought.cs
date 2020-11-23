using System;
using Enums;
using Gameplay.WeatherEvent;
using UnityEngine;

namespace Gameplay
{
	public class Drought : AbstractWeatherEvent
	{
		public override WeatherEventType WeatherType => WeatherEventType.Drought;

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