using System.Collections.Generic;
using Enums;
using Events;
using ScriptableObjects;
using Structs;
using UnityEngine;
using VDFramework;
using VDFramework.EventSystem;
using VDFramework.Extensions;
using VDFramework.Utility;
using Random = UnityEngine.Random;

namespace Gameplay
{
	public class WeatherEventManager : BetterMonoBehaviour
	{
		private float timer;
		private float timeToEvent;

		[SerializeField]
		private List<EventDataPerEventType> eventDataPerEventType;

		[SerializeField]
		private float minTime = 900.0f;

		[SerializeField]
		private float maxTime = 3000.0f;
		
		private void Start()
		{
			timeToEvent = Random.Range(minTime, maxTime);
			timer       = timeToEvent;
		}

		private void Update()
		{
			timer -= Time.deltaTime;

			if (timer <= 0.0f)
			{
				WeatherEventData weatherEventData = GetData(RandomWeatherEventType.Drought.GetRandomValue());
				print(weatherEventData.name);
				EventManager.Instance.RaiseEvent(new RandomWeatherEvent(weatherEventData));
				
				timeToEvent = Random.Range(minTime, maxTime);
				timer       = timeToEvent;
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
		
		[ContextMenu("Populate")]
		private void PopulateList()
		{
			EnumDictionaryUtil.PopulateEnumDictionary<EventDataPerEventType, RandomWeatherEventType, WeatherEventData>(eventDataPerEventType);
		}
	}
}