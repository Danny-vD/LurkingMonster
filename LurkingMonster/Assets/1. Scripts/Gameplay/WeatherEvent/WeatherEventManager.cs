using System;
using System.Collections.Generic;
using Enums;
using Events;
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

		private TextMeshProUGUI popupText;

		private float timer;

		private WeatherEventType weatherEventType;

		private AbstractWeatherEvent abstractWeatherEvent;

		private WeatherEventData weatherEventData;

		private float weatherEventTimer;

		private void Start()
		{
			popupText = newsEventScreen.GetComponentInChildren<TextMeshProUGUI>();
			
			timer = Random.Range(minTime, maxTime);

			weatherEventActive = false;

			UserSettings.OnGameQuit += SaveData;

			if (UserSettings.SettingsExist)
			{
				LoadData();
			}
		}

		private void Update()
		{
			if (weatherEventActive)
			{
				WeatherEventTimer();
			}
			else
			{
				TimerToNextWeatherEvent();
			}
		}

		private void TimerToNextWeatherEvent()
		{
			timer -= Time.deltaTime;

			if (timer <= 0.0f && !PowerUpManager.Instance.AvoidWeatherActive)
			{
				// TODO: instead of taking a random event, have a list of unlocked events or something
				//weatherEventType     = default(WeatherEventType).GetRandomValue();
				weatherEventType     = WeatherEventType.Storm;
				weatherEventData     = GetData(weatherEventType);
				EventManager.Instance.RaiseEvent(new RandomWeatherEvent(abstractWeatherEvent));
				weatherEventActive = true;
				weatherEventTimer  = weatherEventData.Timer;
				EnableEventScreen(true);
				
				timer = Random.Range(minTime, maxTime);
			}
		}

		private void WeatherEventTimer()
		{
			weatherEventTimer -= Time.deltaTime;

			if (weatherEventTimer <= 0)
			{
				weatherEventType = (WeatherEventType) (-1);
				weatherEventActive     = false;
				EnableEventScreen(false);
				Destroy(abstractWeatherEvent.gameObject);
			}
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

			gameData.WeatherEventType = weatherEventType;
			gameData.TimerWeatherEvent      = weatherEventTimer;
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

			weatherEventTimer  = gameData.TimerWeatherEvent;
			weatherEventActive = true;

			EventManager.Instance.RaiseEvent(new RandomWeatherEvent(abstractWeatherEvent));
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