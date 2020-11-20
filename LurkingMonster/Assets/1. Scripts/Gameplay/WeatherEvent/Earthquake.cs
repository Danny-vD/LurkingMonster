using System;
using Enums;
using Gameplay.WeatherEvent;
using UnityEngine;

namespace Gameplay
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

			ActivateEffects();
			timer = WeatherEventData.interval;
		}
	}
}