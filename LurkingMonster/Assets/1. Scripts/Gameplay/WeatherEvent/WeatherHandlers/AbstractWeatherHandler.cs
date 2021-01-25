using System;
using Enums;
using Events.WeatherEvents;
using ScriptableObjects;
using VDFramework;
using VDFramework.EventSystem;

namespace Gameplay.WeatherEvent.WeatherHandlers
{
	/// <summary>
	/// A class that has functions for The Start, the intervals and the End for each weather event
	/// </summary>
	public abstract class AbstractWeatherHandler : BetterMonoBehaviour
	{
		/// <summary>
		/// Should this class add listeners to listen to the callbacks per interval?
		/// </summary>
		protected abstract bool AddWeatherListener { get; }

		private AbstractWeatherEvent abstractWeatherEvent;

		protected virtual void Start()
		{
			StartWeatherEvent.Listeners += WeatherEventStarted;
			SetToDefault();
		}

		private void WeatherEventStarted(StartWeatherEvent weatherEvent)
		{
			abstractWeatherEvent = weatherEvent.AbstractWeatherEvent;

			StartWeather(abstractWeatherEvent.WeatherType, abstractWeatherEvent.WeatherEventData);

			if (AddWeatherListener)
			{
				AddRemoveListener(abstractWeatherEvent.AddListener);
			}

			abstractWeatherEvent.AddEndListener(ResetState);
		}

		protected virtual void StartWeather(WeatherEventType type, WeatherEventData data)
		{
			switch (type)
			{
				case WeatherEventType.Drought:
					OnDroughtStart(data);
					break;
				case WeatherEventType.HeavyRain:
					OnHeavyRainStart(data);
					break;
				case WeatherEventType.Earthquake:
					OnEarthQuakeStart(data);
					break;
				case WeatherEventType.Storm:
					OnStormStart(data);
					break;
				case WeatherEventType.GasWinning:
					OnGasWinningStart(data);
					break;
				case WeatherEventType.BuildingTunnels:
					OnBuildingTunnelsStart(data);
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(type), type, null);
			}
		}

		// Start Weather
		protected virtual void OnDroughtStart(WeatherEventData weatherData)
		{
		}

		protected virtual void OnHeavyRainStart(WeatherEventData weatherData)
		{
		}

		protected virtual void OnEarthQuakeStart(WeatherEventData weatherData)
		{
		}

		protected virtual void OnStormStart(WeatherEventData weatherData)
		{
		}

		protected virtual void OnGasWinningStart(WeatherEventData weatherData)
		{
		}

		protected virtual void OnBuildingTunnelsStart(WeatherEventData weatherData)
		{
		}

		// Weather Listeners
		protected virtual void OnDrought(WeatherEventData weatherData)
		{
		}

		protected virtual void OnHeavyRain(WeatherEventData weatherData)
		{
		}

		protected virtual void OnEarthQuake(WeatherEventData weatherData)
		{
		}

		protected virtual void OnStorm(WeatherEventData weatherData)
		{
		}

		protected virtual void OnGasWinning(WeatherEventData weatherData)
		{
		}

		protected virtual void OnBuildingTunnels(WeatherEventData weatherData)
		{
		}

		protected virtual void SetToDefault()
		{
		}

		// Weather ends
		private void ResetState()
		{
			abstractWeatherEvent = null;
			SetToDefault();
		}

		/// <summary>
		/// An abstraction to add or remove the listeners convienently, without having to duplicate the switch
		/// </summary>
		private void AddRemoveListener(Action<Action<WeatherEventData>> modifyListener)
		{
			switch (abstractWeatherEvent.WeatherType)
			{
				case WeatherEventType.Drought:
					modifyListener(OnDrought);
					break;
				case WeatherEventType.HeavyRain:
					modifyListener(OnHeavyRain);
					break;
				case WeatherEventType.Earthquake:
					modifyListener(OnEarthQuake);
					break;
				case WeatherEventType.Storm:
					modifyListener(OnStorm);
					break;
				case WeatherEventType.GasWinning:
					modifyListener(OnGasWinning);
					break;
				case WeatherEventType.BuildingTunnels:
					modifyListener(OnBuildingTunnels);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void RemoveListeners()
		{
			if (EventManager.IsInitialized)
			{
				StartWeatherEvent.Listeners -= WeatherEventStarted;
			}

			if (abstractWeatherEvent)
			{
				if (AddWeatherListener)
				{
					AddRemoveListener(abstractWeatherEvent.RemoveListener);
				}

				abstractWeatherEvent.RemoveEndListener(ResetState);
			}
		}

		protected virtual void OnDestroy()
		{
			RemoveListeners();
		}
	}
}