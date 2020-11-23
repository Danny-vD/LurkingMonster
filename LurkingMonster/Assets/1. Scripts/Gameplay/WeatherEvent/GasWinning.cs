using Enums;
using Gameplay.WeatherEvent;
using Grid.Tiles;
using UnityEngine;

namespace _1._Scripts.Gameplay.WeatherEvent
{
	public class GasWinning : AbstractWeatherEvent
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