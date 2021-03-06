﻿using Enums;
using Gameplay.WeatherEvent;
using UnityEngine;

namespace Gameplay
{
	public class BuildingTunnels : AbstractWeatherEvent
	{
		public override WeatherEventType WeatherType => WeatherEventType.BuildingTunnels;

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