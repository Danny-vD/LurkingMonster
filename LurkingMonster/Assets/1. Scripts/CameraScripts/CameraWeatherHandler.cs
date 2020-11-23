using System;
using System.Collections;
using Enums;
using Events;
using Gameplay.WeatherEvent;
using ScriptableObjects;
using UnityEngine;
using VDFramework;
using VDFramework.EventSystem;

namespace CameraScripts
{
	public class CameraWeatherHandler : BetterMonoBehaviour
	{
		private void Start()
		{
			EventManager.Instance.AddListener<RandomWeatherEvent>(AddListener);
		}
		
		private void AddListener(RandomWeatherEvent randomWeatherEvent)
		{
			AbstractWeatherEvent abstractWeatherEvent = randomWeatherEvent.AbstractWeatherEvent;
			
			switch (abstractWeatherEvent.WeatherType)
			{
				case WeatherEventType.Drought:
					//abstractWeatherEvent.RegisterListener(EarthquakeEffects);
					break;
				case WeatherEventType.HeavyRain:
					//abstractWeatherEvent.RegisterListener(EarthquakeEffects);
					break;
				case WeatherEventType.Earthquake:
					abstractWeatherEvent.RegisterListener(EarthquakeEffects);
					break;
				case WeatherEventType.Storm:
					//abstractWeatherEvent.RegisterListener(EarthquakeEffects);
					break;
				case WeatherEventType.GasWinning:
					//abstractWeatherEvent.RegisterListener(EarthquakeEffects);
					break;
				case WeatherEventType.BuildingTunnels:
					//abstractWeatherEvent.RegisterListener(EarthquakeEffects);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void EarthquakeEffects(WeatherEventData data)
		{
			CameraMovement(0.05f);
		}
		
		private void CameraMovement(float movement)
		{
			StopAllCoroutines();
			StartCoroutine(Shake(movement, 5f, 13f));
		}

		private IEnumerator Shake(float movement, float time, float frequency)
		{
			while (time > 0 && WeatherEventManager.WeatherEventActive)
			{
				time -= Time.deltaTime;
				Vector3 test = new Vector3(Mathf.Sin(Time.realtimeSinceStartup * frequency) * movement, Mathf.Sin(Time.realtimeSinceStartup * frequency / 4 - 0.5f) * movement / 4, 0);
				CachedTransform.Translate(test, Space.Self);
				yield return new WaitForEndOfFrame();
			}
		}
	}
}