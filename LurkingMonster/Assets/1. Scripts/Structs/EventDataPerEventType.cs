using System;
using Enums;
using ScriptableObjects;
using UnityEngine;
using VDFramework.Interfaces;

namespace Structs
{
	[Serializable]
	public struct EventDataPerEventType : IKeyValuePair<RandomWeatherEventType, WeatherEventData>
	{
		[SerializeField]
		private RandomWeatherEventType key;
		
		[SerializeField]
		private WeatherEventData value;

		public bool Equals(IKeyValuePair<RandomWeatherEventType, WeatherEventData> other)
		{
			return Key.Equals(other.Key);
		}

		public RandomWeatherEventType Key
		{
			get => key;
			set => key = value;
		}

		public WeatherEventData Value
		{
			get => value;
			set => value = value;
		}
	}
}