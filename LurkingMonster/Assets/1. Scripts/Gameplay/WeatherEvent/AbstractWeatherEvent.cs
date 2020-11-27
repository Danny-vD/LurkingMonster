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
		private Action stopEvent;

		public void AddListener(Action<WeatherEventData> action)
		{
			activateEffects += action;
		}

		public void AddEndListener(Action action)
		{
			stopEvent += action;
		}
		
		// ReSharper disable DelegateSubtraction
		// Reason: The warning is not applicable for single subtraction from delegates
		public void RemoveListener(Action<WeatherEventData> action)
		{
			activateEffects -= action;
		}

		public void RemoveEndListener(Action action)
		{
			stopEvent -= action;
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

		private void EndEvent()
		{
			stopEvent?.Invoke();
		}
		
		private void OnDestroy()
		{
			EndEvent();
			activateEffects = null;
		}
	}
}