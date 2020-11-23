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
		[SerializeField]
		private float Ehorizontal, eVertical, eFrequency;
		[SerializeField]
		private float sHorizontal, sVertical, sFrequency;
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
					abstractWeatherEvent.RegisterListener(StormEffects);
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
			CameraMovement(Ehorizontal, eVertical, eFrequency, data.interval);
		}

		private void StormEffects(WeatherEventData data)
		{
			CameraMovement(sHorizontal, sVertical, sFrequency, data.interval);
		}
		
		private void CameraMovement(float horizontal, float vertical, float frequency, float interval)
		{
			StopAllCoroutines();
			StartCoroutine(Shake(interval, horizontal, vertical, frequency * Mathf.PI * 2));
		}

		private IEnumerator Shake(float time, float horizontal, float vertical, float frequency)
		{
			while (time > 0 && WeatherEventManager.WeatherEventActive)
			{
				time -= Time.deltaTime;
				Vector3 test = new Vector3(Mathf.Sin(Time.realtimeSinceStartup * frequency) * horizontal, Mathf.Sin(Time.realtimeSinceStartup * frequency / 4 - 0.5f) * vertical / 8, 0);
				CachedTransform.Translate(test, Space.Self);
				yield return new WaitForEndOfFrame();
			}
		}
	}
}