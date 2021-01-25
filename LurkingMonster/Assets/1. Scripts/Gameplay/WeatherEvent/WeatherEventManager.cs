using System;
using System.Collections.Generic;
using System.Linq;
using _1._Scripts.Gameplay.WeatherEvent;
using Enums;
using Events;
using Events.WeatherEvents;
using IO;
using ScriptableObjects;
using Singletons;
using Structs;
using TMPro;
using UnityEngine;
using Utility;
using VDFramework;
using VDFramework.EventSystem;
using VDFramework.Extensions;
using VDFramework.Utility;
using Random = UnityEngine.Random;

namespace Gameplay.WeatherEvent
{
	public class WeatherEventManager : BetterMonoBehaviour
	{
		private static bool weatherEventActive;

		[SerializeField]
		private string reporterName = "Lif van der Zandt";

		[SerializeField]
		private List<EventDataPerEventType> eventDataPerEventType = null;

		[SerializeField]
		private GameObject newsEventScreen;

		[SerializeField]
		private float minTime = 900.0f;

		[SerializeField]
		private float maxTime = 3000.0f;

		[SerializeField]
		private WeatherEventTimer weatherEventTimer;

		private TextMeshProUGUI popupText;

		private float timerTillNextEvent;

		private WeatherEventType weatherEventType;

		private AbstractWeatherEvent abstractWeatherEvent;

		private WeatherEventData weatherEventData;

		private readonly WeatherEventType[] availableWeather = {WeatherEventType.Earthquake, WeatherEventType.Storm, WeatherEventType.HeavyRain};

		private void Start()
		{
			popupText = newsEventScreen.GetComponentInChildren<TextMeshProUGUI>();

			timerTillNextEvent = Random.Range(minTime, maxTime);

			weatherEventActive = false;

			UserSettings.OnGameQuit += SaveData;
			EventManager.Instance.AddListener<PowerUpActivateEvent>(CheckPowerUp);

			if (UserSettings.SettingsExist)
			{
				LoadData();
			}
		}

		private void CheckPowerUp(PowerUpActivateEvent @event)
		{
			if (@event.Type != PowerUpType.AvoidWeatherEvent)
			{
				return;
			}

			EndWeatherEvent();
			weatherEventTimer.DisableTimer();
		}

		private void Update()
		{
			if (!weatherEventActive)
			{
				TimerToNextWeatherEvent();
			}
		}

		private void OnDestroy()
		{
			if (!EventManager.IsInitialized)
			{
				return;
			}
			
			EventManager.Instance.RemoveListener<PowerUpActivateEvent>(CheckPowerUp);
		}

		private void TimerToNextWeatherEvent()
		{
			timerTillNextEvent -= Time.unscaledDeltaTime;

			if (timerTillNextEvent <= 0.0f && !PowerUpManager.Instance.AvoidWeatherActive)
			{
				if (TimeManager.Instance.IsPaused())
				{
					return;
				}

				weatherEventType = GetRandomWeather();
				weatherEventData = GetData(weatherEventType);

				EventManager.Instance.RaiseEvent(new StartWeatherEvent(abstractWeatherEvent));
				weatherEventTimer.StartTimer(weatherEventData.Timer, EndWeatherEvent, weatherEventType);

				EnableEventScreen(true);
				weatherEventActive = true;

				timerTillNextEvent = Random.Range(minTime, maxTime);
			}
		}

		private void EndWeatherEvent()
		{
			weatherEventType   = (WeatherEventType) (-1);
			weatherEventActive = false;
			EnableEventScreen(false);
			Destroy(abstractWeatherEvent.gameObject);
		}


		private WeatherEventData GetData(WeatherEventType weatherEventType)
		{
			for (int i = 0; i < eventDataPerEventType.Count; i++)
			{
				if (eventDataPerEventType[i].Key == weatherEventType)
				{
					WeatherEventData data = eventDataPerEventType[i].Value;
					abstractWeatherEvent = WeatherEventInstanceCreator.CreateInstance(weatherEventType, data);
					return data;
				}
			}

			throw new NullReferenceException("No Data found for " + weatherEventType);
		}

		private void EnableEventScreen(bool active)
		{
			newsEventScreen.SetActive(active);

			if (active)
			{
				popupText.text = LanguageUtil.GetJsonString(weatherEventData.JsonContentKey, reporterName);
			}
		}

		private void SaveData()
		{
			GameData gameData = UserSettings.GameData;

			if (!weatherEventActive)
			{
				return;
			}

			gameData.WeatherEventType  = weatherEventType;
			gameData.TimerWeatherEvent = weatherEventTimer.Timer;
		}

		private void LoadData()
		{
			GameData gameData = UserSettings.GameData;

			weatherEventType = gameData.WeatherEventType;

			if (weatherEventType == (WeatherEventType) (-1))
			{
				return;
			}

			weatherEventData = GetData(weatherEventType);
			weatherEventTimer.StartTimer(weatherEventData.Timer, EndWeatherEvent, weatherEventType);
			weatherEventTimer.Timer = gameData.TimerWeatherEvent;
			weatherEventActive      = true;

			EventManager.Instance.RaiseEvent(new StartWeatherEvent(abstractWeatherEvent, false));
		}

		private WeatherEventType GetRandomWeather()
		{
			WeatherEventType oldType = weatherEventType;
			IEnumerable<WeatherEventType> weatherEventTypes = availableWeather.Where(element => element != oldType);

			return weatherEventTypes.GetRandomItem();
		}

		[ContextMenu("Populate")]
		private void PopulateList()
		{
			EnumDictionaryUtil.PopulateEnumDictionary<EventDataPerEventType, WeatherEventType, WeatherEventData>(
				eventDataPerEventType);
		}

		public static bool WeatherEventActive => weatherEventActive;
	}
}