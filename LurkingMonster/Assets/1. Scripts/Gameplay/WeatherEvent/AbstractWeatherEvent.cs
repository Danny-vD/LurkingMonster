using System;
using Enums;
using ScriptableObjects;
using UnityEngine;
using VDFramework;

namespace Gameplay
{
	public abstract class AbstractWeatherEvent : BetterMonoBehaviour
	{
		public abstract WeatherEventType type { get; }
		public WeatherEventData WeatherEventData { get; set;}
		protected Action<WeatherEventData> action;

		public void RegisterListener(Action<WeatherEventData> action)
		{
			this.action += action;
		}
	}
}