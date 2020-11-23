using System;
using Enums;
using ScriptableObjects;
using Singletons;
using TMPro;
using VDFramework;

namespace Gameplay.WeatherEvent
{
	public abstract class AbstractWeatherEvent : BetterMonoBehaviour
	{
		public abstract WeatherEventType WeatherType { get; }

		public WeatherEventData WeatherEventData { get; set; }

		private Action<WeatherEventData> activateEffects;

		public void RegisterListener(Action<WeatherEventData> action)
		{
			activateEffects += action;
		}

		public void RegisterListener(Action action)
		{
			activateEffects += delegate(WeatherEventData data) { action(); };
		}

		protected bool ActivateEffects(bool ignorePause = false)
		{
			if (!ignorePause && TimeManager.Instance.IsPaused())
			{
				return false;
			}

			activateEffects?.Invoke(WeatherEventData);
			return true;
		}

		private void OnDestroy()
		{
			activateEffects = null;
		}
	}
}