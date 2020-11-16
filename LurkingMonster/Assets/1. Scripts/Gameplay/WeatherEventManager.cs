using System.Collections.Generic;
using Enums;
using Events;
using ScriptableObjects;
using Singletons;
using Structs;
using UnityEngine;
using Utility;
using VDFramework;
using VDFramework.EventSystem;
using VDFramework.Extensions;
using VDFramework.Singleton;
using VDFramework.Utility;
using Random = UnityEngine.Random;

namespace Gameplay
{
	public class WeatherEventManager : BetterMonoBehaviour
	{
		private float timer;
		private float timeToEvent;

		[SerializeField]
		private List<EventDataPerEventType> eventDataPerEventType = null;

		private RandomWeatherEventType randomWeatherEventType;

		private bool weatherEventActive;

		private WeatherEventData weatherEventData;

		private float weatherEventTimer;

		[SerializeField]
		private float minTime = 900.0f;

		[SerializeField]
		private float maxTime = 3000.0f;

		private void Start()
		{
			timeToEvent        = Random.Range(minTime, maxTime);
			timer              = timeToEvent;
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

			if (timer <= 0.0f)
			{
				randomWeatherEventType = RandomWeatherEventType.Drought.GetRandomValue();
				weatherEventData       = GetData(randomWeatherEventType);
				EventManager.Instance.RaiseEvent(new RandomWeatherEvent(this));
				weatherEventActive = true;
				weatherEventTimer  = weatherEventData.Timer;

				print(randomWeatherEventType);

				timeToEvent = Random.Range(minTime, maxTime);
				timer       = timeToEvent;
			}
		}

		private void WeatherEventTimer()
		{
			weatherEventTimer -= Time.deltaTime;

			if (weatherEventTimer <= 0)
			{
				randomWeatherEventType = (RandomWeatherEventType) (-1);
				weatherEventActive     = false;
			}
		}

		private WeatherEventData GetData(RandomWeatherEventType randomWeatherEventType)
		{
			for (int i = 0; i < eventDataPerEventType.Count; i++)
			{
				if (eventDataPerEventType[i].Key == randomWeatherEventType)
				{
					return eventDataPerEventType[i].Value;
				}
			}

			Debug.LogError("No Data found for " + randomWeatherEventType);
			return null;
		}

		private void SaveData()
		{
			GameData gameData = UserSettings.GameData;

			if (!weatherEventActive)
			{
				return;
			}

			gameData.RandomWeatherEventType = randomWeatherEventType;
			gameData.TimerWeatherEvent      = weatherEventTimer;
		}

		private void LoadData()
		{
			GameData gameData = UserSettings.GameData;

			randomWeatherEventType = gameData.RandomWeatherEventType;

			if (randomWeatherEventType == (RandomWeatherEventType) (-1))
			{
				return;
			}
			
			weatherEventData = GetData(randomWeatherEventType);
			
			weatherEventTimer  = gameData.TimerWeatherEvent;
			weatherEventActive = true;

			EventManager.Instance.RaiseEvent(new RandomWeatherEvent(this));
		}

		[ContextMenu("Populate")]
		private void PopulateList()
		{
			EnumDictionaryUtil.PopulateEnumDictionary<EventDataPerEventType, RandomWeatherEventType, WeatherEventData>(
				eventDataPerEventType);
		}

		public bool WeatherEventActive => weatherEventActive;

		public WeatherEventData WeatherEventData => weatherEventData;
	}
}