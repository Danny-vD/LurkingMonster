using System;
using Enums;
using ScriptableObjects;
using Singletons;
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

		protected void ActivateEffects(bool ignorePause = false)
		{
			if (!ignorePause && TimeManager.Instance.IsPaused())
			{
				return;
			}

			activateEffects?.Invoke(WeatherEventData);
		}

		private void OnDestroy()
		{
			activateEffects = null;
		}
	}
}